using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000007 RID: 7
	public class MaterialReferenceManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002319 File Offset: 0x00000519
		public static MaterialReferenceManager instance
		{
			get
			{
				if (MaterialReferenceManager.s_Instance == null)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002331 File Offset: 0x00000531
		public static void AddFontAsset(TMP_FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000233E File Offset: 0x0000053E
		private void AddFontAssetInternal(TMP_FontAsset fontAsset)
		{
			if (this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode))
			{
				return;
			}
			this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
			this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000237D File Offset: 0x0000057D
		public static void AddSpriteAsset(TMP_SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000238A File Offset: 0x0000058A
		private void AddSpriteAssetInternal(TMP_SpriteAsset spriteAsset)
		{
			if (this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode))
			{
				return;
			}
			this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
			this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C9 File Offset: 0x000005C9
		public static void AddSpriteAsset(int hashCode, TMP_SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023D7 File Offset: 0x000005D7
		private void AddSpriteAssetInternal(int hashCode, TMP_SpriteAsset spriteAsset)
		{
			if (this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode))
			{
				return;
			}
			this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
			this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
			if (spriteAsset.hashCode == 0)
			{
				spriteAsset.hashCode = hashCode;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002416 File Offset: 0x00000616
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002424 File Offset: 0x00000624
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002433 File Offset: 0x00000633
		public static void AddColorGradientPreset(int hashCode, TMP_ColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002441 File Offset: 0x00000641
		private void AddColorGradientPreset_Internal(int hashCode, TMP_ColorGradient spriteAsset)
		{
			if (this.m_ColorGradientReferenceLookup.ContainsKey(hashCode))
			{
				return;
			}
			this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000245F File Offset: 0x0000065F
		public bool Contains(TMP_FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002472 File Offset: 0x00000672
		public bool Contains(TMP_SpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002485 File Offset: 0x00000685
		public static bool TryGetFontAsset(int hashCode, out TMP_FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002493 File Offset: 0x00000693
		private bool TryGetFontAssetInternal(int hashCode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A5 File Offset: 0x000006A5
		public static bool TryGetSpriteAsset(int hashCode, out TMP_SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B3 File Offset: 0x000006B3
		private bool TryGetSpriteAssetInternal(int hashCode, out TMP_SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024C5 File Offset: 0x000006C5
		public static bool TryGetColorGradientPreset(int hashCode, out TMP_ColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D3 File Offset: 0x000006D3
		private bool TryGetColorGradientPresetInternal(int hashCode, out TMP_ColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E5 File Offset: 0x000006E5
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024F3 File Offset: 0x000006F3
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002505 File Offset: 0x00000705
		public MaterialReferenceManager()
		{
		}

		// Token: 0x04000009 RID: 9
		private static MaterialReferenceManager s_Instance;

		// Token: 0x0400000A RID: 10
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x0400000B RID: 11
		private Dictionary<int, TMP_FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, TMP_FontAsset>();

		// Token: 0x0400000C RID: 12
		private Dictionary<int, TMP_SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, TMP_SpriteAsset>();

		// Token: 0x0400000D RID: 13
		private Dictionary<int, TMP_ColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TMP_ColorGradient>();
	}
}
