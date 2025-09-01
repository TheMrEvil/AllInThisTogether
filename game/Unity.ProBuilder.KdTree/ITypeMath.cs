using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000009 RID: 9
	internal interface ITypeMath<T>
	{
		// Token: 0x06000039 RID: 57
		int Compare(T a, T b);

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58
		T MinValue { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59
		T MaxValue { get; }

		// Token: 0x0600003C RID: 60
		T Min(T a, T b);

		// Token: 0x0600003D RID: 61
		T Max(T a, T b);

		// Token: 0x0600003E RID: 62
		bool AreEqual(T a, T b);

		// Token: 0x0600003F RID: 63
		bool AreEqual(T[] a, T[] b);

		// Token: 0x06000040 RID: 64
		T Add(T a, T b);

		// Token: 0x06000041 RID: 65
		T Subtract(T a, T b);

		// Token: 0x06000042 RID: 66
		T Multiply(T a, T b);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67
		T Zero { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68
		T NegativeInfinity { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69
		T PositiveInfinity { get; }

		// Token: 0x06000046 RID: 70
		T DistanceSquaredBetweenPoints(T[] a, T[] b);
	}
}
