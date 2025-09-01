using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200002D RID: 45
	public class OneAxisInputControl : IInputControl
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005D9F File Offset: 0x00003F9F
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00005DA7 File Offset: 0x00003FA7
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

		// Token: 0x06000196 RID: 406 RVA: 0x00005DB0 File Offset: 0x00003FB0
		private void PrepareForUpdate(ulong updateTick)
		{
			if (this.isNullControl)
			{
				return;
			}
			if (updateTick < this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated with an earlier tick.");
			}
			if (this.pendingCommit && updateTick != this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated for a new tick until pending tick is committed.");
			}
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005E24 File Offset: 0x00004024
		public bool UpdateWithState(bool state, ulong updateTick, float deltaTime)
		{
			if (this.isNullControl)
			{
				return false;
			}
			this.PrepareForUpdate(updateTick);
			this.nextState.Set(state || this.nextState.State);
			return state;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005E54 File Offset: 0x00004054
		public bool UpdateWithValue(float value, ulong updateTick, float deltaTime)
		{
			if (this.isNullControl)
			{
				return false;
			}
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				if (!this.Raw)
				{
					value = Utility.ApplyDeadZone(value, this.lowerDeadZone, this.upperDeadZone);
				}
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005EC8 File Offset: 0x000040C8
		internal bool UpdateWithRawValue(float value, ulong updateTick, float deltaTime)
		{
			if (this.isNullControl)
			{
				return false;
			}
			this.Raw = true;
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005F28 File Offset: 0x00004128
		internal void SetValue(float value, ulong updateTick)
		{
			if (this.isNullControl)
			{
				return;
			}
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
			this.nextState.RawValue = value;
			this.nextState.Set(value, this.StateThreshold);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005F8A File Offset: 0x0000418A
		public void ClearInputState()
		{
			this.lastState.Reset();
			this.thisState.Reset();
			this.nextState.Reset();
			this.wasRepeated = false;
			this.clearInputState = true;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005FBC File Offset: 0x000041BC
		public void Commit()
		{
			if (this.isNullControl)
			{
				return;
			}
			this.pendingCommit = false;
			this.thisState = this.nextState;
			if (this.clearInputState)
			{
				this.lastState = this.nextState;
				this.UpdateTick = this.pendingTick;
				this.clearInputState = false;
				return;
			}
			bool state = this.lastState.State;
			bool state2 = this.thisState.State;
			this.wasRepeated = false;
			if (state && !state2)
			{
				this.nextRepeatTime = 0f;
			}
			else if (state2)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				if (!state)
				{
					this.nextRepeatTime = realtimeSinceStartup + this.FirstRepeatDelay;
				}
				else if (realtimeSinceStartup >= this.nextRepeatTime)
				{
					this.wasRepeated = true;
					this.nextRepeatTime = realtimeSinceStartup + this.RepeatDelay;
				}
			}
			if (this.thisState != this.lastState)
			{
				this.UpdateTick = this.pendingTick;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006098 File Offset: 0x00004298
		public void CommitWithState(bool state, ulong updateTick, float deltaTime)
		{
			this.UpdateWithState(state, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000060AA File Offset: 0x000042AA
		public void CommitWithValue(float value, ulong updateTick, float deltaTime)
		{
			this.UpdateWithValue(value, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000060BC File Offset: 0x000042BC
		internal void CommitWithSides(InputControl negativeSide, InputControl positiveSide, ulong updateTick, float deltaTime)
		{
			this.LowerDeadZone = Mathf.Max(negativeSide.LowerDeadZone, positiveSide.LowerDeadZone);
			this.UpperDeadZone = Mathf.Min(negativeSide.UpperDeadZone, positiveSide.UpperDeadZone);
			this.Raw = (negativeSide.Raw || positiveSide.Raw);
			float value = Utility.ValueFromSides(negativeSide.RawValue, positiveSide.RawValue);
			this.CommitWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000612A File Offset: 0x0000432A
		public bool State
		{
			get
			{
				return this.EnabledInHierarchy && this.thisState.State;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006141 File Offset: 0x00004341
		public bool LastState
		{
			get
			{
				return this.EnabledInHierarchy && this.lastState.State;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006158 File Offset: 0x00004358
		public float Value
		{
			get
			{
				if (!this.EnabledInHierarchy)
				{
					return 0f;
				}
				return this.thisState.Value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006173 File Offset: 0x00004373
		public float LastValue
		{
			get
			{
				if (!this.EnabledInHierarchy)
				{
					return 0f;
				}
				return this.lastState.Value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000618E File Offset: 0x0000438E
		public float RawValue
		{
			get
			{
				if (!this.EnabledInHierarchy)
				{
					return 0f;
				}
				return this.thisState.RawValue;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000061A9 File Offset: 0x000043A9
		internal float NextRawValue
		{
			get
			{
				if (!this.EnabledInHierarchy)
				{
					return 0f;
				}
				return this.nextState.RawValue;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000061C4 File Offset: 0x000043C4
		internal bool HasInput
		{
			get
			{
				return this.EnabledInHierarchy && Utility.IsNotZero(this.thisState.Value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000061E0 File Offset: 0x000043E0
		public bool HasChanged
		{
			get
			{
				return this.EnabledInHierarchy && this.thisState != this.lastState;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000061FD File Offset: 0x000043FD
		public bool IsPressed
		{
			get
			{
				return this.EnabledInHierarchy && this.thisState.State;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006214 File Offset: 0x00004414
		public bool WasPressed
		{
			get
			{
				return this.EnabledInHierarchy && this.thisState && !this.lastState;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000623B File Offset: 0x0000443B
		public bool WasReleased
		{
			get
			{
				return this.EnabledInHierarchy && !this.thisState && this.lastState;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000625F File Offset: 0x0000445F
		public bool WasRepeated
		{
			get
			{
				return this.EnabledInHierarchy && this.wasRepeated;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006271 File Offset: 0x00004471
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006279 File Offset: 0x00004479
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006287 File Offset: 0x00004487
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000628F File Offset: 0x0000448F
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000629D File Offset: 0x0000449D
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000062A5 File Offset: 0x000044A5
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000062B3 File Offset: 0x000044B3
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000062BB File Offset: 0x000044BB
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000062C9 File Offset: 0x000044C9
		public bool IsNullControl
		{
			get
			{
				return this.isNullControl;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000062D1 File Offset: 0x000044D1
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000062D9 File Offset: 0x000044D9
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000062E2 File Offset: 0x000044E2
		public bool EnabledInHierarchy
		{
			get
			{
				return this.enabled && this.ownerEnabled;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000062F4 File Offset: 0x000044F4
		public static implicit operator bool(OneAxisInputControl instance)
		{
			return instance.State;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000062FC File Offset: 0x000044FC
		public static implicit operator float(OneAxisInputControl instance)
		{
			return instance.Value;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006304 File Offset: 0x00004504
		public OneAxisInputControl()
		{
		}

		// Token: 0x04000209 RID: 521
		[CompilerGenerated]
		private ulong <UpdateTick>k__BackingField;

		// Token: 0x0400020A RID: 522
		private float sensitivity = 1f;

		// Token: 0x0400020B RID: 523
		private float lowerDeadZone;

		// Token: 0x0400020C RID: 524
		private float upperDeadZone = 1f;

		// Token: 0x0400020D RID: 525
		private float stateThreshold;

		// Token: 0x0400020E RID: 526
		protected bool isNullControl;

		// Token: 0x0400020F RID: 527
		public float FirstRepeatDelay = 0.8f;

		// Token: 0x04000210 RID: 528
		public float RepeatDelay = 0.1f;

		// Token: 0x04000211 RID: 529
		public bool Raw;

		// Token: 0x04000212 RID: 530
		private bool enabled = true;

		// Token: 0x04000213 RID: 531
		protected bool ownerEnabled = true;

		// Token: 0x04000214 RID: 532
		private ulong pendingTick;

		// Token: 0x04000215 RID: 533
		private bool pendingCommit;

		// Token: 0x04000216 RID: 534
		private float nextRepeatTime;

		// Token: 0x04000217 RID: 535
		private bool wasRepeated;

		// Token: 0x04000218 RID: 536
		private bool clearInputState;

		// Token: 0x04000219 RID: 537
		private InputControlState lastState;

		// Token: 0x0400021A RID: 538
		private InputControlState nextState;

		// Token: 0x0400021B RID: 539
		private InputControlState thisState;
	}
}
