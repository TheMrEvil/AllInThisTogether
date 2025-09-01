using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that an assembly is a reference assembly that contains contracts.</summary>
	// Token: 0x020009C8 RID: 2504
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ContractReferenceAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractReferenceAssemblyAttribute" /> class.</summary>
		// Token: 0x06005A01 RID: 23041 RVA: 0x00002050 File Offset: 0x00000250
		public ContractReferenceAssemblyAttribute()
		{
		}
	}
}
