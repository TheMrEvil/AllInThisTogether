using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public abstract class ES3SerializableDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISerializationCallbackReceiver
	{
		// Token: 0x06000496 RID: 1174
		protected abstract bool KeysAreEqual(TKey a, TKey b);

		// Token: 0x06000497 RID: 1175
		protected abstract bool ValuesAreEqual(TVal a, TVal b);

		// Token: 0x06000498 RID: 1176 RVA: 0x0001D58C File Offset: 0x0001B78C
		public void OnBeforeSerialize()
		{
			this._Keys = new List<TKey>();
			this._Values = new List<TVal>();
			foreach (KeyValuePair<TKey, TVal> keyValuePair in this)
			{
				try
				{
					this._Keys.Add(keyValuePair.Key);
					this._Values.Add(keyValuePair.Value);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001D620 File Offset: 0x0001B820
		public void OnAfterDeserialize()
		{
			if (this._Keys == null || this._Values == null)
			{
				return;
			}
			if (this._Keys.Count != this._Values.Count)
			{
				throw new Exception(string.Format("Key count is different to value count after deserialising dictionary.", Array.Empty<object>()));
			}
			base.Clear();
			for (int i = 0; i < this._Keys.Count; i++)
			{
				if (this._Keys[i] != null)
				{
					try
					{
						base.Add(this._Keys[i], this._Values[i]);
					}
					catch
					{
					}
				}
			}
			this._Keys = null;
			this._Values = null;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public int RemoveNullValues()
		{
			List<TKey> list = (from pair in this
			where pair.Value == null
			select pair.Key).ToList<TKey>();
			foreach (TKey key in list)
			{
				base.Remove(key);
			}
			return list.Count;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001D780 File Offset: 0x0001B980
		public bool ChangeKey(TKey oldKey, TKey newKey)
		{
			if (this.KeysAreEqual(oldKey, newKey))
			{
				return false;
			}
			TVal value = base[oldKey];
			base.Remove(oldKey);
			base[newKey] = value;
			return true;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001D7B2 File Offset: 0x0001B9B2
		protected ES3SerializableDictionary()
		{
		}

		// Token: 0x0400014D RID: 333
		[SerializeField]
		private List<TKey> _Keys;

		// Token: 0x0400014E RID: 334
		[SerializeField]
		private List<TVal> _Values;

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C8 RID: 1480 RVA: 0x00020894 File Offset: 0x0001EA94
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x000208A0 File Offset: 0x0001EAA0
			public <>c()
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x000208A8 File Offset: 0x0001EAA8
			internal bool <RemoveNullValues>b__6_0(KeyValuePair<TKey, TVal> pair)
			{
				return pair.Value == null;
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x000208B9 File Offset: 0x0001EAB9
			internal TKey <RemoveNullValues>b__6_1(KeyValuePair<TKey, TVal> pair)
			{
				return pair.Key;
			}

			// Token: 0x04000214 RID: 532
			public static readonly ES3SerializableDictionary<TKey, TVal>.<>c <>9 = new ES3SerializableDictionary<TKey, TVal>.<>c();

			// Token: 0x04000215 RID: 533
			public static Func<KeyValuePair<TKey, TVal>, bool> <>9__6_0;

			// Token: 0x04000216 RID: 534
			public static Func<KeyValuePair<TKey, TVal>, TKey> <>9__6_1;
		}
	}
}
