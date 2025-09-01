using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerMovieTexture : DownloadHandler
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002165 File Offset: 0x00000365
		public DownloadHandlerMovieTexture()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002178 File Offset: 0x00000378
		protected override byte[] GetData()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002191 File Offset: 0x00000391
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for movies");
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021A0 File Offset: 0x000003A0
		public MovieTexture movieTexture
		{
			get
			{
				DownloadHandlerMovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021BC File Offset: 0x000003BC
		public static MovieTexture GetContent(UnityWebRequest uwr)
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021D5 File Offset: 0x000003D5
		private static void FeatureRemoved()
		{
			throw new Exception("Movie texture has been removed, use VideoPlayer instead");
		}
	}
}
