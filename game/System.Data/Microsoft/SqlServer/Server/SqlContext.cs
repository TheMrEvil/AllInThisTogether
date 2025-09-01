using System;
using System.Security.Principal;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Represents an abstraction of the caller's context, which provides access to the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" />, <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" />, and <see cref="T:System.Security.Principal.WindowsIdentity" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000064 RID: 100
	public sealed class SqlContext
	{
		/// <summary>Specifies whether the calling code is running within SQL Server, and if the context connection can be accessed.</summary>
		/// <returns>
		///   <see langword="True" /> if the context connection is available and the other <see cref="T:Microsoft.SqlServer.Server.SqlContext" /> members can be accessed.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00006D64 File Offset: 0x00004F64
		public static bool IsAvailable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the pipe object that allows the caller to send result sets, messages, and the results of executing commands back to the client.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> if a pipe is available, or <see langword="null" /> if called in a context where pipe is not available (for example, in a user-defined function).</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00003E32 File Offset: 0x00002032
		public static SqlPipe Pipe
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the trigger context used to provide the caller with information about what caused the trigger to fire, and a map of the columns that were updated.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> if a trigger context is available, or <see langword="null" /> if called outside of a trigger invocation.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00003E32 File Offset: 0x00002032
		public static SqlTriggerContext TriggerContext
		{
			get
			{
				return null;
			}
		}

		/// <summary>The Microsoft Windows identity of the caller.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> instance representing the Windows identity of the caller, or <see langword="null" /> if the client was authenticated using SQL Server Authentication.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00003E32 File Offset: 0x00002032
		public static WindowsIdentity WindowsIdentity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00003D93 File Offset: 0x00001F93
		public SqlContext()
		{
		}
	}
}
