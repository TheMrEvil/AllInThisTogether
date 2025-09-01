using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeConditional("ENABLE_CLUSTERINPUT")]
	[NativeHeader("Modules/ClusterInput/ClusterInput.h")]
	public class ClusterInput
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string name);

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string name);

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		[NativeConditional("ENABLE_CLUSTERINPUT", "Vector3f(0.0f, 0.0f, 0.0f)")]
		public static Vector3 GetTrackerPosition(string name)
		{
			Vector3 result;
			ClusterInput.GetTrackerPosition_Injected(name, out result);
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		[NativeConditional("ENABLE_CLUSTERINPUT", "Quartenion::identity")]
		public static Quaternion GetTrackerRotation(string name)
		{
			Quaternion result;
			ClusterInput.GetTrackerRotation_Injected(name, out result);
			return result;
		}

		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAxis(string name, float value);

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetButton(string name, bool value);

		// Token: 0x06000007 RID: 7 RVA: 0x0000207E File Offset: 0x0000027E
		public static void SetTrackerPosition(string name, Vector3 value)
		{
			ClusterInput.SetTrackerPosition_Injected(name, ref value);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002088 File Offset: 0x00000288
		public static void SetTrackerRotation(string name, Quaternion value)
		{
			ClusterInput.SetTrackerRotation_Injected(name, ref value);
		}

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool AddInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool EditInput(string name, string deviceName, string serverUrl, int index, ClusterInputType type);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CheckConnectionToServer(string name);

		// Token: 0x0600000C RID: 12 RVA: 0x00002092 File Offset: 0x00000292
		public ClusterInput()
		{
		}

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerPosition_Injected(string name, out Vector3 ret);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrackerRotation_Injected(string name, out Quaternion ret);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerPosition_Injected(string name, ref Vector3 value);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrackerRotation_Injected(string name, ref Quaternion value);
	}
}
