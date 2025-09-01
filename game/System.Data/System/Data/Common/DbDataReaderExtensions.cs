using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	/// <summary>This class contains column schema extension methods for <see cref="T:System.Data.Common.DbDataReader" />.</summary>
	// Token: 0x02000393 RID: 915
	public static class DbDataReaderExtensions
	{
		/// <summary>Gets the column schema (<see cref="T:System.Data.Common.DbColumn" /> collection) for a <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Data.Common.DbDataReader" /> to return the column schema.</param>
		/// <returns>The column schema (<see cref="T:System.Data.Common.DbColumn" /> collection) for a <see cref="T:System.Data.Common.DbDataReader" />.</returns>
		// Token: 0x06002C77 RID: 11383 RVA: 0x000BEA54 File Offset: 0x000BCC54
		public static ReadOnlyCollection<DbColumn> GetColumnSchema(this DbDataReader reader)
		{
			if (reader.CanGetColumnSchema())
			{
				return ((IDbColumnSchemaGenerator)reader).GetColumnSchema();
			}
			throw new NotSupportedException();
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.Common.DbDataReader" /> can get a column schema.</summary>
		/// <param name="reader">The <see cref="T:System.Data.Common.DbDataReader" /> to be checked for column schema support.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbDataReader" /> can get a column schema; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C78 RID: 11384 RVA: 0x000BEA6F File Offset: 0x000BCC6F
		public static bool CanGetColumnSchema(this DbDataReader reader)
		{
			return reader is IDbColumnSchemaGenerator;
		}
	}
}
