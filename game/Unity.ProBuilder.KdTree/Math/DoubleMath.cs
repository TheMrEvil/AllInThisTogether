using System;

namespace UnityEngine.ProBuilder.KdTree.Math
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal class DoubleMath : TypeMath<double>
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002FD8 File Offset: 0x000011D8
		public override int Compare(double a, double b)
		{
			return a.CompareTo(b);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FE2 File Offset: 0x000011E2
		public override bool AreEqual(double a, double b)
		{
			return a == b;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FE8 File Offset: 0x000011E8
		public override double MinValue
		{
			get
			{
				return double.MinValue;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002FF3 File Offset: 0x000011F3
		public override double MaxValue
		{
			get
			{
				return double.MaxValue;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002FFE File Offset: 0x000011FE
		public override double Zero
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003009 File Offset: 0x00001209
		public override double NegativeInfinity
		{
			get
			{
				return double.NegativeInfinity;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003014 File Offset: 0x00001214
		public override double PositiveInfinity
		{
			get
			{
				return double.PositiveInfinity;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000301F File Offset: 0x0000121F
		public override double Add(double a, double b)
		{
			return a + b;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003024 File Offset: 0x00001224
		public override double Subtract(double a, double b)
		{
			return a - b;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003029 File Offset: 0x00001229
		public override double Multiply(double a, double b)
		{
			return a * b;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003030 File Offset: 0x00001230
		public override double DistanceSquaredBetweenPoints(double[] a, double[] b)
		{
			double num = this.Zero;
			int num2 = a.Length;
			for (int i = 0; i < num2; i++)
			{
				double num3 = this.Subtract(a[i], b[i]);
				double b2 = this.Multiply(num3, num3);
				num = this.Add(num, b2);
			}
			return num;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003076 File Offset: 0x00001276
		public DoubleMath()
		{
		}
	}
}
