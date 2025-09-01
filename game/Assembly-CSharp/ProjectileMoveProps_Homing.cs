using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class ProjectileMoveProps_Homing : ProjectileMoveModuleNode
{
	// Token: 0x06001AE8 RID: 6888 RVA: 0x000A71BC File Offset: 0x000A53BC
	public override void UpdateVelocity(ref Vector3 vel, Projectile p, EffectProperties props, Projectile.ProjectileMoveMods mod)
	{
		Vector3 vector = Vector3.one.INVALID();
		if (!mod.didAssign && this.OverrideRandom)
		{
			mod.didAssign = true;
			mod.savedSeed = props.RandomInt(0, 99999999);
			mod.cachedSeek = (this.LocOverride as LocationNode).GetLocation(props).GetPosition(props);
		}
		if (this.RequireLiving)
		{
			EntityControl applicationEntity = props.GetApplicationEntity(this.LiveReq);
			if (applicationEntity == null || applicationEntity.IsDead)
			{
				return;
			}
		}
		if (this.OverrideRandom)
		{
			props.OverrideSeed(mod.savedSeed, 0);
		}
		if (this.NoUpdate)
		{
			vector = mod.cachedSeek;
		}
		else
		{
			vector = (this.LocOverride as LocationNode).GetLocation(props).GetPosition(props);
		}
		if (vector.IsValid())
		{
			Vector3 vector2 = (vector - p.transform.position).normalized * vel.magnitude;
			if (!this.OnlyInFront || Vector3.Dot(vector2.normalized, vel.normalized) > 0f)
			{
				float num = this.SeekStrength;
				if (this.Value != null)
				{
					NumberNode numberNode = this.Value as NumberNode;
					if (numberNode != null)
					{
						num = numberNode.Evaluate(props);
					}
				}
				num *= this.SeekOverTime.Evaluate(p.timeAlive);
				if (num < Mathf.Epsilon && num > -Mathf.Epsilon)
				{
					return;
				}
				if (num >= 0f)
				{
					vel = Vector3.RotateTowards(vel, vector2, num * 720f * 0.017453292f * GameplayManager.deltaTime, 0f);
					return;
				}
				vel = Vector3.RotateTowards(vel, -vector2, -num * 720f * 0.017453292f * GameplayManager.deltaTime, 0f);
			}
		}
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x000A739C File Offset: 0x000A559C
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.SeekOverTime.GetString());
		this.SeekOverTime = animationCurve;
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x000A73C7 File Offset: 0x000A55C7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Homing Module"
		};
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x000A73DC File Offset: 0x000A55DC
	public ProjectileMoveProps_Homing()
	{
	}

	// Token: 0x04001B83 RID: 7043
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Loc", PortLocation.Header)]
	public Node LocOverride;

	// Token: 0x04001B84 RID: 7044
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001B85 RID: 7045
	[Tooltip("Number of 360 turns that can occur per 0.5s of flight time")]
	public float SeekStrength = 0.5f;

	// Token: 0x04001B86 RID: 7046
	public AnimationCurve SeekOverTime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001B87 RID: 7047
	public bool OnlyInFront;

	// Token: 0x04001B88 RID: 7048
	public bool NoUpdate;

	// Token: 0x04001B89 RID: 7049
	public bool OverrideRandom = true;

	// Token: 0x04001B8A RID: 7050
	public bool RequireLiving;

	// Token: 0x04001B8B RID: 7051
	public ApplyOn LiveReq = ApplyOn.Affected;
}
