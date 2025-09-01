using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioReverbFilter : Behaviour
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010D RID: 269
		// (set) Token: 0x0600010E RID: 270
		public extern AudioReverbPreset reverbPreset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010F RID: 271
		// (set) Token: 0x06000110 RID: 272
		public extern float dryLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000111 RID: 273
		// (set) Token: 0x06000112 RID: 274
		public extern float room { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000113 RID: 275
		// (set) Token: 0x06000114 RID: 276
		public extern float roomHF { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002AD4 File Offset: 0x00000CD4
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002A72 File Offset: 0x00000C72
		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000117 RID: 279
		// (set) Token: 0x06000118 RID: 280
		public extern float decayTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000119 RID: 281
		// (set) Token: 0x0600011A RID: 282
		public extern float decayHFRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600011B RID: 283
		// (set) Token: 0x0600011C RID: 284
		public extern float reflectionsLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600011D RID: 285
		// (set) Token: 0x0600011E RID: 286
		public extern float reflectionsDelay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011F RID: 287
		// (set) Token: 0x06000120 RID: 288
		public extern float reverbLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000121 RID: 289
		// (set) Token: 0x06000122 RID: 290
		public extern float reverbDelay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000123 RID: 291
		// (set) Token: 0x06000124 RID: 292
		public extern float diffusion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000125 RID: 293
		// (set) Token: 0x06000126 RID: 294
		public extern float density { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000127 RID: 295
		// (set) Token: 0x06000128 RID: 296
		public extern float hfReference { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000129 RID: 297
		// (set) Token: 0x0600012A RID: 298
		public extern float roomLF { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012B RID: 299
		// (set) Token: 0x0600012C RID: 300
		public extern float lfReference { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600012D RID: 301 RVA: 0x00002760 File Offset: 0x00000960
		public AudioReverbFilter()
		{
		}
	}
}
