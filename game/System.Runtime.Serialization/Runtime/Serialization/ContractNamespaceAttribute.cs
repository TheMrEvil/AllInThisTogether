using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies the CLR namespace and XML namespace of the data contract.</summary>
	// Token: 0x020000BA RID: 186
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, Inherited = false, AllowMultiple = true)]
	public sealed class ContractNamespaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ContractNamespaceAttribute" /> class using the supplied namespace.</summary>
		/// <param name="contractNamespace">The namespace of the contract.</param>
		// Token: 0x06000A66 RID: 2662 RVA: 0x0002CD78 File Offset: 0x0002AF78
		public ContractNamespaceAttribute(string contractNamespace)
		{
			this.contractNamespace = contractNamespace;
		}

		/// <summary>Gets or sets the CLR namespace of the data contract type.</summary>
		/// <returns>The CLR-legal namespace of a type.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002CD87 File Offset: 0x0002AF87
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002CD8F File Offset: 0x0002AF8F
		public string ClrNamespace
		{
			get
			{
				return this.clrNamespace;
			}
			set
			{
				this.clrNamespace = value;
			}
		}

		/// <summary>Gets the namespace of the data contract members.</summary>
		/// <returns>The namespace of the data contract members.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002CD98 File Offset: 0x0002AF98
		public string ContractNamespace
		{
			get
			{
				return this.contractNamespace;
			}
		}

		// Token: 0x04000464 RID: 1124
		private string clrNamespace;

		// Token: 0x04000465 RID: 1125
		private string contractNamespace;
	}
}
