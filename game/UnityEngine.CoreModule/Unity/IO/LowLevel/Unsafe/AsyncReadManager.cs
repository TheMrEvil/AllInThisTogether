using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007D RID: 125
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00003D54 File Offset: 0x00001F54
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem)
		{
			ReadHandle result;
			AsyncReadManager.ReadInternal_Injected(filename, cmds, cmdCount, assetName, typeID, subsystem, out result);
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003D74 File Offset: 0x00001F74
		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount, string assetName = "", ulong typeID = 0UL, AssetLoadingSubsystem subsystem = AssetLoadingSubsystem.Scripts)
		{
			return AsyncReadManager.ReadInternal(filename, (void*)readCmds, readCmdCount, assetName, typeID, subsystem);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00003D94 File Offset: 0x00001F94
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetFileInfo", IsThreadSafe = true)]
		private unsafe static ReadHandle GetFileInfoInternal(string filename, void* cmd)
		{
			ReadHandle result;
			AsyncReadManager.GetFileInfoInternal_Injected(filename, cmd, out result);
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00003DAC File Offset: 0x00001FAC
		public unsafe static ReadHandle GetFileInfo(string filename, FileInfoResult* result)
		{
			bool flag = result == null;
			if (flag)
			{
				throw new NullReferenceException("GetFileInfo must have a valid FileInfoResult to write into.");
			}
			return AsyncReadManager.GetFileInfoInternal(filename, (void*)result);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00003DDC File Offset: 0x00001FDC
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativePtr", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativePtr(in FileHandle fileHandle, void* readCmdArray, JobHandle dependency)
		{
			ReadHandle result;
			AsyncReadManager.ReadWithHandlesInternal_NativePtr_Injected(fileHandle, readCmdArray, ref dependency, out result);
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00003DF8 File Offset: 0x00001FF8
		[FreeFunction("AsyncReadManagerManaged::ReadWithHandles_NativeCopy", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadWithHandlesInternal_NativeCopy(in FileHandle fileHandle, void* readCmdArray)
		{
			ReadHandle result;
			AsyncReadManager.ReadWithHandlesInternal_NativeCopy_Injected(fileHandle, readCmdArray, out result);
			return result;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00003E10 File Offset: 0x00002010
		public unsafe static ReadHandle ReadDeferred(in FileHandle fileHandle, ReadCommandArray* readCmdArray, JobHandle dependency)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativePtr(fileHandle, (void*)readCmdArray, dependency);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00003E44 File Offset: 0x00002044
		public static ReadHandle Read(in FileHandle fileHandle, ReadCommandArray readCmdArray)
		{
			bool flag = !fileHandle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("FileHandle is invalid and may not be read from.");
			}
			return AsyncReadManager.ReadWithHandlesInternal_NativeCopy(fileHandle, UnsafeUtility.AddressOf<ReadCommandArray>(ref readCmdArray));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003E7C File Offset: 0x0000207C
		[FreeFunction("AsyncReadManagerManaged::ScheduleOpenRequest", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static FileHandle OpenFileAsync_Internal(string fileName)
		{
			FileHandle result;
			AsyncReadManager.OpenFileAsync_Internal_Injected(fileName, out result);
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00003E94 File Offset: 0x00002094
		public static FileHandle OpenFileAsync(string fileName)
		{
			bool flag = fileName.Length == 0;
			if (flag)
			{
				throw new InvalidOperationException("FileName is empty");
			}
			return AsyncReadManager.OpenFileAsync_Internal(fileName);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00003EC4 File Offset: 0x000020C4
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseRequest", IsThreadSafe = true)]
		internal static JobHandle CloseFileAsync(in FileHandle fileHandle, JobHandle dependency)
		{
			JobHandle result;
			AsyncReadManager.CloseFileAsync_Injected(fileHandle, ref dependency, out result);
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00003EDC File Offset: 0x000020DC
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ScheduleCloseCachedFileRequest", IsThreadSafe = true)]
		public static JobHandle CloseCachedFileAsync(string fileName, JobHandle dependency = default(JobHandle))
		{
			JobHandle result;
			AsyncReadManager.CloseCachedFileAsync_Injected(fileName, ref dependency, out result);
			return result;
		}

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadInternal_Injected(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem, out ReadHandle ret);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetFileInfoInternal_Injected(string filename, void* cmd, out ReadHandle ret);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadWithHandlesInternal_NativePtr_Injected(in FileHandle fileHandle, void* readCmdArray, ref JobHandle dependency, out ReadHandle ret);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadWithHandlesInternal_NativeCopy_Injected(in FileHandle fileHandle, void* readCmdArray, out ReadHandle ret);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OpenFileAsync_Internal_Injected(string fileName, out FileHandle ret);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CloseFileAsync_Injected(in FileHandle fileHandle, ref JobHandle dependency, out JobHandle ret);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CloseCachedFileAsync_Injected(string fileName, ref JobHandle dependency = null, out JobHandle ret);
	}
}
