using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000D RID: 13
	internal static class Clipping
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000040E4 File Offset: 0x000022E4
		private static Clipping.OutCode ComputeOutCode(Rect rect, float x, float y)
		{
			Clipping.OutCode outCode = Clipping.OutCode.Inside;
			if (x < rect.xMin)
			{
				outCode |= Clipping.OutCode.Left;
			}
			else if (x > rect.xMax)
			{
				outCode |= Clipping.OutCode.Right;
			}
			if (y < rect.yMin)
			{
				outCode |= Clipping.OutCode.Bottom;
			}
			else if (y > rect.yMax)
			{
				outCode |= Clipping.OutCode.Top;
			}
			return outCode;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004130 File Offset: 0x00002330
		internal static bool RectContainsLineSegment(Rect rect, float x0, float y0, float x1, float y1)
		{
			Clipping.OutCode outCode = Clipping.ComputeOutCode(rect, x0, y0);
			Clipping.OutCode outCode2 = Clipping.ComputeOutCode(rect, x1, y1);
			bool result = false;
			while ((outCode | outCode2) != Clipping.OutCode.Inside)
			{
				if ((outCode & outCode2) != Clipping.OutCode.Inside)
				{
					return result;
				}
				float num = 0f;
				float num2 = 0f;
				Clipping.OutCode outCode3 = (outCode != Clipping.OutCode.Inside) ? outCode : outCode2;
				if ((outCode3 & Clipping.OutCode.Top) == Clipping.OutCode.Top)
				{
					num = x0 + (x1 - x0) * (rect.yMax - y0) / (y1 - y0);
					num2 = rect.yMax;
				}
				else if ((outCode3 & Clipping.OutCode.Bottom) == Clipping.OutCode.Bottom)
				{
					num = x0 + (x1 - x0) * (rect.yMin - y0) / (y1 - y0);
					num2 = rect.yMin;
				}
				else if ((outCode3 & Clipping.OutCode.Right) == Clipping.OutCode.Right)
				{
					num2 = y0 + (y1 - y0) * (rect.xMax - x0) / (x1 - x0);
					num = rect.xMax;
				}
				else if ((outCode3 & Clipping.OutCode.Left) == Clipping.OutCode.Left)
				{
					num2 = y0 + (y1 - y0) * (rect.xMin - x0) / (x1 - x0);
					num = rect.xMin;
				}
				if (outCode3 == outCode)
				{
					x0 = num;
					y0 = num2;
					outCode = Clipping.ComputeOutCode(rect, x0, y0);
				}
				else
				{
					x1 = num;
					y1 = num2;
					outCode2 = Clipping.ComputeOutCode(rect, x1, y1);
				}
			}
			return true;
		}

		// Token: 0x02000093 RID: 147
		[Flags]
		private enum OutCode
		{
			// Token: 0x04000293 RID: 659
			Inside = 0,
			// Token: 0x04000294 RID: 660
			Left = 1,
			// Token: 0x04000295 RID: 661
			Right = 2,
			// Token: 0x04000296 RID: 662
			Bottom = 4,
			// Token: 0x04000297 RID: 663
			Top = 8
		}
	}
}
