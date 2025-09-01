using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class AnimBoneScaler : MonoBehaviour
{
	// Token: 0x060016B2 RID: 5810 RVA: 0x0008F68E File Offset: 0x0008D88E
	private void LateUpdate()
	{
		base.transform.localScale = Vector3.one * this.Scale;
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x0008F6AB File Offset: 0x0008D8AB
	public AnimBoneScaler()
	{
	}

	// Token: 0x04001643 RID: 5699
	public float Scale = 1f;
}
