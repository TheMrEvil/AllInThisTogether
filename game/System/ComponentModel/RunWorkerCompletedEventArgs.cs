using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodName<see langword="Completed" /> event.</summary>
	// Token: 0x0200041F RID: 1055
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> class.</summary>
		/// <param name="result">The result of an asynchronous operation.</param>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		// Token: 0x06002255 RID: 8789 RVA: 0x000778A2 File Offset: 0x00075AA2
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled) : base(error, cancelled, null)
		{
			this.result = result;
		}

		/// <summary>Gets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> is not <see langword="null" />. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> is <see langword="true" />.</exception>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000778B4 File Offset: 0x00075AB4
		public object Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		/// <summary>Gets a value that represents the user state.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the user state.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000778C2 File Offset: 0x00075AC2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object UserState
		{
			get
			{
				return base.UserState;
			}
		}

		// Token: 0x0400106C RID: 4204
		private object result;
	}
}
