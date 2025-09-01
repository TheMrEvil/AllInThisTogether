using System;
using UnityEngine;

namespace MimicSpace
{
	// Token: 0x020003DD RID: 989
	public class Movement : MonoBehaviour
	{
		// Token: 0x06002031 RID: 8241 RVA: 0x000BF991 File Offset: 0x000BDB91
		private void Start()
		{
			this.myMimic = base.GetComponent<Mimic>();
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000BF9A0 File Offset: 0x000BDBA0
		private void Update()
		{
			this.velocity = Vector3.Lerp(this.velocity, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized * this.speed, this.velocityLerpCoef * Time.deltaTime);
			this.myMimic.velocity = this.velocity;
			base.transform.position = base.transform.position + this.velocity * Time.deltaTime;
			Vector3 position = base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 5f, -Vector3.up, out raycastHit, 100f, this.myMimic.LegLayerMask))
			{
				position = new Vector3(base.transform.position.x, raycastHit.point.y + this.height, base.transform.position.z);
			}
			base.transform.position = Vector3.Lerp(base.transform.position, position, this.velocityLerpCoef * Time.deltaTime);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000BFAEC File Offset: 0x000BDCEC
		public Movement()
		{
		}

		// Token: 0x040020A1 RID: 8353
		[Header("Controls")]
		[Tooltip("Body Height from ground")]
		[Range(0.5f, 5f)]
		public float height = 0.8f;

		// Token: 0x040020A2 RID: 8354
		public float speed = 5f;

		// Token: 0x040020A3 RID: 8355
		private Vector3 velocity = Vector3.zero;

		// Token: 0x040020A4 RID: 8356
		public float velocityLerpCoef = 4f;

		// Token: 0x040020A5 RID: 8357
		private Mimic myMimic;
	}
}
