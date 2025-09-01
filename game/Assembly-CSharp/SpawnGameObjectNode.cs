using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class SpawnGameObjectNode : EffectNode
{
	// Token: 0x06001A97 RID: 6807 RVA: 0x000A556C File Offset: 0x000A376C
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (!this.ShouldApply(properties, applicationEntity) && !this.AlwaysSpawn)
		{
			return;
		}
		Vector3 position = Vector3.zero;
		if (this.Loc != null)
		{
			position = (this.Loc as LocationNode).GetLocation(properties).GetPosition(properties);
		}
		else if (properties.OutLoc != null)
		{
			position = properties.GetOutputPoint();
		}
		else
		{
			position = properties.GetOrigin();
		}
		ActionPrefab component = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, this.prefab.transform.rotation).GetComponent<ActionPrefab>();
		if (component != null)
		{
			component.Setup(properties);
		}
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x000A5611 File Offset: 0x000A3811
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Spawn Prefab",
			MinInspectorSize = new Vector2(160f, 0f)
		};
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x000A5638 File Offset: 0x000A3838
	public SpawnGameObjectNode()
	{
	}

	// Token: 0x04001B1F RID: 6943
	public GameObject prefab;

	// Token: 0x04001B20 RID: 6944
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Loc", PortLocation.Header)]
	public Node Loc;

	// Token: 0x04001B21 RID: 6945
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001B22 RID: 6946
	public bool AlwaysSpawn;
}
