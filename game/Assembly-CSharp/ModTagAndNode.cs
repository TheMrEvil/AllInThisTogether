using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class ModTagAndNode : ModTagNode
{
	// Token: 0x06001C6D RID: 7277 RVA: 0x000AD8E8 File Offset: 0x000ABAE8
	public override bool Validate(EntityControl control)
	{
		if (this.Tests.Count == 0)
		{
			return true;
		}
		bool flag = true;
		foreach (Node node in this.Tests)
		{
			if (!(node is ModTagNode))
			{
				Type type = node.GetType();
				Debug.LogError(((type != null) ? type.ToString() : null) + " is not a ModTagNode - " + (this.RootNode as AugmentRootNode).Name);
			}
			else
			{
				bool flag2 = (node as ModTagNode).Validate(control);
				flag = (flag && flag2);
			}
		}
		return flag;
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x000AD994 File Offset: 0x000ABB94
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "AND";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x000AD9C3 File Offset: 0x000ABBC3
	public ModTagAndNode()
	{
	}

	// Token: 0x04001D43 RID: 7491
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(ModTagNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
