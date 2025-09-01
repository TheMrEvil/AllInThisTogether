using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class TeleportEffectNode : EffectNode
{
	// Token: 0x06001AB7 RID: 6839 RVA: 0x000A628C File Offset: 0x000A448C
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.Target);
		if (applicationEntity == null)
		{
			return;
		}
		if (!applicationEntity.movement.IsMoverEnabled())
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.StartLoc = effectProperties.OutLoc.Copy();
		if (this.Loc != null)
		{
			effectProperties.StartLoc = (effectProperties.OutLoc = (this.Loc as PoseNode).GetPose(effectProperties));
		}
		ValueTuple<Vector3, Vector3> originVectors = effectProperties.GetOriginVectors();
		Vector3 item = originVectors.Item1;
		Vector3 item2 = originVectors.Item2;
		applicationEntity.movement.SetPositionImmediate(item, item2, this.ResetVelocity);
		AIControl aicontrol = applicationEntity as AIControl;
		if (aicontrol != null && aicontrol.Level != EnemyLevel.Boss)
		{
			AIManager.instance.DoTPFX(aicontrol, item, applicationEntity.movement.GetPosition());
		}
		base.Completed();
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x000A636D File Offset: 0x000A456D
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Teleport",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x000A6394 File Offset: 0x000A4594
	public TeleportEffectNode()
	{
	}

	// Token: 0x04001B47 RID: 6983
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "To Pose", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001B48 RID: 6984
	public ApplyOn Target = ApplyOn.Affected;

	// Token: 0x04001B49 RID: 6985
	public bool ResetVelocity;
}
