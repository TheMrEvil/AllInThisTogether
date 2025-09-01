using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A2 RID: 162
	[Preserve]
	[ES3Properties(new string[]
	{
		"bounds",
		"subMeshCount",
		"boneWeights",
		"bindposes",
		"vertices",
		"normals",
		"tangents",
		"uv",
		"uv2",
		"uv3",
		"uv4",
		"colors32",
		"triangles",
		"subMeshes"
	})]
	public class ES3Type_Mesh : ES3UnityObjectType
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00013ED4 File Offset: 0x000120D4
		public ES3Type_Mesh() : base(typeof(Mesh))
		{
			ES3Type_Mesh.Instance = this;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013EEC File Offset: 0x000120EC
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Mesh mesh = (Mesh)obj;
			if (!mesh.isReadable)
			{
				ES3Debug.LogWarning("Easy Save cannot save the vertices for this Mesh because it is not marked as readable, so it will be stored by reference. To save the vertex data for this Mesh, check the 'Read/Write Enabled' checkbox in its Import Settings.", mesh, 0);
				return;
			}
			writer.WriteProperty("indexFormat", mesh.indexFormat);
			writer.WriteProperty("name", mesh.name);
			writer.WriteProperty("vertices", mesh.vertices, ES3Type_Vector3Array.Instance);
			writer.WriteProperty("triangles", mesh.triangles, ES3Type_intArray.Instance);
			writer.WriteProperty("bounds", mesh.bounds, ES3Type_Bounds.Instance);
			writer.WriteProperty("boneWeights", mesh.boneWeights, ES3Type_BoneWeightArray.Instance);
			writer.WriteProperty("bindposes", mesh.bindposes, ES3Type_Matrix4x4Array.Instance);
			writer.WriteProperty("normals", mesh.normals, ES3Type_Vector3Array.Instance);
			writer.WriteProperty("tangents", mesh.tangents, ES3Type_Vector4Array.Instance);
			writer.WriteProperty("uv", mesh.uv, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv2", mesh.uv2, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv3", mesh.uv3, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("uv4", mesh.uv4, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("colors32", mesh.colors32, ES3Type_Color32Array.Instance);
			writer.WriteProperty("subMeshCount", mesh.subMeshCount, ES3Type_int.Instance);
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				writer.WriteProperty("subMesh" + i.ToString(), mesh.GetTriangles(i), ES3Type_intArray.Instance);
			}
			writer.WriteProperty("blendShapeCount", mesh.blendShapeCount);
			for (int j = 0; j < mesh.blendShapeCount; j++)
			{
				writer.WriteProperty("GetBlendShapeName" + j.ToString(), mesh.GetBlendShapeName(j));
				writer.WriteProperty("GetBlendShapeFrameCount" + j.ToString(), mesh.GetBlendShapeFrameCount(j));
				for (int k = 0; k < mesh.GetBlendShapeFrameCount(j); k++)
				{
					Vector3[] array = new Vector3[mesh.vertexCount];
					Vector3[] array2 = new Vector3[mesh.vertexCount];
					Vector3[] array3 = new Vector3[mesh.vertexCount];
					mesh.GetBlendShapeFrameVertices(j, k, array, array2, array3);
					writer.WriteProperty("blendShapeDeltaVertices" + j.ToString() + "_" + k.ToString(), array);
					writer.WriteProperty("blendShapeDeltaNormals" + j.ToString() + "_" + k.ToString(), array2);
					writer.WriteProperty("blendShapeDeltaTangents" + j.ToString() + "_" + k.ToString(), array3);
					writer.WriteProperty("blendShapeFrameWeight" + j.ToString() + "_" + k.ToString(), mesh.GetBlendShapeFrameWeight(j, k));
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000141F4 File Offset: 0x000123F4
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Mesh mesh = new Mesh();
			this.ReadUnityObject<T>(reader, mesh);
			return mesh;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00014210 File Offset: 0x00012410
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			Mesh mesh = (Mesh)obj;
			if (mesh == null)
			{
				return;
			}
			if (!mesh.isReadable)
			{
				ES3Debug.LogWarning("Easy Save cannot load the vertices for this Mesh because it is not marked as readable, so it will be loaded by reference. To load the vertex data for this Mesh, check the 'Read/Write Enabled' checkbox in its Import Settings.", mesh, 0);
			}
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!mesh.isReadable)
				{
					reader.Skip();
				}
				else
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 3148426828U)
					{
						if (num <= 1229132946U)
						{
							if (num <= 502968136U)
							{
								if (num != 247908339U)
								{
									if (num == 502968136U)
									{
										if (text == "boneWeights")
										{
											mesh.boneWeights = reader.Read<BoneWeight[]>(ES3Type_BoneWeightArray.Instance);
											continue;
										}
									}
								}
								else if (text == "normals")
								{
									mesh.normals = reader.Read<Vector3[]>(ES3Type_Vector3Array.Instance);
									continue;
								}
							}
							else if (num != 1029089800U)
							{
								if (num == 1229132946U)
								{
									if (text == "uv")
									{
										mesh.uv = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
										continue;
									}
								}
							}
							else if (text == "bounds")
							{
								mesh.bounds = reader.Read<Bounds>(ES3Type_Bounds.Instance);
								continue;
							}
						}
						else if (num <= 2323447011U)
						{
							if (num != 2082523534U)
							{
								if (num == 2323447011U)
								{
									if (text == "tangents")
									{
										mesh.tangents = reader.Read<Vector4[]>(ES3Type_Vector4Array.Instance);
										continue;
									}
								}
							}
							else if (text == "vertices")
							{
								mesh.vertices = reader.Read<Vector3[]>(ES3Type_Vector3Array.Instance);
								continue;
							}
						}
						else if (num != 2369371622U)
						{
							if (num == 3148426828U)
							{
								if (text == "blendShapeCount")
								{
									mesh.ClearBlendShapes();
									int num2 = reader.Read<int>(ES3Type_int.Instance);
									for (int i = 0; i < num2; i++)
									{
										string shapeName = reader.ReadProperty<string>();
										int num3 = reader.ReadProperty<int>();
										for (int j = 0; j < num3; j++)
										{
											Vector3[] deltaVertices = reader.ReadProperty<Vector3[]>();
											Vector3[] deltaNormals = reader.ReadProperty<Vector3[]>();
											Vector3[] deltaTangents = reader.ReadProperty<Vector3[]>();
											float frameWeight = reader.ReadProperty<float>();
											mesh.AddBlendShapeFrame(shapeName, frameWeight, deltaVertices, deltaNormals, deltaTangents);
										}
									}
									continue;
								}
							}
						}
						else if (text == "name")
						{
							mesh.name = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (num <= 3905652020U)
					{
						if (num <= 3634721602U)
						{
							if (num != 3226776912U)
							{
								if (num == 3634721602U)
								{
									if (text == "triangles")
									{
										mesh.triangles = reader.Read<int[]>(ES3Type_intArray.Instance);
										continue;
									}
								}
							}
							else if (text == "bindposes")
							{
								mesh.bindposes = reader.Read<Matrix4x4[]>(ES3Type_Matrix4x4Array.Instance);
								continue;
							}
						}
						else if (num != 3685293843U)
						{
							if (num == 3905652020U)
							{
								if (text == "indexFormat")
								{
									mesh.indexFormat = reader.Read<IndexFormat>();
									continue;
								}
							}
						}
						else if (text == "subMeshCount")
						{
							mesh.subMeshCount = reader.Read<int>(ES3Type_int.Instance);
							for (int k = 0; k < mesh.subMeshCount; k++)
							{
								mesh.SetTriangles(reader.ReadProperty<int[]>(ES3Type_intArray.Instance), k);
							}
							continue;
						}
					}
					else if (num <= 4103698400U)
					{
						if (num != 4074257916U)
						{
							if (num == 4103698400U)
							{
								if (text == "uv2")
								{
									mesh.uv2 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
									continue;
								}
							}
						}
						else if (text == "colors32")
						{
							mesh.colors32 = reader.Read<Color32[]>(ES3Type_Color32Array.Instance);
							continue;
						}
					}
					else if (num != 4120476019U)
					{
						if (num == 4204364114U)
						{
							if (text == "uv4")
							{
								mesh.uv4 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
								continue;
							}
						}
					}
					else if (text == "uv3")
					{
						mesh.uv3 = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
						continue;
					}
					reader.Skip();
				}
			}
		}

		// Token: 0x040000F5 RID: 245
		public static ES3Type Instance;
	}
}
