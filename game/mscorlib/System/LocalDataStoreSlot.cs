using System;
using System.Runtime.InteropServices;
using Unity;

namespace System
{
	/// <summary>Encapsulates a memory slot to store local data. This class cannot be inherited.</summary>
	// Token: 0x020001E8 RID: 488
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x060014D5 RID: 5333 RVA: 0x00052038 File Offset: 0x00050238
		internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
		{
			this.m_mgr = mgr;
			this.m_slot = slot;
			this.m_cookie = cookie;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00052055 File Offset: 0x00050255
		internal LocalDataStoreMgr Manager
		{
			get
			{
				return this.m_mgr;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0005205D File Offset: 0x0005025D
		internal int Slot
		{
			get
			{
				return this.m_slot;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00052065 File Offset: 0x00050265
		internal long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.LocalDataStoreSlot" /> object.</summary>
		// Token: 0x060014D9 RID: 5337 RVA: 0x00052070 File Offset: 0x00050270
		protected override void Finalize()
		{
			try
			{
				LocalDataStoreMgr mgr = this.m_mgr;
				if (mgr != null)
				{
					int slot = this.m_slot;
					this.m_slot = -1;
					mgr.FreeDataSlot(slot, this.m_cookie);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000173AD File Offset: 0x000155AD
		internal LocalDataStoreSlot()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040014E9 RID: 5353
		private LocalDataStoreMgr m_mgr;

		// Token: 0x040014EA RID: 5354
		private int m_slot;

		// Token: 0x040014EB RID: 5355
		private long m_cookie;
	}
}
