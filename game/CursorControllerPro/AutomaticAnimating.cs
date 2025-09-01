using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AutomaticAnimating : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		base.InvokeRepeating("Animating", 1f, 1f);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
	private void Animating()
	{
		if (!base.GetComponent<Animator>().GetBool("Fade") && !this.calledYet)
		{
			this.calledYet = true;
			base.GetComponent<Animator>().SetBool("Fade", true);
		}
		else if (base.GetComponent<Animator>().GetBool("Fade") && !this.calledYet)
		{
			this.calledYet = true;
			base.GetComponent<Animator>().SetBool("Fade", false);
		}
		this.calledYet = false;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020E2 File Offset: 0x000002E2
	public AutomaticAnimating()
	{
	}

	// Token: 0x04000001 RID: 1
	private bool calledYet;
}
