using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public struct GlobalDynamicResolutionSettings
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		public static GlobalDynamicResolutionSettings NewDefault()
		{
			return new GlobalDynamicResolutionSettings
			{
				useMipBias = false,
				maxPercentage = 100f,
				minPercentage = 100f,
				dynResType = DynamicResolutionType.Hardware,
				upsampleFilter = DynamicResUpscaleFilter.CatmullRom,
				forcedPercentage = 100f,
				lowResTransparencyMinimumThreshold = 0f,
				rayTracingHalfResThreshold = 50f,
				enableDLSS = false,
				DLSSUseOptimalSettings = true,
				DLSSPerfQualitySetting = 0U,
				DLSSSharpness = 0.5f
			};
		}

		// Token: 0x040001E9 RID: 489
		public bool enabled;

		// Token: 0x040001EA RID: 490
		public bool useMipBias;

		// Token: 0x040001EB RID: 491
		public bool enableDLSS;

		// Token: 0x040001EC RID: 492
		public uint DLSSPerfQualitySetting;

		// Token: 0x040001ED RID: 493
		public bool DLSSUseOptimalSettings;

		// Token: 0x040001EE RID: 494
		[Range(0f, 1f)]
		public float DLSSSharpness;

		// Token: 0x040001EF RID: 495
		public float maxPercentage;

		// Token: 0x040001F0 RID: 496
		public float minPercentage;

		// Token: 0x040001F1 RID: 497
		public DynamicResolutionType dynResType;

		// Token: 0x040001F2 RID: 498
		public DynamicResUpscaleFilter upsampleFilter;

		// Token: 0x040001F3 RID: 499
		public bool forceResolution;

		// Token: 0x040001F4 RID: 500
		public float forcedPercentage;

		// Token: 0x040001F5 RID: 501
		public float lowResTransparencyMinimumThreshold;

		// Token: 0x040001F6 RID: 502
		public float rayTracingHalfResThreshold;
	}
}
