using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000028 RID: 40
	[UsedByNativeCode]
	[Serializable]
	internal struct LigatureSubstitutionRecord
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005160 File Offset: 0x00003360
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005178 File Offset: 0x00003378
		public uint[] componentGlyphIDs
		{
			get
			{
				return this.m_ComponentGlyphIDs;
			}
			set
			{
				this.m_ComponentGlyphIDs = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005184 File Offset: 0x00003384
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000519C File Offset: 0x0000339C
		public uint ligatureGlyphID
		{
			get
			{
				return this.m_LigatureGlyphID;
			}
			set
			{
				this.m_LigatureGlyphID = value;
			}
		}

		// Token: 0x040000D6 RID: 214
		[SerializeField]
		[NativeName("componentGlyphs")]
		private uint[] m_ComponentGlyphIDs;

		// Token: 0x040000D7 RID: 215
		[SerializeField]
		[NativeName("ligatureGlyph")]
		private uint m_LigatureGlyphID;
	}
}
