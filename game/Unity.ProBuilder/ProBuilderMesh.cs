using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000043 RID: 67
	[AddComponentMenu("//ProBuilder MeshFilter")]
	[RequireComponent(typeof(MeshRenderer))]
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	public sealed class ProBuilderMesh : MonoBehaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00016B83 File Offset: 0x00014D83
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00016B8B File Offset: 0x00014D8B
		public bool userCollisions
		{
			[CompilerGenerated]
			get
			{
				return this.<userCollisions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<userCollisions>k__BackingField = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00016B94 File Offset: 0x00014D94
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00016B9C File Offset: 0x00014D9C
		public UnwrapParameters unwrapParameters
		{
			get
			{
				return this.m_UnwrapParameters;
			}
			set
			{
				this.m_UnwrapParameters = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00016BA5 File Offset: 0x00014DA5
		internal MeshRenderer renderer
		{
			get
			{
				if (!base.gameObject.TryGetComponent<MeshRenderer>(out this.m_MeshRenderer))
				{
					return null;
				}
				return this.m_MeshRenderer;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00016BC2 File Offset: 0x00014DC2
		internal MeshFilter filter
		{
			get
			{
				if (this.m_MeshFilter == null && !base.gameObject.TryGetComponent<MeshFilter>(out this.m_MeshFilter))
				{
					return null;
				}
				return this.m_MeshFilter;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00016BED File Offset: 0x00014DED
		internal ushort versionIndex
		{
			get
			{
				return this.m_VersionIndex;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00016BF5 File Offset: 0x00014DF5
		internal ushort nonSerializedVersionIndex
		{
			get
			{
				return this.m_InstanceVersionIndex;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00016BFD File Offset: 0x00014DFD
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00016C05 File Offset: 0x00014E05
		public bool preserveMeshAssetOnDestroy
		{
			get
			{
				return this.m_PreserveMeshAssetOnDestroy;
			}
			set
			{
				this.m_PreserveMeshAssetOnDestroy = value;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00016C10 File Offset: 0x00014E10
		public bool HasArrays(MeshArrays channels)
		{
			bool flag = false;
			int vertexCount = this.vertexCount;
			flag |= ((channels & MeshArrays.Position) == MeshArrays.Position && this.m_Positions == null);
			flag |= ((channels & MeshArrays.Normal) == MeshArrays.Normal && (this.m_Normals == null || this.m_Normals.Length != vertexCount));
			flag |= ((channels & MeshArrays.Texture0) == MeshArrays.Texture0 && (this.m_Textures0 == null || this.m_Textures0.Length != vertexCount));
			flag |= ((channels & MeshArrays.Texture2) == MeshArrays.Texture2 && (this.m_Textures2 == null || this.m_Textures2.Count != vertexCount));
			flag |= ((channels & MeshArrays.Texture3) == MeshArrays.Texture3 && (this.m_Textures3 == null || this.m_Textures3.Count != vertexCount));
			flag |= ((channels & MeshArrays.Color) == MeshArrays.Color && (this.m_Colors == null || this.m_Colors.Length != vertexCount));
			flag |= ((channels & MeshArrays.Tangent) == MeshArrays.Tangent && (this.m_Tangents == null || this.m_Tangents.Length != vertexCount));
			if ((channels & MeshArrays.Texture1) == MeshArrays.Texture1 && this.mesh != null)
			{
				flag |= !this.mesh.HasVertexAttribute(VertexAttribute.TexCoord1);
			}
			return !flag;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00016D57 File Offset: 0x00014F57
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00016D5F File Offset: 0x00014F5F
		internal Face[] facesInternal
		{
			get
			{
				return this.m_Faces;
			}
			set
			{
				this.m_Faces = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00016D68 File Offset: 0x00014F68
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00016D75 File Offset: 0x00014F75
		public IList<Face> faces
		{
			get
			{
				return new ReadOnlyCollection<Face>(this.m_Faces);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Faces = value.ToArray<Face>();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00016D91 File Offset: 0x00014F91
		internal void InvalidateSharedVertexLookup()
		{
			if (this.m_SharedVertexLookup == null)
			{
				this.m_SharedVertexLookup = new Dictionary<int, int>();
			}
			this.m_SharedVertexLookup.Clear();
			this.m_CacheValid &= ~ProBuilderMesh.CacheValidState.SharedVertex;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00016DC3 File Offset: 0x00014FC3
		internal void InvalidateSharedTextureLookup()
		{
			if (this.m_SharedTextureLookup == null)
			{
				this.m_SharedTextureLookup = new Dictionary<int, int>();
			}
			this.m_SharedTextureLookup.Clear();
			this.m_CacheValid &= ~ProBuilderMesh.CacheValidState.SharedTexture;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00016DF8 File Offset: 0x00014FF8
		internal void InvalidateFaces()
		{
			if (this.m_Faces == null)
			{
				this.m_Faces = new Face[0];
				return;
			}
			foreach (Face face in this.faces)
			{
				face.InvalidateCache();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00016E58 File Offset: 0x00015058
		internal void InvalidateCaches()
		{
			this.InvalidateSharedVertexLookup();
			this.InvalidateSharedTextureLookup();
			this.InvalidateFaces();
			this.m_SelectedCacheDirty = true;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00016E73 File Offset: 0x00015073
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00016E7B File Offset: 0x0001507B
		internal SharedVertex[] sharedVerticesInternal
		{
			get
			{
				return this.m_SharedVertices;
			}
			set
			{
				this.m_SharedVertices = value;
				this.InvalidateSharedVertexLookup();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00016E8A File Offset: 0x0001508A
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00016E98 File Offset: 0x00015098
		public IList<SharedVertex> sharedVertices
		{
			get
			{
				return new ReadOnlyCollection<SharedVertex>(this.m_SharedVertices);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int count = value.Count;
				this.m_SharedVertices = new SharedVertex[count];
				for (int i = 0; i < count; i++)
				{
					this.m_SharedVertices[i] = new SharedVertex(value[i]);
				}
				this.InvalidateSharedVertexLookup();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00016EEC File Offset: 0x000150EC
		internal Dictionary<int, int> sharedVertexLookup
		{
			get
			{
				if ((this.m_CacheValid & ProBuilderMesh.CacheValidState.SharedVertex) != ProBuilderMesh.CacheValidState.SharedVertex)
				{
					if (this.m_SharedVertexLookup == null)
					{
						this.m_SharedVertexLookup = new Dictionary<int, int>();
					}
					SharedVertex.GetSharedVertexLookup(this.m_SharedVertices, this.m_SharedVertexLookup);
					this.m_CacheValid |= ProBuilderMesh.CacheValidState.SharedVertex;
				}
				return this.m_SharedVertexLookup;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00016F3C File Offset: 0x0001513C
		internal void SetSharedVertices(IEnumerable<KeyValuePair<int, int>> indexes)
		{
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			this.m_SharedVertices = SharedVertex.ToSharedVertices(indexes);
			this.InvalidateSharedVertexLookup();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00016F5E File Offset: 0x0001515E
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00016F66 File Offset: 0x00015166
		internal SharedVertex[] sharedTextures
		{
			get
			{
				return this.m_SharedTextures;
			}
			set
			{
				this.m_SharedTextures = value;
				this.InvalidateSharedTextureLookup();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00016F78 File Offset: 0x00015178
		internal Dictionary<int, int> sharedTextureLookup
		{
			get
			{
				if ((this.m_CacheValid & ProBuilderMesh.CacheValidState.SharedTexture) != ProBuilderMesh.CacheValidState.SharedTexture)
				{
					this.m_CacheValid |= ProBuilderMesh.CacheValidState.SharedTexture;
					if (this.m_SharedTextureLookup == null)
					{
						this.m_SharedTextureLookup = new Dictionary<int, int>();
					}
					SharedVertex.GetSharedVertexLookup(this.m_SharedTextures, this.m_SharedTextureLookup);
				}
				return this.m_SharedTextureLookup;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00016FC8 File Offset: 0x000151C8
		internal void SetSharedTextures(IEnumerable<KeyValuePair<int, int>> indexes)
		{
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			this.m_SharedTextures = SharedVertex.ToSharedVertices(indexes);
			this.InvalidateSharedTextureLookup();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00016FEA File Offset: 0x000151EA
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00016FF2 File Offset: 0x000151F2
		internal Vector3[] positionsInternal
		{
			get
			{
				return this.m_Positions;
			}
			set
			{
				this.m_Positions = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00016FFB File Offset: 0x000151FB
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00017008 File Offset: 0x00015208
		public IList<Vector3> positions
		{
			get
			{
				return new ReadOnlyCollection<Vector3>(this.m_Positions);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Positions = value.ToArray<Vector3>();
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00017024 File Offset: 0x00015224
		public Vertex[] GetVertices(IList<int> indexes = null)
		{
			int vertexCount = this.vertexCount;
			int num = (indexes != null) ? indexes.Count : this.vertexCount;
			Vertex[] array = new Vertex[num];
			Vector3[] positionsInternal = this.positionsInternal;
			Color[] colorsInternal = this.colorsInternal;
			Vector2[] texturesInternal = this.texturesInternal;
			Vector4[] tangents = this.GetTangents();
			Vector3[] normals = this.GetNormals();
			Vector2[] array2 = (this.mesh != null) ? this.mesh.uv2 : null;
			List<Vector4> list = new List<Vector4>();
			List<Vector4> list2 = new List<Vector4>();
			this.GetUVs(2, list);
			this.GetUVs(3, list2);
			bool flag = positionsInternal != null && positionsInternal.Length == vertexCount;
			bool flag2 = colorsInternal != null && colorsInternal.Length == vertexCount;
			bool flag3 = normals != null && normals.Length == vertexCount;
			bool flag4 = tangents != null && tangents.Length == vertexCount;
			bool flag5 = texturesInternal != null && texturesInternal.Length == vertexCount;
			bool flag6 = array2 != null && array2.Length == vertexCount;
			bool flag7 = list.Count == vertexCount;
			bool flag8 = list2.Count == vertexCount;
			for (int i = 0; i < num; i++)
			{
				array[i] = new Vertex();
				int num2 = (indexes == null) ? i : indexes[i];
				if (flag)
				{
					array[i].position = positionsInternal[num2];
				}
				if (flag2)
				{
					array[i].color = colorsInternal[num2];
				}
				if (flag3)
				{
					array[i].normal = normals[num2];
				}
				if (flag4)
				{
					array[i].tangent = tangents[num2];
				}
				if (flag5)
				{
					array[i].uv0 = texturesInternal[num2];
				}
				if (flag6)
				{
					array[i].uv2 = array2[num2];
				}
				if (flag7)
				{
					array[i].uv3 = list[num2];
				}
				if (flag8)
				{
					array[i].uv4 = list2[num2];
				}
			}
			return array;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001720C File Offset: 0x0001540C
		internal void GetVerticesInList(IList<Vertex> vertices)
		{
			int vertexCount = this.vertexCount;
			vertices.Clear();
			Vector3[] positionsInternal = this.positionsInternal;
			Color[] colorsInternal = this.colorsInternal;
			Vector2[] texturesInternal = this.texturesInternal;
			Vector4[] tangents = this.GetTangents();
			Vector3[] normals = this.GetNormals();
			Vector2[] array = (this.mesh != null) ? this.mesh.uv2 : null;
			List<Vector4> list = new List<Vector4>();
			List<Vector4> list2 = new List<Vector4>();
			this.GetUVs(2, list);
			this.GetUVs(3, list2);
			bool flag = positionsInternal != null && positionsInternal.Length == vertexCount;
			bool flag2 = colorsInternal != null && colorsInternal.Length == vertexCount;
			bool flag3 = normals != null && normals.Length == vertexCount;
			bool flag4 = tangents != null && tangents.Length == vertexCount;
			bool flag5 = texturesInternal != null && texturesInternal.Length == vertexCount;
			bool flag6 = array != null && array.Length == vertexCount;
			bool flag7 = list.Count == vertexCount;
			bool flag8 = list2.Count == vertexCount;
			for (int i = 0; i < vertexCount; i++)
			{
				vertices.Add(new Vertex());
				if (flag)
				{
					vertices[i].position = positionsInternal[i];
				}
				if (flag2)
				{
					vertices[i].color = colorsInternal[i];
				}
				if (flag3)
				{
					vertices[i].normal = normals[i];
				}
				if (flag4)
				{
					vertices[i].tangent = tangents[i];
				}
				if (flag5)
				{
					vertices[i].uv0 = texturesInternal[i];
				}
				if (flag6)
				{
					vertices[i].uv2 = array[i];
				}
				if (flag7)
				{
					vertices[i].uv3 = list[i];
				}
				if (flag8)
				{
					vertices[i].uv4 = list2[i];
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000173EC File Offset: 0x000155EC
		public void SetVertices(IList<Vertex> vertices, bool applyMesh = false)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			Vertex vertex = vertices.FirstOrDefault<Vertex>();
			if (vertex == null || !vertex.HasArrays(MeshArrays.Position))
			{
				this.Clear();
				return;
			}
			Vector3[] array;
			Color[] colors;
			Vector2[] array2;
			Vector3[] normals;
			Vector4[] tangents;
			Vector2[] uv;
			List<Vector4> list;
			List<Vector4> list2;
			Vertex.GetArrays(vertices, out array, out colors, out array2, out normals, out tangents, out uv, out list, out list2);
			this.m_Positions = array;
			this.m_Colors = colors;
			this.m_Normals = normals;
			this.m_Tangents = tangents;
			this.m_Textures0 = array2;
			this.m_Textures2 = list;
			this.m_Textures3 = list2;
			if (applyMesh)
			{
				Mesh mesh = this.mesh;
				if (vertex.HasArrays(MeshArrays.Position))
				{
					mesh.vertices = array;
				}
				if (vertex.HasArrays(MeshArrays.Color))
				{
					mesh.colors = colors;
				}
				if (vertex.HasArrays(MeshArrays.Texture0))
				{
					mesh.uv = array2;
				}
				if (vertex.HasArrays(MeshArrays.Normal))
				{
					mesh.normals = normals;
				}
				if (vertex.HasArrays(MeshArrays.Tangent))
				{
					mesh.tangents = tangents;
				}
				if (vertex.HasArrays(MeshArrays.Texture1))
				{
					mesh.uv2 = uv;
				}
				if (vertex.HasArrays(MeshArrays.Texture2))
				{
					mesh.SetUVs(2, list);
				}
				if (vertex.HasArrays(MeshArrays.Texture3))
				{
					mesh.SetUVs(3, list2);
				}
				this.IncrementVersionIndex();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0001751C File Offset: 0x0001571C
		public IList<Vector3> normals
		{
			get
			{
				if (this.m_Normals == null)
				{
					return null;
				}
				return new ReadOnlyCollection<Vector3>(this.m_Normals);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00017533 File Offset: 0x00015733
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0001753B File Offset: 0x0001573B
		internal Vector3[] normalsInternal
		{
			get
			{
				return this.m_Normals;
			}
			set
			{
				this.m_Normals = value;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00017544 File Offset: 0x00015744
		public Vector3[] GetNormals()
		{
			if (!this.HasArrays(MeshArrays.Normal))
			{
				Normals.CalculateNormals(this);
			}
			return this.normals.ToArray<Vector3>();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00017561 File Offset: 0x00015761
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00017569 File Offset: 0x00015769
		internal Color[] colorsInternal
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				this.m_Colors = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00017572 File Offset: 0x00015772
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00017589 File Offset: 0x00015789
		public IList<Color> colors
		{
			get
			{
				if (this.m_Colors == null)
				{
					return null;
				}
				return new ReadOnlyCollection<Color>(this.m_Colors);
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.m_Colors = null;
					return;
				}
				if (value.Count != this.vertexCount)
				{
					throw new ArgumentOutOfRangeException("value", "Array length must match vertex count.");
				}
				this.m_Colors = value.ToArray<Color>();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000175C8 File Offset: 0x000157C8
		public Color[] GetColors()
		{
			if (this.HasArrays(MeshArrays.Color))
			{
				return this.colors.ToArray<Color>();
			}
			return ArrayUtility.Fill<Color>(Color.white, this.vertexCount);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000175F0 File Offset: 0x000157F0
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00017617 File Offset: 0x00015817
		public IList<Vector4> tangents
		{
			get
			{
				if (this.m_Tangents != null && this.m_Tangents.Length == this.vertexCount)
				{
					return new ReadOnlyCollection<Vector4>(this.m_Tangents);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.m_Tangents = null;
					return;
				}
				if (value.Count != this.vertexCount)
				{
					throw new ArgumentOutOfRangeException("value", "Tangent array length must match vertex count");
				}
				this.m_Tangents = value.ToArray<Vector4>();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001764E File Offset: 0x0001584E
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00017656 File Offset: 0x00015856
		internal Vector4[] tangentsInternal
		{
			get
			{
				return this.m_Tangents;
			}
			set
			{
				this.m_Tangents = value;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001765F File Offset: 0x0001585F
		public Vector4[] GetTangents()
		{
			if (!this.HasArrays(MeshArrays.Tangent))
			{
				Normals.CalculateTangents(this);
			}
			return this.tangents.ToArray<Vector4>();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0001767F File Offset: 0x0001587F
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00017687 File Offset: 0x00015887
		internal Vector2[] texturesInternal
		{
			get
			{
				return this.m_Textures0;
			}
			set
			{
				this.m_Textures0 = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00017690 File Offset: 0x00015890
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00017698 File Offset: 0x00015898
		internal List<Vector4> textures2Internal
		{
			get
			{
				return this.m_Textures2;
			}
			set
			{
				this.m_Textures2 = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000176A1 File Offset: 0x000158A1
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000176A9 File Offset: 0x000158A9
		internal List<Vector4> textures3Internal
		{
			get
			{
				return this.m_Textures3;
			}
			set
			{
				this.m_Textures3 = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000176B2 File Offset: 0x000158B2
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000176C9 File Offset: 0x000158C9
		public IList<Vector2> textures
		{
			get
			{
				if (this.m_Textures0 == null)
				{
					return null;
				}
				return new ReadOnlyCollection<Vector2>(this.m_Textures0);
			}
			set
			{
				if (value == null)
				{
					this.m_Textures0 = null;
					return;
				}
				if (value.Count != this.vertexCount)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Textures0 = value.ToArray<Vector2>();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000176FC File Offset: 0x000158FC
		public void GetUVs(int channel, List<Vector4> uvs)
		{
			if (uvs == null)
			{
				throw new ArgumentNullException("uvs");
			}
			if (channel < 0 || channel > 3)
			{
				throw new ArgumentOutOfRangeException("channel");
			}
			uvs.Clear();
			switch (channel)
			{
			case 0:
				for (int i = 0; i < this.vertexCount; i++)
				{
					uvs.Add(this.m_Textures0[i]);
				}
				return;
			case 1:
				if (this.mesh != null && this.mesh.uv2 != null)
				{
					Vector2[] uv = this.mesh.uv2;
					for (int j = 0; j < uv.Length; j++)
					{
						uvs.Add(uv[j]);
					}
					return;
				}
				break;
			case 2:
				if (this.m_Textures2 != null)
				{
					uvs.AddRange(this.m_Textures2);
					return;
				}
				break;
			case 3:
				if (this.m_Textures3 != null)
				{
					uvs.AddRange(this.m_Textures3);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000177E4 File Offset: 0x000159E4
		internal ReadOnlyCollection<Vector2> GetUVs(int channel)
		{
			if (channel == 0)
			{
				return new ReadOnlyCollection<Vector2>(this.m_Textures0);
			}
			if (channel == 1)
			{
				return new ReadOnlyCollection<Vector2>(this.mesh.uv2);
			}
			if (channel == 2)
			{
				if (this.m_Textures2 != null)
				{
					return new ReadOnlyCollection<Vector2>(this.m_Textures2.Cast<Vector2>().ToList<Vector2>());
				}
				return null;
			}
			else
			{
				if (channel != 3)
				{
					return null;
				}
				if (this.m_Textures3 != null)
				{
					return new ReadOnlyCollection<Vector2>(this.m_Textures3.Cast<Vector2>().ToList<Vector2>());
				}
				return null;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00017860 File Offset: 0x00015A60
		public void SetUVs(int channel, List<Vector4> uvs)
		{
			switch (channel)
			{
			case 0:
			{
				Vector2[] textures;
				if (uvs == null)
				{
					textures = null;
				}
				else
				{
					textures = (from x in uvs
					select x).ToArray<Vector2>();
				}
				this.m_Textures0 = textures;
				return;
			}
			case 1:
			{
				Mesh mesh = this.mesh;
				Vector2[] uv;
				if (uvs == null)
				{
					uv = null;
				}
				else
				{
					uv = (from x in uvs
					select x).ToArray<Vector2>();
				}
				mesh.uv2 = uv;
				return;
			}
			case 2:
				this.m_Textures2 = ((uvs != null) ? new List<Vector4>(uvs) : null);
				return;
			case 3:
				this.m_Textures3 = ((uvs != null) ? new List<Vector4>(uvs) : null);
				return;
			default:
				return;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0001791C File Offset: 0x00015B1C
		public int faceCount
		{
			get
			{
				if (this.m_Faces != null)
				{
					return this.m_Faces.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00017930 File Offset: 0x00015B30
		public int vertexCount
		{
			get
			{
				if (this.m_Positions != null)
				{
					return this.m_Positions.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00017944 File Offset: 0x00015B44
		public int edgeCount
		{
			get
			{
				int num = 0;
				int i = 0;
				int faceCount = this.faceCount;
				while (i < faceCount)
				{
					num += this.facesInternal[i].edgesInternal.Length;
					i++;
				}
				return num;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00017979 File Offset: 0x00015B79
		public int indexCount
		{
			get
			{
				if (this.m_Faces != null)
				{
					return this.m_Faces.Sum((Face x) => x.indexesInternal.Length);
				}
				return 0;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000179AF File Offset: 0x00015BAF
		public int triangleCount
		{
			get
			{
				if (this.m_Faces != null)
				{
					return this.m_Faces.Sum((Face x) => x.indexesInternal.Length) / 3;
				}
				return 0;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600024F RID: 591 RVA: 0x000179E8 File Offset: 0x00015BE8
		// (remove) Token: 0x06000250 RID: 592 RVA: 0x00017A1C File Offset: 0x00015C1C
		public static event Action<ProBuilderMesh> meshWillBeDestroyed
		{
			[CompilerGenerated]
			add
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.meshWillBeDestroyed;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.meshWillBeDestroyed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.meshWillBeDestroyed;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.meshWillBeDestroyed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000251 RID: 593 RVA: 0x00017A50 File Offset: 0x00015C50
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x00017A84 File Offset: 0x00015C84
		internal static event Action<ProBuilderMesh> meshWasInitialized
		{
			[CompilerGenerated]
			add
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.meshWasInitialized;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.meshWasInitialized, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.meshWasInitialized;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.meshWasInitialized, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000253 RID: 595 RVA: 0x00017AB8 File Offset: 0x00015CB8
		// (remove) Token: 0x06000254 RID: 596 RVA: 0x00017AEC File Offset: 0x00015CEC
		internal static event Action<ProBuilderMesh> componentWillBeDestroyed
		{
			[CompilerGenerated]
			add
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.componentWillBeDestroyed;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.componentWillBeDestroyed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.componentWillBeDestroyed;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.componentWillBeDestroyed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000255 RID: 597 RVA: 0x00017B20 File Offset: 0x00015D20
		// (remove) Token: 0x06000256 RID: 598 RVA: 0x00017B54 File Offset: 0x00015D54
		internal static event Action<ProBuilderMesh> componentHasBeenReset
		{
			[CompilerGenerated]
			add
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.componentHasBeenReset;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.componentHasBeenReset, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.componentHasBeenReset;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.componentHasBeenReset, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000257 RID: 599 RVA: 0x00017B88 File Offset: 0x00015D88
		// (remove) Token: 0x06000258 RID: 600 RVA: 0x00017BBC File Offset: 0x00015DBC
		public static event Action<ProBuilderMesh> elementSelectionChanged
		{
			[CompilerGenerated]
			add
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.elementSelectionChanged;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.elementSelectionChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProBuilderMesh> action = ProBuilderMesh.elementSelectionChanged;
				Action<ProBuilderMesh> action2;
				do
				{
					action2 = action;
					Action<ProBuilderMesh> value2 = (Action<ProBuilderMesh>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProBuilderMesh>>(ref ProBuilderMesh.elementSelectionChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00017BEF File Offset: 0x00015DEF
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00017C24 File Offset: 0x00015E24
		internal Mesh mesh
		{
			get
			{
				if (this.m_Mesh == null && this.filter != null)
				{
					this.m_Mesh = this.filter.sharedMesh;
				}
				return this.m_Mesh;
			}
			set
			{
				this.m_Mesh = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00017C2D File Offset: 0x00015E2D
		[Obsolete("InstanceID is not used to track mesh references as of 2023/04/12")]
		internal int id
		{
			get
			{
				return base.gameObject.GetInstanceID();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00017C3A File Offset: 0x00015E3A
		public MeshSyncState meshSyncState
		{
			get
			{
				if (this.mesh == null)
				{
					return MeshSyncState.Null;
				}
				if (this.m_VersionIndex != this.m_InstanceVersionIndex && this.m_InstanceVersionIndex != 0)
				{
					return MeshSyncState.NeedsRebuild;
				}
				if (this.mesh.uv2 != null)
				{
					return MeshSyncState.InSync;
				}
				return MeshSyncState.Lightmap;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00017C74 File Offset: 0x00015E74
		internal int meshFormatVersion
		{
			get
			{
				return this.m_MeshFormatVersion;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00017C7C File Offset: 0x00015E7C
		private void Awake()
		{
			this.EnsureMeshFilterIsAssigned();
			this.EnsureMeshColliderIsAssigned();
			this.ClearSelection();
			if (this.vertexCount > 0 && this.faceCount > 0 && this.meshSyncState == MeshSyncState.Null)
			{
				using (new ProBuilderMesh.NonVersionedEditScope(this))
				{
					this.Rebuild();
					Action<ProBuilderMesh> action = ProBuilderMesh.meshWasInitialized;
					if (action != null)
					{
						action(this);
					}
				}
				this.m_InstanceVersionIndex = this.m_VersionIndex;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00017D00 File Offset: 0x00015F00
		private void Reset()
		{
			if (this.meshSyncState != MeshSyncState.Null)
			{
				this.Rebuild();
				if (ProBuilderMesh.componentHasBeenReset != null)
				{
					ProBuilderMesh.componentHasBeenReset(this);
				}
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00017D24 File Offset: 0x00015F24
		private void OnDestroy()
		{
			if (this.m_MeshFilter != null || base.TryGetComponent<MeshFilter>(out this.m_MeshFilter))
			{
				this.m_MeshFilter.hideFlags = HideFlags.None;
			}
			if (ProBuilderMesh.componentWillBeDestroyed != null)
			{
				ProBuilderMesh.componentWillBeDestroyed(this);
			}
			if (!this.preserveMeshAssetOnDestroy && Application.isEditor && !Application.isPlaying && Time.frameCount > 0)
			{
				this.DestroyUnityMesh();
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00017D8F File Offset: 0x00015F8F
		internal void DestroyUnityMesh()
		{
			if (ProBuilderMesh.meshWillBeDestroyed != null)
			{
				ProBuilderMesh.meshWillBeDestroyed(this);
				return;
			}
			Object.DestroyImmediate(base.gameObject.GetComponent<MeshFilter>().sharedMesh, true);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00017DBC File Offset: 0x00015FBC
		private void IncrementVersionIndex()
		{
			ushort num = this.m_VersionIndex + 1;
			this.m_VersionIndex = num;
			if (num == 0)
			{
				this.m_VersionIndex = 1;
			}
			this.m_InstanceVersionIndex = this.m_VersionIndex;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public void Clear()
		{
			this.m_Faces = new Face[0];
			this.m_Positions = new Vector3[0];
			this.m_Textures0 = new Vector2[0];
			this.m_Textures2 = null;
			this.m_Textures3 = null;
			this.m_Tangents = null;
			this.m_SharedVertices = new SharedVertex[0];
			this.m_SharedTextures = new SharedVertex[0];
			this.InvalidateSharedVertexLookup();
			this.InvalidateSharedTextureLookup();
			this.m_Colors = null;
			this.m_MeshFormatVersion = 2;
			this.IncrementVersionIndex();
			this.ClearSelection();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00017E74 File Offset: 0x00016074
		internal void EnsureMeshFilterIsAssigned()
		{
			if (this.filter == null)
			{
				this.m_MeshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (!this.renderer.isPartOfStaticBatch && this.filter.sharedMesh != this.m_Mesh)
			{
				this.filter.sharedMesh = this.m_Mesh;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00017ED6 File Offset: 0x000160D6
		internal static ProBuilderMesh CreateInstanceWithPoints(Vector3[] positions)
		{
			if (positions.Length % 4 != 0)
			{
				Log.Warning("Invalid Geometry. Make sure vertices in are pairs of 4 (faces).");
				return null;
			}
			ProBuilderMesh proBuilderMesh = new GameObject
			{
				name = "ProBuilder Mesh"
			}.AddComponent<ProBuilderMesh>();
			proBuilderMesh.m_MeshFormatVersion = 2;
			proBuilderMesh.GeometryWithPoints(positions);
			return proBuilderMesh;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00017F0E File Offset: 0x0001610E
		public static ProBuilderMesh Create()
		{
			ProBuilderMesh proBuilderMesh = new GameObject().AddComponent<ProBuilderMesh>();
			proBuilderMesh.m_MeshFormatVersion = 2;
			proBuilderMesh.Clear();
			return proBuilderMesh;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00017F28 File Offset: 0x00016128
		public static ProBuilderMesh Create(IEnumerable<Vector3> positions, IEnumerable<Face> faces)
		{
			GameObject gameObject = new GameObject();
			ProBuilderMesh proBuilderMesh = gameObject.AddComponent<ProBuilderMesh>();
			gameObject.name = "ProBuilder Mesh";
			proBuilderMesh.m_MeshFormatVersion = 2;
			proBuilderMesh.RebuildWithPositionsAndFaces(positions, faces);
			return proBuilderMesh;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00017F5C File Offset: 0x0001615C
		public static ProBuilderMesh Create(IList<Vertex> vertices, IList<Face> faces, IList<SharedVertex> sharedVertices = null, IList<SharedVertex> sharedTextures = null, IList<Material> materials = null)
		{
			ProBuilderMesh proBuilderMesh = new GameObject
			{
				name = "ProBuilder Mesh"
			}.AddComponent<ProBuilderMesh>();
			if (materials != null)
			{
				proBuilderMesh.renderer.sharedMaterials = materials.ToArray<Material>();
			}
			proBuilderMesh.m_MeshFormatVersion = 2;
			proBuilderMesh.SetVertices(vertices, false);
			proBuilderMesh.faces = faces;
			proBuilderMesh.sharedVertices = sharedVertices;
			proBuilderMesh.sharedTextures = ((sharedTextures != null) ? sharedTextures.ToArray<SharedVertex>() : null);
			proBuilderMesh.ToMesh(MeshTopology.Triangles);
			proBuilderMesh.Refresh(RefreshMask.All);
			return proBuilderMesh;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00017FD4 File Offset: 0x000161D4
		internal void GeometryWithPoints(Vector3[] points)
		{
			Face[] array = new Face[points.Length / 4];
			for (int i = 0; i < points.Length; i += 4)
			{
				array[i / 4] = new Face(new int[]
				{
					i,
					i + 1,
					i + 2,
					i + 1,
					i + 3,
					i + 2
				}, 0, AutoUnwrapSettings.tile, 0, -1, -1, false);
			}
			this.Clear();
			this.positions = points;
			this.m_Faces = array;
			this.m_SharedVertices = SharedVertex.GetSharedVerticesWithPositions(points);
			this.InvalidateCaches();
			this.ToMesh(MeshTopology.Triangles);
			this.Refresh(RefreshMask.All);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001806C File Offset: 0x0001626C
		public void RebuildWithPositionsAndFaces(IEnumerable<Vector3> vertices, IEnumerable<Face> faces)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			this.Clear();
			this.m_Positions = vertices.ToArray<Vector3>();
			this.m_Faces = faces.ToArray<Face>();
			this.m_SharedVertices = SharedVertex.GetSharedVerticesWithPositions(this.m_Positions);
			this.InvalidateSharedVertexLookup();
			this.InvalidateSharedTextureLookup();
			this.ToMesh(MeshTopology.Triangles);
			this.Refresh(RefreshMask.All);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000180D1 File Offset: 0x000162D1
		internal void Rebuild()
		{
			this.ToMesh(MeshTopology.Triangles);
			this.Refresh(RefreshMask.All);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000180E4 File Offset: 0x000162E4
		public void ToMesh(MeshTopology preferredTopology = MeshTopology.Triangles)
		{
			bool usedInParticleSystem = false;
			if (this.mesh == null)
			{
				this.mesh = new Mesh
				{
					name = string.Format("pb_Mesh{0}", base.GetInstanceID())
				};
			}
			else if (this.mesh.vertexCount != this.vertexCount)
			{
				usedInParticleSystem = MeshUtility.IsUsedInParticleSystem(this);
				this.mesh.Clear();
			}
			this.mesh.indexFormat = ((this.vertexCount > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			this.mesh.vertices = this.m_Positions;
			this.mesh.uv2 = null;
			if (this.m_MeshFormatVersion < 2)
			{
				if (this.m_MeshFormatVersion < 1)
				{
					Submesh.MapFaceMaterialsToSubmeshIndex(this);
				}
				if (this.m_MeshFormatVersion < 2)
				{
					UvUnwrapping.UpgradeAutoUVScaleOffset(this);
				}
				this.m_MeshFormatVersion = 2;
			}
			this.m_MeshFormatVersion = 2;
			int materialCount = MaterialUtility.GetMaterialCount(this.renderer);
			Submesh[] submeshes = Submesh.GetSubmeshes(this.facesInternal, materialCount, preferredTopology);
			this.mesh.subMeshCount = submeshes.Length;
			if (this.mesh.subMeshCount == 0)
			{
				this.FinalizeToMesh(usedInParticleSystem);
				return;
			}
			int num = 0;
			bool flag = false;
			for (int i = 0; i < this.mesh.subMeshCount; i++)
			{
				if (submeshes[i].m_Indexes.Length == 0)
				{
					if (!flag)
					{
						MaterialUtility.s_MaterialArray.Clear();
						this.renderer.GetSharedMaterials(MaterialUtility.s_MaterialArray);
						flag = true;
					}
					submeshes[i].submeshIndex = -1;
					MaterialUtility.s_MaterialArray.RemoveAt(num);
					foreach (Face face in this.facesInternal)
					{
						if (num < face.submeshIndex)
						{
							face.submeshIndex--;
						}
					}
				}
				else
				{
					submeshes[i].submeshIndex = num;
					this.mesh.SetIndices(submeshes[i].m_Indexes, submeshes[i].m_Topology, submeshes[i].submeshIndex, false);
					num++;
				}
			}
			if (this.mesh.subMeshCount < materialCount)
			{
				int num2 = materialCount - this.mesh.subMeshCount;
				int index = MaterialUtility.s_MaterialArray.Count - num2;
				MaterialUtility.s_MaterialArray.RemoveRange(index, num2);
				flag = true;
			}
			if (flag)
			{
				this.renderer.sharedMaterials = MaterialUtility.s_MaterialArray.ToArray();
			}
			this.FinalizeToMesh(usedInParticleSystem);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001832F File Offset: 0x0001652F
		private void FinalizeToMesh(bool usedInParticleSystem)
		{
			this.EnsureMeshFilterIsAssigned();
			if (usedInParticleSystem)
			{
				MeshUtility.RestoreParticleSystem(this);
			}
			this.IncrementVersionIndex();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00018348 File Offset: 0x00016548
		public void MakeUnique()
		{
			Mesh mesh;
			if (!(this.mesh != null))
			{
				(mesh = new Mesh()).name = string.Format("pb_Mesh{0}", base.GetInstanceID());
			}
			else
			{
				mesh = Object.Instantiate<Mesh>(this.mesh);
			}
			this.mesh = mesh;
			if (this.meshSyncState == MeshSyncState.InSync)
			{
				this.filter.mesh = this.mesh;
				return;
			}
			this.ToMesh(MeshTopology.Triangles);
			this.Refresh(RefreshMask.All);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000183C0 File Offset: 0x000165C0
		public void CopyFrom(ProBuilderMesh other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.Clear();
			this.positions = other.positions;
			this.sharedVertices = other.sharedVerticesInternal;
			this.SetSharedTextures(other.sharedTextureLookup);
			this.facesInternal = (from x in other.faces
			select new Face(x)).ToArray<Face>();
			List<Vector4> uvs = new List<Vector4>();
			for (int i = 0; i < 4; i++)
			{
				other.GetUVs(i, uvs);
				this.SetUVs(i, uvs);
			}
			this.tangents = other.tangents;
			this.colors = other.colors;
			this.userCollisions = other.userCollisions;
			this.selectable = other.selectable;
			this.unwrapParameters = new UnwrapParameters(other.unwrapParameters);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000184A4 File Offset: 0x000166A4
		public void Refresh(RefreshMask mask = RefreshMask.All)
		{
			if ((mask & RefreshMask.UV) > (RefreshMask)0)
			{
				this.RefreshUV(this.facesInternal);
			}
			if ((mask & RefreshMask.Colors) > (RefreshMask)0)
			{
				this.RefreshColors();
			}
			if ((mask & RefreshMask.Normals) > (RefreshMask)0)
			{
				this.RefreshNormals();
			}
			if ((mask & RefreshMask.Tangents) > (RefreshMask)0)
			{
				this.RefreshTangents();
			}
			if ((mask & RefreshMask.Collisions) > (RefreshMask)0)
			{
				this.EnsureMeshColliderIsAssigned();
			}
			if ((mask & RefreshMask.Bounds) > (RefreshMask)0 && this.mesh != null)
			{
				this.mesh.RecalculateBounds();
			}
			this.IncrementVersionIndex();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001851C File Offset: 0x0001671C
		internal void EnsureMeshColliderIsAssigned()
		{
			MeshCollider meshCollider;
			if (base.gameObject.TryGetComponent<MeshCollider>(out meshCollider))
			{
				meshCollider.sharedMesh = ((this.mesh != null && this.mesh.vertexCount > 0) ? this.mesh : null);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00018564 File Offset: 0x00016764
		internal int GetUnusedTextureGroup(int i = 1)
		{
			while (Array.Exists<Face>(this.facesInternal, (Face element) => element.textureGroup == i))
			{
				int i2 = i;
				i = i2 + 1;
			}
			return i;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000185AF File Offset: 0x000167AF
		private static bool IsValidTextureGroup(int group)
		{
			return group > 0;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000185B8 File Offset: 0x000167B8
		internal int UnusedElementGroup(int i = 1)
		{
			while (Array.Exists<Face>(this.facesInternal, (Face element) => element.elementGroup == i))
			{
				int i2 = i;
				i = i2 + 1;
			}
			return i;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00018604 File Offset: 0x00016804
		public void RefreshUV(IEnumerable<Face> facesToRefresh)
		{
			if (!this.HasArrays(MeshArrays.Texture0))
			{
				this.m_Textures0 = new Vector2[this.vertexCount];
				Face[] facesInternal = this.facesInternal;
				for (int i = 0; i < facesInternal.Length; i++)
				{
					facesInternal[i].manualUV = false;
				}
				facesToRefresh = this.facesInternal;
			}
			ProBuilderMesh.s_CachedHashSet.Clear();
			foreach (Face face in facesToRefresh)
			{
				if (!face.manualUV)
				{
					int[] indexesInternal = face.indexesInternal;
					if (indexesInternal == null || indexesInternal.Length >= 3)
					{
						int textureGroup = face.textureGroup;
						if (!ProBuilderMesh.IsValidTextureGroup(textureGroup))
						{
							UvUnwrapping.Unwrap(this, face, default(Vector3));
						}
						else if (ProBuilderMesh.s_CachedHashSet.Add(textureGroup))
						{
							UvUnwrapping.ProjectTextureGroup(this, textureGroup, face.uv);
						}
					}
				}
			}
			this.mesh.uv = this.m_Textures0;
			if (this.HasArrays(MeshArrays.Texture2))
			{
				this.mesh.SetUVs(2, this.m_Textures2);
			}
			if (this.HasArrays(MeshArrays.Texture3))
			{
				this.mesh.SetUVs(3, this.m_Textures3);
			}
			this.IncrementVersionIndex();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001873C File Offset: 0x0001693C
		internal void SetGroupUV(AutoUnwrapSettings settings, int group)
		{
			if (!ProBuilderMesh.IsValidTextureGroup(group))
			{
				return;
			}
			foreach (Face face in this.facesInternal)
			{
				if (face.textureGroup == group)
				{
					face.uv = settings;
				}
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001877B File Offset: 0x0001697B
		private void RefreshColors()
		{
			this.filter.sharedMesh.colors = this.m_Colors;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00018794 File Offset: 0x00016994
		public void SetFaceColor(Face face, Color color)
		{
			if (face == null)
			{
				throw new ArgumentNullException("face");
			}
			if (!this.HasArrays(MeshArrays.Color))
			{
				this.m_Colors = ArrayUtility.Fill<Color>(Color.white, this.vertexCount);
			}
			foreach (int num in face.distinctIndexes)
			{
				this.m_Colors[num] = color;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00018818 File Offset: 0x00016A18
		public void SetMaterial(IEnumerable<Face> faces, Material material)
		{
			Material[] sharedMaterials = this.renderer.sharedMaterials;
			int num = sharedMaterials.Length;
			int num2 = -1;
			int num3 = 0;
			while (num3 < num && num2 < 0)
			{
				if (sharedMaterials[num3] == material)
				{
					num2 = num3;
				}
				num3++;
			}
			if (num2 < 0)
			{
				bool[] array = new bool[num];
				foreach (Face face in this.m_Faces)
				{
					array[Math.Clamp(face.submeshIndex, 0, num - 1)] = true;
				}
				num2 = Array.IndexOf<bool>(array, false);
				if (num2 > -1)
				{
					sharedMaterials[num2] = material;
					this.renderer.sharedMaterials = sharedMaterials;
				}
				else
				{
					num2 = sharedMaterials.Length;
					Material[] array2 = new Material[num2 + 1];
					Array.Copy(sharedMaterials, array2, num2);
					array2[num2] = material;
					this.renderer.sharedMaterials = array2;
				}
			}
			foreach (Face face2 in faces)
			{
				face2.submeshIndex = num2;
			}
			this.IncrementVersionIndex();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00018928 File Offset: 0x00016B28
		private void RefreshNormals()
		{
			Normals.CalculateNormals(this);
			this.mesh.normals = this.m_Normals;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00018941 File Offset: 0x00016B41
		private void RefreshTangents()
		{
			Normals.CalculateTangents(this);
			this.mesh.tangents = this.m_Tangents;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001895C File Offset: 0x00016B5C
		internal int GetSharedVertexHandle(int vertex)
		{
			int result;
			if (this.m_SharedVertexLookup.TryGetValue(vertex, out result))
			{
				return result;
			}
			for (int i = 0; i < this.m_SharedVertices.Length; i++)
			{
				int j = 0;
				int count = this.m_SharedVertices[i].Count;
				while (j < count)
				{
					if (this.m_SharedVertices[i][j] == vertex)
					{
						return i;
					}
					j++;
				}
			}
			throw new ArgumentOutOfRangeException("vertex");
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000189C8 File Offset: 0x00016BC8
		internal HashSet<int> GetSharedVertexHandles(IEnumerable<int> vertices)
		{
			Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
			HashSet<int> hashSet = new HashSet<int>();
			foreach (int key in vertices)
			{
				hashSet.Add(sharedVertexLookup[key]);
			}
			return hashSet;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00018A28 File Offset: 0x00016C28
		public List<int> GetCoincidentVertices(IEnumerable<int> vertices)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			List<int> list = new List<int>();
			this.GetCoincidentVertices(vertices, list);
			return list;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00018A54 File Offset: 0x00016C54
		public void GetCoincidentVertices(IEnumerable<Face> faces, List<int> coincident)
		{
			if (faces == null)
			{
				throw new ArgumentNullException("faces");
			}
			if (coincident == null)
			{
				throw new ArgumentNullException("coincident");
			}
			coincident.Clear();
			ProBuilderMesh.s_CachedHashSet.Clear();
			Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
			foreach (Face face in faces)
			{
				foreach (int key in face.distinctIndexesInternal)
				{
					int num = sharedVertexLookup[key];
					if (ProBuilderMesh.s_CachedHashSet.Add(num))
					{
						SharedVertex sharedVertex = this.m_SharedVertices[num];
						int j = 0;
						int count = sharedVertex.Count;
						while (j < count)
						{
							coincident.Add(sharedVertex[j]);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00018B2C File Offset: 0x00016D2C
		public void GetCoincidentVertices(IEnumerable<Edge> edges, List<int> coincident)
		{
			if (this.faces == null)
			{
				throw new ArgumentNullException("edges");
			}
			if (coincident == null)
			{
				throw new ArgumentNullException("coincident");
			}
			coincident.Clear();
			ProBuilderMesh.s_CachedHashSet.Clear();
			Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
			foreach (Edge edge in edges)
			{
				int num = sharedVertexLookup[edge.a];
				if (ProBuilderMesh.s_CachedHashSet.Add(num))
				{
					SharedVertex sharedVertex = this.m_SharedVertices[num];
					int i = 0;
					int count = sharedVertex.Count;
					while (i < count)
					{
						coincident.Add(sharedVertex[i]);
						i++;
					}
				}
				num = sharedVertexLookup[edge.b];
				if (ProBuilderMesh.s_CachedHashSet.Add(num))
				{
					SharedVertex sharedVertex2 = this.m_SharedVertices[num];
					int j = 0;
					int count2 = sharedVertex2.Count;
					while (j < count2)
					{
						coincident.Add(sharedVertex2[j]);
						j++;
					}
				}
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00018C48 File Offset: 0x00016E48
		public void GetCoincidentVertices(IEnumerable<int> vertices, List<int> coincident)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			if (coincident == null)
			{
				throw new ArgumentNullException("coincident");
			}
			coincident.Clear();
			ProBuilderMesh.s_CachedHashSet.Clear();
			Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
			foreach (int key in vertices)
			{
				int num = sharedVertexLookup[key];
				if (ProBuilderMesh.s_CachedHashSet.Add(num))
				{
					SharedVertex sharedVertex = this.m_SharedVertices[num];
					int i = 0;
					int count = sharedVertex.Count;
					while (i < count)
					{
						coincident.Add(sharedVertex[i]);
						i++;
					}
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00018D04 File Offset: 0x00016F04
		public void GetCoincidentVertices(int vertex, List<int> coincident)
		{
			if (coincident == null)
			{
				throw new ArgumentNullException("coincident");
			}
			int num;
			if (!this.sharedVertexLookup.TryGetValue(vertex, out num))
			{
				throw new ArgumentOutOfRangeException("vertex");
			}
			SharedVertex sharedVertex = this.m_SharedVertices[num];
			int i = 0;
			int count = sharedVertex.Count;
			while (i < count)
			{
				coincident.Add(sharedVertex[i]);
				i++;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00018D64 File Offset: 0x00016F64
		public void SetVerticesCoincident(IEnumerable<int> vertices)
		{
			Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
			List<int> list = new List<int>();
			this.GetCoincidentVertices(vertices, list);
			SharedVertex.SetCoincident(ref sharedVertexLookup, list);
			this.SetSharedVertices(sharedVertexLookup);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00018D98 File Offset: 0x00016F98
		internal void SetTexturesCoincident(IEnumerable<int> vertices)
		{
			Dictionary<int, int> sharedTextureLookup = this.sharedTextureLookup;
			SharedVertex.SetCoincident(ref sharedTextureLookup, vertices);
			this.SetSharedTextures(sharedTextureLookup);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00018DBB File Offset: 0x00016FBB
		internal void AddToSharedVertex(int sharedVertexHandle, int vertex)
		{
			if (sharedVertexHandle < 0 || sharedVertexHandle >= this.m_SharedVertices.Length)
			{
				throw new ArgumentOutOfRangeException("sharedVertexHandle");
			}
			this.m_SharedVertices[sharedVertexHandle].Add(vertex);
			this.InvalidateSharedVertexLookup();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00018DEB File Offset: 0x00016FEB
		internal void AddSharedVertex(SharedVertex vertex)
		{
			if (vertex == null)
			{
				throw new ArgumentNullException("vertex");
			}
			this.m_SharedVertices = this.m_SharedVertices.Add(vertex);
			this.InvalidateSharedVertexLookup();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00018E13 File Offset: 0x00017013
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00018E1B File Offset: 0x0001701B
		public bool selectable
		{
			get
			{
				return this.m_IsSelectable;
			}
			set
			{
				this.m_IsSelectable = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00018E24 File Offset: 0x00017024
		public int selectedFaceCount
		{
			get
			{
				return this.m_SelectedFaces.Length;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00018E2E File Offset: 0x0001702E
		public int selectedVertexCount
		{
			get
			{
				return this.m_SelectedVertices.Length;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00018E38 File Offset: 0x00017038
		public int selectedEdgeCount
		{
			get
			{
				return this.m_SelectedEdges.Length;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00018E42 File Offset: 0x00017042
		internal int selectedSharedVerticesCount
		{
			get
			{
				this.CacheSelection();
				return this.m_SelectedSharedVerticesCount;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00018E50 File Offset: 0x00017050
		internal int selectedCoincidentVertexCount
		{
			get
			{
				this.CacheSelection();
				return this.m_SelectedCoincidentVertexCount;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00018E5E File Offset: 0x0001705E
		internal IEnumerable<int> selectedSharedVertices
		{
			get
			{
				this.CacheSelection();
				return this.m_SelectedSharedVertices;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00018E6C File Offset: 0x0001706C
		internal IEnumerable<int> selectedCoincidentVertices
		{
			get
			{
				this.CacheSelection();
				return this.m_SelectedCoincidentVertices;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00018E7C File Offset: 0x0001707C
		private void CacheSelection()
		{
			if (this.m_SelectedCacheDirty)
			{
				this.m_SelectedCacheDirty = false;
				this.m_SelectedSharedVertices.Clear();
				this.m_SelectedCoincidentVertices.Clear();
				Dictionary<int, int> sharedVertexLookup = this.sharedVertexLookup;
				this.m_SelectedSharedVerticesCount = 0;
				this.m_SelectedCoincidentVertexCount = 0;
				try
				{
					foreach (int key in this.m_SelectedVertices)
					{
						if (this.m_SelectedSharedVertices.Add(sharedVertexLookup[key]))
						{
							SharedVertex sharedVertex = this.sharedVerticesInternal[sharedVertexLookup[key]];
							this.m_SelectedSharedVerticesCount++;
							this.m_SelectedCoincidentVertexCount += sharedVertex.Count;
							this.m_SelectedCoincidentVertices.AddRange(sharedVertex);
						}
					}
				}
				catch
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00018F50 File Offset: 0x00017150
		public Face[] GetSelectedFaces()
		{
			int num = this.m_SelectedFaces.Length;
			Face[] array = new Face[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.m_Faces[this.m_SelectedFaces[i]];
			}
			return array;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00018F8C File Offset: 0x0001718C
		public ReadOnlyCollection<int> selectedFaceIndexes
		{
			get
			{
				return new ReadOnlyCollection<int>(this.m_SelectedFaces);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00018F99 File Offset: 0x00017199
		public ReadOnlyCollection<int> selectedVertices
		{
			get
			{
				return new ReadOnlyCollection<int>(this.m_SelectedVertices);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00018FA6 File Offset: 0x000171A6
		public ReadOnlyCollection<Edge> selectedEdges
		{
			get
			{
				return new ReadOnlyCollection<Edge>(this.m_SelectedEdges);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00018FB3 File Offset: 0x000171B3
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00018FBB File Offset: 0x000171BB
		internal Face[] selectedFacesInternal
		{
			get
			{
				return this.GetSelectedFaces();
			}
			set
			{
				this.m_SelectedFaces = (from x in value
				select Array.IndexOf<Face>(this.m_Faces, x)).ToArray<int>();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00018FDA File Offset: 0x000171DA
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00018FE2 File Offset: 0x000171E2
		internal int[] selectedFaceIndicesInternal
		{
			get
			{
				return this.m_SelectedFaces;
			}
			set
			{
				this.m_SelectedFaces = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00018FEB File Offset: 0x000171EB
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00018FF3 File Offset: 0x000171F3
		internal Edge[] selectedEdgesInternal
		{
			get
			{
				return this.m_SelectedEdges;
			}
			set
			{
				this.m_SelectedEdges = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00018FFC File Offset: 0x000171FC
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00019004 File Offset: 0x00017204
		internal int[] selectedIndexesInternal
		{
			get
			{
				return this.m_SelectedVertices;
			}
			set
			{
				this.m_SelectedVertices = value;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001900D File Offset: 0x0001720D
		internal Face GetActiveFace()
		{
			if (this.selectedFaceCount < 1)
			{
				return null;
			}
			return this.m_Faces[this.selectedFaceIndicesInternal[this.selectedFaceCount - 1]];
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00019030 File Offset: 0x00017230
		internal Edge GetActiveEdge()
		{
			if (this.selectedEdgeCount < 1)
			{
				return Edge.Empty;
			}
			return this.m_SelectedEdges[this.selectedEdgeCount - 1];
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00019054 File Offset: 0x00017254
		internal int GetActiveVertex()
		{
			if (this.selectedVertexCount < 1)
			{
				return -1;
			}
			return this.m_SelectedVertices[this.selectedVertexCount - 1];
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00019070 File Offset: 0x00017270
		internal void AddToFaceSelection(int index)
		{
			if (index > -1)
			{
				this.SetSelectedFaces(this.m_SelectedFaces.Add(index));
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00019088 File Offset: 0x00017288
		public void SetSelectedFaces(IEnumerable<Face> selected)
		{
			this.SetSelectedFaces((selected != null) ? (from x in selected
			select Array.IndexOf<Face>(this.facesInternal, x)) : null);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000190A8 File Offset: 0x000172A8
		internal void SetSelectedFaces(IEnumerable<int> selected)
		{
			if (selected == null)
			{
				this.ClearSelection();
			}
			else
			{
				this.m_SelectedFaces = selected.ToArray<int>();
				this.m_SelectedVertices = this.m_SelectedFaces.SelectMany((int x) => this.facesInternal[x].distinctIndexesInternal).ToArray<int>();
				this.m_SelectedEdges = this.m_SelectedFaces.SelectMany((int x) => this.facesInternal[x].edges).ToArray<Edge>();
			}
			this.m_SelectedCacheDirty = true;
			if (ProBuilderMesh.elementSelectionChanged != null)
			{
				ProBuilderMesh.elementSelectionChanged(this);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001912C File Offset: 0x0001732C
		public void SetSelectedEdges(IEnumerable<Edge> edges)
		{
			if (edges == null)
			{
				this.ClearSelection();
			}
			else
			{
				this.m_SelectedFaces = new int[0];
				this.m_SelectedEdges = edges.ToArray<Edge>();
				this.m_SelectedVertices = this.m_SelectedEdges.AllTriangles();
			}
			this.m_SelectedCacheDirty = true;
			if (ProBuilderMesh.elementSelectionChanged != null)
			{
				ProBuilderMesh.elementSelectionChanged(this);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00019188 File Offset: 0x00017388
		public void SetSelectedVertices(IEnumerable<int> vertices)
		{
			this.m_SelectedFaces = new int[0];
			this.m_SelectedEdges = new Edge[0];
			this.m_SelectedVertices = ((vertices != null) ? vertices.Distinct<int>().ToArray<int>() : new int[0]);
			this.m_SelectedCacheDirty = true;
			if (ProBuilderMesh.elementSelectionChanged != null)
			{
				ProBuilderMesh.elementSelectionChanged(this);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000191E2 File Offset: 0x000173E2
		internal void RemoveFromFaceSelectionAtIndex(int index)
		{
			this.SetSelectedFaces(this.m_SelectedFaces.RemoveAt(index));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000191F6 File Offset: 0x000173F6
		public void ClearSelection()
		{
			this.m_SelectedFaces = new int[0];
			this.m_SelectedEdges = new Edge[0];
			this.m_SelectedVertices = new int[0];
			this.m_SelectedCacheDirty = true;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00019224 File Offset: 0x00017424
		public ProBuilderMesh()
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00019278 File Offset: 0x00017478
		// Note: this type is marked as 'beforefieldinit'.
		static ProBuilderMesh()
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00019284 File Offset: 0x00017484
		[CompilerGenerated]
		private int <set_selectedFacesInternal>b__231_0(Face x)
		{
			return Array.IndexOf<Face>(this.m_Faces, x);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00019292 File Offset: 0x00017492
		[CompilerGenerated]
		private int <SetSelectedFaces>b__245_0(Face x)
		{
			return Array.IndexOf<Face>(this.facesInternal, x);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000192A0 File Offset: 0x000174A0
		[CompilerGenerated]
		private IEnumerable<int> <SetSelectedFaces>b__246_0(int x)
		{
			return this.facesInternal[x].distinctIndexesInternal;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000192AF File Offset: 0x000174AF
		[CompilerGenerated]
		private IEnumerable<Edge> <SetSelectedFaces>b__246_1(int x)
		{
			return this.facesInternal[x].edges;
		}

		// Token: 0x0400017F RID: 383
		internal const HideFlags k_MeshFilterHideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;

		// Token: 0x04000180 RID: 384
		private const int k_UVChannelCount = 4;

		// Token: 0x04000181 RID: 385
		internal const int k_MeshFormatVersion = 2;

		// Token: 0x04000182 RID: 386
		internal const int k_MeshFormatVersionSubmeshMaterialRefactor = 1;

		// Token: 0x04000183 RID: 387
		internal const int k_MeshFormatVersionAutoUVScaleOffset = 2;

		// Token: 0x04000184 RID: 388
		public const uint maxVertexCount = 65535U;

		// Token: 0x04000185 RID: 389
		[SerializeField]
		private int m_MeshFormatVersion;

		// Token: 0x04000186 RID: 390
		[SerializeField]
		[FormerlySerializedAs("_quads")]
		private Face[] m_Faces;

		// Token: 0x04000187 RID: 391
		[SerializeField]
		[FormerlySerializedAs("_sharedIndices")]
		[FormerlySerializedAs("m_SharedVertexes")]
		private SharedVertex[] m_SharedVertices;

		// Token: 0x04000188 RID: 392
		[NonSerialized]
		private ProBuilderMesh.CacheValidState m_CacheValid;

		// Token: 0x04000189 RID: 393
		[NonSerialized]
		private Dictionary<int, int> m_SharedVertexLookup;

		// Token: 0x0400018A RID: 394
		[SerializeField]
		[FormerlySerializedAs("_sharedIndicesUV")]
		private SharedVertex[] m_SharedTextures;

		// Token: 0x0400018B RID: 395
		[NonSerialized]
		private Dictionary<int, int> m_SharedTextureLookup;

		// Token: 0x0400018C RID: 396
		[SerializeField]
		[FormerlySerializedAs("_vertices")]
		private Vector3[] m_Positions;

		// Token: 0x0400018D RID: 397
		[SerializeField]
		[FormerlySerializedAs("_uv")]
		private Vector2[] m_Textures0;

		// Token: 0x0400018E RID: 398
		[SerializeField]
		[FormerlySerializedAs("_uv3")]
		private List<Vector4> m_Textures2;

		// Token: 0x0400018F RID: 399
		[SerializeField]
		[FormerlySerializedAs("_uv4")]
		private List<Vector4> m_Textures3;

		// Token: 0x04000190 RID: 400
		[SerializeField]
		[FormerlySerializedAs("_tangents")]
		private Vector4[] m_Tangents;

		// Token: 0x04000191 RID: 401
		[NonSerialized]
		private Vector3[] m_Normals;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		[FormerlySerializedAs("_colors")]
		private Color[] m_Colors;

		// Token: 0x04000193 RID: 403
		[CompilerGenerated]
		private bool <userCollisions>k__BackingField;

		// Token: 0x04000194 RID: 404
		[FormerlySerializedAs("unwrapParameters")]
		[SerializeField]
		private UnwrapParameters m_UnwrapParameters;

		// Token: 0x04000195 RID: 405
		[FormerlySerializedAs("dontDestroyMeshOnDelete")]
		[SerializeField]
		private bool m_PreserveMeshAssetOnDestroy;

		// Token: 0x04000196 RID: 406
		[SerializeField]
		internal string assetGuid;

		// Token: 0x04000197 RID: 407
		[SerializeField]
		private Mesh m_Mesh;

		// Token: 0x04000198 RID: 408
		[NonSerialized]
		private MeshRenderer m_MeshRenderer;

		// Token: 0x04000199 RID: 409
		[NonSerialized]
		private MeshFilter m_MeshFilter;

		// Token: 0x0400019A RID: 410
		internal const ushort k_UnitializedVersionIndex = 0;

		// Token: 0x0400019B RID: 411
		[SerializeField]
		private ushort m_VersionIndex;

		// Token: 0x0400019C RID: 412
		[NonSerialized]
		private ushort m_InstanceVersionIndex;

		// Token: 0x0400019D RID: 413
		[CompilerGenerated]
		private static Action<ProBuilderMesh> meshWillBeDestroyed;

		// Token: 0x0400019E RID: 414
		[CompilerGenerated]
		private static Action<ProBuilderMesh> meshWasInitialized;

		// Token: 0x0400019F RID: 415
		[CompilerGenerated]
		private static Action<ProBuilderMesh> componentWillBeDestroyed;

		// Token: 0x040001A0 RID: 416
		[CompilerGenerated]
		private static Action<ProBuilderMesh> componentHasBeenReset;

		// Token: 0x040001A1 RID: 417
		[CompilerGenerated]
		private static Action<ProBuilderMesh> elementSelectionChanged;

		// Token: 0x040001A2 RID: 418
		private static HashSet<int> s_CachedHashSet = new HashSet<int>();

		// Token: 0x040001A3 RID: 419
		[SerializeField]
		private bool m_IsSelectable = true;

		// Token: 0x040001A4 RID: 420
		[SerializeField]
		private int[] m_SelectedFaces = new int[0];

		// Token: 0x040001A5 RID: 421
		[SerializeField]
		private Edge[] m_SelectedEdges = new Edge[0];

		// Token: 0x040001A6 RID: 422
		[SerializeField]
		private int[] m_SelectedVertices = new int[0];

		// Token: 0x040001A7 RID: 423
		private bool m_SelectedCacheDirty;

		// Token: 0x040001A8 RID: 424
		private int m_SelectedSharedVerticesCount;

		// Token: 0x040001A9 RID: 425
		private int m_SelectedCoincidentVertexCount;

		// Token: 0x040001AA RID: 426
		private HashSet<int> m_SelectedSharedVertices = new HashSet<int>();

		// Token: 0x040001AB RID: 427
		private List<int> m_SelectedCoincidentVertices = new List<int>();

		// Token: 0x0200009A RID: 154
		[Flags]
		private enum CacheValidState : byte
		{
			// Token: 0x040002A6 RID: 678
			SharedVertex = 1,
			// Token: 0x040002A7 RID: 679
			SharedTexture = 2
		}

		// Token: 0x0200009B RID: 155
		internal struct NonVersionedEditScope : IDisposable
		{
			// Token: 0x0600053E RID: 1342 RVA: 0x00035BB4 File Offset: 0x00033DB4
			public NonVersionedEditScope(ProBuilderMesh mesh)
			{
				this.m_Mesh = mesh;
				this.m_VersionIndex = mesh.versionIndex;
				this.m_InstanceVersionIndex = mesh.m_InstanceVersionIndex;
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00035BD5 File Offset: 0x00033DD5
			public void Dispose()
			{
				this.m_Mesh.m_VersionIndex = this.m_VersionIndex;
				this.m_Mesh.m_InstanceVersionIndex = this.m_InstanceVersionIndex;
			}

			// Token: 0x040002A8 RID: 680
			private readonly ProBuilderMesh m_Mesh;

			// Token: 0x040002A9 RID: 681
			private readonly ushort m_VersionIndex;

			// Token: 0x040002AA RID: 682
			private readonly ushort m_InstanceVersionIndex;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000540 RID: 1344 RVA: 0x00035BF9 File Offset: 0x00033DF9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x00035C05 File Offset: 0x00033E05
			public <>c()
			{
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x00035C0D File Offset: 0x00033E0D
			internal Vector2 <SetUVs>b__118_0(Vector4 x)
			{
				return x;
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x00035C15 File Offset: 0x00033E15
			internal Vector2 <SetUVs>b__118_1(Vector4 x)
			{
				return x;
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x00035C1D File Offset: 0x00033E1D
			internal int <get_indexCount>b__126_0(Face x)
			{
				return x.indexesInternal.Length;
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x00035C27 File Offset: 0x00033E27
			internal int <get_triangleCount>b__128_0(Face x)
			{
				return x.indexesInternal.Length;
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x00035C31 File Offset: 0x00033E31
			internal Face <CopyFrom>b__171_0(Face x)
			{
				return new Face(x);
			}

			// Token: 0x040002AB RID: 683
			public static readonly ProBuilderMesh.<>c <>9 = new ProBuilderMesh.<>c();

			// Token: 0x040002AC RID: 684
			public static Func<Vector4, Vector2> <>9__118_0;

			// Token: 0x040002AD RID: 685
			public static Func<Vector4, Vector2> <>9__118_1;

			// Token: 0x040002AE RID: 686
			public static Func<Face, int> <>9__126_0;

			// Token: 0x040002AF RID: 687
			public static Func<Face, int> <>9__128_0;

			// Token: 0x040002B0 RID: 688
			public static Func<Face, Face> <>9__171_0;
		}

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		private sealed class <>c__DisplayClass174_0
		{
			// Token: 0x06000547 RID: 1351 RVA: 0x00035C39 File Offset: 0x00033E39
			public <>c__DisplayClass174_0()
			{
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x00035C41 File Offset: 0x00033E41
			internal bool <GetUnusedTextureGroup>b__0(Face element)
			{
				return element.textureGroup == this.i;
			}

			// Token: 0x040002B1 RID: 689
			public int i;
		}

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		private sealed class <>c__DisplayClass176_0
		{
			// Token: 0x06000549 RID: 1353 RVA: 0x00035C51 File Offset: 0x00033E51
			public <>c__DisplayClass176_0()
			{
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00035C59 File Offset: 0x00033E59
			internal bool <UnusedElementGroup>b__0(Face element)
			{
				return element.elementGroup == this.i;
			}

			// Token: 0x040002B2 RID: 690
			public int i;
		}
	}
}
