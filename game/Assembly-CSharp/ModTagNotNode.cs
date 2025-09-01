using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class ModTagNotNode : ModTagNode
{
	// Token: 0x06001C79 RID: 7289 RVA: 0x000ADACC File Offset: 0x000ABCCC
	public override bool Validate(EntityControl control)
	{
		return !(this.Test == null) && !(this.Test as ModTagNode).Validate(control);
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x000ADAF2 File Offset: 0x000ABCF2
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "NOT";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x000ADB21 File Offset: 0x000ABD21
	public ModTagNotNode()
	{
	}

	// Token: 0x04001D49 RID: 7497
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(ModTagNode), false, "", PortLocation.Vertical)]
	public Node Test;
}
