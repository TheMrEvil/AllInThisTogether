using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B7 RID: 951
	internal static class CameraEventUtils
	{
		// Token: 0x06001F59 RID: 8025 RVA: 0x00033230 File Offset: 0x00031430
		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}

		// Token: 0x04000AFE RID: 2814
		private const CameraEvent k_MinimumValue = CameraEvent.BeforeDepthTexture;

		// Token: 0x04000AFF RID: 2815
		private const CameraEvent k_MaximumValue = CameraEvent.AfterHaloAndLensFlares;
	}
}
