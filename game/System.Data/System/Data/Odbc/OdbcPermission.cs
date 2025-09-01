using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Enables the .NET Framework Data Provider for ODBC to help make sure that a user has a security level sufficient to access an ODBC data source. This class cannot be inherited.</summary>
	// Token: 0x02000301 RID: 769
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		// Token: 0x06002220 RID: 8736 RVA: 0x0009ED29 File Offset: 0x0009CF29
		[Obsolete("OdbcPermission() has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission() : this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class with one of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06002221 RID: 8737 RVA: 0x0005AABF File Offset: 0x00058CBF
		public OdbcPermission(PermissionState state) : base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		/// <param name="state">One of the <see langword="System.Security.Permissions.PermissionState" /> values.</param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed.</param>
		// Token: 0x06002222 RID: 8738 RVA: 0x0009ED32 File Offset: 0x0009CF32
		[Obsolete("OdbcPermission(PermissionState state, Boolean allowBlankPassword) has been deprecated.  Use the OdbcPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0005AAD8 File Offset: 0x00058CD8
		private OdbcPermission(OdbcPermission permission) : base(permission)
		{
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0005AAE1 File Offset: 0x00058CE1
		internal OdbcPermission(OdbcPermissionAttribute permissionAttribute) : base(permissionAttribute)
		{
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0005AAEA File Offset: 0x00058CEA
		internal OdbcPermission(OdbcConnectionString constr) : base(constr)
		{
			if (constr == null || constr.IsEmpty)
			{
				base.Add(ADP.StrEmpty, ADP.StrEmpty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		/// <summary>Adds access for the specified connection string to the existing state of the permission.</summary>
		/// <param name="connectionString">A permitted connection string.</param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values.</param>
		// Token: 0x06002226 RID: 8742 RVA: 0x0009ED44 File Offset: 0x0009CF44
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, null, true);
			base.AddPermissionEntry(entry);
		}

		/// <summary>Returns the <see cref="T:System.Data.Odbc.OdbcPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06002227 RID: 8743 RVA: 0x0009ED63 File Offset: 0x0009CF63
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}
	}
}
