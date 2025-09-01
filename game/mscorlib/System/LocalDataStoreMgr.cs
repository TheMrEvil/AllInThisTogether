using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020001E9 RID: 489
	internal sealed class LocalDataStoreMgr
	{
		// Token: 0x060014DB RID: 5339 RVA: 0x000520C0 File Offset: 0x000502C0
		[SecuritySafeCritical]
		public LocalDataStoreHolder CreateLocalDataStore()
		{
			LocalDataStore localDataStore = new LocalDataStore(this, this.m_SlotInfoTable.Length);
			LocalDataStoreHolder result = new LocalDataStoreHolder(localDataStore);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Add(localDataStore);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return result;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0005211C File Offset: 0x0005031C
		[SecuritySafeCritical]
		public void DeleteLocalDataStore(LocalDataStore store)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Remove(store);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00052164 File Offset: 0x00050364
		[SecuritySafeCritical]
		public LocalDataStoreSlot AllocateDataSlot()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot result;
			try
			{
				Monitor.Enter(this, ref flag);
				int num = this.m_SlotInfoTable.Length;
				int num2 = this.m_FirstAvailableSlot;
				while (num2 < num && this.m_SlotInfoTable[num2])
				{
					num2++;
				}
				if (num2 >= num)
				{
					int num3;
					if (num < 512)
					{
						num3 = num * 2;
					}
					else
					{
						num3 = num + 128;
					}
					bool[] array = new bool[num3];
					Array.Copy(this.m_SlotInfoTable, array, num);
					this.m_SlotInfoTable = array;
				}
				this.m_SlotInfoTable[num2] = true;
				int slot = num2;
				long cookieGenerator = this.m_CookieGenerator;
				this.m_CookieGenerator = checked(cookieGenerator + 1L);
				LocalDataStoreSlot localDataStoreSlot = new LocalDataStoreSlot(this, slot, cookieGenerator);
				this.m_FirstAvailableSlot = num2 + 1;
				result = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return result;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005222C File Offset: 0x0005042C
		[SecuritySafeCritical]
		public LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot result;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot localDataStoreSlot = this.AllocateDataSlot();
				this.m_KeyToSlotMap.Add(name, localDataStoreSlot);
				result = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return result;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0005227C File Offset: 0x0005047C
		[SecuritySafeCritical]
		public LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot result;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot valueOrDefault = this.m_KeyToSlotMap.GetValueOrDefault(name);
				if (valueOrDefault == null)
				{
					result = this.AllocateNamedDataSlot(name);
				}
				else
				{
					result = valueOrDefault;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return result;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000522D4 File Offset: 0x000504D4
		[SecuritySafeCritical]
		public void FreeNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_KeyToSlotMap.Remove(name);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005231C File Offset: 0x0005051C
		[SecuritySafeCritical]
		internal void FreeDataSlot(int slot, long cookie)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				for (int i = 0; i < this.m_ManagedLocalDataStores.Count; i++)
				{
					this.m_ManagedLocalDataStores[i].FreeData(slot, cookie);
				}
				this.m_SlotInfoTable[slot] = false;
				if (slot < this.m_FirstAvailableSlot)
				{
					this.m_FirstAvailableSlot = slot;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00052398 File Offset: 0x00050598
		public void ValidateSlot(LocalDataStoreSlot slot)
		{
			if (slot == null || slot.Manager != this)
			{
				throw new ArgumentException(Environment.GetResourceString("Specified slot number was invalid."));
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000523B6 File Offset: 0x000505B6
		internal int GetSlotTableLength()
		{
			return this.m_SlotInfoTable.Length;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000523C0 File Offset: 0x000505C0
		public LocalDataStoreMgr()
		{
		}

		// Token: 0x040014EC RID: 5356
		private const int InitialSlotTableSize = 64;

		// Token: 0x040014ED RID: 5357
		private const int SlotTableDoubleThreshold = 512;

		// Token: 0x040014EE RID: 5358
		private const int LargeSlotTableSizeIncrease = 128;

		// Token: 0x040014EF RID: 5359
		private bool[] m_SlotInfoTable = new bool[64];

		// Token: 0x040014F0 RID: 5360
		private int m_FirstAvailableSlot;

		// Token: 0x040014F1 RID: 5361
		private List<LocalDataStore> m_ManagedLocalDataStores = new List<LocalDataStore>();

		// Token: 0x040014F2 RID: 5362
		private Dictionary<string, LocalDataStoreSlot> m_KeyToSlotMap = new Dictionary<string, LocalDataStoreSlot>();

		// Token: 0x040014F3 RID: 5363
		private long m_CookieGenerator;
	}
}
