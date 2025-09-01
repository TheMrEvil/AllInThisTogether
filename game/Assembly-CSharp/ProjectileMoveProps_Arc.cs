using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class ProjectileMoveProps_Arc : ProjectileMoveModuleNode
{
	// Token: 0x06001ADC RID: 6876 RVA: 0x000A6D50 File Offset: 0x000A4F50
	public override void UpdateVelocity(ref Vector3 vel, Projectile p, EffectProperties props, Projectile.ProjectileMoveMods mod)
	{
		if (!mod.started)
		{
			this.SetupDist(p.transform.position, props, mod);
		}
		if (this.UpdateTarget && this.TargetLoc != null)
		{
			mod.targPt = (this.TargetLoc as LocationNode).GetLocation(props).GetPosition(props);
		}
		if (mod.progress < 1f)
		{
			Debug.DrawLine(p.transform.position, mod.targPt);
			if (this.ConstantFlightTime)
			{
				float num = GameplayManager.deltaTime * (1f / Mathf.Max(0.1f, mod.flightTime));
				mod.progress = Mathf.Min(mod.progress + num, 1f);
			}
			else
			{
				float num2 = 1f / mod.dist;
				mod.progress = Mathf.Min(mod.progress + vel.magnitude * GameplayManager.deltaTime * num2, 1f);
			}
			Vector3 a = ProjectileMoveProps_Arc.NextPoint(mod.startPt, mod.targPt, p.transform.position, mod.arcHeight, mod.progress);
			vel = (a - p.transform.position) / Time.deltaTime;
			if (mod.progress == 1f && this.ExpireOnEnd)
			{
				props.OutLoc = global::Pose.WorldPoint(mod.targPt, Vector3.up);
				Debug.DrawLine(props.GetOutputPoint(), props.GetOutputPoint() + Vector3.up * 5f, Color.blue, 2f);
				p.ExpireNext(ActionEffect.EffectExpireReason.Distance, props);
			}
		}
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x000A6F00 File Offset: 0x000A5100
	private void SetupDist(Vector3 startPoint, EffectProperties props, Projectile.ProjectileMoveMods mod)
	{
		mod.started = true;
		mod.startPt = startPoint;
		mod.targPt = ((this.TargetLoc == null) ? props.GetOutputPoint() : (this.TargetLoc as LocationNode).GetLocation(props).GetPosition(props));
		mod.dist = Vector3.Distance(startPoint, mod.targPt);
		mod.dist = Mathf.Max(mod.dist, 0.1f);
		mod.arcHeight = this.ArcHeight * this.HeightDistCurve.Evaluate(mod.dist);
		mod.flightTime = ((this.DynFlightTime == null) ? this.FlightTime : (this.DynFlightTime as NumberNode).Evaluate(props));
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x000A6FC4 File Offset: 0x000A51C4
	private static Vector3 NextPoint(Vector3 start, Vector3 target, Vector3 curPos, float arcHeight, float progress)
	{
		float num = 1f - 4f * (progress - 0.5f) * (progress - 0.5f);
		Vector3 result = Vector3.Lerp(start, target, progress);
		result.y += num * arcHeight;
		return result;
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x000A7008 File Offset: 0x000A5208
	private float EaseInOutCubic(float x)
	{
		if (x >= 0.5f)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
		}
		return 4f * x * x * x;
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x000A7044 File Offset: 0x000A5244
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.HeightDistCurve.GetString());
		this.HeightDistCurve = animationCurve;
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x000A706F File Offset: 0x000A526F
	public bool CanUseNumberNode()
	{
		return this.ConstantFlightTime;
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x000A7077 File Offset: 0x000A5277
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Arc Movement"
		};
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x000A708C File Offset: 0x000A528C
	public ProjectileMoveProps_Arc()
	{
	}

	// Token: 0x04001B7A RID: 7034
	public float ArcHeight = 1f;

	// Token: 0x04001B7B RID: 7035
	[Tooltip("Height Distance Multiplier")]
	public AnimationCurve HeightDistCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(50f, 1f)
	});

	// Token: 0x04001B7C RID: 7036
	public bool ExpireOnEnd;

	// Token: 0x04001B7D RID: 7037
	public bool UpdateTarget;

	// Token: 0x04001B7E RID: 7038
	public bool ConstantFlightTime;

	// Token: 0x04001B7F RID: 7039
	public float FlightTime = 3f;

	// Token: 0x04001B80 RID: 7040
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Target Point", PortLocation.Header)]
	public Node TargetLoc;

	// Token: 0x04001B81 RID: 7041
	[HideInInspector]
	[SerializeField]
	[ShowPort("CanUseNumberNode")]
	[OutputPort(typeof(NumberNode), false, "Dynamic Flight Time", PortLocation.Default)]
	public Node DynFlightTime;
}
