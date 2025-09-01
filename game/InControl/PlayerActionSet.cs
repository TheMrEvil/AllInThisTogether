using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace InControl
{
	// Token: 0x0200001A RID: 26
	public abstract class PlayerActionSet
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000416E File Offset: 0x0000236E
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004176 File Offset: 0x00002376
		public InputDevice Device
		{
			[CompilerGenerated]
			get
			{
				return this.<Device>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Device>k__BackingField = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000417F File Offset: 0x0000237F
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004187 File Offset: 0x00002387
		public List<InputDevice> IncludeDevices
		{
			[CompilerGenerated]
			get
			{
				return this.<IncludeDevices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IncludeDevices>k__BackingField = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004190 File Offset: 0x00002390
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004198 File Offset: 0x00002398
		public List<InputDevice> ExcludeDevices
		{
			[CompilerGenerated]
			get
			{
				return this.<ExcludeDevices>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExcludeDevices>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000041A9 File Offset: 0x000023A9
		public ReadOnlyCollection<PlayerAction> Actions
		{
			[CompilerGenerated]
			get
			{
				return this.<Actions>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Actions>k__BackingField = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000041B2 File Offset: 0x000023B2
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000041BA File Offset: 0x000023BA
		public ulong UpdateTick
		{
			[CompilerGenerated]
			get
			{
				return this.<UpdateTick>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UpdateTick>k__BackingField = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000DE RID: 222 RVA: 0x000041C4 File Offset: 0x000023C4
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x000041FC File Offset: 0x000023FC
		public event Action<BindingSourceType> OnLastInputTypeChanged
		{
			[CompilerGenerated]
			add
			{
				Action<BindingSourceType> action = this.OnLastInputTypeChanged;
				Action<BindingSourceType> action2;
				do
				{
					action2 = action;
					Action<BindingSourceType> value2 = (Action<BindingSourceType>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<BindingSourceType>>(ref this.OnLastInputTypeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<BindingSourceType> action = this.OnLastInputTypeChanged;
				Action<BindingSourceType> action2;
				do
				{
					action2 = action;
					Action<BindingSourceType> value2 = (Action<BindingSourceType>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<BindingSourceType>>(ref this.OnLastInputTypeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004231 File Offset: 0x00002431
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004239 File Offset: 0x00002439
		public bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return this.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004242 File Offset: 0x00002442
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000424A File Offset: 0x0000244A
		public bool PreventInputWhileListeningForBinding
		{
			[CompilerGenerated]
			get
			{
				return this.<PreventInputWhileListeningForBinding>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PreventInputWhileListeningForBinding>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004253 File Offset: 0x00002453
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000425B File Offset: 0x0000245B
		public object UserData
		{
			[CompilerGenerated]
			get
			{
				return this.<UserData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserData>k__BackingField = value;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004264 File Offset: 0x00002464
		protected PlayerActionSet()
		{
			this.Enabled = true;
			this.PreventInputWhileListeningForBinding = true;
			this.Device = null;
			this.IncludeDevices = new List<InputDevice>();
			this.ExcludeDevices = new List<InputDevice>();
			this.Actions = new ReadOnlyCollection<PlayerAction>(this.actions);
			InputManager.AttachPlayerActionSet(this);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000042F0 File Offset: 0x000024F0
		public void Destroy()
		{
			this.OnLastInputTypeChanged = null;
			InputManager.DetachPlayerActionSet(this);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000042FF File Offset: 0x000024FF
		protected PlayerAction CreatePlayerAction(string name)
		{
			return new PlayerAction(name, this);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004308 File Offset: 0x00002508
		internal void AddPlayerAction(PlayerAction action)
		{
			action.Device = this.FindActiveDevice();
			if (this.actionsByName.ContainsKey(action.Name))
			{
				throw new InControlException("Action '" + action.Name + "' already exists in this set.");
			}
			this.actions.Add(action);
			this.actionsByName.Add(action.Name, action);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004370 File Offset: 0x00002570
		protected PlayerOneAxisAction CreateOneAxisPlayerAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			PlayerOneAxisAction playerOneAxisAction = new PlayerOneAxisAction(negativeAction, positiveAction);
			this.oneAxisActions.Add(playerOneAxisAction);
			return playerOneAxisAction;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004394 File Offset: 0x00002594
		protected PlayerTwoAxisAction CreateTwoAxisPlayerAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			PlayerTwoAxisAction playerTwoAxisAction = new PlayerTwoAxisAction(negativeXAction, positiveXAction, negativeYAction, positiveYAction);
			this.twoAxisActions.Add(playerTwoAxisAction);
			return playerTwoAxisAction;
		}

		// Token: 0x1700004A RID: 74
		public PlayerAction this[string actionName]
		{
			get
			{
				PlayerAction result;
				if (this.actionsByName.TryGetValue(actionName, out result))
				{
					return result;
				}
				throw new KeyNotFoundException("Action '" + actionName + "' does not exist in this action set.");
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000043F0 File Offset: 0x000025F0
		public PlayerAction GetPlayerActionByName(string actionName)
		{
			PlayerAction result;
			if (this.actionsByName.TryGetValue(actionName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004410 File Offset: 0x00002610
		internal void Update(ulong updateTick, float deltaTime)
		{
			InputDevice device = this.Device ?? this.FindActiveDevice();
			BindingSourceType lastInputType = this.LastInputType;
			ulong lastInputTypeChangedTick = this.LastInputTypeChangedTick;
			InputDeviceClass lastDeviceClass = this.LastDeviceClass;
			InputDeviceStyle lastDeviceStyle = this.LastDeviceStyle;
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = this.actions[i];
				playerAction.Update(updateTick, deltaTime, device);
				if (playerAction.UpdateTick > this.UpdateTick)
				{
					this.UpdateTick = playerAction.UpdateTick;
					this.activeDevice = playerAction.ActiveDevice;
				}
				if (playerAction.LastInputTypeChangedTick > lastInputTypeChangedTick)
				{
					lastInputType = playerAction.LastInputType;
					lastInputTypeChangedTick = playerAction.LastInputTypeChangedTick;
					lastDeviceClass = playerAction.LastDeviceClass;
					lastDeviceStyle = playerAction.LastDeviceStyle;
				}
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].Update(updateTick, deltaTime);
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].Update(updateTick, deltaTime);
			}
			if (lastInputTypeChangedTick > this.LastInputTypeChangedTick)
			{
				bool flag = lastInputType != this.LastInputType;
				this.LastInputType = lastInputType;
				this.LastInputTypeChangedTick = lastInputTypeChangedTick;
				this.LastDeviceClass = lastDeviceClass;
				this.LastDeviceStyle = lastDeviceStyle;
				if (this.OnLastInputTypeChanged != null && flag)
				{
					this.OnLastInputTypeChanged(lastInputType);
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004588 File Offset: 0x00002788
		public void Reset()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ResetBindings();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045C0 File Offset: 0x000027C0
		private InputDevice FindActiveDevice()
		{
			bool flag = this.IncludeDevices.Count > 0;
			bool flag2 = this.ExcludeDevices.Count > 0;
			if (flag || flag2)
			{
				InputDevice inputDevice = InputDevice.Null;
				int count = InputManager.Devices.Count;
				for (int i = 0; i < count; i++)
				{
					InputDevice inputDevice2 = InputManager.Devices[i];
					if (inputDevice2 != inputDevice && inputDevice2.LastInputAfter(inputDevice) && !inputDevice2.Passive && (!flag2 || !this.ExcludeDevices.Contains(inputDevice2)) && (!flag || this.IncludeDevices.Contains(inputDevice2)))
					{
						inputDevice = inputDevice2;
					}
				}
				return inputDevice;
			}
			return InputManager.ActiveDevice;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004668 File Offset: 0x00002868
		public void ClearInputState()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ClearInputState();
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].ClearInputState();
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].ClearInputState();
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000046FC File Offset: 0x000028FC
		public bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.actions[i].HasBinding(binding))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004744 File Offset: 0x00002944
		public void RemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].RemoveBinding(binding);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004785 File Offset: 0x00002985
		public bool IsListeningForBinding
		{
			get
			{
				return this.listenWithAction != null;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004790 File Offset: 0x00002990
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004798 File Offset: 0x00002998
		public BindingListenOptions ListenOptions
		{
			get
			{
				return this.listenOptions;
			}
			set
			{
				this.listenOptions = (value ?? new BindingListenOptions());
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000047AA File Offset: 0x000029AA
		public InputDevice ActiveDevice
		{
			get
			{
				return this.activeDevice ?? InputDevice.Null;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000047BC File Offset: 0x000029BC
		public byte[] SaveData()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					binaryWriter.Write(66);
					binaryWriter.Write(73);
					binaryWriter.Write(78);
					binaryWriter.Write(68);
					binaryWriter.Write(2);
					int count = this.actions.Count;
					binaryWriter.Write(count);
					for (int i = 0; i < count; i++)
					{
						this.actions[i].Save(binaryWriter);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004874 File Offset: 0x00002A74
		public void LoadData(byte[] data)
		{
			if (data == null)
			{
				return;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						if (binaryReader.ReadUInt32() != 1145981250U)
						{
							throw new Exception("Unknown data format.");
						}
						ushort num = binaryReader.ReadUInt16();
						if (num < 1 || num > 2)
						{
							throw new Exception("Unknown data format version: " + num.ToString());
						}
						int num2 = binaryReader.ReadInt32();
						for (int i = 0; i < num2; i++)
						{
							PlayerAction playerAction;
							if (this.actionsByName.TryGetValue(binaryReader.ReadString(), out playerAction))
							{
								playerAction.Load(binaryReader, num);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogError("Provided state could not be loaded:\n" + ex.Message);
				this.Reset();
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000496C File Offset: 0x00002B6C
		public string Save()
		{
			return Convert.ToBase64String(this.SaveData());
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000497C File Offset: 0x00002B7C
		public void Load(string data)
		{
			if (data == null)
			{
				return;
			}
			try
			{
				this.LoadData(Convert.FromBase64String(data));
			}
			catch (Exception ex)
			{
				Logger.LogError("Provided state could not be loaded:\n" + ex.Message);
				this.Reset();
			}
		}

		// Token: 0x040000FC RID: 252
		[CompilerGenerated]
		private InputDevice <Device>k__BackingField;

		// Token: 0x040000FD RID: 253
		[CompilerGenerated]
		private List<InputDevice> <IncludeDevices>k__BackingField;

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		private List<InputDevice> <ExcludeDevices>k__BackingField;

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private ReadOnlyCollection<PlayerAction> <Actions>k__BackingField;

		// Token: 0x04000100 RID: 256
		[CompilerGenerated]
		private ulong <UpdateTick>k__BackingField;

		// Token: 0x04000101 RID: 257
		public BindingSourceType LastInputType;

		// Token: 0x04000102 RID: 258
		[CompilerGenerated]
		private Action<BindingSourceType> OnLastInputTypeChanged;

		// Token: 0x04000103 RID: 259
		public ulong LastInputTypeChangedTick;

		// Token: 0x04000104 RID: 260
		public InputDeviceClass LastDeviceClass;

		// Token: 0x04000105 RID: 261
		public InputDeviceStyle LastDeviceStyle;

		// Token: 0x04000106 RID: 262
		[CompilerGenerated]
		private bool <Enabled>k__BackingField;

		// Token: 0x04000107 RID: 263
		[CompilerGenerated]
		private bool <PreventInputWhileListeningForBinding>k__BackingField;

		// Token: 0x04000108 RID: 264
		[CompilerGenerated]
		private object <UserData>k__BackingField;

		// Token: 0x04000109 RID: 265
		private List<PlayerAction> actions = new List<PlayerAction>();

		// Token: 0x0400010A RID: 266
		private List<PlayerOneAxisAction> oneAxisActions = new List<PlayerOneAxisAction>();

		// Token: 0x0400010B RID: 267
		private List<PlayerTwoAxisAction> twoAxisActions = new List<PlayerTwoAxisAction>();

		// Token: 0x0400010C RID: 268
		private Dictionary<string, PlayerAction> actionsByName = new Dictionary<string, PlayerAction>();

		// Token: 0x0400010D RID: 269
		private BindingListenOptions listenOptions = new BindingListenOptions();

		// Token: 0x0400010E RID: 270
		internal PlayerAction listenWithAction;

		// Token: 0x0400010F RID: 271
		private InputDevice activeDevice;

		// Token: 0x04000110 RID: 272
		private const ushort currentDataFormatVersion = 2;
	}
}
