using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002C RID: 44
	internal sealed class SmiParameterMetaData : SmiExtendedMetaData
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00007B24 File Offset: 0x00005D24
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007B54 File Offset: 0x00005D54
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._direction = direction;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007B8A File Offset: 0x00005D8A
		internal ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x04000496 RID: 1174
		private ParameterDirection _direction;
	}
}
