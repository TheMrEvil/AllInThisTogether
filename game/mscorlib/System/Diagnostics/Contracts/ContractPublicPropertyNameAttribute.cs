using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies that a field can be used in method contracts when the field has less visibility than the method.</summary>
	// Token: 0x020009CB RID: 2507
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute" /> class.</summary>
		/// <param name="name">The property name to apply to the field.</param>
		// Token: 0x06005A05 RID: 23045 RVA: 0x00133D4C File Offset: 0x00131F4C
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		/// <summary>Gets the property name to be applied to the field.</summary>
		/// <returns>The property name to be applied to the field.</returns>
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005A06 RID: 23046 RVA: 0x00133D5B File Offset: 0x00131F5B
		public string Name
		{
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x040037A6 RID: 14246
		private string _publicName;
	}
}
