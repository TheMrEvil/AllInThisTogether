using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	// Token: 0x0200050A RID: 1290
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred.</param>
		// Token: 0x060029EA RID: 10730 RVA: 0x0009027E File Offset: 0x0008E47E
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		// Token: 0x060029EB RID: 10731 RVA: 0x0009028D File Offset: 0x0008E48D
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x04001637 RID: 5687
		private Exception exception;
	}
}
