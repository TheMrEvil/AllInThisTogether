using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class FPD_WidthAttribute : PropertyAttribute
{
	// Token: 0x06000047 RID: 71 RVA: 0x0000329D File Offset: 0x0000149D
	public FPD_WidthAttribute(int labelWidth)
	{
		this.LabelWidth = (float)labelWidth;
	}

	// Token: 0x04000038 RID: 56
	public float LabelWidth;
}
