using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource consumed by the member of a class. This class cannot be inherited.</summary>
	// Token: 0x0200063F RID: 1599
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		// Token: 0x06003C2B RID: 15403 RVA: 0x000D1166 File Offset: 0x000CF366
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource and the scope of how it is consumed.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		/// <param name="consumptionScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> used by this member.</param>
		// Token: 0x06003C2C RID: 15404 RVA: 0x000D1181 File Offset: 0x000CF381
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		/// <summary>Gets the resource scope for the consumed resource.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope of the consumed member.</returns>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000D1197 File Offset: 0x000CF397
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		/// <summary>Gets the consumption scope for this member.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope used by this member.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000D119F File Offset: 0x000CF39F
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x040026F6 RID: 9974
		private ResourceScope _consumptionScope;

		// Token: 0x040026F7 RID: 9975
		private ResourceScope _resourceScope;
	}
}
