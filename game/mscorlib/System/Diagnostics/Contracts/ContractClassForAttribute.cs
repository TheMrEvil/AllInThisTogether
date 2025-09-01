using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a class is a contract for a type.</summary>
	// Token: 0x020009C6 RID: 2502
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ContractClassForAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractClassForAttribute" /> class, specifying the type the current class is a contract for.</summary>
		/// <param name="typeContractsAreFor">The type the current class is a contract for.</param>
		// Token: 0x060059FE RID: 23038 RVA: 0x00133D1E File Offset: 0x00131F1E
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		/// <summary>Gets the type that this code contract applies to.</summary>
		/// <returns>The type that this contract applies to.</returns>
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060059FF RID: 23039 RVA: 0x00133D2D File Offset: 0x00131F2D
		public Type TypeContractsAreFor
		{
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x040037A4 RID: 14244
		private Type _typeIAmAContractFor;
	}
}
