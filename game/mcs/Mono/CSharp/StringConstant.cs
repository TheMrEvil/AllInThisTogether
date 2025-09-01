using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200014F RID: 335
	public class StringConstant : Constant
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x000444E5 File Offset: 0x000426E5
		public StringConstant(BuiltinTypes types, string s, Location loc) : this(types.String, s, loc)
		{
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000444F5 File Offset: 0x000426F5
		public StringConstant(TypeSpec type, string s, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
			this.Value = s;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00044513 File Offset: 0x00042713
		protected StringConstant(Location loc) : base(loc)
		{
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0004451C File Offset: 0x0004271C
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x00044524 File Offset: 0x00042724
		public string Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004452D File Offset: 0x0004272D
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00044535 File Offset: 0x00042735
		public override string GetValueAsLiteral()
		{
			return "\"" + this.Value + "\"";
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0000225C File Offset: 0x0000045C
		public override long GetValueAsLong()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004454C File Offset: 0x0004274C
		public override void Emit(EmitContext ec)
		{
			if (this.Value == null)
			{
				ec.EmitNull();
				return;
			}
			if (this.Value.Length == 0 && ec.Module.Compiler.Settings.Optimize)
			{
				BuiltinTypeSpec @string = ec.BuiltinTypes.String;
				if (ec.CurrentType != @string)
				{
					FieldSpec fieldSpec = ec.Module.PredefinedMembers.StringEmpty.Get();
					if (fieldSpec != null)
					{
						ec.Emit(OpCodes.Ldsfld, fieldSpec);
						return;
					}
				}
			}
			string value = this.Value;
			if (ec.Module.GetResourceStrings != null && !ec.Module.GetResourceStrings.TryGetValue(value, out value))
			{
				value = this.Value;
			}
			ec.Emit(OpCodes.Ldstr, value);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00044603 File Offset: 0x00042803
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			if (this.type != targetType)
			{
				enc.Encode(this.type);
			}
			enc.Encode(this.Value);
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00044627 File Offset: 0x00042827
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == null;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00044632 File Offset: 0x00042832
		public override bool IsNull
		{
			get
			{
				return this.IsDefaultValue;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			return null;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004463A File Offset: 0x0004283A
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.IsDefaultValue && type.BuiltinType == BuiltinTypeSpec.Type.Object)
			{
				return new NullConstant(type, this.loc);
			}
			return base.ConvertImplicitly(type);
		}

		// Token: 0x04000743 RID: 1859
		[CompilerGenerated]
		private string <Value>k__BackingField;
	}
}
