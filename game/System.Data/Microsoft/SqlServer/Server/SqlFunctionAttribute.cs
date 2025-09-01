using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition of a user-defined aggregate as a function in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server.</summary>
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		/// <summary>An optional attribute on a user-defined aggregate, used to indicate that the method should be registered in SQL Server as a function. Also used to set the <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.DataAccess" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.FillRowMethodName" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsDeterministic" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsPrecise" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.Name" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.SystemDataAccess" />, and <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.TableDefinition" /> properties of the function attribute.</summary>
		// Token: 0x06000463 RID: 1123 RVA: 0x00010902 File Offset: 0x0000EB02
		public SqlFunctionAttribute()
		{
			this.m_fDeterministic = false;
			this.m_eDataAccess = DataAccessKind.None;
			this.m_eSystemDataAccess = SystemDataAccessKind.None;
			this.m_fPrecise = false;
			this.m_fName = null;
			this.m_fTableDefinition = null;
			this.m_FillRowMethodName = null;
		}

		/// <summary>Indicates whether the user-defined function is deterministic.</summary>
		/// <returns>
		///   <see langword="true" /> if the function is deterministic; otherwise <see langword="false" />.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001093B File Offset: 0x0000EB3B
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00010943 File Offset: 0x0000EB43
		public bool IsDeterministic
		{
			get
			{
				return this.m_fDeterministic;
			}
			set
			{
				this.m_fDeterministic = value;
			}
		}

		/// <summary>Indicates whether the function involves access to user data stored in the local instance of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.<see langword="None" />: Does not access data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.<see langword="Read" />: Only reads user data.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001094C File Offset: 0x0000EB4C
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00010954 File Offset: 0x0000EB54
		public DataAccessKind DataAccess
		{
			get
			{
				return this.m_eDataAccess;
			}
			set
			{
				this.m_eDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function requires access to data stored in the system catalogs or virtual system tables of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.<see langword="None" />: Does not access system data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.<see langword="Read" />: Only reads system data.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001095D File Offset: 0x0000EB5D
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00010965 File Offset: 0x0000EB65
		public SystemDataAccessKind SystemDataAccess
		{
			get
			{
				return this.m_eSystemDataAccess;
			}
			set
			{
				this.m_eSystemDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function involves imprecise computations, such as floating point operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the function involves precise computations; otherwise <see langword="false" />.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001096E File Offset: 0x0000EB6E
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00010976 File Offset: 0x0000EB76
		public bool IsPrecise
		{
			get
			{
				return this.m_fPrecise;
			}
			set
			{
				this.m_fPrecise = value;
			}
		}

		/// <summary>The name under which the function should be registered in SQL Server.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name under which the function should be registered.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001097F File Offset: 0x0000EB7F
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00010987 File Offset: 0x0000EB87
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		/// <summary>A string that represents the table definition of the results, if the method is used as a table-valued function (TVF).</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table definition of the results.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00010990 File Offset: 0x0000EB90
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00010998 File Offset: 0x0000EB98
		public string TableDefinition
		{
			get
			{
				return this.m_fTableDefinition;
			}
			set
			{
				this.m_fTableDefinition = value;
			}
		}

		/// <summary>The name of a method in the same class which is used to fill a row of data in the table returned by the table-valued function.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of a method in the same class which is used to fill a row of data in the table returned by the table-valued function.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000109A1 File Offset: 0x0000EBA1
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000109A9 File Offset: 0x0000EBA9
		public string FillRowMethodName
		{
			get
			{
				return this.m_FillRowMethodName;
			}
			set
			{
				this.m_FillRowMethodName = value;
			}
		}

		// Token: 0x04000548 RID: 1352
		private bool m_fDeterministic;

		// Token: 0x04000549 RID: 1353
		private DataAccessKind m_eDataAccess;

		// Token: 0x0400054A RID: 1354
		private SystemDataAccessKind m_eSystemDataAccess;

		// Token: 0x0400054B RID: 1355
		private bool m_fPrecise;

		// Token: 0x0400054C RID: 1356
		private string m_fName;

		// Token: 0x0400054D RID: 1357
		private string m_fTableDefinition;

		// Token: 0x0400054E RID: 1358
		private string m_FillRowMethodName;
	}
}
