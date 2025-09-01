using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000060 RID: 96
	public class UnityInputDeviceManager : InputDeviceManager
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x00010AB3 File Offset: 0x0000ECB3
		public UnityInputDeviceManager()
		{
			this.systemDeviceProfiles = new List<InputDeviceProfile>(UnityInputDeviceProfileList.Profiles.Length);
			this.customDeviceProfiles = new List<InputDeviceProfile>();
			this.AddSystemDeviceProfiles();
			this.QueryJoystickInfo();
			this.AttachDevices();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.deviceRefreshTimer += deltaTime;
			if (this.deviceRefreshTimer >= 1f)
			{
				this.deviceRefreshTimer = 0f;
				this.QueryJoystickInfo();
				if (this.JoystickInfoHasChanged)
				{
					Logger.LogInfo("Change in attached Unity joysticks detected; refreshing device list.");
					this.DetachDevices();
					this.AttachDevices();
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00010B44 File Offset: 0x0000ED44
		private void QueryJoystickInfo()
		{
			this.joystickNames = Input.GetJoystickNames();
			this.joystickCount = this.joystickNames.Length;
			this.joystickHash = 527 + this.joystickCount;
			for (int i = 0; i < this.joystickCount; i++)
			{
				this.joystickHash = this.joystickHash * 31 + this.joystickNames[i].GetHashCode();
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00010BAA File Offset: 0x0000EDAA
		private bool JoystickInfoHasChanged
		{
			get
			{
				return this.joystickHash != this.lastJoystickHash || this.joystickCount != this.lastJoystickCount;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		private void AttachDevices()
		{
			try
			{
				for (int i = 0; i < this.joystickCount; i++)
				{
					this.DetectJoystickDevice(i + 1, this.joystickNames[i]);
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex.Message);
				Logger.LogError(ex.StackTrace);
			}
			this.lastJoystickCount = this.joystickCount;
			this.lastJoystickHash = this.joystickHash;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00010C40 File Offset: 0x0000EE40
		private void DetachDevices()
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.DetachDevice(this.devices[i]);
			}
			this.devices.Clear();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00010C81 File Offset: 0x0000EE81
		public void ReloadDevices()
		{
			this.QueryJoystickInfo();
			this.DetachDevices();
			this.AttachDevices();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00010C95 File Offset: 0x0000EE95
		private void AttachDevice(UnityInputDevice device)
		{
			this.devices.Add(device);
			InputManager.AttachDevice(device);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00010CAC File Offset: 0x0000EEAC
		private bool HasAttachedDeviceWithJoystickId(int unityJoystickId)
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				UnityInputDevice unityInputDevice = this.devices[i] as UnityInputDevice;
				if (unityInputDevice != null && unityInputDevice.JoystickId == unityJoystickId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		private void DetectJoystickDevice(int unityJoystickId, string unityJoystickName)
		{
			if (this.HasAttachedDeviceWithJoystickId(unityJoystickId))
			{
				return;
			}
			if (unityJoystickName.IndexOf("webcam", StringComparison.OrdinalIgnoreCase) != -1)
			{
				return;
			}
			if (InputManager.UnityVersion < new VersionInfo(4, 5, 0, 0) && (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) && unityJoystickName == "Unknown Wireless Controller")
			{
				return;
			}
			if (InputManager.UnityVersion >= new VersionInfo(4, 6, 3, 0) && (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) && string.IsNullOrEmpty(unityJoystickName))
			{
				return;
			}
			InputDeviceProfile inputDeviceProfile = this.DetectDevice(unityJoystickName);
			if (inputDeviceProfile == null)
			{
				UnityInputDevice device = new UnityInputDevice(unityJoystickId, unityJoystickName);
				this.AttachDevice(device);
				Logger.LogWarning(string.Concat(new string[]
				{
					"Device ",
					unityJoystickId.ToString(),
					" with name \"",
					unityJoystickName,
					"\" does not match any supported profiles and will be considered an unknown controller."
				}));
				return;
			}
			if (!inputDeviceProfile.IsHidden)
			{
				UnityInputDevice device2 = new UnityInputDevice(inputDeviceProfile, unityJoystickId, unityJoystickName);
				this.AttachDevice(device2);
				Logger.LogInfo(string.Concat(new string[]
				{
					"Device ",
					unityJoystickId.ToString(),
					" matched profile ",
					inputDeviceProfile.GetType().Name,
					" (",
					inputDeviceProfile.DeviceName,
					")"
				}));
				return;
			}
			Logger.LogInfo(string.Concat(new string[]
			{
				"Device ",
				unityJoystickId.ToString(),
				" matching profile ",
				inputDeviceProfile.GetType().Name,
				" (",
				inputDeviceProfile.DeviceName,
				") is hidden and will not be attached."
			}));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010E88 File Offset: 0x0000F088
		private InputDeviceProfile DetectDevice(string unityJoystickName)
		{
			InputDeviceProfile inputDeviceProfile = null;
			InputDeviceInfo deviceInfo = new InputDeviceInfo
			{
				name = unityJoystickName
			};
			return (((inputDeviceProfile ?? this.customDeviceProfiles.Find((InputDeviceProfile profile) => profile.Matches(deviceInfo))) ?? this.systemDeviceProfiles.Find((InputDeviceProfile profile) => profile.Matches(deviceInfo))) ?? this.customDeviceProfiles.Find((InputDeviceProfile profile) => profile.LastResortMatches(deviceInfo))) ?? this.systemDeviceProfiles.Find((InputDeviceProfile profile) => profile.LastResortMatches(deviceInfo));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00010F1F File Offset: 0x0000F11F
		private void AddSystemDeviceProfile(InputDeviceProfile deviceProfile)
		{
			if (deviceProfile != null && deviceProfile.IsSupportedOnThisPlatform)
			{
				this.systemDeviceProfiles.Add(deviceProfile);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00010F38 File Offset: 0x0000F138
		private void AddSystemDeviceProfiles()
		{
			for (int i = 0; i < UnityInputDeviceProfileList.Profiles.Length; i++)
			{
				InputDeviceProfile deviceProfile = InputDeviceProfile.CreateInstanceOfType(UnityInputDeviceProfileList.Profiles[i]);
				this.AddSystemDeviceProfile(deviceProfile);
			}
		}

		// Token: 0x040003F8 RID: 1016
		private const float deviceRefreshInterval = 1f;

		// Token: 0x040003F9 RID: 1017
		private float deviceRefreshTimer;

		// Token: 0x040003FA RID: 1018
		private readonly List<InputDeviceProfile> systemDeviceProfiles;

		// Token: 0x040003FB RID: 1019
		private readonly List<InputDeviceProfile> customDeviceProfiles;

		// Token: 0x040003FC RID: 1020
		private string[] joystickNames;

		// Token: 0x040003FD RID: 1021
		private int lastJoystickCount;

		// Token: 0x040003FE RID: 1022
		private int lastJoystickHash;

		// Token: 0x040003FF RID: 1023
		private int joystickCount;

		// Token: 0x04000400 RID: 1024
		private int joystickHash;

		// Token: 0x02000219 RID: 537
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x0600091F RID: 2335 RVA: 0x00052E18 File Offset: 0x00051018
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x00052E20 File Offset: 0x00051020
			internal bool <DetectDevice>b__0(InputDeviceProfile profile)
			{
				return profile.Matches(this.deviceInfo);
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x00052E2E File Offset: 0x0005102E
			internal bool <DetectDevice>b__1(InputDeviceProfile profile)
			{
				return profile.Matches(this.deviceInfo);
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x00052E3C File Offset: 0x0005103C
			internal bool <DetectDevice>b__2(InputDeviceProfile profile)
			{
				return profile.LastResortMatches(this.deviceInfo);
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x00052E4A File Offset: 0x0005104A
			internal bool <DetectDevice>b__3(InputDeviceProfile profile)
			{
				return profile.LastResortMatches(this.deviceInfo);
			}

			// Token: 0x0400049A RID: 1178
			public InputDeviceInfo deviceInfo;
		}
	}
}
