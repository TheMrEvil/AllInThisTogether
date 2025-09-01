using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000154 RID: 340
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerSelectsPlaylistEntry_t : ICallbackData
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00016516 File Offset: 0x00014716
		public int DataSize
		{
			get
			{
				return MusicPlayerSelectsPlaylistEntry_t._datasize;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0001651D File Offset: 0x0001471D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerSelectsPlaylistEntry;
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00016524 File Offset: 0x00014724
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerSelectsPlaylistEntry_t()
		{
		}

		// Token: 0x040009AB RID: 2475
		internal int NID;

		// Token: 0x040009AC RID: 2476
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerSelectsPlaylistEntry_t));
	}
}
