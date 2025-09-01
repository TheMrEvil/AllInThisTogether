using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000122 RID: 290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t : ICallbackData
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x00015D16 File Offset: 0x00013F16
		internal string TitleUTF8()
		{
			return Encoding.UTF8.GetString(this.Title, 0, Array.IndexOf<byte>(this.Title, 0));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00015D35 File Offset: 0x00013F35
		internal string DescriptionUTF8()
		{
			return Encoding.UTF8.GetString(this.Description, 0, Array.IndexOf<byte>(this.Description, 0));
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00015D54 File Offset: 0x00013F54
		internal string TagsUTF8()
		{
			return Encoding.UTF8.GetString(this.Tags, 0, Array.IndexOf<byte>(this.Tags, 0));
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00015D73 File Offset: 0x00013F73
		internal string PchFileNameUTF8()
		{
			return Encoding.UTF8.GetString(this.PchFileName, 0, Array.IndexOf<byte>(this.PchFileName, 0));
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00015D92 File Offset: 0x00013F92
		internal string URLUTF8()
		{
			return Encoding.UTF8.GetString(this.URL, 0, Array.IndexOf<byte>(this.URL, 0));
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00015DB1 File Offset: 0x00013FB1
		public int DataSize
		{
			get
			{
				return RemoteStorageGetPublishedFileDetailsResult_t._datasize;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00015DB8 File Offset: 0x00013FB8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageGetPublishedFileDetailsResult;
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00015DBF File Offset: 0x00013FBF
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageGetPublishedFileDetailsResult_t()
		{
		}

		// Token: 0x040008FE RID: 2302
		internal Result Result;

		// Token: 0x040008FF RID: 2303
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000900 RID: 2304
		internal AppId CreatorAppID;

		// Token: 0x04000901 RID: 2305
		internal AppId ConsumerAppID;

		// Token: 0x04000902 RID: 2306
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		internal byte[] Title;

		// Token: 0x04000903 RID: 2307
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		internal byte[] Description;

		// Token: 0x04000904 RID: 2308
		internal ulong File;

		// Token: 0x04000905 RID: 2309
		internal ulong PreviewFile;

		// Token: 0x04000906 RID: 2310
		internal ulong SteamIDOwner;

		// Token: 0x04000907 RID: 2311
		internal uint TimeCreated;

		// Token: 0x04000908 RID: 2312
		internal uint TimeUpdated;

		// Token: 0x04000909 RID: 2313
		internal RemoteStoragePublishedFileVisibility Visibility;

		// Token: 0x0400090A RID: 2314
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x0400090B RID: 2315
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		internal byte[] Tags;

		// Token: 0x0400090C RID: 2316
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated;

		// Token: 0x0400090D RID: 2317
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		internal byte[] PchFileName;

		// Token: 0x0400090E RID: 2318
		internal int FileSize;

		// Token: 0x0400090F RID: 2319
		internal int PreviewFileSize;

		// Token: 0x04000910 RID: 2320
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] URL;

		// Token: 0x04000911 RID: 2321
		internal WorkshopFileType FileType;

		// Token: 0x04000912 RID: 2322
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse;

		// Token: 0x04000913 RID: 2323
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageGetPublishedFileDetailsResult_t));
	}
}
