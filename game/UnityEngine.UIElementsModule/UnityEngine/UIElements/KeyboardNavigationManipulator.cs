using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000047 RID: 71
	public class KeyboardNavigationManipulator : Manipulator
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x000082AC File Offset: 0x000064AC
		public KeyboardNavigationManipulator(Action<KeyboardNavigationOperation, EventBase> action)
		{
			this.m_Action = action;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000082C0 File Offset: 0x000064C0
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008334 File Offset: 0x00006534
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000083A8 File Offset: 0x000065A8
		internal void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.target.panel;
			bool flag = panel != null && panel.contextType == ContextType.Editor;
			if (flag)
			{
				this.OnEditorKeyDown(evt);
			}
			else
			{
				this.OnRuntimeKeyDown(evt);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000083E8 File Offset: 0x000065E8
		private void OnRuntimeKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			this.Invoke(KeyboardNavigationManipulator.<OnRuntimeKeyDown>g__GetOperation|5_0(ref CS$<>8__locals1), CS$<>8__locals1.evt);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008414 File Offset: 0x00006614
		private void OnEditorKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			this.Invoke(KeyboardNavigationManipulator.<OnEditorKeyDown>g__GetOperation|6_0(ref CS$<>8__locals1), CS$<>8__locals1.evt);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000843F File Offset: 0x0000663F
		private void OnNavigationCancel(NavigationCancelEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Cancel, evt);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000844B File Offset: 0x0000664B
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Submit, evt);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008458 File Offset: 0x00006658
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			NavigationMoveEvent.Direction direction = evt.direction;
			NavigationMoveEvent.Direction direction2 = direction;
			if (direction2 != NavigationMoveEvent.Direction.Up)
			{
				if (direction2 == NavigationMoveEvent.Direction.Down)
				{
					this.Invoke(KeyboardNavigationOperation.Next, evt);
				}
			}
			else
			{
				this.Invoke(KeyboardNavigationOperation.Previous, evt);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008494 File Offset: 0x00006694
		private void Invoke(KeyboardNavigationOperation operation, EventBase evt)
		{
			bool flag = operation == KeyboardNavigationOperation.None;
			if (!flag)
			{
				Action<KeyboardNavigationOperation, EventBase> action = this.m_Action;
				if (action != null)
				{
					action(operation, evt);
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000084C0 File Offset: 0x000066C0
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnRuntimeKeyDown>g__GetOperation|5_0(ref KeyboardNavigationManipulator.<>c__DisplayClass5_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				}
			}
			else if (A_0.evt.actionKey)
			{
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000852C File Offset: 0x0000672C
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnEditorKeyDown>g__GetOperation|6_0(ref KeyboardNavigationManipulator.<>c__DisplayClass6_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 <= KeyCode.Escape)
			{
				if (keyCode2 != KeyCode.Return)
				{
					if (keyCode2 != KeyCode.Escape)
					{
						goto IL_97;
					}
					return KeyboardNavigationOperation.Cancel;
				}
			}
			else if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.RightArrow:
				case KeyCode.LeftArrow:
				case KeyCode.Insert:
					goto IL_97;
				case KeyCode.UpArrow:
					return KeyboardNavigationOperation.Previous;
				case KeyCode.DownArrow:
					return KeyboardNavigationOperation.Next;
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				default:
					goto IL_97;
				}
			}
			else
			{
				if (!A_0.evt.actionKey)
				{
					goto IL_97;
				}
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.Submit;
			IL_97:
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x040000CD RID: 205
		private readonly Action<KeyboardNavigationOperation, EventBase> m_Action;

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass5_0
		{
			// Token: 0x040000CE RID: 206
			public KeyDownEvent evt;
		}

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass6_0
		{
			// Token: 0x040000CF RID: 207
			public KeyDownEvent evt;
		}
	}
}
