using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AE RID: 430
	internal class OpcodeCast : TypeCast
	{
		// Token: 0x060016BF RID: 5823 RVA: 0x0006D1A6 File Offset: 0x0006B3A6
		public OpcodeCast(Expression child, TypeSpec return_type, OpCode op) : base(child, return_type)
		{
			this.op = op;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0006D1B7 File Offset: 0x0006B3B7
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ec.Emit(this.op);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		public TypeSpec UnderlyingType
		{
			get
			{
				return this.child.Type;
			}
		}

		// Token: 0x04000958 RID: 2392
		private readonly OpCode op;
	}
}
