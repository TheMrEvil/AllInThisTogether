using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal class ReadOnlyDictionaryInternal<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000121 RID: 289 RVA: 0x0000571C File Offset: 0x0000391C
		public ReadOnlyDictionaryInternal(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000572B File Offset: 0x0000392B
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005738 File Offset: 0x00003938
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000573B File Offset: 0x0000393B
		public ICollection<TKey> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005748 File Offset: 0x00003948
		public ICollection<TValue> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x1700002D RID: 45
		public TValue this[TKey key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				throw Fx.Exception.AsError(this.CreateReadOnlyException());
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005775 File Offset: 0x00003975
		public static IDictionary<TKey, TValue> Create(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary.IsReadOnly)
			{
				return dictionary;
			}
			return new ReadOnlyDictionaryInternal<TKey, TValue>(dictionary);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005787 File Offset: 0x00003987
		private Exception CreateReadOnlyException()
		{
			return new InvalidOperationException("Dictionary Is Read Only");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005793 File Offset: 0x00003993
		public void Add(TKey key, TValue value)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000057A5 File Offset: 0x000039A5
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000057B7 File Offset: 0x000039B7
		public void Clear()
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000057C9 File Offset: 0x000039C9
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.dictionary.Contains(item);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000057D7 File Offset: 0x000039D7
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000057E5 File Offset: 0x000039E5
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000057F4 File Offset: 0x000039F4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005801 File Offset: 0x00003A01
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005809 File Offset: 0x00003A09
		public bool Remove(TKey key)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000581B File Offset: 0x00003A1B
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw Fx.Exception.AsError(this.CreateReadOnlyException());
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000582D File Offset: 0x00003A2D
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x040000C7 RID: 199
		private IDictionary<TKey, TValue> dictionary;
	}
}
