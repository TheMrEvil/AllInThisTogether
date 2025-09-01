using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001B RID: 27
	internal class TriangulationUtil
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public static bool SmartIncircle(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc, TriangulationPoint pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			double num7 = num5 - num6;
			if (num7 <= 0.0)
			{
				return false;
			}
			double num8 = pc.X - x;
			double num9 = pc.Y - y;
			double num10 = num8 * num2;
			double num11 = num * num9;
			double num12 = num10 - num11;
			if (num12 <= 0.0)
			{
				return false;
			}
			double num13 = num3 * num9;
			double num14 = num8 * num4;
			double num15 = num * num + num2 * num2;
			double num16 = num3 * num3 + num4 * num4;
			double num17 = num8 * num8 + num9 * num9;
			return num15 * (num13 - num14) + num16 * num12 + num17 * num7 > 0.0;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004D94 File Offset: 0x00002F94
		public static bool InScanArea(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc, TriangulationPoint pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			if (num5 - num6 <= 0.0)
			{
				return false;
			}
			double num7 = pc.X - x;
			double num8 = pc.Y - y;
			double num9 = num7 * num2;
			double num10 = num * num8;
			return num9 - num10 > 0.0;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004E1C File Offset: 0x0000301C
		public static Orientation Orient2d(TriangulationPoint pa, TriangulationPoint pb, TriangulationPoint pc)
		{
			double num = (pa.X - pc.X) * (pb.Y - pc.Y);
			double num2 = (pa.Y - pc.Y) * (pb.X - pc.X);
			double num3 = num - num2;
			if (num3 > -TriangulationUtil.EPSILON && num3 < TriangulationUtil.EPSILON)
			{
				return Orientation.Collinear;
			}
			if (num3 > 0.0)
			{
				return Orientation.CCW;
			}
			return Orientation.CW;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004E85 File Offset: 0x00003085
		public TriangulationUtil()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004E8D File Offset: 0x0000308D
		// Note: this type is marked as 'beforefieldinit'.
		static TriangulationUtil()
		{
		}

		// Token: 0x0400004C RID: 76
		public static double EPSILON = 1E-12;
	}
}
