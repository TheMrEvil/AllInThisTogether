using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public sealed class AndroidJavaException : Exception
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		internal AndroidJavaException(string message, string javaStackTrace) : base(message)
		{
			this.mJavaStackTrace = javaStackTrace;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002064 File Offset: 0x00000264
		public override string StackTrace
		{
			get
			{
				return this.mJavaStackTrace + base.StackTrace;
			}
		}

		// Token: 0x04000001 RID: 1
		private string mJavaStackTrace;
	}
}
