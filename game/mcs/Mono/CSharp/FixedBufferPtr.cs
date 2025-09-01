using System;

namespace Mono.CSharp
{
	// Token: 0x02000208 RID: 520
	internal class FixedBufferPtr : Expression
	{
		// Token: 0x06001ADC RID: 6876 RVA: 0x000828C3 File Offset: 0x00080AC3
		public FixedBufferPtr(Expression array, TypeSpec array_type, Location l)
		{
			this.type = array_type;
			this.array = array;
			this.loc = l;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override bool ContainsEmitWithAwait()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00075589 File Offset: 0x00073789
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			base.Error_PointerInsideExpressionTree(ec);
			return null;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000828E0 File Offset: 0x00080AE0
		public override void Emit(EmitContext ec)
		{
			this.array.Emit(ec);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000828EE File Offset: 0x00080AEE
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.type = PointerContainer.MakeType(ec.Module, this.type);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x04000A09 RID: 2569
		private readonly Expression array;
	}
}
