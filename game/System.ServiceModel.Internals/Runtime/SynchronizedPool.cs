using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200002C RID: 44
	internal class SynchronizedPool<T> where T : class
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000059BC File Offset: 0x00003BBC
		public SynchronizedPool(int maxCount)
		{
			int num = maxCount;
			int num2 = 16 + SynchronizedPool<T>.SynchronizedPoolHelper.ProcessorCount;
			if (num > num2)
			{
				num = num2;
			}
			this.maxCount = maxCount;
			this.entries = new SynchronizedPool<T>.Entry[num];
			this.pending = new SynchronizedPool<T>.PendingEntry[4];
			this.globalPool = new SynchronizedPool<T>.GlobalPool(maxCount);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005A0B File Offset: 0x00003C0B
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005A10 File Offset: 0x00003C10
		public void Clear()
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].value = default(T);
			}
			this.globalPool.Clear();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005A50 File Offset: 0x00003C50
		private void HandlePromotionFailure(int thisThreadID)
		{
			int num = this.promotionFailures + 1;
			if (num >= 64)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.entries = new SynchronizedPool<T>.Entry[this.entries.Length];
					this.globalPool.MaxCount = this.maxCount;
				}
				this.PromoteThread(thisThreadID);
				return;
			}
			this.promotionFailures = num;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005ACC File Offset: 0x00003CCC
		private bool PromoteThread(int thisThreadID)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				for (int i = 0; i < this.entries.Length; i++)
				{
					int threadID = this.entries[i].threadID;
					if (threadID == thisThreadID)
					{
						return true;
					}
					if (threadID == 0)
					{
						this.globalPool.DecrementMaxCount();
						this.entries[i].threadID = thisThreadID;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005B60 File Offset: 0x00003D60
		private void RecordReturnToGlobalPool(int thisThreadID)
		{
			SynchronizedPool<T>.PendingEntry[] array = this.pending;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					int num = array[i].returnCount + 1;
					if (num < 64)
					{
						array[i].returnCount = num;
						return;
					}
					array[i].returnCount = 0;
					if (!this.PromoteThread(thisThreadID))
					{
						this.HandlePromotionFailure(thisThreadID);
						return;
					}
					break;
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005BD8 File Offset: 0x00003DD8
		private void RecordTakeFromGlobalPool(int thisThreadID)
		{
			SynchronizedPool<T>.PendingEntry[] array = this.pending;
			for (int i = 0; i < array.Length; i++)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					return;
				}
				if (threadID == 0)
				{
					SynchronizedPool<T>.PendingEntry[] obj = array;
					lock (obj)
					{
						if (array[i].threadID == 0)
						{
							array[i].threadID = thisThreadID;
							return;
						}
					}
				}
			}
			if (array.Length >= 128)
			{
				this.pending = new SynchronizedPool<T>.PendingEntry[array.Length];
				return;
			}
			SynchronizedPool<T>.PendingEntry[] destinationArray = new SynchronizedPool<T>.PendingEntry[array.Length * 2];
			Array.Copy(array, destinationArray, array.Length);
			this.pending = destinationArray;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005C90 File Offset: 0x00003E90
		public bool Return(T value)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			return managedThreadId != 0 && (this.ReturnToPerThreadPool(managedThreadId, value) || this.ReturnToGlobalPool(managedThreadId, value));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005CC4 File Offset: 0x00003EC4
		private bool ReturnToPerThreadPool(int thisThreadID, T value)
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					if (array[i].value == null)
					{
						array[i].value = value;
						return true;
					}
					return false;
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005D1F File Offset: 0x00003F1F
		private bool ReturnToGlobalPool(int thisThreadID, T value)
		{
			this.RecordReturnToGlobalPool(thisThreadID);
			return this.globalPool.Return(value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D34 File Offset: 0x00003F34
		public T Take()
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (managedThreadId == 0)
			{
				return default(T);
			}
			T t = this.TakeFromPerThreadPool(managedThreadId);
			if (t != null)
			{
				return t;
			}
			return this.TakeFromGlobalPool(managedThreadId);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D74 File Offset: 0x00003F74
		private T TakeFromPerThreadPool(int thisThreadID)
		{
			SynchronizedPool<T>.Entry[] array = this.entries;
			int i = 0;
			while (i < array.Length)
			{
				int threadID = array[i].threadID;
				if (threadID == thisThreadID)
				{
					T value = array[i].value;
					if (value != null)
					{
						array[i].value = default(T);
						return value;
					}
					return default(T);
				}
				else
				{
					if (threadID == 0)
					{
						break;
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005DE8 File Offset: 0x00003FE8
		private T TakeFromGlobalPool(int thisThreadID)
		{
			this.RecordTakeFromGlobalPool(thisThreadID);
			return this.globalPool.Take();
		}

		// Token: 0x040000CC RID: 204
		private const int maxPendingEntries = 128;

		// Token: 0x040000CD RID: 205
		private const int maxPromotionFailures = 64;

		// Token: 0x040000CE RID: 206
		private const int maxReturnsBeforePromotion = 64;

		// Token: 0x040000CF RID: 207
		private const int maxThreadItemsPerProcessor = 16;

		// Token: 0x040000D0 RID: 208
		private SynchronizedPool<T>.Entry[] entries;

		// Token: 0x040000D1 RID: 209
		private SynchronizedPool<T>.GlobalPool globalPool;

		// Token: 0x040000D2 RID: 210
		private int maxCount;

		// Token: 0x040000D3 RID: 211
		private SynchronizedPool<T>.PendingEntry[] pending;

		// Token: 0x040000D4 RID: 212
		private int promotionFailures;

		// Token: 0x02000081 RID: 129
		private struct Entry
		{
			// Token: 0x040002BB RID: 699
			public int threadID;

			// Token: 0x040002BC RID: 700
			public T value;
		}

		// Token: 0x02000082 RID: 130
		private struct PendingEntry
		{
			// Token: 0x040002BD RID: 701
			public int returnCount;

			// Token: 0x040002BE RID: 702
			public int threadID;
		}

		// Token: 0x02000083 RID: 131
		private static class SynchronizedPoolHelper
		{
			// Token: 0x060003DF RID: 991 RVA: 0x0001262B File Offset: 0x0001082B
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Read = "NUMBER_OF_PROCESSORS")]
			private static int GetProcessorCount()
			{
				return Environment.ProcessorCount;
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x00012632 File Offset: 0x00010832
			// Note: this type is marked as 'beforefieldinit'.
			static SynchronizedPoolHelper()
			{
			}

			// Token: 0x040002BF RID: 703
			public static readonly int ProcessorCount = SynchronizedPool<T>.SynchronizedPoolHelper.GetProcessorCount();
		}

		// Token: 0x02000084 RID: 132
		private class GlobalPool
		{
			// Token: 0x060003E1 RID: 993 RVA: 0x0001263E File Offset: 0x0001083E
			public GlobalPool(int maxCount)
			{
				this.items = new Stack<T>();
				this.maxCount = maxCount;
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x00012658 File Offset: 0x00010858
			// (set) Token: 0x060003E3 RID: 995 RVA: 0x00012660 File Offset: 0x00010860
			public int MaxCount
			{
				get
				{
					return this.maxCount;
				}
				set
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						while (this.items.Count > value)
						{
							this.items.Pop();
						}
						this.maxCount = value;
					}
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x000126C0 File Offset: 0x000108C0
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x000126C4 File Offset: 0x000108C4
			public void DecrementMaxCount()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.items.Count == this.maxCount)
					{
						this.items.Pop();
					}
					this.maxCount--;
				}
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x0001272C File Offset: 0x0001092C
			public T Take()
			{
				if (this.items.Count > 0)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.items.Count > 0)
						{
							return this.items.Pop();
						}
					}
				}
				return default(T);
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x0001279C File Offset: 0x0001099C
			public bool Return(T value)
			{
				if (this.items.Count < this.MaxCount)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.items.Count < this.MaxCount)
						{
							this.items.Push(value);
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x00012810 File Offset: 0x00010A10
			public void Clear()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.items.Clear();
				}
			}

			// Token: 0x040002C0 RID: 704
			private Stack<T> items;

			// Token: 0x040002C1 RID: 705
			private int maxCount;
		}
	}
}
