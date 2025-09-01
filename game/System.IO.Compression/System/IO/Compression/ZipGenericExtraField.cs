using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000030 RID: 48
	internal struct ZipGenericExtraField
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008468 File Offset: 0x00006668
		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008470 File Offset: 0x00006670
		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008478 File Offset: 0x00006678
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008480 File Offset: 0x00006680
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(this.Tag);
			binaryWriter.Write(this.Size);
			binaryWriter.Write(this.Data);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000084AC File Offset: 0x000066AC
		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = default(ZipGenericExtraField);
			if (endExtraField - reader.BaseStream.Position < 4L)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < (long)((ulong)field._size))
			{
				return false;
			}
			field._data = reader.ReadBytes((int)field._size);
			return true;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008518 File Offset: 0x00006718
		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			using (BinaryReader binaryReader = new BinaryReader(extraFieldData))
			{
				ZipGenericExtraField item;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldData.Length, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008568 File Offset: 0x00006768
		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int num = 0;
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				num += (int)(zipGenericExtraField.Size + 4);
			}
			return num;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000085C0 File Offset: 0x000067C0
		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				zipGenericExtraField.WriteBlock(stream);
			}
		}

		// Token: 0x040001B1 RID: 433
		private const int SizeOfHeader = 4;

		// Token: 0x040001B2 RID: 434
		private ushort _tag;

		// Token: 0x040001B3 RID: 435
		private ushort _size;

		// Token: 0x040001B4 RID: 436
		private byte[] _data;
	}
}
