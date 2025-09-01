using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005A2 RID: 1442
	[ComVisible(true)]
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060037FC RID: 14332 RVA: 0x000C91CC File Offset: 0x000C73CC
		public AggregateDictionary(IDictionary[] dics)
		{
			this.dictionaries = dics;
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007DB RID: 2011
		public object this[object key]
		{
			get
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000C9214 File Offset: 0x000C7414
		public ICollection Keys
		{
			get
			{
				if (this._keys != null)
				{
					return this._keys;
				}
				this._keys = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._keys.AddRange(dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x000C926C File Offset: 0x000C746C
		public ICollection Values
		{
			get
			{
				if (this._values != null)
				{
					return this._values;
				}
				this._values = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._values.AddRange(dictionary.Values);
				}
				return this._values;
			}
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000472C8 File Offset: 0x000454C8
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000472C8 File Offset: 0x000454C8
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000C92C4 File Offset: 0x000C74C4
		public bool Contains(object ob)
		{
			IDictionary[] array = this.dictionaries;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(ob))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000C92F4 File Offset: 0x000C74F4
		public IDictionaryEnumerator GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000C92F4 File Offset: 0x000C74F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000472C8 File Offset: 0x000454C8
		public void Remove(object ob)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000C9304 File Offset: 0x000C7504
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000C935C File Offset: 0x000C755C
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IDictionary dictionary in this.dictionaries)
				{
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x0000270D File Offset: 0x0000090D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040025C7 RID: 9671
		private IDictionary[] dictionaries;

		// Token: 0x040025C8 RID: 9672
		private ArrayList _values;

		// Token: 0x040025C9 RID: 9673
		private ArrayList _keys;
	}
}
