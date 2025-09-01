using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000239 RID: 569
	internal sealed class SQLMessage
	{
		// Token: 0x06001BA3 RID: 7075 RVA: 0x00003D93 File Offset: 0x00001F93
		private SQLMessage()
		{
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0007D883 File Offset: 0x0007BA83
		internal static string CultureIdError()
		{
			return SR.GetString("The Collation specified by SQL Server is not supported.");
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0007D88F File Offset: 0x0007BA8F
		internal static string EncryptionNotSupportedByClient()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to requires encryption but this machine does not support it.");
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0007D89B File Offset: 0x0007BA9B
		internal static string EncryptionNotSupportedByServer()
		{
			return SR.GetString("The instance of SQL Server you attempted to connect to does not support encryption.");
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0007D8A7 File Offset: 0x0007BAA7
		internal static string OperationCancelled()
		{
			return SR.GetString("Operation cancelled by user.");
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0007D8B3 File Offset: 0x0007BAB3
		internal static string SevereError()
		{
			return SR.GetString("A severe error occurred on the current command.  The results, if any, should be discarded.");
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0007D8BF File Offset: 0x0007BABF
		internal static string SSPIInitializeError()
		{
			return SR.GetString("Cannot initialize SSPI package.");
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0007D8CB File Offset: 0x0007BACB
		internal static string SSPIGenerateError()
		{
			return SR.GetString("Failed to generate SSPI context.");
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007D8D7 File Offset: 0x0007BAD7
		internal static string SqlServerBrowserNotAccessible()
		{
			return SR.GetString("Cannot connect to SQL Server Browser. Ensure SQL Server Browser has been started.");
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0007D8E3 File Offset: 0x0007BAE3
		internal static string KerberosTicketMissingError()
		{
			return SR.GetString("Cannot authenticate using Kerberos. Ensure Kerberos has been initialized on the client with 'kinit' and a Service Principal Name has been registered for the SQL Server to allow Kerberos authentication.");
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0007D8EF File Offset: 0x0007BAEF
		internal static string Timeout()
		{
			return SR.GetString("Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.");
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0007D8FB File Offset: 0x0007BAFB
		internal static string Timeout_PreLogin_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the pre-login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0007D907 File Offset: 0x0007BB07
		internal static string Timeout_PreLogin_InitializeConnection()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to create and initialize a socket to the server.  This could be either because the server was unreachable or unable to respond back in time.");
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0007D913 File Offset: 0x0007BB13
		internal static string Timeout_PreLogin_SendHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while making a pre-login handshake request.  This could be because the server was unable to respond back in time.");
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0007D91F File Offset: 0x0007BB1F
		internal static string Timeout_PreLogin_ConsumeHandshake()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement.  This could be because the pre-login handshake failed or the server was unable to respond back in time.");
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0007D92B File Offset: 0x0007BB2B
		internal static string Timeout_Login_Begin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed at the start of the login phase.  This could be because of insufficient time provided for connection timeout.");
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0007D937 File Offset: 0x0007BB37
		internal static string Timeout_Login_ProcessConnectionAuth()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed while attempting to authenticate the login.  This could be because the server failed to authenticate the user or the server was unable to respond back in time.");
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0007D943 File Offset: 0x0007BB43
		internal static string Timeout_PostLogin()
		{
			return SR.GetString("Connection Timeout Expired.  The timeout period elapsed during the post-login phase.  The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.");
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0007D94F File Offset: 0x0007BB4F
		internal static string Timeout_FailoverInfo()
		{
			return SR.GetString("This failure occurred while attempting to connect to the {0} server.");
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0007D95B File Offset: 0x0007BB5B
		internal static string Timeout_RoutingDestination()
		{
			return SR.GetString("This failure occurred while attempting to connect to the routing destination. The duration spent while attempting to connect to the original server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4};  ");
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0007D967 File Offset: 0x0007BB67
		internal static string Duration_PreLogin_Begin(long PreLoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0};", new object[]
			{
				PreLoginBeginDuration
			});
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0007D982 File Offset: 0x0007BB82
		internal static string Duration_PreLoginHandshake(long PreLoginBeginDuration, long PreLoginHandshakeDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; ", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration
			});
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0007D9A6 File Offset: 0x0007BBA6
		internal static string Duration_Login_Begin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; ", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration
			});
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0007D9D3 File Offset: 0x0007BBD3
		internal static string Duration_Login_ProcessConnectionAuth(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; ", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration,
				LoginAuthDuration
			});
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0007DA09 File Offset: 0x0007BC09
		internal static string Duration_PostLogin(long PreLoginBeginDuration, long PreLoginHandshakeDuration, long LoginBeginDuration, long LoginAuthDuration, long PostLoginDuration)
		{
			return SR.GetString("The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4}; ", new object[]
			{
				PreLoginBeginDuration,
				PreLoginHandshakeDuration,
				LoginBeginDuration,
				LoginAuthDuration,
				PostLoginDuration
			});
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0007DA49 File Offset: 0x0007BC49
		internal static string UserInstanceFailure()
		{
			return SR.GetString("A user instance was requested in the connection string but the server specified does not support this option.");
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0007DA55 File Offset: 0x0007BC55
		internal static string PreloginError()
		{
			return SR.GetString("A connection was successfully established with the server, but then an error occurred during the pre-login handshake.");
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0007DA61 File Offset: 0x0007BC61
		internal static string ExClientConnectionId()
		{
			return SR.GetString("ClientConnectionId:{0}");
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0007DA6D File Offset: 0x0007BC6D
		internal static string ExErrorNumberStateClass()
		{
			return SR.GetString("Error Number:{0},State:{1},Class:{2}");
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0007DA79 File Offset: 0x0007BC79
		internal static string ExOriginalClientConnectionId()
		{
			return SR.GetString("ClientConnectionId before routing:{0}");
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0007DA85 File Offset: 0x0007BC85
		internal static string ExRoutingDestination()
		{
			return SR.GetString("Routing Destination:{0}");
		}
	}
}
