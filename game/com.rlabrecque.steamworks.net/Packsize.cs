using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018C RID: 396
	public static class Packsize
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 32 && num2 == 616;
		}

		// Token: 0x04000A58 RID: 2648
		public const int value = 8;

		// Token: 0x020001EC RID: 492
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000B1D RID: 2845
			private uint m_u32;

			// Token: 0x04000B1E RID: 2846
			private ulong m_u64;

			// Token: 0x04000B1F RID: 2847
			private ushort m_u16;

			// Token: 0x04000B20 RID: 2848
			private double m_d;
		}
	}
}
