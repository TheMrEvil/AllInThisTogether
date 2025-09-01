using System;

namespace System.IO.Compression
{
	// Token: 0x02000036 RID: 54
	internal struct ZipEndOfCentralDirectoryBlock
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00009098 File Offset: 0x00007298
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			ushort value = (numberOfEntries > 65535L) ? ushort.MaxValue : ((ushort)numberOfEntries);
			uint value2 = (startOfCentralDirectory > (long)((ulong)-1)) ? uint.MaxValue : ((uint)startOfCentralDirectory);
			uint value3 = (sizeOfCentralDirectory > (long)((ulong)-1)) ? uint.MaxValue : ((uint)sizeOfCentralDirectory);
			binaryWriter.Write(101010256U);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(value);
			binaryWriter.Write(value);
			binaryWriter.Write(value3);
			binaryWriter.Write(value2);
			binaryWriter.Write((archiveComment != null) ? ((ushort)archiveComment.Length) : 0);
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009128 File Offset: 0x00007328
		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = default(ZipEndOfCentralDirectoryBlock);
			if (reader.ReadUInt32() != 101010256U)
			{
				return false;
			}
			eocdBlock.Signature = 101010256U;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			ushort count = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)count);
			return true;
		}

		// Token: 0x040001E5 RID: 485
		public const uint SignatureConstant = 101010256U;

		// Token: 0x040001E6 RID: 486
		public const int SizeOfBlockWithoutSignature = 18;

		// Token: 0x040001E7 RID: 487
		public uint Signature;

		// Token: 0x040001E8 RID: 488
		public ushort NumberOfThisDisk;

		// Token: 0x040001E9 RID: 489
		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		// Token: 0x040001EA RID: 490
		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		// Token: 0x040001EB RID: 491
		public ushort NumberOfEntriesInTheCentralDirectory;

		// Token: 0x040001EC RID: 492
		public uint SizeOfCentralDirectory;

		// Token: 0x040001ED RID: 493
		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		// Token: 0x040001EE RID: 494
		public byte[] ArchiveComment;
	}
}
