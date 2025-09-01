using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace MagicFX5
{
	// Token: 0x0200000B RID: 11
	public static class MagicFX5_CoreUtils
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000328F File Offset: 0x0000148F
		public static void SetFloatPropertyBlock(this Renderer rend, int shaderNameID, float value)
		{
			rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
			MagicFX5_CoreUtils.SharedMaterialPropertyBlock.SetFloat(shaderNameID, value);
			rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000032B3 File Offset: 0x000014B3
		public static void SetColorPropertyBlock(this Renderer rend, int shaderNameID, Color value)
		{
			rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
			MagicFX5_CoreUtils.SharedMaterialPropertyBlock.SetVector(shaderNameID, value);
			rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000032DC File Offset: 0x000014DC
		public static void SetColorPropertyBlock(this Renderer rend, int shaderNameID, Color value, int materialIndex)
		{
			rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock, materialIndex);
			MagicFX5_CoreUtils.SharedMaterialPropertyBlock.SetVector(shaderNameID, value);
			rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock, materialIndex);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003307 File Offset: 0x00001507
		public static void SetVectorPropertyBlock(this Renderer rend, int shaderNameID, Vector4 value)
		{
			rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
			MagicFX5_CoreUtils.SharedMaterialPropertyBlock.SetVector(shaderNameID, value);
			rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000332B File Offset: 0x0000152B
		public static void SetMatrixPropertyBlock(this Renderer rend, int shaderNameID, Matrix4x4 value)
		{
			rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
			MagicFX5_CoreUtils.SharedMaterialPropertyBlock.SetMatrix(shaderNameID, value);
			rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000334F File Offset: 0x0000154F
		public static GraphicsFormat GetGraphicsFormatHDR()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Render))
			{
				return GraphicsFormat.B10G11R11_UFloatPack32;
			}
			return GraphicsFormat.R16G16B16A16_SFloat;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003360 File Offset: 0x00001560
		public static void AddMaterialInstance(this Renderer rend, Material newInstance)
		{
			MagicFX5_CoreUtils._materialsDummy.Clear();
			rend.GetSharedMaterials(MagicFX5_CoreUtils._materialsDummy);
			MagicFX5_CoreUtils._materialsDummy.Add(newInstance);
			rend.sharedMaterials = MagicFX5_CoreUtils._materialsDummy.ToArray();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003392 File Offset: 0x00001592
		public static void RemoveMaterialInstance(this Renderer rend, Material newInstance)
		{
			MagicFX5_CoreUtils._materialsDummy.Clear();
			rend.GetSharedMaterials(MagicFX5_CoreUtils._materialsDummy);
			MagicFX5_CoreUtils._materialsDummy.Remove(newInstance);
			rend.sharedMaterials = MagicFX5_CoreUtils._materialsDummy.ToArray();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033C5 File Offset: 0x000015C5
		// Note: this type is marked as 'beforefieldinit'.
		static MagicFX5_CoreUtils()
		{
		}

		// Token: 0x0400004D RID: 77
		public static MaterialPropertyBlock SharedMaterialPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x0400004E RID: 78
		private static List<Material> _materialsDummy = new List<Material>();
	}
}
