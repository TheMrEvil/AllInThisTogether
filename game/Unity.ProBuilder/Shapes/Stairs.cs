using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000076 RID: 118
	[Shape("Stairs")]
	public class Stairs : Shape
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00027A14 File Offset: 0x00025C14
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00027A1C File Offset: 0x00025C1C
		public bool sides
		{
			get
			{
				return this.m_Sides;
			}
			set
			{
				this.m_Sides = value;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00027A28 File Offset: 0x00025C28
		public override void CopyShape(Shape shape)
		{
			if (shape is Stairs)
			{
				Stairs stairs = (Stairs)shape;
				this.m_StepGenerationType = stairs.m_StepGenerationType;
				this.m_StepsHeight = stairs.m_StepsHeight;
				this.m_StepsCount = stairs.m_StepsCount;
				this.m_HomogeneousSteps = stairs.m_HomogeneousSteps;
				this.m_Circumference = stairs.m_Circumference;
				this.m_Sides = stairs.m_Sides;
				this.m_InnerRadius = stairs.m_InnerRadius;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00027A98 File Offset: 0x00025C98
		public override Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			if (Mathf.Abs(this.m_Circumference) > 0f)
			{
				return this.BuildCurvedStairs(mesh, size, rotation);
			}
			return this.BuildStairs(mesh, size, rotation);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00027AC0 File Offset: 0x00025CC0
		public override Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			if (Mathf.Abs(this.m_Circumference) > 0f)
			{
				bounds.center = mesh.mesh.bounds.center;
				bounds.size = Vector3.Scale(size.Sign(), mesh.mesh.bounds.size);
			}
			else
			{
				bounds = mesh.mesh.bounds;
				bounds.size = size;
			}
			return bounds;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00027B38 File Offset: 0x00025D38
		private Bounds BuildStairs(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = Vector3.Scale(rotation * Vector3.up, size);
			Vector3 vector2 = Vector3.Scale(rotation * Vector3.right, size);
			Vector3 vector3 = Vector3.Scale(rotation * Vector3.forward, size);
			Vector3 vector4 = new Vector3(vector2.magnitude, vector.magnitude, vector3.magnitude);
			bool flag = this.m_StepGenerationType == StepGenerationType.Height;
			float y = vector4.y;
			float num = Mathf.Min(this.m_StepsHeight, y);
			int num2 = this.m_StepsCount;
			if (flag)
			{
				if (y > 0f)
				{
					num2 = (int)(y / num);
					if (this.m_HomogeneousSteps)
					{
						num = y / (float)num2;
					}
					else
					{
						num2 += ((y / num - (float)num2 > 0.001f) ? 1 : 0);
					}
				}
				else
				{
					num2 = 1;
				}
			}
			if (num2 > 256)
			{
				num2 = 256;
				num = y / (float)num2;
			}
			Vector3[] array = new Vector3[4 * num2 * 2];
			Face[] array2 = new Face[num2 * 2];
			Vector3 vector5 = vector4 * 0.5f;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < num2; i++)
			{
				float num5 = (float)i * num;
				float num6 = (i != num2 - 1) ? ((float)(i + 1) * num) : vector4.y;
				float num7 = (float)i / (float)num2;
				float num8 = (float)(i + 1) / (float)num2;
				float x = vector4.x - vector5.x;
				float x2 = 0f - vector5.x;
				float y2 = (flag ? num5 : (vector4.y * num7)) - vector5.y;
				float y3 = (flag ? num6 : (vector4.y * num8)) - vector5.y;
				float z = vector4.z * num7 - vector5.z;
				float z2 = vector4.z * num8 - vector5.z;
				array[num3] = new Vector3(x, y2, z);
				array[num3 + 1] = new Vector3(x2, y2, z);
				array[num3 + 2] = new Vector3(x, y3, z);
				array[num3 + 3] = new Vector3(x2, y3, z);
				array[num3 + 4] = new Vector3(x, y3, z);
				array[num3 + 5] = new Vector3(x2, y3, z);
				array[num3 + 6] = new Vector3(x, y3, z2);
				array[num3 + 7] = new Vector3(x2, y3, z2);
				array2[num4] = new Face(new int[]
				{
					num3,
					num3 + 1,
					num3 + 2,
					num3 + 1,
					num3 + 3,
					num3 + 2
				});
				array2[num4 + 1] = new Face(new int[]
				{
					num3 + 4,
					num3 + 5,
					num3 + 6,
					num3 + 5,
					num3 + 7,
					num3 + 6
				});
				num3 += 8;
				num4 += 2;
			}
			if (this.sides)
			{
				float num9 = 0f;
				for (int j = 0; j < 2; j++)
				{
					Vector3[] array3 = new Vector3[num2 * 4 + (num2 - 1) * 3];
					Face[] array4 = new Face[num2 + num2 - 1];
					int num10 = 0;
					int num11 = 0;
					for (int k = 0; k < num2; k++)
					{
						float num5 = (float)Mathf.Max(k, 1) * num;
						float num6 = (k != num2 - 1) ? ((float)(k + 1) * num) : vector4.y;
						float num7 = (float)Mathf.Max(k, 1) / (float)num2;
						float num8 = (float)(k + 1) / (float)num2;
						float y2 = flag ? num5 : (num7 * vector4.y);
						float y3 = flag ? num6 : (num8 * vector4.y);
						num7 = (float)k / (float)num2;
						float z = num7 * vector4.z;
						float z2 = num8 * vector4.z;
						array3[num10] = new Vector3(num9, 0f, z) - vector5;
						array3[num10 + 1] = new Vector3(num9, 0f, z2) - vector5;
						array3[num10 + 2] = new Vector3(num9, y2, z) - vector5;
						array3[num10 + 3] = new Vector3(num9, y3, z2) - vector5;
						Face[] array5 = array4;
						int num12 = num11++;
						IEnumerable<int> indices;
						if (j % 2 != 0)
						{
							int[] array6 = new int[6];
							array6[0] = num3 + 2;
							array6[1] = num3 + 1;
							array6[2] = num3;
							array6[3] = num3 + 2;
							array6[4] = num3 + 3;
							indices = array6;
							array6[5] = num3 + 1;
						}
						else
						{
							int[] array7 = new int[6];
							array7[0] = num3;
							array7[1] = num3 + 1;
							array7[2] = num3 + 2;
							array7[3] = num3 + 1;
							array7[4] = num3 + 3;
							indices = array7;
							array7[5] = num3 + 2;
						}
						array5[num12] = new Face(indices);
						array4[num11 - 1].textureGroup = j + 1;
						num3 += 4;
						num10 += 4;
						if (k > 0)
						{
							array3[num10] = new Vector3(num9, y2, z) - vector5;
							array3[num10 + 1] = new Vector3(num9, y3, z) - vector5;
							array3[num10 + 2] = new Vector3(num9, y3, z2) - vector5;
							Face[] array8 = array4;
							int num13 = num11++;
							IEnumerable<int> indices2;
							if (j % 2 != 0)
							{
								int[] array9 = new int[3];
								array9[0] = num3;
								array9[1] = num3 + 1;
								indices2 = array9;
								array9[2] = num3 + 2;
							}
							else
							{
								int[] array10 = new int[3];
								array10[0] = num3 + 2;
								array10[1] = num3 + 1;
								indices2 = array10;
								array10[2] = num3;
							}
							array8[num13] = new Face(indices2);
							array4[num11 - 1].textureGroup = j + 1;
							num3 += 3;
							num10 += 3;
						}
					}
					array = array.Concat(array3);
					array2 = array2.Concat(array4);
					num9 += vector4.x;
				}
				array = array.Concat(new Vector3[]
				{
					new Vector3(0f, 0f, vector4.z) - vector5,
					new Vector3(vector4.x, 0f, vector4.z) - vector5,
					new Vector3(0f, vector4.y, vector4.z) - vector5,
					new Vector3(vector4.x, vector4.y, vector4.z) - vector5
				});
				array2 = array2.Add(new Face(new int[]
				{
					num3,
					num3 + 1,
					num3 + 2,
					num3 + 1,
					num3 + 3,
					num3 + 2
				}));
			}
			Vector3 vector6 = size.Sign();
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = rotation * array[l];
				array[l].Scale(vector6);
			}
			if (vector6.x * vector6.y * vector6.z < 0f)
			{
				Face[] array11 = array2;
				for (int m = 0; m < array11.Length; m++)
				{
					array11[m].Reverse();
				}
			}
			mesh.RebuildWithPositionsAndFaces(array, array2);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00028290 File Offset: 0x00026490
		private Bounds BuildCurvedStairs(ProBuilderMesh mesh, Vector3 size, Quaternion rotation)
		{
			Vector3 vector = size.Abs();
			bool sides = this.m_Sides;
			float num = Mathf.Min(vector.x, vector.z);
			float num2 = Mathf.Clamp(this.m_InnerRadius, 0f, num - float.Epsilon);
			float num3 = num - num2;
			float num4 = Mathf.Abs(vector.y);
			float circumference = this.m_Circumference;
			bool flag = num2 < Mathf.Epsilon;
			bool flag2 = this.m_StepGenerationType == StepGenerationType.Height;
			float num5 = Mathf.Min(this.m_StepsHeight, num4);
			int num6 = this.m_StepsCount;
			if (flag2 && num5 > 0.01f * this.m_StepsHeight)
			{
				if (num4 > 0f)
				{
					num6 = (int)(num4 / this.m_StepsHeight);
					if (this.m_HomogeneousSteps && num6 > 0)
					{
						num5 = num4 / (float)num6;
					}
					else
					{
						num6 += ((num4 / this.m_StepsHeight - (float)num6 > 0.001f) ? 1 : 0);
					}
				}
				else
				{
					num6 = 1;
				}
			}
			if (num6 > 256)
			{
				num6 = 256;
				num5 = num4 / (float)num6;
			}
			Vector3[] array = new Vector3[4 * num6 + (flag ? 3 : 4) * num6];
			Face[] array2 = new Face[num6 * 2];
			int num7 = 0;
			int num8 = 0;
			float num9 = Mathf.Abs(circumference) * 0.017453292f;
			float num10 = num2 + num3;
			for (int i = 0; i < num6; i++)
			{
				float num11 = (float)i / (float)num6 * num9;
				float num12 = (float)(i + 1) / (float)num6 * num9;
				float y = flag2 ? ((float)i * num5) : ((float)i / (float)num6 * num4);
				float y2 = flag2 ? ((i != num6 - 1) ? ((float)(i + 1) * num5) : num4) : ((float)(i + 1) / (float)num6 * num4);
				Vector3 a = new Vector3(-Mathf.Cos(num11), 0f, Mathf.Sin(num11));
				Vector3 a2 = new Vector3(-Mathf.Cos(num12), 0f, Mathf.Sin(num12));
				array[num7] = a * num2;
				array[num7 + 1] = a * num10;
				array[num7 + 2] = a * num2;
				array[num7 + 3] = a * num10;
				array[num7].y = y;
				array[num7 + 1].y = y;
				array[num7 + 2].y = y2;
				array[num7 + 3].y = y2;
				array[num7 + 4] = array[num7 + 2];
				array[num7 + 5] = array[num7 + 3];
				array[num7 + 6] = a2 * num10;
				array[num7 + 6].y = y2;
				if (!flag)
				{
					array[num7 + 7] = a2 * num2;
					array[num7 + 7].y = y2;
				}
				array2[num8] = new Face(new int[]
				{
					num7,
					num7 + 1,
					num7 + 2,
					num7 + 1,
					num7 + 3,
					num7 + 2
				});
				if (flag)
				{
					array2[num8 + 1] = new Face(new int[]
					{
						num7 + 4,
						num7 + 5,
						num7 + 6
					});
				}
				else
				{
					array2[num8 + 1] = new Face(new int[]
					{
						num7 + 4,
						num7 + 5,
						num7 + 6,
						num7 + 4,
						num7 + 6,
						num7 + 7
					});
				}
				float num13 = (num12 + num11) * -0.5f * 57.29578f;
				num13 %= 360f;
				if (num13 < 0f)
				{
					num13 = 360f + num13;
				}
				AutoUnwrapSettings uv = array2[num8 + 1].uv;
				uv.rotation = num13;
				array2[num8 + 1].uv = uv;
				num7 += (flag ? 7 : 8);
				num8 += 2;
			}
			if (sides)
			{
				float num14 = flag ? (num2 + num3) : num2;
				for (int j = flag ? 1 : 0; j < 2; j++)
				{
					Vector3[] array3 = new Vector3[num6 * 4 + (num6 - 1) * 3];
					Face[] array4 = new Face[num6 + num6 - 1];
					int num15 = 0;
					int num16 = 0;
					for (int k = 0; k < num6; k++)
					{
						float f = (float)k / (float)num6 * num9;
						float f2 = (float)(k + 1) / (float)num6 * num9;
						float y3 = flag2 ? ((float)Mathf.Max(k, 1) * num5) : ((float)Mathf.Max(k, 1) / (float)num6 * num4);
						float y4 = flag2 ? ((k != num6 - 1) ? ((float)(k + 1) * num5) : vector.y) : ((float)(k + 1) / (float)num6 * num4);
						Vector3 vector2 = new Vector3(-Mathf.Cos(f), 0f, Mathf.Sin(f)) * num14;
						Vector3 vector3 = new Vector3(-Mathf.Cos(f2), 0f, Mathf.Sin(f2)) * num14;
						array3[num15] = vector2;
						array3[num15 + 1] = vector3;
						array3[num15 + 2] = vector2;
						array3[num15 + 3] = vector3;
						array3[num15].y = 0f;
						array3[num15 + 1].y = 0f;
						array3[num15 + 2].y = y3;
						array3[num15 + 3].y = y4;
						Face[] array5 = array4;
						int num17 = num16++;
						IEnumerable<int> indices;
						if (j % 2 != 0)
						{
							int[] array6 = new int[6];
							array6[0] = num7;
							array6[1] = num7 + 1;
							array6[2] = num7 + 2;
							array6[3] = num7 + 1;
							array6[4] = num7 + 3;
							indices = array6;
							array6[5] = num7 + 2;
						}
						else
						{
							int[] array7 = new int[6];
							array7[0] = num7 + 2;
							array7[1] = num7 + 1;
							array7[2] = num7;
							array7[3] = num7 + 2;
							array7[4] = num7 + 3;
							indices = array7;
							array7[5] = num7 + 1;
						}
						array5[num17] = new Face(indices);
						array4[num16 - 1].smoothingGroup = j + 1;
						num7 += 4;
						num15 += 4;
						if (k > 0)
						{
							array4[num16 - 1].textureGroup = j * num6 + k;
							array3[num15] = vector2;
							array3[num15 + 1] = vector3;
							array3[num15 + 2] = vector2;
							array3[num15].y = y3;
							array3[num15 + 1].y = y4;
							array3[num15 + 2].y = y4;
							Face[] array8 = array4;
							int num18 = num16++;
							IEnumerable<int> indices2;
							if (j % 2 != 0)
							{
								int[] array9 = new int[3];
								array9[0] = num7;
								array9[1] = num7 + 1;
								indices2 = array9;
								array9[2] = num7 + 2;
							}
							else
							{
								int[] array10 = new int[3];
								array10[0] = num7 + 2;
								array10[1] = num7 + 1;
								indices2 = array10;
								array10[2] = num7;
							}
							array8[num18] = new Face(indices2);
							array4[num16 - 1].textureGroup = j * num6 + k;
							array4[num16 - 1].smoothingGroup = j + 1;
							num7 += 3;
							num15 += 3;
						}
					}
					array = array.Concat(array3);
					array2 = array2.Concat(array4);
					num14 += num3;
				}
				float num19 = -Mathf.Cos(num9);
				float num20 = Mathf.Sin(num9);
				array = array.Concat(new Vector3[]
				{
					new Vector3(num19, 0f, num20) * num2,
					new Vector3(num19, 0f, num20) * num10,
					new Vector3(num19 * num2, num4, num20 * num2),
					new Vector3(num19 * num10, num4, num20 * num10)
				});
				array2 = array2.Add(new Face(new int[]
				{
					num7 + 2,
					num7 + 1,
					num7,
					num7 + 2,
					num7 + 3,
					num7 + 1
				}));
			}
			if (circumference < 0f)
			{
				Vector3 scale = new Vector3(-1f, 1f, 1f);
				for (int l = 0; l < array.Length; l++)
				{
					array[l].Scale(scale);
				}
				Face[] array11 = array2;
				for (int m = 0; m < array11.Length; m++)
				{
					array11[m].Reverse();
				}
			}
			Vector3 vector4 = size.Sign();
			for (int n = 0; n < array.Length; n++)
			{
				array[n] = rotation * array[n];
				array[n].Scale(vector4);
			}
			if (vector4.x * vector4.y * vector4.z < 0f)
			{
				Face[] array11 = array2;
				for (int m = 0; m < array11.Length; m++)
				{
					array11[m].Reverse();
				}
			}
			mesh.RebuildWithPositionsAndFaces(array, array2);
			mesh.TranslateVerticesInWorldSpace(mesh.mesh.triangles, mesh.transform.TransformDirection(-mesh.mesh.bounds.center));
			mesh.Refresh(RefreshMask.All);
			return this.UpdateBounds(mesh, size, rotation, default(Bounds));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00028BC4 File Offset: 0x00026DC4
		public Stairs()
		{
		}

		// Token: 0x04000255 RID: 597
		[SerializeField]
		private StepGenerationType m_StepGenerationType = StepGenerationType.Count;

		// Token: 0x04000256 RID: 598
		[Min(0.01f)]
		[SerializeField]
		private float m_StepsHeight = 0.2f;

		// Token: 0x04000257 RID: 599
		[Range(1f, 256f)]
		[SerializeField]
		private int m_StepsCount = 10;

		// Token: 0x04000258 RID: 600
		[SerializeField]
		private bool m_HomogeneousSteps = true;

		// Token: 0x04000259 RID: 601
		[Range(-360f, 360f)]
		[SerializeField]
		private float m_Circumference;

		// Token: 0x0400025A RID: 602
		[SerializeField]
		private bool m_Sides = true;

		// Token: 0x0400025B RID: 603
		[SerializeField]
		[Min(0f)]
		private float m_InnerRadius;
	}
}
