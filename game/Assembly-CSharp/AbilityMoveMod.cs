using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class AbilityMoveMod : EffectNode
{
	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06001952 RID: 6482 RVA: 0x0009DD88 File Offset: 0x0009BF88
	public float SpeedMult
	{
		get
		{
			if (this.DynamicValue != null)
			{
				return this.DynamicCalculated;
			}
			return this.SpeedMultiplier;
		}
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x0009DDA8 File Offset: 0x0009BFA8
	internal override void Apply(EffectProperties properties)
	{
		AbilityMoveMod abilityMoveMod = this.Clone(null, false) as AbilityMoveMod;
		abilityMoveMod.appliedAt = Time.realtimeSinceStartup;
		if (this.DynamicValue != null)
		{
			this.DynamicCalculated = (this.DynamicValue as NumberNode).Evaluate(properties);
		}
		EntityControl entityControl = properties.SourceControl;
		if (this.ApplyOn == ApplyOn.Affected)
		{
			entityControl = properties.AffectedControl;
		}
		if (entityControl != null)
		{
			entityControl.movement.AddModifier(abilityMoveMod);
		}
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x0009DE19 File Offset: 0x0009C019
	internal override void OnCancel(EffectProperties props)
	{
		EntityControl sourceControl = props.SourceControl;
		if (sourceControl == null)
		{
			return;
		}
		sourceControl.movement.RemoveMod(this);
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x0009DE31 File Offset: 0x0009C031
	private bool ValidateDuration(float val)
	{
		return this.CalledFrom == null || !(this.CalledFrom is StatusRootNode) || val > 0f;
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x0009DE5A File Offset: 0x0009C05A
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Movement Modifier",
			MinInspectorSize = new Vector2(130f, 0f)
		};
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x0009DE84 File Offset: 0x0009C084
	public AbilityMoveMod()
	{
	}

	// Token: 0x04001977 RID: 6519
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "", PortLocation.Header)]
	public Node DynamicValue;

	// Token: 0x04001978 RID: 6520
	public float SpeedMultiplier = 1f;

	// Token: 0x04001979 RID: 6521
	public AnimationCurve SpeedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400197A RID: 6522
	public float Duration = 10f;

	// Token: 0x0400197B RID: 6523
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x0400197C RID: 6524
	[NonSerialized]
	public float appliedAt;

	// Token: 0x0400197D RID: 6525
	[NonSerialized]
	public float DynamicCalculated;
}
