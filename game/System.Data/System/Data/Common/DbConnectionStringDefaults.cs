using System;
using System.Data.SqlClient;

namespace System.Data.Common
{
	// Token: 0x020003CC RID: 972
	internal static class DbConnectionStringDefaults
	{
		// Token: 0x04001C08 RID: 7176
		internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

		// Token: 0x04001C09 RID: 7177
		internal const string ApplicationName = "Core .Net SqlClient Data Provider";

		// Token: 0x04001C0A RID: 7178
		internal const string AttachDBFilename = "";

		// Token: 0x04001C0B RID: 7179
		internal const int ConnectTimeout = 15;

		// Token: 0x04001C0C RID: 7180
		internal const string CurrentLanguage = "";

		// Token: 0x04001C0D RID: 7181
		internal const string DataSource = "";

		// Token: 0x04001C0E RID: 7182
		internal const bool Encrypt = false;

		// Token: 0x04001C0F RID: 7183
		internal const bool Enlist = true;

		// Token: 0x04001C10 RID: 7184
		internal const string FailoverPartner = "";

		// Token: 0x04001C11 RID: 7185
		internal const string InitialCatalog = "";

		// Token: 0x04001C12 RID: 7186
		internal const bool IntegratedSecurity = false;

		// Token: 0x04001C13 RID: 7187
		internal const int LoadBalanceTimeout = 0;

		// Token: 0x04001C14 RID: 7188
		internal const bool MultipleActiveResultSets = false;

		// Token: 0x04001C15 RID: 7189
		internal const bool MultiSubnetFailover = false;

		// Token: 0x04001C16 RID: 7190
		internal const int MaxPoolSize = 100;

		// Token: 0x04001C17 RID: 7191
		internal const int MinPoolSize = 0;

		// Token: 0x04001C18 RID: 7192
		internal const int PacketSize = 8000;

		// Token: 0x04001C19 RID: 7193
		internal const string Password = "";

		// Token: 0x04001C1A RID: 7194
		internal const bool PersistSecurityInfo = false;

		// Token: 0x04001C1B RID: 7195
		internal const bool Pooling = true;

		// Token: 0x04001C1C RID: 7196
		internal const bool TrustServerCertificate = false;

		// Token: 0x04001C1D RID: 7197
		internal const string TypeSystemVersion = "Latest";

		// Token: 0x04001C1E RID: 7198
		internal const string UserID = "";

		// Token: 0x04001C1F RID: 7199
		internal const bool UserInstance = false;

		// Token: 0x04001C20 RID: 7200
		internal const bool Replication = false;

		// Token: 0x04001C21 RID: 7201
		internal const string WorkstationID = "";

		// Token: 0x04001C22 RID: 7202
		internal const string TransactionBinding = "Implicit Unbind";

		// Token: 0x04001C23 RID: 7203
		internal const int ConnectRetryCount = 1;

		// Token: 0x04001C24 RID: 7204
		internal const int ConnectRetryInterval = 10;

		// Token: 0x04001C25 RID: 7205
		internal const string Dsn = "";

		// Token: 0x04001C26 RID: 7206
		internal const string Driver = "";
	}
}
