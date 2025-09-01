using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public class ES3IdRefDictionary : ES3SerializableDictionary<long, UnityEngine.Object>
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0001C406 File Offset: 0x0001A606
		protected override bool KeysAreEqual(long a, long b)
		{
			return a == b;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001C40C File Offset: 0x0001A60C
		protected override bool ValuesAreEqual(UnityEngine.Object a, UnityEngine.Object b)
		{
			return a == b;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001C415 File Offset: 0x0001A615
		public ES3IdRefDictionary()
		{
		}
	}
}
