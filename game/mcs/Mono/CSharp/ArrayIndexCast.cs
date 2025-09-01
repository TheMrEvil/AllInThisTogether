using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200020A RID: 522
	public class ArrayIndexCast : TypeCast
	{
		// Token: 0x06001AE3 RID: 6883 RVA: 0x00082945 File Offset: 0x00080B45
		public ArrayIndexCast(Expression expr, TypeSpec returnType) : base(expr, returnType)
		{
			if (expr.Type == returnType)
			{
				throw new ArgumentException("unnecessary array index conversion");
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00082964 File Offset: 0x00080B64
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Expression result;
			using (ec.Set(ResolveContext.Options.CheckedScope))
			{
				result = base.CreateExpressionTree(ec);
			}
			return result;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000829A4 File Offset: 0x00080BA4
		public override void Emit(EmitContext ec)
		{
			this.child.Emit(ec);
			switch (this.child.Type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.UInt:
				ec.Emit(OpCodes.Conv_U);
				return;
			case BuiltinTypeSpec.Type.Long:
				ec.Emit(OpCodes.Conv_Ovf_I);
				return;
			case BuiltinTypeSpec.Type.ULong:
				ec.Emit(OpCodes.Conv_Ovf_I_Un);
				return;
			default:
				throw new InternalErrorException("Cannot emit cast to unknown array element type", new object[]
				{
					this.type
				});
			}
		}
	}
}
