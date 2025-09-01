using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class Util_DisableRigidbodies : MonoBehaviour
{
	// Token: 0x060017B1 RID: 6065 RVA: 0x00094B90 File Offset: 0x00092D90
	private void Start()
	{
		Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = true;
		}
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x00094BBB File Offset: 0x00092DBB
	public Util_DisableRigidbodies()
	{
	}
}
