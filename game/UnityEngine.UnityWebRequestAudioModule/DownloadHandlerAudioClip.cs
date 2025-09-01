using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityWebRequestAudio/Public/DownloadHandlerAudioClip.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAudioClip : DownloadHandler
	{
		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerAudioClip obj, string url, AudioType audioType);

		// Token: 0x06000006 RID: 6 RVA: 0x000020C7 File Offset: 0x000002C7
		private void InternalCreateAudioClip(string url, AudioType audioType)
		{
			this.m_Ptr = DownloadHandlerAudioClip.Create(this, url, audioType);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		public DownloadHandlerAudioClip(string url, AudioType audioType)
		{
			this.InternalCreateAudioClip(url, audioType);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020EB File Offset: 0x000002EB
		public DownloadHandlerAudioClip(Uri uri, AudioType audioType)
		{
			this.InternalCreateAudioClip(uri.AbsoluteUri, audioType);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002104 File Offset: 0x00000304
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002122 File Offset: 0x00000322
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002138 File Offset: 0x00000338
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for audio clips");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12
		[NativeThrows]
		public extern AudioClip audioClip { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13
		// (set) Token: 0x0600000E RID: 14
		public extern bool streamAudio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x06000010 RID: 16
		public extern bool compressed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002148 File Offset: 0x00000348
		public static AudioClip GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAudioClip>(www).audioClip;
		}

		// Token: 0x04000001 RID: 1
		private NativeArray<byte> m_NativeData;
	}
}
