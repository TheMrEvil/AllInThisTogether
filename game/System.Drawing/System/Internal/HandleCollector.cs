using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Internal
{
	// Token: 0x0200000B RID: 11
	internal sealed class HandleCollector
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000E RID: 14 RVA: 0x0000208C File Offset: 0x0000028C
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000020C0 File Offset: 0x000002C0
		internal static event HandleChangeEventHandler HandleAdded
		{
			[CompilerGenerated]
			add
			{
				HandleChangeEventHandler handleChangeEventHandler = HandleCollector.HandleAdded;
				HandleChangeEventHandler handleChangeEventHandler2;
				do
				{
					handleChangeEventHandler2 = handleChangeEventHandler;
					HandleChangeEventHandler value2 = (HandleChangeEventHandler)Delegate.Combine(handleChangeEventHandler2, value);
					handleChangeEventHandler = Interlocked.CompareExchange<HandleChangeEventHandler>(ref HandleCollector.HandleAdded, value2, handleChangeEventHandler2);
				}
				while (handleChangeEventHandler != handleChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				HandleChangeEventHandler handleChangeEventHandler = HandleCollector.HandleAdded;
				HandleChangeEventHandler handleChangeEventHandler2;
				do
				{
					handleChangeEventHandler2 = handleChangeEventHandler;
					HandleChangeEventHandler value2 = (HandleChangeEventHandler)Delegate.Remove(handleChangeEventHandler2, value);
					handleChangeEventHandler = Interlocked.CompareExchange<HandleChangeEventHandler>(ref HandleCollector.HandleAdded, value2, handleChangeEventHandler2);
				}
				while (handleChangeEventHandler != handleChangeEventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000020F4 File Offset: 0x000002F4
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002128 File Offset: 0x00000328
		internal static event HandleChangeEventHandler HandleRemoved
		{
			[CompilerGenerated]
			add
			{
				HandleChangeEventHandler handleChangeEventHandler = HandleCollector.HandleRemoved;
				HandleChangeEventHandler handleChangeEventHandler2;
				do
				{
					handleChangeEventHandler2 = handleChangeEventHandler;
					HandleChangeEventHandler value2 = (HandleChangeEventHandler)Delegate.Combine(handleChangeEventHandler2, value);
					handleChangeEventHandler = Interlocked.CompareExchange<HandleChangeEventHandler>(ref HandleCollector.HandleRemoved, value2, handleChangeEventHandler2);
				}
				while (handleChangeEventHandler != handleChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				HandleChangeEventHandler handleChangeEventHandler = HandleCollector.HandleRemoved;
				HandleChangeEventHandler handleChangeEventHandler2;
				do
				{
					handleChangeEventHandler2 = handleChangeEventHandler;
					HandleChangeEventHandler value2 = (HandleChangeEventHandler)Delegate.Remove(handleChangeEventHandler2, value);
					handleChangeEventHandler = Interlocked.CompareExchange<HandleChangeEventHandler>(ref HandleCollector.HandleRemoved, value2, handleChangeEventHandler2);
				}
				while (handleChangeEventHandler != handleChangeEventHandler2);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000215B File Offset: 0x0000035B
		internal static IntPtr Add(IntPtr handle, int type)
		{
			HandleCollector.s_handleTypes[type - 1].Add(handle);
			return handle;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002170 File Offset: 0x00000370
		internal static int RegisterType(string typeName, int expense, int initialThreshold)
		{
			object obj = HandleCollector.s_internalSyncObject;
			int result;
			lock (obj)
			{
				if (HandleCollector.s_handleTypeCount == 0 || HandleCollector.s_handleTypeCount == HandleCollector.s_handleTypes.Length)
				{
					HandleCollector.HandleType[] destinationArray = new HandleCollector.HandleType[HandleCollector.s_handleTypeCount + 10];
					if (HandleCollector.s_handleTypes != null)
					{
						Array.Copy(HandleCollector.s_handleTypes, 0, destinationArray, 0, HandleCollector.s_handleTypeCount);
					}
					HandleCollector.s_handleTypes = destinationArray;
				}
				HandleCollector.s_handleTypes[HandleCollector.s_handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
				result = HandleCollector.s_handleTypeCount;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002210 File Offset: 0x00000410
		internal static IntPtr Remove(IntPtr handle, int type)
		{
			return HandleCollector.s_handleTypes[type - 1].Remove(handle);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002050 File Offset: 0x00000250
		public HandleCollector()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002221 File Offset: 0x00000421
		// Note: this type is marked as 'beforefieldinit'.
		static HandleCollector()
		{
		}

		// Token: 0x04000087 RID: 135
		private static HandleCollector.HandleType[] s_handleTypes;

		// Token: 0x04000088 RID: 136
		private static int s_handleTypeCount;

		// Token: 0x04000089 RID: 137
		[CompilerGenerated]
		private static HandleChangeEventHandler HandleAdded;

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		private static HandleChangeEventHandler HandleRemoved;

		// Token: 0x0400008B RID: 139
		private static object s_internalSyncObject = new object();

		// Token: 0x0200000C RID: 12
		private class HandleType
		{
			// Token: 0x06000017 RID: 23 RVA: 0x0000222D File Offset: 0x0000042D
			internal HandleType(string name, int expense, int initialThreshHold)
			{
				this.name = name;
				this._initialThreshHold = initialThreshHold;
				this._threshHold = initialThreshHold;
				this._deltaPercent = 100 - expense;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002254 File Offset: 0x00000454
			internal void Add(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return;
				}
				bool flag = false;
				int currentHandleCount = 0;
				lock (this)
				{
					this._handleCount++;
					flag = this.NeedCollection();
					currentHandleCount = this._handleCount;
				}
				object s_internalSyncObject = HandleCollector.s_internalSyncObject;
				lock (s_internalSyncObject)
				{
					HandleChangeEventHandler handleAdded = HandleCollector.HandleAdded;
					if (handleAdded != null)
					{
						handleAdded(this.name, handle, currentHandleCount);
					}
				}
				if (!flag)
				{
					return;
				}
				if (flag)
				{
					GC.Collect();
					Thread.Sleep((100 - this._deltaPercent) / 4);
				}
			}

			// Token: 0x06000019 RID: 25 RVA: 0x00002318 File Offset: 0x00000518
			internal bool NeedCollection()
			{
				if (this._handleCount > this._threshHold)
				{
					this._threshHold = this._handleCount + this._handleCount * this._deltaPercent / 100;
					return true;
				}
				int num = 100 * this._threshHold / (100 + this._deltaPercent);
				if (num >= this._initialThreshHold && this._handleCount < (int)((float)num * 0.9f))
				{
					this._threshHold = num;
				}
				return false;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002388 File Offset: 0x00000588
			internal IntPtr Remove(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return handle;
				}
				int currentHandleCount = 0;
				lock (this)
				{
					this._handleCount--;
					if (this._handleCount < 0)
					{
						this._handleCount = 0;
					}
					currentHandleCount = this._handleCount;
				}
				object s_internalSyncObject = HandleCollector.s_internalSyncObject;
				lock (s_internalSyncObject)
				{
					HandleChangeEventHandler handleRemoved = HandleCollector.HandleRemoved;
					if (handleRemoved != null)
					{
						handleRemoved(this.name, handle, currentHandleCount);
					}
				}
				return handle;
			}

			// Token: 0x0400008C RID: 140
			internal readonly string name;

			// Token: 0x0400008D RID: 141
			private int _initialThreshHold;

			// Token: 0x0400008E RID: 142
			private int _threshHold;

			// Token: 0x0400008F RID: 143
			private int _handleCount;

			// Token: 0x04000090 RID: 144
			private readonly int _deltaPercent;
		}
	}
}
