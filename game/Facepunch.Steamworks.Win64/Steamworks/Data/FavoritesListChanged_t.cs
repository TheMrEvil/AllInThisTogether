using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FC RID: 252
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FavoritesListChanged_t : ICallbackData
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00015742 File Offset: 0x00013942
		public int DataSize
		{
			get
			{
				return FavoritesListChanged_t._datasize;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00015749 File Offset: 0x00013949
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FavoritesListChanged;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00015750 File Offset: 0x00013950
		// Note: this type is marked as 'beforefieldinit'.
		static FavoritesListChanged_t()
		{
		}

		// Token: 0x0400085E RID: 2142
		internal uint IP;

		// Token: 0x0400085F RID: 2143
		internal uint QueryPort;

		// Token: 0x04000860 RID: 2144
		internal uint ConnPort;

		// Token: 0x04000861 RID: 2145
		internal uint AppID;

		// Token: 0x04000862 RID: 2146
		internal uint Flags;

		// Token: 0x04000863 RID: 2147
		[MarshalAs(UnmanagedType.I1)]
		internal bool Add;

		// Token: 0x04000864 RID: 2148
		internal uint AccountId;

		// Token: 0x04000865 RID: 2149
		public static int _datasize = Marshal.SizeOf(typeof(FavoritesListChanged_t));
	}
}
