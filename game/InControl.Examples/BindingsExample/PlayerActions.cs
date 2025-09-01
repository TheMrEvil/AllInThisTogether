using System;
using System.Runtime.CompilerServices;
using InControl;
using UnityEngine;

namespace BindingsExample
{
	// Token: 0x02000009 RID: 9
	public class PlayerActions : PlayerActionSet
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002D38 File Offset: 0x00000F38
		public PlayerActions()
		{
			this.Fire = base.CreatePlayerAction("Fire");
			this.Jump = base.CreatePlayerAction("Jump");
			this.Left = base.CreatePlayerAction("Move Left");
			this.Right = base.CreatePlayerAction("Move Right");
			this.Up = base.CreatePlayerAction("Move Up");
			this.Down = base.CreatePlayerAction("Move Down");
			this.Move = base.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public static PlayerActions CreateWithDefaultBindings()
		{
			PlayerActions playerActions = new PlayerActions();
			playerActions.Fire.AddDefaultBinding(new Key[]
			{
				Key.A
			});
			playerActions.Fire.AddDefaultBinding(InputControlType.Action1);
			playerActions.Jump.AddDefaultBinding(new Key[]
			{
				Key.Space
			});
			playerActions.Jump.AddDefaultBinding(InputControlType.Action3);
			playerActions.Up.AddDefaultBinding(new Key[]
			{
				Key.UpArrow
			});
			playerActions.Down.AddDefaultBinding(new Key[]
			{
				Key.DownArrow
			});
			playerActions.Left.AddDefaultBinding(new Key[]
			{
				Key.LeftArrow
			});
			playerActions.Right.AddDefaultBinding(new Key[]
			{
				Key.RightArrow
			});
			playerActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
			playerActions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
			playerActions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
			playerActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.Right.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.Up.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.Down.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.Up.AddDefaultBinding(Mouse.PositiveY);
			playerActions.Down.AddDefaultBinding(Mouse.NegativeY);
			playerActions.Left.AddDefaultBinding(Mouse.NegativeX);
			playerActions.Right.AddDefaultBinding(Mouse.PositiveX);
			playerActions.ListenOptions.IncludeUnknownControllers = true;
			playerActions.ListenOptions.MaxAllowedBindings = 4U;
			playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;
			playerActions.ListenOptions.OnBindingFound = delegate(PlayerAction action, BindingSource binding)
			{
				if (binding == new KeyBindingSource(new Key[]
				{
					Key.Escape
				}))
				{
					action.StopListeningForBinding();
					return false;
				}
				return true;
			};
			BindingListenOptions listenOptions = playerActions.ListenOptions;
			listenOptions.OnBindingAdded = (Action<PlayerAction, BindingSource>)Delegate.Combine(listenOptions.OnBindingAdded, new Action<PlayerAction, BindingSource>(delegate(PlayerAction action, BindingSource binding)
			{
				Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
			}));
			BindingListenOptions listenOptions2 = playerActions.ListenOptions;
			listenOptions2.OnBindingRejected = (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)Delegate.Combine(listenOptions2.OnBindingRejected, new Action<PlayerAction, BindingSource, BindingSourceRejectionType>(delegate(PlayerAction action, BindingSource binding, BindingSourceRejectionType reason)
			{
				Debug.Log("Binding rejected... " + reason.ToString());
			}));
			return playerActions;
		}

		// Token: 0x04000015 RID: 21
		public readonly PlayerAction Fire;

		// Token: 0x04000016 RID: 22
		public readonly PlayerAction Jump;

		// Token: 0x04000017 RID: 23
		public readonly PlayerAction Left;

		// Token: 0x04000018 RID: 24
		public readonly PlayerAction Right;

		// Token: 0x04000019 RID: 25
		public readonly PlayerAction Up;

		// Token: 0x0400001A RID: 26
		public readonly PlayerAction Down;

		// Token: 0x0400001B RID: 27
		public readonly PlayerTwoAxisAction Move;

		// Token: 0x0200000B RID: 11
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000028 RID: 40 RVA: 0x0000309B File Offset: 0x0000129B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000030A7 File Offset: 0x000012A7
			public <>c()
			{
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000030AF File Offset: 0x000012AF
			internal bool <CreateWithDefaultBindings>b__8_0(PlayerAction action, BindingSource binding)
			{
				if (binding == new KeyBindingSource(new Key[]
				{
					Key.Escape
				}))
				{
					action.StopListeningForBinding();
					return false;
				}
				return true;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x000030D2 File Offset: 0x000012D2
			internal void <CreateWithDefaultBindings>b__8_1(PlayerAction action, BindingSource binding)
			{
				Debug.Log("Binding added... " + binding.DeviceName + ": " + binding.Name);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000030F4 File Offset: 0x000012F4
			internal void <CreateWithDefaultBindings>b__8_2(PlayerAction action, BindingSource binding, BindingSourceRejectionType reason)
			{
				Debug.Log("Binding rejected... " + reason.ToString());
			}

			// Token: 0x0400001C RID: 28
			public static readonly PlayerActions.<>c <>9 = new PlayerActions.<>c();

			// Token: 0x0400001D RID: 29
			public static Func<PlayerAction, BindingSource, bool> <>9__8_0;

			// Token: 0x0400001E RID: 30
			public static Action<PlayerAction, BindingSource> <>9__8_1;

			// Token: 0x0400001F RID: 31
			public static Action<PlayerAction, BindingSource, BindingSourceRejectionType> <>9__8_2;
		}
	}
}
