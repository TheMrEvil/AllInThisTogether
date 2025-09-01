using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000328 RID: 808
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x00072738 File Offset: 0x00070938
		public VectorImageRenderInfoPool() : base(() => new VectorImageRenderInfo(), delegate(VectorImageRenderInfo vectorImageInfo)
		{
			vectorImageInfo.Reset();
		}, 10000)
		{
		}

		// Token: 0x02000329 RID: 809
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A60 RID: 6752 RVA: 0x00072790 File Offset: 0x00070990
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A61 RID: 6753 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001A62 RID: 6754 RVA: 0x0007279C File Offset: 0x0007099C
			internal VectorImageRenderInfo <.ctor>b__0_0()
			{
				return new VectorImageRenderInfo();
			}

			// Token: 0x06001A63 RID: 6755 RVA: 0x000727A3 File Offset: 0x000709A3
			internal void <.ctor>b__0_1(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}

			// Token: 0x04000C19 RID: 3097
			public static readonly VectorImageRenderInfoPool.<>c <>9 = new VectorImageRenderInfoPool.<>c();

			// Token: 0x04000C1A RID: 3098
			public static Func<VectorImageRenderInfo> <>9__0_0;

			// Token: 0x04000C1B RID: 3099
			public static Action<VectorImageRenderInfo> <>9__0_1;
		}
	}
}
