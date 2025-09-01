using System;

namespace Mono.CSharp
{
	// Token: 0x0200011E RID: 286
	public class StackFieldExpr : FieldExpr, IExpressionCleanup
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x000345C0 File Offset: 0x000327C0
		public StackFieldExpr(Field field) : base(field, Location.Null)
		{
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000345CE File Offset: 0x000327CE
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x000345E5 File Offset: 0x000327E5
		public bool IsAvailableForReuse
		{
			get
			{
				return ((Field)this.spec.MemberDefinition).IsAvailableForReuse;
			}
			set
			{
				((Field)this.spec.MemberDefinition).IsAvailableForReuse = value;
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000345FD File Offset: 0x000327FD
		public override void AddressOf(EmitContext ec, AddressOp mode)
		{
			base.AddressOf(ec, mode);
			if (mode == AddressOp.Load)
			{
				this.IsAvailableForReuse = true;
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00034612 File Offset: 0x00032812
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			this.PrepareCleanup(ec);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00034622 File Offset: 0x00032822
		public void EmitLoad(EmitContext ec)
		{
			base.Emit(ec);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003462B File Offset: 0x0003282B
		public void PrepareCleanup(EmitContext ec)
		{
			this.IsAvailableForReuse = true;
			if (TypeSpec.IsReferenceType(this.type))
			{
				ec.AddStatementEpilog(this);
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00034648 File Offset: 0x00032848
		void IExpressionCleanup.EmitCleanup(EmitContext ec)
		{
			base.EmitAssign(ec, new NullConstant(this.type, this.loc), false, false);
		}
	}
}
