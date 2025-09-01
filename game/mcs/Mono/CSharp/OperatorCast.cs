using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001A8 RID: 424
	public class OperatorCast : TypeCast
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x0006C3D4 File Offset: 0x0006A5D4
		public OperatorCast(Expression expr, TypeSpec target_type) : this(expr, target_type, target_type, false)
		{
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0006C3E0 File Offset: 0x0006A5E0
		public OperatorCast(Expression expr, TypeSpec target_type, bool find_explicit) : this(expr, target_type, target_type, find_explicit)
		{
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		public OperatorCast(Expression expr, TypeSpec declaringType, TypeSpec returnType, bool isExplicit) : base(expr, returnType)
		{
			Operator.OpType op = isExplicit ? Operator.OpType.Explicit : Operator.OpType.Implicit;
			IList<MemberSpec> userOperator = MemberCache.GetUserOperator(declaringType, op, true);
			if (userOperator != null)
			{
				foreach (MemberSpec memberSpec in userOperator)
				{
					MethodSpec methodSpec = (MethodSpec)memberSpec;
					if (methodSpec.ReturnType == returnType && methodSpec.Parameters.Types[0] == expr.Type)
					{
						this.conversion_operator = methodSpec;
						return;
					}
				}
			}
			throw new InternalErrorException("Missing predefined user operator between `{0}' and `{1}'", new object[]
			{
				returnType.GetSignatureForError(),
				expr.Type.GetSignatureForError()
			});
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0006C4A0 File Offset: 0x0006A6A0
		public override void Emit(EmitContext ec)
		{
			this.child.Emit(ec);
			ec.Emit(OpCodes.Call, this.conversion_operator);
		}

		// Token: 0x04000954 RID: 2388
		private readonly MethodSpec conversion_operator;
	}
}
