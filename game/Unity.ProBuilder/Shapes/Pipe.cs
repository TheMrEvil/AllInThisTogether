using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x0200006D RID: 109
	[Shape("Pipe")]
	public class Pipe : Shape
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0002616C File Offset: 0x0002436C
		public override void CopyShape(Shape shape)
		{
			if (shape is Pipe)
			{
				Pipe pipe = (Pipe)shape;
				this.m_Thickness = pipe.m_Thickness;
				this.m_NumberOfSides = pipe.m_NumberOfSides;
				this.m_HeightCuts = pipe.m_HeightCuts;
				this.m_Smooth = pipe.m_Smooth;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000261B8 File Offset: 0x000243B8
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			bounds.size = size;
			return bounds;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000261C4 File Offset: 0x000243C4
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = Vector3.Scale(rotation * Vector3.up, size);
			Vector3 vector2 = Vector3.Scale(rotation * Vector3.right, size);
			Vector3 vector3 = Vector3.Scale(rotation * Vector3.forward, size);
			float magnitude = vector.magnitude;
			float xRadius = vector2.magnitude / 2f;
			float yRadius = vector3.magnitude / 2f;
			Vector2[] array = new Vector2[this.m_NumberOfSides];
			Vector2[] array2 = new Vector2[this.m_NumberOfSides];
			for (int i = 0; i < this.m_NumberOfSides; i++)
			{
				float angleInDegrees = (float)i * (360f / (float)this.m_NumberOfSides);
				Vector2 vector4;
				array[i] = Math.PointInEllipseCircumference(xRadius, yRadius, angleInDegrees, Vector2.zero, out vector4);
				Vector2 a = new Vector2(-vector4.y, vector4.x);
				array2[i] = array[i] + this.m_Thickness * a;
			}
			List<Vector3> list = new List<Vector3>();
			float num = magnitude / 2f;
			int num2 = this.m_HeightCuts + 1;
			for (int j = 0; j < num2; j++)
			{
				float y = (float)j * (magnitude / (float)num2) - num;
				float y2 = (float)(j + 1) * (magnitude / (float)num2) - num;
				for (int k = 0; k < this.m_NumberOfSides; k++)
				{
					Vector2 vector5 = array[k];
					Vector2 vector6 = (k < this.m_NumberOfSides - 1) ? array[k + 1] : array[0];
					Vector3[] collection = new Vector3[]
					{
						new Vector3(vector6.x, y, vector6.y),
						new Vector3(vector5.x, y, vector5.y),
						new Vector3(vector6.x, y2, vector6.y),
						new Vector3(vector5.x, y2, vector5.y)
					};
					vector5 = array2[k];
					vector6 = ((k < this.m_NumberOfSides - 1) ? array2[k + 1] : array2[0]);
					Vector3[] collection2 = new Vector3[]
					{
						new Vector3(vector5.x, y, vector5.y),
						new Vector3(vector6.x, y, vector6.y),
						new Vector3(vector5.x, y2, vector5.y),
						new Vector3(vector6.x, y2, vector6.y)
					};
					list.AddRange(collection);
					list.AddRange(collection2);
				}
			}
			for (int l = 0; l < this.m_NumberOfSides; l++)
			{
				Vector2 vector5 = array[l];
				Vector2 vector6 = (l < this.m_NumberOfSides - 1) ? array[l + 1] : array[0];
				Vector2 vector7 = array2[l];
				Vector2 vector8 = (l < this.m_NumberOfSides - 1) ? array2[l + 1] : array2[0];
				Vector3[] collection3 = new Vector3[]
				{
					new Vector3(vector6.x, magnitude - num, vector6.y),
					new Vector3(vector5.x, magnitude - num, vector5.y),
					new Vector3(vector8.x, magnitude - num, vector8.y),
					new Vector3(vector7.x, magnitude - num, vector7.y)
				};
				Vector3[] collection4 = new Vector3[]
				{
					new Vector3(vector5.x, -num, vector5.y),
					new Vector3(vector6.x, -num, vector6.y),
					new Vector3(vector7.x, -num, vector7.y),
					new Vector3(vector8.x, -num, vector8.y)
				};
				list.AddRange(collection4);
				list.AddRange(collection3);
			}
			for (int m = 0; m < list.Count; m++)
			{
				list[m] = rotation * list[m];
			}
			mesh.GeometryWithPoints(list.ToArray());
			if (this.m_Smooth)
			{
				int num3 = 2 * num2 * this.m_NumberOfSides;
				for (int n = 0; n < num3; n++)
				{
					mesh.facesInternal[n].smoothingGroup = 1;
				}
			}
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00026694 File Offset: 0x00024894
		public Pipe()
		{
		}

		// Token: 0x0400023D RID: 573
		[Min(0.01f)]
		[SerializeField]
		private float m_Thickness = 0.25f;

		// Token: 0x0400023E RID: 574
		[Range(3f, 64f)]
		[SerializeField]
		private int m_NumberOfSides = 6;

		// Token: 0x0400023F RID: 575
		[Range(0f, 31f)]
		[SerializeField]
		private int m_HeightCuts;

		// Token: 0x04000240 RID: 576
		[SerializeField]
		private bool m_Smooth = true;
	}
}
