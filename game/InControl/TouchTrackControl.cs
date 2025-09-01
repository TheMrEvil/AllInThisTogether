using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000053 RID: 83
	public class TouchTrackControl : TouchControl
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000E3CA File Offset: 0x0000C5CA
		public override void CreateControl()
		{
			this.ConfigureControl();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E3D2 File Offset: 0x0000C5D2
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000E3EF File Offset: 0x0000C5EF
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E408 File Offset: 0x0000C608
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000E41A File Offset: 0x0000C61A
		private void OnValidate()
		{
			if (this.maxTapDuration < 0f)
			{
				this.maxTapDuration = 0f;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000E434 File Offset: 0x0000C634
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000E44C File Offset: 0x0000C64C
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector3 a = this.thisPosition - this.lastPosition;
			base.SubmitRawAnalogValue(this.target, a * this.scale, updateTick, deltaTime);
			this.lastPosition = this.thisPosition;
			base.SubmitButtonState(this.tapTarget, this.fireButtonTarget, updateTick, deltaTime);
			this.fireButtonTarget = false;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
			base.CommitButton(this.tapTarget);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(this.beganPosition))
			{
				this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
				this.lastPosition = this.thisPosition;
				this.currentTouch = touch;
				this.beganTime = Time.realtimeSinceStartup;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E53F File Offset: 0x0000C73F
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E568 File Offset: 0x0000C768
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			Vector3 vector = TouchManager.ScreenToWorldPoint(touch.position) - this.beganPosition;
			float num = Time.realtimeSinceStartup - this.beganTime;
			if (vector.magnitude <= this.maxTapMovement && num <= this.maxTapDuration && this.tapTarget != TouchControl.ButtonTarget.None)
			{
				this.fireButtonTarget = true;
			}
			this.thisPosition = Vector3.zero;
			this.lastPosition = Vector3.zero;
			this.currentTouch = null;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000E5E7 File Offset: 0x0000C7E7
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000E5EF File Offset: 0x0000C7EF
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000E60D File Offset: 0x0000C80D
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000E615 File Offset: 0x0000C815
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

		// Token: 0x06000404 RID: 1028 RVA: 0x0000E630 File Offset: 0x0000C830
		public TouchTrackControl()
		{
		}

		// Token: 0x04000392 RID: 914
		[Header("Dimensions")]
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x04000393 RID: 915
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x04000394 RID: 916
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x04000395 RID: 917
		public float scale = 1f;

		// Token: 0x04000396 RID: 918
		[Header("Button Target")]
		public TouchControl.ButtonTarget tapTarget;

		// Token: 0x04000397 RID: 919
		public float maxTapDuration = 0.5f;

		// Token: 0x04000398 RID: 920
		public float maxTapMovement = 1f;

		// Token: 0x04000399 RID: 921
		private Rect worldActiveArea;

		// Token: 0x0400039A RID: 922
		private Vector3 lastPosition;

		// Token: 0x0400039B RID: 923
		private Vector3 thisPosition;

		// Token: 0x0400039C RID: 924
		private Touch currentTouch;

		// Token: 0x0400039D RID: 925
		private bool dirty;

		// Token: 0x0400039E RID: 926
		private bool fireButtonTarget;

		// Token: 0x0400039F RID: 927
		private float beganTime;

		// Token: 0x040003A0 RID: 928
		private Vector3 beganPosition;
	}
}
