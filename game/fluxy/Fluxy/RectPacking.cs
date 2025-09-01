using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x02000012 RID: 18
	public static class RectPacking
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00005B34 File Offset: 0x00003D34
		public static Vector2 Pack(Vector4[] rects, int[] indices, int first, int length, int margin)
		{
			RectPacking.RectComparer comparer = new RectPacking.RectComparer();
			Array.Sort<Vector4, int>(rects, indices, first, length, comparer);
			Array.Sort<Vector4>(rects, first, length, comparer);
			float num = 0f;
			float num2 = 0f;
			for (int i = first; i < first + length; i++)
			{
				num += (rects[i].z + (float)margin) * (rects[i].w + (float)margin);
				num2 = Mathf.Max(num2, rects[i].z + (float)margin);
			}
			float width = Mathf.Max(Mathf.Ceil(Mathf.Sqrt(num / 0.95f)), num2);
			List<Rect> list = new List<Rect>
			{
				new Rect(0f, 0f, width, float.PositiveInfinity)
			};
			Vector2 zero = Vector2.zero;
			for (int j = first; j < first + length; j++)
			{
				int k = list.Count - 1;
				while (k >= 0)
				{
					Rect value = list[k];
					if (rects[j].z + (float)margin <= value.width && rects[j].w + (float)margin <= value.height)
					{
						rects[j].x = list[k].x + (float)margin;
						rects[j].y = list[k].y + (float)margin;
						if ((int)rects[j].z + margin == (int)value.width && (int)rects[j].w + margin == (int)value.height)
						{
							value = list[list.Count - 1];
							list.RemoveAt(list.Count - 1);
						}
						else if ((int)rects[j].w + margin == (int)value.height)
						{
							value.xMin += rects[j].z + (float)margin;
						}
						else if ((int)rects[j].z + margin == (int)value.width)
						{
							value.yMin += rects[j].w + (float)margin;
						}
						else
						{
							list.Add(new Rect(value.x + rects[j].z + (float)margin, value.y, value.width - rects[j].z - (float)margin, rects[j].w + (float)margin));
							value.yMin += rects[j].w + (float)margin;
						}
						if (k < list.Count)
						{
							list[k] = value;
							break;
						}
						break;
					}
					else
					{
						k--;
					}
				}
				zero.x = Mathf.Max(zero.x, rects[j].x + rects[j].z + (float)margin);
				zero.y = Mathf.Max(zero.y, rects[j].y + rects[j].w + (float)margin);
			}
			return zero;
		}

		// Token: 0x0200002E RID: 46
		private class RectComparer : IComparer<Vector4>
		{
			// Token: 0x060000B8 RID: 184 RVA: 0x00006F08 File Offset: 0x00005108
			public int Compare(Vector4 a, Vector4 b)
			{
				int num = b.w.CompareTo(a.w);
				if (num == 0)
				{
					return b.z.CompareTo(a.z);
				}
				return num;
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00006F3F File Offset: 0x0000513F
			public RectComparer()
			{
			}
		}
	}
}
