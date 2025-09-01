using System;
using System.Collections.Generic;
using UnityEngine.ProBuilder.MeshOperations;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000077 RID: 119
	[Shape("Torus")]
	public class Torus : Shape
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00028BF4 File Offset: 0x00026DF4
		public override void CopyShape(Shape shape)
		{
			if (shape is Torus)
			{
				Torus torus = (Torus)shape;
				this.m_Rows = torus.m_Rows;
				this.m_Columns = torus.m_Columns;
				this.m_TubeRadius = torus.m_TubeRadius;
				this.m_HorizontalCircumference = torus.m_HorizontalCircumference;
				this.m_VerticalCircumference = torus.m_VerticalCircumference;
				this.m_Smooth = torus.m_Smooth;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00028C58 File Offset: 0x00026E58
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			bounds.size = mesh.mesh.bounds.size;
			return bounds;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00028C84 File Offset: 0x00026E84
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = (rotation * size).Abs();
			float num = Mathf.Clamp(vector.x / 2f, 0.01f, 2048f);
			float num2 = Mathf.Clamp(vector.z / 2f, 0.01f, 2048f);
			int num3 = Mathf.Clamp(this.m_Rows + 1, 4, 128);
			int num4 = Mathf.Clamp(this.m_Columns + 1, 4, 128);
			float num5 = Mathf.Clamp(this.m_TubeRadius, 0.01f, Mathf.Min(num, num2) - 0.001f);
			num -= num5;
			num2 -= num5;
			float num6 = Mathf.Clamp(this.m_HorizontalCircumference, 0.01f, 360f);
			float circumference = Mathf.Clamp(this.m_VerticalCircumference, 0.01f, 360f);
			List<Vector3> list = new List<Vector3>();
			int num7 = num4 - 1;
			float offset = num;
			Vector3[] circlePoints = Torus.GetCirclePoints(num3, num5, circumference, Quaternion.Euler(0f, 0f, 0f), offset);
			for (int i = 1; i < num4; i++)
			{
				list.AddRange(circlePoints);
				float num8 = (float)i / (float)num7 * num6;
				Vector2 vector2 = new Vector2(num * Mathf.Cos(0.017453292f * num8), num2 * Mathf.Sin(0.017453292f * num8));
				Vector2 vector3 = new Vector2(-vector2.y / (num2 * num2), vector2.x / (num * num));
				Quaternion rotation2 = Quaternion.Euler(Vector3.up * Vector2.SignedAngle(Vector2.up, vector3.normalized));
				circlePoints = Torus.GetCirclePoints(num3, num5, circumference, rotation2, new Vector3(vector2.x, 0f, -vector2.y));
				list.AddRange(circlePoints);
			}
			List<Face> list2 = new List<Face>();
			int num9 = 0;
			for (int j = 0; j < (num4 - 1) * 2; j += 2)
			{
				for (int k = 0; k < num3 - 1; k++)
				{
					int num10 = j * ((num3 - 1) * 2) + k * 2;
					int num11 = (j + 1) * ((num3 - 1) * 2) + k * 2;
					int num12 = j * ((num3 - 1) * 2) + k * 2 + 1;
					int num13 = (j + 1) * ((num3 - 1) * 2) + k * 2 + 1;
					list2.Add(new Face(new int[]
					{
						num10,
						num11,
						num12,
						num11,
						num13,
						num12
					}));
					list2[num9].smoothingGroup = (this.m_Smooth ? 1 : -1);
					list2[num9].manualUV = true;
					num9++;
				}
			}
			for (int l = 0; l < list.Count; l++)
			{
				list[l] = rotation * list[l];
			}
			mesh.RebuildWithPositionsAndFaces(list, list2);
			mesh.TranslateVerticesInWorldSpace(mesh.mesh.triangles, mesh.transform.TransformDirection(-mesh.mesh.bounds.center));
			mesh.Refresh(RefreshMask.All);
			UVEditing.ProjectFacesBox(mesh, mesh.facesInternal, 0);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00028FB8 File Offset: 0x000271B8
		private static Vector3[] GetCirclePoints(int segments, float radius, float circumference, Quaternion rotation, float offset)
		{
			float num = (float)segments - 1f;
			Vector3[] array = new Vector3[(segments - 1) * 2];
			array[0] = new Vector3(Mathf.Cos(0f / num * circumference * 0.017453292f) * radius, Mathf.Sin(0f / num * circumference * 0.017453292f) * radius, 0f);
			array[1] = new Vector3(Mathf.Cos(1f / num * circumference * 0.017453292f) * radius, Mathf.Sin(1f / num * circumference * 0.017453292f) * radius, 0f);
			array[0] = rotation * (array[0] + Vector3.right * offset);
			array[1] = rotation * (array[1] + Vector3.right * offset);
			int num2 = 2;
			for (int i = 2; i < segments; i++)
			{
				float f = (float)i / num * circumference * 0.017453292f;
				array[num2] = array[num2 - 1];
				array[num2 + 1] = rotation * (new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f) + Vector3.right * offset);
				num2 += 2;
			}
			return array;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002910C File Offset: 0x0002730C
		private static Vector3[] GetCirclePoints(int segments, float radius, float circumference, Quaternion rotation, Vector3 offset)
		{
			float num = (float)segments - 1f;
			Vector3[] array = new Vector3[(segments - 1) * 2];
			array[0] = new Vector3(Mathf.Cos(0f / num * circumference * 0.017453292f) * radius, Mathf.Sin(0f / num * circumference * 0.017453292f) * radius, 0f);
			array[1] = new Vector3(Mathf.Cos(1f / num * circumference * 0.017453292f) * radius, Mathf.Sin(1f / num * circumference * 0.017453292f) * radius, 0f);
			array[0] = rotation * array[0] + offset;
			array[1] = rotation * array[1] + offset;
			int num2 = 2;
			for (int i = 2; i < segments; i++)
			{
				float f = (float)i / num * circumference * 0.017453292f;
				array[num2] = array[num2 - 1];
				array[num2 + 1] = rotation * new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f) + offset;
				num2 += 2;
			}
			return array;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00029240 File Offset: 0x00027440
		public Torus()
		{
		}

		// Token: 0x0400025C RID: 604
		[Range(3f, 64f)]
		[SerializeField]
		private int m_Rows = 16;

		// Token: 0x0400025D RID: 605
		[Range(3f, 64f)]
		[SerializeField]
		private int m_Columns = 24;

		// Token: 0x0400025E RID: 606
		[Min(0.01f)]
		[SerializeField]
		private float m_TubeRadius = 0.1f;

		// Token: 0x0400025F RID: 607
		[Range(0f, 360f)]
		[SerializeField]
		private float m_HorizontalCircumference = 360f;

		// Token: 0x04000260 RID: 608
		[Range(0f, 360f)]
		[SerializeField]
		private float m_VerticalCircumference = 360f;

		// Token: 0x04000261 RID: 609
		[SerializeField]
		private bool m_Smooth = true;
	}
}
