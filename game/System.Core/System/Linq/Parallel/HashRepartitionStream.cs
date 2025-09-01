using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000110 RID: 272
	internal abstract class HashRepartitionStream<TInputOutput, THashKey, TOrderKey> : PartitionedStream<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x0001E7BB File Offset: 0x0001C9BB
		internal HashRepartitionStream(int partitionsCount, IComparer<TOrderKey> orderKeyComparer, IEqualityComparer<THashKey> hashKeyComparer, IEqualityComparer<TInputOutput> elementComparer) : base(partitionsCount, orderKeyComparer, OrdinalIndexState.Shuffled)
		{
			this._keyComparer = hashKeyComparer;
			this._elementComparer = elementComparer;
			this._distributionMod = 503;
			checked
			{
				while (this._distributionMod < partitionsCount)
				{
					this._distributionMod *= 2;
				}
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001E7F9 File Offset: 0x0001C9F9
		internal int GetHashCode(TInputOutput element)
		{
			return (int.MaxValue & ((this._elementComparer == null) ? ((element == null) ? 0 : element.GetHashCode()) : this._elementComparer.GetHashCode(element))) % this._distributionMod;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001E836 File Offset: 0x0001CA36
		internal int GetHashCode(THashKey key)
		{
			return (int.MaxValue & ((this._keyComparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this._keyComparer.GetHashCode(key))) % this._distributionMod;
		}

		// Token: 0x04000632 RID: 1586
		private readonly IEqualityComparer<THashKey> _keyComparer;

		// Token: 0x04000633 RID: 1587
		private readonly IEqualityComparer<TInputOutput> _elementComparer;

		// Token: 0x04000634 RID: 1588
		private readonly int _distributionMod;

		// Token: 0x04000635 RID: 1589
		private const int NULL_ELEMENT_HASH_CODE = 0;

		// Token: 0x04000636 RID: 1590
		private const int HashCodeMask = 2147483647;
	}
}
