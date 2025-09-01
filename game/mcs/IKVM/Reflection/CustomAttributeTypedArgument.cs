using System;

namespace IKVM.Reflection
{
	// Token: 0x0200000F RID: 15
	public struct CustomAttributeTypedArgument
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004ED5 File Offset: 0x000030D5
		internal CustomAttributeTypedArgument(Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004EE8 File Offset: 0x000030E8
		public override bool Equals(object obj)
		{
			return this == obj as CustomAttributeTypedArgument?;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F20 File Offset: 0x00003120
		public override int GetHashCode()
		{
			return this.type.GetHashCode() ^ 77 * ((this.value == null) ? 0 : this.value.GetHashCode());
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004F47 File Offset: 0x00003147
		public Type ArgumentType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004F4F File Offset: 0x0000314F
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004F58 File Offset: 0x00003158
		public static bool operator ==(CustomAttributeTypedArgument arg1, CustomAttributeTypedArgument arg2)
		{
			return arg1.type.Equals(arg2.type) && (arg1.value == arg2.value || (arg1.value != null && arg1.value.Equals(arg2.value)));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004FA5 File Offset: 0x000031A5
		public static bool operator !=(CustomAttributeTypedArgument arg1, CustomAttributeTypedArgument arg2)
		{
			return !(arg1 == arg2);
		}

		// Token: 0x0400003D RID: 61
		private readonly Type type;

		// Token: 0x0400003E RID: 62
		private readonly object value;
	}
}
