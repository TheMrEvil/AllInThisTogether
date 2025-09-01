using System;

namespace Mono.CSharp
{
	// Token: 0x020001C1 RID: 449
	public class ConstantExpr : MemberExpr
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x00071A5D File Offset: 0x0006FC5D
		public ConstantExpr(ConstSpec constant, Location loc)
		{
			this.constant = constant;
			this.loc = loc;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00071A73 File Offset: 0x0006FC73
		public override string KindName
		{
			get
			{
				return "constant";
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x00071A7A File Offset: 0x0006FC7A
		public override bool IsInstance
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00071A85 File Offset: 0x0006FC85
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.constant.DeclaringType;
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00071A94 File Offset: 0x0006FC94
		protected override Expression DoResolve(ResolveContext rc)
		{
			base.ResolveInstanceExpression(rc, null);
			base.DoBestMemberChecks<ConstSpec>(rc, this.constant);
			Constant constant = this.constant.GetConstant(rc);
			return Constant.CreateConstantFromValue(this.constant.MemberType, constant.GetValue(), this.loc);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0000225C File Offset: 0x0000045C
		public override void Emit(EmitContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00071AE0 File Offset: 0x0006FCE0
		public override string GetSignatureForError()
		{
			return this.constant.GetSignatureForError();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00071AED File Offset: 0x0006FCED
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			Expression.Error_TypeArgumentsCannotBeUsed(ec, "constant", this.GetSignatureForError(), this.loc);
		}

		// Token: 0x0400097E RID: 2430
		private readonly ConstSpec constant;
	}
}
