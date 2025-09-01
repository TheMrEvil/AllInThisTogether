using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Runtime.Collections
{
	// Token: 0x02000055 RID: 85
	internal class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000106C6 File Offset: 0x0000E8C6
		public OrderedDictionary()
		{
			this.privateDictionary = new OrderedDictionary();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000106DC File Offset: 0x0000E8DC
		public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary != null)
			{
				this.privateDictionary = new OrderedDictionary();
				foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
				{
					this.privateDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00010754 File Offset: 0x0000E954
		public int Count
		{
			get
			{
				return this.privateDictionary.Count;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00010761 File Offset: 0x0000E961
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006E RID: 110
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw Fx.Exception.ArgumentNull("key");
				}
				if (this.privateDictionary.Contains(key))
				{
					return (TValue)((object)this.privateDictionary[key]);
				}
				throw Fx.Exception.AsError(new KeyNotFoundException("Key Not Found In Dictionary"));
			}
			set
			{
				if (key == null)
				{
					throw Fx.Exception.ArgumentNull("key");
				}
				this.privateDictionary[key] = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public ICollection<TKey> Keys
		{
			get
			{
				List<TKey> list = new List<TKey>(this.privateDictionary.Count);
				foreach (object obj in this.privateDictionary.Keys)
				{
					TKey item = (TKey)((object)obj);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00010868 File Offset: 0x0000EA68
		public ICollection<TValue> Values
		{
			get
			{
				List<TValue> list = new List<TValue>(this.privateDictionary.Count);
				foreach (object obj in this.privateDictionary.Values)
				{
					TValue item = (TValue)((object)obj);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000108EE File Offset: 0x0000EAEE
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			this.privateDictionary.Add(key, value);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001091F File Offset: 0x0000EB1F
		public void Clear()
		{
			this.privateDictionary.Clear();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001092C File Offset: 0x0000EB2C
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return item.Key != null && this.privateDictionary.Contains(item.Key) && this.privateDictionary[item.Key].Equals(item.Value);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001098A File Offset: 0x0000EB8A
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			return this.privateDictionary.Contains(key);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000109B8 File Offset: 0x0000EBB8
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw Fx.Exception.ArgumentNull("array");
			}
			if (arrayIndex < 0)
			{
				throw Fx.Exception.AsError(new ArgumentOutOfRangeException("arrayIndex"));
			}
			if (array.Rank > 1 || arrayIndex >= array.Length || array.Length - arrayIndex < this.privateDictionary.Count)
			{
				throw Fx.Exception.Argument("array", "Bad Copy To Array");
			}
			int num = arrayIndex;
			foreach (object obj in this.privateDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num] = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
				num++;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010A98 File Offset: 0x0000EC98
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (object obj in this.privateDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
			}
			IDictionaryEnumerator dictionaryEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010AA7 File Offset: 0x0000ECA7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010AAF File Offset: 0x0000ECAF
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this.Contains(item))
			{
				this.privateDictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010AD4 File Offset: 0x0000ECD4
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			if (this.privateDictionary.Contains(key))
			{
				this.privateDictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010B20 File Offset: 0x0000ED20
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw Fx.Exception.ArgumentNull("key");
			}
			bool flag = this.privateDictionary.Contains(key);
			value = (flag ? ((TValue)((object)this.privateDictionary[key])) : default(TValue));
			return flag;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010B82 File Offset: 0x0000ED82
		void IDictionary.Add(object key, object value)
		{
			this.privateDictionary.Add(key, value);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010B91 File Offset: 0x0000ED91
		void IDictionary.Clear()
		{
			this.privateDictionary.Clear();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010B9E File Offset: 0x0000ED9E
		bool IDictionary.Contains(object key)
		{
			return this.privateDictionary.Contains(key);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010BAC File Offset: 0x0000EDAC
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.privateDictionary.GetEnumerator();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00010BB9 File Offset: 0x0000EDB9
		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IDictionary)this.privateDictionary).IsFixedSize;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00010BC6 File Offset: 0x0000EDC6
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.privateDictionary.IsReadOnly;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00010BD3 File Offset: 0x0000EDD3
		ICollection IDictionary.Keys
		{
			get
			{
				return this.privateDictionary.Keys;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		void IDictionary.Remove(object key)
		{
			this.privateDictionary.Remove(key);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00010BEE File Offset: 0x0000EDEE
		ICollection IDictionary.Values
		{
			get
			{
				return this.privateDictionary.Values;
			}
		}

		// Token: 0x17000075 RID: 117
		object IDictionary.this[object key]
		{
			get
			{
				return this.privateDictionary[key];
			}
			set
			{
				this.privateDictionary[key] = value;
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010C18 File Offset: 0x0000EE18
		void ICollection.CopyTo(Array array, int index)
		{
			this.privateDictionary.CopyTo(array, index);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00010C27 File Offset: 0x0000EE27
		int ICollection.Count
		{
			get
			{
				return this.privateDictionary.Count;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010C34 File Offset: 0x0000EE34
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.privateDictionary).IsSynchronized;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00010C41 File Offset: 0x0000EE41
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.privateDictionary).SyncRoot;
			}
		}

		// Token: 0x04000204 RID: 516
		private OrderedDictionary privateDictionary;

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__20 : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x0600043F RID: 1087 RVA: 0x000135A0 File Offset: 0x000117A0
			[DebuggerHidden]
			public <GetEnumerator>d__20(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x000135B0 File Offset: 0x000117B0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x000135E8 File Offset: 0x000117E8
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					OrderedDictionary<TKey, TValue> orderedDictionary = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						dictionaryEnumerator = orderedDictionary.privateDictionary.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!dictionaryEnumerator.MoveNext())
					{
						this.<>m__Finally1();
						dictionaryEnumerator = null;
						result = false;
					}
					else
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)dictionaryEnumerator.Current;
						this.<>2__current = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x000136B0 File Offset: 0x000118B0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = dictionaryEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x000136D9 File Offset: 0x000118D9
			KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<!0, !1>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x000136E1 File Offset: 0x000118E1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x000136E8 File Offset: 0x000118E8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000306 RID: 774
			private int <>1__state;

			// Token: 0x04000307 RID: 775
			private KeyValuePair<TKey, TValue> <>2__current;

			// Token: 0x04000308 RID: 776
			public OrderedDictionary<TKey, TValue> <>4__this;

			// Token: 0x04000309 RID: 777
			private IDictionaryEnumerator <>7__wrap1;
		}
	}
}
