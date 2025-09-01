using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012D RID: 301
	public class MechSpiderParticles : MonoBehaviour
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x000561F2 File Offset: 0x000543F2
		private void Start()
		{
			this.particles = (ParticleSystem)base.GetComponent(typeof(ParticleSystem));
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00056210 File Offset: 0x00054410
		private void Update()
		{
			float magnitude = this.mechSpiderController.inputVector.magnitude;
			float constant = Mathf.Clamp(magnitude * 50f, 30f, 50f);
			this.particles.emission.rateOverTime = new ParticleSystem.MinMaxCurve(constant);
			this.particles.main.startColor = new Color(this.particles.main.startColor.color.r, this.particles.main.startColor.color.g, this.particles.main.startColor.color.b, Mathf.Clamp(magnitude, 0.4f, 1f));
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000562F5 File Offset: 0x000544F5
		public MechSpiderParticles()
		{
		}

		// Token: 0x04000A2E RID: 2606
		public MechSpiderController mechSpiderController;

		// Token: 0x04000A2F RID: 2607
		private ParticleSystem particles;
	}
}
