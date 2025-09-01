using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioEchoFilter : Behaviour
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F3 RID: 243
		// (set) Token: 0x060000F4 RID: 244
		public extern float delay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F5 RID: 245
		// (set) Token: 0x060000F6 RID: 246
		public extern float decayRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F7 RID: 247
		// (set) Token: 0x060000F8 RID: 248
		public extern float dryMix { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		public extern float wetMix { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000FB RID: 251 RVA: 0x00002760 File Offset: 0x00000960
		public AudioEchoFilter()
		{
		}
	}
}
