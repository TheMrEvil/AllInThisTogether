using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000004 RID: 4
internal static class Interop
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	internal unsafe static void GetRandomBytes(byte* buffer, int length)
	{
		Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptGenRandom(IntPtr.Zero, buffer, length, 2);
		if (ntstatus == Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
		{
			return;
		}
		if (ntstatus == (Interop.BCrypt.NTSTATUS)3221225495U)
		{
			throw new OutOfMemoryException();
		}
		throw new InvalidOperationException();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000208A File Offset: 0x0000028A
	internal static IntPtr MemAlloc(UIntPtr sizeInBytes)
	{
		IntPtr intPtr = Interop.mincore.HeapAlloc(Interop.mincore.GetProcessHeap(), 0U, sizeInBytes);
		if (intPtr == IntPtr.Zero)
		{
			throw new OutOfMemoryException();
		}
		return intPtr;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020AB File Offset: 0x000002AB
	internal static void MemFree(IntPtr allocatedMemory)
	{
		Interop.mincore.HeapFree(Interop.mincore.GetProcessHeap(), 0U, allocatedMemory);
	}

	// Token: 0x02000005 RID: 5
	internal static class Kernel32
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		internal static int CopyFile(string src, string dst, bool failIfExists)
		{
			int flags = failIfExists ? 1 : 0;
			int num = 0;
			if (!Interop.Kernel32.CopyFileEx(src, dst, IntPtr.Zero, IntPtr.Zero, ref num, flags))
			{
				return Marshal.GetLastWin32Error();
			}
			return 0;
		}

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "DeleteVolumeMountPointW", SetLastError = true)]
		internal static extern bool DeleteVolumeMountPointPrivate(string mountPoint);

		// Token: 0x06000008 RID: 8 RVA: 0x000020F0 File Offset: 0x000002F0
		internal static bool DeleteVolumeMountPoint(string mountPoint)
		{
			mountPoint = PathInternal.EnsureExtendedPrefixIfNeeded(mountPoint);
			return Interop.Kernel32.DeleteVolumeMountPointPrivate(mountPoint);
		}

		// Token: 0x06000009 RID: 9
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x0600000B RID: 11
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetFileMUIPath(uint flags, string filePath, [Out] StringBuilder language, ref int languageLength, [Out] StringBuilder fileMuiPath, ref int fileMuiPathLength, ref long enumerator);

		// Token: 0x0600000C RID: 12
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetDynamicTimeZoneInformation(out Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x0600000D RID: 13
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetTimeZoneInformation(out Interop.Kernel32.TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x0600000E RID: 14
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CopyFileExW", SetLastError = true)]
		private static extern bool CopyFileExPrivate(string src, string dst, IntPtr progressRoutine, IntPtr progressData, ref int cancel, int flags);

		// Token: 0x06000010 RID: 16 RVA: 0x00002100 File Offset: 0x00000300
		internal static bool CopyFileEx(string src, string dst, IntPtr progressRoutine, IntPtr progressData, ref int cancel, int flags)
		{
			src = PathInternal.EnsureExtendedPrefixIfNeeded(src);
			dst = PathInternal.EnsureExtendedPrefixIfNeeded(dst);
			return Interop.Kernel32.CopyFileExPrivate(src, dst, progressRoutine, progressData, ref cancel, flags);
		}

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateDirectoryW", SetLastError = true)]
		private static extern bool CreateDirectoryPrivate(string path, ref Interop.Kernel32.SECURITY_ATTRIBUTES lpSecurityAttributes);

		// Token: 0x06000012 RID: 18 RVA: 0x0000211F File Offset: 0x0000031F
		internal static bool CreateDirectory(string path, ref Interop.Kernel32.SECURITY_ATTRIBUTES lpSecurityAttributes)
		{
			path = PathInternal.EnsureExtendedPrefix(path);
			return Interop.Kernel32.CreateDirectoryPrivate(path, ref lpSecurityAttributes);
		}

		// Token: 0x06000013 RID: 19
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern IntPtr CreateFilePrivate(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000014 RID: 20 RVA: 0x00002130 File Offset: 0x00000330
		internal unsafe static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, ref Interop.Kernel32.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			fixed (Interop.Kernel32.SECURITY_ATTRIBUTES* ptr = &securityAttrs)
			{
				Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs2 = ptr;
				IntPtr intPtr = Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs2, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
				SafeFileHandle result;
				try
				{
					result = new SafeFileHandle(intPtr, true);
				}
				catch
				{
					Interop.Kernel32.CloseHandle(intPtr);
					throw;
				}
				return result;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002180 File Offset: 0x00000380
		internal static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			IntPtr intPtr = Interop.Kernel32.CreateFile_IntPtr(lpFileName, dwDesiredAccess, dwShareMode, dwCreationDisposition, dwFlagsAndAttributes);
			SafeFileHandle result;
			try
			{
				result = new SafeFileHandle(intPtr, true);
			}
			catch
			{
				Interop.Kernel32.CloseHandle(intPtr);
				throw;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021C0 File Offset: 0x000003C0
		internal static IntPtr CreateFile_IntPtr(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			return Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, null, dwCreationDisposition, dwFlagsAndAttributes, IntPtr.Zero);
		}

		// Token: 0x06000017 RID: 23
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "DeleteFileW", SetLastError = true)]
		private static extern bool DeleteFilePrivate(string path);

		// Token: 0x06000018 RID: 24 RVA: 0x000021DC File Offset: 0x000003DC
		internal static bool DeleteFile(string path)
		{
			path = PathInternal.EnsureExtendedPrefixIfNeeded(path);
			return Interop.Kernel32.DeleteFilePrivate(path);
		}

		// Token: 0x06000019 RID: 25
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindFirstFileExW", SetLastError = true)]
		private static extern SafeFindHandle FindFirstFileExPrivate(string lpFileName, Interop.Kernel32.FINDEX_INFO_LEVELS fInfoLevelId, ref Interop.Kernel32.WIN32_FIND_DATA lpFindFileData, Interop.Kernel32.FINDEX_SEARCH_OPS fSearchOp, IntPtr lpSearchFilter, int dwAdditionalFlags);

		// Token: 0x0600001A RID: 26 RVA: 0x000021EC File Offset: 0x000003EC
		internal static SafeFindHandle FindFirstFile(string fileName, ref Interop.Kernel32.WIN32_FIND_DATA data)
		{
			fileName = PathInternal.EnsureExtendedPrefixIfNeeded(fileName);
			return Interop.Kernel32.FindFirstFileExPrivate(fileName, Interop.Kernel32.FINDEX_INFO_LEVELS.FindExInfoBasic, ref data, Interop.Kernel32.FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, 0);
		}

		// Token: 0x0600001B RID: 27
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindNextFileW", SetLastError = true)]
		internal static extern bool FindNextFile(SafeFindHandle hndFindFile, ref Interop.Kernel32.WIN32_FIND_DATA lpFindFileData);

		// Token: 0x0600001C RID: 28
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", SetLastError = true)]
		private unsafe static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, char* lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x0600001D RID: 29 RVA: 0x00002205 File Offset: 0x00000405
		internal static string GetMessage(int errorCode)
		{
			return Interop.Kernel32.GetMessage(IntPtr.Zero, errorCode);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002214 File Offset: 0x00000414
		internal unsafe static string GetMessage(IntPtr moduleHandle, int errorCode)
		{
			Span<char> buffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			string result;
			while (!Interop.Kernel32.TryGetErrorMessage(moduleHandle, errorCode, buffer, out result))
			{
				buffer = new char[buffer.Length * 4];
				if (buffer.Length >= 66560)
				{
					return string.Format("Unknown error (0x{0:x})", errorCode);
				}
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002274 File Offset: 0x00000474
		private unsafe static bool TryGetErrorMessage(IntPtr moduleHandle, int errorCode, Span<char> buffer, out string errorMsg)
		{
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 2048;
			}
			int num2;
			fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
			{
				char* lpBuffer = reference;
				num2 = Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, lpBuffer, buffer.Length, null);
			}
			if (num2 != 0)
			{
				int i;
				for (i = num2; i > 0; i--)
				{
					char c = *buffer[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = buffer.Slice(0, i).ToString();
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					errorMsg = "";
					return false;
				}
				errorMsg = string.Format("Unknown error (0x{0:x})", errorCode);
			}
			return true;
		}

		// Token: 0x06000020 RID: 32
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesExW", SetLastError = true)]
		private static extern bool GetFileAttributesExPrivate(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

		// Token: 0x06000021 RID: 33 RVA: 0x0000232B File Offset: 0x0000052B
		internal static bool GetFileAttributesEx(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation)
		{
			name = PathInternal.EnsureExtendedPrefixIfNeeded(name);
			return Interop.Kernel32.GetFileAttributesExPrivate(name, fileInfoLevel, ref lpFileInformation);
		}

		// Token: 0x06000022 RID: 34
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetLogicalDrives();

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "MoveFileExW", SetLastError = true)]
		private static extern bool MoveFileExPrivate(string src, string dst, uint flags);

		// Token: 0x06000024 RID: 36 RVA: 0x0000233D File Offset: 0x0000053D
		internal static bool MoveFile(string src, string dst)
		{
			src = PathInternal.EnsureExtendedPrefixIfNeeded(src);
			dst = PathInternal.EnsureExtendedPrefixIfNeeded(dst);
			return Interop.Kernel32.MoveFileExPrivate(src, dst, 2U);
		}

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RemoveDirectoryW", SetLastError = true)]
		private static extern bool RemoveDirectoryPrivate(string path);

		// Token: 0x06000026 RID: 38 RVA: 0x00002357 File Offset: 0x00000557
		internal static bool RemoveDirectory(string path)
		{
			path = PathInternal.EnsureExtendedPrefixIfNeeded(path);
			return Interop.Kernel32.RemoveDirectoryPrivate(path);
		}

		// Token: 0x06000027 RID: 39
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "ReplaceFileW", SetLastError = true)]
		private static extern bool ReplaceFilePrivate(string replacedFileName, string replacementFileName, string backupFileName, int dwReplaceFlags, IntPtr lpExclude, IntPtr lpReserved);

		// Token: 0x06000028 RID: 40 RVA: 0x00002367 File Offset: 0x00000567
		internal static bool ReplaceFile(string replacedFileName, string replacementFileName, string backupFileName, int dwReplaceFlags, IntPtr lpExclude, IntPtr lpReserved)
		{
			replacedFileName = PathInternal.EnsureExtendedPrefixIfNeeded(replacedFileName);
			replacementFileName = PathInternal.EnsureExtendedPrefixIfNeeded(replacementFileName);
			backupFileName = PathInternal.EnsureExtendedPrefixIfNeeded(backupFileName);
			return Interop.Kernel32.ReplaceFilePrivate(replacedFileName, replacementFileName, backupFileName, dwReplaceFlags, lpExclude, lpReserved);
		}

		// Token: 0x06000029 RID: 41
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetFileAttributesW", SetLastError = true)]
		private static extern bool SetFileAttributesPrivate(string name, int attr);

		// Token: 0x0600002A RID: 42 RVA: 0x0000238E File Offset: 0x0000058E
		internal static bool SetFileAttributes(string name, int attr)
		{
			name = PathInternal.EnsureExtendedPrefixIfNeeded(name);
			return Interop.Kernel32.SetFileAttributesPrivate(name, attr);
		}

		// Token: 0x0600002B RID: 43
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetFileInformationByHandle(SafeFileHandle hFile, Interop.Kernel32.FILE_INFO_BY_HANDLE_CLASS FileInformationClass, ref Interop.Kernel32.FILE_BASIC_INFO lpFileInformation, uint dwBufferSize);

		// Token: 0x0600002C RID: 44 RVA: 0x000023A0 File Offset: 0x000005A0
		internal static bool SetFileTime(SafeFileHandle hFile, long creationTime = -1L, long lastAccessTime = -1L, long lastWriteTime = -1L, long changeTime = -1L, uint fileAttributes = 0U)
		{
			Interop.Kernel32.FILE_BASIC_INFO file_BASIC_INFO = new Interop.Kernel32.FILE_BASIC_INFO
			{
				CreationTime = creationTime,
				LastAccessTime = lastAccessTime,
				LastWriteTime = lastWriteTime,
				ChangeTime = changeTime,
				FileAttributes = fileAttributes
			};
			return Interop.Kernel32.SetFileInformationByHandle(hFile, Interop.Kernel32.FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo, ref file_BASIC_INFO, (uint)sizeof(Interop.Kernel32.FILE_BASIC_INFO));
		}

		// Token: 0x0600002D RID: 45
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetThreadErrorMode(uint dwNewMode, out uint lpOldMode);

		// Token: 0x04000001 RID: 1
		internal const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x04000002 RID: 2
		internal const int MAX_PATH = 260;

		// Token: 0x04000003 RID: 3
		internal const uint MUI_PREFERRED_UI_LANGUAGES = 16U;

		// Token: 0x04000004 RID: 4
		internal const uint TIME_ZONE_ID_INVALID = 4294967295U;

		// Token: 0x04000005 RID: 5
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x04000006 RID: 6
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04000007 RID: 7
		private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		// Token: 0x04000008 RID: 8
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04000009 RID: 9
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x0400000A RID: 10
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x0400000B RID: 11
		private const int InitialBufferSize = 256;

		// Token: 0x0400000C RID: 12
		private const int BufferSizeIncreaseFactor = 4;

		// Token: 0x0400000D RID: 13
		private const int MaxAllowedBufferSize = 66560;

		// Token: 0x0400000E RID: 14
		internal const int REPLACEFILE_IGNORE_MERGE_ERRORS = 2;

		// Token: 0x02000006 RID: 6
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600002E RID: 46 RVA: 0x000023F0 File Offset: 0x000005F0
			internal unsafe ReadOnlySpan<char> cFileName
			{
				get
				{
					fixed (char* ptr = &this._cFileName.FixedElementField)
					{
						return new ReadOnlySpan<char>((void*)ptr, 260);
					}
				}
			}

			// Token: 0x0400000F RID: 15
			internal uint dwFileAttributes;

			// Token: 0x04000010 RID: 16
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x04000011 RID: 17
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x04000012 RID: 18
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x04000013 RID: 19
			internal uint nFileSizeHigh;

			// Token: 0x04000014 RID: 20
			internal uint nFileSizeLow;

			// Token: 0x04000015 RID: 21
			internal uint dwReserved0;

			// Token: 0x04000016 RID: 22
			internal uint dwReserved1;

			// Token: 0x04000017 RID: 23
			[FixedBuffer(typeof(char), 260)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cFileName>e__FixedBuffer _cFileName;

			// Token: 0x04000018 RID: 24
			[FixedBuffer(typeof(char), 14)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cAlternateFileName>e__FixedBuffer _cAlternateFileName;

			// Token: 0x02000007 RID: 7
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 520)]
			public struct <_cFileName>e__FixedBuffer
			{
				// Token: 0x04000019 RID: 25
				public char FixedElementField;
			}

			// Token: 0x02000008 RID: 8
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 28)]
			public struct <_cAlternateFileName>e__FixedBuffer
			{
				// Token: 0x0400001A RID: 26
				public char FixedElementField;
			}
		}

		// Token: 0x02000009 RID: 9
		internal struct REG_TZI_FORMAT
		{
			// Token: 0x0600002F RID: 47 RVA: 0x00002415 File Offset: 0x00000615
			internal REG_TZI_FORMAT(in Interop.Kernel32.TIME_ZONE_INFORMATION tzi)
			{
				this.Bias = tzi.Bias;
				this.StandardDate = tzi.StandardDate;
				this.StandardBias = tzi.StandardBias;
				this.DaylightDate = tzi.DaylightDate;
				this.DaylightBias = tzi.DaylightBias;
			}

			// Token: 0x0400001B RID: 27
			internal int Bias;

			// Token: 0x0400001C RID: 28
			internal int StandardBias;

			// Token: 0x0400001D RID: 29
			internal int DaylightBias;

			// Token: 0x0400001E RID: 30
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x0400001F RID: 31
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;
		}

		// Token: 0x0200000A RID: 10
		internal struct SYSTEMTIME
		{
			// Token: 0x06000030 RID: 48 RVA: 0x00002454 File Offset: 0x00000654
			internal bool Equals(in Interop.Kernel32.SYSTEMTIME other)
			{
				return this.Year == other.Year && this.Month == other.Month && this.DayOfWeek == other.DayOfWeek && this.Day == other.Day && this.Hour == other.Hour && this.Minute == other.Minute && this.Second == other.Second && this.Milliseconds == other.Milliseconds;
			}

			// Token: 0x04000020 RID: 32
			internal ushort Year;

			// Token: 0x04000021 RID: 33
			internal ushort Month;

			// Token: 0x04000022 RID: 34
			internal ushort DayOfWeek;

			// Token: 0x04000023 RID: 35
			internal ushort Day;

			// Token: 0x04000024 RID: 36
			internal ushort Hour;

			// Token: 0x04000025 RID: 37
			internal ushort Minute;

			// Token: 0x04000026 RID: 38
			internal ushort Second;

			// Token: 0x04000027 RID: 39
			internal ushort Milliseconds;
		}

		// Token: 0x0200000B RID: 11
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_DYNAMIC_ZONE_INFORMATION
		{
			// Token: 0x06000031 RID: 49 RVA: 0x000024D4 File Offset: 0x000006D4
			internal unsafe string GetTimeZoneKeyName()
			{
				fixed (char* ptr = &this.TimeZoneKeyName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x04000028 RID: 40
			internal int Bias;

			// Token: 0x04000029 RID: 41
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x0400002A RID: 42
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x0400002B RID: 43
			internal int StandardBias;

			// Token: 0x0400002C RID: 44
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x0400002D RID: 45
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x0400002E RID: 46
			internal int DaylightBias;

			// Token: 0x0400002F RID: 47
			[FixedBuffer(typeof(char), 128)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<TimeZoneKeyName>e__FixedBuffer TimeZoneKeyName;

			// Token: 0x04000030 RID: 48
			internal byte DynamicDaylightTimeDisabled;

			// Token: 0x0200000C RID: 12
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x04000031 RID: 49
				public char FixedElementField;
			}

			// Token: 0x0200000D RID: 13
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x04000032 RID: 50
				public char FixedElementField;
			}

			// Token: 0x0200000E RID: 14
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
			public struct <TimeZoneKeyName>e__FixedBuffer
			{
				// Token: 0x04000033 RID: 51
				public char FixedElementField;
			}
		}

		// Token: 0x0200000F RID: 15
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x06000032 RID: 50 RVA: 0x000024F4 File Offset: 0x000006F4
			internal unsafe TIME_ZONE_INFORMATION(in Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION dtzi)
			{
				fixed (Interop.Kernel32.TIME_ZONE_INFORMATION* ptr = &this)
				{
					ref Interop.Kernel32.TIME_ZONE_INFORMATION ptr2 = ref *ptr;
					fixed (Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr3 = &dtzi)
					{
						Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr4 = ptr3;
						ptr2 = *(Interop.Kernel32.TIME_ZONE_INFORMATION*)ptr4;
					}
				}
			}

			// Token: 0x06000033 RID: 51 RVA: 0x0000251C File Offset: 0x0000071C
			internal unsafe string GetStandardName()
			{
				fixed (char* ptr = &this.StandardName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x06000034 RID: 52 RVA: 0x0000253C File Offset: 0x0000073C
			internal unsafe string GetDaylightName()
			{
				fixed (char* ptr = &this.DaylightName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x04000034 RID: 52
			internal int Bias;

			// Token: 0x04000035 RID: 53
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x04000036 RID: 54
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x04000037 RID: 55
			internal int StandardBias;

			// Token: 0x04000038 RID: 56
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x04000039 RID: 57
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x0400003A RID: 58
			internal int DaylightBias;

			// Token: 0x02000010 RID: 16
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x0400003B RID: 59
				public char FixedElementField;
			}

			// Token: 0x02000011 RID: 17
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x0400003C RID: 60
				public char FixedElementField;
			}
		}

		// Token: 0x02000012 RID: 18
		internal enum FILE_INFO_BY_HANDLE_CLASS : uint
		{
			// Token: 0x0400003E RID: 62
			FileBasicInfo,
			// Token: 0x0400003F RID: 63
			FileStandardInfo,
			// Token: 0x04000040 RID: 64
			FileNameInfo,
			// Token: 0x04000041 RID: 65
			FileRenameInfo,
			// Token: 0x04000042 RID: 66
			FileDispositionInfo,
			// Token: 0x04000043 RID: 67
			FileAllocationInfo,
			// Token: 0x04000044 RID: 68
			FileEndOfFileInfo,
			// Token: 0x04000045 RID: 69
			FileStreamInfo,
			// Token: 0x04000046 RID: 70
			FileCompressionInfo,
			// Token: 0x04000047 RID: 71
			FileAttributeTagInfo,
			// Token: 0x04000048 RID: 72
			FileIdBothDirectoryInfo,
			// Token: 0x04000049 RID: 73
			FileIdBothDirectoryRestartInfo,
			// Token: 0x0400004A RID: 74
			FileIoPriorityHintInfo,
			// Token: 0x0400004B RID: 75
			FileRemoteProtocolInfo,
			// Token: 0x0400004C RID: 76
			FileFullDirectoryInfo,
			// Token: 0x0400004D RID: 77
			FileFullDirectoryRestartInfo
		}

		// Token: 0x02000013 RID: 19
		internal struct FILE_TIME
		{
			// Token: 0x06000035 RID: 53 RVA: 0x0000255C File Offset: 0x0000075C
			internal FILE_TIME(long fileTime)
			{
				this.dwLowDateTime = (uint)fileTime;
				this.dwHighDateTime = (uint)(fileTime >> 32);
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002571 File Offset: 0x00000771
			internal long ToTicks()
			{
				return (long)(((ulong)this.dwHighDateTime << 32) + (ulong)this.dwLowDateTime);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002585 File Offset: 0x00000785
			internal DateTime ToDateTimeUtc()
			{
				return DateTime.FromFileTimeUtc(this.ToTicks());
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002592 File Offset: 0x00000792
			internal DateTimeOffset ToDateTimeOffset()
			{
				return DateTimeOffset.FromFileTime(this.ToTicks());
			}

			// Token: 0x0400004E RID: 78
			internal uint dwLowDateTime;

			// Token: 0x0400004F RID: 79
			internal uint dwHighDateTime;
		}

		// Token: 0x02000014 RID: 20
		internal enum FINDEX_INFO_LEVELS : uint
		{
			// Token: 0x04000051 RID: 81
			FindExInfoStandard,
			// Token: 0x04000052 RID: 82
			FindExInfoBasic,
			// Token: 0x04000053 RID: 83
			FindExInfoMaxInfoLevel
		}

		// Token: 0x02000015 RID: 21
		internal enum FINDEX_SEARCH_OPS : uint
		{
			// Token: 0x04000055 RID: 85
			FindExSearchNameMatch,
			// Token: 0x04000056 RID: 86
			FindExSearchLimitToDirectories,
			// Token: 0x04000057 RID: 87
			FindExSearchLimitToDevices,
			// Token: 0x04000058 RID: 88
			FindExSearchMaxSearchOp
		}

		// Token: 0x02000016 RID: 22
		internal class FileAttributes
		{
			// Token: 0x06000039 RID: 57 RVA: 0x0000259F File Offset: 0x0000079F
			public FileAttributes()
			{
			}

			// Token: 0x04000059 RID: 89
			internal const int FILE_ATTRIBUTE_NORMAL = 128;

			// Token: 0x0400005A RID: 90
			internal const int FILE_ATTRIBUTE_READONLY = 1;

			// Token: 0x0400005B RID: 91
			internal const int FILE_ATTRIBUTE_DIRECTORY = 16;

			// Token: 0x0400005C RID: 92
			internal const int FILE_ATTRIBUTE_REPARSE_POINT = 1024;
		}

		// Token: 0x02000017 RID: 23
		internal class IOReparseOptions
		{
			// Token: 0x0600003A RID: 58 RVA: 0x0000259F File Offset: 0x0000079F
			public IOReparseOptions()
			{
			}

			// Token: 0x0400005D RID: 93
			internal const uint IO_REPARSE_TAG_FILE_PLACEHOLDER = 2147483669U;

			// Token: 0x0400005E RID: 94
			internal const uint IO_REPARSE_TAG_MOUNT_POINT = 2684354563U;
		}

		// Token: 0x02000018 RID: 24
		internal class FileOperations
		{
			// Token: 0x0600003B RID: 59 RVA: 0x0000259F File Offset: 0x0000079F
			public FileOperations()
			{
			}

			// Token: 0x0400005F RID: 95
			internal const int OPEN_EXISTING = 3;

			// Token: 0x04000060 RID: 96
			internal const int COPY_FILE_FAIL_IF_EXISTS = 1;

			// Token: 0x04000061 RID: 97
			internal const int FILE_ACTION_ADDED = 1;

			// Token: 0x04000062 RID: 98
			internal const int FILE_ACTION_REMOVED = 2;

			// Token: 0x04000063 RID: 99
			internal const int FILE_ACTION_MODIFIED = 3;

			// Token: 0x04000064 RID: 100
			internal const int FILE_ACTION_RENAMED_OLD_NAME = 4;

			// Token: 0x04000065 RID: 101
			internal const int FILE_ACTION_RENAMED_NEW_NAME = 5;

			// Token: 0x04000066 RID: 102
			internal const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

			// Token: 0x04000067 RID: 103
			internal const int FILE_FLAG_FIRST_PIPE_INSTANCE = 524288;

			// Token: 0x04000068 RID: 104
			internal const int FILE_FLAG_OVERLAPPED = 1073741824;

			// Token: 0x04000069 RID: 105
			internal const int FILE_LIST_DIRECTORY = 1;
		}

		// Token: 0x02000019 RID: 25
		internal enum GET_FILEEX_INFO_LEVELS : uint
		{
			// Token: 0x0400006B RID: 107
			GetFileExInfoStandard,
			// Token: 0x0400006C RID: 108
			GetFileExMaxInfoLevel
		}

		// Token: 0x0200001A RID: 26
		internal class GenericOperations
		{
			// Token: 0x0600003C RID: 60 RVA: 0x0000259F File Offset: 0x0000079F
			public GenericOperations()
			{
			}

			// Token: 0x0400006D RID: 109
			internal const int GENERIC_READ = -2147483648;

			// Token: 0x0400006E RID: 110
			internal const int GENERIC_WRITE = 1073741824;
		}

		// Token: 0x0200001B RID: 27
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x0400006F RID: 111
			internal uint nLength;

			// Token: 0x04000070 RID: 112
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x04000071 RID: 113
			internal Interop.BOOL bInheritHandle;
		}

		// Token: 0x0200001C RID: 28
		internal struct FILE_BASIC_INFO
		{
			// Token: 0x04000072 RID: 114
			internal long CreationTime;

			// Token: 0x04000073 RID: 115
			internal long LastAccessTime;

			// Token: 0x04000074 RID: 116
			internal long LastWriteTime;

			// Token: 0x04000075 RID: 117
			internal long ChangeTime;

			// Token: 0x04000076 RID: 118
			internal uint FileAttributes;
		}

		// Token: 0x0200001D RID: 29
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x0600003D RID: 61 RVA: 0x000025A8 File Offset: 0x000007A8
			internal void PopulateFrom(ref Interop.Kernel32.WIN32_FIND_DATA findData)
			{
				this.dwFileAttributes = (int)findData.dwFileAttributes;
				this.ftCreationTime = findData.ftCreationTime;
				this.ftLastAccessTime = findData.ftLastAccessTime;
				this.ftLastWriteTime = findData.ftLastWriteTime;
				this.nFileSizeHigh = findData.nFileSizeHigh;
				this.nFileSizeLow = findData.nFileSizeLow;
			}

			// Token: 0x04000077 RID: 119
			internal int dwFileAttributes;

			// Token: 0x04000078 RID: 120
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x04000079 RID: 121
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x0400007A RID: 122
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x0400007B RID: 123
			internal uint nFileSizeHigh;

			// Token: 0x0400007C RID: 124
			internal uint nFileSizeLow;
		}
	}

	// Token: 0x0200001E RID: 30
	internal class BCrypt
	{
		// Token: 0x0600003E RID: 62
		[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern Interop.BCrypt.NTSTATUS BCryptGenRandom(IntPtr hAlgorithm, byte* pbBuffer, int cbBuffer, int dwFlags);

		// Token: 0x0600003F RID: 63 RVA: 0x0000259F File Offset: 0x0000079F
		public BCrypt()
		{
		}

		// Token: 0x0400007D RID: 125
		internal const int BCRYPT_USE_SYSTEM_PREFERRED_RNG = 2;

		// Token: 0x0200001F RID: 31
		internal enum NTSTATUS : uint
		{
			// Token: 0x0400007F RID: 127
			STATUS_SUCCESS,
			// Token: 0x04000080 RID: 128
			STATUS_NOT_FOUND = 3221226021U,
			// Token: 0x04000081 RID: 129
			STATUS_INVALID_PARAMETER = 3221225485U,
			// Token: 0x04000082 RID: 130
			STATUS_NO_MEMORY = 3221225495U
		}
	}

	// Token: 0x02000020 RID: 32
	internal class User32
	{
		// Token: 0x06000040 RID: 64
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadStringW", SetLastError = true)]
		internal static extern int LoadString(SafeLibraryHandle handle, int id, [Out] StringBuilder buffer, int bufferLength);

		// Token: 0x06000041 RID: 65 RVA: 0x0000259F File Offset: 0x0000079F
		public User32()
		{
		}
	}

	// Token: 0x02000021 RID: 33
	internal enum BOOL
	{
		// Token: 0x04000084 RID: 132
		FALSE,
		// Token: 0x04000085 RID: 133
		TRUE
	}

	// Token: 0x02000022 RID: 34
	internal enum BOOLEAN : byte
	{
		// Token: 0x04000087 RID: 135
		FALSE,
		// Token: 0x04000088 RID: 136
		TRUE
	}

	// Token: 0x02000023 RID: 35
	internal class Errors
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000259F File Offset: 0x0000079F
		public Errors()
		{
		}

		// Token: 0x04000089 RID: 137
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x0400008A RID: 138
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x0400008B RID: 139
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400008C RID: 140
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400008D RID: 141
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400008E RID: 142
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400008F RID: 143
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04000090 RID: 144
		internal const int ERROR_INVALID_DATA = 13;

		// Token: 0x04000091 RID: 145
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x04000092 RID: 146
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000093 RID: 147
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x04000094 RID: 148
		internal const int ERROR_BAD_COMMAND = 22;

		// Token: 0x04000095 RID: 149
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000096 RID: 150
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000097 RID: 151
		internal const int ERROR_LOCK_VIOLATION = 33;

		// Token: 0x04000098 RID: 152
		internal const int ERROR_HANDLE_EOF = 38;

		// Token: 0x04000099 RID: 153
		internal const int ERROR_BAD_NETPATH = 53;

		// Token: 0x0400009A RID: 154
		internal const int ERROR_BAD_NET_NAME = 67;

		// Token: 0x0400009B RID: 155
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x0400009C RID: 156
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400009D RID: 157
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x0400009E RID: 158
		internal const int ERROR_SEM_TIMEOUT = 121;

		// Token: 0x0400009F RID: 159
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x040000A0 RID: 160
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040000A1 RID: 161
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x040000A2 RID: 162
		internal const int ERROR_NEGATIVE_SEEK = 131;

		// Token: 0x040000A3 RID: 163
		internal const int ERROR_DIR_NOT_EMPTY = 145;

		// Token: 0x040000A4 RID: 164
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x040000A5 RID: 165
		internal const int ERROR_LOCK_FAILED = 167;

		// Token: 0x040000A6 RID: 166
		internal const int ERROR_BUSY = 170;

		// Token: 0x040000A7 RID: 167
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x040000A8 RID: 168
		internal const int ERROR_BAD_EXE_FORMAT = 193;

		// Token: 0x040000A9 RID: 169
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x040000AA RID: 170
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x040000AB RID: 171
		internal const int ERROR_EXE_MACHINE_TYPE_MISMATCH = 216;

		// Token: 0x040000AC RID: 172
		internal const int ERROR_PIPE_BUSY = 231;

		// Token: 0x040000AD RID: 173
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x040000AE RID: 174
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x040000AF RID: 175
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x040000B0 RID: 176
		internal const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x040000B1 RID: 177
		internal const int ERROR_DIRECTORY = 267;

		// Token: 0x040000B2 RID: 178
		internal const int ERROR_PARTIAL_COPY = 299;

		// Token: 0x040000B3 RID: 179
		internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

		// Token: 0x040000B4 RID: 180
		internal const int ERROR_PIPE_CONNECTED = 535;

		// Token: 0x040000B5 RID: 181
		internal const int ERROR_PIPE_LISTENING = 536;

		// Token: 0x040000B6 RID: 182
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x040000B7 RID: 183
		internal const int ERROR_IO_INCOMPLETE = 996;

		// Token: 0x040000B8 RID: 184
		internal const int ERROR_IO_PENDING = 997;

		// Token: 0x040000B9 RID: 185
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x040000BA RID: 186
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x040000BB RID: 187
		internal const int ERROR_COUNTER_TIMEOUT = 1121;

		// Token: 0x040000BC RID: 188
		internal const int ERROR_NO_ASSOCIATION = 1155;

		// Token: 0x040000BD RID: 189
		internal const int ERROR_DDE_FAIL = 1156;

		// Token: 0x040000BE RID: 190
		internal const int ERROR_DLL_NOT_FOUND = 1157;

		// Token: 0x040000BF RID: 191
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x040000C0 RID: 192
		internal const int ERROR_NETWORK_UNREACHABLE = 1231;

		// Token: 0x040000C1 RID: 193
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x040000C2 RID: 194
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x040000C3 RID: 195
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x040000C4 RID: 196
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x040000C5 RID: 197
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x040000C6 RID: 198
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x040000C7 RID: 199
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x040000C8 RID: 200
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x040000C9 RID: 201
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x040000CA RID: 202
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x040000CB RID: 203
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x040000CC RID: 204
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x040000CD RID: 205
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x040000CE RID: 206
		internal const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		// Token: 0x040000CF RID: 207
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x040000D0 RID: 208
		internal const int ERROR_RESOURCE_LANG_NOT_FOUND = 1815;

		// Token: 0x040000D1 RID: 209
		internal const int EFail = -2147467259;

		// Token: 0x040000D2 RID: 210
		internal const int E_FILENOTFOUND = -2147024894;
	}

	// Token: 0x02000024 RID: 36
	internal static class Libraries
	{
		// Token: 0x040000D3 RID: 211
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x040000D4 RID: 212
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x040000D5 RID: 213
		internal const string CoreComm_L1_1_1 = "api-ms-win-core-comm-l1-1-1.dll";

		// Token: 0x040000D6 RID: 214
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x040000D7 RID: 215
		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		// Token: 0x040000D8 RID: 216
		internal const string HttpApi = "httpapi.dll";

		// Token: 0x040000D9 RID: 217
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x040000DA RID: 218
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x040000DB RID: 219
		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		// Token: 0x040000DC RID: 220
		internal const string Mswsock = "mswsock.dll";

		// Token: 0x040000DD RID: 221
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x040000DE RID: 222
		internal const string NtDll = "ntdll.dll";

		// Token: 0x040000DF RID: 223
		internal const string Odbc32 = "odbc32.dll";

		// Token: 0x040000E0 RID: 224
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x040000E1 RID: 225
		internal const string PerfCounter = "perfcounter.dll";

		// Token: 0x040000E2 RID: 226
		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		// Token: 0x040000E3 RID: 227
		internal const string Secur32 = "secur32.dll";

		// Token: 0x040000E4 RID: 228
		internal const string Shell32 = "shell32.dll";

		// Token: 0x040000E5 RID: 229
		internal const string SspiCli = "sspicli.dll";

		// Token: 0x040000E6 RID: 230
		internal const string User32 = "user32.dll";

		// Token: 0x040000E7 RID: 231
		internal const string Version = "version.dll";

		// Token: 0x040000E8 RID: 232
		internal const string WebSocket = "websocket.dll";

		// Token: 0x040000E9 RID: 233
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x040000EA RID: 234
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x040000EB RID: 235
		internal const string Wtsapi32 = "wtsapi32.dll";

		// Token: 0x040000EC RID: 236
		internal const string CompressionNative = "clrcompression.dll";

		// Token: 0x040000ED RID: 237
		internal const string ErrorHandling = "api-ms-win-core-errorhandling-l1-1-0.dll";

		// Token: 0x040000EE RID: 238
		internal const string Handle = "api-ms-win-core-handle-l1-1-0.dll";

		// Token: 0x040000EF RID: 239
		internal const string IO = "api-ms-win-core-io-l1-1-0.dll";

		// Token: 0x040000F0 RID: 240
		internal const string Memory = "api-ms-win-core-memory-l1-1-0.dll";

		// Token: 0x040000F1 RID: 241
		internal const string ProcessEnvironment = "api-ms-win-core-processenvironment-l1-1-0.dll";

		// Token: 0x040000F2 RID: 242
		internal const string ProcessThreads = "api-ms-win-core-processthreads-l1-1-0.dll";

		// Token: 0x040000F3 RID: 243
		internal const string RealTime = "api-ms-win-core-realtime-l1-1-0.dll";

		// Token: 0x040000F4 RID: 244
		internal const string SysInfo = "api-ms-win-core-sysinfo-l1-2-0.dll";

		// Token: 0x040000F5 RID: 245
		internal const string ThreadPool = "api-ms-win-core-threadpool-l1-2-0.dll";

		// Token: 0x040000F6 RID: 246
		internal const string Localization = "api-ms-win-core-localization-l1-2-1.dll";
	}

	// Token: 0x02000025 RID: 37
	internal struct LongFileTime
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000025FD File Offset: 0x000007FD
		internal DateTimeOffset ToDateTimeOffset()
		{
			return new DateTimeOffset(DateTime.FromFileTimeUtc(this.TicksSince1601));
		}

		// Token: 0x040000F7 RID: 247
		internal long TicksSince1601;
	}

	// Token: 0x02000026 RID: 38
	internal struct UNICODE_STRING
	{
		// Token: 0x040000F8 RID: 248
		internal ushort Length;

		// Token: 0x040000F9 RID: 249
		internal ushort MaximumLength;

		// Token: 0x040000FA RID: 250
		internal IntPtr Buffer;
	}

	// Token: 0x02000027 RID: 39
	internal class NtDll
	{
		// Token: 0x06000044 RID: 68
		[DllImport("ntdll.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private unsafe static extern int NtCreateFile(out IntPtr FileHandle, Interop.NtDll.DesiredAccess DesiredAccess, ref Interop.NtDll.OBJECT_ATTRIBUTES ObjectAttributes, out Interop.NtDll.IO_STATUS_BLOCK IoStatusBlock, long* AllocationSize, System.IO.FileAttributes FileAttributes, FileShare ShareAccess, Interop.NtDll.CreateDisposition CreateDisposition, Interop.NtDll.CreateOptions CreateOptions, void* EaBuffer, uint EaLength);

		// Token: 0x06000045 RID: 69 RVA: 0x00002610 File Offset: 0x00000810
		[return: TupleElementNames(new string[]
		{
			"status",
			"handle"
		})]
		internal unsafe static ValueTuple<int, IntPtr> CreateFile(ReadOnlySpan<char> path, IntPtr rootDirectory, Interop.NtDll.CreateDisposition createDisposition, Interop.NtDll.DesiredAccess desiredAccess = Interop.NtDll.DesiredAccess.SYNCHRONIZE | Interop.NtDll.DesiredAccess.FILE_GENERIC_READ, FileShare shareAccess = FileShare.Read | FileShare.Write | FileShare.Delete, System.IO.FileAttributes fileAttributes = (System.IO.FileAttributes)0, Interop.NtDll.CreateOptions createOptions = Interop.NtDll.CreateOptions.FILE_SYNCHRONOUS_IO_NONALERT, Interop.NtDll.ObjectAttributes objectAttributes = Interop.NtDll.ObjectAttributes.OBJ_CASE_INSENSITIVE)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(path))
			{
				char* value = reference;
				Interop.UNICODE_STRING unicode_STRING = checked(new Interop.UNICODE_STRING
				{
					Length = (ushort)(path.Length * 2),
					MaximumLength = (ushort)(path.Length * 2),
					Buffer = (IntPtr)((void*)value)
				});
				Interop.NtDll.OBJECT_ATTRIBUTES object_ATTRIBUTES = new Interop.NtDll.OBJECT_ATTRIBUTES(&unicode_STRING, objectAttributes, rootDirectory);
				IntPtr item;
				Interop.NtDll.IO_STATUS_BLOCK io_STATUS_BLOCK;
				return new ValueTuple<int, IntPtr>(Interop.NtDll.NtCreateFile(out item, desiredAccess, ref object_ATTRIBUTES, out io_STATUS_BLOCK, null, fileAttributes, shareAccess, createDisposition, createOptions, null, 0U), item);
			}
		}

		// Token: 0x06000046 RID: 70
		[DllImport("ntdll.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int NtQueryDirectoryFile(IntPtr FileHandle, IntPtr Event, IntPtr ApcRoutine, IntPtr ApcContext, out Interop.NtDll.IO_STATUS_BLOCK IoStatusBlock, IntPtr FileInformation, uint Length, Interop.NtDll.FILE_INFORMATION_CLASS FileInformationClass, Interop.BOOLEAN ReturnSingleEntry, Interop.UNICODE_STRING* FileName, Interop.BOOLEAN RestartScan);

		// Token: 0x06000047 RID: 71
		[DllImport("ntdll.dll", ExactSpelling = true)]
		public static extern uint RtlNtStatusToDosError(int Status);

		// Token: 0x06000048 RID: 72 RVA: 0x0000259F File Offset: 0x0000079F
		public NtDll()
		{
		}

		// Token: 0x02000028 RID: 40
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct FILE_FULL_DIR_INFORMATION
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00002690 File Offset: 0x00000890
			public unsafe ReadOnlySpan<char> FileName
			{
				get
				{
					fixed (char* ptr = &this._fileName)
					{
						return new ReadOnlySpan<char>((void*)ptr, (int)(this.FileNameLength / 2U));
					}
				}
			}

			// Token: 0x0600004A RID: 74 RVA: 0x000026B4 File Offset: 0x000008B4
			public unsafe static Interop.NtDll.FILE_FULL_DIR_INFORMATION* GetNextInfo(Interop.NtDll.FILE_FULL_DIR_INFORMATION* info)
			{
				if (info == null)
				{
					return null;
				}
				uint nextEntryOffset = info->NextEntryOffset;
				if (nextEntryOffset == 0U)
				{
					return null;
				}
				return info + nextEntryOffset / (uint)sizeof(Interop.NtDll.FILE_FULL_DIR_INFORMATION);
			}

			// Token: 0x040000FB RID: 251
			public uint NextEntryOffset;

			// Token: 0x040000FC RID: 252
			public uint FileIndex;

			// Token: 0x040000FD RID: 253
			public Interop.LongFileTime CreationTime;

			// Token: 0x040000FE RID: 254
			public Interop.LongFileTime LastAccessTime;

			// Token: 0x040000FF RID: 255
			public Interop.LongFileTime LastWriteTime;

			// Token: 0x04000100 RID: 256
			public Interop.LongFileTime ChangeTime;

			// Token: 0x04000101 RID: 257
			public long EndOfFile;

			// Token: 0x04000102 RID: 258
			public long AllocationSize;

			// Token: 0x04000103 RID: 259
			public System.IO.FileAttributes FileAttributes;

			// Token: 0x04000104 RID: 260
			public uint FileNameLength;

			// Token: 0x04000105 RID: 261
			public uint EaSize;

			// Token: 0x04000106 RID: 262
			private char _fileName;
		}

		// Token: 0x02000029 RID: 41
		public enum FILE_INFORMATION_CLASS : uint
		{
			// Token: 0x04000108 RID: 264
			FileDirectoryInformation = 1U,
			// Token: 0x04000109 RID: 265
			FileFullDirectoryInformation,
			// Token: 0x0400010A RID: 266
			FileBothDirectoryInformation,
			// Token: 0x0400010B RID: 267
			FileBasicInformation,
			// Token: 0x0400010C RID: 268
			FileStandardInformation,
			// Token: 0x0400010D RID: 269
			FileInternalInformation,
			// Token: 0x0400010E RID: 270
			FileEaInformation,
			// Token: 0x0400010F RID: 271
			FileAccessInformation,
			// Token: 0x04000110 RID: 272
			FileNameInformation,
			// Token: 0x04000111 RID: 273
			FileRenameInformation,
			// Token: 0x04000112 RID: 274
			FileLinkInformation,
			// Token: 0x04000113 RID: 275
			FileNamesInformation,
			// Token: 0x04000114 RID: 276
			FileDispositionInformation,
			// Token: 0x04000115 RID: 277
			FilePositionInformation,
			// Token: 0x04000116 RID: 278
			FileFullEaInformation,
			// Token: 0x04000117 RID: 279
			FileModeInformation,
			// Token: 0x04000118 RID: 280
			FileAlignmentInformation,
			// Token: 0x04000119 RID: 281
			FileAllInformation,
			// Token: 0x0400011A RID: 282
			FileAllocationInformation,
			// Token: 0x0400011B RID: 283
			FileEndOfFileInformation,
			// Token: 0x0400011C RID: 284
			FileAlternateNameInformation,
			// Token: 0x0400011D RID: 285
			FileStreamInformation,
			// Token: 0x0400011E RID: 286
			FilePipeInformation,
			// Token: 0x0400011F RID: 287
			FilePipeLocalInformation,
			// Token: 0x04000120 RID: 288
			FilePipeRemoteInformation,
			// Token: 0x04000121 RID: 289
			FileMailslotQueryInformation,
			// Token: 0x04000122 RID: 290
			FileMailslotSetInformation,
			// Token: 0x04000123 RID: 291
			FileCompressionInformation,
			// Token: 0x04000124 RID: 292
			FileObjectIdInformation,
			// Token: 0x04000125 RID: 293
			FileCompletionInformation,
			// Token: 0x04000126 RID: 294
			FileMoveClusterInformation,
			// Token: 0x04000127 RID: 295
			FileQuotaInformation,
			// Token: 0x04000128 RID: 296
			FileReparsePointInformation,
			// Token: 0x04000129 RID: 297
			FileNetworkOpenInformation,
			// Token: 0x0400012A RID: 298
			FileAttributeTagInformation,
			// Token: 0x0400012B RID: 299
			FileTrackingInformation,
			// Token: 0x0400012C RID: 300
			FileIdBothDirectoryInformation,
			// Token: 0x0400012D RID: 301
			FileIdFullDirectoryInformation,
			// Token: 0x0400012E RID: 302
			FileValidDataLengthInformation,
			// Token: 0x0400012F RID: 303
			FileShortNameInformation,
			// Token: 0x04000130 RID: 304
			FileIoCompletionNotificationInformation,
			// Token: 0x04000131 RID: 305
			FileIoStatusBlockRangeInformation,
			// Token: 0x04000132 RID: 306
			FileIoPriorityHintInformation,
			// Token: 0x04000133 RID: 307
			FileSfioReserveInformation,
			// Token: 0x04000134 RID: 308
			FileSfioVolumeInformation,
			// Token: 0x04000135 RID: 309
			FileHardLinkInformation,
			// Token: 0x04000136 RID: 310
			FileProcessIdsUsingFileInformation,
			// Token: 0x04000137 RID: 311
			FileNormalizedNameInformation,
			// Token: 0x04000138 RID: 312
			FileNetworkPhysicalNameInformation,
			// Token: 0x04000139 RID: 313
			FileIdGlobalTxDirectoryInformation,
			// Token: 0x0400013A RID: 314
			FileIsRemoteDeviceInformation,
			// Token: 0x0400013B RID: 315
			FileUnusedInformation,
			// Token: 0x0400013C RID: 316
			FileNumaNodeInformation,
			// Token: 0x0400013D RID: 317
			FileStandardLinkInformation,
			// Token: 0x0400013E RID: 318
			FileRemoteProtocolInformation,
			// Token: 0x0400013F RID: 319
			FileRenameInformationBypassAccessCheck,
			// Token: 0x04000140 RID: 320
			FileLinkInformationBypassAccessCheck,
			// Token: 0x04000141 RID: 321
			FileVolumeNameInformation,
			// Token: 0x04000142 RID: 322
			FileIdInformation,
			// Token: 0x04000143 RID: 323
			FileIdExtdDirectoryInformation,
			// Token: 0x04000144 RID: 324
			FileReplaceCompletionInformation,
			// Token: 0x04000145 RID: 325
			FileHardLinkFullIdInformation,
			// Token: 0x04000146 RID: 326
			FileIdExtdBothDirectoryInformation,
			// Token: 0x04000147 RID: 327
			FileDispositionInformationEx,
			// Token: 0x04000148 RID: 328
			FileRenameInformationEx,
			// Token: 0x04000149 RID: 329
			FileRenameInformationExBypassAccessCheck,
			// Token: 0x0400014A RID: 330
			FileDesiredStorageClassInformation,
			// Token: 0x0400014B RID: 331
			FileStatInformation
		}

		// Token: 0x0200002A RID: 42
		public struct IO_STATUS_BLOCK
		{
			// Token: 0x0400014C RID: 332
			public Interop.NtDll.IO_STATUS_BLOCK.IO_STATUS Status;

			// Token: 0x0400014D RID: 333
			public IntPtr Information;

			// Token: 0x0200002B RID: 43
			[StructLayout(LayoutKind.Explicit)]
			public struct IO_STATUS
			{
				// Token: 0x0400014E RID: 334
				[FieldOffset(0)]
				public uint Status;

				// Token: 0x0400014F RID: 335
				[FieldOffset(0)]
				public IntPtr Pointer;
			}
		}

		// Token: 0x0200002C RID: 44
		public struct OBJECT_ATTRIBUTES
		{
			// Token: 0x0600004B RID: 75 RVA: 0x000026DA File Offset: 0x000008DA
			public unsafe OBJECT_ATTRIBUTES(Interop.UNICODE_STRING* objectName, Interop.NtDll.ObjectAttributes attributes, IntPtr rootDirectory)
			{
				this.Length = (uint)sizeof(Interop.NtDll.OBJECT_ATTRIBUTES);
				this.RootDirectory = rootDirectory;
				this.ObjectName = objectName;
				this.Attributes = attributes;
				this.SecurityDescriptor = null;
				this.SecurityQualityOfService = null;
			}

			// Token: 0x04000150 RID: 336
			public uint Length;

			// Token: 0x04000151 RID: 337
			public IntPtr RootDirectory;

			// Token: 0x04000152 RID: 338
			public unsafe Interop.UNICODE_STRING* ObjectName;

			// Token: 0x04000153 RID: 339
			public Interop.NtDll.ObjectAttributes Attributes;

			// Token: 0x04000154 RID: 340
			public unsafe void* SecurityDescriptor;

			// Token: 0x04000155 RID: 341
			public unsafe void* SecurityQualityOfService;
		}

		// Token: 0x0200002D RID: 45
		[Flags]
		public enum ObjectAttributes : uint
		{
			// Token: 0x04000157 RID: 343
			OBJ_INHERIT = 2U,
			// Token: 0x04000158 RID: 344
			OBJ_PERMANENT = 16U,
			// Token: 0x04000159 RID: 345
			OBJ_EXCLUSIVE = 32U,
			// Token: 0x0400015A RID: 346
			OBJ_CASE_INSENSITIVE = 64U,
			// Token: 0x0400015B RID: 347
			OBJ_OPENIF = 128U,
			// Token: 0x0400015C RID: 348
			OBJ_OPENLINK = 256U
		}

		// Token: 0x0200002E RID: 46
		public enum CreateDisposition : uint
		{
			// Token: 0x0400015E RID: 350
			FILE_SUPERSEDE,
			// Token: 0x0400015F RID: 351
			FILE_OPEN,
			// Token: 0x04000160 RID: 352
			FILE_CREATE,
			// Token: 0x04000161 RID: 353
			FILE_OPEN_IF,
			// Token: 0x04000162 RID: 354
			FILE_OVERWRITE,
			// Token: 0x04000163 RID: 355
			FILE_OVERWRITE_IF
		}

		// Token: 0x0200002F RID: 47
		public enum CreateOptions : uint
		{
			// Token: 0x04000165 RID: 357
			FILE_DIRECTORY_FILE = 1U,
			// Token: 0x04000166 RID: 358
			FILE_WRITE_THROUGH,
			// Token: 0x04000167 RID: 359
			FILE_SEQUENTIAL_ONLY = 4U,
			// Token: 0x04000168 RID: 360
			FILE_NO_INTERMEDIATE_BUFFERING = 8U,
			// Token: 0x04000169 RID: 361
			FILE_SYNCHRONOUS_IO_ALERT = 16U,
			// Token: 0x0400016A RID: 362
			FILE_SYNCHRONOUS_IO_NONALERT = 32U,
			// Token: 0x0400016B RID: 363
			FILE_NON_DIRECTORY_FILE = 64U,
			// Token: 0x0400016C RID: 364
			FILE_CREATE_TREE_CONNECTION = 128U,
			// Token: 0x0400016D RID: 365
			FILE_COMPLETE_IF_OPLOCKED = 256U,
			// Token: 0x0400016E RID: 366
			FILE_NO_EA_KNOWLEDGE = 512U,
			// Token: 0x0400016F RID: 367
			FILE_RANDOM_ACCESS = 2048U,
			// Token: 0x04000170 RID: 368
			FILE_DELETE_ON_CLOSE = 4096U,
			// Token: 0x04000171 RID: 369
			FILE_OPEN_BY_FILE_ID = 8192U,
			// Token: 0x04000172 RID: 370
			FILE_OPEN_FOR_BACKUP_INTENT = 16384U,
			// Token: 0x04000173 RID: 371
			FILE_NO_COMPRESSION = 32768U,
			// Token: 0x04000174 RID: 372
			FILE_OPEN_REQUIRING_OPLOCK = 65536U,
			// Token: 0x04000175 RID: 373
			FILE_DISALLOW_EXCLUSIVE = 131072U,
			// Token: 0x04000176 RID: 374
			FILE_SESSION_AWARE = 262144U,
			// Token: 0x04000177 RID: 375
			FILE_RESERVE_OPFILTER = 1048576U,
			// Token: 0x04000178 RID: 376
			FILE_OPEN_REPARSE_POINT = 2097152U,
			// Token: 0x04000179 RID: 377
			FILE_OPEN_NO_RECALL = 4194304U
		}

		// Token: 0x02000030 RID: 48
		[Flags]
		public enum DesiredAccess : uint
		{
			// Token: 0x0400017B RID: 379
			FILE_READ_DATA = 1U,
			// Token: 0x0400017C RID: 380
			FILE_LIST_DIRECTORY = 1U,
			// Token: 0x0400017D RID: 381
			FILE_WRITE_DATA = 2U,
			// Token: 0x0400017E RID: 382
			FILE_ADD_FILE = 2U,
			// Token: 0x0400017F RID: 383
			FILE_APPEND_DATA = 4U,
			// Token: 0x04000180 RID: 384
			FILE_ADD_SUBDIRECTORY = 4U,
			// Token: 0x04000181 RID: 385
			FILE_CREATE_PIPE_INSTANCE = 4U,
			// Token: 0x04000182 RID: 386
			FILE_READ_EA = 8U,
			// Token: 0x04000183 RID: 387
			FILE_WRITE_EA = 16U,
			// Token: 0x04000184 RID: 388
			FILE_EXECUTE = 32U,
			// Token: 0x04000185 RID: 389
			FILE_TRAVERSE = 32U,
			// Token: 0x04000186 RID: 390
			FILE_DELETE_CHILD = 64U,
			// Token: 0x04000187 RID: 391
			FILE_READ_ATTRIBUTES = 128U,
			// Token: 0x04000188 RID: 392
			FILE_WRITE_ATTRIBUTES = 256U,
			// Token: 0x04000189 RID: 393
			FILE_ALL_ACCESS = 983551U,
			// Token: 0x0400018A RID: 394
			DELETE = 65536U,
			// Token: 0x0400018B RID: 395
			READ_CONTROL = 131072U,
			// Token: 0x0400018C RID: 396
			WRITE_DAC = 262144U,
			// Token: 0x0400018D RID: 397
			WRITE_OWNER = 524288U,
			// Token: 0x0400018E RID: 398
			SYNCHRONIZE = 1048576U,
			// Token: 0x0400018F RID: 399
			STANDARD_RIGHTS_READ = 131072U,
			// Token: 0x04000190 RID: 400
			STANDARD_RIGHTS_WRITE = 131072U,
			// Token: 0x04000191 RID: 401
			STANDARD_RIGHTS_EXECUTE = 131072U,
			// Token: 0x04000192 RID: 402
			FILE_GENERIC_READ = 2147483648U,
			// Token: 0x04000193 RID: 403
			FILE_GENERIC_WRITE = 1073741824U,
			// Token: 0x04000194 RID: 404
			FILE_GENERIC_EXECUTE = 536870912U
		}
	}

	// Token: 0x02000031 RID: 49
	internal class StatusOptions
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000259F File Offset: 0x0000079F
		public StatusOptions()
		{
		}

		// Token: 0x04000195 RID: 405
		internal const uint STATUS_SUCCESS = 0U;

		// Token: 0x04000196 RID: 406
		internal const uint STATUS_SOME_NOT_MAPPED = 263U;

		// Token: 0x04000197 RID: 407
		internal const uint STATUS_NO_MORE_FILES = 2147483654U;

		// Token: 0x04000198 RID: 408
		internal const uint STATUS_INVALID_PARAMETER = 3221225485U;

		// Token: 0x04000199 RID: 409
		internal const uint STATUS_NO_MEMORY = 3221225495U;

		// Token: 0x0400019A RID: 410
		internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;

		// Token: 0x0400019B RID: 411
		internal const uint STATUS_NONE_MAPPED = 3221225587U;

		// Token: 0x0400019C RID: 412
		internal const uint STATUS_INSUFFICIENT_RESOURCES = 3221225626U;

		// Token: 0x0400019D RID: 413
		internal const uint STATUS_ACCESS_DENIED = 3221225506U;

		// Token: 0x0400019E RID: 414
		internal const uint STATUS_ACCOUNT_RESTRICTION = 3221225582U;
	}

	// Token: 0x02000032 RID: 50
	internal class Advapi32
	{
		// Token: 0x0600004D RID: 77
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x0600004E RID: 78
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegConnectRegistryW")]
		internal static extern int RegConnectRegistry(string machineName, SafeRegistryHandle key, out SafeRegistryHandle result);

		// Token: 0x0600004F RID: 79
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegCreateKeyExW")]
		internal static extern int RegCreateKeyEx(SafeRegistryHandle hKey, string lpSubKey, int Reserved, string lpClass, int dwOptions, int samDesired, ref Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs, out SafeRegistryHandle hkResult, out int lpdwDisposition);

		// Token: 0x06000050 RID: 80
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegDeleteKeyExW")]
		internal static extern int RegDeleteKeyEx(SafeRegistryHandle hKey, string lpSubKey, int samDesired, int Reserved);

		// Token: 0x06000051 RID: 81
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegDeleteValueW")]
		internal static extern int RegDeleteValue(SafeRegistryHandle hKey, string lpValueName);

		// Token: 0x06000052 RID: 82
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumKeyExW")]
		internal static extern int RegEnumKeyEx(SafeRegistryHandle hKey, int dwIndex, char[] lpName, ref int lpcbName, int[] lpReserved, [Out] StringBuilder lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x06000053 RID: 83
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumValueW")]
		internal static extern int RegEnumValue(SafeRegistryHandle hKey, int dwIndex, char[] lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x06000054 RID: 84
		[DllImport("advapi32.dll")]
		internal static extern int RegFlushKey(SafeRegistryHandle hKey);

		// Token: 0x06000055 RID: 85
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW")]
		internal static extern int RegOpenKeyEx(SafeRegistryHandle hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x06000056 RID: 86
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW")]
		internal static extern int RegOpenKeyEx(IntPtr hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x06000057 RID: 87
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryInfoKeyW")]
		internal static extern int RegQueryInfoKey(SafeRegistryHandle hKey, [Out] StringBuilder lpClass, int[] lpcbClass, IntPtr lpReserved_MustBeZero, ref int lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, ref int lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, int[] lpftLastWriteTime);

		// Token: 0x06000058 RID: 88
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

		// Token: 0x06000059 RID: 89
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref int lpData, ref int lpcbData);

		// Token: 0x0600005A RID: 90
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref long lpData, ref int lpcbData);

		// Token: 0x0600005B RID: 91
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] char[] lpData, ref int lpcbData);

		// Token: 0x0600005C RID: 92
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] StringBuilder lpData, ref int lpcbData);

		// Token: 0x0600005D RID: 93
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, byte[] lpData, int cbData);

		// Token: 0x0600005E RID: 94
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, char[] lpData, int cbData);

		// Token: 0x0600005F RID: 95
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref int lpData, int cbData);

		// Token: 0x06000060 RID: 96
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref long lpData, int cbData);

		// Token: 0x06000061 RID: 97
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, string lpData, int cbData);

		// Token: 0x06000062 RID: 98
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr RegisterServiceCtrlHandler(string serviceName, Delegate callback);

		// Token: 0x06000063 RID: 99
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr RegisterServiceCtrlHandlerEx(string serviceName, Delegate callback, IntPtr userData);

		// Token: 0x06000064 RID: 100 RVA: 0x0000259F File Offset: 0x0000079F
		public Advapi32()
		{
		}

		// Token: 0x02000033 RID: 51
		internal class RegistryOptions
		{
			// Token: 0x06000065 RID: 101 RVA: 0x0000259F File Offset: 0x0000079F
			public RegistryOptions()
			{
			}

			// Token: 0x0400019F RID: 415
			internal const int REG_OPTION_NON_VOLATILE = 0;

			// Token: 0x040001A0 RID: 416
			internal const int REG_OPTION_VOLATILE = 1;

			// Token: 0x040001A1 RID: 417
			internal const int REG_OPTION_CREATE_LINK = 2;

			// Token: 0x040001A2 RID: 418
			internal const int REG_OPTION_BACKUP_RESTORE = 4;
		}

		// Token: 0x02000034 RID: 52
		internal class RegistryView
		{
			// Token: 0x06000066 RID: 102 RVA: 0x0000259F File Offset: 0x0000079F
			public RegistryView()
			{
			}

			// Token: 0x040001A3 RID: 419
			internal const int KEY_WOW64_64KEY = 256;

			// Token: 0x040001A4 RID: 420
			internal const int KEY_WOW64_32KEY = 512;
		}

		// Token: 0x02000035 RID: 53
		internal class RegistryOperations
		{
			// Token: 0x06000067 RID: 103 RVA: 0x0000259F File Offset: 0x0000079F
			public RegistryOperations()
			{
			}

			// Token: 0x040001A5 RID: 421
			internal const int KEY_QUERY_VALUE = 1;

			// Token: 0x040001A6 RID: 422
			internal const int KEY_SET_VALUE = 2;

			// Token: 0x040001A7 RID: 423
			internal const int KEY_CREATE_SUB_KEY = 4;

			// Token: 0x040001A8 RID: 424
			internal const int KEY_ENUMERATE_SUB_KEYS = 8;

			// Token: 0x040001A9 RID: 425
			internal const int KEY_NOTIFY = 16;

			// Token: 0x040001AA RID: 426
			internal const int KEY_CREATE_LINK = 32;

			// Token: 0x040001AB RID: 427
			internal const int KEY_READ = 131097;

			// Token: 0x040001AC RID: 428
			internal const int KEY_WRITE = 131078;

			// Token: 0x040001AD RID: 429
			internal const int SYNCHRONIZE = 1048576;

			// Token: 0x040001AE RID: 430
			internal const int READ_CONTROL = 131072;

			// Token: 0x040001AF RID: 431
			internal const int STANDARD_RIGHTS_READ = 131072;

			// Token: 0x040001B0 RID: 432
			internal const int STANDARD_RIGHTS_WRITE = 131072;
		}

		// Token: 0x02000036 RID: 54
		internal class RegistryValues
		{
			// Token: 0x06000068 RID: 104 RVA: 0x0000259F File Offset: 0x0000079F
			public RegistryValues()
			{
			}

			// Token: 0x040001B1 RID: 433
			internal const int REG_NONE = 0;

			// Token: 0x040001B2 RID: 434
			internal const int REG_SZ = 1;

			// Token: 0x040001B3 RID: 435
			internal const int REG_EXPAND_SZ = 2;

			// Token: 0x040001B4 RID: 436
			internal const int REG_BINARY = 3;

			// Token: 0x040001B5 RID: 437
			internal const int REG_DWORD = 4;

			// Token: 0x040001B6 RID: 438
			internal const int REG_DWORD_LITTLE_ENDIAN = 4;

			// Token: 0x040001B7 RID: 439
			internal const int REG_DWORD_BIG_ENDIAN = 5;

			// Token: 0x040001B8 RID: 440
			internal const int REG_LINK = 6;

			// Token: 0x040001B9 RID: 441
			internal const int REG_MULTI_SZ = 7;

			// Token: 0x040001BA RID: 442
			internal const int REG_QWORD = 11;
		}
	}

	// Token: 0x02000037 RID: 55
	internal static class mincore
	{
		// Token: 0x06000069 RID: 105
		[DllImport("api-ms-win-core-heap-l1-1-0.dll")]
		internal static extern IntPtr GetProcessHeap();

		// Token: 0x0600006A RID: 106
		[DllImport("api-ms-win-core-heap-l1-1-0.dll")]
		internal static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

		// Token: 0x0600006B RID: 107
		[DllImport("api-ms-win-core-heap-l1-1-0.dll")]
		internal static extern int HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

		// Token: 0x0600006C RID: 108
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll", SetLastError = true)]
		internal static extern SafeThreadPoolIOHandle CreateThreadpoolIo(SafeHandle fl, IntPtr pfnio, IntPtr context, IntPtr pcbe);

		// Token: 0x0600006D RID: 109
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void CloseThreadpoolIo(IntPtr pio);

		// Token: 0x0600006E RID: 110
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void StartThreadpoolIo(SafeThreadPoolIOHandle pio);

		// Token: 0x0600006F RID: 111
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void CancelThreadpoolIo(SafeThreadPoolIOHandle pio);
	}

	// Token: 0x02000038 RID: 56
	// (Invoke) Token: 0x06000071 RID: 113
	internal delegate void NativeIoCompletionCallback(IntPtr instance, IntPtr context, IntPtr overlapped, uint ioResult, UIntPtr numberOfBytesTransferred, IntPtr io);
}
