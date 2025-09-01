using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000034 RID: 52
	internal readonly struct ZipLocalFileHeader
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00008D08 File Offset: 0x00006F08
		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			reader.BaseStream.Seek(26L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			reader.BaseStream.Seek((long)((ulong)num), SeekOrigin.Current);
			List<ZipGenericExtraField> list;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)num2)))
			{
				list = ZipGenericExtraField.ParseExtraField(stream);
			}
			Zip64ExtraField.RemoveZip64Blocks(list);
			return list;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008D88 File Offset: 0x00006F88
		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752U)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + 22L)
			{
				return false;
			}
			reader.BaseStream.Seek(22L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)((ulong)num) + (long)((ulong)num2))
			{
				return false;
			}
			reader.BaseStream.Seek((long)(num + num2), SeekOrigin.Current);
			return true;
		}

		// Token: 0x040001CC RID: 460
		public const uint DataDescriptorSignature = 134695760U;

		// Token: 0x040001CD RID: 461
		public const uint SignatureConstant = 67324752U;

		// Token: 0x040001CE RID: 462
		public const int OffsetToCrcFromHeaderStart = 14;

		// Token: 0x040001CF RID: 463
		public const int OffsetToBitFlagFromHeaderStart = 6;

		// Token: 0x040001D0 RID: 464
		public const int SizeOfLocalHeader = 30;
	}
}
