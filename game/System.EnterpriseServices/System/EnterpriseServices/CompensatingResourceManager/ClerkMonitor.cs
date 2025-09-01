using System;
using System.Collections;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Contains a snapshot of all Clerks active in the process.</summary>
	// Token: 0x02000072 RID: 114
	public sealed class ClerkMonitor : IEnumerable
	{
		/// <summary>Frees the resources of the current <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" /> before it is reclaimed by the garbage collector.</summary>
		// Token: 0x060001C0 RID: 448 RVA: 0x0000276C File Offset: 0x0000096C
		[MonoTODO]
		~ClerkMonitor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" /> class.</summary>
		// Token: 0x060001C1 RID: 449 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public ClerkMonitor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the count of the Clerk monitors in the Compensating Resource Manager (CRM) monitor collection.</summary>
		/// <returns>The number of Clerk monitors in the CRM monitor collection.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</summary>
		/// <param name="index">The numeric index that identifies the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</param>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</returns>
		// Token: 0x17000072 RID: 114
		[MonoTODO]
		public ClerkInfo this[string index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</summary>
		/// <param name="index">The integer index that identifies the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</param>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</returns>
		// Token: 0x17000073 RID: 115
		[MonoTODO]
		public ClerkInfo this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the enumeration of the clerks in the Compensating Resource Manager (CRM) monitor collection.</summary>
		/// <returns>An enumerator describing the clerks in the collection.</returns>
		// Token: 0x060001C5 RID: 453 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the Clerks collection object, which is a snapshot of the current state of the Clerks.</summary>
		// Token: 0x060001C6 RID: 454 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Populate()
		{
			throw new NotImplementedException();
		}
	}
}
