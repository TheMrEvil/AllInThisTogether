using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020004BE RID: 1214
	internal class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x00088955 File Offset: 0x00086B55
		internal GenericAdapter(StringDictionary stringDictionary)
		{
			this.m_stringDictionary = stringDictionary;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00088964 File Offset: 0x00086B64
		public void Add(string key, string value)
		{
			this[key] = value;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0008896E File Offset: 0x00086B6E
		public bool ContainsKey(string key)
		{
			return this.m_stringDictionary.ContainsKey(key);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0008897C File Offset: 0x00086B7C
		public void Clear()
		{
			this.m_stringDictionary.Clear();
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x00088989 File Offset: 0x00086B89
		public int Count
		{
			get
			{
				return this.m_stringDictionary.Count;
			}
		}

		// Token: 0x17000806 RID: 2054
		public string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					throw new KeyNotFoundException();
				}
				return this.m_stringDictionary[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.m_stringDictionary[key] = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000889E3 File Offset: 0x00086BE3
		public ICollection<string> Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Key);
				}
				return this._keys;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00088A05 File Offset: 0x00086C05
		public ICollection<string> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, GenericAdapter.KeyOrValue.Value);
				}
				return this._values;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00088A27 File Offset: 0x00086C27
		public bool Remove(string key)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				return false;
			}
			this.m_stringDictionary.Remove(key);
			return true;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00088A46 File Offset: 0x00086C46
		public bool TryGetValue(string key, out string value)
		{
			if (!this.m_stringDictionary.ContainsKey(key))
			{
				value = null;
				return false;
			}
			value = this.m_stringDictionary[key];
			return true;
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00088A6A File Offset: 0x00086C6A
		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			this.m_stringDictionary.Add(item.Key, item.Value);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00088A88 File Offset: 0x00086C88
		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
		{
			string text;
			return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00088AB8 File Offset: 0x00086CB8
		void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("Array cannot be null."));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("Non-negative number required."));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(SR.GetString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
			}
			int num = arrayIndex;
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<KeyValuePair<string, string>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00088B84 File Offset: 0x00086D84
		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
		{
			if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
			{
				return false;
			}
			this.m_stringDictionary.Remove(item.Key);
			return true;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00088BA4 File Offset: 0x00086DA4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00088BAC File Offset: 0x00086DAC
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			foreach (object obj in this.m_stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400153D RID: 5437
		private StringDictionary m_stringDictionary;

		// Token: 0x0400153E RID: 5438
		private GenericAdapter.ICollectionToGenericCollectionAdapter _values;

		// Token: 0x0400153F RID: 5439
		private GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

		// Token: 0x020004BF RID: 1215
		internal enum KeyOrValue
		{
			// Token: 0x04001541 RID: 5441
			Key,
			// Token: 0x04001542 RID: 5442
			Value
		}

		// Token: 0x020004C0 RID: 1216
		private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06002768 RID: 10088 RVA: 0x00088BBB File Offset: 0x00086DBB
			public ICollectionToGenericCollectionAdapter(StringDictionary source, GenericAdapter.KeyOrValue keyOrValue)
			{
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				this._internal = source;
				this._keyOrValue = keyOrValue;
			}

			// Token: 0x06002769 RID: 10089 RVA: 0x00088BDF File Offset: 0x00086DDF
			public void Add(string item)
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x0600276A RID: 10090 RVA: 0x00088BDF File Offset: 0x00086DDF
			public void Clear()
			{
				this.ThrowNotSupportedException();
			}

			// Token: 0x0600276B RID: 10091 RVA: 0x00088BE7 File Offset: 0x00086DE7
			public void ThrowNotSupportedException()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					throw new NotSupportedException(SR.GetString("Mutating a key collection derived from a dictionary is not allowed."));
				}
				throw new NotSupportedException(SR.GetString("Mutating a value collection derived from a dictionary is not allowed."));
			}

			// Token: 0x0600276C RID: 10092 RVA: 0x00088C10 File Offset: 0x00086E10
			public bool Contains(string item)
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.ContainsKey(item);
				}
				return this._internal.ContainsValue(item);
			}

			// Token: 0x0600276D RID: 10093 RVA: 0x00088C33 File Offset: 0x00086E33
			public void CopyTo(string[] array, int arrayIndex)
			{
				this.GetUnderlyingCollection().CopyTo(array, arrayIndex);
			}

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x0600276E RID: 10094 RVA: 0x00088C42 File Offset: 0x00086E42
			public int Count
			{
				get
				{
					return this._internal.Count;
				}
			}

			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x0600276F RID: 10095 RVA: 0x0000390E File Offset: 0x00001B0E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002770 RID: 10096 RVA: 0x00088C4F File Offset: 0x00086E4F
			public bool Remove(string item)
			{
				this.ThrowNotSupportedException();
				return false;
			}

			// Token: 0x06002771 RID: 10097 RVA: 0x00088C58 File Offset: 0x00086E58
			private ICollection GetUnderlyingCollection()
			{
				if (this._keyOrValue == GenericAdapter.KeyOrValue.Key)
				{
					return this._internal.Keys;
				}
				return this._internal.Values;
			}

			// Token: 0x06002772 RID: 10098 RVA: 0x00088C79 File Offset: 0x00086E79
			public IEnumerator<string> GetEnumerator()
			{
				ICollection underlyingCollection = this.GetUnderlyingCollection();
				foreach (object obj in underlyingCollection)
				{
					string text = (string)obj;
					yield return text;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002773 RID: 10099 RVA: 0x00088C88 File Offset: 0x00086E88
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetUnderlyingCollection().GetEnumerator();
			}

			// Token: 0x04001543 RID: 5443
			private StringDictionary _internal;

			// Token: 0x04001544 RID: 5444
			private GenericAdapter.KeyOrValue _keyOrValue;

			// Token: 0x020004C1 RID: 1217
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__14 : IEnumerator<string>, IDisposable, IEnumerator
			{
				// Token: 0x06002774 RID: 10100 RVA: 0x00088C95 File Offset: 0x00086E95
				[DebuggerHidden]
				public <GetEnumerator>d__14(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x06002775 RID: 10101 RVA: 0x00088CA4 File Offset: 0x00086EA4
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

				// Token: 0x06002776 RID: 10102 RVA: 0x00088CDC File Offset: 0x00086EDC
				bool IEnumerator.MoveNext()
				{
					bool result;
					try
					{
						int num = this.<>1__state;
						GenericAdapter.ICollectionToGenericCollectionAdapter collectionToGenericCollectionAdapter = this;
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
							ICollection underlyingCollection = collectionToGenericCollectionAdapter.GetUnderlyingCollection();
							enumerator = underlyingCollection.GetEnumerator();
							this.<>1__state = -3;
						}
						if (!enumerator.MoveNext())
						{
							this.<>m__Finally1();
							enumerator = null;
							result = false;
						}
						else
						{
							string text = (string)enumerator.Current;
							this.<>2__current = text;
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

				// Token: 0x06002777 RID: 10103 RVA: 0x00088D8C File Offset: 0x00086F8C
				private void <>m__Finally1()
				{
					this.<>1__state = -1;
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}

				// Token: 0x1700080C RID: 2060
				// (get) Token: 0x06002778 RID: 10104 RVA: 0x00088DB5 File Offset: 0x00086FB5
				string IEnumerator<string>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06002779 RID: 10105 RVA: 0x000044FA File Offset: 0x000026FA
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x1700080D RID: 2061
				// (get) Token: 0x0600277A RID: 10106 RVA: 0x00088DB5 File Offset: 0x00086FB5
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x04001545 RID: 5445
				private int <>1__state;

				// Token: 0x04001546 RID: 5446
				private string <>2__current;

				// Token: 0x04001547 RID: 5447
				public GenericAdapter.ICollectionToGenericCollectionAdapter <>4__this;

				// Token: 0x04001548 RID: 5448
				private IEnumerator <>7__wrap1;
			}
		}

		// Token: 0x020004C2 RID: 1218
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__25 : IEnumerator<KeyValuePair<string, string>>, IDisposable, IEnumerator
		{
			// Token: 0x0600277B RID: 10107 RVA: 0x00088DBD File Offset: 0x00086FBD
			[DebuggerHidden]
			public <GetEnumerator>d__25(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600277C RID: 10108 RVA: 0x00088DCC File Offset: 0x00086FCC
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

			// Token: 0x0600277D RID: 10109 RVA: 0x00088E04 File Offset: 0x00087004
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					GenericAdapter genericAdapter = this;
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
						enumerator = genericAdapter.m_stringDictionary.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
						this.<>2__current = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
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

			// Token: 0x0600277E RID: 10110 RVA: 0x00088ECC File Offset: 0x000870CC
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x1700080E RID: 2062
			// (get) Token: 0x0600277F RID: 10111 RVA: 0x00088EF5 File Offset: 0x000870F5
			KeyValuePair<string, string> IEnumerator<KeyValuePair<string, string>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002780 RID: 10112 RVA: 0x000044FA File Offset: 0x000026FA
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700080F RID: 2063
			// (get) Token: 0x06002781 RID: 10113 RVA: 0x00088EFD File Offset: 0x000870FD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04001549 RID: 5449
			private int <>1__state;

			// Token: 0x0400154A RID: 5450
			private KeyValuePair<string, string> <>2__current;

			// Token: 0x0400154B RID: 5451
			public GenericAdapter <>4__this;

			// Token: 0x0400154C RID: 5452
			private IEnumerator <>7__wrap1;
		}
	}
}
