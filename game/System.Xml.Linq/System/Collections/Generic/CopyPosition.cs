using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000064 RID: 100
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00010ADA File Offset: 0x0000ECDA
		internal int Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		internal int Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010AEA File Offset: 0x0000ECEA
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00010B0A File Offset: 0x0000ED0A
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}

		// Token: 0x040001E7 RID: 487
		[CompilerGenerated]
		private readonly int <Row>k__BackingField;

		// Token: 0x040001E8 RID: 488
		[CompilerGenerated]
		private readonly int <Column>k__BackingField;
	}
}
