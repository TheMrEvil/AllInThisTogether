using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000069 RID: 105
	[Shape("Cone")]
	public class Cone : Shape
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x0002504C File Offset: 0x0002324C
		public override void CopyShape(Shape shape)
		{
			if (shape is Cone)
			{
				Cone cone = (Cone)shape;
				this.m_NumberOfSides = cone.m_NumberOfSides;
				this.m_Radius = cone.m_Radius;
				this.m_Smooth = cone.m_Smooth;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0002508C File Offset: 0x0002328C
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			Vector3 vector = rotation * Vector3.up;
			vector = vector.Abs();
			Vector3 size2 = mesh.mesh.bounds.size;
			size2.x = Mathf.Lerp(this.m_Radius * 2f, size2.x, vector.x);
			size2.y = Mathf.Lerp(this.m_Radius * 2f, size2.y, vector.y);
			size2.z = Mathf.Lerp(this.m_Radius * 2f, size2.z, vector.z);
			bounds.size = size2;
			return bounds;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00025138 File Offset: 0x00023338
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = size.Abs();
			this.m_Radius = Math.Min(vector.x, vector.z);
			float y = vector.y;
			int numberOfSides = this.m_NumberOfSides;
			Vector3[] array = new Vector3[numberOfSides];
			for (int i = 0; i < numberOfSides; i++)
			{
				Vector2 vector2 = Math.PointInCircumference(this.m_Radius, (float)i * (360f / (float)numberOfSides), Vector2.zero);
				array[i] = new Vector3(vector2.x, -y / 2f, vector2.y);
			}
			List<Vector3> list = new List<Vector3>();
			List<Face> list2 = new List<Face>();
			for (int j = 0; j < numberOfSides; j++)
			{
				list.Add(array[j]);
				list.Add((j < numberOfSides - 1) ? array[j + 1] : array[0]);
				list.Add(Vector3.up * y / 2f);
				list.Add(array[j]);
				list.Add((j < numberOfSides - 1) ? array[j + 1] : array[0]);
				list.Add(Vector3.down * y / 2f);
			}
			List<Face> list3 = new List<Face>();
			for (int k = 0; k < numberOfSides * 6; k += 6)
			{
				Face face = new Face(new int[]
				{
					k + 2,
					k + 1,
					k
				});
				face.smoothingGroup = (this.m_Smooth ? 1 : 0);
				list2.Add(face);
				list3.Add(face);
				list2.Add(new Face(new int[]
				{
					k + 3,
					k + 4,
					k + 5
				}));
			}
			Vector3 b = size.Sign();
			for (int l = 0; l < list.Count; l++)
			{
				list[l] = Vector3.Scale(rotation * list[l], b);
			}
			if (Mathf.Sign(size.x) * Mathf.Sign(size.y) * Mathf.Sign(size.z) < 0f)
			{
				foreach (Face face2 in list2)
				{
					face2.Reverse();
				}
			}
			mesh.RebuildWithPositionsAndFaces(list, list2);
			mesh.unwrapParameters = new UnwrapParameters
			{
				packMargin = 30f
			};
			Face face3 = list3[0];
			AutoUnwrapSettings uv = face3.uv;
			uv.anchor = AutoUnwrapSettings.Anchor.LowerLeft;
			face3.uv = uv;
			face3.manualUV = true;
			UvUnwrapping.Unwrap(mesh, face3, Vector3.up);
			for (int m = 1; m < list3.Count; m++)
			{
				Face face4 = list3[m];
				face4.manualUV = true;
				UvUnwrapping.CopyUVs(mesh, face3, face4);
			}
			mesh.RefreshUV(list3);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00025458 File Offset: 0x00023658
		public Cone()
		{
		}

		// Token: 0x04000233 RID: 563
		[Range(3f, 64f)]
		[SerializeField]
		internal int m_NumberOfSides = 6;

		// Token: 0x04000234 RID: 564
		private float m_Radius;

		// Token: 0x04000235 RID: 565
		[SerializeField]
		private bool m_Smooth = true;
	}
}
