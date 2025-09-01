using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class AddMeshFXNode : EffectNode
{
	// Token: 0x06001D08 RID: 7432 RVA: 0x000B0838 File Offset: 0x000AEA38
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
		entityControl.display.AddMeshFX(this.Effect, properties, this.MeshIndexOverride);
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x000B087E File Offset: 0x000AEA7E
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Mesh VFX",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x000B08A5 File Offset: 0x000AEAA5
	private bool ValidateObject(GameObject o)
	{
		return o == null || o.GetComponent<MeshFX>() != null;
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x000B08BE File Offset: 0x000AEABE
	public AddMeshFXNode()
	{
	}

	// Token: 0x04001DAD RID: 7597
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x04001DAE RID: 7598
	public GameObject Effect;

	// Token: 0x04001DAF RID: 7599
	public int MeshIndexOverride = -1;
}
