using System;

namespace Photon.Pun
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class PhotonTransformViewPositionModel
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public PhotonTransformViewPositionModel()
		{
		}

		// Token: 0x040000F5 RID: 245
		public bool SynchronizeEnabled;

		// Token: 0x040000F6 RID: 246
		public bool TeleportEnabled = true;

		// Token: 0x040000F7 RID: 247
		public float TeleportIfDistanceGreaterThan = 3f;

		// Token: 0x040000F8 RID: 248
		public PhotonTransformViewPositionModel.InterpolateOptions InterpolateOption = PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed;

		// Token: 0x040000F9 RID: 249
		public float InterpolateMoveTowardsSpeed = 1f;

		// Token: 0x040000FA RID: 250
		public float InterpolateLerpSpeed = 1f;

		// Token: 0x040000FB RID: 251
		public PhotonTransformViewPositionModel.ExtrapolateOptions ExtrapolateOption;

		// Token: 0x040000FC RID: 252
		public float ExtrapolateSpeed = 1f;

		// Token: 0x040000FD RID: 253
		public bool ExtrapolateIncludingRoundTripTime = true;

		// Token: 0x040000FE RID: 254
		public int ExtrapolateNumberOfStoredPositions = 1;

		// Token: 0x0200003B RID: 59
		public enum InterpolateOptions
		{
			// Token: 0x0400013C RID: 316
			Disabled,
			// Token: 0x0400013D RID: 317
			FixedSpeed,
			// Token: 0x0400013E RID: 318
			EstimatedSpeed,
			// Token: 0x0400013F RID: 319
			SynchronizeValues,
			// Token: 0x04000140 RID: 320
			Lerp
		}

		// Token: 0x0200003C RID: 60
		public enum ExtrapolateOptions
		{
			// Token: 0x04000142 RID: 322
			Disabled,
			// Token: 0x04000143 RID: 323
			SynchronizeValues,
			// Token: 0x04000144 RID: 324
			EstimateSpeedAndTurn,
			// Token: 0x04000145 RID: 325
			FixedSpeed
		}
	}
}
