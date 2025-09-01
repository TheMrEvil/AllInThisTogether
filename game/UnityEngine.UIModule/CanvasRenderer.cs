using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UI/CanvasRenderer.h")]
	[NativeClass("UI::CanvasRenderer")]
	public sealed class CanvasRenderer : Component
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12
		// (set) Token: 0x0600000D RID: 13
		public extern bool hasPopInstruction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		public extern int materialCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		public extern int popMaterialCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18
		public extern int absoluteDepth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19
		public extern bool hasMoved { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20
		// (set) Token: 0x06000015 RID: 21
		public extern bool cullTransparentMesh { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22
		[NativeProperty("RectClipping", false, TargetType.Function)]
		public extern bool hasRectClipping { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23
		[NativeProperty("Depth", false, TargetType.Function)]
		public extern int relativeDepth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24
		// (set) Token: 0x06000019 RID: 25
		[NativeProperty("ShouldCull", false, TargetType.Function)]
		public extern bool cull { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002071 File Offset: 0x00000271
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002079 File Offset: 0x00000279
		[Obsolete("isMask is no longer supported.See EnableClipping for vertex clipping configuration", false)]
		public bool isMask
		{
			[CompilerGenerated]
			get
			{
				return this.<isMask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isMask>k__BackingField = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002082 File Offset: 0x00000282
		public void SetColor(Color color)
		{
			this.SetColor_Injected(ref color);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000208C File Offset: 0x0000028C
		public Color GetColor()
		{
			Color result;
			this.GetColor_Injected(out result);
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020A2 File Offset: 0x000002A2
		public void EnableRectClipping(Rect rect)
		{
			this.EnableRectClipping_Injected(ref rect);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000020C2 File Offset: 0x000002C2
		public Vector2 clippingSoftness
		{
			get
			{
				Vector2 result;
				this.get_clippingSoftness_Injected(out result);
				return result;
			}
			set
			{
				this.set_clippingSoftness_Injected(ref value);
			}
		}

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableRectClipping();

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMaterial(Material material, int index);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Material GetMaterial(int index);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPopMaterial(Material material, int index);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Material GetPopMaterial(int index);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(Texture texture);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAlphaTexture(Texture texture);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMesh(Mesh mesh);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x0600002A RID: 42 RVA: 0x000020CC File Offset: 0x000002CC
		public float GetAlpha()
		{
			return this.GetColor().a;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000020EC File Offset: 0x000002EC
		public void SetAlpha(float alpha)
		{
			Color color = this.GetColor();
			color.a = alpha;
			this.SetColor(color);
		}

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInheritedAlpha();

		// Token: 0x0600002D RID: 45 RVA: 0x00002111 File Offset: 0x00000311
		public void SetMaterial(Material material, Texture texture)
		{
			this.materialCount = Math.Max(1, this.materialCount);
			this.SetMaterial(material, 0);
			this.SetTexture(texture);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002138 File Offset: 0x00000338
		public Material GetMaterial()
		{
			return this.GetMaterial(0);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002154 File Offset: 0x00000354
		public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.SplitUIVertexStreams(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002180 File Offset: 0x00000380
		public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
			CanvasRenderer.SplitIndicesStreamsInternal(verts, indices);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000021AC File Offset: 0x000003AC
		public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.CreateUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000021D8 File Offset: 0x000003D8
		public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
		{
			CanvasRenderer.CreateUIVertexStreamInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents, indices);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000021FC File Offset: 0x000003FC
		public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents)
		{
			CanvasRenderer.AddUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002224 File Offset: 0x00000424
		public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002246 File Offset: 0x00000446
		[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
		public void SetVertices(List<UIVertex> vertices)
		{
			this.SetVertices(vertices.ToArray(), vertices.Count);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000225C File Offset: 0x0000045C
		[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
		public void SetVertices(UIVertex[] vertices, int size)
		{
			Mesh mesh = new Mesh();
			List<Vector3> list = new List<Vector3>();
			List<Color32> list2 = new List<Color32>();
			List<Vector4> list3 = new List<Vector4>();
			List<Vector4> list4 = new List<Vector4>();
			List<Vector4> list5 = new List<Vector4>();
			List<Vector4> list6 = new List<Vector4>();
			List<Vector3> list7 = new List<Vector3>();
			List<Vector4> list8 = new List<Vector4>();
			List<int> list9 = new List<int>();
			for (int i = 0; i < size; i += 4)
			{
				for (int j = 0; j < 4; j++)
				{
					list.Add(vertices[i + j].position);
					list2.Add(vertices[i + j].color);
					list3.Add(vertices[i + j].uv0);
					list4.Add(vertices[i + j].uv1);
					list5.Add(vertices[i + j].uv2);
					list6.Add(vertices[i + j].uv3);
					list7.Add(vertices[i + j].normal);
					list8.Add(vertices[i + j].tangent);
				}
				list9.Add(i);
				list9.Add(i + 1);
				list9.Add(i + 2);
				list9.Add(i + 2);
				list9.Add(i + 3);
				list9.Add(i);
			}
			mesh.SetVertices(list);
			mesh.SetColors(list2);
			mesh.SetNormals(list7);
			mesh.SetTangents(list8);
			mesh.SetUVs(0, list3);
			mesh.SetUVs(1, list4);
			mesh.SetUVs(2, list5);
			mesh.SetUVs(3, list6);
			mesh.SetIndices(list9.ToArray(), MeshTopology.Triangles, 0);
			this.SetMesh(mesh);
			Object.DestroyImmediate(mesh);
		}

		// Token: 0x06000037 RID: 55
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitIndicesStreamsInternal(object verts, object indices);

		// Token: 0x06000038 RID: 56
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitUIVertexStreamsInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents);

		// Token: 0x06000039 RID: 57
		[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUIVertexStreamInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents, object indices);

		// Token: 0x0600003A RID: 58 RVA: 0x00002451 File Offset: 0x00000651
		public CanvasRenderer()
		{
		}

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColor_Injected(ref Color color);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColor_Injected(out Color ret);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EnableRectClipping_Injected(ref Rect rect);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_clippingSoftness_Injected(out Vector2 ret);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_clippingSoftness_Injected(ref Vector2 value);

		// Token: 0x04000001 RID: 1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <isMask>k__BackingField;
	}
}
