using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
	// Token: 0x020005BE RID: 1470
	public class UploadValuesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000A5166 File Offset: 0x000A3366
		internal UploadValuesCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadValuesAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000A5179 File Offset: 0x000A3379
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal UploadValuesCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A4C RID: 6732
		private readonly byte[] _result;
	}
}
