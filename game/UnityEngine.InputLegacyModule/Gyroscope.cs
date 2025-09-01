using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Runtime/Input/GetInput.h")]
	public class Gyroscope
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000228C File Offset: 0x0000048C
		internal Gyroscope(int index)
		{
			this.m_GyroIndex = index;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022A0 File Offset: 0x000004A0
		[FreeFunction("GetGyroRotationRate")]
		private static Vector3 rotationRate_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.rotationRate_Internal_Injected(idx, out result);
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022B8 File Offset: 0x000004B8
		[FreeFunction("GetGyroRotationRateUnbiased")]
		private static Vector3 rotationRateUnbiased_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.rotationRateUnbiased_Internal_Injected(idx, out result);
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022D0 File Offset: 0x000004D0
		[FreeFunction("GetGravity")]
		private static Vector3 gravity_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.gravity_Internal_Injected(idx, out result);
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022E8 File Offset: 0x000004E8
		[FreeFunction("GetUserAcceleration")]
		private static Vector3 userAcceleration_Internal(int idx)
		{
			Vector3 result;
			Gyroscope.userAcceleration_Internal_Injected(idx, out result);
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002300 File Offset: 0x00000500
		[FreeFunction("GetAttitude")]
		private static Quaternion attitude_Internal(int idx)
		{
			Quaternion result;
			Gyroscope.attitude_Internal_Injected(idx, out result);
			return result;
		}

		// Token: 0x06000025 RID: 37
		[FreeFunction("IsGyroEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool getEnabled_Internal(int idx);

		// Token: 0x06000026 RID: 38
		[FreeFunction("SetGyroEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		// Token: 0x06000027 RID: 39
		[FreeFunction("GetGyroUpdateInterval")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float getUpdateInterval_Internal(int idx);

		// Token: 0x06000028 RID: 40
		[FreeFunction("SetGyroUpdateInterval")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002318 File Offset: 0x00000518
		public Vector3 rotationRate
		{
			get
			{
				return Gyroscope.rotationRate_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002338 File Offset: 0x00000538
		public Vector3 rotationRateUnbiased
		{
			get
			{
				return Gyroscope.rotationRateUnbiased_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002358 File Offset: 0x00000558
		public Vector3 gravity
		{
			get
			{
				return Gyroscope.gravity_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002378 File Offset: 0x00000578
		public Vector3 userAcceleration
		{
			get
			{
				return Gyroscope.userAcceleration_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002398 File Offset: 0x00000598
		public Quaternion attitude
		{
			get
			{
				return Gyroscope.attitude_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000023D5 File Offset: 0x000005D5
		public bool enabled
		{
			get
			{
				return Gyroscope.getEnabled_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setEnabled_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002405 File Offset: 0x00000605
		public float updateInterval
		{
			get
			{
				return Gyroscope.getUpdateInterval_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setUpdateInterval_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRate_Internal_Injected(int idx, out Vector3 ret);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRateUnbiased_Internal_Injected(int idx, out Vector3 ret);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void gravity_Internal_Injected(int idx, out Vector3 ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void userAcceleration_Internal_Injected(int idx, out Vector3 ret);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void attitude_Internal_Injected(int idx, out Quaternion ret);

		// Token: 0x04000029 RID: 41
		private int m_GyroIndex;
	}
}
