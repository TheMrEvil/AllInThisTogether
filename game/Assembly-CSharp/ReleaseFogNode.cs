using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class ReleaseFogNode : EffectNode
{
	// Token: 0x06001A72 RID: 6770 RVA: 0x000A4598 File Offset: 0x000A2798
	internal override void Apply(EffectProperties properties)
	{
		if (this.FogMat == null)
		{
			return;
		}
		if (properties.AffectedControl != PlayerControl.myInstance)
		{
			return;
		}
		Scene_Settings.instance.ReleaseFog(this.FogMat);
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000A45CC File Offset: 0x000A27CC
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Release Fog",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000A45F3 File Offset: 0x000A27F3
	public ReleaseFogNode()
	{
	}

	// Token: 0x04001AE7 RID: 6887
	public Material FogMat;
}
