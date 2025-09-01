using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class RemapNumberNode : NumberNode
{
	// Token: 0x06001D8E RID: 7566 RVA: 0x000B3A0D File Offset: 0x000B1C0D
	public override float Evaluate(EffectProperties props)
	{
		if (this.Value == null)
		{
			return this.OutputCurve.Evaluate(0f);
		}
		return this.OutputCurve.Evaluate((this.Value as NumberNode).Evaluate(props));
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x000B3A4C File Offset: 0x000B1C4C
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.OutputCurve.GetString());
		this.OutputCurve = animationCurve;
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x000B3A77 File Offset: 0x000B1C77
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remap",
			MinInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true,
			ShowInspectorView = true
		};
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x000B3AAC File Offset: 0x000B1CAC
	public RemapNumberNode()
	{
	}

	// Token: 0x04001E38 RID: 7736
	public AnimationCurve OutputCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001E39 RID: 7737
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Input", PortLocation.Header)]
	public Node Value;
}
