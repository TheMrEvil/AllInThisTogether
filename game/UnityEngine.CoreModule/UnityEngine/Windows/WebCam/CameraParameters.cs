using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002B3 RID: 691
	[UsedByNativeCode]
	[NativeHeader("PlatformDependent/Win/Webcam/CameraParameters.h")]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public struct CameraParameters
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0002E80C File Offset: 0x0002CA0C
		public float hologramOpacity
		{
			get
			{
				return this.m_HologramOpacity;
			}
			set
			{
				this.m_HologramOpacity = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0002E818 File Offset: 0x0002CA18
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0002E830 File Offset: 0x0002CA30
		public float frameRate
		{
			get
			{
				return this.m_FrameRate;
			}
			set
			{
				this.m_FrameRate = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0002E83C File Offset: 0x0002CA3C
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x0002E854 File Offset: 0x0002CA54
		public int cameraResolutionWidth
		{
			get
			{
				return this.m_CameraResolutionWidth;
			}
			set
			{
				this.m_CameraResolutionWidth = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0002E860 File Offset: 0x0002CA60
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0002E878 File Offset: 0x0002CA78
		public int cameraResolutionHeight
		{
			get
			{
				return this.m_CameraResolutionHeight;
			}
			set
			{
				this.m_CameraResolutionHeight = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0002E884 File Offset: 0x0002CA84
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0002E89C File Offset: 0x0002CA9C
		public CapturePixelFormat pixelFormat
		{
			get
			{
				return this.m_PixelFormat;
			}
			set
			{
				this.m_PixelFormat = value;
			}
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0002E8A8 File Offset: 0x0002CAA8
		public CameraParameters(WebCamMode webCamMode)
		{
			this.m_HologramOpacity = 1f;
			this.m_PixelFormat = CapturePixelFormat.BGRA32;
			this.m_FrameRate = 0f;
			this.m_CameraResolutionWidth = 0;
			this.m_CameraResolutionHeight = 0;
			bool flag = webCamMode == WebCamMode.PhotoMode;
			if (flag)
			{
				Resolution resolution = (from res in PhotoCapture.SupportedResolutions
				orderby res.width * res.height descending
				select res).First<Resolution>();
				this.m_CameraResolutionWidth = resolution.width;
				this.m_CameraResolutionHeight = resolution.height;
			}
			else
			{
				bool flag2 = webCamMode == WebCamMode.VideoMode;
				if (flag2)
				{
					Resolution resolution2 = (from res in VideoCapture.SupportedResolutions
					orderby res.width * res.height descending
					select res).First<Resolution>();
					float frameRate = (from fps in VideoCapture.GetSupportedFrameRatesForResolution(resolution2)
					orderby fps descending
					select fps).First<float>();
					this.m_CameraResolutionWidth = resolution2.width;
					this.m_CameraResolutionHeight = resolution2.height;
					this.m_FrameRate = frameRate;
				}
			}
		}

		// Token: 0x04000981 RID: 2433
		private float m_HologramOpacity;

		// Token: 0x04000982 RID: 2434
		private float m_FrameRate;

		// Token: 0x04000983 RID: 2435
		private int m_CameraResolutionWidth;

		// Token: 0x04000984 RID: 2436
		private int m_CameraResolutionHeight;

		// Token: 0x04000985 RID: 2437
		private CapturePixelFormat m_PixelFormat;

		// Token: 0x020002B4 RID: 692
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001D19 RID: 7449 RVA: 0x0002E9C8 File Offset: 0x0002CBC8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001D1A RID: 7450 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x06001D1B RID: 7451 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
			internal int <.ctor>b__20_0(Resolution res)
			{
				return res.width * res.height;
			}

			// Token: 0x06001D1C RID: 7452 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
			internal int <.ctor>b__20_1(Resolution res)
			{
				return res.width * res.height;
			}

			// Token: 0x06001D1D RID: 7453 RVA: 0x0002E9E5 File Offset: 0x0002CBE5
			internal float <.ctor>b__20_2(float fps)
			{
				return fps;
			}

			// Token: 0x04000986 RID: 2438
			public static readonly CameraParameters.<>c <>9 = new CameraParameters.<>c();

			// Token: 0x04000987 RID: 2439
			public static Func<Resolution, int> <>9__20_0;

			// Token: 0x04000988 RID: 2440
			public static Func<Resolution, int> <>9__20_1;

			// Token: 0x04000989 RID: 2441
			public static Func<float, float> <>9__20_2;
		}
	}
}
