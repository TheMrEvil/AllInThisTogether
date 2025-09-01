using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x02000208 RID: 520
	internal class SessionData
	{
		// Token: 0x0600193A RID: 6458 RVA: 0x00074BEC File Offset: 0x00072DEC
		public SessionData(SessionData recoveryData)
		{
			this._initialDatabase = recoveryData._initialDatabase;
			this._initialCollation = recoveryData._initialCollation;
			this._initialLanguage = recoveryData._initialLanguage;
			this._resolvedAliases = recoveryData._resolvedAliases;
			for (int i = 0; i < 256; i++)
			{
				if (recoveryData._initialState[i] != null)
				{
					this._initialState[i] = (byte[])recoveryData._initialState[i].Clone();
				}
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00074C83 File Offset: 0x00072E83
		public SessionData()
		{
			this._resolvedAliases = new Dictionary<string, Tuple<string, string>>(2);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00074CB7 File Offset: 0x00072EB7
		public void Reset()
		{
			this._database = null;
			this._collation = null;
			this._language = null;
			if (this._deltaDirty)
			{
				this._delta = new SessionStateRecord[256];
				this._deltaDirty = false;
			}
			this._unrecoverableStatesCount = 0;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00074CF4 File Offset: 0x00072EF4
		[Conditional("DEBUG")]
		public void AssertUnrecoverableStateCountIsCorrect()
		{
			byte b = 0;
			foreach (SessionStateRecord sessionStateRecord in this._delta)
			{
				if (sessionStateRecord != null && !sessionStateRecord._recoverable)
				{
					b += 1;
				}
			}
		}

		// Token: 0x04001054 RID: 4180
		internal const int _maxNumberOfSessionStates = 256;

		// Token: 0x04001055 RID: 4181
		internal uint _tdsVersion;

		// Token: 0x04001056 RID: 4182
		internal bool _encrypted;

		// Token: 0x04001057 RID: 4183
		internal string _database;

		// Token: 0x04001058 RID: 4184
		internal SqlCollation _collation;

		// Token: 0x04001059 RID: 4185
		internal string _language;

		// Token: 0x0400105A RID: 4186
		internal string _initialDatabase;

		// Token: 0x0400105B RID: 4187
		internal SqlCollation _initialCollation;

		// Token: 0x0400105C RID: 4188
		internal string _initialLanguage;

		// Token: 0x0400105D RID: 4189
		internal byte _unrecoverableStatesCount;

		// Token: 0x0400105E RID: 4190
		internal Dictionary<string, Tuple<string, string>> _resolvedAliases;

		// Token: 0x0400105F RID: 4191
		internal SessionStateRecord[] _delta = new SessionStateRecord[256];

		// Token: 0x04001060 RID: 4192
		internal bool _deltaDirty;

		// Token: 0x04001061 RID: 4193
		internal byte[][] _initialState = new byte[256][];
	}
}
