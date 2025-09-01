using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000313 RID: 787
	public class LineNumberEntry
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x000B0D83 File Offset: 0x000AEF83
		public LineNumberEntry(int file, int row, int column, int offset) : this(file, row, column, offset, false)
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000B0D91 File Offset: 0x000AEF91
		public LineNumberEntry(int file, int row, int offset) : this(file, row, -1, offset, false)
		{
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000B0D9E File Offset: 0x000AEF9E
		public LineNumberEntry(int file, int row, int column, int offset, bool is_hidden) : this(file, row, column, -1, -1, offset, is_hidden)
		{
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000B0DAF File Offset: 0x000AEFAF
		public LineNumberEntry(int file, int row, int column, int end_row, int end_column, int offset, bool is_hidden)
		{
			this.File = file;
			this.Row = row;
			this.Column = column;
			this.EndRow = end_row;
			this.EndColumn = end_column;
			this.Offset = offset;
			this.IsHidden = is_hidden;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000B0DEC File Offset: 0x000AEFEC
		public override string ToString()
		{
			return string.Format("[Line {0}:{1,2}-{3,4}:{5}]", new object[]
			{
				this.File,
				this.Row,
				this.Column,
				this.EndRow,
				this.EndColumn,
				this.Offset
			});
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000B0E5D File Offset: 0x000AF05D
		// Note: this type is marked as 'beforefieldinit'.
		static LineNumberEntry()
		{
		}

		// Token: 0x04000DCC RID: 3532
		public readonly int Row;

		// Token: 0x04000DCD RID: 3533
		public int Column;

		// Token: 0x04000DCE RID: 3534
		public int EndRow;

		// Token: 0x04000DCF RID: 3535
		public int EndColumn;

		// Token: 0x04000DD0 RID: 3536
		public readonly int File;

		// Token: 0x04000DD1 RID: 3537
		public readonly int Offset;

		// Token: 0x04000DD2 RID: 3538
		public readonly bool IsHidden;

		// Token: 0x04000DD3 RID: 3539
		public static readonly LineNumberEntry Null = new LineNumberEntry(0, 0, 0, 0);

		// Token: 0x02000411 RID: 1041
		public sealed class LocationComparer : IComparer<LineNumberEntry>
		{
			// Token: 0x06002859 RID: 10329 RVA: 0x000BF49C File Offset: 0x000BD69C
			public int Compare(LineNumberEntry l1, LineNumberEntry l2)
			{
				if (l1.Row != l2.Row)
				{
					return l1.Row.CompareTo(l2.Row);
				}
				return l1.Column.CompareTo(l2.Column);
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x00002CCC File Offset: 0x00000ECC
			public LocationComparer()
			{
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x000BF4DD File Offset: 0x000BD6DD
			// Note: this type is marked as 'beforefieldinit'.
			static LocationComparer()
			{
			}

			// Token: 0x04001197 RID: 4503
			public static readonly LineNumberEntry.LocationComparer Default = new LineNumberEntry.LocationComparer();
		}
	}
}
