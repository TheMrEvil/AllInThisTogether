using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class Microphone
	{
		// Token: 0x0600012E RID: 302
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMicrophoneDeviceIDFromName(string name);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip StartRecord(int deviceID, bool loop, float lengthSec, int frequency);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRecord(int deviceID);

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRecording(int deviceID);

		// Token: 0x06000132 RID: 306
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRecordPosition(int deviceID);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceCaps(int deviceID, out int minFreq, out int maxFreq);

		// Token: 0x06000134 RID: 308 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (flag)
			{
				throw new ArgumentException("Couldn't acquire device ID for device name " + deviceName);
			}
			bool flag2 = lengthSec <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of recording must be greater than zero seconds (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag3 = lengthSec > 3600;
			if (flag3)
			{
				throw new ArgumentException("Length of recording must be less than one hour (was: " + lengthSec.ToString() + " seconds)");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency of recording must be greater than zero (was: " + frequency.ToString() + " Hz)");
			}
			return Microphone.StartRecord(microphoneDeviceIDFromName, loop, (float)lengthSec, frequency);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public static void End(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.EndRecord(microphoneDeviceIDFromName);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000136 RID: 310
		public static extern string[] devices { [NativeName("GetRecordDevices")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000137 RID: 311
		internal static extern bool isAnyDeviceRecording { [NativeName("IsAnyRecordDeviceActive")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000138 RID: 312 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static bool IsRecording(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			return !flag && Microphone.IsRecording(microphoneDeviceIDFromName);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00002C04 File Offset: 0x00000E04
		public static int GetPosition(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = Microphone.GetRecordPosition(microphoneDeviceIDFromName);
			}
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002C30 File Offset: 0x00000E30
		public static void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq)
		{
			minFreq = 0;
			maxFreq = 0;
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.GetDeviceCaps(microphoneDeviceIDFromName, out minFreq, out maxFreq);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00002300 File Offset: 0x00000500
		public Microphone()
		{
		}
	}
}
