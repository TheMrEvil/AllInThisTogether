using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000015 RID: 21
	internal struct MaterialReference
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00006384 File Offset: 0x00004584
		public MaterialReference(int index, FontAsset fontAsset, SpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isDefaultMaterial = (material.GetInstanceID() == fontAsset.material.GetInstanceID());
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000063E8 File Offset: 0x000045E8
		public static bool Contains(MaterialReference[] materialReferences, FontAsset fontAsset)
		{
			int instanceID = fontAsset.GetInstanceID();
			int num = 0;
			while (num < materialReferences.Length && materialReferences[num].fontAsset != null)
			{
				bool flag = materialReferences[num].fontAsset.GetInstanceID() == instanceID;
				if (flag)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000644C File Offset: 0x0000464C
		public static int AddMaterialReference(Material material, FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, out count);
			int result;
			if (flag)
			{
				result = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				bool flag2 = count >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
				}
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = fontAsset;
				materialReferences[count].spriteAsset = null;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = (instanceID == fontAsset.material.GetInstanceID());
				materialReferences[count].referenceCount = 0;
				result = count;
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000650C File Offset: 0x0000470C
		public static int AddMaterialReference(Material material, SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, out count);
			int result;
			if (flag)
			{
				result = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				bool flag2 = count >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
				}
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = materialReferences[0].fontAsset;
				materialReferences[count].spriteAsset = spriteAsset;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = true;
				materialReferences[count].referenceCount = 0;
				result = count;
			}
			return result;
		}

		// Token: 0x04000089 RID: 137
		public int index;

		// Token: 0x0400008A RID: 138
		public FontAsset fontAsset;

		// Token: 0x0400008B RID: 139
		public SpriteAsset spriteAsset;

		// Token: 0x0400008C RID: 140
		public Material material;

		// Token: 0x0400008D RID: 141
		public bool isDefaultMaterial;

		// Token: 0x0400008E RID: 142
		public bool isFallbackMaterial;

		// Token: 0x0400008F RID: 143
		public Material fallbackMaterial;

		// Token: 0x04000090 RID: 144
		public float padding;

		// Token: 0x04000091 RID: 145
		public int referenceCount;
	}
}
