using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	public struct LocationInfo
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002418 File Offset: 0x00000618
		public float latitude
		{
			get
			{
				return this.m_Latitude;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002430 File Offset: 0x00000630
		public float longitude
		{
			get
			{
				return this.m_Longitude;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002448 File Offset: 0x00000648
		public float altitude
		{
			get
			{
				return this.m_Altitude;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002460 File Offset: 0x00000660
		public float horizontalAccuracy
		{
			get
			{
				return this.m_HorizontalAccuracy;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002478 File Offset: 0x00000678
		public float verticalAccuracy
		{
			get
			{
				return this.m_VerticalAccuracy;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002490 File Offset: 0x00000690
		public double timestamp
		{
			get
			{
				return this.m_Timestamp;
			}
		}

		// Token: 0x0400002A RID: 42
		internal double m_Timestamp;

		// Token: 0x0400002B RID: 43
		internal float m_Latitude;

		// Token: 0x0400002C RID: 44
		internal float m_Longitude;

		// Token: 0x0400002D RID: 45
		internal float m_Altitude;

		// Token: 0x0400002E RID: 46
		internal float m_HorizontalAccuracy;

		// Token: 0x0400002F RID: 47
		internal float m_VerticalAccuracy;
	}
}
