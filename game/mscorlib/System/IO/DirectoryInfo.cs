using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.IO
{
	/// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	// Token: 0x02000B2F RID: 2863
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryInfo" /> class on the specified path.</summary>
		/// <param name="path">A string specifying the path on which to create the <see langword="DirectoryInfo" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06006715 RID: 26389 RVA: 0x0015FA2D File Offset: 0x0015DC2D
		public DirectoryInfo(string path)
		{
			this.Init(path, Path.GetFullPath(path), null, true);
		}

		// Token: 0x06006716 RID: 26390 RVA: 0x0015FA44 File Offset: 0x0015DC44
		internal DirectoryInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			this.Init(originalPath, fullPath, fileName, isNormalized);
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x0015FA58 File Offset: 0x0015DC58
		private void Init(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("path");
			}
			this.OriginalPath = originalPath;
			fullPath = (fullPath ?? originalPath);
			fullPath = (isNormalized ? fullPath : Path.GetFullPath(fullPath));
			this._name = (fileName ?? (PathInternal.IsRoot(fullPath) ? fullPath : Path.GetFileName(PathInternal.TrimEndingDirectorySeparator(fullPath.AsSpan()))).ToString());
			this.FullPath = fullPath;
		}

		/// <summary>Gets the parent directory of a specified subdirectory.</summary>
		/// <returns>The parent directory, or <see langword="null" /> if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x0015FAD8 File Offset: 0x0015DCD8
		public DirectoryInfo Parent
		{
			get
			{
				string directoryName = Path.GetDirectoryName(PathInternal.IsRoot(this.FullPath) ? this.FullPath : PathInternal.TrimEndingDirectorySeparator(this.FullPath));
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName, null, null, false);
			}
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid <see langword="DirectoryInfo" /> characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.  
		///  -or-  
		///  A file or directory already has the name specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.  
		///  -or-  
		///  The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		// Token: 0x06006719 RID: 26393 RVA: 0x0015FB20 File Offset: 0x0015DD20
		public DirectoryInfo CreateSubdirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.IsEffectivelyEmpty(path))
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			if (Path.IsPathRooted(path))
			{
				throw new ArgumentException("Second path fragment must not be a drive or UNC name.", "path");
			}
			string fullPath = Path.GetFullPath(Path.Combine(this.FullPath, path));
			ReadOnlySpan<char> span = PathInternal.TrimEndingDirectorySeparator(fullPath.AsSpan());
			ReadOnlySpan<char> value = PathInternal.TrimEndingDirectorySeparator(this.FullPath.AsSpan());
			if (span.StartsWith(value, PathInternal.StringComparison) && (span.Length == value.Length || PathInternal.IsDirectorySeparator(fullPath[value.Length])))
			{
				FileSystem.CreateDirectory(fullPath);
				return new DirectoryInfo(fullPath);
			}
			throw new ArgumentException(SR.Format("The directory specified, '{0}', is not a subdirectory of '{1}'.", path, this.FullPath), "path");
		}

		/// <summary>Creates a directory.</summary>
		/// <exception cref="T:System.IO.IOException">The directory cannot be created.</exception>
		// Token: 0x0600671A RID: 26394 RVA: 0x0015FBF9 File Offset: 0x0015DDF9
		public void Create()
		{
			FileSystem.CreateDirectory(this.FullPath);
			base.Invalidate();
		}

		/// <summary>Returns a file list from the current directory.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
		// Token: 0x0600671B RID: 26395 RVA: 0x0015FC0C File Offset: 0x0015DE0C
		public FileInfo[] GetFiles()
		{
			return this.GetFiles("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
		/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600671C RID: 26396 RVA: 0x0015FC1E File Offset: 0x0015DE1E
		public FileInfo[] GetFiles(string searchPattern)
		{
			return this.GetFiles(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns a file list from the current directory matching the given search pattern and using a value to determine whether to search subdirectories.</summary>
		/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600671D RID: 26397 RVA: 0x0015FC2C File Offset: 0x0015DE2C
		public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
		{
			return this.GetFiles(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x0015FC3B File Offset: 0x0015DE3B
		public FileInfo[] GetFiles(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<FileInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Files, enumerationOptions)).ToArray<FileInfo>();
		}

		/// <summary>Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries representing all the files and subdirectories in a directory.</summary>
		/// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x0600671F RID: 26399 RVA: 0x0015FC55 File Offset: 0x0015DE55
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.GetFileSystemInfos("*", EnumerationOptions.Compatible);
		}

		/// <summary>Retrieves an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> objects representing the files and subdirectories that match the specified search criteria.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories and files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of strongly typed <see langword="FileSystemInfo" /> objects matching the search criteria.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006720 RID: 26400 RVA: 0x0015FC67 File Offset: 0x0015DE67
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
		{
			return this.GetFileSystemInfos(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Retrieves an array of <see cref="T:System.IO.FileSystemInfo" /> objects that represent the files and subdirectories matching the specified search criteria.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories and filesa.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An array of file system entries that match the search criteria.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006721 RID: 26401 RVA: 0x0015FC75 File Offset: 0x0015DE75
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			return this.GetFileSystemInfos(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x0015FC84 File Offset: 0x0015DE84
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<FileSystemInfo>();
		}

		/// <summary>Returns the subdirectories of the current directory.</summary>
		/// <returns>An array of <see cref="T:System.IO.DirectoryInfo" /> objects.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006723 RID: 26403 RVA: 0x0015FC99 File Offset: 0x0015DE99
		public DirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of type <see langword="DirectoryInfo" /> matching <paramref name="searchPattern" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see langword="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006724 RID: 26404 RVA: 0x0015FCAB File Offset: 0x0015DEAB
		public DirectoryInfo[] GetDirectories(string searchPattern)
		{
			return this.GetDirectories(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
		/// <returns>An array of type <see langword="DirectoryInfo" /> matching <paramref name="searchPattern" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see langword="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006725 RID: 26405 RVA: 0x0015FCB9 File Offset: 0x0015DEB9
		public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
		{
			return this.GetDirectories(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x0015FCC8 File Offset: 0x0015DEC8
		public DirectoryInfo[] GetDirectories(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return ((IEnumerable<DirectoryInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Directories, enumerationOptions)).ToArray<DirectoryInfo>();
		}

		/// <summary>Returns an enumerable collection of directory information in the current directory.</summary>
		/// <returns>An enumerable collection of directories in the current directory.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006727 RID: 26407 RVA: 0x0015FCE2 File Offset: 0x0015DEE2
		public IEnumerable<DirectoryInfo> EnumerateDirectories()
		{
			return this.EnumerateDirectories("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006728 RID: 26408 RVA: 0x0015FCF4 File Offset: 0x0015DEF4
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
		{
			return this.EnumerateDirectories(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern and search subdirectory option.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006729 RID: 26409 RVA: 0x0015FD02 File Offset: 0x0015DF02
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateDirectories(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x0015FD11 File Offset: 0x0015DF11
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return (IEnumerable<DirectoryInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Directories, enumerationOptions);
		}

		/// <summary>Returns an enumerable collection of file information in the current directory.</summary>
		/// <returns>An enumerable collection of the files in the current directory.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600672B RID: 26411 RVA: 0x0015FD26 File Offset: 0x0015DF26
		public IEnumerable<FileInfo> EnumerateFiles()
		{
			return this.EnumerateFiles("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file information that matches a search pattern.</summary>
		/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600672C RID: 26412 RVA: 0x0015FD38 File Offset: 0x0015DF38
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
		{
			return this.EnumerateFiles(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file information that matches a specified search pattern and search subdirectory option.</summary>
		/// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600672D RID: 26413 RVA: 0x0015FD46 File Offset: 0x0015DF46
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateFiles(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x0015FD55 File Offset: 0x0015DF55
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return (IEnumerable<FileInfo>)DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Files, enumerationOptions);
		}

		/// <summary>Returns an enumerable collection of file system information in the current directory.</summary>
		/// <returns>An enumerable collection of file system information in the current directory.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600672F RID: 26415 RVA: 0x0015FD6A File Offset: 0x0015DF6A
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
		{
			return this.EnumerateFileSystemInfos("*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006730 RID: 26416 RVA: 0x0015FD7C File Offset: 0x0015DF7C
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
		{
			return this.EnumerateFileSystemInfos(searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern and search subdirectory option.</summary>
		/// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006731 RID: 26417 RVA: 0x0015FD8A File Offset: 0x0015DF8A
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			return this.EnumerateFileSystemInfos(searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x0015FD99 File Offset: 0x0015DF99
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, EnumerationOptions enumerationOptions)
		{
			return DirectoryInfo.InternalEnumerateInfos(this.FullPath, searchPattern, SearchTarget.Both, enumerationOptions);
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x0015FDAC File Offset: 0x0015DFAC
		internal static IEnumerable<FileSystemInfo> InternalEnumerateInfos(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.FileInfos(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.DirectoryInfos(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.FileSystemInfos(path, searchPattern, options);
			default:
				throw new ArgumentException("Enum value was out of legal range.", "searchTarget");
			}
		}

		/// <summary>Gets the root portion of the directory.</summary>
		/// <returns>An object that represents the root of the directory.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x0015FE11 File Offset: 0x0015E011
		public DirectoryInfo Root
		{
			get
			{
				return new DirectoryInfo(Path.GetPathRoot(this.FullPath));
			}
		}

		/// <summary>Moves a <see cref="T:System.IO.DirectoryInfo" /> instance and its contents to a new path.</summary>
		/// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destDirName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destDirName" /> is an empty string (''").</exception>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume.  
		///  -or-  
		///  <paramref name="destDirName" /> already exists.  
		///  -or-  
		///  You are not authorized to access this path.  
		///  -or-  
		///  The directory being moved and the destination directory have the same name.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The destination directory cannot be found.</exception>
		// Token: 0x06006735 RID: 26421 RVA: 0x0015FE24 File Offset: 0x0015E024
		public void MoveTo(string destDirName)
		{
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destDirName");
			}
			string fullPath = Path.GetFullPath(destDirName);
			string text = PathInternal.EnsureTrailingSeparator(fullPath);
			string text2 = PathInternal.EnsureTrailingSeparator(this.FullPath);
			if (string.Equals(text2, text, PathInternal.StringComparison))
			{
				throw new IOException("Source and destination path must be different.");
			}
			string pathRoot = Path.GetPathRoot(text2);
			string pathRoot2 = Path.GetPathRoot(text);
			if (!string.Equals(pathRoot, pathRoot2, PathInternal.StringComparison))
			{
				throw new IOException("Source and destination path must have identical roots. Move will not work across volumes.");
			}
			if (!this.Exists && !FileSystem.FileExists(this.FullPath))
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", this.FullPath));
			}
			if (FileSystem.DirectoryExists(fullPath))
			{
				throw new IOException(SR.Format("Cannot create '{0}' because a file or directory with the same name already exists.", text));
			}
			FileSystem.MoveDirectory(this.FullPath, fullPath);
			this.Init(destDirName, text, null, true);
			base.Invalidate();
		}

		/// <summary>Deletes this <see cref="T:System.IO.DirectoryInfo" /> if it is empty.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is not empty.  
		///  -or-  
		///  The directory is the application's current working directory.  
		///  -or-  
		///  There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006736 RID: 26422 RVA: 0x0015FF0D File Offset: 0x0015E10D
		public override void Delete()
		{
			FileSystem.RemoveDirectory(this.FullPath, false);
		}

		/// <summary>Deletes this instance of a <see cref="T:System.IO.DirectoryInfo" />, specifying whether to delete subdirectories and files.</summary>
		/// <param name="recursive">
		///   <see langword="true" /> to delete this directory, its subdirectories, and all files; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is read-only.  
		///  -or-  
		///  The directory contains one or more files or subdirectories and <paramref name="recursive" /> is <see langword="false" />.  
		///  -or-  
		///  The directory is the application's current working directory.  
		///  -or-  
		///  There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06006737 RID: 26423 RVA: 0x0015FF1B File Offset: 0x0015E11B
		public void Delete(bool recursive)
		{
			FileSystem.RemoveDirectory(this.FullPath, recursive);
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x0015FF29 File Offset: 0x0015E129
		private DirectoryInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Creates a directory using a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object.</summary>
		/// <param name="directorySecurity">The access control to apply to the directory.</param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">Creating a directory with only the colon (:) character was attempted.</exception>
		// Token: 0x06006739 RID: 26425 RVA: 0x0015FF33 File Offset: 0x0015E133
		public void Create(DirectorySecurity directorySecurity)
		{
			FileSystem.CreateDirectory(this.FullPath);
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
		/// <param name="directorySecurity">The security to apply.</param>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid <see langword="DirectoryInfo" /> characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.  
		///  -or-  
		///  A file or directory already has the name specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.  
		///  -or-  
		///  The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		// Token: 0x0600673A RID: 26426 RVA: 0x0015FF40 File Offset: 0x0015E140
		public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
		{
			return this.CreateSubdirectory(path);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the directory.</returns>
		/// <exception cref="T:System.SystemException">The directory could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		// Token: 0x0600673B RID: 26427 RVA: 0x0015FF49 File Offset: 0x0015E149
		public DirectorySecurity GetAccessControl()
		{
			return Directory.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.  
		///  Exceptions  
		///   Exception type  
		///
		///   Condition  
		///
		///  <see cref="T:System.SystemException" /> The directory could not be found or modified.  
		///
		///  <see cref="T:System.UnauthorizedAccessException" /> The current process does not have access to open the directory.  
		///
		///  <see cref="T:System.IO.IOException" /> An I/O error occurred while opening the directory.  
		///
		///  <see cref="T:System.PlatformNotSupportedException" /> The current operating system is not Microsoft Windows 2000 or later.  
		///
		///  <see cref="T:System.UnauthorizedAccessException" /> The directory is read-only.  
		///
		///  -or-  
		///
		///  This operation is not supported on the current platform.  
		///
		///  -or-  
		///
		///  The caller does not have the required permission.</returns>
		// Token: 0x0600673C RID: 26428 RVA: 0x0015FF58 File Offset: 0x0015E158
		public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return Directory.GetAccessControl(this.FullPath, includeSections);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <param name="directorySecurity">An object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		// Token: 0x0600673D RID: 26429 RVA: 0x0015FF66 File Offset: 0x0015E166
		public void SetAccessControl(DirectorySecurity directorySecurity)
		{
			Directory.SetAccessControl(this.FullPath, directorySecurity);
		}
	}
}
