using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Internal.IO
{
	// Token: 0x020000CC RID: 204
	internal static class File
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00017D30 File Offset: 0x00015F30
		internal static bool InternalExists(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			return File.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true) == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) == 0;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017D68 File Offset: 0x00015F68
		internal static int FillAttributeInfo(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data, bool returnErrorOnNotFound)
		{
			int num = 0;
			using (DisableMediaInsertionPrompt.Create())
			{
				if (!Interop.Kernel32.GetFileAttributesEx(path, Interop.Kernel32.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, ref data))
				{
					num = Marshal.GetLastWin32Error();
					if (num == 5)
					{
						Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
						using (SafeFindHandle safeFindHandle = Interop.Kernel32.FindFirstFile(path, ref win32_FIND_DATA))
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
	}
}
