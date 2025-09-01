using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class AIPlayerCountCheck : AITestNode
{
	// Token: 0x06001BB6 RID: 7094 RVA: 0x000AA638 File Offset: 0x000A8838
	public override bool Evaluate(EntityControl entity)
	{
		int num = 1;
		if (PhotonNetwork.InRoom)
		{
			num = PhotonNetwork.CurrentRoom.PlayerCount;
		}
		return AICheckNode.RunNumericTest((float)num, this.Value, this.Test);
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000AA66C File Offset: 0x000A886C
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Player Count";
		return inspectorProps;
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000AA67F File Offset: 0x000A887F
	public AIPlayerCountCheck()
	{
	}

	// Token: 0x04001C10 RID: 7184
	public NumericTest Test;

	// Token: 0x04001C11 RID: 7185
	[Range(1f, 4f)]
	public float Value;
}
