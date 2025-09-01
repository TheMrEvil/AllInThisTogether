using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000012 RID: 18
	[UsedByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct InputDevice : IEquatable<InputDevice>
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F7E File Offset: 0x0000117E
		internal InputDevice(ulong deviceId)
		{
			this.m_DeviceId = deviceId;
			this.m_Initialized = true;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002F90 File Offset: 0x00001190
		private ulong deviceId
		{
			get
			{
				return this.m_Initialized ? this.m_DeviceId : ulong.MaxValue;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FB4 File Offset: 0x000011B4
		public XRInputSubsystem subsystem
		{
			get
			{
				bool flag = InputDevice.s_InputSubsystemCache == null;
				if (flag)
				{
					InputDevice.s_InputSubsystemCache = new List<XRInputSubsystem>();
				}
				bool initialized = this.m_Initialized;
				if (initialized)
				{
					uint num = (uint)(this.m_DeviceId >> 32);
					SubsystemManager.GetSubsystems<XRInputSubsystem>(InputDevice.s_InputSubsystemCache);
					for (int i = 0; i < InputDevice.s_InputSubsystemCache.Count; i++)
					{
						bool flag2 = num == InputDevice.s_InputSubsystemCache[i].GetIndex();
						if (flag2)
						{
							return InputDevice.s_InputSubsystemCache[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003048 File Offset: 0x00001248
		public bool isValid
		{
			get
			{
				return this.IsValidId() && InputDevices.IsDeviceValid(this.m_DeviceId);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003070 File Offset: 0x00001270
		public string name
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceName(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003098 File Offset: 0x00001298
		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevice.characteristics instead.")]
		public InputDeviceRole role
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceRole(this.m_DeviceId) : InputDeviceRole.Unknown;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000030C0 File Offset: 0x000012C0
		public string manufacturer
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceManufacturer(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030E8 File Offset: 0x000012E8
		public string serialNumber
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceSerialNumber(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003110 File Offset: 0x00001310
		public InputDeviceCharacteristics characteristics
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceCharacteristics(this.m_DeviceId) : InputDeviceCharacteristics.None;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003138 File Offset: 0x00001338
		private bool IsValidId()
		{
			return this.deviceId != ulong.MaxValue;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003158 File Offset: 0x00001358
		public bool SendHapticImpulse(uint channel, float amplitude, float duration = 1f)
		{
			bool flag = !this.IsValidId();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = amplitude < 0f;
				if (flag2)
				{
					throw new ArgumentException("Amplitude of SendHapticImpulse cannot be negative.");
				}
				bool flag3 = duration < 0f;
				if (flag3)
				{
					throw new ArgumentException("Duration of SendHapticImpulse cannot be negative.");
				}
				result = InputDevices.SendHapticImpulse(this.m_DeviceId, channel, amplitude, duration);
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031B8 File Offset: 0x000013B8
		public bool SendHapticBuffer(uint channel, byte[] buffer)
		{
			bool flag = !this.IsValidId();
			return !flag && InputDevices.SendHapticBuffer(this.m_DeviceId, channel, buffer);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031E8 File Offset: 0x000013E8
		public bool TryGetHapticCapabilities(out HapticCapabilities capabilities)
		{
			bool flag = this.CheckValidAndSetDefault<HapticCapabilities>(out capabilities);
			return flag && InputDevices.TryGetHapticCapabilities(this.m_DeviceId, out capabilities);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003218 File Offset: 0x00001418
		public void StopHaptics()
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				InputDevices.StopHaptics(this.m_DeviceId);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000323C File Offset: 0x0000143C
		public bool TryGetFeatureUsages(List<InputFeatureUsage> featureUsages)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureUsages(this.m_DeviceId, featureUsages);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003268 File Offset: 0x00001468
		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValue_bool(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000329C File Offset: 0x0000149C
		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032D0 File Offset: 0x000014D0
		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValue_float(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003304 File Offset: 0x00001504
		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector2f(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003338 File Offset: 0x00001538
		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector3f(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000336C File Offset: 0x0000156C
		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValue_Quaternionf(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033A0 File Offset: 0x000015A0
		public bool TryGetFeatureValue(InputFeatureUsage<Hand> usage, out Hand value)
		{
			bool flag = this.CheckValidAndSetDefault<Hand>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRHand(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033D4 File Offset: 0x000015D4
		public bool TryGetFeatureValue(InputFeatureUsage<Bone> usage, out Bone value)
		{
			bool flag = this.CheckValidAndSetDefault<Bone>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRBone(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003408 File Offset: 0x00001608
		public bool TryGetFeatureValue(InputFeatureUsage<Eyes> usage, out Eyes value)
		{
			bool flag = this.CheckValidAndSetDefault<Eyes>(out value);
			return flag && InputDevices.TryGetFeatureValue_XREyes(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000343C File Offset: 0x0000163C
		public bool TryGetFeatureValue(InputFeatureUsage<byte[]> usage, byte[] value)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureValue_Custom(this.m_DeviceId, usage.name, value);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003470 File Offset: 0x00001670
		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				uint num = 0U;
				bool flag2 = InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034B4 File Offset: 0x000016B4
		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, DateTime time, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_bool(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034F0 File Offset: 0x000016F0
		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, DateTime time, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000352C File Offset: 0x0000172C
		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, DateTime time, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_float(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003568 File Offset: 0x00001768
		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, DateTime time, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector2f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035A4 File Offset: 0x000017A4
		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, DateTime time, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector3f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035E0 File Offset: 0x000017E0
		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, DateTime time, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Quaternionf(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000361C File Offset: 0x0000181C
		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, DateTime time, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				uint num = 0U;
				bool flag2 = InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003668 File Offset: 0x00001868
		private bool CheckValidAndSetDefault<T>(out T value)
		{
			value = default(T);
			return this.IsValidId();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003688 File Offset: 0x00001888
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputDevice);
			return !flag && this.Equals((InputDevice)obj);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036BC File Offset: 0x000018BC
		public bool Equals(InputDevice other)
		{
			return this.deviceId == other.deviceId;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036E0 File Offset: 0x000018E0
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003700 File Offset: 0x00001900
		public static bool operator ==(InputDevice a, InputDevice b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000371C File Offset: 0x0000191C
		public static bool operator !=(InputDevice a, InputDevice b)
		{
			return !(a == b);
		}

		// Token: 0x04000099 RID: 153
		private static List<XRInputSubsystem> s_InputSubsystemCache;

		// Token: 0x0400009A RID: 154
		private ulong m_DeviceId;

		// Token: 0x0400009B RID: 155
		private bool m_Initialized;
	}
}
