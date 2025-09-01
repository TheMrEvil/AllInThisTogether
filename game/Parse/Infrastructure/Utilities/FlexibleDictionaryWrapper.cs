using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000052 RID: 82
	[Preserve(AllMembers = true, Conditional = false)]
	public class FlexibleDictionaryWrapper<TOut, TIn> : IDictionary<string, TOut>, ICollection<KeyValuePair<string, TOut>>, IEnumerable<KeyValuePair<string, TOut>>, IEnumerable
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000CDC7 File Offset: 0x0000AFC7
		public FlexibleDictionaryWrapper(IDictionary<string, TIn> toWrap)
		{
			this.toWrap = toWrap;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000CDD6 File Offset: 0x0000AFD6
		public void Add(string key, TOut value)
		{
			this.toWrap.Add(key, (TIn)((object)Conversion.ConvertTo<TIn>(value)));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public bool ContainsKey(string key)
		{
			return this.toWrap.ContainsKey(key);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000CE02 File Offset: 0x0000B002
		public ICollection<string> Keys
		{
			get
			{
				return this.toWrap.Keys;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000CE0F File Offset: 0x0000B00F
		public bool Remove(string key)
		{
			return this.toWrap.Remove(key);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000CE20 File Offset: 0x0000B020
		public bool TryGetValue(string key, out TOut value)
		{
			TIn tin;
			bool result = this.toWrap.TryGetValue(key, out tin);
			value = (TOut)((object)Conversion.ConvertTo<TOut>(tin));
			return result;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000CE51 File Offset: 0x0000B051
		public ICollection<TOut> Values
		{
			get
			{
				return (from item in this.toWrap.Values
				select (TOut)((object)Conversion.ConvertTo<TOut>(item))).ToList<TOut>();
			}
		}

		// Token: 0x17000148 RID: 328
		public TOut this[string key]
		{
			get
			{
				return (TOut)((object)Conversion.ConvertTo<TOut>(this.toWrap[key]));
			}
			set
			{
				this.toWrap[key] = (TIn)((object)Conversion.ConvertTo<TIn>(value));
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		public void Add(KeyValuePair<string, TOut> item)
		{
			this.toWrap.Add(new KeyValuePair<string, TIn>(item.Key, (TIn)((object)Conversion.ConvertTo<TIn>(item.Value))));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000CEF1 File Offset: 0x0000B0F1
		public void Clear()
		{
			this.toWrap.Clear();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CEFE File Offset: 0x0000B0FE
		public bool Contains(KeyValuePair<string, TOut> item)
		{
			return this.toWrap.Contains(new KeyValuePair<string, TIn>(item.Key, (TIn)((object)Conversion.ConvertTo<TIn>(item.Value))));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000CF2D File Offset: 0x0000B12D
		public void CopyTo(KeyValuePair<string, TOut>[] array, int arrayIndex)
		{
			this.toWrap.Select(delegate(KeyValuePair<string, TIn> pair)
			{
				KeyValuePair<string, TIn> keyValuePair = pair;
				string key = keyValuePair.Key;
				keyValuePair = pair;
				return new KeyValuePair<string, TOut>(key, (TOut)((object)Conversion.ConvertTo<TOut>(keyValuePair.Value)));
			}).ToList<KeyValuePair<string, TOut>>().CopyTo(array, arrayIndex);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000CF65 File Offset: 0x0000B165
		public int Count
		{
			get
			{
				return this.toWrap.Count;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000CF72 File Offset: 0x0000B172
		public bool IsReadOnly
		{
			get
			{
				return this.toWrap.IsReadOnly;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000CF7F File Offset: 0x0000B17F
		public bool Remove(KeyValuePair<string, TOut> item)
		{
			return this.toWrap.Remove(new KeyValuePair<string, TIn>(item.Key, (TIn)((object)Conversion.ConvertTo<TIn>(item.Value))));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		public IEnumerator<KeyValuePair<string, TOut>> GetEnumerator()
		{
			foreach (KeyValuePair<string, TIn> keyValuePair in this.toWrap)
			{
				yield return new KeyValuePair<string, TOut>(keyValuePair.Key, (TOut)((object)Conversion.ConvertTo<TOut>(keyValuePair.Value)));
			}
			IEnumerator<KeyValuePair<string, TIn>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000CFBD File Offset: 0x0000B1BD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000C8 RID: 200
		private readonly IDictionary<string, TIn> toWrap;

		// Token: 0x0200011C RID: 284
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000752 RID: 1874 RVA: 0x00016506 File Offset: 0x00014706
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x00016512 File Offset: 0x00014712
			public <>c()
			{
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0001651A File Offset: 0x0001471A
			internal TOut <get_Values>b__9_0(TIn item)
			{
				return (TOut)((object)Conversion.ConvertTo<TOut>(item));
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x0001652C File Offset: 0x0001472C
			internal KeyValuePair<string, TOut> <CopyTo>b__16_0(KeyValuePair<string, TIn> pair)
			{
				KeyValuePair<string, TIn> keyValuePair = pair;
				string key = keyValuePair.Key;
				keyValuePair = pair;
				return new KeyValuePair<string, TOut>(key, (TOut)((object)Conversion.ConvertTo<TOut>(keyValuePair.Value)));
			}

			// Token: 0x04000296 RID: 662
			public static readonly FlexibleDictionaryWrapper<TOut, TIn>.<>c <>9 = new FlexibleDictionaryWrapper<TOut, TIn>.<>c();

			// Token: 0x04000297 RID: 663
			public static Func<TIn, TOut> <>9__9_0;

			// Token: 0x04000298 RID: 664
			public static Func<KeyValuePair<string, TIn>, KeyValuePair<string, TOut>> <>9__16_0;
		}

		// Token: 0x0200011D RID: 285
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__22 : IEnumerator<KeyValuePair<string, TOut>>, IEnumerator, IDisposable
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x0001655F File Offset: 0x0001475F
			[DebuggerHidden]
			public <GetEnumerator>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00016570 File Offset: 0x00014770
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

			// Token: 0x06000758 RID: 1880 RVA: 0x000165A8 File Offset: 0x000147A8
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					FlexibleDictionaryWrapper<TOut, TIn> flexibleDictionaryWrapper = this;
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
						enumerator = flexibleDictionaryWrapper.toWrap.GetEnumerator();
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
						KeyValuePair<string, TIn> keyValuePair = enumerator.Current;
						this.<>2__current = new KeyValuePair<string, TOut>(keyValuePair.Key, (TOut)((object)Conversion.ConvertTo<TOut>(keyValuePair.Value)));
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

			// Token: 0x06000759 RID: 1881 RVA: 0x00016670 File Offset: 0x00014870
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001668C File Offset: 0x0001488C
			KeyValuePair<string, TOut> IEnumerator<KeyValuePair<string, !0>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x00016694 File Offset: 0x00014894
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001669B File Offset: 0x0001489B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000299 RID: 665
			private int <>1__state;

			// Token: 0x0400029A RID: 666
			private KeyValuePair<string, TOut> <>2__current;

			// Token: 0x0400029B RID: 667
			public FlexibleDictionaryWrapper<TOut, TIn> <>4__this;

			// Token: 0x0400029C RID: 668
			private IEnumerator<KeyValuePair<string, TIn>> <>7__wrap1;
		}
	}
}
