using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class FPD_HeaderAttribute : PropertyAttribute
{
	// Token: 0x06000041 RID: 65 RVA: 0x000031D5 File Offset: 0x000013D5
	public FPD_HeaderAttribute(string headerText, float upperPadding = 6f, float bottomPadding = 4f, int addHeight = 2)
	{
		this.HeaderText = headerText;
		this.UpperPadding = upperPadding;
		this.BottomPadding = bottomPadding;
		this.Height = (float)addHeight;
	}

	// Token: 0x0400002D RID: 45
	public string HeaderText;

	// Token: 0x0400002E RID: 46
	public float UpperPadding;

	// Token: 0x0400002F RID: 47
	public float BottomPadding;

	// Token: 0x04000030 RID: 48
	public float Height;
}
