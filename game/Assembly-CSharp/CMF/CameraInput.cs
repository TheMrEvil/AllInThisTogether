using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B0 RID: 944
	public abstract class CameraInput : MonoBehaviour
	{
		// Token: 0x06001F46 RID: 8006
		public abstract float GetHorizontalCameraInput();

		// Token: 0x06001F47 RID: 8007
		public abstract float GetVerticalCameraInput();

		// Token: 0x06001F48 RID: 8008 RVA: 0x000BB2F4 File Offset: 0x000B94F4
		protected CameraInput()
		{
		}
	}
}
