using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Curl
{
	// Token: 0x02000004 RID: 4
	[StaticAccessor("UnityCurl", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/UnityCurl/Public/UnityCurl.h")]
	internal static class UnityCurl
	{
		// Token: 0x06000001 RID: 1
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateMultiHandle();

		// Token: 0x06000002 RID: 2
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyMultiHandle(IntPtr handle);

		// Token: 0x06000003 RID: 3
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateEasyHandle(byte* method, byte* url, out uint curlMethod);

		// Token: 0x06000004 RID: 4
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetupEasyHandle(IntPtr handle, uint curlMethod, IntPtr headers, ulong contentLen, uint flags);

		// Token: 0x06000005 RID: 5
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyEasyHandle(IntPtr handle);

		// Token: 0x06000006 RID: 6
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void QueueRequest(IntPtr multiHandle, IntPtr easyHandle);

		// Token: 0x06000007 RID: 7
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr AppendHeader(IntPtr headerList, byte* header);

		// Token: 0x06000008 RID: 8
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeHeaderList(IntPtr headerList);

		// Token: 0x06000009 RID: 9
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetRequestErrorCode(IntPtr request);

		// Token: 0x0600000A RID: 10
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetRequestStatus(IntPtr request);

		// Token: 0x0600000B RID: 11
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetRequestStatusCode(IntPtr request);

		// Token: 0x0600000C RID: 12
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDownloadSize(IntPtr request, out ulong downloaded, out ulong expected);

		// Token: 0x0600000D RID: 13
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* GetResponseHeader(IntPtr request, uint index, out uint length);

		// Token: 0x0600000E RID: 14
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* GetMoreBody(IntPtr handle, out int length);

		// Token: 0x0600000F RID: 15 RVA: 0x00002050 File Offset: 0x00000250
		internal unsafe static void SendMoreBody(IntPtr handle, byte* chunk, uint length, BufferOwnership ownership)
		{
			UnityCurl.SendMoreBody(handle, chunk, length, (int)ownership);
		}

		// Token: 0x06000010 RID: 16
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SendMoreBody(IntPtr handle, byte* chunk, uint length, int ownership);

		// Token: 0x06000011 RID: 17
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void AbortRequest(IntPtr handle);
	}
}
