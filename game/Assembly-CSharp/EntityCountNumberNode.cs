using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class EntityCountNumberNode : NumberNode
{
	// Token: 0x06001D6D RID: 7533 RVA: 0x000B2E80 File Offset: 0x000B1080
	public override float Evaluate(EffectProperties props)
	{
		List<EffectProperties> list = new List<EffectProperties>();
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if ((!entityControl.IsDead || this.IncludeDead) && !(entityControl == null) && (!this.ExcludeSelf || !(entityControl == props.SourceControl)) && (!this.ExcludeAffected || !(entityControl == props.AffectedControl)))
			{
				EffectProperties effectProperties = props.Copy(false);
				effectProperties.SeekTarget = entityControl.gameObject;
				effectProperties.Affected = effectProperties.SeekTarget;
				list.Add(effectProperties);
			}
		}
		foreach (Node node in this.Filters)
		{
			((LogicFilterNode)node).Filter(ref list, props);
		}
		return (float)list.Count;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x000B2F8C File Offset: 0x000B118C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Entity Count",
			MinInspectorSize = new Vector2(210f, 0f),
			MaxInspectorSize = new Vector2(210f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x000B2FDA File Offset: 0x000B11DA
	public EntityCountNumberNode()
	{
	}

	// Token: 0x04001E19 RID: 7705
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicFilterNode), true, "Filter", PortLocation.Vertical)]
	public List<Node> Filters = new List<Node>();

	// Token: 0x04001E1A RID: 7706
	public bool ExcludeSelf;

	// Token: 0x04001E1B RID: 7707
	public bool ExcludeAffected;

	// Token: 0x04001E1C RID: 7708
	public bool IncludeDead;
}
