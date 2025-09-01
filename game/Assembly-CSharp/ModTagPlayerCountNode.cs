using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class ModTagPlayerCountNode : ModTagNode
{
	// Token: 0x06001C7F RID: 7295 RVA: 0x000ADBD0 File Offset: 0x000ABDD0
	public override bool Validate(EntityControl control)
	{
		int num = 1;
		if (PhotonNetwork.InRoom)
		{
			num = PhotonNetwork.CurrentRoom.PlayerCount;
		}
		return num < this.MinLevel || (num > this.MaxLevel && this.MaxLevel >= this.MinLevel);
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x000ADC18 File Offset: 0x000ABE18
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Player Count";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x000ADC40 File Offset: 0x000ABE40
	public ModTagPlayerCountNode()
	{
	}

	// Token: 0x04001D4B RID: 7499
	public int MinLevel = 1;

	// Token: 0x04001D4C RID: 7500
	public int MaxLevel = 4;

	// Token: 0x0200066D RID: 1645
	public enum MatchTest
	{
		// Token: 0x04002B86 RID: 11142
		Augment
	}
}
