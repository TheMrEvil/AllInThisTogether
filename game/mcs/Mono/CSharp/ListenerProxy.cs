using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000177 RID: 375
	internal static class ListenerProxy
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x0004A7AC File Offset: 0x000489AC
		public static int Register(ValueModificationHandler listener)
		{
			Dictionary<int, ValueModificationHandler> obj = ListenerProxy.listeners;
			int result;
			lock (obj)
			{
				int num = ListenerProxy.counter++;
				ListenerProxy.listeners.Add(num, listener);
				result = num;
			}
			return result;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0004A7FC File Offset: 0x000489FC
		public static void Unregister(int listenerId)
		{
			Dictionary<int, ValueModificationHandler> obj = ListenerProxy.listeners;
			lock (obj)
			{
				ListenerProxy.listeners.Remove(listenerId);
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004A83C File Offset: 0x00048A3C
		public static void ValueChanged(object value, int row, int col, string name, int listenerId)
		{
			Dictionary<int, ValueModificationHandler> obj = ListenerProxy.listeners;
			ValueModificationHandler valueModificationHandler;
			lock (obj)
			{
				if (!ListenerProxy.listeners.TryGetValue(listenerId, out valueModificationHandler))
				{
					return;
				}
			}
			valueModificationHandler(name, row, col, value);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004A88C File Offset: 0x00048A8C
		// Note: this type is marked as 'beforefieldinit'.
		static ListenerProxy()
		{
		}

		// Token: 0x040007A7 RID: 1959
		private static readonly Dictionary<int, ValueModificationHandler> listeners = new Dictionary<int, ValueModificationHandler>();

		// Token: 0x040007A8 RID: 1960
		private static int counter;
	}
}
