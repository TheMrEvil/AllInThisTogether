using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ObjectRotate : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x000036BB File Offset: 0x000018BB
	private void Start()
	{
		this.startAngles = base.transform.eulerAngles;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000036D0 File Offset: 0x000018D0
	private void Update()
	{
		float t = Mathf.PingPong(Time.time * this.speed, 1f);
		t = Mathf.SmoothStep(0f, 1f, t);
		Vector3 eulerAngles = Vector3.Slerp(this.startAngles, this.endAngles, t);
		base.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003724 File Offset: 0x00001924
	public ObjectRotate()
	{
	}

	// Token: 0x04000050 RID: 80
	public Vector3 endAngles;

	// Token: 0x04000051 RID: 81
	public float speed = 0.5f;

	// Token: 0x04000052 RID: 82
	private Vector3 startAngles;
}
