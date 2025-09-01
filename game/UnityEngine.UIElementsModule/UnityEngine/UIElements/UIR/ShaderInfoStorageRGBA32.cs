using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031D RID: 797
	internal class ShaderInfoStorageRGBA32 : ShaderInfoStorage<Color32>
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x0006EC3F File Offset: 0x0006CE3F
		public ShaderInfoStorageRGBA32(int initialSize = 64, int maxSize = 4096) : base(TextureFormat.RGBA32, ShaderInfoStorageRGBA32.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0006EC51 File Offset: 0x0006CE51
		// Note: this type is marked as 'beforefieldinit'.
		static ShaderInfoStorageRGBA32()
		{
		}

		// Token: 0x04000BE3 RID: 3043
		private static readonly Func<Color, Color32> s_Convert = (Color c) => c;

		// Token: 0x0200031E RID: 798
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A21 RID: 6689 RVA: 0x0006EC68 File Offset: 0x0006CE68
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A22 RID: 6690 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001A23 RID: 6691 RVA: 0x0006EC74 File Offset: 0x0006CE74
			internal Color32 <.cctor>b__2_0(Color c)
			{
				return c;
			}

			// Token: 0x04000BE4 RID: 3044
			public static readonly ShaderInfoStorageRGBA32.<>c <>9 = new ShaderInfoStorageRGBA32.<>c();
		}
	}
}
