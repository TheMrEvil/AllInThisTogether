using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C4 RID: 196
	internal struct SteamParamStringArray : IDisposable
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00013300 File Offset: 0x00011500
		public static SteamParamStringArray From(string[] array)
		{
			SteamParamStringArray steamParamStringArray = default(SteamParamStringArray);
			steamParamStringArray.NativeStrings = new IntPtr[array.Length];
			for (int i = 0; i < steamParamStringArray.NativeStrings.Length; i++)
			{
				steamParamStringArray.NativeStrings[i] = Marshal.StringToHGlobalAnsi(array[i]);
			}
			int cb = Marshal.SizeOf(typeof(IntPtr)) * steamParamStringArray.NativeStrings.Length;
			steamParamStringArray.NativeArray = Marshal.AllocHGlobal(cb);
			Marshal.Copy(steamParamStringArray.NativeStrings, 0, steamParamStringArray.NativeArray, steamParamStringArray.NativeStrings.Length);
			steamParamStringArray.Value = new SteamParamStringArray_t
			{
				Strings = steamParamStringArray.NativeArray,
				NumStrings = array.Length
			};
			return steamParamStringArray;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000133C0 File Offset: 0x000115C0
		public void Dispose()
		{
			foreach (IntPtr hglobal in this.NativeStrings)
			{
				Marshal.FreeHGlobal(hglobal);
			}
			Marshal.FreeHGlobal(this.NativeArray);
		}

		// Token: 0x04000785 RID: 1925
		public SteamParamStringArray_t Value;

		// Token: 0x04000786 RID: 1926
		private IntPtr[] NativeStrings;

		// Token: 0x04000787 RID: 1927
		private IntPtr NativeArray;
	}
}
