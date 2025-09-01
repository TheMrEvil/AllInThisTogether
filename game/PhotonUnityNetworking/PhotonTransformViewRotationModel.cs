using System;

namespace Photon.Pun
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class PhotonTransformViewRotationModel
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000B18D File Offset: 0x0000938D
		public PhotonTransformViewRotationModel()
		{
		}

		// Token: 0x04000107 RID: 263
		public bool SynchronizeEnabled;

		// Token: 0x04000108 RID: 264
		public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;

		// Token: 0x04000109 RID: 265
		public float InterpolateRotateTowardsSpeed = 180f;

		// Token: 0x0400010A RID: 266
		public float InterpolateLerpSpeed = 5f;

		// Token: 0x0200003D RID: 61
		public enum InterpolateOptions
		{
			// Token: 0x04000147 RID: 327
			Disabled,
			// Token: 0x04000148 RID: 328
			RotateTowards,
			// Token: 0x04000149 RID: 329
			Lerp
		}
	}
}
