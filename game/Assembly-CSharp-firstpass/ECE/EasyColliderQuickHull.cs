using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ECE
{
	// Token: 0x02000080 RID: 128
	public class EasyColliderQuickHull
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00022374 File Offset: 0x00020574
		public static EasyColliderQuickHull CalculateHull(List<Vector3> points)
		{
			EasyColliderQuickHull easyColliderQuickHull = new EasyColliderQuickHull();
			easyColliderQuickHull.GenerateHull(points);
			return easyColliderQuickHull;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00022384 File Offset: 0x00020584
		public static EasyColliderQuickHull CalculateHullWorld(List<Vector3> points, Transform attachTo)
		{
			List<Vector3> list = new List<Vector3>();
			foreach (Vector3 position in points)
			{
				list.Add(attachTo.InverseTransformPoint(position));
			}
			EasyColliderQuickHull easyColliderQuickHull = new EasyColliderQuickHull();
			easyColliderQuickHull.GenerateHull(list);
			return easyColliderQuickHull;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000223EC File Offset: 0x000205EC
		public static MeshColliderData CalculateHullData(List<Vector3> points, Transform attachTo)
		{
			if (points == null || points.Count < 4)
			{
				return new MeshColliderData();
			}
			EasyColliderQuickHull easyColliderQuickHull = EasyColliderQuickHull.CalculateHullWorld(points, attachTo);
			return new MeshColliderData
			{
				ConvexMesh = easyColliderQuickHull.Result,
				IsValid = true,
				Matrix = attachTo.localToWorldMatrix,
				ColliderType = CREATE_COLLIDER_TYPE.CONVEX_MESH
			};
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00022440 File Offset: 0x00020640
		public static MeshColliderData CalculateHullData(List<Vector3> points)
		{
			EasyColliderQuickHull easyColliderQuickHull = EasyColliderQuickHull.CalculateHull(points);
			return new MeshColliderData
			{
				ConvexMesh = easyColliderQuickHull.Result,
				IsValid = true,
				ColliderType = CREATE_COLLIDER_TYPE.CONVEX_MESH
			};
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00022474 File Offset: 0x00020674
		private void AddToOutsideSet(EasyColliderQuickHull.Face face, HashSet<int> vertices)
		{
			foreach (int num in vertices)
			{
				if (!this.AssignedVertices.Contains(num) && !this.ClosedVertices.Contains(num))
				{
					float num2 = this.DistanceFromPlane(this.VerticesList[num], face.Normal, this.VerticesList[face.V0]);
					if (this.IsApproxZero(num2))
					{
						if (this.IsVertOnFace(num, face))
						{
							this.ClosedVertices.Add(num);
						}
					}
					else if (num2 > 0f)
					{
						this.AssignedVertices.Add(num);
						face.OutsideVertices.Add(num);
					}
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00022550 File Offset: 0x00020750
		private bool AreVertsCoincident(Vector3 a, Vector3 b)
		{
			return Mathf.Abs(a.x - b.x) <= this.Epsilon && Mathf.Abs(a.y - b.y) <= this.Epsilon && Mathf.Abs(a.z - b.z) <= this.Epsilon;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000225B0 File Offset: 0x000207B0
		private bool AreVertsCoincident(int a, int b)
		{
			return Mathf.Abs(this.VerticesList[a].x - this.VerticesList[b].x) <= this.Epsilon && Mathf.Abs(this.VerticesList[a].y - this.VerticesList[b].y) <= this.Epsilon && Mathf.Abs(this.VerticesList[a].z - this.VerticesList[b].z) <= this.Epsilon;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00022650 File Offset: 0x00020850
		private void CloseUnAssignedVertsOnFaces()
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (EasyColliderQuickHull.Face face in this.Faces)
			{
				if (face.OnConvexHull)
				{
					foreach (int num in this.UnAssignedVertices)
					{
						if (!this.ClosedVertices.Contains(num) && this.IsVertOnFace(num, face))
						{
							hashSet.Add(num);
							this.ClosedVertices.Add(num);
						}
					}
				}
			}
			this.UnAssignedVertices.ExceptWith(hashSet);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00022724 File Offset: 0x00020924
		private bool IsVertOnFace(int i, EasyColliderQuickHull.Face face)
		{
			if (this.AreVertsCoincident(i, face.V0) || this.AreVertsCoincident(i, face.V1) || this.AreVertsCoincident(i, face.V2))
			{
				return true;
			}
			float a = this.CalcTriangleArea(face.V0, face.V1, face.V2);
			float num = this.CalcTriangleArea(i, face.V0, face.V1);
			float num2 = this.CalcTriangleArea(i, face.V1, face.V2);
			float num3 = this.CalcTriangleArea(i, face.V2, face.V0);
			return this.isApproxEqual(a, num + num2 + num3);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000227C8 File Offset: 0x000209C8
		private Vector3 CalcNormal(Vector3 a, Vector3 b, Vector3 c)
		{
			return Vector3.Cross(b - a, c - a).normalized;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000227F0 File Offset: 0x000209F0
		private Vector3 CalcNormal(int a, int b, int c)
		{
			return Vector3.Cross(this.VerticesList[b] - this.VerticesList[a], this.VerticesList[c] - this.VerticesList[a]).normalized;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00022844 File Offset: 0x00020A44
		private float CalcTriangleArea(int v0, int v1, int v2)
		{
			return 0.5f * Vector3.Cross(this.VerticesList[v1] - this.VerticesList[v0], this.VerticesList[v2] - this.VerticesList[v1]).magnitude;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000228A0 File Offset: 0x00020AA0
		private void CalculateHorizon(int eyePoint, EasyColliderQuickHull.Horizon crossedEdge, int currFace, bool firstFace = true)
		{
			float num = this.DistanceFromPlane(this.VerticesList[eyePoint], this.Faces[currFace].Normal, this.VerticesList[this.Faces[currFace].V0]);
			if (!this.Faces[currFace].OnConvexHull)
			{
				crossedEdge.OnConvexHull = false;
				return;
			}
			if (num > 0f)
			{
				this.Faces[currFace].OnConvexHull = false;
				this.UnAssignedVertices.UnionWith(this.Faces[currFace].OutsideVertices);
				this.Faces[currFace].OutsideVertices.Clear();
				if (!firstFace)
				{
					crossedEdge.OnConvexHull = false;
				}
				if (firstFace)
				{
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V0, this.Faces[currFace].V1, this.Faces[currFace].F0, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F0, false);
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V1, this.Faces[currFace].V2, this.Faces[currFace].F1, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F1, false);
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V2, this.Faces[currFace].V0, this.Faces[currFace].F2, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F2, false);
					return;
				}
				if (this.Faces[currFace].F0 == crossedEdge.From)
				{
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V1, this.Faces[currFace].V2, this.Faces[currFace].F1, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F1, false);
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V2, this.Faces[currFace].V0, this.Faces[currFace].F2, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F2, false);
					return;
				}
				if (this.Faces[currFace].F1 == crossedEdge.From)
				{
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V2, this.Faces[currFace].V0, this.Faces[currFace].F2, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F2, false);
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V0, this.Faces[currFace].V1, this.Faces[currFace].F0, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F0, false);
					return;
				}
				if (this.Faces[currFace].F2 == crossedEdge.From)
				{
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V0, this.Faces[currFace].V1, this.Faces[currFace].F0, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F0, false);
					this.CurrentHorizon.Add(new EasyColliderQuickHull.Horizon(this.Faces[currFace].V1, this.Faces[currFace].V2, this.Faces[currFace].F1, currFace));
					this.CalculateHorizon(eyePoint, this.CurrentHorizon[this.CurrentHorizon.Count - 1], this.Faces[currFace].F1, false);
				}
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00022DE0 File Offset: 0x00020FE0
		private Mesh CreateMesh(List<EasyColliderQuickHull.Face> allFaces)
		{
			Mesh mesh = new Mesh();
			List<Vector3> list = new List<Vector3>();
			List<EasyColliderQuickHull.Face> list2 = (from face in allFaces
			where face.OnConvexHull
			select face).ToList<EasyColliderQuickHull.Face>();
			List<Vector3> list3 = new List<Vector3>();
			int[] array = new int[list2.Count * 3];
			for (int i = 0; i < list2.Count; i++)
			{
				int num = list.IndexOf(this.VerticesList[list2[i].V0]);
				int num2 = list.IndexOf(this.VerticesList[list2[i].V1]);
				int num3 = list.IndexOf(this.VerticesList[list2[i].V2]);
				if (num < 0)
				{
					list3.Add(list2[i].Normal);
					list.Add(this.VerticesList[list2[i].V0]);
					num = list.Count - 1;
				}
				else
				{
					list3[num] = (list3[num] + list2[i].Normal).normalized;
				}
				if (num2 < 0)
				{
					list3.Add(list2[i].Normal);
					list.Add(this.VerticesList[list2[i].V1]);
					num2 = list.Count - 1;
				}
				else
				{
					list3[num2] = (list3[num2] + list2[i].Normal).normalized;
				}
				if (num3 < 0)
				{
					list3.Add(list2[i].Normal);
					list.Add(this.VerticesList[list2[i].V2]);
					num3 = list.Count - 1;
				}
				else
				{
					list3[num3] = (list3[num3] + list2[i].Normal).normalized;
				}
				array[i * 3] = num;
				array[i * 3 + 1] = num2;
				array[i * 3 + 2] = num3;
			}
			mesh.SetVertices(list);
			mesh.SetTriangles(array, 0);
			mesh.SetNormals(list3);
			return mesh;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002303C File Offset: 0x0002123C
		private float DistanceFromLine(Vector3 point, Vector3 line, Vector3 pointOnLine)
		{
			float d = Vector3.Dot(point - pointOnLine, line);
			return Vector3.Distance(pointOnLine + d * line, point);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002306A File Offset: 0x0002126A
		private float DistanceFromPlane(Vector3 point, Plane p)
		{
			return p.GetDistanceToPoint(point);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00023074 File Offset: 0x00021274
		private float DistanceFromPlane(Vector3 point, Vector3 normal, Vector3 pointOnPlane)
		{
			return Vector3.Dot(normal, point - pointOnPlane);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00023084 File Offset: 0x00021284
		private bool FindInitialHull(List<Vector3> points)
		{
			bool flag = false;
			List<int> list;
			if (this.FindInitialPoints(points, out list))
			{
				flag = true;
			}
			else if (this.FindInitialPointsFallBack(points, out list))
			{
				flag = true;
			}
			if (!flag)
			{
				return false;
			}
			float b = float.NegativeInfinity;
			int num = 0;
			Vector3 normalized = (points[list[1]] - points[list[0]]).normalized;
			int index = 0;
			for (int i = 2; i < 6; i++)
			{
				float num2 = this.DistanceFromLine(points[list[i]], normalized, points[list[0]]);
				if (this.isAGreaterThanB(num2, b))
				{
					b = num2;
					num = list[i];
					index = i;
				}
			}
			list[index] = list[2];
			list[2] = num;
			b = float.NegativeInfinity;
			Plane p = new Plane(points[list[0]], points[list[1]], points[num]);
			int num3 = -1;
			for (int j = 2; j < 6; j++)
			{
				if (list[j] != num)
				{
					float num4 = this.DistanceFromPlane(points[list[j]], p);
					if (!this.IsApproxZero(num4) && this.isAGreaterThanB(Mathf.Abs(num4), b))
					{
						num3 = list[j];
						b = num4;
						index = j;
					}
				}
			}
			if (num3 == -1)
			{
				return false;
			}
			list[index] = list[3];
			list[3] = num3;
			if (this.DistanceFromPlane(points[num3], p) < 0f)
			{
				int value = list[2];
				list[2] = list[0];
				list[0] = value;
			}
			this.Faces.Add(new EasyColliderQuickHull.Face(list[0], list[2], list[1], this.CalcNormal(points[list[0]], points[list[2]], points[list[1]]), 2, 3, 1));
			this.Faces.Add(new EasyColliderQuickHull.Face(list[0], list[1], list[3], this.CalcNormal(points[list[0]], points[list[1]], points[list[3]]), 0, 3, 2));
			this.Faces.Add(new EasyColliderQuickHull.Face(list[0], list[3], list[2], this.CalcNormal(points[list[0]], points[list[3]], points[list[2]]), 1, 3, 0));
			this.Faces.Add(new EasyColliderQuickHull.Face(list[1], list[2], list[3], this.CalcNormal(points[list[1]], points[list[2]], points[list[3]]), 0, 2, 1));
			this.UnAssignedVertices.UnionWith(Enumerable.Range(0, points.Count));
			this.AssignedVertices = new HashSet<int>();
			foreach (EasyColliderQuickHull.Face face in this.Faces)
			{
				this.AddToOutsideSet(face, this.UnAssignedVertices);
			}
			this.ClosedVertices.UnionWith(this.UnAssignedVertices);
			this.ClosedVertices.ExceptWith(this.AssignedVertices);
			return true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00023420 File Offset: 0x00021620
		private bool FindInitialPointsFallBack(List<Vector3> points, out List<int> initialPoints)
		{
			List<int> ips = new List<int>(6)
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			};
			initialPoints = new List<int>(6)
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			};
			Vector3 vector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			Vector3 vector3;
			Vector3 vector2 = vector3 = vector;
			Vector3 vector4 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			Vector3 vector6;
			Vector3 vector5 = vector6 = vector4;
			int i = 0;
			Predicate<int> <>9__0;
			Predicate<int> <>9__1;
			Predicate<int> <>9__2;
			Predicate<int> <>9__3;
			Predicate<int> <>9__4;
			Predicate<int> <>9__5;
			while (i < points.Count)
			{
				if (this.isALessThanB(points[i].x, vector.x))
				{
					goto IL_113;
				}
				if (this.isApproxEqual(points[i].x, vector.x))
				{
					List<int> list = initialPoints;
					Predicate<int> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((int element) => element == ips[0]));
					}
					if (list.FindAll(match).Count > 1)
					{
						goto IL_113;
					}
				}
				IL_134:
				if (this.isAGreaterThanB(points[i].x, vector4.x))
				{
					goto IL_19C;
				}
				if (this.isApproxEqual(points[i].x, vector4.x))
				{
					List<int> list2 = initialPoints;
					Predicate<int> match2;
					if ((match2 = <>9__1) == null)
					{
						match2 = (<>9__1 = ((int element) => element == ips[1]));
					}
					if (list2.FindAll(match2).Count > 1)
					{
						goto IL_19C;
					}
				}
				IL_1BE:
				if (this.isALessThanB(points[i].y, vector2.y))
				{
					goto IL_224;
				}
				if (this.isApproxEqual(points[i].y, vector2.y))
				{
					List<int> list3 = initialPoints;
					Predicate<int> match3;
					if ((match3 = <>9__2) == null)
					{
						match3 = (<>9__2 = ((int element) => element == ips[2]));
					}
					if (list3.FindAll(match3).Count > 1)
					{
						goto IL_224;
					}
				}
				IL_245:
				if (this.isAGreaterThanB(points[i].y, vector5.y))
				{
					goto IL_2AD;
				}
				if (this.isApproxEqual(points[i].y, vector5.y))
				{
					List<int> list4 = initialPoints;
					Predicate<int> match4;
					if ((match4 = <>9__3) == null)
					{
						match4 = (<>9__3 = ((int element) => element == ips[3]));
					}
					if (list4.FindAll(match4).Count > 1)
					{
						goto IL_2AD;
					}
				}
				IL_2CF:
				if (this.isALessThanB(points[i].z, vector3.z))
				{
					goto IL_335;
				}
				if (this.isApproxEqual(points[i].z, vector3.z))
				{
					List<int> list5 = initialPoints;
					Predicate<int> match5;
					if ((match5 = <>9__4) == null)
					{
						match5 = (<>9__4 = ((int element) => element == ips[4]));
					}
					if (list5.FindAll(match5).Count > 1)
					{
						goto IL_335;
					}
				}
				IL_356:
				if (this.isAGreaterThanB(points[i].z, vector6.z))
				{
					goto IL_3BE;
				}
				if (this.isApproxEqual(points[i].z, vector6.z))
				{
					List<int> list6 = initialPoints;
					Predicate<int> match6;
					if ((match6 = <>9__5) == null)
					{
						match6 = (<>9__5 = ((int element) => element == ips[5]));
					}
					if (list6.FindAll(match6).Count > 1)
					{
						goto IL_3BE;
					}
				}
				IL_3E0:
				i++;
				continue;
				IL_3BE:
				initialPoints[5] = i;
				ips[5] = i;
				vector6 = points[i];
				goto IL_3E0;
				IL_335:
				initialPoints[4] = i;
				ips[4] = i;
				vector3 = points[i];
				goto IL_356;
				IL_2AD:
				initialPoints[3] = i;
				ips[3] = i;
				vector5 = points[i];
				goto IL_2CF;
				IL_224:
				initialPoints[2] = i;
				ips[2] = i;
				vector2 = points[i];
				goto IL_245;
				IL_19C:
				initialPoints[1] = i;
				ips[1] = i;
				vector4 = points[i];
				goto IL_1BE;
				IL_113:
				initialPoints[0] = i;
				ips[0] = i;
				vector = points[i];
				goto IL_134;
			}
			return !this.isApproxEqual(vector.x, vector4.x) && !this.isApproxEqual(vector2.y, vector5.y) && !this.isApproxEqual(vector3.z, vector6.z);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00023864 File Offset: 0x00021A64
		private bool FindInitialPoints(List<Vector3> points, out List<int> initialPoints)
		{
			initialPoints = new List<int>(6)
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			};
			Vector3 b = Vector3.zero;
			for (int i = 0; i < points.Count; i++)
			{
				if (i + 3 < points.Count && i + 2 < points.Count && i + 1 < points.Count)
				{
					Vector3 a = points[i];
					Vector3 a2 = points[i + 1];
					Vector3 a3 = points[i + 2];
					b = points[i + 3];
					float a4 = Mathf.Abs(Vector3.Dot(a - b, Vector3.Cross(a2 - b, a3 - b))) / 6f;
					if (!this.IsApproxZero(a4))
					{
						initialPoints[0] = i;
						initialPoints[1] = i + 1;
						initialPoints[2] = i + 2;
						initialPoints[3] = i + 3;
						if (i + 4 < points.Count)
						{
							initialPoints[4] = i + 4;
						}
						else
						{
							initialPoints[4] = i;
						}
						if (i + 5 < points.Count)
						{
							initialPoints[5] = i + 5;
						}
						else
						{
							initialPoints[5] = i;
						}
						return true;
					}
					for (int j = i + 4; j < points.Count; j++)
					{
						b = points[j];
						a4 = Mathf.Abs(Vector3.Dot(a - b, Vector3.Cross(a2 - b, a3 - b))) / 6f;
						if (!this.IsApproxZero(a4))
						{
							initialPoints[0] = i;
							initialPoints[1] = i + 1;
							initialPoints[2] = i + 2;
							initialPoints[3] = j;
							if (i + 4 < points.Count)
							{
								initialPoints[4] = i + 4;
							}
							else
							{
								initialPoints[4] = i;
							}
							if (i + 5 < points.Count)
							{
								initialPoints[5] = i + 5;
							}
							else
							{
								initialPoints[5] = i;
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00023AA3 File Offset: 0x00021CA3
		public bool isFinished
		{
			get
			{
				return this.Result != null;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00023AB4 File Offset: 0x00021CB4
		private void CalculateEpsilon(List<Vector3> points)
		{
			Vector3 vector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			Vector3 vector2 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			foreach (Vector3 vector3 in points)
			{
				if (vector3.x < vector.x)
				{
					vector.x = vector3.x;
				}
				if (vector3.y < vector.y)
				{
					vector.y = vector3.y;
				}
				if (vector3.z < vector.z)
				{
					vector.z = vector3.z;
				}
				if (vector3.x > vector2.x)
				{
					vector2.x = vector3.x;
				}
				if (vector3.y > vector2.y)
				{
					vector2.y = vector3.y;
				}
				if (vector3.z > vector2.z)
				{
					vector2.z = vector3.z;
				}
			}
			this.Epsilon = Vector3.Distance(vector, vector2) * 1E-06f;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public void GenerateHull(List<Vector3> points)
		{
			this.CalculateEpsilon(points);
			this.VerticesList = points;
			if (this.FindInitialHull(points))
			{
				while (this.HaveNonEmptyFaceSet())
				{
					this.UnAssignedVertices = new HashSet<int>();
					this.CurrentHorizon = new List<EasyColliderQuickHull.Horizon>();
					int nonEmptyFaceIndex = this.GetNonEmptyFaceIndex();
					int furthestPointFromFace = this.GetFurthestPointFromFace(nonEmptyFaceIndex);
					this.CalculateHorizon(furthestPointFromFace, null, nonEmptyFaceIndex, true);
					this.AssignedVertices.ExceptWith(this.UnAssignedVertices);
					int count = this.Faces.Count;
					int f = this.Faces.Count + (from item in this.CurrentHorizon
					where item.OnConvexHull
					select item).ToList<EasyColliderQuickHull.Horizon>().Count - 1;
					int count2 = (from item in this.CurrentHorizon
					where item.OnConvexHull
					select item).ToList<EasyColliderQuickHull.Horizon>().Count;
					this.NewFaces = new List<int>();
					int num = 0;
					for (int i = 0; i < this.CurrentHorizon.Count; i++)
					{
						EasyColliderQuickHull.Horizon horizon = this.CurrentHorizon[i];
						if (horizon.OnConvexHull)
						{
							if (num == 0)
							{
								this.Faces.Add(new EasyColliderQuickHull.Face(horizon.V0, horizon.V1, furthestPointFromFace, this.CalcNormal(horizon.V0, horizon.V1, furthestPointFromFace), horizon.Face, this.Faces.Count + 1, f));
							}
							else if (num == count2 - 1)
							{
								this.Faces.Add(new EasyColliderQuickHull.Face(horizon.V0, horizon.V1, furthestPointFromFace, this.CalcNormal(horizon.V0, horizon.V1, furthestPointFromFace), horizon.Face, count, this.Faces.Count - 1));
							}
							else
							{
								this.Faces.Add(new EasyColliderQuickHull.Face(horizon.V0, horizon.V1, furthestPointFromFace, this.CalcNormal(horizon.V0, horizon.V1, furthestPointFromFace), horizon.Face, this.Faces.Count + 1, this.Faces.Count - 1));
							}
							this.NewFaces.Add(this.Faces.Count - 1);
							this.UpdateFace(horizon, this.Faces.Count - 1);
							num++;
						}
					}
					this.CloseUnAssignedVertsOnFaces();
					for (int j = 0; j < this.NewFaces.Count; j++)
					{
						this.AddToOutsideSet(this.Faces[this.NewFaces[j]], this.UnAssignedVertices);
					}
					this.UnAssignedVertices.ExceptWith(this.AssignedVertices);
					this.ClosedVertices.UnionWith(this.UnAssignedVertices);
				}
				this.Result = this.CreateMesh(this.Faces);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00023ED0 File Offset: 0x000220D0
		private int GetFurthestPointFromFace(int faceIndex)
		{
			EasyColliderQuickHull.Face face = this.Faces[faceIndex];
			float num = float.NegativeInfinity;
			int result = -1;
			foreach (int num2 in face.OutsideVertices)
			{
				float num3 = this.DistanceFromPlane(this.VerticesList[num2], face.Normal, this.VerticesList[face.V0]);
				if (num3 > num)
				{
					result = num2;
					num = num3;
				}
			}
			return result;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00023F6C File Offset: 0x0002216C
		private int GetNonEmptyFaceIndex()
		{
			for (int i = 0; i < this.Faces.Count; i++)
			{
				if (this.Faces[i].OutsideVertices.Count > 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00023FAC File Offset: 0x000221AC
		private bool HaveNonEmptyFaceSet()
		{
			using (List<EasyColliderQuickHull.Face>.Enumerator enumerator = this.Faces.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.OutsideVertices.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002400C File Offset: 0x0002220C
		private bool isAGreaterThanB(float a, float b)
		{
			return a - b > this.Epsilon;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002401C File Offset: 0x0002221C
		private bool isALessThanB(float a, float b)
		{
			return b - a > this.Epsilon;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002402C File Offset: 0x0002222C
		private bool isApproxEqual(float a, float b)
		{
			return Mathf.Abs(a - b) < this.Epsilon;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002403E File Offset: 0x0002223E
		private bool IsApproxZero(float a)
		{
			return Mathf.Abs(a) < this.Epsilon;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00024050 File Offset: 0x00022250
		private void UpdateFace(EasyColliderQuickHull.Horizon horizon, int newFace)
		{
			if (this.Faces[horizon.Face].OnConvexHull)
			{
				if (this.Faces[horizon.Face].F0 == horizon.From)
				{
					this.Faces[horizon.Face].F0 = newFace;
					return;
				}
				if (this.Faces[horizon.Face].F1 == horizon.From)
				{
					this.Faces[horizon.Face].F1 = newFace;
					return;
				}
				if (this.Faces[horizon.Face].F2 == horizon.From)
				{
					this.Faces[horizon.Face].F2 = newFace;
				}
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002411C File Offset: 0x0002231C
		private Vector3 CalcFaceCenter(EasyColliderQuickHull.Face face)
		{
			return (this.VerticesList[face.V0] + this.VerticesList[face.V1] + this.VerticesList[face.V2]) / 3f;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00024170 File Offset: 0x00022370
		private void DebugInitialPoints(List<Vector3> points, List<int> initialPoints)
		{
			string str = "";
			string str2 = "";
			foreach (int index in initialPoints)
			{
				str = str + index.ToString() + " : ";
				str2 = str2 + points[index].ToString() + " : ";
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000241F8 File Offset: 0x000223F8
		private void DrawFace(int face, Color color, float size = 0.08f)
		{
			EasyColliderQuickHull.Face face2 = this.Faces[face];
			this.DrawPoint(this.VerticesList[face2.V0], color, size);
			this.DrawPoint(this.VerticesList[face2.V1], color, size);
			this.DrawPoint(this.VerticesList[face2.V2], color, size);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00024260 File Offset: 0x00022460
		private void DrawFaceConnections(int face)
		{
			this.DrawFaceNormal(this.Faces[this.Faces[face].F0], Color.red, 1.025f);
			this.DrawFaceNormal(this.Faces[this.Faces[face].F1], Color.green, 1.05f);
			this.DrawFaceNormal(this.Faces[this.Faces[face].F2], Color.blue, 1.075f);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000242F1 File Offset: 0x000224F1
		private void DrawFaceNormal(EasyColliderQuickHull.Face face, Color color, float distance = 1f)
		{
			Vector3 vector = this.CalcFaceCenter(face);
			Debug.DrawLine(vector, vector + face.Normal * distance, color, this.DrawTime);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00024318 File Offset: 0x00022518
		private void ForceUpdateFace(int faceIndex)
		{
			if (true)
			{
				EasyColliderQuickHull.Face face = this.Faces[faceIndex];
				for (int i = 0; i < this.Faces.Count; i++)
				{
					if (faceIndex != i && this.Faces[i].OnConvexHull)
					{
						EasyColliderQuickHull.Face face2 = this.Faces[i];
						if ((face.V0 == face2.V0 || face.V0 == face2.V1 || face.V0 == face2.V2) && (face.V1 == face2.V0 || face.V1 == face2.V1 || face.V1 == face2.V2))
						{
							face.F0 = i;
						}
						else if ((face.V2 == face2.V0 || face.V2 == face2.V1 || face.V2 == face2.V2) && (face.V1 == face2.V0 || face.V1 == face2.V1 || face.V1 == face2.V2))
						{
							face.F1 = i;
						}
						else if ((face.V0 == face2.V0 || face.V0 == face2.V1 || face.V0 == face2.V2) && (face.V2 == face2.V0 || face.V2 == face2.V1 || face.V2 == face2.V2))
						{
							face.F2 = i;
						}
					}
				}
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00024498 File Offset: 0x00022698
		private Color RandomColor()
		{
			return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000244CC File Offset: 0x000226CC
		private void DrawPoint(Vector3 point, Color color, float size = 0.05f)
		{
			Debug.DrawLine(point - Vector3.up * size, point + Vector3.up * size, color, this.DrawTime);
			Debug.DrawLine(point - Vector3.left * size, point + Vector3.left * size, color, this.DrawTime);
			Debug.DrawLine(point - Vector3.forward * size, point + Vector3.forward * size, color, this.DrawTime);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00024564 File Offset: 0x00022764
		public EasyColliderQuickHull()
		{
		}

		// Token: 0x0400046C RID: 1132
		public bool DebugHorizon;

		// Token: 0x0400046D RID: 1133
		public Color DebugHorizonColor = new Color(1f, 0.5f, 0f, 1f);

		// Token: 0x0400046E RID: 1134
		public int DebugLoopNumber;

		// Token: 0x0400046F RID: 1135
		public int DebugMaxLoopNumber;

		// Token: 0x04000470 RID: 1136
		public bool DebugNewFaces;

		// Token: 0x04000471 RID: 1137
		public bool DebugNormals;

		// Token: 0x04000472 RID: 1138
		public bool DebugOutsideSet;

		// Token: 0x04000473 RID: 1139
		public Color DebugNormalColor = new Color(0.5f, 0f, 0.5f, 1f);

		// Token: 0x04000474 RID: 1140
		public float DrawTime = 2f;

		// Token: 0x04000475 RID: 1141
		private HashSet<int> AssignedVertices = new HashSet<int>();

		// Token: 0x04000476 RID: 1142
		private HashSet<int> ClosedVertices = new HashSet<int>();

		// Token: 0x04000477 RID: 1143
		private List<EasyColliderQuickHull.Horizon> CurrentHorizon = new List<EasyColliderQuickHull.Horizon>();

		// Token: 0x04000478 RID: 1144
		private float Epsilon = 1E-06f;

		// Token: 0x04000479 RID: 1145
		private List<EasyColliderQuickHull.Face> Faces = new List<EasyColliderQuickHull.Face>();

		// Token: 0x0400047A RID: 1146
		private List<int> NewFaces = new List<int>();

		// Token: 0x0400047B RID: 1147
		public Mesh Result;

		// Token: 0x0400047C RID: 1148
		private HashSet<int> UnAssignedVertices = new HashSet<int>();

		// Token: 0x0400047D RID: 1149
		private List<Vector3> VerticesList = new List<Vector3>();

		// Token: 0x020001C1 RID: 449
		private class Face
		{
			// Token: 0x06000F8B RID: 3979 RVA: 0x0006380C File Offset: 0x00061A0C
			public Face(int v0, int v1, int v2, Vector3 normal, int f0, int f1, int f2)
			{
				this.V0 = v0;
				this.V1 = v1;
				this.V2 = v2;
				this.Normal = normal;
				this.OutsideVertices = new List<int>();
				this.F0 = f0;
				this.F1 = f1;
				this.F2 = f2;
				this.OnConvexHull = true;
			}

			// Token: 0x04000DC5 RID: 3525
			public int F0;

			// Token: 0x04000DC6 RID: 3526
			public int F1;

			// Token: 0x04000DC7 RID: 3527
			public int F2;

			// Token: 0x04000DC8 RID: 3528
			public Vector3 Normal;

			// Token: 0x04000DC9 RID: 3529
			public bool OnConvexHull;

			// Token: 0x04000DCA RID: 3530
			public List<int> OutsideVertices;

			// Token: 0x04000DCB RID: 3531
			public int V0;

			// Token: 0x04000DCC RID: 3532
			public int V1;

			// Token: 0x04000DCD RID: 3533
			public int V2;
		}

		// Token: 0x020001C2 RID: 450
		private class Horizon
		{
			// Token: 0x06000F8C RID: 3980 RVA: 0x00063866 File Offset: 0x00061A66
			public Horizon(int v0, int v1, int face, int from)
			{
				this.V0 = v0;
				this.V1 = v1;
				this.Face = face;
				this.From = from;
				this.OnConvexHull = true;
			}

			// Token: 0x04000DCE RID: 3534
			public int Face;

			// Token: 0x04000DCF RID: 3535
			public int From;

			// Token: 0x04000DD0 RID: 3536
			public bool OnConvexHull;

			// Token: 0x04000DD1 RID: 3537
			public int V0;

			// Token: 0x04000DD2 RID: 3538
			public int V1;
		}

		// Token: 0x020001C3 RID: 451
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000F8D RID: 3981 RVA: 0x00063892 File Offset: 0x00061A92
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000F8E RID: 3982 RVA: 0x0006389E File Offset: 0x00061A9E
			public <>c()
			{
			}

			// Token: 0x06000F8F RID: 3983 RVA: 0x000638A6 File Offset: 0x00061AA6
			internal bool <CreateMesh>b__33_0(EasyColliderQuickHull.Face face)
			{
				return face.OnConvexHull;
			}

			// Token: 0x06000F90 RID: 3984 RVA: 0x000638AE File Offset: 0x00061AAE
			internal bool <GenerateHull>b__43_0(EasyColliderQuickHull.Horizon item)
			{
				return item.OnConvexHull;
			}

			// Token: 0x06000F91 RID: 3985 RVA: 0x000638B6 File Offset: 0x00061AB6
			internal bool <GenerateHull>b__43_1(EasyColliderQuickHull.Horizon item)
			{
				return item.OnConvexHull;
			}

			// Token: 0x04000DD3 RID: 3539
			public static readonly EasyColliderQuickHull.<>c <>9 = new EasyColliderQuickHull.<>c();

			// Token: 0x04000DD4 RID: 3540
			public static Func<EasyColliderQuickHull.Face, bool> <>9__33_0;

			// Token: 0x04000DD5 RID: 3541
			public static Func<EasyColliderQuickHull.Horizon, bool> <>9__43_0;

			// Token: 0x04000DD6 RID: 3542
			public static Func<EasyColliderQuickHull.Horizon, bool> <>9__43_1;
		}

		// Token: 0x020001C4 RID: 452
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0
		{
			// Token: 0x06000F92 RID: 3986 RVA: 0x000638BE File Offset: 0x00061ABE
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x06000F93 RID: 3987 RVA: 0x000638C6 File Offset: 0x00061AC6
			internal bool <FindInitialPointsFallBack>b__0(int element)
			{
				return element == this.ips[0];
			}

			// Token: 0x06000F94 RID: 3988 RVA: 0x000638D7 File Offset: 0x00061AD7
			internal bool <FindInitialPointsFallBack>b__1(int element)
			{
				return element == this.ips[1];
			}

			// Token: 0x06000F95 RID: 3989 RVA: 0x000638E8 File Offset: 0x00061AE8
			internal bool <FindInitialPointsFallBack>b__2(int element)
			{
				return element == this.ips[2];
			}

			// Token: 0x06000F96 RID: 3990 RVA: 0x000638F9 File Offset: 0x00061AF9
			internal bool <FindInitialPointsFallBack>b__3(int element)
			{
				return element == this.ips[3];
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x0006390A File Offset: 0x00061B0A
			internal bool <FindInitialPointsFallBack>b__4(int element)
			{
				return element == this.ips[4];
			}

			// Token: 0x06000F98 RID: 3992 RVA: 0x0006391B File Offset: 0x00061B1B
			internal bool <FindInitialPointsFallBack>b__5(int element)
			{
				return element == this.ips[5];
			}

			// Token: 0x04000DD7 RID: 3543
			public List<int> ips;

			// Token: 0x04000DD8 RID: 3544
			public Predicate<int> <>9__0;

			// Token: 0x04000DD9 RID: 3545
			public Predicate<int> <>9__1;

			// Token: 0x04000DDA RID: 3546
			public Predicate<int> <>9__2;

			// Token: 0x04000DDB RID: 3547
			public Predicate<int> <>9__3;

			// Token: 0x04000DDC RID: 3548
			public Predicate<int> <>9__4;

			// Token: 0x04000DDD RID: 3549
			public Predicate<int> <>9__5;
		}
	}
}
