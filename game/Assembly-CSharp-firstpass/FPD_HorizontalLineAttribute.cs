using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class FPD_HorizontalLineAttribute : PropertyAttribute
{
	// Token: 0x06000049 RID: 73 RVA: 0x000032D1 File Offset: 0x000014D1
	public FPD_HorizontalLineAttribute(float r = 0.55f, float g = 0.55f, float b = 0.55f, float a = 0.7f)
	{
		this.color = new Color(r, g, b, a);
	}

	// Token: 0x0400003C RID: 60
	public Color color;
}
