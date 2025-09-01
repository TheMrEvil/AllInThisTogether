using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AA RID: 426
	public class EnumConstant : Constant
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x0006C66B File Offset: 0x0006A86B
		public EnumConstant(Constant child, TypeSpec enum_type) : base(child.Location)
		{
			this.Child = child;
			this.eclass = ExprClass.Value;
			this.type = enum_type;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00044513 File Offset: 0x00042713
		protected EnumConstant(Location loc) : base(loc)
		{
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0006C68E File Offset: 0x0006A88E
		public override void Emit(EmitContext ec)
		{
			this.Child.Emit(ec);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0006C69C File Offset: 0x0006A89C
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			this.Child.EncodeAttributeValue(rc, enc, this.Child.Type, parameterType);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0006C6B8 File Offset: 0x0006A8B8
		public override void EmitBranchable(EmitContext ec, Label label, bool on_true)
		{
			this.Child.EmitBranchable(ec, label, on_true);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0006C6C8 File Offset: 0x0006A8C8
		public override void EmitSideEffect(EmitContext ec)
		{
			this.Child.EmitSideEffect(ec);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0006C6D6 File Offset: 0x0006A8D6
		public override string GetSignatureForError()
		{
			return base.Type.GetSignatureForError();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0006C6E3 File Offset: 0x0006A8E3
		public override object GetValue()
		{
			return this.Child.GetValue();
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0006C6F0 File Offset: 0x0006A8F0
		public override object GetTypedValue()
		{
			return Enum.ToObject(this.type.GetMetaInfo(), this.Child.GetValue());
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0006C70D File Offset: 0x0006A90D
		public override string GetValueAsLiteral()
		{
			return this.Child.GetValueAsLiteral();
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0006C71A File Offset: 0x0006A91A
		public override long GetValueAsLong()
		{
			return this.Child.GetValueAsLong();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0006C727 File Offset: 0x0006A927
		public EnumConstant Increment()
		{
			return new EnumConstant(((IntegralConstant)this.Child).Increment(), this.type);
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0006C744 File Offset: 0x0006A944
		public override bool IsDefaultValue
		{
			get
			{
				return this.Child.IsDefaultValue;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0006C751 File Offset: 0x0006A951
		public override bool IsSideEffectFree
		{
			get
			{
				return this.Child.IsSideEffectFree;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0006C75E File Offset: 0x0006A95E
		public override bool IsZeroInteger
		{
			get
			{
				return this.Child.IsZeroInteger;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0006C76B File Offset: 0x0006A96B
		public override bool IsNegative
		{
			get
			{
				return this.Child.IsNegative;
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0006C778 File Offset: 0x0006A978
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			if (this.Child.Type == target_type)
			{
				return this.Child;
			}
			return this.Child.ConvertExplicitly(in_checked_context, target_type);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0006C79C File Offset: 0x0006A99C
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.type == type)
			{
				return this;
			}
			if (!Convert.ImplicitStandardConversionExists(this, type))
			{
				return null;
			}
			return this.Child.ConvertImplicitly(type);
		}

		// Token: 0x04000956 RID: 2390
		public Constant Child;
	}
}
