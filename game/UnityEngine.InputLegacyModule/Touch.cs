using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public struct Touch
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
			set
			{
				this.m_FingerId = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		public Vector2 rawPosition
		{
			get
			{
				return this.m_RawPosition;
			}
			set
			{
				this.m_RawPosition = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		public Vector2 deltaPosition
		{
			get
			{
				return this.m_PositionDelta;
			}
			set
			{
				this.m_PositionDelta = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020F8 File Offset: 0x000002F8
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
			set
			{
				this.m_TimeDelta = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000211C File Offset: 0x0000031C
		public int tapCount
		{
			get
			{
				return this.m_TapCount;
			}
			set
			{
				this.m_TapCount = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
			set
			{
				this.m_Phase = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002164 File Offset: 0x00000364
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
			set
			{
				this.m_Pressure = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002188 File Offset: 0x00000388
		public float maximumPossiblePressure
		{
			get
			{
				return this.m_maximumPossiblePressure;
			}
			set
			{
				this.m_maximumPossiblePressure = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021AC File Offset: 0x000003AC
		public TouchType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021D0 File Offset: 0x000003D0
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
			set
			{
				this.m_AltitudeAngle = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021DC File Offset: 0x000003DC
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021F4 File Offset: 0x000003F4
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
			set
			{
				this.m_AzimuthAngle = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002218 File Offset: 0x00000418
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				this.m_Radius = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000223C File Offset: 0x0000043C
		public float radiusVariance
		{
			get
			{
				return this.m_RadiusVariance;
			}
			set
			{
				this.m_RadiusVariance = value;
			}
		}

		// Token: 0x0400000F RID: 15
		private int m_FingerId;

		// Token: 0x04000010 RID: 16
		private Vector2 m_Position;

		// Token: 0x04000011 RID: 17
		private Vector2 m_RawPosition;

		// Token: 0x04000012 RID: 18
		private Vector2 m_PositionDelta;

		// Token: 0x04000013 RID: 19
		private float m_TimeDelta;

		// Token: 0x04000014 RID: 20
		private int m_TapCount;

		// Token: 0x04000015 RID: 21
		private TouchPhase m_Phase;

		// Token: 0x04000016 RID: 22
		private TouchType m_Type;

		// Token: 0x04000017 RID: 23
		private float m_Pressure;

		// Token: 0x04000018 RID: 24
		private float m_maximumPossiblePressure;

		// Token: 0x04000019 RID: 25
		private float m_Radius;

		// Token: 0x0400001A RID: 26
		private float m_RadiusVariance;

		// Token: 0x0400001B RID: 27
		private float m_AltitudeAngle;

		// Token: 0x0400001C RID: 28
		private float m_AzimuthAngle;
	}
}
