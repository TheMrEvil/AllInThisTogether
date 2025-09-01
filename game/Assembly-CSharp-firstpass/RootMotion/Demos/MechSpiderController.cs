using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012B RID: 299
	public class MechSpiderController : MonoBehaviour
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00055B65 File Offset: 0x00053D65
		public Vector3 inputVector
		{
			get
			{
				return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00055B88 File Offset: 0x00053D88
		private void Update()
		{
			Vector3 forward = this.cameraTransform.forward;
			Vector3 up = base.transform.up;
			Vector3.OrthoNormalize(ref up, ref forward);
			Quaternion quaternion = Quaternion.LookRotation(forward, base.transform.up);
			base.transform.Translate(quaternion * this.inputVector.normalized * Time.deltaTime * this.speed * this.mechSpider.scale, Space.World);
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, quaternion, Time.deltaTime * this.turnSpeed);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00055C35 File Offset: 0x00053E35
		public MechSpiderController()
		{
		}

		// Token: 0x04000A14 RID: 2580
		public MechSpider mechSpider;

		// Token: 0x04000A15 RID: 2581
		public Transform cameraTransform;

		// Token: 0x04000A16 RID: 2582
		public float speed = 6f;

		// Token: 0x04000A17 RID: 2583
		public float turnSpeed = 30f;
	}
}
