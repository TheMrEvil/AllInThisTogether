using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007C RID: 124
	public struct ReadHandle : IDisposable
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public bool IsValid()
		{
			return ReadHandle.IsReadHandleValid(this);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00003B04 File Offset: 0x00001D04
		public void Dispose()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			bool flag2 = this.Status == ReadStatus.InProgress;
			if (flag2)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReadHandle.ReleaseReadHandle(this);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003B54 File Offset: 0x00001D54
		public void Cancel()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.Cancel cannot be called on a disposed ReadHandle");
			}
			ReadHandle.CancelInternal(this);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00003B8B File Offset: 0x00001D8B
		[FreeFunction("AsyncReadManagerManaged::CancelReadRequest")]
		private static void CancelInternal(ReadHandle handle)
		{
			ReadHandle.CancelInternal_Injected(ref handle);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003B94 File Offset: 0x00001D94
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return ReadHandle.GetJobHandle(this);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public ReadStatus Status
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadStatus(this);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003C0C File Offset: 0x00001E0C
		public long ReadCount
		{
			get
			{
				bool flag = !ReadHandle.IsReadHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("Cannot use a ReadHandle that has been disposed");
				}
				return ReadHandle.GetReadCount(this);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003C48 File Offset: 0x00001E48
		public long GetBytesRead()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesRead(this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00003C84 File Offset: 0x00001E84
		public long GetBytesRead(uint readCommandIndex)
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesRead cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadForCommand(this, readCommandIndex);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public unsafe ulong* GetBytesReadArray()
		{
			bool flag = !ReadHandle.IsReadHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("ReadHandle.GetBytesReadArray cannot be called after the ReadHandle has been disposed");
			}
			return ReadHandle.GetBytesReadArray(this);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00003CFA File Offset: 0x00001EFA
		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return ReadHandle.GetReadStatus_Injected(ref handle);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00003D03 File Offset: 0x00001F03
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetReadCount", IsThreadSafe = true)]
		private static long GetReadCount(ReadHandle handle)
		{
			return ReadHandle.GetReadCount_Injected(ref handle);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003D0C File Offset: 0x00001F0C
		[FreeFunction("AsyncReadManagerManaged::GetBytesRead", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static long GetBytesRead(ReadHandle handle)
		{
			return ReadHandle.GetBytesRead_Injected(ref handle);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003D15 File Offset: 0x00001F15
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadForCommand", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static long GetBytesReadForCommand(ReadHandle handle, uint readCommandIndex)
		{
			return ReadHandle.GetBytesReadForCommand_Injected(ref handle, readCommandIndex);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00003D1F File Offset: 0x00001F1F
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::GetBytesReadArray", IsThreadSafe = true)]
		private unsafe static ulong* GetBytesReadArray(ReadHandle handle)
		{
			return ReadHandle.GetBytesReadArray_Injected(ref handle);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00003D28 File Offset: 0x00001F28
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReadHandle.ReleaseReadHandle_Injected(ref handle);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00003D31 File Offset: 0x00001F31
		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return ReadHandle.IsReadHandleValid_Injected(ref handle);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00003D3C File Offset: 0x00001F3C
		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			JobHandle result;
			ReadHandle.GetJobHandle_Injected(ref handle, out result);
			return result;
		}

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInternal_Injected(ref ReadHandle handle);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReadStatus GetReadStatus_Injected(ref ReadHandle handle);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetReadCount_Injected(ref ReadHandle handle);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetBytesRead_Injected(ref ReadHandle handle);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetBytesReadForCommand_Injected(ref ReadHandle handle, uint readCommandIndex);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ulong* GetBytesReadArray_Injected(ref ReadHandle handle);

		// Token: 0x060001DC RID: 476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseReadHandle_Injected(ref ReadHandle handle);

		// Token: 0x060001DD RID: 477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReadHandleValid_Injected(ref ReadHandle handle);

		// Token: 0x060001DE RID: 478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Injected(ref ReadHandle handle, out JobHandle ret);

		// Token: 0x040001D1 RID: 465
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		// Token: 0x040001D2 RID: 466
		internal int version;
	}
}
