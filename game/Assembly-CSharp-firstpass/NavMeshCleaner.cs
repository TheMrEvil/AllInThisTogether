using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class NavMeshCleaner : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x0000331E File Offset: 0x0000151E
	public NavMeshCleaner()
	{
	}

	// Token: 0x04000043 RID: 67
	public List<Vector3> m_WalkablePoint = new List<Vector3>();

	// Token: 0x04000044 RID: 68
	public float m_Height = 1f;

	// Token: 0x04000045 RID: 69
	public float m_Offset;

	// Token: 0x04000046 RID: 70
	public int m_MidLayerCount = 3;
}
