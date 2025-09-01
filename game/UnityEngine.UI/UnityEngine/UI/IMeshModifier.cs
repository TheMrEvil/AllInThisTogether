using System;

namespace UnityEngine.UI
{
	// Token: 0x02000041 RID: 65
	public interface IMeshModifier
	{
		// Token: 0x060004AB RID: 1195
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(Mesh mesh);

		// Token: 0x060004AC RID: 1196
		void ModifyMesh(VertexHelper verts);
	}
}
