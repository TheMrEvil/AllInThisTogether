using System;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	// Token: 0x02000250 RID: 592
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x0004EA54 File Offset: 0x0004CC54
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0004EA63 File Offset: 0x0004CC63
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal DataReceivedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A98 RID: 2712
		private string data;
	}
}
