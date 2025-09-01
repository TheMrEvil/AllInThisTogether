using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000028 RID: 40
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006C3B File Offset: 0x00004E3B
		internal bool HasMessages
		{
			get
			{
				return this._errors != null || this._warnings != null;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003E32 File Offset: 0x00002032
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006C50 File Offset: 0x00004E50
		protected virtual void DispatchMessages()
		{
			SqlException ex = this.ProcessMessages(true);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006C6C File Offset: 0x00004E6C
		protected SqlException ProcessMessages(bool ignoreWarnings)
		{
			SqlException result = null;
			SqlErrorCollection sqlErrorCollection = null;
			if (this._errors != null)
			{
				if (this._warnings != null)
				{
					foreach (object obj in this._warnings)
					{
						SqlError error = (SqlError)obj;
						this._errors.Add(error);
					}
				}
				sqlErrorCollection = this._errors;
				this._errors = null;
				this._warnings = null;
			}
			else
			{
				if (!ignoreWarnings)
				{
					sqlErrorCollection = this._warnings;
				}
				this._warnings = null;
			}
			if (sqlErrorCollection != null)
			{
				result = SqlException.CreateException(sqlErrorCollection, this.ServerVersion);
			}
			return result;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006D1C File Offset: 0x00004F1C
		internal void ProcessMessagesAndThrow()
		{
			if (this.HasMessages)
			{
				this.DispatchMessages();
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x0400044F RID: 1103
		private SqlErrorCollection _errors;

		// Token: 0x04000450 RID: 1104
		private SqlErrorCollection _warnings;
	}
}
