using System;
using UnityEngine;

namespace MysticArsenal
{
	// Token: 0x020003D8 RID: 984
	public class MysticLightFade : MonoBehaviour
	{
		// Token: 0x06002015 RID: 8213 RVA: 0x000BE7E8 File Offset: 0x000BC9E8
		private void Start()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li = base.gameObject.GetComponent<Light>();
				this.initIntensity = this.li.intensity;
				return;
			}
			MonoBehaviour.print("No light object found on " + base.gameObject.name);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000BE844 File Offset: 0x000BCA44
		private void Update()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li.intensity -= this.initIntensity * (Time.deltaTime / this.life);
				if (this.killAfterLife && this.li.intensity <= 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000BE8AD File Offset: 0x000BCAAD
		public MysticLightFade()
		{
		}

		// Token: 0x04002062 RID: 8290
		[Header("Seconds to dim the light")]
		public float life = 0.2f;

		// Token: 0x04002063 RID: 8291
		public bool killAfterLife = true;

		// Token: 0x04002064 RID: 8292
		private Light li;

		// Token: 0x04002065 RID: 8293
		private float initIntensity;
	}
}
