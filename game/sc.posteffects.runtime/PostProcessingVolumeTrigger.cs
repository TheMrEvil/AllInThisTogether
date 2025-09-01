using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004F RID: 79
	[ExecuteInEditMode]
	public class PostProcessingVolumeTrigger : MonoBehaviour
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000842C File Offset: 0x0000662C
		private void OnEnable()
		{
			if (this.volume == null)
			{
				this.volume = base.GetComponent<PostProcessVolume>();
				if (this.volume)
				{
					this.volume.weight = 0f;
					return;
				}
			}
			else
			{
				this.volume.weight = 0f;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008481 File Offset: 0x00006681
		private void OnTriggerEnter(Collider other)
		{
			this.Trigger();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008489 File Offset: 0x00006689
		public void Trigger()
		{
			this.currentWeight = 1f;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008496 File Offset: 0x00006696
		private void Update()
		{
			this.currentWeight = Mathf.Clamp01(this.currentWeight - Time.deltaTime * this.decreaseSpeed);
			if (!this.volume)
			{
				return;
			}
			this.volume.weight = this.currentWeight;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000084D5 File Offset: 0x000066D5
		public PostProcessingVolumeTrigger()
		{
		}

		// Token: 0x04000165 RID: 357
		[Header("Target volume")]
		public PostProcessVolume volume;

		// Token: 0x04000166 RID: 358
		[Space]
		public float decreaseSpeed = 1f;

		// Token: 0x04000167 RID: 359
		private float currentWeight;
	}
}
