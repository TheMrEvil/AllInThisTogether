using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000054 RID: 84
	public class Touch
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000E68A File Offset: 0x0000C88A
		internal Touch()
		{
			this.fingerId = -1;
			this.phase = TouchPhase.Ended;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		internal void Reset()
		{
			this.fingerId = -1;
			this.mouseButton = 0;
			this.phase = TouchPhase.Ended;
			this.tapCount = 0;
			this.position = Vector2.zero;
			this.startPosition = Vector2.zero;
			this.deltaPosition = Vector2.zero;
			this.lastPosition = Vector2.zero;
			this.deltaTime = 0f;
			this.updateTick = 0UL;
			this.type = TouchType.Direct;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.maximumPossiblePressure = 1f;
			this.pressure = 0f;
			this.radius = 0f;
			this.radiusVariance = 0f;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000E751 File Offset: 0x0000C951
		[Obsolete("normalizedPressure is deprecated, please use NormalizedPressure instead.")]
		public float normalizedPressure
		{
			get
			{
				return Mathf.Clamp(this.pressure / this.maximumPossiblePressure, 0.001f, 1f);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000E76F File Offset: 0x0000C96F
		public float NormalizedPressure
		{
			get
			{
				return Mathf.Clamp(this.pressure / this.maximumPossiblePressure, 0.001f, 1f);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000E78D File Offset: 0x0000C98D
		public bool IsMouse
		{
			get
			{
				return this.type == TouchType.Mouse;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E798 File Offset: 0x0000C998
		internal void SetWithTouchData(Touch touch, ulong updateTick, float deltaTime)
		{
			this.phase = touch.phase;
			this.tapCount = touch.tapCount;
			this.mouseButton = 0;
			this.altitudeAngle = touch.altitudeAngle;
			this.azimuthAngle = touch.azimuthAngle;
			this.maximumPossiblePressure = touch.maximumPossiblePressure;
			this.pressure = touch.pressure;
			this.radius = touch.radius;
			this.radiusVariance = touch.radiusVariance;
			Vector2 vector = touch.position;
			vector.x = Mathf.Clamp(vector.x, 0f, (float)Screen.width);
			vector.y = Mathf.Clamp(vector.y, 0f, (float)Screen.height);
			if (this.phase == TouchPhase.Began)
			{
				this.startPosition = vector;
				this.deltaPosition = Vector2.zero;
				this.lastPosition = vector;
				this.position = vector;
			}
			else
			{
				if (this.phase == TouchPhase.Stationary)
				{
					this.phase = TouchPhase.Moved;
				}
				this.deltaPosition = vector - this.lastPosition;
				this.lastPosition = this.position;
				this.position = vector;
			}
			this.deltaTime = deltaTime;
			this.updateTick = updateTick;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		internal bool SetWithMouseData(int button, ulong updateTick, float deltaTime)
		{
			if (Input.touchCount > 0)
			{
				return false;
			}
			if (button < 0 || button > 2)
			{
				return false;
			}
			Vector2 vector = InputManager.MouseProvider.GetPosition();
			Vector2 a = new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
			Mouse control = Mouse.LeftButton + button;
			if (InputManager.MouseProvider.GetButtonWasPressed(control))
			{
				this.phase = TouchPhase.Began;
				this.pressure = 1f;
				this.maximumPossiblePressure = 1f;
				this.tapCount = 1;
				this.type = TouchType.Mouse;
				this.mouseButton = button;
				this.startPosition = a;
				this.deltaPosition = Vector2.zero;
				this.lastPosition = a;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (InputManager.MouseProvider.GetButtonWasReleased(control))
			{
				this.phase = TouchPhase.Ended;
				this.pressure = 0f;
				this.maximumPossiblePressure = 1f;
				this.tapCount = 1;
				this.type = TouchType.Mouse;
				this.mouseButton = button;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (InputManager.MouseProvider.GetButtonIsPressed(control))
			{
				this.phase = TouchPhase.Moved;
				this.pressure = 1f;
				this.maximumPossiblePressure = 1f;
				this.tapCount = 1;
				this.type = TouchType.Mouse;
				this.mouseButton = button;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			return false;
		}

		// Token: 0x040003A1 RID: 929
		public const int FingerID_None = -1;

		// Token: 0x040003A2 RID: 930
		public const int FingerID_Mouse = -2;

		// Token: 0x040003A3 RID: 931
		public int fingerId;

		// Token: 0x040003A4 RID: 932
		public int mouseButton;

		// Token: 0x040003A5 RID: 933
		public TouchPhase phase;

		// Token: 0x040003A6 RID: 934
		public int tapCount;

		// Token: 0x040003A7 RID: 935
		public Vector2 position;

		// Token: 0x040003A8 RID: 936
		public Vector2 startPosition;

		// Token: 0x040003A9 RID: 937
		public Vector2 deltaPosition;

		// Token: 0x040003AA RID: 938
		public Vector2 lastPosition;

		// Token: 0x040003AB RID: 939
		public float deltaTime;

		// Token: 0x040003AC RID: 940
		public ulong updateTick;

		// Token: 0x040003AD RID: 941
		public TouchType type;

		// Token: 0x040003AE RID: 942
		public float altitudeAngle;

		// Token: 0x040003AF RID: 943
		public float azimuthAngle;

		// Token: 0x040003B0 RID: 944
		public float maximumPossiblePressure;

		// Token: 0x040003B1 RID: 945
		public float pressure;

		// Token: 0x040003B2 RID: 946
		public float radius;

		// Token: 0x040003B3 RID: 947
		public float radiusVariance;
	}
}
