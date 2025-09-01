using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public abstract class TextElement
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00007C64 File Offset: 0x00005E64
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007C7C File Offset: 0x00005E7C
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00007C94 File Offset: 0x00005E94
		public uint unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007CA0 File Offset: 0x00005EA0
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public TextAsset textAsset
		{
			get
			{
				return this.m_TextAsset;
			}
			set
			{
				this.m_TextAsset = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007CC4 File Offset: 0x00005EC4
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007CDC File Offset: 0x00005EDC
		public Glyph glyph
		{
			get
			{
				return this.m_Glyph;
			}
			set
			{
				this.m_Glyph = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007CE8 File Offset: 0x00005EE8
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00007D00 File Offset: 0x00005F00
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007D0C File Offset: 0x00005F0C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00007D24 File Offset: 0x00005F24
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000059A4 File Offset: 0x00003BA4
		protected TextElement()
		{
		}

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		protected TextElementType m_ElementType;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x040000C9 RID: 201
		internal TextAsset m_TextAsset;

		// Token: 0x040000CA RID: 202
		internal Glyph m_Glyph;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		internal float m_Scale;
	}
}
