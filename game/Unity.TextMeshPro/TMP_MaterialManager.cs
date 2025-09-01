using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000044 RID: 68
	public static class TMP_MaterialManager
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00022AFB File Offset: 0x00020CFB
		static TMP_MaterialManager()
		{
			Canvas.willRenderCanvases += TMP_MaterialManager.OnPreRender;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00022B36 File Offset: 0x00020D36
		private static void OnPreRender()
		{
			if (TMP_MaterialManager.isFallbackListDirty)
			{
				TMP_MaterialManager.CleanupFallbackMaterials();
				TMP_MaterialManager.isFallbackListDirty = false;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00022B4C File Offset: 0x00020D4C
		public static Material GetStencilMaterial(Material baseMaterial, int stencilID)
		{
			if (!baseMaterial.HasProperty(ShaderUtilities.ID_StencilID))
			{
				Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
				return baseMaterial;
			}
			int instanceID = baseMaterial.GetInstanceID();
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				if (TMP_MaterialManager.m_materialList[i].baseMaterial.GetInstanceID() == instanceID && TMP_MaterialManager.m_materialList[i].stencilID == stencilID)
				{
					TMP_MaterialManager.m_materialList[i].count++;
					return TMP_MaterialManager.m_materialList[i].stencilMaterial;
				}
			}
			Material material = new Material(baseMaterial);
			material.hideFlags = HideFlags.HideAndDontSave;
			material.shaderKeywords = baseMaterial.shaderKeywords;
			ShaderUtilities.GetShaderPropertyIDs();
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
			maskingMaterial.baseMaterial = baseMaterial;
			maskingMaterial.stencilMaterial = material;
			maskingMaterial.stencilID = stencilID;
			maskingMaterial.count = 1;
			TMP_MaterialManager.m_materialList.Add(maskingMaterial);
			return material;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00022C50 File Offset: 0x00020E50
		public static void ReleaseStencilMaterial(Material stencilMaterial)
		{
			int instanceID = stencilMaterial.GetInstanceID();
			int i = 0;
			while (i < TMP_MaterialManager.m_materialList.Count)
			{
				if (TMP_MaterialManager.m_materialList[i].stencilMaterial.GetInstanceID() == instanceID)
				{
					if (TMP_MaterialManager.m_materialList[i].count > 1)
					{
						TMP_MaterialManager.m_materialList[i].count--;
						return;
					}
					UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
					TMP_MaterialManager.m_materialList.RemoveAt(i);
					stencilMaterial = null;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00022CE4 File Offset: 0x00020EE4
		public static Material GetBaseMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				return null;
			}
			return TMP_MaterialManager.m_materialList[num].baseMaterial;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00022D2B File Offset: 0x00020F2B
		public static Material SetStencil(Material material, int stencilID)
		{
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			if (stencilID == 0)
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 8f);
			}
			else
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			}
			return material;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00022D60 File Offset: 0x00020F60
		public static void AddMaskingMaterial(Material baseMaterial, Material stencilMaterial, int stencilID)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
				maskingMaterial.baseMaterial = baseMaterial;
				maskingMaterial.stencilMaterial = stencilMaterial;
				maskingMaterial.stencilID = stencilID;
				maskingMaterial.count = 1;
				TMP_MaterialManager.m_materialList.Add(maskingMaterial);
				return;
			}
			stencilMaterial = TMP_MaterialManager.m_materialList[num].stencilMaterial;
			TMP_MaterialManager.m_materialList[num].count++;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00022DF8 File Offset: 0x00020FF8
		public static void RemoveStencilMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num != -1)
			{
				TMP_MaterialManager.m_materialList.RemoveAt(num);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00022E38 File Offset: 0x00021038
		public static void ReleaseBaseMaterial(Material baseMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.baseMaterial == baseMaterial);
			if (num == -1)
			{
				Debug.Log("No Masking Material exists for " + baseMaterial.name);
				return;
			}
			if (TMP_MaterialManager.m_materialList[num].count > 1)
			{
				TMP_MaterialManager.m_materialList[num].count--;
				Debug.Log(string.Concat(new string[]
				{
					"Removed (1) reference to ",
					TMP_MaterialManager.m_materialList[num].stencilMaterial.name,
					". There are ",
					TMP_MaterialManager.m_materialList[num].count.ToString(),
					" references left."
				}));
				return;
			}
			Debug.Log("Removed last reference to " + TMP_MaterialManager.m_materialList[num].stencilMaterial.name + " with ID " + TMP_MaterialManager.m_materialList[num].stencilMaterial.GetInstanceID().ToString());
			UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[num].stencilMaterial);
			TMP_MaterialManager.m_materialList.RemoveAt(num);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00022F74 File Offset: 0x00021174
		public static void ClearMaterials()
		{
			if (TMP_MaterialManager.m_materialList.Count == 0)
			{
				Debug.Log("Material List has already been cleared.");
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
			}
			TMP_MaterialManager.m_materialList.Clear();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00022FCC File Offset: 0x000211CC
		public static int GetStencilID(GameObject obj)
		{
			int num = 0;
			Transform transform = obj.transform;
			Transform y = TMP_MaterialManager.FindRootSortOverrideCanvas(transform);
			if (transform == y)
			{
				return num;
			}
			Transform parent = transform.parent;
			List<Mask> list = TMP_ListPool<Mask>.Get();
			while (parent != null)
			{
				parent.GetComponents<Mask>(list);
				for (int i = 0; i < list.Count; i++)
				{
					Mask mask = list[i];
					if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
					{
						num++;
						break;
					}
				}
				if (parent == y)
				{
					break;
				}
				parent = parent.parent;
			}
			TMP_ListPool<Mask>.Release(list);
			return Mathf.Min((1 << num) - 1, 255);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00023088 File Offset: 0x00021288
		public static Material GetMaterialForRendering(MaskableGraphic graphic, Material baseMaterial)
		{
			if (baseMaterial == null)
			{
				return null;
			}
			List<IMaterialModifier> list = TMP_ListPool<IMaterialModifier>.Get();
			graphic.GetComponents<IMaterialModifier>(list);
			Material material = baseMaterial;
			for (int i = 0; i < list.Count; i++)
			{
				material = list[i].GetModifiedMaterial(material);
			}
			TMP_ListPool<IMaterialModifier>.Release(list);
			return material;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000230D8 File Offset: 0x000212D8
		private static Transform FindRootSortOverrideCanvas(Transform start)
		{
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			start.GetComponentsInParent<Canvas>(false, list);
			Canvas canvas = null;
			for (int i = 0; i < list.Count; i++)
			{
				canvas = list[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00023130 File Offset: 0x00021330
		internal static Material GetFallbackMaterial(TMP_FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture texture = fontAsset.atlasTextures[atlasIndex];
			int instanceID2 = texture.GetInstanceID();
			long num = instanceID << 32 | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				Material material = new Material(sourceMaterial);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture);
				material.hideFlags = HideFlags.HideAndDontSave;
				fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
				fallbackMaterial.fallbackID = num;
				fallbackMaterial.sourceMaterial = fontAsset.material;
				fallbackMaterial.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallbackMaterial.fallbackMaterial = material;
				fallbackMaterial.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
				return material;
			}
			int num2 = sourceMaterial.ComputeCRC();
			if (num2 == fallbackMaterial.sourceMaterialCRC)
			{
				return fallbackMaterial.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial.fallbackMaterial);
			fallbackMaterial.sourceMaterialCRC = num2;
			return fallbackMaterial.fallbackMaterial;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00023210 File Offset: 0x00021410
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = instanceID << 32 | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				Material material;
				if (sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					material = new Material(sourceMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
					material.SetTexture(ShaderUtilities.ID_MainTex, texture);
					material.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
					material.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
					material.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
					material.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
					material.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
				}
				else
				{
					material = new Material(targetMaterial);
				}
				fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
				fallbackMaterial.fallbackID = num;
				fallbackMaterial.sourceMaterial = sourceMaterial;
				fallbackMaterial.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallbackMaterial.fallbackMaterial = material;
				fallbackMaterial.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
				return material;
			}
			int num2 = sourceMaterial.ComputeCRC();
			if (num2 == fallbackMaterial.sourceMaterialCRC)
			{
				return fallbackMaterial.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial.fallbackMaterial);
			fallbackMaterial.sourceMaterialCRC = num2;
			return fallbackMaterial.fallbackMaterial;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0002338C File Offset: 0x0002158C
		public static void AddFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallbackMaterial))
			{
				fallbackMaterial.count++;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000233D8 File Offset: 0x000215D8
		public static void RemoveFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallbackMaterial))
			{
				fallbackMaterial.count--;
				if (fallbackMaterial.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial);
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00023438 File Offset: 0x00021638
		public static void CleanupFallbackMaterials()
		{
			if (TMP_MaterialManager.m_fallbackCleanupList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_fallbackCleanupList.Count; i++)
			{
				TMP_MaterialManager.FallbackMaterial fallbackMaterial = TMP_MaterialManager.m_fallbackCleanupList[i];
				if (fallbackMaterial.count < 1)
				{
					Material fallbackMaterial2 = fallbackMaterial.fallbackMaterial;
					TMP_MaterialManager.m_fallbackMaterials.Remove(fallbackMaterial.fallbackID);
					TMP_MaterialManager.m_fallbackMaterialLookup.Remove(fallbackMaterial2.GetInstanceID());
					UnityEngine.Object.DestroyImmediate(fallbackMaterial2);
				}
			}
			TMP_MaterialManager.m_fallbackCleanupList.Clear();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000234B8 File Offset: 0x000216B8
		public static void ReleaseFallbackMaterial(Material fallbackMaterial)
		{
			if (fallbackMaterial == null)
			{
				return;
			}
			int instanceID = fallbackMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial2;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallbackMaterial2))
			{
				fallbackMaterial2.count--;
				if (fallbackMaterial2.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial2);
				}
			}
			TMP_MaterialManager.isFallbackListDirty = true;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0002351C File Offset: 0x0002171C
		public static void CopyMaterialPresetProperties(Material source, Material destination)
		{
			if (!source.HasProperty(ShaderUtilities.ID_GradientScale) || !destination.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				return;
			}
			Texture texture = destination.GetTexture(ShaderUtilities.ID_MainTex);
			float @float = destination.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = destination.GetFloat(ShaderUtilities.ID_TextureWidth);
			float float3 = destination.GetFloat(ShaderUtilities.ID_TextureHeight);
			float float4 = destination.GetFloat(ShaderUtilities.ID_WeightNormal);
			float float5 = destination.GetFloat(ShaderUtilities.ID_WeightBold);
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(ShaderUtilities.ID_MainTex, texture);
			destination.SetFloat(ShaderUtilities.ID_GradientScale, @float);
			destination.SetFloat(ShaderUtilities.ID_TextureWidth, float2);
			destination.SetFloat(ShaderUtilities.ID_TextureHeight, float3);
			destination.SetFloat(ShaderUtilities.ID_WeightNormal, float4);
			destination.SetFloat(ShaderUtilities.ID_WeightBold, float5);
		}

		// Token: 0x04000274 RID: 628
		private static List<TMP_MaterialManager.MaskingMaterial> m_materialList = new List<TMP_MaterialManager.MaskingMaterial>();

		// Token: 0x04000275 RID: 629
		private static Dictionary<long, TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials = new Dictionary<long, TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x04000276 RID: 630
		private static Dictionary<int, long> m_fallbackMaterialLookup = new Dictionary<int, long>();

		// Token: 0x04000277 RID: 631
		private static List<TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList = new List<TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x04000278 RID: 632
		private static bool isFallbackListDirty;

		// Token: 0x02000098 RID: 152
		private class FallbackMaterial
		{
			// Token: 0x0600062E RID: 1582 RVA: 0x00038A8B File Offset: 0x00036C8B
			public FallbackMaterial()
			{
			}

			// Token: 0x040005D6 RID: 1494
			public long fallbackID;

			// Token: 0x040005D7 RID: 1495
			public Material sourceMaterial;

			// Token: 0x040005D8 RID: 1496
			internal int sourceMaterialCRC;

			// Token: 0x040005D9 RID: 1497
			public Material fallbackMaterial;

			// Token: 0x040005DA RID: 1498
			public int count;
		}

		// Token: 0x02000099 RID: 153
		private class MaskingMaterial
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x00038A93 File Offset: 0x00036C93
			public MaskingMaterial()
			{
			}

			// Token: 0x040005DB RID: 1499
			public Material baseMaterial;

			// Token: 0x040005DC RID: 1500
			public Material stencilMaterial;

			// Token: 0x040005DD RID: 1501
			public int count;

			// Token: 0x040005DE RID: 1502
			public int stencilID;
		}

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000630 RID: 1584 RVA: 0x00038A9B File Offset: 0x00036C9B
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x00038AA3 File Offset: 0x00036CA3
			internal bool <GetBaseMaterial>b__0(TMP_MaterialManager.MaskingMaterial item)
			{
				return item.stencilMaterial == this.stencilMaterial;
			}

			// Token: 0x040005DF RID: 1503
			public Material stencilMaterial;
		}

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06000632 RID: 1586 RVA: 0x00038AB6 File Offset: 0x00036CB6
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x00038ABE File Offset: 0x00036CBE
			internal bool <AddMaskingMaterial>b__0(TMP_MaterialManager.MaskingMaterial item)
			{
				return item.stencilMaterial == this.stencilMaterial;
			}

			// Token: 0x040005E0 RID: 1504
			public Material stencilMaterial;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06000634 RID: 1588 RVA: 0x00038AD1 File Offset: 0x00036CD1
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x06000635 RID: 1589 RVA: 0x00038AD9 File Offset: 0x00036CD9
			internal bool <RemoveStencilMaterial>b__0(TMP_MaterialManager.MaskingMaterial item)
			{
				return item.stencilMaterial == this.stencilMaterial;
			}

			// Token: 0x040005E1 RID: 1505
			public Material stencilMaterial;
		}

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06000636 RID: 1590 RVA: 0x00038AEC File Offset: 0x00036CEC
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x00038AF4 File Offset: 0x00036CF4
			internal bool <ReleaseBaseMaterial>b__0(TMP_MaterialManager.MaskingMaterial item)
			{
				return item.baseMaterial == this.baseMaterial;
			}

			// Token: 0x040005E2 RID: 1506
			public Material baseMaterial;
		}
	}
}
