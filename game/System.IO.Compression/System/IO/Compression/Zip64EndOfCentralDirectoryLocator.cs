using System;

namespace System.IO.Compression
{
	// Token: 0x02000032 RID: 50
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00008BB3 File Offset: 0x00006DB3
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryLocator zip64EOCDLocator)
		{
			zip64EOCDLocator = default(Zip64EndOfCentralDirectoryLocator);
			if (reader.ReadUInt32() != 117853008U)
			{
				return false;
			}
			zip64EOCDLocator.NumberOfDiskWithZip64EOCD = reader.ReadUInt32();
			zip64EOCDLocator.OffsetOfZip64EOCD = reader.ReadUInt64();
			zip64EOCDLocator.TotalNumberOfDisks = reader.ReadUInt32();
			return true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static void WriteBlock(Stream stream, long zip64EOCDRecordStart)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(117853008U);
			binaryWriter.Write(0U);
			binaryWriter.Write(zip64EOCDRecordStart);
			binaryWriter.Write(1U);
		}

		// Token: 0x040001BC RID: 444
		public const uint SignatureConstant = 117853008U;

		// Token: 0x040001BD RID: 445
		public const int SizeOfBlockWithoutSignature = 16;

		// Token: 0x040001BE RID: 446
		public uint NumberOfDiskWithZip64EOCD;

		// Token: 0x040001BF RID: 447
		public ulong OffsetOfZip64EOCD;

		// Token: 0x040001C0 RID: 448
		public uint TotalNumberOfDisks;
	}
}
