using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000112 RID: 274
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GeometryUtilityScripting", StaticAccessorType.DoubleColon)]
	public sealed class GeometryUtility
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00009C54 File Offset: 0x00007E54
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(camera, array);
			return array;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00009C78 File Offset: 0x00007E78
		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(worldToProjectionMatrix, array);
			return array;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00009C9A File Offset: 0x00007E9A
		public static void CalculateFrustumPlanes(Camera camera, Plane[] planes)
		{
			GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix, planes);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public static void CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix, Plane[] planes)
		{
			bool flag = planes == null;
			if (flag)
			{
				throw new ArgumentNullException("planes");
			}
			bool flag2 = planes.Length != 6;
			if (flag2)
			{
				throw new ArgumentException("Planes array must be of length 6.", "planes");
			}
			GeometryUtility.Internal_ExtractPlanes(planes, worldToProjectionMatrix);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00009D00 File Offset: 0x00007F00
		public static Bounds CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = positions.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.", "positions");
			}
			return GeometryUtility.Internal_CalculateBounds(positions, transform);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00009D48 File Offset: 0x00007F48
		public static bool TryCreatePlaneFromPolygon(Vector3[] vertices, out Plane plane)
		{
			bool flag = vertices == null || vertices.Length < 3;
			bool result;
			if (flag)
			{
				plane = new Plane(Vector3.up, 0f);
				result = false;
			}
			else
			{
				bool flag2 = vertices.Length == 3;
				if (flag2)
				{
					Vector3 a = vertices[0];
					Vector3 b = vertices[1];
					Vector3 c = vertices[2];
					plane = new Plane(a, b, c);
					result = (plane.normal.sqrMagnitude > 0f);
				}
				else
				{
					Vector3 zero = Vector3.zero;
					int num = vertices.Length - 1;
					Vector3 vector = vertices[num];
					foreach (Vector3 vector2 in vertices)
					{
						zero.x += (vector.y - vector2.y) * (vector.z + vector2.z);
						zero.y += (vector.z - vector2.z) * (vector.x + vector2.x);
						zero.z += (vector.x - vector2.x) * (vector.y + vector2.y);
						vector = vector2;
					}
					zero.Normalize();
					float num2 = 0f;
					foreach (Vector3 rhs in vertices)
					{
						num2 -= Vector3.Dot(zero, rhs);
					}
					num2 /= (float)vertices.Length;
					plane = new Plane(zero, num2);
					result = (plane.normal.sqrMagnitude > 0f);
				}
			}
			return result;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00009F0B File Offset: 0x0000810B
		public static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			return GeometryUtility.TestPlanesAABB_Injected(planes, ref bounds);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00009F15 File Offset: 0x00008115
		[NativeName("ExtractPlanes")]
		private static void Internal_ExtractPlanes([Out] Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			GeometryUtility.Internal_ExtractPlanes_Injected(planes, ref worldToProjectionMatrix);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00009F20 File Offset: 0x00008120
		[NativeName("CalculateBounds")]
		private static Bounds Internal_CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			Bounds result;
			GeometryUtility.Internal_CalculateBounds_Injected(positions, ref transform, out result);
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00002072 File Offset: 0x00000272
		public GeometryUtility()
		{
		}

		// Token: 0x060006DD RID: 1757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TestPlanesAABB_Injected(Plane[] planes, ref Bounds bounds);

		// Token: 0x060006DE RID: 1758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExtractPlanes_Injected([Out] Plane[] planes, ref Matrix4x4 worldToProjectionMatrix);

		// Token: 0x060006DF RID: 1759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalculateBounds_Injected(Vector3[] positions, ref Matrix4x4 transform, out Bounds ret);
	}
}
