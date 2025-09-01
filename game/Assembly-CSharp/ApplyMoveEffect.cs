using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class ApplyMoveEffect : EffectNode
{
	// Token: 0x060019F2 RID: 6642 RVA: 0x000A18B4 File Offset: 0x0009FAB4
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.Target);
		if (applicationEntity == null)
		{
			return;
		}
		EffectProperties props = properties.Copy(false);
		if (this.ShouldApply(props, applicationEntity))
		{
			applicationEntity.movement.ForceMovement(this, props);
		}
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x000A18F8 File Offset: 0x0009FAF8
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		if (applyTo == props.SourceControl)
		{
			return true;
		}
		if (applyTo != null)
		{
			AIControl aicontrol = applyTo as AIControl;
			if (aicontrol != null && aicontrol.Level == EnemyLevel.Boss)
			{
				return false;
			}
		}
		return !(applyTo is PlayerControl) || !(applyTo != PlayerControl.myInstance);
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x000A194C File Offset: 0x0009FB4C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Movement",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x000A1974 File Offset: 0x0009FB74
	public ApplyMoveEffect()
	{
	}

	// Token: 0x04001A5E RID: 6750
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location", PortLocation.Header)]
	public Node Loc;

	// Token: 0x04001A5F RID: 6751
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Land", PortLocation.Default)]
	public List<Node> OnLand = new List<Node>();

	// Token: 0x04001A60 RID: 6752
	public ApplyOn Target = ApplyOn.Affected;

	// Token: 0x04001A61 RID: 6753
	public bool IgnoreObstacles;

	// Token: 0x04001A62 RID: 6754
	public bool UseNavMesh = true;

	// Token: 0x04001A63 RID: 6755
	public float Speed = 5f;

	// Token: 0x04001A64 RID: 6756
	public AnimationCurve speedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001A65 RID: 6757
	public AnimationCurve HeightCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 0f)
	});
}
