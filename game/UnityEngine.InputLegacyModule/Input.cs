using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		// Token: 0x0600005E RID: 94
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(KeyCode key);

		// Token: 0x0600005F RID: 95
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString(string name);

		// Token: 0x06000060 RID: 96
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(KeyCode key);

		// Token: 0x06000061 RID: 97
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString(string name);

		// Token: 0x06000062 RID: 98
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(KeyCode key);

		// Token: 0x06000063 RID: 99
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString(string name);

		// Token: 0x06000064 RID: 100
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string axisName);

		// Token: 0x06000065 RID: 101
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxisRaw(string axisName);

		// Token: 0x06000066 RID: 102
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string buttonName);

		// Token: 0x06000067 RID: 103
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonDown(string buttonName);

		// Token: 0x06000068 RID: 104
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonUp(string buttonName);

		// Token: 0x06000069 RID: 105
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x0600006A RID: 106
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x0600006B RID: 107
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x0600006C RID: 108
		[FreeFunction("ResetInput")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();

		// Token: 0x0600006D RID: 109
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		// Token: 0x0600006E RID: 110 RVA: 0x00002650 File Offset: 0x00000850
		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			Touch result;
			Input.GetTouch_Injected(index, out result);
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002668 File Offset: 0x00000868
		[NativeThrows]
		public static AccelerationEvent GetAccelerationEvent(int index)
		{
			AccelerationEvent result;
			Input.GetAccelerationEvent_Injected(index, out result);
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002680 File Offset: 0x00000880
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt(key);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002698 File Offset: 0x00000898
		public static bool GetKey(string name)
		{
			return Input.GetKeyString(name);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000026B0 File Offset: 0x000008B0
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt(key);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000026C8 File Offset: 0x000008C8
		public static bool GetKeyUp(string name)
		{
			return Input.GetKeyUpString(name);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000026E0 File Offset: 0x000008E0
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt(key);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000026F8 File Offset: 0x000008F8
		public static bool GetKeyDown(string name)
		{
			return Input.GetKeyDownString(name);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002710 File Offset: 0x00000910
		[Conditional("UNITY_EDITOR")]
		internal static void SimulateTouch(Touch touch)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002713 File Offset: 0x00000913
		[Conditional("UNITY_EDITOR")]
		[NativeConditional("UNITY_EDITOR")]
		[FreeFunction("SimulateTouch")]
		private static void SimulateTouchInternal(Touch touch, long timestamp)
		{
			Input.SimulateTouchInternal_Injected(ref touch, timestamp);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000078 RID: 120
		// (set) Token: 0x06000079 RID: 121
		public static extern bool simulateMouseWithTouches { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007A RID: 122
		[NativeThrows]
		public static extern bool anyKey { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123
		[NativeThrows]
		public static extern bool anyKeyDown { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007C RID: 124
		[NativeThrows]
		public static extern string inputString { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002720 File Offset: 0x00000920
		[NativeThrows]
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 result;
				Input.get_mousePosition_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002738 File Offset: 0x00000938
		[NativeThrows]
		public static Vector2 mouseScrollDelta
		{
			get
			{
				Vector2 result;
				Input.get_mouseScrollDelta_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		public static extern IMECompositionMode imeCompositionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129
		public static extern string compositionString { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130
		public static extern bool imeIsSelected { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002765 File Offset: 0x00000965
		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 result;
				Input.get_compositionCursorPos_Injected(out result);
				return result;
			}
			set
			{
				Input.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000087 RID: 135
		public static extern bool mousePresent { [FreeFunction("GetMousePresent")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000088 RID: 136
		public static extern int touchCount { [FreeFunction("GetTouchCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000089 RID: 137
		public static extern bool touchPressureSupported { [FreeFunction("IsTouchPressureSupported")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008A RID: 138
		public static extern bool stylusTouchSupported { [FreeFunction("IsStylusTouchSupported")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008B RID: 139
		public static extern bool touchSupported { [FreeFunction("IsTouchSupported")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008C RID: 140
		// (set) Token: 0x0600008D RID: 141
		public static extern bool multiTouchEnabled { [FreeFunction("IsMultiTouchEnabled")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("SetMultiTouchEnabled")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008E RID: 142
		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable { [FreeFunction("IsGyroAvailable")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008F RID: 143
		public static extern DeviceOrientation deviceOrientation { [FreeFunction("GetOrientation")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002770 File Offset: 0x00000970
		public static Vector3 acceleration
		{
			[FreeFunction("GetAcceleration")]
			get
			{
				Vector3 result;
				Input.get_acceleration_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		public static extern bool compensateSensors { [FreeFunction("IsCompensatingSensors")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("SetCompensatingSensors")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000093 RID: 147
		public static extern int accelerationEventCount { [FreeFunction("GetAccelerationCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000094 RID: 148
		// (set) Token: 0x06000095 RID: 149
		public static extern bool backButtonLeavesApp { [FreeFunction("GetBackButtonLeavesApp")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("SetBackButtonLeavesApp")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002788 File Offset: 0x00000988
		public static LocationService location
		{
			get
			{
				bool flag = Input.locationServiceInstance == null;
				if (flag)
				{
					Input.locationServiceInstance = new LocationService();
				}
				return Input.locationServiceInstance;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000027B8 File Offset: 0x000009B8
		public static Compass compass
		{
			get
			{
				bool flag = Input.compassInstance == null;
				if (flag)
				{
					Input.compassInstance = new Compass();
				}
				return Input.compassInstance;
			}
		}

		// Token: 0x06000098 RID: 152
		[FreeFunction("GetGyro")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGyroInternal();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000027E8 File Offset: 0x000009E8
		public static Gyroscope gyro
		{
			get
			{
				bool flag = Input.s_MainGyro == null;
				if (flag)
				{
					Input.s_MainGyro = new Gyroscope(Input.GetGyroInternal());
				}
				return Input.s_MainGyro;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000281C File Offset: 0x00000A1C
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000285C File Offset: 0x00000A5C
		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int accelerationEventCount = Input.accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[accelerationEventCount];
				for (int i = 0; i < accelerationEventCount; i++)
				{
					array[i] = Input.GetAccelerationEvent(i);
				}
				return array;
			}
		}

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckDisabled();

		// Token: 0x0600009D RID: 157 RVA: 0x0000257E File Offset: 0x0000077E
		public Input()
		{
		}

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccelerationEvent_Injected(int index, out AccelerationEvent ret);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SimulateTouchInternal_Injected(ref Touch touch, long timestamp);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_acceleration_Injected(out Vector3 ret);

		// Token: 0x0400003A RID: 58
		private static LocationService locationServiceInstance;

		// Token: 0x0400003B RID: 59
		private static Compass compassInstance;

		// Token: 0x0400003C RID: 60
		private static Gyroscope s_MainGyro;
	}
}
