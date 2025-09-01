using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class FPD_IndentAttribute : PropertyAttribute
{
	// Token: 0x06000048 RID: 72 RVA: 0x000032AD File Offset: 0x000014AD
	public FPD_IndentAttribute(int indent = 1, int labelsWidth = 0, int spaceAfter = 0)
	{
		this.IndentCount = indent;
		this.LabelsWidth = labelsWidth;
		this.SpaceAfter = spaceAfter;
	}

	// Token: 0x04000039 RID: 57
	public int IndentCount = 1;

	// Token: 0x0400003A RID: 58
	public int LabelsWidth;

	// Token: 0x0400003B RID: 59
	public int SpaceAfter;
}
