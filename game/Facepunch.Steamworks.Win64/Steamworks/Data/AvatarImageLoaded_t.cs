using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AvatarImageLoaded_t : ICallbackData
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0001542F File Offset: 0x0001362F
		public int DataSize
		{
			get
			{
				return AvatarImageLoaded_t._datasize;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00015436 File Offset: 0x00013636
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AvatarImageLoaded;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001543D File Offset: 0x0001363D
		// Note: this type is marked as 'beforefieldinit'.
		static AvatarImageLoaded_t()
		{
		}

		// Token: 0x0400081C RID: 2076
		internal ulong SteamID;

		// Token: 0x0400081D RID: 2077
		internal int Image;

		// Token: 0x0400081E RID: 2078
		internal int Wide;

		// Token: 0x0400081F RID: 2079
		internal int Tall;

		// Token: 0x04000820 RID: 2080
		public static int _datasize = Marshal.SizeOf(typeof(AvatarImageLoaded_t));
	}
}
