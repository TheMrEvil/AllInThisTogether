using System;

namespace System
{
	// Token: 0x020001E5 RID: 485
	internal sealed class LocalDataStoreHolder
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x00051DD1 File Offset: 0x0004FFD1
		public LocalDataStoreHolder(LocalDataStore store)
		{
			this.m_Store = store;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00051DE0 File Offset: 0x0004FFE0
		protected override void Finalize()
		{
			try
			{
				LocalDataStore store = this.m_Store;
				if (store != null)
				{
					store.Dispose();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00051E18 File Offset: 0x00050018
		public LocalDataStore Store
		{
			get
			{
				return this.m_Store;
			}
		}

		// Token: 0x040014E4 RID: 5348
		private LocalDataStore m_Store;
	}
}
