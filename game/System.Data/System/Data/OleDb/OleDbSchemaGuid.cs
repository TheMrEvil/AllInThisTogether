using System;

namespace System.Data.OleDb
{
	/// <summary>Returns the type of schema table specified by the <see cref="M:System.Data.OleDb.OleDbConnection.GetOleDbSchemaTable(System.Guid,System.Object[])" /> method.</summary>
	// Token: 0x02000173 RID: 371
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbSchemaGuid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbSchemaGuid" /> class.</summary>
		// Token: 0x060013D4 RID: 5076 RVA: 0x00003D93 File Offset: 0x00001F93
		public OleDbSchemaGuid()
		{
		}

		/// <summary>Returns the assertions defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C02 RID: 3074
		public static readonly Guid Assertions;

		/// <summary>Returns the physical attributes associated with catalogs accessible from the data source. Returns the assertions defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C03 RID: 3075
		public static readonly Guid Catalogs;

		/// <summary>Returns the character sets defined in the catalog that is accessible to a given user.</summary>
		// Token: 0x04000C04 RID: 3076
		public static readonly Guid Character_Sets;

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C05 RID: 3077
		public static readonly Guid Check_Constraints;

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C06 RID: 3078
		public static readonly Guid Check_Constraints_By_Table;

		/// <summary>Returns the character collations defined in the catalog that is accessible to a given user.</summary>
		// Token: 0x04000C07 RID: 3079
		public static readonly Guid Collations;

		/// <summary>Returns the columns defined in the catalog that are dependent on a domain defined in the catalog and owned by a given user.</summary>
		// Token: 0x04000C08 RID: 3080
		public static readonly Guid Column_Domain_Usage;

		/// <summary>Returns the privileges on columns of tables defined in the catalog that are available to or granted by a given user.</summary>
		// Token: 0x04000C09 RID: 3081
		public static readonly Guid Column_Privileges;

		/// <summary>Returns the columns of tables (including views) defined in the catalog that is accessible to a given user.</summary>
		// Token: 0x04000C0A RID: 3082
		public static readonly Guid Columns;

		/// <summary>Returns the columns used by referential constraints, unique constraints, check constraints, and assertions, defined in the catalog and owned by a given user.</summary>
		// Token: 0x04000C0B RID: 3083
		public static readonly Guid Constraint_Column_Usage;

		/// <summary>Returns the tables that are used by referential constraints, unique constraints, check constraints, and assertions defined in the catalog and owned by a given user.</summary>
		// Token: 0x04000C0C RID: 3084
		public static readonly Guid Constraint_Table_Usage;

		/// <summary>Returns a list of provider-specific keywords.</summary>
		// Token: 0x04000C0D RID: 3085
		public static readonly Guid DbInfoKeywords;

		/// <summary>Returns a list of provider-specific literals used in text commands.</summary>
		// Token: 0x04000C0E RID: 3086
		public static readonly Guid DbInfoLiterals;

		/// <summary>Returns the foreign key columns defined in the catalog by a given user.</summary>
		// Token: 0x04000C0F RID: 3087
		public static readonly Guid Foreign_Keys;

		/// <summary>Returns the indexes defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C10 RID: 3088
		public static readonly Guid Indexes;

		/// <summary>Returns the columns defined in the catalog that is constrained as keys by a given user.</summary>
		// Token: 0x04000C11 RID: 3089
		public static readonly Guid Key_Column_Usage;

		/// <summary>Returns the primary key columns defined in the catalog by a given user.</summary>
		// Token: 0x04000C12 RID: 3090
		public static readonly Guid Primary_Keys;

		/// <summary>Returns information about the columns of rowsets returned by procedures.</summary>
		// Token: 0x04000C13 RID: 3091
		public static readonly Guid Procedure_Columns;

		/// <summary>Returns information about the parameters and return codes of procedures.</summary>
		// Token: 0x04000C14 RID: 3092
		public static readonly Guid Procedure_Parameters;

		/// <summary>Returns the procedures defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C15 RID: 3093
		public static readonly Guid Procedures;

		/// <summary>Returns the base data types supported by the .NET Framework Data Provider for OLE DB.</summary>
		// Token: 0x04000C16 RID: 3094
		public static readonly Guid Provider_Types;

		/// <summary>Returns the referential constraints defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C17 RID: 3095
		public static readonly Guid Referential_Constraints;

		/// <summary>Returns a list of schema rowsets, identified by their GUIDs, and a pointer to the descriptions of the restriction columns.</summary>
		// Token: 0x04000C18 RID: 3096
		public static readonly Guid SchemaGuids;

		/// <summary>Returns the schema objects that are owned by a given user.</summary>
		// Token: 0x04000C19 RID: 3097
		public static readonly Guid Schemata;

		/// <summary>Returns the conformance levels, options, and dialects supported by the SQL-implementation processing data defined in the catalog.</summary>
		// Token: 0x04000C1A RID: 3098
		public static readonly Guid Sql_Languages;

		/// <summary>Returns the statistics defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C1B RID: 3099
		public static readonly Guid Statistics;

		/// <summary>Returns the table constraints defined in the catalog that is owned by a given user.</summary>
		// Token: 0x04000C1C RID: 3100
		public static readonly Guid Table_Constraints;

		/// <summary>Returns the privileges on tables defined in the catalog that are available to, or granted by, a given user.</summary>
		// Token: 0x04000C1D RID: 3101
		public static readonly Guid Table_Privileges;

		/// <summary>Describes the available set of statistics on tables in the provider.</summary>
		// Token: 0x04000C1E RID: 3102
		public static readonly Guid Table_Statistics;

		/// <summary>Returns the tables (including views) defined in the catalog that are accessible to a given user.</summary>
		// Token: 0x04000C1F RID: 3103
		public static readonly Guid Tables;

		/// <summary>Returns the tables (including views) that are accessible to a given user.</summary>
		// Token: 0x04000C20 RID: 3104
		public static readonly Guid Tables_Info;

		/// <summary>Returns the character translations defined in the catalog that is accessible to a given user.</summary>
		// Token: 0x04000C21 RID: 3105
		public static readonly Guid Translations;

		/// <summary>Identifies the trustees defined in the data source.</summary>
		// Token: 0x04000C22 RID: 3106
		public static readonly Guid Trustee;

		/// <summary>Returns the USAGE privileges on objects defined in the catalog that are available to or granted by a given user.</summary>
		// Token: 0x04000C23 RID: 3107
		public static readonly Guid Usage_Privileges;

		/// <summary>Returns the columns on which viewed tables depend, as defined in the catalog and owned by a given user.</summary>
		// Token: 0x04000C24 RID: 3108
		public static readonly Guid View_Column_Usage;

		/// <summary>Returns the tables on which viewed tables, defined in the catalog and owned by a given user, are dependent.</summary>
		// Token: 0x04000C25 RID: 3109
		public static readonly Guid View_Table_Usage;

		/// <summary>Returns the views defined in the catalog that is accessible to a given user.</summary>
		// Token: 0x04000C26 RID: 3110
		public static readonly Guid Views;
	}
}
