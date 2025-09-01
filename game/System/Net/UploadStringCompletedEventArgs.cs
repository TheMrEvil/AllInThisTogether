using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
	// Token: 0x020005BB RID: 1467
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FBA RID: 12218 RVA: 0x000A5103 File Offset: 0x000A3303
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a string upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000A5116 File Offset: 0x000A3316
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal UploadStringCompletedEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A49 RID: 6729
		private readonly string _result;
	}
}
