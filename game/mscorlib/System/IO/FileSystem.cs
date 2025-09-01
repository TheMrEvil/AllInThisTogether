using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000B01 RID: 2817
	internal static class FileSystem
	{
		// Token: 0x06006494 RID: 25748 RVA: 0x00155C58 File Offset: 0x00153E58
		public static void CopyFile(string sourceFullPath, string destFullPath, bool overwrite)
		{
			int num = FileSystem.UnityCopyFile(sourceFullPath, destFullPath, !overwrite);
			if (num != 0)
			{
				string path = destFullPath;
				if (num != 80)
				{
					using (SafeFileHandle safeFileHandle = Interop.Kernel32.CreateFile(sourceFullPath, int.MinValue, FileShare.Read, FileMode.Open, 0))
					{
						if (safeFileHandle.IsInvalid)
						{
							path = sourceFullPath;
						}
					}
					if (num == 5 && FileSystem.DirectoryExists(destFullPath))
					{
						throw new IOException(SR.Format("The target file '{0}' is a directory, not a file.", destFullPath), 5);
					}
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, path);
			}
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x00155CD8 File Offset: 0x00153ED8
		public static void ReplaceFile(string sourceFullPath, string destFullPath, string destBackupFullPath, bool ignoreMetadataErrors)
		{
			int dwReplaceFlags = ignoreMetadataErrors ? 2 : 0;
			if (!Interop.Kernel32.ReplaceFile(destFullPath, sourceFullPath, destBackupFullPath, dwReplaceFlags, IntPtr.Zero, IntPtr.Zero))
			{
				throw Win32Marshal.GetExceptionForWin32Error(Marshal.GetLastWin32Error(), "");
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00155D14 File Offset: 0x00153F14
		public static void CreateDirectory(string fullPath)
		{
			if (FileSystem.DirectoryExists(fullPath))
			{
				return;
			}
			List<string> list = new List<string>();
			bool flag = false;
			int num = fullPath.Length;
			if (num >= 2 && PathInternal.EndsInDirectorySeparator(fullPath))
			{
				num--;
			}
			int rootLength = PathInternal.GetRootLength(fullPath);
			if (num > rootLength)
			{
				int num2 = num - 1;
				while (num2 >= rootLength && !flag)
				{
					string text = fullPath.Substring(0, num2 + 1);
					if (!FileSystem.DirectoryExists(text))
					{
						list.Add(text);
					}
					else
					{
						flag = true;
					}
					while (num2 > rootLength && !PathInternal.IsDirectorySeparator(fullPath[num2]))
					{
						num2--;
					}
					num2--;
				}
			}
			int count = list.Count;
			bool flag2 = true;
			int num3 = 0;
			string path = fullPath;
			while (list.Count > 0)
			{
				string text2 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				flag2 = FileSystem.UnityCreateDirectory(text2);
				if (!flag2 && num3 == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 183)
					{
						num3 = lastWin32Error;
					}
					else if (FileSystem.FileExists(text2) || (!FileSystem.DirectoryExists(text2, out lastWin32Error) && lastWin32Error == 5))
					{
						num3 = lastWin32Error;
						path = text2;
					}
				}
			}
			if (count == 0 && !flag)
			{
				string text3 = Directory.InternalGetDirectoryRoot(fullPath);
				if (!FileSystem.DirectoryExists(text3))
				{
					throw Win32Marshal.GetExceptionForWin32Error(3, text3);
				}
				return;
			}
			else
			{
				if (!flag2 && num3 != 0)
				{
					throw Win32Marshal.GetExceptionForWin32Error(num3, path);
				}
				return;
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x00155E68 File Offset: 0x00154068
		public static void DeleteFile(string fullPath)
		{
			if (FileSystem.UnityDeleteFile(fullPath))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 2)
			{
				return;
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, fullPath);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x00155E90 File Offset: 0x00154090
		public static bool DirectoryExists(string fullPath)
		{
			int num;
			return FileSystem.DirectoryExists(fullPath, out num);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00155EA8 File Offset: 0x001540A8
		private static bool DirectoryExists(string path, out int lastError)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			lastError = FileSystem.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, true);
			return lastError == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) != 0;
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x00155EE4 File Offset: 0x001540E4
		internal static int FillAttributeInfo(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data, bool returnErrorOnNotFound)
		{
			int num = 0;
			path = PathInternal.TrimEndingDirectorySeparator(path);
			using (DisableMediaInsertionPrompt.Create())
			{
				if (!FileSystem.UnityGetFileAttributesEx(path, ref data))
				{
					num = Marshal.GetLastWin32Error();
					if (num != 2 && num != 3 && num != 21 && num != 123 && num != 161 && num != 53 && num != 67 && num != 87 && num != 1231)
					{
						Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
						using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(path, ref win32_FIND_DATA))
						{
							if (safeFindHandle.IsInvalid)
							{
								num = Marshal.GetLastWin32Error();
							}
							else
							{
								num = 0;
								data.PopulateFrom(ref win32_FIND_DATA);
							}
						}
					}
				}
			}
			if (num != 0 && !returnErrorOnNotFound && (num - 2 <= 1 || num == 21))
			{
				data.dwFileAttributes = -1;
				return 0;
			}
			return num;
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x00155FC0 File Offset: 0x001541C0
		public static bool FileExists(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			return FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true) == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) == 0;
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00155FF8 File Offset: 0x001541F8
		public static FileAttributes GetAttributes(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return (FileAttributes)win32_FILE_ATTRIBUTE_DATA.dwFileAttributes;
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x00156028 File Offset: 0x00154228
		public static DateTimeOffset GetCreationTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftCreationTime.ToDateTimeOffset();
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x0015605E File Offset: 0x0015425E
		public static FileSystemInfo GetFileSystemInfo(string fullPath, bool asDirectory)
		{
			if (!asDirectory)
			{
				return new FileInfo(fullPath, null, null, false);
			}
			return new DirectoryInfo(fullPath, null, null, false);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x00156078 File Offset: 0x00154278
		public static DateTimeOffset GetLastAccessTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftLastAccessTime.ToDateTimeOffset();
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001560B0 File Offset: 0x001542B0
		public static DateTimeOffset GetLastWriteTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftLastWriteTime.ToDateTimeOffset();
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x001560E8 File Offset: 0x001542E8
		public static void MoveDirectory(string sourceFullPath, string destFullPath)
		{
			if (FileSystem.UnityMoveFile(sourceFullPath, destFullPath))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 2)
			{
				throw Win32Marshal.GetExceptionForWin32Error(3, sourceFullPath);
			}
			if (lastWin32Error == 5)
			{
				throw new IOException(SR.Format("Access to the path '{0}' is denied.", sourceFullPath), Win32Marshal.MakeHRFromErrorCode(lastWin32Error));
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x00156137 File Offset: 0x00154337
		public static void MoveFile(string sourceFullPath, string destFullPath)
		{
			if (!FileSystem.UnityMoveFile(sourceFullPath, destFullPath))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x00156150 File Offset: 0x00154350
		private static SafeFileHandle OpenHandle(string fullPath, bool asDirectory)
		{
			string text = fullPath.Substring(0, PathInternal.GetRootLength(fullPath));
			if (text == fullPath && text[1] == Path.VolumeSeparatorChar)
			{
				throw new ArgumentException("Path must not be a drive.", "path");
			}
			SafeFileHandle safeFileHandle = Interop.Kernel32.CreateFile(fullPath, 1073741824, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, asDirectory ? 33554432 : 0);
			if (safeFileHandle.IsInvalid)
			{
				int num = Marshal.GetLastWin32Error();
				if (!asDirectory && num == 3 && fullPath.Equals(Directory.GetDirectoryRoot(fullPath)))
				{
					num = 5;
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return safeFileHandle;
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x001561DC File Offset: 0x001543DC
		public static void RemoveDirectory(string fullPath, bool recursive)
		{
			if (!recursive)
			{
				FileSystem.RemoveDirectoryInternal(fullPath, true, false);
				return;
			}
			Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
			FileSystem.GetFindData(fullPath, ref win32_FIND_DATA);
			if (FileSystem.IsNameSurrogateReparsePoint(ref win32_FIND_DATA))
			{
				FileSystem.RemoveDirectoryInternal(fullPath, true, false);
				return;
			}
			fullPath = PathInternal.EnsureExtendedPrefix(fullPath);
			FileSystem.RemoveDirectoryRecursive(fullPath, ref win32_FIND_DATA, true);
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x00156228 File Offset: 0x00154428
		private static void GetFindData(string fullPath, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(PathInternal.TrimEndingDirectorySeparator(fullPath), ref findData))
			{
				if (safeFindHandle.IsInvalid)
				{
					int num = Marshal.GetLastWin32Error();
					if (num == 2)
					{
						num = 3;
					}
					throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
				}
			}
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x0015627C File Offset: 0x0015447C
		private static bool IsNameSurrogateReparsePoint(ref Interop.Kernel32.WIN32_FIND_DATA data)
		{
			return (data.dwFileAttributes & 1024U) != 0U && (data.dwReserved0 & 536870912U) > 0U;
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x001562A0 File Offset: 0x001544A0
		private static void RemoveDirectoryRecursive(string fullPath, ref Interop.Kernel32.WIN32_FIND_DATA findData, bool topLevel)
		{
			Exception ex = null;
			using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(Path.Join(fullPath, "*"), ref findData))
			{
				if (safeFindHandle.IsInvalid)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
				int lastWin32Error;
				do
				{
					if ((findData.dwFileAttributes & 16U) == 0U)
					{
						string stringFromFixedBuffer = findData.cFileName.GetStringFromFixedBuffer();
						if (!FileSystem.UnityDeleteFile(Path.Combine(fullPath, stringFromFixedBuffer)) && ex == null)
						{
							lastWin32Error = Marshal.GetLastWin32Error();
							if (lastWin32Error != 2)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(lastWin32Error, stringFromFixedBuffer);
							}
						}
					}
					else if (!findData.cFileName.FixedBufferEqualsString(".") && !findData.cFileName.FixedBufferEqualsString(".."))
					{
						string stringFromFixedBuffer2 = findData.cFileName.GetStringFromFixedBuffer();
						if (!FileSystem.IsNameSurrogateReparsePoint(ref findData))
						{
							try
							{
								FileSystem.RemoveDirectoryRecursive(Path.Combine(fullPath, stringFromFixedBuffer2), ref findData, false);
								goto IL_13D;
							}
							catch (Exception ex2)
							{
								if (ex == null)
								{
									ex = ex2;
								}
								goto IL_13D;
							}
						}
						if (findData.dwReserved0 == 2684354563U && !Interop.Kernel32.DeleteVolumeMountPoint(Path.Join(fullPath, stringFromFixedBuffer2, "\\")) && ex == null)
						{
							lastWin32Error = Marshal.GetLastWin32Error();
							if (lastWin32Error != 0 && lastWin32Error != 3)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(lastWin32Error, stringFromFixedBuffer2);
							}
						}
						if (!FileSystem.UnityRemoveDirectory(Path.Combine(fullPath, stringFromFixedBuffer2)) && ex == null)
						{
							lastWin32Error = Marshal.GetLastWin32Error();
							if (lastWin32Error != 3)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(lastWin32Error, stringFromFixedBuffer2);
							}
						}
					}
					IL_13D:;
				}
				while (FileSystem.UnityFindNextFile(safeFindHandle, ref findData));
				if (ex != null)
				{
					throw ex;
				}
				lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 0 && lastWin32Error != 18)
				{
					throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, fullPath);
				}
			}
			FileSystem.RemoveDirectoryInternal(fullPath, topLevel, true);
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x0015645C File Offset: 0x0015465C
		private static void RemoveDirectoryInternal(string fullPath, bool topLevel, bool allowDirectoryNotEmpty = false)
		{
			if (!FileSystem.UnityRemoveDirectory(fullPath))
			{
				int num = Marshal.GetLastWin32Error();
				switch (num)
				{
				case 2:
					num = 3;
					break;
				case 3:
					break;
				case 4:
					goto IL_4B;
				case 5:
					throw new IOException(SR.Format("Access to the path '{0}' is denied.", fullPath));
				default:
					if (num != 145)
					{
						goto IL_4B;
					}
					if (allowDirectoryNotEmpty)
					{
						return;
					}
					goto IL_4B;
				}
				if (!topLevel)
				{
					return;
				}
				IL_4B:
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x001564BC File Offset: 0x001546BC
		public static void SetAttributes(string fullPath, FileAttributes attributes)
		{
			if (FileSystem.UnitySetFileAttributes(fullPath, attributes))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 87)
			{
				throw new ArgumentException("Invalid File or Directory attributes value.", "attributes");
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, fullPath);
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x001564E8 File Offset: 0x001546E8
		public static void SetCreationTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, time.ToFileTime(), -1L, -1L, -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x00156538 File Offset: 0x00154738
		public static void SetLastAccessTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, -1L, time.ToFileTime(), -1L, -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x00156588 File Offset: 0x00154788
		public static void SetLastWriteTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, -1L, -1L, time.ToFileTime(), -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x001565D8 File Offset: 0x001547D8
		public static string[] GetLogicalDrives()
		{
			return DriveInfoInternal.GetLogicalDrives();
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x001565E0 File Offset: 0x001547E0
		private static bool UnityCreateDirectory(string name)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES security_ATTRIBUTES = default(Interop.Kernel32.SECURITY_ATTRIBUTES);
			return Interop.Kernel32.CreateDirectory(name, ref security_ATTRIBUTES);
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x001565FD File Offset: 0x001547FD
		private static bool UnityRemoveDirectory(string fullPath)
		{
			return Interop.Kernel32.RemoveDirectory(fullPath);
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x00156608 File Offset: 0x00154808
		private static bool UnityGetFileAttributesEx(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data)
		{
			if ((path.StartsWith("\\?\\") || path.StartsWith("\\\\?\\")) && path.Contains("GLOBALROOT\\Device\\Harddisk") && path.Length - path.IndexOf("Partition") <= 11 && path[path.Length - 1] != '\\')
			{
				path += "\\";
			}
			return Interop.Kernel32.GetFileAttributesEx(path, Interop.Kernel32.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, ref data);
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x00156679 File Offset: 0x00154879
		private static bool UnitySetFileAttributes(string fullPath, FileAttributes attributes)
		{
			return Interop.Kernel32.SetFileAttributes(fullPath, (int)attributes);
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x00156682 File Offset: 0x00154882
		internal static IntPtr UnityCreateFile_IntPtr(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			return Interop.Kernel32.CreateFile_IntPtr(lpFileName, dwDesiredAccess, dwShareMode, dwCreationDisposition, dwFlagsAndAttributes);
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0015668F File Offset: 0x0015488F
		private static int UnityCopyFile(string sourceFullPath, string destFullPath, bool failIfExists)
		{
			return Interop.Kernel32.CopyFile(sourceFullPath, destFullPath, failIfExists);
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x00156699 File Offset: 0x00154899
		private static bool UnityDeleteFile(string path)
		{
			return Interop.Kernel32.DeleteFile(path);
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x001566A1 File Offset: 0x001548A1
		private static bool UnityMoveFile(string sourceFullPath, string destFullPath)
		{
			return Interop.Kernel32.MoveFile(sourceFullPath, destFullPath);
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x001566AA File Offset: 0x001548AA
		private static SafeFindHandle UnityFindFirstFile(string path, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			return Interop.Kernel32.FindFirstFile(path, ref findData);
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x001566B4 File Offset: 0x001548B4
		private static bool UnityFindNextFile(SafeFindHandle handle, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			bool flag = false;
			bool result = false;
			if (!flag)
			{
				result = Interop.Kernel32.FindNextFile(handle, ref findData);
			}
			return result;
		}

		// Token: 0x04003B2C RID: 15148
		internal const int GENERIC_READ = -2147483648;
	}
}
