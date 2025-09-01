using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
	public sealed class MovieTexture : Texture
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00002CF4 File Offset: 0x00000EF4
		private static void FeatureRemoved()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00002D01 File Offset: 0x00000F01
		private MovieTexture()
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00002D0B File Offset: 0x00000F0B
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Play()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002D0B File Offset: 0x00000F0B
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Stop()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002D0B File Offset: 0x00000F0B
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Pause()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002D14 File Offset: 0x00000F14
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public AudioClip audioClip
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002D30 File Offset: 0x00000F30
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00002D0B File Offset: 0x00000F0B
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool loop
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
			set
			{
				MovieTexture.FeatureRemoved();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002D4C File Offset: 0x00000F4C
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isPlaying
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002D68 File Offset: 0x00000F68
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isReadyToPlay
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002D84 File Offset: 0x00000F84
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public float duration
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return 1f;
			}
		}
	}
}
