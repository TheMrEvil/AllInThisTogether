using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Subsystems/Subsystem.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystem : ISubsystem
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetHandle(IntegratedSubsystem subsystem);

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Start();

		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x06000004 RID: 4 RVA: 0x00002050 File Offset: 0x00000250
		public void Destroy()
		{
			IntPtr ptr = this.m_Ptr;
			SubsystemManager.RemoveIntegratedSubsystemByPtr(this.m_Ptr);
			SubsystemBindings.DestroySubsystem(ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002083 File Offset: 0x00000283
		public bool running
		{
			get
			{
				return this.valid && this.IsRunning();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002096 File Offset: 0x00000296
		internal bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsRunning();

		// Token: 0x06000008 RID: 8 RVA: 0x000020A8 File Offset: 0x000002A8
		public IntegratedSubsystem()
		{
		}

		// Token: 0x04000001 RID: 1
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		internal ISubsystemDescriptor m_SubsystemDescriptor;
	}
}
