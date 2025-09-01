using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200004E RID: 78
	public class TouchButtonControl : TouchControl
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000D39F File Offset: 0x0000B59F
		public override void CreateControl()
		{
			this.button.Create("Button", base.transform, 1000);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public override void DestroyControl()
		{
			this.button.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		public override void ConfigureControl()
		{
			base.transform.position = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, this.lockAspectRatio);
			this.button.Update(true);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000D41B File Offset: 0x0000B61B
		public override void DrawGizmos()
		{
			this.button.DrawGizmos(this.ButtonPosition, Color.yellow);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D433 File Offset: 0x0000B633
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
				return;
			}
			this.button.Update();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D458 File Offset: 0x0000B658
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			if (this.pressureSensitive)
			{
				float num = 0f;
				if (this.currentTouch == null)
				{
					if (this.allowSlideToggle)
					{
						int touchCount = TouchManager.TouchCount;
						for (int i = 0; i < touchCount; i++)
						{
							Touch touch = TouchManager.GetTouch(i);
							if (this.button.Contains(touch))
							{
								num = Utility.Max(num, touch.NormalizedPressure);
							}
						}
					}
				}
				else
				{
					num = this.currentTouch.NormalizedPressure;
				}
				this.ButtonState = (num > 0f);
				base.SubmitButtonValue(this.target, num, updateTick, deltaTime);
				return;
			}
			if (this.currentTouch == null && this.allowSlideToggle)
			{
				this.ButtonState = false;
				int touchCount2 = TouchManager.TouchCount;
				for (int j = 0; j < touchCount2; j++)
				{
					this.ButtonState = (this.ButtonState || this.button.Contains(TouchManager.GetTouch(j)));
				}
			}
			base.SubmitButtonState(this.target, this.ButtonState, updateTick, deltaTime);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000D549 File Offset: 0x0000B749
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitButton(this.target);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000D557 File Offset: 0x0000B757
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			if (this.button.Contains(touch))
			{
				this.ButtonState = true;
				this.currentTouch = touch;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D57E File Offset: 0x0000B77E
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			if (this.toggleOnLeave && !this.button.Contains(touch))
			{
				this.ButtonState = false;
				this.currentTouch = null;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.ButtonState = false;
			this.currentTouch = null;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		private bool ButtonState
		{
			get
			{
				return this.buttonState;
			}
			set
			{
				if (this.buttonState != value)
				{
					this.buttonState = value;
					this.button.State = value;
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000D5EE File Offset: 0x0000B7EE
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000D614 File Offset: 0x0000B814
		public Vector3 ButtonPosition
		{
			get
			{
				if (!this.button.Ready)
				{
					return base.transform.position;
				}
				return this.button.Position;
			}
			set
			{
				if (this.button.Ready)
				{
					this.button.Position = value;
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000D62F File Offset: 0x0000B82F
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000D637 File Offset: 0x0000B837
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000D650 File Offset: 0x0000B850
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000D658 File Offset: 0x0000B858
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000D676 File Offset: 0x0000B876
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000D67E File Offset: 0x0000B87E
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

		// Token: 0x060003C9 RID: 969 RVA: 0x0000D698 File Offset: 0x0000B898
		public TouchButtonControl()
		{
		}

		// Token: 0x0400034C RID: 844
		[Header("Position")]
		[SerializeField]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomRight;

		// Token: 0x0400034D RID: 845
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x0400034E RID: 846
		[SerializeField]
		private Vector2 offset = new Vector2(-10f, 10f);

		// Token: 0x0400034F RID: 847
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x04000350 RID: 848
		[Header("Options")]
		public TouchControl.ButtonTarget target = TouchControl.ButtonTarget.Action1;

		// Token: 0x04000351 RID: 849
		public bool allowSlideToggle = true;

		// Token: 0x04000352 RID: 850
		public bool toggleOnLeave;

		// Token: 0x04000353 RID: 851
		public bool pressureSensitive;

		// Token: 0x04000354 RID: 852
		[Header("Sprites")]
		public TouchSprite button = new TouchSprite(15f);

		// Token: 0x04000355 RID: 853
		private bool buttonState;

		// Token: 0x04000356 RID: 854
		private Touch currentTouch;

		// Token: 0x04000357 RID: 855
		private bool dirty;
	}
}
