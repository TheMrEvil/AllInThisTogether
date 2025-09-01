using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class LookAtPlayerCam : MonoBehaviour
{
	// Token: 0x0600173E RID: 5950 RVA: 0x000930DC File Offset: 0x000912DC
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		Vector3 vector = (base.transform.position - PlayerControl.MyCamera.transform.position).normalized;
		if (this.KeepOnPlane)
		{
			vector = Vector3.ProjectOnPlane(vector, Vector3.up);
		}
		if (this.Invert)
		{
			vector = -vector;
		}
		base.transform.LookAt(base.transform.position + vector);
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x0009315E File Offset: 0x0009135E
	public LookAtPlayerCam()
	{
	}

	// Token: 0x04001704 RID: 5892
	public bool Invert;

	// Token: 0x04001705 RID: 5893
	public bool KeepOnPlane;
}
