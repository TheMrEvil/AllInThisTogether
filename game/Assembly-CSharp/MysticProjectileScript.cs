using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class MysticProjectileScript : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00004F20 File Offset: 0x00003120
	private void Start()
	{
		this.projectileParticle = UnityEngine.Object.Instantiate<GameObject>(this.projectileParticle, base.transform.position, base.transform.rotation);
		this.projectileParticle.transform.parent = base.transform;
		if (this.muzzleParticle)
		{
			this.muzzleParticle = UnityEngine.Object.Instantiate<GameObject>(this.muzzleParticle, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(this.muzzleParticle, 1.5f);
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00004FB0 File Offset: 0x000031B0
	private void OnCollisionEnter(Collision hit)
	{
		if (!this.hasCollided)
		{
			this.hasCollided = true;
			this.impactParticle = UnityEngine.Object.Instantiate<GameObject>(this.impactParticle, base.transform.position, Quaternion.FromToRotation(Vector3.up, this.impactNormal));
			if (hit.gameObject.tag == "Destructible")
			{
				UnityEngine.Object.Destroy(hit.gameObject);
			}
			foreach (GameObject gameObject in this.trailParticles)
			{
				GameObject gameObject2 = base.transform.Find(this.projectileParticle.name + "/" + gameObject.name).gameObject;
				gameObject2.transform.parent = null;
				UnityEngine.Object.Destroy(gameObject2, 3f);
			}
			UnityEngine.Object.Destroy(this.projectileParticle, 3f);
			UnityEngine.Object.Destroy(this.impactParticle, 5f);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000050A2 File Offset: 0x000032A2
	public MysticProjectileScript()
	{
	}

	// Token: 0x0400003B RID: 59
	public GameObject impactParticle;

	// Token: 0x0400003C RID: 60
	public GameObject projectileParticle;

	// Token: 0x0400003D RID: 61
	public GameObject muzzleParticle;

	// Token: 0x0400003E RID: 62
	public GameObject[] trailParticles;

	// Token: 0x0400003F RID: 63
	[HideInInspector]
	public Vector3 impactNormal;

	// Token: 0x04000040 RID: 64
	private bool hasCollided;
}
