using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000016 RID: 22
	internal class MaterialReferenceManager
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000065CC File Offset: 0x000047CC
		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000065F9 File Offset: 0x000047F9
		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006608 File Offset: 0x00004808
		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006658 File Offset: 0x00004858
		public static void AddSpriteAsset(SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006668 File Offset: 0x00004868
		private void AddSpriteAssetInternal(SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000066B8 File Offset: 0x000048B8
		public static void AddSpriteAsset(int hashCode, SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000066C8 File Offset: 0x000048C8
		private void AddSpriteAssetInternal(int hashCode, SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006709 File Offset: 0x00004909
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006719 File Offset: 0x00004919
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000672A File Offset: 0x0000492A
		public static void AddColorGradientPreset(int hashCode, TextColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000673C File Offset: 0x0000493C
		private void AddColorGradientPreset_Internal(int hashCode, TextColorGradient spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000676C File Offset: 0x0000496C
		public bool Contains(FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006790 File Offset: 0x00004990
		public bool Contains(SpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000067B4 File Offset: 0x000049B4
		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000067D4 File Offset: 0x000049D4
		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000067F8 File Offset: 0x000049F8
		public static bool TryGetSpriteAsset(int hashCode, out SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006818 File Offset: 0x00004A18
		private bool TryGetSpriteAssetInternal(int hashCode, out SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000683C File Offset: 0x00004A3C
		public static bool TryGetColorGradientPreset(int hashCode, out TextColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000685C File Offset: 0x00004A5C
		private bool TryGetColorGradientPresetInternal(int hashCode, out TextColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006880 File Offset: 0x00004A80
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000068A0 File Offset: 0x00004AA0
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000068C2 File Offset: 0x00004AC2
		public MaterialReferenceManager()
		{
		}

		// Token: 0x04000092 RID: 146
		private static MaterialReferenceManager s_Instance;

		// Token: 0x04000093 RID: 147
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x04000094 RID: 148
		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x04000095 RID: 149
		private Dictionary<int, SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, SpriteAsset>();

		// Token: 0x04000096 RID: 150
		private Dictionary<int, TextColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TextColorGradient>();
	}
}
