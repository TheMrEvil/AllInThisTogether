using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x020002FF RID: 767
	public class Wrap : TypeCast
	{
		// Token: 0x06002466 RID: 9318 RVA: 0x0006C7C0 File Offset: 0x0006A9C0
		private Wrap(Expression expr, TypeSpec type) : base(expr, type)
		{
			this.eclass = ExprClass.Value;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000AE0E0 File Offset: 0x000AC2E0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			TypeCast typeCast = this.child as TypeCast;
			if (typeCast != null)
			{
				this.child.Type = this.type;
				return typeCast.CreateExpressionTree(ec);
			}
			UserCast userCast = this.child as UserCast;
			if (userCast != null)
			{
				this.child.Type = this.type;
				return userCast.CreateExpressionTree(ec);
			}
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000AE144 File Offset: 0x000AC344
		public static Expression Create(Expression expr, TypeSpec type)
		{
			Unwrap unwrap = expr as Unwrap;
			if (unwrap != null && expr.Type == NullableInfo.GetUnderlyingType(type))
			{
				return unwrap.Original;
			}
			return new Wrap(expr, type);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000AE177 File Offset: 0x000AC377
		public override void Emit(EmitContext ec)
		{
			this.child.Emit(ec);
			ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
		}
	}
}
