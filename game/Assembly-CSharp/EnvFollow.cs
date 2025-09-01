using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class EnvFollow : MonoBehaviour
{
	// Token: 0x060017DA RID: 6106 RVA: 0x000955E8 File Offset: 0x000937E8
	private void Update()
	{
		if (!(PlayerControl.myInstance == null))
		{
			base.transform.position = PlayerControl.myInstance.Movement.cameraRoot.transform.position;
			return;
		}
		if (NetworkManager.instance.loadingCamera == null)
		{
			return;
		}
		base.transform.position = NetworkManager.instance.loadingCamera.transform.position;
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x00095659 File Offset: 0x00093859
	public EnvFollow()
	{
	}
}
