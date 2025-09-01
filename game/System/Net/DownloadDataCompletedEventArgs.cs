using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
	// Token: 0x020005BA RID: 1466
	public class DownloadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x000A50E2 File Offset: 0x000A32E2
		internal DownloadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the downloaded data.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000A50F5 File Offset: 0x000A32F5
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal DownloadDataCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A48 RID: 6728
		private readonly byte[] _result;
	}
}
