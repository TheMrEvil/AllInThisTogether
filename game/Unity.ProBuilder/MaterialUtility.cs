using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000026 RID: 38
	internal static class MaterialUtility
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000118A3 File Offset: 0x0000FAA3
		internal static int GetMaterialCount(Renderer renderer)
		{
			MaterialUtility.s_MaterialArray.Clear();
			renderer.GetSharedMaterials(MaterialUtility.s_MaterialArray);
			return MaterialUtility.s_MaterialArray.Count;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000118C4 File Offset: 0x0000FAC4
		internal static Material GetSharedMaterial(Renderer renderer, int index)
		{
			MaterialUtility.s_MaterialArray.Clear();
			renderer.GetSharedMaterials(MaterialUtility.s_MaterialArray);
			int count = MaterialUtility.s_MaterialArray.Count;
			if (count < 1)
			{
				return null;
			}
			return MaterialUtility.s_MaterialArray[Math.Clamp(index, 0, count - 1)];
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0001190B File Offset: 0x0000FB0B
		// Note: this type is marked as 'beforefieldinit'.
		static MaterialUtility()
		{
		}

		// Token: 0x0400007B RID: 123
		internal static List<Material> s_MaterialArray = new List<Material>();
	}
}
