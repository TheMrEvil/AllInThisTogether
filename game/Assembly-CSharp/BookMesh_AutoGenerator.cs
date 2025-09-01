using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class BookMesh_AutoGenerator : MonoBehaviour
{
	// Token: 0x0600001F RID: 31 RVA: 0x00004298 File Offset: 0x00002498
	private void Start()
	{
		BookMesh[] componentsInChildren = base.GetComponentsInChildren<BookMesh>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GenerateMesh();
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000042C2 File Offset: 0x000024C2
	public BookMesh_AutoGenerator()
	{
	}
}
