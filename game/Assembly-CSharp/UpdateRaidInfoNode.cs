using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class UpdateRaidInfoNode : EffectNode
{
	// Token: 0x06001AC0 RID: 6848 RVA: 0x000A6650 File Offset: 0x000A4850
	internal override void Apply(EffectProperties properties)
	{
		string text = this.UpdateText;
		for (int i = 0; i < this.Numbers.Count; i++)
		{
			if (!(this.Numbers[i] == null))
			{
				NumberNode numberNode = this.Numbers[i] as NumberNode;
				if (numberNode != null)
				{
					text = text.Replace(string.Format("$Value_{0}$", i + 1), ((int)numberNode.Evaluate(properties)).ToString());
				}
			}
		}
		RaidHUDInfo.UpdateInfo(text);
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x000A66D2 File Offset: 0x000A48D2
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Set Raid Info",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x000A66F9 File Offset: 0x000A48F9
	public UpdateRaidInfoNode()
	{
	}

	// Token: 0x04001B4F RID: 6991
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Inputs", PortLocation.Default)]
	public List<Node> Numbers = new List<Node>();

	// Token: 0x04001B50 RID: 6992
	[TextArea(2, 2)]
	public string UpdateText;
}
