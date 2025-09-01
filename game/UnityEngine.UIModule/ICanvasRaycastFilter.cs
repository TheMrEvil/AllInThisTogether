using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public interface ICanvasRaycastFilter
	{
		// Token: 0x06000001 RID: 1
		bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera);
	}
}
