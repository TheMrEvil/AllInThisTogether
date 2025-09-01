using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class FontData : ISerializationCallbackReceiver
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005054 File Offset: 0x00003254
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_FontStyle = FontStyle.Normal,
					m_BestFit = false,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_Alignment = TextAnchor.UpperLeft,
					m_HorizontalOverflow = HorizontalWrapMode.Wrap,
					m_VerticalOverflow = VerticalWrapMode.Truncate,
					m_RichText = true,
					m_AlignByGeometry = false
				};
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000050BA File Offset: 0x000032BA
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000050C2 File Offset: 0x000032C2
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000050CB File Offset: 0x000032CB
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000050D3 File Offset: 0x000032D3
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000050DC File Offset: 0x000032DC
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000050E4 File Offset: 0x000032E4
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000050ED File Offset: 0x000032ED
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000050F5 File Offset: 0x000032F5
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000050FE File Offset: 0x000032FE
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005106 File Offset: 0x00003306
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000510F File Offset: 0x0000330F
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00005117 File Offset: 0x00003317
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005120 File Offset: 0x00003320
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00005128 File Offset: 0x00003328
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005131 File Offset: 0x00003331
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00005139 File Offset: 0x00003339
		public bool alignByGeometry
		{
			get
			{
				return this.m_AlignByGeometry;
			}
			set
			{
				this.m_AlignByGeometry = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005142 File Offset: 0x00003342
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000514A File Offset: 0x0000334A
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005153 File Offset: 0x00003353
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000515B File Offset: 0x0000335B
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005164 File Offset: 0x00003364
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000516C File Offset: 0x0000336C
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005175 File Offset: 0x00003375
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000517D File Offset: 0x0000337D
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005186 File Offset: 0x00003386
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005188 File Offset: 0x00003388
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, this.m_FontSize);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, this.m_FontSize, 300);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000051E0 File Offset: 0x000033E0
		public FontData()
		{
		}

		// Token: 0x04000041 RID: 65
		[SerializeField]
		[FormerlySerializedAs("font")]
		private Font m_Font;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		[FormerlySerializedAs("fontStyle")]
		private FontStyle m_FontStyle;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private int m_MinSize;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		[FormerlySerializedAs("alignment")]
		private TextAnchor m_Alignment;

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private bool m_AlignByGeometry;

		// Token: 0x04000049 RID: 73
		[SerializeField]
		[FormerlySerializedAs("richText")]
		private bool m_RichText;

		// Token: 0x0400004A RID: 74
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x0400004C RID: 76
		[SerializeField]
		private float m_LineSpacing;
	}
}
