using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000242 RID: 578
	public struct Location : IEquatable<Location>
	{
		// Token: 0x06001CB2 RID: 7346 RVA: 0x0008AB41 File Offset: 0x00088D41
		static Location()
		{
			Location.Reset();
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0008AB48 File Offset: 0x00088D48
		public static void Reset()
		{
			Location.source_list = new List<SourceFile>();
			Location.checkpoint_index = 0;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0008AB5A File Offset: 0x00088D5A
		public static void AddFile(SourceFile file)
		{
			Location.source_list.Add(file);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0008AB68 File Offset: 0x00088D68
		public static void Initialize(List<SourceFile> files)
		{
			Location.source_list.AddRange(files.ToArray());
			Location.checkpoints = new Location.Checkpoint[Math.Max(1, Location.source_list.Count * 2)];
			if (Location.checkpoints.Length != 0)
			{
				Location.checkpoints[0] = new Location.Checkpoint(0, 0);
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0008ABBC File Offset: 0x00088DBC
		public Location(SourceFile file, int row, int column)
		{
			if (row <= 0)
			{
				this.token = 0;
				return;
			}
			if (column > 255)
			{
				column = 255;
			}
			long num = -1L;
			long num2 = 0L;
			int num3 = (file == null) ? 0 : file.Index;
			int num4 = (Location.checkpoint_index < 10) ? Location.checkpoint_index : 10;
			for (int i = 0; i < num4; i++)
			{
				int lineOffset = Location.checkpoints[Location.checkpoint_index - i].LineOffset;
				num2 = (long)(row - lineOffset);
				if (num2 >= 0L && num2 < 256L && Location.checkpoints[Location.checkpoint_index - i].File == num3)
				{
					num = (long)(Location.checkpoint_index - i);
					break;
				}
			}
			if (num == -1L)
			{
				Location.AddCheckpoint(num3, row);
				num = (long)Location.checkpoint_index;
				num2 = (long)(row % 256);
			}
			long num5 = (long)column + (num2 << 8) + (num << 16);
			this.token = ((num5 > (long)((ulong)-1)) ? 0 : ((int)num5));
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0008ACA8 File Offset: 0x00088EA8
		public static Location operator -(Location loc, int columns)
		{
			return new Location(loc.SourceFile, loc.Row, loc.Column - columns);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0008ACC6 File Offset: 0x00088EC6
		private static void AddCheckpoint(int file, int row)
		{
			if (Location.checkpoints.Length == ++Location.checkpoint_index)
			{
				Array.Resize<Location.Checkpoint>(ref Location.checkpoints, Location.checkpoint_index * 2);
			}
			Location.checkpoints[Location.checkpoint_index] = new Location.Checkpoint(file, row);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0008AD08 File Offset: 0x00088F08
		private string FormatLocation(string fileName)
		{
			if (Location.InEmacs)
			{
				return fileName + "(" + this.Row.ToString() + "):";
			}
			return string.Concat(new string[]
			{
				fileName,
				"(",
				this.Row.ToString(),
				",",
				this.Column.ToString(),
				(this.Column == 255) ? "+):" : "):"
			});
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0008AD98 File Offset: 0x00088F98
		public override string ToString()
		{
			return this.FormatLocation(this.Name);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0008ADA6 File Offset: 0x00088FA6
		public string ToStringFullName()
		{
			return this.FormatLocation(this.NameFullPath);
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0008ADB4 File Offset: 0x00088FB4
		public bool IsNull
		{
			get
			{
				return this.token == 0;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x0008ADC0 File Offset: 0x00088FC0
		public string Name
		{
			get
			{
				int file = this.File;
				if (this.token == 0 || file <= 0)
				{
					return null;
				}
				return Location.source_list[file - 1].Name;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0008ADF4 File Offset: 0x00088FF4
		public string NameFullPath
		{
			get
			{
				int file = this.File;
				if (this.token == 0 || file <= 0)
				{
					return null;
				}
				return Location.source_list[file - 1].FullPathName;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0008AE28 File Offset: 0x00089028
		private int CheckpointIndex
		{
			get
			{
				return this.token >> 16 & 65535;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0008AE39 File Offset: 0x00089039
		public int Row
		{
			get
			{
				if (this.token == 0)
				{
					return 1;
				}
				return Location.checkpoints[this.CheckpointIndex].LineOffset + (this.token >> 8 & 255);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0008AE69 File Offset: 0x00089069
		public int Column
		{
			get
			{
				if (this.token == 0)
				{
					return 1;
				}
				return this.token & 255;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0008AE84 File Offset: 0x00089084
		public int File
		{
			get
			{
				if (this.token == 0)
				{
					return 0;
				}
				if (Location.checkpoints.Length <= this.CheckpointIndex)
				{
					throw new Exception(string.Format("Should not happen. Token is {0:X04}, checkpoints are {1}, index is {2}", this.token, Location.checkpoints.Length, this.CheckpointIndex));
				}
				return Location.checkpoints[this.CheckpointIndex].File;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0008AEF4 File Offset: 0x000890F4
		public SourceFile SourceFile
		{
			get
			{
				int file = this.File;
				if (file == 0)
				{
					return null;
				}
				return Location.source_list[file - 1];
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0008AF1A File Offset: 0x0008911A
		public bool Equals(Location other)
		{
			return this.token == other.token;
		}

		// Token: 0x04000A92 RID: 2706
		private readonly int token;

		// Token: 0x04000A93 RID: 2707
		private const int column_bits = 8;

		// Token: 0x04000A94 RID: 2708
		private const int line_delta_bits = 8;

		// Token: 0x04000A95 RID: 2709
		private const int checkpoint_bits = 16;

		// Token: 0x04000A96 RID: 2710
		private const int column_mask = 255;

		// Token: 0x04000A97 RID: 2711
		private const int max_column = 255;

		// Token: 0x04000A98 RID: 2712
		private static List<SourceFile> source_list;

		// Token: 0x04000A99 RID: 2713
		private static Location.Checkpoint[] checkpoints;

		// Token: 0x04000A9A RID: 2714
		private static int checkpoint_index;

		// Token: 0x04000A9B RID: 2715
		public static readonly Location Null;

		// Token: 0x04000A9C RID: 2716
		public static bool InEmacs;

		// Token: 0x020003CE RID: 974
		private struct Checkpoint
		{
			// Token: 0x0600276A RID: 10090 RVA: 0x000BC2A3 File Offset: 0x000BA4A3
			public Checkpoint(int file, int line)
			{
				this.File = file;
				this.LineOffset = line - line % 256;
			}

			// Token: 0x040010C7 RID: 4295
			public readonly int LineOffset;

			// Token: 0x040010C8 RID: 4296
			public readonly int File;
		}
	}
}
