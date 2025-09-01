using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
	// Token: 0x02000203 RID: 515
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060018FE RID: 6398 RVA: 0x00074640 File Offset: 0x00072840
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this._exception = exception;
		}

		/// <summary>Gets the collection of warnings sent from the server.</summary>
		/// <returns>The collection of warnings sent from the server.</returns>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0007464F File Offset: 0x0007284F
		public SqlErrorCollection Errors
		{
			get
			{
				return this._exception.Errors;
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0007465C File Offset: 0x0007285C
		private bool ShouldSerializeErrors()
		{
			return this._exception != null && 0 < this._exception.Errors.Count;
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0007467B File Offset: 0x0007287B
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00074688 File Offset: 0x00072888
		public string Source
		{
			get
			{
				return this._exception.Source;
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</returns>
		// Token: 0x06001903 RID: 6403 RVA: 0x00074695 File Offset: 0x00072895
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal SqlInfoMessageEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400103E RID: 4158
		private SqlException _exception;
	}
}
