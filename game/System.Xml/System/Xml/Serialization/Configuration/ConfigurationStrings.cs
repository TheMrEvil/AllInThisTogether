using System;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000315 RID: 789
	internal static class ConfigurationStrings
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x000D17C1 File Offset: 0x000CF9C1
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml.serialization", sectionName);
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x000D17D8 File Offset: 0x000CF9D8
		internal static string SchemaImporterExtensionsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("schemaImporterExtensions");
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000D17E4 File Offset: 0x000CF9E4
		internal static string DateTimeSerializationSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("dateTimeSerialization");
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000D17F0 File Offset: 0x000CF9F0
		internal static string XmlSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("xmlSerializer");
			}
		}

		// Token: 0x04001B43 RID: 6979
		internal const string Name = "name";

		// Token: 0x04001B44 RID: 6980
		internal const string SchemaImporterExtensionsSectionName = "schemaImporterExtensions";

		// Token: 0x04001B45 RID: 6981
		internal const string DateTimeSerializationSectionName = "dateTimeSerialization";

		// Token: 0x04001B46 RID: 6982
		internal const string XmlSerializerSectionName = "xmlSerializer";

		// Token: 0x04001B47 RID: 6983
		internal const string SectionGroupName = "system.xml.serialization";

		// Token: 0x04001B48 RID: 6984
		internal const string SqlTypesSchemaImporterChar = "SqlTypesSchemaImporterChar";

		// Token: 0x04001B49 RID: 6985
		internal const string SqlTypesSchemaImporterNChar = "SqlTypesSchemaImporterNChar";

		// Token: 0x04001B4A RID: 6986
		internal const string SqlTypesSchemaImporterVarChar = "SqlTypesSchemaImporterVarChar";

		// Token: 0x04001B4B RID: 6987
		internal const string SqlTypesSchemaImporterNVarChar = "SqlTypesSchemaImporterNVarChar";

		// Token: 0x04001B4C RID: 6988
		internal const string SqlTypesSchemaImporterText = "SqlTypesSchemaImporterText";

		// Token: 0x04001B4D RID: 6989
		internal const string SqlTypesSchemaImporterNText = "SqlTypesSchemaImporterNText";

		// Token: 0x04001B4E RID: 6990
		internal const string SqlTypesSchemaImporterVarBinary = "SqlTypesSchemaImporterVarBinary";

		// Token: 0x04001B4F RID: 6991
		internal const string SqlTypesSchemaImporterBinary = "SqlTypesSchemaImporterBinary";

		// Token: 0x04001B50 RID: 6992
		internal const string SqlTypesSchemaImporterImage = "SqlTypesSchemaImporterImage";

		// Token: 0x04001B51 RID: 6993
		internal const string SqlTypesSchemaImporterDecimal = "SqlTypesSchemaImporterDecimal";

		// Token: 0x04001B52 RID: 6994
		internal const string SqlTypesSchemaImporterNumeric = "SqlTypesSchemaImporterNumeric";

		// Token: 0x04001B53 RID: 6995
		internal const string SqlTypesSchemaImporterBigInt = "SqlTypesSchemaImporterBigInt";

		// Token: 0x04001B54 RID: 6996
		internal const string SqlTypesSchemaImporterInt = "SqlTypesSchemaImporterInt";

		// Token: 0x04001B55 RID: 6997
		internal const string SqlTypesSchemaImporterSmallInt = "SqlTypesSchemaImporterSmallInt";

		// Token: 0x04001B56 RID: 6998
		internal const string SqlTypesSchemaImporterTinyInt = "SqlTypesSchemaImporterTinyInt";

		// Token: 0x04001B57 RID: 6999
		internal const string SqlTypesSchemaImporterBit = "SqlTypesSchemaImporterBit";

		// Token: 0x04001B58 RID: 7000
		internal const string SqlTypesSchemaImporterFloat = "SqlTypesSchemaImporterFloat";

		// Token: 0x04001B59 RID: 7001
		internal const string SqlTypesSchemaImporterReal = "SqlTypesSchemaImporterReal";

		// Token: 0x04001B5A RID: 7002
		internal const string SqlTypesSchemaImporterDateTime = "SqlTypesSchemaImporterDateTime";

		// Token: 0x04001B5B RID: 7003
		internal const string SqlTypesSchemaImporterSmallDateTime = "SqlTypesSchemaImporterSmallDateTime";

		// Token: 0x04001B5C RID: 7004
		internal const string SqlTypesSchemaImporterMoney = "SqlTypesSchemaImporterMoney";

		// Token: 0x04001B5D RID: 7005
		internal const string SqlTypesSchemaImporterSmallMoney = "SqlTypesSchemaImporterSmallMoney";

		// Token: 0x04001B5E RID: 7006
		internal const string SqlTypesSchemaImporterUniqueIdentifier = "SqlTypesSchemaImporterUniqueIdentifier";

		// Token: 0x04001B5F RID: 7007
		internal const string Type = "type";

		// Token: 0x04001B60 RID: 7008
		internal const string Mode = "mode";

		// Token: 0x04001B61 RID: 7009
		internal const string CheckDeserializeAdvances = "checkDeserializeAdvances";

		// Token: 0x04001B62 RID: 7010
		internal const string TempFilesLocation = "tempFilesLocation";

		// Token: 0x04001B63 RID: 7011
		internal const string UseLegacySerializerGeneration = "useLegacySerializerGeneration";
	}
}
