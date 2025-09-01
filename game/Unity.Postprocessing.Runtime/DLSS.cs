using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000026 RID: 38
	[Preserve]
	[Serializable]
	public class DLSS
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000055D7 File Offset: 0x000037D7
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000055DF File Offset: 0x000037DF
		public Vector2 jitter
		{
			[CompilerGenerated]
			get
			{
				return this.<jitter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<jitter>k__BackingField = value;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000055E8 File Offset: 0x000037E8
		public bool IsSupported()
		{
			return false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000055EC File Offset: 0x000037EC
		public DLSS()
		{
		}

		// Token: 0x0400009E RID: 158
		public PostProcessLayer.Antialiasing fallBackAA;

		// Token: 0x0400009F RID: 159
		[CompilerGenerated]
		private Vector2 <jitter>k__BackingField;

		// Token: 0x040000A0 RID: 160
		[Header("DLSS Settings")]
		public DLSS_Quality qualityMode = DLSS_Quality.MaximumQuality;

		// Token: 0x040000A1 RID: 161
		[Tooltip("Apply sharpening to the image during upscaling.")]
		public bool Sharpening = true;

		// Token: 0x040000A2 RID: 162
		[Tooltip("Strength of the sharpening effect.")]
		[Range(0f, 1f)]
		public float sharpness = 0.5f;

		// Token: 0x040000A3 RID: 163
		[Range(0f, 1f)]
		public float antiGhosting = 0.1f;

		// Token: 0x040000A4 RID: 164
		[Header("MipMap Settings")]
		public bool autoTextureUpdate = true;

		// Token: 0x040000A5 RID: 165
		public float updateFrequency = 2f;

		// Token: 0x040000A6 RID: 166
		[Range(0f, 1f)]
		public float mipMapBiasOverride = 1f;
	}
}
