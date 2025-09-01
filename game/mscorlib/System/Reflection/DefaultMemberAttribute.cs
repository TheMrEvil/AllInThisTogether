using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Defines the member of a type that is the default member used by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
	// Token: 0x02000897 RID: 2199
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class DefaultMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.DefaultMemberAttribute" /> class.</summary>
		/// <param name="memberName">A <see langword="String" /> containing the name of the member to invoke. This may be a constructor, method, property, or field. A suitable invocation attribute must be specified when the member is invoked. The default member of a class can be specified by passing an empty <see langword="String" /> as the name of the member.  
		///  The default member of a type is marked with the <see langword="DefaultMemberAttribute" /> custom attribute or marked in COM in the usual way.</param>
		// Token: 0x06004898 RID: 18584 RVA: 0x000EE237 File Offset: 0x000EC437
		public DefaultMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		/// <summary>Gets the name from the attribute.</summary>
		/// <returns>A string representing the member name.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004899 RID: 18585 RVA: 0x000EE246 File Offset: 0x000EC446
		public string MemberName
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberName>k__BackingField;
			}
		}

		// Token: 0x04002E85 RID: 11909
		[CompilerGenerated]
		private readonly string <MemberName>k__BackingField;
	}
}
