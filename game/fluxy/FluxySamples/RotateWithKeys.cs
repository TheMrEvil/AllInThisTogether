using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001D RID: 29
	public class RotateWithKeys : MonoBehaviour
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00006C38 File Offset: 0x00004E38
		private void Update()
		{
			if (Input.GetKey(KeyCode.A))
			{
				this.angularAccel = Time.deltaTime * -this.speed;
			}
			if (Input.GetKey(KeyCode.D))
			{
				this.angularAccel = Time.deltaTime * this.speed;
			}
			this.angularAccel *= Mathf.Pow(1f - this.angularDrag, Time.deltaTime);
			base.transform.Rotate(Vector3.forward, this.angularAccel);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006CB5 File Offset: 0x00004EB5
		public RotateWithKeys()
		{
		}

		// Token: 0x040000CD RID: 205
		public float speed = 20f;

		// Token: 0x040000CE RID: 206
		[Range(0f, 1f)]
		public float angularDrag = 0.8f;

		// Token: 0x040000CF RID: 207
		private float angularAccel;
	}
}
