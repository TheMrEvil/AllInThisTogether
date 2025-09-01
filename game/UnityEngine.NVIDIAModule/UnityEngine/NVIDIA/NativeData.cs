using System;
using System.Runtime.InteropServices;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000F RID: 15
	internal class NativeData<T> : IDisposable where T : new()
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000258C File Offset: 0x0000078C
		public IntPtr Ptr
		{
			get
			{
				Marshal.StructureToPtr(this.Value, this.m_MarshalledValue, true);
				return this.m_MarshalledValue;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000025BC File Offset: 0x000007BC
		public NativeData()
		{
			this.m_MarshalledValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000025F6 File Offset: 0x000007F6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002608 File Offset: 0x00000808
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_MarshalledValue != IntPtr.Zero;
			if (flag)
			{
				Marshal.FreeHGlobal(this.m_MarshalledValue);
				this.m_MarshalledValue = IntPtr.Zero;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002644 File Offset: 0x00000844
		~NativeData()
		{
			this.Dispose(false);
		}

		// Token: 0x04000043 RID: 67
		private IntPtr m_MarshalledValue = IntPtr.Zero;

		// Token: 0x04000044 RID: 68
		public T Value = Activator.CreateInstance<T>();
	}
}
