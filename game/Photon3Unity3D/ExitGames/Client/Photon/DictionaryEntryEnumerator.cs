using System;
using System.Collections;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000006 RID: 6
	public struct DictionaryEntryEnumerator : IEnumerator<DictionaryEntry>, IEnumerator, IDisposable
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000031F4 File Offset: 0x000013F4
		public DictionaryEntryEnumerator(Dictionary<object, object>.Enumerator original)
		{
			this.enumerator = original;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003200 File Offset: 0x00001400
		object IEnumerator.Current
		{
			get
			{
				KeyValuePair<object, object> keyValuePair = this.enumerator.Current;
				object key = keyValuePair.Key;
				keyValuePair = this.enumerator.Current;
				return new DictionaryEntry(key, keyValuePair.Value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003244 File Offset: 0x00001444
		public DictionaryEntry Current
		{
			get
			{
				KeyValuePair<object, object> keyValuePair = this.enumerator.Current;
				object key = keyValuePair.Key;
				keyValuePair = this.enumerator.Current;
				return new DictionaryEntry(key, keyValuePair.Value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003284 File Offset: 0x00001484
		public object Key
		{
			get
			{
				KeyValuePair<object, object> keyValuePair = this.enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000032AC File Offset: 0x000014AC
		public object Value
		{
			get
			{
				KeyValuePair<object, object> keyValuePair = this.enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000032D4 File Offset: 0x000014D4
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032F1 File Offset: 0x000014F1
		public void Reset()
		{
			((IEnumerator)this.enumerator).Reset();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003305 File Offset: 0x00001505
		public void Dispose()
		{
		}

		// Token: 0x04000014 RID: 20
		private Dictionary<object, object>.Enumerator enumerator;
	}
}
