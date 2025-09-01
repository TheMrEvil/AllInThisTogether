using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class TargetDummy : AIControl
{
	// Token: 0x0600047C RID: 1148 RVA: 0x000222B8 File Offset: 0x000204B8
	public override void OnDie(DamageInfo dmg)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.DeathExplode, this.DeathExplode.transform.position, this.DeathExplode.transform.rotation).SetActive(true);
		this.MeshHolder.SetActive(false);
		base.Invoke("Destroy", 3f);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00022312 File Offset: 0x00020512
	public TargetDummy()
	{
	}

	// Token: 0x040003BA RID: 954
	public GameObject DeathExplode;

	// Token: 0x040003BB RID: 955
	public GameObject MeshHolder;
}
