using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	internal class GUIStateObjects
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000B4C0 File Offset: 0x000096C0
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			bool flag = !GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t;
			if (flag)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B50C File Offset: 0x0000970C
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			bool flag = t.IsInstanceOfType(obj);
			object result;
			if (flag)
			{
				result = obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B53B File Offset: 0x0000973B
		internal static void Tests_ClearObjects()
		{
			GUIStateObjects.s_StateCache.Clear();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUIStateObjects()
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B549 File Offset: 0x00009749
		// Note: this type is marked as 'beforefieldinit'.
		static GUIStateObjects()
		{
		}

		// Token: 0x040000C8 RID: 200
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
