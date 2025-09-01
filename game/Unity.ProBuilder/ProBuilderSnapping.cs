using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000044 RID: 68
	internal static class ProBuilderSnapping
	{
		// Token: 0x060002AD RID: 685 RVA: 0x000192C0 File Offset: 0x000174C0
		internal static bool IsCardinalDirection(Vector3 direction)
		{
			return (Mathf.Abs(direction.x) > 0f && Mathf.Approximately(direction.y, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.y) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.z) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.y, 0f));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00019371 File Offset: 0x00017571
		public static float Snap(float val, float snap)
		{
			if (snap == 0f)
			{
				return val;
			}
			return snap * Mathf.Round(val / snap);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00019388 File Offset: 0x00017588
		public static Vector3 Snap(Vector3 val, Vector3 snap)
		{
			return new Vector3((Mathf.Abs(snap.x) > 0.0001f) ? ProBuilderSnapping.Snap(val.x, snap.x) : val.x, (Mathf.Abs(snap.y) > 0.0001f) ? ProBuilderSnapping.Snap(val.y, snap.y) : val.y, (Mathf.Abs(snap.z) > 0.0001f) ? ProBuilderSnapping.Snap(val.z, snap.z) : val.z);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0001941C File Offset: 0x0001761C
		public static void SnapVertices(ProBuilderMesh mesh, IEnumerable<int> indexes, Vector3 snap)
		{
			Vector3[] positionsInternal = mesh.positionsInternal;
			foreach (int num in indexes)
			{
				positionsInternal[num] = mesh.transform.InverseTransformPoint(ProBuilderSnapping.Snap(mesh.transform.TransformPoint(positionsInternal[num]), snap));
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00019490 File Offset: 0x00017690
		internal static Vector3 GetSnappingMaskBasedOnNormalVector(Vector3 normal)
		{
			return new Vector3(Mathf.Approximately(Mathf.Abs(normal.x), 1f) ? 0f : 1f, Mathf.Approximately(Mathf.Abs(normal.y), 1f) ? 0f : 1f, Mathf.Approximately(Mathf.Abs(normal.z), 1f) ? 0f : 1f);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001950C File Offset: 0x0001770C
		internal static Vector3 SnapValueOnRay(Ray ray, float distance, float snap, Vector3Mask mask)
		{
			float num = float.PositiveInfinity;
			Ray ray2 = new Ray(ray.origin, ray.direction);
			Ray ray3 = new Ray(ray.origin, -ray.direction);
			for (int i = 0; i < 3; i++)
			{
				if (mask[i] > 0f)
				{
					Vector3Mask mask2 = new Vector3Mask(new Vector3Mask((byte)(1 << i)), float.Epsilon);
					Vector3 b = Vector3.Project(ray.direction * Math.MakeNonZero(distance, 0.0001f), mask2 * Mathf.Sign(ray.direction[i]));
					Vector3 val = ray.origin + b;
					Plane plane = new Plane(mask2, ProBuilderSnapping.Snap(val, mask2 * snap));
					if (Mathf.Abs(plane.GetDistanceToPoint(ray.origin)) < 0.0001f)
					{
						num = 0f;
					}
					else
					{
						float num2;
						if (plane.Raycast(ray2, out num2) && Mathf.Abs(num2) < Mathf.Abs(num))
						{
							num = num2;
						}
						if (plane.Raycast(ray3, out num2) && Mathf.Abs(num2) < Mathf.Abs(num))
						{
							num = -num2;
						}
					}
				}
			}
			return ray.origin + ray.direction * ((Mathf.Abs(num) >= float.PositiveInfinity) ? distance : num);
		}

		// Token: 0x040001AC RID: 428
		private const float k_MaxRaySnapDistance = float.PositiveInfinity;
	}
}
