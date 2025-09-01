using System;

namespace UnityEngine.ProBuilder.KdTree.Math
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class FloatMath : TypeMath<float>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000307E File Offset: 0x0000127E
		public override int Compare(float a, float b)
		{
			return a.CompareTo(b);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003088 File Offset: 0x00001288
		public override bool AreEqual(float a, float b)
		{
			return a == b;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000308E File Offset: 0x0000128E
		public override float MinValue
		{
			get
			{
				return float.MinValue;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003095 File Offset: 0x00001295
		public override float MaxValue
		{
			get
			{
				return float.MaxValue;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000309C File Offset: 0x0000129C
		public override float Zero
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000030A3 File Offset: 0x000012A3
		public override float NegativeInfinity
		{
			get
			{
				return float.NegativeInfinity;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000030AA File Offset: 0x000012AA
		public override float PositiveInfinity
		{
			get
			{
				return float.PositiveInfinity;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030B1 File Offset: 0x000012B1
		public override float Add(float a, float b)
		{
			return a + b;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030B6 File Offset: 0x000012B6
		public override float Subtract(float a, float b)
		{
			return a - b;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030BB File Offset: 0x000012BB
		public override float Multiply(float a, float b)
		{
			return a * b;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030C0 File Offset: 0x000012C0
		public override float DistanceSquaredBetweenPoints(float[] a, float[] b)
		{
			float num = this.Zero;
			int num2 = a.Length;
			for (int i = 0; i < num2; i++)
			{
				float num3 = this.Subtract(a[i], b[i]);
				float b2 = this.Multiply(num3, num3);
				num = this.Add(num, b2);
			}
			return num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003106 File Offset: 0x00001306
		public FloatMath()
		{
		}
	}
}
