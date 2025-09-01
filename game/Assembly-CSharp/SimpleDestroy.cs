using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class SimpleDestroy : MonoBehaviour
{
	// Token: 0x06000DFC RID: 3580 RVA: 0x000597E0 File Offset: 0x000579E0
	public void SimpleDestroyAll()
	{
		for (int i = base.transform.childCount; i > 0; i--)
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(i - 1).gameObject);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00059826 File Offset: 0x00057A26
	public SimpleDestroy()
	{
	}
}
