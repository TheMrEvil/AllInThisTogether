using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.</summary>
	// Token: 0x020009AE RID: 2478
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConditionalAttribute" /> class.</summary>
		/// <param name="conditionString">A string that specifies the case-sensitive conditional compilation symbol that is associated with the attribute.</param>
		// Token: 0x0600598A RID: 22922 RVA: 0x00132E9B File Offset: 0x0013109B
		public ConditionalAttribute(string conditionString)
		{
			this.ConditionString = conditionString;
		}

		/// <summary>Gets the conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</summary>
		/// <returns>A string that specifies the case-sensitive conditional compilation symbol that is associated with the <see cref="T:System.Diagnostics.ConditionalAttribute" /> attribute.</returns>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x00132EAA File Offset: 0x001310AA
		public string ConditionString
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionString>k__BackingField;
			}
		}

		// Token: 0x0400376C RID: 14188
		[CompilerGenerated]
		private readonly string <ConditionString>k__BackingField;
	}
}
