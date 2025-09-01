using System;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000312 RID: 786
	public class OffsetTable
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x000B0A6C File Offset: 0x000AEC6C
		internal OffsetTable()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform != 4 && platform != 128)
			{
				this.FileFlags |= OffsetTable.Flags.WindowsFileNames;
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000B0ABC File Offset: 0x000AECBC
		internal OffsetTable(BinaryReader reader, int major_version, int minor_version)
		{
			this.TotalFileSize = reader.ReadInt32();
			this.DataSectionOffset = reader.ReadInt32();
			this.DataSectionSize = reader.ReadInt32();
			this.CompileUnitCount = reader.ReadInt32();
			this.CompileUnitTableOffset = reader.ReadInt32();
			this.CompileUnitTableSize = reader.ReadInt32();
			this.SourceCount = reader.ReadInt32();
			this.SourceTableOffset = reader.ReadInt32();
			this.SourceTableSize = reader.ReadInt32();
			this.MethodCount = reader.ReadInt32();
			this.MethodTableOffset = reader.ReadInt32();
			this.MethodTableSize = reader.ReadInt32();
			this.TypeCount = reader.ReadInt32();
			this.AnonymousScopeCount = reader.ReadInt32();
			this.AnonymousScopeTableOffset = reader.ReadInt32();
			this.AnonymousScopeTableSize = reader.ReadInt32();
			this.LineNumberTable_LineBase = reader.ReadInt32();
			this.LineNumberTable_LineRange = reader.ReadInt32();
			this.LineNumberTable_OpcodeBase = reader.ReadInt32();
			this.FileFlags = (OffsetTable.Flags)reader.ReadInt32();
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000B0BD8 File Offset: 0x000AEDD8
		internal void Write(BinaryWriter bw, int major_version, int minor_version)
		{
			bw.Write(this.TotalFileSize);
			bw.Write(this.DataSectionOffset);
			bw.Write(this.DataSectionSize);
			bw.Write(this.CompileUnitCount);
			bw.Write(this.CompileUnitTableOffset);
			bw.Write(this.CompileUnitTableSize);
			bw.Write(this.SourceCount);
			bw.Write(this.SourceTableOffset);
			bw.Write(this.SourceTableSize);
			bw.Write(this.MethodCount);
			bw.Write(this.MethodTableOffset);
			bw.Write(this.MethodTableSize);
			bw.Write(this.TypeCount);
			bw.Write(this.AnonymousScopeCount);
			bw.Write(this.AnonymousScopeTableOffset);
			bw.Write(this.AnonymousScopeTableSize);
			bw.Write(this.LineNumberTable_LineBase);
			bw.Write(this.LineNumberTable_LineRange);
			bw.Write(this.LineNumberTable_OpcodeBase);
			bw.Write((int)this.FileFlags);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000B0CD8 File Offset: 0x000AEED8
		public override string ToString()
		{
			return string.Format("OffsetTable [{0} - {1}:{2} - {3}:{4}:{5} - {6}:{7}:{8} - {9}]", new object[]
			{
				this.TotalFileSize,
				this.DataSectionOffset,
				this.DataSectionSize,
				this.SourceCount,
				this.SourceTableOffset,
				this.SourceTableSize,
				this.MethodCount,
				this.MethodTableOffset,
				this.MethodTableSize,
				this.TypeCount
			});
		}

		// Token: 0x04000DB5 RID: 3509
		public const int MajorVersion = 50;

		// Token: 0x04000DB6 RID: 3510
		public const int MinorVersion = 0;

		// Token: 0x04000DB7 RID: 3511
		public const long Magic = 5037318119232611860L;

		// Token: 0x04000DB8 RID: 3512
		public int TotalFileSize;

		// Token: 0x04000DB9 RID: 3513
		public int DataSectionOffset;

		// Token: 0x04000DBA RID: 3514
		public int DataSectionSize;

		// Token: 0x04000DBB RID: 3515
		public int CompileUnitCount;

		// Token: 0x04000DBC RID: 3516
		public int CompileUnitTableOffset;

		// Token: 0x04000DBD RID: 3517
		public int CompileUnitTableSize;

		// Token: 0x04000DBE RID: 3518
		public int SourceCount;

		// Token: 0x04000DBF RID: 3519
		public int SourceTableOffset;

		// Token: 0x04000DC0 RID: 3520
		public int SourceTableSize;

		// Token: 0x04000DC1 RID: 3521
		public int MethodCount;

		// Token: 0x04000DC2 RID: 3522
		public int MethodTableOffset;

		// Token: 0x04000DC3 RID: 3523
		public int MethodTableSize;

		// Token: 0x04000DC4 RID: 3524
		public int TypeCount;

		// Token: 0x04000DC5 RID: 3525
		public int AnonymousScopeCount;

		// Token: 0x04000DC6 RID: 3526
		public int AnonymousScopeTableOffset;

		// Token: 0x04000DC7 RID: 3527
		public int AnonymousScopeTableSize;

		// Token: 0x04000DC8 RID: 3528
		public OffsetTable.Flags FileFlags;

		// Token: 0x04000DC9 RID: 3529
		public int LineNumberTable_LineBase = -1;

		// Token: 0x04000DCA RID: 3530
		public int LineNumberTable_LineRange = 8;

		// Token: 0x04000DCB RID: 3531
		public int LineNumberTable_OpcodeBase = 9;

		// Token: 0x02000410 RID: 1040
		[Flags]
		public enum Flags
		{
			// Token: 0x04001195 RID: 4501
			IsAspxSource = 1,
			// Token: 0x04001196 RID: 4502
			WindowsFileNames = 2
		}
	}
}
