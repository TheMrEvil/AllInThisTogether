using System;
using System.Collections;

namespace System.EnterpriseServices
{
	/// <summary>Provides an ordered collection of identities in the current call chain.</summary>
	// Token: 0x02000043 RID: 67
	public sealed class SecurityCallers : IEnumerable
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000021E0 File Offset: 0x000003E0
		internal SecurityCallers()
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000021E0 File Offset: 0x000003E0
		internal SecurityCallers(ISecurityCallersColl collection)
		{
		}

		/// <summary>Gets the number of callers in the chain.</summary>
		/// <returns>The number of callers in the chain.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002085 File Offset: 0x00000285
		public int Count
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the specified <see cref="T:System.EnterpriseServices.SecurityIdentity" /> item.</summary>
		/// <param name="idx">The item to access using an index number.</param>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object.</returns>
		// Token: 0x17000046 RID: 70
		public SecurityIdentity this[int idx]
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the enumeration interface for the object.</summary>
		/// <returns>The enumerator interface for the <see langword="ISecurityCallersColl" /> collection.</returns>
		// Token: 0x060000F2 RID: 242 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
