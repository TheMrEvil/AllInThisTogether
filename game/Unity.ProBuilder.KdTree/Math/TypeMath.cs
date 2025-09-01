using System;

namespace UnityEngine.ProBuilder.KdTree.Math
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal abstract class TypeMath<T> : ITypeMath<T>
	{
		// Token: 0x06000074 RID: 116
		public abstract int Compare(T a, T b);

		// Token: 0x06000075 RID: 117
		public abstract bool AreEqual(T a, T b);

		// Token: 0x06000076 RID: 118 RVA: 0x00003110 File Offset: 0x00001310
		public virtual bool AreEqual(T[] a, T[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (!this.AreEqual(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000077 RID: 119
		public abstract T MinValue { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000078 RID: 120
		public abstract T MaxValue { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x0000314E File Offset: 0x0000134E
		public T Min(T a, T b)
		{
			if (this.Compare(a, b) < 0)
			{
				return a;
			}
			return b;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000315E File Offset: 0x0000135E
		public T Max(T a, T b)
		{
			if (this.Compare(a, b) > 0)
			{
				return a;
			}
			return b;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007B RID: 123
		public abstract T Zero { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007C RID: 124
		public abstract T NegativeInfinity { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007D RID: 125
		public abstract T PositiveInfinity { get; }

		// Token: 0x0600007E RID: 126
		public abstract T Add(T a, T b);

		// Token: 0x0600007F RID: 127
		public abstract T Subtract(T a, T b);

		// Token: 0x06000080 RID: 128
		public abstract T Multiply(T a, T b);

		// Token: 0x06000081 RID: 129
		public abstract T DistanceSquaredBetweenPoints(T[] a, T[] b);

		// Token: 0x06000082 RID: 130 RVA: 0x0000316E File Offset: 0x0000136E
		protected TypeMath()
		{
		}
	}
}
