using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200000A RID: 10
	[ExecuteAlways]
	public class MagicFX5_CommandBufferDistortion : MonoBehaviour
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003266 File Offset: 0x00001466
		private void OnEnable()
		{
			MagicFX5_GlobalUpdate.CreateInstanceIfRequired();
			MagicFX5_GlobalUpdate.DistortionInstances.Add(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003279 File Offset: 0x00001479
		private void OnDisable()
		{
			MagicFX5_GlobalUpdate.DistortionInstances.Remove(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003287 File Offset: 0x00001487
		public MagicFX5_CommandBufferDistortion()
		{
		}
	}
}
