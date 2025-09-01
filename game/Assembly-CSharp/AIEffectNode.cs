using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class AIEffectNode : AINode
{
	// Token: 0x06001AFD RID: 6909 RVA: 0x000A79B8 File Offset: 0x000A5BB8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Effect"
		};
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x000A79CC File Offset: 0x000A5BCC
	internal override AILogicState Run(AIControl entity)
	{
		EffectProperties properties = new EffectProperties(entity);
		if (this.Effect != null)
		{
			EffectNode effectNode = this.Effect as EffectNode;
			if (effectNode != null)
			{
				effectNode.Invoke(properties);
			}
		}
		return AILogicState.Success;
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x000A7A05 File Offset: 0x000A5C05
	public AIEffectNode()
	{
	}

	// Token: 0x04001BA1 RID: 7073
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), false, "", PortLocation.Header)]
	public Node Effect;

	// Token: 0x04001BA2 RID: 7074
	public string Info = "- FOR DEBUG ONLY -";
}
