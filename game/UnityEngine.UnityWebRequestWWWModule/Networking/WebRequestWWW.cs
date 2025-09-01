using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityWebRequestAudio/Public/DownloadHandlerAudioClip.h")]
	internal static class WebRequestWWW
	{
		// Token: 0x06000038 RID: 56
		[FreeFunction("UnityWebRequestCreateAudioClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AudioClip InternalCreateAudioClipUsingDH(DownloadHandler dh, string url, bool stream, bool compressed, AudioType audioType);
	}
}
