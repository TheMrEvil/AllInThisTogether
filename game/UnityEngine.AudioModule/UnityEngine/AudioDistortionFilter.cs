using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioDistortionFilter : Behaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F0 RID: 240
		// (set) Token: 0x060000F1 RID: 241
		public extern float distortionLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x00002760 File Offset: 0x00000960
		public AudioDistortionFilter()
		{
		}
	}
}
