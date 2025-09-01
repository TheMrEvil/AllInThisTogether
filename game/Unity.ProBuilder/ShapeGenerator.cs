using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.ProBuilder.MeshOperations;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000054 RID: 84
	public static class ShapeGenerator
	{
		// Token: 0x0600031F RID: 799 RVA: 0x0001CC18 File Offset: 0x0001AE18
		public static ProBuilderMesh CreateShape(ShapeType shape, PivotLocation pivotType = PivotLocation.Center)
		{
			ProBuilderMesh proBuilderMesh = null;
			if (shape == ShapeType.Cube)
			{
				proBuilderMesh = ShapeGenerator.GenerateCube(pivotType, Vector3.one);
			}
			if (shape == ShapeType.Stair)
			{
				proBuilderMesh = ShapeGenerator.GenerateStair(pivotType, new Vector3(2f, 2.5f, 4f), 6, true);
			}
			if (shape == ShapeType.CurvedStair)
			{
				proBuilderMesh = ShapeGenerator.GenerateCurvedStair(pivotType, 2f, 2.5f, 2f, 180f, 8, true);
			}
			if (shape == ShapeType.Prism)
			{
				proBuilderMesh = ShapeGenerator.GeneratePrism(pivotType, Vector3.one);
			}
			if (shape == ShapeType.Cylinder)
			{
				proBuilderMesh = ShapeGenerator.GenerateCylinder(pivotType, 8, 1f, 2f, 2, -1);
			}
			if (shape == ShapeType.Plane)
			{
				proBuilderMesh = ShapeGenerator.GeneratePlane(pivotType, 5f, 5f, 5, 5, Axis.Up);
			}
			if (shape == ShapeType.Door)
			{
				proBuilderMesh = ShapeGenerator.GenerateDoor(pivotType, 3f, 2.5f, 0.5f, 0.75f, 1f);
			}
			if (shape == ShapeType.Pipe)
			{
				proBuilderMesh = ShapeGenerator.GeneratePipe(pivotType, 1f, 2f, 0.25f, 8, 2);
			}
			if (shape == ShapeType.Cone)
			{
				proBuilderMesh = ShapeGenerator.GenerateCone(pivotType, 0.5f, 1f, 8);
			}
			if (shape == ShapeType.Sprite)
			{
				proBuilderMesh = ShapeGenerator.GeneratePlane(pivotType, 1f, 1f, 0, 0, Axis.Up);
			}
			if (shape == ShapeType.Arch)
			{
				proBuilderMesh = ShapeGenerator.GenerateArch(pivotType, 180f, 2f, 1f, 1f, 9, true, true, true, true, true);
			}
			if (shape == ShapeType.Sphere)
			{
				proBuilderMesh = ShapeGenerator.GenerateIcosahedron(pivotType, 0.5f, 2, true, false);
			}
			if (shape == ShapeType.Torus)
			{
				proBuilderMesh = ShapeGenerator.GenerateTorus(pivotType, 12, 16, 1f, 0.3f, true, 360f, 360f, true);
				UVEditing.ProjectFacesBox(proBuilderMesh, proBuilderMesh.facesInternal, 0);
			}
			if (proBuilderMesh == null)
			{
				proBuilderMesh = ShapeGenerator.GenerateCube(pivotType, Vector3.one);
			}
			proBuilderMesh.gameObject.name = shape.ToString();
			proBuilderMesh.renderer.sharedMaterial = BuiltinMaterials.defaultMaterial;
			return proBuilderMesh;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		public static ProBuilderMesh GenerateStair(PivotLocation pivotType, Vector3 size, int steps, bool buildSides)
		{
			Vector3[] array = new Vector3[4 * steps * 2];
			Face[] array2 = new Face[steps * 2];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < steps; i++)
			{
				float num3 = (float)i / (float)steps;
				float num4 = (float)(i + 1) / (float)steps;
				float x = size.x;
				float x2 = 0f;
				float y = size.y * num3;
				float y2 = size.y * num4;
				float z = size.z * num3;
				float z2 = size.z * num4;
				array[num] = new Vector3(x, y, z);
				array[num + 1] = new Vector3(x2, y, z);
				array[num + 2] = new Vector3(x, y2, z);
				array[num + 3] = new Vector3(x2, y2, z);
				array[num + 4] = new Vector3(x, y2, z);
				array[num + 5] = new Vector3(x2, y2, z);
				array[num + 6] = new Vector3(x, y2, z2);
				array[num + 7] = new Vector3(x2, y2, z2);
				array2[num2] = new Face(new int[]
				{
					num,
					num + 1,
					num + 2,
					num + 1,
					num + 3,
					num + 2
				});
				array2[num2 + 1] = new Face(new int[]
				{
					num + 4,
					num + 5,
					num + 6,
					num + 5,
					num + 7,
					num + 6
				});
				num += 8;
				num2 += 2;
			}
			if (buildSides)
			{
				float num5 = 0f;
				for (int j = 0; j < 2; j++)
				{
					Vector3[] array3 = new Vector3[steps * 4 + (steps - 1) * 3];
					Face[] array4 = new Face[steps + steps - 1];
					int num6 = 0;
					int num7 = 0;
					for (int k = 0; k < steps; k++)
					{
						float y3 = (float)Mathf.Max(k, 1) / (float)steps * size.y;
						float y4 = (float)(k + 1) / (float)steps * size.y;
						float z3 = (float)k / (float)steps * size.z;
						float z4 = (float)(k + 1) / (float)steps * size.z;
						array3[num6] = new Vector3(num5, 0f, z3);
						array3[num6 + 1] = new Vector3(num5, 0f, z4);
						array3[num6 + 2] = new Vector3(num5, y3, z3);
						array3[num6 + 3] = new Vector3(num5, y4, z4);
						Face[] array5 = array4;
						int num8 = num7++;
						IEnumerable<int> indices;
						if (j % 2 != 0)
						{
							int[] array6 = new int[6];
							array6[0] = num + 2;
							array6[1] = num + 1;
							array6[2] = num;
							array6[3] = num + 2;
							array6[4] = num + 3;
							indices = array6;
							array6[5] = num + 1;
						}
						else
						{
							int[] array7 = new int[6];
							array7[0] = num;
							array7[1] = num + 1;
							array7[2] = num + 2;
							array7[3] = num + 1;
							array7[4] = num + 3;
							indices = array7;
							array7[5] = num + 2;
						}
						array5[num8] = new Face(indices);
						array4[num7 - 1].textureGroup = j + 1;
						num += 4;
						num6 += 4;
						if (k > 0)
						{
							array3[num6] = new Vector3(num5, y3, z3);
							array3[num6 + 1] = new Vector3(num5, y4, z3);
							array3[num6 + 2] = new Vector3(num5, y4, z4);
							Face[] array8 = array4;
							int num9 = num7++;
							IEnumerable<int> indices2;
							if (j % 2 != 0)
							{
								int[] array9 = new int[3];
								array9[0] = num;
								array9[1] = num + 1;
								indices2 = array9;
								array9[2] = num + 2;
							}
							else
							{
								int[] array10 = new int[3];
								array10[0] = num + 2;
								array10[1] = num + 1;
								indices2 = array10;
								array10[2] = num;
							}
							array8[num9] = new Face(indices2);
							array4[num7 - 1].textureGroup = j + 1;
							num += 3;
							num6 += 3;
						}
					}
					array = array.Concat(array3);
					array2 = array2.Concat(array4);
					num5 += size.x;
				}
				array = array.Concat(new Vector3[]
				{
					new Vector3(0f, 0f, size.z),
					new Vector3(size.x, 0f, size.z),
					new Vector3(0f, size.y, size.z),
					new Vector3(size.x, size.y, size.z)
				});
				array2 = array2.Add(new Face(new int[]
				{
					num,
					num + 1,
					num + 2,
					num + 1,
					num + 3,
					num + 2
				}));
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(array, array2);
			proBuilderMesh.gameObject.name = "Stairs";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001D27C File Offset: 0x0001B47C
		public static ProBuilderMesh GenerateCurvedStair(PivotLocation pivotType, float stairWidth, float height, float innerRadius, float circumference, int steps, bool buildSides)
		{
			bool flag = innerRadius < Mathf.Epsilon;
			Vector3[] array = new Vector3[4 * steps + (flag ? 3 : 4) * steps];
			Face[] array2 = new Face[steps * 2];
			int num = 0;
			int num2 = 0;
			float num3 = Mathf.Abs(circumference) * 0.017453292f;
			float num4 = innerRadius + stairWidth;
			for (int i = 0; i < steps; i++)
			{
				float num5 = (float)i / (float)steps * num3;
				float num6 = (float)(i + 1) / (float)steps * num3;
				float y = (float)i / (float)steps * height;
				float y2 = (float)(i + 1) / (float)steps * height;
				Vector3 a = new Vector3(-Mathf.Cos(num5), 0f, Mathf.Sin(num5));
				Vector3 a2 = new Vector3(-Mathf.Cos(num6), 0f, Mathf.Sin(num6));
				array[num] = a * innerRadius;
				array[num + 1] = a * num4;
				array[num + 2] = a * innerRadius;
				array[num + 3] = a * num4;
				array[num].y = y;
				array[num + 1].y = y;
				array[num + 2].y = y2;
				array[num + 3].y = y2;
				array[num + 4] = array[num + 2];
				array[num + 5] = array[num + 3];
				array[num + 6] = a2 * num4;
				array[num + 6].y = y2;
				if (!flag)
				{
					array[num + 7] = a2 * innerRadius;
					array[num + 7].y = y2;
				}
				array2[num2] = new Face(new int[]
				{
					num,
					num + 1,
					num + 2,
					num + 1,
					num + 3,
					num + 2
				});
				if (flag)
				{
					array2[num2 + 1] = new Face(new int[]
					{
						num + 4,
						num + 5,
						num + 6
					});
				}
				else
				{
					array2[num2 + 1] = new Face(new int[]
					{
						num + 4,
						num + 5,
						num + 6,
						num + 4,
						num + 6,
						num + 7
					});
				}
				float num7 = (num6 + num5) * -0.5f * 57.29578f;
				num7 %= 360f;
				if (num7 < 0f)
				{
					num7 = 360f + num7;
				}
				AutoUnwrapSettings uv = array2[num2 + 1].uv;
				uv.rotation = num7;
				array2[num2 + 1].uv = uv;
				num += (flag ? 7 : 8);
				num2 += 2;
			}
			if (buildSides)
			{
				float num8 = flag ? (innerRadius + stairWidth) : innerRadius;
				for (int j = flag ? 1 : 0; j < 2; j++)
				{
					Vector3[] array3 = new Vector3[steps * 4 + (steps - 1) * 3];
					Face[] array4 = new Face[steps + steps - 1];
					int num9 = 0;
					int num10 = 0;
					for (int k = 0; k < steps; k++)
					{
						float f = (float)k / (float)steps * num3;
						float f2 = (float)(k + 1) / (float)steps * num3;
						float y3 = (float)Mathf.Max(k, 1) / (float)steps * height;
						float y4 = (float)(k + 1) / (float)steps * height;
						Vector3 vector = new Vector3(-Mathf.Cos(f), 0f, Mathf.Sin(f)) * num8;
						Vector3 vector2 = new Vector3(-Mathf.Cos(f2), 0f, Mathf.Sin(f2)) * num8;
						array3[num9] = vector;
						array3[num9 + 1] = vector2;
						array3[num9 + 2] = vector;
						array3[num9 + 3] = vector2;
						array3[num9].y = 0f;
						array3[num9 + 1].y = 0f;
						array3[num9 + 2].y = y3;
						array3[num9 + 3].y = y4;
						Face[] array5 = array4;
						int num11 = num10++;
						IEnumerable<int> indices;
						if (j % 2 != 0)
						{
							int[] array6 = new int[6];
							array6[0] = num;
							array6[1] = num + 1;
							array6[2] = num + 2;
							array6[3] = num + 1;
							array6[4] = num + 3;
							indices = array6;
							array6[5] = num + 2;
						}
						else
						{
							int[] array7 = new int[6];
							array7[0] = num + 2;
							array7[1] = num + 1;
							array7[2] = num;
							array7[3] = num + 2;
							array7[4] = num + 3;
							indices = array7;
							array7[5] = num + 1;
						}
						array5[num11] = new Face(indices);
						array4[num10 - 1].smoothingGroup = j + 1;
						num += 4;
						num9 += 4;
						if (k > 0)
						{
							array4[num10 - 1].textureGroup = j * steps + k;
							array3[num9] = vector;
							array3[num9 + 1] = vector2;
							array3[num9 + 2] = vector;
							array3[num9].y = y3;
							array3[num9 + 1].y = y4;
							array3[num9 + 2].y = y4;
							Face[] array8 = array4;
							int num12 = num10++;
							IEnumerable<int> indices2;
							if (j % 2 != 0)
							{
								int[] array9 = new int[3];
								array9[0] = num;
								array9[1] = num + 1;
								indices2 = array9;
								array9[2] = num + 2;
							}
							else
							{
								int[] array10 = new int[3];
								array10[0] = num + 2;
								array10[1] = num + 1;
								indices2 = array10;
								array10[2] = num;
							}
							array8[num12] = new Face(indices2);
							array4[num10 - 1].textureGroup = j * steps + k;
							array4[num10 - 1].smoothingGroup = j + 1;
							num += 3;
							num9 += 3;
						}
					}
					array = array.Concat(array3);
					array2 = array2.Concat(array4);
					num8 += stairWidth;
				}
				float num13 = -Mathf.Cos(num3);
				float num14 = Mathf.Sin(num3);
				array = array.Concat(new Vector3[]
				{
					new Vector3(num13, 0f, num14) * innerRadius,
					new Vector3(num13, 0f, num14) * num4,
					new Vector3(num13 * innerRadius, height, num14 * innerRadius),
					new Vector3(num13 * num4, height, num14 * num4)
				});
				array2 = array2.Add(new Face(new int[]
				{
					num + 2,
					num + 1,
					num,
					num + 2,
					num + 3,
					num + 1
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
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(array, array2);
			proBuilderMesh.gameObject.name = "Stairs";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001D940 File Offset: 0x0001BB40
		internal static ProBuilderMesh GenerateStair(PivotLocation pivotType, int steps, float width, float height, float depth, bool sidesGoToFloor, bool generateBack, bool platformsOnly)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3[] array = platformsOnly ? new Vector3[8] : new Vector3[16];
			float num = height / (float)steps;
			float num2 = depth / (float)steps;
			for (int i = 0; i < steps; i++)
			{
				float num3 = width / 2f;
				float y = (float)i * num;
				float num4 = (float)i * num2;
				if (sidesGoToFloor)
				{
					y = 0f;
				}
				float y2 = (float)i * num + num;
				array[0] = new Vector3(num3, (float)i * num, num4);
				array[1] = new Vector3(-num3, (float)i * num, num4);
				array[2] = new Vector3(num3, y2, num4);
				array[3] = new Vector3(-num3, y2, num4);
				array[4] = new Vector3(num3, y2, num4);
				array[5] = new Vector3(-num3, y2, num4);
				array[6] = new Vector3(num3, y2, num4 + num2);
				array[7] = new Vector3(-num3, y2, num4 + num2);
				if (!platformsOnly)
				{
					array[8] = new Vector3(num3, y, num4 + num2);
					array[9] = new Vector3(num3, y, num4);
					array[10] = new Vector3(num3, y2, num4 + num2);
					array[11] = new Vector3(num3, y2, num4);
					array[12] = new Vector3(-num3, y, num4);
					array[13] = new Vector3(-num3, y, num4 + num2);
					array[14] = new Vector3(-num3, y2, num4);
					array[15] = new Vector3(-num3, y2, num4 + num2);
				}
				list.AddRange(array);
			}
			if (generateBack)
			{
				list.Add(new Vector3(-width / 2f, 0f, depth));
				list.Add(new Vector3(width / 2f, 0f, depth));
				list.Add(new Vector3(-width / 2f, height, depth));
				list.Add(new Vector3(width / 2f, height, depth));
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.CreateInstanceWithPoints(list.ToArray());
			proBuilderMesh.gameObject.name = "Stairs";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001DB94 File Offset: 0x0001BD94
		public static ProBuilderMesh GenerateCube(PivotLocation pivotType, Vector3 size)
		{
			Vector3[] array = new Vector3[ShapeGenerator.k_CubeTriangles.Length];
			for (int i = 0; i < ShapeGenerator.k_CubeTriangles.Length; i++)
			{
				array[i] = Vector3.Scale(ShapeGenerator.k_CubeVertices[ShapeGenerator.k_CubeTriangles[i]], size);
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.CreateInstanceWithPoints(array);
			proBuilderMesh.gameObject.name = "Cube";
			if (pivotType == PivotLocation.Center)
			{
				foreach (Face face in proBuilderMesh.facesInternal)
				{
					face.uv = new AutoUnwrapSettings(face.uv)
					{
						offset = new Vector2(-0.5f, -0.5f)
					};
				}
			}
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001DC4C File Offset: 0x0001BE4C
		public static ProBuilderMesh GenerateCylinder(PivotLocation pivotType, int axisDivisions, float radius, float height, int heightCuts, int smoothing = -1)
		{
			if (axisDivisions % 2 != 0)
			{
				axisDivisions++;
			}
			if (axisDivisions > 64)
			{
				axisDivisions = 64;
			}
			float num = 360f / (float)axisDivisions;
			float num2 = height / (float)(heightCuts + 1);
			Vector3[] array = new Vector3[axisDivisions];
			for (int i = 0; i < axisDivisions; i++)
			{
				float f = num * (float)i * 0.017453292f;
				float x = Mathf.Cos(f) * radius;
				float z = Mathf.Sin(f) * radius;
				array[i] = new Vector3(x, 0f, z);
			}
			Vector3[] array2 = new Vector3[axisDivisions * (heightCuts + 1) * 4 + axisDivisions * 6];
			Face[] array3 = new Face[axisDivisions * (heightCuts + 1) + axisDivisions * 2];
			int num3 = 0;
			for (int j = 0; j < heightCuts + 1; j++)
			{
				float y = (float)j * num2;
				float y2 = (float)(j + 1) * num2;
				for (int k = 0; k < axisDivisions; k++)
				{
					array2[num3] = new Vector3(array[k].x, y, array[k].z);
					array2[num3 + 1] = new Vector3(array[k].x, y2, array[k].z);
					if (k != axisDivisions - 1)
					{
						array2[num3 + 2] = new Vector3(array[k + 1].x, y, array[k + 1].z);
						array2[num3 + 3] = new Vector3(array[k + 1].x, y2, array[k + 1].z);
					}
					else
					{
						array2[num3 + 2] = new Vector3(array[0].x, y, array[0].z);
						array2[num3 + 3] = new Vector3(array[0].x, y2, array[0].z);
					}
					num3 += 4;
				}
			}
			int num4 = 0;
			for (int l = 0; l < heightCuts + 1; l++)
			{
				for (int m = 0; m < axisDivisions * 4; m += 4)
				{
					int num6;
					int num5 = num6 = l * (axisDivisions * 4) + m;
					int num7 = num5 + 1;
					int num8 = num5 + 2;
					int num9 = num5 + 3;
					array3[num4++] = new Face(new int[]
					{
						num6,
						num7,
						num8,
						num7,
						num9,
						num8
					}, 0, AutoUnwrapSettings.tile, smoothing, -1, -1, false);
				}
			}
			int num10 = axisDivisions * (heightCuts + 1) * 4;
			int num11 = axisDivisions * (heightCuts + 1);
			for (int n = 0; n < axisDivisions; n++)
			{
				array2[num10] = new Vector3(array[n].x, 0f, array[n].z);
				array2[num10 + 1] = Vector3.zero;
				if (n != axisDivisions - 1)
				{
					array2[num10 + 2] = new Vector3(array[n + 1].x, 0f, array[n + 1].z);
				}
				else
				{
					array2[num10 + 2] = new Vector3(array[0].x, 0f, array[0].z);
				}
				array3[num11 + n] = new Face(new int[]
				{
					num10 + 2,
					num10 + 1,
					num10
				});
				num10 += 3;
				array2[num10] = new Vector3(array[n].x, height, array[n].z);
				array2[num10 + 1] = new Vector3(0f, height, 0f);
				if (n != axisDivisions - 1)
				{
					array2[num10 + 2] = new Vector3(array[n + 1].x, height, array[n + 1].z);
				}
				else
				{
					array2[num10 + 2] = new Vector3(array[0].x, height, array[0].z);
				}
				array3[num11 + (n + axisDivisions)] = new Face(new int[]
				{
					num10,
					num10 + 1,
					num10 + 2
				});
				num10 += 3;
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(array2, array3);
			proBuilderMesh.gameObject.name = "Cylinder";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public static ProBuilderMesh GeneratePrism(PivotLocation pivotType, Vector3 size)
		{
			size.y *= 2f;
			Vector3[] array = new Vector3[]
			{
				Vector3.Scale(new Vector3(-0.5f, 0f, -0.5f), size),
				Vector3.Scale(new Vector3(0.5f, 0f, -0.5f), size),
				Vector3.Scale(new Vector3(0f, 0.5f, -0.5f), size),
				Vector3.Scale(new Vector3(-0.5f, 0f, 0.5f), size),
				Vector3.Scale(new Vector3(0.5f, 0f, 0.5f), size),
				Vector3.Scale(new Vector3(0f, 0.5f, 0.5f), size)
			};
			Vector3[] positions = new Vector3[]
			{
				array[0],
				array[1],
				array[2],
				array[1],
				array[4],
				array[2],
				array[5],
				array[4],
				array[3],
				array[5],
				array[3],
				array[0],
				array[5],
				array[2],
				array[0],
				array[1],
				array[3],
				array[4]
			};
			Face[] array2 = new Face[5];
			int num = 0;
			int[] array3 = new int[3];
			array3[0] = 2;
			array3[1] = 1;
			array2[num] = new Face(array3);
			array2[1] = new Face(new int[]
			{
				5,
				4,
				3,
				5,
				6,
				4
			});
			array2[2] = new Face(new int[]
			{
				9,
				8,
				7
			});
			array2[3] = new Face(new int[]
			{
				12,
				11,
				10,
				12,
				13,
				11
			});
			array2[4] = new Face(new int[]
			{
				14,
				15,
				16,
				15,
				17,
				16
			});
			Face[] faces = array2;
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(positions, faces);
			proBuilderMesh.gameObject.name = "Prism";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001E33C File Offset: 0x0001C53C
		public static ProBuilderMesh GenerateDoor(PivotLocation pivotType, float totalWidth, float totalHeight, float ledgeHeight, float legWidth, float depth)
		{
			float num = totalWidth / 2f;
			legWidth = num - legWidth;
			ledgeHeight = totalHeight - ledgeHeight;
			Vector3[] array = new Vector3[]
			{
				new Vector3(-num, 0f, depth),
				new Vector3(-legWidth, 0f, depth),
				new Vector3(legWidth, 0f, depth),
				new Vector3(num, 0f, depth),
				new Vector3(-num, ledgeHeight, depth),
				new Vector3(-legWidth, ledgeHeight, depth),
				new Vector3(legWidth, ledgeHeight, depth),
				new Vector3(num, ledgeHeight, depth),
				new Vector3(-num, totalHeight, depth),
				new Vector3(-legWidth, totalHeight, depth),
				new Vector3(legWidth, totalHeight, depth),
				new Vector3(num, totalHeight, depth)
			};
			List<Vector3> list = new List<Vector3>();
			list.Add(array[0]);
			list.Add(array[1]);
			list.Add(array[4]);
			list.Add(array[5]);
			list.Add(array[2]);
			list.Add(array[3]);
			list.Add(array[6]);
			list.Add(array[7]);
			list.Add(array[4]);
			list.Add(array[5]);
			list.Add(array[8]);
			list.Add(array[9]);
			list.Add(array[6]);
			list.Add(array[7]);
			list.Add(array[10]);
			list.Add(array[11]);
			list.Add(array[5]);
			list.Add(array[6]);
			list.Add(array[9]);
			list.Add(array[10]);
			List<Vector3> list2 = new List<Vector3>();
			for (int i = 0; i < list.Count; i += 4)
			{
				list2.Add(list[i + 1] - Vector3.forward * depth);
				list2.Add(list[i] - Vector3.forward * depth);
				list2.Add(list[i + 3] - Vector3.forward * depth);
				list2.Add(list[i + 2] - Vector3.forward * depth);
			}
			list.AddRange(list2);
			list.Add(array[6]);
			list.Add(array[5]);
			list.Add(array[6] - Vector3.forward * depth);
			list.Add(array[5] - Vector3.forward * depth);
			list.Add(array[2] - Vector3.forward * depth);
			list.Add(array[2]);
			list.Add(array[6] - Vector3.forward * depth);
			list.Add(array[6]);
			list.Add(array[1]);
			list.Add(array[1] - Vector3.forward * depth);
			list.Add(array[5]);
			list.Add(array[5] - Vector3.forward * depth);
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.CreateInstanceWithPoints(list.ToArray());
			proBuilderMesh.gameObject.name = "Door";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001E720 File Offset: 0x0001C920
		public static ProBuilderMesh GeneratePlane(PivotLocation pivotType, float width, float height, int widthCuts, int heightCuts, Axis axis)
		{
			int num = widthCuts + 1;
			int num2 = heightCuts + 1;
			Vector2[] array = new Vector2[num * num2 * 4];
			Vector3[] array2 = new Vector3[num * num2 * 4];
			Face[] array3 = new Face[num * num2];
			int i = 0;
			int num3 = 0;
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					float x = (float)k * (width / (float)num) - width / 2f;
					float x2 = (float)(k + 1) * (width / (float)num) - width / 2f;
					float y = (float)j * (height / (float)num2) - height / 2f;
					float y2 = (float)(j + 1) * (height / (float)num2) - height / 2f;
					array[i] = new Vector2(x, y);
					array[i + 1] = new Vector2(x2, y);
					array[i + 2] = new Vector2(x, y2);
					array[i + 3] = new Vector2(x2, y2);
					array3[num3++] = new Face(new int[]
					{
						i,
						i + 1,
						i + 2,
						i + 1,
						i + 3,
						i + 2
					});
					i += 4;
				}
			}
			switch (axis)
			{
			case Axis.Right:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(0f, array[i].x, array[i].y);
				}
				break;
			case Axis.Left:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(0f, array[i].y, array[i].x);
				}
				break;
			case Axis.Up:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(array[i].y, 0f, array[i].x);
				}
				break;
			case Axis.Down:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(array[i].x, 0f, array[i].y);
				}
				break;
			case Axis.Forward:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(array[i].x, array[i].y, 0f);
				}
				break;
			case Axis.Backward:
				for (i = 0; i < array2.Length; i++)
				{
					array2[i] = new Vector3(array[i].y, array[i].x, 0f);
				}
				break;
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(array2, array3);
			proBuilderMesh.gameObject.name = "Plane";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		public static ProBuilderMesh GeneratePipe(PivotLocation pivotType, float radius, float height, float thickness, int subdivAxis, int subdivHeight)
		{
			Vector2[] array = new Vector2[subdivAxis];
			Vector2[] array2 = new Vector2[subdivAxis];
			for (int i = 0; i < subdivAxis; i++)
			{
				array[i] = Math.PointInCircumference(radius, (float)i * (360f / (float)subdivAxis), Vector2.zero);
				array2[i] = Math.PointInCircumference(radius - thickness, (float)i * (360f / (float)subdivAxis), Vector2.zero);
			}
			List<Vector3> list = new List<Vector3>();
			subdivHeight++;
			for (int j = 0; j < subdivHeight; j++)
			{
				float y = (float)j * (height / (float)subdivHeight);
				float y2 = (float)(j + 1) * (height / (float)subdivHeight);
				for (int k = 0; k < subdivAxis; k++)
				{
					Vector2 vector = array[k];
					Vector2 vector2 = (k < subdivAxis - 1) ? array[k + 1] : array[0];
					Vector3[] collection = new Vector3[]
					{
						new Vector3(vector2.x, y, vector2.y),
						new Vector3(vector.x, y, vector.y),
						new Vector3(vector2.x, y2, vector2.y),
						new Vector3(vector.x, y2, vector.y)
					};
					vector = array2[k];
					vector2 = ((k < subdivAxis - 1) ? array2[k + 1] : array2[0]);
					Vector3[] collection2 = new Vector3[]
					{
						new Vector3(vector.x, y, vector.y),
						new Vector3(vector2.x, y, vector2.y),
						new Vector3(vector.x, y2, vector.y),
						new Vector3(vector2.x, y2, vector2.y)
					};
					list.AddRange(collection);
					list.AddRange(collection2);
				}
			}
			for (int l = 0; l < subdivAxis; l++)
			{
				Vector2 vector = array[l];
				Vector2 vector2 = (l < subdivAxis - 1) ? array[l + 1] : array[0];
				Vector2 vector3 = array2[l];
				Vector2 vector4 = (l < subdivAxis - 1) ? array2[l + 1] : array2[0];
				Vector3[] collection3 = new Vector3[]
				{
					new Vector3(vector2.x, height, vector2.y),
					new Vector3(vector.x, height, vector.y),
					new Vector3(vector4.x, height, vector4.y),
					new Vector3(vector3.x, height, vector3.y)
				};
				Vector3[] collection4 = new Vector3[]
				{
					new Vector3(vector.x, 0f, vector.y),
					new Vector3(vector2.x, 0f, vector2.y),
					new Vector3(vector3.x, 0f, vector3.y),
					new Vector3(vector4.x, 0f, vector4.y)
				};
				list.AddRange(collection4);
				list.AddRange(collection3);
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.CreateInstanceWithPoints(list.ToArray());
			proBuilderMesh.gameObject.name = "Pipe";
			if (pivotType == PivotLocation.Center)
			{
				proBuilderMesh.SetPivot(pivotType);
			}
			else
			{
				proBuilderMesh.CenterPivot(new int[]
				{
					1
				});
			}
			return proBuilderMesh;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		public static ProBuilderMesh GenerateCone(PivotLocation pivotType, float radius, float height, int subdivAxis)
		{
			Vector3[] array = new Vector3[subdivAxis];
			for (int i = 0; i < subdivAxis; i++)
			{
				Vector2 vector = Math.PointInCircumference(radius, (float)i * (360f / (float)subdivAxis), Vector2.zero);
				array[i] = new Vector3(vector.x, 0f, vector.y);
			}
			List<Vector3> list = new List<Vector3>();
			List<Face> list2 = new List<Face>();
			for (int j = 0; j < subdivAxis; j++)
			{
				list.Add(array[j]);
				list.Add((j < subdivAxis - 1) ? array[j + 1] : array[0]);
				list.Add(Vector3.up * height);
				list.Add(array[j]);
				list.Add((j < subdivAxis - 1) ? array[j + 1] : array[0]);
				list.Add(Vector3.zero);
			}
			List<Face> list3 = new List<Face>();
			for (int k = 0; k < subdivAxis * 6; k += 6)
			{
				Face item = new Face(new int[]
				{
					k + 2,
					k + 1,
					k
				});
				list2.Add(item);
				list3.Add(item);
				list2.Add(new Face(new int[]
				{
					k + 3,
					k + 4,
					k + 5
				}));
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(list.ToArray(), list2.ToArray());
			proBuilderMesh.gameObject.name = "Cone";
			proBuilderMesh.SetPivot(pivotType);
			proBuilderMesh.unwrapParameters = new UnwrapParameters
			{
				packMargin = 30f
			};
			Face face = list3[0];
			AutoUnwrapSettings uv = face.uv;
			uv.anchor = AutoUnwrapSettings.Anchor.LowerLeft;
			face.uv = uv;
			face.manualUV = true;
			UvUnwrapping.Unwrap(proBuilderMesh, face, Vector3.up);
			for (int l = 1; l < list3.Count; l++)
			{
				Face face2 = list3[l];
				face2.manualUV = true;
				UvUnwrapping.CopyUVs(proBuilderMesh, face, face2);
			}
			proBuilderMesh.RefreshUV(list3);
			return proBuilderMesh;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		public static ProBuilderMesh GenerateArch(PivotLocation pivotType, float angle, float radius, float width, float depth, int radialCuts, bool insideFaces, bool outsideFaces, bool frontFaces, bool backFaces, bool endCaps)
		{
			Vector2[] array = new Vector2[radialCuts];
			Vector2[] array2 = new Vector2[radialCuts];
			for (int i = 0; i < radialCuts; i++)
			{
				array[i] = Math.PointInCircumference(radius, (float)i * (angle / (float)(radialCuts - 1)), Vector2.zero);
				array2[i] = Math.PointInCircumference(radius - width, (float)i * (angle / (float)(radialCuts - 1)), Vector2.zero);
			}
			List<Vector3> list = new List<Vector3>();
			float z = 0f;
			for (int j = 0; j < radialCuts - 1; j++)
			{
				Vector2 vector = array[j];
				Vector2 vector2 = (j < radialCuts - 1) ? array[j + 1] : array[j];
				Vector3[] collection = new Vector3[]
				{
					new Vector3(vector.x, vector.y, z),
					new Vector3(vector2.x, vector2.y, z),
					new Vector3(vector.x, vector.y, depth),
					new Vector3(vector2.x, vector2.y, depth)
				};
				vector = array2[j];
				vector2 = ((j < radialCuts - 1) ? array2[j + 1] : array2[j]);
				Vector3[] collection2 = new Vector3[]
				{
					new Vector3(vector2.x, vector2.y, z),
					new Vector3(vector.x, vector.y, z),
					new Vector3(vector2.x, vector2.y, depth),
					new Vector3(vector.x, vector.y, depth)
				};
				if (outsideFaces)
				{
					list.AddRange(collection);
				}
				if (j != radialCuts - 1 && insideFaces)
				{
					list.AddRange(collection2);
				}
				if (angle < 360f && endCaps)
				{
					if (j == 0)
					{
						list.AddRange(new Vector3[]
						{
							new Vector3(array[j].x, array[j].y, depth),
							new Vector3(array2[j].x, array2[j].y, depth),
							new Vector3(array[j].x, array[j].y, z),
							new Vector3(array2[j].x, array2[j].y, z)
						});
					}
					if (j == radialCuts - 2)
					{
						list.AddRange(new Vector3[]
						{
							new Vector3(array2[j + 1].x, array2[j + 1].y, depth),
							new Vector3(array[j + 1].x, array[j + 1].y, depth),
							new Vector3(array2[j + 1].x, array2[j + 1].y, z),
							new Vector3(array[j + 1].x, array[j + 1].y, z)
						});
					}
				}
			}
			for (int k = 0; k < radialCuts - 1; k++)
			{
				Vector2 vector = array[k];
				Vector2 vector2 = (k < radialCuts - 1) ? array[k + 1] : array[k];
				Vector2 vector3 = array2[k];
				Vector2 vector4 = (k < radialCuts - 1) ? array2[k + 1] : array2[k];
				Vector3[] collection3 = new Vector3[]
				{
					new Vector3(vector.x, vector.y, depth),
					new Vector3(vector2.x, vector2.y, depth),
					new Vector3(vector3.x, vector3.y, depth),
					new Vector3(vector4.x, vector4.y, depth)
				};
				Vector3[] collection4 = new Vector3[]
				{
					new Vector3(vector2.x, vector2.y, 0f),
					new Vector3(vector.x, vector.y, 0f),
					new Vector3(vector4.x, vector4.y, 0f),
					new Vector3(vector3.x, vector3.y, 0f)
				};
				if (frontFaces)
				{
					list.AddRange(collection3);
				}
				if (backFaces)
				{
					list.AddRange(collection4);
				}
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.CreateInstanceWithPoints(list.ToArray());
			proBuilderMesh.gameObject.name = "Arch";
			proBuilderMesh.SetPivot(pivotType);
			int num;
			MeshValidation.EnsureMeshIsValid(proBuilderMesh, out num);
			return proBuilderMesh;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001F510 File Offset: 0x0001D710
		public static ProBuilderMesh GenerateIcosahedron(PivotLocation pivotType, float radius, int subdivisions, bool weldVertices = true, bool manualUvs = true)
		{
			Vector3[] array = new Vector3[ShapeGenerator.k_IcosphereTriangles.Length];
			for (int i = 0; i < ShapeGenerator.k_IcosphereTriangles.Length; i += 3)
			{
				array[i] = ShapeGenerator.k_IcosphereVertices[ShapeGenerator.k_IcosphereTriangles[i]].normalized * radius;
				array[i + 1] = ShapeGenerator.k_IcosphereVertices[ShapeGenerator.k_IcosphereTriangles[i + 1]].normalized * radius;
				array[i + 2] = ShapeGenerator.k_IcosphereVertices[ShapeGenerator.k_IcosphereTriangles[i + 2]].normalized * radius;
			}
			for (int j = 0; j < subdivisions; j++)
			{
				array = ShapeGenerator.SubdivideIcosahedron(array, radius);
			}
			Face[] array2 = new Face[array.Length / 3];
			Vector3 vector = Vector3.positiveInfinity;
			int num = -1;
			for (int k = 0; k < array.Length; k += 3)
			{
				array2[k / 3] = new Face(new int[]
				{
					k,
					k + 1,
					k + 2
				});
				array2[k / 3].manualUV = manualUvs;
				for (int l = 0; l < array2[k / 3].indexes.Count; l++)
				{
					int num2 = array2[k / 3].indexes[l];
					if (array[num2].y < vector.y)
					{
						vector = array[num2];
						num = num2;
					}
				}
			}
			if (!manualUvs)
			{
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
			}
			ProBuilderMesh proBuilderMesh = new GameObject().AddComponent<ProBuilderMesh>();
			proBuilderMesh.Clear();
			proBuilderMesh.positionsInternal = array;
			proBuilderMesh.facesInternal = array2;
			if (!weldVertices)
			{
				SharedVertex[] array3 = new SharedVertex[array.Length];
				for (int n = 0; n < array3.Length; n++)
				{
					array3[n] = new SharedVertex(new int[]
					{
						n
					});
				}
				proBuilderMesh.sharedVerticesInternal = array3;
			}
			else
			{
				proBuilderMesh.sharedVerticesInternal = SharedVertex.GetSharedVerticesWithPositions(array);
			}
			proBuilderMesh.ToMesh(MeshTopology.Triangles);
			proBuilderMesh.Refresh(RefreshMask.All);
			proBuilderMesh.gameObject.name = "Icosphere";
			if (pivotType == PivotLocation.Center)
			{
				proBuilderMesh.SetPivot(pivotType);
			}
			else
			{
				proBuilderMesh.CenterPivot(new int[]
				{
					num
				});
			}
			proBuilderMesh.unwrapParameters = new UnwrapParameters
			{
				packMargin = 30f
			};
			return proBuilderMesh;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001F80C File Offset: 0x0001DA0C
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

		// Token: 0x0600032D RID: 813 RVA: 0x0001F98C File Offset: 0x0001DB8C
		private static Vector3[] GetCirclePoints(int segments, float radius, float circumference, Quaternion rotation, float offset)
		{
			float num = (float)segments - 1f;
			Vector3[] array = new Vector3[(segments - 1) * 2];
			array[0] = new Vector3(Mathf.Cos(0f / num * circumference * 0.017453292f) * radius, Mathf.Sin(0f / num * circumference * 0.017453292f) * radius, 0f);
			array[1] = new Vector3(Mathf.Cos(1f / num * circumference * 0.017453292f) * radius, Mathf.Sin(1f / num * circumference * 0.017453292f) * radius, 0f);
			array[0] = rotation * (array[0] + Vector3.right * offset);
			array[1] = rotation * (array[1] + Vector3.right * offset);
			int num2 = 2;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 2; i < segments; i++)
			{
				float f = (float)i / num * circumference * 0.017453292f;
				stringBuilder.AppendLine(f.ToString());
				array[num2] = array[num2 - 1];
				array[num2 + 1] = rotation * (new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f) + Vector3.right * offset);
				num2 += 2;
			}
			return array;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001FAF8 File Offset: 0x0001DCF8
		public static ProBuilderMesh GenerateTorus(PivotLocation pivotType, int rows, int columns, float innerRadius, float outerRadius, bool smooth, float horizontalCircumference, float verticalCircumference, bool manualUvs = false)
		{
			int num = Mathf.Clamp(rows + 1, 4, 128);
			int num2 = Mathf.Clamp(columns + 1, 4, 128);
			float num3 = Mathf.Clamp(innerRadius, 0.01f, 2048f);
			float num4 = Mathf.Clamp(outerRadius, 0.01f, num3 - 0.001f);
			num3 -= num4;
			float num5 = Mathf.Clamp(horizontalCircumference, 0.01f, 360f);
			float circumference = Mathf.Clamp(verticalCircumference, 0.01f, 360f);
			List<Vector3> list = new List<Vector3>();
			int num6 = num2 - 1;
			Vector3[] circlePoints = ShapeGenerator.GetCirclePoints(num, num4, circumference, Quaternion.Euler(Vector3.zero), num3);
			for (int i = 1; i < num2; i++)
			{
				list.AddRange(circlePoints);
				Quaternion rotation = Quaternion.Euler(Vector3.up * ((float)i / (float)num6 * num5));
				circlePoints = ShapeGenerator.GetCirclePoints(num, num4, circumference, rotation, num3);
				list.AddRange(circlePoints);
			}
			List<Face> list2 = new List<Face>();
			int num7 = 0;
			for (int j = 0; j < (num2 - 1) * 2; j += 2)
			{
				for (int k = 0; k < num - 1; k++)
				{
					int num8 = j * ((num - 1) * 2) + k * 2;
					int num9 = (j + 1) * ((num - 1) * 2) + k * 2;
					int num10 = j * ((num - 1) * 2) + k * 2 + 1;
					int num11 = (j + 1) * ((num - 1) * 2) + k * 2 + 1;
					list2.Add(new Face(new int[]
					{
						num8,
						num9,
						num10,
						num9,
						num11,
						num10
					}));
					list2[num7].smoothingGroup = (smooth ? 1 : -1);
					list2[num7].manualUV = manualUvs;
					num7++;
				}
			}
			ProBuilderMesh proBuilderMesh = ProBuilderMesh.Create(list.ToArray(), list2.ToArray());
			proBuilderMesh.gameObject.name = "Torus";
			proBuilderMesh.SetPivot(pivotType);
			return proBuilderMesh;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeGenerator()
		{
		}

		// Token: 0x040001EE RID: 494
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

		// Token: 0x040001EF RID: 495
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

		// Token: 0x040001F0 RID: 496
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

		// Token: 0x040001F1 RID: 497
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
