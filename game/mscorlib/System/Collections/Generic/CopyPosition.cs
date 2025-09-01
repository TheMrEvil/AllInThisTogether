using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AA8 RID: 2728
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x060061A9 RID: 25001 RVA: 0x001467E0 File Offset: 0x001449E0
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x001467F0 File Offset: 0x001449F0
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x00146806 File Offset: 0x00144A06
		internal int Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060061AC RID: 25004 RVA: 0x0014680E File Offset: 0x00144A0E
		internal int Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x00146816 File Offset: 0x00144A16
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x00146836 File Offset: 0x00144A36
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}

		// Token: 0x040039F9 RID: 14841
		[CompilerGenerated]
		private readonly int <Row>k__BackingField;

		// Token: 0x040039FA RID: 14842
		[CompilerGenerated]
		private readonly int <Column>k__BackingField;
	}
}
