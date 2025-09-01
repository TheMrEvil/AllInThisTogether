using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200026A RID: 618
	internal struct MultiPartTableName
	{
		// Token: 0x06001CE0 RID: 7392 RVA: 0x00089761 File Offset: 0x00087961
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008978E File Offset: 0x0008798E
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x000897B3 File Offset: 0x000879B3
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x000897C1 File Offset: 0x000879C1
		internal string ServerName
		{
			get
			{
				this.ParseMultipartName();
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x000897CA File Offset: 0x000879CA
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x000897D8 File Offset: 0x000879D8
		internal string CatalogName
		{
			get
			{
				this.ParseMultipartName();
				return this._catalogName;
			}
			set
			{
				this._catalogName = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000897E1 File Offset: 0x000879E1
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x000897EF File Offset: 0x000879EF
		internal string SchemaName
		{
			get
			{
				this.ParseMultipartName();
				return this._schemaName;
			}
			set
			{
				this._schemaName = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000897F8 File Offset: 0x000879F8
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x00089806 File Offset: 0x00087A06
		internal string TableName
		{
			get
			{
				this.ParseMultipartName();
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00089810 File Offset: 0x00087A10
		private void ParseMultipartName()
		{
			if (this._multipartName != null)
			{
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this._multipartName, "[\"", "]\"", "Processing of results from SQL Server failed because of an invalid multipart name", false);
				this._serverName = array[0];
				this._catalogName = array[1];
				this._schemaName = array[2];
				this._tableName = array[3];
				this._multipartName = null;
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0008986C File Offset: 0x00087A6C
		// Note: this type is marked as 'beforefieldinit'.
		static MultiPartTableName()
		{
		}

		// Token: 0x0400141A RID: 5146
		private string _multipartName;

		// Token: 0x0400141B RID: 5147
		private string _serverName;

		// Token: 0x0400141C RID: 5148
		private string _catalogName;

		// Token: 0x0400141D RID: 5149
		private string _schemaName;

		// Token: 0x0400141E RID: 5150
		private string _tableName;

		// Token: 0x0400141F RID: 5151
		internal static readonly MultiPartTableName Null = new MultiPartTableName(new string[4]);
	}
}
