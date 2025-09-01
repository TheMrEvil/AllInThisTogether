using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the set of arguments passed to the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventHandler" />.</summary>
	// Token: 0x02000189 RID: 393
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object.</summary>
		/// <param name="rowsCopied">An <see cref="T:System.Int64" /> that indicates the number of rows copied during the current bulk copy operation.</param>
		// Token: 0x0600141F RID: 5151 RVA: 0x0005B3B1 File Offset: 0x000595B1
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		/// <summary>Gets or sets a value that indicates whether the bulk copy operation should be aborted.</summary>
		/// <returns>
		///   <see langword="true" /> if the bulk copy operation should be aborted; otherwise <see langword="false" />.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0005B3C0 File Offset: 0x000595C0
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x0005B3C8 File Offset: 0x000595C8
		public bool Abort
		{
			get
			{
				return this._abort;
			}
			set
			{
				this._abort = value;
			}
		}

		/// <summary>Gets a value that returns the number of rows copied during the current bulk copy operation.</summary>
		/// <returns>
		///   <see langword="int" /> that returns the number of rows copied.</returns>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0005B3D1 File Offset: 0x000595D1
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04000CA7 RID: 3239
		private bool _abort;

		// Token: 0x04000CA8 RID: 3240
		private long _rowsCopied;
	}
}
