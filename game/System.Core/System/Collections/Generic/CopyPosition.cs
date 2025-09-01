using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000355 RID: 853
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x00056F38 File Offset: 0x00055138
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00056F48 File Offset: 0x00055148
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00056F5E File Offset: 0x0005515E
		internal int Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00056F66 File Offset: 0x00055166
		internal int Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00056F6E File Offset: 0x0005516E
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00056F8E File Offset: 0x0005518E
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}

		// Token: 0x04000C72 RID: 3186
		[CompilerGenerated]
		private readonly int <Row>k__BackingField;

		// Token: 0x04000C73 RID: 3187
		[CompilerGenerated]
		private readonly int <Column>k__BackingField;
	}
}
