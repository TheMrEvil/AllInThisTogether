using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class ModOverrideNode : Node
{
	// Token: 0x06001BFC RID: 7164 RVA: 0x000ABC14 File Offset: 0x000A9E14
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Override",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x000ABC3B File Offset: 0x000A9E3B
	internal virtual bool CanScope()
	{
		return true;
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000ABC3E File Offset: 0x000A9E3E
	public virtual void OverrideNodeProperties(EffectProperties props, Node node, object[] values)
	{
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x000ABC40 File Offset: 0x000A9E40
	public virtual void OverrideNodeEffects(EffectProperties props, Node node, ref List<ModOverrideNode> overrides)
	{
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x000ABC42 File Offset: 0x000A9E42
	public virtual bool ShouldOverride(EffectProperties props, Node node)
	{
		return false;
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x000ABC45 File Offset: 0x000A9E45
	public bool ScopeMatches(PlayerAbilityType abilityType)
	{
		return (abilityType != PlayerAbilityType.None || this.OverrideScope == PlayerAbilityType.Any) && (this.OverrideScope == PlayerAbilityType.Any || abilityType == PlayerAbilityType.Any || (this.OverrideScope != PlayerAbilityType.None && this.OverrideScope == abilityType));
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x000ABC79 File Offset: 0x000A9E79
	public ModOverrideNode()
	{
	}

	// Token: 0x04001C64 RID: 7268
	public PlayerAbilityType OverrideScope;

	// Token: 0x04001C65 RID: 7269
	[Range(1f, 10f)]
	public int MaxDepth = 4;
}
