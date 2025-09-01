using System;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class NoteNode : Node
{
	// Token: 0x06001D58 RID: 7512 RVA: 0x000B28CB File Offset: 0x000B0ACB
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 300f;
		inspectorProps.ShowInputNode = false;
		inspectorProps.Title = "Note";
		return inspectorProps;
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x000B28F4 File Offset: 0x000B0AF4
	public NoteNode()
	{
	}

	// Token: 0x04001E08 RID: 7688
	[TextArea(5, 40)]
	public string Note;
}
