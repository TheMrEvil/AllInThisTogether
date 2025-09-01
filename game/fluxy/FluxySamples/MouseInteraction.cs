using System;
using Fluxy;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001B RID: 27
	public class MouseInteraction : MonoBehaviour
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00006ADC File Offset: 0x00004CDC
		private void Awake()
		{
			this.target = base.GetComponent<FluxyTarget>();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006AEC File Offset: 0x00004CEC
		private void Update()
		{
			if (Camera.main == null)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.target.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.6f, 1f, 1f, 1f, 1f, 1f);
			}
			if (Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				base.transform.position = ray.origin + ray.direction;
				this.target.enabled = true;
				return;
			}
			this.target.enabled = false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006B96 File Offset: 0x00004D96
		public MouseInteraction()
		{
		}

		// Token: 0x040000C8 RID: 200
		private FluxyTarget target;
	}
}
