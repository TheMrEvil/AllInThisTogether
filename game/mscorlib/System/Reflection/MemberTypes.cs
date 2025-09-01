using System;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of <see cref="T:System.Reflection.MemberInfo" />.</summary>
	// Token: 0x020008AB RID: 2219
	[Flags]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor</summary>
		// Token: 0x04002EBB RID: 11963
		Constructor = 1,
		/// <summary>Specifies that the member is an event.</summary>
		// Token: 0x04002EBC RID: 11964
		Event = 2,
		/// <summary>Specifies that the member is a field.</summary>
		// Token: 0x04002EBD RID: 11965
		Field = 4,
		/// <summary>Specifies that the member is a method.</summary>
		// Token: 0x04002EBE RID: 11966
		Method = 8,
		/// <summary>Specifies that the member is a property.</summary>
		// Token: 0x04002EBF RID: 11967
		Property = 16,
		/// <summary>Specifies that the member is a type.</summary>
		// Token: 0x04002EC0 RID: 11968
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type.</summary>
		// Token: 0x04002EC1 RID: 11969
		Custom = 64,
		/// <summary>Specifies that the member is a nested type.</summary>
		// Token: 0x04002EC2 RID: 11970
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x04002EC3 RID: 11971
		All = 191
	}
}
