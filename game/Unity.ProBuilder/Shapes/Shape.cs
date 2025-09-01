using System;

namespace UnityEngine.ProBuilder.Shapes
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public abstract class Shape
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000272B4 File Offset: 0x000254B4
		public virtual Bounds UpdateBounds(ProBuilderMesh mesh, Vector3 size, Quaternion rotation, Bounds bounds)
		{
			return mesh.mesh.bounds;
		}

		// Token: 0x06000463 RID: 1123
		public abstract Bounds RebuildMesh(ProBuilderMesh mesh, Vector3 size, Quaternion rotation);

		// Token: 0x06000464 RID: 1124
		public abstract void CopyShape(Shape shape);

		// Token: 0x06000465 RID: 1125 RVA: 0x000272C1 File Offset: 0x000254C1
		protected Shape()
		{
		}
	}
}
