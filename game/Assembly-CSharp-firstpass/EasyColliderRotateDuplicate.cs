using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[Serializable]
public class EasyColliderRotateDuplicate
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002D34 File Offset: 0x00000F34
	public EasyColliderRotateDuplicate()
	{
	}

	// Token: 0x04000021 RID: 33
	public bool enabled;

	// Token: 0x04000022 RID: 34
	public EasyColliderRotateDuplicate.ROTATE_AXIS axis;

	// Token: 0x04000023 RID: 35
	public int NumberOfDuplications = 4;

	// Token: 0x04000024 RID: 36
	public float StartRotation;

	// Token: 0x04000025 RID: 37
	public float EndRotation = 360f;

	// Token: 0x04000026 RID: 38
	public GameObject pivot;

	// Token: 0x04000027 RID: 39
	public GameObject attachTo;

	// Token: 0x0200018B RID: 395
	public enum ROTATE_AXIS
	{
		// Token: 0x04000C45 RID: 3141
		X,
		// Token: 0x04000C46 RID: 3142
		Y,
		// Token: 0x04000C47 RID: 3143
		Z
	}
}
