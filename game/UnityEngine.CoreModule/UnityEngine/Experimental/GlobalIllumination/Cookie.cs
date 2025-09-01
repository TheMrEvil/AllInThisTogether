using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000462 RID: 1122
	public struct Cookie
	{
		// Token: 0x060027CB RID: 10187 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public static Cookie Defaults()
		{
			Cookie result;
			result.instanceID = 0;
			result.scale = 1f;
			result.sizes = new Vector2(1f, 1f);
			return result;
		}

		// Token: 0x04000EB0 RID: 3760
		public int instanceID;

		// Token: 0x04000EB1 RID: 3761
		public float scale;

		// Token: 0x04000EB2 RID: 3762
		public Vector2 sizes;
	}
}
