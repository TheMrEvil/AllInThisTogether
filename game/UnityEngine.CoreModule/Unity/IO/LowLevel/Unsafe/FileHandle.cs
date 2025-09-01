using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200007B RID: 123
	public readonly struct FileHandle
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003A20 File Offset: 0x00001C20
		public FileStatus Status
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.Status cannot be called on a closed FileHandle");
				}
				return FileHandle.GetFileStatus_Internal(this);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003A50 File Offset: 0x00001C50
		public JobHandle JobHandle
		{
			get
			{
				bool flag = !FileHandle.IsFileHandleValid(this);
				if (flag)
				{
					throw new InvalidOperationException("FileHandle.JobHandle cannot be called on a closed FileHandle");
				}
				return FileHandle.GetJobHandle_Internal(this);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003A80 File Offset: 0x00001C80
		public bool IsValid()
		{
			return FileHandle.IsFileHandleValid(this);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00003A98 File Offset: 0x00001C98
		public JobHandle Close(JobHandle dependency = default(JobHandle))
		{
			bool flag = !FileHandle.IsFileHandleValid(this);
			if (flag)
			{
				throw new InvalidOperationException("FileHandle.Close cannot be called twice on the same FileHandle");
			}
			return AsyncReadManager.CloseFileAsync(this, dependency);
		}

		// Token: 0x060001C0 RID: 448
		[FreeFunction("AsyncReadManagerManaged::IsFileHandleValid")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFileHandleValid(in FileHandle handle);

		// Token: 0x060001C1 RID: 449
		[FreeFunction("AsyncReadManagerManaged::GetFileStatusFromManagedHandle")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FileStatus GetFileStatus_Internal(in FileHandle handle);

		// Token: 0x060001C2 RID: 450 RVA: 0x00003ACC File Offset: 0x00001CCC
		[FreeFunction("AsyncReadManagerManaged::GetJobFenceFromManagedHandle")]
		private static JobHandle GetJobHandle_Internal(in FileHandle handle)
		{
			JobHandle result;
			FileHandle.GetJobHandle_Internal_Injected(handle, out result);
			return result;
		}

		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Internal_Injected(in FileHandle handle, out JobHandle ret);

		// Token: 0x040001CF RID: 463
		[NativeDisableUnsafePtrRestriction]
		internal readonly IntPtr fileCommandPtr;

		// Token: 0x040001D0 RID: 464
		internal readonly int version;
	}
}
