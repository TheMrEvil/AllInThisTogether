using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001D3 RID: 467
	[NativeHeader("Runtime/Utilities/PlayerPrefs.h")]
	public class PlayerPrefs
	{
		// Token: 0x060015BF RID: 5567
		[NativeMethod("SetInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x060015C0 RID: 5568
		[NativeMethod("SetFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x060015C1 RID: 5569
		[NativeMethod("SetString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x060015C2 RID: 5570 RVA: 0x00023078 File Offset: 0x00021278
		public static void SetInt(string key, int value)
		{
			bool flag = !PlayerPrefs.TrySetInt(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015C3 RID: 5571
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, int defaultValue);

		// Token: 0x060015C4 RID: 5572 RVA: 0x000230A0 File Offset: 0x000212A0
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key, 0);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000230BC File Offset: 0x000212BC
		public static void SetFloat(string key, float value)
		{
			bool flag = !PlayerPrefs.TrySetFloat(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015C6 RID: 5574
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, float defaultValue);

		// Token: 0x060015C7 RID: 5575 RVA: 0x000230E4 File Offset: 0x000212E4
		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key, 0f);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00023104 File Offset: 0x00021304
		public static void SetString(string key, string value)
		{
			bool flag = !PlayerPrefs.TrySetSetString(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x060015C9 RID: 5577
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, string defaultValue);

		// Token: 0x060015CA RID: 5578 RVA: 0x0002312C File Offset: 0x0002132C
		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key, "");
		}

		// Token: 0x060015CB RID: 5579
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		// Token: 0x060015CC RID: 5580
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteKey(string key);

		// Token: 0x060015CD RID: 5581
		[NativeMethod("DeleteAllWithCallback")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteAll();

		// Token: 0x060015CE RID: 5582
		[NativeMethod("Sync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Save();

		// Token: 0x060015CF RID: 5583 RVA: 0x00002072 File Offset: 0x00000272
		public PlayerPrefs()
		{
		}
	}
}
