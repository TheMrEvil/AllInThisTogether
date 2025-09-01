using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000035 RID: 53
	internal struct ZipCentralDirectoryFileHeader
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00008E14 File Offset: 0x00007014
		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
		{
			header = default(ZipCentralDirectoryFileHeader);
			if (reader.ReadUInt32() != 33639248U)
			{
				return false;
			}
			header.VersionMadeBySpecification = reader.ReadByte();
			header.VersionMadeByCompatibility = reader.ReadByte();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool readUncompressedSize = num2 == uint.MaxValue;
			bool readCompressedSize = num == uint.MaxValue;
			bool readLocalHeaderOffset = num4 == uint.MaxValue;
			bool readStartDiskNumber = num3 == ushort.MaxValue;
			long position = reader.BaseStream.Position + (long)((ulong)header.ExtraFieldLength);
			Zip64ExtraField zip64ExtraField;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)header.ExtraFieldLength)))
			{
				if (saveExtraFieldsAndComments)
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(stream);
					zip64ExtraField = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
				else
				{
					header.ExtraFields = null;
					zip64ExtraField = Zip64ExtraField.GetJustZip64Block(stream, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
			}
			reader.BaseStream.AdvanceToPosition(position);
			if (saveExtraFieldsAndComments)
			{
				header.FileComment = reader.ReadBytes((int)header.FileCommentLength);
			}
			else
			{
				reader.BaseStream.Position += (long)((ulong)header.FileCommentLength);
				header.FileComment = null;
			}
			header.UncompressedSize = (long)((zip64ExtraField.UncompressedSize == null) ? ((ulong)num2) : ((ulong)zip64ExtraField.UncompressedSize.Value));
			header.CompressedSize = (long)((zip64ExtraField.CompressedSize == null) ? ((ulong)num) : ((ulong)zip64ExtraField.CompressedSize.Value));
			header.RelativeOffsetOfLocalHeader = (long)((zip64ExtraField.LocalHeaderOffset == null) ? ((ulong)num4) : ((ulong)zip64ExtraField.LocalHeaderOffset.Value));
			header.DiskNumberStart = ((zip64ExtraField.StartDiskNumber == null) ? ((int)num3) : zip64ExtraField.StartDiskNumber.Value);
			return true;
		}

		// Token: 0x040001D1 RID: 465
		public const uint SignatureConstant = 33639248U;

		// Token: 0x040001D2 RID: 466
		public byte VersionMadeByCompatibility;

		// Token: 0x040001D3 RID: 467
		public byte VersionMadeBySpecification;

		// Token: 0x040001D4 RID: 468
		public ushort VersionNeededToExtract;

		// Token: 0x040001D5 RID: 469
		public ushort GeneralPurposeBitFlag;

		// Token: 0x040001D6 RID: 470
		public ushort CompressionMethod;

		// Token: 0x040001D7 RID: 471
		public uint LastModified;

		// Token: 0x040001D8 RID: 472
		public uint Crc32;

		// Token: 0x040001D9 RID: 473
		public long CompressedSize;

		// Token: 0x040001DA RID: 474
		public long UncompressedSize;

		// Token: 0x040001DB RID: 475
		public ushort FilenameLength;

		// Token: 0x040001DC RID: 476
		public ushort ExtraFieldLength;

		// Token: 0x040001DD RID: 477
		public ushort FileCommentLength;

		// Token: 0x040001DE RID: 478
		public int DiskNumberStart;

		// Token: 0x040001DF RID: 479
		public ushort InternalFileAttributes;

		// Token: 0x040001E0 RID: 480
		public uint ExternalFileAttributes;

		// Token: 0x040001E1 RID: 481
		public long RelativeOffsetOfLocalHeader;

		// Token: 0x040001E2 RID: 482
		public byte[] Filename;

		// Token: 0x040001E3 RID: 483
		public byte[] FileComment;

		// Token: 0x040001E4 RID: 484
		public List<ZipGenericExtraField> ExtraFields;
	}
}
