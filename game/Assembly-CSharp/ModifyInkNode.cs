using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class ModifyInkNode : EffectNode
{
	// Token: 0x06001A49 RID: 6729 RVA: 0x000A3644 File Offset: 0x000A1844
	internal override void Apply(EffectProperties properties)
	{
		if (properties.AffectedControl == null)
		{
			return;
		}
		PlayerControl playerControl = properties.AffectedControl as PlayerControl;
		if (playerControl == null)
		{
			return;
		}
		if (!this.ShouldApply(properties, playerControl))
		{
			return;
		}
		InkManager.instance.AddInk(this.Amount);
		base.Completed();
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x000A3691 File Offset: 0x000A1891
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000A36B5 File Offset: 0x000A18B5
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Add Ink",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000A36DC File Offset: 0x000A18DC
	public ModifyInkNode()
	{
	}

	// Token: 0x04001AB5 RID: 6837
	public int Amount;
}
