using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class FPD_SuffixAttribute : PropertyAttribute
{
	// Token: 0x0600004A RID: 74 RVA: 0x000032E9 File Offset: 0x000014E9
	public FPD_SuffixAttribute(float min, float max, FPD_SuffixAttribute.SuffixMode mode = FPD_SuffixAttribute.SuffixMode.From0to100, string suffix = "%", bool editable = true, int wider = 0)
	{
		this.Min = min;
		this.Max = max;
		this.Mode = mode;
		this.Suffix = suffix;
		this.editableValue = editable;
		this.widerField = wider;
	}

	// Token: 0x0400003D RID: 61
	public readonly float Min;

	// Token: 0x0400003E RID: 62
	public readonly float Max;

	// Token: 0x0400003F RID: 63
	public readonly FPD_SuffixAttribute.SuffixMode Mode;

	// Token: 0x04000040 RID: 64
	public readonly string Suffix;

	// Token: 0x04000041 RID: 65
	public readonly bool editableValue;

	// Token: 0x04000042 RID: 66
	public readonly int widerField;

	// Token: 0x0200018E RID: 398
	public enum SuffixMode
	{
		// Token: 0x04000C54 RID: 3156
		From0to100,
		// Token: 0x04000C55 RID: 3157
		PercentageUnclamped,
		// Token: 0x04000C56 RID: 3158
		FromMinToMax,
		// Token: 0x04000C57 RID: 3159
		FromMinToMaxRounded
	}
}
