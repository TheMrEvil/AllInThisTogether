using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine.ProBuilder.Stl
{
	// Token: 0x02000003 RID: 3
	internal static class pb_Stl
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool WriteFile(string path, Mesh mesh, FileType type = FileType.Ascii, bool convertToRightHandedCoordinates = true)
		{
			return pb_Stl.WriteFile(path, new Mesh[]
			{
				mesh
			}, type, convertToRightHandedCoordinates);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public static bool WriteFile(string path, IList<Mesh> meshes, FileType type = FileType.Ascii, bool convertToRightHandedCoordinates = true)
		{
			try
			{
				if (type == FileType.Binary)
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create), new ASCIIEncoding()))
					{
						binaryWriter.Write(new byte[80]);
						uint value = (uint)(meshes.Sum((Mesh x) => x.triangles.Length) / 3);
						binaryWriter.Write(value);
						foreach (Mesh mesh in meshes)
						{
							Vector3[] array = convertToRightHandedCoordinates ? pb_Stl.Left2Right(mesh.vertices) : mesh.vertices;
							Vector3[] array2 = convertToRightHandedCoordinates ? pb_Stl.Left2Right(mesh.normals) : mesh.normals;
							int[] triangles = mesh.triangles;
							int num = triangles.Length;
							if (convertToRightHandedCoordinates)
							{
								Array.Reverse<int>(triangles);
							}
							for (int i = 0; i < num; i += 3)
							{
								int num2 = triangles[i];
								int num3 = triangles[i + 1];
								int num4 = triangles[i + 2];
								Vector3 vector = pb_Stl.AvgNrm(array2[num2], array2[num3], array2[num4]);
								binaryWriter.Write(vector.x);
								binaryWriter.Write(vector.y);
								binaryWriter.Write(vector.z);
								binaryWriter.Write(array[num2].x);
								binaryWriter.Write(array[num2].y);
								binaryWriter.Write(array[num2].z);
								binaryWriter.Write(array[num3].x);
								binaryWriter.Write(array[num3].y);
								binaryWriter.Write(array[num3].z);
								binaryWriter.Write(array[num4].x);
								binaryWriter.Write(array[num4].y);
								binaryWriter.Write(array[num4].z);
								binaryWriter.Write(0);
							}
						}
						goto IL_21C;
					}
				}
				string contents = pb_Stl.WriteString(meshes, true);
				File.WriteAllText(path, contents);
				IL_21C:;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022F0 File Offset: 0x000004F0
		public static string WriteString(Mesh mesh, bool convertToRightHandedCoordinates = true)
		{
			return pb_Stl.WriteString(new Mesh[]
			{
				mesh
			}, convertToRightHandedCoordinates);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002304 File Offset: 0x00000504
		public static string WriteString(IList<Mesh> meshes, bool convertToRightHandedCoordinates = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string arg = (meshes.Count == 1) ? meshes[0].name : "Composite Mesh";
			stringBuilder.AppendLine(string.Format("solid {0}", arg));
			foreach (Mesh mesh in meshes)
			{
				Vector3[] array = convertToRightHandedCoordinates ? pb_Stl.Left2Right(mesh.vertices) : mesh.vertices;
				Vector3[] array2 = convertToRightHandedCoordinates ? pb_Stl.Left2Right(mesh.normals) : mesh.normals;
				int[] triangles = mesh.triangles;
				if (convertToRightHandedCoordinates)
				{
					Array.Reverse<int>(triangles);
				}
				int num = triangles.Length;
				for (int i = 0; i < num; i += 3)
				{
					int num2 = triangles[i];
					int num3 = triangles[i + 1];
					int num4 = triangles[i + 2];
					Vector3 vector = pb_Stl.AvgNrm(array2[num2], array2[num3], array2[num4]);
					stringBuilder.AppendLine(string.Format("facet normal {0} {1} {2}", vector.x, vector.y, vector.z));
					stringBuilder.AppendLine("outer loop");
					stringBuilder.AppendLine(string.Format("\tvertex {0} {1} {2}", array[num2].x, array[num2].y, array[num2].z));
					stringBuilder.AppendLine(string.Format("\tvertex {0} {1} {2}", array[num3].x, array[num3].y, array[num3].z));
					stringBuilder.AppendLine(string.Format("\tvertex {0} {1} {2}", array[num4].x, array[num4].y, array[num4].z));
					stringBuilder.AppendLine("endloop");
					stringBuilder.AppendLine("endfacet");
				}
			}
			stringBuilder.AppendLine(string.Format("endsolid {0}", arg));
			return stringBuilder.ToString();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002578 File Offset: 0x00000778
		private static Vector3[] Left2Right(Vector3[] v)
		{
			Vector3[] array = new Vector3[v.Length];
			for (int i = 0; i < v.Length; i++)
			{
				array[i] = new Vector3(v[i].z, -v[i].x, v[i].y);
			}
			return array;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025D0 File Offset: 0x000007D0
		private static Vector3 AvgNrm(Vector3 a, Vector3 b, Vector3 c)
		{
			return new Vector3((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f, (a.z + b.z + c.z) / 3f);
		}

		// Token: 0x02000006 RID: 6
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000013 RID: 19 RVA: 0x00002EAC File Offset: 0x000010AC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000014 RID: 20 RVA: 0x00002EB8 File Offset: 0x000010B8
			public <>c()
			{
			}

			// Token: 0x06000015 RID: 21 RVA: 0x00002EC0 File Offset: 0x000010C0
			internal int <WriteFile>b__1_0(Mesh x)
			{
				return x.triangles.Length;
			}

			// Token: 0x0400000D RID: 13
			public static readonly pb_Stl.<>c <>9 = new pb_Stl.<>c();

			// Token: 0x0400000E RID: 14
			public static Func<Mesh, int> <>9__1_0;
		}
	}
}
