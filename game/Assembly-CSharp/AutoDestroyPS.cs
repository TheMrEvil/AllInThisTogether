using System;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class AutoDestroyPS : MonoBehaviour
{
	// Token: 0x060016B4 RID: 5812 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
	private void Awake()
	{
		ParticleSystem.MainModule main = base.GetComponent<ParticleSystem>().main;
		this.timeLeft = main.startLifetimeMultiplier + main.duration;
		UnityEngine.Object.Destroy(base.gameObject, this.timeLeft);
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x0008F6FF File Offset: 0x0008D8FF
	public AutoDestroyPS()
	{
	}

	// Token: 0x04001644 RID: 5700
	private float timeLeft;
}
