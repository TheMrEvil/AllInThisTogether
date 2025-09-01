using System;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class RemoveMaterialNode : EffectNode
{
	// Token: 0x06001D12 RID: 7442 RVA: 0x000B09FC File Offset: 0x000AEBFC
	internal override void Apply(EffectProperties properties)
	{
		EntityControl entityControl = properties.SourceControl;
		if (this.ApplyOn == ApplyOn.Affected)
		{
			entityControl = properties.AffectedControl;
		}
		if (entityControl == null)
		{
			return;
		}
		entityControl.display.RemoveMeshMaterial(this.Material, this.FadeTime);
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x000B0A41 File Offset: 0x000AEC41
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remove Material",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x000B0A68 File Offset: 0x000AEC68
	public RemoveMaterialNode()
	{
	}

	// Token: 0x04001DB9 RID: 7609
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x04001DBA RID: 7610
	public Material Material;

	// Token: 0x04001DBB RID: 7611
	public float FadeTime;
}
