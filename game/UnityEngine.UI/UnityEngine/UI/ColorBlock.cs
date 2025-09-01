using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public struct ColorBlock : IEquatable<ColorBlock>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000267F File Offset: 0x0000087F
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002687 File Offset: 0x00000887
		public Color normalColor
		{
			get
			{
				return this.m_NormalColor;
			}
			set
			{
				this.m_NormalColor = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002690 File Offset: 0x00000890
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002698 File Offset: 0x00000898
		public Color highlightedColor
		{
			get
			{
				return this.m_HighlightedColor;
			}
			set
			{
				this.m_HighlightedColor = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026A1 File Offset: 0x000008A1
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000026A9 File Offset: 0x000008A9
		public Color pressedColor
		{
			get
			{
				return this.m_PressedColor;
			}
			set
			{
				this.m_PressedColor = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000026BA File Offset: 0x000008BA
		public Color selectedColor
		{
			get
			{
				return this.m_SelectedColor;
			}
			set
			{
				this.m_SelectedColor = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026C3 File Offset: 0x000008C3
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000026CB File Offset: 0x000008CB
		public Color disabledColor
		{
			get
			{
				return this.m_DisabledColor;
			}
			set
			{
				this.m_DisabledColor = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000026D4 File Offset: 0x000008D4
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000026DC File Offset: 0x000008DC
		public float colorMultiplier
		{
			get
			{
				return this.m_ColorMultiplier;
			}
			set
			{
				this.m_ColorMultiplier = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026E5 File Offset: 0x000008E5
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000026ED File Offset: 0x000008ED
		public float fadeDuration
		{
			get
			{
				return this.m_FadeDuration;
			}
			set
			{
				this.m_FadeDuration = value;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026F8 File Offset: 0x000008F8
		static ColorBlock()
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027E4 File Offset: 0x000009E4
		public override bool Equals(object obj)
		{
			return obj is ColorBlock && this.Equals((ColorBlock)obj);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027FC File Offset: 0x000009FC
		public bool Equals(ColorBlock other)
		{
			return this.normalColor == other.normalColor && this.highlightedColor == other.highlightedColor && this.pressedColor == other.pressedColor && this.selectedColor == other.selectedColor && this.disabledColor == other.disabledColor && this.colorMultiplier == other.colorMultiplier && this.fadeDuration == other.fadeDuration;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000288D File Offset: 0x00000A8D
		public static bool operator ==(ColorBlock point1, ColorBlock point2)
		{
			return point1.Equals(point2);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002897 File Offset: 0x00000A97
		public static bool operator !=(ColorBlock point1, ColorBlock point2)
		{
			return !point1.Equals(point2);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A4 File Offset: 0x00000AA4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400001B RID: 27
		[FormerlySerializedAs("normalColor")]
		[SerializeField]
		private Color m_NormalColor;

		// Token: 0x0400001C RID: 28
		[FormerlySerializedAs("highlightedColor")]
		[SerializeField]
		private Color m_HighlightedColor;

		// Token: 0x0400001D RID: 29
		[FormerlySerializedAs("pressedColor")]
		[SerializeField]
		private Color m_PressedColor;

		// Token: 0x0400001E RID: 30
		[FormerlySerializedAs("m_HighlightedColor")]
		[SerializeField]
		private Color m_SelectedColor;

		// Token: 0x0400001F RID: 31
		[FormerlySerializedAs("disabledColor")]
		[SerializeField]
		private Color m_DisabledColor;

		// Token: 0x04000020 RID: 32
		[Range(1f, 5f)]
		[SerializeField]
		private float m_ColorMultiplier;

		// Token: 0x04000021 RID: 33
		[FormerlySerializedAs("fadeDuration")]
		[SerializeField]
		private float m_FadeDuration;

		// Token: 0x04000022 RID: 34
		public static ColorBlock defaultColorBlock = new ColorBlock
		{
			m_NormalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
			m_HighlightedColor = new Color32(245, 245, 245, byte.MaxValue),
			m_PressedColor = new Color32(200, 200, 200, byte.MaxValue),
			m_SelectedColor = new Color32(245, 245, 245, byte.MaxValue),
			m_DisabledColor = new Color32(200, 200, 200, 128),
			colorMultiplier = 1f,
			fadeDuration = 0.1f
		};
	}
}
