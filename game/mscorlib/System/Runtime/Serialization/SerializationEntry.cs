using System;

namespace System.Runtime.Serialization
{
	/// <summary>Holds the value, <see cref="T:System.Type" />, and name of a serialized object.</summary>
	// Token: 0x0200064C RID: 1612
	public readonly struct SerializationEntry
	{
		// Token: 0x06003C54 RID: 15444 RVA: 0x000D14B0 File Offset: 0x000CF6B0
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this._name = entryName;
			this._value = entryValue;
			this._type = entryType;
		}

		/// <summary>Gets the value contained in the object.</summary>
		/// <returns>The value contained in the object.</returns>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000D14C7 File Offset: 0x000CF6C7
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Gets the name of the object.</summary>
		/// <returns>The name of the object.</returns>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000D14CF File Offset: 0x000CF6CF
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000D14D7 File Offset: 0x000CF6D7
		public Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04002716 RID: 10006
		private readonly string _name;

		// Token: 0x04002717 RID: 10007
		private readonly object _value;

		// Token: 0x04002718 RID: 10008
		private readonly Type _type;
	}
}
