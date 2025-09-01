using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x020002D4 RID: 724
public class PostProcessNode : EffectNode
{
	// Token: 0x06001A69 RID: 6761 RVA: 0x000A4402 File Offset: 0x000A2602
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Post Process",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000A4429 File Offset: 0x000A2629
	internal override void Apply(EffectProperties properties)
	{
		if (this.Profile == null)
		{
			return;
		}
		if (this.ShouldApply(properties, null))
		{
			PostFXManager.instance.CreateTempFX(this, properties);
		}
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000A4450 File Offset: 0x000A2650
	public override void TryCancel(EffectProperties props)
	{
		if (this.Lifetime <= 0f && this.canCancel && this.ShouldApply(props, null))
		{
			PostFXManager.instance.ReleaseTempFX(this);
		}
		base.OnCancel(props);
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000A4483 File Offset: 0x000A2683
	public float GetDesiredWeight(EffectProperties props)
	{
		if (this.WeightType == ValueSource.Flat)
		{
			return 1f;
		}
		return this.WeightCurve.Evaluate(props.GetExtra(this.EffectProp, 0f));
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x000A44AF File Offset: 0x000A26AF
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return !(PlayerControl.myInstance == null) && (!(props.SourceControl != PlayerControl.myInstance) || !(props.AffectedControl != PlayerControl.myInstance));
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x000A44E8 File Offset: 0x000A26E8
	public PostProcessNode()
	{
	}

	// Token: 0x04001ADE RID: 6878
	public PostProcessProfile Profile;

	// Token: 0x04001ADF RID: 6879
	public float Lifetime = 0.25f;

	// Token: 0x04001AE0 RID: 6880
	public ValueSource WeightType;

	// Token: 0x04001AE1 RID: 6881
	public EProp EffectProp;

	// Token: 0x04001AE2 RID: 6882
	public AnimationCurve WeightCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x04001AE3 RID: 6883
	public bool FastIn;

	// Token: 0x04001AE4 RID: 6884
	public bool canCancel;
}
