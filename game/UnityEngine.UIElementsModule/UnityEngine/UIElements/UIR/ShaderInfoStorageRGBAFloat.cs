using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031F RID: 799
	internal class ShaderInfoStorageRGBAFloat : ShaderInfoStorage<Color>
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0006EC7C File Offset: 0x0006CE7C
		public ShaderInfoStorageRGBAFloat(int initialSize = 64, int maxSize = 4096) : base(TextureFormat.RGBAFloat, ShaderInfoStorageRGBAFloat.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006EC8F File Offset: 0x0006CE8F
		// Note: this type is marked as 'beforefieldinit'.
		static ShaderInfoStorageRGBAFloat()
		{
		}

		// Token: 0x04000BE5 RID: 3045
		private static readonly Func<Color, Color> s_Convert = (Color c) => c;

		// Token: 0x02000320 RID: 800
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A26 RID: 6694 RVA: 0x0006ECA6 File Offset: 0x0006CEA6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A27 RID: 6695 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001A28 RID: 6696 RVA: 0x0000A501 File Offset: 0x00008701
			internal Color <.cctor>b__2_0(Color c)
			{
				return c;
			}

			// Token: 0x04000BE6 RID: 3046
			public static readonly ShaderInfoStorageRGBAFloat.<>c <>9 = new ShaderInfoStorageRGBAFloat.<>c();
		}
	}
}
