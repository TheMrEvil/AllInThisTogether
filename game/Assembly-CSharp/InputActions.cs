using System;
using InControl;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class InputActions : PlayerActionSet
{
	// Token: 0x060005A9 RID: 1449 RVA: 0x00028D74 File Offset: 0x00026F74
	public InputActions()
	{
		this.Left = base.CreatePlayerAction("Move Left");
		this.Left.ListenOptions = InputActions.GetListenOptions(true);
		this.Right = base.CreatePlayerAction("Move Right");
		this.Right.ListenOptions = InputActions.GetListenOptions(true);
		this.Up = base.CreatePlayerAction("Move Up");
		this.Up.ListenOptions = InputActions.GetListenOptions(true);
		this.Down = base.CreatePlayerAction("Move Down");
		this.Down.ListenOptions = InputActions.GetListenOptions(true);
		this.Move = base.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
		this.CLeft = base.CreatePlayerAction("Camera Left");
		this.CRight = base.CreatePlayerAction("Camera Right");
		this.CUp = base.CreatePlayerAction("Camera Up");
		this.CDown = base.CreatePlayerAction("Camera Down");
		this.Camera = base.CreateTwoAxisPlayerAction(this.CLeft, this.CRight, this.CDown, this.CUp);
		this.Ping = base.CreatePlayerAction("Ping");
		this.Ping.ListenOptions = InputActions.GetListenOptions(true);
		this.Emote = base.CreatePlayerAction("Emote");
		this.Emote.ListenOptions = InputActions.GetListenOptions(true);
		this.MovementAbility = base.CreatePlayerAction("Boost");
		this.MovementAbility.ListenOptions = InputActions.GetListenOptions(true);
		this.Jump = base.CreatePlayerAction("Jump");
		this.Jump.ListenOptions = InputActions.GetListenOptions(true);
		this.PrimaryUse = base.CreatePlayerAction("Primary Action");
		this.PrimaryUse.ListenOptions = InputActions.GetListenOptions(true);
		this.SecondaryUse = base.CreatePlayerAction("Secondary Action");
		this.SecondaryUse.ListenOptions = InputActions.GetListenOptions(true);
		this.UtilityUse = base.CreatePlayerAction("Utility Action");
		this.UtilityUse.ListenOptions = InputActions.GetListenOptions(true);
		this.Interact = base.CreatePlayerAction("Interact");
		this.Interact.ListenOptions = InputActions.GetListenOptions(true);
		this.Augments = base.CreatePlayerAction("Augments");
		this.Augments.ListenOptions = InputActions.GetListenOptions(true);
		this.Pause = base.CreatePlayerAction("Pause");
		this.DPLeft = base.CreatePlayerAction("DPad Left");
		this.DPRight = base.CreatePlayerAction("DPad Right");
		this.DPUp = base.CreatePlayerAction("DPad Up");
		this.DPDown = base.CreatePlayerAction("DPad Down");
		this.DPad = base.CreateTwoAxisPlayerAction(this.DPLeft, this.DPRight, this.DPDown, this.DPUp);
		this.SpectateUp = base.CreatePlayerAction("Spectate Up");
		this.SpectateDown = base.CreatePlayerAction("Spectate Down");
		this.SpectateMultiplyPos = base.CreatePlayerAction("Multiply_Positive");
		this.SpectateMultiplyNeg = base.CreatePlayerAction("Multiply_Negative");
		this.SpectateMultiplier = base.CreateOneAxisPlayerAction(this.SpectateMultiplyPos, this.SpectateMultiplyNeg);
		this.SpectateSpeedUp = base.CreatePlayerAction("Spectate Speed Incr");
		this.SpectateSpeedDown = base.CreatePlayerAction("Spectate Speed Decr");
		this.SpectateLockHeight = base.CreatePlayerAction("Spectate Height Lock");
		this.SpectateLockRot = base.CreatePlayerAction("Spectate Rot Lock");
		this.SpectateToggleRecord = base.CreatePlayerAction("Spectate Toggle Recording");
		this.SpectatePlayRecord = base.CreatePlayerAction("Spectate Watch Recorded");
		this.SpectateXButton = base.CreatePlayerAction("Spectate X Button");
		this.SpectateYButton = base.CreatePlayerAction("Spectate Y Button");
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0002912C File Offset: 0x0002732C
	public static InputActions CreateWithDefaultBindings()
	{
		InputActions inputActions = new InputActions();
		inputActions.Up.AddDefaultBinding(new Key[]
		{
			Key.W
		});
		inputActions.Down.AddDefaultBinding(new Key[]
		{
			Key.S
		});
		inputActions.Left.AddDefaultBinding(new Key[]
		{
			Key.A
		});
		inputActions.Right.AddDefaultBinding(new Key[]
		{
			Key.D
		});
		inputActions.CUp.AddDefaultBinding(Mouse.PositiveY);
		inputActions.CDown.AddDefaultBinding(Mouse.NegativeY);
		inputActions.CLeft.AddDefaultBinding(Mouse.NegativeX);
		inputActions.CRight.AddDefaultBinding(Mouse.PositiveX);
		inputActions.Jump.AddDefaultBinding(new Key[]
		{
			Key.Space
		});
		inputActions.MovementAbility.AddDefaultBinding(new Key[]
		{
			Key.LeftShift
		});
		inputActions.PrimaryUse.AddDefaultBinding(Mouse.LeftButton);
		inputActions.SecondaryUse.AddDefaultBinding(Mouse.RightButton);
		inputActions.UtilityUse.AddDefaultBinding(new Key[]
		{
			Key.Q
		});
		inputActions.Interact.AddDefaultBinding(new Key[]
		{
			Key.E
		});
		inputActions.Augments.AddDefaultBinding(new Key[]
		{
			Key.Tab
		});
		inputActions.Ping.AddDefaultBinding(Mouse.MiddleButton);
		inputActions.Emote.AddDefaultBinding(new Key[]
		{
			Key.T
		});
		inputActions.Pause.AddDefaultBinding(new Key[]
		{
			Key.Escape
		});
		inputActions.SpectateUp.AddDefaultBinding(new Key[]
		{
			Key.Q
		});
		inputActions.SpectateDown.AddDefaultBinding(new Key[]
		{
			Key.E
		});
		inputActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
		inputActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
		inputActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
		inputActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
		inputActions.CLeft.AddDefaultBinding(InputControlType.RightStickLeft);
		inputActions.CRight.AddDefaultBinding(InputControlType.RightStickRight);
		inputActions.CUp.AddDefaultBinding(InputControlType.RightStickUp);
		inputActions.CDown.AddDefaultBinding(InputControlType.RightStickDown);
		inputActions.Jump.AddDefaultBinding(InputControlType.Action1);
		inputActions.MovementAbility.AddDefaultBinding(InputControlType.LeftBumper);
		inputActions.MovementAbility.AddDefaultBinding(InputControlType.Action2);
		inputActions.PrimaryUse.AddDefaultBinding(InputControlType.RightTrigger);
		inputActions.SecondaryUse.AddDefaultBinding(InputControlType.LeftTrigger);
		inputActions.SecondaryUse.AddDefaultBinding(InputControlType.RightBumper);
		inputActions.UtilityUse.AddDefaultBinding(InputControlType.Action4);
		inputActions.Interact.AddDefaultBinding(InputControlType.Action3);
		inputActions.Ping.AddDefaultBinding(InputControlType.RightStickButton);
		inputActions.Emote.AddDefaultBinding(InputControlType.DPadLeft);
		inputActions.Augments.AddDefaultBinding(InputControlType.TouchPadButton);
		inputActions.Augments.AddDefaultBinding(InputControlType.LeftCommand);
		inputActions.Augments.AddDefaultBinding(InputControlType.Minus);
		inputActions.Pause.AddDefaultBinding(InputControlType.Menu);
		inputActions.Pause.AddDefaultBinding(InputControlType.RightCommand);
		inputActions.DPLeft.AddDefaultBinding(InputControlType.DPadLeft);
		inputActions.DPRight.AddDefaultBinding(InputControlType.DPadRight);
		inputActions.DPUp.AddDefaultBinding(InputControlType.DPadUp);
		inputActions.DPDown.AddDefaultBinding(InputControlType.DPadDown);
		inputActions.SpectateUp.AddDefaultBinding(InputControlType.RightTrigger);
		inputActions.SpectateDown.AddDefaultBinding(InputControlType.LeftTrigger);
		inputActions.SpectateMultiplyPos.AddDefaultBinding(InputControlType.LeftTrigger);
		inputActions.SpectateSpeedUp.AddDefaultBinding(InputControlType.DPadUp);
		inputActions.SpectateSpeedDown.AddDefaultBinding(InputControlType.DPadDown);
		inputActions.SpectateLockHeight.AddDefaultBinding(InputControlType.LeftStickButton);
		inputActions.SpectateLockRot.AddDefaultBinding(InputControlType.RightStickButton);
		inputActions.SpectateToggleRecord.AddDefaultBinding(InputControlType.LeftBumper);
		inputActions.SpectatePlayRecord.AddDefaultBinding(InputControlType.RightBumper);
		inputActions.SpectateXButton.AddDefaultBinding(InputControlType.Action3);
		inputActions.SpectateYButton.AddDefaultBinding(InputControlType.Action4);
		inputActions.ListenOptions = InputActions.GetListenOptions(false);
		if (PlayerPrefs.HasKey("InputBindings"))
		{
			string @string = PlayerPrefs.GetString("InputBindings");
			inputActions.Load(@string);
		}
		return inputActions;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000294CC File Offset: 0x000276CC
	public static InputActions.InputAction GetInputFromString(string inline)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(inline);
		if (num <= 1297420860U)
		{
			if (num <= 375255177U)
			{
				if (num <= 292226511U)
				{
					if (num != 89534040U)
					{
						if (num == 292226511U)
						{
							if (inline == "interact")
							{
								return InputActions.InputAction.Interact;
							}
						}
					}
					else if (inline == "movement")
					{
						return InputActions.InputAction.Ability_Movement;
					}
				}
				else if (num != 308562864U)
				{
					if (num == 375255177U)
					{
						if (inline == "ping")
						{
							return InputActions.InputAction.Ping;
						}
					}
				}
				else if (inline == "spender")
				{
					return InputActions.InputAction.Ability_Secondary;
				}
			}
			else if (num <= 494016593U)
			{
				if (num != 451128030U)
				{
					if (num == 494016593U)
					{
						if (inline == "augments")
						{
							return InputActions.InputAction.Game_Info;
						}
					}
				}
				else if (inline == "move_forward")
				{
					return InputActions.InputAction.Move_Forward;
				}
			}
			else if (num != 621169566U)
			{
				if (num != 1266453457U)
				{
					if (num == 1297420860U)
					{
						if (inline == "move_left")
						{
							return InputActions.InputAction.Move_Left;
						}
					}
				}
				else if (inline == "secondary")
				{
					return InputActions.InputAction.Ability_Secondary;
				}
			}
			else if (inline == "move_back")
			{
				return InputActions.InputAction.Move_Back;
			}
		}
		else if (num <= 1860974018U)
		{
			if (num <= 1523588291U)
			{
				if (num != 1512988633U)
				{
					if (num == 1523588291U)
					{
						if (inline == "emote")
						{
							return InputActions.InputAction.Emote;
						}
					}
				}
				else if (inline == "primary")
				{
					return InputActions.InputAction.Ability_Primary;
				}
			}
			else if (num != 1605069165U)
			{
				if (num == 1860974018U)
				{
					if (inline == "generator")
					{
						return InputActions.InputAction.Ability_Primary;
					}
				}
			}
			else if (inline == "signature")
			{
				return InputActions.InputAction.Ability_Signature;
			}
		}
		else if (num <= 3217471839U)
		{
			if (num != 2805947405U)
			{
				if (num == 3217471839U)
				{
					if (inline == "utility")
					{
						return InputActions.InputAction.Ability_Signature;
					}
				}
			}
			else if (inline == "jump")
			{
				return InputActions.InputAction.Jump;
			}
		}
		else if (num != 3713949822U)
		{
			if (num != 3761822729U)
			{
				if (num == 3945505414U)
				{
					if (inline == "ultimate")
					{
						return InputActions.InputAction.Ability_Signature;
					}
				}
			}
			else if (inline == "move_right")
			{
				return InputActions.InputAction.Move_Right;
			}
		}
		else if (inline == "core")
		{
			return InputActions.InputAction.Ability_Signature;
		}
		return InputActions.InputAction.Game_Pause;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000297C4 File Offset: 0x000279C4
	public PlayerAction GetAction(InputActions.InputAction action)
	{
		switch (action)
		{
		case InputActions.InputAction.Move_Forward:
			return this.Up;
		case InputActions.InputAction.Move_Back:
			return this.Down;
		case InputActions.InputAction.Move_Left:
			return this.Left;
		case InputActions.InputAction.Move_Right:
			return this.Right;
		case InputActions.InputAction.Move:
		case InputActions.InputAction.Camera:
			break;
		case InputActions.InputAction.Camera_Up:
			return this.CUp;
		case InputActions.InputAction.Camera_Down:
			return this.CDown;
		case InputActions.InputAction.Camera_Left:
			return this.CLeft;
		case InputActions.InputAction.Camera_Right:
			return this.CRight;
		case InputActions.InputAction.Jump:
			return this.Jump;
		case InputActions.InputAction.Interact:
			return this.Interact;
		case InputActions.InputAction.Ping:
			return this.Ping;
		case InputActions.InputAction.Emote:
			return this.Emote;
		default:
			switch (action)
			{
			case InputActions.InputAction.Ability_Primary:
				return this.PrimaryUse;
			case InputActions.InputAction.Ability_Secondary:
				return this.SecondaryUse;
			case InputActions.InputAction.Ability_Movement:
				return this.MovementAbility;
			case InputActions.InputAction.Ability_Signature:
				return this.UtilityUse;
			case InputActions.InputAction.Game_Pause:
				return this.UtilityUse;
			case InputActions.InputAction.Game_Info:
				return this.Augments;
			case InputActions.InputAction.Game_ToggleHUD:
				return null;
			default:
				switch (action)
				{
				case InputActions.InputAction.UI_Confirm:
					return global::InputManager.UIAct.UIPrimary;
				case InputActions.InputAction.UI_Back:
					return global::InputManager.UIAct.UIBack;
				case InputActions.InputAction.UI_Tab_Forward:
					return global::InputManager.UIAct.Tab_Next;
				case InputActions.InputAction.UI_Tab_Back:
					return global::InputManager.UIAct.Tab_Prev;
				case InputActions.InputAction.UI_Page_Forward:
					return global::InputManager.UIAct.Page_Next;
				case InputActions.InputAction.UI_Page_Back:
					return global::InputManager.UIAct.Page_Prev;
				case InputActions.InputAction.UI_Secondary:
					return global::InputManager.UIAct.UISecondary;
				case InputActions.InputAction.UI_Tertiary:
					return global::InputManager.UIAct.UITertiary;
				}
				break;
			}
			break;
		}
		return null;
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x000299A6 File Offset: 0x00027BA6
	public void NewBinding(InputActions.InputAction action)
	{
		PlayerAction action2 = this.GetAction(action);
		if (action2 == null)
		{
			return;
		}
		action2.ListenForBinding();
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000299B9 File Offset: 0x00027BB9
	private static bool OnBindingFound(PlayerAction action, BindingSource binding)
	{
		if (binding == new KeyBindingSource(new Key[]
		{
			Key.Escape
		}))
		{
			action.StopListeningForBinding();
			Debug.Log("Binding Canceled by Escape");
			return false;
		}
		return true;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000299E6 File Offset: 0x00027BE6
	private static void OnBindingAdded(PlayerAction action, BindingSource binding)
	{
		Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
		Action onBindingChanged = global::InputManager.OnBindingChanged;
		if (onBindingChanged == null)
		{
			return;
		}
		onBindingChanged();
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00029A17 File Offset: 0x00027C17
	private static void OnBindingRejected(PlayerAction action, BindingSource binding, BindingSourceRejectionType reason)
	{
		Debug.Log("Binding rejected... " + reason.ToString());
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00029A38 File Offset: 0x00027C38
	private static BindingListenOptions GetListenOptions(bool includeMouse = false)
	{
		return new BindingListenOptions
		{
			IncludeMouseButtons = true,
			IncludeUnknownControllers = true,
			IncludeModifiersAsFirstClassKeys = true,
			IncludeControllers = true,
			UnsetDuplicateBindingsOnSet = false,
			AllowDuplicateBindingsPerSet = true,
			MaxAllowedBindingsPerType = 3U,
			OnBindingFound = new Func<PlayerAction, BindingSource, bool>(InputActions.OnBindingFound),
			OnBindingAdded = new Action<PlayerAction, BindingSource>(InputActions.OnBindingAdded),
			OnBindingRejected = new Action<PlayerAction, BindingSource, BindingSourceRejectionType>(InputActions.OnBindingRejected)
		};
	}

	// Token: 0x04000477 RID: 1143
	public const string SAVE_KEY = "InputBindings";

	// Token: 0x04000478 RID: 1144
	public readonly PlayerAction Left;

	// Token: 0x04000479 RID: 1145
	public readonly PlayerAction Right;

	// Token: 0x0400047A RID: 1146
	public readonly PlayerAction Up;

	// Token: 0x0400047B RID: 1147
	public readonly PlayerAction Down;

	// Token: 0x0400047C RID: 1148
	public readonly PlayerTwoAxisAction Move;

	// Token: 0x0400047D RID: 1149
	public readonly PlayerAction CLeft;

	// Token: 0x0400047E RID: 1150
	public readonly PlayerAction CRight;

	// Token: 0x0400047F RID: 1151
	public readonly PlayerAction CUp;

	// Token: 0x04000480 RID: 1152
	public readonly PlayerAction CDown;

	// Token: 0x04000481 RID: 1153
	public readonly PlayerTwoAxisAction Camera;

	// Token: 0x04000482 RID: 1154
	public readonly PlayerAction MovementAbility;

	// Token: 0x04000483 RID: 1155
	public readonly PlayerAction Jump;

	// Token: 0x04000484 RID: 1156
	public readonly PlayerAction PrimaryUse;

	// Token: 0x04000485 RID: 1157
	public readonly PlayerAction SecondaryUse;

	// Token: 0x04000486 RID: 1158
	public readonly PlayerAction UtilityUse;

	// Token: 0x04000487 RID: 1159
	public readonly PlayerAction Interact;

	// Token: 0x04000488 RID: 1160
	public readonly PlayerAction Ping;

	// Token: 0x04000489 RID: 1161
	public readonly PlayerAction Emote;

	// Token: 0x0400048A RID: 1162
	public readonly PlayerAction Augments;

	// Token: 0x0400048B RID: 1163
	public readonly PlayerAction Pause;

	// Token: 0x0400048C RID: 1164
	public readonly PlayerAction DPLeft;

	// Token: 0x0400048D RID: 1165
	public readonly PlayerAction DPRight;

	// Token: 0x0400048E RID: 1166
	public readonly PlayerAction DPUp;

	// Token: 0x0400048F RID: 1167
	public readonly PlayerAction DPDown;

	// Token: 0x04000490 RID: 1168
	public readonly PlayerTwoAxisAction DPad;

	// Token: 0x04000491 RID: 1169
	public readonly PlayerAction SpectateUp;

	// Token: 0x04000492 RID: 1170
	public readonly PlayerAction SpectateDown;

	// Token: 0x04000493 RID: 1171
	public readonly PlayerAction SpectateMultiplyPos;

	// Token: 0x04000494 RID: 1172
	public readonly PlayerAction SpectateMultiplyNeg;

	// Token: 0x04000495 RID: 1173
	public readonly PlayerOneAxisAction SpectateMultiplier;

	// Token: 0x04000496 RID: 1174
	public readonly PlayerAction SpectateSpeedUp;

	// Token: 0x04000497 RID: 1175
	public readonly PlayerAction SpectateSpeedDown;

	// Token: 0x04000498 RID: 1176
	public readonly PlayerAction SpectateLockHeight;

	// Token: 0x04000499 RID: 1177
	public readonly PlayerAction SpectateLockRot;

	// Token: 0x0400049A RID: 1178
	public readonly PlayerAction SpectateToggleRecord;

	// Token: 0x0400049B RID: 1179
	public readonly PlayerAction SpectatePlayRecord;

	// Token: 0x0400049C RID: 1180
	public readonly PlayerAction SpectateXButton;

	// Token: 0x0400049D RID: 1181
	public readonly PlayerAction SpectateYButton;

	// Token: 0x0200049E RID: 1182
	public enum InputAction
	{
		// Token: 0x04002394 RID: 9108
		Move_Forward,
		// Token: 0x04002395 RID: 9109
		Move_Back,
		// Token: 0x04002396 RID: 9110
		Move_Left,
		// Token: 0x04002397 RID: 9111
		Move_Right,
		// Token: 0x04002398 RID: 9112
		Move,
		// Token: 0x04002399 RID: 9113
		Camera_Up,
		// Token: 0x0400239A RID: 9114
		Camera_Down,
		// Token: 0x0400239B RID: 9115
		Camera_Left,
		// Token: 0x0400239C RID: 9116
		Camera_Right,
		// Token: 0x0400239D RID: 9117
		Camera,
		// Token: 0x0400239E RID: 9118
		Jump,
		// Token: 0x0400239F RID: 9119
		Interact,
		// Token: 0x040023A0 RID: 9120
		Ping,
		// Token: 0x040023A1 RID: 9121
		Emote,
		// Token: 0x040023A2 RID: 9122
		Ability_Primary = 32,
		// Token: 0x040023A3 RID: 9123
		Ability_Secondary,
		// Token: 0x040023A4 RID: 9124
		Ability_Movement,
		// Token: 0x040023A5 RID: 9125
		Ability_Signature,
		// Token: 0x040023A6 RID: 9126
		Game_Pause,
		// Token: 0x040023A7 RID: 9127
		Game_Info,
		// Token: 0x040023A8 RID: 9128
		Game_ToggleHUD,
		// Token: 0x040023A9 RID: 9129
		UI_Up = 128,
		// Token: 0x040023AA RID: 9130
		UI_Down,
		// Token: 0x040023AB RID: 9131
		UI_Left,
		// Token: 0x040023AC RID: 9132
		UI_Right,
		// Token: 0x040023AD RID: 9133
		UI_Confirm,
		// Token: 0x040023AE RID: 9134
		UI_Back,
		// Token: 0x040023AF RID: 9135
		UI_Tab_Forward,
		// Token: 0x040023B0 RID: 9136
		UI_Tab_Back,
		// Token: 0x040023B1 RID: 9137
		UI_Page_Forward,
		// Token: 0x040023B2 RID: 9138
		UI_Page_Back,
		// Token: 0x040023B3 RID: 9139
		UI_Secondary,
		// Token: 0x040023B4 RID: 9140
		UI_Tertiary
	}
}
