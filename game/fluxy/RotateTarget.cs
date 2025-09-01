using System;
using Fluxy;
using UnityEngine;

// Token: 0x02000006 RID: 6
[RequireComponent(typeof(FluxyTarget))]
public class RotateTarget : MonoBehaviour
{
	// Token: 0x0600000C RID: 12 RVA: 0x000022AB File Offset: 0x000004AB
	private void Start()
	{
		this.target = base.GetComponent<FluxyTarget>();
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000022B9 File Offset: 0x000004B9
	private void Update()
	{
		this.target.rotation += this.speed * Time.deltaTime * 57.29578f;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000022DF File Offset: 0x000004DF
	public RotateTarget()
	{
	}

	// Token: 0x0400000E RID: 14
	public float speed = 1f;

	// Token: 0x0400000F RID: 15
	private FluxyTarget target;
}
