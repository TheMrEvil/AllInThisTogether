using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000189 RID: 393
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetVideoURLResult_t : ICallbackData
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x00016CC8 File Offset: 0x00014EC8
		internal string URLUTF8()
		{
			return Encoding.UTF8.GetString(this.URL, 0, Array.IndexOf<byte>(this.URL, 0));
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00016CE7 File Offset: 0x00014EE7
		public int DataSize
		{
			get
			{
				return GetVideoURLResult_t._datasize;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00016CEE File Offset: 0x00014EEE
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GetVideoURLResult;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00016CF5 File Offset: 0x00014EF5
		// Note: this type is marked as 'beforefieldinit'.
		static GetVideoURLResult_t()
		{
		}

		// Token: 0x04000A7F RID: 2687
		internal Result Result;

		// Token: 0x04000A80 RID: 2688
		internal AppId VideoAppID;

		// Token: 0x04000A81 RID: 2689
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] URL;

		// Token: 0x04000A82 RID: 2690
		public static int _datasize = Marshal.SizeOf(typeof(GetVideoURLResult_t));
	}
}
