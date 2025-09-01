using System;
using InControl;

// Token: 0x02000086 RID: 134
public class UIActions : PlayerActionSet
{
	// Token: 0x060005B2 RID: 1458 RVA: 0x00029AB4 File Offset: 0x00027CB4
	public UIActions()
	{
		this.UIBack = base.CreatePlayerAction("UI Back");
		this.UIPrimary = base.CreatePlayerAction("UI Primary");
		this.UISecondary = base.CreatePlayerAction("UI Secondary");
		this.UITertiary = base.CreatePlayerAction("UI Tertiary");
		this.UILeftStickButton = base.CreatePlayerAction("UI Left Stick Button");
		this.UIRightStickButton = base.CreatePlayerAction("UI Right Stick Button");
		this.Page_Prev = base.CreatePlayerAction("UI Prev Page 2");
		this.Page_Next = base.CreatePlayerAction("UI Next Page 2");
		this.Tab_Prev = base.CreatePlayerAction("UI Prev Tab");
		this.Tab_Next = base.CreatePlayerAction("UI Next Tab");
		this.UILeft = base.CreatePlayerAction("UI Left");
		this.UIRight = base.CreatePlayerAction("UI Right");
		this.UIUp = base.CreatePlayerAction("UI Up");
		this.UIDown = base.CreatePlayerAction("UI Down");
		this.UIMove = base.CreateTwoAxisPlayerAction(this.UILeft, this.UIRight, this.UIDown, this.UIUp);
		this.Left = base.CreatePlayerAction("UI Analog Left");
		this.Right = base.CreatePlayerAction("UI Analog Right");
		this.Up = base.CreatePlayerAction("UI Analog Up");
		this.Down = base.CreatePlayerAction("UI Analog Down");
		this.AnalogMove = base.CreateTwoAxisPlayerAction(this.Left, this.Right, this.Down, this.Up);
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00029C44 File Offset: 0x00027E44
	public static UIActions CreateWithDefaultBindings()
	{
		UIActions uiactions = new UIActions();
		uiactions.UIBack.AddDefaultBinding(new Key[]
		{
			Key.Escape
		});
		uiactions.UIPrimary.AddDefaultBinding(Mouse.LeftButton);
		uiactions.UISecondary.AddDefaultBinding(Mouse.RightButton);
		uiactions.UITertiary.AddDefaultBinding(new Key[]
		{
			Key.F
		});
		uiactions.Tab_Prev.AddDefaultBinding(new Key[]
		{
			Key.Q
		});
		uiactions.Tab_Next.AddDefaultBinding(new Key[]
		{
			Key.E
		});
		uiactions.UIBack.AddDefaultBinding(InputControlType.Action2);
		uiactions.UIPrimary.AddDefaultBinding(InputControlType.Action1);
		uiactions.UISecondary.AddDefaultBinding(InputControlType.Action3);
		uiactions.UITertiary.AddDefaultBinding(InputControlType.Action4);
		uiactions.UILeftStickButton.AddDefaultBinding(InputControlType.LeftStickButton);
		uiactions.UIRightStickButton.AddDefaultBinding(InputControlType.RightStickButton);
		uiactions.Tab_Prev.AddDefaultBinding(InputControlType.LeftBumper);
		uiactions.Tab_Next.AddDefaultBinding(InputControlType.RightBumper);
		uiactions.Page_Next.AddDefaultBinding(InputControlType.RightTrigger);
		uiactions.Page_Prev.AddDefaultBinding(InputControlType.LeftTrigger);
		uiactions.UILeft.AddDefaultBinding(InputControlType.DPadLeft);
		uiactions.UIRight.AddDefaultBinding(InputControlType.DPadRight);
		uiactions.UIUp.AddDefaultBinding(InputControlType.DPadUp);
		uiactions.UIDown.AddDefaultBinding(InputControlType.DPadDown);
		uiactions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
		uiactions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
		uiactions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
		uiactions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
		return uiactions;
	}

	// Token: 0x0400049E RID: 1182
	public readonly PlayerAction UIBack;

	// Token: 0x0400049F RID: 1183
	public readonly PlayerAction UIPrimary;

	// Token: 0x040004A0 RID: 1184
	public readonly PlayerAction UISecondary;

	// Token: 0x040004A1 RID: 1185
	public readonly PlayerAction UITertiary;

	// Token: 0x040004A2 RID: 1186
	public readonly PlayerAction UILeftStickButton;

	// Token: 0x040004A3 RID: 1187
	public readonly PlayerAction UIRightStickButton;

	// Token: 0x040004A4 RID: 1188
	public readonly PlayerAction Page_Next;

	// Token: 0x040004A5 RID: 1189
	public readonly PlayerAction Page_Prev;

	// Token: 0x040004A6 RID: 1190
	public readonly PlayerAction Tab_Next;

	// Token: 0x040004A7 RID: 1191
	public readonly PlayerAction Tab_Prev;

	// Token: 0x040004A8 RID: 1192
	public readonly PlayerAction UILeft;

	// Token: 0x040004A9 RID: 1193
	public readonly PlayerAction UIRight;

	// Token: 0x040004AA RID: 1194
	public readonly PlayerAction UIUp;

	// Token: 0x040004AB RID: 1195
	public readonly PlayerAction UIDown;

	// Token: 0x040004AC RID: 1196
	public readonly PlayerTwoAxisAction UIMove;

	// Token: 0x040004AD RID: 1197
	public readonly PlayerAction Left;

	// Token: 0x040004AE RID: 1198
	public readonly PlayerAction Right;

	// Token: 0x040004AF RID: 1199
	public readonly PlayerAction Up;

	// Token: 0x040004B0 RID: 1200
	public readonly PlayerAction Down;

	// Token: 0x040004B1 RID: 1201
	public readonly PlayerTwoAxisAction AnalogMove;

	// Token: 0x040004B2 RID: 1202
	public readonly PlayerAction Augments;

	// Token: 0x040004B3 RID: 1203
	public readonly PlayerAction Pause;
}
