using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000654 RID: 1620
	[Serializable]
	internal class PathList
	{
		// Token: 0x0600330D RID: 13069 RVA: 0x000B2330 File Offset: 0x000B0530
		public PathList()
		{
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000B234D File Offset: 0x000B054D
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000B235C File Offset: 0x000B055C
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000B23EC File Offset: 0x000B05EC
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x17000A4F RID: 2639
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000B2450 File Offset: 0x000B0650
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000B245D File Offset: 0x000B065D
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x04001DF9 RID: 7673
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x02000655 RID: 1621
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x06003315 RID: 13077 RVA: 0x000B246C File Offset: 0x000B066C
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x06003316 RID: 13078 RVA: 0x0000219B File Offset: 0x0000039B
			public PathListComparer()
			{
			}

			// Token: 0x06003317 RID: 13079 RVA: 0x000B24E0 File Offset: 0x000B06E0
			// Note: this type is marked as 'beforefieldinit'.
			static PathListComparer()
			{
			}

			// Token: 0x04001DFA RID: 7674
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}
