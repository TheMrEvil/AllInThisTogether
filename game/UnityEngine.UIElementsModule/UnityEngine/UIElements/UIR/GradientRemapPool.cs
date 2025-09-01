using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032B RID: 811
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x000727D4 File Offset: 0x000709D4
		public GradientRemapPool() : base(() => new GradientRemap(), delegate(GradientRemap gradientRemap)
		{
			gradientRemap.Reset();
		}, 10000)
		{
		}

		// Token: 0x0200032C RID: 812
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A67 RID: 6759 RVA: 0x0007282C File Offset: 0x00070A2C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A68 RID: 6760 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001A69 RID: 6761 RVA: 0x00072838 File Offset: 0x00070A38
			internal GradientRemap <.ctor>b__0_0()
			{
				return new GradientRemap();
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x0007283F File Offset: 0x00070A3F
			internal void <.ctor>b__0_1(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}

			// Token: 0x04000C1F RID: 3103
			public static readonly GradientRemapPool.<>c <>9 = new GradientRemapPool.<>c();

			// Token: 0x04000C20 RID: 3104
			public static Func<GradientRemap> <>9__0_0;

			// Token: 0x04000C21 RID: 3105
			public static Action<GradientRemap> <>9__0_1;
		}
	}
}
