using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000058 RID: 88
	public static class Smoothing
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00020588 File Offset: 0x0001E788
		public static int GetUnusedSmoothingGroup(ProBuilderMesh mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			return Smoothing.GetNextUnusedSmoothingGroup(1, new HashSet<int>(from x in mesh.facesInternal
			select x.smoothingGroup));
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000205DE File Offset: 0x0001E7DE
		private static int GetNextUnusedSmoothingGroup(int start, HashSet<int> used)
		{
			while (used.Contains(start) && start < 2147483646)
			{
				start++;
				if (start > 24 && start < 42)
				{
					start = 43;
				}
			}
			return start;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00020607 File Offset: 0x0001E807
		public static bool IsSmooth(int index)
		{
			return index > 0 && (index < 25 || index > 42);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0002061B File Offset: 0x0001E81B
		public static void ApplySmoothingGroups(ProBuilderMesh mesh, IEnumerable<Face> faces, float angleThreshold)
		{
			Smoothing.ApplySmoothingGroups(mesh, faces, angleThreshold, null);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00020628 File Offset: 0x0001E828
		internal static void ApplySmoothingGroups(ProBuilderMesh mesh, IEnumerable<Face> faces, float angleThreshold, Vector3[] normals)
		{
			if (mesh == null || faces == null)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag = false;
			foreach (Face face in faces)
			{
				if (face.smoothingGroup != 0)
				{
					flag = true;
				}
				face.smoothingGroup = 0;
			}
			if (normals == null)
			{
				if (flag)
				{
					mesh.mesh.normals = null;
				}
				normals = mesh.GetNormals();
			}
			float angleThreshold2 = Mathf.Abs(Mathf.Cos(Mathf.Clamp(angleThreshold, 0f, 89.999f) * 0.017453292f));
			HashSet<int> hashSet = new HashSet<int>(from x in mesh.facesInternal
			select x.smoothingGroup);
			int nextUnusedSmoothingGroup = Smoothing.GetNextUnusedSmoothingGroup(1, hashSet);
			HashSet<Face> hashSet2 = new HashSet<Face>();
			List<WingedEdge> wingedEdges = WingedEdge.GetWingedEdges(mesh, faces, true);
			try
			{
				foreach (WingedEdge wingedEdge in wingedEdges)
				{
					if (hashSet2.Add(wingedEdge.face))
					{
						wingedEdge.face.smoothingGroup = nextUnusedSmoothingGroup;
						if (Smoothing.FindSoftEdgesRecursive(normals, wingedEdge, angleThreshold2, hashSet2))
						{
							hashSet.Add(nextUnusedSmoothingGroup);
							nextUnusedSmoothingGroup = Smoothing.GetNextUnusedSmoothingGroup(nextUnusedSmoothingGroup, hashSet);
						}
						else
						{
							wingedEdge.face.smoothingGroup = 0;
						}
					}
				}
			}
			catch
			{
				Debug.LogWarning("Smoothing has been aborted: Too many edges in the analyzed mesh");
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000207B8 File Offset: 0x0001E9B8
		private static bool FindSoftEdgesRecursive(Vector3[] normals, WingedEdge wing, float angleThreshold, HashSet<Face> processed)
		{
			bool result = false;
			using (WingedEdgeEnumerator wingedEdgeEnumerator = new WingedEdgeEnumerator(wing))
			{
				while (wingedEdgeEnumerator.MoveNext())
				{
					WingedEdge wingedEdge = wingedEdgeEnumerator.Current;
					if (wingedEdge.opposite != null && wingedEdge.opposite.face.smoothingGroup == 0 && Smoothing.IsSoftEdge(normals, wingedEdge.edge, wingedEdge.opposite.edge, angleThreshold) && processed.Add(wingedEdge.opposite.face))
					{
						result = true;
						wingedEdge.opposite.face.smoothingGroup = wing.face.smoothingGroup;
						Smoothing.FindSoftEdgesRecursive(normals, wingedEdge.opposite, angleThreshold, processed);
					}
				}
			}
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00020874 File Offset: 0x0001EA74
		private static bool IsSoftEdge(Vector3[] normals, EdgeLookup left, EdgeLookup right, float threshold)
		{
			Vector3 lhs = normals[left.local.a];
			Vector3 lhs2 = normals[left.local.b];
			Vector3 rhs = normals[(right.common.a == left.common.a) ? right.local.a : right.local.b];
			Vector3 rhs2 = normals[(right.common.b == left.common.b) ? right.local.b : right.local.a];
			lhs.Normalize();
			lhs2.Normalize();
			rhs.Normalize();
			rhs2.Normalize();
			return Mathf.Abs(Vector3.Dot(lhs, rhs)) > threshold && Mathf.Abs(Vector3.Dot(lhs2, rhs2)) > threshold;
		}

		// Token: 0x040001F8 RID: 504
		internal const int smoothingGroupNone = 0;

		// Token: 0x040001F9 RID: 505
		internal const int smoothRangeMin = 1;

		// Token: 0x040001FA RID: 506
		internal const int smoothRangeMax = 24;

		// Token: 0x040001FB RID: 507
		internal const int hardRangeMin = 25;

		// Token: 0x040001FC RID: 508
		internal const int hardRangeMax = 42;

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600055C RID: 1372 RVA: 0x00035EA1 File Offset: 0x000340A1
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x00035EAD File Offset: 0x000340AD
			public <>c()
			{
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00035EB5 File Offset: 0x000340B5
			internal int <GetUnusedSmoothingGroup>b__5_0(Face x)
			{
				return x.smoothingGroup;
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x00035EBD File Offset: 0x000340BD
			internal int <ApplySmoothingGroups>b__9_0(Face x)
			{
				return x.smoothingGroup;
			}

			// Token: 0x040002BC RID: 700
			public static readonly Smoothing.<>c <>9 = new Smoothing.<>c();

			// Token: 0x040002BD RID: 701
			public static Func<Face, int> <>9__5_0;

			// Token: 0x040002BE RID: 702
			public static Func<Face, int> <>9__9_0;
		}
	}
}
