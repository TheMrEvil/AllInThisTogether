using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	public static class UnityWebRequestMultimedia
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public static UnityWebRequest GetAudioClip(Uri uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(string uri)
		{
			return null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(Uri uri)
		{
			return null;
		}
	}
}
