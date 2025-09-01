using System;

namespace UnityEngine.XR
{
	// Token: 0x02000011 RID: 17
	public static class CommonUsages
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002BFC File Offset: 0x00000DFC
		// Note: this type is marked as 'beforefieldinit'.
		static CommonUsages()
		{
		}

		// Token: 0x0400005E RID: 94
		public static InputFeatureUsage<bool> isTracked = new InputFeatureUsage<bool>("IsTracked");

		// Token: 0x0400005F RID: 95
		public static InputFeatureUsage<bool> primaryButton = new InputFeatureUsage<bool>("PrimaryButton");

		// Token: 0x04000060 RID: 96
		public static InputFeatureUsage<bool> primaryTouch = new InputFeatureUsage<bool>("PrimaryTouch");

		// Token: 0x04000061 RID: 97
		public static InputFeatureUsage<bool> secondaryButton = new InputFeatureUsage<bool>("SecondaryButton");

		// Token: 0x04000062 RID: 98
		public static InputFeatureUsage<bool> secondaryTouch = new InputFeatureUsage<bool>("SecondaryTouch");

		// Token: 0x04000063 RID: 99
		public static InputFeatureUsage<bool> gripButton = new InputFeatureUsage<bool>("GripButton");

		// Token: 0x04000064 RID: 100
		public static InputFeatureUsage<bool> triggerButton = new InputFeatureUsage<bool>("TriggerButton");

		// Token: 0x04000065 RID: 101
		public static InputFeatureUsage<bool> menuButton = new InputFeatureUsage<bool>("MenuButton");

		// Token: 0x04000066 RID: 102
		public static InputFeatureUsage<bool> primary2DAxisClick = new InputFeatureUsage<bool>("Primary2DAxisClick");

		// Token: 0x04000067 RID: 103
		public static InputFeatureUsage<bool> primary2DAxisTouch = new InputFeatureUsage<bool>("Primary2DAxisTouch");

		// Token: 0x04000068 RID: 104
		public static InputFeatureUsage<bool> secondary2DAxisClick = new InputFeatureUsage<bool>("Secondary2DAxisClick");

		// Token: 0x04000069 RID: 105
		public static InputFeatureUsage<bool> secondary2DAxisTouch = new InputFeatureUsage<bool>("Secondary2DAxisTouch");

		// Token: 0x0400006A RID: 106
		public static InputFeatureUsage<bool> userPresence = new InputFeatureUsage<bool>("UserPresence");

		// Token: 0x0400006B RID: 107
		public static InputFeatureUsage<InputTrackingState> trackingState = new InputFeatureUsage<InputTrackingState>("TrackingState");

		// Token: 0x0400006C RID: 108
		public static InputFeatureUsage<float> batteryLevel = new InputFeatureUsage<float>("BatteryLevel");

		// Token: 0x0400006D RID: 109
		public static InputFeatureUsage<float> trigger = new InputFeatureUsage<float>("Trigger");

		// Token: 0x0400006E RID: 110
		public static InputFeatureUsage<float> grip = new InputFeatureUsage<float>("Grip");

		// Token: 0x0400006F RID: 111
		public static InputFeatureUsage<Vector2> primary2DAxis = new InputFeatureUsage<Vector2>("Primary2DAxis");

		// Token: 0x04000070 RID: 112
		public static InputFeatureUsage<Vector2> secondary2DAxis = new InputFeatureUsage<Vector2>("Secondary2DAxis");

		// Token: 0x04000071 RID: 113
		public static InputFeatureUsage<Vector3> devicePosition = new InputFeatureUsage<Vector3>("DevicePosition");

		// Token: 0x04000072 RID: 114
		public static InputFeatureUsage<Vector3> leftEyePosition = new InputFeatureUsage<Vector3>("LeftEyePosition");

		// Token: 0x04000073 RID: 115
		public static InputFeatureUsage<Vector3> rightEyePosition = new InputFeatureUsage<Vector3>("RightEyePosition");

		// Token: 0x04000074 RID: 116
		public static InputFeatureUsage<Vector3> centerEyePosition = new InputFeatureUsage<Vector3>("CenterEyePosition");

		// Token: 0x04000075 RID: 117
		public static InputFeatureUsage<Vector3> colorCameraPosition = new InputFeatureUsage<Vector3>("CameraPosition");

		// Token: 0x04000076 RID: 118
		public static InputFeatureUsage<Vector3> deviceVelocity = new InputFeatureUsage<Vector3>("DeviceVelocity");

		// Token: 0x04000077 RID: 119
		public static InputFeatureUsage<Vector3> deviceAngularVelocity = new InputFeatureUsage<Vector3>("DeviceAngularVelocity");

		// Token: 0x04000078 RID: 120
		public static InputFeatureUsage<Vector3> leftEyeVelocity = new InputFeatureUsage<Vector3>("LeftEyeVelocity");

		// Token: 0x04000079 RID: 121
		public static InputFeatureUsage<Vector3> leftEyeAngularVelocity = new InputFeatureUsage<Vector3>("LeftEyeAngularVelocity");

		// Token: 0x0400007A RID: 122
		public static InputFeatureUsage<Vector3> rightEyeVelocity = new InputFeatureUsage<Vector3>("RightEyeVelocity");

		// Token: 0x0400007B RID: 123
		public static InputFeatureUsage<Vector3> rightEyeAngularVelocity = new InputFeatureUsage<Vector3>("RightEyeAngularVelocity");

		// Token: 0x0400007C RID: 124
		public static InputFeatureUsage<Vector3> centerEyeVelocity = new InputFeatureUsage<Vector3>("CenterEyeVelocity");

		// Token: 0x0400007D RID: 125
		public static InputFeatureUsage<Vector3> centerEyeAngularVelocity = new InputFeatureUsage<Vector3>("CenterEyeAngularVelocity");

		// Token: 0x0400007E RID: 126
		public static InputFeatureUsage<Vector3> colorCameraVelocity = new InputFeatureUsage<Vector3>("CameraVelocity");

		// Token: 0x0400007F RID: 127
		public static InputFeatureUsage<Vector3> colorCameraAngularVelocity = new InputFeatureUsage<Vector3>("CameraAngularVelocity");

		// Token: 0x04000080 RID: 128
		public static InputFeatureUsage<Vector3> deviceAcceleration = new InputFeatureUsage<Vector3>("DeviceAcceleration");

		// Token: 0x04000081 RID: 129
		public static InputFeatureUsage<Vector3> deviceAngularAcceleration = new InputFeatureUsage<Vector3>("DeviceAngularAcceleration");

		// Token: 0x04000082 RID: 130
		public static InputFeatureUsage<Vector3> leftEyeAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAcceleration");

		// Token: 0x04000083 RID: 131
		public static InputFeatureUsage<Vector3> leftEyeAngularAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAngularAcceleration");

		// Token: 0x04000084 RID: 132
		public static InputFeatureUsage<Vector3> rightEyeAcceleration = new InputFeatureUsage<Vector3>("RightEyeAcceleration");

		// Token: 0x04000085 RID: 133
		public static InputFeatureUsage<Vector3> rightEyeAngularAcceleration = new InputFeatureUsage<Vector3>("RightEyeAngularAcceleration");

		// Token: 0x04000086 RID: 134
		public static InputFeatureUsage<Vector3> centerEyeAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAcceleration");

		// Token: 0x04000087 RID: 135
		public static InputFeatureUsage<Vector3> centerEyeAngularAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAngularAcceleration");

		// Token: 0x04000088 RID: 136
		public static InputFeatureUsage<Vector3> colorCameraAcceleration = new InputFeatureUsage<Vector3>("CameraAcceleration");

		// Token: 0x04000089 RID: 137
		public static InputFeatureUsage<Vector3> colorCameraAngularAcceleration = new InputFeatureUsage<Vector3>("CameraAngularAcceleration");

		// Token: 0x0400008A RID: 138
		public static InputFeatureUsage<Quaternion> deviceRotation = new InputFeatureUsage<Quaternion>("DeviceRotation");

		// Token: 0x0400008B RID: 139
		public static InputFeatureUsage<Quaternion> leftEyeRotation = new InputFeatureUsage<Quaternion>("LeftEyeRotation");

		// Token: 0x0400008C RID: 140
		public static InputFeatureUsage<Quaternion> rightEyeRotation = new InputFeatureUsage<Quaternion>("RightEyeRotation");

		// Token: 0x0400008D RID: 141
		public static InputFeatureUsage<Quaternion> centerEyeRotation = new InputFeatureUsage<Quaternion>("CenterEyeRotation");

		// Token: 0x0400008E RID: 142
		public static InputFeatureUsage<Quaternion> colorCameraRotation = new InputFeatureUsage<Quaternion>("CameraRotation");

		// Token: 0x0400008F RID: 143
		public static InputFeatureUsage<Hand> handData = new InputFeatureUsage<Hand>("HandData");

		// Token: 0x04000090 RID: 144
		public static InputFeatureUsage<Eyes> eyesData = new InputFeatureUsage<Eyes>("EyesData");

		// Token: 0x04000091 RID: 145
		[Obsolete("CommonUsages.dPad is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<Vector2> dPad = new InputFeatureUsage<Vector2>("DPad");

		// Token: 0x04000092 RID: 146
		[Obsolete("CommonUsages.indexFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> indexFinger = new InputFeatureUsage<float>("IndexFinger");

		// Token: 0x04000093 RID: 147
		[Obsolete("CommonUsages.MiddleFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> middleFinger = new InputFeatureUsage<float>("MiddleFinger");

		// Token: 0x04000094 RID: 148
		[Obsolete("CommonUsages.RingFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> ringFinger = new InputFeatureUsage<float>("RingFinger");

		// Token: 0x04000095 RID: 149
		[Obsolete("CommonUsages.PinkyFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> pinkyFinger = new InputFeatureUsage<float>("PinkyFinger");

		// Token: 0x04000096 RID: 150
		[Obsolete("CommonUsages.thumbrest is Oculus only, and is being moved to their package. Please use OculusUsages.thumbrest. These will still function until removed.")]
		public static InputFeatureUsage<bool> thumbrest = new InputFeatureUsage<bool>("Thumbrest");

		// Token: 0x04000097 RID: 151
		[Obsolete("CommonUsages.indexTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.indexTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> indexTouch = new InputFeatureUsage<float>("IndexTouch");

		// Token: 0x04000098 RID: 152
		[Obsolete("CommonUsages.thumbTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.thumbTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> thumbTouch = new InputFeatureUsage<float>("ThumbTouch");
	}
}
