using System;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class ModTagNode : Node
{
	// Token: 0x06001C76 RID: 7286 RVA: 0x000ADAA1 File Offset: 0x000ABCA1
	public virtual bool Validate(EntityControl control)
	{
		return false;
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x000ADAA4 File Offset: 0x000ABCA4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Augment Tag Filter",
			ShowInputNode = false,
			SortX = true
		};
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x000ADAC4 File Offset: 0x000ABCC4
	public ModTagNode()
	{
	}

	// Token: 0x04001D48 RID: 7496
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(Node), false, "", PortLocation.Vertical)]
	public Node TestNode;
}
