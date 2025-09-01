using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x02000019 RID: 25
	public class FirstPersonLauncher : MonoBehaviour
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000067A4 File Offset: 0x000049A4
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Rigidbody component = UnityEngine.Object.Instantiate<GameObject>(this.prefab, ray.origin, Quaternion.identity).GetComponent<Rigidbody>();
				if (component != null)
				{
					component.velocity = ray.direction * this.power;
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006807 File Offset: 0x00004A07
		public FirstPersonLauncher()
		{
		}

		// Token: 0x040000BF RID: 191
		public GameObject prefab;

		// Token: 0x040000C0 RID: 192
		public float power = 2f;
	}
}
