using System;
using System.ComponentModel;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
	// Token: 0x020005B7 RID: 1463
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FAE RID: 12206 RVA: 0x000A507F File Offset: 0x000A327F
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets a readable stream that contains data downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that contains the downloaded data.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000A5092 File Offset: 0x000A3292
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal OpenReadCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A45 RID: 6725
		private readonly Stream _result;
	}
}
