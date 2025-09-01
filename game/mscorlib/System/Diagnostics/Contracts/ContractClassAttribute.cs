using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a separate type contains the code contracts for this type.</summary>
	// Token: 0x020009C5 RID: 2501
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[Conditional("DEBUG")]
	public sealed class ContractClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassAttribute" /> class.</summary>
		/// <param name="typeContainingContracts">The type that contains the code contracts for this type.</param>
		// Token: 0x060059FC RID: 23036 RVA: 0x00133D07 File Offset: 0x00131F07
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		/// <summary>Gets the type that contains the code contracts for this type.</summary>
		/// <returns>The type that contains the code contracts for this type.</returns>
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060059FD RID: 23037 RVA: 0x00133D16 File Offset: 0x00131F16
		public Type TypeContainingContracts
		{
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x040037A3 RID: 14243
		private Type _typeWithContracts;
	}
}
