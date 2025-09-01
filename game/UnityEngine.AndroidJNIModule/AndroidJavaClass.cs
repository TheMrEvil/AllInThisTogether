using System;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000045A5 File Offset: 0x000027A5
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000045B8 File Offset: 0x000027B8
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(intPtr);
			this.m_jobject = null;
			AndroidJNISafe.DeleteLocalRef(intPtr);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004604 File Offset: 0x00002804
		internal AndroidJavaClass(IntPtr jclass)
		{
			bool flag = jclass == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			this.m_jobject = null;
		}
	}
}
