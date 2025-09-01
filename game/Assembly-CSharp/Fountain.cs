using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class Fountain : MonoBehaviour
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00038560 File Offset: 0x00036760
	// (set) Token: 0x060007EA RID: 2026 RVA: 0x00038567 File Offset: 0x00036767
	public static bool voteWaiting
	{
		[CompilerGenerated]
		get
		{
			return Fountain.<voteWaiting>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Fountain.<voteWaiting>k__BackingField = value;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x0003856F File Offset: 0x0003676F
	public static ScrollManager ScrollManager
	{
		get
		{
			Fountain fountain = Fountain.instance;
			if (fountain == null)
			{
				return null;
			}
			return fountain.scrollManager;
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00038581 File Offset: 0x00036781
	private void Awake()
	{
		Fountain.instance = this;
		Fountain.voteWaiting = false;
		this.UpdateFountainLoc(null);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00038596 File Offset: 0x00036796
	public void UpdateFountainLoc(Transform t = null)
	{
		if (t == null)
		{
			t = base.transform;
		}
		Shader.SetGlobalVector("_FountainLoc", t.position);
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000385C0 File Offset: 0x000367C0
	private void Update()
	{
		this.Interaction.IsInteractable = this.CanInteract();
		if (this.ChapterCompletePlayerFX != null && this.ChapterCompletePlayerFX.isPlaying)
		{
			this.ChapterCompletePlayerFX.transform.position = PlayerControl.myInstance.display.GetLocation(ActionLocation.Floor).position;
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0003861E File Offset: 0x0003681E
	public void TryStartVote()
	{
		if (VoteManager.CurrentState == VoteState.VotePrepared)
		{
			VoteManager.RequestPrepared();
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0003862D File Offset: 0x0003682D
	public static int GetRandom(int min, int max)
	{
		return UnityEngine.Random.Range(min, max);
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00038636 File Offset: 0x00036836
	private void PulseDelayed()
	{
		base.Invoke("ChapterCompletePulse", 2.5f);
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00038648 File Offset: 0x00036848
	public void ChapterCompletePulse()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.ChapterCompletePlayerFX.transform.position = PlayerControl.myInstance.display.GetLocation(ActionLocation.Floor).position;
		this.ChapterCompletePlayerFX.Play();
		AudioManager.PlayLoudSFX2D(this.ChapterCompleteSFX, 1f, 0.1f);
		PostFXManager.instance.DoPulse();
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x000386B2 File Offset: 0x000368B2
	public void FountainLayerAdded()
	{
		this.DepositSuckFX.Play();
		this.DepositStartAudio.Play();
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x000386CC File Offset: 0x000368CC
	public void FountainPowerAdded(AugmentTree augment)
	{
		Fountain.FountainPulse pulse = this.GetPulse(45);
		if (pulse == null)
		{
			return;
		}
		pulse.PulseFX.Play();
		AudioManager.PlayClipAtPoint(pulse.Clip, base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 0.8f, 5f, 25f);
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0003872C File Offset: 0x0003692C
	private Fountain.FountainPulse GetPulse(int Amount)
	{
		for (int i = this.Pulses.Count - 1; i >= 0; i--)
		{
			if (this.Pulses[i].InkThreshold <= Amount)
			{
				return this.Pulses[i];
			}
		}
		return null;
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00038773 File Offset: 0x00036973
	public bool CanInteract()
	{
		return false;
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00038776 File Offset: 0x00036976
	public Fountain()
	{
	}

	// Token: 0x040006AF RID: 1711
	public static Fountain instance;

	// Token: 0x040006B0 RID: 1712
	public AudioSource DepositStartAudio;

	// Token: 0x040006B1 RID: 1713
	public ParticleSystem DepositSuckFX;

	// Token: 0x040006B2 RID: 1714
	public List<Fountain.FountainPulse> Pulses;

	// Token: 0x040006B3 RID: 1715
	public ParticleSystem WaveTornado_Base;

	// Token: 0x040006B4 RID: 1716
	public ParticleSystem WaveTornado_Extra;

	// Token: 0x040006B5 RID: 1717
	public ParticleSystem WavePulse;

	// Token: 0x040006B6 RID: 1718
	public AudioSource WaveTornadoSFX;

	// Token: 0x040006B7 RID: 1719
	public AudioClip ChapterCompleteSFX;

	// Token: 0x040006B8 RID: 1720
	public ParticleSystem ChapterCompletePlayerFX;

	// Token: 0x040006B9 RID: 1721
	public Transform ScrollRewardLoc;

	// Token: 0x040006BA RID: 1722
	[CompilerGenerated]
	private static bool <voteWaiting>k__BackingField;

	// Token: 0x040006BB RID: 1723
	public AudioClip ExitReadySFX;

	// Token: 0x040006BC RID: 1724
	public FountainIndicator Indicator;

	// Token: 0x040006BD RID: 1725
	public FountainInteraction Interaction;

	// Token: 0x040006BE RID: 1726
	[SerializeField]
	private ScrollManager scrollManager;

	// Token: 0x040006BF RID: 1727
	public GameObject ExitIndicator;

	// Token: 0x020004B4 RID: 1204
	[Serializable]
	public class FountainPulse
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x000C6DEB File Offset: 0x000C4FEB
		public FountainPulse()
		{
		}

		// Token: 0x04002415 RID: 9237
		public int InkThreshold;

		// Token: 0x04002416 RID: 9238
		public ParticleSystem PulseFX;

		// Token: 0x04002417 RID: 9239
		public AudioClip Clip;
	}
}
