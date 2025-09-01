using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace System.IO.Compression
{
	/// <summary>Represents a package of compressed files in the zip archive format.</summary>
	// Token: 0x02000029 RID: 41
	public class ZipArchive : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream.</summary>
		/// <param name="stream">The stream that contains the archive to be read.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed or does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream are not in the zip archive format.</exception>
		// Token: 0x0600011A RID: 282 RVA: 0x000064DF File Offset: 0x000046DF
		public ZipArchive(Stream stream) : this(stream, ZipArchiveMode.Read, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream and with the specified mode.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		// Token: 0x0600011B RID: 283 RVA: 0x000064EB File Offset: 0x000046EB
		public ZipArchive(Stream stream, ZipArchiveMode mode) : this(stream, mode, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <param name="leaveOpen">
		///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		// Token: 0x0600011C RID: 284 RVA: 0x000064F7 File Offset: 0x000046F7
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen) : this(stream, mode, leaveOpen, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, uses the specified encoding for entry names, and optionally leaves the stream open.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <param name="leaveOpen">
		///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		// Token: 0x0600011D RID: 285 RVA: 0x00006503 File Offset: 0x00004703
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.EntryNameEncoding = entryNameEncoding;
			this.Init(stream, mode, leaveOpen);
		}

		/// <summary>Gets the collection of entries that are currently in the zip archive.</summary>
		/// <returns>The collection of entries that are currently in the zip archive.</returns>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000652A File Offset: 0x0000472A
		public ReadOnlyCollection<ZipArchiveEntry> Entries
		{
			get
			{
				if (this._mode == ZipArchiveMode.Create)
				{
					throw new NotSupportedException("Cannot access entries in Create mode.");
				}
				this.ThrowIfDisposed();
				this.EnsureCentralDirectoryRead();
				return this._entriesCollection;
			}
		}

		/// <summary>Gets a value that describes the type of action the zip archive can perform on entries.</summary>
		/// <returns>One of the enumeration values that describes the type of action (read, create, or update) the zip archive can perform on entries.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006552 File Offset: 0x00004752
		public ZipArchiveMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		/// <summary>Creates an empty entry that has the specified path and entry name in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
		/// <returns>An empty entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		// Token: 0x06000120 RID: 288 RVA: 0x0000655C File Offset: 0x0000475C
		public ZipArchiveEntry CreateEntry(string entryName)
		{
			return this.DoCreateEntry(entryName, null);
		}

		/// <summary>Creates an empty entry that has the specified entry name and compression level in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <returns>An empty entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		// Token: 0x06000121 RID: 289 RVA: 0x00006579 File Offset: 0x00004779
		public ZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
		{
			return this.DoCreateEntry(entryName, new CompressionLevel?(compressionLevel));
		}

		/// <summary>Called by the <see cref="M:System.IO.Compression.ZipArchive.Dispose" /> and <see cref="M:System.Object.Finalize" /> methods to release the unmanaged resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class, and optionally finishes writing the archive and releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to finish writing the archive and release unmanaged and managed resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000122 RID: 290 RVA: 0x00006588 File Offset: 0x00004788
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				try
				{
					ZipArchiveMode mode = this._mode;
					if (mode != ZipArchiveMode.Read)
					{
						int num = mode - ZipArchiveMode.Create;
						this.WriteFile();
					}
				}
				finally
				{
					this.CloseStreams();
					this._isDisposed = true;
				}
			}
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class.</summary>
		// Token: 0x06000123 RID: 291 RVA: 0x000065D8 File Offset: 0x000047D8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Retrieves a wrapper for the specified entry in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that identifies the entry to retrieve.</param>
		/// <returns>A wrapper for the specified entry in the archive; <see langword="null" /> if the entry does not exist in the archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		// Token: 0x06000124 RID: 292 RVA: 0x000065E8 File Offset: 0x000047E8
		public ZipArchiveEntry GetEntry(string entryName)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException("Cannot access entries in Create mode.");
			}
			this.EnsureCentralDirectoryRead();
			ZipArchiveEntry result;
			this._entriesDictionary.TryGetValue(entryName, out result);
			return result;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000662D File Offset: 0x0000482D
		internal BinaryReader ArchiveReader
		{
			get
			{
				return this._archiveReader;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006635 File Offset: 0x00004835
		internal Stream ArchiveStream
		{
			get
			{
				return this._archiveStream;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000663D File Offset: 0x0000483D
		internal uint NumberOfThisDisk
		{
			get
			{
				return this._numberOfThisDisk;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006645 File Offset: 0x00004845
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000664D File Offset: 0x0000484D
		internal Encoding EntryNameEncoding
		{
			get
			{
				return this._entryNameEncoding;
			}
			private set
			{
				if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode)))
				{
					throw new ArgumentException("The specified entry name encoding is not supported.", "EntryNameEncoding");
				}
				this._entryNameEncoding = value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006684 File Offset: 0x00004884
		private ZipArchiveEntry DoCreateEntry(string entryName, CompressionLevel? compressionLevel)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentException("String cannot be empty.", "entryName");
			}
			if (this._mode == ZipArchiveMode.Read)
			{
				throw new NotSupportedException("Cannot create entries on an archive opened in read mode.");
			}
			this.ThrowIfDisposed();
			ZipArchiveEntry zipArchiveEntry = (compressionLevel != null) ? new ZipArchiveEntry(this, entryName, compressionLevel.Value) : new ZipArchiveEntry(this, entryName);
			this.AddEntry(zipArchiveEntry);
			return zipArchiveEntry;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000066F9 File Offset: 0x000048F9
		internal void AcquireArchiveStream(ZipArchiveEntry entry)
		{
			if (this._archiveStreamOwner != null)
			{
				if (this._archiveStreamOwner.EverOpenedForWrite)
				{
					throw new IOException("Entries cannot be created while previously created entries are still open.");
				}
				this._archiveStreamOwner.WriteAndFinishLocalEntry();
			}
			this._archiveStreamOwner = entry;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006730 File Offset: 0x00004930
		private void AddEntry(ZipArchiveEntry entry)
		{
			this._entries.Add(entry);
			string fullName = entry.FullName;
			if (!this._entriesDictionary.ContainsKey(fullName))
			{
				this._entriesDictionary.Add(fullName, entry);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000042E9 File Offset: 0x000024E9
		[Conditional("DEBUG")]
		internal void DebugAssertIsStillArchiveStreamOwner(ZipArchiveEntry entry)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000676B File Offset: 0x0000496B
		internal void ReleaseArchiveStream(ZipArchiveEntry entry)
		{
			this._archiveStreamOwner = null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006774 File Offset: 0x00004974
		internal void RemoveEntry(ZipArchiveEntry entry)
		{
			this._entries.Remove(entry);
			this._entriesDictionary.Remove(entry.FullName);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006795 File Offset: 0x00004995
		internal void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000067B0 File Offset: 0x000049B0
		private void CloseStreams()
		{
			if (this._leaveOpen)
			{
				if (this._backingStream != null)
				{
					this._archiveStream.Dispose();
				}
				return;
			}
			this._archiveStream.Dispose();
			Stream backingStream = this._backingStream;
			if (backingStream != null)
			{
				backingStream.Dispose();
			}
			BinaryReader archiveReader = this._archiveReader;
			if (archiveReader == null)
			{
				return;
			}
			archiveReader.Dispose();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006805 File Offset: 0x00004A05
		private void EnsureCentralDirectoryRead()
		{
			if (!this._readEntries)
			{
				this.ReadCentralDirectory();
				this._readEntries = true;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000681C File Offset: 0x00004A1C
		private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		{
			Stream stream2 = null;
			try
			{
				this._backingStream = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					if (!stream.CanRead)
					{
						throw new ArgumentException("Cannot use read mode on a non-readable stream.");
					}
					if (!stream.CanSeek)
					{
						this._backingStream = stream;
						stream = (stream2 = new MemoryStream());
						this._backingStream.CopyTo(stream);
						stream.Seek(0L, SeekOrigin.Begin);
					}
					break;
				case ZipArchiveMode.Create:
					if (!stream.CanWrite)
					{
						throw new ArgumentException("Cannot use create mode on a non-writable stream.");
					}
					break;
				case ZipArchiveMode.Update:
					if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
					{
						throw new ArgumentException("Update mode requires a stream with read, write, and seek capabilities.");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("mode");
				}
				this._mode = mode;
				if (mode == ZipArchiveMode.Create && !stream.CanSeek)
				{
					this._archiveStream = new PositionPreservingWriteOnlyStreamWrapper(stream);
				}
				else
				{
					this._archiveStream = stream;
				}
				this._archiveStreamOwner = null;
				if (mode == ZipArchiveMode.Create)
				{
					this._archiveReader = null;
				}
				else
				{
					this._archiveReader = new BinaryReader(this._archiveStream);
				}
				this._entries = new List<ZipArchiveEntry>();
				this._entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(this._entries);
				this._entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
				this._readEntries = false;
				this._leaveOpen = leaveOpen;
				this._centralDirectoryStart = 0L;
				this._isDisposed = false;
				this._numberOfThisDisk = 0U;
				this._archiveComment = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					this.ReadEndOfCentralDirectory();
					goto IL_1BC;
				case ZipArchiveMode.Create:
					this._readEntries = true;
					goto IL_1BC;
				}
				if (this._archiveStream.Length == 0L)
				{
					this._readEntries = true;
				}
				else
				{
					this.ReadEndOfCentralDirectory();
					this.EnsureCentralDirectoryRead();
					foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
					{
						zipArchiveEntry.ThrowIfNotOpenable(false, true);
					}
				}
				IL_1BC:;
			}
			catch
			{
				if (stream2 != null)
				{
					stream2.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006A28 File Offset: 0x00004C28
		private void ReadCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(this._centralDirectoryStart, SeekOrigin.Begin);
				long num = 0L;
				bool saveExtraFieldsAndComments = this.Mode == ZipArchiveMode.Update;
				ZipCentralDirectoryFileHeader cd;
				while (ZipCentralDirectoryFileHeader.TryReadBlock(this._archiveReader, saveExtraFieldsAndComments, out cd))
				{
					this.AddEntry(new ZipArchiveEntry(this, cd));
					num += 1L;
				}
				if (num != this._expectedNumberOfEntries)
				{
					throw new InvalidDataException("Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.");
				}
			}
			catch (EndOfStreamException p)
			{
				throw new InvalidDataException(SR.Format("Central Directory is invalid.", p));
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006AB4 File Offset: 0x00004CB4
		private void ReadEndOfCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(-18L, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(this._archiveStream, 101010256U))
				{
					throw new InvalidDataException("End of Central Directory record could not be found.");
				}
				long position = this._archiveStream.Position;
				ZipEndOfCentralDirectoryBlock zipEndOfCentralDirectoryBlock;
				ZipEndOfCentralDirectoryBlock.TryReadBlock(this._archiveReader, out zipEndOfCentralDirectoryBlock);
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk != zipEndOfCentralDirectoryBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				this._numberOfThisDisk = (uint)zipEndOfCentralDirectoryBlock.NumberOfThisDisk;
				this._centralDirectoryStart = (long)((ulong)zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
				if (zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory != zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				this._expectedNumberOfEntries = (long)((ulong)zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory);
				if (this._mode == ZipArchiveMode.Update)
				{
					this._archiveComment = zipEndOfCentralDirectoryBlock.ArchiveComment;
				}
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk == 65535 || zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == 4294967295U || zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory == 65535)
				{
					this._archiveStream.Seek(position - 16L, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(this._archiveStream, 117853008U))
					{
						Zip64EndOfCentralDirectoryLocator zip64EndOfCentralDirectoryLocator;
						Zip64EndOfCentralDirectoryLocator.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryLocator);
						if (zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD > 9223372036854775807UL)
						{
							throw new InvalidDataException("Offset to Zip64 End Of Central Directory record cannot be held in an Int64.");
						}
						long offsetOfZip64EOCD = (long)zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD;
						this._archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
						Zip64EndOfCentralDirectoryRecord zip64EndOfCentralDirectoryRecord;
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryRecord))
						{
							throw new InvalidDataException("Zip 64 End of Central Directory Record not where indicated.");
						}
						this._numberOfThisDisk = zip64EndOfCentralDirectoryRecord.NumberOfThisDisk;
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal > 9223372036854775807UL)
						{
							throw new InvalidDataException("Number of Entries cannot be held in an Int64.");
						}
						if (zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory > 9223372036854775807UL)
						{
							throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
						}
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal != zip64EndOfCentralDirectoryRecord.NumberOfEntriesOnThisDisk)
						{
							throw new InvalidDataException("Split or spanned archives are not supported.");
						}
						this._expectedNumberOfEntries = (long)zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal;
						this._centralDirectoryStart = (long)zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory;
					}
				}
				if (this._centralDirectoryStart > this._archiveStream.Length)
				{
					throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidDataException("Central Directory corrupt.", innerException);
			}
			catch (IOException innerException2)
			{
				throw new InvalidDataException("Central Directory corrupt.", innerException2);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006D0C File Offset: 0x00004F0C
		private void WriteFile()
		{
			if (this._mode == ZipArchiveMode.Update)
			{
				List<ZipArchiveEntry> list = new List<ZipArchiveEntry>();
				foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
				{
					if (!zipArchiveEntry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
					{
						list.Add(zipArchiveEntry);
					}
				}
				foreach (ZipArchiveEntry zipArchiveEntry2 in list)
				{
					zipArchiveEntry2.Delete();
				}
				this._archiveStream.Seek(0L, SeekOrigin.Begin);
				this._archiveStream.SetLength(0L);
			}
			foreach (ZipArchiveEntry zipArchiveEntry3 in this._entries)
			{
				zipArchiveEntry3.WriteAndFinishLocalEntry();
			}
			long position = this._archiveStream.Position;
			foreach (ZipArchiveEntry zipArchiveEntry4 in this._entries)
			{
				zipArchiveEntry4.WriteCentralDirectoryFileHeader();
			}
			long sizeOfCentralDirectory = this._archiveStream.Position - position;
			this.WriteArchiveEpilogue(position, sizeOfCentralDirectory);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006E74 File Offset: 0x00005074
		private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			if (startOfCentralDirectory >= (long)((ulong)-1) || sizeOfCentralDirectory >= (long)((ulong)-1) || this._entries.Count >= 65535)
			{
				long position = this._archiveStream.Position;
				Zip64EndOfCentralDirectoryRecord.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
				Zip64EndOfCentralDirectoryLocator.WriteBlock(this._archiveStream, position);
			}
			ZipEndOfCentralDirectoryBlock.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, this._archiveComment);
		}

		// Token: 0x0400016B RID: 363
		private Stream _archiveStream;

		// Token: 0x0400016C RID: 364
		private ZipArchiveEntry _archiveStreamOwner;

		// Token: 0x0400016D RID: 365
		private BinaryReader _archiveReader;

		// Token: 0x0400016E RID: 366
		private ZipArchiveMode _mode;

		// Token: 0x0400016F RID: 367
		private List<ZipArchiveEntry> _entries;

		// Token: 0x04000170 RID: 368
		private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

		// Token: 0x04000171 RID: 369
		private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

		// Token: 0x04000172 RID: 370
		private bool _readEntries;

		// Token: 0x04000173 RID: 371
		private bool _leaveOpen;

		// Token: 0x04000174 RID: 372
		private long _centralDirectoryStart;

		// Token: 0x04000175 RID: 373
		private bool _isDisposed;

		// Token: 0x04000176 RID: 374
		private uint _numberOfThisDisk;

		// Token: 0x04000177 RID: 375
		private long _expectedNumberOfEntries;

		// Token: 0x04000178 RID: 376
		private Stream _backingStream;

		// Token: 0x04000179 RID: 377
		private byte[] _archiveComment;

		// Token: 0x0400017A RID: 378
		private Encoding _entryNameEncoding;
	}
}
