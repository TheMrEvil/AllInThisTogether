using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class UpdateObjectiveTextNode : EffectNode
{
	// Token: 0x06001ABD RID: 6845 RVA: 0x000A658C File Offset: 0x000A478C
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
		GoalManager.instance.UpdateObjectiveText(text);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x000A6613 File Offset: 0x000A4813
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Set Objective Text",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x000A663A File Offset: 0x000A483A
	public UpdateObjectiveTextNode()
	{
	}

	// Token: 0x04001B4D RID: 6989
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Inputs", PortLocation.Default)]
	public List<Node> Numbers = new List<Node>();

	// Token: 0x04001B4E RID: 6990
	[TextArea(3, 4)]
	public string UpdateText;
}
