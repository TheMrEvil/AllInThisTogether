using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class KillEntityNode : EffectNode
{
	// Token: 0x06001A41 RID: 6721 RVA: 0x000A3364 File Offset: 0x000A1564
	internal override void Apply(EffectProperties properties)
	{
		base.Completed();
		EntityControl applicationEntity = properties.GetApplicationEntity(this.Target);
		if (applicationEntity == null)
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		DamageInfo dmg = new DamageInfo(0f, DNumType.Default, applicationEntity.ViewID, 0f, properties);
		applicationEntity.net.Kill(dmg);
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x000A33BD File Offset: 0x000A15BD
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return (!(applyTo != null) || !applyTo.health.Immortal) && ((props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance);
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x000A33F9 File Offset: 0x000A15F9
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Kill Entity",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x000A3420 File Offset: 0x000A1620
	public KillEntityNode()
	{
	}

	// Token: 0x04001AAC RID: 6828
	public ApplyOn Target = ApplyOn.Affected;
}
