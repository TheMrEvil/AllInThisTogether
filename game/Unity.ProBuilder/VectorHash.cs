using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000061 RID: 97
	internal static class VectorHash
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0002231C File Offset: 0x0002051C
		private static int HashFloat(float f)
		{
			return (int)((ulong)(f * 1000f) % 2147483647UL);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0002232E File Offset: 0x0002052E
		public static int GetHashCode(Vector2 v)
		{
			return (27 * 29 + VectorHash.HashFloat(v.x)) * 29 + VectorHash.HashFloat(v.y);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00022350 File Offset: 0x00020550
		public static int GetHashCode(Vector3 v)
		{
			return ((27 * 29 + VectorHash.HashFloat(v.x)) * 29 + VectorHash.HashFloat(v.y)) * 29 + VectorHash.HashFloat(v.z);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00022381 File Offset: 0x00020581
		public static int GetHashCode(Vector4 v)
		{
			return (((27 * 29 + VectorHash.HashFloat(v.x)) * 29 + VectorHash.HashFloat(v.y)) * 29 + VectorHash.HashFloat(v.z)) * 29 + VectorHash.HashFloat(v.w);
		}

		// Token: 0x04000212 RID: 530
		public const float FltCompareResolution = 1000f;
	}
}
