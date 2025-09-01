using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002B RID: 43
	[UsedByNativeCode]
	[Serializable]
	internal struct ContextualSubstitutionRecord
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005214 File Offset: 0x00003414
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000522C File Offset: 0x0000342C
		public GlyphIDSequence[] inputSequences
		{
			get
			{
				return this.m_InputGlyphSequences;
			}
			set
			{
				this.m_InputGlyphSequences = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005238 File Offset: 0x00003438
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005250 File Offset: 0x00003450
		public SequenceLookupRecord[] sequenceLookupRecords
		{
			get
			{
				return this.m_SequenceLookupRecords;
			}
			set
			{
				this.m_SequenceLookupRecords = value;
			}
		}

		// Token: 0x040000DB RID: 219
		[SerializeField]
		[NativeName("inputGlyphSequences")]
		private GlyphIDSequence[] m_InputGlyphSequences;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		[NativeName("sequenceLookupRecords")]
		private SequenceLookupRecord[] m_SequenceLookupRecords;
	}
}
