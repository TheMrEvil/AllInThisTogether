using System;

namespace Mono.CSharp
{
	// Token: 0x02000204 RID: 516
	public class ErrorExpression : EmptyExpression
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x00082446 File Offset: 0x00080646
		private ErrorExpression() : base(InternalType.ErrorType)
		{
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00005936 File Offset: 0x00003B36
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00005936 File Offset: 0x00003B36
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			return this;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Error_ValueAssignment(ResolveContext rc, Expression rhs)
		{
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Error_UnexpectedKind(ResolveContext ec, ResolveFlags flags, Location loc)
		{
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Error_OperatorCannotBeApplied(ResolveContext rc, Location loc, string oper, TypeSpec t)
		{
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00082453 File Offset: 0x00080653
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0008245C File Offset: 0x0008065C
		// Note: this type is marked as 'beforefieldinit'.
		static ErrorExpression()
		{
		}

		// Token: 0x04000A00 RID: 2560
		public static readonly ErrorExpression Instance = new ErrorExpression();
	}
}
