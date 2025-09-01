using System;

namespace System.Collections.Generic
{
	/// <summary>Defines a key/value pair that can be set or retrieved.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x02000AA2 RID: 2722
	[Serializable]
	public readonly struct KeyValuePair<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structure with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		// Token: 0x0600613D RID: 24893 RVA: 0x00145267 File Offset: 0x00143467
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		/// <summary>Gets the key in the key/value pair.</summary>
		/// <returns>A <typeparamref name="TKey" /> that is the key of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />.</returns>
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600613E RID: 24894 RVA: 0x00145277 File Offset: 0x00143477
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the value in the key/value pair.</summary>
		/// <returns>A <typeparamref name="TValue" /> that is the value of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />.</returns>
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600613F RID: 24895 RVA: 0x0014527F File Offset: 0x0014347F
		public TValue Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
		/// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
		// Token: 0x06006140 RID: 24896 RVA: 0x00145287 File Offset: 0x00143487
		public override string ToString()
		{
			return KeyValuePair.PairToString(this.Key, this.Value);
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x001452A4 File Offset: 0x001434A4
		public void Deconstruct(out TKey key, out TValue value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x040039E6 RID: 14822
		private readonly TKey key;

		// Token: 0x040039E7 RID: 14823
		private readonly TValue value;
	}
}
