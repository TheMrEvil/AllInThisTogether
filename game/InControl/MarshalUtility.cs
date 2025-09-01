using System;
using System.Runtime.InteropServices;

namespace InControl
{
	// Token: 0x02000064 RID: 100
	public static class MarshalUtility
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00011667 File Offset: 0x0000F867
		public static void Copy(IntPtr source, uint[] destination, int length)
		{
			Utility.ArrayExpand<int>(ref MarshalUtility.buffer, length);
			Marshal.Copy(source, MarshalUtility.buffer, 0, length);
			Buffer.BlockCopy(MarshalUtility.buffer, 0, destination, 0, 4 * length);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011691 File Offset: 0x0000F891
		// Note: this type is marked as 'beforefieldinit'.
		static MarshalUtility()
		{
		}

		// Token: 0x04000402 RID: 1026
		private static int[] buffer = new int[32];
	}
}
