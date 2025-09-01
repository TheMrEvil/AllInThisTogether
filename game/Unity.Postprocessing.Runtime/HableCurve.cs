using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000064 RID: 100
	public class HableCurve
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000103C6 File Offset: 0x0000E5C6
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000103CE File Offset: 0x0000E5CE
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000103D7 File Offset: 0x0000E5D7
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000103DF File Offset: 0x0000E5DF
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000103E8 File Offset: 0x0000E5E8
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000103F0 File Offset: 0x0000E5F0
		internal float x0
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000103F9 File Offset: 0x0000E5F9
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00010401 File Offset: 0x0000E601
		internal float x1
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

		// Token: 0x060001FB RID: 507 RVA: 0x0001040C File Offset: 0x0000E60C
		public HableCurve()
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_Segments[i] = new HableCurve.Segment();
			}
			this.uniforms = new HableCurve.Uniforms(this);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00010450 File Offset: 0x0000E650
		public float Eval(float x)
		{
			float num = x * this.inverseWhitePoint;
			int num2 = (num < this.x0) ? 0 : ((num < this.x1) ? 1 : 2);
			return this.m_Segments[num2].Eval(num);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00010490 File Offset: 0x0000E690
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
			float num6 = RuntimeUtilities.Exp2(shoulderLength) - 1f;
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

		// Token: 0x060001FE RID: 510 RVA: 0x000105A4 File Offset: 0x0000E7A4
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
			HableCurve.Segment segment = this.m_Segments[1];
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
			HableCurve.Segment segment2 = this.m_Segments[0];
			segment2.offsetX = 0f;
			segment2.offsetY = 0f;
			segment2.scaleX = 1f;
			segment2.scaleY = 1f;
			float lnA;
			float b;
			this.SolveAB(out lnA, out b, directParams.x0, directParams.y0, m);
			segment2.lnA = lnA;
			segment2.B = b;
			HableCurve.Segment segment3 = this.m_Segments[2];
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
			float num3 = this.m_Segments[2].Eval(1f);
			float num4 = 1f / num3;
			this.m_Segments[0].offsetY *= num4;
			this.m_Segments[0].scaleY *= num4;
			this.m_Segments[1].offsetY *= num4;
			this.m_Segments[1].scaleY *= num4;
			this.m_Segments[2].offsetY *= num4;
			this.m_Segments[2].scaleY *= num4;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000108BD File Offset: 0x0000EABD
		private void SolveAB(out float lnA, out float B, float x0, float y0, float m)
		{
			B = m * x0 / y0;
			lnA = Mathf.Log(y0) - B * Mathf.Log(x0);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000108DC File Offset: 0x0000EADC
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

		// Token: 0x06000201 RID: 513 RVA: 0x00010913 File Offset: 0x0000EB13
		private float EvalDerivativeLinearGamma(float m, float b, float g, float x)
		{
			return g * m * Mathf.Pow(m * x + b, g - 1f);
		}

		// Token: 0x04000213 RID: 531
		[CompilerGenerated]
		private float <whitePoint>k__BackingField;

		// Token: 0x04000214 RID: 532
		[CompilerGenerated]
		private float <inverseWhitePoint>k__BackingField;

		// Token: 0x04000215 RID: 533
		[CompilerGenerated]
		private float <x0>k__BackingField;

		// Token: 0x04000216 RID: 534
		[CompilerGenerated]
		private float <x1>k__BackingField;

		// Token: 0x04000217 RID: 535
		private readonly HableCurve.Segment[] m_Segments = new HableCurve.Segment[3];

		// Token: 0x04000218 RID: 536
		public readonly HableCurve.Uniforms uniforms;

		// Token: 0x02000095 RID: 149
		private class Segment
		{
			// Token: 0x06000290 RID: 656 RVA: 0x00013370 File Offset: 0x00011570
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

			// Token: 0x06000291 RID: 657 RVA: 0x000133C4 File Offset: 0x000115C4
			public Segment()
			{
			}

			// Token: 0x04000382 RID: 898
			public float offsetX;

			// Token: 0x04000383 RID: 899
			public float offsetY;

			// Token: 0x04000384 RID: 900
			public float scaleX;

			// Token: 0x04000385 RID: 901
			public float scaleY;

			// Token: 0x04000386 RID: 902
			public float lnA;

			// Token: 0x04000387 RID: 903
			public float B;
		}

		// Token: 0x02000096 RID: 150
		private struct DirectParams
		{
			// Token: 0x04000388 RID: 904
			internal float x0;

			// Token: 0x04000389 RID: 905
			internal float y0;

			// Token: 0x0400038A RID: 906
			internal float x1;

			// Token: 0x0400038B RID: 907
			internal float y1;

			// Token: 0x0400038C RID: 908
			internal float W;

			// Token: 0x0400038D RID: 909
			internal float overshootX;

			// Token: 0x0400038E RID: 910
			internal float overshootY;

			// Token: 0x0400038F RID: 911
			internal float gamma;
		}

		// Token: 0x02000097 RID: 151
		public class Uniforms
		{
			// Token: 0x06000292 RID: 658 RVA: 0x000133CC File Offset: 0x000115CC
			internal Uniforms(HableCurve parent)
			{
				this.parent = parent;
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000293 RID: 659 RVA: 0x000133DB File Offset: 0x000115DB
			public Vector4 curve
			{
				get
				{
					return new Vector4(this.parent.inverseWhitePoint, this.parent.x0, this.parent.x1, 0f);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000294 RID: 660 RVA: 0x00013408 File Offset: 0x00011608
			public Vector4 toeSegmentA
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[0];
					return new Vector4(segment.offsetX, segment.offsetY, segment.scaleX, segment.scaleY);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000295 RID: 661 RVA: 0x00013440 File Offset: 0x00011640
			public Vector4 toeSegmentB
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[0];
					return new Vector4(segment.lnA, segment.B, 0f, 0f);
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000296 RID: 662 RVA: 0x00013478 File Offset: 0x00011678
			public Vector4 midSegmentA
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[1];
					return new Vector4(segment.offsetX, segment.offsetY, segment.scaleX, segment.scaleY);
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000297 RID: 663 RVA: 0x000134B0 File Offset: 0x000116B0
			public Vector4 midSegmentB
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[1];
					return new Vector4(segment.lnA, segment.B, 0f, 0f);
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000298 RID: 664 RVA: 0x000134E8 File Offset: 0x000116E8
			public Vector4 shoSegmentA
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[2];
					return new Vector4(segment.offsetX, segment.offsetY, segment.scaleX, segment.scaleY);
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000299 RID: 665 RVA: 0x00013520 File Offset: 0x00011720
			public Vector4 shoSegmentB
			{
				get
				{
					HableCurve.Segment segment = this.parent.m_Segments[2];
					return new Vector4(segment.lnA, segment.B, 0f, 0f);
				}
			}

			// Token: 0x04000390 RID: 912
			private HableCurve parent;
		}
	}
}
