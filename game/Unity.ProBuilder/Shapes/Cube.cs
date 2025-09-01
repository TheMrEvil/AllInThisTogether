using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x0200006A RID: 106
	[Shape("Cube")]
	public class Cube : Shape
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0002546E File Offset: 0x0002366E
		public override void CopyShape(Shape shape)
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00025470 File Offset: 0x00023670
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			mesh.Clear();
			Vector3[] array = new Vector3[Cube.k_CubeTriangles.Length];
			for (int i = 0; i < Cube.k_CubeTriangles.Length; i++)
			{
				array[i] = rotation * Vector3.Scale(Cube.k_CubeVertices[Cube.k_CubeTriangles[i]], size.Abs());
			}
			mesh.GeometryWithPoints(array);
			return mesh.mesh.bounds;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000254DD File Offset: 0x000236DD
		public Cube()
		{
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000254E8 File Offset: 0x000236E8
		// Note: this type is marked as 'beforefieldinit'.
		static Cube()
		{
		}

		// Token: 0x04000236 RID: 566
		private static readonly Vector3[] k_CubeVertices = new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f)
		};

		// Token: 0x04000237 RID: 567
		private static readonly int[] k_CubeTriangles = new int[]
		{
			0,
			1,
			4,
			5,
			1,
			2,
			5,
			6,
			2,
			3,
			6,
			7,
			3,
			0,
			7,
			4,
			4,
			5,
			7,
			6,
			3,
			2,
			0,
			1
		};
	}
}
