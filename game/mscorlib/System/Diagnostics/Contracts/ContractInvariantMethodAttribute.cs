using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Marks a method as being the invariant method for a class.</summary>
	// Token: 0x020009C7 RID: 2503
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractInvariantMethodAttribute" /> class.</summary>
		// Token: 0x06005A00 RID: 23040 RVA: 0x00002050 File Offset: 0x00000250
		public ContractInvariantMethodAttribute()
		{
		}
	}
}
