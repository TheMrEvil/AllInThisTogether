using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000027 RID: 39
	public static class Math
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00011918 File Offset: 0x0000FB18
		internal static Vector2 PointInCircumference(float radius, float angleInDegrees, Vector2 origin)
		{
			float x = radius * Mathf.Cos(0.017453292f * angleInDegrees) + origin.x;
			float y = radius * Mathf.Sin(0.017453292f * angleInDegrees) + origin.y;
			return new Vector2(x, y);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00011958 File Offset: 0x0000FB58
		internal static Vector2 PointInEllipseCircumference(float xRadius, float yRadius, float angleInDegrees, Vector2 origin, out Vector2 tangent)
		{
			float num = Mathf.Cos(0.017453292f * angleInDegrees);
			float num2 = Mathf.Sin(0.017453292f * angleInDegrees);
			float num3 = xRadius * num + origin.x;
			float num4 = yRadius * num2 + origin.y;
			tangent = new Vector2(-num4 / (yRadius * yRadius), num3 / (xRadius * xRadius));
			tangent.Normalize();
			return new Vector2(num3, num4);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000119BC File Offset: 0x0000FBBC
		internal static Vector2 PointInEllipseCircumferenceWithConstantAngle(float xRadius, float yRadius, float angleInDegrees, Vector2 origin, out Vector2 tangent)
		{
			float num = Mathf.Cos(0.017453292f * angleInDegrees);
			float num2 = Mathf.Sin(0.017453292f * angleInDegrees);
			float num3 = Mathf.Tan(0.017453292f * angleInDegrees);
			float num4 = num3 * num3;
			float num5 = xRadius * yRadius / Mathf.Sqrt(yRadius * yRadius + xRadius * xRadius * num4);
			if (num < 0f)
			{
				num5 = -num5;
			}
			float num6 = xRadius * yRadius / Mathf.Sqrt(xRadius * xRadius + yRadius * yRadius / num4);
			if (num2 < 0f)
			{
				num6 = -num6;
			}
			tangent = new Vector2(-num6 / (yRadius * yRadius), num5 / (xRadius * xRadius));
			tangent.Normalize();
			return new Vector2(num5, num6);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00011A54 File Offset: 0x0000FC54
		internal static Vector3 PointInSphere(float radius, float latitudeAngle, float longitudeAngle)
		{
			float x = radius * Mathf.Cos(0.017453292f * latitudeAngle) * Mathf.Sin(0.017453292f * longitudeAngle);
			float y = radius * Mathf.Sin(0.017453292f * latitudeAngle) * Mathf.Sin(0.017453292f * longitudeAngle);
			float z = radius * Mathf.Cos(0.017453292f * longitudeAngle);
			return new Vector3(x, y, z);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		internal static float SignedAngle(Vector2 a, Vector2 b)
		{
			float num = Vector2.Angle(a, b);
			if (b.x - a.x < 0f)
			{
				num = 360f - num;
			}
			return num;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public static float SqrDistance(Vector3 a, Vector3 b)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			float num3 = b.z - a.z;
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00011B24 File Offset: 0x0000FD24
		public static float TriangleArea(Vector3 x, Vector3 y, Vector3 z)
		{
			float num = Math.SqrDistance(x, y);
			float num2 = Math.SqrDistance(y, z);
			float num3 = Math.SqrDistance(z, x);
			return Mathf.Sqrt((2f * num * num2 + 2f * num2 * num3 + 2f * num3 * num - num * num - num2 * num2 - num3 * num3) / 16f);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00011B80 File Offset: 0x0000FD80
		internal static float PolygonArea(Vector3[] vertices, int[] indexes)
		{
			float num = 0f;
			for (int i = 0; i < indexes.Length; i += 3)
			{
				num += Math.TriangleArea(vertices[indexes[i]], vertices[indexes[i + 1]], vertices[indexes[i + 2]]);
			}
			return num;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00011BCC File Offset: 0x0000FDCC
		internal static Vector2 RotateAroundPoint(this Vector2 v, Vector2 origin, float theta)
		{
			float x = origin.x;
			float y = origin.y;
			float x2 = v.x;
			float num = v.y;
			float num2 = Mathf.Sin(theta * 0.017453292f);
			float num3 = Mathf.Cos(theta * 0.017453292f);
			float num4 = x2 - x;
			num -= y;
			float num5 = num4 * num3 + num * num2;
			float num6 = -num4 * num2 + num * num3;
			float x3 = num5 + x;
			num = num6 + y;
			return new Vector2(x3, num);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00011C38 File Offset: 0x0000FE38
		public static Vector2 ScaleAroundPoint(this Vector2 v, Vector2 origin, Vector2 scale)
		{
			return Vector2.Scale(v - origin, scale) + origin;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00011C4D File Offset: 0x0000FE4D
		internal static Vector2 Perpendicular(Vector2 value)
		{
			return new Vector2(-value.y, value.x);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00011C64 File Offset: 0x0000FE64
		public static Vector2 ReflectPoint(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
		{
			Vector2 vector = lineEnd - lineStart;
			Vector2 vector2 = new Vector2(-vector.y, vector.x);
			float num = Mathf.Sin(Vector2.Angle(vector, point - lineStart) * 0.017453292f) * Vector2.Distance(point, lineStart);
			return point + num * 2f * ((Vector2.Dot(point - lineStart, vector2) > 0f) ? -1f : 1f) * vector2;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		internal static float SqrDistanceRayPoint(Ray ray, Vector3 point)
		{
			return Vector3.Cross(ray.direction, point - ray.origin).sqrMagnitude;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00011D14 File Offset: 0x0000FF14
		public static float DistancePointLineSegment(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
		{
			float num = (lineStart.x - lineEnd.x) * (lineStart.x - lineEnd.x) + (lineStart.y - lineEnd.y) * (lineStart.y - lineEnd.y);
			if (num == 0f)
			{
				return Vector2.Distance(point, lineStart);
			}
			float num2 = Vector2.Dot(point - lineStart, lineEnd - lineStart) / num;
			if ((double)num2 < 0.0)
			{
				return Vector2.Distance(point, lineStart);
			}
			if ((double)num2 > 1.0)
			{
				return Vector2.Distance(point, lineEnd);
			}
			Vector2 b = lineStart + num2 * (lineEnd - lineStart);
			return Vector2.Distance(point, b);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public static float DistancePointLineSegment(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
		{
			float num = (lineStart.x - lineEnd.x) * (lineStart.x - lineEnd.x) + (lineStart.y - lineEnd.y) * (lineStart.y - lineEnd.y) + (lineStart.z - lineEnd.z) * (lineStart.z - lineEnd.z);
			if (num == 0f)
			{
				return Vector3.Distance(point, lineStart);
			}
			float num2 = Vector3.Dot(point - lineStart, lineEnd - lineStart) / num;
			if ((double)num2 < 0.0)
			{
				return Vector3.Distance(point, lineStart);
			}
			if ((double)num2 > 1.0)
			{
				return Vector3.Distance(point, lineEnd);
			}
			Vector3 b = lineStart + num2 * (lineEnd - lineStart);
			return Vector3.Distance(point, b);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00011E90 File Offset: 0x00010090
		public static Vector3 GetNearestPointRayRay(Ray a, Ray b)
		{
			return Math.GetNearestPointRayRay(a.origin, a.direction, b.origin, b.direction);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00011EB4 File Offset: 0x000100B4
		internal static Vector3 GetNearestPointRayRay(Vector3 ao, Vector3 ad, Vector3 bo, Vector3 bd)
		{
			float num = Vector3.Dot(ad, bd);
			float num2 = Mathf.Abs(num);
			if (num2 - 1f > Mathf.Epsilon || num2 < Mathf.Epsilon)
			{
				return ao;
			}
			Vector3 rhs = bo - ao;
			float num3 = -num * Vector3.Dot(bd, rhs) + Vector3.Dot(ad, rhs) * Vector3.Dot(bd, bd);
			float num4 = Vector3.Dot(ad, ad) * Vector3.Dot(bd, bd) - num * num;
			return ao + ad * (num3 / num4);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00011F30 File Offset: 0x00010130
		internal static bool GetLineSegmentIntersect(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, ref Vector2 intersect)
		{
			intersect = Vector2.zero;
			Vector2 vector;
			vector.x = p1.x - p0.x;
			vector.y = p1.y - p0.y;
			Vector2 vector2;
			vector2.x = p3.x - p2.x;
			vector2.y = p3.y - p2.y;
			float num = (-vector.y * (p0.x - p2.x) + vector.x * (p0.y - p2.y)) / (-vector2.x * vector.y + vector.x * vector2.y);
			float num2 = (vector2.x * (p0.y - p2.y) - vector2.y * (p0.x - p2.x)) / (-vector2.x * vector.y + vector.x * vector2.y);
			if (num >= 0f && num <= 1f && num2 >= 0f && num2 <= 1f)
			{
				intersect.x = p0.x + num2 * vector.x;
				intersect.y = p0.y + num2 * vector.y;
				return true;
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00012078 File Offset: 0x00010278
		internal static bool GetLineSegmentIntersect(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			Vector2 vector;
			vector.x = p1.x - p0.x;
			vector.y = p1.y - p0.y;
			Vector2 vector2;
			vector2.x = p3.x - p2.x;
			vector2.y = p3.y - p2.y;
			float num = (-vector.y * (p0.x - p2.x) + vector.x * (p0.y - p2.y)) / (-vector2.x * vector.y + vector.x * vector2.y);
			float num2 = (vector2.x * (p0.y - p2.y) - vector2.y * (p0.x - p2.x)) / (-vector2.x * vector.y + vector.x * vector2.y);
			return num >= 0f && num <= 1f && num2 >= 0f && num2 <= 1f;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0001218C File Offset: 0x0001038C
		internal static bool PointInPolygon(Vector2[] polygon, Vector2 point, int[] indexes = null)
		{
			int num = (indexes != null) ? indexes.Length : polygon.Length;
			if (num % 2 != 0)
			{
				Debug.LogError("PointInPolygon requires polygon indexes be divisible by 2!");
				return false;
			}
			Bounds2D bounds2D = new Bounds2D(polygon, indexes);
			if (bounds2D.ContainsPoint(point))
			{
				Vector2 vector = polygon[(indexes != null) ? indexes[0] : 0];
				Vector2 a = polygon[(indexes != null) ? indexes[1] : 1];
				Vector2 a2 = vector + (a - vector) * 0.5f - bounds2D.center;
				Vector2 p = bounds2D.center + a2 * (bounds2D.size.y + bounds2D.size.x + 2f);
				int num2 = 0;
				for (int i = 0; i < num; i += 2)
				{
					int num3 = (indexes != null) ? indexes[i] : i;
					int num4 = (indexes != null) ? indexes[i + 1] : (i + 1);
					if (Math.GetLineSegmentIntersect(p, point, polygon[num3], polygon[num4]))
					{
						num2++;
					}
				}
				return num2 % 2 != 0;
			}
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0001229C File Offset: 0x0001049C
		internal static bool PointInPolygon(Vector2[] positions, Bounds2D polyBounds, Edge[] edges, Vector2 point)
		{
			int num = edges.Length * 2;
			Vector2 p = polyBounds.center + Vector2.up * (polyBounds.size.y + 2f);
			int num2 = 0;
			for (int i = 0; i < num; i += 2)
			{
				if (Math.GetLineSegmentIntersect(p, point, positions[i], positions[i + 1]))
				{
					num2++;
				}
			}
			return num2 % 2 != 0;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00012308 File Offset: 0x00010508
		internal static bool PointInPolygon(Vector3[] positions, Bounds2D polyBounds, Edge[] edges, Vector2 point)
		{
			int num = edges.Length * 2;
			Vector2 p = polyBounds.center + Vector2.up * (polyBounds.size.y + 2f);
			int num2 = 0;
			for (int i = 0; i < num; i += 2)
			{
				if (Math.GetLineSegmentIntersect(p, point, positions[i], positions[i + 1]))
				{
					num2++;
				}
			}
			return num2 % 2 != 0;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0001237D File Offset: 0x0001057D
		internal static bool RectIntersectsLineSegment(Rect rect, Vector2 a, Vector2 b)
		{
			return Clipping.RectContainsLineSegment(rect, a.x, a.y, b.x, b.y);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0001239D File Offset: 0x0001059D
		internal static bool RectIntersectsLineSegment(Rect rect, Vector3 a, Vector3 b)
		{
			return Clipping.RectContainsLineSegment(rect, a.x, a.y, b.x, b.y);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000123C0 File Offset: 0x000105C0
		public static bool RayIntersectsTriangle(Ray InRay, Vector3 InTriangleA, Vector3 InTriangleB, Vector3 InTriangleC, out float OutDistance, out Vector3 OutPoint)
		{
			OutDistance = 0f;
			OutPoint = Vector3.zero;
			Vector3 vector = InTriangleB - InTriangleA;
			Vector3 vector2 = InTriangleC - InTriangleA;
			Vector3 rhs = Vector3.Cross(InRay.direction, vector2);
			float num = Vector3.Dot(vector, rhs);
			if (num > -Mathf.Epsilon && num < Mathf.Epsilon)
			{
				return false;
			}
			float num2 = 1f / num;
			Vector3 lhs = InRay.origin - InTriangleA;
			float num3 = Vector3.Dot(lhs, rhs) * num2;
			if (num3 < 0f || num3 > 1f)
			{
				return false;
			}
			Vector3 rhs2 = Vector3.Cross(lhs, vector);
			float num4 = Vector3.Dot(InRay.direction, rhs2) * num2;
			if (num4 < 0f || num3 + num4 > 1f)
			{
				return false;
			}
			float num5 = Vector3.Dot(vector2, rhs2) * num2;
			if (num5 > Mathf.Epsilon)
			{
				OutDistance = num5;
				OutPoint.x = num3 * InTriangleB.x + num4 * InTriangleC.x + (1f - (num3 + num4)) * InTriangleA.x;
				OutPoint.y = num3 * InTriangleB.y + num4 * InTriangleC.y + (1f - (num3 + num4)) * InTriangleA.y;
				OutPoint.z = num3 * InTriangleB.z + num4 * InTriangleC.z + (1f - (num3 + num4)) * InTriangleA.z;
				return true;
			}
			return false;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00012530 File Offset: 0x00010730
		internal static bool RayIntersectsTriangle2(Vector3 origin, Vector3 dir, Vector3 vert0, Vector3 vert1, Vector3 vert2, ref float distance, ref Vector3 normal)
		{
			Math.Subtract(vert0, vert1, ref Math.tv1);
			Math.Subtract(vert0, vert2, ref Math.tv2);
			Math.Cross(dir, Math.tv2, ref Math.tv4);
			float num = Vector3.Dot(Math.tv1, Math.tv4);
			if (num < Mathf.Epsilon)
			{
				return false;
			}
			Math.Subtract(vert0, origin, ref Math.tv3);
			float num2 = Vector3.Dot(Math.tv3, Math.tv4);
			if (num2 < 0f || num2 > num)
			{
				return false;
			}
			Math.Cross(Math.tv3, Math.tv1, ref Math.tv4);
			float num3 = Vector3.Dot(dir, Math.tv4);
			if (num3 < 0f || num2 + num3 > num)
			{
				return false;
			}
			distance = Vector3.Dot(Math.tv2, Math.tv4) * (1f / num);
			Math.Cross(Math.tv1, Math.tv2, ref normal);
			return true;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00012606 File Offset: 0x00010806
		public static float Secant(float x)
		{
			return 1f / Mathf.Cos(x);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00012614 File Offset: 0x00010814
		public static Vector3 Normal(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			float num = p1.x - p0.x;
			float num2 = p1.y - p0.y;
			float num3 = p1.z - p0.z;
			float num4 = p2.x - p0.x;
			float num5 = p2.y - p0.y;
			float num6 = p2.z - p0.z;
			Vector3 result = new Vector3(num2 * num6 - num3 * num5, num3 * num4 - num * num6, num * num5 - num2 * num4);
			if (result.magnitude < Mathf.Epsilon)
			{
				return new Vector3(0f, 0f, 0f);
			}
			result.Normalize();
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000126C4 File Offset: 0x000108C4
		internal static Vector3 Normal(IList<Vertex> vertices, IList<int> indexes = null)
		{
			if (indexes == null || indexes.Count % 3 != 0)
			{
				Vector3 result = Vector3.Cross(vertices[1].position - vertices[0].position, vertices[2].position - vertices[0].position);
				result.Normalize();
				return result;
			}
			int count = indexes.Count;
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < count; i += 3)
			{
				vector += Math.Normal(vertices[indexes[i]].position, vertices[indexes[i + 1]].position, vertices[indexes[i + 2]].position);
			}
			vector /= (float)count / 3f;
			vector.Normalize();
			return vector;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000127A0 File Offset: 0x000109A0
		public static Vector3 Normal(ProBuilderMesh mesh, Face face)
		{
			if (mesh == null || face == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Vector3[] positionsInternal = mesh.positionsInternal;
			Vector3 vector = Math.Normal(positionsInternal[face.indexesInternal[0]], positionsInternal[face.indexesInternal[1]], positionsInternal[face.indexesInternal[2]]);
			if (face.indexesInternal.Length > 6)
			{
				Vector3 normal = Projection.FindBestPlane(positionsInternal, face.distinctIndexesInternal).normal;
				if (Vector3.Dot(vector, normal) < 0f)
				{
					vector.x = -normal.x;
					vector.y = -normal.y;
					vector.z = -normal.z;
				}
				else
				{
					vector.x = normal.x;
					vector.y = normal.y;
					vector.z = normal.z;
				}
			}
			return vector;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00012880 File Offset: 0x00010A80
		public static Normal NormalTangentBitangent(ProBuilderMesh mesh, Face face)
		{
			if (mesh == null || face == null || face.indexesInternal.Length < 3)
			{
				throw new ArgumentNullException("mesh", "Cannot find normal, tangent, and bitangent for null object, or faces with < 3 indexes.");
			}
			if (mesh.texturesInternal == null || mesh.texturesInternal.Length != mesh.vertexCount)
			{
				throw new ArgumentException("Mesh textures[0] channel is not present, cannot calculate tangents.");
			}
			Vector3 vector = Math.Normal(mesh, face);
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			Vector4 vector4 = new Vector4(0f, 0f, 0f, 1f);
			long num = (long)face.indexesInternal[0];
			long num2 = (long)face.indexesInternal[1];
			long num3 = (long)face.indexesInternal[2];
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector2 vector8;
			Vector2 vector9;
			Vector2 vector10;
			checked
			{
				vector5 = mesh.positionsInternal[(int)((IntPtr)num)];
				vector6 = mesh.positionsInternal[(int)((IntPtr)num2)];
				vector7 = mesh.positionsInternal[(int)((IntPtr)num3)];
				vector8 = mesh.texturesInternal[(int)((IntPtr)num)];
				vector9 = mesh.texturesInternal[(int)((IntPtr)num2)];
				vector10 = mesh.texturesInternal[(int)((IntPtr)num3)];
			}
			float num4 = vector6.x - vector5.x;
			float num5 = vector7.x - vector5.x;
			float num6 = vector6.y - vector5.y;
			float num7 = vector7.y - vector5.y;
			float num8 = vector6.z - vector5.z;
			float num9 = vector7.z - vector5.z;
			float num10 = vector9.x - vector8.x;
			float num11 = vector10.x - vector8.x;
			float num12 = vector9.y - vector8.y;
			float num13 = vector10.y - vector8.y;
			float num14 = 1f / (num10 * num13 - num11 * num12);
			Vector3 b = new Vector3((num13 * num4 - num12 * num5) * num14, (num13 * num6 - num12 * num7) * num14, (num13 * num8 - num12 * num9) * num14);
			Vector3 b2 = new Vector3((num10 * num5 - num11 * num4) * num14, (num10 * num7 - num11 * num6) * num14, (num10 * num9 - num11 * num8) * num14);
			vector2 += b;
			vector3 += b2;
			Vector3 lhs = vector;
			Vector3.OrthoNormalize(ref lhs, ref vector2);
			vector4.x = vector2.x;
			vector4.y = vector2.y;
			vector4.z = vector2.z;
			vector4.w = ((Vector3.Dot(Vector3.Cross(lhs, vector2), vector3) < 0f) ? -1f : 1f);
			return new Normal
			{
				normal = vector,
				tangent = vector4,
				bitangent = Vector3.Cross(vector, vector4 * vector4.w)
			};
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00012B48 File Offset: 0x00010D48
		internal static bool IsCardinalAxis(Vector3 v, float epsilon = 1E-45f)
		{
			if (v == Vector3.zero)
			{
				return false;
			}
			v.Normalize();
			return 1f - Mathf.Abs(Vector3.Dot(Vector3.up, v)) < epsilon || 1f - Mathf.Abs(Vector3.Dot(Vector3.forward, v)) < epsilon || 1f - Mathf.Abs(Vector3.Dot(Vector3.right, v)) < epsilon;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00012BB8 File Offset: 0x00010DB8
		internal static Vector2 DivideBy(this Vector2 v, Vector2 o)
		{
			return new Vector2(v.x / o.x, v.y / o.y);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00012BD9 File Offset: 0x00010DD9
		internal static Vector3 DivideBy(this Vector3 v, Vector3 o)
		{
			return new Vector3(v.x / o.x, v.y / o.y, v.z / o.z);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00012C08 File Offset: 0x00010E08
		internal static T Max<T>(T[] array) where T : IComparable<T>
		{
			if (array == null || array.Length < 1)
			{
				return default(T);
			}
			T t = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i].CompareTo(t) >= 0)
				{
					t = array[i];
				}
			}
			return t;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00012C60 File Offset: 0x00010E60
		internal static T Min<T>(T[] array) where T : IComparable<T>
		{
			if (array == null || array.Length < 1)
			{
				return default(T);
			}
			T t = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i].CompareTo(t) < 0)
				{
					t = array[i];
				}
			}
			return t;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00012CB8 File Offset: 0x00010EB8
		internal static float LargestValue(Vector3 v)
		{
			if (v.x > v.y && v.x > v.z)
			{
				return v.x;
			}
			if (v.y > v.x && v.y > v.z)
			{
				return v.y;
			}
			return v.z;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00012D11 File Offset: 0x00010F11
		internal static float LargestValue(Vector2 v)
		{
			if (v.x <= v.y)
			{
				return v.y;
			}
			return v.x;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00012D30 File Offset: 0x00010F30
		internal static Vector2 SmallestVector2(Vector2[] v)
		{
			int num = v.Length;
			Vector2 vector = v[0];
			for (int i = 0; i < num; i++)
			{
				if (v[i].x < vector.x)
				{
					vector.x = v[i].x;
				}
				if (v[i].y < vector.y)
				{
					vector.y = v[i].y;
				}
			}
			return vector;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00012DA4 File Offset: 0x00010FA4
		internal static Vector2 SmallestVector2(Vector2[] v, IList<int> indexes)
		{
			int count = indexes.Count;
			Vector2 vector = v[indexes[0]];
			for (int i = 0; i < count; i++)
			{
				if (v[indexes[i]].x < vector.x)
				{
					vector.x = v[indexes[i]].x;
				}
				if (v[indexes[i]].y < vector.y)
				{
					vector.y = v[indexes[i]].y;
				}
			}
			return vector;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00012E3C File Offset: 0x0001103C
		internal static Vector2 LargestVector2(Vector2[] v)
		{
			int num = v.Length;
			Vector2 vector = v[0];
			for (int i = 0; i < num; i++)
			{
				if (v[i].x > vector.x)
				{
					vector.x = v[i].x;
				}
				if (v[i].y > vector.y)
				{
					vector.y = v[i].y;
				}
			}
			return vector;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00012EB0 File Offset: 0x000110B0
		internal static Vector2 LargestVector2(Vector2[] v, IList<int> indexes)
		{
			int count = indexes.Count;
			Vector2 vector = v[indexes[0]];
			for (int i = 0; i < count; i++)
			{
				if (v[indexes[i]].x > vector.x)
				{
					vector.x = v[indexes[i]].x;
				}
				if (v[indexes[i]].y > vector.y)
				{
					vector.y = v[indexes[i]].y;
				}
			}
			return vector;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00012F48 File Offset: 0x00011148
		internal static Bounds GetBounds(Vector3[] positions, IList<int> indices = null)
		{
			bool flag = indices != null;
			if ((flag && indices.Count < 1) || positions.Length < 1)
			{
				return default(Bounds);
			}
			Vector3 vector = positions[flag ? indices[0] : 0];
			Vector3 vector2 = vector;
			if (flag)
			{
				int i = 1;
				int count = indices.Count;
				while (i < count)
				{
					vector.x = Mathf.Min(positions[indices[i]].x, vector.x);
					vector2.x = Mathf.Max(positions[indices[i]].x, vector2.x);
					vector.y = Mathf.Min(positions[indices[i]].y, vector.y);
					vector2.y = Mathf.Max(positions[indices[i]].y, vector2.y);
					vector.z = Mathf.Min(positions[indices[i]].z, vector.z);
					vector2.z = Mathf.Max(positions[indices[i]].z, vector2.z);
					i++;
				}
			}
			else
			{
				int j = 1;
				int num = positions.Length;
				while (j < num)
				{
					vector.x = Mathf.Min(positions[j].x, vector.x);
					vector2.x = Mathf.Max(positions[j].x, vector2.x);
					vector.y = Mathf.Min(positions[j].y, vector.y);
					vector2.y = Mathf.Max(positions[j].y, vector2.y);
					vector.z = Mathf.Min(positions[j].z, vector.z);
					vector2.z = Mathf.Max(positions[j].z, vector2.z);
					j++;
				}
			}
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00013188 File Offset: 0x00011388
		public static Vector2 Average(IList<Vector2> array, IList<int> indexes = null)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Vector2 a = Vector2.zero;
			float num = (float)((indexes == null) ? array.Count : indexes.Count);
			if (indexes == null)
			{
				int num2 = 0;
				while ((float)num2 < num)
				{
					a += array[num2];
					num2++;
				}
			}
			else
			{
				int num3 = 0;
				while ((float)num3 < num)
				{
					a += array[indexes[num3]];
					num3++;
				}
			}
			return a / num;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00013204 File Offset: 0x00011404
		public static Vector3 Average(IList<Vector3> array, IList<int> indexes = null)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Vector3 zero = Vector3.zero;
			float num = (float)((indexes == null) ? array.Count : indexes.Count);
			if (indexes == null)
			{
				int num2 = 0;
				while ((float)num2 < num)
				{
					zero.x += array[num2].x;
					zero.y += array[num2].y;
					zero.z += array[num2].z;
					num2++;
				}
			}
			else
			{
				int num3 = 0;
				while ((float)num3 < num)
				{
					zero.x += array[indexes[num3]].x;
					zero.y += array[indexes[num3]].y;
					zero.z += array[indexes[num3]].z;
					num3++;
				}
			}
			return zero / num;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000132FC File Offset: 0x000114FC
		public static Vector4 Average(IList<Vector4> array, IList<int> indexes = null)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Vector4 a = Vector3.zero;
			float num = (float)((indexes == null) ? array.Count : indexes.Count);
			if (indexes == null)
			{
				int num2 = 0;
				while ((float)num2 < num)
				{
					a.x += array[num2].x;
					a.y += array[num2].y;
					a.z += array[num2].z;
					num2++;
				}
			}
			else
			{
				int num3 = 0;
				while ((float)num3 < num)
				{
					a.x += array[indexes[num3]].x;
					a.y += array[indexes[num3]].y;
					a.z += array[indexes[num3]].z;
					num3++;
				}
			}
			return a / num;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000133F8 File Offset: 0x000115F8
		internal static Vector3 InvertScaleVector(Vector3 scaleVector)
		{
			for (int i = 0; i < 3; i++)
			{
				scaleVector[i] = ((scaleVector[i] == 0f) ? 0f : (1f / scaleVector[i]));
			}
			return scaleVector;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0001343E File Offset: 0x0001163E
		internal static bool Approx2(this Vector2 a, Vector2 b, float delta = 0.0001f)
		{
			return Mathf.Abs(a.x - b.x) < delta && Mathf.Abs(a.y - b.y) < delta;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0001346C File Offset: 0x0001166C
		internal static bool Approx3(this Vector3 a, Vector3 b, float delta = 0.0001f)
		{
			return Mathf.Abs(a.x - b.x) < delta && Mathf.Abs(a.y - b.y) < delta && Mathf.Abs(a.z - b.z) < delta;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000134BC File Offset: 0x000116BC
		internal static bool Approx4(this Vector4 a, Vector4 b, float delta = 0.0001f)
		{
			return Mathf.Abs(a.x - b.x) < delta && Mathf.Abs(a.y - b.y) < delta && Mathf.Abs(a.z - b.z) < delta && Mathf.Abs(a.w - b.w) < delta;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00013520 File Offset: 0x00011720
		internal static bool ApproxC(this Color a, Color b, float delta = 0.0001f)
		{
			return Mathf.Abs(a.r - b.r) < delta && Mathf.Abs(a.g - b.g) < delta && Mathf.Abs(a.b - b.b) < delta && Mathf.Abs(a.a - b.a) < delta;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00013583 File Offset: 0x00011783
		internal static bool Approx(this float a, float b, float delta = 0.0001f)
		{
			return Mathf.Abs(b - a) < Mathf.Abs(delta);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00013595 File Offset: 0x00011795
		public static int Clamp(int value, int lowerBound, int upperBound)
		{
			if (value < lowerBound)
			{
				return lowerBound;
			}
			if (value <= upperBound)
			{
				return value;
			}
			return upperBound;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000135A4 File Offset: 0x000117A4
		internal static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000135CC File Offset: 0x000117CC
		internal static Vector3 Sign(this Vector3 v)
		{
			return new Vector3(Mathf.Sign(v.x), Mathf.Sign(v.y), Mathf.Sign(v.z));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000135F4 File Offset: 0x000117F4
		internal static float Sum(this Vector3 v)
		{
			return Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0001361C File Offset: 0x0001181C
		internal static void Cross(Vector3 a, Vector3 b, ref Vector3 res)
		{
			res.x = a.y * b.z - a.z * b.y;
			res.y = a.z * b.x - a.x * b.z;
			res.z = a.x * b.y - a.y * b.x;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0001368C File Offset: 0x0001188C
		internal static void Subtract(Vector3 a, Vector3 b, ref Vector3 res)
		{
			res.x = b.x - a.x;
			res.y = b.y - a.y;
			res.z = b.z - a.z;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000136C7 File Offset: 0x000118C7
		internal static bool IsNumber(float value)
		{
			return !float.IsInfinity(value) && !float.IsNaN(value);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000136DC File Offset: 0x000118DC
		internal static bool IsNumber(Vector2 value)
		{
			return Math.IsNumber(value.x) && Math.IsNumber(value.y);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000136F8 File Offset: 0x000118F8
		internal static bool IsNumber(Vector3 value)
		{
			return Math.IsNumber(value.x) && Math.IsNumber(value.y) && Math.IsNumber(value.z);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00013721 File Offset: 0x00011921
		internal static bool IsNumber(Vector4 value)
		{
			return Math.IsNumber(value.x) && Math.IsNumber(value.y) && Math.IsNumber(value.z) && Math.IsNumber(value.w);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00013757 File Offset: 0x00011957
		internal static float MakeNonZero(float value, float min = 0.0001f)
		{
			if (float.IsNaN(value) || float.IsInfinity(value) || Mathf.Abs(value) < min)
			{
				return min * Mathf.Sign(value);
			}
			return value;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0001377C File Offset: 0x0001197C
		internal static Vector4 FixNaN(Vector4 value)
		{
			value.x = (Math.IsNumber(value.x) ? value.x : 0f);
			value.y = (Math.IsNumber(value.y) ? value.y : 0f);
			value.z = (Math.IsNumber(value.z) ? value.z : 0f);
			value.w = (Math.IsNumber(value.w) ? value.w : 0f);
			return value;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0001380E File Offset: 0x00011A0E
		internal static Vector2 EnsureUnitVector(Vector2 value)
		{
			if (Mathf.Abs(value.sqrMagnitude) >= 1E-45f)
			{
				return value.normalized;
			}
			return Vector2.right;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00013830 File Offset: 0x00011A30
		internal static Vector3 EnsureUnitVector(Vector3 value)
		{
			if (Mathf.Abs(value.sqrMagnitude) >= 1E-45f)
			{
				return value.normalized;
			}
			return Vector3.up;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00013854 File Offset: 0x00011A54
		internal static Vector4 EnsureUnitVector(Vector4 value)
		{
			Vector3 vector = Math.EnsureUnitVector(value);
			return new Vector4(vector.x, vector.y, vector.z, Math.MakeNonZero(value.w, 1f));
		}

		// Token: 0x0400007C RID: 124
		public const float phi = 1.618034f;

		// Token: 0x0400007D RID: 125
		private const float k_FltEpsilon = 1E-45f;

		// Token: 0x0400007E RID: 126
		private const float k_FltCompareEpsilon = 0.0001f;

		// Token: 0x0400007F RID: 127
		internal const float handleEpsilon = 0.0001f;

		// Token: 0x04000080 RID: 128
		private static Vector3 tv1;

		// Token: 0x04000081 RID: 129
		private static Vector3 tv2;

		// Token: 0x04000082 RID: 130
		private static Vector3 tv3;

		// Token: 0x04000083 RID: 131
		private static Vector3 tv4;
	}
}
