using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000052 RID: 82
	public class TouchSwipeControl : TouchControl
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000DF4A File Offset: 0x0000C14A
		public override void CreateControl()
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000DF4C File Offset: 0x0000C14C
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000DF69 File Offset: 0x0000C169
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000DF82 File Offset: 0x0000C182
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000DF94 File Offset: 0x0000C194
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector3 v = TouchControl.SnapTo(this.currentVector, this.snapAngles);
			base.SubmitAnalogValue(this.target, v, 0f, 1f, updateTick, deltaTime);
			base.SubmitButtonState(this.upTarget, this.fireButtonTarget && this.nextButtonTarget == this.upTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.downTarget, this.fireButtonTarget && this.nextButtonTarget == this.downTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.leftTarget, this.fireButtonTarget && this.nextButtonTarget == this.leftTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.rightTarget, this.fireButtonTarget && this.nextButtonTarget == this.rightTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.tapTarget, this.fireButtonTarget && this.nextButtonTarget == this.tapTarget, updateTick, deltaTime);
			if (this.fireButtonTarget && this.nextButtonTarget != TouchControl.ButtonTarget.None)
			{
				this.fireButtonTarget = !this.oneSwipePerTouch;
				this.lastButtonTarget = this.nextButtonTarget;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
			base.CommitButton(this.upTarget);
			base.CommitButton(this.downTarget);
			base.CommitButton(this.leftTarget);
			base.CommitButton(this.rightTarget);
			base.CommitButton(this.tapTarget);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E13C File Offset: 0x0000C33C
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(this.beganPosition))
			{
				this.lastPosition = this.beganPosition;
				this.currentTouch = touch;
				this.currentVector = Vector2.zero;
				this.currentVectorIsSet = false;
				this.fireButtonTarget = true;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			Vector3 a = TouchManager.ScreenToWorldPoint(touch.position);
			Vector3 vector = a - this.lastPosition;
			if (vector.magnitude >= this.sensitivity)
			{
				this.lastPosition = a;
				if (!this.oneSwipePerTouch || !this.currentVectorIsSet)
				{
					this.currentVector = vector.normalized;
					this.currentVectorIsSet = true;
				}
				if (this.fireButtonTarget)
				{
					TouchControl.ButtonTarget buttonTargetForVector = this.GetButtonTargetForVector(this.currentVector);
					if (buttonTargetForVector != this.lastButtonTarget)
					{
						this.nextButtonTarget = buttonTargetForVector;
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E24C File Offset: 0x0000C44C
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.currentTouch = null;
			this.currentVector = Vector2.zero;
			this.currentVectorIsSet = false;
			Vector3 b = TouchManager.ScreenToWorldPoint(touch.position);
			if ((this.beganPosition - b).magnitude < this.sensitivity)
			{
				this.fireButtonTarget = true;
				this.nextButtonTarget = this.tapTarget;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
				return;
			}
			this.fireButtonTarget = false;
			this.nextButtonTarget = TouchControl.ButtonTarget.None;
			this.lastButtonTarget = TouchControl.ButtonTarget.None;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		private TouchControl.ButtonTarget GetButtonTargetForVector(Vector2 vector)
		{
			Vector2 lhs = TouchControl.SnapTo(vector, TouchControl.SnapAngles.Four);
			if (lhs == Vector2.up)
			{
				return this.upTarget;
			}
			if (lhs == Vector2.right)
			{
				return this.rightTarget;
			}
			if (lhs == -Vector2.up)
			{
				return this.downTarget;
			}
			if (lhs == -Vector2.right)
			{
				return this.leftTarget;
			}
			return TouchControl.ButtonTarget.None;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000E351 File Offset: 0x0000C551
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000E359 File Offset: 0x0000C559
		public Rect ActiveArea
		{
			get
			{
				return this.activeArea;
			}
			set
			{
				if (this.activeArea != value)
				{
					this.activeArea = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E377 File Offset: 0x0000C577
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E37F File Offset: 0x0000C57F
		public TouchUnitType AreaUnitType
		{
			get
			{
				return this.areaUnitType;
			}
			set
			{
				if (this.areaUnitType != value)
				{
					this.areaUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000E398 File Offset: 0x0000C598
		public TouchSwipeControl()
		{
		}

		// Token: 0x0400037D RID: 893
		[Header("Position")]
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x0400037E RID: 894
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x0400037F RID: 895
		[Header("Options")]
		[Range(0f, 1f)]
		public float sensitivity = 0.1f;

		// Token: 0x04000380 RID: 896
		public bool oneSwipePerTouch;

		// Token: 0x04000381 RID: 897
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target;

		// Token: 0x04000382 RID: 898
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x04000383 RID: 899
		[Header("Button Targets")]
		public TouchControl.ButtonTarget upTarget;

		// Token: 0x04000384 RID: 900
		public TouchControl.ButtonTarget downTarget;

		// Token: 0x04000385 RID: 901
		public TouchControl.ButtonTarget leftTarget;

		// Token: 0x04000386 RID: 902
		public TouchControl.ButtonTarget rightTarget;

		// Token: 0x04000387 RID: 903
		public TouchControl.ButtonTarget tapTarget;

		// Token: 0x04000388 RID: 904
		private Rect worldActiveArea;

		// Token: 0x04000389 RID: 905
		private Vector3 currentVector;

		// Token: 0x0400038A RID: 906
		private bool currentVectorIsSet;

		// Token: 0x0400038B RID: 907
		private Vector3 beganPosition;

		// Token: 0x0400038C RID: 908
		private Vector3 lastPosition;

		// Token: 0x0400038D RID: 909
		private Touch currentTouch;

		// Token: 0x0400038E RID: 910
		private bool fireButtonTarget;

		// Token: 0x0400038F RID: 911
		private TouchControl.ButtonTarget nextButtonTarget;

		// Token: 0x04000390 RID: 912
		private TouchControl.ButtonTarget lastButtonTarget;

		// Token: 0x04000391 RID: 913
		private bool dirty;
	}
}
