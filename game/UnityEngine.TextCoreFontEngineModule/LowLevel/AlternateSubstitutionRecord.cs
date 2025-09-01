using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000027 RID: 39
	[UsedByNativeCode]
	[Serializable]
	internal struct AlternateSubstitutionRecord
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005118 File Offset: 0x00003318
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00005130 File Offset: 0x00003330
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000513C File Offset: 0x0000333C
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005154 File Offset: 0x00003354
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

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		[NativeName("targetGlyphID")]
		private uint m_TargetGlyphID;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		[NativeName("substituteGlyphIDs")]
		private uint[] m_SubstituteGlyphIDs;
	}
}
