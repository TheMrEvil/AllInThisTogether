using System;
using UnityEngine;

namespace SCPE
{
	// Token: 0x02000019 RID: 25
	[ExecuteInEditMode]
	[RequireComponent(typeof(Light))]
	public class FogLightSource : MonoBehaviour
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003FB4 File Offset: 0x000021B4
		private void OnEnable()
		{
			FogLightSource.sunDirection = -base.transform.forward;
			if (!this.sunLight)
			{
				this.sunLight = base.GetComponent<Light>();
				if (this.sunLight)
				{
					FogLightSource.color = this.sunLight.color;
					FogLightSource.intensity = this.sunLight.intensity;
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000401C File Offset: 0x0000221C
		private void OnDisable()
		{
			FogLightSource.sunDirection = Vector3.zero;
			Fog.LightDirection = Vector3.zero;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004034 File Offset: 0x00002234
		private void Update()
		{
			FogLightSource.sunDirection = -base.transform.forward;
			Fog.LightDirection = FogLightSource.sunDirection;
			if (this.sunLight)
			{
				FogLightSource.color = this.sunLight.color;
				FogLightSource.intensity = this.sunLight.intensity;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000408D File Offset: 0x0000228D
		public FogLightSource()
		{
		}

		// Token: 0x0400007D RID: 125
		public Light sunLight;

		// Token: 0x0400007E RID: 126
		public static Vector3 sunDirection;

		// Token: 0x0400007F RID: 127
		public static Color color;

		// Token: 0x04000080 RID: 128
		public static float intensity;
	}
}
