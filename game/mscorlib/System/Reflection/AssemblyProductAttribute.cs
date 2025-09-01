using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Defines a product name custom attribute for an assembly manifest.</summary>
	// Token: 0x0200088D RID: 2189
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyProductAttribute" /> class.</summary>
		/// <param name="product">The product name information.</param>
		// Token: 0x0600486F RID: 18543 RVA: 0x000EE134 File Offset: 0x000EC334
		public AssemblyProductAttribute(string product)
		{
			this.Product = product;
		}

		/// <summary>Gets product name information.</summary>
		/// <returns>A string containing the product name.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x000EE143 File Offset: 0x000EC343
		public string Product
		{
			[CompilerGenerated]
			get
			{
				return this.<Product>k__BackingField;
			}
		}

		// Token: 0x04002E61 RID: 11873
		[CompilerGenerated]
		private readonly string <Product>k__BackingField;
	}
}
