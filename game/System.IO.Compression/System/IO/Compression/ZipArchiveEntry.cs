using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity;

namespace System.IO.Compression
{
	/// <summary>Represents a compressed file within a zip archive.</summary>
	// Token: 0x0200002A RID: 42
	public class ZipArchiveEntry
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd)
		{
			this._archive = archive;
			this._originallyInArchive = true;
			this._diskNumberStart = cd.DiskNumberStart;
			this._versionMadeByPlatform = (ZipVersionMadeByPlatform)cd.VersionMadeByCompatibility;
			this._versionMadeBySpecification = (ZipVersionNeededValues)cd.VersionMadeBySpecification;
			this._versionToExtract = (ZipVersionNeededValues)cd.VersionNeededToExtract;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)cd.GeneralPurposeBitFlag;
			this.CompressionMethod = (ZipArchiveEntry.CompressionMethodValues)cd.CompressionMethod;
			this._lastModified = new DateTimeOffset(ZipHelper.DosTimeToDateTime(cd.LastModified));
			this._compressedSize = cd.CompressedSize;
			this._uncompressedSize = cd.UncompressedSize;
			this._externalFileAttr = cd.ExternalFileAttributes;
			this._offsetOfLocalHeader = cd.RelativeOffsetOfLocalHeader;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = cd.Crc32;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = this.DecodeEntryName(cd.Filename);
			this._lhUnknownExtraFields = null;
			this._cdUnknownExtraFields = cd.ExtraFields;
			this._fileComment = cd.FileComment;
			this._compressionLevel = null;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007017 File Offset: 0x00005217
		internal ZipArchiveEntry(ZipArchive archive, string entryName, CompressionLevel compressionLevel) : this(archive, entryName)
		{
			this._compressionLevel = new CompressionLevel?(compressionLevel);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007030 File Offset: 0x00005230
		internal ZipArchiveEntry(ZipArchive archive, string entryName)
		{
			this._archive = archive;
			this._originallyInArchive = false;
			this._diskNumberStart = 0;
			this._versionMadeByPlatform = ZipArchiveEntry.CurrentZipPlatform;
			this._versionMadeBySpecification = ZipVersionNeededValues.Default;
			this._versionToExtract = ZipVersionNeededValues.Default;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)0;
			this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
			this._lastModified = DateTimeOffset.Now;
			this._compressedSize = 0L;
			this._uncompressedSize = 0L;
			this._externalFileAttr = 0U;
			this._offsetOfLocalHeader = 0L;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = 0U;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = entryName;
			this._cdUnknownExtraFields = null;
			this._lhUnknownExtraFields = null;
			this._fileComment = null;
			this._compressionLevel = null;
			if (this._storedEntryNameBytes.Length > 65535)
			{
				throw new ArgumentException("Entry names cannot require more than 2^16 bits.");
			}
			if (this._archive.Mode == ZipArchiveMode.Create)
			{
				this._archive.AcquireArchiveStream(this);
			}
		}

		/// <summary>Gets the zip archive that the entry belongs to.</summary>
		/// <returns>The zip archive that the entry belongs to, or <see langword="null" /> if the entry has been deleted.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000713D File Offset: 0x0000533D
		public ZipArchive Archive
		{
			get
			{
				return this._archive;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007145 File Offset: 0x00005345
		[CLSCompliant(false)]
		public uint Crc32
		{
			get
			{
				return this._crc32;
			}
		}

		/// <summary>Gets the compressed size of the entry in the zip archive.</summary>
		/// <returns>The compressed size of the entry in the zip archive.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the property is not available because the entry has been modified.</exception>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000714D File Offset: 0x0000534D
		public long CompressedLength
		{
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException("Length properties are unavailable once an entry has been opened for writing.");
				}
				return this._compressedSize;
			}
		}

		/// <summary>
		/// 		  OS and Application specific file attributes.
		/// </summary>
		/// <returns>The external attributes written by the application when this entry was written. It is both host OS and application dependent.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007168 File Offset: 0x00005368
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00007170 File Offset: 0x00005370
		public int ExternalAttributes
		{
			get
			{
				return (int)this._externalFileAttr;
			}
			set
			{
				this.ThrowIfInvalidArchive();
				this._externalFileAttr = (uint)value;
			}
		}

		/// <summary>Gets the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry in the zip archive.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000717F File Offset: 0x0000537F
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00007188 File Offset: 0x00005388
		public string FullName
		{
			get
			{
				return this._storedEntryName;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FullName");
				}
				bool flag;
				this._storedEntryNameBytes = this.EncodeEntryName(value, out flag);
				this._storedEntryName = value;
				if (flag)
				{
					this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				else
				{
					this._generalPurposeBitFlag &= ~ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				if (ZipArchiveEntry.ParseFileName(value, this._versionMadeByPlatform) == "")
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
			}
		}

		/// <summary>Gets or sets the last time the entry in the zip archive was changed.</summary>
		/// <returns>The last time the entry in the zip archive was changed.</returns>
		/// <exception cref="T:System.NotSupportedException">The attempt to set this property failed, because the zip archive for the entry is in <see cref="F:System.IO.Compression.ZipArchiveMode.Read" /> mode.</exception>
		/// <exception cref="T:System.IO.IOException">The archive mode is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Create" />.- or -The archive mode is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and the entry has been opened.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is either earlier than 1980 January 1 0:00:00 (midnight) or later than 2107 December 31 23:59:58 (one second before midnight).</exception>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007202 File Offset: 0x00005402
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000720C File Offset: 0x0000540C
		public DateTimeOffset LastWriteTime
		{
			get
			{
				return this._lastModified;
			}
			set
			{
				this.ThrowIfInvalidArchive();
				if (this._archive.Mode == ZipArchiveMode.Read)
				{
					throw new NotSupportedException("Cannot modify read-only archive.");
				}
				if (this._archive.Mode == ZipArchiveMode.Create && this._everOpenedForWrite)
				{
					throw new IOException("Cannot modify entry in Create mode after entry has been opened for writing.");
				}
				if (value.DateTime.Year < 1980 || value.DateTime.Year > 2107)
				{
					throw new ArgumentOutOfRangeException("value", "The DateTimeOffset specified cannot be converted into a Zip file timestamp.");
				}
				this._lastModified = value;
			}
		}

		/// <summary>Gets the uncompressed size of the entry in the zip archive.</summary>
		/// <returns>The uncompressed size of the entry in the zip archive.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the property is not available because the entry has been modified.</exception>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000729B File Offset: 0x0000549B
		public long Length
		{
			get
			{
				if (this._everOpenedForWrite)
				{
					throw new InvalidOperationException("Length properties are unavailable once an entry has been opened for writing.");
				}
				return this._uncompressedSize;
			}
		}

		/// <summary>Gets the file name of the entry in the zip archive.</summary>
		/// <returns>The file name of the entry in the zip archive.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000072B6 File Offset: 0x000054B6
		public string Name
		{
			get
			{
				return ZipArchiveEntry.ParseFileName(this.FullName, this._versionMadeByPlatform);
			}
		}

		/// <summary>Deletes the entry from the zip archive.</summary>
		/// <exception cref="T:System.IO.IOException">The entry is already open for reading or writing.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive for this entry was opened in a mode other than <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		// Token: 0x06000146 RID: 326 RVA: 0x000072CC File Offset: 0x000054CC
		public void Delete()
		{
			if (this._archive == null)
			{
				return;
			}
			if (this._currentlyOpenForWrite)
			{
				throw new IOException("Cannot delete an entry currently open for writing.");
			}
			if (this._archive.Mode != ZipArchiveMode.Update)
			{
				throw new NotSupportedException("Delete can only be used when the archive is in Update mode.");
			}
			this._archive.ThrowIfDisposed();
			this._archive.RemoveEntry(this);
			this._archive = null;
			this.UnloadStreams();
		}

		/// <summary>Opens the entry from the zip archive.</summary>
		/// <returns>The stream that represents the contents of the entry.</returns>
		/// <exception cref="T:System.IO.IOException">The entry is already currently open for writing.-or-The entry has been deleted from the archive.-or-The archive for this entry was opened with the <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, and this entry has already been written to. </exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is either missing from the archive or is corrupt and cannot be read. -or-The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		// Token: 0x06000147 RID: 327 RVA: 0x00007334 File Offset: 0x00005534
		public Stream Open()
		{
			this.ThrowIfInvalidArchive();
			switch (this._archive.Mode)
			{
			case ZipArchiveMode.Read:
				return this.OpenInReadMode(true);
			case ZipArchiveMode.Create:
				return this.OpenInWriteMode();
			}
			return this.OpenInUpdateMode();
		}

		/// <summary>Retrieves the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry, which is the value stored in the <see cref="P:System.IO.Compression.ZipArchiveEntry.FullName" /> property.</returns>
		// Token: 0x06000148 RID: 328 RVA: 0x0000737C File Offset: 0x0000557C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007384 File Offset: 0x00005584
		internal bool EverOpenedForWrite
		{
			get
			{
				return this._everOpenedForWrite;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000738C File Offset: 0x0000558C
		private long OffsetOfCompressedData
		{
			get
			{
				if (this._storedOffsetOfCompressedData == null)
				{
					this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
					if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
					{
						throw new InvalidDataException("A local file header is corrupt.");
					}
					this._storedOffsetOfCompressedData = new long?(this._archive.ArchiveStream.Position);
				}
				return this._storedOffsetOfCompressedData.Value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007404 File Offset: 0x00005604
		private MemoryStream UncompressedData
		{
			get
			{
				if (this._storedUncompressedData == null)
				{
					this._storedUncompressedData = new MemoryStream((int)this._uncompressedSize);
					if (this._originallyInArchive)
					{
						using (Stream stream = this.OpenInReadMode(false))
						{
							try
							{
								stream.CopyTo(this._storedUncompressedData);
							}
							catch (InvalidDataException)
							{
								this._storedUncompressedData.Dispose();
								this._storedUncompressedData = null;
								this._currentlyOpenForWrite = false;
								this._everOpenedForWrite = false;
								throw;
							}
						}
					}
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
				}
				return this._storedUncompressedData;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000074A0 File Offset: 0x000056A0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000074A8 File Offset: 0x000056A8
		private ZipArchiveEntry.CompressionMethodValues CompressionMethod
		{
			get
			{
				return this._storedCompressionMethod;
			}
			set
			{
				if (value == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
				else if (value == ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.Deflate64);
				}
				this._storedCompressionMethod = value;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000074CC File Offset: 0x000056CC
		private string DecodeEntryName(byte[] entryNameBytes)
		{
			Encoding encoding;
			if ((this._generalPurposeBitFlag & ZipArchiveEntry.BitFlagValues.UnicodeFileName) == (ZipArchiveEntry.BitFlagValues)0)
			{
				encoding = ((this._archive == null) ? Encoding.UTF8 : (this._archive.EntryNameEncoding ?? Encoding.UTF8));
			}
			else
			{
				encoding = Encoding.UTF8;
			}
			return encoding.GetString(entryNameBytes);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000751C File Offset: 0x0000571C
		private byte[] EncodeEntryName(string entryName, out bool isUTF8)
		{
			Encoding encoding;
			if (this._archive != null && this._archive.EntryNameEncoding != null)
			{
				encoding = this._archive.EntryNameEncoding;
			}
			else
			{
				encoding = (ZipHelper.RequiresUnicode(entryName) ? Encoding.UTF8 : Encoding.ASCII);
			}
			isUTF8 = encoding.Equals(Encoding.UTF8);
			return encoding.GetBytes(entryName);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007575 File Offset: 0x00005775
		internal void WriteAndFinishLocalEntry()
		{
			this.CloseStreams();
			this.WriteLocalFileHeaderAndDataIfNeeded();
			this.UnloadStreams();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000758C File Offset: 0x0000578C
		internal void WriteCentralDirectoryFileHeader()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (this.SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
			}
			else
			{
				value = (uint)this._compressedSize;
				value2 = (uint)this._uncompressedSize;
			}
			uint value3;
			if (this._offsetOfLocalHeader > (long)((ulong)-1))
			{
				flag = true;
				value3 = uint.MaxValue;
				zip64ExtraField.LocalHeaderOffset = new long?(this._offsetOfLocalHeader);
			}
			else
			{
				value3 = (uint)this._offsetOfLocalHeader;
			}
			if (flag)
			{
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			int num = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._cdUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._cdUnknownExtraFields) : 0);
			ushort value4;
			if (num > 65535)
			{
				value4 = (flag ? zip64ExtraField.TotalSize : 0);
				this._cdUnknownExtraFields = null;
			}
			else
			{
				value4 = (ushort)num;
			}
			binaryWriter.Write(33639248U);
			binaryWriter.Write((byte)this._versionMadeBySpecification);
			binaryWriter.Write((byte)ZipArchiveEntry.CurrentZipPlatform);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(value4);
			binaryWriter.Write((this._fileComment != null) ? ((ushort)this._fileComment.Length) : 0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(this._externalFileAttr);
			binaryWriter.Write(value3);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._cdUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._cdUnknownExtraFields, this._archive.ArchiveStream);
			}
			if (this._fileComment != null)
			{
				binaryWriter.Write(this._fileComment);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000077A0 File Offset: 0x000059A0
		internal bool LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded()
		{
			if (this._originallyInArchive)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				this._lhUnknownExtraFields = ZipLocalFileHeader.GetExtraFields(this._archive.ArchiveReader);
			}
			if (!this._everOpenedForWrite && this._originallyInArchive)
			{
				this._compressedBytes = new byte[this._compressedSize / 2147483591L + 1L][];
				for (int i = 0; i < this._compressedBytes.Length - 1; i++)
				{
					this._compressedBytes[i] = new byte[2147483591];
				}
				this._compressedBytes[this._compressedBytes.Length - 1] = new byte[this._compressedSize % 2147483591L];
				this._archive.ArchiveStream.Seek(this.OffsetOfCompressedData, SeekOrigin.Begin);
				for (int j = 0; j < this._compressedBytes.Length - 1; j++)
				{
					ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes[j], 2147483591);
				}
				ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes[this._compressedBytes.Length - 1], (int)(this._compressedSize % 2147483591L));
			}
			return true;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000078DC File Offset: 0x00005ADC
		internal void ThrowIfNotOpenable(bool needToUncompress, bool needToLoadIntoMemory)
		{
			string message;
			if (!this.IsOpenable(needToUncompress, needToLoadIntoMemory, out message))
			{
				throw new InvalidDataException(message);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000078FC File Offset: 0x00005AFC
		private CheckSumAndSizeWriteStream GetDataCompressor(Stream backingStream, bool leaveBackingStreamOpen, EventHandler onClose)
		{
			Stream baseStream = (this._compressionLevel != null) ? new DeflateStream(backingStream, this._compressionLevel.Value, leaveBackingStreamOpen) : new DeflateStream(backingStream, CompressionMode.Compress, leaveBackingStreamOpen);
			bool flag = true;
			bool leaveOpenOnClose = leaveBackingStreamOpen && !flag;
			return new CheckSumAndSizeWriteStream(baseStream, backingStream, leaveOpenOnClose, this, onClose, delegate(long initialPosition, long currentPosition, uint checkSum, Stream backing, ZipArchiveEntry thisRef, EventHandler closeHandler)
			{
				thisRef._crc32 = checkSum;
				thisRef._uncompressedSize = currentPosition;
				thisRef._compressedSize = backing.Position - initialPosition;
				if (closeHandler != null)
				{
					closeHandler(thisRef, EventArgs.Empty);
				}
			});
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007968 File Offset: 0x00005B68
		private Stream GetDataDecompressor(Stream compressedStreamToRead)
		{
			ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
			if (compressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored)
			{
				if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					return new DeflateStream(compressedStreamToRead, CompressionMode.Decompress);
				}
				if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					return new DeflateManagedStream(compressedStreamToRead, ZipArchiveEntry.CompressionMethodValues.Deflate64);
				}
			}
			return compressedStreamToRead;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000079A4 File Offset: 0x00005BA4
		private Stream OpenInReadMode(bool checkOpenable)
		{
			if (checkOpenable)
			{
				this.ThrowIfNotOpenable(true, false);
			}
			Stream compressedStreamToRead = new SubReadStream(this._archive.ArchiveStream, this.OffsetOfCompressedData, this._compressedSize);
			return this.GetDataDecompressor(compressedStreamToRead);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000079E0 File Offset: 0x00005BE0
		private Stream OpenInWriteMode()
		{
			if (this._everOpenedForWrite)
			{
				throw new IOException("Entries in create mode may only be written to once, and only one entry may be held open at a time.");
			}
			this._everOpenedForWrite = true;
			CheckSumAndSizeWriteStream dataCompressor = this.GetDataCompressor(this._archive.ArchiveStream, true, delegate(object o, EventArgs e)
			{
				ZipArchiveEntry zipArchiveEntry = (ZipArchiveEntry)o;
				zipArchiveEntry._archive.ReleaseArchiveStream(zipArchiveEntry);
				zipArchiveEntry._outstandingWriteStream = null;
			});
			this._outstandingWriteStream = new ZipArchiveEntry.DirectToArchiveWriterStream(dataCompressor, this);
			return new WrappedStream(this._outstandingWriteStream, true);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007A54 File Offset: 0x00005C54
		private Stream OpenInUpdateMode()
		{
			if (this._currentlyOpenForWrite)
			{
				throw new IOException("Entries cannot be opened multiple times in Update mode.");
			}
			this.ThrowIfNotOpenable(true, true);
			this._everOpenedForWrite = true;
			this._currentlyOpenForWrite = true;
			this.UncompressedData.Seek(0L, SeekOrigin.Begin);
			return new WrappedStream(this.UncompressedData, this, delegate(ZipArchiveEntry thisRef)
			{
				thisRef._currentlyOpenForWrite = false;
			});
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007AC4 File Offset: 0x00005CC4
		private bool IsOpenable(bool needToUncompress, bool needToLoadIntoMemory, out string message)
		{
			message = null;
			if (this._originallyInArchive)
			{
				if (needToUncompress && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
					if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.BZip2 || compressionMethod == ZipArchiveEntry.CompressionMethodValues.LZMA)
					{
						message = SR.Format("The archive entry was compressed using {0} and is not supported.", this.CompressionMethod.ToString());
					}
					else
					{
						message = "The archive entry was compressed using an unsupported compression method.";
					}
					return false;
				}
				if ((long)this._diskNumberStart != (long)((ulong)this._archive.NumberOfThisDisk))
				{
					message = "Split or spanned archives are not supported.";
					return false;
				}
				if (this._offsetOfLocalHeader > this._archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (this.OffsetOfCompressedData + this._compressedSize > this._archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (needToLoadIntoMemory && this._compressedSize > 2147483647L && !ZipArchiveEntry.s_allowLargeZipArchiveEntriesInUpdateMode)
				{
					message = "Entries larger than 4GB are not supported in Update mode.";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007BF5 File Offset: 0x00005DF5
		private bool SizesTooLarge()
		{
			return this._compressedSize > (long)((ulong)-1) || this._uncompressedSize > (long)((ulong)-1);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007C10 File Offset: 0x00005E10
		private bool WriteLocalFileHeader(bool isEmptyFile)
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (isEmptyFile)
			{
				this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				value = 0U;
				value2 = 0U;
			}
			else if (this._archive.Mode == ZipArchiveMode.Create && !this._archive.ArchiveStream.CanSeek && !isEmptyFile)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				flag = false;
				value = 0U;
				value2 = 0U;
			}
			else if (this.SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			else
			{
				flag = false;
				value = (uint)this._compressedSize;
				value2 = (uint)this._uncompressedSize;
			}
			this._offsetOfLocalHeader = binaryWriter.BaseStream.Position;
			int num = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._lhUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._lhUnknownExtraFields) : 0);
			ushort value3;
			if (num > 65535)
			{
				value3 = (flag ? zip64ExtraField.TotalSize : 0);
				this._lhUnknownExtraFields = null;
			}
			else
			{
				value3 = (ushort)num;
			}
			binaryWriter.Write(67324752U);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(value3);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._lhUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._lhUnknownExtraFields, this._archive.ArchiveStream);
			}
			return flag;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007DEC File Offset: 0x00005FEC
		private void WriteLocalFileHeaderAndDataIfNeeded()
		{
			if (this._storedUncompressedData != null || this._compressedBytes != null)
			{
				if (this._storedUncompressedData != null)
				{
					this._uncompressedSize = this._storedUncompressedData.Length;
					using (Stream stream = new ZipArchiveEntry.DirectToArchiveWriterStream(this.GetDataCompressor(this._archive.ArchiveStream, true, null), this))
					{
						this._storedUncompressedData.Seek(0L, SeekOrigin.Begin);
						this._storedUncompressedData.CopyTo(stream);
						this._storedUncompressedData.Dispose();
						this._storedUncompressedData = null;
						return;
					}
				}
				if (this._uncompressedSize == 0L)
				{
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				}
				this.WriteLocalFileHeader(false);
				foreach (byte[] array in this._compressedBytes)
				{
					this._archive.ArchiveStream.Write(array, 0, array.Length);
				}
				return;
			}
			if (this._archive.Mode == ZipArchiveMode.Update || !this._everOpenedForWrite)
			{
				this._everOpenedForWrite = true;
				this.WriteLocalFileHeader(true);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007EF4 File Offset: 0x000060F4
		private void WriteCrcAndSizesInLocalHeader(bool zip64HeaderUsed)
		{
			long position = this._archive.ArchiveStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			bool flag = this.SizesTooLarge();
			bool flag2 = flag && !zip64HeaderUsed;
			uint value = flag ? uint.MaxValue : ((uint)this._compressedSize);
			uint value2 = flag ? uint.MaxValue : ((uint)this._uncompressedSize);
			if (flag2)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 6L, SeekOrigin.Begin);
				binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			}
			this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 14L, SeekOrigin.Begin);
			if (!flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(value);
				binaryWriter.Write(value2);
			}
			else
			{
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
			}
			if (zip64HeaderUsed)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 30L + (long)this._storedEntryNameBytes.Length + 4L, SeekOrigin.Begin);
				binaryWriter.Write(this._uncompressedSize);
				binaryWriter.Write(this._compressedSize);
				this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			if (flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000806C File Offset: 0x0000626C
		private void WriteDataDescriptor()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			binaryWriter.Write(134695760U);
			binaryWriter.Write(this._crc32);
			if (this.SizesTooLarge())
			{
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
				return;
			}
			binaryWriter.Write((uint)this._compressedSize);
			binaryWriter.Write((uint)this._uncompressedSize);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000080DC File Offset: 0x000062DC
		private void UnloadStreams()
		{
			if (this._storedUncompressedData != null)
			{
				this._storedUncompressedData.Dispose();
			}
			this._compressedBytes = null;
			this._outstandingWriteStream = null;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000080FF File Offset: 0x000062FF
		private void CloseStreams()
		{
			if (this._outstandingWriteStream != null)
			{
				this._outstandingWriteStream.Dispose();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008114 File Offset: 0x00006314
		private void VersionToExtractAtLeast(ZipVersionNeededValues value)
		{
			if (this._versionToExtract < value)
			{
				this._versionToExtract = value;
			}
			if (this._versionMadeBySpecification < value)
			{
				this._versionMadeBySpecification = value;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008136 File Offset: 0x00006336
		private void ThrowIfInvalidArchive()
		{
			if (this._archive == null)
			{
				throw new InvalidOperationException("Cannot modify deleted entry.");
			}
			this._archive.ThrowIfDisposed();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008158 File Offset: 0x00006358
		private static string GetFileName_Windows(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				char c = path[num];
				if (c == '\\' || c == '/' || c == ':')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008198 File Offset: 0x00006398
		private static string GetFileName_Unix(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				if (path[num] == '/')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000081CC File Offset: 0x000063CC
		internal static string ParseFileName(string path, ZipVersionMadeByPlatform madeByPlatform)
		{
			if (madeByPlatform == ZipVersionMadeByPlatform.Windows)
			{
				return ZipArchiveEntry.GetFileName_Windows(path);
			}
			if (madeByPlatform != ZipVersionMadeByPlatform.Unix)
			{
				return ZipArchiveEntry.ParseFileName(path, ZipArchiveEntry.CurrentZipPlatform);
			}
			return ZipArchiveEntry.GetFileName_Unix(path);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000081F0 File Offset: 0x000063F0
		// Note: this type is marked as 'beforefieldinit'.
		static ZipArchiveEntry()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008211 File Offset: 0x00006411
		internal ZipArchiveEntry()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400017B RID: 379
		private const ushort DefaultVersionToExtract = 10;

		// Token: 0x0400017C RID: 380
		private const int MaxSingleBufferSize = 2147483591;

		// Token: 0x0400017D RID: 381
		private ZipArchive _archive;

		// Token: 0x0400017E RID: 382
		private readonly bool _originallyInArchive;

		// Token: 0x0400017F RID: 383
		private readonly int _diskNumberStart;

		// Token: 0x04000180 RID: 384
		private readonly ZipVersionMadeByPlatform _versionMadeByPlatform;

		// Token: 0x04000181 RID: 385
		private ZipVersionNeededValues _versionMadeBySpecification;

		// Token: 0x04000182 RID: 386
		private ZipVersionNeededValues _versionToExtract;

		// Token: 0x04000183 RID: 387
		private ZipArchiveEntry.BitFlagValues _generalPurposeBitFlag;

		// Token: 0x04000184 RID: 388
		private ZipArchiveEntry.CompressionMethodValues _storedCompressionMethod;

		// Token: 0x04000185 RID: 389
		private DateTimeOffset _lastModified;

		// Token: 0x04000186 RID: 390
		private long _compressedSize;

		// Token: 0x04000187 RID: 391
		private long _uncompressedSize;

		// Token: 0x04000188 RID: 392
		private long _offsetOfLocalHeader;

		// Token: 0x04000189 RID: 393
		private long? _storedOffsetOfCompressedData;

		// Token: 0x0400018A RID: 394
		private uint _crc32;

		// Token: 0x0400018B RID: 395
		private byte[][] _compressedBytes;

		// Token: 0x0400018C RID: 396
		private MemoryStream _storedUncompressedData;

		// Token: 0x0400018D RID: 397
		private bool _currentlyOpenForWrite;

		// Token: 0x0400018E RID: 398
		private bool _everOpenedForWrite;

		// Token: 0x0400018F RID: 399
		private Stream _outstandingWriteStream;

		// Token: 0x04000190 RID: 400
		private uint _externalFileAttr;

		// Token: 0x04000191 RID: 401
		private string _storedEntryName;

		// Token: 0x04000192 RID: 402
		private byte[] _storedEntryNameBytes;

		// Token: 0x04000193 RID: 403
		private List<ZipGenericExtraField> _cdUnknownExtraFields;

		// Token: 0x04000194 RID: 404
		private List<ZipGenericExtraField> _lhUnknownExtraFields;

		// Token: 0x04000195 RID: 405
		private byte[] _fileComment;

		// Token: 0x04000196 RID: 406
		private CompressionLevel? _compressionLevel;

		// Token: 0x04000197 RID: 407
		private static readonly bool s_allowLargeZipArchiveEntriesInUpdateMode = IntPtr.Size > 4;

		// Token: 0x04000198 RID: 408
		internal static readonly ZipVersionMadeByPlatform CurrentZipPlatform = (Path.PathSeparator == '/') ? ZipVersionMadeByPlatform.Unix : ZipVersionMadeByPlatform.Windows;

		// Token: 0x0200002B RID: 43
		private sealed class DirectToArchiveWriterStream : Stream
		{
			// Token: 0x06000168 RID: 360 RVA: 0x00008218 File Offset: 0x00006418
			public DirectToArchiveWriterStream(CheckSumAndSizeWriteStream crcSizeStream, ZipArchiveEntry entry)
			{
				this._position = 0L;
				this._crcSizeStream = crcSizeStream;
				this._everWritten = false;
				this._isDisposed = false;
				this._entry = entry;
				this._usedZip64inLH = false;
				this._canWrite = true;
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000169 RID: 361 RVA: 0x00008252 File Offset: 0x00006452
			public override long Length
			{
				get
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600016A RID: 362 RVA: 0x00008264 File Offset: 0x00006464
			// (set) Token: 0x0600016B RID: 363 RVA: 0x00008252 File Offset: 0x00006452
			public override long Position
			{
				get
				{
					this.ThrowIfDisposed();
					return this._position;
				}
				set
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600016C RID: 364 RVA: 0x00002289 File Offset: 0x00000489
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00002289 File Offset: 0x00000489
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600016E RID: 366 RVA: 0x00008272 File Offset: 0x00006472
			public override bool CanWrite
			{
				get
				{
					return this._canWrite;
				}
			}

			// Token: 0x0600016F RID: 367 RVA: 0x0000827A File Offset: 0x0000647A
			private void ThrowIfDisposed()
			{
				if (this._isDisposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x0000829A File Offset: 0x0000649A
			public override int Read(byte[] buffer, int offset, int count)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00008252 File Offset: 0x00006452
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}

			// Token: 0x06000172 RID: 370 RVA: 0x000082AC File Offset: 0x000064AC
			public override void SetLength(long value)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
			}

			// Token: 0x06000173 RID: 371 RVA: 0x000082C0 File Offset: 0x000064C0
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
				}
				if (buffer.Length - offset < count)
				{
					throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
				}
				this.ThrowIfDisposed();
				if (count == 0)
				{
					return;
				}
				if (!this._everWritten)
				{
					this._everWritten = true;
					this._usedZip64inLH = this._entry.WriteLocalFileHeader(false);
				}
				this._crcSizeStream.Write(buffer, offset, count);
				this._position += (long)count;
			}

			// Token: 0x06000174 RID: 372 RVA: 0x0000835E File Offset: 0x0000655E
			public override void Flush()
			{
				this.ThrowIfDisposed();
				this._crcSizeStream.Flush();
			}

			// Token: 0x06000175 RID: 373 RVA: 0x00008374 File Offset: 0x00006574
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this._isDisposed)
				{
					this._crcSizeStream.Dispose();
					if (!this._everWritten)
					{
						this._entry.WriteLocalFileHeader(true);
					}
					else if (this._entry._archive.ArchiveStream.CanSeek)
					{
						this._entry.WriteCrcAndSizesInLocalHeader(this._usedZip64inLH);
					}
					else
					{
						this._entry.WriteDataDescriptor();
					}
					this._canWrite = false;
					this._isDisposed = true;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000199 RID: 409
			private long _position;

			// Token: 0x0400019A RID: 410
			private CheckSumAndSizeWriteStream _crcSizeStream;

			// Token: 0x0400019B RID: 411
			private bool _everWritten;

			// Token: 0x0400019C RID: 412
			private bool _isDisposed;

			// Token: 0x0400019D RID: 413
			private ZipArchiveEntry _entry;

			// Token: 0x0400019E RID: 414
			private bool _usedZip64inLH;

			// Token: 0x0400019F RID: 415
			private bool _canWrite;
		}

		// Token: 0x0200002C RID: 44
		[Flags]
		private enum BitFlagValues : ushort
		{
			// Token: 0x040001A1 RID: 417
			DataDescriptor = 8,
			// Token: 0x040001A2 RID: 418
			UnicodeFileName = 2048
		}

		// Token: 0x0200002D RID: 45
		internal enum CompressionMethodValues : ushort
		{
			// Token: 0x040001A4 RID: 420
			Stored,
			// Token: 0x040001A5 RID: 421
			Deflate = 8,
			// Token: 0x040001A6 RID: 422
			Deflate64,
			// Token: 0x040001A7 RID: 423
			BZip2 = 12,
			// Token: 0x040001A8 RID: 424
			LZMA = 14
		}

		// Token: 0x0200002E RID: 46
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000176 RID: 374 RVA: 0x000083F8 File Offset: 0x000065F8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000177 RID: 375 RVA: 0x0000353B File Offset: 0x0000173B
			public <>c()
			{
			}

			// Token: 0x06000178 RID: 376 RVA: 0x00008404 File Offset: 0x00006604
			internal void <GetDataCompressor>b__69_0(long initialPosition, long currentPosition, uint checkSum, Stream backing, ZipArchiveEntry thisRef, EventHandler closeHandler)
			{
				thisRef._crc32 = checkSum;
				thisRef._uncompressedSize = currentPosition;
				thisRef._compressedSize = backing.Position - initialPosition;
				if (closeHandler != null)
				{
					closeHandler(thisRef, EventArgs.Empty);
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00008438 File Offset: 0x00006638
			internal void <OpenInWriteMode>b__72_0(object o, EventArgs e)
			{
				ZipArchiveEntry zipArchiveEntry = (ZipArchiveEntry)o;
				zipArchiveEntry._archive.ReleaseArchiveStream(zipArchiveEntry);
				zipArchiveEntry._outstandingWriteStream = null;
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0000845F File Offset: 0x0000665F
			internal void <OpenInUpdateMode>b__73_0(ZipArchiveEntry thisRef)
			{
				thisRef._currentlyOpenForWrite = false;
			}

			// Token: 0x040001A9 RID: 425
			public static readonly ZipArchiveEntry.<>c <>9 = new ZipArchiveEntry.<>c();

			// Token: 0x040001AA RID: 426
			public static Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> <>9__69_0;

			// Token: 0x040001AB RID: 427
			public static EventHandler <>9__72_0;

			// Token: 0x040001AC RID: 428
			public static Action<ZipArchiveEntry> <>9__73_0;
		}
	}
}
