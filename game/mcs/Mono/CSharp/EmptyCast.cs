using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001A7 RID: 423
	public class EmptyCast : TypeCast
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x0006C344 File Offset: 0x0006A544
		private EmptyCast(Expression child, TypeSpec target_type) : base(child, target_type)
		{
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0006C350 File Offset: 0x0006A550
		public static Expression Create(Expression child, TypeSpec type)
		{
			Constant constant = child as Constant;
			if (constant != null)
			{
				EnumConstant enumConstant = constant as EnumConstant;
				if (enumConstant != null)
				{
					constant = enumConstant.Child;
				}
				if (!(constant is ReducedExpression.ReducedConstantExpression))
				{
					if (constant.Type == type)
					{
						return constant;
					}
					Constant constant2 = constant.ConvertImplicitly(type);
					if (constant2 != null)
					{
						return constant2;
					}
				}
			}
			EmptyCast emptyCast = child as EmptyCast;
			if (emptyCast != null)
			{
				return new EmptyCast(emptyCast.child, type);
			}
			return new EmptyCast(child, type);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0006C3B6 File Offset: 0x0006A5B6
		public override void EmitBranchable(EmitContext ec, Label label, bool on_true)
		{
			this.child.EmitBranchable(ec, label, on_true);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0006C3C6 File Offset: 0x0006A5C6
		public override void EmitSideEffect(EmitContext ec)
		{
			this.child.EmitSideEffect(ec);
		}
	}
}
