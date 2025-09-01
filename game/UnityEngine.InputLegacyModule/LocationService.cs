using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Runtime/Input/LocationService.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class LocationService
	{
		// Token: 0x0600003D RID: 61
		[FreeFunction("LocationService::IsServiceEnabledByUser")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsServiceEnabledByUser();

		// Token: 0x0600003E RID: 62
		[FreeFunction("LocationService::GetLocationStatus")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LocationServiceStatus GetLocationStatus();

		// Token: 0x0600003F RID: 63 RVA: 0x000024A8 File Offset: 0x000006A8
		[FreeFunction("LocationService::GetLastLocation")]
		internal static LocationInfo GetLastLocation()
		{
			LocationInfo result;
			LocationService.GetLastLocation_Injected(out result);
			return result;
		}

		// Token: 0x06000040 RID: 64
		[FreeFunction("LocationService::SetDesiredAccuracy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDesiredAccuracy(float value);

		// Token: 0x06000041 RID: 65
		[FreeFunction("LocationService::SetDistanceFilter")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDistanceFilter(float value);

		// Token: 0x06000042 RID: 66
		[FreeFunction("LocationService::StartUpdatingLocation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StartUpdatingLocation();

		// Token: 0x06000043 RID: 67
		[FreeFunction("LocationService::StopUpdatingLocation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StopUpdatingLocation();

		// Token: 0x06000044 RID: 68 RVA: 0x000024C0 File Offset: 0x000006C0
		[FreeFunction("LocationService::GetLastHeading")]
		internal static LocationService.HeadingInfo GetLastHeading()
		{
			LocationService.HeadingInfo result;
			LocationService.GetLastHeading_Injected(out result);
			return result;
		}

		// Token: 0x06000045 RID: 69
		[FreeFunction("LocationService::IsHeadingUpdatesEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsHeadingUpdatesEnabled();

		// Token: 0x06000046 RID: 70
		[FreeFunction("LocationService::SetHeadingUpdatesEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetHeadingUpdatesEnabled(bool value);

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024D8 File Offset: 0x000006D8
		public bool isEnabledByUser
		{
			get
			{
				return LocationService.IsServiceEnabledByUser();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000024F0 File Offset: 0x000006F0
		public LocationServiceStatus status
		{
			get
			{
				return LocationService.GetLocationStatus();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002508 File Offset: 0x00000708
		public LocationInfo lastData
		{
			get
			{
				bool flag = this.status != LocationServiceStatus.Running;
				if (flag)
				{
					Debug.Log("Location service updates are not enabled. Check LocationService.status before querying last location.");
				}
				return LocationService.GetLastLocation();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000253A File Offset: 0x0000073A
		public void Start(float desiredAccuracyInMeters, float updateDistanceInMeters)
		{
			LocationService.SetDesiredAccuracy(desiredAccuracyInMeters);
			LocationService.SetDistanceFilter(updateDistanceInMeters);
			LocationService.StartUpdatingLocation();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002551 File Offset: 0x00000751
		public void Start(float desiredAccuracyInMeters)
		{
			this.Start(desiredAccuracyInMeters, 10f);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002561 File Offset: 0x00000761
		public void Start()
		{
			this.Start(10f, 10f);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002575 File Offset: 0x00000775
		public void Stop()
		{
			LocationService.StopUpdatingLocation();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000257E File Offset: 0x0000077E
		public LocationService()
		{
		}

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastLocation_Injected(out LocationInfo ret);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastHeading_Injected(out LocationService.HeadingInfo ret);

		// Token: 0x0200000C RID: 12
		internal struct HeadingInfo
		{
			// Token: 0x04000035 RID: 53
			public float magneticHeading;

			// Token: 0x04000036 RID: 54
			public float trueHeading;

			// Token: 0x04000037 RID: 55
			public float headingAccuracy;

			// Token: 0x04000038 RID: 56
			public Vector3 raw;

			// Token: 0x04000039 RID: 57
			public double timestamp;
		}
	}
}
