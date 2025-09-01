using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class CacheNumberNode : EffectNode
{
	// Token: 0x06001A0B RID: 6667 RVA: 0x000A260C File Offset: 0x000A080C
	internal override void Apply(EffectProperties properties)
	{
		if (!(this.Num == null))
		{
			NumberNode num = this.Num;
			if (num != null)
			{
				float v = num.Evaluate(properties);
				properties.SaveFloat(this.ID, v);
				return;
			}
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000A2647 File Offset: 0x000A0847
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Cache Num",
			MinInspectorSize = new Vector2(160f, 0f),
			MaxInspectorSize = new Vector2(160f, 0f)
		};
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x000A2683 File Offset: 0x000A0883
	public CacheNumberNode()
	{
	}

	// Token: 0x04001A89 RID: 6793
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "", PortLocation.Header)]
	public NumberNode Num;

	// Token: 0x04001A8A RID: 6794
	public string ID;
}
