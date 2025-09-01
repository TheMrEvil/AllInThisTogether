using System;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000084 RID: 132
	public static class MeshTransform
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00032488 File Offset: 0x00030688
		internal static void SetPivot(this ProBuilderMesh mesh, PivotLocation pivotLocation)
		{
			Bounds bounds = mesh.GetBounds();
			Vector3 position = (pivotLocation == PivotLocation.Center) ? bounds.center : (bounds.center - bounds.extents);
			mesh.SetPivot(mesh.transform.TransformPoint(position));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000324D0 File Offset: 0x000306D0
		public static void CenterPivot(this ProBuilderMesh mesh, int[] indexes)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Vector3 vector = Vector3.zero;
			if (indexes != null && indexes.Length != 0)
			{
				Vector3[] positionsInternal = mesh.positionsInternal;
				if (positionsInternal == null || positionsInternal.Length < 3)
				{
					return;
				}
				foreach (int num in indexes)
				{
					vector += positionsInternal[num];
				}
				vector = mesh.transform.TransformPoint(vector / (float)indexes.Length);
			}
			else
			{
				vector = mesh.transform.TransformPoint(mesh.mesh.bounds.center);
			}
			Vector3 offset = mesh.transform.position - vector;
			mesh.transform.position = vector;
			mesh.ToMesh(MeshTopology.Triangles);
			mesh.TranslateVerticesInWorldSpace(mesh.mesh.triangles, offset);
			mesh.Refresh(RefreshMask.All);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000325B0 File Offset: 0x000307B0
		public static void SetPivot(this ProBuilderMesh mesh, Vector3 worldPosition)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Transform transform = mesh.transform;
			Vector3 offset = transform.position - worldPosition;
			transform.position = worldPosition;
			mesh.ToMesh(MeshTopology.Triangles);
			mesh.TranslateVerticesInWorldSpace(mesh.mesh.triangles, offset);
			mesh.Refresh(RefreshMask.All);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0003260C File Offset: 0x0003080C
		public static void FreezeScaleTransform(this ProBuilderMesh mesh)
		{
			if (mesh == null)
			{
				throw new ArgumentNullException("mesh");
			}
			Vector3[] positionsInternal = mesh.positionsInternal;
			for (int i = 0; i < positionsInternal.Length; i++)
			{
				positionsInternal[i] = Vector3.Scale(positionsInternal[i], mesh.transform.localScale);
			}
			mesh.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
}
