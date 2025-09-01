using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Baselib.LowLevel
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ThreadLocalStorage.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_RegisteredNetwork.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Thread.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ErrorCode.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_SourceLocation.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Socket.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Memory.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ErrorState.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Timer.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_DynamicLibrary.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_FileIO.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_NetworkAddress.gen.binding.h")]
	internal static class Binding
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002238 File Offset: 0x00000438
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_OpenUtf8(byte* pathnameUtf8, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_DynamicLibrary_Handle result;
			Binding.Baselib_DynamicLibrary_OpenUtf8_Injected(pathnameUtf8, errorState, out result);
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002250 File Offset: 0x00000450
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_OpenUtf16(char* pathnameUtf16, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_DynamicLibrary_Handle result;
			Binding.Baselib_DynamicLibrary_OpenUtf16_Injected(pathnameUtf16, errorState, out result);
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002268 File Offset: 0x00000468
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_OpenProgramHandle(Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_DynamicLibrary_Handle result;
			Binding.Baselib_DynamicLibrary_OpenProgramHandle_Injected(errorState, out result);
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002280 File Offset: 0x00000480
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_FromNativeHandle(ulong handle, uint type, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_DynamicLibrary_Handle result;
			Binding.Baselib_DynamicLibrary_FromNativeHandle_Injected(handle, type, errorState, out result);
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002298 File Offset: 0x00000498
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static IntPtr Baselib_DynamicLibrary_GetFunction(Binding.Baselib_DynamicLibrary_Handle handle, byte* functionName, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_DynamicLibrary_GetFunction_Injected(ref handle, functionName, errorState);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000022A3 File Offset: 0x000004A3
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_DynamicLibrary_Close(Binding.Baselib_DynamicLibrary_Handle handle)
		{
			Binding.Baselib_DynamicLibrary_Close_Injected(ref handle);
		}

		// Token: 0x0600002E RID: 46
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint Baselib_ErrorState_Explain(Binding.Baselib_ErrorState* errorState, byte* buffer, uint bufferLen, Binding.Baselib_ErrorState_ExplainVerbosity verbosity);

		// Token: 0x0600002F RID: 47 RVA: 0x000022AC File Offset: 0x000004AC
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_FileIO_EventQueue Baselib_FileIO_EventQueue_Create()
		{
			Binding.Baselib_FileIO_EventQueue result;
			Binding.Baselib_FileIO_EventQueue_Create_Injected(out result);
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000022C1 File Offset: 0x000004C1
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_FileIO_EventQueue_Free(Binding.Baselib_FileIO_EventQueue eq)
		{
			Binding.Baselib_FileIO_EventQueue_Free_Injected(ref eq);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022CA File Offset: 0x000004CA
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static ulong Baselib_FileIO_EventQueue_Dequeue(Binding.Baselib_FileIO_EventQueue eq, Binding.Baselib_FileIO_EventQueue_Result* results, ulong count, uint timeoutInMilliseconds)
		{
			return Binding.Baselib_FileIO_EventQueue_Dequeue_Injected(ref eq, results, count, timeoutInMilliseconds);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022D6 File Offset: 0x000004D6
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_FileIO_EventQueue_Shutdown(Binding.Baselib_FileIO_EventQueue eq, uint threadCount)
		{
			Binding.Baselib_FileIO_EventQueue_Shutdown_Injected(ref eq, threadCount);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000022E0 File Offset: 0x000004E0
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_FileIO_AsyncFile Baselib_FileIO_AsyncOpen(Binding.Baselib_FileIO_EventQueue eq, byte* pathname, ulong userdata, Binding.Baselib_FileIO_Priority priority)
		{
			Binding.Baselib_FileIO_AsyncFile result;
			Binding.Baselib_FileIO_AsyncOpen_Injected(ref eq, pathname, userdata, priority, out result);
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022FA File Offset: 0x000004FA
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_FileIO_AsyncRead(Binding.Baselib_FileIO_AsyncFile file, Binding.Baselib_FileIO_ReadRequest* requests, ulong count, ulong userdata, Binding.Baselib_FileIO_Priority priority)
		{
			Binding.Baselib_FileIO_AsyncRead_Injected(ref file, requests, count, userdata, priority);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002308 File Offset: 0x00000508
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_FileIO_AsyncClose(Binding.Baselib_FileIO_AsyncFile file)
		{
			Binding.Baselib_FileIO_AsyncClose_Injected(ref file);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002314 File Offset: 0x00000514
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_FileIO_SyncFile Baselib_FileIO_SyncOpen(byte* pathname, Binding.Baselib_FileIO_OpenFlags openFlags, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_FileIO_SyncFile result;
			Binding.Baselib_FileIO_SyncOpen_Injected(pathname, openFlags, errorState, out result);
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000232C File Offset: 0x0000052C
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_FileIO_SyncFile Baselib_FileIO_SyncFileFromNativeHandle(ulong handle, uint type)
		{
			Binding.Baselib_FileIO_SyncFile result;
			Binding.Baselib_FileIO_SyncFileFromNativeHandle_Injected(handle, type, out result);
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002343 File Offset: 0x00000543
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static ulong Baselib_FileIO_SyncRead(Binding.Baselib_FileIO_SyncFile file, ulong offset, IntPtr buffer, ulong size, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_FileIO_SyncRead_Injected(ref file, offset, buffer, size, errorState);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002351 File Offset: 0x00000551
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static ulong Baselib_FileIO_SyncWrite(Binding.Baselib_FileIO_SyncFile file, ulong offset, IntPtr buffer, ulong size, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_FileIO_SyncWrite_Injected(ref file, offset, buffer, size, errorState);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000235F File Offset: 0x0000055F
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_FileIO_SyncFlush(Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_FileIO_SyncFlush_Injected(ref file, errorState);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002369 File Offset: 0x00000569
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_FileIO_SyncSetFileSize(Binding.Baselib_FileIO_SyncFile file, ulong size, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_FileIO_SyncSetFileSize_Injected(ref file, size, errorState);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002374 File Offset: 0x00000574
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static ulong Baselib_FileIO_SyncGetFileSize(Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_FileIO_SyncGetFileSize_Injected(ref file, errorState);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000237E File Offset: 0x0000057E
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_FileIO_SyncClose(Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_FileIO_SyncClose_Injected(ref file, errorState);
		}

		// Token: 0x0600003E RID: 62
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Memory_GetPageSizeInfo(Binding.Baselib_Memory_PageSizeInfo* outPagesSizeInfo);

		// Token: 0x0600003F RID: 63
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_Allocate(UIntPtr size);

		// Token: 0x06000040 RID: 64
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_Reallocate(IntPtr ptr, UIntPtr newSize);

		// Token: 0x06000041 RID: 65
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Memory_Free(IntPtr ptr);

		// Token: 0x06000042 RID: 66
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_AlignedAllocate(UIntPtr size, UIntPtr alignment);

		// Token: 0x06000043 RID: 67
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_AlignedReallocate(IntPtr ptr, UIntPtr newSize, UIntPtr alignment);

		// Token: 0x06000044 RID: 68
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Memory_AlignedFree(IntPtr ptr);

		// Token: 0x06000045 RID: 69 RVA: 0x00002388 File Offset: 0x00000588
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Memory_PageAllocation Baselib_Memory_AllocatePages(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_PageAllocation result;
			Binding.Baselib_Memory_AllocatePages_Injected(pageSize, pageCount, alignmentInMultipleOfPageSize, pageState, errorState, out result);
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000023A3 File Offset: 0x000005A3
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Memory_ReleasePages(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_ReleasePages_Injected(ref pageAllocation, errorState);
		}

		// Token: 0x06000047 RID: 71
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Memory_SetPageState(IntPtr addressOfFirstPage, ulong pageSize, ulong pageCount, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000048 RID: 72
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_NetworkAddress_Encode(Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_NetworkAddress_Family family, byte* ip, ushort port, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000049 RID: 73
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_NetworkAddress_Decode(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_NetworkAddress_Family* family, byte* ipAddressBuffer, uint ipAddressBufferLen, ushort* port, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600004A RID: 74 RVA: 0x000023B0 File Offset: 0x000005B0
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Buffer Baselib_RegisteredNetwork_Buffer_Register(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Buffer result;
			Binding.Baselib_RegisteredNetwork_Buffer_Register_Injected(ref pageAllocation, errorState, out result);
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000023C8 File Offset: 0x000005C8
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Buffer_Deregister(Binding.Baselib_RegisteredNetwork_Buffer buffer)
		{
			Binding.Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref buffer);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000023D4 File Offset: 0x000005D4
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Create(Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size)
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice result;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref buffer, offset, size, out result);
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000023F0 File Offset: 0x000005F0
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Empty()
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice result;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out result);
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002408 File Offset: 0x00000608
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Create(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint result;
			Binding.Baselib_RegisteredNetwork_Endpoint_Create_Injected(srcAddress, ref dstSlice, errorState, out result);
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002424 File Offset: 0x00000624
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Empty()
		{
			Binding.Baselib_RegisteredNetwork_Endpoint result;
			Binding.Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out result);
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002439 File Offset: 0x00000639
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref endpoint, dstAddress, errorState);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002444 File Offset: 0x00000644
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Create(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP result;
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(bindAddress, endpointReuse, sendQueueSize, recvQueueSize, errorState, out result);
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000245F File Offset: 0x0000065F
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref socket, requests, requestsCount, errorState);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000246B File Offset: 0x0000066B
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref socket, requests, requestsCount, errorState);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002477 File Offset: 0x00000677
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref socket, errorState);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002481 File Offset: 0x00000681
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref socket, errorState);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000248B File Offset: 0x0000068B
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002496 File Offset: 0x00000696
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000024A1 File Offset: 0x000006A1
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref socket, results, resultsCount, errorState);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000024AD File Offset: 0x000006AD
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref socket, results, resultsCount, errorState);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000024B9 File Offset: 0x000006B9
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref socket, dstAddress, errorState);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000024C4 File Offset: 0x000006C4
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Socket_UDP_Close(Binding.Baselib_RegisteredNetwork_Socket_UDP socket)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref socket);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000024D0 File Offset: 0x000006D0
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Socket_Handle Baselib_Socket_Create(Binding.Baselib_NetworkAddress_Family family, Binding.Baselib_Socket_Protocol protocol, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Handle result;
			Binding.Baselib_Socket_Create_Injected(family, protocol, errorState, out result);
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000024E8 File Offset: 0x000006E8
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_Bind(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Bind_Injected(ref socket, address, addressReuse, errorState);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000024F4 File Offset: 0x000006F4
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_TCP_Connect(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_TCP_Connect_Injected(ref socket, address, addressReuse, errorState);
		}

		// Token: 0x0600005F RID: 95
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Socket_Poll(Binding.Baselib_Socket_PollFd* sockets, uint socketsCount, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000060 RID: 96 RVA: 0x00002500 File Offset: 0x00000700
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_GetAddress(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_GetAddress_Injected(ref socket, address, errorState);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000250B File Offset: 0x0000070B
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_TCP_Listen(Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_TCP_Listen_Injected(ref socket, errorState);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002518 File Offset: 0x00000718
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Socket_Handle Baselib_Socket_TCP_Accept(Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Handle result;
			Binding.Baselib_Socket_TCP_Accept_Injected(ref socket, errorState, out result);
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002530 File Offset: 0x00000730
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_UDP_Send(Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_UDP_Send_Injected(ref socket, messages, messagesCount, errorState);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000253C File Offset: 0x0000073C
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_TCP_Send(Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_TCP_Send_Injected(ref socket, data, dataLen, errorState);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002548 File Offset: 0x00000748
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_UDP_Recv(Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_UDP_Recv_Injected(ref socket, messages, messagesCount, errorState);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002554 File Offset: 0x00000754
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_TCP_Recv(Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_TCP_Recv_Injected(ref socket, data, dataLen, errorState);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002560 File Offset: 0x00000760
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_Socket_Close(Binding.Baselib_Socket_Handle socket)
		{
			Binding.Baselib_Socket_Close_Injected(ref socket);
		}

		// Token: 0x06000068 RID: 104
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Thread_YieldExecution();

		// Token: 0x06000069 RID: 105
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Thread_GetCurrentThreadId();

		// Token: 0x0600006A RID: 106
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr Baselib_TLS_Alloc();

		// Token: 0x0600006B RID: 107
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_TLS_Free(UIntPtr handle);

		// Token: 0x0600006C RID: 108
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_TLS_Set(UIntPtr handle, UIntPtr value);

		// Token: 0x0600006D RID: 109
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr Baselib_TLS_Get(UIntPtr handle);

		// Token: 0x0600006E RID: 110 RVA: 0x0000256C File Offset: 0x0000076C
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_Timer_TickToNanosecondConversionRatio Baselib_Timer_GetTicksToNanosecondsConversionRatio()
		{
			Binding.Baselib_Timer_TickToNanosecondConversionRatio result;
			Binding.Baselib_Timer_GetTicksToNanosecondsConversionRatio_Injected(out result);
			return result;
		}

		// Token: 0x0600006F RID: 111
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Baselib_Timer_GetHighPrecisionTimerTicks();

		// Token: 0x06000070 RID: 112
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Timer_WaitForAtLeast(uint timeInMilliseconds);

		// Token: 0x06000071 RID: 113
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Baselib_Timer_GetTimeSinceStartupInSeconds();

		// Token: 0x06000072 RID: 114 RVA: 0x00002584 File Offset: 0x00000784
		// Note: this type is marked as 'beforefieldinit'.
		static Binding()
		{
		}

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_DynamicLibrary_OpenUtf8_Injected(byte* pathnameUtf8, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_DynamicLibrary_Handle ret);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_DynamicLibrary_OpenUtf16_Injected(char* pathnameUtf16, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_DynamicLibrary_Handle ret);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_DynamicLibrary_OpenProgramHandle_Injected(Binding.Baselib_ErrorState* errorState, out Binding.Baselib_DynamicLibrary_Handle ret);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_DynamicLibrary_FromNativeHandle_Injected(ulong handle, uint type, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_DynamicLibrary_Handle ret);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Baselib_DynamicLibrary_GetFunction_Injected(ref Binding.Baselib_DynamicLibrary_Handle handle, byte* functionName, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_DynamicLibrary_Close_Injected(ref Binding.Baselib_DynamicLibrary_Handle handle);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_FileIO_EventQueue_Create_Injected(out Binding.Baselib_FileIO_EventQueue ret);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_FileIO_EventQueue_Free_Injected(ref Binding.Baselib_FileIO_EventQueue eq);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong Baselib_FileIO_EventQueue_Dequeue_Injected(ref Binding.Baselib_FileIO_EventQueue eq, Binding.Baselib_FileIO_EventQueue_Result* results, ulong count, uint timeoutInMilliseconds);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_FileIO_EventQueue_Shutdown_Injected(ref Binding.Baselib_FileIO_EventQueue eq, uint threadCount);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_AsyncOpen_Injected(ref Binding.Baselib_FileIO_EventQueue eq, byte* pathname, ulong userdata, Binding.Baselib_FileIO_Priority priority, out Binding.Baselib_FileIO_AsyncFile ret);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_AsyncRead_Injected(ref Binding.Baselib_FileIO_AsyncFile file, Binding.Baselib_FileIO_ReadRequest* requests, ulong count, ulong userdata, Binding.Baselib_FileIO_Priority priority);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_FileIO_AsyncClose_Injected(ref Binding.Baselib_FileIO_AsyncFile file);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_SyncOpen_Injected(byte* pathname, Binding.Baselib_FileIO_OpenFlags openFlags, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_FileIO_SyncFile ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_FileIO_SyncFileFromNativeHandle_Injected(ulong handle, uint type, out Binding.Baselib_FileIO_SyncFile ret);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong Baselib_FileIO_SyncRead_Injected(ref Binding.Baselib_FileIO_SyncFile file, ulong offset, IntPtr buffer, ulong size, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong Baselib_FileIO_SyncWrite_Injected(ref Binding.Baselib_FileIO_SyncFile file, ulong offset, IntPtr buffer, ulong size, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_SyncFlush_Injected(ref Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_SyncSetFileSize_Injected(ref Binding.Baselib_FileIO_SyncFile file, ulong size, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong Baselib_FileIO_SyncGetFileSize_Injected(ref Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_FileIO_SyncClose_Injected(ref Binding.Baselib_FileIO_SyncFile file, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Memory_AllocatePages_Injected(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Memory_PageAllocation ret);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Memory_ReleasePages_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Buffer_Register_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Buffer ret);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size, out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_Create_Injected(Binding.Baselib_NetworkAddress* srcAddress, ref Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Socket_UDP ret);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_Create_Injected(Binding.Baselib_NetworkAddress_Family family, Binding.Baselib_Socket_Protocol protocol, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Socket_Handle ret);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_Bind_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Connect_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_GetAddress_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Listen_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Accept_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Socket_Handle ret);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_UDP_Send_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_TCP_Send_Injected(ref Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_UDP_Recv_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_TCP_Recv_Injected(ref Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_Socket_Close_Injected(ref Binding.Baselib_Socket_Handle socket);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_Timer_GetTicksToNanosecondsConversionRatio_Injected(out Binding.Baselib_Timer_TickToNanosecondConversionRatio ret);

		// Token: 0x04000027 RID: 39
		public static readonly UIntPtr Baselib_Memory_MaxAlignment = new UIntPtr(65536U);

		// Token: 0x04000028 RID: 40
		public static readonly UIntPtr Baselib_Memory_MinGuaranteedAlignment = new UIntPtr(8U);

		// Token: 0x04000029 RID: 41
		public const uint Baselib_NetworkAddress_IpMaxStringLength = 46U;

		// Token: 0x0400002A RID: 42
		public static readonly IntPtr Baselib_RegisteredNetwork_Buffer_Id_Invalid = IntPtr.Zero;

		// Token: 0x0400002B RID: 43
		public const uint Baselib_RegisteredNetwork_Endpoint_MaxSize = 28U;

		// Token: 0x0400002C RID: 44
		public static readonly IntPtr Baselib_Thread_InvalidId = IntPtr.Zero;

		// Token: 0x0400002D RID: 45
		public static readonly UIntPtr Baselib_Thread_MaxThreadNameLength = new UIntPtr(64U);

		// Token: 0x0400002E RID: 46
		public const uint Baselib_TLS_MinimumGuaranteedSlots = 100U;

		// Token: 0x0400002F RID: 47
		public const ulong Baselib_SecondsPerMinute = 60UL;

		// Token: 0x04000030 RID: 48
		public const ulong Baselib_MillisecondsPerSecond = 1000UL;

		// Token: 0x04000031 RID: 49
		public const ulong Baselib_MillisecondsPerMinute = 60000UL;

		// Token: 0x04000032 RID: 50
		public const ulong Baselib_MicrosecondsPerMillisecond = 1000UL;

		// Token: 0x04000033 RID: 51
		public const ulong Baselib_MicrosecondsPerSecond = 1000000UL;

		// Token: 0x04000034 RID: 52
		public const ulong Baselib_MicrosecondsPerMinute = 60000000UL;

		// Token: 0x04000035 RID: 53
		public const ulong Baselib_NanosecondsPerMicrosecond = 1000UL;

		// Token: 0x04000036 RID: 54
		public const ulong Baselib_NanosecondsPerMillisecond = 1000000UL;

		// Token: 0x04000037 RID: 55
		public const ulong Baselib_NanosecondsPerSecond = 1000000000UL;

		// Token: 0x04000038 RID: 56
		public const ulong Baselib_NanosecondsPerMinute = 60000000000UL;

		// Token: 0x04000039 RID: 57
		public const ulong Baselib_Timer_MaxNumberOfNanosecondsPerTick = 1000UL;

		// Token: 0x0400003A RID: 58
		public const double Baselib_Timer_MinNumberOfNanosecondsPerTick = 0.01;

		// Token: 0x0400003B RID: 59
		public static readonly Binding.Baselib_Memory_PageAllocation Baselib_Memory_PageAllocation_Invalid = default(Binding.Baselib_Memory_PageAllocation);

		// Token: 0x0400003C RID: 60
		public static readonly Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Invalid = default(Binding.Baselib_RegisteredNetwork_Socket_UDP);

		// Token: 0x0400003D RID: 61
		public static readonly Binding.Baselib_Socket_Handle Baselib_Socket_Handle_Invalid = new Binding.Baselib_Socket_Handle
		{
			handle = (IntPtr)(-1)
		};

		// Token: 0x0400003E RID: 62
		public static readonly Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_Handle_Invalid = new Binding.Baselib_DynamicLibrary_Handle
		{
			handle = (IntPtr)(-1)
		};

		// Token: 0x0400003F RID: 63
		public static readonly Binding.Baselib_FileIO_EventQueue Baselib_FileIO_EventQueue_Invalid = new Binding.Baselib_FileIO_EventQueue
		{
			handle = (IntPtr)0
		};

		// Token: 0x04000040 RID: 64
		public static readonly Binding.Baselib_FileIO_AsyncFile Baselib_FileIO_AsyncFile_Invalid = new Binding.Baselib_FileIO_AsyncFile
		{
			handle = (IntPtr)0
		};

		// Token: 0x04000041 RID: 65
		public static readonly Binding.Baselib_FileIO_SyncFile Baselib_FileIO_SyncFile_Invalid = new Binding.Baselib_FileIO_SyncFile
		{
			handle = (IntPtr)(-1)
		};

		// Token: 0x02000017 RID: 23
		public struct Baselib_DynamicLibrary_Handle
		{
			// Token: 0x04000042 RID: 66
			public IntPtr handle;
		}

		// Token: 0x02000018 RID: 24
		public enum Baselib_ErrorCode
		{
			// Token: 0x04000044 RID: 68
			Success,
			// Token: 0x04000045 RID: 69
			OutOfMemory = 16777216,
			// Token: 0x04000046 RID: 70
			OutOfSystemResources,
			// Token: 0x04000047 RID: 71
			InvalidAddressRange,
			// Token: 0x04000048 RID: 72
			InvalidArgument,
			// Token: 0x04000049 RID: 73
			InvalidBufferSize,
			// Token: 0x0400004A RID: 74
			InvalidState,
			// Token: 0x0400004B RID: 75
			NotSupported,
			// Token: 0x0400004C RID: 76
			Timeout,
			// Token: 0x0400004D RID: 77
			UnsupportedAlignment = 33554432,
			// Token: 0x0400004E RID: 78
			InvalidPageSize,
			// Token: 0x0400004F RID: 79
			InvalidPageCount,
			// Token: 0x04000050 RID: 80
			UnsupportedPageState,
			// Token: 0x04000051 RID: 81
			ThreadCannotJoinSelf = 50331648,
			// Token: 0x04000052 RID: 82
			NetworkInitializationError = 67108864,
			// Token: 0x04000053 RID: 83
			AddressInUse,
			// Token: 0x04000054 RID: 84
			AddressUnreachable,
			// Token: 0x04000055 RID: 85
			AddressFamilyNotSupported,
			// Token: 0x04000056 RID: 86
			Disconnected,
			// Token: 0x04000057 RID: 87
			InvalidPathname = 83886080,
			// Token: 0x04000058 RID: 88
			RequestedAccessIsNotAllowed,
			// Token: 0x04000059 RID: 89
			IOError,
			// Token: 0x0400005A RID: 90
			FailedToOpenDynamicLibrary = 100663296,
			// Token: 0x0400005B RID: 91
			FunctionNotFound,
			// Token: 0x0400005C RID: 92
			UnexpectedError = -1
		}

		// Token: 0x02000019 RID: 25
		public enum Baselib_ErrorState_NativeErrorCodeType : byte
		{
			// Token: 0x0400005E RID: 94
			None,
			// Token: 0x0400005F RID: 95
			PlatformDefined
		}

		// Token: 0x0200001A RID: 26
		public enum Baselib_ErrorState_ExtraInformationType : byte
		{
			// Token: 0x04000061 RID: 97
			None,
			// Token: 0x04000062 RID: 98
			StaticString,
			// Token: 0x04000063 RID: 99
			GenerationCounter
		}

		// Token: 0x0200001B RID: 27
		public struct Baselib_ErrorState
		{
			// Token: 0x04000064 RID: 100
			public Binding.Baselib_SourceLocation sourceLocation;

			// Token: 0x04000065 RID: 101
			public ulong nativeErrorCode;

			// Token: 0x04000066 RID: 102
			public ulong extraInformation;

			// Token: 0x04000067 RID: 103
			public Binding.Baselib_ErrorCode code;

			// Token: 0x04000068 RID: 104
			public Binding.Baselib_ErrorState_NativeErrorCodeType nativeErrorCodeType;

			// Token: 0x04000069 RID: 105
			public Binding.Baselib_ErrorState_ExtraInformationType extraInformationType;
		}

		// Token: 0x0200001C RID: 28
		public enum Baselib_ErrorState_ExplainVerbosity
		{
			// Token: 0x0400006B RID: 107
			ErrorType,
			// Token: 0x0400006C RID: 108
			ErrorType_SourceLocation_Explanation
		}

		// Token: 0x0200001D RID: 29
		public struct Baselib_FileIO_EventQueue
		{
			// Token: 0x0400006D RID: 109
			public IntPtr handle;
		}

		// Token: 0x0200001E RID: 30
		public struct Baselib_FileIO_AsyncFile
		{
			// Token: 0x0400006E RID: 110
			public IntPtr handle;
		}

		// Token: 0x0200001F RID: 31
		public struct Baselib_FileIO_SyncFile
		{
			// Token: 0x0400006F RID: 111
			public IntPtr handle;
		}

		// Token: 0x02000020 RID: 32
		public enum Baselib_FileIO_OpenFlags : uint
		{
			// Token: 0x04000071 RID: 113
			Read = 1U,
			// Token: 0x04000072 RID: 114
			Write,
			// Token: 0x04000073 RID: 115
			OpenAlways = 4U,
			// Token: 0x04000074 RID: 116
			CreateAlways = 8U
		}

		// Token: 0x02000021 RID: 33
		public struct Baselib_FileIO_ReadRequest
		{
			// Token: 0x04000075 RID: 117
			public ulong offset;

			// Token: 0x04000076 RID: 118
			public IntPtr buffer;

			// Token: 0x04000077 RID: 119
			public ulong size;
		}

		// Token: 0x02000022 RID: 34
		public enum Baselib_FileIO_Priority
		{
			// Token: 0x04000079 RID: 121
			Normal,
			// Token: 0x0400007A RID: 122
			High
		}

		// Token: 0x02000023 RID: 35
		public enum Baselib_FileIO_EventQueue_ResultType
		{
			// Token: 0x0400007C RID: 124
			Baselib_FileIO_EventQueue_Callback = 1,
			// Token: 0x0400007D RID: 125
			Baselib_FileIO_EventQueue_OpenFile,
			// Token: 0x0400007E RID: 126
			Baselib_FileIO_EventQueue_ReadFile,
			// Token: 0x0400007F RID: 127
			Baselib_FileIO_EventQueue_CloseFile
		}

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x060000A9 RID: 169
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void EventQueueCallback(ulong arg0);

		// Token: 0x02000025 RID: 37
		public struct Baselib_FileIO_EventQueue_Result_Callback
		{
			// Token: 0x04000080 RID: 128
			public IntPtr callback;
		}

		// Token: 0x02000026 RID: 38
		public struct Baselib_FileIO_EventQueue_Result_OpenFile
		{
			// Token: 0x04000081 RID: 129
			public ulong fileSize;
		}

		// Token: 0x02000027 RID: 39
		public struct Baselib_FileIO_EventQueue_Result_ReadFile
		{
			// Token: 0x04000082 RID: 130
			public ulong bytesTransferred;
		}

		// Token: 0x02000028 RID: 40
		[StructLayout(LayoutKind.Explicit)]
		public struct Baselib_FileIO_EventQueue_Result
		{
			// Token: 0x04000083 RID: 131
			[FieldOffset(0)]
			public Binding.Baselib_FileIO_EventQueue_ResultType type;

			// Token: 0x04000084 RID: 132
			[FieldOffset(8)]
			public ulong userdata;

			// Token: 0x04000085 RID: 133
			[FieldOffset(16)]
			public Binding.Baselib_ErrorState errorState;

			// Token: 0x04000086 RID: 134
			[FieldOffset(64)]
			public Binding.Baselib_FileIO_EventQueue_Result_Callback callback;

			// Token: 0x04000087 RID: 135
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(64)]
			public Binding.Baselib_FileIO_EventQueue_Result_OpenFile openFile;

			// Token: 0x04000088 RID: 136
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(64)]
			public Binding.Baselib_FileIO_EventQueue_Result_ReadFile readFile;
		}

		// Token: 0x02000029 RID: 41
		public struct Baselib_Memory_PageSizeInfo
		{
			// Token: 0x04000089 RID: 137
			public ulong defaultPageSize;

			// Token: 0x0400008A RID: 138
			public ulong pageSizes0;

			// Token: 0x0400008B RID: 139
			public ulong pageSizes1;

			// Token: 0x0400008C RID: 140
			public ulong pageSizes2;

			// Token: 0x0400008D RID: 141
			public ulong pageSizes3;

			// Token: 0x0400008E RID: 142
			public ulong pageSizes4;

			// Token: 0x0400008F RID: 143
			public ulong pageSizes5;

			// Token: 0x04000090 RID: 144
			public ulong pageSizesLen;
		}

		// Token: 0x0200002A RID: 42
		public struct Baselib_Memory_PageAllocation
		{
			// Token: 0x04000091 RID: 145
			public IntPtr ptr;

			// Token: 0x04000092 RID: 146
			public ulong pageSize;

			// Token: 0x04000093 RID: 147
			public ulong pageCount;
		}

		// Token: 0x0200002B RID: 43
		public enum Baselib_Memory_PageState
		{
			// Token: 0x04000095 RID: 149
			Reserved,
			// Token: 0x04000096 RID: 150
			NoAccess,
			// Token: 0x04000097 RID: 151
			ReadOnly,
			// Token: 0x04000098 RID: 152
			ReadWrite = 4,
			// Token: 0x04000099 RID: 153
			ReadOnly_Executable = 18,
			// Token: 0x0400009A RID: 154
			ReadWrite_Executable = 20
		}

		// Token: 0x0200002C RID: 44
		public enum Baselib_NetworkAddress_Family
		{
			// Token: 0x0400009C RID: 156
			Invalid,
			// Token: 0x0400009D RID: 157
			IPv4,
			// Token: 0x0400009E RID: 158
			IPv6
		}

		// Token: 0x0200002D RID: 45
		[StructLayout(LayoutKind.Explicit)]
		public struct Baselib_NetworkAddress
		{
			// Token: 0x0400009F RID: 159
			[FieldOffset(0)]
			public byte data0;

			// Token: 0x040000A0 RID: 160
			[FieldOffset(1)]
			public byte data1;

			// Token: 0x040000A1 RID: 161
			[FieldOffset(2)]
			public byte data2;

			// Token: 0x040000A2 RID: 162
			[FieldOffset(3)]
			public byte data3;

			// Token: 0x040000A3 RID: 163
			[FieldOffset(4)]
			public byte data4;

			// Token: 0x040000A4 RID: 164
			[FieldOffset(5)]
			public byte data5;

			// Token: 0x040000A5 RID: 165
			[FieldOffset(6)]
			public byte data6;

			// Token: 0x040000A6 RID: 166
			[FieldOffset(7)]
			public byte data7;

			// Token: 0x040000A7 RID: 167
			[FieldOffset(8)]
			public byte data8;

			// Token: 0x040000A8 RID: 168
			[FieldOffset(9)]
			public byte data9;

			// Token: 0x040000A9 RID: 169
			[FieldOffset(10)]
			public byte data10;

			// Token: 0x040000AA RID: 170
			[FieldOffset(11)]
			public byte data11;

			// Token: 0x040000AB RID: 171
			[FieldOffset(12)]
			public byte data12;

			// Token: 0x040000AC RID: 172
			[FieldOffset(13)]
			public byte data13;

			// Token: 0x040000AD RID: 173
			[FieldOffset(14)]
			public byte data14;

			// Token: 0x040000AE RID: 174
			[FieldOffset(15)]
			public byte data15;

			// Token: 0x040000AF RID: 175
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(0)]
			public byte ipv6_0;

			// Token: 0x040000B0 RID: 176
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(1)]
			public byte ipv6_1;

			// Token: 0x040000B1 RID: 177
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(2)]
			public byte ipv6_2;

			// Token: 0x040000B2 RID: 178
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(3)]
			public byte ipv6_3;

			// Token: 0x040000B3 RID: 179
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(4)]
			public byte ipv6_4;

			// Token: 0x040000B4 RID: 180
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(5)]
			public byte ipv6_5;

			// Token: 0x040000B5 RID: 181
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(6)]
			public byte ipv6_6;

			// Token: 0x040000B6 RID: 182
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(7)]
			public byte ipv6_7;

			// Token: 0x040000B7 RID: 183
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(8)]
			public byte ipv6_8;

			// Token: 0x040000B8 RID: 184
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(9)]
			public byte ipv6_9;

			// Token: 0x040000B9 RID: 185
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(10)]
			public byte ipv6_10;

			// Token: 0x040000BA RID: 186
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(11)]
			public byte ipv6_11;

			// Token: 0x040000BB RID: 187
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(12)]
			public byte ipv6_12;

			// Token: 0x040000BC RID: 188
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(13)]
			public byte ipv6_13;

			// Token: 0x040000BD RID: 189
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(14)]
			public byte ipv6_14;

			// Token: 0x040000BE RID: 190
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(15)]
			public byte ipv6_15;

			// Token: 0x040000BF RID: 191
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(0)]
			public byte ipv4_0;

			// Token: 0x040000C0 RID: 192
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(1)]
			public byte ipv4_1;

			// Token: 0x040000C1 RID: 193
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(2)]
			public byte ipv4_2;

			// Token: 0x040000C2 RID: 194
			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(3)]
			public byte ipv4_3;

			// Token: 0x040000C3 RID: 195
			[FieldOffset(16)]
			public byte port0;

			// Token: 0x040000C4 RID: 196
			[FieldOffset(17)]
			public byte port1;

			// Token: 0x040000C5 RID: 197
			[FieldOffset(18)]
			public byte family;

			// Token: 0x040000C6 RID: 198
			[FieldOffset(19)]
			public byte _padding;

			// Token: 0x040000C7 RID: 199
			[FieldOffset(20)]
			public uint ipv6_scope_id;
		}

		// Token: 0x0200002E RID: 46
		public enum Baselib_NetworkAddress_AddressReuse
		{
			// Token: 0x040000C9 RID: 201
			DoNotAllow,
			// Token: 0x040000CA RID: 202
			Allow
		}

		// Token: 0x0200002F RID: 47
		public struct Baselib_RegisteredNetwork_Buffer
		{
			// Token: 0x040000CB RID: 203
			public IntPtr id;

			// Token: 0x040000CC RID: 204
			public Binding.Baselib_Memory_PageAllocation allocation;
		}

		// Token: 0x02000030 RID: 48
		public struct Baselib_RegisteredNetwork_BufferSlice
		{
			// Token: 0x040000CD RID: 205
			public IntPtr id;

			// Token: 0x040000CE RID: 206
			public IntPtr data;

			// Token: 0x040000CF RID: 207
			public uint size;

			// Token: 0x040000D0 RID: 208
			public uint offset;
		}

		// Token: 0x02000031 RID: 49
		public struct Baselib_RegisteredNetwork_Endpoint
		{
			// Token: 0x040000D1 RID: 209
			public Binding.Baselib_RegisteredNetwork_BufferSlice slice;
		}

		// Token: 0x02000032 RID: 50
		public struct Baselib_RegisteredNetwork_Request
		{
			// Token: 0x040000D2 RID: 210
			public Binding.Baselib_RegisteredNetwork_BufferSlice payload;

			// Token: 0x040000D3 RID: 211
			public Binding.Baselib_RegisteredNetwork_Endpoint remoteEndpoint;

			// Token: 0x040000D4 RID: 212
			public IntPtr requestUserdata;
		}

		// Token: 0x02000033 RID: 51
		public enum Baselib_RegisteredNetwork_CompletionStatus
		{
			// Token: 0x040000D6 RID: 214
			Failed,
			// Token: 0x040000D7 RID: 215
			Success
		}

		// Token: 0x02000034 RID: 52
		public struct Baselib_RegisteredNetwork_CompletionResult
		{
			// Token: 0x040000D8 RID: 216
			public Binding.Baselib_RegisteredNetwork_CompletionStatus status;

			// Token: 0x040000D9 RID: 217
			public uint bytesTransferred;

			// Token: 0x040000DA RID: 218
			public IntPtr requestUserdata;
		}

		// Token: 0x02000035 RID: 53
		public struct Baselib_RegisteredNetwork_Socket_UDP
		{
			// Token: 0x040000DB RID: 219
			public IntPtr handle;
		}

		// Token: 0x02000036 RID: 54
		public enum Baselib_RegisteredNetwork_ProcessStatus
		{
			// Token: 0x040000DD RID: 221
			NonePendingImmediately,
			// Token: 0x040000DE RID: 222
			Done = 0,
			// Token: 0x040000DF RID: 223
			Pending
		}

		// Token: 0x02000037 RID: 55
		public enum Baselib_RegisteredNetwork_CompletionQueueStatus
		{
			// Token: 0x040000E1 RID: 225
			NoResultsAvailable,
			// Token: 0x040000E2 RID: 226
			ResultsAvailable
		}

		// Token: 0x02000038 RID: 56
		public struct Baselib_Socket_Handle
		{
			// Token: 0x040000E3 RID: 227
			public IntPtr handle;
		}

		// Token: 0x02000039 RID: 57
		public enum Baselib_Socket_Protocol
		{
			// Token: 0x040000E5 RID: 229
			UDP = 1,
			// Token: 0x040000E6 RID: 230
			TCP
		}

		// Token: 0x0200003A RID: 58
		public struct Baselib_Socket_Message
		{
			// Token: 0x040000E7 RID: 231
			public unsafe Binding.Baselib_NetworkAddress* address;

			// Token: 0x040000E8 RID: 232
			public IntPtr data;

			// Token: 0x040000E9 RID: 233
			public uint dataLen;
		}

		// Token: 0x0200003B RID: 59
		public enum Baselib_Socket_PollEvents
		{
			// Token: 0x040000EB RID: 235
			Readable = 1,
			// Token: 0x040000EC RID: 236
			Writable,
			// Token: 0x040000ED RID: 237
			Connected = 4
		}

		// Token: 0x0200003C RID: 60
		public struct Baselib_Socket_PollFd
		{
			// Token: 0x040000EE RID: 238
			public Binding.Baselib_Socket_Handle handle;

			// Token: 0x040000EF RID: 239
			public Binding.Baselib_Socket_PollEvents requestedEvents;

			// Token: 0x040000F0 RID: 240
			public Binding.Baselib_Socket_PollEvents resultEvents;

			// Token: 0x040000F1 RID: 241
			public unsafe Binding.Baselib_ErrorState* errorState;
		}

		// Token: 0x0200003D RID: 61
		public struct Baselib_SourceLocation
		{
			// Token: 0x040000F2 RID: 242
			public unsafe byte* file;

			// Token: 0x040000F3 RID: 243
			public unsafe byte* function;

			// Token: 0x040000F4 RID: 244
			public uint lineNumber;
		}

		// Token: 0x0200003E RID: 62
		public struct Baselib_Timer_TickToNanosecondConversionRatio
		{
			// Token: 0x040000F5 RID: 245
			public ulong ticksToNanosecondsNumerator;

			// Token: 0x040000F6 RID: 246
			public ulong ticksToNanosecondsDenominator;
		}
	}
}
