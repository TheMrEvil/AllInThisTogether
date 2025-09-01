using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class ActionEffect : MonoBehaviour
{
	// Token: 0x06000096 RID: 150 RVA: 0x00006DD8 File Offset: 0x00004FD8
	public virtual void Awake()
	{
		this.childSystems = base.gameObject.GetAllComponents<ParticleSystem>();
		this.childMeshes = base.gameObject.GetAllComponents<MeshRenderer>();
		this.childProjectors = base.gameObject.GetAllComponents<Projector>();
		foreach (Projector projector in this.childProjectors)
		{
			this.ProjectorSizes.Add(projector, projector.orthographicSize * base.transform.localScale.x);
		}
		this.childDecals = base.gameObject.GetAllComponents<DynamicDecal>();
		this.childAudio = base.gameObject.GetAllComponents<AudioPlayer>();
		this.colliders = base.gameObject.GetAllComponents<ActionCollider>();
		this.lights = base.gameObject.GetAllComponents<EffectLight>();
		this.childMaterials = base.gameObject.GetAllComponents<ActionMaterial>();
		this.childScales = base.gameObject.GetAllComponents<ActionScaleCurves>();
		this.childIndicators = base.gameObject.GetAllComponents<EntityIndicator>();
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00006EF4 File Offset: 0x000050F4
	internal virtual void OnEnable()
	{
		this.isFinished = false;
		this.timeAlive = 0f;
		this.opacity = 0f;
		this.triggeredFadeOut = false;
		foreach (Projector projector in this.childProjectors)
		{
			ProjectorScale component = projector.GetComponent<ProjectorScale>();
			if (component != null)
			{
				component.Setup(this);
			}
		}
		foreach (DynamicDecal dynamicDecal in this.childDecals)
		{
			dynamicDecal.Setup(this);
		}
		foreach (ActionMaterial actionMaterial in this.childMaterials)
		{
			actionMaterial.Enter();
		}
		foreach (EntityIndicator entityIndicator in this.childIndicators)
		{
			entityIndicator.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00007040 File Offset: 0x00005240
	internal void SetupSFX()
	{
		AudioManager.SetupSFXObject(base.gameObject, this.actionProps.SourceControl is PlayerControl && this.actionProps.SourceControl != PlayerControl.myInstance);
		foreach (EffectBase effectBase in base.gameObject.GetAllComponents<EffectBase>())
		{
			effectBase.SetupInfo(this.actionProps);
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x000070D0 File Offset: 0x000052D0
	public virtual void Expire(EffectProperties props, ActionEffect.EffectExpireReason reason, bool ignoreEvents = false)
	{
		this.Finish();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000070D8 File Offset: 0x000052D8
	internal virtual void Finish()
	{
		if (this.isFinished)
		{
			return;
		}
		this.isFinished = true;
		foreach (ParticleSystem particleSystem in this.childSystems)
		{
			if (particleSystem != null && particleSystem.gameObject.activeSelf && particleSystem != null)
			{
				particleSystem.Stop();
			}
		}
		foreach (ActionMaterial actionMaterial in this.childMaterials)
		{
			actionMaterial.Exit();
		}
		foreach (AudioPlayer audioPlayer in this.childAudio)
		{
			if (audioPlayer != null)
			{
				audioPlayer.Stop();
			}
		}
		foreach (ActionCollider actionCollider in this.colliders)
		{
			if (actionCollider.activated)
			{
				actionCollider.Deactivate();
			}
		}
		foreach (EffectLight effectLight in this.lights)
		{
			effectLight.Deactivate();
		}
		foreach (ActionScaleCurves actionScaleCurves in this.childScales)
		{
			actionScaleCurves.DoScaleOut();
		}
		foreach (EntityIndicator entityIndicator in this.childIndicators)
		{
			entityIndicator.gameObject.SetActive(false);
		}
		if (this.actionProps != null)
		{
			if (this.actionProps.SourceControl != null)
			{
				this.actionProps.SourceControl.OwnedEffects.Remove(this);
			}
			else
			{
				GameplayManager.WorldEffects.Remove(this);
			}
		}
		foreach (ActionEnabler actionEnabler in base.gameObject.GetAllComponents<ActionEnabler>())
		{
			actionEnabler.Ended();
		}
		ActionPool.ReleaseDelayed(base.gameObject, this.DestroyAfterCompleted);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00007380 File Offset: 0x00005580
	public void Update()
	{
		if (PausePanel.IsGamePaused)
		{
			return;
		}
		if (this.isFinished)
		{
			this.FadeOut();
			return;
		}
		this.timeAlive += Time.deltaTime;
		this.TickUpdate();
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000073B4 File Offset: 0x000055B4
	public virtual void TickUpdate()
	{
		foreach (Projector projector in this.childProjectors)
		{
			projector.orthographicSize = this.ProjectorSizes[projector] * base.transform.localScale.x;
		}
		foreach (ActionMaterial actionMaterial in this.childMaterials)
		{
			actionMaterial.TickUpdate();
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00007464 File Offset: 0x00005664
	internal void ApplyEffects(List<Node> effectNodes, EntityControl entity, bool updateOutputPoint = false)
	{
		if (entity == null)
		{
			return;
		}
		this.ApplyEffects(effectNodes, entity, entity.display.CenterOfMass.position, updateOutputPoint);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000748C File Offset: 0x0000568C
	internal void ApplyEffects(List<Node> effectNodes, EntityControl entity, Vector3 atPoint, bool updateOutputPoint = false)
	{
		if (effectNodes == null || effectNodes.Count == 0 || entity == null)
		{
			return;
		}
		foreach (Node node in effectNodes)
		{
			EffectNode effectNode = node as EffectNode;
			if (!(effectNode == null) && !(entity == null))
			{
				EffectProperties effectProperties = this.actionProps.Copy(false);
				Location location = Location.AtWorldPoint(base.transform.position);
				location.transformOverride = base.transform;
				effectProperties.SourceLocation = base.transform;
				effectProperties.StartLoc = new global::Pose(location, effectProperties.StartLoc.GetLookAt());
				if (updateOutputPoint)
				{
					effectProperties.OutLoc = global::Pose.WorldPoint(atPoint, (atPoint - base.transform.position).normalized);
				}
				effectProperties.Affected = entity.gameObject;
				effectNode.Apply(effectProperties);
			}
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00007598 File Offset: 0x00005798
	internal virtual void FadeOut()
	{
		foreach (Projector projector in this.childProjectors)
		{
			if (!(projector.material == null))
			{
				this.opacity += Time.deltaTime;
				this.opacity = Mathf.Clamp(this.opacity, 0f, 1f);
				projector.material.SetFloat("_DissolveAmount", this.opacity);
			}
		}
		if (!this.triggeredFadeOut)
		{
			foreach (DynamicDecal dynamicDecal in this.childDecals)
			{
				dynamicDecal.Hide();
			}
			this.triggeredFadeOut = true;
		}
		foreach (ActionMaterial actionMaterial in this.childMaterials)
		{
			actionMaterial.TickUpdate();
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000076C4 File Offset: 0x000058C4
	public void Setup(EffectProperties actionProps)
	{
		this.actionProps = actionProps;
		this.actionProps.SourceLocation = base.transform;
		this.sourceInstanceID = actionProps.RandSeed;
		Location loc = new Location
		{
			transformOverride = base.transform,
			LocType = LocationType.Transform
		};
		this.actionProps.StartLoc.OverrideLocation(loc);
		if (this.actionProps.SourceControl != null)
		{
			this.actionProps.SourceControl.OwnedEffects.Add(this);
		}
		else
		{
			GameplayManager.WorldEffects.Add(this);
		}
		if (actionProps.SourceControl != null)
		{
			this.IsEnemyEffect = (actionProps.SourceControl.TeamID == 2);
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00007778 File Offset: 0x00005978
	public ActionEffect()
	{
	}

	// Token: 0x0400008E RID: 142
	public float DestroyAfterCompleted = 1f;

	// Token: 0x0400008F RID: 143
	internal List<ParticleSystem> childSystems = new List<ParticleSystem>();

	// Token: 0x04000090 RID: 144
	internal List<MeshRenderer> childMeshes = new List<MeshRenderer>();

	// Token: 0x04000091 RID: 145
	internal List<Projector> childProjectors = new List<Projector>();

	// Token: 0x04000092 RID: 146
	internal List<DynamicDecal> childDecals = new List<DynamicDecal>();

	// Token: 0x04000093 RID: 147
	internal Dictionary<Projector, float> ProjectorSizes = new Dictionary<Projector, float>();

	// Token: 0x04000094 RID: 148
	internal List<AudioPlayer> childAudio = new List<AudioPlayer>();

	// Token: 0x04000095 RID: 149
	internal List<ActionCollider> colliders = new List<ActionCollider>();

	// Token: 0x04000096 RID: 150
	internal List<EffectLight> lights = new List<EffectLight>();

	// Token: 0x04000097 RID: 151
	internal List<ActionMaterial> childMaterials = new List<ActionMaterial>();

	// Token: 0x04000098 RID: 152
	internal List<ActionScaleCurves> childScales = new List<ActionScaleCurves>();

	// Token: 0x04000099 RID: 153
	internal List<EntityIndicator> childIndicators = new List<EntityIndicator>();

	// Token: 0x0400009A RID: 154
	internal EffectProperties actionProps;

	// Token: 0x0400009B RID: 155
	[NonSerialized]
	public float timeAlive;

	// Token: 0x0400009C RID: 156
	[NonSerialized]
	public string sourceGUID;

	// Token: 0x0400009D RID: 157
	[NonSerialized]
	public int sourceInstanceID;

	// Token: 0x0400009E RID: 158
	[NonSerialized]
	public bool IsEnemyEffect;

	// Token: 0x0400009F RID: 159
	private float opacity;

	// Token: 0x040000A0 RID: 160
	private bool triggeredFadeOut;

	// Token: 0x040000A1 RID: 161
	[NonSerialized]
	internal bool isFinished;

	// Token: 0x020003EB RID: 1003
	public enum EffectExpireReason
	{
		// Token: 0x040020D7 RID: 8407
		Distance,
		// Token: 0x040020D8 RID: 8408
		Time,
		// Token: 0x040020D9 RID: 8409
		Impact,
		// Token: 0x040020DA RID: 8410
		CasterDead,
		// Token: 0x040020DB RID: 8411
		Cancel
	}
}
