using System;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class Logic_InputNode : LogicNode
{
	// Token: 0x060018FC RID: 6396 RVA: 0x0009BEC3 File Offset: 0x0009A0C3
	public override bool Evaluate(EffectProperties props)
	{
		return (!(props.SourceControl != PlayerControl.myInstance) || !(props.AffectedControl != PlayerControl.myInstance)) && PlayerControl.myInstance.Input.IsInputActive(this.Input);
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x0009BF00 File Offset: 0x0009A100
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Player Input";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x0009BF3D File Offset: 0x0009A13D
	public Logic_InputNode()
	{
	}

	// Token: 0x04001913 RID: 6419
	public PlayerInput.AbilityInput Input;
}
