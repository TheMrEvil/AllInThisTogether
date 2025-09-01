using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000052 RID: 82
	public static class CollectionExtensions
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00008AAC File Offset: 0x00006CAC
		public static Dictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> source)
		{
			Dictionary<TValue, TKey> dictionary = new Dictionary<TValue, TKey>();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
			{
				if (!dictionary.ContainsKey(keyValuePair.Value))
				{
					dictionary.Add(keyValuePair.Value, keyValuePair.Key);
				}
			}
			return dictionary;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008B18 File Offset: 0x00006D18
		public static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] array = new T[length];
			Array.Copy(data, index, array, 0, length);
			return array;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008B37 File Offset: 0x00006D37
		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source)
		{
			using (IEnumerator<T> enumurator = source.GetEnumerator())
			{
				if (enumurator.MoveNext())
				{
					T t = enumurator.Current;
					while (enumurator.MoveNext())
					{
						yield return t;
						t = enumurator.Current;
					}
				}
			}
			IEnumerator<T> enumurator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008B47 File Offset: 0x00006D47
		public static IEnumerable<T> Reversed<T>(this IReadOnlyList<T> source)
		{
			int num;
			for (int i = source.Count - 1; i >= 0; i = num - 1)
			{
				yield return source[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008B57 File Offset: 0x00006D57
		public static IEnumerable<TValue> DistinctBy<TValue, TDistinct>(this IEnumerable<TValue> source, Func<TValue, TDistinct> predicate)
		{
			HashSet<TDistinct> set = new HashSet<TDistinct>();
			foreach (TValue tvalue in source)
			{
				if (set.Add(predicate(tvalue)))
				{
					yield return tvalue;
				}
			}
			IEnumerator<TValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008B6E File Offset: 0x00006D6E
		public static IEnumerable<T> Yield<T>(this T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008B80 File Offset: 0x00006D80
		public static T LastOr<T>(this IEnumerable<T> source, T value)
		{
			T result;
			try
			{
				result = source.Last<T>();
			}
			catch (InvalidOperationException)
			{
				result = value;
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008BAC File Offset: 0x00006DAC
		public unsafe static void InsertionSortBy<T>(this IList<T> collection, Func<T, int> keySelector)
		{
			if (collection.Count <= 512)
			{
				int* keyBuffer = stackalloc int[checked(unchecked((UIntPtr)collection.Count) * 4)];
				collection.InsertionSortBy(keySelector, keyBuffer);
				return;
			}
			int[] array;
			int* keyBuffer2;
			if ((array = new int[collection.Count]) == null || array.Length == 0)
			{
				keyBuffer2 = null;
			}
			else
			{
				keyBuffer2 = &array[0];
			}
			collection.InsertionSortBy(keySelector, keyBuffer2);
			array = null;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008C08 File Offset: 0x00006E08
		private unsafe static void InsertionSortBy<T>(this IList<T> collection, Func<T, int> keySelector, int* keyBuffer)
		{
			int count = collection.Count;
			for (int i = 0; i < count; i++)
			{
				keyBuffer[i] = keySelector(collection[i]);
			}
			for (int j = 1; j < count; j++)
			{
				T value = collection[j];
				int num = keyBuffer[j];
				int num2 = j - 1;
				while (num2 >= 0 && keyBuffer[num2] > num)
				{
					collection[num2 + 1] = collection[num2];
					keyBuffer[num2 + 1] = keyBuffer[num2];
					num2--;
				}
				collection[num2 + 1] = value;
				keyBuffer[num2 + 1] = num;
			}
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		private sealed class <SkipLast>d__2<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000322 RID: 802 RVA: 0x0000C013 File Offset: 0x0000A213
			[DebuggerHidden]
			public <SkipLast>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000323 RID: 803 RVA: 0x0000C030 File Offset: 0x0000A230
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

			// Token: 0x06000324 RID: 804 RVA: 0x0000C068 File Offset: 0x0000A268
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					T t;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						t = enumurator.Current;
					}
					else
					{
						this.<>1__state = -1;
						enumurator = source.GetEnumerator();
						this.<>1__state = -3;
						if (!enumurator.MoveNext())
						{
							goto IL_83;
						}
						t = enumurator.Current;
					}
					if (enumurator.MoveNext())
					{
						this.<>2__current = t;
						this.<>1__state = 1;
						return true;
					}
					IL_83:
					this.<>m__Finally1();
					enumurator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000C124 File Offset: 0x0000A324
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumurator != null)
				{
					enumurator.Dispose();
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C140 File Offset: 0x0000A340
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000327 RID: 807 RVA: 0x0000C148 File Offset: 0x0000A348
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0000C14F File Offset: 0x0000A34F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000C15C File Offset: 0x0000A35C
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				CollectionExtensions.<SkipLast>d__2<T> <SkipLast>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<SkipLast>d__ = this;
				}
				else
				{
					<SkipLast>d__ = new CollectionExtensions.<SkipLast>d__2<T>(0);
				}
				<SkipLast>d__.source = source;
				return <SkipLast>d__;
			}

			// Token: 0x0600032A RID: 810 RVA: 0x0000C19F File Offset: 0x0000A39F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000202 RID: 514
			private int <>1__state;

			// Token: 0x04000203 RID: 515
			private T <>2__current;

			// Token: 0x04000204 RID: 516
			private int <>l__initialThreadId;

			// Token: 0x04000205 RID: 517
			private IEnumerable<T> source;

			// Token: 0x04000206 RID: 518
			public IEnumerable<T> <>3__source;

			// Token: 0x04000207 RID: 519
			private IEnumerator<T> <enumurator>5__2;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		private sealed class <Reversed>d__3<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x0600032B RID: 811 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
			[DebuggerHidden]
			public <Reversed>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600032C RID: 812 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600032D RID: 813 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 - 1;
				}
				else
				{
					this.<>1__state = -1;
					i = source.Count - 1;
				}
				if (i < 0)
				{
					return false;
				}
				this.<>2__current = source[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C23E File Offset: 0x0000A43E
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000C246 File Offset: 0x0000A446
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C24D File Offset: 0x0000A44D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000331 RID: 817 RVA: 0x0000C25C File Offset: 0x0000A45C
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				CollectionExtensions.<Reversed>d__3<T> <Reversed>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Reversed>d__ = this;
				}
				else
				{
					<Reversed>d__ = new CollectionExtensions.<Reversed>d__3<T>(0);
				}
				<Reversed>d__.source = source;
				return <Reversed>d__;
			}

			// Token: 0x06000332 RID: 818 RVA: 0x0000C29F File Offset: 0x0000A49F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000208 RID: 520
			private int <>1__state;

			// Token: 0x04000209 RID: 521
			private T <>2__current;

			// Token: 0x0400020A RID: 522
			private int <>l__initialThreadId;

			// Token: 0x0400020B RID: 523
			private IReadOnlyList<T> source;

			// Token: 0x0400020C RID: 524
			public IReadOnlyList<T> <>3__source;

			// Token: 0x0400020D RID: 525
			private int <i>5__2;
		}

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		private sealed class <DistinctBy>d__4<TValue, TDistinct> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000333 RID: 819 RVA: 0x0000C2A7 File Offset: 0x0000A4A7
			[DebuggerHidden]
			public <DistinctBy>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000334 RID: 820 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
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

			// Token: 0x06000335 RID: 821 RVA: 0x0000C2FC File Offset: 0x0000A4FC
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
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
						set = new HashSet<TDistinct>();
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						TValue arg = enumerator.Current;
						if (set.Add(predicate(arg)))
						{
							this.<>2__current = arg;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000336 RID: 822 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C3DC File Offset: 0x0000A5DC
			TValue IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C3EB File Offset: 0x0000A5EB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600033A RID: 826 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
			[DebuggerHidden]
			IEnumerator<TValue> IEnumerable<!0>.GetEnumerator()
			{
				CollectionExtensions.<DistinctBy>d__4<TValue, TDistinct> <DistinctBy>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<DistinctBy>d__ = this;
				}
				else
				{
					<DistinctBy>d__ = new CollectionExtensions.<DistinctBy>d__4<TValue, TDistinct>(0);
				}
				<DistinctBy>d__.source = source;
				<DistinctBy>d__.predicate = predicate;
				return <DistinctBy>d__;
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000C447 File Offset: 0x0000A647
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TValue>.GetEnumerator();
			}

			// Token: 0x0400020E RID: 526
			private int <>1__state;

			// Token: 0x0400020F RID: 527
			private TValue <>2__current;

			// Token: 0x04000210 RID: 528
			private int <>l__initialThreadId;

			// Token: 0x04000211 RID: 529
			private IEnumerable<TValue> source;

			// Token: 0x04000212 RID: 530
			public IEnumerable<TValue> <>3__source;

			// Token: 0x04000213 RID: 531
			private Func<TValue, TDistinct> predicate;

			// Token: 0x04000214 RID: 532
			public Func<TValue, TDistinct> <>3__predicate;

			// Token: 0x04000215 RID: 533
			private HashSet<TDistinct> <set>5__2;

			// Token: 0x04000216 RID: 534
			private IEnumerator<TValue> <>7__wrap2;
		}

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		private sealed class <Yield>d__5<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x0600033C RID: 828 RVA: 0x0000C44F File Offset: 0x0000A64F
			[DebuggerHidden]
			public <Yield>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600033D RID: 829 RVA: 0x0000C469 File Offset: 0x0000A669
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600033E RID: 830 RVA: 0x0000C46C File Offset: 0x0000A66C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = item;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C4AD File Offset: 0x0000A6AD
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000341 RID: 833 RVA: 0x0000C4BC File Offset: 0x0000A6BC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000342 RID: 834 RVA: 0x0000C4CC File Offset: 0x0000A6CC
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				CollectionExtensions.<Yield>d__5<T> <Yield>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Yield>d__ = this;
				}
				else
				{
					<Yield>d__ = new CollectionExtensions.<Yield>d__5<T>(0);
				}
				<Yield>d__.item = item;
				return <Yield>d__;
			}

			// Token: 0x06000343 RID: 835 RVA: 0x0000C50F File Offset: 0x0000A70F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000217 RID: 535
			private int <>1__state;

			// Token: 0x04000218 RID: 536
			private T <>2__current;

			// Token: 0x04000219 RID: 537
			private int <>l__initialThreadId;

			// Token: 0x0400021A RID: 538
			private T item;

			// Token: 0x0400021B RID: 539
			public T <>3__item;
		}
	}
}
