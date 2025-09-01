using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class ModTagOrNode : ModTagNode
{
	// Token: 0x06001C7C RID: 7292 RVA: 0x000ADB2C File Offset: 0x000ABD2C
	public override bool Validate(EntityControl control)
	{
		using (List<Node>.Enumerator enumerator = this.Tests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((ModTagNode)enumerator.Current).Validate(control))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x000ADB8C File Offset: 0x000ABD8C
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "OR";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x000ADBBB File Offset: 0x000ABDBB
	public ModTagOrNode()
	{
	}

	// Token: 0x04001D4A RID: 7498
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(ModTagNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
