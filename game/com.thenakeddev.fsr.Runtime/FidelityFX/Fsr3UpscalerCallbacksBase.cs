using System;
using UnityEngine;

namespace FidelityFX
{
	// Token: 0x02000008 RID: 8
	public class Fsr3UpscalerCallbacksBase : IFsr3UpscalerCallbacks
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002764 File Offset: 0x00000964
		public virtual void ApplyMipmapBias(float biasOffset)
		{
			if (float.IsNaN(biasOffset) || float.IsInfinity(biasOffset))
			{
				return;
			}
			this.CurrentBiasOffset += biasOffset;
			if (Mathf.Approximately(this.CurrentBiasOffset, 0f))
			{
				this.CurrentBiasOffset = 0f;
			}
			foreach (Texture2D texture2D in Resources.FindObjectsOfTypeAll<Texture2D>())
			{
				if (texture2D.mipmapCount > 1)
				{
					texture2D.mipMapBias += biasOffset;
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027DC File Offset: 0x000009DC
		public virtual void UndoMipmapBias()
		{
			if (this.CurrentBiasOffset == 0f)
			{
				return;
			}
			this.ApplyMipmapBias(-this.CurrentBiasOffset);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027F9 File Offset: 0x000009F9
		public Fsr3UpscalerCallbacksBase()
		{
		}

		// Token: 0x0400004C RID: 76
		protected float CurrentBiasOffset;
	}
}
