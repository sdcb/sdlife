using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using sdlife.web.Models.SqlAntlr.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sdlife.web.Models.SqlAntlr
{
    public static class SqlAntlrHelper
    {
        public static Result<IQueryable<T>> Filter<T>(this IQueryable<T> dataIn, string sqlWherePredicate)
        {
            try
            {
                var inputStream = new AntlrInputStream(sqlWherePredicate);
                var lexer = new SqlLexer(inputStream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new SqlParser(tokenStream);

                var visitor = new PredicateVisitor<T>();
                return Result.Ok(visitor.DoQuery(dataIn, parser.run()));
            }
            catch (Exception e)
            {
                return Result.Fail<IQueryable<T>>(e.Message);
            }
        }
    }
}
