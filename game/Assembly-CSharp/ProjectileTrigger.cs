using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class ProjectileTrigger : ActionEffect, EffectBase
{
	// Token: 0x060000E8 RID: 232 RVA: 0x0000BAEA File Offset: 0x00009CEA
	public override void Awake()
	{
		base.Awake();
		this.cols = base.gameObject.GetAllComponents<Collider>();
		if (!this.ignoreLayerChange)
		{
			base.gameObject.layer = 14;
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000BB18 File Offset: 0x00009D18
	internal override void OnEnable()
	{
		base.OnEnable();
		foreach (Collider collider in this.cols)
		{
			collider.enabled = true;
		}
		this.projectilesInteracted = 0;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000BB78 File Offset: 0x00009D78
	public void SetupInfo(EffectProperties props)
	{
		this.EffectSource = props.SourceControl;
		if (this.EffectSource == null)
		{
			GameplayManager.WorldEffects.Add(this);
			return;
		}
		this.EffectSource.OwnedEffects.Add(this);
		EntityHealth health = this.EffectSource.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000BBE8 File Offset: 0x00009DE8
	public bool InteractsWithTeamProjectile(EffectProperties projectileProps)
	{
		if (this.InteractionMask == EffectInteractsWith.Anything)
		{
			return true;
		}
		if (!(this.EffectSource != null))
		{
			return EntityControl.CanInteractWith(ActionSource.Fountain, projectileProps.SourceControl, this.InteractionMask);
		}
		return EntityControl.CanInteractWith(new EffectProperties(this.EffectSource), projectileProps.SourceControl, this.InteractionMask);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000BC3D File Offset: 0x00009E3D
	public bool BlocksInteractingProjectiles()
	{
		return true;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x0000BC40 File Offset: 0x00009E40
	public void ProjectileEntered(Projectile projectile)
	{
		Action<Vector3> onHit = this.OnHit;
		if (onHit != null)
		{
			onHit(projectile.transform.position);
		}
		this.projectilesInteracted++;
		if (this.projectilesInteracted >= this.ShotsToBreak && this.ShotsToBreak > 0)
		{
			this.Finish();
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000BC94 File Offset: 0x00009E94
	private void OnOwnerDied(DamageInfo dmg)
	{
		this.Finish();
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000BC9C File Offset: 0x00009E9C
	internal override void Finish()
	{
		if (this.isFinished)
		{
			return;
		}
		foreach (Collider collider in this.cols)
		{
			collider.enabled = false;
		}
		if (this.EffectSource != null)
		{
			EntityHealth health = this.EffectSource.health;
			health.OnDie = (Action<DamageInfo>)Delegate.Remove(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
		}
		base.Finish();
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000BD38 File Offset: 0x00009F38
	public ProjectileTrigger()
	{
	}

	// Token: 0x0400010A RID: 266
	private EntityControl EffectSource;

	// Token: 0x0400010B RID: 267
	private int projectilesInteracted;

	// Token: 0x0400010C RID: 268
	public int ShotsToBreak;

	// Token: 0x0400010D RID: 269
	public EffectInteractsWith InteractionMask;

	// Token: 0x0400010E RID: 270
	public bool ignoreLayerChange;

	// Token: 0x0400010F RID: 271
	private List<Collider> cols = new List<Collider>();

	// Token: 0x04000110 RID: 272
	public Action<Vector3> OnHit;
}
