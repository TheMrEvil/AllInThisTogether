using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x0200000D RID: 13
	public class SoundReceiver : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000370A File Offset: 0x0000190A
		private AudioSource source
		{
			get
			{
				return base.GetComponent<AudioSource>();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003712 File Offset: 0x00001912
		private void Start()
		{
			base.gameObject.AddComponent<AudioSource>();
			this.source.playOnAwake = false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000372C File Offset: 0x0000192C
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.soundControllerObj == null)
			{
				this.soundControllerObj = GameObject.Find("CursorControl");
				this.soundController = this.soundControllerObj.GetComponent<SoundController>();
				base.GetComponent<AudioSource>().outputAudioMixerGroup = this.soundController.audioMixer;
			}
			if (this.playHoverSound && this.soundController.hoverSound != null)
			{
				base.gameObject.GetComponent<AudioSource>().volume = this.soundController.vol;
				base.gameObject.GetComponent<AudioSource>().pitch = this.soundController.hoverPitch;
				this.source.PlayOneShot(this.soundController.hoverSound);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000037E8 File Offset: 0x000019E8
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.playClickSound && this.soundController.clickSound != null)
			{
				base.gameObject.GetComponent<AudioSource>().pitch = this.soundController.clickPitch;
				this.source.PlayOneShot(this.soundController.clickSound);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003844 File Offset: 0x00001A44
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.playExitSound && this.soundController.exitSound != null)
			{
				base.gameObject.GetComponent<AudioSource>().pitch = this.soundController.exitPitch;
				this.source.PlayOneShot(this.soundController.exitSound);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000389D File Offset: 0x00001A9D
		public SoundReceiver()
		{
		}

		// Token: 0x04000063 RID: 99
		private GameObject soundControllerObj;

		// Token: 0x04000064 RID: 100
		private SoundController soundController;

		// Token: 0x04000065 RID: 101
		[Header("SOUND BEHAVIORS")]
		public bool playHoverSound = true;

		// Token: 0x04000066 RID: 102
		public bool playExitSound;

		// Token: 0x04000067 RID: 103
		public bool playClickSound = true;
	}
}
