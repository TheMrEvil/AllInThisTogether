using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Defines abbreviations that you can use in place of the full contract syntax.</summary>
	// Token: 0x020009CD RID: 2509
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractAbbreviatorAttribute" /> class.</summary>
		// Token: 0x06005A08 RID: 23048 RVA: 0x00002050 File Offset: 0x00000250
		public ContractAbbreviatorAttribute()
		{
		}
	}
}
