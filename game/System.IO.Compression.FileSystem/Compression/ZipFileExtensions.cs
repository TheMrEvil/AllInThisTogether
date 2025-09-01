using System;
using System.ComponentModel;

namespace System.IO.Compression
{
	/// <summary>Provides extension methods for the <see cref="T:System.IO.Compression.ZipArchive" /> and <see cref="T:System.IO.Compression.ZipArchiveEntry" /> classes.</summary>
	// Token: 0x02000006 RID: 6
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ZipFileExtensions
	{
		/// <summary>Archives a file by compressing it and adding it to the zip archive.</summary>
		/// <param name="destination">The zip archive to add the file to.</param>
		/// <param name="sourceFileName">The path to the file to be archived. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="entryName">The name of the entry to create in the zip archive.</param>
		/// <returns>A wrapper for the new entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The file specified by <paramref name="sourceFileName" /> cannot be opened, or is too large to be updated (current limit is Int32.MaxValue).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="sourceFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified by <paramref name="sourceFileName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="sourceFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="sourceFileName" /> parameter is in an invalid format.  
		///  -or-  
		///  The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		// Token: 0x06000014 RID: 20 RVA: 0x00002514 File Offset: 0x00000714
		public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName)
		{
			return ZipFileExtensions.DoCreateEntryFromFile(destination, sourceFileName, entryName, null);
		}

		/// <summary>Archives a file by compressing it using the specified compression level and adding it to the zip archive.</summary>
		/// <param name="destination">The zip archive to add the file to.</param>
		/// <param name="sourceFileName">The path to the file to be archived. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="entryName">The name of the entry to create in the zip archive.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <returns>A wrapper for the new entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">The file specified by <paramref name="sourceFileName" /> cannot be opened, or is too large to be updated (current limit is Int32.MaxValue).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="sourceFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified by <paramref name="sourceFileName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="sourceFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="sourceFileName" /> parameter is in an invalid format.  
		///  -or-  
		///  The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		// Token: 0x06000015 RID: 21 RVA: 0x00002532 File Offset: 0x00000732
		public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName, CompressionLevel compressionLevel)
		{
			return ZipFileExtensions.DoCreateEntryFromFile(destination, sourceFileName, entryName, new CompressionLevel?(compressionLevel));
		}

		/// <summary>Extracts all the files in the zip archive to a directory on the file system.</summary>
		/// <param name="source">The zip archive to extract files from.</param>
		/// <param name="destinationDirectoryName">The path to the directory to place the extracted files in. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationDirectoryName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationDirectoryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  The name of an entry in the archive is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		///  -or-  
		///  Extracting an entry from the archive would create a file that is outside the directory specified by <paramref name="destinationDirectoryName" />. (For example, this might happen if the entry name contains parent directory accessors.)  
		///  -or-  
		///  Two or more entries in the archive have the same name.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to write to the destination directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationDirectoryName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">An archive entry cannot be found or is corrupt.  
		///  -or-  
		///  An archive entry was compressed by using a compression method that is not supported.</exception>
		// Token: 0x06000016 RID: 22 RVA: 0x00002542 File Offset: 0x00000742
		public static void ExtractToDirectory(this ZipArchive source, string destinationDirectoryName)
		{
			source.ExtractToDirectory(destinationDirectoryName, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000254C File Offset: 0x0000074C
		public static void ExtractToDirectory(this ZipArchive source, string destinationDirectoryName, bool overwrite)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("destinationDirectoryName");
			}
			string text = Directory.CreateDirectory(destinationDirectoryName).FullName;
			if (!text.EndsWith(Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar.ToString();
			}
			foreach (ZipArchiveEntry zipArchiveEntry in source.Entries)
			{
				string fullPath = Path.GetFullPath(Path.Combine(text, zipArchiveEntry.FullName));
				if (!fullPath.StartsWith(text, PathInternal.StringComparison))
				{
					throw new IOException("Extracting Zip entry would have resulted in a file outside the specified destination directory.");
				}
				if (Path.GetFileName(fullPath).Length == 0)
				{
					if (zipArchiveEntry.Length != 0L)
					{
						throw new IOException("Zip entry name ends in directory separator character but contains data.");
					}
					Directory.CreateDirectory(fullPath);
				}
				else
				{
					Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
					zipArchiveEntry.ExtractToFile(fullPath, overwrite);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002640 File Offset: 0x00000840
		internal static ZipArchiveEntry DoCreateEntryFromFile(ZipArchive destination, string sourceFileName, string entryName, CompressionLevel? compressionLevel)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			ZipArchiveEntry result;
			using (Stream stream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false))
			{
				ZipArchiveEntry zipArchiveEntry = (compressionLevel != null) ? destination.CreateEntry(entryName, compressionLevel.Value) : destination.CreateEntry(entryName);
				DateTime lastWriteTime = File.GetLastWriteTime(sourceFileName);
				if (lastWriteTime.Year < 1980 || lastWriteTime.Year > 2107)
				{
					lastWriteTime = new DateTime(1980, 1, 1, 0, 0, 0);
				}
				zipArchiveEntry.LastWriteTime = lastWriteTime;
				using (Stream stream2 = zipArchiveEntry.Open())
				{
					stream.CopyTo(stream2);
				}
				result = zipArchiveEntry;
			}
			return result;
		}

		/// <summary>Extracts an entry in the zip archive to a file.</summary>
		/// <param name="source">The zip archive entry to extract a file from.</param>
		/// <param name="destinationFileName">The path of the file to create from the contents of the entry. You can  specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="destinationFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationFileName" /> already exists.  
		/// -or-  
		/// An I/O error occurred.  
		/// -or-  
		/// The entry is currently open for writing.  
		/// -or-  
		/// The entry has been deleted from the archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to create the new file.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is missing from the archive, or is corrupt and cannot be read.  
		///  -or-  
		///  The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive that this entry belongs to has been disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationFileName" /> is in an invalid format.  
		/// -or-  
		/// The zip archive for this entry was opened in <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, which does not permit the retrieval of entries.</exception>
		// Token: 0x06000019 RID: 25 RVA: 0x00002730 File Offset: 0x00000930
		public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName)
		{
			source.ExtractToFile(destinationFileName, false);
		}

		/// <summary>Extracts an entry in the zip archive to a file, and optionally overwrites an existing file that has the same name.</summary>
		/// <param name="source">The zip archive entry to extract a file from.</param>
		/// <param name="destinationFileName">The path of the file to create from the contents of the entry. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> to overwrite an existing file that has the same name as the destination file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="destinationFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationFileName" /> already exists and <paramref name="overwrite" /> is <see langword="false" />.  
		/// -or-  
		/// An I/O error occurred.  
		/// -or-  
		/// The entry is currently open for writing.  
		/// -or-  
		/// The entry has been deleted from the archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to create the new file.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is missing from the archive or is corrupt and cannot be read.  
		///  -or-  
		///  The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive that this entry belongs to has been disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationFileName" /> is in an invalid format.  
		/// -or-  
		/// The zip archive for this entry was opened in <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, which does not permit the retrieval of entries.</exception>
		// Token: 0x0600001A RID: 26 RVA: 0x0000273C File Offset: 0x0000093C
		public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName, bool overwrite)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			FileMode mode = overwrite ? FileMode.Create : FileMode.CreateNew;
			using (Stream stream = new FileStream(destinationFileName, mode, FileAccess.Write, FileShare.None, 4096, false))
			{
				using (Stream stream2 = source.Open())
				{
					stream2.CopyTo(stream);
				}
			}
			File.SetLastWriteTime(destinationFileName, source.LastWriteTime.DateTime);
		}
	}
}
