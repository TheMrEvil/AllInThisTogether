using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Enables the .NET Framework Data Provider for SQL Server to help make sure that a user has a security level sufficient to access a data source.</summary>
	// Token: 0x0200027F RID: 639
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		// Token: 0x06001E18 RID: 7704 RVA: 0x0008EEC2 File Offset: 0x0008D0C2
		[Obsolete("SqlClientPermission() has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission() : this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06001E19 RID: 7705 RVA: 0x0005AABF File Offset: 0x00058CBF
		public SqlClientPermission(PermissionState state) : base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed.</param>
		// Token: 0x06001E1A RID: 7706 RVA: 0x0008EECB File Offset: 0x0008D0CB
		[Obsolete("SqlClientPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the SqlClientPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0005AAD8 File Offset: 0x00058CD8
		private SqlClientPermission(SqlClientPermission permission) : base(permission)
		{
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0005AAE1 File Offset: 0x00058CE1
		internal SqlClientPermission(SqlClientPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0005AAEA File Offset: 0x00058CEA
		internal SqlClientPermission(SqlConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds a new connection string and a set of restricted keywords to the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="restrictions">The key restrictions.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> enumerations.</param>
		// Token: 0x06001E1E RID: 7710 RVA: 0x0008EEDC File Offset: 0x0008D0DC
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, SqlConnectionString.GetParseSynonyms(), false);
			base.AddPermissionEntry(entry);
		}

		/// <summary>Returns the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0008EEFF File Offset: 0x0008D0FF
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}
	}
}
