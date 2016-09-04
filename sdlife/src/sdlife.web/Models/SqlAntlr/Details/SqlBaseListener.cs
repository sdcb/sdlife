//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3.1-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Sql.g4 by ANTLR 4.5.3.1-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace sdlife.web.Models.SqlAntlr.Details {

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ISqlListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3.1-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class SqlBaseListener : ISqlListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>Parenthesis</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParenthesis([NotNull] SqlParser.ParenthesisContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Parenthesis</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParenthesis([NotNull] SqlParser.ParenthesisContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNot([NotNull] SqlParser.NotContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNot([NotNull] SqlParser.NotContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>SingleOperator</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSingleOperator([NotNull] SqlParser.SingleOperatorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>SingleOperator</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSingleOperator([NotNull] SqlParser.SingleOperatorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Between</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBetween([NotNull] SqlParser.BetweenContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Between</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBetween([NotNull] SqlParser.BetweenContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Contains</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterContains([NotNull] SqlParser.ContainsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Contains</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitContains([NotNull] SqlParser.ContainsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>AndOr</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAndOr([NotNull] SqlParser.AndOrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>AndOr</c>
	/// labeled alternative in <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAndOr([NotNull] SqlParser.AndOrContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Function</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] SqlParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Function</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] SqlParser.FunctionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Number</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNumber([NotNull] SqlParser.NumberContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Number</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNumber([NotNull] SqlParser.NumberContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>BinaryFunction</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinaryFunction([NotNull] SqlParser.BinaryFunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>BinaryFunction</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinaryFunction([NotNull] SqlParser.BinaryFunctionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionParenthesis</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpressionParenthesis([NotNull] SqlParser.ExpressionParenthesisContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionParenthesis</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpressionParenthesis([NotNull] SqlParser.ExpressionParenthesisContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Binary</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBinary([NotNull] SqlParser.BinaryContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Binary</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBinary([NotNull] SqlParser.BinaryContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>String</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterString([NotNull] SqlParser.StringContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>String</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitString([NotNull] SqlParser.StringContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Date</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDate([NotNull] SqlParser.DateContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Date</c>
	/// labeled alternative in <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDate([NotNull] SqlParser.DateContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="SqlParser.run"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRun([NotNull] SqlParser.RunContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SqlParser.run"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRun([NotNull] SqlParser.RunContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPredicate([NotNull] SqlParser.PredicateContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SqlParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPredicate([NotNull] SqlParser.PredicateContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpression([NotNull] SqlParser.ExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SqlParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpression([NotNull] SqlParser.ExpressionContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace sdlife.web.Managers.SqlAntlr