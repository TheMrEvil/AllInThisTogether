using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x0200000C RID: 12
	public class SoundController : MonoBehaviour
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000036BA File Offset: 0x000018BA
		public void PlaySound(int soundType)
		{
			this.SoundFiles = Resources.LoadAll("SlimUI/Sound", typeof(UnityEngine.Object));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036D6 File Offset: 0x000018D6
		public SoundController()
		{
		}

		// Token: 0x0400005A RID: 90
		[Header("UI AUDIO MIXER")]
		public AudioMixerGroup audioMixer;

		// Token: 0x0400005B RID: 91
		private UnityEngine.Object[] SoundFiles;

		// Token: 0x0400005C RID: 92
		[Header("SOUND SETTINGS")]
		[Range(0f, 1f)]
		public float vol = 1f;

		// Token: 0x0400005D RID: 93
		[Space]
		public AudioClip hoverSound;

		// Token: 0x0400005E RID: 94
		[Range(0.5f, 2f)]
		public float hoverPitch = 1f;

		// Token: 0x0400005F RID: 95
		[Space]
		public AudioClip clickSound;

		// Token: 0x04000060 RID: 96
		[Range(0.5f, 2f)]
		public float clickPitch = 1f;

		// Token: 0x04000061 RID: 97
		[Space]
		[Range(0.5f, 2f)]
		public float exitPitch = 1f;

		// Token: 0x04000062 RID: 98
		public AudioClip exitSound;
	}
}
