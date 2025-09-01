using System;
using UnityEngine;

namespace SCPE
{
	// Token: 0x02000045 RID: 69
	[ExecuteInEditMode]
	[RequireComponent(typeof(Light))]
	public class SunshaftCaster : MonoBehaviour
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000077C8 File Offset: 0x000059C8
		private void OnEnable()
		{
			this.sunPosition = base.transform.position;
			if (!this.sunLight)
			{
				this.sunLight = base.GetComponent<Light>();
				if (this.sunLight)
				{
					SunshaftCaster.color = this.sunLight.color;
					SunshaftCaster.intensity = this.sunLight.intensity;
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000782C File Offset: 0x00005A2C
		private void OnDisable()
		{
			this.sunPosition = Vector3.zero;
			Sunshafts.sunPosition = Vector3.zero;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007843 File Offset: 0x00005A43
		private void OnDrawGizmos()
		{
			Gizmos.DrawIcon(Sunshafts.sunPosition, "LensFlare Icon", true);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007855 File Offset: 0x00005A55
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
			Gizmos.DrawRay(base.transform.position, this.sunPosition);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000788C File Offset: 0x00005A8C
		private void Update()
		{
			this.sunPosition = -base.transform.forward * (this.infiniteDistance ? 1E+10f : this.distance);
			Sunshafts.sunPosition = this.sunPosition;
			if (this.sunLight)
			{
				SunshaftCaster.color = this.sunLight.color;
				SunshaftCaster.intensity = this.sunLight.intensity;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007901 File Offset: 0x00005B01
		public SunshaftCaster()
		{
		}

		// Token: 0x04000141 RID: 321
		[Range(0f, 10000f)]
		public float distance = 10000f;

		// Token: 0x04000142 RID: 322
		[Tooltip("Use this to match the casting position to a skybox sun")]
		public bool infiniteDistance;

		// Token: 0x04000143 RID: 323
		[Tooltip("This light will be used to sample the intensity if color")]
		public Light sunLight;

		// Token: 0x04000144 RID: 324
		private Vector3 sunPosition;

		// Token: 0x04000145 RID: 325
		public static Color color;

		// Token: 0x04000146 RID: 326
		public static float intensity;
	}
}
