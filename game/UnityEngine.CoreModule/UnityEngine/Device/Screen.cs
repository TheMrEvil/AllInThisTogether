using System;
using System.Collections.Generic;
using UnityEngine.Internal;

namespace UnityEngine.Device
{
	// Token: 0x02000452 RID: 1106
	public static class Screen
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000413FA File Offset: 0x0003F5FA
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x00041401 File Offset: 0x0003F601
		public static float brightness
		{
			get
			{
				return Screen.brightness;
			}
			set
			{
				Screen.brightness = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0004140A File Offset: 0x0003F60A
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x00041411 File Offset: 0x0003F611
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.autorotateToLandscapeLeft;
			}
			set
			{
				Screen.autorotateToLandscapeLeft = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0004141A File Offset: 0x0003F61A
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x00041421 File Offset: 0x0003F621
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.autorotateToLandscapeRight;
			}
			set
			{
				Screen.autorotateToLandscapeRight = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0004142A File Offset: 0x0003F62A
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x00041431 File Offset: 0x0003F631
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.autorotateToPortrait;
			}
			set
			{
				Screen.autorotateToPortrait = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0004143A File Offset: 0x0003F63A
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x00041441 File Offset: 0x0003F641
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.autorotateToPortraitUpsideDown;
			}
			set
			{
				Screen.autorotateToPortraitUpsideDown = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x0004144A File Offset: 0x0003F64A
		public static Resolution currentResolution
		{
			get
			{
				return Screen.currentResolution;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x00041451 File Offset: 0x0003F651
		public static Rect[] cutouts
		{
			get
			{
				return Screen.cutouts;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x00041458 File Offset: 0x0003F658
		public static float dpi
		{
			get
			{
				return Screen.dpi;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x0004145F File Offset: 0x0003F65F
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x00041466 File Offset: 0x0003F666
		public static bool fullScreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				Screen.fullScreen = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x0004146F File Offset: 0x0003F66F
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x00041476 File Offset: 0x0003F676
		public static FullScreenMode fullScreenMode
		{
			get
			{
				return Screen.fullScreenMode;
			}
			set
			{
				Screen.fullScreenMode = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0004147F File Offset: 0x0003F67F
		public static int height
		{
			get
			{
				return Screen.height;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00041486 File Offset: 0x0003F686
		public static int width
		{
			get
			{
				return Screen.width;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0004148D File Offset: 0x0003F68D
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x00041494 File Offset: 0x0003F694
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.orientation;
			}
			set
			{
				Screen.orientation = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0004149D File Offset: 0x0003F69D
		public static Resolution[] resolutions
		{
			get
			{
				return Screen.resolutions;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x000414A4 File Offset: 0x0003F6A4
		public static Rect safeArea
		{
			get
			{
				return Screen.safeArea;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x000414AB File Offset: 0x0003F6AB
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x000414B2 File Offset: 0x0003F6B2
		public static int sleepTimeout
		{
			get
			{
				return Screen.sleepTimeout;
			}
			set
			{
				Screen.sleepTimeout = value;
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000414BB File Offset: 0x0003F6BB
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreenMode, preferredRefreshRate);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000414C8 File Offset: 0x0003F6C8
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, 0);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000414D5 File Offset: 0x0003F6D5
		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000414E8 File Offset: 0x0003F6E8
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000414F5 File Offset: 0x0003F6F5
		public static Vector2Int mainWindowPosition
		{
			get
			{
				return Screen.mainWindowPosition;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x000414FC File Offset: 0x0003F6FC
		public static DisplayInfo mainWindowDisplayInfo
		{
			get
			{
				return Screen.mainWindowDisplayInfo;
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00041503 File Offset: 0x0003F703
		public static void GetDisplayLayout(List<DisplayInfo> displayLayout)
		{
			Screen.GetDisplayLayout(displayLayout);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0004150C File Offset: 0x0003F70C
		public static AsyncOperation MoveMainWindowTo(in DisplayInfo display, Vector2Int position)
		{
			return Screen.MoveMainWindowTo(display, position);
		}
	}
}
