using System;
using System.Threading;

namespace System.Dynamic.Utils
{
	// Token: 0x02000323 RID: 803
	internal sealed class CacheDict<TKey, TValue>
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x00051F58 File Offset: 0x00050158
		internal CacheDict(int size)
		{
			int num = CacheDict<TKey, TValue>.AlignSize(size);
			this._mask = num - 1;
			this._entries = new CacheDict<TKey, TValue>.Entry[num];
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00051F87 File Offset: 0x00050187
		private static int AlignSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			size++;
			return size;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00051FB8 File Offset: 0x000501B8
		internal bool TryGetValue(TKey key, out TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					value = entry._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0005202C File Offset: 0x0005022C
		internal void Add(TKey key, TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					return;
				}
			}
			Volatile.Write<CacheDict<TKey, TValue>.Entry>(ref this._entries[num], new CacheDict<TKey, TValue>.Entry(hashCode, key, value));
		}

		// Token: 0x1700043B RID: 1083
		internal TValue this[TKey key]
		{
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x04000BD9 RID: 3033
		private readonly int _mask;

		// Token: 0x04000BDA RID: 3034
		private readonly CacheDict<TKey, TValue>.Entry[] _entries;

		// Token: 0x02000324 RID: 804
		private sealed class Entry
		{
			// Token: 0x0600184E RID: 6222 RVA: 0x000520AD File Offset: 0x000502AD
			internal Entry(int hash, TKey key, TValue value)
			{
				this._hash = hash;
				this._key = key;
				this._value = value;
			}

			// Token: 0x04000BDB RID: 3035
			internal readonly int _hash;

			// Token: 0x04000BDC RID: 3036
			internal readonly TKey _key;

			// Token: 0x04000BDD RID: 3037
			internal readonly TValue _value;
		}
	}
}
