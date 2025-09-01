using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioChorusFilter : Behaviour
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FC RID: 252
		// (set) Token: 0x060000FD RID: 253
		public extern float dryMix { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000FE RID: 254
		// (set) Token: 0x060000FF RID: 255
		public extern float wetMix1 { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000100 RID: 256
		// (set) Token: 0x06000101 RID: 257
		public extern float wetMix2 { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000102 RID: 258
		// (set) Token: 0x06000103 RID: 259
		public extern float wetMix3 { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000104 RID: 260
		// (set) Token: 0x06000105 RID: 261
		public extern float delay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000106 RID: 262
		// (set) Token: 0x06000107 RID: 263
		public extern float rate { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000108 RID: 264
		// (set) Token: 0x06000109 RID: 265
		public extern float depth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002AA4 File Offset: 0x00000CA4
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002AC6 File Offset: 0x00000CC6
		[Obsolete("Warning! Feedback is deprecated. This property does nothing.")]
		public float feedback
		{
			get
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
				return 0f;
			}
			set
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002760 File Offset: 0x00000960
		public AudioChorusFilter()
		{
		}
	}
}
