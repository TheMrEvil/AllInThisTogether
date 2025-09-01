using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class ModTagTeamCheck : ModTagNode
{
	// Token: 0x06001C82 RID: 7298 RVA: 0x000ADC58 File Offset: 0x000ABE58
	public override bool Validate(EntityControl control)
	{
		if (this.Tags.Count == 0)
		{
			return true;
		}
		bool flag = true;
		foreach (Node node in this.Tags)
		{
			if (!(node is ModTagNode))
			{
				Type type = node.GetType();
				Debug.LogError(((type != null) ? type.ToString() : null) + " is not a ModTagNode - " + (this.RootNode as AugmentRootNode).Name);
			}
			else
			{
				bool flag2 = this.Needs == ModTagTeamCheck.TeamTestType.Everyone;
				foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
				{
					if (this.Needs == ModTagTeamCheck.TeamTestType.Anyone)
					{
						flag2 |= (node as ModTagNode).Validate(control);
					}
					else
					{
						flag2 &= (node as ModTagNode).Validate(control);
					}
				}
				flag = (flag && flag2);
			}
		}
		return flag;
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x000ADD68 File Offset: 0x000ABF68
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Team Has";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x000ADD90 File Offset: 0x000ABF90
	public ModTagTeamCheck()
	{
	}

	// Token: 0x04001D4D RID: 7501
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(ModTagNode), true, "", PortLocation.Vertical)]
	public List<Node> Tags = new List<Node>();

	// Token: 0x04001D4E RID: 7502
	public ModTagTeamCheck.TeamTestType Needs;

	// Token: 0x0200066E RID: 1646
	public enum TeamTestType
	{
		// Token: 0x04002B88 RID: 11144
		Anyone,
		// Token: 0x04002B89 RID: 11145
		Everyone
	}
}
