using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002B RID: 43
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00007A60 File Offset: 0x00005C60
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007A8C File Offset: 0x00005C8C
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007ABC File Offset: 0x00005CBC
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007B02 File Offset: 0x00005D02
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007B0A File Offset: 0x00005D0A
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007B12 File Offset: 0x00005D12
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007B1A File Offset: 0x00005D1A
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x04000492 RID: 1170
		private string _name;

		// Token: 0x04000493 RID: 1171
		private string _typeSpecificNamePart1;

		// Token: 0x04000494 RID: 1172
		private string _typeSpecificNamePart2;

		// Token: 0x04000495 RID: 1173
		private string _typeSpecificNamePart3;
	}
}
