using System;

// Token: 0x020002BC RID: 700
public class ApplyTagNode : EffectNode
{
	// Token: 0x06001A00 RID: 6656 RVA: 0x000A214C File Offset: 0x000A034C
	internal override void Apply(EffectProperties properties)
	{
		if (this.ApplyTo == AITestNode.TestTarget.Self)
		{
			this.AddTag(properties.SourceControl);
		}
		else if (this.ApplyTo == AITestNode.TestTarget.Affected)
		{
			this.AddTag(properties.AffectedControl);
		}
		else if (this.ApplyTo == AITestNode.TestTarget.CurrentTarget)
		{
			this.AddTag(properties.SeekTargetControl);
		}
		base.Completed();
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x000A21A1 File Offset: 0x000A03A1
	private void AddTag(EntityControl control)
	{
		if (control == null || !(control is AIControl))
		{
			return;
		}
		(control as AIControl).AddTag(this.Tag);
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x000A21C6 File Offset: 0x000A03C6
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Tag"
		};
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x000A21D8 File Offset: 0x000A03D8
	public ApplyTagNode()
	{
	}

	// Token: 0x04001A7A RID: 6778
	public string Tag;

	// Token: 0x04001A7B RID: 6779
	public AITestNode.TestTarget ApplyTo;
}
