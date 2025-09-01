using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies the type of contract that failed.</summary>
	// Token: 0x020009D0 RID: 2512
	public enum ContractFailureKind
	{
		/// <summary>A <see cref="Overload:System.Diagnostics.Contracts.Contract.Requires" /> contract failed.</summary>
		// Token: 0x040037AD RID: 14253
		Precondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Ensures" /> contract failed.</summary>
		// Token: 0x040037AE RID: 14254
		Postcondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.EnsuresOnThrow" /> contract failed.</summary>
		// Token: 0x040037AF RID: 14255
		PostconditionOnException,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Invariant" /> contract failed.</summary>
		// Token: 0x040037B0 RID: 14256
		Invariant,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assert" /> contract failed.</summary>
		// Token: 0x040037B1 RID: 14257
		Assert,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assume" /> contract failed.</summary>
		// Token: 0x040037B2 RID: 14258
		Assume
	}
}
