using System;

namespace System.EnterpriseServices
{
	/// <summary>Stores objects in the current transaction. This class cannot be inherited.</summary>
	// Token: 0x0200003F RID: 63
	public sealed class ResourcePool
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ResourcePool" /> class.</summary>
		/// <param name="cb">A <see cref="T:System.EnterpriseServices.ResourcePool.TransactionEndDelegate" />, that is called when a transaction is finished. All items currently stored in the transaction are handed back to the user through the delegate.</param>
		// Token: 0x060000DB RID: 219 RVA: 0x000021E0 File Offset: 0x000003E0
		[MonoTODO]
		public ResourcePool(ResourcePool.TransactionEndDelegate cb)
		{
		}

		/// <summary>Gets a resource from the current transaction.</summary>
		/// <returns>The resource object.</returns>
		// Token: 0x060000DC RID: 220 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public object GetResource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a resource to the current transaction.</summary>
		/// <param name="resource">The resource to add.</param>
		/// <returns>
		///   <see langword="true" /> if the resource object was added to the pool; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000DD RID: 221 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool PutResource(object resource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that handles the ending of a transaction.</summary>
		/// <param name="resource">The object that is passed back to the delegate.</param>
		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x060000DF RID: 223
		public delegate void TransactionEndDelegate(object resource);
	}
}
