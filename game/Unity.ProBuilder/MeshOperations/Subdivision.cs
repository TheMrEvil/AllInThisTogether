using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.MeshOperations
{
	// Token: 0x02000087 RID: 135
	internal static class Subdivision
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00033581 File Offset: 0x00031781
		public static ActionResult Subdivide(this ProBuilderMesh pb)
		{
			if (pb.Subdivide(pb.facesInternal) == null)
			{
				return new ActionResult(ActionResult.Status.Failure, "Subdivide Failed");
			}
			return new ActionResult(ActionResult.Status.Success, "Subdivide");
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000335A8 File Offset: 0x000317A8
		public static Face[] Subdivide(this ProBuilderMesh pb, IList<Face> faces)
		{
			return pb.Connect(faces);
		}
	}
}
