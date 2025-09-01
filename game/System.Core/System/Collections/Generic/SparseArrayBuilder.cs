using System;

namespace System.Collections.Generic
{
	// Token: 0x0200035A RID: 858
	internal struct SparseArrayBuilder<T>
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0005743E File Offset: 0x0005563E
		public SparseArrayBuilder(bool initialize)
		{
			this = default(SparseArrayBuilder<T>);
			this._builder = new LargeArrayBuilder<T>(true);
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00057453 File Offset: 0x00055653
		public int Count
		{
			get
			{
				return checked(this._builder.Count + this._reservedCount);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00057467 File Offset: 0x00055667
		public ArrayBuilder<Marker> Markers
		{
			get
			{
				return this._markers;
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0005746F File Offset: 0x0005566F
		public void Add(T item)
		{
			this._builder.Add(item);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0005747D File Offset: 0x0005567D
		public void AddRange(IEnumerable<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0005748C File Offset: 0x0005568C
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			CopyPosition position = CopyPosition.Start;
			for (int i = 0; i < this._markers.Count; i++)
			{
				Marker marker = this._markers[i];
				int num2 = Math.Min(marker.Index - num, count);
				if (num2 > 0)
				{
					position = this._builder.CopyTo(position, array, arrayIndex, num2);
					arrayIndex += num2;
					num += num2;
					count -= num2;
				}
				if (count == 0)
				{
					return;
				}
				int num3 = Math.Min(marker.Count, count);
				arrayIndex += num3;
				num += num3;
				count -= num3;
			}
			if (count > 0)
			{
				this._builder.CopyTo(position, array, arrayIndex, count);
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00057534 File Offset: 0x00055734
		public void Reserve(int count)
		{
			this._markers.Add(new Marker(count, this.Count));
			checked
			{
				this._reservedCount += count;
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0005755C File Offset: 0x0005575C
		public bool ReserveOrAdd(IEnumerable<T> items)
		{
			int num;
			if (EnumerableHelpers.TryGetCount<T>(items, out num))
			{
				if (num > 0)
				{
					this.Reserve(num);
					return true;
				}
			}
			else
			{
				this.AddRange(items);
			}
			return false;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00057588 File Offset: 0x00055788
		public T[] ToArray()
		{
			if (this._markers.Count == 0)
			{
				return this._builder.ToArray();
			}
			T[] array = new T[this.Count];
			this.CopyTo(array, 0, array.Length);
			return array;
		}

		// Token: 0x04000C82 RID: 3202
		private LargeArrayBuilder<T> _builder;

		// Token: 0x04000C83 RID: 3203
		private ArrayBuilder<Marker> _markers;

		// Token: 0x04000C84 RID: 3204
		private int _reservedCount;
	}
}
