using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000074 RID: 116
	[Shape("Sprite")]
	public class Sprite : Shape
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x000278BB File Offset: 0x00025ABB
		public override void CopyShape(Shape shape)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000278C0 File Offset: 0x00025AC0
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = size.Abs();
			if (vector.x < 1E-45f || vector.z < 1E-45f)
			{
				mesh.Clear();
				if (mesh.mesh != null)
				{
					mesh.mesh.Clear();
				}
				return default(Bounds);
			}
			float x = vector.x;
			float z = vector.z;
			Vector2[] array = new Vector2[4];
			Vector3[] array2 = new Vector3[4];
			Face[] array3 = new Face[1];
			float x2 = -(x / 2f);
			float x3 = x / 2f;
			float y = -(z / 2f);
			float y2 = z / 2f;
			array[0] = new Vector2(x2, y);
			array[1] = new Vector2(x3, y);
			array[2] = new Vector2(x2, y2);
			array[3] = new Vector2(x3, y2);
			array3[0] = new Face(new int[]
			{
				0,
				1,
				2,
				1,
				3,
				2
			});
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new Vector3(array[i].y, 0f, array[i].x);
			}
			mesh.RebuildWithPositionsAndFaces(array2, array3);
			return mesh.mesh.bounds;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00027A0C File Offset: 0x00025C0C
		public Sprite()
		{
		}
	}
}
