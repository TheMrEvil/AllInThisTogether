using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{
	// Token: 0x0200000F RID: 15
	internal class ShadowSettingSnapshot
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000457C File Offset: 0x0000277C
		internal ShadowSettingSnapshot(TrueShadow shadow)
		{
			this.shadow = shadow;
			this.canvas = shadow.Graphic.canvas;
			this.canvasRt = (RectTransform)this.canvas.transform;
			Bounds bounds;
			if (shadow.SpriteMesh)
			{
				bounds = shadow.SpriteMesh.bounds;
			}
			else
			{
				bounds = new Bounds(Vector3.zero, Vector3.zero);
			}
			this.canvasScale = this.canvas.scaleFactor;
			Quaternion quaternion = Quaternion.Inverse(this.canvasRt.rotation) * shadow.RectTransform.rotation;
			this.canvasRelativeOffset = shadow.Offset.Rotate(-quaternion.eulerAngles.z) * this.canvasScale;
			this.dimensions = bounds.size * this.canvasScale;
			this.size = shadow.Size * this.canvasScale;
			this.CalcHash();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000467C File Offset: 0x0000287C
		private void CalcHash()
		{
			Graphic graphic = this.shadow.Graphic;
			int h = (int)((double)this.canvasScale * 10000.0);
			int h2 = this.shadow.Inset ? 1 : 0;
			Color clearColor = this.shadow.ClearColor;
			Color color = graphic.color;
			if (this.shadow.IgnoreCasterColor)
			{
				color = Color.clear;
			}
			int h3 = HashUtils.CombineHashCodes(this.shadow.IgnoreCasterColor ? 1 : 0, (int)this.shadow.ColorBleedMode, (int)(color.r * 255f), (int)(color.g * 255f), (int)(color.b * 255f), (int)(color.a * 255f), (int)(clearColor.r * 255f), (int)(clearColor.g * 255f), (int)(clearColor.b * 255f), (int)(clearColor.a * 255f));
			int h4 = HashUtils.CombineHashCodes(this.shadow.Cutout ? 1 : 0, (int)(this.canvasRelativeOffset.x * 100f), (int)(this.canvasRelativeOffset.y * 100f));
			Image image = graphic as Image;
			int h5 = (image != null && image.type == Image.Type.Tiled) ? this.dimensions.GetHashCode() : HashUtils.CombineHashCodes(Mathf.CeilToInt(this.dimensions.x / 1f), Mathf.CeilToInt(this.dimensions.y / 1f));
			int h6 = Mathf.CeilToInt(this.size * 100f);
			int h7 = Mathf.CeilToInt(this.shadow.Spread * 100f);
			int h8 = HashUtils.CombineHashCodes(graphic.materialForRendering.ComputeCRC(), h, h2, h3, h4, h5, h6, h7, this.shadow.CustomHash);
			Image image2 = graphic as Image;
			if (image2 != null)
			{
				int h9 = 0;
				if (image2.sprite)
				{
					h9 = image2.sprite.GetHashCode();
				}
				int h10 = HashUtils.CombineHashCodes((int)image2.type, (int)(image2.fillAmount * 360f * 20f), (int)image2.fillMethod, image2.fillOrigin, image2.fillClockwise ? 1 : 0);
				this.hash = HashUtils.CombineHashCodes(h8, h9, h10);
				return;
			}
			RawImage rawImage = graphic as RawImage;
			if (rawImage != null)
			{
				int h11 = 0;
				if (rawImage.texture)
				{
					h11 = rawImage.texture.GetInstanceID();
				}
				this.hash = HashUtils.CombineHashCodes(h8, h11);
				return;
			}
			Text text = graphic as Text;
			if (text != null)
			{
				this.hash = HashUtils.CombineHashCodes(h8, text.text.GetHashCode(), text.font.GetHashCode(), (int)text.alignment);
				return;
			}
			TextMeshProUGUI textMeshProUGUI = graphic as TextMeshProUGUI;
			if (textMeshProUGUI != null)
			{
				this.hash = HashUtils.CombineHashCodes(h8, this.CalcTMPHash(textMeshProUGUI));
				return;
			}
			TMP_SubMeshUI tmp_SubMeshUI = graphic as TMP_SubMeshUI;
			if (tmp_SubMeshUI == null)
			{
				this.hash = h8;
				return;
			}
			this.hash = HashUtils.CombineHashCodes(h8, this.CalcTMPHash(tmp_SubMeshUI.textComponent));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000049B4 File Offset: 0x00002BB4
		private int CalcTMPHash(TMP_Text tmp)
		{
			int h = 0;
			if (!this.shadow.IgnoreCasterColor)
			{
				h = HashUtils.CombineHashCodes(tmp.enableVertexGradient.GetHashCode(), tmp.colorGradient.GetHashCode(), tmp.overrideColorTags.GetHashCode());
			}
			string text = tmp.text;
			return HashUtils.CombineHashCodes((text != null) ? text.GetHashCode() : 0, Mathf.CeilToInt(tmp.transform.lossyScale.y * 100f), tmp.font.GetHashCode(), tmp.fontSize.GetHashCode(), h, tmp.characterSpacing.GetHashCode(), tmp.wordSpacing.GetHashCode(), tmp.lineSpacing.GetHashCode(), tmp.paragraphSpacing.GetHashCode(), (int)tmp.alignment);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004A90 File Offset: 0x00002C90
		public override int GetHashCode()
		{
			return this.hash;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A98 File Offset: 0x00002C98
		public override bool Equals(object obj)
		{
			return obj != null && this.GetHashCode() == obj.GetHashCode();
		}

		// Token: 0x0400005D RID: 93
		public readonly TrueShadow shadow;

		// Token: 0x0400005E RID: 94
		public readonly Canvas canvas;

		// Token: 0x0400005F RID: 95
		public readonly RectTransform canvasRt;

		// Token: 0x04000060 RID: 96
		public readonly float canvasScale;

		// Token: 0x04000061 RID: 97
		public readonly float size;

		// Token: 0x04000062 RID: 98
		public readonly Vector2 canvasRelativeOffset;

		// Token: 0x04000063 RID: 99
		public readonly Vector2 dimensions;

		// Token: 0x04000064 RID: 100
		private const int DIMENSIONS_HASH_STEP = 1;

		// Token: 0x04000065 RID: 101
		private int hash;
	}
}
