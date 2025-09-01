using System;
using System.Collections.Generic;

// Token: 0x0200038C RID: 908
public class RotationOffsetNode : Node
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000B4125 File Offset: 0x000B2325
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x000B4128 File Offset: 0x000B2328
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 200f;
		inspectorProps.Title = "Rot Offset";
		return inspectorProps;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x000B414A File Offset: 0x000B234A
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		Node result = base.Clone(alreadyCloned, fullClone) as RotationOffsetNode;
		this.Offset = this.Offset.Copy();
		return result;
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x000B416A File Offset: 0x000B236A
	public override void OnCloned()
	{
		this.Offset = this.Offset.Copy();
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x000B417D File Offset: 0x000B237D
	public RotationOffsetNode()
	{
	}

	// Token: 0x04001E55 RID: 7765
	public RotOffset Offset;
}
