using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using cakeslice;
using CMF;
using FIMSpace.FTail;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class PlayerDisplay : EntityDisplay
{
	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002DC71 File Offset: 0x0002BE71
	public PlayerControl Control
	{
		get
		{
			return this.control as PlayerControl;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000633 RID: 1587 RVA: 0x0002DC7E File Offset: 0x0002BE7E
	// (set) Token: 0x06000634 RID: 1588 RVA: 0x0002DC86 File Offset: 0x0002BE86
	public bool DisplayGrounded
	{
		[CompilerGenerated]
		get
		{
			return this.<DisplayGrounded>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<DisplayGrounded>k__BackingField = value;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x0002DC90 File Offset: 0x0002BE90
	public global::Pose CenterLocation
	{
		get
		{
			return new global::Pose(new Location
			{
				LocType = LocationType.Entity,
				AtPoint = ActionLocation.CenterOfMass,
				EntityRef = CenteredOn.Source
			}, new Location
			{
				LocType = LocationType.WorldPoint,
				WorldPoint = this.CenterOfMass.position + this.Control.movement.GetForward()
			});
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
	public override void Awake()
	{
		base.Awake();
		this.CurSet = new CosmeticSet();
		this.flashBlock = new MaterialPropertyBlock();
		this.headMesh = this.HeadRoot.GetComponentInChildren<MeshRenderer>();
		this.curHead = this.HeadRoot.GetChild(0);
		this.ChangeHead(CosmeticDB.DB.DefaultHead);
		this.ChangeSkin(CosmeticDB.DB.DefaultSkin);
		this.ChangeBook(CosmeticDB.DB.DefaultBook);
		this.ChangeBack(CosmeticDB.DB.DefaultSignature);
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0002DD7C File Offset: 0x0002BF7C
	public override void Setup()
	{
		this.displayHolder = this.DisplayHolder;
		base.Setup();
		int layer = GameplayManager.GetLayer(this.control);
		EntityDisplay.SetLayerRecursively(base.gameObject, layer);
		this.CanvasObj.SetActive(!this.control.IsMine);
		this.UpdateCoreDisplay();
		EntityHealth health = this.Control.health;
		health.OnShieldChargeStart = (Action)Delegate.Combine(health.OnShieldChargeStart, new Action(this.OnShieldChargeSarted));
		EntityHealth health2 = this.Control.health;
		health2.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health2.OnDamageTaken, new Action<DamageInfo>(this.DamageAnim));
		PlayerMovement movement = this.Control.Movement;
		movement.Jumped = (Action)Delegate.Combine(movement.Jumped, new Action(this.OnJumped));
		PlayerActions actions = this.Control.actions;
		actions.coreChanged = (Action<MagicColor>)Delegate.Combine(actions.coreChanged, new Action<MagicColor>(this.OnCoreChanged));
		GameHUD instance = GameHUD.instance;
		instance.OnHUDModeChanged = (Action)Delegate.Combine(instance.OnHUDModeChanged, new Action(this.OnHUDModeChanged));
		if (this.Control.IsMine)
		{
			PanelManager instance2 = PanelManager.instance;
			instance2.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(instance2.OnPanelChanged, new Action<PanelType, PanelType>(this.OnUIPanelChanged));
			this.ChangeCosmeticSet(Settings.GetOutfit());
		}
		this.SpawnFX.gameObject.SetActive(true);
		this.SpawnFX.transform.SetParent(null);
		this.RangeDisplay.Hide();
		this.RangeDisplay.SetOverrideScaling(true);
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0002DF1F File Offset: 0x0002C11F
	private IEnumerator Start()
	{
		if (this.control.IsMine)
		{
			MapManager.ApplyCameraEffects();
		}
		yield return true;
		if (!this.control.IsMine)
		{
			this.ToggleOutline();
			this.OnCoreChanged(this.Control.actions.core.Root.magicColor);
		}
		yield break;
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0002DF30 File Offset: 0x0002C130
	internal override void Update()
	{
		base.Update();
		if (this.Control.IsMine)
		{
			this.CameraPivot.transform.localPosition = Vector3.up * this.DisplayHolder.localScale.x * 0.95f;
		}
		else if (this.Control.IsSpectator)
		{
			this.SpectatorCamDisplay.gameObject.SetActive(this.ShowSpectatorCam);
			if (this.ShowSpectatorCam)
			{
				this.SpectatorCamDisplay.position = Vector3.Lerp(this.SpectatorCamDisplay.position, this.SpectatorPos, Time.deltaTime * 10f);
				this.SpectatorCamDisplay.rotation = Quaternion.Lerp(this.SpectatorCamDisplay.rotation, this.SpectatorRot, Time.deltaTime * 10f);
			}
		}
		bool flag = this.Control.actions.GetAbility(PlayerAbilityType.Utility).IsOnCooldown || PanelManager.CurPanel != PanelType.GameInvisible;
		if (flag && this.CoreReadyFX.isPlaying)
		{
			this.CoreReadyFX.Stop();
		}
		else if (!flag && !this.CoreReadyFX.isPlaying)
		{
			this.CoreReadyFX.Play();
		}
		this.UpdateAnimValues();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0002E071 File Offset: 0x0002C271
	private void LateUpdate()
	{
		this.GroundFXContact();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0002E07C File Offset: 0x0002C27C
	private void UpdateAnimValues()
	{
		this.PlayerAnims.speed = (PausePanel.IsGamePaused ? 0.0001f : Mathf.Lerp(this.PlayerAnims.speed, this.DisplayMoveSpeed, Time.deltaTime * 10f));
		this.BookAnims.speed = Mathf.Lerp(this.BookAnims.speed, this.DisplayMoveSpeed, Time.deltaTime * 10f);
		this.RootAnims.speed = (PausePanel.IsGamePaused ? 0.0001f : Mathf.Lerp(this.RootAnims.speed, this.DisplayMoveSpeed, Time.deltaTime * 10f));
		Vector3 velocity = this.Control.Movement.GetVelocity();
		Vector3 forward = this.Control.Movement.GetForward();
		Vector3 up = Vector3.up;
		Vector3 vector = Vector3.ProjectOnPlane(velocity.TransformDirection(up, forward), Vector3.up) / this.Control.Movement.BaseSpeed;
		this.velRef = velocity;
		this.transfVelRef = vector;
		float @float = this.PlayerAnims.GetFloat("Move_X");
		float float2 = this.PlayerAnims.GetFloat("Move_Y");
		this.PlayerAnims.SetFloat("Move_X", Mathf.Lerp(@float, -vector.x, Time.deltaTime * 3f));
		this.PlayerAnims.SetFloat("Move_Y", Mathf.Lerp(float2, vector.z, Time.deltaTime * 3f));
		if (this.Control.Movement.input.movementAxis.magnitude > 0.1f)
		{
			this.CancelEmote();
		}
		float layerWeight = this.PlayerAnims.GetLayerWeight(3);
		float layerWeight2 = this.PlayerAnims.GetLayerWeight(4);
		if ((this.curEmote == null || !this.curEmote.UseLegs) && layerWeight > 0.01f)
		{
			this.PlayerAnims.SetLayerWeight(3, Mathf.Lerp(layerWeight, 0f, Time.deltaTime * 3f));
		}
		if ((this.curEmote == null || this.curEmote.UseLegs) && layerWeight2 > 0.01f)
		{
			this.PlayerAnims.SetLayerWeight(4, Mathf.Lerp(layerWeight2, 0f, Time.deltaTime * 3f));
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
	public void PlayAbilityAnim(string animName, float crossfadeTime, PlayerAbilityType AbilityType, bool stopBonePhys = false)
	{
		this.CancelEmote();
		Animator animator = this.PlayerAnims;
		if (AbilityType == PlayerAbilityType.Ghost || animName.Contains("Book"))
		{
			animator = this.BookAnims;
		}
		if (stopBonePhys)
		{
			this.DisableBonePhys(0.75f);
		}
		int layer = 1;
		if (animName == "Jump")
		{
			layer = 0;
		}
		if (crossfadeTime == 0f)
		{
			animator.Play(animName, layer, 0f);
			return;
		}
		animator.CrossFade(animName, crossfadeTime, layer, 0f);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0002E33C File Offset: 0x0002C53C
	private void DisableBonePhys(float duration)
	{
		foreach (TailAnimator2 tailAnimator in this.BonePhysics)
		{
			tailAnimator.TailAnimatorAmount = 0f;
			tailAnimator.enabled = false;
		}
		if (this.ReactRoutine != null)
		{
			base.StopCoroutine(this.ReactRoutine);
		}
		this.ReactRoutine = base.StartCoroutine(this.ReactivateBonePhys(duration));
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
	private IEnumerator ReactivateBonePhys(float delay)
	{
		float t = 0f;
		while (t < delay)
		{
			t += GameplayManager.deltaTime;
			yield return true;
		}
		foreach (TailAnimator2 tailAnimator in this.BonePhysics)
		{
			tailAnimator.TailAnimatorAmount = 0f;
			tailAnimator.enabled = true;
		}
		t = 0f;
		while (t < 0.5f)
		{
			t += GameplayManager.deltaTime;
			foreach (TailAnimator2 tailAnimator2 in this.BonePhysics)
			{
				tailAnimator2.TailAnimatorAmount = t * 2f;
			}
			yield return true;
		}
		using (List<TailAnimator2>.Enumerator enumerator = this.BonePhysics.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TailAnimator2 tailAnimator3 = enumerator.Current;
				tailAnimator3.TailAnimatorAmount = 1f;
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0002E3D6 File Offset: 0x0002C5D6
	public override void PlayAbilityAnim(string animName, float crossfadeTime, bool stopBonePhys)
	{
		UnityEngine.Debug.LogError("Player not allowed to use Default PlayAbilityAnim() Method -> Requires Ability Type!");
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
	public override bool IsPlayingAbilityAnim(string animName)
	{
		foreach (AnimatorClipInfo animatorClipInfo in this.PlayerAnims.GetCurrentAnimatorClipInfo(1))
		{
			if (animatorClipInfo.clip.name == animName)
			{
				return true;
			}
		}
		return this.PlayerAnims.GetCurrentAnimatorStateInfo(1).IsName(animName) || this.BookAnims.GetCurrentAnimatorStateInfo(0).IsName(animName);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002E458 File Offset: 0x0002C658
	private PlayerDisplay.DamageLevel CurDamageLevel()
	{
		AnimatorStateInfo currentAnimatorStateInfo = this.PlayerAnims.GetCurrentAnimatorStateInfo(2);
		if (currentAnimatorStateInfo.IsName("Damage_Light"))
		{
			return PlayerDisplay.DamageLevel.Light;
		}
		if (currentAnimatorStateInfo.IsName("Damage_Medium"))
		{
			return PlayerDisplay.DamageLevel.Medium;
		}
		if (currentAnimatorStateInfo.IsName("Damage_Heavy"))
		{
			return PlayerDisplay.DamageLevel.Heavy;
		}
		return PlayerDisplay.DamageLevel.None;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002E4A3 File Offset: 0x0002C6A3
	public void StopCurrentAbilityAnim(PlayerAbilityType aType)
	{
		if (this.control.health.isDead)
		{
			return;
		}
		if (aType == PlayerAbilityType.Primary)
		{
			this.PlayerAnims.SetTrigger("Cancel");
			return;
		}
		if (aType == PlayerAbilityType.Secondary)
		{
			this.BookAnims.SetTrigger("Cancel");
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
	public override void StopCurrentAbilityAnim()
	{
		this.PlayerAnims.SetTrigger("Cancel");
		this.BookAnims.SetTrigger("Cancel");
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002E504 File Offset: 0x0002C704
	public float AbilityAnimNormalizedTime(PlayerAbilityType aType)
	{
		if (aType == PlayerAbilityType.Primary)
		{
			return this.PlayerAnims.GetCurrentAnimatorStateInfo(1).normalizedTime;
		}
		if (aType == PlayerAbilityType.Secondary)
		{
			return this.BookAnims.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}
		return 0f;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002E547 File Offset: 0x0002C747
	public override float AbilityAnimNormalizedTime()
	{
		UnityEngine.Debug.LogError("Player not allowed to use Default AbilityAnimNormalizedTime() Method -> Requires Ability Type!");
		return 0f;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0002E558 File Offset: 0x0002C758
	private void OnUIPanelChanged(PanelType from, PanelType to)
	{
		if (PanelManager.curSelect.gameplayInteractable && !PanelManager.curSelect.NoBook && to != PanelType.GameInvisible)
		{
			this.ToggleBookUI(true);
			return;
		}
		this.ToggleBookUI(false);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0002E588 File Offset: 0x0002C788
	public void ToggleBookUI(bool inUI)
	{
		if (!this.control.IsMine)
		{
			return;
		}
		this.BookFollow.SetCameraFollowDistance(PanelManager.curSelect.BookOffset);
		if (inUI)
		{
			this.BookAnims.SetBool("InUI", true);
			if (!this.BookFollow.FollowCamera)
			{
				this.BookFollow.FollowCamera = true;
				AudioManager.PlayUIBook(true);
				return;
			}
		}
		else
		{
			this.BookAnims.SetBool("InUI", false);
			if (this.BookFollow.FollowCamera)
			{
				this.BookFollow.FollowCamera = false;
				AudioManager.PlayUIBook(false);
			}
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0002E61C File Offset: 0x0002C81C
	public void Emote(string emoteID)
	{
		Cosmetic_Emote emote = CosmeticDB.GetEmote(emoteID);
		if (emote == null)
		{
			return;
		}
		if (!string.IsNullOrEmpty(emote.Animation))
		{
			this.curEmote = emote;
			int num = emote.UseLegs ? 3 : 4;
			this.PlayerAnims.SetLayerWeight(num, 1f);
			this.PlayerAnims.CrossFade(emote.Animation, 0.25f, num);
		}
		if (emote.Status != null && this.Control.IsMine)
		{
			if (this.emoteStatus != null)
			{
				this.Control.Net.RemoveStatus(this.emoteStatus.HashCode, this.Control.ViewID, 0, false, true);
			}
			this.Control.Net.ApplyStatus(emote.Status.HashCode, this.Control.ViewID, emote.Duration, 0, false, 0);
			this.emoteStatus = emote.Status;
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0002E70A File Offset: 0x0002C90A
	public void CancelEmote()
	{
		if (this.curEmote == null)
		{
			return;
		}
		this.Control.Net.StopEmote();
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0002E728 File Offset: 0x0002C928
	public void StopCurrentEmote()
	{
		if (this.curEmote == null)
		{
			return;
		}
		this.curEmote = null;
		this.PlayerAnims.SetTrigger("StopEmote");
		if (this.emoteStatus != null && this.Control.IsMine)
		{
			this.Control.Net.RemoveStatus(this.emoteStatus.HashCode, this.Control.ViewID, 0, false, true);
			this.emoteStatus = null;
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
	public void ShowRange(float radius)
	{
		if (this.control != PlayerControl.myInstance || this.RangeDisplay == null || radius <= 0f)
		{
			return;
		}
		Vector3 vector = Vector3.one * radius;
		vector.y = Mathf.Clamp(vector.y, 4f, Mathf.Sqrt(radius));
		this.RangeDisplay.transform.localScale = vector * 2f;
		this.RangeDisplay.Show();
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0002E825 File Offset: 0x0002CA25
	public void ReleaseRange()
	{
		if (this.control != PlayerControl.myInstance || this.RangeDisplay == null)
		{
			return;
		}
		this.RangeDisplay.Hide();
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0002E854 File Offset: 0x0002CA54
	private void DamageAnim(DamageInfo dmg)
	{
		if (dmg.TotalAmount == 0f)
		{
			return;
		}
		PlayerDisplay.DamageLevel damageLevel = this.CurDamageLevel();
		PlayerDisplay.DamageLevel damageLevel2 = PlayerDisplay.DamageLevel.Light;
		if (dmg.TotalAmount >= 15f)
		{
			damageLevel2 = PlayerDisplay.DamageLevel.Heavy;
		}
		else if (dmg.TotalAmount >= 7f)
		{
			damageLevel2 = PlayerDisplay.DamageLevel.Medium;
		}
		if (damageLevel > damageLevel2)
		{
			return;
		}
		float num = 1f;
		if (dmg.TotalAmount > dmg.ShieldAmount)
		{
			num = 1.5f;
		}
		float weight = Mathf.Clamp(0.2f + dmg.TotalAmount / 25f * num, 0f, 1f);
		this.PlayerAnims.SetLayerWeight(2, weight);
		string text;
		switch (damageLevel2)
		{
		case PlayerDisplay.DamageLevel.Light:
			text = "Damage_Light";
			break;
		case PlayerDisplay.DamageLevel.Medium:
			text = "Damage_Medium";
			break;
		case PlayerDisplay.DamageLevel.Heavy:
			text = "Damage_Heavy";
			break;
		default:
			text = "";
			break;
		}
		string stateName = text;
		this.PlayerAnims.Play(stateName, 2);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002E930 File Offset: 0x0002CB30
	public override void DamageFlash(DamageInfo dmg)
	{
		float num = dmg.TotalAmount / 5f;
		if (num <= 0f)
		{
			return;
		}
		if (dmg.ShieldAmount > dmg.TotalAmount / 2f)
		{
			float num2 = Mathf.Clamp(this.flashBlock.GetFloat(PlayerDisplay.ShieldAmount), 0f, 4f);
			num2 += num;
			num2 = Mathf.Clamp(num2, 0f, 4f);
			this.flashBlock.SetFloat(PlayerDisplay.ShieldAmount, num2);
			return;
		}
		float num3 = Mathf.Clamp(this.flashBlock.GetFloat(PlayerDisplay.DamagePower), 0f, 4f);
		num3 += num;
		num3 = Mathf.Clamp(num3, 0f, 4f);
		this.flashBlock.SetFloat(PlayerDisplay.DamagePower, num3);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
	internal override void ReduceDamageFlash()
	{
		float num = this.flashBlock.GetFloat(PlayerDisplay.DamagePower);
		float num2 = this.flashBlock.GetFloat(PlayerDisplay.HealPower);
		float num3 = this.flashBlock.GetFloat(PlayerDisplay.ShieldAmount);
		num = Mathf.Clamp(num - Time.deltaTime * 1f, 0f, 1f);
		num2 = Mathf.Clamp(num2 - Time.deltaTime * 1f, 0f, 1f);
		float num4 = Mathf.Clamp(this.Control.health.CurrentShieldProportion, 0f, 2f);
		if (num4 <= 0f || (num4 < 1f && this.Control.health.shieldDelay > 0f) || num4 == 1f)
		{
			num4 = 0f;
		}
		num3 = Mathf.Lerp(num3, num4, Time.deltaTime * 1f);
		this.flashBlock.SetFloat(PlayerDisplay.DamagePower, num);
		this.flashBlock.SetFloat(PlayerDisplay.HealPower, num2);
		this.flashBlock.SetFloat(PlayerDisplay.ShieldAmount, num3);
		foreach (Renderer renderer in this.Meshes)
		{
			renderer.SetPropertyBlock(this.flashBlock);
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0002EB54 File Offset: 0x0002CD54
	private void OnShieldChargeSarted()
	{
		this.flashBlock.SetFloat(PlayerDisplay.ShieldAmount, 2.5f);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0002EB6C File Offset: 0x0002CD6C
	public void DebugFlash()
	{
		this.flashBlock = new MaterialPropertyBlock();
		this.flashBlock.SetFloat(PlayerDisplay.ShieldAmount, 2.5f);
		foreach (Renderer renderer in this.Meshes)
		{
			renderer.SetPropertyBlock(this.flashBlock);
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
	public void HealPulse(int amount)
	{
		float num = (float)amount / 10f;
		if (num <= 0f)
		{
			return;
		}
		float num2 = this.flashBlock.GetFloat(PlayerDisplay.HealPower);
		num2 += num;
		num2 = Mathf.Clamp(num2, 0f, 1f);
		this.flashBlock.SetFloat(PlayerDisplay.HealPower, num2);
		foreach (Renderer renderer in this.Meshes)
		{
			renderer.SetPropertyBlock(this.flashBlock);
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0002EC84 File Offset: 0x0002CE84
	public override void AddMeshFX(GameObject refObj, EffectProperties props, int MeshIndex = -1)
	{
		base.RemoveMeshFX(refObj);
		Renderer renderer = this.Meshes[0];
		if (MeshIndex >= 1)
		{
			renderer = this.BookRenderer;
		}
		if (renderer == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(refObj);
		gameObject.name = refObj.name;
		MeshFX component = gameObject.GetComponent<MeshFX>();
		component.Setup(this.control, renderer, props, refObj, 1f);
		this.AppliedMeshFX.Add(component);
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0002ECF2 File Offset: 0x0002CEF2
	public void LandAnim()
	{
		this.PlayerAnims.CrossFade("Land", 0.15f);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0002ED09 File Offset: 0x0002CF09
	public void LandFX(Vector3 point, Vector3 normal)
	{
		this.LandParticles.transform.position = point;
		this.LandParticles.transform.forward = normal;
		this.LandParticles.Play();
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0002ED38 File Offset: 0x0002CF38
	public void ToggleOutline()
	{
		bool flag = true;
		flag &= (PlayerControl.myInstance != this.Control);
		flag &= !this.Control.IsSpectator;
		flag &= (GameHUD.Mode != GameHUD.HUDMode.Off);
		flag &= !PlayerControl.myInstance.IsSpectator;
		foreach (MeshMaterialDisplay meshMaterialDisplay in this.AppliedMaterials)
		{
			flag &= !meshMaterialDisplay.baseHidden;
		}
		foreach (Outline outline in base.GetComponentsInChildren<Outline>())
		{
			outline.color = 1;
			if (flag)
			{
				outline.Activate();
			}
			else
			{
				outline.Deactivate();
			}
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0002EE10 File Offset: 0x0002D010
	private void OnHUDModeChanged()
	{
		this.ToggleOutline();
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002EE18 File Offset: 0x0002D018
	public void OnJumped()
	{
		this.CancelEmote();
		this.PlayerAnims.CrossFade("Jump", 0.05f);
		this.PlayerAnims.SetLayerWeight(2, 0.5f);
		this.PlayerAnims.CrossFade("Jump", 0.1f, 2);
		this.JumpParticles.transform.position = this.GetLocation(ActionLocation.Floor).position;
		this.JumpParticles.Play();
		this.JumpParticlesCore.Play();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002EE99 File Offset: 0x0002D099
	public void AbilityCDComplete(PlayerAbilityType aType)
	{
		if (aType == PlayerAbilityType.Movement)
		{
			this.MovementCDFX.Play();
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0002EEAC File Offset: 0x0002D0AC
	private void OnCoreChanged(MagicColor color)
	{
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(color);
		this.CoreReadyFX.main.startColor = core.ParticleColor;
		this.CoreReadyFX.GetComponent<ParticleSystemRenderer>().material = core.ParticleMaterial;
		this.PlayerLight.color = core.LightColor;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0002EF08 File Offset: 0x0002D108
	internal override void OnDeath(DamageInfo dmg)
	{
		Animator bookAnims = this.BookAnims;
		if (bookAnims != null)
		{
			bookAnims.CrossFade("Book_Ghost_Idle", 0.3f, 0, 0f);
		}
		if (this.CamFollowSmooth != null)
		{
			this.CamFollowSmooth.target = this.Control.GhostPlayerDisplay.transform;
		}
		if (this.Control != null && dmg != null)
		{
			this.Control.LastDamageTaken = dmg.TotalAmount;
			this.Control.LastDamageTakenPoint = dmg.AtPoint;
		}
		if (this.control.IsMine && dmg != null)
		{
			EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
			if (entity != null)
			{
				this.Control.LastDamageTakenPoint += (this.CenterOfMass.position - entity.display.CenterOfMass.position).normalized;
			}
			foreach (Behaviour behaviour in this.ragdollDisabledComponents)
			{
				if (behaviour != null && !(behaviour is Animator))
				{
					behaviour.enabled = false;
				}
			}
			this.DeathDelayFrame = base.StartCoroutine("DeathSequence", dmg);
			return;
		}
		base.OnDeath(dmg);
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0002F06C File Offset: 0x0002D26C
	private IEnumerator DeathSequence(DamageInfo dmg)
	{
		yield return true;
		base.OnDeath(dmg);
		this.DeathDelayFrame = null;
		yield break;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0002F084 File Offset: 0x0002D284
	internal override void OnRevived(int healthAmt)
	{
		this.BookAnims.SetTrigger("Cancel");
		if (this.CamFollowSmooth != null)
		{
			this.CamFollowSmooth.target = this.CamFollowLoc;
		}
		if (this.DeathDelayFrame != null)
		{
			base.StopCoroutine(this.DeathDelayFrame);
		}
		base.OnRevived(healthAmt);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0002F0DC File Offset: 0x0002D2DC
	private void GroundFXContact()
	{
		if (this.groundFX == null)
		{
			return;
		}
		bool flag = false;
		Vector3 vector = Vector3.zero;
		Vector3 b = Vector3.zero;
		Ray ray = new Ray(this.GroundFXFollowAnchor.position, Vector3.down);
		float num = 0.75f;
		UnityEngine.Debug.DrawLine(ray.origin, ray.origin + ray.direction * num, Color.green);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, num, this.GroundTestMask))
		{
			flag = true;
			vector = raycastHit.point;
			b = raycastHit.normal;
		}
		if (this.control.IsMine)
		{
			Controller walkerController = this.Control.Movement.walkerController;
			Mover mover = this.Control.Movement.mover;
			flag = walkerController.IsGrounded();
		}
		this.DisplayGrounded = flag;
		if (flag && vector != Vector3.zero)
		{
			this.GroundFXHolder.position = vector;
			this.GroundFXHolder.LookAt(vector + b);
		}
		else
		{
			this.GroundFXHolder.position = this.GroundFXFollowAnchor.position;
		}
		if (flag && !this.groundFX.isPlaying)
		{
			this.groundFX.Play();
			return;
		}
		if (!flag && this.groundFX.isPlaying)
		{
			this.groundFX.Stop();
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002F231 File Offset: 0x0002D431
	internal override void ShieldBreak(DamageInfo dmg)
	{
		base.ShieldBreak(dmg);
		if (this.Control.IsMine)
		{
			PostFXManager.instance.ShieldBreak();
		}
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0002F254 File Offset: 0x0002D454
	public void ChangeCosmeticSet(CosmeticSet cSet)
	{
		this.CurSet = cSet;
		this.ChangeHead(this.CurSet.Head);
		this.ChangeSkin(this.CurSet.Skin);
		this.ChangeBook(this.CurSet.Book);
		this.ChangeBack(this.CurSet.Back);
		if (this.control.IsMine)
		{
			this.Control.Net.SendCosmetics(this.CurSet, null);
		}
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0002F2D0 File Offset: 0x0002D4D0
	public void ChangeCosmetic(Cosmetic c)
	{
		Cosmetic_Head cosmetic_Head = c as Cosmetic_Head;
		if (cosmetic_Head != null)
		{
			this.ChangeHead(cosmetic_Head);
		}
		else
		{
			Cosmetic_Skin cosmetic_Skin = c as Cosmetic_Skin;
			if (cosmetic_Skin != null)
			{
				this.ChangeSkin(cosmetic_Skin);
			}
			else
			{
				Cosmetic_Book cosmetic_Book = c as Cosmetic_Book;
				if (cosmetic_Book != null)
				{
					this.ChangeBook(cosmetic_Book);
				}
				else
				{
					Cosmetic_Signature cosmetic_Signature = c as Cosmetic_Signature;
					if (cosmetic_Signature != null)
					{
						this.ChangeBack(cosmetic_Signature);
					}
				}
			}
		}
		if (this.control.IsMine)
		{
			this.Control.Net.SendCosmetics(this.CurSet, null);
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002F34C File Offset: 0x0002D54C
	public void LoadCosmetics(string cosmeticData)
	{
		CosmeticSet cosmeticSet = new CosmeticSet(cosmeticData);
		this.ChangeHead(cosmeticSet.Head);
		this.ChangeSkin(cosmeticSet.Skin);
		this.ChangeBook(cosmeticSet.Book);
		this.ChangeBack(cosmeticSet.Back);
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0002F390 File Offset: 0x0002D590
	public void ChangeHead(Cosmetic_Head head)
	{
		if (this.headMesh != null)
		{
			this.Meshes.Remove(this.headMesh);
		}
		if (this.curHead != null)
		{
			UnityEngine.Object.DestroyImmediate(this.curHead.gameObject);
		}
		this.CurSet.Head = head;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(head.Prefab, this.HeadRoot);
		gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		this.curHead = gameObject.transform;
		MeshRenderer componentInChildren = this.curHead.GetComponentInChildren<MeshRenderer>();
		this.headMesh = componentInChildren;
		this.headMesh.material.SetColor(PlayerDisplay.BodyEmissiveTint, PlayerDB.GetCore(this.Control.actions.core).BodyGlowColor);
		base.AddMesh(componentInChildren);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0002F464 File Offset: 0x0002D664
	public void ChangeSkin(Cosmetic_Skin skin)
	{
		this.CurSet.Skin = skin;
		this.Body.material = skin.Torso;
		this.Legs.material = skin.Legs;
		this.Arm_L.material = skin.Arm_L;
		this.Arm_R.material = skin.Arm_R;
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(this.Control.actions.core);
		this.Body.material.SetColor(PlayerDisplay.BodyEmissiveTint, core.BodyGlowColor);
		this.Legs.material.SetColor(PlayerDisplay.BodyEmissiveTint, core.BodyGlowColor);
		this.Arm_L.material.SetColor(PlayerDisplay.BodyEmissiveTint, core.BodyGlowColor);
		this.Arm_R.material.SetColor(PlayerDisplay.BodyEmissiveTint, core.BodyGlowColor);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0002F544 File Offset: 0x0002D744
	public void ChangeBack(Cosmetic_Signature back)
	{
		if (this.curSignature != null)
		{
			UnityEngine.Object.DestroyImmediate(this.curSignature.gameObject);
		}
		this.CurSet.Back = back;
		if (back.Prefab == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(back.Prefab, this.Signature_Holder);
		gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		gameObject.transform.localScale = back.Prefab.transform.localScale / 1.386f;
		this.curSignature = gameObject;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0002F5DD File Offset: 0x0002D7DD
	public void ChangeBook(Cosmetic_Book book)
	{
		this.CurSet.Book = book;
		this.BookDisplay.ChangeCosmetic(book, PlayerDB.GetCore(this.Control.actions.core).BodyGlowColor);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0002F614 File Offset: 0x0002D814
	public void UpdateCoreDisplay()
	{
		if (this.Control == null || this.Control.actions == null)
		{
			UnityEngine.Debug.LogError("Control not fully initialized - aborting CoreDisplay setup");
			return;
		}
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(this.Control.actions.core);
		if (core != null)
		{
			Color bodyGlowColor = core.BodyGlowColor;
			this.Body.material.SetColor(PlayerDisplay.BodyEmissiveTint, bodyGlowColor);
			this.Legs.material.SetColor(PlayerDisplay.BodyEmissiveTint, bodyGlowColor);
			this.Arm_L.material.SetColor(PlayerDisplay.BodyEmissiveTint, bodyGlowColor);
			this.Arm_R.material.SetColor(PlayerDisplay.BodyEmissiveTint, bodyGlowColor);
			this.headMesh.material.SetColor(PlayerDisplay.BodyEmissiveTint, bodyGlowColor);
			this.BookDisplay.UpdateColor(bodyGlowColor);
			ParticleSystem.MainModule main = this.groundFX.main;
			Color particleColor = core.ParticleColor;
			particleColor.a = 1f;
			main.startColor = particleColor;
			return;
		}
		string str = "Could not find Core: ";
		AugmentTree core2 = this.Control.actions.core;
		UnityEngine.Debug.LogError(str + ((core2 != null) ? core2.ToString() : null) + " - Skipping color set");
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0002F747 File Offset: 0x0002D947
	public override Transform GetLocation(ActionLocation loc)
	{
		if (this.control.IsDead)
		{
			return this.GhostLoc.root;
		}
		return base.GetLocation(loc);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0002F76C File Offset: 0x0002D96C
	public void ToggleSpectator(bool isSpectator)
	{
		this.ToggleOutline();
		this.DisplayHolder.gameObject.SetActive(!isSpectator);
		if (this.CamRays != null)
		{
			this.CamRays.enabled = !isSpectator;
		}
		if (!isSpectator && this.CameraHolder != null)
		{
			this.CameraHolder.transform.localEulerAngles = Vector3.zero;
		}
		if (this.SpectatorCam != null)
		{
			this.SpectatorCam.SpectatorChanged(isSpectator);
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0002F7F0 File Offset: 0x0002D9F0
	internal override void UpdateBase()
	{
		base.UpdateBase();
		bool flag = false;
		foreach (MeshMaterialDisplay meshMaterialDisplay in this.AppliedMaterials)
		{
			flag |= meshMaterialDisplay.baseHidden;
		}
		this.BookDisplay.ToggleVisibility(!flag);
		if (this.curSignature != null)
		{
			this.curSignature.gameObject.SetActive(!flag);
		}
		this.ToggleOutline();
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0002F884 File Offset: 0x0002DA84
	private void OnDestroy()
	{
		PanelManager instance = PanelManager.instance;
		instance.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Remove(instance.OnPanelChanged, new Action<PanelType, PanelType>(this.OnUIPanelChanged));
		GameHUD instance2 = GameHUD.instance;
		instance2.OnHUDModeChanged = (Action)Delegate.Remove(instance2.OnHUDModeChanged, new Action(this.OnHUDModeChanged));
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002F8DD File Offset: 0x0002DADD
	public PlayerDisplay()
	{
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0002F8E5 File Offset: 0x0002DAE5
	// Note: this type is marked as 'beforefieldinit'.
	static PlayerDisplay()
	{
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0002F923 File Offset: 0x0002DB23
	[CompilerGenerated]
	[DebuggerHidden]
	private void <>n__0(DamageInfo dmg)
	{
		base.OnDeath(dmg);
	}

	// Token: 0x04000517 RID: 1303
	public Transform DisplayHolder;

	// Token: 0x04000518 RID: 1304
	public EntityLocation GhostLoc;

	// Token: 0x04000519 RID: 1305
	private List<Material> playerMats;

	// Token: 0x0400051A RID: 1306
	public Animator PlayerAnims;

	// Token: 0x0400051B RID: 1307
	public Animator RootAnims;

	// Token: 0x0400051C RID: 1308
	public Transform FishingAnchor;

	// Token: 0x0400051D RID: 1309
	public List<TailAnimator2> BonePhysics;

	// Token: 0x0400051E RID: 1310
	public Animator BookAnims;

	// Token: 0x0400051F RID: 1311
	public BookFollow BookFollow;

	// Token: 0x04000520 RID: 1312
	public PlayerBookDisplay BookDisplay;

	// Token: 0x04000521 RID: 1313
	public ParticleSystem CoreReadyFX;

	// Token: 0x04000522 RID: 1314
	public Transform CameraPivot;

	// Token: 0x04000523 RID: 1315
	public Transform CamFollowLoc;

	// Token: 0x04000524 RID: 1316
	public SmoothPosition CamFollowSmooth;

	// Token: 0x04000525 RID: 1317
	public CameraController CamController;

	// Token: 0x04000526 RID: 1318
	public GameObject CanvasObj;

	// Token: 0x04000527 RID: 1319
	public CameraDistanceRaycaster CamRays;

	// Token: 0x04000528 RID: 1320
	public Transform CameraHolder;

	// Token: 0x04000529 RID: 1321
	public SpectatorCam SpectatorCam;

	// Token: 0x0400052A RID: 1322
	public Transform SpectatorCamDisplay;

	// Token: 0x0400052B RID: 1323
	public bool ShowSpectatorCam;

	// Token: 0x0400052C RID: 1324
	[NonSerialized]
	public Vector3 SpectatorPos;

	// Token: 0x0400052D RID: 1325
	[NonSerialized]
	public Quaternion SpectatorRot;

	// Token: 0x0400052E RID: 1326
	public SkinnedMeshRenderer BookRenderer;

	// Token: 0x0400052F RID: 1327
	public ParticleSystem JumpParticles;

	// Token: 0x04000530 RID: 1328
	public ParticleSystem JumpParticlesCore;

	// Token: 0x04000531 RID: 1329
	public ParticleSystem LandParticles;

	// Token: 0x04000532 RID: 1330
	public Transform GroundFXFollowAnchor;

	// Token: 0x04000533 RID: 1331
	public LayerMask GroundTestMask;

	// Token: 0x04000534 RID: 1332
	public Transform GroundFXHolder;

	// Token: 0x04000535 RID: 1333
	public ParticleSystem groundFX;

	// Token: 0x04000536 RID: 1334
	public DynamicDecal RangeDisplay;

	// Token: 0x04000537 RID: 1335
	public ParticleSystem MovementCDFX;

	// Token: 0x04000538 RID: 1336
	public ParticleSystem SpawnFX;

	// Token: 0x04000539 RID: 1337
	public Light PlayerLight;

	// Token: 0x0400053A RID: 1338
	public Transform HeadRoot;

	// Token: 0x0400053B RID: 1339
	private MeshRenderer headMesh;

	// Token: 0x0400053C RID: 1340
	private Transform curHead;

	// Token: 0x0400053D RID: 1341
	public SkinnedMeshRenderer Body;

	// Token: 0x0400053E RID: 1342
	public SkinnedMeshRenderer Legs;

	// Token: 0x0400053F RID: 1343
	public SkinnedMeshRenderer Arm_L;

	// Token: 0x04000540 RID: 1344
	public SkinnedMeshRenderer Arm_R;

	// Token: 0x04000541 RID: 1345
	public Transform Signature_Holder;

	// Token: 0x04000542 RID: 1346
	private GameObject curSignature;

	// Token: 0x04000543 RID: 1347
	private Cosmetic_Emote curEmote;

	// Token: 0x04000544 RID: 1348
	private StatusTree emoteStatus;

	// Token: 0x04000545 RID: 1349
	private Coroutine DeathDelayFrame;

	// Token: 0x04000546 RID: 1350
	[CompilerGenerated]
	private bool <DisplayGrounded>k__BackingField;

	// Token: 0x04000547 RID: 1351
	private static readonly int DamagePower = Shader.PropertyToID("_DamagePower");

	// Token: 0x04000548 RID: 1352
	private static readonly int HealPower = Shader.PropertyToID("_HealPower");

	// Token: 0x04000549 RID: 1353
	private static readonly int ShieldAmount = Shader.PropertyToID("_ShieldAmount");

	// Token: 0x0400054A RID: 1354
	private MaterialPropertyBlock flashBlock;

	// Token: 0x0400054B RID: 1355
	public Vector3 velRef;

	// Token: 0x0400054C RID: 1356
	public Vector3 transfVelRef;

	// Token: 0x0400054D RID: 1357
	private Coroutine ReactRoutine;

	// Token: 0x0400054E RID: 1358
	[NonSerialized]
	public CosmeticSet CurSet;

	// Token: 0x0400054F RID: 1359
	public static readonly int BodyEmissiveTint = Shader.PropertyToID("_EmissiveTint");

	// Token: 0x020004A1 RID: 1185
	private enum DamageLevel
	{
		// Token: 0x040023BF RID: 9151
		None,
		// Token: 0x040023C0 RID: 9152
		Light,
		// Token: 0x040023C1 RID: 9153
		Medium,
		// Token: 0x040023C2 RID: 9154
		Heavy
	}

	// Token: 0x020004A2 RID: 1186
	[CompilerGenerated]
	private sealed class <Start>d__57 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002232 RID: 8754 RVA: 0x000C5C71 File Offset: 0x000C3E71
		[DebuggerHidden]
		public <Start>d__57(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000C5C80 File Offset: 0x000C3E80
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000C5C84 File Offset: 0x000C3E84
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerDisplay playerDisplay = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				if (playerDisplay.control.IsMine)
				{
					MapManager.ApplyCameraEffects();
				}
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			if (!playerDisplay.control.IsMine)
			{
				playerDisplay.ToggleOutline();
				playerDisplay.OnCoreChanged(playerDisplay.Control.actions.core.Root.magicColor);
			}
			return false;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000C5D11 File Offset: 0x000C3F11
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000C5D19 File Offset: 0x000C3F19
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000C5D20 File Offset: 0x000C3F20
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023C3 RID: 9155
		private int <>1__state;

		// Token: 0x040023C4 RID: 9156
		private object <>2__current;

		// Token: 0x040023C5 RID: 9157
		public PlayerDisplay <>4__this;
	}

	// Token: 0x020004A3 RID: 1187
	[CompilerGenerated]
	private sealed class <ReactivateBonePhys>d__70 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x000C5D28 File Offset: 0x000C3F28
		[DebuggerHidden]
		public <ReactivateBonePhys>d__70(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000C5D37 File Offset: 0x000C3F37
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000C5D3C File Offset: 0x000C3F3C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerDisplay playerDisplay = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_12B;
			default:
				return false;
			}
			if (t < delay)
			{
				t += GameplayManager.deltaTime;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			foreach (TailAnimator2 tailAnimator in playerDisplay.BonePhysics)
			{
				tailAnimator.TailAnimatorAmount = 0f;
				tailAnimator.enabled = true;
			}
			t = 0f;
			IL_12B:
			if (t >= 0.5f)
			{
				foreach (TailAnimator2 tailAnimator2 in playerDisplay.BonePhysics)
				{
					tailAnimator2.TailAnimatorAmount = 1f;
				}
				return false;
			}
			t += GameplayManager.deltaTime;
			foreach (TailAnimator2 tailAnimator3 in playerDisplay.BonePhysics)
			{
				tailAnimator3.TailAnimatorAmount = t * 2f;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000C5EE4 File Offset: 0x000C40E4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000C5EEC File Offset: 0x000C40EC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000C5EF3 File Offset: 0x000C40F3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023C6 RID: 9158
		private int <>1__state;

		// Token: 0x040023C7 RID: 9159
		private object <>2__current;

		// Token: 0x040023C8 RID: 9160
		public float delay;

		// Token: 0x040023C9 RID: 9161
		public PlayerDisplay <>4__this;

		// Token: 0x040023CA RID: 9162
		private float <t>5__2;
	}

	// Token: 0x020004A4 RID: 1188
	[CompilerGenerated]
	private sealed class <DeathSequence>d__100 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600223E RID: 8766 RVA: 0x000C5EFB File Offset: 0x000C40FB
		[DebuggerHidden]
		public <DeathSequence>d__100(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000C5F0A File Offset: 0x000C410A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000C5F0C File Offset: 0x000C410C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerDisplay playerDisplay = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			playerDisplay.<>n__0(dmg);
			playerDisplay.DeathDelayFrame = null;
			return false;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000C5F67 File Offset: 0x000C4167
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000C5F6F File Offset: 0x000C416F
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000C5F76 File Offset: 0x000C4176
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040023CB RID: 9163
		private int <>1__state;

		// Token: 0x040023CC RID: 9164
		private object <>2__current;

		// Token: 0x040023CD RID: 9165
		public PlayerDisplay <>4__this;

		// Token: 0x040023CE RID: 9166
		public DamageInfo dmg;
	}
}
