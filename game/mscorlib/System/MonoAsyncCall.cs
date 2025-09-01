using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200023D RID: 573
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoAsyncCall
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x0000259F File Offset: 0x0000079F
		public MonoAsyncCall()
		{
		}

		// Token: 0x04001721 RID: 5921
		private object msg;

		// Token: 0x04001722 RID: 5922
		private IntPtr cb_method;

		// Token: 0x04001723 RID: 5923
		private object cb_target;

		// Token: 0x04001724 RID: 5924
		private object state;

		// Token: 0x04001725 RID: 5925
		private object res;

		// Token: 0x04001726 RID: 5926
		private object out_args;
	}
}
