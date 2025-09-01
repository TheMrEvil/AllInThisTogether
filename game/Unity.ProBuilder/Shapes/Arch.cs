using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000068 RID: 104
	[Shape("Arch")]
	public class Arch : Shape
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00024A2C File Offset: 0x00022C2C
		public override void CopyShape(Shape shape)
		{
			if (shape is Arch)
			{
				Arch arch = (Arch)shape;
				this.m_Thickness = arch.m_Thickness;
				this.m_NumberOfSides = arch.m_NumberOfSides;
				this.m_ArchDegrees = arch.m_ArchDegrees;
				this.m_EndCaps = arch.m_EndCaps;
				this.m_Smooth = arch.m_Smooth;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00024A84 File Offset: 0x00022C84
		private Vector3[] GetFace(Vector2 vertex1, Vector2 vertex2, float depth)
		{
			return new Vector3[]
			{
				new Vector3(vertex1.x, vertex1.y, depth),
				new Vector3(vertex2.x, vertex2.y, depth),
				new Vector3(vertex1.x, vertex1.y, -depth),
				new Vector3(vertex2.x, vertex2.y, -depth)
			};
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00024B00 File Offset: 0x00022D00
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = Vector3.Scale(rotation * Vector3.up, size);
			Vector3 vector2 = Vector3.Scale(rotation * Vector3.right, size);
			Vector3 vector3 = Vector3.Scale(rotation * Vector3.forward, size);
			float num = vector2.magnitude / 2f;
			float num2 = vector.magnitude;
			float num3 = vector3.magnitude / 2f;
			int num4 = this.m_NumberOfSides + 1;
			float archDegrees = this.m_ArchDegrees;
			Vector2[] array = new Vector2[num4];
			Vector2[] array2 = new Vector2[num4];
			if (archDegrees < 90f)
			{
				num *= 2f;
			}
			else if (archDegrees < 180f)
			{
				num *= 1f + Mathf.Lerp(1f, 0f, Mathf.Abs(Mathf.Cos(archDegrees * 0.017453292f)));
			}
			else if (archDegrees > 180f)
			{
				num2 /= 1f + Mathf.Lerp(0f, 1f, (archDegrees - 180f) / 90f);
			}
			for (int i = 0; i < num4; i++)
			{
				float angleInDegrees = (float)i * (archDegrees / (float)(num4 - 1));
				Vector2 vector4;
				array[i] = Math.PointInEllipseCircumference(num, num2, angleInDegrees, Vector2.zero, out vector4);
				array2[i] = Math.PointInEllipseCircumference(num - this.m_Thickness, num2 - this.m_Thickness, angleInDegrees, Vector2.zero, out vector4);
			}
			List<Vector3> list = new List<Vector3>();
			float z = -num3;
			int num5 = 0;
			for (int j = 0; j < num4 - 1; j++)
			{
				Vector2 vector5 = array[j];
				Vector2 vector6 = (j < num4 - 1) ? array[j + 1] : array[j];
				Vector3[] face = this.GetFace(vector5, vector6, -num3);
				vector5 = array2[j];
				vector6 = ((j < num4 - 1) ? array2[j + 1] : array2[j]);
				Vector3[] face2 = this.GetFace(vector6, vector5, -num3);
				if (archDegrees < 360f && this.m_EndCaps && j == 0)
				{
					list.AddRange(this.GetFace(array[j], array2[j], num3));
				}
				list.AddRange(face);
				list.AddRange(face2);
				num5 += 2;
				if (archDegrees < 360f && this.m_EndCaps && j == num4 - 2)
				{
					list.AddRange(this.GetFace(array2[j + 1], array[j + 1], num3));
				}
			}
			for (int k = 0; k < num4 - 1; k++)
			{
				Vector2 vector5 = array[k];
				Vector2 vector6 = (k < num4 - 1) ? array[k + 1] : array[k];
				Vector2 vector7 = array2[k];
				Vector2 vector8 = (k < num4 - 1) ? array2[k + 1] : array2[k];
				Vector3[] collection = new Vector3[]
				{
					new Vector3(vector5.x, vector5.y, num3),
					new Vector3(vector6.x, vector6.y, num3),
					new Vector3(vector7.x, vector7.y, num3),
					new Vector3(vector8.x, vector8.y, num3)
				};
				Vector3[] collection2 = new Vector3[]
				{
					new Vector3(vector6.x, vector6.y, z),
					new Vector3(vector5.x, vector5.y, z),
					new Vector3(vector8.x, vector8.y, z),
					new Vector3(vector7.x, vector7.y, z)
				};
				list.AddRange(collection);
				list.AddRange(collection2);
			}
			Vector3 vector9 = size.Sign();
			for (int l = 0; l < list.Count; l++)
			{
				list[l] = Vector3.Scale(rotation * list[l], vector9);
			}
			mesh.GeometryWithPoints(list.ToArray());
			if (this.m_Smooth)
			{
				for (int m = (archDegrees < 360f && this.m_EndCaps) ? 1 : 0; m < num5; m++)
				{
					mesh.facesInternal[m].smoothingGroup = 1;
				}
			}
			if (vector9.x * vector9.y * vector9.z < 0f)
			{
				Face[] facesInternal = mesh.facesInternal;
				for (int n = 0; n < facesInternal.Length; n++)
				{
					facesInternal[n].Reverse();
				}
			}
			mesh.TranslateVerticesInWorldSpace(mesh.mesh.triangles, mesh.transform.TransformDirection(-mesh.mesh.bounds.center));
			mesh.Refresh(RefreshMask.All);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00025019 File Offset: 0x00023219
		public Arch()
		{
		}

		// Token: 0x0400022E RID: 558
		[Min(0.01f)]
		[SerializeField]
		private float m_Thickness = 0.1f;

		// Token: 0x0400022F RID: 559
		[Range(2f, 200f)]
		[SerializeField]
		private int m_NumberOfSides = 5;

		// Token: 0x04000230 RID: 560
		[Range(1f, 360f)]
		[SerializeField]
		private float m_ArchDegrees = 180f;

		// Token: 0x04000231 RID: 561
		[SerializeField]
		private bool m_EndCaps = true;

		// Token: 0x04000232 RID: 562
		[SerializeField]
		private bool m_Smooth = true;
	}
}
