using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class MeshFX : MonoBehaviour
{
	// Token: 0x060017F8 RID: 6136 RVA: 0x00095DB4 File Offset: 0x00093FB4
	private void Awake()
	{
		this.Systems = new List<ParticleSystem>();
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		if (component != null)
		{
			this.Systems.Add(component);
		}
		foreach (ParticleSystem item in base.GetComponentsInChildren<ParticleSystem>())
		{
			this.Systems.Add(item);
		}
		if (this.StopAfterTime > 0f)
		{
			base.Invoke("AutoRemove", this.StopAfterTime);
		}
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x00095E2C File Offset: 0x0009402C
	public void Setup(EntityControl entity, Renderer applyTo, EffectProperties props, GameObject source, float meshCount)
	{
		base.transform.SetParent(entity.gameObject.transform);
		this.entityRef = entity;
		this.sourceRef = source;
		EntityHealth health = this.entityRef.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
		float num = 1f / Mathf.Max(meshCount, 1f);
		AIControl aicontrol = entity as AIControl;
		if (aicontrol != null && aicontrol.Display.MeshFXParticleCountMult > 0f)
		{
			num = aicontrol.Display.MeshFXParticleCountMult;
		}
		int num2 = 0;
		foreach (ParticleSystem particleSystem in this.Systems)
		{
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			bool flag = this.QualityEnabled(particleSystem, props);
			emission.enabled = flag;
			if (flag)
			{
				emission.rateOverTimeMultiplier *= num;
				emission.rateOverDistanceMultiplier *= num;
				if (emission.burstCount > 0 && num != 1f)
				{
					ParticleSystem.Burst[] array = new ParticleSystem.Burst[emission.burstCount];
					emission.GetBursts(array);
					for (int i = 0; i < array.Length; i++)
					{
						array[i].minCount = (short)Mathf.CeilToInt((float)array[i].minCount * num);
						array[i].maxCount = (short)Mathf.CeilToInt((float)array[i].maxCount * num);
					}
					emission.SetBursts(array);
				}
				ParticleSystem.ShapeModule shape = particleSystem.shape;
				SkinnedMeshRenderer skinnedMeshRenderer = applyTo as SkinnedMeshRenderer;
				if (skinnedMeshRenderer != null)
				{
					shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
					shape.skinnedMeshRenderer = skinnedMeshRenderer;
				}
				else
				{
					MeshRenderer meshRenderer = applyTo as MeshRenderer;
					if (meshRenderer != null)
					{
						shape.shapeType = ParticleSystemShapeType.MeshRenderer;
						shape.meshRenderer = meshRenderer;
					}
				}
			}
			particleSystem.Play();
			num2++;
		}
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x00096044 File Offset: 0x00094244
	private bool QualityEnabled(ParticleSystem p, EffectProperties props)
	{
		ActionEnabler.EffectQuality effectQuality = (props.SourceTeam == 1) ? ActionEnabler.PlayerQuality : ActionEnabler.EnemyQuality;
		return (effectQuality == ActionEnabler.EffectQuality.Full || !this.FullFX.Contains(p)) && (effectQuality != ActionEnabler.EffectQuality.Minimal || !this.ReducedFX.Contains(p));
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x0009608F File Offset: 0x0009428F
	private void AutoRemove()
	{
		this.entityRef.display.RemoveMeshFX(this.sourceRef);
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000960A7 File Offset: 0x000942A7
	private void OnOwnerDied(DamageInfo dmg)
	{
		this.RemoveEffect();
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x000960B0 File Offset: 0x000942B0
	public void RemoveEffect()
	{
		if (this.isRemoved)
		{
			return;
		}
		this.isRemoved = true;
		foreach (ParticleSystem particleSystem in this.Systems)
		{
			particleSystem.Stop();
		}
		base.transform.SetParent(null);
		UnityEngine.Object.Destroy(base.gameObject, this.DestroyTimeAfterStop);
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x00096130 File Offset: 0x00094330
	private void OnDestroy()
	{
		if (this.entityRef != null)
		{
			EntityHealth health = this.entityRef.health;
			health.OnDie = (Action<DamageInfo>)Delegate.Remove(health.OnDie, new Action<DamageInfo>(this.OnOwnerDied));
		}
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x0009616C File Offset: 0x0009436C
	public MeshFX()
	{
	}

	// Token: 0x040017C1 RID: 6081
	public float DestroyTimeAfterStop = 1f;

	// Token: 0x040017C2 RID: 6082
	public float StopAfterTime;

	// Token: 0x040017C3 RID: 6083
	private List<ParticleSystem> Systems;

	// Token: 0x040017C4 RID: 6084
	private bool isRemoved;

	// Token: 0x040017C5 RID: 6085
	private EntityControl entityRef;

	// Token: 0x040017C6 RID: 6086
	private GameObject sourceRef;

	// Token: 0x040017C7 RID: 6087
	public List<ParticleSystem> FullFX;

	// Token: 0x040017C8 RID: 6088
	public List<ParticleSystem> ReducedFX;
}
