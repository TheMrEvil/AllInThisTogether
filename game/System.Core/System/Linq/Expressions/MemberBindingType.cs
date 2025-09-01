using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the binding types that are used in <see cref="T:System.Linq.Expressions.MemberInitExpression" /> objects.</summary>
	// Token: 0x02000276 RID: 630
	public enum MemberBindingType
	{
		/// <summary>A binding that represents initializing a member with the value of an expression.</summary>
		// Token: 0x04000A1E RID: 2590
		Assignment,
		/// <summary>A binding that represents recursively initializing members of a member.</summary>
		// Token: 0x04000A1F RID: 2591
		MemberBinding,
		/// <summary>A binding that represents initializing a member of type <see cref="T:System.Collections.IList" /> or <see cref="T:System.Collections.Generic.ICollection`1" /> from a list of elements.</summary>
		// Token: 0x04000A20 RID: 2592
		ListBinding
	}
}
