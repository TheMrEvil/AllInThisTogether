using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000040 RID: 64
	public static class InputManager
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002F6 RID: 758 RVA: 0x00009C18 File Offset: 0x00007E18
		// (remove) Token: 0x060002F7 RID: 759 RVA: 0x00009C4C File Offset: 0x00007E4C
		public static event Action OnSetup
		{
			[CompilerGenerated]
			add
			{
				Action action = InputManager.OnSetup;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref InputManager.OnSetup, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = InputManager.OnSetup;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref InputManager.OnSetup, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002F8 RID: 760 RVA: 0x00009C80 File Offset: 0x00007E80
		// (remove) Token: 0x060002F9 RID: 761 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public static event Action<ulong, float> OnUpdate
		{
			[CompilerGenerated]
			add
			{
				Action<ulong, float> action = InputManager.OnUpdate;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnUpdate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ulong, float> action = InputManager.OnUpdate;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnUpdate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002FA RID: 762 RVA: 0x00009CE8 File Offset: 0x00007EE8
		// (remove) Token: 0x060002FB RID: 763 RVA: 0x00009D1C File Offset: 0x00007F1C
		public static event Action OnReset
		{
			[CompilerGenerated]
			add
			{
				Action action = InputManager.OnReset;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref InputManager.OnReset, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = InputManager.OnReset;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref InputManager.OnReset, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002FC RID: 764 RVA: 0x00009D50 File Offset: 0x00007F50
		// (remove) Token: 0x060002FD RID: 765 RVA: 0x00009D84 File Offset: 0x00007F84
		public static event Action<InputDevice> OnDeviceAttached
		{
			[CompilerGenerated]
			add
			{
				Action<InputDevice> action = InputManager.OnDeviceAttached;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnDeviceAttached, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<InputDevice> action = InputManager.OnDeviceAttached;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnDeviceAttached, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002FE RID: 766 RVA: 0x00009DB8 File Offset: 0x00007FB8
		// (remove) Token: 0x060002FF RID: 767 RVA: 0x00009DEC File Offset: 0x00007FEC
		public static event Action<InputDevice> OnDeviceDetached
		{
			[CompilerGenerated]
			add
			{
				Action<InputDevice> action = InputManager.OnDeviceDetached;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnDeviceDetached, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<InputDevice> action = InputManager.OnDeviceDetached;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnDeviceDetached, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000300 RID: 768 RVA: 0x00009E20 File Offset: 0x00008020
		// (remove) Token: 0x06000301 RID: 769 RVA: 0x00009E54 File Offset: 0x00008054
		public static event Action<InputDevice> OnActiveDeviceChanged
		{
			[CompilerGenerated]
			add
			{
				Action<InputDevice> action = InputManager.OnActiveDeviceChanged;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnActiveDeviceChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<InputDevice> action = InputManager.OnActiveDeviceChanged;
				Action<InputDevice> action2;
				do
				{
					action2 = action;
					Action<InputDevice> value2 = (Action<InputDevice>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<InputDevice>>(ref InputManager.OnActiveDeviceChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000302 RID: 770 RVA: 0x00009E88 File Offset: 0x00008088
		// (remove) Token: 0x06000303 RID: 771 RVA: 0x00009EBC File Offset: 0x000080BC
		internal static event Action<ulong, float> OnUpdateDevices
		{
			[CompilerGenerated]
			add
			{
				Action<ulong, float> action = InputManager.OnUpdateDevices;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnUpdateDevices, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ulong, float> action = InputManager.OnUpdateDevices;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnUpdateDevices, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000304 RID: 772 RVA: 0x00009EF0 File Offset: 0x000080F0
		// (remove) Token: 0x06000305 RID: 773 RVA: 0x00009F24 File Offset: 0x00008124
		internal static event Action<ulong, float> OnCommitDevices
		{
			[CompilerGenerated]
			add
			{
				Action<ulong, float> action = InputManager.OnCommitDevices;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnCommitDevices, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ulong, float> action = InputManager.OnCommitDevices;
				Action<ulong, float> action2;
				do
				{
					action2 = action;
					Action<ulong, float> value2 = (Action<ulong, float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ulong, float>>(ref InputManager.OnCommitDevices, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00009F57 File Offset: 0x00008157
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00009F5E File Offset: 0x0000815E
		public static bool CommandWasPressed
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<CommandWasPressed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				InputManager.<CommandWasPressed>k__BackingField = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009F66 File Offset: 0x00008166
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00009F6D File Offset: 0x0000816D
		public static bool InvertYAxis
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<InvertYAxis>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				InputManager.<InvertYAxis>k__BackingField = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009F75 File Offset: 0x00008175
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00009F7C File Offset: 0x0000817C
		public static bool IsSetup
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<IsSetup>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				InputManager.<IsSetup>k__BackingField = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00009F84 File Offset: 0x00008184
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00009F8B File Offset: 0x0000818B
		public static IMouseProvider MouseProvider
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<MouseProvider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				InputManager.<MouseProvider>k__BackingField = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009F93 File Offset: 0x00008193
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00009F9A File Offset: 0x0000819A
		public static IKeyboardProvider KeyboardProvider
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<KeyboardProvider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				InputManager.<KeyboardProvider>k__BackingField = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009FA2 File Offset: 0x000081A2
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00009FA9 File Offset: 0x000081A9
		internal static string Platform
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<Platform>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				InputManager.<Platform>k__BackingField = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009FB1 File Offset: 0x000081B1
		[Obsolete("Use InputManager.CommandWasPressed instead.")]
		public static bool MenuWasPressed
		{
			get
			{
				return InputManager.CommandWasPressed;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009FB8 File Offset: 0x000081B8
		internal static bool SetupInternal()
		{
			if (InputManager.IsSetup)
			{
				return false;
			}
			InputManager.Platform = Utility.GetPlatformName(true);
			InputManager.enabled = true;
			InputManager.initialTime = 0f;
			InputManager.currentTime = 0f;
			InputManager.lastUpdateTime = 0f;
			InputManager.currentTick = 0UL;
			InputManager.applicationIsFocused = true;
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
			InputManager.devices.Clear();
			InputManager.Devices = InputManager.devices.AsReadOnly();
			InputManager.activeDevice = InputDevice.Null;
			InputManager.activeDevices.Clear();
			InputManager.ActiveDevices = InputManager.activeDevices.AsReadOnly();
			InputManager.playerActionSets.Clear();
			InputManager.MouseProvider = new UnityMouseProvider();
			InputManager.MouseProvider.Setup();
			InputManager.KeyboardProvider = new UnityKeyboardProvider();
			InputManager.KeyboardProvider.Setup();
			InputManager.IsSetup = true;
			bool flag = true;
			if (InputManager.EnableNativeInput && NativeInputDeviceManager.Enable())
			{
				flag = false;
			}
			if (InputManager.EnableXInput && flag)
			{
				XInputDeviceManager.Enable();
				Logger.LogInfo("[InControl] XInputDeviceManager enabled.");
			}
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			if (flag)
			{
				InputManager.AddDeviceManager<UnityInputDeviceManager>();
				Logger.LogInfo("UnityInputDeviceManager enabled.");
			}
			return true;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A0E8 File Offset: 0x000082E8
		internal static void ResetInternal()
		{
			if (InputManager.OnReset != null)
			{
				InputManager.OnReset();
			}
			InputManager.OnSetup = null;
			InputManager.OnUpdate = null;
			InputManager.OnReset = null;
			InputManager.OnActiveDeviceChanged = null;
			InputManager.OnDeviceAttached = null;
			InputManager.OnDeviceDetached = null;
			InputManager.OnUpdateDevices = null;
			InputManager.OnCommitDevices = null;
			InputManager.DestroyDeviceManagers();
			InputManager.DestroyDevices();
			InputManager.playerActionSets.Clear();
			InputManager.MouseProvider.Reset();
			InputManager.KeyboardProvider.Reset();
			InputManager.IsSetup = false;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A164 File Offset: 0x00008364
		public static void Update()
		{
			InputManager.UpdateInternal();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A16C File Offset: 0x0000836C
		internal static void UpdateInternal()
		{
			InputManager.AssertIsSetup();
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			if (!InputManager.enabled)
			{
				return;
			}
			if (InputManager.SuspendInBackground && !InputManager.applicationIsFocused)
			{
				return;
			}
			InputManager.currentTick += 1UL;
			InputManager.UpdateCurrentTime();
			float num = InputManager.currentTime - InputManager.lastUpdateTime;
			InputManager.MouseProvider.Update();
			InputManager.KeyboardProvider.Update();
			InputManager.UpdateDeviceManagers(num);
			InputManager.CommandWasPressed = false;
			InputManager.UpdateDevices(num);
			InputManager.CommitDevices(num);
			InputDevice inputDevice = InputManager.ActiveDevice;
			InputManager.UpdateActiveDevice();
			InputManager.UpdatePlayerActionSets(num);
			if (inputDevice != InputManager.ActiveDevice && InputManager.OnActiveDeviceChanged != null)
			{
				InputManager.OnActiveDeviceChanged(InputManager.ActiveDevice);
			}
			if (InputManager.OnUpdate != null)
			{
				InputManager.OnUpdate(InputManager.currentTick, num);
			}
			InputManager.lastUpdateTime = InputManager.currentTime;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A244 File Offset: 0x00008444
		public static void Reload()
		{
			InputManager.ResetInternal();
			InputManager.SetupInternal();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A251 File Offset: 0x00008451
		private static void AssertIsSetup()
		{
			if (!InputManager.IsSetup)
			{
				throw new Exception("InputManager is not initialized. Call InputManager.Setup() first.");
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A268 File Offset: 0x00008468
		private static void SetZeroTickOnAllControls()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				ReadOnlyCollection<InputControl> controls = InputManager.devices[i].Controls;
				int count2 = controls.Count;
				for (int j = 0; j < count2; j++)
				{
					InputControl inputControl = controls[j];
					if (inputControl != null)
					{
						inputControl.SetZeroTick();
					}
				}
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A2CC File Offset: 0x000084CC
		public static void ClearInputState()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.devices[i].ClearInputState();
			}
			int count2 = InputManager.playerActionSets.Count;
			for (int j = 0; j < count2; j++)
			{
				InputManager.playerActionSets[j].ClearInputState();
			}
			InputManager.activeDevice = InputDevice.Null;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A331 File Offset: 0x00008531
		internal static void OnApplicationFocus(bool focusState)
		{
			if (!focusState)
			{
				if (InputManager.SuspendInBackground)
				{
					InputManager.ClearInputState();
				}
				InputManager.SetZeroTickOnAllControls();
			}
			InputManager.applicationIsFocused = focusState;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A34D File Offset: 0x0000854D
		internal static void OnApplicationPause(bool pauseState)
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A34F File Offset: 0x0000854F
		internal static void OnApplicationQuit()
		{
			InputManager.ResetInternal();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A356 File Offset: 0x00008556
		internal static void OnLevelWasLoaded()
		{
			InputManager.SetZeroTickOnAllControls();
			InputManager.UpdateInternal();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A364 File Offset: 0x00008564
		public static void AddDeviceManager(InputDeviceManager deviceManager)
		{
			InputManager.AssertIsSetup();
			Type type = deviceManager.GetType();
			if (InputManager.deviceManagerTable.ContainsKey(type))
			{
				Logger.LogError("A device manager of type '" + type.Name + "' already exists; cannot add another.");
				return;
			}
			InputManager.deviceManagers.Add(deviceManager);
			InputManager.deviceManagerTable.Add(type, deviceManager);
			deviceManager.Update(InputManager.currentTick, InputManager.currentTime - InputManager.lastUpdateTime);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A3D2 File Offset: 0x000085D2
		public static void AddDeviceManager<T>() where T : InputDeviceManager, new()
		{
			InputManager.AddDeviceManager(Activator.CreateInstance<T>());
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public static T GetDeviceManager<T>() where T : InputDeviceManager
		{
			InputDeviceManager inputDeviceManager;
			if (InputManager.deviceManagerTable.TryGetValue(typeof(T), out inputDeviceManager))
			{
				return inputDeviceManager as T;
			}
			return default(T);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A41E File Offset: 0x0000861E
		public static bool HasDeviceManager<T>() where T : InputDeviceManager
		{
			return InputManager.deviceManagerTable.ContainsKey(typeof(T));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A434 File Offset: 0x00008634
		private static void UpdateCurrentTime()
		{
			if (InputManager.initialTime < 1E-45f)
			{
				InputManager.initialTime = Time.realtimeSinceStartup;
			}
			InputManager.currentTime = Mathf.Max(0f, Time.realtimeSinceStartup - InputManager.initialTime);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A468 File Offset: 0x00008668
		private static void UpdateDeviceManagers(float deltaTime)
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A4A4 File Offset: 0x000086A4
		private static void DestroyDeviceManagers()
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Destroy();
			}
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000A4EC File Offset: 0x000086EC
		private static void DestroyDevices()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.devices[i].OnDetached();
			}
			InputManager.devices.Clear();
			InputManager.activeDevice = InputDevice.Null;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A534 File Offset: 0x00008734
		private static void UpdateDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.devices[i].Update(InputManager.currentTick, deltaTime);
			}
			if (InputManager.OnUpdateDevices != null)
			{
				InputManager.OnUpdateDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A588 File Offset: 0x00008788
		private static void CommitDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.Commit(InputManager.currentTick, deltaTime);
				if (inputDevice.CommandWasPressed)
				{
					InputManager.CommandWasPressed = true;
				}
			}
			if (InputManager.OnCommitDevices != null)
			{
				InputManager.OnCommitDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private static void UpdateActiveDevice()
		{
			InputManager.activeDevices.Clear();
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				if (inputDevice.LastInputAfter(InputManager.ActiveDevice) && !inputDevice.Passive)
				{
					InputManager.ActiveDevice = inputDevice;
				}
				if (inputDevice.IsActive)
				{
					InputManager.activeDevices.Add(inputDevice);
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000A650 File Offset: 0x00008850
		public static void AttachDevice(InputDevice inputDevice)
		{
			InputManager.AssertIsSetup();
			if (!inputDevice.IsSupportedOnThisPlatform)
			{
				return;
			}
			if (inputDevice.IsAttached)
			{
				return;
			}
			if (!InputManager.devices.Contains(inputDevice))
			{
				InputManager.devices.Add(inputDevice);
				InputManager.devices.Sort((InputDevice d1, InputDevice d2) => d1.SortOrder.CompareTo(d2.SortOrder));
			}
			inputDevice.OnAttached();
			if (InputManager.OnDeviceAttached != null)
			{
				InputManager.OnDeviceAttached(inputDevice);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public static void DetachDevice(InputDevice inputDevice)
		{
			if (!InputManager.IsSetup)
			{
				return;
			}
			if (!inputDevice.IsAttached)
			{
				return;
			}
			InputManager.devices.Remove(inputDevice);
			if (InputManager.ActiveDevice == inputDevice)
			{
				InputManager.ActiveDevice = InputDevice.Null;
			}
			inputDevice.OnDetached();
			if (InputManager.OnDeviceDetached != null)
			{
				InputManager.OnDeviceDetached(inputDevice);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A724 File Offset: 0x00008924
		public static void HideDevicesWithProfile(Type type)
		{
			if (type.IsSubclassOf(typeof(InputDeviceProfile)))
			{
				InputDeviceProfile.Hide(type);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000A73E File Offset: 0x0000893E
		internal static void AttachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			if (!InputManager.playerActionSets.Contains(playerActionSet))
			{
				InputManager.playerActionSets.Add(playerActionSet);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000A758 File Offset: 0x00008958
		internal static void DetachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			InputManager.playerActionSets.Remove(playerActionSet);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000A768 File Offset: 0x00008968
		internal static void UpdatePlayerActionSets(float deltaTime)
		{
			int count = InputManager.playerActionSets.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.playerActionSets[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public static bool AnyKeyIsPressed
		{
			get
			{
				return KeyCombo.Detect(true).IncludeCount > 0;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A7C2 File Offset: 0x000089C2
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000A7D2 File Offset: 0x000089D2
		public static InputDevice ActiveDevice
		{
			get
			{
				return InputManager.activeDevice ?? InputDevice.Null;
			}
			private set
			{
				InputManager.activeDevice = (value ?? InputDevice.Null);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000A7E3 File Offset: 0x000089E3
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000A7EA File Offset: 0x000089EA
		public static bool Enabled
		{
			get
			{
				return InputManager.enabled;
			}
			set
			{
				if (InputManager.enabled != value)
				{
					if (value)
					{
						InputManager.SetZeroTickOnAllControls();
						InputManager.UpdateInternal();
					}
					else
					{
						InputManager.ClearInputState();
						InputManager.SetZeroTickOnAllControls();
					}
					InputManager.enabled = value;
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A813 File Offset: 0x00008A13
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000A81A File Offset: 0x00008A1A
		public static bool SuspendInBackground
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<SuspendInBackground>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				InputManager.<SuspendInBackground>k__BackingField = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A822 File Offset: 0x00008A22
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000A829 File Offset: 0x00008A29
		public static bool EnableNativeInput
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<EnableNativeInput>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<EnableNativeInput>k__BackingField = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A831 File Offset: 0x00008A31
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000A838 File Offset: 0x00008A38
		public static bool EnableXInput
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<EnableXInput>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<EnableXInput>k__BackingField = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A840 File Offset: 0x00008A40
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000A847 File Offset: 0x00008A47
		public static uint XInputUpdateRate
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<XInputUpdateRate>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<XInputUpdateRate>k__BackingField = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A84F File Offset: 0x00008A4F
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000A856 File Offset: 0x00008A56
		public static uint XInputBufferSize
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<XInputBufferSize>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<XInputBufferSize>k__BackingField = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A85E File Offset: 0x00008A5E
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000A865 File Offset: 0x00008A65
		public static bool NativeInputEnableXInput
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<NativeInputEnableXInput>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<NativeInputEnableXInput>k__BackingField = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A86D File Offset: 0x00008A6D
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000A874 File Offset: 0x00008A74
		public static bool NativeInputEnableMFi
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<NativeInputEnableMFi>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<NativeInputEnableMFi>k__BackingField = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A87C File Offset: 0x00008A7C
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000A883 File Offset: 0x00008A83
		public static bool NativeInputPreventSleep
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<NativeInputPreventSleep>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<NativeInputPreventSleep>k__BackingField = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A88B File Offset: 0x00008A8B
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000A892 File Offset: 0x00008A92
		public static uint NativeInputUpdateRate
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<NativeInputUpdateRate>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<NativeInputUpdateRate>k__BackingField = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A89A File Offset: 0x00008A9A
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000A8A1 File Offset: 0x00008AA1
		public static bool EnableICade
		{
			[CompilerGenerated]
			get
			{
				return InputManager.<EnableICade>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				InputManager.<EnableICade>k__BackingField = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A8A9 File Offset: 0x00008AA9
		internal static VersionInfo UnityVersion
		{
			get
			{
				if (InputManager.unityVersion == null)
				{
					InputManager.unityVersion = new VersionInfo?(VersionInfo.UnityVersion());
				}
				return InputManager.unityVersion.Value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public static ulong CurrentTick
		{
			get
			{
				return InputManager.currentTick;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		public static float CurrentTime
		{
			get
			{
				return InputManager.currentTime;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		// Note: this type is marked as 'beforefieldinit'.
		static InputManager()
		{
		}

		// Token: 0x040002EC RID: 748
		public static readonly VersionInfo Version = VersionInfo.InControlVersion();

		// Token: 0x040002ED RID: 749
		[CompilerGenerated]
		private static Action OnSetup;

		// Token: 0x040002EE RID: 750
		[CompilerGenerated]
		private static Action<ulong, float> OnUpdate;

		// Token: 0x040002EF RID: 751
		[CompilerGenerated]
		private static Action OnReset;

		// Token: 0x040002F0 RID: 752
		[CompilerGenerated]
		private static Action<InputDevice> OnDeviceAttached;

		// Token: 0x040002F1 RID: 753
		[CompilerGenerated]
		private static Action<InputDevice> OnDeviceDetached;

		// Token: 0x040002F2 RID: 754
		[CompilerGenerated]
		private static Action<InputDevice> OnActiveDeviceChanged;

		// Token: 0x040002F3 RID: 755
		[CompilerGenerated]
		private static Action<ulong, float> OnUpdateDevices;

		// Token: 0x040002F4 RID: 756
		[CompilerGenerated]
		private static Action<ulong, float> OnCommitDevices;

		// Token: 0x040002F5 RID: 757
		private static readonly List<InputDeviceManager> deviceManagers = new List<InputDeviceManager>();

		// Token: 0x040002F6 RID: 758
		private static readonly Dictionary<Type, InputDeviceManager> deviceManagerTable = new Dictionary<Type, InputDeviceManager>();

		// Token: 0x040002F7 RID: 759
		private static readonly List<InputDevice> devices = new List<InputDevice>();

		// Token: 0x040002F8 RID: 760
		private static InputDevice activeDevice = InputDevice.Null;

		// Token: 0x040002F9 RID: 761
		private static readonly List<InputDevice> activeDevices = new List<InputDevice>();

		// Token: 0x040002FA RID: 762
		private static readonly List<PlayerActionSet> playerActionSets = new List<PlayerActionSet>();

		// Token: 0x040002FB RID: 763
		public static ReadOnlyCollection<InputDevice> Devices;

		// Token: 0x040002FC RID: 764
		public static ReadOnlyCollection<InputDevice> ActiveDevices;

		// Token: 0x040002FD RID: 765
		[CompilerGenerated]
		private static bool <CommandWasPressed>k__BackingField;

		// Token: 0x040002FE RID: 766
		[CompilerGenerated]
		private static bool <InvertYAxis>k__BackingField;

		// Token: 0x040002FF RID: 767
		[CompilerGenerated]
		private static bool <IsSetup>k__BackingField;

		// Token: 0x04000300 RID: 768
		[CompilerGenerated]
		private static IMouseProvider <MouseProvider>k__BackingField;

		// Token: 0x04000301 RID: 769
		[CompilerGenerated]
		private static IKeyboardProvider <KeyboardProvider>k__BackingField;

		// Token: 0x04000302 RID: 770
		[CompilerGenerated]
		private static string <Platform>k__BackingField;

		// Token: 0x04000303 RID: 771
		private static bool applicationIsFocused;

		// Token: 0x04000304 RID: 772
		private static float initialTime;

		// Token: 0x04000305 RID: 773
		private static float currentTime;

		// Token: 0x04000306 RID: 774
		private static float lastUpdateTime;

		// Token: 0x04000307 RID: 775
		private static ulong currentTick;

		// Token: 0x04000308 RID: 776
		private static VersionInfo? unityVersion;

		// Token: 0x04000309 RID: 777
		private static bool enabled;

		// Token: 0x0400030A RID: 778
		[CompilerGenerated]
		private static bool <SuspendInBackground>k__BackingField;

		// Token: 0x0400030B RID: 779
		[CompilerGenerated]
		private static bool <EnableNativeInput>k__BackingField;

		// Token: 0x0400030C RID: 780
		[CompilerGenerated]
		private static bool <EnableXInput>k__BackingField;

		// Token: 0x0400030D RID: 781
		[CompilerGenerated]
		private static uint <XInputUpdateRate>k__BackingField;

		// Token: 0x0400030E RID: 782
		[CompilerGenerated]
		private static uint <XInputBufferSize>k__BackingField;

		// Token: 0x0400030F RID: 783
		[CompilerGenerated]
		private static bool <NativeInputEnableXInput>k__BackingField;

		// Token: 0x04000310 RID: 784
		[CompilerGenerated]
		private static bool <NativeInputEnableMFi>k__BackingField;

		// Token: 0x04000311 RID: 785
		[CompilerGenerated]
		private static bool <NativeInputPreventSleep>k__BackingField;

		// Token: 0x04000312 RID: 786
		[CompilerGenerated]
		private static uint <NativeInputUpdateRate>k__BackingField;

		// Token: 0x04000313 RID: 787
		[CompilerGenerated]
		private static bool <EnableICade>k__BackingField;

		// Token: 0x02000211 RID: 529
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600090B RID: 2315 RVA: 0x00052C1A File Offset: 0x00050E1A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x00052C26 File Offset: 0x00050E26
			public <>c()
			{
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x00052C30 File Offset: 0x00050E30
			internal int <AttachDevice>b__88_0(InputDevice d1, InputDevice d2)
			{
				return d1.SortOrder.CompareTo(d2.SortOrder);
			}

			// Token: 0x04000455 RID: 1109
			public static readonly InputManager.<>c <>9 = new InputManager.<>c();

			// Token: 0x04000456 RID: 1110
			public static Comparison<InputDevice> <>9__88_0;
		}
	}
}
