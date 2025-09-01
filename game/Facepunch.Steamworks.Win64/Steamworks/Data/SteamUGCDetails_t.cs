using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020001AF RID: 431
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCDetails_t
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00017844 File Offset: 0x00015A44
		internal string TitleUTF8()
		{
			return Encoding.UTF8.GetString(this.Title, 0, Array.IndexOf<byte>(this.Title, 0));
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00017863 File Offset: 0x00015A63
		internal string DescriptionUTF8()
		{
			return Encoding.UTF8.GetString(this.Description, 0, Array.IndexOf<byte>(this.Description, 0));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00017882 File Offset: 0x00015A82
		internal string TagsUTF8()
		{
			return Encoding.UTF8.GetString(this.Tags, 0, Array.IndexOf<byte>(this.Tags, 0));
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000178A1 File Offset: 0x00015AA1
		internal string PchFileNameUTF8()
		{
			return Encoding.UTF8.GetString(this.PchFileName, 0, Array.IndexOf<byte>(this.PchFileName, 0));
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000178C0 File Offset: 0x00015AC0
		internal string URLUTF8()
		{
			return Encoding.UTF8.GetString(this.URL, 0, Array.IndexOf<byte>(this.URL, 0));
		}

		// Token: 0x04000B6D RID: 2925
		internal PublishedFileId PublishedFileId;

		// Token: 0x04000B6E RID: 2926
		internal Result Result;

		// Token: 0x04000B6F RID: 2927
		internal WorkshopFileType FileType;

		// Token: 0x04000B70 RID: 2928
		internal AppId CreatorAppID;

		// Token: 0x04000B71 RID: 2929
		internal AppId ConsumerAppID;

		// Token: 0x04000B72 RID: 2930
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		internal byte[] Title;

		// Token: 0x04000B73 RID: 2931
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		internal byte[] Description;

		// Token: 0x04000B74 RID: 2932
		internal ulong SteamIDOwner;

		// Token: 0x04000B75 RID: 2933
		internal uint TimeCreated;

		// Token: 0x04000B76 RID: 2934
		internal uint TimeUpdated;

		// Token: 0x04000B77 RID: 2935
		internal uint TimeAddedToUserList;

		// Token: 0x04000B78 RID: 2936
		internal RemoteStoragePublishedFileVisibility Visibility;

		// Token: 0x04000B79 RID: 2937
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x04000B7A RID: 2938
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse;

		// Token: 0x04000B7B RID: 2939
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated;

		// Token: 0x04000B7C RID: 2940
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		internal byte[] Tags;

		// Token: 0x04000B7D RID: 2941
		internal ulong File;

		// Token: 0x04000B7E RID: 2942
		internal ulong PreviewFile;

		// Token: 0x04000B7F RID: 2943
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		internal byte[] PchFileName;

		// Token: 0x04000B80 RID: 2944
		internal int FileSize;

		// Token: 0x04000B81 RID: 2945
		internal int PreviewFileSize;

		// Token: 0x04000B82 RID: 2946
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] URL;

		// Token: 0x04000B83 RID: 2947
		internal uint VotesUp;

		// Token: 0x04000B84 RID: 2948
		internal uint VotesDown;

		// Token: 0x04000B85 RID: 2949
		internal float Score;

		// Token: 0x04000B86 RID: 2950
		internal uint NumChildren;
	}
}
