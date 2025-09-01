using System;

namespace System.Diagnostics
{
	// Token: 0x020009B1 RID: 2481
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x06005995 RID: 22933
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);

		// Token: 0x06005996 RID: 22934 RVA: 0x0000259F File Offset: 0x0000079F
		protected AssertFilter()
		{
		}
	}
}
