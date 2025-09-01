using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000EE RID: 238
	[NativeHeader("Runtime/Export/Bootstrap/BootConfig.bindings.h")]
	internal class BootConfigData
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00006F1B File Offset: 0x0000511B
		public void AddKey(string key)
		{
			this.Append(key, null);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00006F28 File Offset: 0x00005128
		public string Get(string key)
		{
			return this.GetValue(key, 0);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00006F44 File Offset: 0x00005144
		public string Get(string key, int index)
		{
			return this.GetValue(key, index);
		}

		// Token: 0x06000444 RID: 1092
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Append(string key, string value);

		// Token: 0x06000445 RID: 1093
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Set(string key, string value);

		// Token: 0x06000446 RID: 1094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetValue(string key, int index);

		// Token: 0x06000447 RID: 1095 RVA: 0x00006F60 File Offset: 0x00005160
		[RequiredByNativeCode]
		private static BootConfigData WrapBootConfigData(IntPtr nativeHandle)
		{
			return new BootConfigData(nativeHandle);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00006F78 File Offset: 0x00005178
		private BootConfigData(IntPtr nativeHandle)
		{
			bool flag = nativeHandle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("native handle can not be null");
			}
			this.m_Ptr = nativeHandle;
		}

		// Token: 0x04000326 RID: 806
		private IntPtr m_Ptr;
	}
}
