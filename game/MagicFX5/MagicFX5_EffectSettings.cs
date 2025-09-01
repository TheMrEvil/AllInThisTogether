using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class MagicFX5_EffectSettings : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	private void OnEnable()
	{
		if (this.UseAnimatorTriggerAfterCollision)
		{
			this.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(this.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnterTrigger));
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
	private void OnDisable()
	{
		if (this.UseAnimatorTriggerAfterCollision)
		{
			this.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(this.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnterTrigger));
		}
		this._targetCache.Clear();
		this._triggeredAnimators.Clear();
		this._animatorTriggerLastIndex = 0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020D8 File Offset: 0x000002D8
	private void OnEffectCollisionEnterTrigger(MagicFX5_EffectSettings.EffectCollisionHit hit)
	{
		this.TriggerAnimator(hit.Target);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
	private void TriggerAnimator(Transform target)
	{
		if (!this.UseAnimatorTriggerAfterCollision || this.AnimatorRandomTriggerNames.Length == 0)
		{
			return;
		}
		Animator animator = target.GetComponent<Animator>();
		if (animator == null)
		{
			animator = target.GetComponentInParent<Animator>();
		}
		if (animator != null && !this._triggeredAnimators.Contains(animator))
		{
			this._triggeredAnimators.Add(animator);
			animator.SetTrigger(this.AnimatorRandomTriggerNames[this._animatorTriggerLastIndex]);
			int num = this._animatorTriggerLastIndex + 1;
			this._animatorTriggerLastIndex = num;
			if (num >= this.AnimatorRandomTriggerNames.Length)
			{
				this._animatorTriggerLastIndex = 0;
			}
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002178 File Offset: 0x00000378
	internal Vector3 GetTargetCenter(Transform target)
	{
		if (!this._targetCache.ContainsKey(target))
		{
			this._targetCache.Add(target, new MagicFX5_EffectSettings.TargetCache(target));
		}
		MagicFX5_EffectSettings.TargetCache targetCache = this._targetCache[target];
		if (targetCache.SkinnedMeshRenderer != null)
		{
			return targetCache.SkinnedMeshRenderer.bounds.center + Vector3.up * this.TargetCenterHeightOffset;
		}
		if (targetCache.Renderer != null)
		{
			return targetCache.Renderer.bounds.center + Vector3.up * this.TargetCenterHeightOffset;
		}
		return target.position + Vector3.up * this.TargetCenterHeightOffset;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000223C File Offset: 0x0000043C
	public MagicFX5_EffectSettings()
	{
	}

	// Token: 0x04000001 RID: 1
	public Transform[] Targets;

	// Token: 0x04000002 RID: 2
	public float TargetCenterHeightOffset = 0.35f;

	// Token: 0x04000003 RID: 3
	public float ProjectileSpeed = 15f;

	// Token: 0x04000004 RID: 4
	[Space(25f)]
	public bool UseSkinMeshImpactEffects = true;

	// Token: 0x04000005 RID: 5
	public bool UseCameraShakeCinemachine = true;

	// Token: 0x04000006 RID: 6
	public bool UseForce = true;

	// Token: 0x04000007 RID: 7
	public LayerMask ForceLayerMask = -1;

	// Token: 0x04000008 RID: 8
	public float Force = 3f;

	// Token: 0x04000009 RID: 9
	public float ForceRadius = 10f;

	// Token: 0x0400000A RID: 10
	public bool UseRagdollForce = true;

	// Token: 0x0400000B RID: 11
	[Space]
	public bool UseAnimatorTriggerAfterCollision;

	// Token: 0x0400000C RID: 12
	public string[] AnimatorRandomTriggerNames;

	// Token: 0x0400000D RID: 13
	public Action<MagicFX5_EffectSettings.EffectCollisionHit> OnEffectCollisionEnter;

	// Token: 0x0400000E RID: 14
	public Action<MagicFX5_EffectSettings.EffectCollisionHit> OnEffectSkinActivated;

	// Token: 0x0400000F RID: 15
	public Action<MagicFX5_EffectSettings.EffectCollisionHit> OnEffectImpactActivated;

	// Token: 0x04000010 RID: 16
	private int _animatorTriggerLastIndex;

	// Token: 0x04000011 RID: 17
	public bool _isProjectile = true;

	// Token: 0x04000012 RID: 18
	private Dictionary<Transform, MagicFX5_EffectSettings.TargetCache> _targetCache = new Dictionary<Transform, MagicFX5_EffectSettings.TargetCache>();

	// Token: 0x04000013 RID: 19
	private List<Animator> _triggeredAnimators = new List<Animator>();

	// Token: 0x02000026 RID: 38
	public struct EffectCollisionHit
	{
		// Token: 0x04000143 RID: 323
		public Transform Target;

		// Token: 0x04000144 RID: 324
		public Vector3 Position;

		// Token: 0x04000145 RID: 325
		public Vector3 Normal;
	}

	// Token: 0x02000027 RID: 39
	private class TargetCache
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00006848 File Offset: 0x00004A48
		public TargetCache(Transform target)
		{
			this.SkinnedMeshRenderer = target.GetComponentInChildren<SkinnedMeshRenderer>();
			if (this.SkinnedMeshRenderer == null)
			{
				this.Renderer = target.GetComponentInChildren<Renderer>();
			}
		}

		// Token: 0x04000146 RID: 326
		public SkinnedMeshRenderer SkinnedMeshRenderer;

		// Token: 0x04000147 RID: 327
		public Renderer Renderer;
	}
}
