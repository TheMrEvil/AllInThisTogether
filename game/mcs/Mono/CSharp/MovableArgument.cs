using System;

namespace Mono.CSharp
{
	// Token: 0x020000FC RID: 252
	public class MovableArgument : Argument
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x0002CDAA File Offset: 0x0002AFAA
		public MovableArgument(Argument arg) : this(arg.Expr, arg.ArgType)
		{
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002CDBE File Offset: 0x0002AFBE
		protected MovableArgument(Expression expr, Argument.AType modifier) : base(expr, modifier)
		{
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002CDC8 File Offset: 0x0002AFC8
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			if (this.variable != null)
			{
				this.variable.Release(ec);
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
		public void EmitToVariable(EmitContext ec)
		{
			TypeSpec typeSpec = this.Expr.Type;
			if (base.IsByRef)
			{
				((IMemoryLocation)this.Expr).AddressOf(ec, AddressOp.LoadStore);
				typeSpec = ReferenceContainer.MakeType(ec.Module, typeSpec);
			}
			else
			{
				this.Expr.Emit(ec);
			}
			this.variable = new LocalTemporary(typeSpec);
			this.variable.Store(ec);
			this.Expr = this.variable;
		}

		// Token: 0x04000617 RID: 1559
		private LocalTemporary variable;
	}
}
