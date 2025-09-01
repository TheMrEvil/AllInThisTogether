using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x0200011E RID: 286
	internal static class BeforeRenderHelper
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0000BB74 File Offset: 0x00009D74
		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), true);
			BeforeRenderOrderAttribute beforeRenderOrderAttribute = (customAttributes != null && customAttributes.Length != 0) ? (customAttributes[0] as BeforeRenderOrderAttribute) : null;
			return (beforeRenderOrderAttribute != null) ? beforeRenderOrderAttribute.order : 0;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock value = BeforeRenderHelper.s_OrderBlocks[num];
						value.callback = (UnityAction)Delegate.Combine(value.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = value;
						return;
					}
					num++;
				}
				BeforeRenderHelper.OrderBlock item = default(BeforeRenderHelper.OrderBlock);
				item.order = updateOrder;
				item.callback = (UnityAction)Delegate.Combine(item.callback, callback);
				BeforeRenderHelper.s_OrderBlocks.Insert(num, item);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Remove(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						bool flag2 = orderBlock.callback == null;
						if (flag2)
						{
							BeforeRenderHelper.s_OrderBlocks.RemoveAt(num);
						}
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000BD90 File Offset: 0x00009F90
		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag = callback != null;
					if (flag)
					{
						callback();
					}
				}
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000BE04 File Offset: 0x0000A004
		// Note: this type is marked as 'beforefieldinit'.
		static BeforeRenderHelper()
		{
		}

		// Token: 0x040003A3 RID: 931
		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		// Token: 0x0200011F RID: 287
		private struct OrderBlock
		{
			// Token: 0x040003A4 RID: 932
			internal int order;

			// Token: 0x040003A5 RID: 933
			internal UnityAction callback;
		}
	}
}
