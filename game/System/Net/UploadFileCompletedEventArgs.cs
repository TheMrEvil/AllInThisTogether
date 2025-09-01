using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
	// Token: 0x020005BD RID: 1469
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FC0 RID: 12224 RVA: 0x000A5145 File Offset: 0x000A3345
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000A5158 File Offset: 0x000A3358
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal UploadFileCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A4B RID: 6731
		private readonly byte[] _result;
	}
}
