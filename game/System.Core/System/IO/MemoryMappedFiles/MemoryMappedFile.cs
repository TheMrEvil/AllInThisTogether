using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents a memory-mapped file. </summary>
	// Token: 0x0200033B RID: 827
	public class MemoryMappedFile : IDisposable
	{
		/// <summary>Creates a memory-mapped file from a file on disk.</summary>
		/// <param name="path">The path to file to map.</param>
		/// <returns>A memory-mapped file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="path" /> is an empty string, contains only white space, or has one or more invalid characters, as defined by the <see cref="M:System.IO.Path.GetInvalidFileNameChars" /> method. -or-
		///         <paramref name="path" /> refers to an invalid device. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///         <paramref name="path" /> exceeds the maximum length defined by the operating system. In Windows, paths must contain fewer than 248 characters, and file names must contain fewer than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions for the file.</exception>
		// Token: 0x060018F3 RID: 6387 RVA: 0x00053E5C File Offset: 0x0005205C
		public static MemoryMappedFile CreateFromFile(string path)
		{
			return MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates a memory-mapped file that has the specified access mode from a file on disk. </summary>
		/// <param name="path">The path to file to map.</param>
		/// <param name="mode">Access mode; must be <see cref="F:System.IO.FileMode.Open" />.</param>
		/// <returns>A memory-mapped file that has the specified access mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="path" /> is an empty string, contains only white space, or has one or more invalid characters, as defined by the <see cref="M:System.IO.Path.GetInvalidFileNameChars" /> method. -or-
		///         <paramref name="path" /> refers to an invalid device.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" />, <see cref="F:System.IO.FileMode.CreateNew" />, or <see cref="F:System.IO.FileMode.Truncate" />.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.OpenOrCreate" /> and the file on disk does not exist.-or-An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///         <paramref name="path" /> exceeds the maximum length defined by the operating system. In Windows, paths must contain fewer than 248 characters, and file names must contain fewer than 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions for the file.</exception>
		// Token: 0x060018F4 RID: 6388 RVA: 0x00053E6C File Offset: 0x0005206C
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode)
		{
			long num = 0L;
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("path");
			}
			if (mode == FileMode.Append)
			{
				throw new ArgumentException("mode");
			}
			IntPtr preexistingHandle = MemoryMapImpl.OpenFile(path, mode, null, out num, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None);
			return new MemoryMappedFile
			{
				handle = new SafeMemoryMappedFileHandle(preexistingHandle, true)
			};
		}

		/// <summary>Creates a memory-mapped file that has the specified access mode and name from a file on disk.</summary>
		/// <param name="path">The path to the file to map.</param>
		/// <param name="mode">Access mode; must be <see cref="F:System.IO.FileMode.Open" />.</param>
		/// <param name="mapName">A name to assign to the memory-mapped file. </param>
		/// <returns>A memory-mapped file that has the specified name and access mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="path" /> is an empty string, contains only white space, or has one or more invalid characters, as defined by the <see cref="M:System.IO.Path.GetInvalidFileNameChars" /> method. -or-
		///         <paramref name="path" /> refers to an invalid device.-or-
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="path" /> or <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" />, <see cref="F:System.IO.FileMode.CreateNew" />, or <see cref="F:System.IO.FileMode.Truncate" />.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.OpenOrCreate" /> and the file on disk does not exist.-or-An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///         <paramref name="path" /> exceeds the maximum length defined by the operating system. In Windows, paths must contain fewer than 248 characters, and file names must contain fewer than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions for the file.</exception>
		// Token: 0x060018F5 RID: 6389 RVA: 0x00053ECB File Offset: 0x000520CB
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName)
		{
			return MemoryMappedFile.CreateFromFile(path, mode, mapName, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates a memory-mapped file that has the specified access mode, name, and capacity from a file on disk.</summary>
		/// <param name="path">The path to the file to map.</param>
		/// <param name="mode">Access mode; can be any of the <see cref="T:System.IO.FileMode" /> enumeration values except <see cref="F:System.IO.FileMode.Append" />.</param>
		/// <param name="mapName">A name to assign to the memory-mapped file. </param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file. Specify 0 to set the capacity to the size of the file on disk.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="path" /> is an empty string, contains only white space, or has one or more invalid characters, as defined by the <see cref="M:System.IO.Path.GetInvalidFileNameChars" /> method. -or-
		///         <paramref name="path" /> refers to an invalid device.-or-
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="path" /> or <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than zero.-or-
		///         <paramref name="capacity" /> is less than the file size (but not zero).-or-
		///         <paramref name="capacity" /> is zero, and the size of the file on disk is also zero.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///         <paramref name="path" /> exceeds the maximum length defined by the operating system. In Windows, paths must contain fewer than 248 characters, and file names must contain fewer than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions for the file.</exception>
		// Token: 0x060018F6 RID: 6390 RVA: 0x00053ED8 File Offset: 0x000520D8
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity)
		{
			return MemoryMappedFile.CreateFromFile(path, mode, mapName, capacity, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates a memory-mapped file that has the specified access mode, name, capacity, and access type from a file on disk.</summary>
		/// <param name="path">The path to the file to map.</param>
		/// <param name="mode">Access mode; can be any of the <see cref="T:System.IO.FileMode" /> enumeration values except <see cref="F:System.IO.FileMode.Append" />.</param>
		/// <param name="mapName">A name to assign to the memory-mapped file. </param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file. Specify 0 to set the capacity to the size of the file on disk.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is not an allowed value.-or-
		///         <paramref name="path" /> specifies an empty file.-or-
		///         <paramref name="access" /> is specified as <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Read" /> and capacity is greater than the size of the file indicated by <paramref name="path" />.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="path" /> or <paramref name="mapName" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than zero.-or-
		///         <paramref name="capacity" /> is less than the file size (but not zero).-or-
		///         <paramref name="capacity" /> is zero, and the size of the file on disk is also zero.-or-
		///         <paramref name="access" /> is not a defined <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> value.-or-The size of the file indicated by <paramref name="path" /> is greater than <paramref name="capacity" />.</exception>
		/// <exception cref="T:System.IO.IOException">-or-An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///         <paramref name="path" /> exceeds the maximum length defined by the operating system. In Windows, paths must contain fewer than 248 characters, and file names must contain fewer than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions for the file.</exception>
		// Token: 0x060018F7 RID: 6391 RVA: 0x00053EE4 File Offset: 0x000520E4
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity, MemoryMappedFileAccess access)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("path");
			}
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException("mapName");
			}
			if (mode == FileMode.Append)
			{
				throw new ArgumentException("mode");
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			IntPtr preexistingHandle = MemoryMapImpl.OpenFile(path, mode, mapName, out capacity, access, MemoryMappedFileOptions.None);
			return new MemoryMappedFile
			{
				handle = new SafeMemoryMappedFileHandle(preexistingHandle, true)
			};
		}

		/// <summary>Creates a memory-mapped file from an existing file with the specified access mode, name, inheritability, and capacity.</summary>
		/// <param name="fileStream">The file stream of the existing file.</param>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file. Specify 0 to set the capacity to the size of <paramref name="filestream" />.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. This parameter can’t be set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" />. </param>
		/// <param name="inheritability">One of the enumeration values that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <param name="leaveOpen">A value that indicates whether to close the source file stream when the <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFile" /> is disposed. </param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is <see langword="null" /> or an empty string.-or-
		///         <paramref name="capacity" /> and the length of the file are zero.-or-
		///         <paramref name="access" /> is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> or <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value, which is not allowed.-or-
		///         <paramref name="access" /> is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Read" /> and <paramref name="capacity" /> is larger than the length of <see langword="filestream" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="fileStream" />  is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than zero.-or-
		///         <paramref name="capacity" /> is less than the file size.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		// Token: 0x060018F8 RID: 6392 RVA: 0x00053F68 File Offset: 0x00052168
		public static MemoryMappedFile CreateFromFile(FileStream fileStream, string mapName, long capacity, MemoryMappedFileAccess access, HandleInheritability inheritability, bool leaveOpen)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException("mapName");
			}
			if ((!MonoUtil.IsUnix && capacity == 0L && fileStream.Length == 0L) || capacity > fileStream.Length)
			{
				throw new ArgumentException("capacity");
			}
			IntPtr preexistingHandle = MemoryMapImpl.OpenHandle(fileStream.SafeFileHandle.DangerousGetHandle(), mapName, out capacity, access, MemoryMappedFileOptions.None);
			MemoryMapImpl.ConfigureHandleInheritability(preexistingHandle, inheritability);
			return new MemoryMappedFile
			{
				handle = new SafeMemoryMappedFileHandle(preexistingHandle, true),
				stream = fileStream,
				keepOpen = leaveOpen
			};
		}

		/// <summary>Creates a memory-mapped file that has the specified name, capacity, access type, security permissions, inheritability, and disposal requirement from a file on disk. </summary>
		/// <param name="fileStream">The <paramref name="fileStream" /> to the file to map.</param>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file. Specify 0 to set the capacity to the size of the file on disk.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. This parameter can’t be set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" />. </param>
		/// <param name="memoryMappedFileSecurity">The permissions that can be granted for file access and operations on memory-mapped files.This parameter can be <see langword="null" />.</param>
		/// <param name="inheritability">One of the enumeration values that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <param name="leaveOpen">
		///       <see langword="true" /> to not dispose <paramref name="fileStream" /> after the <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFile" /> is closed; <see langword="false" /> to dispose <paramref name="fileStream" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="capacity" /> and the length of the file are zero.-or-
		///         <paramref name="access" /> is set to the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Read" /> or <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value, which is not allowed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="fileStream" /> or <paramref name="mapname" />  is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than zero.-or-
		///         <paramref name="capacity" /> is less than the file size.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///         <paramref name="fileStream" /> was closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///         <paramref name="access" /> is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" /> when <paramref name="fileStream" />'s access is set to <see cref="F:System.IO.FileAccess.Read" /> or <see cref="F:System.IO.FileAccess.Write" />. </exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="mapName" /> already exists.</exception>
		// Token: 0x060018F9 RID: 6393 RVA: 0x00054000 File Offset: 0x00052200
		[MonoLimitation("memoryMappedFileSecurity is currently ignored")]
		public static MemoryMappedFile CreateFromFile(FileStream fileStream, string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability, bool leaveOpen)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException("mapName");
			}
			if ((!MonoUtil.IsUnix && capacity == 0L && fileStream.Length == 0L) || capacity > fileStream.Length)
			{
				throw new ArgumentException("capacity");
			}
			IntPtr preexistingHandle = MemoryMapImpl.OpenHandle(fileStream.SafeFileHandle.DangerousGetHandle(), mapName, out capacity, access, MemoryMappedFileOptions.None);
			MemoryMapImpl.ConfigureHandleInheritability(preexistingHandle, inheritability);
			return new MemoryMappedFile
			{
				handle = new SafeMemoryMappedFileHandle(preexistingHandle, true),
				stream = fileStream,
				keepOpen = leaveOpen
			};
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00054098 File Offset: 0x00052298
		private static MemoryMappedFile CoreShmCreate(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability, FileMode mode)
		{
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException("mapName");
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			IntPtr preexistingHandle = MemoryMapImpl.OpenFile(null, mode, mapName, out capacity, access, options);
			return new MemoryMappedFile
			{
				handle = new SafeMemoryMappedFileHandle(preexistingHandle, true)
			};
		}

		/// <summary>Creates a memory-mapped file that has the specified capacity in system memory. </summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <returns>A memory-mapped file that has the specified name and capacity.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than or equal to zero.</exception>
		// Token: 0x060018FB RID: 6395 RVA: 0x000540EB File Offset: 0x000522EB
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateNew(string mapName, long capacity)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		/// <summary>Creates a memory-mapped file that has the specified capacity and access type in system memory. </summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />. </param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		// Token: 0x060018FC RID: 6396 RVA: 0x000540F8 File Offset: 0x000522F8
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, access, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		/// <summary>Creates a memory-mapped file that has the specified name, capacity, access type, memory allocation options and inheritability.</summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <param name="options">A bitwise combination of enumeration values that specifies memory allocation options for the memory-mapped file.</param>
		/// <param name="inheritability">A value that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x060018FD RID: 6397 RVA: 0x00054105 File Offset: 0x00052305
		[MonoLimitation("Named mappings scope is process local; options is ignored")]
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, access, options, null, inheritability);
		}

		/// <summary>Creates a memory-mapped file that has the specified capacity, access type, memory allocation, security permissions, and inheritability in system memory.</summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />. </param>
		/// <param name="options">A bitwise combination of enumeration values that specifies memory allocation options for the memory-mapped file.</param>
		/// <param name="memoryMappedFileSecurity">The permissions that can be granted for file access and operations on memory-mapped files.This parameter can be <see langword="null" />.</param>
		/// <param name="inheritability">One of the enumeration values that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		// Token: 0x060018FE RID: 6398 RVA: 0x00054113 File Offset: 0x00052313
		[MonoLimitation("Named mappings scope is process local; options and memoryMappedFileSecurity are ignored")]
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CoreShmCreate(mapName, capacity, access, options, memoryMappedFileSecurity, inheritability, FileMode.CreateNew);
		}

		/// <summary>Creates or opens a memory-mapped file that has the specified capacity in system memory.</summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <returns>A memory-mapped file that has the specified name and size.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than or equal to zero.</exception>
		// Token: 0x060018FF RID: 6399 RVA: 0x00054123 File Offset: 0x00052323
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates or opens a memory-mapped file that has the specified capacity and access type in system memory. </summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />. </param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The operating system denied the specified access to the file; for example, access is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> or <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />, but the file or directory is read-only. </exception>
		// Token: 0x06001900 RID: 6400 RVA: 0x0005412D File Offset: 0x0005232D
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, access, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		/// <summary>Creates a new empty memory mapped file or opens an existing memory mapped file if one exists with the same name. If opening an existing file, the capacity, options, and memory arguments will be ignored. </summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />. </param>
		/// <param name="options">A bitwise combination of values that indicate the memory allocation options to apply to the file.</param>
		/// <param name="inheritability">A value that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.-or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The operating system denied the specified access to the file; for example, access is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> or <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />, but the file or directory is read-only. </exception>
		// Token: 0x06001901 RID: 6401 RVA: 0x0005413A File Offset: 0x0005233A
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, access, options, null, inheritability);
		}

		/// <summary>Creates or opens a memory-mapped file that has the specified capacity, access type, memory allocation, security permissions, and inheritability in system memory.</summary>
		/// <param name="mapName">A name to assign to the memory-mapped file.</param>
		/// <param name="capacity">The maximum size, in bytes, to allocate to the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />. </param>
		/// <param name="options">A bitwise combination of enumeration values that specifies memory allocation options for the memory-mapped file.</param>
		/// <param name="memoryMappedFileSecurity">The permissions that can be granted for file access and operations on memory-mapped files.This parameter can be <see langword="null" />.</param>
		/// <param name="inheritability">One of the enumeration values that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string. -or-
		///         <paramref name="access" /> is set to write-only with the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> enumeration value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="capacity" /> is greater than the size of the logical address space.-or-
		///         <paramref name="capacity" /> is less than or equal to zero.-or-
		///         <paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The operating system denied the specified <paramref name="access" /> to the file; for example, <paramref name="access" /> is set to <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> or <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />, but the file or directory is read-only. </exception>
		// Token: 0x06001902 RID: 6402 RVA: 0x00054148 File Offset: 0x00052348
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CoreShmCreate(mapName, capacity, access, options, memoryMappedFileSecurity, inheritability, FileMode.OpenOrCreate);
		}

		/// <summary>Opens an existing memory-mapped file that has the specified name in system memory.</summary>
		/// <param name="mapName">The name of the memory-mapped file to open.</param>
		/// <returns>A memory-mapped file that has the specified name. </returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified for <paramref name="mapName" /> does not exist.</exception>
		// Token: 0x06001903 RID: 6403 RVA: 0x00054158 File Offset: 0x00052358
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile OpenExisting(string mapName)
		{
			return MemoryMappedFile.OpenExisting(mapName, MemoryMappedFileRights.ReadWrite);
		}

		/// <summary>Opens an existing memory-mapped file that has the specified name and access rights in system memory.</summary>
		/// <param name="mapName">The name of the memory-mapped file to open.</param>
		/// <param name="desiredAccessRights">One of the enumeration values that specifies the access rights to apply to the memory-mapped file.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="desiredAccessRights" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileRights" /> enumeration value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified for <paramref name="mapName" /> does not exist.</exception>
		// Token: 0x06001904 RID: 6404 RVA: 0x00054161 File Offset: 0x00052361
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights)
		{
			return MemoryMappedFile.OpenExisting(mapName, desiredAccessRights, HandleInheritability.None);
		}

		/// <summary>Opens an existing memory-mapped file that has the specified name, access rights, and inheritability in system memory.</summary>
		/// <param name="mapName">The name of the memory-mapped file to open.</param>
		/// <param name="desiredAccessRights">One of the enumeration values that specifies the access rights to apply to the memory-mapped file.</param>
		/// <param name="inheritability">One of the enumeration values that specifies whether a handle to the memory-mapped file can be inherited by a child process. The default is <see cref="F:System.IO.HandleInheritability.None" />.</param>
		/// <returns>A memory-mapped file that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mapName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="mapName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="desiredAccessRights" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileRights" /> enumeration value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> enumeration value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The requested access is invalid for the memory-mapped file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified for <paramref name="mapName" /> does not exist.</exception>
		// Token: 0x06001905 RID: 6405 RVA: 0x0005416B File Offset: 0x0005236B
		[MonoLimitation("Named mappings scope is process local")]
		public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CoreShmCreate(mapName, 0L, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, null, inheritability, FileMode.Open);
		}

		/// <summary>Creates a stream that maps to a view of the memory-mapped file.  </summary>
		/// <returns>A stream of memory.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
		// Token: 0x06001906 RID: 6406 RVA: 0x0005417A File Offset: 0x0005237A
		public MemoryMappedViewStream CreateViewStream()
		{
			return this.CreateViewStream(0L, 0L);
		}

		/// <summary>Creates a stream that maps to a view of the memory-mapped file, and that has the specified offset and size.</summary>
		/// <param name="offset">The byte at which to start the view.</param>
		/// <param name="size">The size of the view. Specify 0 (zero) to create a view that starts at <paramref name="offset" /> and ends approximately at the end of the memory-mapped file.</param>
		/// <returns>A stream of memory that has the specified offset and size.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="size" /> is a negative value.-or-
		///         <paramref name="size" /> is greater than the logical address space.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="size" /> is greater than the total virtual memory.</exception>
		// Token: 0x06001907 RID: 6407 RVA: 0x00054186 File Offset: 0x00052386
		public MemoryMappedViewStream CreateViewStream(long offset, long size)
		{
			return this.CreateViewStream(offset, size, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates a stream that maps to a view of the memory-mapped file, and that has the specified offset, size, and access type.</summary>
		/// <param name="offset">The byte at which to start the view.</param>
		/// <param name="size">The size of the view. Specify 0 (zero) to create a view that starts at <paramref name="offset" /> and ends approximately at the end of the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <returns>A stream of memory that has the specified characteristics.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="size" /> is a negative value.-or-
		///         <paramref name="size" /> is greater than the logical address space.-or-
		///         <paramref name="access " />is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///         <paramref name="access" /> is invalid for the memory-mapped file.</exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="size" /> is greater than the total virtual memory.-or-
		///         <paramref name="access" /> is invalid for the memory-mapped file.</exception>
		// Token: 0x06001908 RID: 6408 RVA: 0x00054191 File Offset: 0x00052391
		public MemoryMappedViewStream CreateViewStream(long offset, long size, MemoryMappedFileAccess access)
		{
			return new MemoryMappedViewStream(MemoryMappedView.Create(this.handle.DangerousGetHandle(), offset, size, access));
		}

		/// <summary>Creates a <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedViewAccessor" /> that maps to a view of the memory-mapped file.</summary>
		/// <returns>A randomly accessible block of memory.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
		// Token: 0x06001909 RID: 6409 RVA: 0x000541AB File Offset: 0x000523AB
		public MemoryMappedViewAccessor CreateViewAccessor()
		{
			return this.CreateViewAccessor(0L, 0L);
		}

		/// <summary>Creates a <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedViewAccessor" /> that maps to a view of the memory-mapped file, and that has the specified offset and size.</summary>
		/// <param name="offset">The byte at which to start the view.</param>
		/// <param name="size">The size of the view. Specify 0 (zero) to create a view that starts at <paramref name="offset" /> and ends approximately at the end of the memory-mapped file.</param>
		/// <returns>A randomly accessible block of memory.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="size" /> is a negative value.-or-
		///         <paramref name="size" /> is greater than the logical address space.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the memory-mapped file is unauthorized.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x0600190A RID: 6410 RVA: 0x000541B7 File Offset: 0x000523B7
		public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size)
		{
			return this.CreateViewAccessor(offset, size, MemoryMappedFileAccess.ReadWrite);
		}

		/// <summary>Creates a <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedViewAccessor" /> that maps to a view of the memory-mapped file, and that has the specified offset, size, and access restrictions.</summary>
		/// <param name="offset">The byte at which to start the view.</param>
		/// <param name="size">The size of the view. Specify 0 (zero) to create a view that starts at <paramref name="offset" /> and ends approximately at the end of the memory-mapped file.</param>
		/// <param name="access">One of the enumeration values that specifies the type of access allowed to the memory-mapped file. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <returns>A randomly accessible block of memory.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="size" /> is a negative value.-or-
		///         <paramref name="size" /> is greater than the logical address space.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///         <paramref name="access" /> is invalid for the memory-mapped file.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x0600190B RID: 6411 RVA: 0x000541C2 File Offset: 0x000523C2
		public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size, MemoryMappedFileAccess access)
		{
			return new MemoryMappedViewAccessor(MemoryMappedView.Create(this.handle.DangerousGetHandle(), offset, size, access));
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00002162 File Offset: 0x00000362
		private MemoryMappedFile()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFile" />. </summary>
		// Token: 0x0600190D RID: 6413 RVA: 0x000541DC File Offset: 0x000523DC
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFile" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources. </param>
		// Token: 0x0600190E RID: 6414 RVA: 0x000541E8 File Offset: 0x000523E8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				if (!this.keepOpen)
				{
					this.stream.Close();
				}
				this.stream = null;
			}
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		/// <summary>Gets the access control to the memory-mapped file resource.</summary>
		/// <returns>The permissions that can be granted for file access and operations on memory-mapped files.</returns>
		/// <exception cref="T:System.InvalidOperationException">An underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">An underlying call to set security information failed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The memory-mapped file is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is Windows 98 or earlier.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">An underlying call to set security information failed.-or-The memory-mapped file was opened as <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Write" /> only.</exception>
		// Token: 0x0600190F RID: 6415 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public MemoryMappedFileSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the access control to the memory-mapped file resource.</summary>
		/// <param name="memoryMappedFileSecurity">The permissions that can be granted for file access and operations on memory-mapped files.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="memoryMappedFileSecurity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">An underlying call to set security information failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">An underlying call to set security information failed.</exception>
		// Token: 0x06001910 RID: 6416 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public void SetAccessControl(MemoryMappedFileSecurity memoryMappedFileSecurity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the file handle of a memory-mapped file.</summary>
		/// <returns>The handle to the memory-mapped file.</returns>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00054234 File Offset: 0x00052434
		public SafeMemoryMappedFileHandle SafeMemoryMappedFileHandle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005423C File Offset: 0x0005243C
		internal static FileAccess GetFileAccess(MemoryMappedFileAccess access)
		{
			if (access == MemoryMappedFileAccess.Read)
			{
				return FileAccess.Read;
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				return FileAccess.Write;
			}
			if (access == MemoryMappedFileAccess.ReadWrite)
			{
				return FileAccess.ReadWrite;
			}
			if (access == MemoryMappedFileAccess.CopyOnWrite)
			{
				return FileAccess.ReadWrite;
			}
			if (access == MemoryMappedFileAccess.ReadExecute)
			{
				return FileAccess.Read;
			}
			if (access == MemoryMappedFileAccess.ReadWriteExecute)
			{
				return FileAccess.ReadWrite;
			}
			throw new ArgumentOutOfRangeException("access");
		}

		// Token: 0x04000C08 RID: 3080
		private FileStream stream;

		// Token: 0x04000C09 RID: 3081
		private bool keepOpen;

		// Token: 0x04000C0A RID: 3082
		private SafeMemoryMappedFileHandle handle;
	}
}
