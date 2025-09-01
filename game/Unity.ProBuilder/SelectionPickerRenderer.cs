using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200004C RID: 76
	internal static class SelectionPickerRenderer
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001AB48 File Offset: 0x00018D48
		private static RenderTextureFormat renderTextureFormat
		{
			get
			{
				if (SelectionPickerRenderer.s_Initialized)
				{
					return SelectionPickerRenderer.s_RenderTextureFormat;
				}
				SelectionPickerRenderer.s_Initialized = true;
				for (int i = 0; i < SelectionPickerRenderer.s_PreferredFormats.Length; i++)
				{
					if (SystemInfo.SupportsRenderTextureFormat(SelectionPickerRenderer.s_PreferredFormats[i]))
					{
						SelectionPickerRenderer.s_RenderTextureFormat = SelectionPickerRenderer.s_PreferredFormats[i];
						break;
					}
				}
				return SelectionPickerRenderer.s_RenderTextureFormat;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0001AB9B File Offset: 0x00018D9B
		private static TextureFormat textureFormat
		{
			get
			{
				return TextureFormat.ARGB32;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		private static SelectionPickerRenderer.ISelectionPickerRenderer pickerRenderer
		{
			get
			{
				if (SelectionPickerRenderer.s_PickerRenderer == null)
				{
					SelectionPickerRenderer.ISelectionPickerRenderer selectionPickerRenderer2;
					if (!SelectionPickerRenderer.ShouldUseHDRP())
					{
						SelectionPickerRenderer.ISelectionPickerRenderer selectionPickerRenderer = new SelectionPickerRenderer.SelectionPickerRendererStandard();
						selectionPickerRenderer2 = selectionPickerRenderer;
					}
					else
					{
						SelectionPickerRenderer.ISelectionPickerRenderer selectionPickerRenderer = new SelectionPickerRenderer.SelectionPickerRendererHDRP();
						selectionPickerRenderer2 = selectionPickerRenderer;
					}
					SelectionPickerRenderer.s_PickerRenderer = selectionPickerRenderer2;
				}
				return SelectionPickerRenderer.s_PickerRenderer;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		public static Dictionary<ProBuilderMesh, HashSet<Face>> PickFacesInRect(Camera camera, Rect pickerRect, IList<ProBuilderMesh> selection, int renderTextureWidth = -1, int renderTextureHeight = -1)
		{
			Dictionary<uint, SimpleTuple<ProBuilderMesh, Face>> dictionary;
			Texture2D texture2D = SelectionPickerRenderer.RenderSelectionPickerTexture(camera, selection, out dictionary, renderTextureWidth, renderTextureHeight);
			Color32[] pixels = texture2D.GetPixels32();
			int num = Math.Max(0, Mathf.FloorToInt(pickerRect.x));
			int num2 = Math.Max(0, Mathf.FloorToInt((float)texture2D.height - pickerRect.y - pickerRect.height));
			int width = texture2D.width;
			int height = texture2D.height;
			int num3 = Mathf.FloorToInt(pickerRect.width);
			int num4 = Mathf.FloorToInt(pickerRect.height);
			Object.DestroyImmediate(texture2D);
			Dictionary<ProBuilderMesh, HashSet<Face>> dictionary2 = new Dictionary<ProBuilderMesh, HashSet<Face>>();
			HashSet<Face> hashSet = null;
			HashSet<uint> hashSet2 = new HashSet<uint>();
			for (int i = num2; i < Math.Min(num2 + num4, height); i++)
			{
				for (int j = num; j < Math.Min(num + num3, width); j++)
				{
					uint num5 = SelectionPickerRenderer.DecodeRGBA(pixels[i * width + j]);
					SimpleTuple<ProBuilderMesh, Face> simpleTuple;
					if (hashSet2.Add(num5) && dictionary.TryGetValue(num5, out simpleTuple))
					{
						if (dictionary2.TryGetValue(simpleTuple.item1, out hashSet))
						{
							hashSet.Add(simpleTuple.item2);
						}
						else
						{
							dictionary2.Add(simpleTuple.item1, new HashSet<Face>
							{
								simpleTuple.item2
							});
						}
					}
				}
			}
			return dictionary2;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001AD24 File Offset: 0x00018F24
		public static Dictionary<ProBuilderMesh, HashSet<int>> PickVerticesInRect(Camera camera, Rect pickerRect, IList<ProBuilderMesh> selection, bool doDepthTest, int renderTextureWidth = -1, int renderTextureHeight = -1)
		{
			Dictionary<ProBuilderMesh, HashSet<int>> dictionary = new Dictionary<ProBuilderMesh, HashSet<int>>();
			Dictionary<uint, SimpleTuple<ProBuilderMesh, int>> dictionary2;
			Texture2D texture2D = SelectionPickerRenderer.RenderSelectionPickerTexture(camera, selection, doDepthTest, out dictionary2, renderTextureWidth, renderTextureHeight);
			Color32[] pixels = texture2D.GetPixels32();
			int num = Math.Max(0, Mathf.FloorToInt(pickerRect.x));
			int num2 = Math.Max(0, Mathf.FloorToInt((float)texture2D.height - pickerRect.y - pickerRect.height));
			int width = texture2D.width;
			int height = texture2D.height;
			int num3 = Mathf.FloorToInt(pickerRect.width);
			int num4 = Mathf.FloorToInt(pickerRect.height);
			Object.DestroyImmediate(texture2D);
			HashSet<int> hashSet = null;
			HashSet<uint> hashSet2 = new HashSet<uint>();
			for (int i = num2; i < Math.Min(num2 + num4, height); i++)
			{
				for (int j = num; j < Math.Min(num + num3, width); j++)
				{
					uint num5 = SelectionPickerRenderer.DecodeRGBA(pixels[i * width + j]);
					SimpleTuple<ProBuilderMesh, int> simpleTuple;
					if (hashSet2.Add(num5) && dictionary2.TryGetValue(num5, out simpleTuple))
					{
						if (dictionary.TryGetValue(simpleTuple.item1, out hashSet))
						{
							hashSet.Add(simpleTuple.item2);
						}
						else
						{
							dictionary.Add(simpleTuple.item1, new HashSet<int>
							{
								simpleTuple.item2
							});
						}
					}
				}
			}
			Dictionary<ProBuilderMesh, HashSet<int>> dictionary3 = new Dictionary<ProBuilderMesh, HashSet<int>>();
			foreach (KeyValuePair<ProBuilderMesh, HashSet<int>> keyValuePair in dictionary)
			{
				Vector3[] positions = keyValuePair.Key.positionsInternal;
				SharedVertex[] sharedVertices = keyValuePair.Key.sharedVerticesInternal;
				HashSet<int> hashSet3 = new HashSet<int>(from x in keyValuePair.Value
				select VectorHash.GetHashCode(positions[sharedVertices[x][0]]));
				HashSet<int> hashSet4 = new HashSet<int>();
				int k = 0;
				int num6 = sharedVertices.Length;
				while (k < num6)
				{
					int hashCode = VectorHash.GetHashCode(positions[sharedVertices[k][0]]);
					if (hashSet3.Contains(hashCode))
					{
						hashSet4.Add(k);
					}
					k++;
				}
				dictionary3.Add(keyValuePair.Key, hashSet4);
			}
			return dictionary3;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001AF78 File Offset: 0x00019178
		public static Dictionary<ProBuilderMesh, HashSet<Edge>> PickEdgesInRect(Camera camera, Rect pickerRect, IList<ProBuilderMesh> selection, bool doDepthTest, int renderTextureWidth = -1, int renderTextureHeight = -1)
		{
			Dictionary<ProBuilderMesh, HashSet<Edge>> dictionary = new Dictionary<ProBuilderMesh, HashSet<Edge>>();
			Dictionary<uint, SimpleTuple<ProBuilderMesh, Edge>> dictionary2;
			Texture2D texture2D = SelectionPickerRenderer.RenderSelectionPickerTexture(camera, selection, doDepthTest, out dictionary2, renderTextureWidth, renderTextureHeight);
			Color32[] pixels = texture2D.GetPixels32();
			int num = Math.Max(0, Mathf.FloorToInt(pickerRect.x));
			int num2 = Math.Max(0, Mathf.FloorToInt((float)texture2D.height - pickerRect.y - pickerRect.height));
			int width = texture2D.width;
			int height = texture2D.height;
			int num3 = Mathf.FloorToInt(pickerRect.width);
			int num4 = Mathf.FloorToInt(pickerRect.height);
			Object.DestroyImmediate(texture2D);
			Dictionary<uint, uint> dictionary3 = new Dictionary<uint, uint>();
			for (int i = num2; i < Math.Min(num2 + num4, height); i++)
			{
				for (int j = num; j < Math.Min(num + num3, width); j++)
				{
					uint num5 = SelectionPickerRenderer.DecodeRGBA(pixels[i * width + j]);
					if (num5 != 0U && num5 != 16777215U)
					{
						if (!dictionary3.ContainsKey(num5))
						{
							dictionary3.Add(num5, 1U);
						}
						else
						{
							dictionary3[num5] += 1U;
						}
					}
				}
			}
			foreach (KeyValuePair<uint, uint> keyValuePair in dictionary3)
			{
				SimpleTuple<ProBuilderMesh, Edge> simpleTuple;
				if (keyValuePair.Value > 1U && dictionary2.TryGetValue(keyValuePair.Key, out simpleTuple))
				{
					HashSet<Edge> hashSet = null;
					if (dictionary.TryGetValue(simpleTuple.item1, out hashSet))
					{
						hashSet.Add(simpleTuple.item2);
					}
					else
					{
						dictionary.Add(simpleTuple.item1, new HashSet<Edge>
						{
							simpleTuple.item2
						});
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001B138 File Offset: 0x00019338
		internal static Texture2D RenderSelectionPickerTexture(Camera camera, IList<ProBuilderMesh> selection, out Dictionary<uint, SimpleTuple<ProBuilderMesh, Face>> map, int width = -1, int height = -1)
		{
			GameObject[] array = SelectionPickerRenderer.GenerateFacePickingObjects(selection, out map);
			BuiltinMaterials.facePickerMaterial.SetColor("_Tint", SelectionPickerRenderer.k_Whitef);
			Texture2D result = SelectionPickerRenderer.pickerRenderer.RenderLookupTexture(camera, BuiltinMaterials.selectionPickerShader, "ProBuilderPicker", width, height);
			foreach (GameObject gameObject in array)
			{
				Object.DestroyImmediate(gameObject.GetComponent<MeshFilter>().sharedMesh);
				Object.DestroyImmediate(gameObject);
			}
			return result;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001B1A4 File Offset: 0x000193A4
		internal static Texture2D RenderSelectionPickerTexture(Camera camera, IList<ProBuilderMesh> selection, bool doDepthTest, out Dictionary<uint, SimpleTuple<ProBuilderMesh, int>> map, int width = -1, int height = -1)
		{
			GameObject[] array;
			GameObject[] array2;
			SelectionPickerRenderer.GenerateVertexPickingObjects(selection, doDepthTest, out map, out array, out array2);
			BuiltinMaterials.facePickerMaterial.SetColor("_Tint", SelectionPickerRenderer.k_Blackf);
			Texture2D result = SelectionPickerRenderer.pickerRenderer.RenderLookupTexture(camera, BuiltinMaterials.selectionPickerShader, "ProBuilderPicker", width, height);
			int i = 0;
			int num = array2.Length;
			while (i < num)
			{
				Object.DestroyImmediate(array2[i].GetComponent<MeshFilter>().sharedMesh);
				Object.DestroyImmediate(array2[i]);
				i++;
			}
			if (doDepthTest)
			{
				int j = 0;
				int num2 = array.Length;
				while (j < num2)
				{
					Object.DestroyImmediate(array[j]);
					j++;
				}
			}
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001B23C File Offset: 0x0001943C
		internal static Texture2D RenderSelectionPickerTexture(Camera camera, IList<ProBuilderMesh> selection, bool doDepthTest, out Dictionary<uint, SimpleTuple<ProBuilderMesh, Edge>> map, int width = -1, int height = -1)
		{
			GameObject[] array;
			GameObject[] array2;
			SelectionPickerRenderer.GenerateEdgePickingObjects(selection, doDepthTest, out map, out array, out array2);
			BuiltinMaterials.facePickerMaterial.SetColor("_Tint", SelectionPickerRenderer.k_Blackf);
			Texture2D result = SelectionPickerRenderer.pickerRenderer.RenderLookupTexture(camera, BuiltinMaterials.selectionPickerShader, "ProBuilderPicker", width, height);
			int i = 0;
			int num = array2.Length;
			while (i < num)
			{
				Object.DestroyImmediate(array2[i].GetComponent<MeshFilter>().sharedMesh);
				Object.DestroyImmediate(array2[i]);
				i++;
			}
			if (doDepthTest)
			{
				int j = 0;
				int num2 = array.Length;
				while (j < num2)
				{
					Object.DestroyImmediate(array[j]);
					j++;
				}
			}
			return result;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001B2D4 File Offset: 0x000194D4
		private static GameObject[] GenerateFacePickingObjects(IList<ProBuilderMesh> selection, out Dictionary<uint, SimpleTuple<ProBuilderMesh, Face>> map)
		{
			int count = selection.Count;
			GameObject[] array = new GameObject[count];
			map = new Dictionary<uint, SimpleTuple<ProBuilderMesh, Face>>();
			uint num = 0U;
			for (int i = 0; i < count; i++)
			{
				ProBuilderMesh proBuilderMesh = selection[i];
				Mesh mesh = new Mesh();
				mesh.vertices = proBuilderMesh.positionsInternal;
				mesh.triangles = proBuilderMesh.facesInternal.SelectMany((Face x) => x.indexesInternal).ToArray<int>();
				Color32[] array2 = new Color32[mesh.vertexCount];
				foreach (Face face in proBuilderMesh.facesInternal)
				{
					Color32 color = SelectionPickerRenderer.EncodeRGBA(num++);
					map.Add(SelectionPickerRenderer.DecodeRGBA(color), new SimpleTuple<ProBuilderMesh, Face>(proBuilderMesh, face));
					for (int k = 0; k < face.distinctIndexesInternal.Length; k++)
					{
						array2[face.distinctIndexesInternal[k]] = color;
					}
				}
				mesh.colors32 = array2;
				GameObject gameObject = InternalUtility.MeshGameObjectWithTransform(proBuilderMesh.name + " (Face Depth Test)", proBuilderMesh.transform, mesh, BuiltinMaterials.facePickerMaterial, true);
				array[i] = gameObject;
			}
			return array;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001B414 File Offset: 0x00019614
		private static void GenerateVertexPickingObjects(IList<ProBuilderMesh> selection, bool doDepthTest, out Dictionary<uint, SimpleTuple<ProBuilderMesh, int>> map, out GameObject[] depthObjects, out GameObject[] pickerObjects)
		{
			map = new Dictionary<uint, SimpleTuple<ProBuilderMesh, int>>();
			uint num = 2U;
			int count = selection.Count;
			pickerObjects = new GameObject[count];
			for (int i = 0; i < count; i++)
			{
				ProBuilderMesh proBuilderMesh = selection[i];
				Mesh mesh = SelectionPickerRenderer.BuildVertexMesh(proBuilderMesh, map, ref num);
				GameObject gameObject = InternalUtility.MeshGameObjectWithTransform(proBuilderMesh.name + " (Vertex Billboards)", proBuilderMesh.transform, mesh, BuiltinMaterials.vertexPickerMaterial, true);
				pickerObjects[i] = gameObject;
			}
			if (doDepthTest)
			{
				depthObjects = new GameObject[count];
				for (int j = 0; j < count; j++)
				{
					ProBuilderMesh proBuilderMesh2 = selection[j];
					GameObject gameObject2 = InternalUtility.MeshGameObjectWithTransform(proBuilderMesh2.name + " (Depth Mask)", proBuilderMesh2.transform, proBuilderMesh2.mesh, BuiltinMaterials.facePickerMaterial, true);
					depthObjects[j] = gameObject2;
				}
				return;
			}
			depthObjects = null;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001B4E4 File Offset: 0x000196E4
		private static void GenerateEdgePickingObjects(IList<ProBuilderMesh> selection, bool doDepthTest, out Dictionary<uint, SimpleTuple<ProBuilderMesh, Edge>> map, out GameObject[] depthObjects, out GameObject[] pickerObjects)
		{
			map = new Dictionary<uint, SimpleTuple<ProBuilderMesh, Edge>>();
			uint num = 2U;
			int count = selection.Count;
			pickerObjects = new GameObject[count];
			for (int i = 0; i < count; i++)
			{
				ProBuilderMesh proBuilderMesh = selection[i];
				Mesh mesh = SelectionPickerRenderer.BuildEdgeMesh(proBuilderMesh, map, ref num);
				GameObject gameObject = InternalUtility.MeshGameObjectWithTransform(proBuilderMesh.name + " (Edge Billboards)", proBuilderMesh.transform, mesh, BuiltinMaterials.edgePickerMaterial, true);
				pickerObjects[i] = gameObject;
			}
			if (doDepthTest)
			{
				depthObjects = new GameObject[count];
				for (int j = 0; j < count; j++)
				{
					ProBuilderMesh proBuilderMesh2 = selection[j];
					GameObject gameObject2 = InternalUtility.MeshGameObjectWithTransform(proBuilderMesh2.name + " (Depth Mask)", proBuilderMesh2.transform, proBuilderMesh2.mesh, BuiltinMaterials.facePickerMaterial, true);
					depthObjects[j] = gameObject2;
				}
				return;
			}
			depthObjects = null;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001B5B4 File Offset: 0x000197B4
		private static Mesh BuildVertexMesh(ProBuilderMesh pb, Dictionary<uint, SimpleTuple<ProBuilderMesh, int>> map, ref uint index)
		{
			int num = Math.Min(pb.sharedVerticesInternal.Length, 16382);
			Vector3[] array = new Vector3[num * 4];
			Vector2[] array2 = new Vector2[num * 4];
			Vector2[] array3 = new Vector2[num * 4];
			Color[] array4 = new Color[num * 4];
			int[] array5 = new int[num * 6];
			int num2 = 0;
			int num3 = 0;
			Vector3 up = Vector3.up;
			Vector3 right = Vector3.right;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = pb.positionsInternal[pb.sharedVerticesInternal[i][0]];
				array[num3] = vector;
				array[num3 + 1] = vector;
				array[num3 + 2] = vector;
				array[num3 + 3] = vector;
				array2[num3] = Vector3.zero;
				array2[num3 + 1] = Vector3.right;
				array2[num3 + 2] = Vector3.up;
				array2[num3 + 3] = Vector3.one;
				array3[num3] = -up - right;
				array3[num3 + 1] = -up + right;
				array3[num3 + 2] = up - right;
				array3[num3 + 3] = up + right;
				array5[num2] = num3;
				array5[num2 + 1] = num3 + 1;
				array5[num2 + 2] = num3 + 2;
				array5[num2 + 3] = num3 + 1;
				array5[num2 + 4] = num3 + 3;
				array5[num2 + 5] = num3 + 2;
				Color32 c = SelectionPickerRenderer.EncodeRGBA(index);
				uint num4 = index;
				index = num4 + 1U;
				map.Add(num4, new SimpleTuple<ProBuilderMesh, int>(pb, i));
				array4[num3] = c;
				array4[num3 + 1] = c;
				array4[num3 + 2] = c;
				array4[num3 + 3] = c;
				num3 += 4;
				num2 += 6;
			}
			return new Mesh
			{
				name = "Vertex Billboard",
				vertices = array,
				uv = array2,
				uv2 = array3,
				colors = array4,
				triangles = array5
			};
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001B818 File Offset: 0x00019A18
		private static Mesh BuildEdgeMesh(ProBuilderMesh pb, Dictionary<uint, SimpleTuple<ProBuilderMesh, Edge>> map, ref uint index)
		{
			int num = 0;
			int faceCount = pb.faceCount;
			for (int i = 0; i < faceCount; i++)
			{
				num += pb.facesInternal[i].edgesInternal.Length;
			}
			int num2 = Math.Min(num, 32766);
			Vector3[] array = new Vector3[num2 * 2];
			Color32[] array2 = new Color32[num2 * 2];
			int[] array3 = new int[num2 * 2];
			int num3 = 0;
			int num4 = 0;
			while (num4 < faceCount && num3 < num2)
			{
				int num5 = 0;
				while (num5 < pb.facesInternal[num4].edgesInternal.Length && num3 < num2)
				{
					Edge edge = pb.facesInternal[num4].edgesInternal[num5];
					Vector3 vector = pb.positionsInternal[edge.a];
					Vector3 vector2 = pb.positionsInternal[edge.b];
					int num6 = num3 * 2;
					array[num6] = vector;
					array[num6 + 1] = vector2;
					Color32 color = SelectionPickerRenderer.EncodeRGBA(index);
					uint num7 = index;
					index = num7 + 1U;
					map.Add(num7, new SimpleTuple<ProBuilderMesh, Edge>(pb, edge));
					array2[num6] = color;
					array2[num6 + 1] = color;
					array3[num6] = num6;
					array3[num6 + 1] = num6 + 1;
					num3++;
					num5++;
				}
				num4++;
			}
			Mesh mesh = new Mesh();
			mesh.name = "Edge Billboard";
			mesh.vertices = array;
			mesh.colors32 = array2;
			mesh.subMeshCount = 1;
			mesh.SetIndices(array3, MeshTopology.Lines, 0);
			return mesh;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001B9A0 File Offset: 0x00019BA0
		public static uint DecodeRGBA(Color32 color)
		{
			uint r = (uint)color.r;
			uint g = (uint)color.g;
			uint b = (uint)color.b;
			if (BitConverter.IsLittleEndian)
			{
				return r << 16 | g << 8 | b;
			}
			return r << 24 | g << 16 | b << 8;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		public static Color32 EncodeRGBA(uint hash)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new Color32((byte)(hash >> 16 & 255U), (byte)(hash >> 8 & 255U), (byte)(hash & 255U), byte.MaxValue);
			}
			return new Color32((byte)(hash >> 24 & 255U), (byte)(hash >> 16 & 255U), (byte)(hash >> 8 & 255U), byte.MaxValue);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001BA4A File Offset: 0x00019C4A
		private static bool ShouldUseHDRP()
		{
			return false;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001BA50 File Offset: 0x00019C50
		// Note: this type is marked as 'beforefieldinit'.
		static SelectionPickerRenderer()
		{
		}

		// Token: 0x040001B8 RID: 440
		private const string k_FacePickerOcclusionTintUniform = "_Tint";

		// Token: 0x040001B9 RID: 441
		private static readonly Color k_Blackf = new Color(0f, 0f, 0f, 1f);

		// Token: 0x040001BA RID: 442
		private static readonly Color k_Whitef = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040001BB RID: 443
		private const uint k_PickerHashNone = 0U;

		// Token: 0x040001BC RID: 444
		private const uint k_PickerHashMin = 1U;

		// Token: 0x040001BD RID: 445
		private const uint k_PickerHashMax = 16777215U;

		// Token: 0x040001BE RID: 446
		private const uint k_MinEdgePixelsForValidSelection = 1U;

		// Token: 0x040001BF RID: 447
		private static bool s_Initialized = false;

		// Token: 0x040001C0 RID: 448
		private static SelectionPickerRenderer.ISelectionPickerRenderer s_PickerRenderer = null;

		// Token: 0x040001C1 RID: 449
		private static RenderTextureFormat s_RenderTextureFormat = RenderTextureFormat.Default;

		// Token: 0x040001C2 RID: 450
		private static RenderTextureFormat[] s_PreferredFormats = new RenderTextureFormat[]
		{
			RenderTextureFormat.ARGB32,
			RenderTextureFormat.ARGBFloat
		};

		// Token: 0x020000A0 RID: 160
		internal interface ISelectionPickerRenderer
		{
			// Token: 0x0600054F RID: 1359
			Texture2D RenderLookupTexture(Camera camera, Shader shader, string tag, int width, int height);
		}

		// Token: 0x020000A1 RID: 161
		internal class SelectionPickerRendererHDRP : SelectionPickerRenderer.ISelectionPickerRenderer
		{
			// Token: 0x06000550 RID: 1360 RVA: 0x00035C9B File Offset: 0x00033E9B
			public Texture2D RenderLookupTexture(Camera camera, Shader shader, string tag, int width = -1, int height = -1)
			{
				return null;
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x00035C9E File Offset: 0x00033E9E
			public SelectionPickerRendererHDRP()
			{
			}
		}

		// Token: 0x020000A2 RID: 162
		internal class SelectionPickerRendererStandard : SelectionPickerRenderer.ISelectionPickerRenderer
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x00035CA8 File Offset: 0x00033EA8
			public Texture2D RenderLookupTexture(Camera camera, Shader shader, string tag, int width = -1, int height = -1)
			{
				bool flag = width < 0 || height < 0;
				int num = flag ? ((int)camera.pixelRect.width) : width;
				int num2 = flag ? ((int)camera.pixelRect.height) : height;
				GameObject gameObject = new GameObject();
				Camera camera2 = gameObject.AddComponent<Camera>();
				camera2.CopyFrom(camera);
				camera2.renderingPath = RenderingPath.Forward;
				camera2.enabled = false;
				camera2.clearFlags = CameraClearFlags.Color;
				camera2.backgroundColor = Color.white;
				camera2.allowHDR = false;
				camera2.allowMSAA = false;
				camera2.forceIntoRenderTexture = true;
				RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor
				{
					width = num,
					height = num2,
					colorFormat = SelectionPickerRenderer.renderTextureFormat,
					autoGenerateMips = false,
					depthBufferBits = 16,
					dimension = TextureDimension.Tex2D,
					enableRandomWrite = false,
					memoryless = RenderTextureMemoryless.None,
					sRGB = false,
					useMipMap = false,
					volumeDepth = 1,
					msaaSamples = 1
				});
				RenderTexture active = RenderTexture.active;
				camera2.targetTexture = temporary;
				RenderTexture.active = temporary;
				RenderPipelineAsset renderPipelineAsset = GraphicsSettings.renderPipelineAsset;
				RenderPipelineAsset renderPipeline = QualitySettings.renderPipeline;
				GraphicsSettings.renderPipelineAsset = null;
				QualitySettings.renderPipeline = null;
				camera2.RenderWithShader(shader, tag);
				GraphicsSettings.renderPipelineAsset = renderPipelineAsset;
				QualitySettings.renderPipeline = renderPipeline;
				Texture2D texture2D = new Texture2D(num, num2, SelectionPickerRenderer.textureFormat, false, false);
				texture2D.ReadPixels(new Rect(0f, 0f, (float)num, (float)num2), 0, 0);
				texture2D.Apply();
				RenderTexture.active = active;
				RenderTexture.ReleaseTemporary(temporary);
				Object.DestroyImmediate(gameObject);
				return texture2D;
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x00035E35 File Offset: 0x00034035
			public SelectionPickerRendererStandard()
			{
			}
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06000554 RID: 1364 RVA: 0x00035E3D File Offset: 0x0003403D
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x00035E45 File Offset: 0x00034045
			internal int <PickVerticesInRect>b__0(int x)
			{
				return VectorHash.GetHashCode(this.positions[this.sharedVertices[x][0]]);
			}

			// Token: 0x040002B6 RID: 694
			public Vector3[] positions;

			// Token: 0x040002B7 RID: 695
			public SharedVertex[] sharedVertices;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000556 RID: 1366 RVA: 0x00035E65 File Offset: 0x00034065
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x00035E71 File Offset: 0x00034071
			public <>c()
			{
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x00035E79 File Offset: 0x00034079
			internal IEnumerable<int> <GenerateFacePickingObjects>b__24_0(Face x)
			{
				return x.indexesInternal;
			}

			// Token: 0x040002B8 RID: 696
			public static readonly SelectionPickerRenderer.<>c <>9 = new SelectionPickerRenderer.<>c();

			// Token: 0x040002B9 RID: 697
			public static Func<Face, IEnumerable<int>> <>9__24_0;
		}
	}
}
