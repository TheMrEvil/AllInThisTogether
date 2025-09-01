using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	/// <summary>Generates a column schema.</summary>
	// Token: 0x020003A3 RID: 931
	public interface IDbColumnSchemaGenerator
	{
		/// <summary>Gets the column schema (<see cref="T:System.Data.Common.DbColumn" /> collection).</summary>
		/// <returns>The column schema (<see cref="T:System.Data.Common.DbColumn" /> collection).</returns>
		// Token: 0x06002D2B RID: 11563
		ReadOnlyCollection<DbColumn> GetColumnSchema();
	}
}
