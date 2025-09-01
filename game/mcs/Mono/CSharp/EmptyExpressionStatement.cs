using System;

namespace Mono.CSharp
{
	// Token: 0x02000203 RID: 515
	public sealed class EmptyExpressionStatement : ExpressionStatement
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x0008241E File Offset: 0x0008061E
		private EmptyExpressionStatement()
		{
			this.loc = Location.Null;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return null;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitStatement(EmitContext ec)
		{
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00076B2C File Offset: 0x00074D2C
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Value;
			this.type = ec.BuiltinTypes.Object;
			return this;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00082431 File Offset: 0x00080631
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0008243A File Offset: 0x0008063A
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyExpressionStatement()
		{
		}

		// Token: 0x040009FF RID: 2559
		public static readonly EmptyExpressionStatement Instance = new EmptyExpressionStatement();
	}
}
