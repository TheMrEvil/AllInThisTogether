using System;

namespace IKVM.Reflection
{
	// Token: 0x0200000E RID: 14
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004DFB File Offset: 0x00002FFB
		internal CustomAttributeNamedArgument(MemberInfo member, CustomAttributeTypedArgument value)
		{
			this.member = member;
			this.value = value;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E0C File Offset: 0x0000300C
		public override bool Equals(object obj)
		{
			return this == obj as CustomAttributeNamedArgument?;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004E44 File Offset: 0x00003044
		public override int GetHashCode()
		{
			return this.member.GetHashCode() ^ 53 * this.value.GetHashCode();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004E74 File Offset: 0x00003074
		public MemberInfo MemberInfo
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004E7C File Offset: 0x0000307C
		public CustomAttributeTypedArgument TypedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004E84 File Offset: 0x00003084
		public bool IsField
		{
			get
			{
				return this.member.MemberType == MemberTypes.Field;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004E94 File Offset: 0x00003094
		public string MemberName
		{
			get
			{
				return this.member.Name;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004EA1 File Offset: 0x000030A1
		public static bool operator ==(CustomAttributeNamedArgument arg1, CustomAttributeNamedArgument arg2)
		{
			return arg1.member.Equals(arg2.member) && arg1.value == arg2.value;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004EC9 File Offset: 0x000030C9
		public static bool operator !=(CustomAttributeNamedArgument arg1, CustomAttributeNamedArgument arg2)
		{
			return !(arg1 == arg2);
		}

		// Token: 0x0400003B RID: 59
		private readonly MemberInfo member;

		// Token: 0x0400003C RID: 60
		private readonly CustomAttributeTypedArgument value;
	}
}
