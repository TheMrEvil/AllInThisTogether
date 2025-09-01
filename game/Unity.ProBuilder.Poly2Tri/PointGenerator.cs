using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001C RID: 28
	internal class PointGenerator
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static List<TriangulationPoint> UniformDistribution(int n, double scale)
		{
			List<TriangulationPoint> list = new List<TriangulationPoint>();
			for (int i = 0; i < n; i++)
			{
				list.Add(new TriangulationPoint(scale * (0.5 - PointGenerator.RNG.NextDouble()), scale * (0.5 - PointGenerator.RNG.NextDouble()), i));
			}
			return list;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004EF8 File Offset: 0x000030F8
		public static List<TriangulationPoint> UniformGrid(int n, double scale)
		{
			double num = scale / (double)n;
			double num2 = 0.5 * scale;
			List<TriangulationPoint> list = new List<TriangulationPoint>();
			for (int i = 0; i < n + 1; i++)
			{
				double x = num2 - (double)i * num;
				for (int j = 0; j < n + 1; j++)
				{
					list.Add(new TriangulationPoint(x, num2 - (double)j * num, i));
				}
			}
			return list;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F68 File Offset: 0x00003168
		public PointGenerator()
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F70 File Offset: 0x00003170
		// Note: this type is marked as 'beforefieldinit'.
		static PointGenerator()
		{
		}

		// Token: 0x0400004D RID: 77
		private static readonly Random RNG = new Random();
	}
}
