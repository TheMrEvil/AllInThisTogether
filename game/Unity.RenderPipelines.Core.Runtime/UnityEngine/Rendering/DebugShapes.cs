using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006C RID: 108
	public class DebugShapes
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000FFD7 File Offset: 0x0000E1D7
		public static DebugShapes instance
		{
			get
			{
				if (DebugShapes.s_Instance == null)
				{
					DebugShapes.s_Instance = new DebugShapes();
				}
				return DebugShapes.s_Instance;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		private void BuildSphere(ref Mesh outputMesh, float radius, uint longSubdiv, uint latSubdiv)
		{
			outputMesh.Clear();
			Vector3[] array = new Vector3[(longSubdiv + 1U) * latSubdiv + 2U];
			float num = 3.1415927f;
			float num2 = num * 2f;
			array[0] = Vector3.up * radius;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)latSubdiv))
			{
				float f = num * (float)(num3 + 1) / (latSubdiv + 1U);
				float num4 = Mathf.Sin(f);
				float y = Mathf.Cos(f);
				int num5 = 0;
				while ((long)num5 <= (long)((ulong)longSubdiv))
				{
					float f2 = num2 * (float)(((long)num5 == (long)((ulong)longSubdiv)) ? 0 : num5) / longSubdiv;
					float num6 = Mathf.Sin(f2);
					float num7 = Mathf.Cos(f2);
					array[(int)(checked((IntPtr)(unchecked((long)num5 + (long)num3 * (long)((ulong)(longSubdiv + 1U)) + 1L))))] = new Vector3(num4 * num7, y, num4 * num6) * radius;
					num5++;
				}
				num3++;
			}
			array[array.Length - 1] = Vector3.up * -radius;
			Vector3[] array2 = new Vector3[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].normalized;
			}
			Vector2[] array3 = new Vector2[array.Length];
			array3[0] = Vector2.up;
			array3[array3.Length - 1] = Vector2.zero;
			int num8 = 0;
			while ((long)num8 < (long)((ulong)latSubdiv))
			{
				int num9 = 0;
				while ((long)num9 <= (long)((ulong)longSubdiv))
				{
					array3[(int)(checked((IntPtr)(unchecked((long)num9 + (long)num8 * (long)((ulong)(longSubdiv + 1U)) + 1L))))] = new Vector2((float)num9 / longSubdiv, 1f - (float)(num8 + 1) / (latSubdiv + 1U));
					num9++;
				}
				num8++;
			}
			int[] array4 = new int[array.Length * 2 * 3];
			int num10 = 0;
			int num11 = 0;
			while ((long)num11 < (long)((ulong)longSubdiv))
			{
				array4[num10++] = num11 + 2;
				array4[num10++] = num11 + 1;
				array4[num10++] = 0;
				num11++;
			}
			for (uint num12 = 0U; num12 < latSubdiv - 1U; num12 += 1U)
			{
				for (uint num13 = 0U; num13 < longSubdiv; num13 += 1U)
				{
					uint num14 = num13 + num12 * (longSubdiv + 1U) + 1U;
					uint num15 = num14 + longSubdiv + 1U;
					array4[num10++] = (int)num14;
					array4[num10++] = (int)(num14 + 1U);
					array4[num10++] = (int)(num15 + 1U);
					array4[num10++] = (int)num14;
					array4[num10++] = (int)(num15 + 1U);
					array4[num10++] = (int)num15;
				}
			}
			int num16 = 0;
			while ((long)num16 < (long)((ulong)longSubdiv))
			{
				array4[num10++] = array.Length - 1;
				array4[num10++] = array.Length - (num16 + 2) - 1;
				array4[num10++] = array.Length - (num16 + 1) - 1;
				num16++;
			}
			outputMesh.vertices = array;
			outputMesh.normals = array2;
			outputMesh.uv = array3;
			outputMesh.triangles = array4;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000102D8 File Offset: 0x0000E4D8
		private void BuildBox(ref Mesh outputMesh, float length, float width, float height)
		{
			outputMesh.Clear();
			Vector3 vector = new Vector3(-length * 0.5f, -width * 0.5f, height * 0.5f);
			Vector3 vector2 = new Vector3(length * 0.5f, -width * 0.5f, height * 0.5f);
			Vector3 vector3 = new Vector3(length * 0.5f, -width * 0.5f, -height * 0.5f);
			Vector3 vector4 = new Vector3(-length * 0.5f, -width * 0.5f, -height * 0.5f);
			Vector3 vector5 = new Vector3(-length * 0.5f, width * 0.5f, height * 0.5f);
			Vector3 vector6 = new Vector3(length * 0.5f, width * 0.5f, height * 0.5f);
			Vector3 vector7 = new Vector3(length * 0.5f, width * 0.5f, -height * 0.5f);
			Vector3 vector8 = new Vector3(-length * 0.5f, width * 0.5f, -height * 0.5f);
			Vector3[] vertices = new Vector3[]
			{
				vector,
				vector2,
				vector3,
				vector4,
				vector8,
				vector5,
				vector,
				vector4,
				vector5,
				vector6,
				vector2,
				vector,
				vector7,
				vector8,
				vector4,
				vector3,
				vector6,
				vector7,
				vector3,
				vector2,
				vector8,
				vector7,
				vector6,
				vector5
			};
			Vector3 up = Vector3.up;
			Vector3 down = Vector3.down;
			Vector3 forward = Vector3.forward;
			Vector3 back = Vector3.back;
			Vector3 left = Vector3.left;
			Vector3 right = Vector3.right;
			Vector3[] normals = new Vector3[]
			{
				down,
				down,
				down,
				down,
				left,
				left,
				left,
				left,
				forward,
				forward,
				forward,
				forward,
				back,
				back,
				back,
				back,
				right,
				right,
				right,
				right,
				up,
				up,
				up,
				up
			};
			Vector2 vector9 = new Vector2(0f, 0f);
			Vector2 vector10 = new Vector2(1f, 0f);
			Vector2 vector11 = new Vector2(0f, 1f);
			Vector2 vector12 = new Vector2(1f, 1f);
			Vector2[] uv = new Vector2[]
			{
				vector12,
				vector11,
				vector9,
				vector10,
				vector12,
				vector11,
				vector9,
				vector10,
				vector12,
				vector11,
				vector9,
				vector10,
				vector12,
				vector11,
				vector9,
				vector10,
				vector12,
				vector11,
				vector9,
				vector10,
				vector12,
				vector11,
				vector9,
				vector10
			};
			int[] triangles = new int[]
			{
				3,
				1,
				0,
				3,
				2,
				1,
				7,
				5,
				4,
				7,
				6,
				5,
				11,
				9,
				8,
				11,
				10,
				9,
				15,
				13,
				12,
				15,
				14,
				13,
				19,
				17,
				16,
				19,
				18,
				17,
				23,
				21,
				20,
				23,
				22,
				21
			};
			outputMesh.vertices = vertices;
			outputMesh.normals = normals;
			outputMesh.uv = uv;
			outputMesh.triangles = triangles;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010754 File Offset: 0x0000E954
		private void BuildCone(ref Mesh outputMesh, float height, float topRadius, float bottomRadius, int nbSides)
		{
			outputMesh.Clear();
			int num = nbSides + 1;
			Vector3[] array = new Vector3[num + num + nbSides * 2 + 2];
			int i = 0;
			float num2 = 6.2831855f;
			array[i++] = new Vector3(0f, 0f, 0f);
			while (i <= nbSides)
			{
				float f = (float)i / (float)nbSides * num2;
				array[i] = new Vector3(Mathf.Sin(f) * bottomRadius, Mathf.Cos(f) * bottomRadius, 0f);
				i++;
			}
			array[i++] = new Vector3(0f, 0f, height);
			while (i <= nbSides * 2 + 1)
			{
				float f2 = (float)(i - nbSides - 1) / (float)nbSides * num2;
				array[i] = new Vector3(Mathf.Sin(f2) * topRadius, Mathf.Cos(f2) * topRadius, height);
				i++;
			}
			int num3 = 0;
			while (i <= array.Length - 4)
			{
				float f3 = (float)num3 / (float)nbSides * num2;
				array[i] = new Vector3(Mathf.Sin(f3) * topRadius, Mathf.Cos(f3) * topRadius, height);
				array[i + 1] = new Vector3(Mathf.Sin(f3) * bottomRadius, Mathf.Cos(f3) * bottomRadius, 0f);
				i += 2;
				num3++;
			}
			array[i] = array[nbSides * 2 + 2];
			array[i + 1] = array[nbSides * 2 + 3];
			Vector3[] array2 = new Vector3[array.Length];
			i = 0;
			while (i <= nbSides)
			{
				array2[i++] = new Vector3(0f, 0f, -1f);
			}
			while (i <= nbSides * 2 + 1)
			{
				array2[i++] = new Vector3(0f, 0f, 1f);
			}
			num3 = 0;
			while (i <= array.Length - 4)
			{
				float f4 = (float)num3 / (float)nbSides * num2;
				float y = Mathf.Cos(f4);
				float x = Mathf.Sin(f4);
				array2[i] = new Vector3(x, y, 0f);
				array2[i + 1] = array2[i];
				i += 2;
				num3++;
			}
			array2[i] = array2[nbSides * 2 + 2];
			array2[i + 1] = array2[nbSides * 2 + 3];
			Vector2[] array3 = new Vector2[array.Length];
			int j = 0;
			array3[j++] = new Vector2(0.5f, 0.5f);
			while (j <= nbSides)
			{
				float f5 = (float)j / (float)nbSides * num2;
				array3[j] = new Vector2(Mathf.Cos(f5) * 0.5f + 0.5f, Mathf.Sin(f5) * 0.5f + 0.5f);
				j++;
			}
			array3[j++] = new Vector2(0.5f, 0.5f);
			while (j <= nbSides * 2 + 1)
			{
				float f6 = (float)j / (float)nbSides * num2;
				array3[j] = new Vector2(Mathf.Cos(f6) * 0.5f + 0.5f, Mathf.Sin(f6) * 0.5f + 0.5f);
				j++;
			}
			int num4 = 0;
			while (j <= array3.Length - 4)
			{
				float x2 = (float)num4 / (float)nbSides;
				array3[j] = new Vector3(x2, 1f);
				array3[j + 1] = new Vector3(x2, 0f);
				j += 2;
				num4++;
			}
			array3[j] = new Vector2(1f, 1f);
			array3[j + 1] = new Vector2(1f, 0f);
			int num5 = nbSides + nbSides + nbSides * 2;
			int[] array4 = new int[num5 * 3 + 3];
			int k = 0;
			int num6 = 0;
			while (k < nbSides - 1)
			{
				array4[num6] = 0;
				array4[num6 + 1] = k + 1;
				array4[num6 + 2] = k + 2;
				k++;
				num6 += 3;
			}
			array4[num6] = 0;
			array4[num6 + 1] = k + 1;
			array4[num6 + 2] = 1;
			k++;
			num6 += 3;
			while (k < nbSides * 2)
			{
				array4[num6] = k + 2;
				array4[num6 + 1] = k + 1;
				array4[num6 + 2] = num;
				k++;
				num6 += 3;
			}
			array4[num6] = num + 1;
			array4[num6 + 1] = k + 1;
			array4[num6 + 2] = num;
			k++;
			num6 += 3;
			k++;
			while (k <= num5)
			{
				array4[num6] = k + 2;
				array4[num6 + 1] = k + 1;
				array4[num6 + 2] = k;
				k++;
				num6 += 3;
				array4[num6] = k + 1;
				array4[num6 + 1] = k + 2;
				array4[num6 + 2] = k;
				k++;
				num6 += 3;
			}
			outputMesh.vertices = array;
			outputMesh.normals = array2;
			outputMesh.uv = array3;
			outputMesh.triangles = array4;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010C74 File Offset: 0x0000EE74
		private void BuildPyramid(ref Mesh outputMesh, float width, float height, float depth)
		{
			outputMesh.Clear();
			Vector3[] array = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(width / 2f, height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(width / 2f, height / 2f, depth),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(0f, 0f, 0f),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(-width / 2f, height / 2f, depth),
				new Vector3(-width / 2f, -height / 2f, depth),
				new Vector3(width / 2f, -height / 2f, depth),
				new Vector3(width / 2f, height / 2f, depth)
			};
			Vector3[] normals = new Vector3[array.Length];
			Vector2[] uv = new Vector2[array.Length];
			int[] array2 = new int[18];
			for (int i = 0; i < 12; i++)
			{
				array2[i] = i;
			}
			array2[12] = 12;
			array2[13] = 13;
			array2[14] = 14;
			array2[15] = 12;
			array2[16] = 14;
			array2[17] = 15;
			outputMesh.vertices = array;
			outputMesh.normals = normals;
			outputMesh.uv = uv;
			outputMesh.triangles = array2;
			outputMesh.RecalculateBounds();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010EDC File Offset: 0x0000F0DC
		private void BuildShapes()
		{
			this.m_sphereMesh = new Mesh();
			this.BuildSphere(ref this.m_sphereMesh, 1f, 24U, 16U);
			this.m_boxMesh = new Mesh();
			this.BuildBox(ref this.m_boxMesh, 1f, 1f, 1f);
			this.m_coneMesh = new Mesh();
			this.BuildCone(ref this.m_coneMesh, 1f, 1f, 0f, 16);
			this.m_pyramidMesh = new Mesh();
			this.BuildPyramid(ref this.m_pyramidMesh, 1f, 1f, 1f);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010F7D File Offset: 0x0000F17D
		private void RebuildResources()
		{
			if (this.m_sphereMesh == null || this.m_boxMesh == null || this.m_coneMesh == null || this.m_pyramidMesh == null)
			{
				this.BuildShapes();
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010FBD File Offset: 0x0000F1BD
		public Mesh RequestSphereMesh()
		{
			this.RebuildResources();
			return this.m_sphereMesh;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00010FCB File Offset: 0x0000F1CB
		public Mesh RequestBoxMesh()
		{
			this.RebuildResources();
			return this.m_boxMesh;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00010FD9 File Offset: 0x0000F1D9
		public Mesh RequestConeMesh()
		{
			this.RebuildResources();
			return this.m_coneMesh;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010FE7 File Offset: 0x0000F1E7
		public Mesh RequestPyramidMesh()
		{
			this.RebuildResources();
			return this.m_pyramidMesh;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010FF5 File Offset: 0x0000F1F5
		public DebugShapes()
		{
		}

		// Token: 0x04000243 RID: 579
		private static DebugShapes s_Instance;

		// Token: 0x04000244 RID: 580
		private Mesh m_sphereMesh;

		// Token: 0x04000245 RID: 581
		private Mesh m_boxMesh;

		// Token: 0x04000246 RID: 582
		private Mesh m_coneMesh;

		// Token: 0x04000247 RID: 583
		private Mesh m_pyramidMesh;
	}
}
