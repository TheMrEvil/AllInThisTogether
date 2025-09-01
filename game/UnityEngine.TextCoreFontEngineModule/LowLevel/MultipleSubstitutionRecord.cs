using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000026 RID: 38
	[UsedByNativeCode]
	[Serializable]
	internal struct MultipleSubstitutionRecord
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000050D0 File Offset: 0x000032D0
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000050E8 File Offset: 0x000032E8
		public uint targetGlyphID
		{
			get
			{
				return this.m_TargetGlyphID;
			}
			set
			{
				this.m_TargetGlyphID = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000050F4 File Offset: 0x000032F4
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000510C File Offset: 0x0000330C
		public uint[] substituteGlyphIDs
		{
			get
			{
				return this.m_SubstituteGlyphIDs;
			}
			set
			{
				this.m_SubstituteGlyphIDs = value;
			}
		}

		// Token: 0x040000D2 RID: 210
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x040000D3 RID: 211
		[NativeName("substituteGlyphIDs")]
		[SerializeField]
		private uint[] m_SubstituteGlyphIDs;
	}
}
