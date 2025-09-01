using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Caching
{
	// Token: 0x0200000D RID: 13
	public class Cache<T> where T : Component
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002466 File Offset: 0x00000666
		public static Cache<T> Instance
		{
			get
			{
				if (Cache<T>.instance == null)
				{
					Cache<T>.instance = new Cache<T>();
				}
				return Cache<T>.instance;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000247E File Offset: 0x0000067E
		private Cache()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002491 File Offset: 0x00000691
		public void RegisterItem(T item)
		{
			this.store.Add(item.gameObject.GetInstanceID(), item);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024AF File Offset: 0x000006AF
		public bool IsEmpty()
		{
			return this.store.Count == 0;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024C0 File Offset: 0x000006C0
		public void StoreItems(IEnumerable<T> items)
		{
			this.store.Clear();
			foreach (T t in items)
			{
				this.store.Add(t.gameObject.GetInstanceID(), t);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002528 File Offset: 0x00000728
		public ICollection<T> FindAll()
		{
			return this.store.Values;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002535 File Offset: 0x00000735
		public bool RemoveItem(T item)
		{
			return this.store.Remove(item.gameObject.GetInstanceID());
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002554 File Offset: 0x00000754
		[CanBeNull]
		public T FindOrInsert(Component obj)
		{
			if (obj == null)
			{
				return default(T);
			}
			int instanceID = obj.GetInstanceID();
			T result;
			if (this.store.TryGetValue(instanceID, out result))
			{
				return result;
			}
			this.store.Add(instanceID, obj.GetComponent<T>());
			return this.store[instanceID];
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025AB File Offset: 0x000007AB
		public void Purge()
		{
			this.store.Clear();
		}

		// Token: 0x0400001B RID: 27
		private static Cache<T> instance;

		// Token: 0x0400001C RID: 28
		private readonly IDictionary<int, T> store = new Dictionary<int, T>();
	}
}
