using System;

// Token: 0x020002C8 RID: 712
public class EntityEventNode : EffectNode
{
	// Token: 0x06001A35 RID: 6709 RVA: 0x000A3082 File Offset: 0x000A1282
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Entity Event"
		};
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000A3094 File Offset: 0x000A1294
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.Target);
		if (applicationEntity == null)
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		switch (this.EventType)
		{
		case EntityEventNode.EntityEvent.Kill:
			applicationEntity.health.DebugKill();
			return;
		case EntityEventNode.EntityEvent.Revive:
			applicationEntity.health.Revive(1f);
			return;
		case EntityEventNode.EntityEvent.SetInteractions:
			applicationEntity.net.ToggleTargetable(this.Targetable, this.Affectable);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x000A3111 File Offset: 0x000A1311
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x000A3135 File Offset: 0x000A1335
	public EntityEventNode()
	{
	}

	// Token: 0x04001AA3 RID: 6819
	public EntityEventNode.EntityEvent EventType;

	// Token: 0x04001AA4 RID: 6820
	public ApplyOn Target = ApplyOn.Affected;

	// Token: 0x04001AA5 RID: 6821
	public bool Targetable = true;

	// Token: 0x04001AA6 RID: 6822
	public bool Affectable = true;

	// Token: 0x0200064B RID: 1611
	public enum EntityEvent
	{
		// Token: 0x04002ADC RID: 10972
		Kill,
		// Token: 0x04002ADD RID: 10973
		Revive,
		// Token: 0x04002ADE RID: 10974
		SetInteractions
	}
}
