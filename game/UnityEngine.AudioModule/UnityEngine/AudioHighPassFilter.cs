using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EB RID: 235
		// (set) Token: 0x060000EC RID: 236
		public extern float cutoffFrequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000ED RID: 237
		// (set) Token: 0x060000EE RID: 238
		public extern float highpassResonanceQ { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00002760 File Offset: 0x00000960
		public AudioHighPassFilter()
		{
		}
	}
}
