using System;

namespace FidelityFX
{
	// Token: 0x02000007 RID: 7
	public interface IFsr3UpscalerCallbacks
	{
		// Token: 0x06000012 RID: 18
		void ApplyMipmapBias(float biasOffset);

		// Token: 0x06000013 RID: 19
		void UndoMipmapBias();
	}
}
