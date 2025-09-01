using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200008D RID: 141
	public static class EffectAreaExtensions
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00026E88 File Offset: 0x00025088
		public static Rect GetEffectArea(this EffectArea area, VertexHelper vh, Rect rectangle, float aspectRatio = -1f)
		{
			Rect result = default(Rect);
			switch (area)
			{
			case EffectArea.RectTransform:
				result = rectangle;
				break;
			case EffectArea.Fit:
			{
				UIVertex uivertex = default(UIVertex);
				float num = float.MaxValue;
				float num2 = float.MaxValue;
				float num3 = float.MinValue;
				float num4 = float.MinValue;
				for (int i = 0; i < vh.currentVertCount; i++)
				{
					vh.PopulateUIVertex(ref uivertex, i);
					float x = uivertex.position.x;
					float y = uivertex.position.y;
					num = Mathf.Min(num, x);
					num2 = Mathf.Min(num2, y);
					num3 = Mathf.Max(num3, x);
					num4 = Mathf.Max(num4, y);
				}
				result.Set(num, num2, num3 - num, num4 - num2);
				break;
			}
			case EffectArea.Character:
				result = EffectAreaExtensions.rectForCharacter;
				break;
			default:
				result = rectangle;
				break;
			}
			if (0f < aspectRatio)
			{
				if (result.width < result.height)
				{
					result.width = result.height * aspectRatio;
				}
				else
				{
					result.height = result.width / aspectRatio;
				}
			}
			return result;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00026F9C File Offset: 0x0002519C
		public static void GetPositionFactor(this EffectArea area, int index, Rect rect, Vector2 position, bool isText, bool isTMPro, out float x, out float y)
		{
			if (isText && area == EffectArea.Character)
			{
				index = (isTMPro ? ((index + 3) % 4) : (index % 4));
				x = EffectAreaExtensions.splitedCharacterPosition[index].x;
				y = EffectAreaExtensions.splitedCharacterPosition[index].y;
				return;
			}
			if (area == EffectArea.Fit)
			{
				x = Mathf.Clamp01((position.x - rect.xMin) / rect.width);
				y = Mathf.Clamp01((position.y - rect.yMin) / rect.height);
				return;
			}
			x = Mathf.Clamp01(position.x / rect.width + 0.5f);
			y = Mathf.Clamp01(position.y / rect.height + 0.5f);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00027061 File Offset: 0x00025261
		public static void GetNormalizedFactor(this EffectArea area, int index, Matrix2x3 matrix, Vector2 position, bool isText, out Vector2 nomalizedPos)
		{
			if (isText && area == EffectArea.Character)
			{
				nomalizedPos = matrix * EffectAreaExtensions.splitedCharacterPosition[index % 4];
				return;
			}
			nomalizedPos = matrix * position;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00027094 File Offset: 0x00025294
		// Note: this type is marked as 'beforefieldinit'.
		static EffectAreaExtensions()
		{
		}

		// Token: 0x040004DA RID: 1242
		private static readonly Rect rectForCharacter = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040004DB RID: 1243
		private static readonly Vector2[] splitedCharacterPosition = new Vector2[]
		{
			Vector2.up,
			Vector2.one,
			Vector2.right,
			Vector2.zero
		};
	}
}
