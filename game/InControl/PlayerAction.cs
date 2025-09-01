using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000019 RID: 25
	public class PlayerAction : OneAxisInputControl
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000033AE File Offset: 0x000015AE
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000033B6 File Offset: 0x000015B6
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000033BF File Offset: 0x000015BF
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000033C7 File Offset: 0x000015C7
		public PlayerActionSet Owner
		{
			[CompilerGenerated]
			get
			{
				return this.<Owner>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Owner>k__BackingField = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x000033D0 File Offset: 0x000015D0
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00003408 File Offset: 0x00001608
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

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000A8 RID: 168 RVA: 0x00003440 File Offset: 0x00001640
		// (remove) Token: 0x060000A9 RID: 169 RVA: 0x00003478 File Offset: 0x00001678
		public event Action OnBindingsChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnBindingsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnBindingsChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnBindingsChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnBindingsChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000034AD File Offset: 0x000016AD
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000034B5 File Offset: 0x000016B5
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

		// Token: 0x060000AC RID: 172 RVA: 0x000034C0 File Offset: 0x000016C0
		public PlayerAction(string name, PlayerActionSet owner)
		{
			this.Raw = true;
			this.Name = name;
			this.Owner = owner;
			this.bindings = new ReadOnlyCollection<BindingSource>(this.visibleBindings);
			this.unfilteredBindings = new ReadOnlyCollection<BindingSource>(this.regularBindings);
			owner.AddPlayerAction(this);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003560 File Offset: 0x00001760
		public void AddDefaultBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			if (binding.BoundTo != null)
			{
				throw new InControlException("Binding source is already bound to action " + binding.BoundTo.Name);
			}
			if (!this.defaultBindings.Contains(binding))
			{
				this.defaultBindings.Add(binding);
				binding.BoundTo = this;
			}
			if (!this.regularBindings.Contains(binding))
			{
				this.regularBindings.Add(binding);
				binding.BoundTo = this;
				if (binding.IsValid)
				{
					this.visibleBindings.Add(binding);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000035F0 File Offset: 0x000017F0
		public void AddDefaultBinding(params Key[] keys)
		{
			this.AddDefaultBinding(new KeyBindingSource(keys));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000035FE File Offset: 0x000017FE
		public void AddDefaultBinding(KeyCombo keyCombo)
		{
			this.AddDefaultBinding(new KeyBindingSource(keyCombo));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000360C File Offset: 0x0000180C
		public void AddDefaultBinding(Mouse control)
		{
			this.AddDefaultBinding(new MouseBindingSource(control));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000361A File Offset: 0x0000181A
		public void AddDefaultBinding(InputControlType control)
		{
			this.AddDefaultBinding(new DeviceBindingSource(control));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003628 File Offset: 0x00001828
		public bool AddBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Logger.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			this.regularBindings.Add(binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Add(binding);
			}
			this.triggerBindingChanged = true;
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000036A4 File Offset: 0x000018A4
		public bool InsertBindingAt(int index, BindingSource binding)
		{
			if (index < 0 || index > this.visibleBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			if (index == this.visibleBindings.Count)
			{
				return this.AddBinding(binding);
			}
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Logger.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			int index2 = (index == 0) ? 0 : this.regularBindings.IndexOf(this.visibleBindings[index]);
			this.regularBindings.Insert(index2, binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Insert(index, binding);
			}
			this.triggerBindingChanged = true;
			return true;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003774 File Offset: 0x00001974
		public bool ReplaceBinding(BindingSource findBinding, BindingSource withBinding)
		{
			if (findBinding == null || withBinding == null)
			{
				return false;
			}
			if (withBinding.BoundTo != null)
			{
				Logger.LogWarning("Binding source is already bound to action " + withBinding.BoundTo.Name);
				return false;
			}
			int num = this.regularBindings.IndexOf(findBinding);
			if (num < 0)
			{
				Logger.LogWarning("Binding source to replace is not present in this action.");
				return false;
			}
			findBinding.BoundTo = null;
			this.regularBindings[num] = withBinding;
			withBinding.BoundTo = this;
			num = this.visibleBindings.IndexOf(findBinding);
			if (num >= 0)
			{
				this.visibleBindings[num] = withBinding;
			}
			this.triggerBindingChanged = true;
			return true;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003818 File Offset: 0x00001A18
		public bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			BindingSource bindingSource = this.FindBinding(binding);
			return !(bindingSource == null) && bindingSource.BoundTo == this;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000384C File Offset: 0x00001A4C
		public BindingSource FindBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return null;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				return this.regularBindings[num];
			}
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003884 File Offset: 0x00001A84
		private void HardRemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				BindingSource bindingSource = this.regularBindings[num];
				if (bindingSource.BoundTo == this)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(num);
					this.UpdateVisibleBindings();
					this.triggerBindingChanged = true;
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void RemoveBinding(BindingSource binding)
		{
			BindingSource bindingSource = this.FindBinding(binding);
			if (bindingSource != null && bindingSource.BoundTo == this)
			{
				bindingSource.BoundTo = null;
				this.triggerBindingChanged = true;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003919 File Offset: 0x00001B19
		public void RemoveBindingAt(int index)
		{
			if (index < 0 || index >= this.regularBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			this.regularBindings[index].BoundTo = null;
			this.triggerBindingChanged = true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003954 File Offset: 0x00001B54
		private int CountBindingsOfType(BindingSourceType bindingSourceType)
		{
			int num = 0;
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000039A0 File Offset: 0x00001BA0
		private void RemoveFirstBindingOfType(BindingSourceType bindingSourceType)
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(i);
					this.triggerBindingChanged = true;
					return;
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A00 File Offset: 0x00001C00
		private int IndexOfFirstInvalidBinding()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.regularBindings[i].IsValid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A3C File Offset: 0x00001C3C
		public void ClearBindings()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				this.regularBindings[i].BoundTo = null;
			}
			this.regularBindings.Clear();
			this.visibleBindings.Clear();
			this.triggerBindingChanged = true;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003A90 File Offset: 0x00001C90
		public void ResetBindings()
		{
			this.ClearBindings();
			this.regularBindings.AddRange(this.defaultBindings);
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				bindingSource.BoundTo = this;
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
			this.triggerBindingChanged = true;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003AFB File Offset: 0x00001CFB
		public void ListenForBinding()
		{
			this.ListenForBindingReplacing(null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003B04 File Offset: 0x00001D04
		public void ListenForBindingReplacing(BindingSource binding)
		{
			(this.ListenOptions ?? this.Owner.ListenOptions).ReplaceBinding = binding;
			this.Owner.listenWithAction = this;
			int num = this.bindingSourceListeners.Length;
			for (int i = 0; i < num; i++)
			{
				this.bindingSourceListeners[i].Reset();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003B5A File Offset: 0x00001D5A
		public void StopListeningForBinding()
		{
			if (this.IsListeningForBinding)
			{
				this.Owner.listenWithAction = null;
				this.triggerBindingEnded = true;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B77 File Offset: 0x00001D77
		public bool IsListeningForBinding
		{
			get
			{
				return this.Owner.listenWithAction == this;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003B87 File Offset: 0x00001D87
		public ReadOnlyCollection<BindingSource> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003B8F File Offset: 0x00001D8F
		public ReadOnlyCollection<BindingSource> UnfilteredBindings
		{
			get
			{
				return this.unfilteredBindings;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B98 File Offset: 0x00001D98
		private void RemoveOrphanedBindings()
		{
			for (int i = this.regularBindings.Count - 1; i >= 0; i--)
			{
				if (this.regularBindings[i].BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003BE0 File Offset: 0x00001DE0
		internal void Update(ulong updateTick, float deltaTime, InputDevice device)
		{
			this.Device = device;
			this.UpdateBindings(updateTick, deltaTime);
			if (this.triggerBindingChanged)
			{
				if (this.OnBindingsChanged != null)
				{
					this.OnBindingsChanged();
				}
				this.triggerBindingChanged = false;
			}
			if (this.triggerBindingEnded)
			{
				(this.ListenOptions ?? this.Owner.ListenOptions).CallOnBindingEnded(this);
				this.triggerBindingEnded = false;
			}
			this.DetectBindings();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C50 File Offset: 0x00001E50
		private void UpdateBindings(ulong updateTick, float deltaTime)
		{
			bool flag = this.IsListeningForBinding || (this.Owner.IsListeningForBinding && this.Owner.PreventInputWhileListeningForBinding);
			BindingSourceType bindingSourceType = this.LastInputType;
			ulong num = this.LastInputTypeChangedTick;
			ulong updateTick2 = base.UpdateTick;
			InputDeviceClass lastDeviceClass = this.LastDeviceClass;
			InputDeviceStyle lastDeviceStyle = this.LastDeviceStyle;
			int count = this.regularBindings.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
					this.visibleBindings.Remove(bindingSource);
					this.triggerBindingChanged = true;
				}
				else if (!flag)
				{
					float value = bindingSource.GetValue(this.Device);
					if (base.UpdateWithValue(value, updateTick, deltaTime))
					{
						bindingSourceType = bindingSource.BindingSourceType;
						num = updateTick;
						lastDeviceClass = bindingSource.DeviceClass;
						lastDeviceStyle = bindingSource.DeviceStyle;
					}
				}
			}
			if (flag || count == 0)
			{
				base.UpdateWithValue(0f, updateTick, deltaTime);
			}
			base.Commit();
			this.ownerEnabled = this.Owner.Enabled;
			if (num > this.LastInputTypeChangedTick && (bindingSourceType != BindingSourceType.MouseBindingSource || Utility.Abs(base.LastValue - base.Value) >= MouseBindingSource.JitterThreshold))
			{
				bool flag2 = bindingSourceType != this.LastInputType;
				this.LastInputType = bindingSourceType;
				this.LastInputTypeChangedTick = num;
				this.LastDeviceClass = lastDeviceClass;
				this.LastDeviceStyle = lastDeviceStyle;
				if (this.OnLastInputTypeChanged != null && flag2)
				{
					this.OnLastInputTypeChanged(bindingSourceType);
				}
			}
			if (base.UpdateTick > updateTick2)
			{
				this.activeDevice = (this.LastInputTypeIsDevice ? this.Device : null);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003DFC File Offset: 0x00001FFC
		private void DetectBindings()
		{
			if (this.IsListeningForBinding)
			{
				BindingSource bindingSource = null;
				BindingListenOptions bindingListenOptions = this.ListenOptions ?? this.Owner.ListenOptions;
				int num = this.bindingSourceListeners.Length;
				for (int i = 0; i < num; i++)
				{
					bindingSource = this.bindingSourceListeners[i].Listen(bindingListenOptions, this.device);
					if (bindingSource != null)
					{
						break;
					}
				}
				if (bindingSource == null)
				{
					return;
				}
				if (!bindingListenOptions.CallOnBindingFound(this, bindingSource))
				{
					return;
				}
				if (this.HasBinding(bindingSource))
				{
					if (bindingListenOptions.RejectRedundantBindings)
					{
						bindingListenOptions.CallOnBindingRejected(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnAction);
						return;
					}
					this.StopListeningForBinding();
					bindingListenOptions.CallOnBindingAdded(this, bindingSource);
					return;
				}
				else
				{
					if (bindingListenOptions.UnsetDuplicateBindingsOnSet)
					{
						int count = this.Owner.Actions.Count;
						for (int j = 0; j < count; j++)
						{
							this.Owner.Actions[j].HardRemoveBinding(bindingSource);
						}
					}
					if (!bindingListenOptions.AllowDuplicateBindingsPerSet && this.Owner.HasBinding(bindingSource))
					{
						bindingListenOptions.CallOnBindingRejected(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnActionSet);
						return;
					}
					this.StopListeningForBinding();
					if (bindingListenOptions.ReplaceBinding == null)
					{
						if (bindingListenOptions.MaxAllowedBindingsPerType > 0U)
						{
							while ((long)this.CountBindingsOfType(bindingSource.BindingSourceType) >= (long)((ulong)bindingListenOptions.MaxAllowedBindingsPerType))
							{
								this.RemoveFirstBindingOfType(bindingSource.BindingSourceType);
							}
						}
						else if (bindingListenOptions.MaxAllowedBindings > 0U)
						{
							while ((long)this.regularBindings.Count >= (long)((ulong)bindingListenOptions.MaxAllowedBindings))
							{
								int index = Mathf.Max(0, this.IndexOfFirstInvalidBinding());
								this.regularBindings.RemoveAt(index);
								this.triggerBindingChanged = true;
							}
						}
						this.AddBinding(bindingSource);
					}
					else
					{
						this.ReplaceBinding(bindingListenOptions.ReplaceBinding, bindingSource);
					}
					this.UpdateVisibleBindings();
					bindingListenOptions.CallOnBindingAdded(this, bindingSource);
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FAC File Offset: 0x000021AC
		private void UpdateVisibleBindings()
		{
			this.visibleBindings.Clear();
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003FFD File Offset: 0x000021FD
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004024 File Offset: 0x00002224
		internal InputDevice Device
		{
			get
			{
				if (this.device == null)
				{
					this.device = this.Owner.Device;
					this.UpdateVisibleBindings();
				}
				return this.device;
			}
			set
			{
				if (this.device != value)
				{
					this.device = value;
					this.UpdateVisibleBindings();
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000403C File Offset: 0x0000223C
		public InputDevice ActiveDevice
		{
			get
			{
				return this.activeDevice ?? InputDevice.Null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000404D File Offset: 0x0000224D
		private bool LastInputTypeIsDevice
		{
			get
			{
				return this.LastInputType == BindingSourceType.DeviceBindingSource || this.LastInputType == BindingSourceType.UnknownDeviceBindingSource;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004063 File Offset: 0x00002263
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000406A File Offset: 0x0000226A
		[Obsolete("Please set this property on device controls directly. It does nothing here.")]
		public new float LowerDeadZone
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000406C File Offset: 0x0000226C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004073 File Offset: 0x00002273
		[Obsolete("Please set this property on device controls directly. It does nothing here.")]
		public new float UpperDeadZone
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004078 File Offset: 0x00002278
		internal void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			this.ClearBindings();
			int num = reader.ReadInt32();
			int i = 0;
			while (i < num)
			{
				BindingSourceType bindingSourceType = (BindingSourceType)reader.ReadInt32();
				BindingSource bindingSource;
				switch (bindingSourceType)
				{
				case BindingSourceType.None:
					IL_81:
					i++;
					continue;
				case BindingSourceType.DeviceBindingSource:
					bindingSource = new DeviceBindingSource();
					break;
				case BindingSourceType.KeyBindingSource:
					bindingSource = new KeyBindingSource();
					break;
				case BindingSourceType.MouseBindingSource:
					bindingSource = new MouseBindingSource();
					break;
				case BindingSourceType.UnknownDeviceBindingSource:
					bindingSource = new UnknownDeviceBindingSource();
					break;
				default:
					throw new InControlException("Don't know how to load BindingSourceType: " + bindingSourceType.ToString());
				}
				bindingSource.Load(reader, dataFormatVersion);
				this.AddBinding(bindingSource);
				goto IL_81;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004110 File Offset: 0x00002310
		internal void Save(BinaryWriter writer)
		{
			this.RemoveOrphanedBindings();
			writer.Write(this.Name);
			int count = this.regularBindings.Count;
			writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				writer.Write((int)bindingSource.BindingSourceType);
				bindingSource.Save(writer);
			}
		}

		// Token: 0x040000E8 RID: 232
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040000E9 RID: 233
		[CompilerGenerated]
		private PlayerActionSet <Owner>k__BackingField;

		// Token: 0x040000EA RID: 234
		public BindingListenOptions ListenOptions;

		// Token: 0x040000EB RID: 235
		public BindingSourceType LastInputType;

		// Token: 0x040000EC RID: 236
		[CompilerGenerated]
		private Action<BindingSourceType> OnLastInputTypeChanged;

		// Token: 0x040000ED RID: 237
		public ulong LastInputTypeChangedTick;

		// Token: 0x040000EE RID: 238
		public InputDeviceClass LastDeviceClass;

		// Token: 0x040000EF RID: 239
		public InputDeviceStyle LastDeviceStyle;

		// Token: 0x040000F0 RID: 240
		[CompilerGenerated]
		private Action OnBindingsChanged;

		// Token: 0x040000F1 RID: 241
		[CompilerGenerated]
		private object <UserData>k__BackingField;

		// Token: 0x040000F2 RID: 242
		private readonly List<BindingSource> defaultBindings = new List<BindingSource>();

		// Token: 0x040000F3 RID: 243
		private readonly List<BindingSource> regularBindings = new List<BindingSource>();

		// Token: 0x040000F4 RID: 244
		private readonly List<BindingSource> visibleBindings = new List<BindingSource>();

		// Token: 0x040000F5 RID: 245
		private readonly ReadOnlyCollection<BindingSource> bindings;

		// Token: 0x040000F6 RID: 246
		private readonly ReadOnlyCollection<BindingSource> unfilteredBindings;

		// Token: 0x040000F7 RID: 247
		private readonly BindingSourceListener[] bindingSourceListeners = new BindingSourceListener[]
		{
			new DeviceBindingSourceListener(),
			new UnknownDeviceBindingSourceListener(),
			new KeyBindingSourceListener(),
			new MouseBindingSourceListener()
		};

		// Token: 0x040000F8 RID: 248
		private bool triggerBindingEnded;

		// Token: 0x040000F9 RID: 249
		private bool triggerBindingChanged;

		// Token: 0x040000FA RID: 250
		private InputDevice device;

		// Token: 0x040000FB RID: 251
		private InputDevice activeDevice;
	}
}
