using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	public class Compass
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002588 File Offset: 0x00000788
		public float magneticHeading
		{
			get
			{
				return LocationService.GetLastHeading().magneticHeading;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000025A4 File Offset: 0x000007A4
		public float trueHeading
		{
			get
			{
				return LocationService.GetLastHeading().trueHeading;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000025C0 File Offset: 0x000007C0
		public float headingAccuracy
		{
			get
			{
				return LocationService.GetLastHeading().headingAccuracy;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000025DC File Offset: 0x000007DC
		public Vector3 rawVector
		{
			get
			{
				return LocationService.GetLastHeading().raw;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000025F8 File Offset: 0x000007F8
		public double timestamp
		{
			get
			{
				return LocationService.GetLastHeading().timestamp;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002614 File Offset: 0x00000814
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000262B File Offset: 0x0000082B
		public bool enabled
		{
			get
			{
				return LocationService.IsHeadingUpdatesEnabled();
			}
			set
			{
				LocationService.SetHeadingUpdatesEnabled(value);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000257E File Offset: 0x0000077E
		public Compass()
		{
		}
	}
}
