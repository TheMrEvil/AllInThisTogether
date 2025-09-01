using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class FPD_FixedCurveWindowAttribute : PropertyAttribute
{
	// Token: 0x06000040 RID: 64 RVA: 0x0000319D File Offset: 0x0000139D
	public FPD_FixedCurveWindowAttribute(float startTime = 0f, float startValue = 0f, float endTime = 1f, float endValue = 1f, float r = 0f, float g = 1f, float b = 1f, float a = 1f)
	{
		this.StartTime = startTime;
		this.StartValue = startValue;
		this.EndTime = endTime;
		this.EndValue = endValue;
		this.Color = new Color(r, g, b, a);
	}

	// Token: 0x04000028 RID: 40
	public float StartTime;

	// Token: 0x04000029 RID: 41
	public float EndTime;

	// Token: 0x0400002A RID: 42
	public float StartValue;

	// Token: 0x0400002B RID: 43
	public float EndValue;

	// Token: 0x0400002C RID: 44
	public Color Color;
}
