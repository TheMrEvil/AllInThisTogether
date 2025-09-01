using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;

namespace System.IO
{
	/// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000B39 RID: 2873
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		// Token: 0x060067B6 RID: 26550 RVA: 0x00161FEE File Offset: 0x001601EE
		private FileInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileInfo" /> class, which acts as a wrapper for a file path.</summary>
		/// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="fileName" /> contains a colon (:) in the middle of the string.</exception>
		// Token: 0x060067B7 RID: 26551 RVA: 0x00161FF6 File Offset: 0x001601F6
		public FileInfo(string fileName) : this(fileName, null, null, false)
		{
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x00162004 File Offset: 0x00160204
		internal FileInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.OriginalPath = originalPath;
			fullPath = (fullPath ?? originalPath);
			this.FullPath = (isNormalized ? (fullPath ?? originalPath) : Path.GetFullPath(fullPath));
			this._name = (fileName ?? Path.GetFileName(originalPath));
		}

		/// <summary>Gets the size, in bytes, of the current file.</summary>
		/// <returns>The size of the current file in bytes.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.  
		///  -or-  
		///  The <see langword="Length" /> property is called for a directory.</exception>
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x0016205E File Offset: 0x0016025E
		public long Length
		{
			get
			{
				if ((base.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
				{
					throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", this.FullPath), this.FullPath);
				}
				return base.LengthCore;
			}
		}

		/// <summary>Gets a string representing the directory's full path.</summary>
		/// <returns>A string representing the directory's full path.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see langword="null" /> was passed in for the directory name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060067BA RID: 26554 RVA: 0x0016208F File Offset: 0x0016028F
		public string DirectoryName
		{
			get
			{
				return Path.GetDirectoryName(this.FullPath);
			}
		}

		/// <summary>Gets an instance of the parent directory.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object representing the parent directory of this file.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060067BB RID: 26555 RVA: 0x0016209C File Offset: 0x0016029C
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		/// <summary>Gets or sets a value that determines if the current file is read only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current file is read only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The user does not have write permission, but attempted to set this property to <see langword="false" />.</exception>
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060067BC RID: 26556 RVA: 0x001620BB File Offset: 0x001602BB
		// (set) Token: 0x060067BD RID: 26557 RVA: 0x001620C8 File Offset: 0x001602C8
		public bool IsReadOnly
		{
			get
			{
				return (base.Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
			}
			set
			{
				if (value)
				{
					base.Attributes |= FileAttributes.ReadOnly;
					return;
				}
				base.Attributes &= ~FileAttributes.ReadOnly;
			}
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamReader" /> with UTF8 encoding that reads from an existing text file.</summary>
		/// <returns>A new <see langword="StreamReader" /> with UTF8 encoding.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		// Token: 0x060067BE RID: 26558 RVA: 0x001620EB File Offset: 0x001602EB
		public StreamReader OpenText()
		{
			return new StreamReader(base.NormalizedPath, Encoding.UTF8, true);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that writes a new text file.</summary>
		/// <returns>A new <see langword="StreamWriter" />.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The file name is a directory.</exception>
		/// <exception cref="T:System.IO.IOException">The disk is read-only.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060067BF RID: 26559 RVA: 0x001620FE File Offset: 0x001602FE
		public StreamWriter CreateText()
		{
			return new StreamWriter(base.NormalizedPath, false);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="T:System.IO.FileInfo" />.</summary>
		/// <returns>A new <see langword="StreamWriter" />.</returns>
		// Token: 0x060067C0 RID: 26560 RVA: 0x0016210C File Offset: 0x0016030C
		public StreamWriter AppendText()
		{
			return new StreamWriter(base.NormalizedPath, true);
		}

		/// <summary>Copies an existing file to a new file, disallowing the overwriting of an existing file.</summary>
		/// <param name="destFileName">The name of the new file to copy to.</param>
		/// <returns>A new file with a fully qualified path.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) within the string but does not specify the volume.</exception>
		// Token: 0x060067C1 RID: 26561 RVA: 0x0016211A File Offset: 0x0016031A
		public FileInfo CopyTo(string destFileName)
		{
			return this.CopyTo(destFileName, false);
		}

		/// <summary>Copies an existing file to a new file, allowing the overwriting of an existing file.</summary>
		/// <param name="destFileName">The name of the new file to copy to.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> to allow an existing file to be overwritten; otherwise, <see langword="false" />.</param>
		/// <returns>A new file, or an overwrite of an existing file if <paramref name="overwrite" /> is <see langword="true" />. If the file exists and <paramref name="overwrite" /> is <see langword="false" />, an <see cref="T:System.IO.IOException" /> is thrown.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists and <paramref name="overwrite" /> is <see langword="false" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
		// Token: 0x060067C2 RID: 26562 RVA: 0x00162124 File Offset: 0x00160324
		public FileInfo CopyTo(string destFileName, bool overwrite)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(destFileName);
			FileSystem.CopyFile(this.FullPath, fullPath, overwrite);
			return new FileInfo(fullPath, null, null, true);
		}

		/// <summary>Creates a file.</summary>
		/// <returns>A new file.</returns>
		// Token: 0x060067C3 RID: 26563 RVA: 0x00162179 File Offset: 0x00160379
		public FileStream Create()
		{
			return File.Create(base.NormalizedPath);
		}

		/// <summary>Permanently deletes a file.</summary>
		/// <exception cref="T:System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.  
		///  -or-  
		///  There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The path is a directory.</exception>
		// Token: 0x060067C4 RID: 26564 RVA: 0x00162186 File Offset: 0x00160386
		public override void Delete()
		{
			FileSystem.DeleteFile(this.FullPath);
		}

		/// <summary>Opens a file in the specified mode.</summary>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, <see langword="Open" /> or <see langword="Append" />) in which to open the file.</param>
		/// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The file is already open.</exception>
		// Token: 0x060067C5 RID: 26565 RVA: 0x00162193 File Offset: 0x00160393
		public FileStream Open(FileMode mode)
		{
			return this.Open(mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>Opens a file in the specified mode with read, write, or read/write access.</summary>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, <see langword="Open" /> or <see langword="Append" />) in which to open the file.</param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with <see langword="Read" />, <see langword="Write" />, or <see langword="ReadWrite" /> file access.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> object opened in the specified mode and access, and unshared.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The file is already open.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty or contains only white spaces.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more arguments is null.</exception>
		// Token: 0x060067C6 RID: 26566 RVA: 0x001621A5 File Offset: 0x001603A5
		public FileStream Open(FileMode mode, FileAccess access)
		{
			return this.Open(mode, access, FileShare.None);
		}

		/// <summary>Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.</summary>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, <see langword="Open" /> or <see langword="Append" />) in which to open the file.</param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with <see langword="Read" />, <see langword="Write" />, or <see langword="ReadWrite" /> file access.</param>
		/// <param name="share">A <see cref="T:System.IO.FileShare" /> constant specifying the type of access other <see langword="FileStream" /> objects have to this file.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> object opened with the specified mode, access, and sharing options.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The file is already open.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty or contains only white spaces.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more arguments is null.</exception>
		// Token: 0x060067C7 RID: 26567 RVA: 0x001621B0 File Offset: 0x001603B0
		public FileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(base.NormalizedPath, mode, access, share);
		}

		/// <summary>Creates a read-only <see cref="T:System.IO.FileStream" />.</summary>
		/// <returns>A new read-only <see cref="T:System.IO.FileStream" /> object.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The file is already open.</exception>
		// Token: 0x060067C8 RID: 26568 RVA: 0x001621C0 File Offset: 0x001603C0
		public FileStream OpenRead()
		{
			return new FileStream(base.NormalizedPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		/// <summary>Creates a write-only <see cref="T:System.IO.FileStream" />.</summary>
		/// <returns>A write-only unshared <see cref="T:System.IO.FileStream" /> object for a new or existing file.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is invalid, such as being on an unmapped drive.</exception>
		// Token: 0x060067C9 RID: 26569 RVA: 0x001621D6 File Offset: 0x001603D6
		public FileStream OpenWrite()
		{
			return new FileStream(base.NormalizedPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		/// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
		/// <param name="destFileName">The path to move the file to, which can specify a different file name.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the destination file already exists or the destination device is not ready.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="destFileName" /> is read-only or is a directory.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
		// Token: 0x060067CA RID: 26570 RVA: 0x001621E8 File Offset: 0x001603E8
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(destFileName);
			if (!new DirectoryInfo(Path.GetDirectoryName(this.FullName)).Exists)
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", this.FullName));
			}
			if (!this.Exists)
			{
				throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", this.FullName), this.FullName);
			}
			FileSystem.MoveFile(this.FullPath, fullPath);
			this.FullPath = fullPath;
			this.OriginalPath = destFileName;
			this._name = Path.GetFileName(fullPath);
			base.Invalidate();
		}

		/// <summary>Replaces the contents of a specified file with the file described by the current <see cref="T:System.IO.FileInfo" /> object, deleting the original file, and creating a backup of the replaced file.</summary>
		/// <param name="destinationFileName">The name of a file to replace with the current file.</param>
		/// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destFileName" /> parameter.</param>
		/// <returns>A <see cref="T:System.IO.FileInfo" /> object that encapsulates information about the file described by the <paramref name="destFileName" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destFileName" /> parameter was not of a legal form.  
		///  -or-  
		///  The path described by the <paramref name="destBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destFileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.  
		///  -or-  
		///  The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		// Token: 0x060067CB RID: 26571 RVA: 0x0016229F File Offset: 0x0016049F
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			return this.Replace(destinationFileName, destinationBackupFileName, false);
		}

		/// <summary>Replaces the contents of a specified file with the file described by the current <see cref="T:System.IO.FileInfo" /> object, deleting the original file, and creating a backup of the replaced file.  Also specifies whether to ignore merge errors.</summary>
		/// <param name="destinationFileName">The name of a file to replace with the current file.</param>
		/// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destFileName" /> parameter.</param>
		/// <param name="ignoreMetadataErrors">
		///   <see langword="true" /> to ignore merge errors (such as attributes and ACLs) from the replaced file to the replacement file; otherwise <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.IO.FileInfo" /> object that encapsulates information about the file described by the <paramref name="destFileName" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destFileName" /> parameter was not of a legal form.  
		///  -or-  
		///  The path described by the <paramref name="destBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destFileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.  
		///  -or-  
		///  The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		// Token: 0x060067CC RID: 26572 RVA: 0x001622AA File Offset: 0x001604AA
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			FileSystem.ReplaceFile(this.FullPath, Path.GetFullPath(destinationFileName), (destinationBackupFileName != null) ? Path.GetFullPath(destinationBackupFileName) : null, ignoreMetadataErrors);
			return new FileInfo(destinationFileName);
		}

		/// <summary>Decrypts a file that was encrypted by the current account using the <see cref="M:System.IO.FileInfo.Encrypt" /> method.</summary>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file described by the current <see cref="T:System.IO.FileInfo" /> object is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060067CD RID: 26573 RVA: 0x001622DE File Offset: 0x001604DE
		public void Decrypt()
		{
			File.Decrypt(this.FullPath);
		}

		/// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file described by the current <see cref="T:System.IO.FileInfo" /> object is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060067CE RID: 26574 RVA: 0x001622EB File Offset: 0x001604EB
		public void Encrypt()
		{
			File.Encrypt(this.FullPath);
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x0015FF29 File Offset: 0x0015E129
		private FileInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the current file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">The current system account does not have administrative privileges.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060067D0 RID: 26576 RVA: 0x001622F8 File Offset: 0x001604F8
		public FileSecurity GetAccessControl()
		{
			return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies which group of access control entries to retrieve.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the current file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">The current system account does not have administrative privileges.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060067D1 RID: 26577 RVA: 0x00162307 File Offset: 0x00160507
		public FileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return File.GetAccessControl(this.FullPath, includeSections);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <param name="fileSecurity">A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that describes an access control list (ACL) entry to apply to the current file.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		// Token: 0x060067D2 RID: 26578 RVA: 0x00162315 File Offset: 0x00160515
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			File.SetAccessControl(this.FullPath, fileSecurity);
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x00162323 File Offset: 0x00160523
		internal FileInfo(string fullPath, bool ignoreThis)
		{
			this._name = Path.GetFileName(fullPath);
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
		}

		/// <summary>Gets the name of the file.</summary>
		/// <returns>The name of the file.</returns>
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x0016234A File Offset: 0x0016054A
		public override string Name
		{
			get
			{
				return this._name;
			}
		}
	}
}
