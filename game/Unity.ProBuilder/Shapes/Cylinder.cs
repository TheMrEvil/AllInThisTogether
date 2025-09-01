using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x0200006B RID: 107
	[Shape("Cylinder")]
	public class Cylinder : Shape
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x000255EF File Offset: 0x000237EF
		public override void CopyShape(Shape shape)
		{
			if (shape is Cylinder)
			{
				this.m_AxisDivisions = ((Cylinder)shape).m_AxisDivisions;
				this.m_HeightCuts = ((Cylinder)shape).m_HeightCuts;
				this.m_Smooth = ((Cylinder)shape).m_Smooth;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0002562C File Offset: 0x0002382C
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			bounds.size = size;
			return bounds;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00025638 File Offset: 0x00023838
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = Vector3.Scale(rotation * Vector3.up, size);
			Vector3 vector2 = Vector3.Scale(rotation * Vector3.right, size);
			Vector3 vector3 = Vector3.Scale(rotation * Vector3.forward, size);
			float magnitude = vector.magnitude;
			float xRadius = vector2.magnitude / 2f;
			float yRadius = vector3.magnitude / 2f;
			float num = magnitude / (float)(this.m_HeightCuts + 1);
			Vector2[] array = new Vector2[this.m_AxisDivisions];
			for (int i = 0; i < this.m_AxisDivisions; i++)
			{
				float angleInDegrees = (float)i * 360f / (float)this.m_AxisDivisions;
				Vector2 vector4;
				array[i] = Math.PointInEllipseCircumference(xRadius, yRadius, angleInDegrees, Vector2.zero, out vector4);
			}
			Vector3[] array2 = new Vector3[this.m_AxisDivisions * (this.m_HeightCuts + 1) * 4 + this.m_AxisDivisions * 6];
			Face[] array3 = new Face[this.m_AxisDivisions * (this.m_HeightCuts + 1) + this.m_AxisDivisions * 2];
			int num2 = 0;
			for (int j = 0; j < this.m_HeightCuts + 1; j++)
			{
				float y = (float)j * num - magnitude * 0.5f;
				float y2 = (float)(j + 1) * num - magnitude * 0.5f;
				for (int k = 0; k < this.m_AxisDivisions; k++)
				{
					array2[num2] = new Vector3(array[k].x, y, array[k].y);
					array2[num2 + 1] = new Vector3(array[k].x, y2, array[k].y);
					if (k != this.m_AxisDivisions - 1)
					{
						array2[num2 + 2] = new Vector3(array[k + 1].x, y, array[k + 1].y);
						array2[num2 + 3] = new Vector3(array[k + 1].x, y2, array[k + 1].y);
					}
					else
					{
						array2[num2 + 2] = new Vector3(array[0].x, y, array[0].y);
						array2[num2 + 3] = new Vector3(array[0].x, y2, array[0].y);
					}
					num2 += 4;
				}
			}
			int num3 = 0;
			for (int l = 0; l < this.m_HeightCuts + 1; l++)
			{
				for (int m = 0; m < this.m_AxisDivisions * 4; m += 4)
				{
					int num5;
					int num4 = num5 = l * (this.m_AxisDivisions * 4) + m;
					int num6 = num4 + 1;
					int num7 = num4 + 2;
					int num8 = num4 + 3;
					array3[num3++] = new Face(new int[]
					{
						num5,
						num6,
						num7,
						num6,
						num8,
						num7
					}, 0, AutoUnwrapSettings.tile, this.m_Smooth ? 1 : -1, -1, -1, false);
				}
			}
			int num9 = this.m_AxisDivisions * (this.m_HeightCuts + 1) * 4;
			int num10 = this.m_AxisDivisions * (this.m_HeightCuts + 1);
			for (int n = 0; n < this.m_AxisDivisions; n++)
			{
				float y3 = -magnitude * 0.5f;
				array2[num9] = new Vector3(array[n].x, y3, array[n].y);
				array2[num9 + 1] = new Vector3(0f, y3, 0f);
				if (n != this.m_AxisDivisions - 1)
				{
					array2[num9 + 2] = new Vector3(array[n + 1].x, y3, array[n + 1].y);
				}
				else
				{
					array2[num9 + 2] = new Vector3(array[0].x, y3, array[0].y);
				}
				array3[num10 + n] = new Face(new int[]
				{
					num9 + 2,
					num9 + 1,
					num9
				});
				num9 += 3;
				float y4 = magnitude * 0.5f;
				array2[num9] = new Vector3(array[n].x, y4, array[n].y);
				array2[num9 + 1] = new Vector3(0f, y4, 0f);
				if (n != this.m_AxisDivisions - 1)
				{
					array2[num9 + 2] = new Vector3(array[n + 1].x, y4, array[n + 1].y);
				}
				else
				{
					array2[num9 + 2] = new Vector3(array[0].x, y4, array[0].y);
				}
				array3[num10 + (n + this.m_AxisDivisions)] = new Face(new int[]
				{
					num9,
					num9 + 1,
					num9 + 2
				});
				num9 += 3;
			}
			for (int num11 = 0; num11 < array2.Length; num11++)
			{
				array2[num11] = rotation * array2[num11];
			}
			mesh.RebuildWithPositionsAndFaces(array2, array3);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00025BC4 File Offset: 0x00023DC4
		public Cylinder()
		{
		}

		// Token: 0x04000238 RID: 568
		[SerializeField]
		[Range(3f, 64f)]
		private int m_AxisDivisions = 6;

		// Token: 0x04000239 RID: 569
		[Min(0f)]
		[SerializeField]
		private int m_HeightCuts;

		// Token: 0x0400023A RID: 570
		[SerializeField]
		private bool m_Smooth = true;
	}
}
