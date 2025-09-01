using System;

namespace System.Collections
{
	/// <summary>Defines a dictionary key/value pair that can be set or retrieved.</summary>
	// Token: 0x02000A10 RID: 2576
	[Serializable]
	public struct DictionaryEntry
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Collections.DictionaryEntry" /> type with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" /> and the .NET Framework version is 1.0 or 1.1.</exception>
		// Token: 0x06005B73 RID: 23411 RVA: 0x00134B6E File Offset: 0x00132D6E
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		/// <summary>Gets or sets the key in the key/value pair.</summary>
		/// <returns>The key in the key/value pair.</returns>
		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x00134B7E File Offset: 0x00132D7E
		// (set) Token: 0x06005B75 RID: 23413 RVA: 0x00134B86 File Offset: 0x00132D86
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the value in the key/value pair.</summary>
		/// <returns>The value in the key/value pair.</returns>
		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005B76 RID: 23414 RVA: 0x00134B8F File Offset: 0x00132D8F
		// (set) Token: 0x06005B77 RID: 23415 RVA: 0x00134B97 File Offset: 0x00132D97
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x00134BA0 File Offset: 0x00132DA0
		public void Deconstruct(out object key, out object value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04003867 RID: 14439
		private object _key;

		// Token: 0x04003868 RID: 14440
		private object _value;
	}
}
