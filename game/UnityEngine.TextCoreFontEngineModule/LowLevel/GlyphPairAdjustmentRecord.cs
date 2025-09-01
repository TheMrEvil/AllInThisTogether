using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000020 RID: 32
	[UsedByNativeCode]
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}")]
	[Serializable]
	public struct GlyphPairAdjustmentRecord : IEquatable<GlyphPairAdjustmentRecord>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004D7C File Offset: 0x00002F7C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004D94 File Offset: 0x00002F94
		public GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004DA0 File Offset: 0x00002FA0
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004DC4 File Offset: 0x00002FC4
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004DDC File Offset: 0x00002FDC
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004E00 File Offset: 0x00003000
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004E24 File Offset: 0x00003024
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004E48 File Offset: 0x00003048
		[ExcludeFromDocs]
		public bool Equals(GlyphPairAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004E70 File Offset: 0x00003070
		[ExcludeFromDocs]
		public static bool operator ==(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return lhs.m_FirstAdjustmentRecord == rhs.m_FirstAdjustmentRecord && lhs.m_SecondAdjustmentRecord == rhs.m_SecondAdjustmentRecord;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004EAC File Offset: 0x000030AC
		[ExcludeFromDocs]
		public static bool operator !=(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		[NativeName("firstAdjustmentRecord")]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x040000C2 RID: 194
		[NativeName("secondAdjustmentRecord")]
		[SerializeField]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
