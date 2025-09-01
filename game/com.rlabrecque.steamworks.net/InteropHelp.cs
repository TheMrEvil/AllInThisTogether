using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Steamworks
{
	// Token: 0x02000185 RID: 389
	public class InteropHelp
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0000CDEF File Offset: 0x0000AFEF
		public static void TestIfPlatformSupported()
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public static void TestIfAvailableClient()
		{
			InteropHelp.TestIfPlatformSupported();
			if (CSteamAPIContext.GetSteamClient() == IntPtr.Zero && !CSteamAPIContext.Init())
			{
				throw new InvalidOperationException("Steamworks is not initialized.");
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000CE1B File Offset: 0x0000B01B
		public static void TestIfAvailableGameServer()
		{
			InteropHelp.TestIfPlatformSupported();
			if (CSteamGameServerAPIContext.GetSteamClient() == IntPtr.Zero && !CSteamGameServerAPIContext.Init())
			{
				throw new InvalidOperationException("Steamworks GameServer is not initialized.");
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000CE48 File Offset: 0x0000B048
		public static string PtrToStringUTF8(IntPtr nativeUtf8)
		{
			if (nativeUtf8 == IntPtr.Zero)
			{
				return null;
			}
			int num = 0;
			while (Marshal.ReadByte(nativeUtf8, num) != 0)
			{
				num++;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(nativeUtf8, array, 0, array.Length);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public static string ByteArrayToStringUTF8(byte[] buffer)
		{
			int num = 0;
			while (num < buffer.Length && buffer[num] != 0)
			{
				num++;
			}
			return Encoding.UTF8.GetString(buffer, 0, num);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public static void StringToByteArrayUTF8(string str, byte[] outArrayBuffer, int outArrayBufferSize)
		{
			outArrayBuffer = new byte[outArrayBufferSize];
			int bytes = Encoding.UTF8.GetBytes(str, 0, str.Length, outArrayBuffer, 0);
			outArrayBuffer[bytes] = 0;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0000CEFA File Offset: 0x0000B0FA
		public InteropHelp()
		{
		}

		// Token: 0x020001D0 RID: 464
		public class UTF8StringHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06000B88 RID: 2952 RVA: 0x0001079C File Offset: 0x0000E99C
			public UTF8StringHandle(string str) : base(true)
			{
				if (str == null)
				{
					base.SetHandle(IntPtr.Zero);
					return;
				}
				byte[] array = new byte[Encoding.UTF8.GetByteCount(str) + 1];
				Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
				IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				base.SetHandle(intPtr);
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x00010802 File Offset: 0x0000EA02
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					Marshal.FreeHGlobal(this.handle);
				}
				return true;
			}
		}

		// Token: 0x020001D1 RID: 465
		public class SteamParamStringArray
		{
			// Token: 0x06000B8A RID: 2954 RVA: 0x00010818 File Offset: 0x0000EA18
			public SteamParamStringArray(IList<string> strings)
			{
				if (strings == null)
				{
					this.m_pSteamParamStringArray = IntPtr.Zero;
					return;
				}
				this.m_Strings = new IntPtr[strings.Count];
				for (int i = 0; i < strings.Count; i++)
				{
					byte[] array = new byte[Encoding.UTF8.GetByteCount(strings[i]) + 1];
					Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, array, 0);
					this.m_Strings[i] = Marshal.AllocHGlobal(array.Length);
					Marshal.Copy(array, 0, this.m_Strings[i], array.Length);
				}
				this.m_ptrStrings = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * this.m_Strings.Length);
				SteamParamStringArray_t steamParamStringArray_t = new SteamParamStringArray_t
				{
					m_ppStrings = this.m_ptrStrings,
					m_nNumStrings = this.m_Strings.Length
				};
				Marshal.Copy(this.m_Strings, 0, steamParamStringArray_t.m_ppStrings, this.m_Strings.Length);
				this.m_pSteamParamStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray_t)));
				Marshal.StructureToPtr<SteamParamStringArray_t>(steamParamStringArray_t, this.m_pSteamParamStringArray, false);
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x00010944 File Offset: 0x0000EB44
			protected override void Finalize()
			{
				try
				{
					if (this.m_Strings != null)
					{
						IntPtr[] strings = this.m_Strings;
						for (int i = 0; i < strings.Length; i++)
						{
							Marshal.FreeHGlobal(strings[i]);
						}
					}
					if (this.m_ptrStrings != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_ptrStrings);
					}
					if (this.m_pSteamParamStringArray != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_pSteamParamStringArray);
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x000109CC File Offset: 0x0000EBCC
			public static implicit operator IntPtr(InteropHelp.SteamParamStringArray that)
			{
				return that.m_pSteamParamStringArray;
			}

			// Token: 0x04000B0F RID: 2831
			private IntPtr[] m_Strings;

			// Token: 0x04000B10 RID: 2832
			private IntPtr m_ptrStrings;

			// Token: 0x04000B11 RID: 2833
			private IntPtr m_pSteamParamStringArray;
		}
	}
}
