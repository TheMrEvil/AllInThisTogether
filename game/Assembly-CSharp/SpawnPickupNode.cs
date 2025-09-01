using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class SpawnPickupNode : EffectNode
{
	// Token: 0x06001A9A RID: 6810 RVA: 0x000A5648 File Offset: 0x000A3848
	internal override void Apply(EffectProperties properties)
	{
		int num = this.Count;
		if (this.CountOverride != null)
		{
			NumberNode numberNode = this.CountOverride as NumberNode;
			if (numberNode != null)
			{
				num = (int)numberNode.Evaluate(properties);
			}
		}
		if (num == 0)
		{
			return;
		}
		Vector3 pos = properties.GetOrigin();
		if (this.LocOverride != null)
		{
			LocationNode locationNode = this.LocOverride as LocationNode;
			if (locationNode != null)
			{
				pos = locationNode.GetLocation(properties).GetPosition(properties);
			}
		}
		GameDB.PickupOption pickupOption = GameDB.GetPickupOption(this.Pickup);
		if (pickupOption == null)
		{
			return;
		}
		for (int i = 0; i < num; i++)
		{
			PickupManager.instance.SpawnPickup(pickupOption, pos);
		}
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x000A56E7 File Offset: 0x000A38E7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn Pickups"
		};
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x000A56F9 File Offset: 0x000A38F9
	public SpawnPickupNode()
	{
	}

	// Token: 0x04001B23 RID: 6947
	public GameObject Pickup;

	// Token: 0x04001B24 RID: 6948
	public int Count;

	// Token: 0x04001B25 RID: 6949
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Loc", PortLocation.Header)]
	public Node LocOverride;

	// Token: 0x04001B26 RID: 6950
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Dynamic Count", PortLocation.Default)]
	public Node CountOverride;
}
