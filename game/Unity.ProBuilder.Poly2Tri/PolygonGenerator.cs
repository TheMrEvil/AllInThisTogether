using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001D RID: 29
	internal class PolygonGenerator
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004F7C File Offset: 0x0000317C
		public static Polygon RandomCircleSweep(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			PolygonPoint[] array = new PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					if (i % 250 == 0)
					{
						num += scale / 2.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					else if (i % 50 == 0)
					{
						num += scale / 5.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					else
					{
						num += 25.0 * scale / (double)vertexCount * (0.5 - PolygonGenerator.RNG.NextDouble());
					}
					num = ((num > scale / 2.0) ? (scale / 2.0) : num);
					num = ((num < scale / 10.0) ? (scale / 10.0) : num);
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				PolygonPoint polygonPoint = new PolygonPoint(num * Math.Cos(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), num * Math.Sin(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), i);
				array[i] = polygonPoint;
			}
			return new Polygon(array);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000050C0 File Offset: 0x000032C0
		public static Polygon RandomCircleSweep2(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			PolygonPoint[] array = new PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					num += scale / 5.0 * (0.5 - PolygonGenerator.RNG.NextDouble());
					num = ((num > scale / 2.0) ? (scale / 2.0) : num);
					num = ((num < scale / 10.0) ? (scale / 10.0) : num);
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				PolygonPoint polygonPoint = new PolygonPoint(num * Math.Cos(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), num * Math.Sin(PolygonGenerator.PI_2 * (double)i / (double)vertexCount), i);
				array[i] = polygonPoint;
			}
			return new Polygon(array);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000051A0 File Offset: 0x000033A0
		public PolygonGenerator()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000051A8 File Offset: 0x000033A8
		// Note: this type is marked as 'beforefieldinit'.
		static PolygonGenerator()
		{
		}

		// Token: 0x0400004E RID: 78
		private static readonly Random RNG = new Random();

		// Token: 0x0400004F RID: 79
		private static double PI_2 = 6.283185307179586;
	}
}
