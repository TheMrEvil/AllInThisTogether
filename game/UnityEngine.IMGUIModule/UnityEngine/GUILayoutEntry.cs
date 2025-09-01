using System;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	internal class GUILayoutEntry
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000CA76 File Offset: 0x0000AC76
		public virtual int marginLeft
		{
			get
			{
				return this.style.margin.left;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public virtual int marginRight
		{
			get
			{
				return this.style.margin.right;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		public virtual int marginTop
		{
			get
			{
				return this.style.margin.top;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public virtual int marginBottom
		{
			get
			{
				return this.style.margin.bottom;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000CABE File Offset: 0x0000ACBE
		public int marginHorizontal
		{
			get
			{
				return this.marginLeft + this.marginRight;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000CACD File Offset: 0x0000ACCD
		public int marginVertical
		{
			get
			{
				return this.marginBottom + this.marginTop;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000CADC File Offset: 0x0000ACDC
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			bool flag = _style == null;
			if (flag)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000220D File Offset: 0x0000040D
		public virtual void CalcWidth()
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000220D File Offset: 0x0000040D
		public virtual void CalcHeight()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000CBCD File Offset: 0x0000ADCD
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CC08 File Offset: 0x0000AE08
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
			this.stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
			this.m_Style = style;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				foreach (GUILayoutOption guilayoutOption in options)
				{
					switch (guilayoutOption.type)
					{
					case GUILayoutOption.Type.fixedWidth:
						this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
						this.stretchWidth = 0;
						break;
					case GUILayoutOption.Type.fixedHeight:
						this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
						this.stretchHeight = 0;
						break;
					case GUILayoutOption.Type.minWidth:
					{
						this.minWidth = (float)guilayoutOption.value;
						bool flag2 = this.maxWidth < this.minWidth;
						if (flag2)
						{
							this.maxWidth = this.minWidth;
						}
						break;
					}
					case GUILayoutOption.Type.maxWidth:
					{
						this.maxWidth = (float)guilayoutOption.value;
						bool flag3 = this.minWidth > this.maxWidth;
						if (flag3)
						{
							this.minWidth = this.maxWidth;
						}
						this.stretchWidth = 0;
						break;
					}
					case GUILayoutOption.Type.minHeight:
					{
						this.minHeight = (float)guilayoutOption.value;
						bool flag4 = this.maxHeight < this.minHeight;
						if (flag4)
						{
							this.maxHeight = this.minHeight;
						}
						break;
					}
					case GUILayoutOption.Type.maxHeight:
					{
						this.maxHeight = (float)guilayoutOption.value;
						bool flag5 = this.minHeight > this.maxHeight;
						if (flag5)
						{
							this.minHeight = this.maxHeight;
						}
						this.stretchHeight = 0;
						break;
					}
					case GUILayoutOption.Type.stretchWidth:
						this.stretchWidth = (int)guilayoutOption.value;
						break;
					case GUILayoutOption.Type.stretchHeight:
						this.stretchHeight = (int)guilayoutOption.value;
						break;
					}
				}
				bool flag6 = this.maxWidth != 0f && this.maxWidth < this.minWidth;
				if (flag6)
				{
					this.maxWidth = this.minWidth;
				}
				bool flag7 = this.maxHeight != 0f && this.maxHeight < this.minHeight;
				if (flag7)
				{
					this.maxHeight = this.minHeight;
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000CE84 File Offset: 0x0000B084
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new string[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style != null) ? this.style.name : "NULL",
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth.ToString(),
				"-",
				this.maxWidth.ToString(),
				(this.stretchWidth != 0) ? "+" : "",
				", H: ",
				this.minHeight.ToString(),
				"-",
				this.maxHeight.ToString(),
				(this.stretchHeight != 0) ? "+" : ""
			});
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		// Note: this type is marked as 'beforefieldinit'.
		static GUILayoutEntry()
		{
		}

		// Token: 0x040000EF RID: 239
		public float minWidth;

		// Token: 0x040000F0 RID: 240
		public float maxWidth;

		// Token: 0x040000F1 RID: 241
		public float minHeight;

		// Token: 0x040000F2 RID: 242
		public float maxHeight;

		// Token: 0x040000F3 RID: 243
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040000F4 RID: 244
		public int stretchWidth;

		// Token: 0x040000F5 RID: 245
		public int stretchHeight;

		// Token: 0x040000F6 RID: 246
		public bool consideredForMargin = true;

		// Token: 0x040000F7 RID: 247
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x040000F8 RID: 248
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040000F9 RID: 249
		protected static int indent = 0;
	}
}
