using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Identifies a member that has no run-time behavior.</summary>
	// Token: 0x020009C9 RID: 2505
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute" /> class.</summary>
		// Token: 0x06005A02 RID: 23042 RVA: 0x00002050 File Offset: 0x00000250
		public ContractRuntimeIgnoredAttribute()
		{
		}
	}
}
