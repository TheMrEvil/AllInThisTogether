using System;

namespace UnityEngine.Android
{
	// Token: 0x0200001E RID: 30
	public struct Permission
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00008774 File Offset: 0x00006974
		private static AndroidJavaObject GetUnityPermissions()
		{
			bool flag = Permission.m_UnityPermissions != null;
			AndroidJavaObject unityPermissions;
			if (flag)
			{
				unityPermissions = Permission.m_UnityPermissions;
			}
			else
			{
				Permission.m_UnityPermissions = new AndroidJavaClass("com.unity3d.player.UnityPermissions");
				unityPermissions = Permission.m_UnityPermissions;
			}
			return unityPermissions;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000087B0 File Offset: 0x000069B0
		public static bool HasUserAuthorizedPermission(string permission)
		{
			bool flag = permission == null;
			return !flag;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000087D0 File Offset: 0x000069D0
		public static void RequestUserPermission(string permission)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[]
				{
					permission
				}, null);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000087FC File Offset: 0x000069FC
		public static void RequestUserPermissions(string[] permissions)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (!flag)
			{
				Permission.RequestUserPermissions(permissions, null);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008824 File Offset: 0x00006A24
		public static void RequestUserPermission(string permission, PermissionCallbacks callbacks)
		{
			bool flag = permission == null;
			if (!flag)
			{
				Permission.RequestUserPermissions(new string[]
				{
					permission
				}, callbacks);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008850 File Offset: 0x00006A50
		public static void RequestUserPermissions(string[] permissions, PermissionCallbacks callbacks)
		{
			bool flag = permissions == null || permissions.Length == 0;
			if (flag)
			{
			}
		}

		// Token: 0x04000050 RID: 80
		public const string Camera = "android.permission.CAMERA";

		// Token: 0x04000051 RID: 81
		public const string Microphone = "android.permission.RECORD_AUDIO";

		// Token: 0x04000052 RID: 82
		public const string FineLocation = "android.permission.ACCESS_FINE_LOCATION";

		// Token: 0x04000053 RID: 83
		public const string CoarseLocation = "android.permission.ACCESS_COARSE_LOCATION";

		// Token: 0x04000054 RID: 84
		public const string ExternalStorageRead = "android.permission.READ_EXTERNAL_STORAGE";

		// Token: 0x04000055 RID: 85
		public const string ExternalStorageWrite = "android.permission.WRITE_EXTERNAL_STORAGE";

		// Token: 0x04000056 RID: 86
		private static AndroidJavaObject m_UnityPermissions;
	}
}
