using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000060 RID: 96
	internal static class UvUnwrapping
	{
		// Token: 0x06000390 RID: 912 RVA: 0x000218FC File Offset: 0x0001FAFC
		internal static void SetAutoUV(ProBuilderMesh mesh, Face[] faces, bool auto)
		{
			if (auto)
			{
				UvUnwrapping.SetAutoAndAlignUnwrapParamsToUVs(mesh, from x in faces
				where x.manualUV
				select x);
				return;
			}
			foreach (Face face in faces)
			{
				face.textureGroup = -1;
				face.manualUV = true;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00021958 File Offset: 0x0001FB58
		internal static void SetAutoAndAlignUnwrapParamsToUVs(ProBuilderMesh mesh, IEnumerable<Face> facesToConvert)
		{
			Vector2[] dst = mesh.textures.ToArray<Vector2>();
			Face[] array = (facesToConvert as Face[]) ?? facesToConvert.ToArray<Face>();
			foreach (Face face in array)
			{
				face.uv = AutoUnwrapSettings.defaultAutoUnwrapSettings;
				face.elementGroup = -1;
				face.textureGroup = -1;
				face.manualUV = false;
			}
			mesh.RefreshUV(array);
			Vector2[] texturesInternal = mesh.texturesInternal;
			foreach (Face face2 in array)
			{
				UvUnwrapping.UVTransform uvtransform = UvUnwrapping.CalculateDelta(texturesInternal, face2.indexesInternal, dst, face2.indexesInternal);
				AutoUnwrapSettings uv = face2.uv;
				uv.offset = -uvtransform.translation;
				uv.rotation = uvtransform.rotation;
				uv.scale = uvtransform.scale;
				face2.uv = uv;
			}
			mesh.RefreshUV(array);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00021A40 File Offset: 0x0001FC40
		internal static AutoUnwrapSettings GetAutoUnwrapSettings(ProBuilderMesh mesh, Face face)
		{
			if (!face.manualUV)
			{
				return new AutoUnwrapSettings(face.uv);
			}
			UvUnwrapping.UVTransform uvtransform = UvUnwrapping.GetUVTransform(mesh, face);
			AutoUnwrapSettings defaultAutoUnwrapSettings = AutoUnwrapSettings.defaultAutoUnwrapSettings;
			defaultAutoUnwrapSettings.offset = uvtransform.translation;
			defaultAutoUnwrapSettings.rotation = 360f - uvtransform.rotation;
			defaultAutoUnwrapSettings.scale /= uvtransform.scale;
			return defaultAutoUnwrapSettings;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00021AA9 File Offset: 0x0001FCA9
		internal static UvUnwrapping.UVTransform GetUVTransform(ProBuilderMesh mesh, Face face)
		{
			Projection.PlanarProject(mesh.positionsInternal, face.indexesInternal, Math.Normal(mesh, face), UvUnwrapping.s_UVTransformProjectionBuffer);
			return UvUnwrapping.CalculateDelta(mesh.texturesInternal, face.indexesInternal, UvUnwrapping.s_UVTransformProjectionBuffer, null);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00021ADF File Offset: 0x0001FCDF
		private static int GetIndex(IList<int> collection, int index)
		{
			if (collection != null)
			{
				return collection[index];
			}
			return index;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		internal static UvUnwrapping.UVTransform CalculateDelta(IList<Vector2> src, IList<int> srcIndices, IList<Vector2> dst, IList<int> dstIndices)
		{
			Vector2 vector = src[UvUnwrapping.GetIndex(srcIndices, 1)] - src[UvUnwrapping.GetIndex(srcIndices, 0)];
			Vector2 vector2 = dst[UvUnwrapping.GetIndex(dstIndices, 1)] - dst[UvUnwrapping.GetIndex(dstIndices, 0)];
			float num = Vector2.Angle(vector2, vector);
			if (Vector2.Dot(Vector2.Perpendicular(vector2), vector) < 0f)
			{
				num = 360f - num;
			}
			Vector2 vector3 = (dstIndices == null) ? Bounds2D.Center(dst) : Bounds2D.Center(dst, dstIndices);
			Vector2 rotatedSize = UvUnwrapping.GetRotatedSize(dst, dstIndices, vector3, -num);
			Bounds2D bounds2D = (srcIndices == null) ? new Bounds2D(src) : new Bounds2D(src, srcIndices);
			Vector2 b = rotatedSize.DivideBy(bounds2D.size);
			Vector2 b2 = bounds2D.center * b;
			return new UvUnwrapping.UVTransform
			{
				translation = vector3 - b2,
				rotation = num,
				scale = rotatedSize.DivideBy(bounds2D.size)
			};
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		private static Vector2 GetRotatedSize(IList<Vector2> points, IList<int> indices, Vector2 center, float rotation)
		{
			int num = (indices == null) ? points.Count : indices.Count;
			Vector2 vector = points[UvUnwrapping.GetIndex(indices, 0)].RotateAroundPoint(center, rotation);
			float num2 = vector.x;
			float num3 = vector.y;
			float num4 = num2;
			float num5 = num3;
			for (int i = 1; i < num; i++)
			{
				Vector2 vector2 = points[UvUnwrapping.GetIndex(indices, i)].RotateAroundPoint(center, rotation);
				float x = vector2.x;
				float y = vector2.y;
				if (x < num2)
				{
					num2 = x;
				}
				if (x > num4)
				{
					num4 = x;
				}
				if (y < num3)
				{
					num3 = y;
				}
				if (y > num5)
				{
					num5 = y;
				}
			}
			return new Vector2(num4 - num2, num5 - num3);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00021C8B File Offset: 0x0001FE8B
		internal static void Unwrap(ProBuilderMesh mesh, Face face, Vector3 projection = default(Vector3))
		{
			Projection.PlanarProject(mesh, face, (projection != Vector3.zero) ? projection : Vector3.zero);
			UvUnwrapping.ApplyUVSettings(mesh.texturesInternal, face.distinctIndexesInternal, face.uv);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00021CC0 File Offset: 0x0001FEC0
		internal static void CopyUVs(ProBuilderMesh mesh, Face source, Face dest)
		{
			Vector2[] texturesInternal = mesh.texturesInternal;
			int[] distinctIndexesInternal = source.distinctIndexesInternal;
			int[] distinctIndexesInternal2 = dest.distinctIndexesInternal;
			for (int i = 0; i < distinctIndexesInternal.Length; i++)
			{
				texturesInternal[distinctIndexesInternal2[i]].x = texturesInternal[distinctIndexesInternal[i]].x;
				texturesInternal[distinctIndexesInternal2[i]].y = texturesInternal[distinctIndexesInternal[i]].y;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00021D28 File Offset: 0x0001FF28
		internal static void ProjectTextureGroup(ProBuilderMesh mesh, int group, AutoUnwrapSettings unwrapSettings)
		{
			Projection.PlanarProject(mesh, group, unwrapSettings);
			UvUnwrapping.s_IndexBuffer.Clear();
			foreach (Face face in mesh.facesInternal)
			{
				if (face.textureGroup == group)
				{
					UvUnwrapping.s_IndexBuffer.AddRange(face.distinctIndexesInternal);
				}
			}
			UvUnwrapping.ApplyUVSettings(mesh.texturesInternal, UvUnwrapping.s_IndexBuffer, unwrapSettings);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00021D8C File Offset: 0x0001FF8C
		private static void ApplyUVSettings(Vector2[] uvs, IList<int> indexes, AutoUnwrapSettings uvSettings)
		{
			int count = indexes.Count;
			Bounds2D bounds2D = new Bounds2D(uvs, indexes);
			switch (uvSettings.fill)
			{
			case AutoUnwrapSettings.Fill.Fit:
			{
				float num = Mathf.Max(bounds2D.size.x, bounds2D.size.y);
				UvUnwrapping.ScaleUVs(uvs, indexes, new Vector2(num, num), bounds2D);
				bounds2D.center /= num;
				break;
			}
			case AutoUnwrapSettings.Fill.Stretch:
				UvUnwrapping.ScaleUVs(uvs, indexes, bounds2D.size, bounds2D);
				bounds2D.center /= bounds2D.size;
				break;
			}
			if (uvSettings.scale.x != 1f || uvSettings.scale.y != 1f || uvSettings.rotation != 0f)
			{
				Vector2 vector = bounds2D.center * uvSettings.scale;
				Vector2 b = bounds2D.center - vector;
				Vector2 origin = vector;
				for (int i = 0; i < count; i++)
				{
					uvs[indexes[i]] -= b;
					uvs[indexes[i]] = uvs[indexes[i]].ScaleAroundPoint(origin, uvSettings.scale);
					uvs[indexes[i]] = uvs[indexes[i]].RotateAroundPoint(origin, uvSettings.rotation);
				}
			}
			if (!uvSettings.useWorldSpace && uvSettings.anchor != AutoUnwrapSettings.Anchor.None)
			{
				UvUnwrapping.ApplyUVAnchor(uvs, indexes, uvSettings.anchor);
			}
			if (uvSettings.flipU || uvSettings.flipV || uvSettings.swapUV)
			{
				for (int j = 0; j < count; j++)
				{
					float num2 = uvs[indexes[j]].x;
					float num3 = uvs[indexes[j]].y;
					if (uvSettings.flipU)
					{
						num2 = -num2;
					}
					if (uvSettings.flipV)
					{
						num3 = -num3;
					}
					if (!uvSettings.swapUV)
					{
						uvs[indexes[j]].x = num2;
						uvs[indexes[j]].y = num3;
					}
					else
					{
						uvs[indexes[j]].x = num3;
						uvs[indexes[j]].y = num2;
					}
				}
			}
			for (int k = 0; k < indexes.Count; k++)
			{
				int num4 = indexes[k];
				uvs[num4].x = uvs[num4].x - uvSettings.offset.x;
				int num5 = indexes[k];
				uvs[num5].y = uvs[num5].y - uvSettings.offset.y;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00022060 File Offset: 0x00020260
		private static void ScaleUVs(Vector2[] uvs, IList<int> indexes, Vector2 scale, Bounds2D bounds)
		{
			Vector2 vector = bounds.center;
			Vector2 vector2 = vector / scale;
			Vector2 b = vector - vector2;
			vector = vector2;
			for (int i = 0; i < indexes.Count; i++)
			{
				Vector2 vector3 = uvs[indexes[i]] - b;
				vector3.x = (vector3.x - vector.x) / scale.x + vector.x;
				vector3.y = (vector3.y - vector.y) / scale.y + vector.y;
				uvs[indexes[i]] = vector3;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00022104 File Offset: 0x00020304
		private static void ApplyUVAnchor(Vector2[] uvs, IList<int> indexes, AutoUnwrapSettings.Anchor anchor)
		{
			UvUnwrapping.s_TempVector2.x = 0f;
			UvUnwrapping.s_TempVector2.y = 0f;
			Vector2 vector = Math.SmallestVector2(uvs, indexes);
			Vector2 vector2 = Math.LargestVector2(uvs, indexes);
			if (anchor == AutoUnwrapSettings.Anchor.UpperLeft || anchor == AutoUnwrapSettings.Anchor.MiddleLeft || anchor == AutoUnwrapSettings.Anchor.LowerLeft)
			{
				UvUnwrapping.s_TempVector2.x = vector.x;
			}
			else if (anchor == AutoUnwrapSettings.Anchor.UpperRight || anchor == AutoUnwrapSettings.Anchor.MiddleRight || anchor == AutoUnwrapSettings.Anchor.LowerRight)
			{
				UvUnwrapping.s_TempVector2.x = vector2.x - 1f;
			}
			else
			{
				UvUnwrapping.s_TempVector2.x = vector.x + (vector2.x - vector.x) * 0.5f - 0.5f;
			}
			if (anchor == AutoUnwrapSettings.Anchor.UpperLeft || anchor == AutoUnwrapSettings.Anchor.UpperCenter || anchor == AutoUnwrapSettings.Anchor.UpperRight)
			{
				UvUnwrapping.s_TempVector2.y = vector2.y - 1f;
			}
			else if (anchor == AutoUnwrapSettings.Anchor.MiddleLeft || anchor == AutoUnwrapSettings.Anchor.MiddleCenter || anchor == AutoUnwrapSettings.Anchor.MiddleRight)
			{
				UvUnwrapping.s_TempVector2.y = vector.y + (vector2.y - vector.y) * 0.5f - 0.5f;
			}
			else
			{
				UvUnwrapping.s_TempVector2.y = vector.y;
			}
			int count = indexes.Count;
			for (int i = 0; i < count; i++)
			{
				int num = indexes[i];
				uvs[num].x = uvs[num].x - UvUnwrapping.s_TempVector2.x;
				int num2 = indexes[i];
				uvs[num2].y = uvs[num2].y - UvUnwrapping.s_TempVector2.y;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00022268 File Offset: 0x00020468
		internal static void UpgradeAutoUVScaleOffset(ProBuilderMesh mesh)
		{
			Vector2[] src = mesh.textures.ToArray<Vector2>();
			mesh.RefreshUV(mesh.facesInternal);
			Vector2[] texturesInternal = mesh.texturesInternal;
			foreach (Face face in mesh.facesInternal)
			{
				if (!face.manualUV)
				{
					UvUnwrapping.UVTransform uvtransform = UvUnwrapping.CalculateDelta(src, face.indexesInternal, texturesInternal, face.indexesInternal);
					AutoUnwrapSettings uv = face.uv;
					uv.offset += uvtransform.translation;
					face.uv = uv;
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000222F9 File Offset: 0x000204F9
		// Note: this type is marked as 'beforefieldinit'.
		static UvUnwrapping()
		{
		}

		// Token: 0x0400020F RID: 527
		private static List<Vector2> s_UVTransformProjectionBuffer = new List<Vector2>(8);

		// Token: 0x04000210 RID: 528
		private static Vector2 s_TempVector2 = Vector2.zero;

		// Token: 0x04000211 RID: 529
		private static readonly List<int> s_IndexBuffer = new List<int>(64);

		// Token: 0x020000A7 RID: 167
		internal struct UVTransform
		{
			// Token: 0x06000560 RID: 1376 RVA: 0x00035EC8 File Offset: 0x000340C8
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.translation.ToString("F2"),
					", ",
					this.rotation.ToString(),
					", ",
					this.scale.ToString("F2")
				});
			}

			// Token: 0x040002BF RID: 703
			public Vector2 translation;

			// Token: 0x040002C0 RID: 704
			public float rotation;

			// Token: 0x040002C1 RID: 705
			public Vector2 scale;
		}

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000561 RID: 1377 RVA: 0x00035F24 File Offset: 0x00034124
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x00035F30 File Offset: 0x00034130
			public <>c()
			{
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x00035F38 File Offset: 0x00034138
			internal bool <SetAutoUV>b__0_0(Face x)
			{
				return x.manualUV;
			}

			// Token: 0x040002C2 RID: 706
			public static readonly UvUnwrapping.<>c <>9 = new UvUnwrapping.<>c();

			// Token: 0x040002C3 RID: 707
			public static Func<Face, bool> <>9__0_0;
		}
	}
}
