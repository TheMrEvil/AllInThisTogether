using System;

// Token: 0x0200001E RID: 30
internal static class Res
{
	// Token: 0x060000D8 RID: 216 RVA: 0x00005696 File Offset: 0x00003896
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005699 File Offset: 0x00003899
	internal static string GetString(string name, params object[] args)
	{
		return string.Format(name, args);
	}

	// Token: 0x04000400 RID: 1024
	internal const string CodeGen_InvalidIdentifier = "Cannot generate identifier for name '{0}'";

	// Token: 0x04000401 RID: 1025
	internal const string CodeGen_DuplicateTableName = "There is more than one table with the same name '{0}' (even if namespace is different)";

	// Token: 0x04000402 RID: 1026
	internal const string CodeGen_TypeCantBeNull = "Column '{0}': Type '{1}' cannot be null";

	// Token: 0x04000403 RID: 1027
	internal const string CodeGen_NoCtor0 = "Column '{0}': Type '{1}' does not have parameterless constructor";

	// Token: 0x04000404 RID: 1028
	internal const string CodeGen_NoCtor1 = "Column '{0}': Type '{1}' does not have constructor with string argument";

	// Token: 0x04000405 RID: 1029
	internal const string SQLUDT_MaxByteSizeValue = "range: 0-8000";

	// Token: 0x04000406 RID: 1030
	internal const string SqlUdt_InvalidUdtMessage = "'{0}' is an invalid user defined type, reason: {1}.";

	// Token: 0x04000407 RID: 1031
	internal const string Sql_NullCommandText = "Command parameter must have a non null and non empty command text.";

	// Token: 0x04000408 RID: 1032
	internal const string Sql_MismatchedMetaDataDirectionArrayLengths = "MetaData parameter array must have length equivalent to ParameterDirection array argument.";

	// Token: 0x04000409 RID: 1033
	public const string ADP_InvalidXMLBadVersion = "Invalid Xml; can only parse elements of version one.";

	// Token: 0x0400040A RID: 1034
	public const string ADP_NotAPermissionElement = "Given security element is not a permission element.";

	// Token: 0x0400040B RID: 1035
	public const string ADP_PermissionTypeMismatch = "Type mismatch.";

	// Token: 0x0400040C RID: 1036
	public const string ConfigProviderNotFound = "Unable to find the requested .Net Framework Data Provider.  It may not be installed.";

	// Token: 0x0400040D RID: 1037
	public const string ConfigProviderInvalid = "The requested .Net Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.";

	// Token: 0x0400040E RID: 1038
	public const string ConfigProviderNotInstalled = "Failed to find or load the registered .Net Framework Data Provider.";

	// Token: 0x0400040F RID: 1039
	public const string ConfigProviderMissing = "The missing .Net Framework Data Provider's assembly qualified name is required.";

	// Token: 0x04000410 RID: 1040
	public const string ConfigBaseElementsOnly = "Only elements allowed.";

	// Token: 0x04000411 RID: 1041
	public const string ConfigBaseNoChildNodes = "Child nodes not allowed.";

	// Token: 0x04000412 RID: 1042
	public const string ConfigUnrecognizedAttributes = "Unrecognized attribute '{0}'.";

	// Token: 0x04000413 RID: 1043
	public const string ConfigUnrecognizedElement = "Unrecognized element.";

	// Token: 0x04000414 RID: 1044
	public const string ConfigSectionsUnique = "The '{0}' section can only appear once per config file.";

	// Token: 0x04000415 RID: 1045
	public const string ConfigRequiredAttributeMissing = "Required attribute '{0}' not found.";

	// Token: 0x04000416 RID: 1046
	public const string ConfigRequiredAttributeEmpty = "Required attribute '{0}' cannot be empty.";

	// Token: 0x04000417 RID: 1047
	public const string ADP_QuotePrefixNotSet = "{0} requires open connection when the quote prefix has not been set.";
}
