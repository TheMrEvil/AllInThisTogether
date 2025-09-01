using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Runtime/Camera/Camera.h")]
	internal class CameraRaycastHelper
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002635 File Offset: 0x00000835
		[FreeFunction("CameraScripting::RaycastTry")]
		internal static GameObject RaycastTry(Camera cam, Ray ray, float distance, int layerMask)
		{
			return CameraRaycastHelper.RaycastTry_Injected(cam, ref ray, distance, layerMask);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002641 File Offset: 0x00000841
		[FreeFunction("CameraScripting::RaycastTry2D")]
		internal static GameObject RaycastTry2D(Camera cam, Ray ray, float distance, int layerMask)
		{
			return CameraRaycastHelper.RaycastTry2D_Injected(cam, ref ray, distance, layerMask);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000257E File Offset: 0x0000077E
		public CameraRaycastHelper()
		{
		}

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry_Injected(Camera cam, ref Ray ray, float distance, int layerMask);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry2D_Injected(Camera cam, ref Ray ray, float distance, int layerMask);
	}
}
