using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000006 RID: 6
	internal class DuplicateNodeError : Exception
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000021C8 File Offset: 0x000003C8
		public DuplicateNodeError() : base("Cannot Add Node With Duplicate Coordinates")
		{
		}
	}
}
