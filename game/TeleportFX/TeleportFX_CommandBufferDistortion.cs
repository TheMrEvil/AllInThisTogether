using System;
using UnityEngine;

namespace TeleportFX
{
	// Token: 0x02000003 RID: 3
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class TeleportFX_CommandBufferDistortion : MonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000027ED File Offset: 0x000009ED
		private void OnEnable()
		{
			TeleportFX_GlobalUpdate.CreateInstanceIfRequired();
			TeleportFX_GlobalUpdate.DistortionInstances.Add(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027FF File Offset: 0x000009FF
		private void OnDisable()
		{
			TeleportFX_GlobalUpdate.DistortionInstances.Remove(this);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000280D File Offset: 0x00000A0D
		public TeleportFX_CommandBufferDistortion()
		{
		}
	}
}
