using System;

namespace System.IO
{
	// Token: 0x02000AFF RID: 2815
	internal static class DriveInfoInternal
	{
		// Token: 0x06006491 RID: 25745 RVA: 0x00155B84 File Offset: 0x00153D84
		public static string[] GetLogicalDrives()
		{
			int logicalDrives = Interop.Kernel32.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			char[] array2 = new char[]
			{
				'A',
				':',
				'\\'
			};
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = new string(array2);
				}
				num >>= 1;
				char[] array3 = array2;
				int num3 = 0;
				array3[num3] += '\u0001';
			}
			return array;
		}
	}
}
