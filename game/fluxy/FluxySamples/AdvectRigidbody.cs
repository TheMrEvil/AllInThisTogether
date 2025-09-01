using System;
using Fluxy;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x02000016 RID: 22
	[RequireComponent(typeof(Rigidbody))]
	public class AdvectRigidbody : MonoBehaviour
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00005FC0 File Offset: 0x000041C0
		private void Awake()
		{
			this.rb = base.GetComponent<Rigidbody>();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005FCE File Offset: 0x000041CE
		private void FixedUpdate()
		{
			this.rb.velocity = this.container.GetVelocityAt(this.rb.position);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005FF1 File Offset: 0x000041F1
		public AdvectRigidbody()
		{
		}

		// Token: 0x040000A4 RID: 164
		public FluxyContainer container;

		// Token: 0x040000A5 RID: 165
		private Rigidbody rb;
	}
}
