using System;

// Token: 0x020002D8 RID: 728
public class RemoveTagNode : EffectNode
{
	// Token: 0x06001A7A RID: 6778 RVA: 0x000A47B0 File Offset: 0x000A29B0
	internal override void Apply(EffectProperties properties)
	{
		if (this.ApplyTo == AITestNode.TestTarget.Self)
		{
			this.RemoveTag(properties.SourceControl);
		}
		else if (this.ApplyTo == AITestNode.TestTarget.Affected)
		{
			this.RemoveTag(properties.AffectedControl);
		}
		else if (this.ApplyTo == AITestNode.TestTarget.CurrentTarget)
		{
			this.RemoveTag(properties.SeekTargetControl);
		}
		base.Completed();
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x000A4805 File Offset: 0x000A2A05
	private void RemoveTag(EntityControl control)
	{
		if (control == null || !(control is AIControl))
		{
			return;
		}
		(control as AIControl).RemoveTag(this.Tag);
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x000A482A File Offset: 0x000A2A2A
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remove Tag"
		};
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x000A483C File Offset: 0x000A2A3C
	public RemoveTagNode()
	{
	}

	// Token: 0x04001AEE RID: 6894
	public string Tag;

	// Token: 0x04001AEF RID: 6895
	public AITestNode.TestTarget ApplyTo;
}
