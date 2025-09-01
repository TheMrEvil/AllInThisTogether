using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Security.AccessControl;

namespace System.IO
{
	/// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	// Token: 0x02000B2E RID: 2862
	public static class Directory
	{
		/// <summary>Retrieves the parent directory of the specified path, including both absolute and relative paths.</summary>
		/// <param name="path">The path for which to retrieve the parent directory.</param>
		/// <returns>The parent directory, or <see langword="null" /> if <paramref name="path" /> is the root directory, including the root of a UNC server or share name.</returns>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException" /> topic.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">.NET Framework only: The caller does not have the required permissions.</exception>
		// Token: 0x060066DF RID: 26335 RVA: 0x0015F454 File Offset: 0x0015D654
		public static DirectoryInfo GetParent(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(path));
			if (directoryName == null)
			{
				return null;
			}
			return new DirectoryInfo(directoryName);
		}

		/// <summary>Creates all directories and subdirectories in the specified path unless they already exist.</summary>
		/// <param name="path">The directory to create.</param>
		/// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.  
		///  -or-  
		///  The network name is not known.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-  
		/// <paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		// Token: 0x060066E0 RID: 26336 RVA: 0x0015F49E File Offset: 0x0015D69E
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			string fullPath = Path.GetFullPath(path);
			FileSystem.CreateDirectory(fullPath);
			return new DirectoryInfo(fullPath, null, null, false);
		}

		/// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
		/// <param name="path">The path to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> refers to an existing directory; <see langword="false" /> if the directory does not exist or an error occurs when trying to determine if the specified directory exists.</returns>
		// Token: 0x060066E1 RID: 26337 RVA: 0x0015F4DC File Offset: 0x0015D6DC
		public static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				return FileSystem.DirectoryExists(Path.GetFullPath(path));
			}
			catch (ArgumentException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		/// <summary>Sets the creation date and time for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information.</param>
		/// <param name="creationTime">The date and time the file or directory was last written to. This value is expressed in local time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		// Token: 0x060066E2 RID: 26338 RVA: 0x0015F540 File Offset: 0x0015D740
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), creationTime, true);
		}

		/// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information.</param>
		/// <param name="creationTimeUtc">The date and time the directory or file was created. This value is expressed in local time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		// Token: 0x060066E3 RID: 26339 RVA: 0x0015F554 File Offset: 0x0015D754
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			FileSystem.SetCreationTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(creationTimeUtc), true);
		}

		/// <summary>Gets the creation date and time of a directory.</summary>
		/// <param name="path">The path of the directory.</param>
		/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x060066E4 RID: 26340 RVA: 0x0015F568 File Offset: 0x0015D768
		public static DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		/// <summary>Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.</summary>
		/// <param name="path">The path of the directory.</param>
		/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x060066E5 RID: 26341 RVA: 0x0015F570 File Offset: 0x0015D770
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);
		}

		/// <summary>Sets the date and time a directory was last written to.</summary>
		/// <param name="path">The path of the directory.</param>
		/// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060066E6 RID: 26342 RVA: 0x0015F578 File Offset: 0x0015D778
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), lastWriteTime, true);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
		/// <param name="path">The path of the directory.</param>
		/// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> was not found (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060066E7 RID: 26343 RVA: 0x0015F58C File Offset: 0x0015D78C
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			FileSystem.SetLastWriteTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastWriteTimeUtc), true);
		}

		/// <summary>Returns the date and time the specified file or directory was last written to.</summary>
		/// <param name="path">The file or directory for which to obtain modification date and time information.</param>
		/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x060066E8 RID: 26344 RVA: 0x0015F5A0 File Offset: 0x0015D7A0
		public static DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.</summary>
		/// <param name="path">The file or directory for which to obtain modification date and time information.</param>
		/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x060066E9 RID: 26345 RVA: 0x0015F5A8 File Offset: 0x0015D7A8
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		/// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information.</param>
		/// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060066EA RID: 26346 RVA: 0x0015F5B0 File Offset: 0x0015D7B0
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), lastAccessTime, true);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information.</param>
		/// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060066EB RID: 26347 RVA: 0x0015F5C4 File Offset: 0x0015D7C4
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			FileSystem.SetLastAccessTime(Path.GetFullPath(path), File.GetUtcDateTimeOffset(lastAccessTimeUtc), true);
		}

		/// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to obtain access date and time information.</param>
		/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format.</exception>
		// Token: 0x060066EC RID: 26348 RVA: 0x0015F5D8 File Offset: 0x0015D7D8
		public static DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to obtain access date and time information.</param>
		/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format.</exception>
		// Token: 0x060066ED RID: 26349 RVA: 0x0015F5E0 File Offset: 0x0015D7E0
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		/// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An array of the full names (including paths) for the files in the specified directory, or an empty array if no files are found.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.  
		/// -or-  
		/// A network error has occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066EE RID: 26350 RVA: 0x0015F5E8 File Offset: 0x0015D7E8
		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, or an empty array if no files are found.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.  
		/// -or-  
		/// A network error has occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> doesn't contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066EF RID: 26351 RVA: 0x0015F5FA File Offset: 0x0015D7FA
		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchpattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.  
		/// -or-  
		/// A network error has occurred.</exception>
		// Token: 0x060066F0 RID: 26352 RVA: 0x0015F608 File Offset: 0x0015D808
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFiles(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x0015F617 File Offset: 0x0015D817
		public static string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Files, enumerationOptions).ToArray<string>();
		}

		/// <summary>Returns the names of subdirectories (including their paths) in the specified directory.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An array of the full names (including paths) of subdirectories in the specified path, or an empty array if no directories are found.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066F2 RID: 26354 RVA: 0x0015F627 File Offset: 0x0015D827
		public static string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern in the specified directory, or an empty array if no directories are found.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> doesn't contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066F3 RID: 26355 RVA: 0x0015F639 File Offset: 0x0015D839
		public static string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetDirectories(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns the names of the subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
		/// <returns>An array of the full names (including paths) of the subdirectories that match the specified criteria, or an empty array if no directories are found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066F4 RID: 26356 RVA: 0x0015F647 File Offset: 0x0015D847
		public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetDirectories(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x0015F656 File Offset: 0x0015D856
		public static string[] GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions).ToArray<string>();
		}

		/// <summary>Returns the names of all files and subdirectories in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An array of the names of files and subdirectories in the specified directory, or an empty array if no files or subdirectories are found.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066F6 RID: 26358 RVA: 0x0015F666 File Offset: 0x0015D866
		public static string[] GetFileSystemEntries(string path)
		{
			return Directory.GetFileSystemEntries(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an array of file names and directory names that match a search pattern in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of file and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An array of file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x060066F7 RID: 26359 RVA: 0x0015F678 File Offset: 0x0015D878
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of files and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
		///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An array of file the file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060066F8 RID: 26360 RVA: 0x0015F686 File Offset: 0x0015D886
		public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x0015F695 File Offset: 0x0015D895
		public static string[] GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Both, enumerationOptions).ToArray<string>();
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x0015F6A8 File Offset: 0x0015D8A8
		internal static IEnumerable<string> InternalEnumeratePaths(string path, string searchPattern, SearchTarget searchTarget, EnumerationOptions options)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			FileSystemEnumerableFactory.NormalizeInputs(ref path, ref searchPattern, options);
			switch (searchTarget)
			{
			case SearchTarget.Files:
				return FileSystemEnumerableFactory.UserFiles(path, searchPattern, options);
			case SearchTarget.Directories:
				return FileSystemEnumerableFactory.UserDirectories(path, searchPattern, options);
			case SearchTarget.Both:
				return FileSystemEnumerableFactory.UserEntries(path, searchPattern, options);
			default:
				throw new ArgumentOutOfRangeException("searchTarget");
			}
		}

		/// <summary>Returns an enumerable collection of directory names in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060066FB RID: 26363 RVA: 0x0015F716 File Offset: 0x0015D916
		public static IEnumerable<string> EnumerateDirectories(string path)
		{
			return Directory.EnumerateDirectories(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060066FC RID: 26364 RVA: 0x0015F728 File Offset: 0x0015D928
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
		{
			return Directory.EnumerateDirectories(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
		///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060066FD RID: 26365 RVA: 0x0015F736 File Offset: 0x0015D936
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateDirectories(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x0015F745 File Offset: 0x0015D945
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Directories, enumerationOptions);
		}

		/// <summary>Returns an enumerable collection of file names in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060066FF RID: 26367 RVA: 0x0015F750 File Offset: 0x0015D950
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			return Directory.EnumerateFiles(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006700 RID: 26368 RVA: 0x0015F762 File Offset: 0x0015D962
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
		{
			return Directory.EnumerateFiles(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.  
		///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006701 RID: 26369 RVA: 0x0015F770 File Offset: 0x0015D970
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFiles(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x0015F77F File Offset: 0x0015D97F
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Files, enumerationOptions);
		}

		/// <summary>Returns an enumerable collection of file names and directory names in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006703 RID: 26371 RVA: 0x0015F78A File Offset: 0x0015D98A
		public static IEnumerable<string> EnumerateFileSystemEntries(string path)
		{
			return Directory.EnumerateFileSystemEntries(path, "*", EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file names and directory names that  match a search pattern in a specified path.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against the names of file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006704 RID: 26372 RVA: 0x0015F79C File Offset: 0x0015D99C
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, EnumerationOptions.Compatible);
		}

		/// <summary>Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
		/// <param name="searchPattern">The search string to match against file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
		/// <param name="searchOption">One of the enumeration values  that specifies whether the search operation should include only the current directory or should include all subdirectories.  
		///  The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-
		///  <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06006705 RID: 26373 RVA: 0x0015F7AA File Offset: 0x0015D9AA
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemEntries(path, searchPattern, EnumerationOptions.FromSearchOption(searchOption));
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x0015F7B9 File Offset: 0x0015D9B9
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)
		{
			return Directory.InternalEnumeratePaths(path, searchPattern, SearchTarget.Both, enumerationOptions);
		}

		/// <summary>Returns the volume information, root information, or both for the specified path.</summary>
		/// <param name="path">The path of a file or directory.</param>
		/// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06006707 RID: 26375 RVA: 0x0015F7C4 File Offset: 0x0015D9C4
		public static string GetDirectoryRoot(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPath = Path.GetFullPath(path);
			return fullPath.Substring(0, PathInternal.GetRootLength(fullPath));
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0015F7F8 File Offset: 0x0015D9F8
		internal static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, PathInternal.GetRootLength(path));
		}

		/// <summary>Gets the current working directory of the application.</summary>
		/// <returns>A string that contains the absolute path of the current working directory, and does not end with a backslash (\).</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows CE, which does not have current directory functionality.  
		///  This method is available in the .NET Compact Framework, but is not currently supported.</exception>
		// Token: 0x06006709 RID: 26377 RVA: 0x0015F811 File Offset: 0x0015DA11
		public static string GetCurrentDirectory()
		{
			return Environment.CurrentDirectory;
		}

		/// <summary>Sets the application's current working directory to the specified directory.</summary>
		/// <param name="path">The path to which the current working directory is set.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to access unmanaged code.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory was not found.</exception>
		// Token: 0x0600670A RID: 26378 RVA: 0x0015F818 File Offset: 0x0015DA18
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot be the empty string or all whitespace.", "path");
			}
			Environment.CurrentDirectory = Path.GetFullPath(path);
		}

		/// <summary>Moves a file or a directory and its contents to a new location.</summary>
		/// <param name="sourceDirName">The path of the file or directory to move.</param>
		/// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume.  
		///  -or-  
		///  <paramref name="destDirName" /> already exists.  
		///  -or-  
		///  The <paramref name="sourceDirName" /> and <paramref name="destDirName" /> parameters refer to the same file or directory.  
		///  -or-  
		///  The directory or a file within it is being used by another process.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified by <paramref name="sourceDirName" /> is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x0600670B RID: 26379 RVA: 0x0015F84C File Offset: 0x0015DA4C
		public static void Move(string sourceDirName, string destDirName)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (sourceDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destDirName");
			}
			string fullPath = Path.GetFullPath(sourceDirName);
			string text = PathInternal.EnsureTrailingSeparator(fullPath);
			string fullPath2 = Path.GetFullPath(destDirName);
			string text2 = PathInternal.EnsureTrailingSeparator(fullPath2);
			StringComparison stringComparison = PathInternal.StringComparison;
			if (string.Equals(text, text2, stringComparison))
			{
				throw new IOException("Source and destination path must be different.");
			}
			string pathRoot = Path.GetPathRoot(text);
			string pathRoot2 = Path.GetPathRoot(text2);
			if (!string.Equals(pathRoot, pathRoot2, stringComparison))
			{
				throw new IOException("Source and destination path must have identical roots. Move will not work across volumes.");
			}
			if (!FileSystem.DirectoryExists(fullPath) && !FileSystem.FileExists(fullPath))
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", fullPath));
			}
			if (FileSystem.DirectoryExists(fullPath2))
			{
				throw new IOException(SR.Format("Cannot create '{0}' because a file or directory with the same name already exists.", fullPath2));
			}
			FileSystem.MoveDirectory(fullPath, fullPath2);
		}

		/// <summary>Deletes an empty directory from a specified path.</summary>
		/// <param name="path">The name of the empty directory to remove. This directory must be writable and empty.</param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.  
		///  -or-  
		///  The directory is the application's current working directory.  
		///  -or-  
		///  The directory specified by <paramref name="path" /> is not empty.  
		///  -or-  
		///  The directory is read-only or contains a read-only file.  
		///  -or-  
		///  The directory is being used by another process.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.  
		/// -or-  
		/// The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x0600670C RID: 26380 RVA: 0x0015F93E File Offset: 0x0015DB3E
		public static void Delete(string path)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), false);
		}

		/// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory.</summary>
		/// <param name="path">The name of the directory to remove.</param>
		/// <param name="recursive">
		///   <see langword="true" /> to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.  
		///  -or-  
		///  The directory specified by <paramref name="path" /> is read-only, or <paramref name="recursive" /> is <see langword="false" /> and <paramref name="path" /> is not an empty directory.  
		///  -or-  
		///  The directory is the application's current working directory.  
		///  -or-  
		///  The directory contains a read-only file.  
		///  -or-  
		///  The directory is being used by another process.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.  
		/// -or-  
		/// The specified path is invalid (for example, it is on an unmapped drive).</exception>
		// Token: 0x0600670D RID: 26381 RVA: 0x0015F94C File Offset: 0x0015DB4C
		public static void Delete(string path, bool recursive)
		{
			FileSystem.RemoveDirectory(Path.GetFullPath(path), recursive);
		}

		/// <summary>Retrieves the names of the logical drives on this computer in the form "&lt;drive letter&gt;:\".</summary>
		/// <returns>The logical drives on this computer.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occured (for example, a disk error).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x0600670E RID: 26382 RVA: 0x0015F95A File Offset: 0x0015DB5A
		public static string[] GetLogicalDrives()
		{
			return FileSystem.GetLogicalDrives();
		}

		/// <summary>Creates all the directories in the specified path, unless the already exist, applying the specified Windows security.</summary>
		/// <param name="path">The directory to create.</param>
		/// <param name="directorySecurity">The access control to apply to the directory.</param>
		/// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.  
		///  -or-  
		///  The network name is not known.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.  
		/// -or-  
		/// <paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		// Token: 0x0600670F RID: 26383 RVA: 0x0015F961 File Offset: 0x0015DB61
		public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
		{
			return Directory.CreateDirectory(path);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for a specified directory.</summary>
		/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x06006710 RID: 26384 RVA: 0x0015F969 File Offset: 0x0015DB69
		public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new DirectorySecurity(path, includeSections);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for a specified directory.</summary>
		/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x06006711 RID: 26385 RVA: 0x0015F972 File Offset: 0x0015DB72
		public static DirectorySecurity GetAccessControl(string path)
		{
			return Directory.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the specified directory.</summary>
		/// <param name="path">A directory to add or remove access control list (ACL) entries from.</param>
		/// <param name="directorySecurity">A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> was invalid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to the directory specified by <paramref name="path" />.  
		///  -or-  
		///  The current process does not have sufficient privilege to set the ACL entry.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		// Token: 0x06006712 RID: 26386 RVA: 0x0015F97C File Offset: 0x0015DB7C
		public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			string fullPath = Path.GetFullPath(path);
			directorySecurity.PersistModifications(fullPath);
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x0015F9A8 File Offset: 0x0015DBA8
		internal static string InsecureGetCurrentDirectory()
		{
			MonoIOError monoIOError;
			string currentDirectory = MonoIO.GetCurrentDirectory(out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			return currentDirectory;
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x0015F9C8 File Offset: 0x0015DBC8
		internal static void InsecureSetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("path string must not be an empty string or whitespace string");
			}
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
			}
			MonoIOError monoIOError;
			MonoIO.SetCurrentDirectory(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}
	}
}
