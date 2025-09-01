using System;
using System.Runtime.CompilerServices;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x0200033A RID: 826
	internal static class MemoryMapImpl
	{
		// Token: 0x060018E6 RID: 6374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenFileInternal(char* path, int path_length, FileMode mode, char* mapName, int mapName_length, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, out int error);

		// Token: 0x060018E7 RID: 6375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenHandleInternal(IntPtr handle, char* mapName, int mapName_length, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, out int error);

		// Token: 0x060018E8 RID: 6376
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CloseMapping(IntPtr handle);

		// Token: 0x060018E9 RID: 6377
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Flush(IntPtr file_handle);

		// Token: 0x060018EA RID: 6378
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConfigureHandleInheritability(IntPtr handle, HandleInheritability inheritability);

		// Token: 0x060018EB RID: 6379
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Unmap(IntPtr mmap_handle);

		// Token: 0x060018EC RID: 6380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MapInternal(IntPtr handle, long offset, ref long size, MemoryMappedFileAccess access, out IntPtr mmap_handle, out IntPtr base_address);

		// Token: 0x060018ED RID: 6381 RVA: 0x00053C78 File Offset: 0x00051E78
		internal static void Map(IntPtr handle, long offset, ref long size, MemoryMappedFileAccess access, out IntPtr mmap_handle, out IntPtr base_address)
		{
			int num = MemoryMapImpl.MapInternal(handle, offset, ref size, access, out mmap_handle, out base_address);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, "<none>");
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00053CA4 File Offset: 0x00051EA4
		private static Exception CreateException(int error, string path)
		{
			switch (error)
			{
			case 1:
				return new ArgumentException("A positive capacity must be specified for a Memory Mapped File backed by an empty file.");
			case 2:
				return new ArgumentOutOfRangeException("capacity", "The capacity may not be smaller than the file size.");
			case 3:
				return new FileNotFoundException(path);
			case 4:
				return new IOException("The file already exists");
			case 5:
				return new PathTooLongException();
			case 6:
				return new IOException("Could not open file");
			case 7:
				return new ArgumentException("Capacity must be bigger than zero for non-file mappings");
			case 8:
				return new ArgumentException("Invalid FileMode value.");
			case 9:
				return new IOException("Could not map file");
			case 10:
				return new UnauthorizedAccessException("Access to the path is denied.");
			case 11:
				return new ArgumentOutOfRangeException("capacity", "The capacity cannot be greater than the size of the system's logical address space.");
			default:
				return new IOException("Failed with unknown error code " + error.ToString());
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00053D77 File Offset: 0x00051F77
		private static int StringLength(string a)
		{
			if (a == null)
			{
				return 0;
			}
			return a.Length;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00053D84 File Offset: 0x00051F84
		private static void CheckString(string name, string value)
		{
			if (value != null && value.IndexOf('\0') >= 0)
			{
				throw new ArgumentException("String must not contain embedded NULs.", name);
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00053DA0 File Offset: 0x00051FA0
		internal unsafe static IntPtr OpenFile(string path, FileMode mode, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options)
		{
			MemoryMapImpl.CheckString("path", path);
			MemoryMapImpl.CheckString("mapName", mapName);
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = mapName;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 0;
			IntPtr result = MemoryMapImpl.OpenFileInternal(ptr, MemoryMapImpl.StringLength(path), mode, ptr2, MemoryMapImpl.StringLength(mapName), out capacity, access, options, out num);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, path);
			}
			return result;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00053E10 File Offset: 0x00052010
		internal unsafe static IntPtr OpenHandle(IntPtr handle, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options)
		{
			MemoryMapImpl.CheckString("mapName", mapName);
			char* ptr = mapName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 0;
			IntPtr result = MemoryMapImpl.OpenHandleInternal(handle, ptr, MemoryMapImpl.StringLength(mapName), out capacity, access, options, out num);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, "<none>");
			}
			return result;
		}
	}
}
