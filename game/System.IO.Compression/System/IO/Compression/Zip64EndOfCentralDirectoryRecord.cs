using System;

namespace System.IO.Compression
{
	// Token: 0x02000033 RID: 51
	internal struct Zip64EndOfCentralDirectoryRecord
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00008C18 File Offset: 0x00006E18
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryRecord zip64EOCDRecord)
		{
			zip64EOCDRecord = default(Zip64EndOfCentralDirectoryRecord);
			if (reader.ReadUInt32() != 101075792U)
			{
				return false;
			}
			zip64EOCDRecord.SizeOfThisRecord = reader.ReadUInt64();
			zip64EOCDRecord.VersionMadeBy = reader.ReadUInt16();
			zip64EOCDRecord.VersionNeededToExtract = reader.ReadUInt16();
			zip64EOCDRecord.NumberOfThisDisk = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfDiskWithStartOfCD = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfEntriesOnThisDisk = reader.ReadUInt64();
			zip64EOCDRecord.NumberOfEntriesTotal = reader.ReadUInt64();
			zip64EOCDRecord.SizeOfCentralDirectory = reader.ReadUInt64();
			zip64EOCDRecord.OffsetOfCentralDirectory = reader.ReadUInt64();
			return true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(101075792U);
			binaryWriter.Write(44UL);
			binaryWriter.Write(45);
			binaryWriter.Write(45);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(sizeOfCentralDirectory);
			binaryWriter.Write(startOfCentralDirectory);
		}

		// Token: 0x040001C1 RID: 449
		private const uint SignatureConstant = 101075792U;

		// Token: 0x040001C2 RID: 450
		private const ulong NormalSize = 44UL;

		// Token: 0x040001C3 RID: 451
		public ulong SizeOfThisRecord;

		// Token: 0x040001C4 RID: 452
		public ushort VersionMadeBy;

		// Token: 0x040001C5 RID: 453
		public ushort VersionNeededToExtract;

		// Token: 0x040001C6 RID: 454
		public uint NumberOfThisDisk;

		// Token: 0x040001C7 RID: 455
		public uint NumberOfDiskWithStartOfCD;

		// Token: 0x040001C8 RID: 456
		public ulong NumberOfEntriesOnThisDisk;

		// Token: 0x040001C9 RID: 457
		public ulong NumberOfEntriesTotal;

		// Token: 0x040001CA RID: 458
		public ulong SizeOfCentralDirectory;

		// Token: 0x040001CB RID: 459
		public ulong OffsetOfCentralDirectory;
	}
}
