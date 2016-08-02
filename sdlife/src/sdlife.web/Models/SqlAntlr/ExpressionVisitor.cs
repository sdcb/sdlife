using Antlr4.Runtime.Misc;
using sdlife.web.Models.SqlAntlr.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static sdlife.web.Models.SqlAntlr.Details.SqlParser;

namespace sdlife.web.Models.SqlAntlr
{
    public class ExpressionVisitor : SqlBaseVisitor<SqlValue>
    {
        public override SqlValue VisitExpressionParenthesis([NotNull] ExpressionParenthesisContext context)
        {
            return Visit(context.GetChild<ExpressionContext>(0));
        }

        public override SqlValue VisitNumber([NotNull] NumberContext context)
        {
            return SqlValue.ParseNumber(context.GetText());
        }

        public override SqlValue VisitFunction([NotNull] FunctionContext context)
        {
            var func = context.GetChild(0).GetText();
            var v = (double)Visit(context.GetChild<ExpressionContext>(0));

            switch (func)
            {
                case "abs":
                    return Math.Abs(v);
                default:
                    throw new ArgumentOutOfRangeException(nameof(func));
            }
        }

        public override SqlValue VisitBinaryFunction([NotNull] BinaryFunctionContext context)
        {
            var func = context.GetChild(0).GetText();
            var v1 = (double)Visit(context.GetChild<ExpressionContext>(0));
            var v2 = (double)Visit(context.GetChild<ExpressionContext>(1));

            switch (func)
            {
                case "pow":
                    return Math.Pow(v1, v2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(func));
            }
        }

        public override SqlValue VisitString([NotNull] StringContext context)
        {
            var text = context.GetText();
            return text.Substring(1, text.Length - 2);
        }

        public override SqlValue VisitDate([NotNull] DateContext context)
        {
            return SqlValue.ParseDate(context.GetText());
        }

        public override SqlValue VisitBinary([NotNull] BinaryContext context)
        {
            var op = context.GetChild(1).GetText();
            var l = Visit(context.GetChild(0));
            var r = Visit(context.GetChild(2));

            switch (op)
            {
                case "+":
                    return l + r;
                case "-":
                    return l - r;
                case "*":
                    return (double)l * (double)r;
                case "/":
                    return (double)l / (double)r;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op));
            }
        }
    }
}
