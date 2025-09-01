using System;
using System.Collections.Generic;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class AreaOfEffect : ActionEffect
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060000A2 RID: 162 RVA: 0x00007810 File Offset: 0x00005A10
	public int TotalEntered
	{
		get
		{
			if (this.ApplyProps == null)
			{
				return 0;
			}
			int num = 0;
			foreach (AreaOfEffect.ApplyProp applyProp in this.ApplyProps)
			{
				if (applyProp.EverInside.Count > num)
				{
					num = applyProp.EverInside.Count;
				}
			}
			return num;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060000A3 RID: 163 RVA: 0x00007884 File Offset: 0x00005A84
	public int CurrentInside
	{
		get
		{
			if (this.ApplyProps == null)
			{
				return 0;
			}
			HashSet<EntityControl> hashSet = new HashSet<EntityControl>();
			foreach (AreaOfEffect.ApplyProp applyProp in this.ApplyProps)
			{
				foreach (EntityControl entityControl in applyProp.Inside)
				{
					if (!entityControl.IsDead && !hashSet.Contains(entityControl))
					{
						hashSet.Add(entityControl);
					}
				}
			}
			return hashSet.Count;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x060000A4 RID: 164 RVA: 0x00007938 File Offset: 0x00005B38
	private float ActiveAfter
	{
		get
		{
			float num = this.Properties.ActiveAfter;
			foreach (ModOverrideNode modOverrideNode in this.overrides)
			{
				AoEOverrideNode aoEOverrideNode = (AoEOverrideNode)modOverrideNode;
				if (aoEOverrideNode.OverrideDuration)
				{
					num = Mathf.Lerp(num, aoEOverrideNode.ActiveAfter, aoEOverrideNode.ActiveWeight);
				}
			}
			return num;
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000079B4 File Offset: 0x00005BB4
	internal override void OnEnable()
	{
		this.CurrentScale = 1f;
		this.checkRate = 0f;
		this.checkT = 0f;
		this.hasSpawned = false;
		this.hasActivated = false;
		this.cycleTimer = 0f;
		this.ToCheck.Clear();
		this.ToCheckEntities.Clear();
		this.checkedEntities.Clear();
		this.ApplyProps.Clear();
		this.overrides.Clear();
		this.cacheDur = 0f;
		this.cachePropScale = 0f;
		this.cacheScaleT = 0f;
		this.cachePropHeight = 0f;
		this.cacheHeightT = 0f;
		this.numEntered = 0;
		base.OnEnable();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00007A78 File Offset: 0x00005C78
	public void Activate(AoENode props)
	{
		AreaOfEffect.AllAreas.Add(this);
		if (props == null)
		{
			return;
		}
		this.Properties = props;
		this.actionProps.EffectSource = EffectSource.AreaOfEffect;
		if (this.actionProps.SourceControl != null && this.Properties.CanOverride)
		{
			this.actionProps.Increment(EProp.Override_Depth, 1);
			this.actionProps.SourceControl.AllAugments(true, null).OverrideNodeEffects(this.actionProps, this.Properties, ref this.overrides);
		}
		this.overrides.Clear();
		this.ApplyProps.Clear();
		foreach (Node node in this.Properties.ApplyProps)
		{
			AoEApplicationNode p = (AoEApplicationNode)node;
			this.ApplyProps.Add(new AreaOfEffect.ApplyProp(p));
		}
		int num = 0;
		foreach (ModOverrideNode modOverrideNode in this.overrides)
		{
			foreach (Node node2 in ((AoEOverrideNode)modOverrideNode).ApplyProps)
			{
				AoEApplicationNode p2 = (AoEApplicationNode)node2;
				num++;
				this.ApplyProps.Add(new AreaOfEffect.ApplyProp(p2));
			}
		}
		if (this.baseScale == 0f)
		{
			this.baseScale = base.transform.localScale.x;
		}
		this.UpdateScale();
		this.TickUpdate();
		base.SetupSFX();
		if (props.Duration > 2f || props.Duration <= 0f)
		{
			this.checkRate = UnityEngine.Random.Range(0.06f, 0.11f);
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00007C74 File Offset: 0x00005E74
	public override void TickUpdate()
	{
		if (this.Properties == null)
		{
			return;
		}
		if ((this.Properties.Duration > 0f && this.timeAlive >= this.GetDuration()) | (this.Properties.EndWithCaster && (this.actionProps.SourceControl == null || this.actionProps.SourceControl.IsDead)) | (this.ApplyProps != null && this.HitMaxEntered(null)))
		{
			this.Expire(this.actionProps, ActionEffect.EffectExpireReason.Time, false);
			return;
		}
		this.UpdateScale();
		if (this.Properties.HasSpawnDelay() && !this.hasSpawned)
		{
			EffectProperties effectProperties = this.actionProps.Copy(false);
			effectProperties.SourceLocation = base.transform;
			this.hasSpawned = true;
			foreach (Node node in this.Properties.OnSpawn)
			{
				(node as EffectNode).Invoke(effectProperties);
			}
		}
		base.TickUpdate();
		if (this.timeAlive < this.ActiveAfter)
		{
			return;
		}
		this.cycleTimer += Time.deltaTime;
		if (this.Properties.OnTimer.Count > 0 && this.cycleTimer >= this.Properties.TimerCycle)
		{
			this.cycleTimer = 0f;
			foreach (Node node2 in this.Properties.OnTimer)
			{
				(node2 as EffectNode).Invoke(this.actionProps);
			}
		}
		if (!this.hasActivated)
		{
			this.hasActivated = true;
			foreach (Node node3 in this.Properties.OnStart)
			{
				EffectNode effectNode = (EffectNode)node3;
				if (effectNode != null)
				{
					effectNode.Invoke(this.actionProps);
				}
			}
			foreach (ModOverrideNode modOverrideNode in this.overrides)
			{
				foreach (Node node4 in ((AoEOverrideNode)modOverrideNode).OnStart)
				{
					((EffectNode)node4).Invoke(this.actionProps);
				}
			}
		}
		this.CheckAllInside();
		this.actionProps.Lifetime = this.timeAlive;
		this.actionProps.SetExtra(EProp.AoE_EnteredCount, (float)this.TotalEntered);
		this.actionProps.SetExtra(EProp.AoE_CurrentInside, (float)this.CurrentInside);
		this.AoETick();
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00007F7C File Offset: 0x0000617C
	private void UpdateScale()
	{
		float b = this.UpdateScaleInfo();
		Vector3 vector = new Vector3(Mathf.Lerp(1f, b, this.ScaleMultiplier.x), Mathf.Lerp(1f, b, this.ScaleMultiplier.y), Mathf.Lerp(1f, b, this.ScaleMultiplier.z));
		if (base.transform.localScale != vector)
		{
			base.transform.localScale = vector;
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00007FF8 File Offset: 0x000061F8
	private float GetDuration()
	{
		if (this.cacheDur > 0f)
		{
			return this.cacheDur;
		}
		float num = this.Properties.Duration;
		foreach (ModOverrideNode modOverrideNode in this.overrides)
		{
			AoEOverrideNode aoEOverrideNode = (AoEOverrideNode)modOverrideNode;
			if (aoEOverrideNode.OverrideDuration)
			{
				num = aoEOverrideNode.Duration;
			}
		}
		if (this.actionProps != null && this.actionProps.SourceControl != null)
		{
			num = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Area_Lifetime, num);
		}
		this.cacheDur = num + this.ActiveAfter;
		return this.cacheDur;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000080BC File Offset: 0x000062BC
	private float UpdateScaleInfo()
	{
		if (this.actionProps != null && this.actionProps.SourceControl != null && this.actionProps.SourceControl.IsDead && this.cachePropScale > 0f)
		{
			return this.cachePropScale;
		}
		if (this.cacheScaleT > 0f)
		{
			this.cacheScaleT -= GameplayManager.deltaTime;
		}
		else
		{
			float num = this.cachePropScale;
			float startVal = this.Properties.Scale;
			foreach (ModOverrideNode modOverrideNode in this.overrides)
			{
				AoEOverrideNode aoEOverrideNode = (AoEOverrideNode)modOverrideNode;
				if (aoEOverrideNode.OverrideSize)
				{
					startVal = Mathf.Lerp(this.cachePropScale, aoEOverrideNode.Scale, aoEOverrideNode.ScaleWeight);
				}
			}
			if (this.Properties.DynamicScale != null)
			{
				NumberNode numberNode = this.Properties.DynamicScale as NumberNode;
				if (numberNode != null)
				{
					startVal = numberNode.Evaluate(this.actionProps);
				}
			}
			if (this.actionProps != null && this.actionProps.SourceControl != null)
			{
				this.cachePropScale = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Area_Size, startVal);
			}
			else
			{
				this.cachePropScale = startVal;
			}
			this.cacheScaleT = ((num == this.cachePropScale) ? UnityEngine.Random.Range(0.2f, 0.4f) : 0f);
		}
		float num2 = this.cachePropScale;
		num2 *= this.Properties.SizeOverTime.Evaluate(this.timeAlive);
		foreach (ModOverrideNode modOverrideNode2 in this.overrides)
		{
			AoEOverrideNode aoEOverrideNode2 = (AoEOverrideNode)modOverrideNode2;
			if (aoEOverrideNode2.OverrideSize)
			{
				num2 *= aoEOverrideNode2.SizeOverTime.Evaluate(this.timeAlive);
			}
		}
		num2 = Mathf.Max(num2 * this.baseScale, this.MinSize);
		this.CurrentScale = num2;
		return num2;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000082DC File Offset: 0x000064DC
	private float GetHeight()
	{
		if (this.Properties == null || !this.ScaleHeight)
		{
			return this.Height;
		}
		if (this.actionProps != null && this.actionProps.SourceControl != null && this.actionProps.SourceControl.IsDead && this.cachePropHeight > 0f)
		{
			return this.cachePropHeight;
		}
		if (this.cacheHeightT > 0f)
		{
			this.cacheHeightT -= GameplayManager.deltaTime;
		}
		else
		{
			this.cachePropHeight = this.Properties.Scale;
			if (this.actionProps != null && this.actionProps.SourceControl != null)
			{
				this.cachePropHeight = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Area_Size, this.cachePropHeight);
			}
			this.cacheHeightT = 0.1097f;
		}
		float num = this.cachePropHeight;
		num *= this.Properties.SizeOverTime.Evaluate(this.timeAlive);
		return Mathf.Lerp(this.Height, num * this.Height, this.ScaleMultiplier.z);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000083FC File Offset: 0x000065FC
	public override void Expire(EffectProperties inProps, ActionEffect.EffectExpireReason reason, bool ignoreEvents = false)
	{
		if (!ignoreEvents && this.Properties != null)
		{
			global::Pose outLoc = global::Pose.WorldPoint(base.transform.position, base.transform.up);
			foreach (Node node in this.Properties.OnExpire)
			{
				EffectProperties effectProperties = this.actionProps.Copy(false);
				effectProperties.OutLoc = outLoc;
				(node as EffectNode).Invoke(effectProperties);
			}
			foreach (ModOverrideNode modOverrideNode in this.overrides)
			{
				foreach (Node node2 in ((AoEOverrideNode)modOverrideNode).OnExpire)
				{
					EffectNode effectNode = (EffectNode)node2;
					EffectProperties effectProperties2 = this.actionProps.Copy(false);
					effectProperties2.OutLoc = outLoc;
					effectNode.Invoke(effectProperties2);
				}
			}
			foreach (AreaOfEffect.ApplyProp applyProp in this.ApplyProps)
			{
				if (applyProp.props.OnEndInside != null)
				{
					foreach (EntityControl entity in applyProp.Inside)
					{
						base.ApplyEffects(applyProp.props.OnEndInside, entity, true);
					}
				}
			}
		}
		base.Expire(inProps, reason, ignoreEvents);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000085E0 File Offset: 0x000067E0
	private void AoETick()
	{
		foreach (AreaOfEffect.ApplyProp applyProp in this.ApplyProps)
		{
			if (!(applyProp.props == null) && applyProp.props.TickRate > 0f && applyProp.props.OnTick.Count != 0)
			{
				applyProp.tickTimer -= GameplayManager.deltaTime;
				if (applyProp.tickTimer <= 0f)
				{
					applyProp.tickTimer = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Area_TickRate, applyProp.props.TickRate);
					foreach (EntityControl entity in applyProp.Inside)
					{
						base.ApplyEffects(applyProp.props.OnTick, entity, true);
					}
				}
			}
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000086FC File Offset: 0x000068FC
	private void CheckAllInside()
	{
		if (this.actionProps == null || this.ApplyProps.Count == 0)
		{
			return;
		}
		if (this.checkT > 0f)
		{
			this.checkT -= GameplayManager.deltaTime;
			return;
		}
		float num = this.Radius * this.CurrentScale;
		HashSet<EntityControl> hashSet = new HashSet<EntityControl>();
		if (this.Precise && this.Shape == AoEShape.Sphere)
		{
			int num2 = Physics.OverlapSphereNonAlloc(base.transform.position, num, this.preciseColliders, AIManager.instance.AILayerMask);
			for (int i = 0; i < num2; i++)
			{
				EntityControl componentInParent = this.preciseColliders[i].GetComponentInParent<EntityControl>();
				if (componentInParent != null)
				{
					hashSet.Add(componentInParent);
				}
			}
		}
		foreach (AreaOfEffect.ApplyProp interaction in this.ApplyProps)
		{
			this.ApplyInteraction(interaction, num, hashSet);
		}
		this.checkT = this.checkRate;
		if (AreaOfEffect.AllAreas.Count > 30)
		{
			this.checkT *= 2f;
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00008830 File Offset: 0x00006A30
	private void ApplyInteraction(AreaOfEffect.ApplyProp interaction, float distance, HashSet<EntityControl> OverlapEntities)
	{
		this.ToCheck.Clear();
		this.ToCheckEntities.Clear();
		this.checkedEntities.Clear();
		bool flag = interaction.props.Filters != null && interaction.props.Filters.Count > 0;
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!entityControl.IsDead && entityControl.CanBeInteractedBy(this.actionProps.SourceControl) && this.actionProps.CanInteractWith(entityControl, interaction.props.Affects) && (!(entityControl == this.actionProps.SourceControl) || !interaction.props.ExcludeSelf) && (!(entityControl == this.actionProps.AffectedControl) || !interaction.props.ExcludeAffected) && (OverlapEntities.Contains(entityControl) || this.IsEntityInside(entityControl, distance)))
			{
				if (flag)
				{
					EffectProperties effectProperties = this.actionProps.Copy(false);
					effectProperties.SeekTarget = entityControl.gameObject;
					effectProperties.Affected = entityControl.gameObject;
					this.ToCheck.Add(effectProperties);
				}
				else
				{
					this.ToCheckEntities.Add(entityControl);
				}
			}
		}
		if (flag)
		{
			foreach (Node node in interaction.props.Filters)
			{
				((LogicFilterNode)node).Filter(ref this.ToCheck, this.actionProps);
			}
			using (List<EffectProperties>.Enumerator enumerator3 = this.ToCheck.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					EffectProperties effectProperties2 = enumerator3.Current;
					this.HandleEntitiesInside(effectProperties2.AffectedControl, interaction);
					this.checkedEntities.Add(effectProperties2.AffectedControl);
				}
				goto IL_232;
			}
		}
		foreach (EntityControl entityControl2 in this.ToCheckEntities)
		{
			this.HandleEntitiesInside(entityControl2, interaction);
			this.checkedEntities.Add(entityControl2);
		}
		IL_232:
		for (int i = interaction.Inside.Count - 1; i >= 0; i--)
		{
			EntityControl entityControl3 = interaction.Inside[i];
			if (!this.checkedEntities.Contains(entityControl3) && !(entityControl3 == null) && !(entityControl3.display == null))
			{
				interaction.Inside.Remove(entityControl3);
				base.ApplyEffects(interaction.props.OnExit, entityControl3, entityControl3.display.Position, true);
			}
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00008B24 File Offset: 0x00006D24
	private bool IsEntityInside(EntityControl entity, float distance)
	{
		foreach (Transform transform in entity.display.OverlapPoints)
		{
			if (this.PointInside(transform.position, distance))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00008B8C File Offset: 0x00006D8C
	private void HandleEntitiesInside(EntityControl entity, AreaOfEffect.ApplyProp interaction)
	{
		bool flag = interaction.Inside.Contains(entity);
		Vector3 position = entity.display.CenterOfMass.position;
		if (!flag && this.EntityCanEnter(interaction, entity) && !this.HitMaxEntered(interaction))
		{
			interaction.Inside.Add(entity);
			if (!interaction.EverInside.Contains(entity))
			{
				interaction.EverInside.Add(entity);
			}
			this.actionProps.SetExtra(EProp.AoE_EnteredCount, (float)this.TotalEntered);
			this.actionProps.SetExtra(EProp.AoE_CurrentInside, (float)this.CurrentInside);
			this.numEntered++;
			base.ApplyEffects(interaction.props.OnEnter, entity, position, true);
			EffectProperties actionProps = this.actionProps;
			if (((actionProps != null) ? actionProps.SourceControl : null) != null && !this.actionProps.HasExtra(EProp.Snip_DidHit))
			{
				this.actionProps.SetExtra(EProp.Snip_DidHit, 1f);
				this.actionProps.SourceControl.TriggerSnippets(EventTrigger.AbilityHit, this.actionProps.Copy(false), 1f);
				this.actionProps.TryAbilityFirstHit();
			}
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00008CB4 File Offset: 0x00006EB4
	public bool PointInside(Vector3 pt, float distance)
	{
		Transform transform = base.transform;
		Vector3 position = transform.position;
		float num = distance * distance;
		switch (this.Shape)
		{
		case AoEShape.Sphere:
			if (this.InnerRadius > 0f)
			{
				float num2 = this.InnerRadius * distance;
				float num3 = num2 * num2;
				return (transform.position - pt).sqrMagnitude <= num && (position - pt).sqrMagnitude >= num3;
			}
			return (position - pt).sqrMagnitude <= num;
		case AoEShape.Cylinder:
		{
			float height = this.GetHeight();
			Vector3 pos = position;
			Vector3 offset = transform.forward * height / 2f;
			bool flag = this.InCylinder(transform, pt, pos, offset, height, distance);
			if (this.InnerRadius > 0f)
			{
				flag &= !this.InCylinder(transform, pt, pos, offset, height, this.InnerRadius * distance);
			}
			return flag;
		}
		case AoEShape.Arc:
		{
			float num4 = (this.InnerRadius > 0f) ? (this.InnerRadius * distance * (this.InnerRadius * distance)) : 0f;
			Vector3 vector = pt - position;
			bool flag2 = vector.sqrMagnitude <= num;
			if (this.InnerRadius > 0f)
			{
				flag2 &= (vector.sqrMagnitude >= num4);
			}
			Vector3 to = Vector3.ProjectOnPlane(vector, transform.up);
			float num5 = Vector3.Angle(transform.forward, to);
			return flag2 & num5 <= this.Angle * 0.5f;
		}
		default:
			return false;
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00008E50 File Offset: 0x00007050
	private bool InCylinder(Transform t, Vector3 pt, Vector3 pos, Vector3 offset, float height, float distance)
	{
		bool result;
		switch (this.OriginMode)
		{
		case AreaOfEffect.CylinderOrigin.Base:
			result = AreaOfEffect.PointInCylinder(pt, pos - t.forward * 0.25f, pos + t.forward * height, distance);
			break;
		case AreaOfEffect.CylinderOrigin.Center:
			result = AreaOfEffect.PointInCylinder(pt, pos - offset, pos + offset, distance);
			break;
		case AreaOfEffect.CylinderOrigin.Inverted:
			result = AreaOfEffect.PointInCylinder(pt, pos + t.forward * 0.25f, pos - t.forward * height, distance);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00008F04 File Offset: 0x00007104
	private void OnDrawGizmos()
	{
		float num = this.Radius * this.CurrentScale;
		Color color = new Color(0.75f, 0.1f, 0.1f, 0.3f);
		if (this.testInside != null && this.PointInside(this.testInside.position, num))
		{
			color = new Color(0.1f, 0.75f, 0.1f, 0.5f);
		}
		if (this.Shape == AoEShape.Sphere)
		{
			BetterGizmos.DrawCircle2D(new Color(1f, 0.3f, 0.3f, 1f), base.transform.position, base.transform.forward, num);
			Gizmos.color = color;
			Gizmos.DrawSphere(base.transform.position, num);
			if (this.InnerRadius > 0f)
			{
				BetterGizmos.DrawCircle2D(new Color(0.3f, 1f, 0.3f, 1f), base.transform.position, base.transform.forward, num * this.InnerRadius);
				return;
			}
		}
		else if (this.Shape != AoEShape.Cylinder)
		{
			AoEShape shape = this.Shape;
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000902C File Offset: 0x0000722C
	private bool EntityCanEnter(AreaOfEffect.ApplyProp application, EntityControl entity)
	{
		return (!application.props.SingleEntrance || !application.EverInside.Contains(entity)) && (application.props.MaxInsideAtOnce <= 0 || application.Inside.Count < application.props.MaxInsideAtOnce);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00009080 File Offset: 0x00007280
	public static bool PointInCylinder(Vector3 queryPt, Vector3 cylTop, Vector3 cylBottom, float radius)
	{
		Vector3 rhs = cylBottom - cylTop;
		float sqrMagnitude = rhs.sqrMagnitude;
		float num = radius * radius;
		Vector3 lhs = queryPt - cylTop;
		float num2 = Vector3.Dot(lhs, rhs);
		return num2 >= 0f && num2 <= sqrMagnitude && lhs.sqrMagnitude - num2 * num2 / sqrMagnitude <= num;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000090D7 File Offset: 0x000072D7
	internal override void Finish()
	{
		AreaOfEffect.AllAreas.Remove(this);
		base.Finish();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000090EB File Offset: 0x000072EB
	private void OnDisable()
	{
		AreaOfEffect.AllAreas.Remove(this);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x000090FC File Offset: 0x000072FC
	private bool HitMaxEntered(AreaOfEffect.ApplyProp interaction = null)
	{
		if (interaction != null)
		{
			return interaction.props.EndIfEntered > 0 && interaction.EverInside.Count >= interaction.props.EndIfEntered;
		}
		bool flag = this.ApplyProps.Count == 0;
		foreach (AreaOfEffect.ApplyProp applyProp in this.ApplyProps)
		{
			if (applyProp.props.EndIfEntered > 0 && this.numEntered >= applyProp.props.EndIfEntered)
			{
				if (applyProp.props.CancelIfEnd)
				{
					return true;
				}
			}
			else
			{
				flag = true;
			}
		}
		return !flag;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000091C0 File Offset: 0x000073C0
	private bool HasEntitiesInside()
	{
		if (this.ApplyProps != null)
		{
			return false;
		}
		using (List<AreaOfEffect.ApplyProp>.Enumerator enumerator = this.ApplyProps.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Inside.Count > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000922C File Offset: 0x0000742C
	public bool HasEntityInside(EntityControl e)
	{
		if (this.ApplyProps == null)
		{
			return false;
		}
		using (List<AreaOfEffect.ApplyProp>.Enumerator enumerator = this.ApplyProps.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Inside.Contains(e))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00009298 File Offset: 0x00007498
	public AreaOfEffect()
	{
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00009342 File Offset: 0x00007542
	// Note: this type is marked as 'beforefieldinit'.
	static AreaOfEffect()
	{
	}

	// Token: 0x040000A2 RID: 162
	public static List<AreaOfEffect> AllAreas = new List<AreaOfEffect>();

	// Token: 0x040000A3 RID: 163
	public float Radius = 8f;

	// Token: 0x040000A4 RID: 164
	[Range(0f, 1f)]
	public float InnerRadius;

	// Token: 0x040000A5 RID: 165
	public AoEShape Shape;

	// Token: 0x040000A6 RID: 166
	public AreaOfEffect.CylinderOrigin OriginMode;

	// Token: 0x040000A7 RID: 167
	public bool IsNegative = true;

	// Token: 0x040000A8 RID: 168
	public DangerIndicator.DangerLevel Danger;

	// Token: 0x040000A9 RID: 169
	public bool ForceDangerIndicator;

	// Token: 0x040000AA RID: 170
	public float MinSize = 0.1f;

	// Token: 0x040000AB RID: 171
	public bool Precise;

	// Token: 0x040000AC RID: 172
	public float Height = 10f;

	// Token: 0x040000AD RID: 173
	public bool ScaleHeight = true;

	// Token: 0x040000AE RID: 174
	[Range(0f, 360f)]
	public float Angle = 90f;

	// Token: 0x040000AF RID: 175
	private AoENode Properties;

	// Token: 0x040000B0 RID: 176
	private List<AreaOfEffect.ApplyProp> ApplyProps = new List<AreaOfEffect.ApplyProp>();

	// Token: 0x040000B1 RID: 177
	private List<ModOverrideNode> overrides = new List<ModOverrideNode>();

	// Token: 0x040000B2 RID: 178
	[NonSerialized]
	private float baseScale;

	// Token: 0x040000B3 RID: 179
	public float CurrentScale = 1f;

	// Token: 0x040000B4 RID: 180
	private bool hasSpawned;

	// Token: 0x040000B5 RID: 181
	private bool hasActivated;

	// Token: 0x040000B6 RID: 182
	private float cycleTimer;

	// Token: 0x040000B7 RID: 183
	private int numEntered;

	// Token: 0x040000B8 RID: 184
	private float checkRate;

	// Token: 0x040000B9 RID: 185
	private float checkT;

	// Token: 0x040000BA RID: 186
	private List<EffectProperties> ToCheck = new List<EffectProperties>();

	// Token: 0x040000BB RID: 187
	private List<EntityControl> ToCheckEntities = new List<EntityControl>();

	// Token: 0x040000BC RID: 188
	private List<EntityControl> checkedEntities = new List<EntityControl>();

	// Token: 0x040000BD RID: 189
	private float cacheDur;

	// Token: 0x040000BE RID: 190
	private float cachePropScale;

	// Token: 0x040000BF RID: 191
	private float cacheScaleT;

	// Token: 0x040000C0 RID: 192
	private float cachePropHeight;

	// Token: 0x040000C1 RID: 193
	private float cacheHeightT;

	// Token: 0x040000C2 RID: 194
	public Transform testInside;

	// Token: 0x040000C3 RID: 195
	public Vector3 ScaleMultiplier = Vector3.one;

	// Token: 0x040000C4 RID: 196
	private Collider[] preciseColliders = new Collider[128];

	// Token: 0x020003EC RID: 1004
	[Serializable]
	public class ApplyProp
	{
		// Token: 0x06002066 RID: 8294 RVA: 0x000C0208 File Offset: 0x000BE408
		public ApplyProp(AoEApplicationNode p)
		{
			this.props = p;
		}

		// Token: 0x040020DC RID: 8412
		public float tickTimer;

		// Token: 0x040020DD RID: 8413
		public AoEApplicationNode props;

		// Token: 0x040020DE RID: 8414
		public List<EntityControl> Inside = new List<EntityControl>();

		// Token: 0x040020DF RID: 8415
		public List<EntityControl> EverInside = new List<EntityControl>();
	}

	// Token: 0x020003ED RID: 1005
	public enum CylinderOrigin
	{
		// Token: 0x040020E1 RID: 8417
		Base,
		// Token: 0x040020E2 RID: 8418
		Center,
		// Token: 0x040020E3 RID: 8419
		Inverted
	}
}
