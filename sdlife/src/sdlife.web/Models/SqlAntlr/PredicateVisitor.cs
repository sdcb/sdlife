using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using sdlife.web.Models.SqlAntlr.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using static sdlife.web.Models.SqlAntlr.Details.SqlParser;

namespace sdlife.web.Models.SqlAntlr
{
    public class PredicateVisitor<T> : SqlBaseVisitor<Expression>
    {
        ParameterExpression Pe = Expression.Parameter(typeof(T), "x");

        public override Expression VisitSingleOperator([NotNull] SingleOperatorContext context)
        {
            var syntax = context.GetChild(0).GetText();
            var opText = context.GetChild(1).GetText();
            var val = EvalExpression(context.GetChild(2));

            var left = Expression.PropertyOrField(Pe, syntax);
            var right = Expression.Constant(Convert.ChangeType(val.Value(), left.Type));

            Expression op;
            switch (opText)
            {
                case "=":
                    op = Expression.Equal(left, right);
                    break;
                case "!=":
                    op = Expression.NotEqual(left, right);
                    break;
                case ">":
                    op = Expression.GreaterThan(left, right);
                    break;
                case "<":
                    op = Expression.LessThan(left, right);
                    break;
                case ">=":
                    op = Expression.GreaterThanOrEqual(left, right);
                    break;
                case "<=":
                    op = Expression.LessThanOrEqual(left, right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(opText));
            }
            return op;
        }

        public override Expression VisitAndOr([NotNull] AndOrContext context)
        {
            var left = Visit(context.GetChild<PredicateContext>(0));
            var right = Visit(context.GetChild<PredicateContext>(1));
            var opText = context.GetChild(1).GetText();

            switch (opText.ToUpperInvariant())
            {
                case "AND":
                    return Expression.AndAlso(left, right);
                case "OR":
                    return Expression.OrElse(left, right);
                default:
                    throw new ArgumentOutOfRangeException(nameof(opText));
            }
        }

        public override Expression VisitBetween([NotNull] BetweenContext context)
        {
            var syntax = context.GetChild(0).GetText();
            var exp1 = EvalExpression(context.GetChild<ExpressionContext>(0)).Value();
            var exp2 = EvalExpression(context.GetChild<ExpressionContext>(1)).Value();

            var prop = Expression.PropertyOrField(Pe, syntax);
            var left = Expression.Constant(Convert.ChangeType(exp1, prop.Type));
            var right = Expression.Constant(Convert.ChangeType(exp2, prop.Type));

            return Expression.AndAlso(
                Expression.LessThanOrEqual(left, prop),
                Expression.LessThan(prop, right)
                );
        }

        public override Expression VisitContains([NotNull] ContainsContext context)
        {
            var syntax = context.GetChild(0).GetText();
            var values = context.children
                .OfType<ExpressionContext>()
                .Select(x => EvalExpression(x).Value())
                .ToList();

            var left = Expression.Constant(values);
            var right = Expression.PropertyOrField(Pe, syntax);
            var op = Expression.Call(
                left,
                typeof(List<object>).GetMethod("Contains", new[] { typeof(object) }),
                right);
            return op;
        }

        public override Expression VisitParenthesis([NotNull] ParenthesisContext context)
        {
            return Visit(context.GetChild<PredicateContext>(0));
        }

        private SqlValue EvalExpression(IParseTree expression)
        {
            return new ExpressionVisitor().Visit(expression);
        }

        public IQueryable<T> DoQuery(IQueryable<T> dataIn, IParseTree tree)
        {
            var predicate = Visit(tree);
            var where = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { dataIn.ElementType },
                dataIn.Expression,
                Expression.Lambda<Func<T, bool>>(predicate, new ParameterExpression[] { Pe }));
            return dataIn.Provider.CreateQuery<T>(where);
        }
    }
}
