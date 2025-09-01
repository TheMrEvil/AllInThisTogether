using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using InControl.NativeDeviceProfiles;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000047 RID: 71
	public class NativeInputDeviceManager : InputDeviceManager
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000C110 File Offset: 0x0000A310
		public NativeInputDeviceManager()
		{
			this.attachedDevices = new List<NativeInputDevice>();
			this.detachedDevices = new List<NativeInputDevice>();
			this.systemDeviceProfiles = new List<InputDeviceProfile>(NativeInputDeviceProfileList.Profiles.Length);
			this.customDeviceProfiles = new List<InputDeviceProfile>();
			this.deviceEvents = new uint[32];
			this.AddSystemDeviceProfiles();
			NativeInputOptions options = default(NativeInputOptions);
			options.enableXInput = (InputManager.NativeInputEnableXInput ? 1 : 0);
			options.enableMFi = (InputManager.NativeInputEnableMFi ? 1 : 0);
			options.preventSleep = (InputManager.NativeInputPreventSleep ? 1 : 0);
			if (InputManager.NativeInputUpdateRate > 0U)
			{
				options.updateRate = (ushort)InputManager.NativeInputUpdateRate;
			}
			else
			{
				options.updateRate = (ushort)Mathf.FloorToInt(1f / Time.fixedDeltaTime);
			}
			Native.Init(options);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public override void Destroy()
		{
			Native.Stop();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public override void Update(ulong updateTick, float deltaTime)
		{
			IntPtr source;
			int num = Native.GetDeviceEvents(out source);
			if (num > 0)
			{
				Utility.ArrayExpand<uint>(ref this.deviceEvents, num);
				MarshalUtility.Copy(source, this.deviceEvents, num);
				int num2 = 0;
				uint num3 = this.deviceEvents[num2++];
				int num4 = 0;
				while ((long)num4 < (long)((ulong)num3))
				{
					uint num5 = this.deviceEvents[num2++];
					StringBuilder stringBuilder = new StringBuilder(256);
					stringBuilder.Append("Attached native device with handle " + num5.ToString() + ":\n");
					InputDeviceInfo inputDeviceInfo;
					if (Native.GetDeviceInfo(num5, out inputDeviceInfo))
					{
						stringBuilder.AppendFormat("Name: {0}\n", inputDeviceInfo.name);
						stringBuilder.AppendFormat("Driver Type: {0}\n", inputDeviceInfo.driverType);
						stringBuilder.AppendFormat("Location ID: {0}\n", inputDeviceInfo.location);
						stringBuilder.AppendFormat("Serial Number: {0}\n", inputDeviceInfo.serialNumber);
						stringBuilder.AppendFormat("Vendor ID: 0x{0:x}\n", inputDeviceInfo.vendorID);
						stringBuilder.AppendFormat("Product ID: 0x{0:x}\n", inputDeviceInfo.productID);
						stringBuilder.AppendFormat("Version Number: 0x{0:x}\n", inputDeviceInfo.versionNumber);
						stringBuilder.AppendFormat("Buttons: {0}\n", inputDeviceInfo.numButtons);
						stringBuilder.AppendFormat("Analogs: {0}\n", inputDeviceInfo.numAnalogs);
						this.DetectDevice(num5, inputDeviceInfo);
					}
					Logger.LogInfo(stringBuilder.ToString());
					num4++;
				}
				uint num6 = this.deviceEvents[num2++];
				int num7 = 0;
				while ((long)num7 < (long)((ulong)num6))
				{
					uint deviceHandle = this.deviceEvents[num2++];
					Logger.LogInfo("Detached native device with handle " + deviceHandle.ToString() + ":");
					NativeInputDevice nativeInputDevice = this.FindAttachedDevice(deviceHandle);
					if (nativeInputDevice != null)
					{
						this.DetachDevice(nativeInputDevice);
					}
					else
					{
						Logger.LogWarning("Couldn't find device to detach with handle: " + deviceHandle.ToString());
					}
					num7++;
				}
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		private void DetectDevice(uint deviceHandle, InputDeviceInfo deviceInfo)
		{
			InputDeviceProfile inputDeviceProfile = null;
			inputDeviceProfile = (inputDeviceProfile ?? this.customDeviceProfiles.Find((InputDeviceProfile profile) => profile.Matches(deviceInfo)));
			inputDeviceProfile = (inputDeviceProfile ?? this.systemDeviceProfiles.Find((InputDeviceProfile profile) => profile.Matches(deviceInfo)));
			inputDeviceProfile = (inputDeviceProfile ?? this.customDeviceProfiles.Find((InputDeviceProfile profile) => profile.LastResortMatches(deviceInfo)));
			inputDeviceProfile = (inputDeviceProfile ?? this.systemDeviceProfiles.Find((InputDeviceProfile profile) => profile.LastResortMatches(deviceInfo)));
			if (inputDeviceProfile == null || inputDeviceProfile.IsNotHidden)
			{
				NativeInputDevice nativeInputDevice = this.FindDetachedDevice(deviceInfo) ?? new NativeInputDevice();
				nativeInputDevice.Initialize(deviceHandle, deviceInfo, inputDeviceProfile);
				this.AttachDevice(nativeInputDevice);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C4AE File Offset: 0x0000A6AE
		private void AttachDevice(NativeInputDevice device)
		{
			this.detachedDevices.Remove(device);
			this.attachedDevices.Add(device);
			InputManager.AttachDevice(device);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C4CF File Offset: 0x0000A6CF
		private void DetachDevice(NativeInputDevice device)
		{
			this.attachedDevices.Remove(device);
			this.detachedDevices.Add(device);
			InputManager.DetachDevice(device);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		private NativeInputDevice FindAttachedDevice(uint deviceHandle)
		{
			int count = this.attachedDevices.Count;
			for (int i = 0; i < count; i++)
			{
				NativeInputDevice nativeInputDevice = this.attachedDevices[i];
				if (nativeInputDevice.Handle == deviceHandle)
				{
					return nativeInputDevice;
				}
			}
			return null;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C530 File Offset: 0x0000A730
		private NativeInputDevice FindDetachedDevice(InputDeviceInfo deviceInfo)
		{
			ReadOnlyCollection<NativeInputDevice> arg = new ReadOnlyCollection<NativeInputDevice>(this.detachedDevices);
			if (NativeInputDeviceManager.CustomFindDetachedDevice != null)
			{
				return NativeInputDeviceManager.CustomFindDetachedDevice(deviceInfo, arg);
			}
			return NativeInputDeviceManager.SystemFindDetachedDevice(deviceInfo, arg);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C564 File Offset: 0x0000A764
		private static NativeInputDevice SystemFindDetachedDevice(InputDeviceInfo deviceInfo, ReadOnlyCollection<NativeInputDevice> detachedDevices)
		{
			int count = detachedDevices.Count;
			for (int i = 0; i < count; i++)
			{
				NativeInputDevice nativeInputDevice = detachedDevices[i];
				if (nativeInputDevice.Info.HasSameVendorID(deviceInfo) && nativeInputDevice.Info.HasSameProductID(deviceInfo) && nativeInputDevice.Info.HasSameSerialNumber(deviceInfo))
				{
					return nativeInputDevice;
				}
			}
			for (int j = 0; j < count; j++)
			{
				NativeInputDevice nativeInputDevice2 = detachedDevices[j];
				if (nativeInputDevice2.Info.HasSameVendorID(deviceInfo) && nativeInputDevice2.Info.HasSameProductID(deviceInfo) && nativeInputDevice2.Info.HasSameLocation(deviceInfo))
				{
					return nativeInputDevice2;
				}
			}
			for (int k = 0; k < count; k++)
			{
				NativeInputDevice nativeInputDevice3 = detachedDevices[k];
				if (nativeInputDevice3.Info.HasSameVendorID(deviceInfo) && nativeInputDevice3.Info.HasSameProductID(deviceInfo) && nativeInputDevice3.Info.HasSameVersionNumber(deviceInfo))
				{
					return nativeInputDevice3;
				}
			}
			for (int l = 0; l < count; l++)
			{
				NativeInputDevice nativeInputDevice4 = detachedDevices[l];
				if (nativeInputDevice4.Info.HasSameLocation(deviceInfo))
				{
					return nativeInputDevice4;
				}
			}
			return null;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C697 File Offset: 0x0000A897
		private void AddSystemDeviceProfile(InputDeviceProfile deviceProfile)
		{
			if (deviceProfile != null && deviceProfile.IsSupportedOnThisPlatform)
			{
				this.systemDeviceProfiles.Add(deviceProfile);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		private void AddSystemDeviceProfiles()
		{
			for (int i = 0; i < NativeInputDeviceProfileList.Profiles.Length; i++)
			{
				InputDeviceProfile deviceProfile = InputDeviceProfile.CreateInstanceOfType(NativeInputDeviceProfileList.Profiles[i]);
				this.AddSystemDeviceProfile(deviceProfile);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		public static bool CheckPlatformSupport(ICollection<string> errors)
		{
			if (Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.tvOS)
			{
				return false;
			}
			try
			{
				NativeVersionInfo nativeVersionInfo;
				Native.GetVersionInfo(out nativeVersionInfo);
			}
			catch (DllNotFoundException ex)
			{
				if (errors != null)
				{
					errors.Add(ex.Message + Utility.PluginFileExtension() + " could not be found or is missing a dependency.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C764 File Offset: 0x0000A964
		internal static bool Enable()
		{
			List<string> list = new List<string>();
			if (NativeInputDeviceManager.CheckPlatformSupport(list))
			{
				if (InputManager.NativeInputEnableMFi)
				{
					InputManager.HideDevicesWithProfile(typeof(XboxOneSBluetoothMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(XboxSeriesXBluetoothMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(PlayStation4MacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(PlayStation5USBMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(PlayStation5BluetoothMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(SteelseriesNimbusMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(HoriPadUltimateMacNativeProfile));
					InputManager.HideDevicesWithProfile(typeof(NintendoSwitchProMacNativeProfile));
				}
				InputManager.AddDeviceManager<NativeInputDeviceManager>();
				return true;
			}
			foreach (string str in list)
			{
				Logger.LogError("Error enabling NativeInputDeviceManager: " + str);
			}
			return false;
		}

		// Token: 0x04000339 RID: 825
		public static Func<InputDeviceInfo, ReadOnlyCollection<NativeInputDevice>, NativeInputDevice> CustomFindDetachedDevice;

		// Token: 0x0400033A RID: 826
		private readonly List<NativeInputDevice> attachedDevices;

		// Token: 0x0400033B RID: 827
		private readonly List<NativeInputDevice> detachedDevices;

		// Token: 0x0400033C RID: 828
		private readonly List<InputDeviceProfile> systemDeviceProfiles;

		// Token: 0x0400033D RID: 829
		private readonly List<InputDeviceProfile> customDeviceProfiles;

		// Token: 0x0400033E RID: 830
		private uint[] deviceEvents;

		// Token: 0x02000213 RID: 531
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000914 RID: 2324 RVA: 0x00052D38 File Offset: 0x00050F38
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x00052D40 File Offset: 0x00050F40
			internal bool <DetectDevice>b__0(InputDeviceProfile profile)
			{
				return profile.Matches(this.deviceInfo);
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x00052D4E File Offset: 0x00050F4E
			internal bool <DetectDevice>b__1(InputDeviceProfile profile)
			{
				return profile.Matches(this.deviceInfo);
			}

			// Token: 0x06000917 RID: 2327 RVA: 0x00052D5C File Offset: 0x00050F5C
			internal bool <DetectDevice>b__2(InputDeviceProfile profile)
			{
				return profile.LastResortMatches(this.deviceInfo);
			}

			// Token: 0x06000918 RID: 2328 RVA: 0x00052D6A File Offset: 0x00050F6A
			internal bool <DetectDevice>b__3(InputDeviceProfile profile)
			{
				return profile.LastResortMatches(this.deviceInfo);
			}

			// Token: 0x0400045C RID: 1116
			public InputDeviceInfo deviceInfo;
		}
	}
}
