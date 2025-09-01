using System;

namespace QFSW.QC
{
	// Token: 0x02000028 RID: 40
	[Flags]
	public enum Platform : long
	{
		// Token: 0x04000056 RID: 86
		OSXEditor = 1L,
		// Token: 0x04000057 RID: 87
		OSXPlayer = 2L,
		// Token: 0x04000058 RID: 88
		WindowsPlayer = 4L,
		// Token: 0x04000059 RID: 89
		OSXWebPlayer = 8L,
		// Token: 0x0400005A RID: 90
		OSXDashboardPlayer = 16L,
		// Token: 0x0400005B RID: 91
		WindowsWebPlayer = 32L,
		// Token: 0x0400005C RID: 92
		WindowsEditor = 128L,
		// Token: 0x0400005D RID: 93
		IPhonePlayer = 256L,
		// Token: 0x0400005E RID: 94
		PS3 = 512L,
		// Token: 0x0400005F RID: 95
		XBOX360 = 1024L,
		// Token: 0x04000060 RID: 96
		Android = 2048L,
		// Token: 0x04000061 RID: 97
		NaCl = 4096L,
		// Token: 0x04000062 RID: 98
		LinuxPlayer = 8192L,
		// Token: 0x04000063 RID: 99
		FlashPlayer = 32768L,
		// Token: 0x04000064 RID: 100
		LinuxEditor = 65536L,
		// Token: 0x04000065 RID: 101
		WebGLPlayer = 131072L,
		// Token: 0x04000066 RID: 102
		MetroPlayerX86 = 262144L,
		// Token: 0x04000067 RID: 103
		WSAPlayerX86 = 262144L,
		// Token: 0x04000068 RID: 104
		MetroPlayerX64 = 524288L,
		// Token: 0x04000069 RID: 105
		WSAPlayerX64 = 524288L,
		// Token: 0x0400006A RID: 106
		MetroPlayerARM = 1048576L,
		// Token: 0x0400006B RID: 107
		WSAPlayerARM = 1048576L,
		// Token: 0x0400006C RID: 108
		WP8Player = 2097152L,
		// Token: 0x0400006D RID: 109
		BlackBerryPlayer = 4194304L,
		// Token: 0x0400006E RID: 110
		TizenPlayer = 8388608L,
		// Token: 0x0400006F RID: 111
		PSP2 = 16777216L,
		// Token: 0x04000070 RID: 112
		PS4 = 33554432L,
		// Token: 0x04000071 RID: 113
		PSM = 67108864L,
		// Token: 0x04000072 RID: 114
		XboxOne = 134217728L,
		// Token: 0x04000073 RID: 115
		SamsungTVPlayer = 268435456L,
		// Token: 0x04000074 RID: 116
		WiiU = 1073741824L,
		// Token: 0x04000075 RID: 117
		tvOS = 2147483648L,
		// Token: 0x04000076 RID: 118
		Switch = 4294967296L,
		// Token: 0x04000077 RID: 119
		Lumin = 8589934592L,
		// Token: 0x04000078 RID: 120
		Stadia = 17179869184L,
		// Token: 0x04000079 RID: 121
		None = 0L,
		// Token: 0x0400007A RID: 122
		AllPlatforms = -1L,
		// Token: 0x0400007B RID: 123
		EditorPlatforms = 65665L,
		// Token: 0x0400007C RID: 124
		BuildPlatforms = -65666L,
		// Token: 0x0400007D RID: 125
		MobilePlatforms = 2099456L
	}
}
