using System;

namespace UnityEngine.Sprites
{
	// Token: 0x0200026E RID: 622
	public sealed class DataUtility
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x0002B868 File Offset: 0x00029A68
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			return sprite.GetInnerUVs();
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0002B880 File Offset: 0x00029A80
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			return sprite.GetOuterUVs();
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0002B898 File Offset: 0x00029A98
		public static Vector4 GetPadding(Sprite sprite)
		{
			return sprite.GetPadding();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 result;
			result.x = sprite.border.x + sprite.border.z;
			result.y = sprite.border.y + sprite.border.w;
			return result;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00002072 File Offset: 0x00000272
		public DataUtility()
		{
		}
	}
}
