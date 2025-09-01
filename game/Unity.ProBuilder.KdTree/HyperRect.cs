using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000002 RID: 2
	internal struct HyperRect<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public T[] MinPoint
		{
			get
			{
				return this.minPoint;
			}
			set
			{
				this.minPoint = new T[value.Length];
				value.CopyTo(this.minPoint, 0);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002075 File Offset: 0x00000275
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
		public T[] MaxPoint
		{
			get
			{
				return this.maxPoint;
			}
			set
			{
				this.maxPoint = new T[value.Length];
				value.CopyTo(this.maxPoint, 0);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		public static HyperRect<T> Infinite(int dimensions, ITypeMath<T> math)
		{
			HyperRect<T> result = default(HyperRect<T>);
			result.MinPoint = new T[dimensions];
			result.MaxPoint = new T[dimensions];
			for (int i = 0; i < dimensions; i++)
			{
				result.MinPoint[i] = math.NegativeInfinity;
				result.MaxPoint[i] = math.PositiveInfinity;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public T[] GetClosestPoint(T[] toPoint, ITypeMath<T> math)
		{
			T[] array = new T[toPoint.Length];
			for (int i = 0; i < toPoint.Length; i++)
			{
				if (math.Compare(this.minPoint[i], toPoint[i]) > 0)
				{
					array[i] = this.minPoint[i];
				}
				else if (math.Compare(this.maxPoint[i], toPoint[i]) < 0)
				{
					array[i] = this.maxPoint[i];
				}
				else
				{
					array[i] = toPoint[i];
				}
			}
			return array;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002198 File Offset: 0x00000398
		public HyperRect<T> Clone()
		{
			return new HyperRect<T>
			{
				MinPoint = this.MinPoint,
				MaxPoint = this.MaxPoint
			};
		}

		// Token: 0x04000001 RID: 1
		private T[] minPoint;

		// Token: 0x04000002 RID: 2
		private T[] maxPoint;
	}
}
