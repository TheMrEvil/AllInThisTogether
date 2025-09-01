using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class BackgroundColorAttribute : PropertyAttribute
{
	// Token: 0x06000044 RID: 68 RVA: 0x00003220 File Offset: 0x00001420
	public BackgroundColorAttribute()
	{
		this.r = (this.g = (this.b = (this.a = 1f)));
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003259 File Offset: 0x00001459
	public BackgroundColorAttribute(float aR, float aG, float aB, float aA)
	{
		this.r = aR;
		this.g = aG;
		this.b = aB;
		this.a = aA;
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000046 RID: 70 RVA: 0x0000327E File Offset: 0x0000147E
	public Color Color
	{
		get
		{
			return new Color(this.r, this.g, this.b, this.a);
		}
	}

	// Token: 0x04000034 RID: 52
	public float r;

	// Token: 0x04000035 RID: 53
	public float g;

	// Token: 0x04000036 RID: 54
	public float b;

	// Token: 0x04000037 RID: 55
	public float a;
}
