using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class ReviveEntityNode : EffectNode
{
	// Token: 0x06001A7E RID: 6782 RVA: 0x000A4844 File Offset: 0x000A2A44
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.Target);
		if (applicationEntity == null || !applicationEntity.IsDead)
		{
			return;
		}
		EffectProperties props = properties.Copy(false);
		if (!this.ShouldApply(props, applicationEntity))
		{
			return;
		}
		applicationEntity.health.ReviveFromEffect(this.HealthAdd);
		if (this.LocOverride != null)
		{
			global::Pose pose = (this.LocOverride as PoseNode).GetPose(properties);
			applicationEntity.movement.SetPositionImmediate(pose.GetPosition(props), pose.GetLookDirection(props), true);
		}
		base.Completed();
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x000A48D4 File Offset: 0x000A2AD4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Revive Entity",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x000A48FB File Offset: 0x000A2AFB
	public ReviveEntityNode()
	{
	}

	// Token: 0x04001AF0 RID: 6896
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001AF1 RID: 6897
	public ApplyOn Target = ApplyOn.Affected;

	// Token: 0x04001AF2 RID: 6898
	[Range(0.01f, 1f)]
	public float HealthAdd = 0.75f;
}
