using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000E0 RID: 224
	public static class MeshUtility
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00020590 File Offset: 0x0001E790
		public static Mesh GetSharedMesh(Renderer ren)
		{
			if (ren == null)
			{
				return null;
			}
			if (ren is SkinnedMeshRenderer)
			{
				return (ren as SkinnedMeshRenderer).sharedMesh;
			}
			MeshFilter component = ren.GetComponent<MeshFilter>();
			if (component == null)
			{
				Debug.LogError("Not found MeshFilter!");
				return null;
			}
			return component.sharedMesh;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000205E0 File Offset: 0x0001E7E0
		public static bool SetMesh(Renderer ren, Mesh mesh, Transform[] skinBones = null)
		{
			if (ren is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
				skinnedMeshRenderer.sharedMesh = mesh;
				if (skinBones != null && skinBones.Length != 0)
				{
					skinnedMeshRenderer.bones = skinBones;
				}
			}
			else
			{
				MeshFilter component = ren.GetComponent<MeshFilter>();
				if (component == null)
				{
					Debug.LogError("Not found MeshFilter!");
					return false;
				}
				component.mesh = mesh;
			}
			return true;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00020638 File Offset: 0x0001E838
		public static int GetTransformCount(Renderer ren)
		{
			int num = 0;
			if (ren)
			{
				num++;
				if (ren is SkinnedMeshRenderer)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
					num++;
					int num2 = num;
					Transform[] bones = skinnedMeshRenderer.bones;
					num = num2 + ((bones != null) ? bones.Length : 0);
				}
			}
			return num;
		}
	}
}
