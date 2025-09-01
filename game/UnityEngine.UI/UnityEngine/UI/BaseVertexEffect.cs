using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x0200003E RID: 62
	[Obsolete("Use BaseMeshEffect instead", true)]
	public abstract class BaseVertexEffect
	{
		// Token: 0x060004A1 RID: 1185
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use BaseMeshEffect.ModifyMeshes instead", true)]
		public abstract void ModifyVertices(List<UIVertex> vertices);

		// Token: 0x060004A2 RID: 1186 RVA: 0x000166B1 File Offset: 0x000148B1
		protected BaseVertexEffect()
		{
		}
	}
}
