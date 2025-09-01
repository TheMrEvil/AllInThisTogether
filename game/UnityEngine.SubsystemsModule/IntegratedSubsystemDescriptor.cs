using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[UsedByNativeCode("SubsystemDescriptorBase")]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptorImpl, ISubsystemDescriptor
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020CF File Offset: 0x000002CF
		public string id
		{
			get
			{
				return SubsystemDescriptorBindings.GetId(this.m_Ptr);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020DC File Offset: 0x000002DC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020E4 File Offset: 0x000002E4
		IntPtr ISubsystemDescriptorImpl.ptr
		{
			get
			{
				return this.m_Ptr;
			}
			set
			{
				this.m_Ptr = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020ED File Offset: 0x000002ED
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x06000013 RID: 19
		internal abstract ISubsystem CreateImpl();

		// Token: 0x06000014 RID: 20 RVA: 0x000020A8 File Offset: 0x000002A8
		protected IntegratedSubsystemDescriptor()
		{
		}

		// Token: 0x04000003 RID: 3
		internal IntPtr m_Ptr;
	}
}
