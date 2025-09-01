using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[RequireComponent(typeof(Light))]
public class RotateLight : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x0000224D File Offset: 0x0000044D
	private void Awake()
	{
		this.l = base.GetComponent<Light>();
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000225C File Offset: 0x0000045C
	public void SetRotation(float angle)
	{
		Quaternion localRotation = this.l.transform.localRotation;
		Vector3 eulerAngles = localRotation.eulerAngles;
		eulerAngles.y = angle;
		localRotation.eulerAngles = eulerAngles;
		this.l.transform.localRotation = localRotation;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000022A3 File Offset: 0x000004A3
	public RotateLight()
	{
	}

	// Token: 0x0400000D RID: 13
	private Light l;
}
