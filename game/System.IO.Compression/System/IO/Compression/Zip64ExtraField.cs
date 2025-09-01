using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000031 RID: 49
	internal struct Zip64ExtraField
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008610 File Offset: 0x00006810
		public ushort TotalSize
		{
			get
			{
				return this._size + 4;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000861B File Offset: 0x0000681B
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00008623 File Offset: 0x00006823
		public long? UncompressedSize
		{
			get
			{
				return this._uncompressedSize;
			}
			set
			{
				this._uncompressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008632 File Offset: 0x00006832
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000863A File Offset: 0x0000683A
		public long? CompressedSize
		{
			get
			{
				return this._compressedSize;
			}
			set
			{
				this._compressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008649 File Offset: 0x00006849
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00008651 File Offset: 0x00006851
		public long? LocalHeaderOffset
		{
			get
			{
				return this._localHeaderOffset;
			}
			set
			{
				this._localHeaderOffset = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008660 File Offset: 0x00006860
		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008668 File Offset: 0x00006868
		private void UpdateSize()
		{
			this._size = 0;
			if (this._uncompressedSize != null)
			{
				this._size += 8;
			}
			if (this._compressedSize != null)
			{
				this._size += 8;
			}
			if (this._localHeaderOffset != null)
			{
				this._size += 8;
			}
			if (this._startDiskNumber != null)
			{
				this._size += 4;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000086EC File Offset: 0x000068EC
		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField result;
			using (BinaryReader binaryReader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField extraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldStream.Length, out extraField))
				{
					if (Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(extraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out result))
					{
						return result;
					}
				}
			}
			result = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			return result;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008780 File Offset: 0x00006980
		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			zip64Block = default(Zip64ExtraField);
			zip64Block._compressedSize = null;
			zip64Block._uncompressedSize = null;
			zip64Block._localHeaderOffset = null;
			zip64Block._startDiskNumber = null;
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			bool result;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					memoryStream = null;
					zip64Block._size = extraField.Size;
					ushort num = 0;
					if (readUncompressedSize)
					{
						num += 8;
					}
					if (readCompressedSize)
					{
						num += 8;
					}
					if (readLocalHeaderOffset)
					{
						num += 8;
					}
					if (readStartDiskNumber)
					{
						num += 4;
					}
					if (num != zip64Block._size)
					{
						result = false;
					}
					else
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(binaryReader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(binaryReader.ReadInt32());
						}
						long? num2 = zip64Block._uncompressedSize;
						long num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException("Uncompressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._compressedSize;
						num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException("Compressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._localHeaderOffset;
						num3 = 0L;
						if (num2.GetValueOrDefault() < num3 & num2 != null)
						{
							throw new InvalidDataException("Local Header Offset cannot be held in an Int64.");
						}
						int? startDiskNumber = zip64Block._startDiskNumber;
						int num4 = 0;
						if (startDiskNumber.GetValueOrDefault() < num4 & startDiskNumber != null)
						{
							throw new InvalidDataException("Start Disk Number cannot be held in an Int64.");
						}
						result = true;
					}
				}
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008984 File Offset: 0x00006B84
		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField result = default(Zip64ExtraField);
			result._compressedSize = null;
			result._uncompressedSize = null;
			result._localHeaderOffset = null;
			result._startDiskNumber = null;
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			bool flag = false;
			foreach (ZipGenericExtraField zipGenericExtraField in extraFields)
			{
				if (zipGenericExtraField.Tag == 1)
				{
					list.Add(zipGenericExtraField);
					if (!flag && Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out result))
					{
						flag = true;
					}
				}
			}
			foreach (ZipGenericExtraField item in list)
			{
				extraFields.Remove(item);
			}
			return result;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008A78 File Offset: 0x00006C78
		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			foreach (ZipGenericExtraField item in extraFields)
			{
				if (item.Tag == 1)
				{
					list.Add(item);
				}
			}
			foreach (ZipGenericExtraField item2 in list)
			{
				extraFields.Remove(item2);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008B14 File Offset: 0x00006D14
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(1);
			binaryWriter.Write(this._size);
			if (this._uncompressedSize != null)
			{
				binaryWriter.Write(this._uncompressedSize.Value);
			}
			if (this._compressedSize != null)
			{
				binaryWriter.Write(this._compressedSize.Value);
			}
			if (this._localHeaderOffset != null)
			{
				binaryWriter.Write(this._localHeaderOffset.Value);
			}
			if (this._startDiskNumber != null)
			{
				binaryWriter.Write(this._startDiskNumber.Value);
			}
		}

		// Token: 0x040001B5 RID: 437
		public const int OffsetToFirstField = 4;

		// Token: 0x040001B6 RID: 438
		private const ushort TagConstant = 1;

		// Token: 0x040001B7 RID: 439
		private ushort _size;

		// Token: 0x040001B8 RID: 440
		private long? _uncompressedSize;

		// Token: 0x040001B9 RID: 441
		private long? _compressedSize;

		// Token: 0x040001BA RID: 442
		private long? _localHeaderOffset;

		// Token: 0x040001BB RID: 443
		private int? _startDiskNumber;
	}
}
