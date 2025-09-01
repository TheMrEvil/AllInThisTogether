using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MysticArsenal
{
	// Token: 0x020003D6 RID: 982
	public class MysticFireProjectile : MonoBehaviour
	{
		// Token: 0x0600200B RID: 8203 RVA: 0x000BE5CB File Offset: 0x000BC7CB
		private void Start()
		{
			this.selectedProjectileButton = GameObject.Find("Button").GetComponent<MysticButtonScript>();
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000BE5E4 File Offset: 0x000BC7E4
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				this.nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				this.previousEffect();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.previousEffect();
			}
			if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out this.hit, 100f))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.projectiles[this.currentProjectile], this.spawnPosition.position, Quaternion.identity);
				gameObject.transform.LookAt(this.hit.point);
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * this.speed);
				gameObject.GetComponent<MysticProjectileScript>().impactNormal = this.hit.normal;
			}
			Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100f, Color.yellow);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000BE726 File Offset: 0x000BC926
		public void nextEffect()
		{
			if (this.currentProjectile < this.projectiles.Length - 1)
			{
				this.currentProjectile++;
			}
			else
			{
				this.currentProjectile = 0;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000BE75C File Offset: 0x000BC95C
		public void previousEffect()
		{
			if (this.currentProjectile > 0)
			{
				this.currentProjectile--;
			}
			else
			{
				this.currentProjectile = this.projectiles.Length - 1;
			}
			this.selectedProjectileButton.getProjectileNames();
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000BE792 File Offset: 0x000BC992
		public void AdjustSpeed(float newSpeed)
		{
			this.speed = newSpeed;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000BE79B File Offset: 0x000BC99B
		public MysticFireProjectile()
		{
		}

		// Token: 0x0400205A RID: 8282
		private RaycastHit hit;

		// Token: 0x0400205B RID: 8283
		public GameObject[] projectiles;

		// Token: 0x0400205C RID: 8284
		public Transform spawnPosition;

		// Token: 0x0400205D RID: 8285
		[HideInInspector]
		public int currentProjectile;

		// Token: 0x0400205E RID: 8286
		public float speed = 1000f;

		// Token: 0x0400205F RID: 8287
		private MysticButtonScript selectedProjectileButton;
	}
}
