using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public static class WWWAudioExtensions
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002AEC File Offset: 0x00000CEC
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip()", true)]
		public static AudioClip GetAudioClip(this WWW www)
		{
			return www.GetAudioClip();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B04 File Offset: 0x00000D04
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD)
		{
			return www.GetAudioClip(threeD);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B20 File Offset: 0x00000D20
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream)
		{
			return www.GetAudioClip(threeD, stream);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B3C File Offset: 0x00000D3C
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream, AudioType audioType)
		{
			return www.GetAudioClip(threeD, stream, audioType);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B58 File Offset: 0x00000D58
		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed()", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www)
		{
			return www.GetAudioClipCompressed();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B70 File Offset: 0x00000D70
		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD)
		{
			return www.GetAudioClipCompressed(threeD);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B8C File Offset: 0x00000D8C
		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD, AudioType audioType)
		{
			return www.GetAudioClipCompressed(threeD, audioType);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[Obsolete("WWWAudioExtensions.GetMovieTexture extension method has been replaced by WWW.GetMovieTexture instance method. (UnityUpgradable) -> WWW.GetMovieTexture()", true)]
		public static MovieTexture GetMovieTexture(this WWW www)
		{
			return www.GetMovieTexture();
		}
	}
}
