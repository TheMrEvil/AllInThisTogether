using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LeTai
{
	// Token: 0x02000005 RID: 5
	public static class Utility
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000250C File Offset: 0x0000070C
		public static void LogList<T>(IEnumerable<T> list, Func<T, object> getData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (T arg in list)
			{
				stringBuilder.Append(num.ToString() + ":    ");
				stringBuilder.Append(getData(arg).ToString());
				stringBuilder.Append("\n");
				num++;
			}
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000259C File Offset: 0x0000079C
		public static int SimplePingPong(int t, int max)
		{
			if (t > max)
			{
				return 2 * max - t;
			}
			return t;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025AC File Offset: 0x000007AC
		public static void SafeDestroy(UnityEngine.Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					GameObject gameObject = obj as GameObject;
					if (gameObject != null)
					{
						gameObject.transform.parent = null;
					}
					UnityEngine.Object.Destroy(obj);
					return;
				}
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}
}
