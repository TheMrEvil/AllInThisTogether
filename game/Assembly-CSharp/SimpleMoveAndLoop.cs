using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class SimpleMoveAndLoop : MonoBehaviour
{
	// Token: 0x06000DFE RID: 3582 RVA: 0x0005982E File Offset: 0x00057A2E
	private void Start()
	{
		this.initialPos = base.transform.position;
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x00059844 File Offset: 0x00057A44
	private void Update()
	{
		base.transform.position += this.dir * (this.speed * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.initialPos) >= this.loopDistance)
		{
			base.transform.position = this.initialPos;
		}
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x000598AD File Offset: 0x00057AAD
	public SimpleMoveAndLoop()
	{
	}

	// Token: 0x04000B74 RID: 2932
	[SerializeField]
	private Vector3 dir;

	// Token: 0x04000B75 RID: 2933
	[SerializeField]
	private float speed;

	// Token: 0x04000B76 RID: 2934
	[SerializeField]
	private float loopDistance;

	// Token: 0x04000B77 RID: 2935
	private Vector3 initialPos;
}
