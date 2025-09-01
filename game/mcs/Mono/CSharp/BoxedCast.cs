using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AB RID: 427
	public class BoxedCast : TypeCast
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0006C7C0 File Offset: 0x0006A9C0
		public BoxedCast(Expression expr, TypeSpec target_type) : base(expr, target_type)
		{
			this.eclass = ExprClass.Value;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0006C7D4 File Offset: 0x0006A9D4
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			if (targetType.BuiltinType != BuiltinTypeSpec.Type.Object)
			{
				base.EncodeAttributeValue(rc, enc, targetType, parameterType);
				return;
			}
			enc.Encode(this.child.Type);
			this.child.EncodeAttributeValue(rc, enc, this.child.Type, parameterType);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0006C823 File Offset: 0x0006AA23
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ec.Emit(OpCodes.Box, this.child.Type);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0006C844 File Offset: 0x0006AA44
		public override void EmitSideEffect(EmitContext ec)
		{
			if (this.child.Type.IsStruct && (this.type.BuiltinType == BuiltinTypeSpec.Type.Object || this.type.BuiltinType == BuiltinTypeSpec.Type.ValueType))
			{
				this.child.EmitSideEffect(ec);
				return;
			}
			base.EmitSideEffect(ec);
		}
	}
}
