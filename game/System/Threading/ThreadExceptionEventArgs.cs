using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	// Token: 0x02000180 RID: 384
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred.</param>
		// Token: 0x06000A52 RID: 2642 RVA: 0x0002D105 File Offset: 0x0002B305
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002D114 File Offset: 0x0002B314
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006DC RID: 1756
		private Exception exception;
	}
}
