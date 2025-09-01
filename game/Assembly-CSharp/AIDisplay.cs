using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using cakeslice;
using MiniTools.BetterGizmos;
using RootMotion.FinalIK;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class AIDisplay : EntityDisplay
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600033E RID: 830 RVA: 0x0001AFE8 File Offset: 0x000191E8
	public AIControl Control
	{
		get
		{
			return this.control as AIControl;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600033F RID: 831 RVA: 0x0001AFF5 File Offset: 0x000191F5
	public Animator Anim
	{
		get
		{
			return this.animator;
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001AFFD File Offset: 0x000191FD
	public override void Awake()
	{
		base.Awake();
		this.animator = base.GetComponent<Animator>();
		if (this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001B02C File Offset: 0x0001922C
	public override void Setup()
	{
		base.Setup();
		EntityHealth health = this.Control.health;
		health.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health.OnDamageTaken, new Action<DamageInfo>(this.OnDamageTaken));
		if (this.hitReact != null)
		{
			this.ragdollDisabledComponents.Add(this.hitReact);
		}
		if (this.headLook != null)
		{
			this.ragdollDisabledComponents.Add(this.headLook);
		}
		RootMotion.FinalIK.HitReaction hitReaction = this.hitReact;
		FullBodyBipedIK fullBodyBipedIK = (hitReaction != null) ? hitReaction.gameObject.GetComponent<FullBodyBipedIK>() : null;
		if (fullBodyBipedIK != null)
		{
			this.ragdollDisabledComponents.Add(fullBodyBipedIK);
		}
		if (this.Control.TeamID == 2 && this.Meshes != null && this.Meshes.Count > 0 && this.Meshes[0] != null)
		{
			this.baseHitPow = this.Meshes[0].sharedMaterial.GetFloat(AIDisplay.RimPower);
			return;
		}
		this.baseHitPow = 1f;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001B13B File Offset: 0x0001933B
	private IEnumerator Start()
	{
		yield return true;
		for (int i = this.Meshes.Count - 1; i >= 0; i--)
		{
			if (this.Meshes[i] == null)
			{
				this.Meshes.RemoveAt(i);
			}
		}
		yield break;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0001B14C File Offset: 0x0001934C
	internal override void ShieldBreak(DamageInfo dmg)
	{
		if (this.ShieldBreakFX != null)
		{
			base.ShieldBreak(dmg);
			return;
		}
		if (AIManager.instance == null || this.Meshes.Count == 0 || this.Meshes[0] == null || !(this.Meshes[0] is SkinnedMeshRenderer))
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AIManager.instance.AIShieldBreak);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		component.shape.skinnedMeshRenderer = (this.Meshes[0] as SkinnedMeshRenderer);
		component.Play();
		UnityEngine.Object.Destroy(gameObject, 1.5f);
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001B1F4 File Offset: 0x000193F4
	internal override void Update()
	{
		base.Update();
		if (this.animator != null)
		{
			this.UpdateAnimValues();
		}
		this.UpdateHitFlashValues();
		if (this.headLook != null)
		{
			float ikpositionWeight = this.headLook.solver.GetIKPositionWeight();
			float b = (float)((this.Control.currentTarget == null) ? 0 : 1);
			this.headLook.solver.SetLookAtWeight(Mathf.Lerp(ikpositionWeight, b, Time.deltaTime * 4f));
		}
		if (this.LookTarget != null && this.Control.currentTarget != null)
		{
			this.LookTarget.position = this.Control.currentTarget.display.CenterOfMass.position;
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001B2C4 File Offset: 0x000194C4
	private void UpdateAnimValues()
	{
		this.animator.speed = (PausePanel.IsGamePaused ? 0.0001f : Mathf.Lerp(this.animator.speed, this.DisplayMoveSpeed, Time.deltaTime * 10f));
		float a = this.aSpeed;
		float b = 0f;
		if (this.Control.Movement.IsMoving())
		{
			b = this.Control.Movement.GetVelocity().magnitude / 5f;
		}
		this.aSpeed = Mathf.Lerp(a, b, Time.deltaTime * 10f * this.animator.speed);
		this.animator.SetFloat(AIDisplay.Anim_Speed, this.aSpeed);
		if (this.ScaleMoveAnims)
		{
			float speedMult = this.Control.Movement.SpeedMult;
			this.animator.SetFloat(AIDisplay.Anim_MoveSpeed, speedMult);
		}
		if (this.animator.layerCount > this.HitLayer)
		{
			this.animator.SetLayerWeight(this.HitLayer, this.animator.GetLayerWeight(this.HitLayer) - Time.deltaTime * this.MaxHitPow);
		}
		if (this.hitAnimCD > 0f)
		{
			this.hitAnimCD -= Time.deltaTime;
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001B40E File Offset: 0x0001960E
	private void UpdateHitFlashValues()
	{
		if (this.hitFlashCD > 0f)
		{
			this.hitFlashCD -= Time.deltaTime;
			if (this.hitFlashCD <= 0f)
			{
				this.hitFlashCount--;
			}
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0001B44C File Offset: 0x0001964C
	public override void PlayAbilityAnim(string animName, float crossfadeTime, bool stopBonePhys)
	{
		if (this.animator == null)
		{
			return;
		}
		this.animator.CrossFade(animName, crossfadeTime, this.AbilityLayer);
		if (stopBonePhys && this.headLook != null)
		{
			this.headLook.enabled = false;
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0001B498 File Offset: 0x00019698
	public override bool IsPlayingAbilityAnim(string animName)
	{
		return !(this.animator == null) && this.animator.GetCurrentAnimatorStateInfo(this.AbilityLayer).IsName(animName);
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0001B4CF File Offset: 0x000196CF
	public override void StopCurrentAbilityAnim()
	{
		if (this.animator == null)
		{
			return;
		}
		this.animator.SetTrigger("Cancel");
		if (this.headLook != null)
		{
			this.headLook.enabled = true;
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0001B50C File Offset: 0x0001970C
	public override float AbilityAnimNormalizedTime()
	{
		if (this.animator == null)
		{
			return -1f;
		}
		return this.animator.GetCurrentAnimatorStateInfo(this.AbilityLayer).normalizedTime;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0001B548 File Offset: 0x00019748
	public void TryJump(Vector3 point)
	{
		AIAudio audio = this.Control.Audio;
		if (audio != null)
		{
			audio.Jump(point);
		}
		if (this.animator == null || !this.animator.HasState(0, Animator.StringToHash("Jump")))
		{
			return;
		}
		this.animator.CrossFade("Jump", 0f);
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001B5A8 File Offset: 0x000197A8
	public void TryLand(Vector3 point)
	{
		AIAudio audio = this.Control.Audio;
		if (audio != null)
		{
			audio.Land(point);
		}
		if (this.animator == null || !this.animator.HasState(0, Animator.StringToHash("Land")))
		{
			return;
		}
		this.animator.CrossFade("Land", 0f);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001B608 File Offset: 0x00019808
	private void OnDamageTaken(DamageInfo dmg)
	{
		float force = (dmg.DamageType == DNumType.Crit) ? 5f : 2f;
		RootMotion.FinalIK.HitReaction hitReaction = this.hitReact;
		if (hitReaction != null)
		{
			hitReaction.Hit(force, dmg.AtPoint);
		}
		this.PlayHitAnim(dmg);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001B64C File Offset: 0x0001984C
	public override void PlayHitAnim(DamageInfo dmg)
	{
		if (this.HitAnim.Length == 0)
		{
			return;
		}
		if (this.hitAnimCD <= 0f)
		{
			this.hitAnimCD = 0.5f;
			this.hitAnimCount = 0;
		}
		this.hitAnimCount++;
		if (this.hitAnimCount > 5)
		{
			return;
		}
		float weight = this.MaxHitPow / (float)this.hitAnimCount;
		this.animator.SetLayerWeight(this.HitLayer, weight);
		this.animator.CrossFade(this.HitAnim, this.HitCrossfade, this.HitLayer);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0001B6DC File Offset: 0x000198DC
	public override void DamageFlash(DamageInfo dmg)
	{
		if (!Settings.GetBool(SystemSetting.HitFlash, true))
		{
			return;
		}
		if (dmg.SourceID != PlayerControl.MyViewID)
		{
			EntityControl entity = EntityControl.GetEntity(dmg.SourceID);
			if (!(entity == null))
			{
				AIControl aicontrol = entity as AIControl;
				if (aicontrol != null && aicontrol.PetOwnerID == PlayerControl.MyViewID)
				{
					goto IL_45;
				}
			}
			return;
		}
		IL_45:
		if (this.hitFlashCD <= 0f)
		{
			this.hitFlashCD = 0.25f;
			this.hitFlashCount = 0;
		}
		if (this.hitFlashCount < 10)
		{
			this.hitFlashCount++;
		}
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(this._propBlock, i);
					this.dFlash = 1f;
					this._propBlock.SetFloat(AIDisplay.RimOpacity, this.dFlash);
					renderer.SetPropertyBlock(this._propBlock, i);
				}
			}
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0001B804 File Offset: 0x00019A04
	internal override void ReduceDamageFlash()
	{
		if (this.dFlash <= 0f)
		{
			return;
		}
		this.dFlash -= Time.deltaTime * 4f;
		if (this.dFlash < 0f)
		{
			this.dFlash = 0f;
		}
		float b = this.baseHitPow * (float)Mathf.Max(this.hitFlashCount, 1);
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null) && this._propBlock != null)
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(this._propBlock, i);
					float num = this._propBlock.GetFloat(AIDisplay.RimPower);
					num = Mathf.Lerp(num, b, Time.deltaTime * 8f);
					this._propBlock.SetFloat(AIDisplay.RimOpacity, this.dFlash);
					this._propBlock.SetFloat(AIDisplay.RimPower, num);
					renderer.SetPropertyBlock(this._propBlock, i);
				}
			}
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001B93C File Offset: 0x00019B3C
	public void SetVisible()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(materialPropertyBlock, i);
					materialPropertyBlock.SetFloat("_DissolveAmount", 0f);
					materialPropertyBlock.SetFloat("_Opacity", 1f);
					renderer.SetPropertyBlock(materialPropertyBlock, i);
				}
			}
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0001B9DC File Offset: 0x00019BDC
	public void SetInvisible()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(materialPropertyBlock, i);
					materialPropertyBlock.SetFloat("_DissolveAmount", 1f);
					materialPropertyBlock.SetFloat("_Opacity", 0f);
					renderer.SetPropertyBlock(materialPropertyBlock, i);
				}
			}
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0001BA7C File Offset: 0x00019C7C
	public void TransformInto(AIDisplay disp)
	{
		this.HUDAnchor.transform.localPosition = disp.HUDAnchor.transform.localPosition;
		base.transform.localScale = disp.transform.localScale;
		this.baseScale = base.transform.localScale.x;
		this.ShowHealthbar = disp.ShowHealthbar;
		this.HealthbarAlwaysOn = disp.HealthbarAlwaysOn;
		this.ShowAsAlly = disp.ShowAsAlly;
		UnityEngine.Object.Destroy(this.RagdollRoot);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(disp.RagdollRoot, base.transform);
		gameObject.transform.localPosition = disp.RagdollRoot.transform.localPosition;
		gameObject.transform.localRotation = disp.RagdollRoot.transform.localRotation;
		gameObject.transform.localScale = disp.RagdollRoot.transform.localScale;
		this.RagdollRoot = gameObject;
		this.displayHolder = base.transform;
		this.childSystems = base.gameObject.GetAllComponents<ParticleSystem>();
		this.lights = base.gameObject.GetAllComponents<EffectLight>();
		this.ragdollBits.Clear();
		foreach (Rigidbody rigidbody in this.RagdollRoot.GetComponentsInChildren<Rigidbody>())
		{
			if (!(rigidbody.GetComponent<IgnoreDisplayCollect>() != null))
			{
				rigidbody.isKinematic = true;
				EntityDisplay.RagdollComponent ragdollComponent = new EntityDisplay.RagdollComponent();
				ragdollComponent.rb = rigidbody;
				ragdollComponent.collider = rigidbody.GetComponent<Collider>();
				ragdollComponent.rootPos = rigidbody.transform.localPosition;
				ragdollComponent.rootRot = rigidbody.transform.localRotation;
				ragdollComponent.Unparent = rigidbody.GetComponent<RagdollUnparent>();
				ragdollComponent.Joint = rigidbody.GetComponent<CharacterJoint>();
				this.ragdollBits.Add(ragdollComponent);
			}
		}
		this.Meshes.Clear();
		foreach (Renderer renderer in disp.Meshes)
		{
			Transform transform = renderer.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
			if (transform != null)
			{
				this.Meshes.Add(transform.GetComponent<Renderer>());
			}
		}
		if (this.Control.TeamID == 2 && this.Meshes != null && this.Meshes.Count > 0 && this.Meshes[0] != null)
		{
			this.baseHitPow = this.Meshes[0].sharedMaterial.GetFloat(AIDisplay.RimPower);
		}
		else
		{
			this.baseHitPow = 1f;
		}
		this._overlapPts.Clear();
		this.EffectOverlapPoints.Clear();
		foreach (Transform transform2 in disp.EffectOverlapPoints)
		{
			Transform transform3 = transform2.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
			if (transform3 != null)
			{
				this.EffectOverlapPoints.Add(transform3);
			}
		}
		this.EyelineLocation = disp.EyelineLocation.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
		this.ModelCenter = disp.ModelCenter.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
		this.CenterOfMass = disp.CenterOfMass.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
		foreach (EntityLocation entityLocation in this.actionLocations)
		{
			EntityLocation entityLocation2 = entityLocation;
			Transform location = disp.GetLocation(entityLocation.location);
			entityLocation2.root = ((location != null) ? location.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform) : null);
		}
		this.ragdollDisabledComponents.Clear();
		foreach (Behaviour behaviour in disp.ragdollDisabledComponents)
		{
			Transform transform4 = behaviour.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
			if (!(transform4 == null))
			{
				Type type = behaviour.GetType();
				Behaviour behaviour2 = transform4.GetComponent(type) as Behaviour;
				if (behaviour2 != null)
				{
					this.ragdollDisabledComponents.Add(behaviour2);
				}
			}
		}
		this.animator = this.RagdollRoot.GetComponent<Animator>();
		if (this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		this.AbilityLayer = disp.AbilityLayer;
		this.HitLayer = disp.HitLayer;
		this.HitAnim = disp.HitAnim;
		this.HitCrossfade = disp.HitCrossfade;
		this.MaxHitPow = disp.MaxHitPow;
		this.ScaleMoveAnims = disp.ScaleMoveAnims;
		if (disp.LookTarget != null)
		{
			this.LookTarget = disp.LookTarget.transform.FindInInstance(disp.RagdollRoot.transform, this.RagdollRoot.transform);
		}
		else
		{
			this.LookTarget = null;
		}
		this.VFXScaleFactor = disp.VFXScaleFactor;
		this.MeshScaleFactor = disp.MeshScaleFactor;
		this.MeshFXParticleCountMult = disp.MeshFXParticleCountMult;
		this.ResetRagdoll();
		int layer = GameplayManager.GetLayer(this.control);
		EntityDisplay.SetLayerRecursively(this.RagdollRoot.gameObject, layer);
		this.outlines.Clear();
		foreach (Renderer renderer2 in this.Meshes)
		{
			if (!(renderer2 == null))
			{
				Outline item = renderer2.gameObject.AddComponent<Outline>();
				this.outlines.Add(item);
			}
		}
		base.StartCoroutine("TransformSetupDelayed");
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0001C0DC File Offset: 0x0001A2DC
	private IEnumerator TransformSetupDelayed()
	{
		yield return true;
		this.headLook = base.GetComponentInChildren<LookAtIK>();
		if (this.headLook != null)
		{
			this.ragdollDisabledComponents.Add(this.headLook);
		}
		yield break;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0001C0EC File Offset: 0x0001A2EC
	private void CreateActionTransforms()
	{
		if (this.RagdollRoot == null)
		{
			UnityEngine.Debug.LogError("Assign a Root transform to create action locations!");
			return;
		}
		this.CenterOfMass = new GameObject("Center_of_Mass").transform;
		this.CenterOfMass.transform.SetParent(this.RagdollRoot.transform);
		this.CenterOfMass.forward = this.RagdollRoot.transform.forward;
		this.CenterOfMass.transform.localPosition = Vector3.zero;
		this.EyelineLocation = new GameObject("Eyeline").transform;
		this.EyelineLocation.transform.SetParent(this.RagdollRoot.transform);
		this.EyelineLocation.forward = this.RagdollRoot.transform.forward;
		this.EyelineLocation.transform.localPosition = Vector3.zero;
		this.ModelCenter = new GameObject("Model_Center").transform;
		this.ModelCenter.transform.SetParent(this.RagdollRoot.transform);
		this.ModelCenter.forward = this.RagdollRoot.transform.forward;
		this.ModelCenter.transform.localPosition = Vector3.zero;
		foreach (EntityLocation entityLocation in this.actionLocations)
		{
			Transform transform = new GameObject(entityLocation.location.ToString()).transform;
			transform.transform.SetParent(this.RagdollRoot.transform);
			Transform transform2 = transform;
			Vector3 forward;
			switch (entityLocation.location)
			{
			case ActionLocation.Back:
				forward = -this.RagdollRoot.transform.forward;
				break;
			case ActionLocation.Head:
				forward = this.RagdollRoot.transform.up;
				break;
			case ActionLocation.Floor:
				forward = this.RagdollRoot.transform.up;
				break;
			default:
				forward = this.RagdollRoot.transform.forward;
				break;
			}
			transform2.forward = forward;
			if (entityLocation.location == ActionLocation.Floor || entityLocation.location == ActionLocation.Head)
			{
				transform.transform.Rotate(Vector3.forward, 180f);
			}
			entityLocation.root = transform;
			transform.transform.localPosition = Vector3.zero;
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001C370 File Offset: 0x0001A570
	private void OnDrawGizmos()
	{
		if (!this.ShowGizmos || this.CenterOfMass == null)
		{
			return;
		}
		if (this.CenterOfMass != null)
		{
			this.DrawArrow(this.CenterOfMass.transform, Color.green, "Center");
		}
		if (this.EyelineLocation != null)
		{
			this.DrawArrow(this.EyelineLocation.transform, Color.blue, "Eyes");
		}
		Transform location = this.GetLocation(ActionLocation.Floor);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.green, "Floor");
		}
		location = this.GetLocation(ActionLocation.Front);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.green, "Front");
		}
		location = this.GetLocation(ActionLocation.Back);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.green, "Back");
		}
		location = this.GetLocation(ActionLocation.PrimaryWeapon);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.red, "Primary");
		}
		location = this.GetLocation(ActionLocation.SecondaryWeapon);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.red, "Secondary");
		}
		location = this.GetLocation(ActionLocation.Head);
		if (location != null)
		{
			this.DrawArrow(location.transform, Color.green, "Top");
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
	private void DrawArrow(Transform t, Color c, string text)
	{
		BetterGizmos.DrawViewFacingArrow(c, t.position, t.position + t.forward, 1f, BetterGizmos.DownsizeDisplay.Squash, BetterGizmos.UpsizeDisplay.Offset);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
	public override void Ragdoll()
	{
		if (this.animator != null)
		{
			this.animator.enabled = false;
		}
		base.Ragdoll();
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001C518 File Offset: 0x0001A718
	public override void ResetRagdoll()
	{
		if (this.animator != null)
		{
			this.animator.enabled = true;
		}
		base.ResetRagdoll();
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001C53A File Offset: 0x0001A73A
	internal override void OnDeath(DamageInfo dmg)
	{
		base.OnDeath(dmg);
		base.ReleaseHilight();
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001C549 File Offset: 0x0001A749
	public AIDisplay()
	{
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001C57C File Offset: 0x0001A77C
	// Note: this type is marked as 'beforefieldinit'.
	static AIDisplay()
	{
	}

	// Token: 0x0400032E RID: 814
	private Animator animator;

	// Token: 0x0400032F RID: 815
	public int AbilityLayer;

	// Token: 0x04000330 RID: 816
	public int HitLayer = 1;

	// Token: 0x04000331 RID: 817
	public string HitAnim;

	// Token: 0x04000332 RID: 818
	public float HitCrossfade;

	// Token: 0x04000333 RID: 819
	public float MaxHitPow = 1f;

	// Token: 0x04000334 RID: 820
	public bool ScaleMoveAnims;

	// Token: 0x04000335 RID: 821
	private float baseHitPow;

	// Token: 0x04000336 RID: 822
	public bool UseDropSpawn = true;

	// Token: 0x04000337 RID: 823
	public float MeshScaleFactor = 1f;

	// Token: 0x04000338 RID: 824
	[Tooltip("If 0, Count is devided by number of Meshes")]
	public float MeshFXParticleCountMult;

	// Token: 0x04000339 RID: 825
	public bool ShowHealthbar = true;

	// Token: 0x0400033A RID: 826
	public bool HealthbarAlwaysOn;

	// Token: 0x0400033B RID: 827
	public bool ShowAsAlly;

	// Token: 0x0400033C RID: 828
	[Header("IK Systems")]
	public LookAtIK headLook;

	// Token: 0x0400033D RID: 829
	public RootMotion.FinalIK.HitReaction hitReact;

	// Token: 0x0400033E RID: 830
	public Transform LookTarget;

	// Token: 0x0400033F RID: 831
	[NonSerialized]
	private bool ShowGizmos;

	// Token: 0x04000340 RID: 832
	private static readonly int Anim_MoveSpeed = Animator.StringToHash("MoveSpeed");

	// Token: 0x04000341 RID: 833
	private static readonly int Anim_Speed = Animator.StringToHash("Speed");

	// Token: 0x04000342 RID: 834
	private float aSpeed;

	// Token: 0x04000343 RID: 835
	private float hitAnimCD;

	// Token: 0x04000344 RID: 836
	private int hitAnimCount;

	// Token: 0x04000345 RID: 837
	private static readonly int RimOpacity = Shader.PropertyToID("_RimOpacity");

	// Token: 0x04000346 RID: 838
	private static readonly int RimPower = Shader.PropertyToID("_RimPower");

	// Token: 0x04000347 RID: 839
	private float hitFlashCD;

	// Token: 0x04000348 RID: 840
	private int hitFlashCount;

	// Token: 0x02000481 RID: 1153
	[CompilerGenerated]
	private sealed class <Start>d__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x000C36C4 File Offset: 0x000C18C4
		[DebuggerHidden]
		public <Start>d__26(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000C36D3 File Offset: 0x000C18D3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000C36D8 File Offset: 0x000C18D8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIDisplay aidisplay = this;
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
			for (int i = aidisplay.Meshes.Count - 1; i >= 0; i--)
			{
				if (aidisplay.Meshes[i] == null)
				{
					aidisplay.Meshes.RemoveAt(i);
				}
			}
			return false;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x000C3758 File Offset: 0x000C1958
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000C3760 File Offset: 0x000C1960
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x000C3767 File Offset: 0x000C1967
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022E8 RID: 8936
		private int <>1__state;

		// Token: 0x040022E9 RID: 8937
		private object <>2__current;

		// Token: 0x040022EA RID: 8938
		public AIDisplay <>4__this;
	}

	// Token: 0x02000482 RID: 1154
	[CompilerGenerated]
	private sealed class <TransformSetupDelayed>d__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021AE RID: 8622 RVA: 0x000C376F File Offset: 0x000C196F
		[DebuggerHidden]
		public <TransformSetupDelayed>d__51(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000C377E File Offset: 0x000C197E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000C3780 File Offset: 0x000C1980
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIDisplay aidisplay = this;
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
			aidisplay.headLook = aidisplay.GetComponentInChildren<LookAtIK>();
			if (aidisplay.headLook != null)
			{
				aidisplay.ragdollDisabledComponents.Add(aidisplay.headLook);
			}
			return false;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x000C37F3 File Offset: 0x000C19F3
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000C37FB File Offset: 0x000C19FB
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000C3802 File Offset: 0x000C1A02
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022EB RID: 8939
		private int <>1__state;

		// Token: 0x040022EC RID: 8940
		private object <>2__current;

		// Token: 0x040022ED RID: 8941
		public AIDisplay <>4__this;
	}
}
