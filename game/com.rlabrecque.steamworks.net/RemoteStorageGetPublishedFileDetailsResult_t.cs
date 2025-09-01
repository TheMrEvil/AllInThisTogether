using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B5 RID: 181
	[CallbackIdentity(1318)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0000C027 File Offset: 0x0000A227
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0000C034 File Offset: 0x0000A234
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0000C047 File Offset: 0x0000A247
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0000C054 File Offset: 0x0000A254
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0000C067 File Offset: 0x0000A267
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0000C074 File Offset: 0x0000A274
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0000C087 File Offset: 0x0000A287
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0000C094 File Offset: 0x0000A294
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
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

		// Token: 0x040001FD RID: 509
		public const int k_iCallback = 1318;

		// Token: 0x040001FE RID: 510
		public EResult m_eResult;

		// Token: 0x040001FF RID: 511
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000200 RID: 512
		public AppId_t m_nCreatorAppID;

		// Token: 0x04000201 RID: 513
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000202 RID: 514
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x04000203 RID: 515
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x04000204 RID: 516
		public UGCHandle_t m_hFile;

		// Token: 0x04000205 RID: 517
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000206 RID: 518
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000207 RID: 519
		public uint m_rtimeCreated;

		// Token: 0x04000208 RID: 520
		public uint m_rtimeUpdated;

		// Token: 0x04000209 RID: 521
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x0400020A RID: 522
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x0400020B RID: 523
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x0400020C RID: 524
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x0400020D RID: 525
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x0400020E RID: 526
		public int m_nFileSize;

		// Token: 0x0400020F RID: 527
		public int m_nPreviewFileSize;

		// Token: 0x04000210 RID: 528
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x04000211 RID: 529
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000212 RID: 530
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;
	}
}
