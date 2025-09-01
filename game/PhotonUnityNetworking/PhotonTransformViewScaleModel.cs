using System;

namespace Photon.Pun
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class PhotonTransformViewScaleModel
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000B276 File Offset: 0x00009476
		public PhotonTransformViewScaleModel()
		{
		}

		// Token: 0x0400010D RID: 269
		public bool SynchronizeEnabled;

		// Token: 0x0400010E RID: 270
		public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;

		// Token: 0x0400010F RID: 271
		public float InterpolateMoveTowardsSpeed = 1f;

		// Token: 0x04000110 RID: 272
		public float InterpolateLerpSpeed;

		// Token: 0x0200003E RID: 62
		public enum InterpolateOptions
		{
			// Token: 0x0400014B RID: 331
			Disabled,
			// Token: 0x0400014C RID: 332
			MoveTowards,
			// Token: 0x0400014D RID: 333
			Lerp
		}
	}
}
