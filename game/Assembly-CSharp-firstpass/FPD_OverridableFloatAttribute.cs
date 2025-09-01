using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class FPD_OverridableFloatAttribute : PropertyAttribute
{
	// Token: 0x06000043 RID: 67 RVA: 0x00003203 File Offset: 0x00001403
	public FPD_OverridableFloatAttribute(string boolVariableName, string targetVariableName, int labelWidth = 90)
	{
		this.BoolVarName = boolVariableName;
		this.TargetVarName = targetVariableName;
		this.LabelWidth = labelWidth;
	}

	// Token: 0x04000031 RID: 49
	public string BoolVarName;

	// Token: 0x04000032 RID: 50
	public string TargetVarName;

	// Token: 0x04000033 RID: 51
	public int LabelWidth;
}
