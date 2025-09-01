using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class Projectile : ActionEffect
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000CF RID: 207 RVA: 0x00009EA4 File Offset: 0x000080A4
	private float tickDistance
	{
		get
		{
			return (this.velocity * GameplayManager.deltaTime).magnitude;
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00009EC9 File Offset: 0x000080C9
	public override void Awake()
	{
		base.Awake();
		this.baseScale = base.transform.localScale.x;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00009EE8 File Offset: 0x000080E8
	internal override void OnEnable()
	{
		this.hasEnvInteract = false;
		this.hasEntityInteract = false;
		this.bounceCount = 0;
		this.skippedFirst = false;
		this.impactEnvLastFrame = false;
		this.doneLifeEvents = false;
		this.lifeTick = 0f;
		this.distTick = 0f;
		this.cacheSize = 0f;
		this.cacheRange = 0f;
		this.cacheSpeed = 0f;
		this.cacheLifeT = 0f;
		this.cacheSpeedT = 0f;
		this.distanceTraveled = 0f;
		this.curSpeedMult = 1f;
		this.expired = false;
		this.ShouldExpire = false;
		this.inDisableFrame = false;
		this.AllHits.Clear();
		this.hitColliders.Clear();
		this.enteredTriggers.Clear();
		base.OnEnable();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00009FC0 File Offset: 0x000081C0
	public void Activate(ProjectileNode props, float minEnvDist)
	{
		Projectile.AllProjectiles.Add(this);
		this.properties = props;
		this.minEnvDistance = minEnvDist;
		this.actionProps.StartLoc = global::Pose.WorldPoint(base.transform.position, (this.velocity.magnitude == 0f) ? base.transform.forward : this.velocity.normalized);
		this.actionProps.EffectSource = EffectSource.Projectile;
		this.overrides.Clear();
		if (this.actionProps.SourceControl != null && this.properties.CanOverride)
		{
			this.actionProps.Increment(EProp.Override_Depth, 1);
			this.actionProps.SourceControl.AllAugments(true, null).OverrideNodeEffects(this.actionProps, this.properties, ref this.overrides);
		}
		this.lifetimeProps = (this.properties.LifetimeProps as ProjectileLifetimeProps);
		this.SetupMoveModules();
		if (this.moveProps == null)
		{
			UnityEngine.Debug.LogError("Projectile " + base.gameObject.name + " has no movement properties!");
		}
		if (this.moveProps != null)
		{
			this.velocity = base.transform.forward * this.GetSpeed();
		}
		this.SetupInteractions();
		this.actionProps.SourceLocation = base.transform;
		foreach (Node node in this.properties.OnSpawn)
		{
			(node as EffectNode).Invoke(this.actionProps.Copy(false));
		}
		base.SetupSFX();
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000A188 File Offset: 0x00008388
	private void SetupMoveModules()
	{
		this.moveProps = (this.properties.MoveProps as ProjectileMoveProps);
		this.moveModules.Clear();
		foreach (Node node in this.moveProps.Modules)
		{
			ProjectileMoveModuleNode node2 = (ProjectileMoveModuleNode)node;
			this.moveModules.Add(new Projectile.ProjectileMoveMods
			{
				node = node2
			});
		}
		foreach (ModOverrideNode modOverrideNode in this.overrides)
		{
			foreach (Node node3 in ((ProjectileOverrideNode)modOverrideNode).MoveProps)
			{
				ProjectileMoveModuleNode node4 = (ProjectileMoveModuleNode)node3;
				this.moveModules.Add(new Projectile.ProjectileMoveMods
				{
					node = node4
				});
			}
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000A2AC File Offset: 0x000084AC
	private void SetupInteractions()
	{
		this.interactions.Clear();
		int num = 0;
		int num2 = 0;
		foreach (Node node in this.properties.Interactions)
		{
			ProjectileInteractionNode projectileInteractionNode = (ProjectileInteractionNode)node;
			if (!(projectileInteractionNode == null))
			{
				if (projectileInteractionNode.InteractsWith == EffectInteractsWith.Anything)
				{
					this.hasEnvInteract = true;
					this.hasEntityInteract = true;
					num = Mathf.Max(num, projectileInteractionNode.PierceCount);
					num2 = Mathf.Max(num2, projectileInteractionNode.PierceCount);
				}
				else if (projectileInteractionNode.InteractsWith == EffectInteractsWith.Environment)
				{
					this.hasEnvInteract = true;
					num2 = Mathf.Max(num2, projectileInteractionNode.PierceCount);
				}
				else
				{
					this.hasEntityInteract = true;
					num = Mathf.Max(num, projectileInteractionNode.PierceCount);
				}
				this.interactions.Add(new Projectile.ProjectileInteraction
				{
					node = projectileInteractionNode,
					count = 0,
					affected = new HashSet<EntityControl>()
				});
			}
		}
		foreach (ModOverrideNode modOverrideNode in this.overrides)
		{
			foreach (Node node2 in ((ProjectileOverrideNode)modOverrideNode).Interactions)
			{
				ProjectileInteractionNode projectileInteractionNode2 = (ProjectileInteractionNode)node2;
				if (!(projectileInteractionNode2 == null))
				{
					if (projectileInteractionNode2.InteractsWith == EffectInteractsWith.Anything)
					{
						this.hasEnvInteract = true;
						this.hasEntityInteract = true;
						num = Mathf.Max(num, projectileInteractionNode2.PierceCount);
						num2 = Mathf.Max(num2, projectileInteractionNode2.PierceCount);
					}
					else if (projectileInteractionNode2.InteractsWith == EffectInteractsWith.Environment)
					{
						this.hasEnvInteract = true;
						num2 = Mathf.Max(num2, projectileInteractionNode2.PierceCount);
					}
					else
					{
						this.hasEntityInteract = true;
						num = Mathf.Max(num, projectileInteractionNode2.PierceCount);
					}
					this.interactions.Add(new Projectile.ProjectileInteraction
					{
						node = projectileInteractionNode2,
						count = 0,
						affected = new HashSet<EntityControl>()
					});
				}
			}
		}
		foreach (Projectile.ProjectileInteraction projectileInteraction in this.interactions)
		{
			if (projectileInteraction.node.InteractsWith == EffectInteractsWith.Anything)
			{
				projectileInteraction.pierceCount = Mathf.Max(num, num2);
			}
			else if (projectileInteraction.node.InteractsWith == EffectInteractsWith.Environment)
			{
				projectileInteraction.pierceCount = num2;
			}
			else if (projectileInteraction.node.InteractsWith == EffectInteractsWith.Enemies)
			{
				projectileInteraction.pierceCount = num;
			}
			else
			{
				projectileInteraction.pierceCount = projectileInteraction.node.PierceCount;
			}
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000A584 File Offset: 0x00008784
	public override void TickUpdate()
	{
		if (this.actionProps == null)
		{
			return;
		}
		if (!this.skippedFirst)
		{
			this.skippedFirst = true;
			return;
		}
		this.actionProps.Lifetime = this.timeAlive;
		this.actionProps.SetExtra(EProp.Distance, this.distanceTraveled);
		if (this.bounceCount > 0)
		{
			this.actionProps.SetExtra(EProp.Projectile_BounceCount, (float)this.bounceCount);
		}
		if (this.CurPosition == null)
		{
			this.CurPosition = global::Pose.WorldPoint(base.transform.position, (this.velocity.magnitude == 0f) ? base.transform.forward : this.velocity.normalized);
		}
		else
		{
			Vector3 position = base.transform.position;
			this.CurPosition.UpdateWorldPoint(position, position + ((this.velocity.magnitude == 0f) ? base.transform.forward : this.velocity.normalized));
		}
		this.actionProps.StartLoc = (this.actionProps.OutLoc = this.CurPosition);
		foreach (Projectile.ProjectileInteraction projectileInteraction in this.interactions)
		{
			projectileInteraction.bouncedThisFrame = false;
		}
		if (this.CheckLifetimeEnded(this.actionProps))
		{
			return;
		}
		if (this.moveProps == null)
		{
			return;
		}
		this.moveProps.Move(this, this.actionProps, this.moveModules);
		if (!this.inDisableFrame && (this.hasEnvInteract || this.hasEntityInteract))
		{
			this.CheckInteraction(this.actionProps);
		}
		int num = 0;
		foreach (Projectile.ProjectileInteraction projectileInteraction2 in this.interactions)
		{
			num = Mathf.Max(projectileInteraction2.count, num);
		}
		this.actionProps.SetExtra(EProp.Projectile_HitCount, (float)num);
		if (this.isFinished || this.inDisableFrame)
		{
			return;
		}
		base.transform.position += this.velocity * Time.deltaTime;
		this.distanceTraveled += this.tickDistance;
		base.TickUpdate();
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000A7E0 File Offset: 0x000089E0
	private void CheckInteraction(EffectProperties props)
	{
		if (this.interactions.Count == 0)
		{
			return;
		}
		this.impactEnvLastFrame = false;
		float num = this.GetRadius();
		Ray ray = new Ray(base.transform.position, this.velocity.normalized);
		float num2 = this.velocity.magnitude * Time.deltaTime * 1.01f;
		bool flag = this.interactsWithEnv();
		bool flag2 = this.interactsWithEntities();
		LayerMask mask = 0;
		if (flag)
		{
			mask += GameplayManager.instance.EnvironmentMask;
		}
		if (flag2)
		{
			mask += GameplayManager.instance.EntityMask;
		}
		int num3 = Physics.RaycastNonAlloc(ray, this.rayHitBuffer, num2 + num, mask);
		int num4 = flag ? Physics.SphereCastNonAlloc(ray, num, this.envSphereHitBuffer, num2 + num, GameplayManager.instance.EnvironmentMask) : 0;
		float num5 = num * this.entityRadiusMult;
		int num6 = flag2 ? Physics.SphereCastNonAlloc(ray, num5, this.entitySphereHitBuffer, num2 + num5, GameplayManager.instance.EntityMask) : 0;
		this.AllHits.Clear();
		this.hitColliders.Clear();
		for (int i = 0; i < num3; i++)
		{
			RaycastHit item = this.rayHitBuffer[i];
			if (!(item.collider == null) && !(item.point == Vector3.zero))
			{
				this.AllHits.Add(item);
				this.hitColliders.Add(item.collider);
			}
		}
		for (int j = 0; j < num6; j++)
		{
			RaycastHit item2 = this.entitySphereHitBuffer[j];
			if (!(item2.collider == null) && !(item2.point == Vector3.zero) && !this.hitColliders.Contains(item2.collider))
			{
				this.AllHits.Add(item2);
				this.hitColliders.Add(item2.collider);
			}
		}
		for (int k = 0; k < num4; k++)
		{
			RaycastHit item3 = this.envSphereHitBuffer[k];
			if (!(item3.collider == null) && !(item3.point == Vector3.zero) && !this.hitColliders.Contains(item3.collider))
			{
				this.AllHits.Add(item3);
				this.hitColliders.Add(item3.collider);
			}
		}
		if (this.AllHits.Count > 0)
		{
			this.AllHits.Sort((RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
		}
		foreach (RaycastHit hit in this.AllHits)
		{
			if (this.isFinished)
			{
				break;
			}
			if (this.inDisableFrame)
			{
				break;
			}
			this.HandleCollision(hit, props.Copy(false));
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000AB08 File Offset: 0x00008D08
	private bool CheckLifetimeEnded(EffectProperties props)
	{
		if (this.inDisableFrame)
		{
			return false;
		}
		if (this.ShouldExpire)
		{
			this.Expire((this.expireProps == null) ? props : this.expireProps, this.shouldExpireReason, false);
			return true;
		}
		if (this.lifetimeProps != null)
		{
			if (this.lifetimeProps.HasLifeEvents() && this.timeAlive - this.lifeTick >= this.lifetimeProps.LifeEventRate && (!this.doneLifeEvents || this.lifetimeProps.LifeRepeat))
			{
				this.doneLifeEvents = true;
				this.lifeTick = this.timeAlive;
				foreach (Node node in this.lifetimeProps.OnLived)
				{
					(node as EffectNode).Invoke(props);
				}
				foreach (ModOverrideNode modOverrideNode in this.overrides)
				{
					foreach (Node node2 in ((ProjectileOverrideNode)modOverrideNode).OnLived)
					{
						(node2 as EffectNode).Invoke(props);
					}
				}
			}
			if (this.lifetimeProps.HasDistEvents() && this.distanceTraveled - this.distTick >= this.lifetimeProps.DistanceEventRate)
			{
				this.distTick = this.distanceTraveled;
				foreach (Node node3 in this.lifetimeProps.OnDistance)
				{
					(node3 as EffectNode).Invoke(props);
				}
				foreach (ModOverrideNode modOverrideNode2 in this.overrides)
				{
					foreach (Node node4 in ((ProjectileOverrideNode)modOverrideNode2).OnDistance)
					{
						(node4 as EffectNode).Invoke(props);
					}
				}
			}
			float num = this.lifetimeProps.MaxLifetime;
			foreach (ModOverrideNode modOverrideNode3 in this.overrides)
			{
				ProjectileOverrideNode projectileOverrideNode = (ProjectileOverrideNode)modOverrideNode3;
				if (projectileOverrideNode.LifetimeProps != null)
				{
					ProjectileLifetimeProps projectileLifetimeProps = projectileOverrideNode.LifetimeProps as ProjectileLifetimeProps;
					if (projectileLifetimeProps != null)
					{
						num *= projectileLifetimeProps.MaxLifetime;
					}
				}
			}
			if (this.timeAlive >= this.lifetimeProps.MaxLifetime && this.lifetimeProps.MaxLifetime > 0f)
			{
				this.Expire(props, ActionEffect.EffectExpireReason.Time, false);
				return true;
			}
		}
		if (this.cacheLifeT > 0f)
		{
			this.cacheLifeT -= GameplayManager.deltaTime;
		}
		else
		{
			this.cacheSize = this.baseScale * this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Projectile_Size, 1f);
			this.cacheRange = ((this.moveProps != null) ? this.moveProps.MaxDist : 100f);
			if (this.actionProps != null && this.actionProps.SourceControl != null)
			{
				this.cacheRange = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Projectile_Range, this.cacheRange);
			}
			this.cacheLifeT = 0.1211f;
		}
		float num2 = this.cacheSize;
		if (this.lifetimeProps != null)
		{
			num2 *= this.lifetimeProps.SizeOverTime.Evaluate(this.timeAlive);
		}
		foreach (ModOverrideNode modOverrideNode4 in this.overrides)
		{
			ProjectileOverrideNode projectileOverrideNode2 = (ProjectileOverrideNode)modOverrideNode4;
			if (projectileOverrideNode2.LifetimeProps != null)
			{
				ProjectileLifetimeProps projectileLifetimeProps2 = projectileOverrideNode2.LifetimeProps as ProjectileLifetimeProps;
				if (projectileLifetimeProps2 != null)
				{
					num2 = projectileLifetimeProps2.SizeOverTime.Evaluate(this.timeAlive);
				}
			}
		}
		num2 = Mathf.Max(this.MinScale, num2);
		base.transform.localScale = Vector3.one * num2;
		if (this.distanceTraveled >= this.cacheRange)
		{
			this.Expire(props, ActionEffect.EffectExpireReason.Distance, false);
			return true;
		}
		return false;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000AFC0 File Offset: 0x000091C0
	private bool HandleCollision(RaycastHit hit, EffectProperties propsCopy)
	{
		if (this.inDisableFrame || this.ShouldExpire)
		{
			return false;
		}
		Projectile.ProjectileImpact projectileImpact = new Projectile.ProjectileImpact(hit.collider.gameObject);
		bool result = false;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (Projectile.ProjectileInteraction projectileInteraction in this.interactions)
		{
			ProjectileInteractionNode node = projectileInteraction.node;
			if ((node.InteractsWith != EffectInteractsWith.Allies || !(projectileImpact.entity == propsCopy.SourceControl) || !node.IgnoreSelf) && (node.InteractsWith != EffectInteractsWith.Environment || !projectileInteraction.bouncedThisFrame) && node.InteractionDelay <= this.timeAlive)
			{
				ValueTuple<bool, bool> valueTuple = this.DoesAffect(node.InteractsWith, projectileImpact);
				bool item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				if (projectileImpact.IType == Projectile.ImpactType.Trigger && item2)
				{
					this.InteractTrigger(projectileImpact.trigger);
				}
				else if (projectileImpact.IType == Projectile.ImpactType.Trigger)
				{
					continue;
				}
				if (projectileImpact.IType == Projectile.ImpactType.Entity && projectileInteraction.node.Filter != null)
				{
					LogicNode logicNode = projectileInteraction.node.Filter as LogicNode;
					if (logicNode != null)
					{
						EffectProperties effectProperties = propsCopy.Copy(false);
						EffectProperties effectProperties2 = effectProperties;
						EntityControl entity = projectileImpact.entity;
						effectProperties2.Affected = ((entity != null) ? entity.gameObject : null);
						if (!logicNode.Evaluate(effectProperties))
						{
							continue;
						}
					}
				}
				if (item && (projectileImpact.IType != Projectile.ImpactType.Entity || !projectileInteraction.affected.Contains(projectileImpact.entity)))
				{
					if (projectileImpact.IType == Projectile.ImpactType.Entity)
					{
						projectileInteraction.affected.Add(projectileImpact.entity);
					}
					if (projectileImpact.IType == Projectile.ImpactType.Default)
					{
						if (this.impactEnvLastFrame || (this.minEnvDistance > 0f && this.distanceTraveled + this.tickDistance < this.minEnvDistance && projectileImpact.root.GetComponent<Terrain>() == null))
						{
							continue;
						}
						flag3 = true;
					}
					propsCopy.Affected = hit.collider.gameObject;
					projectileInteraction.count++;
					propsCopy.StartLoc = (propsCopy.OutLoc = global::Pose.WorldPoint(hit.point, hit.normal));
					if (projectileInteraction.pierceCount > 0 && projectileInteraction.count > projectileInteraction.pierceCount && node.OnFinalImpact.Count > 0)
					{
						using (List<Node>.Enumerator enumerator2 = projectileInteraction.node.OnFinalImpact.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Node node2 = enumerator2.Current;
								(node2 as EffectNode).Invoke(propsCopy);
							}
							goto IL_2C5;
						}
						goto IL_28A;
					}
					goto IL_28A;
					IL_2C5:
					if (projectileImpact.reaction != null && propsCopy.IsLocal)
					{
						projectileImpact.reaction.ImpactAction();
					}
					if (propsCopy.SourceControl != null)
					{
						propsCopy.SourceControl.TriggerSnippets(EventTrigger.ProjectileImpact, propsCopy.Copy(false), this.properties.SnippetChanceMult);
						if (projectileImpact.entity != null && !this.actionProps.HasExtra(EProp.Snip_DidHit))
						{
							this.actionProps.SetExtra(EProp.Snip_DidHit, 1f);
							propsCopy.SourceControl.TriggerSnippets(EventTrigger.AbilityHit, propsCopy.Copy(false), this.properties.SnippetChanceMult);
							propsCopy.TryAbilityFirstHit();
						}
					}
					if (projectileInteraction.pierceCount < projectileInteraction.count)
					{
						flag |= true;
						this.shouldExpireReason = ActionEffect.EffectExpireReason.Impact;
					}
					else
					{
						Vector3 normalized = this.velocity.normalized;
						if (node.Result == ProjectileInteractionNode.VelBehaviour.Reflect && !flag2)
						{
							this.Bounce(hit.point, hit.normal, propsCopy, node.OutwardForceAdd);
							flag2 = true;
							projectileInteraction.bouncedThisFrame = true;
						}
						if (node.AngleAffectDamp)
						{
							float num = Mathf.Abs(Vector3.Dot(normalized, hit.normal));
							this.curSpeedMult *= Mathf.Lerp(node.VelocityDampen, 1f, 1f - num);
						}
						else
						{
							this.curSpeedMult *= node.VelocityDampen;
						}
					}
					result = true;
					continue;
					IL_28A:
					foreach (Node node3 in node.OnImpact)
					{
						(node3 as EffectNode).Invoke(propsCopy);
					}
					goto IL_2C5;
				}
			}
		}
		this.impactEnvLastFrame = flag3;
		if (flag)
		{
			float d = Vector3.Distance(base.transform.position, hit.point) - this.GetRadius();
			base.transform.position += base.transform.forward * d;
			this.Expire(propsCopy, this.shouldExpireReason, false);
		}
		return result;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000B4C0 File Offset: 0x000096C0
	private void Bounce(Vector3 point, Vector3 normal, EffectProperties propsCopy, float outwardForceAdd = 0f)
	{
		this.velocity = Vector3.Lerp(Vector3.Reflect(this.velocity, normal), normal, outwardForceAdd);
		if (this.velocity.magnitude < 0.01f)
		{
			this.velocity = Vector3.zero;
		}
		propsCopy.StartLoc = (propsCopy.OutLoc = global::Pose.WorldPoint(point, normal));
		this.bounceCount++;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000B528 File Offset: 0x00009728
	private bool interactsWithEnv()
	{
		foreach (Projectile.ProjectileInteraction projectileInteraction in this.interactions)
		{
			if (projectileInteraction.node.InteractsWith == EffectInteractsWith.Environment || projectileInteraction.node.InteractsWith == EffectInteractsWith.Anything)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000B598 File Offset: 0x00009798
	private bool interactsWithEntities()
	{
		using (List<Projectile.ProjectileInteraction>.Enumerator enumerator = this.interactions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.node.InteractsWith != EffectInteractsWith.Environment)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000B5F8 File Offset: 0x000097F8
	public float GetSpeed()
	{
		if (this.cacheSpeedT > 0f)
		{
			this.cacheSpeedT -= GameplayManager.deltaTime;
		}
		else
		{
			this.cacheSpeed = this.moveProps.speed;
			if (this.actionProps != null && this.actionProps.SourceControl != null)
			{
				this.cacheSpeed = Mathf.Max(0.01f * this.cacheSpeed, this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Projectile_Speed, this.cacheSpeed));
			}
			this.cacheSpeedT = 0.1377f;
		}
		float num = this.cacheSpeed;
		if (this.moveProps.Value != null)
		{
			NumberNode numberNode = this.moveProps.Value as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(this.actionProps);
			}
		}
		num *= Mathf.Max(this.moveProps.speedCurve.Evaluate(this.timeAlive), 1E-05f);
		num *= this.curSpeedMult;
		return Mathf.Max(num, 0.001f);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000B6FC File Offset: 0x000098FC
	public float GetRadius()
	{
		return this.radius * base.transform.localScale.x;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000B715 File Offset: 0x00009915
	private void InteractTrigger(ProjectileTrigger trigger)
	{
		if (this.enteredTriggers.Contains(trigger))
		{
			return;
		}
		this.enteredTriggers.Add(trigger);
		trigger.ProjectileEntered(this);
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000B73C File Offset: 0x0000993C
	public override void Expire(EffectProperties props, ActionEffect.EffectExpireReason reason, bool ignoreExpire = false)
	{
		if (this.expired)
		{
			return;
		}
		this.expired = true;
		if (this.lifetimeProps != null && this.lifetimeProps.OnExpire != null && !ignoreExpire)
		{
			foreach (Node node in this.lifetimeProps.OnExpire)
			{
				(node as EffectNode).Invoke(props);
			}
			foreach (ModOverrideNode modOverrideNode in this.overrides)
			{
				foreach (Node node2 in ((ProjectileOverrideNode)modOverrideNode).OnExpire)
				{
					(node2 as EffectNode).Invoke(props);
				}
			}
			if (reason != ActionEffect.EffectExpireReason.Impact)
			{
				foreach (Node node3 in this.lifetimeProps.OnExpireNatural)
				{
					(node3 as EffectNode).Invoke(props);
				}
			}
		}
		base.StartCoroutine("DisableFrame");
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000B8AC File Offset: 0x00009AAC
	public void ExpireNext(ActionEffect.EffectExpireReason reason, EffectProperties overrideOutputProps = null)
	{
		if (this.ShouldExpire)
		{
			return;
		}
		this.expireProps = overrideOutputProps;
		this.shouldExpireReason = reason;
		this.ShouldExpire = true;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000B8CC File Offset: 0x00009ACC
	private IEnumerator DisableFrame()
	{
		this.inDisableFrame = true;
		yield return true;
		this.Finish();
		yield break;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000B8DC File Offset: 0x00009ADC
	private ValueTuple<bool, bool> DoesAffect(EffectInteractsWith mask, Projectile.ProjectileImpact impact)
	{
		switch (impact.IType)
		{
		case Projectile.ImpactType.HitReact:
			return new ValueTuple<bool, bool>(true, true);
		case Projectile.ImpactType.Trigger:
		{
			bool item = impact.trigger.InteractsWithTeamProjectile(this.actionProps);
			return new ValueTuple<bool, bool>(impact.trigger.BlocksInteractingProjectiles(), item);
		}
		case Projectile.ImpactType.Entity:
		{
			if (mask == EffectInteractsWith.Environment || !impact.entity.CanBeInteractedBy(this.actionProps.SourceControl))
			{
				return new ValueTuple<bool, bool>(false, false);
			}
			if (impact.entity.TeamID == -1)
			{
				return new ValueTuple<bool, bool>(true, true);
			}
			bool flag = this.actionProps.CanInteractWith(impact.entity, mask);
			return new ValueTuple<bool, bool>(flag, flag);
		}
		default:
			return new ValueTuple<bool, bool>(mask == EffectInteractsWith.Environment || mask == EffectInteractsWith.Anything, false);
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000B996 File Offset: 0x00009B96
	internal override void Finish()
	{
		Projectile.AllProjectiles.Remove(this);
		base.Finish();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000B9AA File Offset: 0x00009BAA
	private void OnDisable()
	{
		Projectile.AllProjectiles.Remove(this);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0000B9B8 File Offset: 0x00009BB8
	private void OnDrawGizmos()
	{
		Color color = Color.green;
		color.a = 0.3f;
		BetterGizmos.DrawSphere(color, base.transform.position, this.GetRadius() * this.entityRadiusMult);
		color = Color.blue;
		color.a = 0.8f;
		BetterGizmos.DrawSphere(color, base.transform.position, this.GetRadius());
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000BA20 File Offset: 0x00009C20
	public Projectile()
	{
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000BADE File Offset: 0x00009CDE
	// Note: this type is marked as 'beforefieldinit'.
	static Projectile()
	{
	}

	// Token: 0x040000D6 RID: 214
	public static List<Projectile> AllProjectiles = new List<Projectile>();

	// Token: 0x040000D7 RID: 215
	public float radius = 0.1f;

	// Token: 0x040000D8 RID: 216
	public float entityRadiusMult = 1f;

	// Token: 0x040000D9 RID: 217
	public float MinScale = 0.15f;

	// Token: 0x040000DA RID: 218
	private ProjectileNode properties;

	// Token: 0x040000DB RID: 219
	private ProjectileLifetimeProps lifetimeProps;

	// Token: 0x040000DC RID: 220
	private ProjectileMoveProps moveProps;

	// Token: 0x040000DD RID: 221
	private List<Projectile.ProjectileMoveMods> moveModules = new List<Projectile.ProjectileMoveMods>();

	// Token: 0x040000DE RID: 222
	private List<Projectile.ProjectileInteraction> interactions = new List<Projectile.ProjectileInteraction>();

	// Token: 0x040000DF RID: 223
	private List<ModOverrideNode> overrides = new List<ModOverrideNode>();

	// Token: 0x040000E0 RID: 224
	private bool hasEnvInteract;

	// Token: 0x040000E1 RID: 225
	private bool hasEntityInteract;

	// Token: 0x040000E2 RID: 226
	[NonSerialized]
	public Vector3 velocity = Vector3.zero;

	// Token: 0x040000E3 RID: 227
	[NonSerialized]
	public int projectileID;

	// Token: 0x040000E4 RID: 228
	private int bounceCount;

	// Token: 0x040000E5 RID: 229
	private float curSpeedMult = 1f;

	// Token: 0x040000E6 RID: 230
	[NonSerialized]
	public float distanceTraveled;

	// Token: 0x040000E7 RID: 231
	private float baseScale = 1f;

	// Token: 0x040000E8 RID: 232
	private float minEnvDistance;

	// Token: 0x040000E9 RID: 233
	private bool skippedFirst;

	// Token: 0x040000EA RID: 234
	private bool impactEnvLastFrame;

	// Token: 0x040000EB RID: 235
	private RaycastHit[] rayHitBuffer = new RaycastHit[32];

	// Token: 0x040000EC RID: 236
	private RaycastHit[] envSphereHitBuffer = new RaycastHit[16];

	// Token: 0x040000ED RID: 237
	private RaycastHit[] entitySphereHitBuffer = new RaycastHit[32];

	// Token: 0x040000EE RID: 238
	private List<RaycastHit> AllHits = new List<RaycastHit>();

	// Token: 0x040000EF RID: 239
	private HashSet<Collider> hitColliders = new HashSet<Collider>();

	// Token: 0x040000F0 RID: 240
	private List<ProjectileTrigger> enteredTriggers = new List<ProjectileTrigger>();

	// Token: 0x040000F1 RID: 241
	private float lifeTick;

	// Token: 0x040000F2 RID: 242
	private float distTick;

	// Token: 0x040000F3 RID: 243
	private bool doneLifeEvents;

	// Token: 0x040000F4 RID: 244
	private float cacheSize;

	// Token: 0x040000F5 RID: 245
	private float cacheRange;

	// Token: 0x040000F6 RID: 246
	private float cacheLifeT;

	// Token: 0x040000F7 RID: 247
	private float cacheSpeed;

	// Token: 0x040000F8 RID: 248
	private float cacheSpeedT;

	// Token: 0x040000F9 RID: 249
	private bool expired;

	// Token: 0x040000FA RID: 250
	private bool ShouldExpire;

	// Token: 0x040000FB RID: 251
	private ActionEffect.EffectExpireReason shouldExpireReason;

	// Token: 0x040000FC RID: 252
	private EffectProperties expireProps;

	// Token: 0x040000FD RID: 253
	private bool inDisableFrame;

	// Token: 0x040000FE RID: 254
	private global::Pose CurPosition;

	// Token: 0x020003EF RID: 1007
	private class ProjectileImpact
	{
		// Token: 0x0600206A RID: 8298 RVA: 0x000C0268 File Offset: 0x000BE468
		public ProjectileImpact(GameObject root)
		{
			this.root = root;
			this.reaction = root.GetComponent<HitReaction>();
			this.trigger = root.GetComponent<ProjectileTrigger>();
			if (this.trigger == null)
			{
				this.trigger = root.GetComponentInParent<ProjectileTrigger>();
			}
			if (this.trigger != null)
			{
				this.IType = Projectile.ImpactType.Trigger;
			}
			this.entity = root.GetComponent<EntityControl>();
			if (this.entity == null)
			{
				this.entity = root.GetComponentInParent<EntityControl>();
			}
			if (this.entity != null)
			{
				this.IType = Projectile.ImpactType.Entity;
				return;
			}
			if (this.reaction != null)
			{
				this.IType = Projectile.ImpactType.HitReact;
				return;
			}
		}

		// Token: 0x040020E6 RID: 8422
		public Projectile.ImpactType IType;

		// Token: 0x040020E7 RID: 8423
		public HitReaction reaction;

		// Token: 0x040020E8 RID: 8424
		public EntityControl entity;

		// Token: 0x040020E9 RID: 8425
		public ProjectileTrigger trigger;

		// Token: 0x040020EA RID: 8426
		public GameObject root;
	}

	// Token: 0x020003F0 RID: 1008
	private enum ImpactType
	{
		// Token: 0x040020EC RID: 8428
		Default,
		// Token: 0x040020ED RID: 8429
		HitReact,
		// Token: 0x040020EE RID: 8430
		Trigger,
		// Token: 0x040020EF RID: 8431
		Entity
	}

	// Token: 0x020003F1 RID: 1009
	private class ProjectileInteraction
	{
		// Token: 0x0600206B RID: 8299 RVA: 0x000C031B File Offset: 0x000BE51B
		public ProjectileInteraction()
		{
		}

		// Token: 0x040020F0 RID: 8432
		public ProjectileInteractionNode node;

		// Token: 0x040020F1 RID: 8433
		public int pierceCount;

		// Token: 0x040020F2 RID: 8434
		public int count;

		// Token: 0x040020F3 RID: 8435
		public bool bouncedThisFrame;

		// Token: 0x040020F4 RID: 8436
		public HashSet<EntityControl> affected;
	}

	// Token: 0x020003F2 RID: 1010
	public class ProjectileMoveMods
	{
		// Token: 0x0600206C RID: 8300 RVA: 0x000C0323 File Offset: 0x000BE523
		public ProjectileMoveMods()
		{
		}

		// Token: 0x040020F5 RID: 8437
		public ProjectileMoveModuleNode node;

		// Token: 0x040020F6 RID: 8438
		public bool started;

		// Token: 0x040020F7 RID: 8439
		public float progress;

		// Token: 0x040020F8 RID: 8440
		public float arcHeight;

		// Token: 0x040020F9 RID: 8441
		public Vector3 startPt;

		// Token: 0x040020FA RID: 8442
		public Vector3 targPt;

		// Token: 0x040020FB RID: 8443
		public float dist = 1f;

		// Token: 0x040020FC RID: 8444
		public float flightTime;

		// Token: 0x040020FD RID: 8445
		public Vector3 cachedSeek;

		// Token: 0x040020FE RID: 8446
		public int savedSeed;

		// Token: 0x040020FF RID: 8447
		public bool didAssign;
	}

	// Token: 0x020003F3 RID: 1011
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600206D RID: 8301 RVA: 0x000C0336 File Offset: 0x000BE536
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000C0342 File Offset: 0x000BE542
		public <>c()
		{
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000C034C File Offset: 0x000BE54C
		internal int <CheckInteraction>b__49_0(RaycastHit x, RaycastHit y)
		{
			return x.distance.CompareTo(y.distance);
		}

		// Token: 0x04002100 RID: 8448
		public static readonly Projectile.<>c <>9 = new Projectile.<>c();

		// Token: 0x04002101 RID: 8449
		public static Comparison<RaycastHit> <>9__49_0;
	}

	// Token: 0x020003F4 RID: 1012
	[CompilerGenerated]
	private sealed class <DisableFrame>d__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x000C036F File Offset: 0x000BE56F
		[DebuggerHidden]
		public <DisableFrame>d__60(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000C037E File Offset: 0x000BE57E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000C0380 File Offset: 0x000BE580
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Projectile projectile = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				projectile.inDisableFrame = true;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			projectile.Finish();
			return false;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x000C03D5 File Offset: 0x000BE5D5
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000C03DD File Offset: 0x000BE5DD
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000C03E4 File Offset: 0x000BE5E4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002102 RID: 8450
		private int <>1__state;

		// Token: 0x04002103 RID: 8451
		private object <>2__current;

		// Token: 0x04002104 RID: 8452
		public Projectile <>4__this;
	}
}
