using System;
using System.Runtime.InteropServices;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000010 RID: 16
	internal class NativeStr : IDisposable
	{
		// Token: 0x1700002C RID: 44
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002678 File Offset: 0x00000878
		public string Str
		{
			set
			{
				this.m_Str = value;
				this.Dispose();
				bool flag = value != null;
				if (flag)
				{
					this.m_MarshalledString = Marshal.StringToHGlobalUni(this.m_Str);
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000026B0 File Offset: 0x000008B0
		public IntPtr Ptr
		{
			get
			{
				return this.m_MarshalledString;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026C8 File Offset: 0x000008C8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026DC File Offset: 0x000008DC
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_MarshalledString != IntPtr.Zero;
			if (flag)
			{
				Marshal.FreeHGlobal(this.m_MarshalledString);
				this.m_MarshalledString = IntPtr.Zero;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002718 File Offset: 0x00000918
		~NativeStr()
		{
			this.Dispose(false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000274C File Offset: 0x0000094C
		public NativeStr()
		{
		}

		// Token: 0x04000045 RID: 69
		private string m_Str = null;

		// Token: 0x04000046 RID: 70
		private IntPtr m_MarshalledString = IntPtr.Zero;
	}
}
