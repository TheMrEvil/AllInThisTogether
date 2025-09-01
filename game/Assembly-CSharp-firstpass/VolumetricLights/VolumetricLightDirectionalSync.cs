using System;
using UnityEngine;

namespace VolumetricLights
{
	// Token: 0x0200001A RID: 26
	[ExecuteInEditMode]
	[AddComponentMenu("")]
	[RequireComponent(typeof(VolumetricLight))]
	public class VolumetricLightDirectionalSync : MonoBehaviour
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000073E4 File Offset: 0x000055E4
		private void Start()
		{
			this.vl = base.GetComponent<VolumetricLight>();
			this.fakeLight = base.GetComponent<Light>();
			if (this.follow == null && Camera.main != null)
			{
				this.follow = Camera.main.transform;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007434 File Offset: 0x00005634
		private void LateUpdate()
		{
			if (this.directionalLight != null)
			{
				if (this.follow != null)
				{
					Vector3 position = this.follow.position;
					if (Vector3.Distance(this.lastFollowPos, position) > this.distanceUpdate)
					{
						this.lastFollowPos = position;
						base.transform.position = this.follow.position;
						base.transform.position -= this.directionalLight.transform.forward * this.vl.generatedRange * 0.5f;
					}
				}
				base.transform.forward = this.directionalLight.transform.forward;
				if (this.fakeLight != null)
				{
					this.fakeLight.enabled = false;
					this.fakeLight.color = this.directionalLight.color;
					this.fakeLight.intensity = this.directionalLight.intensity;
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000753E File Offset: 0x0000573E
		public VolumetricLightDirectionalSync()
		{
		}

		// Token: 0x040000E4 RID: 228
		[Tooltip("The directional light that is synced with this volumetric area light.")]
		public Light directionalLight;

		// Token: 0x040000E5 RID: 229
		[Tooltip("Makes this area light position follow the desired target. Usually this is the main camera.")]
		public Transform follow;

		// Token: 0x040000E6 RID: 230
		[Tooltip("Move volumetric light to 'follow' gameobject position if distance is greater than this value. Updating the position of this volumetric light area every frame is not recommended.")]
		public float distanceUpdate = 1f;

		// Token: 0x040000E7 RID: 231
		private VolumetricLight vl;

		// Token: 0x040000E8 RID: 232
		private Light fakeLight;

		// Token: 0x040000E9 RID: 233
		private Vector3 lastFollowPos;
	}
}
