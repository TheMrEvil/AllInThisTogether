using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class SignatureInkUIControl : MonoBehaviour
{
	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000691A9 File Offset: 0x000673A9
	public static bool MyPlayerPrestiging
	{
		get
		{
			return SignatureInkUIControl.instance != null && SignatureInkUIControl.instance.IsMyPlayerInPrestige && SignatureInkUIControl.IsInPrestige;
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000691CC File Offset: 0x000673CC
	private void Awake()
	{
		SignatureInkUIControl.instance = this;
		SignatureInkUIControl.IsInPrestige = false;
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
		Progression.OnPrestige = (Action)Delegate.Combine(Progression.OnPrestige, new Action(this.UpdateDisplays));
		this.UpdateDisplays();
		foreach (SignatureInkUIControl.InkDisplay inkDisplay in this.Orbitals)
		{
			inkDisplay.Rotator.transform.localEulerAngles = new Vector3(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
		}
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0006929C File Offset: 0x0006749C
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		if (to == PanelType.SignatureInk)
		{
			PlayerControl.myInstance.Input.OverrideCamera(this.MetaAnchor, 6f, true);
			return;
		}
		if (to == PanelType.InkCores && !Library_RaidControl.IsInAntechamber)
		{
			PlayerControl.myInstance.Input.OverrideCamera(this.CoreAnchor, 6f, true);
			return;
		}
		if (to == PanelType.InkTalents)
		{
			PlayerControl.myInstance.Input.OverrideCamera(this.TalentAnchor, 6f, true);
			return;
		}
		if (!SignatureInkUIControl.IsInPrestige && (from == PanelType.SignatureInk || from == PanelType.InkCores || from == PanelType.InkTalents))
		{
			PlayerControl.myInstance.Input.ReturnCamera(6f, true);
			base.StopAllCoroutines();
			this.UpdateDisplays();
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0006934A File Offset: 0x0006754A
	public void DebugPrestigeSequence()
	{
		this.BeginPrestigeSequence(true);
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00069354 File Offset: 0x00067554
	public void BeginPrestigeSequence(bool isMyPlayer)
	{
		if (SignatureInkUIControl.IsInPrestige)
		{
			return;
		}
		this.IsMyPlayerInPrestige = isMyPlayer;
		SignatureInkUIControl.IsInPrestige = true;
		if (isMyPlayer)
		{
			PanelManager.instance.GoToPanel(PanelType.GameInvisible);
			StateManager.instance.RoomPrestige();
			GenreUIControl.UpdateBookDisplays();
		}
		else if (PanelManager.CurPanel != PanelType.GameInvisible)
		{
			PanelManager.instance.GoToPanel(PanelType.GameInvisible);
		}
		base.StopAllCoroutines();
		base.StartCoroutine("PrestigeSequence");
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x000693BA File Offset: 0x000675BA
	private IEnumerator PrestigeSequence()
	{
		Unlockable prestigeReward = null;
		if (this.IsMyPlayerInPrestige)
		{
			PlayerControl.myInstance.Input.OverrideCamera(this.PrestigeCameraAnchor, 0.8f, true);
			AudioManager.PlaySFX2D(this.PrestigeUIStartSFX, 1f, 0.1f);
			prestigeReward = UnlockDB.GetPrestigeRewardDisplay(Progression.PrestigeCount);
		}
		this.PrestigeSFX.Play();
		this.Prestige_RampUp_VFX.Play();
		yield return new WaitForSeconds(3.3f);
		PostFXManager.instance.ActivateSketch(false, false);
		yield return new WaitForSeconds(1.4f);
		this.Prestige_PlayerWarp_VFX.Play();
		yield return new WaitForSeconds(0.8f);
		if (this.IsMyPlayerInPrestige)
		{
			PlayerControl.myInstance.Input.CacheCameraLoc();
			PlayerControl.myInstance.Movement.SetPositionWithExplicitCamera(this.PrestigeTeleportLoc.position, this.PrestigeTeleportLoc.forward, this.PrestigeTeleportLocCam.forward);
			Cosmetic cosmetic = prestigeReward as Cosmetic;
			if (cosmetic != null)
			{
				PlayerControl.myInstance.Display.ChangeCosmetic(cosmetic);
				Settings.SaveOutfit();
			}
		}
		this.Prestige_Beam_VFX.Play();
		yield return new WaitForSeconds(0.3f);
		this.Prestige_FontCurtain_VFX.Play();
		yield return new WaitForSeconds(0.5f);
		PostFXManager.instance.ReleaseSketch();
		yield return new WaitForSeconds(0.3f);
		if (this.IsMyPlayerInPrestige)
		{
			PlayerControl.myInstance.Input.ReturnCamera(1f, false);
			this.UpdateDisplays();
			yield return new WaitForSeconds(2.5f);
			AudioManager.PlaySFX2D(this.PrestigeUIEndSFX, 1f, 0.1f);
			PrestigeUIIndicator.instance.Trigger(Progression.PrestigeCount, prestigeReward);
			PlayerControl.myInstance.UpdateTalents();
			yield return new WaitForSeconds(4f);
			PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaSpecialEvent, null, 1f);
		}
		this.IsMyPlayerInPrestige = false;
		SignatureInkUIControl.IsInPrestige = false;
		yield break;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x000693CC File Offset: 0x000675CC
	public void UpdateDisplays()
	{
		foreach (SignatureInkUIControl.InkDisplay inkDisplay in this.Orbitals)
		{
			PlayerDB.CoreDisplay core = PlayerDB.GetCore(inkDisplay.Color);
			if (core != null)
			{
				if (!UnlockManager.IsCoreUnlocked(core.core))
				{
					inkDisplay.Holder.SetActive(false);
				}
				else
				{
					inkDisplay.Holder.SetActive(true);
					int unlockedTalentLevel = PlayerDB.GetUnlockedTalentLevel(inkDisplay.Color);
					foreach (SignatureInkUIControl.InkDisplay.SubDisplay subDisplay in inkDisplay.LevelDisplays)
					{
						if (subDisplay.Level <= unlockedTalentLevel)
						{
							subDisplay.Effect.Play();
						}
					}
				}
			}
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000694B4 File Offset: 0x000676B4
	private SignatureInkUIControl.InkDisplay GetDisplay(MagicColor color)
	{
		foreach (SignatureInkUIControl.InkDisplay inkDisplay in this.Orbitals)
		{
			if (inkDisplay.Color == color)
			{
				return inkDisplay;
			}
		}
		return null;
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x00069510 File Offset: 0x00067710
	public void LevelUp()
	{
		AudioManager.PlaySFX2D(this.LevelSFX, 1f, 0.1f);
		this.LevelBurst.Play();
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x00069532 File Offset: 0x00067732
	public void DebugUnlocked()
	{
		this.NewCoreUnlocked(MagicColor.Yellow);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0006953C File Offset: 0x0006773C
	public void NewCoreUnlocked(MagicColor color)
	{
		SignatureInkUIControl.InkDisplay display = this.GetDisplay(color);
		if (display == null)
		{
			return;
		}
		base.StartCoroutine("InkUnlockedSequence", display);
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x00069562 File Offset: 0x00067762
	private IEnumerator InkUnlockedSequence(SignatureInkUIControl.InkDisplay display)
	{
		yield return new WaitForSeconds(0.66f);
		AudioManager.PlaySFX2D(this.StartUnlockSFX, 1f, 0.1f);
		float t = 1f;
		InkCoresPanel.instance.WantShowPanel = false;
		PlayerControl.myInstance.Display.ToggleBookUI(false);
		while (t > 0f)
		{
			t = Mathf.Lerp(t, 0f, Time.deltaTime * 2f);
			t -= Time.deltaTime;
			InkCoresPanel.instance.PanelFader.alpha = t;
			yield return true;
		}
		yield return new WaitForSeconds(0.25f);
		AudioManager.PlaySFX2D(this.UnlockSFX, 1f, 0.1f);
		this.UnlockVFX.Play();
		foreach (ParticleSystem particleSystem in display.UnlockFX)
		{
			particleSystem.Play();
		}
		yield return new WaitForSeconds(1.49f);
		float y = (float)((display.Rotator.rotation.y > 0f) ? -30 : 30);
		display.Rotator.transform.localEulerAngles = new Vector3(0f, y, 0f);
		display.Holder.SetActive(true);
		yield return new WaitForSeconds(2.5f);
		InkCoresPanel.instance.WantShowPanel = true;
		PlayerControl.myInstance.Display.ToggleBookUI(true);
		yield break;
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x00069578 File Offset: 0x00067778
	public void RandomLoadout()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null || myInstance.IsCastingAnyAbility())
		{
			return;
		}
		List<AugmentTree> unlockedCores = UnlockManager.GetUnlockedCores();
		for (int i = unlockedCores.Count - 1; i >= 0; i--)
		{
			if (unlockedCores[i].Root.magicColor == MagicColor.Neutral)
			{
				unlockedCores.RemoveAt(i);
			}
		}
		List<AbilityTree> unlockedAbilities = UnlockManager.GetUnlockedAbilities();
		List<AbilityTree> list = new List<AbilityTree>();
		List<AbilityTree> list2 = new List<AbilityTree>();
		List<AbilityTree> list3 = new List<AbilityTree>();
		foreach (AbilityTree abilityTree in unlockedAbilities)
		{
			if (abilityTree.Root.PlrAbilityType == PlayerAbilityType.Primary)
			{
				list.Add(abilityTree);
			}
			if (abilityTree.Root.PlrAbilityType == PlayerAbilityType.Secondary)
			{
				list2.Add(abilityTree);
			}
			if (abilityTree.Root.PlrAbilityType == PlayerAbilityType.Movement)
			{
				list3.Add(abilityTree);
			}
		}
		AugmentTree core = unlockedCores[UnityEngine.Random.Range(0, unlockedCores.Count)];
		AbilityTree abilityTree2 = list[UnityEngine.Random.Range(0, list.Count)];
		AbilityTree abilityTree3 = list2[UnityEngine.Random.Range(0, list2.Count)];
		AbilityTree abilityTree4 = list3[UnityEngine.Random.Range(0, list3.Count)];
		myInstance.actions.SetCore(core);
		myInstance.actions.LoadAbility(PlayerAbilityType.Primary, abilityTree2.Root.guid, false);
		myInstance.actions.LoadAbility(PlayerAbilityType.Secondary, abilityTree3.Root.guid, false);
		myInstance.actions.LoadAbility(PlayerAbilityType.Movement, abilityTree4.Root.guid, false);
		List<Cosmetic> unlockedCosmetics = UnlockManager.GetUnlockedCosmetics(CosmeticType.Head);
		List<Cosmetic> unlockedCosmetics2 = UnlockManager.GetUnlockedCosmetics(CosmeticType.Skin);
		List<Cosmetic> unlockedCosmetics3 = UnlockManager.GetUnlockedCosmetics(CosmeticType.Book);
		myInstance.Display.ChangeCosmetic(unlockedCosmetics[UnityEngine.Random.Range(0, unlockedCosmetics.Count)]);
		myInstance.Display.ChangeCosmetic(unlockedCosmetics2[UnityEngine.Random.Range(0, unlockedCosmetics2.Count)]);
		myInstance.Display.ChangeCosmetic(unlockedCosmetics3[UnityEngine.Random.Range(0, unlockedCosmetics3.Count)]);
		this.RandomLoadoutFX.transform.position = PlayerControl.myInstance.Display.GetLocation(ActionLocation.Floor).position;
		this.RandomLoadoutFX.Play();
		AudioManager.PlayInterfaceSFX(this.RandomLoadoutSFX, 1f, 0f);
		Settings.UpdateEquippedFullLoadout(-1);
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000697DC File Offset: 0x000679DC
	private void OnDestroy()
	{
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Remove(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
		Progression.OnPrestige = (Action)Delegate.Remove(Progression.OnPrestige, new Action(this.UpdateDisplays));
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0006982F File Offset: 0x00067A2F
	public SignatureInkUIControl()
	{
	}

	// Token: 0x04000F35 RID: 3893
	public static SignatureInkUIControl instance;

	// Token: 0x04000F36 RID: 3894
	public static bool IsInPrestige;

	// Token: 0x04000F37 RID: 3895
	public Transform MetaAnchor;

	// Token: 0x04000F38 RID: 3896
	public Transform CoreAnchor;

	// Token: 0x04000F39 RID: 3897
	public Transform TalentAnchor;

	// Token: 0x04000F3A RID: 3898
	public float MoveTime = 3f;

	// Token: 0x04000F3B RID: 3899
	public AudioClip LevelStartSFX;

	// Token: 0x04000F3C RID: 3900
	public AudioClip LevelSFX;

	// Token: 0x04000F3D RID: 3901
	public ParticleSystem LevelupVFX;

	// Token: 0x04000F3E RID: 3902
	public ParticleSystem LevelCharge;

	// Token: 0x04000F3F RID: 3903
	public ParticleSystem LevelBurst;

	// Token: 0x04000F40 RID: 3904
	public AudioSource ProgressLoop;

	// Token: 0x04000F41 RID: 3905
	public AudioClip StartUnlockSFX;

	// Token: 0x04000F42 RID: 3906
	public AudioClip UnlockSFX;

	// Token: 0x04000F43 RID: 3907
	public ParticleSystem UnlockVFX;

	// Token: 0x04000F44 RID: 3908
	public List<SignatureInkUIControl.InkDisplay> Orbitals = new List<SignatureInkUIControl.InkDisplay>();

	// Token: 0x04000F45 RID: 3909
	public Transform PrestigeCameraAnchor;

	// Token: 0x04000F46 RID: 3910
	public Transform PrestigeTeleportLoc;

	// Token: 0x04000F47 RID: 3911
	public Transform PrestigeTeleportLocCam;

	// Token: 0x04000F48 RID: 3912
	public AudioSource PrestigeSFX;

	// Token: 0x04000F49 RID: 3913
	public AudioClip PrestigeUIStartSFX;

	// Token: 0x04000F4A RID: 3914
	public AudioClip PrestigeUIEndSFX;

	// Token: 0x04000F4B RID: 3915
	public ParticleSystem Prestige_RampUp_VFX;

	// Token: 0x04000F4C RID: 3916
	public ParticleSystem Prestige_PlayerWarp_VFX;

	// Token: 0x04000F4D RID: 3917
	public ParticleSystem Prestige_Beam_VFX;

	// Token: 0x04000F4E RID: 3918
	public ParticleSystem Prestige_FontCurtain_VFX;

	// Token: 0x04000F4F RID: 3919
	private bool IsMyPlayerInPrestige;

	// Token: 0x04000F50 RID: 3920
	public ParticleSystem RandomLoadoutFX;

	// Token: 0x04000F51 RID: 3921
	public AudioClip RandomLoadoutSFX;

	// Token: 0x02000560 RID: 1376
	[Serializable]
	public class InkDisplay
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x000CEA17 File Offset: 0x000CCC17
		public InkDisplay()
		{
		}

		// Token: 0x040026E6 RID: 9958
		public MagicColor Color;

		// Token: 0x040026E7 RID: 9959
		public GameObject Holder;

		// Token: 0x040026E8 RID: 9960
		public RotateTimed Rotator;

		// Token: 0x040026E9 RID: 9961
		public List<SignatureInkUIControl.InkDisplay.SubDisplay> LevelDisplays = new List<SignatureInkUIControl.InkDisplay.SubDisplay>();

		// Token: 0x040026EA RID: 9962
		public List<ParticleSystem> UnlockFX;

		// Token: 0x020006C1 RID: 1729
		[Serializable]
		public class SubDisplay
		{
			// Token: 0x06002865 RID: 10341 RVA: 0x000D8920 File Offset: 0x000D6B20
			public SubDisplay()
			{
			}

			// Token: 0x04002CDF RID: 11487
			public int Level;

			// Token: 0x04002CE0 RID: 11488
			public ParticleSystem Effect;
		}
	}

	// Token: 0x02000561 RID: 1377
	[CompilerGenerated]
	private sealed class <PrestigeSequence>d__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024A7 RID: 9383 RVA: 0x000CEA2A File Offset: 0x000CCC2A
		[DebuggerHidden]
		public <PrestigeSequence>d__35(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000CEA39 File Offset: 0x000CCC39
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000CEA3C File Offset: 0x000CCC3C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SignatureInkUIControl signatureInkUIControl = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				prestigeReward = null;
				if (signatureInkUIControl.IsMyPlayerInPrestige)
				{
					PlayerControl.myInstance.Input.OverrideCamera(signatureInkUIControl.PrestigeCameraAnchor, 0.8f, true);
					AudioManager.PlaySFX2D(signatureInkUIControl.PrestigeUIStartSFX, 1f, 0.1f);
					prestigeReward = UnlockDB.GetPrestigeRewardDisplay(Progression.PrestigeCount);
				}
				signatureInkUIControl.PrestigeSFX.Play();
				signatureInkUIControl.Prestige_RampUp_VFX.Play();
				this.<>2__current = new WaitForSeconds(3.3f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				PostFXManager.instance.ActivateSketch(false, false);
				this.<>2__current = new WaitForSeconds(1.4f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				signatureInkUIControl.Prestige_PlayerWarp_VFX.Play();
				this.<>2__current = new WaitForSeconds(0.8f);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				if (signatureInkUIControl.IsMyPlayerInPrestige)
				{
					PlayerControl.myInstance.Input.CacheCameraLoc();
					PlayerControl.myInstance.Movement.SetPositionWithExplicitCamera(signatureInkUIControl.PrestigeTeleportLoc.position, signatureInkUIControl.PrestigeTeleportLoc.forward, signatureInkUIControl.PrestigeTeleportLocCam.forward);
					Cosmetic cosmetic = prestigeReward as Cosmetic;
					if (cosmetic != null)
					{
						PlayerControl.myInstance.Display.ChangeCosmetic(cosmetic);
						Settings.SaveOutfit();
					}
				}
				signatureInkUIControl.Prestige_Beam_VFX.Play();
				this.<>2__current = new WaitForSeconds(0.3f);
				this.<>1__state = 4;
				return true;
			case 4:
				this.<>1__state = -1;
				signatureInkUIControl.Prestige_FontCurtain_VFX.Play();
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 5;
				return true;
			case 5:
				this.<>1__state = -1;
				PostFXManager.instance.ReleaseSketch();
				this.<>2__current = new WaitForSeconds(0.3f);
				this.<>1__state = 6;
				return true;
			case 6:
				this.<>1__state = -1;
				if (signatureInkUIControl.IsMyPlayerInPrestige)
				{
					PlayerControl.myInstance.Input.ReturnCamera(1f, false);
					signatureInkUIControl.UpdateDisplays();
					this.<>2__current = new WaitForSeconds(2.5f);
					this.<>1__state = 7;
					return true;
				}
				break;
			case 7:
				this.<>1__state = -1;
				AudioManager.PlaySFX2D(signatureInkUIControl.PrestigeUIEndSFX, 1f, 0.1f);
				PrestigeUIIndicator.instance.Trigger(Progression.PrestigeCount, prestigeReward);
				PlayerControl.myInstance.UpdateTalents();
				this.<>2__current = new WaitForSeconds(4f);
				this.<>1__state = 8;
				return true;
			case 8:
				this.<>1__state = -1;
				PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaSpecialEvent, null, 1f);
				break;
			default:
				return false;
			}
			signatureInkUIControl.IsMyPlayerInPrestige = false;
			SignatureInkUIControl.IsInPrestige = false;
			return false;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000CED0B File Offset: 0x000CCF0B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000CED13 File Offset: 0x000CCF13
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000CED1A File Offset: 0x000CCF1A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026EB RID: 9963
		private int <>1__state;

		// Token: 0x040026EC RID: 9964
		private object <>2__current;

		// Token: 0x040026ED RID: 9965
		public SignatureInkUIControl <>4__this;

		// Token: 0x040026EE RID: 9966
		private Unlockable <prestigeReward>5__2;
	}

	// Token: 0x02000562 RID: 1378
	[CompilerGenerated]
	private sealed class <InkUnlockedSequence>d__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024AD RID: 9389 RVA: 0x000CED22 File Offset: 0x000CCF22
		[DebuggerHidden]
		public <InkUnlockedSequence>d__41(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000CED31 File Offset: 0x000CCF31
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000CED34 File Offset: 0x000CCF34
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SignatureInkUIControl signatureInkUIControl = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.66f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlaySFX2D(signatureInkUIControl.StartUnlockSFX, 1f, 0.1f);
				t = 1f;
				InkCoresPanel.instance.WantShowPanel = false;
				PlayerControl.myInstance.Display.ToggleBookUI(false);
				break;
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				AudioManager.PlaySFX2D(signatureInkUIControl.UnlockSFX, 1f, 0.1f);
				signatureInkUIControl.UnlockVFX.Play();
				foreach (ParticleSystem particleSystem in display.UnlockFX)
				{
					particleSystem.Play();
				}
				this.<>2__current = new WaitForSeconds(1.49f);
				this.<>1__state = 4;
				return true;
			case 4:
			{
				this.<>1__state = -1;
				float y = (float)((display.Rotator.rotation.y > 0f) ? -30 : 30);
				display.Rotator.transform.localEulerAngles = new Vector3(0f, y, 0f);
				display.Holder.SetActive(true);
				this.<>2__current = new WaitForSeconds(2.5f);
				this.<>1__state = 5;
				return true;
			}
			case 5:
				this.<>1__state = -1;
				InkCoresPanel.instance.WantShowPanel = true;
				PlayerControl.myInstance.Display.ToggleBookUI(true);
				return false;
			default:
				return false;
			}
			if (t <= 0f)
			{
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 3;
				return true;
			}
			t = Mathf.Lerp(t, 0f, Time.deltaTime * 2f);
			t -= Time.deltaTime;
			InkCoresPanel.instance.PanelFader.alpha = t;
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000CEF84 File Offset: 0x000CD184
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000CEF8C File Offset: 0x000CD18C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000CEF93 File Offset: 0x000CD193
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026EF RID: 9967
		private int <>1__state;

		// Token: 0x040026F0 RID: 9968
		private object <>2__current;

		// Token: 0x040026F1 RID: 9969
		public SignatureInkUIControl <>4__this;

		// Token: 0x040026F2 RID: 9970
		public SignatureInkUIControl.InkDisplay display;

		// Token: 0x040026F3 RID: 9971
		private float <t>5__2;
	}
}
