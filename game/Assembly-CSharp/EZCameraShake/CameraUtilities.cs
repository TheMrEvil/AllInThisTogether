using System;
using UnityEngine;

namespace EZCameraShake
{
	// Token: 0x020003BC RID: 956
	public class CameraUtilities
	{
		// Token: 0x06001F91 RID: 8081 RVA: 0x000BC350 File Offset: 0x000BA550
		public static Vector3 SmoothDampEuler(Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime)
		{
			Vector3 result;
			result.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, smoothTime);
			result.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, smoothTime);
			result.z = Mathf.SmoothDampAngle(current.z, target.z, ref velocity.z, smoothTime);
			return result;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000BC3BB File Offset: 0x000BA5BB
		public static Vector3 MultiplyVectors(Vector3 v, Vector3 w)
		{
			v.x *= w.x;
			v.y *= w.y;
			v.z *= w.z;
			return v;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000BC3F1 File Offset: 0x000BA5F1
		public CameraUtilities()
		{
		}
	}
}
