using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200002E RID: 46
	public class TwoAxisInputControl : IInputControl
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006351 File Offset: 0x00004551
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006359 File Offset: 0x00004559
		public float X
		{
			[CompilerGenerated]
			get
			{
				return this.<X>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<X>k__BackingField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006362 File Offset: 0x00004562
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000636A File Offset: 0x0000456A
		public float Y
		{
			[CompilerGenerated]
			get
			{
				return this.<Y>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Y>k__BackingField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006373 File Offset: 0x00004573
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000637B File Offset: 0x0000457B
		public OneAxisInputControl Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Left>k__BackingField = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006384 File Offset: 0x00004584
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000638C File Offset: 0x0000458C
		public OneAxisInputControl Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Right>k__BackingField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006395 File Offset: 0x00004595
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000639D File Offset: 0x0000459D
		public OneAxisInputControl Up
		{
			[CompilerGenerated]
			get
			{
				return this.<Up>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Up>k__BackingField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000063A6 File Offset: 0x000045A6
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000063AE File Offset: 0x000045AE
		public OneAxisInputControl Down
		{
			[CompilerGenerated]
			get
			{
				return this.<Down>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Down>k__BackingField = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000063B7 File Offset: 0x000045B7
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000063BF File Offset: 0x000045BF
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

		// Token: 0x060001C9 RID: 457 RVA: 0x000063C8 File Offset: 0x000045C8
		public TwoAxisInputControl()
		{
			this.Left = new OneAxisInputControl();
			this.Right = new OneAxisInputControl();
			this.Up = new OneAxisInputControl();
			this.Down = new OneAxisInputControl();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006430 File Offset: 0x00004630
		public void ClearInputState()
		{
			this.Left.ClearInputState();
			this.Right.ClearInputState();
			this.Up.ClearInputState();
			this.Down.ClearInputState();
			this.lastState = false;
			this.lastValue = Vector2.zero;
			this.thisState = false;
			this.thisValue = Vector2.zero;
			this.X = 0f;
			this.Y = 0f;
			this.clearInputState = true;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000064AA File Offset: 0x000046AA
		public void Filter(TwoAxisInputControl twoAxisInputControl, float deltaTime)
		{
			this.UpdateWithAxes(twoAxisInputControl.X, twoAxisInputControl.Y, InputManager.CurrentTick, deltaTime);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000064C4 File Offset: 0x000046C4
		internal void UpdateWithAxes(float x, float y, ulong updateTick, float deltaTime)
		{
			this.lastState = this.thisState;
			this.lastValue = this.thisValue;
			this.thisValue = (this.Raw ? new Vector2(x, y) : this.DeadZoneFunc(x, y, this.LowerDeadZone, this.UpperDeadZone));
			this.X = this.thisValue.x;
			this.Y = this.thisValue.y;
			this.Left.CommitWithValue(Mathf.Max(0f, -this.X), updateTick, deltaTime);
			this.Right.CommitWithValue(Mathf.Max(0f, this.X), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.Up.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
			}
			else
			{
				this.Up.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
			}
			this.thisState = (this.Up.State || this.Down.State || this.Left.State || this.Right.State);
			if (this.clearInputState)
			{
				this.lastState = this.thisState;
				this.lastValue = this.thisValue;
				this.clearInputState = false;
				this.HasChanged = false;
				return;
			}
			if (this.thisValue != this.lastValue)
			{
				this.UpdateTick = updateTick;
				this.HasChanged = true;
				return;
			}
			this.HasChanged = false;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000668E File Offset: 0x0000488E
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006698 File Offset: 0x00004898
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = Mathf.Clamp01(value);
				this.Left.Sensitivity = this.sensitivity;
				this.Right.Sensitivity = this.sensitivity;
				this.Up.Sensitivity = this.sensitivity;
				this.Down.Sensitivity = this.sensitivity;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000066F5 File Offset: 0x000048F5
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00006700 File Offset: 0x00004900
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = Mathf.Clamp01(value);
				this.Left.StateThreshold = this.stateThreshold;
				this.Right.StateThreshold = this.stateThreshold;
				this.Up.StateThreshold = this.stateThreshold;
				this.Down.StateThreshold = this.stateThreshold;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000675D File Offset: 0x0000495D
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00006768 File Offset: 0x00004968
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
				this.Left.LowerDeadZone = this.lowerDeadZone;
				this.Right.LowerDeadZone = this.lowerDeadZone;
				this.Up.LowerDeadZone = this.lowerDeadZone;
				this.Down.LowerDeadZone = this.lowerDeadZone;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000067C5 File Offset: 0x000049C5
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000067D0 File Offset: 0x000049D0
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
				this.Left.UpperDeadZone = this.upperDeadZone;
				this.Right.UpperDeadZone = this.upperDeadZone;
				this.Up.UpperDeadZone = this.upperDeadZone;
				this.Down.UpperDeadZone = this.upperDeadZone;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000682D File Offset: 0x00004A2D
		public bool State
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006835 File Offset: 0x00004A35
		public bool LastState
		{
			get
			{
				return this.lastState;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000683D File Offset: 0x00004A3D
		public Vector2 Value
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006845 File Offset: 0x00004A45
		public Vector2 LastValue
		{
			get
			{
				return this.lastValue;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000684D File Offset: 0x00004A4D
		public Vector2 Vector
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006855 File Offset: 0x00004A55
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000685D File Offset: 0x00004A5D
		public bool HasChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<HasChanged>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<HasChanged>k__BackingField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006866 File Offset: 0x00004A66
		public bool IsPressed
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000686E File Offset: 0x00004A6E
		public bool WasPressed
		{
			get
			{
				return this.thisState && !this.lastState;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006883 File Offset: 0x00004A83
		public bool WasReleased
		{
			get
			{
				return !this.thisState && this.lastState;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006895 File Offset: 0x00004A95
		public float Angle
		{
			get
			{
				return Utility.VectorToAngle(this.thisValue);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000068A2 File Offset: 0x00004AA2
		public static implicit operator bool(TwoAxisInputControl instance)
		{
			return instance.thisState;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000068AA File Offset: 0x00004AAA
		public static implicit operator Vector2(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000068B2 File Offset: 0x00004AB2
		public static implicit operator Vector3(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000068BF File Offset: 0x00004ABF
		// Note: this type is marked as 'beforefieldinit'.
		static TwoAxisInputControl()
		{
		}

		// Token: 0x0400021C RID: 540
		public static readonly TwoAxisInputControl Null = new TwoAxisInputControl();

		// Token: 0x0400021D RID: 541
		[CompilerGenerated]
		private float <X>k__BackingField;

		// Token: 0x0400021E RID: 542
		[CompilerGenerated]
		private float <Y>k__BackingField;

		// Token: 0x0400021F RID: 543
		[CompilerGenerated]
		private OneAxisInputControl <Left>k__BackingField;

		// Token: 0x04000220 RID: 544
		[CompilerGenerated]
		private OneAxisInputControl <Right>k__BackingField;

		// Token: 0x04000221 RID: 545
		[CompilerGenerated]
		private OneAxisInputControl <Up>k__BackingField;

		// Token: 0x04000222 RID: 546
		[CompilerGenerated]
		private OneAxisInputControl <Down>k__BackingField;

		// Token: 0x04000223 RID: 547
		[CompilerGenerated]
		private ulong <UpdateTick>k__BackingField;

		// Token: 0x04000224 RID: 548
		public DeadZoneFunc DeadZoneFunc = new DeadZoneFunc(DeadZone.Circular);

		// Token: 0x04000225 RID: 549
		private float sensitivity = 1f;

		// Token: 0x04000226 RID: 550
		private float lowerDeadZone;

		// Token: 0x04000227 RID: 551
		private float upperDeadZone = 1f;

		// Token: 0x04000228 RID: 552
		private float stateThreshold;

		// Token: 0x04000229 RID: 553
		public bool Raw;

		// Token: 0x0400022A RID: 554
		private bool thisState;

		// Token: 0x0400022B RID: 555
		private bool lastState;

		// Token: 0x0400022C RID: 556
		private Vector2 thisValue;

		// Token: 0x0400022D RID: 557
		private Vector2 lastValue;

		// Token: 0x0400022E RID: 558
		private bool clearInputState;

		// Token: 0x0400022F RID: 559
		[CompilerGenerated]
		private bool <HasChanged>k__BackingField;
	}
}
