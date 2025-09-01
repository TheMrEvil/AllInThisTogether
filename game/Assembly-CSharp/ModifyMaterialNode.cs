using System;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class ModifyMaterialNode : EffectNode
{
	// Token: 0x06001D0F RID: 7439 RVA: 0x000B0958 File Offset: 0x000AEB58
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
		float value = this.Val;
		if (this.Value != null)
		{
			value = (this.Value as NumberNode).Evaluate(properties);
		}
		entityControl.display.ModifyMeshMaterial(this.Material, this.Property, value);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x000B09C5 File Offset: 0x000AEBC5
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modify Material",
			MinInspectorSize = new Vector2(280f, 0f)
		};
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x000B09EC File Offset: 0x000AEBEC
	public ModifyMaterialNode()
	{
	}

	// Token: 0x04001DB4 RID: 7604
	public ApplyOn ApplyOn = ApplyOn.Affected;

	// Token: 0x04001DB5 RID: 7605
	public Material Material;

	// Token: 0x04001DB6 RID: 7606
	public string Property;

	// Token: 0x04001DB7 RID: 7607
	public float Val;

	// Token: 0x04001DB8 RID: 7608
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Value", PortLocation.Default)]
	public Node Value;
}
