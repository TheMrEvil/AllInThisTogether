using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
	// Token: 0x020005B9 RID: 1465
	public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FB4 RID: 12212 RVA: 0x000A50C1 File Offset: 0x000A32C1
		internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the downloaded data.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000A50D4 File Offset: 0x000A32D4
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal DownloadStringCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A47 RID: 6727
		private readonly string _result;
	}
}
