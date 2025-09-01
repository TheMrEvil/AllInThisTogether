using System;

namespace System
{
	/// <summary>Provides data for the event that is raised when there is an exception that is not handled in any application domain.</summary>
	// Token: 0x020001AA RID: 426
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnhandledExceptionEventArgs" /> class with the exception object and a common language runtime termination flag.</summary>
		/// <param name="exception">The exception that is not handled.</param>
		/// <param name="isTerminating">
		///   <see langword="true" /> if the runtime is terminating; otherwise, <see langword="false" />.</param>
		// Token: 0x06001263 RID: 4707 RVA: 0x000484F5 File Offset: 0x000466F5
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this._exception = exception;
			this._isTerminating = isTerminating;
		}

		/// <summary>Gets the unhandled exception object.</summary>
		/// <returns>The unhandled exception object.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004850B File Offset: 0x0004670B
		public object ExceptionObject
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Indicates whether the common language runtime is terminating.</summary>
		/// <returns>
		///   <see langword="true" /> if the runtime is terminating; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00048513 File Offset: 0x00046713
		public bool IsTerminating
		{
			get
			{
				return this._isTerminating;
			}
		}

		// Token: 0x04001364 RID: 4964
		private object _exception;

		// Token: 0x04001365 RID: 4965
		private bool _isTerminating;
	}
}
