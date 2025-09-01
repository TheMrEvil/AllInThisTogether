using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000176 RID: 374
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCDetails_t
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000C187 File Offset: 0x0000A387
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0000C194 File Offset: 0x0000A394
		public string m_rgchTitle
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTitle_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTitle_, 129);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public string m_rgchDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchDescription_, 8000);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0000C1C7 File Offset: 0x0000A3C7
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public string m_rgchTags
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTags_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTags_, 1025);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000C1E7 File Offset: 0x0000A3E7
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000C207 File Offset: 0x0000A407
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0000C214 File Offset: 0x0000A414
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x040009EA RID: 2538
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040009EB RID: 2539
		public EResult m_eResult;

		// Token: 0x040009EC RID: 2540
		public EWorkshopFileType m_eFileType;

		// Token: 0x040009ED RID: 2541
		public AppId_t m_nCreatorAppID;

		// Token: 0x040009EE RID: 2542
		public AppId_t m_nConsumerAppID;

		// Token: 0x040009EF RID: 2543
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x040009F0 RID: 2544
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x040009F1 RID: 2545
		public ulong m_ulSteamIDOwner;

		// Token: 0x040009F2 RID: 2546
		public uint m_rtimeCreated;

		// Token: 0x040009F3 RID: 2547
		public uint m_rtimeUpdated;

		// Token: 0x040009F4 RID: 2548
		public uint m_rtimeAddedToUserList;

		// Token: 0x040009F5 RID: 2549
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x040009F6 RID: 2550
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x040009F7 RID: 2551
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;

		// Token: 0x040009F8 RID: 2552
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x040009F9 RID: 2553
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x040009FA RID: 2554
		public UGCHandle_t m_hFile;

		// Token: 0x040009FB RID: 2555
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x040009FC RID: 2556
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x040009FD RID: 2557
		public int m_nFileSize;

		// Token: 0x040009FE RID: 2558
		public int m_nPreviewFileSize;

		// Token: 0x040009FF RID: 2559
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x04000A00 RID: 2560
		public uint m_unVotesUp;

		// Token: 0x04000A01 RID: 2561
		public uint m_unVotesDown;

		// Token: 0x04000A02 RID: 2562
		public float m_flScore;

		// Token: 0x04000A03 RID: 2563
		public uint m_unNumChildren;
	}
}
