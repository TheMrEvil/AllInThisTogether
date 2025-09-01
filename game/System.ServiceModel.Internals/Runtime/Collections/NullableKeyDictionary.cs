using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Runtime.Collections
{
	// Token: 0x02000051 RID: 81
	internal class NullableKeyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public NullableKeyDictionary()
		{
			this.innerDictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000FDCF File Offset: 0x0000DFCF
		public int Count
		{
			get
			{
				return this.innerDictionary.Count + (this.isNullKeyPresent ? 1 : 0);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000FDE9 File Offset: 0x0000DFE9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public ICollection<TKey> Keys
		{
			get
			{
				return new NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryKeyCollection<TKey, TValue>(this);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public ICollection<TValue> Values
		{
			get
			{
				return new NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryValueCollection<TKey, TValue>(this);
			}
		}

		// Token: 0x17000063 RID: 99
		public TValue this[TKey key]
		{
			get
			{
				if (key != null)
				{
					return this.innerDictionary[key];
				}
				if (this.isNullKeyPresent)
				{
					return this.nullKeyValue;
				}
				throw Fx.Exception.AsError(new KeyNotFoundException());
			}
			set
			{
				if (key == null)
				{
					this.isNullKeyPresent = true;
					this.nullKeyValue = value;
					return;
				}
				this.innerDictionary[key] = value;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000FE58 File Offset: 0x0000E058
		public void Add(TKey key, TValue value)
		{
			if (key != null)
			{
				this.innerDictionary.Add(key, value);
				return;
			}
			if (this.isNullKeyPresent)
			{
				throw Fx.Exception.Argument("key", "Null Key Already Present");
			}
			this.isNullKeyPresent = true;
			this.nullKeyValue = value;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000FEA6 File Offset: 0x0000E0A6
		public bool ContainsKey(TKey key)
		{
			if (key != null)
			{
				return this.innerDictionary.ContainsKey(key);
			}
			return this.isNullKeyPresent;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				bool result = this.isNullKeyPresent;
				this.isNullKeyPresent = false;
				this.nullKeyValue = default(TValue);
				return result;
			}
			return this.innerDictionary.Remove(key);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key != null)
			{
				return this.innerDictionary.TryGetValue(key, out value);
			}
			if (this.isNullKeyPresent)
			{
				value = this.nullKeyValue;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000FF29 File Offset: 0x0000E129
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000FF3F File Offset: 0x0000E13F
		public void Clear()
		{
			this.isNullKeyPresent = false;
			this.nullKeyValue = default(TValue);
			this.innerDictionary.Clear();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000FF60 File Offset: 0x0000E160
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (item.Key != null)
			{
				return this.innerDictionary.Contains(item);
			}
			if (!this.isNullKeyPresent)
			{
				return false;
			}
			if (item.Value != null)
			{
				TValue value = item.Value;
				return value.Equals(this.nullKeyValue);
			}
			return this.nullKeyValue == null;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.innerDictionary.CopyTo(array, arrayIndex);
			if (this.isNullKeyPresent)
			{
				array[arrayIndex + this.innerDictionary.Count] = new KeyValuePair<TKey, TValue>(default(TKey), this.nullKeyValue);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00010019 File Offset: 0x0000E219
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (item.Key != null)
			{
				return this.innerDictionary.Remove(item);
			}
			if (this.Contains(item))
			{
				this.isNullKeyPresent = false;
				this.nullKeyValue = default(TValue);
				return true;
			}
			return false;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00010055 File Offset: 0x0000E255
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (KeyValuePair<!0, !1> keyValuePair in this.innerDictionary)
			{
				yield return keyValuePair;
			}
			if (this.isNullKeyPresent)
			{
				yield return new KeyValuePair<TKey, TValue>(default(TKey), this.nullKeyValue);
			}
			yield break;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00010064 File Offset: 0x0000E264
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x040001F0 RID: 496
		private bool isNullKeyPresent;

		// Token: 0x040001F1 RID: 497
		private TValue nullKeyValue;

		// Token: 0x040001F2 RID: 498
		private IDictionary<TKey, TValue> innerDictionary;

		// Token: 0x02000094 RID: 148
		private class NullKeyDictionaryKeyCollection<TypeKey, TypeValue> : ICollection<TypeKey>, IEnumerable<TypeKey>, IEnumerable
		{
			// Token: 0x06000415 RID: 1045 RVA: 0x00012FEC File Offset: 0x000111EC
			public NullKeyDictionaryKeyCollection(NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary)
			{
				this.nullKeyDictionary = nullKeyDictionary;
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x00012FFC File Offset: 0x000111FC
			public int Count
			{
				get
				{
					int num = this.nullKeyDictionary.innerDictionary.Keys.Count;
					if (this.nullKeyDictionary.isNullKeyPresent)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013031 File Offset: 0x00011231
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x00013034 File Offset: 0x00011234
			public void Add(TypeKey item)
			{
				throw Fx.Exception.AsError(new NotSupportedException("Key Collection Updates Not Allowed"));
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x0001304A File Offset: 0x0001124A
			public void Clear()
			{
				throw Fx.Exception.AsError(new NotSupportedException("Key Collection Updates Not Allowed"));
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x00013060 File Offset: 0x00011260
			public bool Contains(TypeKey item)
			{
				if (item != null)
				{
					return this.nullKeyDictionary.innerDictionary.Keys.Contains(item);
				}
				return this.nullKeyDictionary.isNullKeyPresent;
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0001308C File Offset: 0x0001128C
			public void CopyTo(TypeKey[] array, int arrayIndex)
			{
				this.nullKeyDictionary.innerDictionary.Keys.CopyTo(array, arrayIndex);
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					array[arrayIndex + this.nullKeyDictionary.innerDictionary.Keys.Count] = default(TypeKey);
				}
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x000130E3 File Offset: 0x000112E3
			public bool Remove(TypeKey item)
			{
				throw Fx.Exception.AsError(new NotSupportedException("Key Collection Updates Not Allowed"));
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x000130F9 File Offset: 0x000112F9
			public IEnumerator<TypeKey> GetEnumerator()
			{
				foreach (TypeKey typeKey in this.nullKeyDictionary.innerDictionary.Keys)
				{
					yield return typeKey;
				}
				IEnumerator<TypeKey> enumerator = null;
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					TypeKey typeKey2 = default(TypeKey);
				}
				yield break;
				yield break;
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00013108 File Offset: 0x00011308
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<TypeKey>)this).GetEnumerator();
			}

			// Token: 0x040002F9 RID: 761
			private NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary;

			// Token: 0x020000B5 RID: 181
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__11 : IEnumerator<TypeKey>, IDisposable, IEnumerator
			{
				// Token: 0x060004A0 RID: 1184 RVA: 0x00013BD7 File Offset: 0x00011DD7
				[DebuggerHidden]
				public <GetEnumerator>d__11(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x060004A1 RID: 1185 RVA: 0x00013BE8 File Offset: 0x00011DE8
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

				// Token: 0x060004A2 RID: 1186 RVA: 0x00013C20 File Offset: 0x00011E20
				bool IEnumerator.MoveNext()
				{
					bool result;
					try
					{
						int num = this.<>1__state;
						NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryKeyCollection<TypeKey, TypeValue> nullKeyDictionaryKeyCollection = this;
						switch (num)
						{
						case 0:
							this.<>1__state = -1;
							enumerator = nullKeyDictionaryKeyCollection.nullKeyDictionary.innerDictionary.Keys.GetEnumerator();
							this.<>1__state = -3;
							break;
						case 1:
							this.<>1__state = -3;
							break;
						case 2:
							this.<>1__state = -1;
							goto IL_BE;
						default:
							return false;
						}
						if (enumerator.MoveNext())
						{
							TypeKey typeKey3 = enumerator.Current;
							typeKey2 = typeKey3;
							this.<>1__state = 1;
							return true;
						}
						this.<>m__Finally1();
						enumerator = null;
						if (nullKeyDictionaryKeyCollection.nullKeyDictionary.isNullKeyPresent)
						{
							typeKey2 = default(TypeKey);
							this.<>1__state = 2;
							return true;
						}
						IL_BE:
						result = false;
					}
					catch
					{
						this.System.IDisposable.Dispose();
						throw;
					}
					return result;
				}

				// Token: 0x060004A3 RID: 1187 RVA: 0x00013D08 File Offset: 0x00011F08
				private void <>m__Finally1()
				{
					this.<>1__state = -1;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}

				// Token: 0x170000D5 RID: 213
				// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00013D24 File Offset: 0x00011F24
				TypeKey IEnumerator<!2>.Current
				{
					[DebuggerHidden]
					get
					{
						return typeKey2;
					}
				}

				// Token: 0x060004A5 RID: 1189 RVA: 0x00013D2C File Offset: 0x00011F2C
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170000D6 RID: 214
				// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00013D33 File Offset: 0x00011F33
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return typeKey2;
					}
				}

				// Token: 0x04000364 RID: 868
				private int <>1__state;

				// Token: 0x04000365 RID: 869
				private TypeKey <>2__current;

				// Token: 0x04000366 RID: 870
				public NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryKeyCollection<TypeKey, TypeValue> <>4__this;

				// Token: 0x04000367 RID: 871
				private IEnumerator<TypeKey> <>7__wrap1;
			}
		}

		// Token: 0x02000095 RID: 149
		private class NullKeyDictionaryValueCollection<TypeKey, TypeValue> : ICollection<TypeValue>, IEnumerable<TypeValue>, IEnumerable
		{
			// Token: 0x0600041F RID: 1055 RVA: 0x00013110 File Offset: 0x00011310
			public NullKeyDictionaryValueCollection(NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary)
			{
				this.nullKeyDictionary = nullKeyDictionary;
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00013120 File Offset: 0x00011320
			public int Count
			{
				get
				{
					int num = this.nullKeyDictionary.innerDictionary.Values.Count;
					if (this.nullKeyDictionary.isNullKeyPresent)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x00013155 File Offset: 0x00011355
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00013158 File Offset: 0x00011358
			public void Add(TypeValue item)
			{
				throw Fx.Exception.AsError(new NotSupportedException("Value Collection Updates Not Allowed"));
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0001316E File Offset: 0x0001136E
			public void Clear()
			{
				throw Fx.Exception.AsError(new NotSupportedException("Value Collection Updates Not Allowed"));
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00013184 File Offset: 0x00011384
			public bool Contains(TypeValue item)
			{
				return this.nullKeyDictionary.innerDictionary.Values.Contains(item) || (this.nullKeyDictionary.isNullKeyPresent && this.nullKeyDictionary.nullKeyValue.Equals(item));
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x000131D8 File Offset: 0x000113D8
			public void CopyTo(TypeValue[] array, int arrayIndex)
			{
				this.nullKeyDictionary.innerDictionary.Values.CopyTo(array, arrayIndex);
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					array[arrayIndex + this.nullKeyDictionary.innerDictionary.Values.Count] = this.nullKeyDictionary.nullKeyValue;
				}
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x00013231 File Offset: 0x00011431
			public bool Remove(TypeValue item)
			{
				throw Fx.Exception.AsError(new NotSupportedException("Value Collection Updates Not Allowed"));
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x00013247 File Offset: 0x00011447
			public IEnumerator<TypeValue> GetEnumerator()
			{
				foreach (TypeValue typeValue in this.nullKeyDictionary.innerDictionary.Values)
				{
					yield return typeValue;
				}
				IEnumerator<TypeValue> enumerator = null;
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					yield return this.nullKeyDictionary.nullKeyValue;
				}
				yield break;
				yield break;
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00013256 File Offset: 0x00011456
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<TypeValue>)this).GetEnumerator();
			}

			// Token: 0x040002FA RID: 762
			private NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary;

			// Token: 0x020000B6 RID: 182
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__11 : IEnumerator<TypeValue>, IDisposable, IEnumerator
			{
				// Token: 0x060004A7 RID: 1191 RVA: 0x00013D40 File Offset: 0x00011F40
				[DebuggerHidden]
				public <GetEnumerator>d__11(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x060004A8 RID: 1192 RVA: 0x00013D50 File Offset: 0x00011F50
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

				// Token: 0x060004A9 RID: 1193 RVA: 0x00013D88 File Offset: 0x00011F88
				bool IEnumerator.MoveNext()
				{
					bool result;
					try
					{
						int num = this.<>1__state;
						NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryValueCollection<TypeKey, TypeValue> nullKeyDictionaryValueCollection = this;
						switch (num)
						{
						case 0:
							this.<>1__state = -1;
							enumerator = nullKeyDictionaryValueCollection.nullKeyDictionary.innerDictionary.Values.GetEnumerator();
							this.<>1__state = -3;
							break;
						case 1:
							this.<>1__state = -3;
							break;
						case 2:
							this.<>1__state = -1;
							goto IL_C3;
						default:
							return false;
						}
						if (enumerator.MoveNext())
						{
							TypeValue typeValue = enumerator.Current;
							this.<>2__current = typeValue;
							this.<>1__state = 1;
							return true;
						}
						this.<>m__Finally1();
						enumerator = null;
						if (nullKeyDictionaryValueCollection.nullKeyDictionary.isNullKeyPresent)
						{
							this.<>2__current = nullKeyDictionaryValueCollection.nullKeyDictionary.nullKeyValue;
							this.<>1__state = 2;
							return true;
						}
						IL_C3:
						result = false;
					}
					catch
					{
						this.System.IDisposable.Dispose();
						throw;
					}
					return result;
				}

				// Token: 0x060004AA RID: 1194 RVA: 0x00013E74 File Offset: 0x00012074
				private void <>m__Finally1()
				{
					this.<>1__state = -1;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}

				// Token: 0x170000D7 RID: 215
				// (get) Token: 0x060004AB RID: 1195 RVA: 0x00013E90 File Offset: 0x00012090
				TypeValue IEnumerator<!3>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060004AC RID: 1196 RVA: 0x00013E98 File Offset: 0x00012098
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170000D8 RID: 216
				// (get) Token: 0x060004AD RID: 1197 RVA: 0x00013E9F File Offset: 0x0001209F
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x04000368 RID: 872
				private int <>1__state;

				// Token: 0x04000369 RID: 873
				private TypeValue <>2__current;

				// Token: 0x0400036A RID: 874
				public NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryValueCollection<TypeKey, TypeValue> <>4__this;

				// Token: 0x0400036B RID: 875
				private IEnumerator<TypeValue> <>7__wrap1;
			}
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__24 : IEnumerator<KeyValuePair<!0, !1>>, IDisposable, IEnumerator
		{
			// Token: 0x06000429 RID: 1065 RVA: 0x0001325E File Offset: 0x0001145E
			[DebuggerHidden]
			public <GetEnumerator>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x0001326D File Offset: 0x0001146D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00013270 File Offset: 0x00011470
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				NullableKeyDictionary<TKey, TValue> nullableKeyDictionary = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					innerEnumerator = nullableKeyDictionary.innerDictionary.GetEnumerator();
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (innerEnumerator.MoveNext())
				{
					this.<>2__current = innerEnumerator.Current;
					this.<>1__state = 1;
					return true;
				}
				if (nullableKeyDictionary.isNullKeyPresent)
				{
					this.<>2__current = new KeyValuePair<TKey, TValue>(default(TKey), nullableKeyDictionary.nullKeyValue);
					this.<>1__state = 2;
					return true;
				}
				return false;
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001331A File Offset: 0x0001151A
			KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<!0, !1>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00013322 File Offset: 0x00011522
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x00013329 File Offset: 0x00011529
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040002FB RID: 763
			private int <>1__state;

			// Token: 0x040002FC RID: 764
			private KeyValuePair<TKey, TValue> <>2__current;

			// Token: 0x040002FD RID: 765
			public NullableKeyDictionary<TKey, TValue> <>4__this;

			// Token: 0x040002FE RID: 766
			private IEnumerator<KeyValuePair<TKey, TValue>> <innerEnumerator>5__2;
		}
	}
}
