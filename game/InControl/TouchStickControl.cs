using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000051 RID: 81
	public class TouchStickControl : TouchControl
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000D6ED File Offset: 0x0000B8ED
		public override void CreateControl()
		{
			this.ring.Create("Ring", base.transform, 1000);
			this.knob.Create("Knob", base.transform, 1001);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000D725 File Offset: 0x0000B925
		public override void DestroyControl()
		{
			this.ring.Delete();
			this.knob.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D758 File Offset: 0x0000B958
		public override void ConfigureControl()
		{
			this.resetPosition = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, true);
			base.transform.position = this.resetPosition;
			this.ring.Update(true);
			this.knob.Update(true);
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
			this.worldKnobRange = TouchManager.ConvertToWorld(this.knobRange, this.knob.SizeUnitType);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public override void DrawGizmos()
		{
			this.ring.DrawGizmos(this.RingPosition, Color.yellow);
			this.knob.DrawGizmos(this.KnobPosition, Color.yellow);
			Utility.DrawCircleGizmo(this.RingPosition, this.worldKnobRange, Color.red);
			Utility.DrawRectGizmo(this.worldActiveArea, Color.green);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D844 File Offset: 0x0000BA44
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
			else
			{
				this.ring.Update();
				this.knob.Update();
			}
			if (this.IsNotActive)
			{
				if (this.resetWhenDone && this.KnobPosition != this.resetPosition)
				{
					Vector3 b = this.KnobPosition - this.RingPosition;
					this.RingPosition = Vector3.MoveTowards(this.RingPosition, this.resetPosition, this.ringResetSpeed * Time.unscaledDeltaTime);
					this.KnobPosition = this.RingPosition + b;
				}
				if (this.KnobPosition != this.RingPosition)
				{
					this.KnobPosition = Vector3.MoveTowards(this.KnobPosition, this.RingPosition, this.knobResetSpeed * Time.unscaledDeltaTime);
				}
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000D921 File Offset: 0x0000BB21
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			base.SubmitAnalogValue(this.target, this.value, this.lowerDeadZone, this.upperDeadZone, updateTick, deltaTime);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D948 File Offset: 0x0000BB48
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000D958 File Offset: 0x0000BB58
		public override void TouchBegan(Touch touch)
		{
			if (this.IsActive)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			bool flag = this.worldActiveArea.Contains(this.beganPosition);
			bool flag2 = this.ring.Contains(this.beganPosition);
			if (this.snapToInitialTouch && (flag || flag2))
			{
				this.RingPosition = this.beganPosition;
				this.KnobPosition = this.beganPosition;
				this.currentTouch = touch;
			}
			else if (flag2)
			{
				this.KnobPosition = this.beganPosition;
				this.beganPosition = this.RingPosition;
				this.currentTouch = touch;
			}
			if (this.IsActive)
			{
				this.TouchMoved(touch);
				this.ring.State = true;
				this.knob.State = true;
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.movedPosition = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.lockToAxis == LockAxis.Horizontal && this.allowDraggingAxis == DragAxis.Horizontal)
			{
				this.movedPosition.y = this.beganPosition.y;
			}
			else if (this.lockToAxis == LockAxis.Vertical && this.allowDraggingAxis == DragAxis.Vertical)
			{
				this.movedPosition.x = this.beganPosition.x;
			}
			Vector3 vector = this.movedPosition - this.beganPosition;
			Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			if (this.allowDragging)
			{
				float num = magnitude - this.worldKnobRange;
				if (num < 0f)
				{
					num = 0f;
				}
				Vector3 b = num * normalized;
				if (this.allowDraggingAxis == DragAxis.Horizontal)
				{
					b.y = 0f;
				}
				else if (this.allowDraggingAxis == DragAxis.Vertical)
				{
					b.x = 0f;
				}
				this.beganPosition += b;
				this.RingPosition = this.beganPosition;
			}
			this.movedPosition = this.beganPosition + Mathf.Clamp(magnitude, 0f, this.worldKnobRange) * normalized;
			if (this.lockToAxis == LockAxis.Horizontal)
			{
				this.movedPosition.y = this.beganPosition.y;
			}
			else if (this.lockToAxis == LockAxis.Vertical)
			{
				this.movedPosition.x = this.beganPosition.x;
			}
			if (this.snapAngles != TouchControl.SnapAngles.None)
			{
				this.movedPosition = TouchControl.SnapTo(this.movedPosition - this.beganPosition, this.snapAngles) + this.beganPosition;
			}
			this.RingPosition = this.beganPosition;
			this.KnobPosition = this.movedPosition;
			this.value = (this.movedPosition - this.beganPosition) / this.worldKnobRange;
			this.value.x = this.inputCurve.Evaluate(Utility.Abs(this.value.x)) * Mathf.Sign(this.value.x);
			this.value.y = this.inputCurve.Evaluate(Utility.Abs(this.value.y)) * Mathf.Sign(this.value.y);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.value = Vector3.zero;
			float magnitude = (this.resetPosition - this.RingPosition).magnitude;
			this.ringResetSpeed = (Utility.IsZero(this.resetDuration) ? magnitude : (magnitude / this.resetDuration));
			float magnitude2 = (this.RingPosition - this.KnobPosition).magnitude;
			this.knobResetSpeed = (Utility.IsZero(this.resetDuration) ? this.knobRange : (magnitude2 / this.resetDuration));
			this.currentTouch = null;
			this.ring.State = false;
			this.knob.State = false;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000DD32 File Offset: 0x0000BF32
		public bool IsActive
		{
			get
			{
				return this.currentTouch != null;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		public bool IsNotActive
		{
			get
			{
				return this.currentTouch == null;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000DD48 File Offset: 0x0000BF48
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000DD6E File Offset: 0x0000BF6E
		public Vector3 RingPosition
		{
			get
			{
				if (!this.ring.Ready)
				{
					return base.transform.position;
				}
				return this.ring.Position;
			}
			set
			{
				if (this.ring.Ready)
				{
					this.ring.Position = value;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000DD89 File Offset: 0x0000BF89
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		public Vector3 KnobPosition
		{
			get
			{
				if (!this.knob.Ready)
				{
					return base.transform.position;
				}
				return this.knob.Position;
			}
			set
			{
				if (this.knob.Ready)
				{
					this.knob.Position = value;
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000DDCA File Offset: 0x0000BFCA
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000DDD2 File Offset: 0x0000BFD2
		public TouchControlAnchor Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				if (this.anchor != value)
				{
					this.anchor = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DDEB File Offset: 0x0000BFEB
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		public Vector2 Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				if (this.offset != value)
				{
					this.offset = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000DE11 File Offset: 0x0000C011
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000DE19 File Offset: 0x0000C019
		public TouchUnitType OffsetUnitType
		{
			get
			{
				return this.offsetUnitType;
			}
			set
			{
				if (this.offsetUnitType != value)
				{
					this.offsetUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000DE32 File Offset: 0x0000C032
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000DE3A File Offset: 0x0000C03A
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

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000DE58 File Offset: 0x0000C058
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000DE60 File Offset: 0x0000C060
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

		// Token: 0x060003E4 RID: 996 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public TouchStickControl()
		{
		}

		// Token: 0x04000360 RID: 864
		[Header("Position")]
		[SerializeField]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomLeft;

		// Token: 0x04000361 RID: 865
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x04000362 RID: 866
		[SerializeField]
		private Vector2 offset = new Vector2(20f, 20f);

		// Token: 0x04000363 RID: 867
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x04000364 RID: 868
		[SerializeField]
		private Rect activeArea = new Rect(0f, 0f, 50f, 100f);

		// Token: 0x04000365 RID: 869
		[Header("Options")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x04000366 RID: 870
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x04000367 RID: 871
		public LockAxis lockToAxis;

		// Token: 0x04000368 RID: 872
		[Range(0f, 1f)]
		public float lowerDeadZone = 0.1f;

		// Token: 0x04000369 RID: 873
		[Range(0f, 1f)]
		public float upperDeadZone = 0.9f;

		// Token: 0x0400036A RID: 874
		public AnimationCurve inputCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x0400036B RID: 875
		public bool allowDragging;

		// Token: 0x0400036C RID: 876
		public DragAxis allowDraggingAxis;

		// Token: 0x0400036D RID: 877
		public bool snapToInitialTouch = true;

		// Token: 0x0400036E RID: 878
		public bool resetWhenDone = true;

		// Token: 0x0400036F RID: 879
		public float resetDuration = 0.1f;

		// Token: 0x04000370 RID: 880
		[Header("Sprites")]
		public TouchSprite ring = new TouchSprite(20f);

		// Token: 0x04000371 RID: 881
		public TouchSprite knob = new TouchSprite(10f);

		// Token: 0x04000372 RID: 882
		public float knobRange = 7.5f;

		// Token: 0x04000373 RID: 883
		private Vector3 resetPosition;

		// Token: 0x04000374 RID: 884
		private Vector3 beganPosition;

		// Token: 0x04000375 RID: 885
		private Vector3 movedPosition;

		// Token: 0x04000376 RID: 886
		private float ringResetSpeed;

		// Token: 0x04000377 RID: 887
		private float knobResetSpeed;

		// Token: 0x04000378 RID: 888
		private Rect worldActiveArea;

		// Token: 0x04000379 RID: 889
		private float worldKnobRange;

		// Token: 0x0400037A RID: 890
		private Vector3 value;

		// Token: 0x0400037B RID: 891
		private Touch currentTouch;

		// Token: 0x0400037C RID: 892
		private bool dirty;
	}
}
