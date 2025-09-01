using System;

namespace Mono.CSharp
{
	// Token: 0x020002B2 RID: 690
	public class BlockConstant : BlockVariable
	{
		// Token: 0x06002101 RID: 8449 RVA: 0x000A1C89 File Offset: 0x0009FE89
		public BlockConstant(FullNamedExpression type, LocalVariable li) : base(type, li)
		{
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000A1C94 File Offset: 0x0009FE94
		protected override Expression ResolveInitializer(BlockContext bc, LocalVariable li, Expression initializer)
		{
			initializer = initializer.Resolve(bc);
			if (initializer == null)
			{
				return null;
			}
			Constant constant = initializer as Constant;
			if (constant == null)
			{
				initializer.Error_ExpressionMustBeConstant(bc, initializer.Location, li.Name);
				return null;
			}
			constant = constant.ConvertImplicitly(li.Type);
			if (constant == null)
			{
				if (TypeSpec.IsReferenceType(li.Type))
				{
					initializer.Error_ConstantCanBeInitializedWithNullOnly(bc, li.Type, initializer.Location, li.Name);
				}
				else
				{
					initializer.Error_ValueCannotBeConverted(bc, li.Type, false);
				}
				return null;
			}
			li.ConstantValue = constant;
			return initializer;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000A1D1E File Offset: 0x0009FF1E
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
