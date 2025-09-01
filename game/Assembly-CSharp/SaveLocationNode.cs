using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class SaveLocationNode : EffectNode
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x000A4C80 File Offset: 0x000A2E80
	internal override void Apply(EffectProperties properties)
	{
		if (!(this.LocOverride == null))
		{
			LocationNode locationNode = this.LocOverride as LocationNode;
			if (locationNode != null)
			{
				Vector3 position = locationNode.GetLocation(properties).GetPosition(properties);
				properties.SaveLocation(this.ID, position);
				return;
			}
		}
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x000A4CC6 File Offset: 0x000A2EC6
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Save Point",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000A4CED File Offset: 0x000A2EED
	public SaveLocationNode()
	{
	}

	// Token: 0x04001B00 RID: 6912
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Point", PortLocation.Header)]
	public Node LocOverride;

	// Token: 0x04001B01 RID: 6913
	public string ID;
}
