using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000025 RID: 37
	[UsedByNativeCode]
	[Serializable]
	internal struct SingleSubstitutionRecord
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005088 File Offset: 0x00003288
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000050A0 File Offset: 0x000032A0
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000050AC File Offset: 0x000032AC
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000050C4 File Offset: 0x000032C4
		public uint substituteGlyphID
		{
			get
			{
				return this.m_SubstituteGlyphID;
			}
			set
			{
				this.m_SubstituteGlyphID = value;
			}
		}

		// Token: 0x040000D0 RID: 208
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		[NativeName("substituteGlyphID")]
		private uint m_SubstituteGlyphID;
	}
}
