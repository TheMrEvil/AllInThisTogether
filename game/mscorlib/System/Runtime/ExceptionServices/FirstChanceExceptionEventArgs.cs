using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Provides data for the notification event that is raised when a managed exception first occurs, before the common language runtime begins searching for event handlers.</summary>
	// Token: 0x020007D0 RID: 2000
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> class with a specified exception.</summary>
		/// <param name="exception">The exception that was just thrown by managed code, and that will be examined by the <see cref="E:System.AppDomain.UnhandledException" /> event.</param>
		// Token: 0x060045A8 RID: 17832 RVA: 0x000E5047 File Offset: 0x000E3247
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}

		/// <summary>The managed exception object that corresponds to the exception thrown in managed code.</summary>
		/// <returns>The newly thrown exception.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x000E5056 File Offset: 0x000E3256
		public Exception Exception
		{
			[CompilerGenerated]
			get
			{
				return this.<Exception>k__BackingField;
			}
		}

		// Token: 0x04002D13 RID: 11539
		[CompilerGenerated]
		private readonly Exception <Exception>k__BackingField;
	}
}
