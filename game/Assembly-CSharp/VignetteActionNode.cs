using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class VignetteActionNode : EffectNode
{
	// Token: 0x06001AC6 RID: 6854 RVA: 0x000A6824 File Offset: 0x000A4A24
	internal override void Apply(EffectProperties properties)
	{
		switch (this.Action)
		{
		case VignetteActionNode.ActionType.Action:
		{
			if (string.IsNullOrEmpty(this.ID))
			{
				return;
			}
			if (!this.LocalOnly)
			{
				StateManager.VignetteAction(this.ID);
				return;
			}
			EntityControl sourceControl = properties.SourceControl;
			int sourceID = (sourceControl != null) ? sourceControl.ViewID : -1;
			if (VignetteControl.instance != null)
			{
				VignetteControl.instance.OnActionTaken(this.ID, sourceID, properties.RandSeed);
				return;
			}
			break;
		}
		case VignetteActionNode.ActionType.OpenExit:
		{
			StateManager instance = StateManager.instance;
			if (instance == null)
			{
				return;
			}
			instance.VignetteOpenExit();
			return;
		}
		case VignetteActionNode.ActionType.UpdateText:
		{
			string text = this.InstructionText;
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
			VignetteInfoDisplay.UpdateDetail(text);
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x000A6931 File Offset: 0x000A4B31
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Vignette Act",
			MinInspectorSize = new Vector2(160f, 0f)
		};
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x000A6958 File Offset: 0x000A4B58
	public VignetteActionNode()
	{
	}

	// Token: 0x04001B59 RID: 7001
	public VignetteActionNode.ActionType Action;

	// Token: 0x04001B5A RID: 7002
	public string ID;

	// Token: 0x04001B5B RID: 7003
	public bool LocalOnly = true;

	// Token: 0x04001B5C RID: 7004
	public string InstructionText;

	// Token: 0x04001B5D RID: 7005
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Inputs", PortLocation.Default)]
	public List<Node> Numbers = new List<Node>();

	// Token: 0x02000651 RID: 1617
	public enum ActionType
	{
		// Token: 0x04002B02 RID: 11010
		Action,
		// Token: 0x04002B03 RID: 11011
		OpenExit,
		// Token: 0x04002B04 RID: 11012
		UpdateText
	}
}
