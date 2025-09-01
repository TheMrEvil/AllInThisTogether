using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class ModManaCheckNode : ModTagNode
{
	// Token: 0x06001C6A RID: 7274 RVA: 0x000AD884 File Offset: 0x000ABA84
	public override bool Validate(EntityControl control)
	{
		if (!(control is PlayerControl))
		{
			return false;
		}
		PlayerControl playerControl = control as PlayerControl;
		return this.Test == ModManaCheckNode.ManaTest.HasNeutral && playerControl.Mana.HasNeutralMana;
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x000AD8B7 File Offset: 0x000ABAB7
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Mana Test";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x000AD8DF File Offset: 0x000ABADF
	public ModManaCheckNode()
	{
	}

	// Token: 0x04001D42 RID: 7490
	public ModManaCheckNode.ManaTest Test;

	// Token: 0x0200066B RID: 1643
	public enum ManaTest
	{
		// Token: 0x04002B81 RID: 11137
		HasNeutral
	}
}
