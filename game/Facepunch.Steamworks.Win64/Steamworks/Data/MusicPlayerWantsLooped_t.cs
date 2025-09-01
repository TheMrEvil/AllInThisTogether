using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000151 RID: 337
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsLooped_t : ICallbackData
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000164AA File Offset: 0x000146AA
		public int DataSize
		{
			get
			{
				return MusicPlayerWantsLooped_t._datasize;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000164B1 File Offset: 0x000146B1
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerWantsLooped;
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000164B8 File Offset: 0x000146B8
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerWantsLooped_t()
		{
		}

		// Token: 0x040009A5 RID: 2469
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped;

		// Token: 0x040009A6 RID: 2470
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerWantsLooped_t));
	}
}
