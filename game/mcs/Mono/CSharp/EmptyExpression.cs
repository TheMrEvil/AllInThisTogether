using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000201 RID: 513
	public class EmptyExpression : Expression
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x00082363 File Offset: 0x00080563
		public EmptyExpression(TypeSpec t)
		{
			this.type = t;
			this.eclass = ExprClass.Value;
			this.loc = Location.Null;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitSideEffect(EmitContext ec)
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00082384 File Offset: 0x00080584
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00082390 File Offset: 0x00080590
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyExpression()
		{
		}

		// Token: 0x040009F7 RID: 2551
		public static readonly EmptyExpression LValueMemberAccess = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009F8 RID: 2552
		public static readonly EmptyExpression LValueMemberOutAccess = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009F9 RID: 2553
		public static readonly EmptyExpression UnaryAddress = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009FA RID: 2554
		public static readonly EmptyExpression EventAddition = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009FB RID: 2555
		public static readonly EmptyExpression EventSubtraction = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009FC RID: 2556
		public static readonly EmptyExpression MissingValue = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009FD RID: 2557
		public static readonly Expression Null = new EmptyExpression(InternalType.FakeInternalType);

		// Token: 0x040009FE RID: 2558
		public static readonly EmptyExpression OutAccess = new EmptyExpression.OutAccessExpression(InternalType.FakeInternalType);

		// Token: 0x020003BB RID: 955
		private sealed class OutAccessExpression : EmptyExpression
		{
			// Token: 0x06002730 RID: 10032 RVA: 0x00082415 File Offset: 0x00080615
			public OutAccessExpression(TypeSpec t) : base(t)
			{
			}

			// Token: 0x06002731 RID: 10033 RVA: 0x000BBBF5 File Offset: 0x000B9DF5
			public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
			{
				rc.Report.Error(206, right_side.Location, "A property, indexer or dynamic member access may not be passed as `ref' or `out' parameter");
				return null;
			}
		}
	}
}
