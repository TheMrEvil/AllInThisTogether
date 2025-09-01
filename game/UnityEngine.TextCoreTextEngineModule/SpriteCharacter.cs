using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class SpriteCharacter : TextElement
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007918 File Offset: 0x00005B18
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00007930 File Offset: 0x00005B30
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value == this.m_Name;
				if (!flag)
				{
					this.m_Name = value;
					this.m_HashCode = TextUtilities.GetHashCodeCaseSensitive(this.m_Name);
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007968 File Offset: 0x00005B68
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007980 File Offset: 0x00005B80
		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007991 File Offset: 0x00005B91
		public SpriteCharacter(uint unicode, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000079CC File Offset: 0x00005BCC
		public SpriteCharacter(uint unicode, SpriteAsset spriteAsset, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private string m_Name;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private int m_HashCode;
	}
}
