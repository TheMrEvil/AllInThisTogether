using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class ProjectileMoveProps_Gravity : ProjectileMoveModuleNode
{
	// Token: 0x06001AE4 RID: 6884 RVA: 0x000A70F2 File Offset: 0x000A52F2
	public override void UpdateVelocity(ref Vector3 vel, Projectile p, EffectProperties props, Projectile.ProjectileMoveMods mod)
	{
		vel += Physics.gravity * this.gravityCurve.Evaluate(p.timeAlive) * GameplayManager.deltaTime;
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x000A712C File Offset: 0x000A532C
	public override void OnCloned()
	{
		AnimationCurve curve = new AnimationCurve();
		curve.LoadFromString(this.gravityCurve.GetString());
		this.gravityCurve = curve;
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x000A7157 File Offset: 0x000A5357
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Gravity Module"
		};
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x000A716C File Offset: 0x000A536C
	public ProjectileMoveProps_Gravity()
	{
	}

	// Token: 0x04001B82 RID: 7042
	public AnimationCurve gravityCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 0f)
	});
}
