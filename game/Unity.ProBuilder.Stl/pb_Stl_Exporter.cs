using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Stl
{
	// Token: 0x02000004 RID: 4
	internal static class pb_Stl_Exporter
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002630 File Offset: 0x00000830
		public static bool Export(string path, GameObject[] gameObjects, FileType type)
		{
			Mesh[] array = pb_Stl_Exporter.CreateWorldSpaceMeshesWithTransforms((from x in gameObjects
			select x.transform).ToArray<Transform>());
			bool result = false;
			if (array != null && array.Length != 0 && !string.IsNullOrEmpty(path))
			{
				result = pb_Stl.WriteFile(path, array, type, true);
			}
			int num = 0;
			while (array != null && num < array.Length)
			{
				Object.DestroyImmediate(array[num]);
				num++;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000026A4 File Offset: 0x000008A4
		private static Mesh[] CreateWorldSpaceMeshesWithTransforms(IList<Transform> transforms)
		{
			if (transforms == null || transforms.Count < 1)
			{
				return null;
			}
			Vector3 a = Vector3.zero;
			for (int i = 0; i < transforms.Count; i++)
			{
				a += transforms[i].position;
			}
			Vector3 position = a / (float)transforms.Count;
			GameObject gameObject = new GameObject();
			gameObject.name = "ROOT";
			gameObject.transform.position = position;
			foreach (Transform transform in transforms)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(transform.parent, false);
				gameObject2.transform.SetParent(gameObject.transform, true);
			}
			gameObject.transform.position = Vector3.zero;
			List<MeshFilter> list = (from x in gameObject.GetComponentsInChildren<MeshFilter>()
			where x.sharedMesh != null
			select x).ToList<MeshFilter>();
			int count = list.Count;
			Mesh[] array = new Mesh[count];
			for (int j = 0; j < count; j++)
			{
				Transform transform2 = list[j].transform;
				Vector3[] vertices = list[j].sharedMesh.vertices;
				Vector3[] normals = list[j].sharedMesh.normals;
				for (int k = 0; k < vertices.Length; k++)
				{
					vertices[k] = transform2.TransformPoint(vertices[k]);
					normals[k] = transform2.TransformDirection(normals[k]);
				}
				array[j] = new Mesh
				{
					name = list[j].name,
					vertices = vertices,
					normals = normals,
					triangles = list[j].sharedMesh.triangles
				};
			}
			Object.DestroyImmediate(gameObject);
			return array;
		}

		// Token: 0x02000007 RID: 7
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000016 RID: 22 RVA: 0x00002ECA File Offset: 0x000010CA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002ED6 File Offset: 0x000010D6
			public <>c()
			{
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002EDE File Offset: 0x000010DE
			internal Transform <Export>b__0_0(GameObject x)
			{
				return x.transform;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x00002EE6 File Offset: 0x000010E6
			internal bool <CreateWorldSpaceMeshesWithTransforms>b__1_0(MeshFilter x)
			{
				return x.sharedMesh != null;
			}

			// Token: 0x0400000F RID: 15
			public static readonly pb_Stl_Exporter.<>c <>9 = new pb_Stl_Exporter.<>c();

			// Token: 0x04000010 RID: 16
			public static Func<GameObject, Transform> <>9__0_0;

			// Token: 0x04000011 RID: 17
			public static Func<MeshFilter, bool> <>9__1_0;
		}
	}
}
