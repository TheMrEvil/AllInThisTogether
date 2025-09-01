using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020001E7 RID: 487
	internal sealed class LocalDataStore
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x00051E48 File Offset: 0x00050048
		public LocalDataStore(LocalDataStoreMgr mgr, int InitialCapacity)
		{
			this.m_Manager = mgr;
			this.m_DataTable = new LocalDataStoreElement[InitialCapacity];
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00051E63 File Offset: 0x00050063
		internal void Dispose()
		{
			this.m_Manager.DeleteLocalDataStore(this);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00051E74 File Offset: 0x00050074
		public object GetData(LocalDataStoreSlot slot)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				if (slot2 >= this.m_DataTable.Length)
				{
					return null;
				}
				LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot2];
				if (localDataStoreElement == null)
				{
					return null;
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					return localDataStoreElement.Value;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00051ED8 File Offset: 0x000500D8
		public void SetData(LocalDataStoreSlot slot, object data)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				LocalDataStoreElement localDataStoreElement = (slot2 < this.m_DataTable.Length) ? this.m_DataTable[slot2] : null;
				if (localDataStoreElement == null)
				{
					localDataStoreElement = this.PopulateElement(slot);
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					localDataStoreElement.Value = data;
					return;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00051F44 File Offset: 0x00050144
		internal void FreeData(int slot, long cookie)
		{
			if (slot >= this.m_DataTable.Length)
			{
				return;
			}
			LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot];
			if (localDataStoreElement != null && localDataStoreElement.Cookie == cookie)
			{
				this.m_DataTable[slot] = null;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00051F7C File Offset: 0x0005017C
		[SecuritySafeCritical]
		private LocalDataStoreElement PopulateElement(LocalDataStoreSlot slot)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreElement result;
			try
			{
				Monitor.Enter(this.m_Manager, ref flag);
				int slot2 = slot.Slot;
				if (slot2 < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
				}
				if (slot2 >= this.m_DataTable.Length)
				{
					LocalDataStoreElement[] array = new LocalDataStoreElement[this.m_Manager.GetSlotTableLength()];
					Array.Copy(this.m_DataTable, array, this.m_DataTable.Length);
					this.m_DataTable = array;
				}
				if (this.m_DataTable[slot2] == null)
				{
					this.m_DataTable[slot2] = new LocalDataStoreElement(slot.Cookie);
				}
				result = this.m_DataTable[slot2];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_Manager);
				}
			}
			return result;
		}

		// Token: 0x040014E7 RID: 5351
		private LocalDataStoreElement[] m_DataTable;

		// Token: 0x040014E8 RID: 5352
		private LocalDataStoreMgr m_Manager;
	}
}
