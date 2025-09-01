using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class ApplyMaterialNode : EffectNode
{
	// Token: 0x06001D0C RID: 7436 RVA: 0x000B08D4 File Offset: 0x000AEAD4
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
		entityControl.display.AddMeshMaterial(this.Material, this.FadeTime, this.HideBase);
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x000B091F File Offset: 0x000AEB1F
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Material",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x000B0946 File Offset: 0x000AEB46
	public ApplyMaterialNode()
	{
	}

	// Token: 0x04001DB0 RID: 7600
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x04001DB1 RID: 7601
	public Material Material;

	// Token: 0x04001DB2 RID: 7602
	[Tooltip("Time in seconds for the material to fade in")]
	public float FadeTime;

	// Token: 0x04001DB3 RID: 7603
	public bool HideBase;
}
