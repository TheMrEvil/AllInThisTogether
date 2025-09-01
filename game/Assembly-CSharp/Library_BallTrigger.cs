using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class Library_BallTrigger : MonoBehaviour
{
	// Token: 0x06000D39 RID: 3385 RVA: 0x00054B6D File Offset: 0x00052D6D
	private void OnTriggerEnter(Collider other)
	{
		if (other == this.Ball && !this.TriggerActivator.activeSelf)
		{
			this.TriggerActivator.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x00054B9B File Offset: 0x00052D9B
	public Library_BallTrigger()
	{
	}

	// Token: 0x04000ABE RID: 2750
	public Collider Ball;

	// Token: 0x04000ABF RID: 2751
	public GameObject TriggerActivator;
}
