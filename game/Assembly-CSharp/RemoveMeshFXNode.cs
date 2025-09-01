using System;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class RemoveMeshFXNode : EffectNode
{
	// Token: 0x06001D15 RID: 7445 RVA: 0x000B0A78 File Offset: 0x000AEC78
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
		entityControl.display.RemoveMeshFX(this.Effect);
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x000B0AB7 File Offset: 0x000AECB7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remove Mesh VFX",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x000B0ADE File Offset: 0x000AECDE
	private bool ValidateObject(GameObject o)
	{
		return o == null || o.GetComponent<MeshFX>() != null;
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x000B0AF7 File Offset: 0x000AECF7
	public RemoveMeshFXNode()
	{
	}

	// Token: 0x04001DBC RID: 7612
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x04001DBD RID: 7613
	public GameObject Effect;
}
