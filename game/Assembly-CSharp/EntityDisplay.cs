using System;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class EntityDisplay : MonoBehaviour
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000505 RID: 1285 RVA: 0x00025118 File Offset: 0x00023318
	public List<Transform> OverlapPoints
	{
		get
		{
			if (this._overlapPts.Count > 0)
			{
				return this._overlapPts;
			}
			this._overlapPts.Add(this.CenterOfMass);
			this._overlapPts.Add(this.GetLocation(ActionLocation.Floor));
			foreach (Transform item in this.EffectOverlapPoints)
			{
				this._overlapPts.Add(item);
			}
			return this._overlapPts;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000506 RID: 1286 RVA: 0x000251B0 File Offset: 0x000233B0
	public Vector3 Position
	{
		get
		{
			Transform centerOfMass = this.CenterOfMass;
			if (centerOfMass != null)
			{
				return centerOfMass.position;
			}
			Transform modelCenter = this.ModelCenter;
			if (modelCenter == null)
			{
				return base.transform.position;
			}
			return modelCenter.position;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000507 RID: 1287 RVA: 0x000251E0 File Offset: 0x000233E0
	public float Size
	{
		get
		{
			if (this.cachedSize == 0f)
			{
				this.cachedSize = this.control.GetPassiveMod(Passive.EntityValue.Size, 1f);
			}
			this.cachedSize = Mathf.Max(0.05f, this.cachedSize);
			return this.cachedSize * this.baseScale;
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00025234 File Offset: 0x00023434
	public virtual void Awake()
	{
		this.control = base.GetComponent<EntityControl>();
		this.displayHolder = base.transform;
		this.childSystems = base.gameObject.GetAllComponents<ParticleSystem>();
		this.lights = base.gameObject.GetAllComponents<EffectLight>();
		foreach (Rigidbody rigidbody in base.GetComponentsInChildren<Rigidbody>())
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
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0002530C File Offset: 0x0002350C
	public virtual void Setup()
	{
		this._propBlock = new MaterialPropertyBlock();
		this.ResetRagdoll();
		EntityHealth health = this.control.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(this.OnDeath));
		EntityHealth health2 = this.control.health;
		health2.OnRevive = (Action<int>)Delegate.Combine(health2.OnRevive, new Action<int>(this.OnRevived));
		EntityHealth health3 = this.control.health;
		health3.OnDamageTaken = (Action<DamageInfo>)Delegate.Combine(health3.OnDamageTaken, new Action<DamageInfo>(this.DamageFlash));
		EntityHealth health4 = this.control.health;
		health4.OnShieldsDepleted = (Action<DamageInfo>)Delegate.Combine(health4.OnShieldsDepleted, new Action<DamageInfo>(this.ShieldBreak));
		this.baseScale = this.displayHolder.localScale.x;
		int layer = GameplayManager.GetLayer(this.control);
		EntityDisplay.SetLayerRecursively(this.RagdollRoot.gameObject, layer);
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				Outline item = renderer.gameObject.AddComponent<Outline>();
				this.outlines.Add(item);
			}
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00025474 File Offset: 0x00023674
	public void ResetDisplay()
	{
		this.ResetMeshMaterials();
		this.ResetMeshFX();
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00025484 File Offset: 0x00023684
	internal void AddMesh(MeshRenderer mesh)
	{
		Outline item = mesh.gameObject.AddComponent<Outline>();
		this.outlines.Add(item);
		this.Meshes.Add(mesh);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x000254B8 File Offset: 0x000236B8
	public void ClearMeshes()
	{
		for (int i = this.outlines.Count - 1; i >= 0; i--)
		{
			this.outlines[i].Deactivate();
			UnityEngine.Object.Destroy(this.outlines[i]);
		}
		this.outlines.Clear();
		this.Meshes.Clear();
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00025518 File Offset: 0x00023718
	internal virtual void Update()
	{
		if (this.isDissolving)
		{
			this.UpdateDissolve();
		}
		float displayMoveSpeed = 1f;
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.control.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Keywords.Contains(StatusKeyword.Freeze_Display) && (!(this is AIDisplay) || !(this as AIDisplay).Control.Level.HasFlag(EnemyLevel.Boss)))
				{
					displayMoveSpeed = 0f;
				}
			}
		}
		this.DisplayMoveSpeed = displayMoveSpeed;
		if (!this.control.IsDead && Mathf.Abs(this.displayHolder.localScale.x - this.Size) > 0.005f)
		{
			this.displayHolder.localScale = Vector3.one * Mathf.Lerp(this.displayHolder.localScale.x, this.Size, Time.deltaTime * 6f);
		}
		this.ReduceDamageFlash();
		this.ResetCached();
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00025638 File Offset: 0x00023838
	public bool CanRotateDisplay()
	{
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.control.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Keywords.Contains(StatusKeyword.Prevent_Rotation))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0002569C File Offset: 0x0002389C
	private void ResetCached()
	{
		this.cachedSize = 0f;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x000256AC File Offset: 0x000238AC
	public virtual void AddMeshFX(GameObject refObj, EffectProperties props, int MeshIndex = -1)
	{
		this.RemoveMeshFX(refObj);
		if (MeshIndex < 0 || MeshIndex >= this.Meshes.Count)
		{
			foreach (Renderer renderer in this.Meshes)
			{
				if (!(renderer == null))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(refObj);
					gameObject.name = refObj.name;
					MeshFX component = gameObject.GetComponent<MeshFX>();
					component.Setup(this.control, renderer, props, refObj, (float)this.Meshes.Count);
					this.AppliedMeshFX.Add(component);
				}
			}
			return;
		}
		if (this.Meshes[MeshIndex] == null)
		{
			return;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(refObj);
		gameObject2.name = refObj.name;
		MeshFX component2 = gameObject2.GetComponent<MeshFX>();
		component2.Setup(this.control, this.Meshes[MeshIndex], props, refObj, 1f);
		this.AppliedMeshFX.Add(component2);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x000257B4 File Offset: 0x000239B4
	public void RemoveMeshFX(GameObject refObj)
	{
		for (int i = this.AppliedMeshFX.Count - 1; i >= 0; i--)
		{
			if (this.AppliedMeshFX[i] == null || this.AppliedMeshFX[i].gameObject == null)
			{
				this.AppliedMeshFX.RemoveAt(i);
			}
			else if (!(this.AppliedMeshFX[i].gameObject.name != refObj.name))
			{
				this.AppliedMeshFX[i].RemoveEffect();
				this.AppliedMeshFX.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00025858 File Offset: 0x00023A58
	public void ResetMeshFX()
	{
		for (int i = this.AppliedMeshFX.Count - 1; i >= 0; i--)
		{
			if (this.AppliedMeshFX[i] == null || this.AppliedMeshFX[i].gameObject == null)
			{
				this.AppliedMeshFX.RemoveAt(i);
			}
			else
			{
				this.AppliedMeshFX[i].RemoveEffect();
				this.AppliedMeshFX.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x000258D8 File Offset: 0x00023AD8
	public void AddMeshMaterial(Material m, float fadeTime, bool hideBase)
	{
		using (List<MeshMaterialDisplay>.Enumerator enumerator = this.AppliedMaterials.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Material == m)
				{
					return;
				}
			}
		}
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				Renderer copyFrom = renderer;
				AIDisplay aidisplay = this as AIDisplay;
				MeshMaterialDisplay meshMaterialDisplay = MeshMaterialDisplay.CreateDisplay(copyFrom, m, fadeTime, hideBase, (aidisplay != null) ? aidisplay.MeshScaleFactor : 1f);
				if (!(meshMaterialDisplay == null))
				{
					this.AppliedMaterials.Add(meshMaterialDisplay);
				}
			}
		}
		this.UpdateBase();
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x000259B8 File Offset: 0x00023BB8
	public void ModifyMeshMaterial(Material m, string property, float value)
	{
		for (int i = this.AppliedMaterials.Count - 1; i >= 0; i--)
		{
			if (!(this.AppliedMaterials[i].Material != m))
			{
				this.AppliedMaterials[i].Modify(property, value);
			}
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00025A0C File Offset: 0x00023C0C
	public void RemoveMeshMaterial(Material m, float fadeTime)
	{
		for (int i = this.AppliedMaterials.Count - 1; i >= 0; i--)
		{
			if (!(this.AppliedMaterials[i].Material != m))
			{
				this.AppliedMaterials[i].RemoveEffect(fadeTime);
				this.AppliedMaterials.RemoveAt(i);
			}
		}
		this.UpdateBase();
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00025A70 File Offset: 0x00023C70
	public void ResetMeshMaterials()
	{
		for (int i = this.AppliedMaterials.Count - 1; i >= 0; i--)
		{
			this.AppliedMaterials[i].RemoveEffect(0f);
			this.AppliedMaterials.RemoveAt(i);
		}
		this.UpdateBase();
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00025AC0 File Offset: 0x00023CC0
	internal virtual void UpdateBase()
	{
		bool flag = false;
		foreach (MeshMaterialDisplay meshMaterialDisplay in this.AppliedMaterials)
		{
			flag |= meshMaterialDisplay.baseHidden;
		}
		foreach (Renderer renderer in this.Meshes)
		{
			if (renderer != null)
			{
				renderer.enabled = !flag;
			}
		}
		foreach (MeshMaterialDisplay meshMaterialDisplay2 in this.AppliedMaterials)
		{
			if (meshMaterialDisplay2 != null && !meshMaterialDisplay2.baseHidden && meshMaterialDisplay2.Render != null)
			{
				meshMaterialDisplay2.Render.enabled = !flag;
			}
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00025BD8 File Offset: 0x00023DD8
	public virtual void PlayAbilityAnim(string animName, float crossfadeTime, bool stopBonePhys)
	{
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00025BDA File Offset: 0x00023DDA
	public virtual void PlayHitAnim(DamageInfo dmg)
	{
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00025BDC File Offset: 0x00023DDC
	public virtual bool IsPlayingAbilityAnim(string animName)
	{
		return false;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00025BDF File Offset: 0x00023DDF
	public virtual void StopCurrentAbilityAnim()
	{
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00025BE1 File Offset: 0x00023DE1
	public virtual float AbilityAnimNormalizedTime()
	{
		return -1f;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00025BE8 File Offset: 0x00023DE8
	public virtual void Ragdoll()
	{
		EntityDisplay.SetLayerRecursively(this.RagdollRoot.gameObject, 13);
		foreach (Behaviour behaviour in this.ragdollDisabledComponents)
		{
			if (behaviour != null)
			{
				behaviour.enabled = false;
			}
		}
		foreach (EntityDisplay.RagdollComponent ragdollComponent in this.ragdollBits)
		{
			if (ragdollComponent.Joint != null)
			{
				ragdollComponent.Joint.enableProjection = true;
			}
			if (ragdollComponent.Unparent != null)
			{
				ragdollComponent.Unparent.Detatch();
			}
			else
			{
				ragdollComponent.rb.isKinematic = false;
				Vector3 vector = (this.control.LastDamageTakenPoint - ragdollComponent.rb.position).normalized;
				if (this.control.LastDamageTakenPoint == Vector3.zero)
				{
					vector = UnityEngine.Random.onUnitSphere;
				}
				float num = Mathf.Min(this.control.LastDamageTaken / (float)this.control.health.MaxHealth * 15f, 150f);
				ragdollComponent.rb.AddExplosionForce(num, ragdollComponent.rb.position + vector, 5f, 0.2f, ForceMode.Impulse);
				if (ragdollComponent.Joint == null && ragdollComponent.collider != null)
				{
					Vector3 lhs = vector - ragdollComponent.rb.worldCenterOfMass;
					Vector3 b = ragdollComponent.collider.ClosestPoint(vector);
					Vector3 normalized = (vector - b).normalized;
					Vector3 torque = Vector3.Cross(lhs, normalized) * num;
					ragdollComponent.rb.AddTorque(torque, ForceMode.Impulse);
				}
			}
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00025E00 File Offset: 0x00024000
	public virtual void ResetRagdoll()
	{
		foreach (EntityDisplay.RagdollComponent ragdollComponent in this.ragdollBits)
		{
			if (ragdollComponent.Unparent)
			{
				ragdollComponent.Unparent.Reattatch();
			}
			ragdollComponent.rb.isKinematic = true;
			ragdollComponent.rb.transform.localPosition = ragdollComponent.rootPos;
			ragdollComponent.rb.transform.localRotation = ragdollComponent.rootRot;
			if (ragdollComponent.Joint != null)
			{
				ragdollComponent.Joint.enableProjection = false;
			}
		}
		foreach (Behaviour behaviour in this.ragdollDisabledComponents)
		{
			behaviour.enabled = true;
		}
		int layer = GameplayManager.GetLayer(this.control);
		EntityDisplay.SetLayerRecursively(this.RagdollRoot.gameObject, layer);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00025F14 File Offset: 0x00024114
	public void ApplyHilight()
	{
		foreach (Outline outline in this.outlines)
		{
			outline.Activate();
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00025F64 File Offset: 0x00024164
	public void ReleaseHilight()
	{
		foreach (Outline outline in this.outlines)
		{
			outline.Deactivate();
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00025FB4 File Offset: 0x000241B4
	internal virtual void ShieldBreak(DamageInfo dmg)
	{
		ParticleSystem shieldBreakFX = this.ShieldBreakFX;
		if (shieldBreakFX == null)
		{
			return;
		}
		shieldBreakFX.Play();
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00025FC8 File Offset: 0x000241C8
	internal virtual void OnDeath(DamageInfo dmg)
	{
		this.Ragdoll();
		foreach (ParticleSystem particleSystem in this.childSystems)
		{
			if (particleSystem != null)
			{
				particleSystem.Stop();
			}
		}
		foreach (EffectLight effectLight in this.lights)
		{
			if (effectLight != null)
			{
				effectLight.Deactivate();
			}
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00026074 File Offset: 0x00024274
	internal virtual void OnRevived(int healthAmt)
	{
		this.ResetRagdoll();
		foreach (ParticleSystem particleSystem in this.childSystems)
		{
			if (particleSystem != null && particleSystem.main.playOnAwake)
			{
				particleSystem.Play();
			}
		}
		foreach (EffectLight effectLight in this.lights)
		{
			if (effectLight != null)
			{
				effectLight.Activate();
			}
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00026134 File Offset: 0x00024334
	public virtual Transform GetLocation(ActionLocation loc)
	{
		if (loc == ActionLocation.CenterOfMass)
		{
			return this.CenterOfMass;
		}
		if (loc == ActionLocation.UIRoot)
		{
			return this.HUDAnchor;
		}
		if (loc == ActionLocation.ModelCenter)
		{
			return this.ModelCenter;
		}
		foreach (EntityLocation entityLocation in this.actionLocations)
		{
			if (entityLocation.location == loc)
			{
				return entityLocation.root;
			}
		}
		return null;
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x000261B8 File Offset: 0x000243B8
	public void LocalDamageDone(DamageInfo dmg)
	{
		this.DamageFlash(dmg);
		this.PlayHitAnim(dmg);
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x000261C8 File Offset: 0x000243C8
	private void SetupDissolve()
	{
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000261CA File Offset: 0x000243CA
	public void Dissolve()
	{
		this.isDissolving = true;
		this.dissolveVal = 0f;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000261E0 File Offset: 0x000243E0
	private void UpdateDissolve()
	{
		this.dissolveVal += Time.deltaTime * 1f;
		if (this.dissolveVal >= 1f)
		{
			this.dissolveVal = 1f;
			this.isDissolving = false;
		}
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(this._propBlock, i);
					this._propBlock.SetFloat("_DissolveAmount", this.dissolveVal);
					this._propBlock.SetFloat("_Opacity", 1f - this.dissolveVal);
					renderer.SetPropertyBlock(this._propBlock, i);
				}
			}
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000262CC File Offset: 0x000244CC
	public virtual void DamageFlash(DamageInfo dmg)
	{
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null))
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(this._propBlock, i);
					this.dFlash = 1f;
					this._propBlock.SetFloat("_RimOpacity", this.dFlash);
					renderer.SetPropertyBlock(this._propBlock, i);
				}
			}
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00026370 File Offset: 0x00024570
	internal virtual void ReduceDamageFlash()
	{
		if (this.dFlash <= 0f)
		{
			return;
		}
		this.dFlash -= Time.deltaTime * 4f;
		foreach (Renderer renderer in this.Meshes)
		{
			if (!(renderer == null) && this._propBlock != null)
			{
				for (int i = 0; i < renderer.sharedMaterials.Length; i++)
				{
					renderer.GetPropertyBlock(this._propBlock, i);
					this._propBlock.SetFloat("_RimOpacity", this.dFlash);
					renderer.SetPropertyBlock(this._propBlock, i);
				}
			}
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00026438 File Offset: 0x00024638
	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		foreach (EntityDisplay.RagdollComponent ragdollComponent in this.ragdollBits)
		{
			if (!(ragdollComponent.Unparent == null) && ragdollComponent.rb != null && ragdollComponent.rb.transform.parent == null)
			{
				UnityEngine.Object.Destroy(ragdollComponent.rb.gameObject);
			}
		}
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x000264D0 File Offset: 0x000246D0
	public static void SetLayerRecursively(GameObject obj, int layer)
	{
		if (obj.layer != 1)
		{
			obj.layer = layer;
		}
		foreach (object obj2 in obj.transform)
		{
			Transform transform = (Transform)obj2;
			if (transform.gameObject.layer != 1)
			{
				EntityDisplay.SetLayerRecursively(transform.gameObject, layer);
			}
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0002654C File Offset: 0x0002474C
	public EntityDisplay()
	{
	}

	// Token: 0x040003F5 RID: 1013
	public GameObject RagdollRoot;

	// Token: 0x040003F6 RID: 1014
	internal Transform displayHolder;

	// Token: 0x040003F7 RID: 1015
	[NonSerialized]
	public EntityControl control;

	// Token: 0x040003F8 RID: 1016
	public Transform EyelineLocation;

	// Token: 0x040003F9 RID: 1017
	public Transform CenterOfMass;

	// Token: 0x040003FA RID: 1018
	public Transform ModelCenter;

	// Token: 0x040003FB RID: 1019
	public List<EntityLocation> actionLocations;

	// Token: 0x040003FC RID: 1020
	public List<Behaviour> ragdollDisabledComponents;

	// Token: 0x040003FD RID: 1021
	public List<Transform> EffectOverlapPoints = new List<Transform>();

	// Token: 0x040003FE RID: 1022
	[NonSerialized]
	public List<EntityDisplay.RagdollComponent> ragdollBits = new List<EntityDisplay.RagdollComponent>();

	// Token: 0x040003FF RID: 1023
	internal List<ParticleSystem> childSystems = new List<ParticleSystem>();

	// Token: 0x04000400 RID: 1024
	internal List<EffectLight> lights = new List<EffectLight>();

	// Token: 0x04000401 RID: 1025
	internal List<Transform> _overlapPts = new List<Transform>();

	// Token: 0x04000402 RID: 1026
	public List<Renderer> Meshes;

	// Token: 0x04000403 RID: 1027
	public float VFXScaleFactor = 1f;

	// Token: 0x04000404 RID: 1028
	public ParticleSystem ShieldBreakFX;

	// Token: 0x04000405 RID: 1029
	public Transform HUDAnchor;

	// Token: 0x04000406 RID: 1030
	[NonSerialized]
	public float DisplayMoveSpeed;

	// Token: 0x04000407 RID: 1031
	internal float baseScale;

	// Token: 0x04000408 RID: 1032
	private float cachedSize;

	// Token: 0x04000409 RID: 1033
	internal List<MeshFX> AppliedMeshFX = new List<MeshFX>();

	// Token: 0x0400040A RID: 1034
	internal List<MeshMaterialDisplay> AppliedMaterials = new List<MeshMaterialDisplay>();

	// Token: 0x0400040B RID: 1035
	internal List<Outline> outlines = new List<Outline>();

	// Token: 0x0400040C RID: 1036
	private bool isDissolving;

	// Token: 0x0400040D RID: 1037
	private float dissolveVal;

	// Token: 0x0400040E RID: 1038
	internal MaterialPropertyBlock _propBlock;

	// Token: 0x0400040F RID: 1039
	internal float dFlash;

	// Token: 0x02000499 RID: 1177
	public class RagdollComponent
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x000C595F File Offset: 0x000C3B5F
		public RagdollComponent()
		{
		}

		// Token: 0x0400235C RID: 9052
		public Collider collider;

		// Token: 0x0400235D RID: 9053
		public Rigidbody rb;

		// Token: 0x0400235E RID: 9054
		public Vector3 rootPos;

		// Token: 0x0400235F RID: 9055
		public Quaternion rootRot;

		// Token: 0x04002360 RID: 9056
		public RagdollUnparent Unparent;

		// Token: 0x04002361 RID: 9057
		public CharacterJoint Joint;
	}
}
