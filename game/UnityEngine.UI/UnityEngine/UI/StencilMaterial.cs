using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000038 RID: 56
	public static class StencilMaterial
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x000148A9 File Offset: 0x00012AA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Material.Add instead.", true)]
		public static Material Add(Material baseMat, int stencilID)
		{
			return null;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000148AC File Offset: 0x00012AAC
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask)
		{
			return StencilMaterial.Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 255, 255);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000148C3 File Offset: 0x00012AC3
		private static void LogWarningWhenNotInBatchmode(string warning, Object context)
		{
			if (!Application.isBatchMode)
			{
				Debug.LogWarning(warning, context);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000148D4 File Offset: 0x00012AD4
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask)
		{
			if ((stencilID <= 0 && colorWriteMask == ColorWriteMask.All) || baseMat == null)
			{
				return baseMat;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilOp"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilComp"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilWriteMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_ColorMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
				return baseMat;
			}
			int count = StencilMaterial.m_List.Count;
			for (int i = 0; i < count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilId == stencilID && matEntry.operation == operation && matEntry.compareFunction == compareFunction && matEntry.readMask == readMask && matEntry.writeMask == writeMask && matEntry.colorMask == colorWriteMask)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			StencilMaterial.MatEntry matEntry2 = new StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new Material(baseMat);
			matEntry2.customMat.hideFlags = HideFlags.HideAndDontSave;
			matEntry2.stencilId = stencilID;
			matEntry2.operation = operation;
			matEntry2.compareFunction = compareFunction;
			matEntry2.readMask = readMask;
			matEntry2.writeMask = writeMask;
			matEntry2.colorMask = colorWriteMask;
			matEntry2.useAlphaClip = (operation != StencilOp.Keep && writeMask > 0);
			matEntry2.customMat.name = string.Format("Stencil Id:{0}, Op:{1}, Comp:{2}, WriteMask:{3}, ReadMask:{4}, ColorMask:{5} AlphaClip:{6} ({7})", new object[]
			{
				stencilID,
				operation,
				compareFunction,
				writeMask,
				readMask,
				colorWriteMask,
				matEntry2.useAlphaClip,
				baseMat.name
			});
			matEntry2.customMat.SetFloat("_Stencil", (float)stencilID);
			matEntry2.customMat.SetFloat("_StencilOp", (float)operation);
			matEntry2.customMat.SetFloat("_StencilComp", (float)compareFunction);
			matEntry2.customMat.SetFloat("_StencilReadMask", (float)readMask);
			matEntry2.customMat.SetFloat("_StencilWriteMask", (float)writeMask);
			matEntry2.customMat.SetFloat("_ColorMask", (float)colorWriteMask);
			matEntry2.customMat.SetFloat("_UseUIAlphaClip", matEntry2.useAlphaClip ? 1f : 0f);
			if (matEntry2.useAlphaClip)
			{
				matEntry2.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
			}
			else
			{
				matEntry2.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
			}
			StencilMaterial.m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014C18 File Offset: 0x00012E18
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			int count = StencilMaterial.m_List.Count;
			for (int i = 0; i < count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					StencilMaterial.MatEntry matEntry2 = matEntry;
					int num = matEntry2.count - 1;
					matEntry2.count = num;
					if (num == 0)
					{
						Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014C94 File Offset: 0x00012E94
		public static void ClearAll()
		{
			int count = StencilMaterial.m_List.Count;
			for (int i = 0; i < count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				Misc.DestroyImmediate(matEntry.customMat);
				matEntry.baseMat = null;
			}
			StencilMaterial.m_List.Clear();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014CDE File Offset: 0x00012EDE
		// Note: this type is marked as 'beforefieldinit'.
		static StencilMaterial()
		{
		}

		// Token: 0x0400016E RID: 366
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x020000AE RID: 174
		private class MatEntry
		{
			// Token: 0x060006EE RID: 1774 RVA: 0x0001C26D File Offset: 0x0001A46D
			public MatEntry()
			{
			}

			// Token: 0x04000308 RID: 776
			public Material baseMat;

			// Token: 0x04000309 RID: 777
			public Material customMat;

			// Token: 0x0400030A RID: 778
			public int count;

			// Token: 0x0400030B RID: 779
			public int stencilId;

			// Token: 0x0400030C RID: 780
			public StencilOp operation;

			// Token: 0x0400030D RID: 781
			public CompareFunction compareFunction = CompareFunction.Always;

			// Token: 0x0400030E RID: 782
			public int readMask;

			// Token: 0x0400030F RID: 783
			public int writeMask;

			// Token: 0x04000310 RID: 784
			public bool useAlphaClip;

			// Token: 0x04000311 RID: 785
			public ColorWriteMask colorMask;
		}
	}
}
