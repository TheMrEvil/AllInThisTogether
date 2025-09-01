using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioLowPassFilter : Behaviour
	{
		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetCustomLowpassLevelCurveCopy();

		// Token: 0x060000E3 RID: 227
		[NativeMethod(Name = "AudioLowPassFilterBindings::SetCustomLowpassLevelCurveHelper", IsFreeFunction = true)]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomLowpassLevelCurveHelper([NotNull("NullExceptionObject")] AudioLowPassFilter source, AnimationCurve curve);

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002A80 File Offset: 0x00000C80
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002A98 File Offset: 0x00000C98
		public AnimationCurve customCutoffCurve
		{
			get
			{
				return this.GetCustomLowpassLevelCurveCopy();
			}
			set
			{
				AudioLowPassFilter.SetCustomLowpassLevelCurveHelper(this, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		public extern float cutoffFrequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E9 RID: 233
		public extern float lowpassResonanceQ { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000EA RID: 234 RVA: 0x00002760 File Offset: 0x00000960
		public AudioLowPassFilter()
		{
		}
	}
}
