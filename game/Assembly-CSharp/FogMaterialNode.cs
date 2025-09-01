using System;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class FogMaterialNode : EffectNode
{
	// Token: 0x06001A39 RID: 6713 RVA: 0x000A3152 File Offset: 0x000A1352
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
		Scene_Settings.instance.OverrideFog(this.FogMat, this.Importance);
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000A318C File Offset: 0x000A138C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Fog Override",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000A31B3 File Offset: 0x000A13B3
	public FogMaterialNode()
	{
	}

	// Token: 0x04001AA7 RID: 6823
	public Material FogMat;

	// Token: 0x04001AA8 RID: 6824
	public int Importance;
}
