using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class SpawnProjectileEffectNode : EffectNode
{
	// Token: 0x06001A9D RID: 6813 RVA: 0x000A5701 File Offset: 0x000A3901
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn Projectile"
		};
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x000A5714 File Offset: 0x000A3914
	internal override void Apply(EffectProperties properties)
	{
		IntHolder intHolder = this.Count;
		FloatHolder floatHolder = this.Angle;
		if (properties.SourceControl != null && this.ProjectileSpawn != null)
		{
			ProjectileNode projectileNode = this.ProjectileSpawn as ProjectileNode;
			if (projectileNode != null && projectileNode.CanOverride)
			{
				properties.SourceControl.AllAugments(true, null).OverrideNodeProperties(properties, this, new object[]
				{
					intHolder,
					floatHolder
				});
			}
		}
		for (int i = 0; i < intHolder; i++)
		{
			EffectProperties effectProperties = (intHolder > 0) ? properties.Copy(false) : properties;
			if (this.LocOverride != null)
			{
				effectProperties.StartLoc = (this.LocOverride as PoseNode).GetPose(effectProperties);
			}
			else if (!this.UseInputLocation)
			{
				effectProperties.StartLoc = effectProperties.OutLoc.Copy();
			}
			EffectProperties effectProperties2 = effectProperties;
			EntityControl sourceControl = effectProperties.SourceControl;
			bool flag = sourceControl != null && sourceControl.CanTriggerSnippets(EventTrigger.ProjectileFired, true, 1f);
			if (flag)
			{
				effectProperties2 = effectProperties.Copy(false);
			}
			GameObject gameObject = this.FireProjectile(effectProperties, this.ProjectileSpawn as ProjectileNode, floatHolder);
			if (flag)
			{
				effectProperties2.SourceLocation = ((gameObject != null) ? gameObject.transform : null);
				effectProperties2.SourceControl.TriggerSnippets(EventTrigger.ProjectileFired, effectProperties2, 1f);
			}
		}
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x000A5870 File Offset: 0x000A3A70
	private GameObject FireProjectile(EffectProperties inputProps, ProjectileNode projectileProps, float angle)
	{
		ValueTuple<Vector3, Vector3> originVectors = inputProps.GetOriginVectors();
		Vector3 item = originVectors.Item1;
		Vector3 vector = originVectors.Item2;
		if (this.AimOverride != null)
		{
			vector = ((this.AimOverride as LocationNode).GetLocation(inputProps).GetPosition(inputProps) - item).normalized;
		}
		if (angle > 0f)
		{
			float d = Mathf.Tan(angle / 2f * 0.017453292f);
			Vector3 b = inputProps.RandomInsideUnitCircle(0f) * d;
			vector = MathHelper.GetRelativePoint(Vector3.forward + b, inputProps.GetOrigin(), vector);
			inputProps.StartLoc.OverrideLookAt(Location.AtWorldPoint(inputProps.GetOrigin() + vector));
		}
		GameObject gameObject = ActionPool.SpawnObject(this.SpawnRef, item, this.SpawnRef.transform.rotation);
		gameObject.transform.forward = ((vector.magnitude > 0f) ? vector : Vector3.up);
		Projectile component = gameObject.GetComponent<Projectile>();
		if (component == null)
		{
			return null;
		}
		float minEnvDist = 0f;
		if (this.HelpPlayerFire && inputProps.SourceControl != null)
		{
			PlayerControl playerControl = inputProps.SourceControl as PlayerControl;
			if (playerControl != null)
			{
				minEnvDist = Vector3.Distance(item, playerControl.CameraAimPoint) * 0.8f;
			}
		}
		inputProps.LocalIndex = inputProps.RandomInt(1, 99999999);
		component.Setup(inputProps);
		component.sourceGUID = this.guid;
		component.Activate(projectileProps, minEnvDist);
		component.projectileID = inputProps.RandomInt(0, 5000000);
		return gameObject;
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x000A5A04 File Offset: 0x000A3C04
	public override void TryCancel(EffectProperties props)
	{
		SpawnProjectileEffectNode.toExpire.Clear();
		List<ActionEffect> list = GameplayManager.WorldEffects;
		if (props.SourceControl != null)
		{
			list = props.SourceControl.OwnedEffects;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			ActionEffect actionEffect = list[i];
			if (actionEffect.sourceGUID == this.guid && (this.Singleton || actionEffect.sourceInstanceID == props.RandSeed))
			{
				Projectile projectile = actionEffect as Projectile;
				if (projectile != null)
				{
					SpawnProjectileEffectNode.toExpire.Add(projectile);
				}
			}
		}
		foreach (Projectile projectile2 in SpawnProjectileEffectNode.toExpire)
		{
			EffectProperties effectProperties = props.Copy(false);
			effectProperties.Lifetime = projectile2.timeAlive;
			effectProperties.SetExtra(EProp.Distance, projectile2.distanceTraveled);
			effectProperties.OutLoc = global::Pose.WorldPoint(projectile2.transform.position, (projectile2.velocity.magnitude == 0f) ? projectile2.transform.forward : projectile2.velocity.normalized);
			projectile2.Expire(effectProperties, ActionEffect.EffectExpireReason.Cancel, false);
		}
		base.TryCancel(props);
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000A5B54 File Offset: 0x000A3D54
	public SpawnProjectileEffectNode()
	{
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000A5B63 File Offset: 0x000A3D63
	// Note: this type is marked as 'beforefieldinit'.
	static SpawnProjectileEffectNode()
	{
	}

	// Token: 0x04001B27 RID: 6951
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001B28 RID: 6952
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Aim At", PortLocation.Default)]
	public Node AimOverride;

	// Token: 0x04001B29 RID: 6953
	public GameObject SpawnRef;

	// Token: 0x04001B2A RID: 6954
	public int Count = 1;

	// Token: 0x04001B2B RID: 6955
	public float Angle;

	// Token: 0x04001B2C RID: 6956
	public bool HelpPlayerFire;

	// Token: 0x04001B2D RID: 6957
	public bool UseInputLocation;

	// Token: 0x04001B2E RID: 6958
	public bool Singleton;

	// Token: 0x04001B2F RID: 6959
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileNode), false, "Properties", PortLocation.Default)]
	public Node ProjectileSpawn;

	// Token: 0x04001B30 RID: 6960
	private static List<Projectile> toExpire = new List<Projectile>();
}
