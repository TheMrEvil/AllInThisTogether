using System;
using System.ComponentModel;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D9 RID: 217
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ES3RefIdDictionary : ES3SerializableDictionary<UnityEngine.Object, long>
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0001C41D File Offset: 0x0001A61D
		protected override bool KeysAreEqual(UnityEngine.Object a, UnityEngine.Object b)
		{
			return a == b;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001C426 File Offset: 0x0001A626
		protected override bool ValuesAreEqual(long a, long b)
		{
			return a == b;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001C42C File Offset: 0x0001A62C
		public ES3RefIdDictionary()
		{
		}
	}
}
