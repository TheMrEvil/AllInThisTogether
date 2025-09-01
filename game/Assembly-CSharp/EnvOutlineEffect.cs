using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200024D RID: 589
[RequireComponent(typeof(LineRenderer))]
public class EnvOutlineEffect : MonoBehaviour
{
	// Token: 0x060017DC RID: 6108 RVA: 0x00095664 File Offset: 0x00093864
	public void SetupOutline()
	{
		LineRenderer component = base.GetComponent<LineRenderer>();
		Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
		List<Vector3> list = new List<Vector3>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			list.Add(base.transform.GetChild(i).localPosition);
		}
		component.positionCount = list.Count;
		component.SetPositions(list.ToArray());
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x000956D1 File Offset: 0x000938D1
	public EnvOutlineEffect()
	{
	}
}
