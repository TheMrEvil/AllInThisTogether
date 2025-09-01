using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	internal class GlobalJavaObjectRef
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002087 File Offset: 0x00000287
		public GlobalJavaObjectRef(IntPtr jobject)
		{
			this.m_jobject = ((jobject == IntPtr.Zero) ? IntPtr.Zero : AndroidJNI.NewGlobalRef(jobject));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000002B8
		~GlobalJavaObjectRef()
		{
			this.Dispose();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		public static implicit operator IntPtr(GlobalJavaObjectRef obj)
		{
			return obj.m_jobject;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002100 File Offset: 0x00000300
		public void Dispose()
		{
			bool disposed = this.m_disposed;
			if (!disposed)
			{
				this.m_disposed = true;
				bool flag = this.m_jobject != IntPtr.Zero;
				if (flag)
				{
					AndroidJNISafe.DeleteGlobalRef(this.m_jobject);
				}
			}
		}

		// Token: 0x04000002 RID: 2
		private bool m_disposed = false;

		// Token: 0x04000003 RID: 3
		protected IntPtr m_jobject;
	}
}
