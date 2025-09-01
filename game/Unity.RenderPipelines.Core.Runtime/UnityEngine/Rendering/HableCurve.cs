using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AA RID: 170
	public class HableCurve
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001B4F0 File Offset: 0x000196F0
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public float whitePoint
		{
			[CompilerGenerated]
			get
			{
				return this.<whitePoint>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<whitePoint>k__BackingField = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001B501 File Offset: 0x00019701
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001B509 File Offset: 0x00019709
		public float inverseWhitePoint
		{
			[CompilerGenerated]
			get
			{
				return this.<inverseWhitePoint>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<inverseWhitePoint>k__BackingField = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001B512 File Offset: 0x00019712
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001B51A File Offset: 0x0001971A
		public float x0
		{
			[CompilerGenerated]
			get
			{
				return this.<x0>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<x0>k__BackingField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001B523 File Offset: 0x00019723
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001B52B File Offset: 0x0001972B
		public float x1
		{
			[CompilerGenerated]
			get
			{
				return this.<x1>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<x1>k__BackingField = value;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B534 File Offset: 0x00019734
		public HableCurve()
		{
			for (int i = 0; i < 3; i++)
			{
				this.segments[i] = new HableCurve.Segment();
			}
			this.uniforms = new HableCurve.Uniforms(this);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B578 File Offset: 0x00019778
		public float Eval(float x)
		{
			float num = x * this.inverseWhitePoint;
			int num2 = (num < this.x0) ? 0 : ((num < this.x1) ? 1 : 2);
			return this.segments[num2].Eval(num);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public void Init(float toeStrength, float toeLength, float shoulderStrength, float shoulderLength, float shoulderAngle, float gamma)
		{
			HableCurve.DirectParams directParams = default(HableCurve.DirectParams);
			toeLength = Mathf.Pow(Mathf.Clamp01(toeLength), 2.2f);
			toeStrength = Mathf.Clamp01(toeStrength);
			shoulderAngle = Mathf.Clamp01(shoulderAngle);
			shoulderStrength = Mathf.Clamp(shoulderStrength, 1E-05f, 0.99999f);
			shoulderLength = Mathf.Max(0f, shoulderLength);
			gamma = Mathf.Max(1E-05f, gamma);
			float num = toeLength * 0.5f;
			float num2 = (1f - toeStrength) * num;
			float num3 = 1f - num2;
			float num4 = num + num3;
			float num5 = (1f - shoulderStrength) * num3;
			float x = num + num5;
			float y = num2 + num5;
			float num6 = Mathf.Pow(2f, shoulderLength) - 1f;
			float w = num4 + num6;
			directParams.x0 = num;
			directParams.y0 = num2;
			directParams.x1 = x;
			directParams.y1 = y;
			directParams.W = w;
			directParams.gamma = gamma;
			directParams.overshootX = directParams.W * 2f * shoulderAngle * shoulderLength;
			directParams.overshootY = 0.5f * shoulderAngle * shoulderLength;
			this.InitSegments(directParams);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001B6D4 File Offset: 0x000198D4
		private void InitSegments(HableCurve.DirectParams srcParams)
		{
			HableCurve.DirectParams directParams = srcParams;
			this.whitePoint = srcParams.W;
			this.inverseWhitePoint = 1f / srcParams.W;
			directParams.W = 1f;
			directParams.x0 /= srcParams.W;
			directParams.x1 /= srcParams.W;
			directParams.overshootX = srcParams.overshootX / srcParams.W;
			float num;
			float num2;
			this.AsSlopeIntercept(out num, out num2, directParams.x0, directParams.x1, directParams.y0, directParams.y1);
			float gamma = srcParams.gamma;
			HableCurve.Segment segment = this.segments[1];
			segment.offsetX = -(num2 / num);
			segment.offsetY = 0f;
			segment.scaleX = 1f;
			segment.scaleY = 1f;
			segment.lnA = gamma * Mathf.Log(num);
			segment.B = gamma;
			float m = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x0);
			float m2 = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x1);
			directParams.y0 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y0, directParams.gamma));
			directParams.y1 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y1, directParams.gamma));
			directParams.overshootY = Mathf.Pow(1f + directParams.overshootY, directParams.gamma) - 1f;
			this.x0 = directParams.x0;
			this.x1 = directParams.x1;
			HableCurve.Segment segment2 = this.segments[0];
			segment2.offsetX = 0f;
			segment2.offsetY = 0f;
			segment2.scaleX = 1f;
			segment2.scaleY = 1f;
			float lnA;
			float b;
			this.SolveAB(out lnA, out b, directParams.x0, directParams.y0, m);
			segment2.lnA = lnA;
			segment2.B = b;
			HableCurve.Segment segment3 = this.segments[2];
			float x = 1f + directParams.overshootX - directParams.x1;
			float y = 1f + directParams.overshootY - directParams.y1;
			float lnA2;
			float b2;
			this.SolveAB(out lnA2, out b2, x, y, m2);
			segment3.offsetX = 1f + directParams.overshootX;
			segment3.offsetY = 1f + directParams.overshootY;
			segment3.scaleX = -1f;
			segment3.scaleY = -1f;
			segment3.lnA = lnA2;
			segment3.B = b2;
			float num3 = this.segments[2].Eval(1f);
			float num4 = 1f / num3;
			this.segments[0].offsetY *= num4;
			this.segments[0].scaleY *= num4;
			this.segments[1].offsetY *= num4;
			this.segments[1].scaleY *= num4;
			this.segments[2].offsetY *= num4;
			this.segments[2].scaleY *= num4;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B9ED File Offset: 0x00019BED
		private void SolveAB(out float lnA, out float B, float x0, float y0, float m)
		{
			B = m * x0 / y0;
			lnA = Mathf.Log(y0) - B * Mathf.Log(x0);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BA0C File Offset: 0x00019C0C
		private void AsSlopeIntercept(out float m, out float b, float x0, float x1, float y0, float y1)
		{
			float num = y1 - y0;
			float num2 = x1 - x0;
			if (num2 == 0f)
			{
				m = 1f;
			}
			else
			{
				m = num / num2;
			}
			b = y0 - x0 * m;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BA43 File Offset: 0x00019C43
		private float EvalDerivativeLinearGamma(float m, float b, float g, float x)
		{
			return g * m * Mathf.Pow(m * x + b, g - 1f);
		}

		// Token: 0x04000370 RID: 880
		[CompilerGenerated]
		private float <whitePoint>k__BackingField;

		// Token: 0x04000371 RID: 881
		[CompilerGenerated]
		private float <inverseWhitePoint>k__BackingField;

		// Token: 0x04000372 RID: 882
		[CompilerGenerated]
		private float <x0>k__BackingField;

		// Token: 0x04000373 RID: 883
		[CompilerGenerated]
		private float <x1>k__BackingField;

		// Token: 0x04000374 RID: 884
		public readonly HableCurve.Segment[] segments = new HableCurve.Segment[3];

		// Token: 0x04000375 RID: 885
		public readonly HableCurve.Uniforms uniforms;

		// Token: 0x02000179 RID: 377
		public class Segment
		{
			// Token: 0x0600090D RID: 2317 RVA: 0x000247B4 File Offset: 0x000229B4
			public float Eval(float x)
			{
				float num = (x - this.offsetX) * this.scaleX;
				float num2 = 0f;
				if (num > 0f)
				{
					num2 = Mathf.Exp(this.lnA + this.B * Mathf.Log(num));
				}
				return num2 * this.scaleY + this.offsetY;
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x00024808 File Offset: 0x00022A08
			public Segment()
			{
			}

			// Token: 0x040005AD RID: 1453
			public float offsetX;

			// Token: 0x040005AE RID: 1454
			public float offsetY;

			// Token: 0x040005AF RID: 1455
			public float scaleX;

			// Token: 0x040005B0 RID: 1456
			public float scaleY;

			// Token: 0x040005B1 RID: 1457
			public float lnA;

			// Token: 0x040005B2 RID: 1458
			public float B;
		}

		// Token: 0x0200017A RID: 378
		private struct DirectParams
		{
			// Token: 0x040005B3 RID: 1459
			internal float x0;

			// Token: 0x040005B4 RID: 1460
			internal float y0;

			// Token: 0x040005B5 RID: 1461
			internal float x1;

			// Token: 0x040005B6 RID: 1462
			internal float y1;

			// Token: 0x040005B7 RID: 1463
			internal float W;

			// Token: 0x040005B8 RID: 1464
			internal float overshootX;

			// Token: 0x040005B9 RID: 1465
			internal float overshootY;

			// Token: 0x040005BA RID: 1466
			internal float gamma;
		}

		// Token: 0x0200017B RID: 379
		public class Uniforms
		{
			// Token: 0x0600090F RID: 2319 RVA: 0x00024810 File Offset: 0x00022A10
			internal Uniforms(HableCurve parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002481F File Offset: 0x00022A1F
			public Vector4 curve
			{
				get
				{
					return new Vector4(this.parent.inverseWhitePoint, this.parent.x0, this.parent.x1, 0f);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002484C File Offset: 0x00022A4C
			public Vector4 toeSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[0].offsetX, this.parent.segments[0].offsetY, this.parent.segments[0].scaleX, this.parent.segments[0].scaleY);
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000912 RID: 2322 RVA: 0x000248A6 File Offset: 0x00022AA6
			public Vector4 toeSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[0].lnA, this.parent.segments[0].B, 0f, 0f);
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000913 RID: 2323 RVA: 0x000248DC File Offset: 0x00022ADC
			public Vector4 midSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[1].offsetX, this.parent.segments[1].offsetY, this.parent.segments[1].scaleX, this.parent.segments[1].scaleY);
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000914 RID: 2324 RVA: 0x00024936 File Offset: 0x00022B36
			public Vector4 midSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[1].lnA, this.parent.segments[1].B, 0f, 0f);
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002496C File Offset: 0x00022B6C
			public Vector4 shoSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[2].offsetX, this.parent.segments[2].offsetY, this.parent.segments[2].scaleX, this.parent.segments[2].scaleY);
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000916 RID: 2326 RVA: 0x000249C6 File Offset: 0x00022BC6
			public Vector4 shoSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[2].lnA, this.parent.segments[2].B, 0f, 0f);
				}
			}

			// Token: 0x040005BB RID: 1467
			private HableCurve parent;
		}
	}
}
