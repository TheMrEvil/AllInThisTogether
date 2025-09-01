using System;
using Fluxy;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001E RID: 30
	public class SampleDensityAndVelocity : MonoBehaviour
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00006CD3 File Offset: 0x00004ED3
		private void Awake()
		{
			this.rend = base.GetComponentInChildren<Renderer>();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006CE4 File Offset: 0x00004EE4
		private void FixedUpdate()
		{
			Vector3 velocityAt = this.container.GetVelocityAt(base.transform.position);
			Vector4 densityAt = this.container.GetDensityAt(base.transform.position);
			base.transform.rotation = Quaternion.LookRotation(velocityAt);
			this.rend.material.color = densityAt;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006D46 File Offset: 0x00004F46
		public SampleDensityAndVelocity()
		{
		}

		// Token: 0x040000D0 RID: 208
		public FluxyContainer container;

		// Token: 0x040000D1 RID: 209
		private Renderer rend;
	}
}
