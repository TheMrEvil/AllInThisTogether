using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables the factoring of legacy <see langword="if-then-throw" /> code into separate methods for reuse, and provides full control over thrown exceptions and arguments.</summary>
	// Token: 0x020009CC RID: 2508
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractArgumentValidatorAttribute" /> class.</summary>
		// Token: 0x06005A07 RID: 23047 RVA: 0x00002050 File Offset: 0x00000250
		public ContractArgumentValidatorAttribute()
		{
		}
	}
}
