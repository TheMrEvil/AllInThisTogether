using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000073 RID: 115
	[Shape("Sphere")]
	public class Sphere : Shape
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x000272D8 File Offset: 0x000254D8
		public override void CopyShape(Shape shape)
		{
			if (shape is Sphere)
			{
				Sphere sphere = (Sphere)shape;
				this.m_Subdivisions = sphere.m_Subdivisions;
				this.m_BottomMostVertexIndex = sphere.m_BottomMostVertexIndex;
				this.m_Smooth = sphere.m_Smooth;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00027318 File Offset: 0x00025518
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			return mesh.mesh.bounds;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0002732C File Offset: 0x0002552C
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			float num = 0.5f;
			Vector3[] array = new Vector3[Sphere.k_IcosphereTriangles.Length];
			for (int i = 0; i < Sphere.k_IcosphereTriangles.Length; i += 3)
			{
				array[i] = Sphere.k_IcosphereVertices[Sphere.k_IcosphereTriangles[i]].normalized * num;
				array[i + 1] = Sphere.k_IcosphereVertices[Sphere.k_IcosphereTriangles[i + 1]].normalized * num;
				array[i + 2] = Sphere.k_IcosphereVertices[Sphere.k_IcosphereTriangles[i + 2]].normalized * num;
			}
			for (int j = 0; j < this.m_Subdivisions; j++)
			{
				array = Sphere.SubdivideIcosahedron(array, num);
			}
			Face[] array2 = new Face[array.Length / 3];
			Vector3 vector = Vector3.positiveInfinity;
			for (int k = 0; k < array.Length; k += 3)
			{
				array2[k / 3] = new Face(new int[]
				{
					k,
					k + 1,
					k + 2
				});
				array2[k / 3].smoothingGroup = (this.m_Smooth ? 1 : 0);
				array2[k / 3].manualUV = false;
				for (int l = 0; l < array2[k / 3].indexes.Count; l++)
				{
					int num2 = array2[k / 3].indexes[l];
					if (array[num2].y < vector.y)
					{
						vector = array[num2];
						this.m_BottomMostVertexIndex = num2;
					}
				}
			}
			for (int m = 0; m < array2.Length; m++)
			{
				ProjectionAxis projectionAxis = Projection.VectorToProjectionAxis(Math.Normal(array[array2[m].indexesInternal[0]], array[array2[m].indexesInternal[1]], array[array2[m].indexesInternal[2]]));
				if (projectionAxis == ProjectionAxis.X)
				{
					array2[m].textureGroup = 2;
				}
				else if (projectionAxis == ProjectionAxis.Y)
				{
					array2[m].textureGroup = 3;
				}
				else if (projectionAxis == ProjectionAxis.Z)
				{
					array2[m].textureGroup = 4;
				}
				else if (projectionAxis == ProjectionAxis.XNegative)
				{
					array2[m].textureGroup = 5;
				}
				else if (projectionAxis == ProjectionAxis.YNegative)
				{
					array2[m].textureGroup = 6;
				}
				else if (projectionAxis == ProjectionAxis.ZNegative)
				{
					array2[m].textureGroup = 7;
				}
			}
			mesh.unwrapParameters = new UnwrapParameters
			{
				packMargin = 30f
			};
			mesh.RebuildWithPositionsAndFaces(array, array2);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000275B0 File Offset: 0x000257B0
		private static Vector3[] SubdivideIcosahedron(Vector3[] vertices, float radius)
		{
			Vector3[] array = new Vector3[vertices.Length * 4];
			int num = 0;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			Vector3 vector4 = Vector3.zero;
			Vector3 vector5 = Vector3.zero;
			Vector3 vector6 = Vector3.zero;
			for (int i = 0; i < vertices.Length; i += 3)
			{
				vector = vertices[i];
				vector3 = vertices[i + 1];
				vector6 = vertices[i + 2];
				vector2 = ((vector + vector3) * 0.5f).normalized * radius;
				vector4 = ((vector + vector6) * 0.5f).normalized * radius;
				vector5 = ((vector3 + vector6) * 0.5f).normalized * radius;
				array[num++] = vector;
				array[num++] = vector2;
				array[num++] = vector4;
				array[num++] = vector2;
				array[num++] = vector3;
				array[num++] = vector5;
				array[num++] = vector2;
				array[num++] = vector5;
				array[num++] = vector4;
				array[num++] = vector4;
				array[num++] = vector5;
				array[num++] = vector6;
			}
			return array;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0002772D File Offset: 0x0002592D
		public Sphere()
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00027744 File Offset: 0x00025944
		// Note: this type is marked as 'beforefieldinit'.
		static Sphere()
		{
		}

		// Token: 0x0400024D RID: 589
		private static readonly Vector3[] k_IcosphereVertices = new Vector3[]
		{
			new Vector3(-1f, 1.618034f, 0f),
			new Vector3(1f, 1.618034f, 0f),
			new Vector3(-1f, -1.618034f, 0f),
			new Vector3(1f, -1.618034f, 0f),
			new Vector3(0f, -1f, 1.618034f),
			new Vector3(0f, 1f, 1.618034f),
			new Vector3(0f, -1f, -1.618034f),
			new Vector3(0f, 1f, -1.618034f),
			new Vector3(1.618034f, 0f, -1f),
			new Vector3(1.618034f, 0f, 1f),
			new Vector3(-1.618034f, 0f, -1f),
			new Vector3(-1.618034f, 0f, 1f)
		};

		// Token: 0x0400024E RID: 590
		private static readonly int[] k_IcosphereTriangles = new int[]
		{
			0,
			11,
			5,
			0,
			5,
			1,
			0,
			1,
			7,
			0,
			7,
			10,
			0,
			10,
			11,
			1,
			5,
			9,
			5,
			11,
			4,
			11,
			10,
			2,
			10,
			7,
			6,
			7,
			1,
			8,
			3,
			9,
			4,
			3,
			4,
			2,
			3,
			2,
			6,
			3,
			6,
			8,
			3,
			8,
			9,
			4,
			9,
			5,
			2,
			4,
			11,
			6,
			2,
			10,
			8,
			6,
			7,
			9,
			8,
			1
		};

		// Token: 0x0400024F RID: 591
		[Range(1f, 5f)]
		[SerializeField]
		private int m_Subdivisions = 3;

		// Token: 0x04000250 RID: 592
		private int m_BottomMostVertexIndex;

		// Token: 0x04000251 RID: 593
		[SerializeField]
		private bool m_Smooth = true;
	}
}
