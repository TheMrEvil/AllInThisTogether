using System;
using System.Globalization;
using System.Reflection;

// Token: 0x0200001C RID: 28
internal static class SR
{
	// Token: 0x060000CA RID: 202 RVA: 0x000056A2 File Offset: 0x000038A2
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000056B0 File Offset: 0x000038B0
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005696 File Offset: 0x00003896
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x000056BA File Offset: 0x000038BA
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000056BD File Offset: 0x000038BD
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000056D0 File Offset: 0x000038D0
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000056DE File Offset: 0x000038DE
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000056ED File Offset: 0x000038ED
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000056F8 File Offset: 0x000038F8
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00005696 File Offset: 0x00003896
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00005708 File Offset: 0x00003908
	public static string GetResourceString(string resourceKey, string defaultString)
	{
		FieldInfo field = typeof(SR).GetField(resourceKey);
		if (field != null)
		{
			return field.GetValue(null).ToString();
		}
		return defaultString;
	}

	// Token: 0x040000BE RID: 190
	public const string ADP_CollectionIndexString = "An {0} with {1} '{2}' is not contained by this {3}.";

	// Token: 0x040000BF RID: 191
	public const string ADP_CollectionInvalidType = "The {0} only accepts non-null {1} type objects, not {2} objects.";

	// Token: 0x040000C0 RID: 192
	public const string ADP_CollectionIsNotParent = "The {0} is already contained by another {1}.";

	// Token: 0x040000C1 RID: 193
	public const string ADP_CollectionNullValue = "The {0} only accepts non-null {1} type objects.";

	// Token: 0x040000C2 RID: 194
	public const string ADP_CollectionRemoveInvalidObject = "Attempted to remove an {0} that is not contained by this {1}.";

	// Token: 0x040000C3 RID: 195
	public const string ADP_CollectionUniqueValue = "The {0}.{1} is required to be unique, '{2}' already exists in the collection.";

	// Token: 0x040000C4 RID: 196
	public const string ADP_ConnectionStateMsg_Closed = "The connection's current state is closed.";

	// Token: 0x040000C5 RID: 197
	public const string ADP_ConnectionStateMsg_Connecting = "The connection's current state is connecting.";

	// Token: 0x040000C6 RID: 198
	public const string ADP_ConnectionStateMsg_Open = "The connection's current state is open.";

	// Token: 0x040000C7 RID: 199
	public const string ADP_ConnectionStateMsg_OpenExecuting = "The connection's current state is executing.";

	// Token: 0x040000C8 RID: 200
	public const string ADP_ConnectionStateMsg_OpenFetching = "The connection's current state is fetching.";

	// Token: 0x040000C9 RID: 201
	public const string ADP_ConnectionStateMsg = "The connection's current state: {0}.";

	// Token: 0x040000CA RID: 202
	public const string ADP_ConnectionStringSyntax = "Format of the initialization string does not conform to specification starting at index {0}.";

	// Token: 0x040000CB RID: 203
	public const string ADP_DataReaderClosed = "Invalid attempt to call {0} when reader is closed.";

	// Token: 0x040000CC RID: 204
	public const string ADP_EmptyString = "Expecting non-empty string for '{0}' parameter.";

	// Token: 0x040000CD RID: 205
	public const string ADP_InvalidEnumerationValue = "The {0} enumeration value, {1}, is invalid.";

	// Token: 0x040000CE RID: 206
	public const string ADP_InvalidKey = "Invalid keyword, contain one or more of 'no characters', 'control characters', 'leading or trailing whitespace' or 'leading semicolons'.";

	// Token: 0x040000CF RID: 207
	public const string ADP_InvalidValue = "The value contains embedded nulls (\\\\u0000).";

	// Token: 0x040000D0 RID: 208
	public const string Xml_SimpleTypeNotSupported = "DataSet doesn't support 'union' or 'list' as simpleType.";

	// Token: 0x040000D1 RID: 209
	public const string Xml_MissingAttribute = "Invalid {0} syntax: missing required '{1}' attribute.";

	// Token: 0x040000D2 RID: 210
	public const string Xml_ValueOutOfRange = "Value '{1}' is invalid for attribute '{0}'.";

	// Token: 0x040000D3 RID: 211
	public const string Xml_AttributeValues = "The value of attribute '{0}' should be '{1}' or '{2}'.";

	// Token: 0x040000D4 RID: 212
	public const string Xml_RelationParentNameMissing = "Parent table name is missing in relation '{0}'.";

	// Token: 0x040000D5 RID: 213
	public const string Xml_RelationChildNameMissing = "Child table name is missing in relation '{0}'.";

	// Token: 0x040000D6 RID: 214
	public const string Xml_RelationTableKeyMissing = "Parent table key is missing in relation '{0}'.";

	// Token: 0x040000D7 RID: 215
	public const string Xml_RelationChildKeyMissing = "Child table key is missing in relation '{0}'.";

	// Token: 0x040000D8 RID: 216
	public const string Xml_UndefinedDatatype = "Undefined data type: '{0}'.";

	// Token: 0x040000D9 RID: 217
	public const string Xml_DatatypeNotDefined = "Data type not defined.";

	// Token: 0x040000DA RID: 218
	public const string Xml_InvalidField = "Invalid XPath selection inside field node. Cannot find: {0}.";

	// Token: 0x040000DB RID: 219
	public const string Xml_InvalidSelector = "Invalid XPath selection inside selector node: {0}.";

	// Token: 0x040000DC RID: 220
	public const string Xml_InvalidKey = "Invalid 'Key' node inside constraint named: {0}.";

	// Token: 0x040000DD RID: 221
	public const string Xml_DuplicateConstraint = "The constraint name {0} is already used in the schema.";

	// Token: 0x040000DE RID: 222
	public const string Xml_CannotConvert = " Cannot convert '{0}' to type '{1}'.";

	// Token: 0x040000DF RID: 223
	public const string Xml_MissingRefer = "Missing '{0}' part in '{1}' constraint named '{2}'.";

	// Token: 0x040000E0 RID: 224
	public const string Xml_MismatchKeyLength = "Invalid Relation definition: different length keys.";

	// Token: 0x040000E1 RID: 225
	public const string Xml_CircularComplexType = "DataSet doesn't allow the circular reference in the ComplexType named '{0}'.";

	// Token: 0x040000E2 RID: 226
	public const string Xml_CannotInstantiateAbstract = "DataSet cannot instantiate an abstract ComplexType for the node {0}.";

	// Token: 0x040000E3 RID: 227
	public const string Xml_MultipleTargetConverterError = "An error occurred with the multiple target converter while writing an Xml Schema.  See the inner exception for details.";

	// Token: 0x040000E4 RID: 228
	public const string Xml_MultipleTargetConverterEmpty = "An error occurred with the multiple target converter while writing an Xml Schema.  A null or empty string was returned.";

	// Token: 0x040000E5 RID: 229
	public const string Xml_MergeDuplicateDeclaration = "Duplicated declaration '{0}'.";

	// Token: 0x040000E6 RID: 230
	public const string Xml_MissingTable = "Cannot load diffGram. Table '{0}' is missing in the destination dataset.";

	// Token: 0x040000E7 RID: 231
	public const string Xml_MissingSQL = "Cannot load diffGram. The 'sql' node is missing.";

	// Token: 0x040000E8 RID: 232
	public const string Xml_ColumnConflict = "Column name '{0}' is defined for different mapping types.";

	// Token: 0x040000E9 RID: 233
	public const string Xml_InvalidPrefix = "Prefix '{0}' is not valid, because it contains special characters.";

	// Token: 0x040000EA RID: 234
	public const string Xml_NestedCircular = "Circular reference in self-nested table '{0}'.";

	// Token: 0x040000EB RID: 235
	public const string Xml_FoundEntity = "DataSet cannot expand entities. Use XmlValidatingReader and set the EntityHandling property accordingly.";

	// Token: 0x040000EC RID: 236
	public const string Xml_PolymorphismNotSupported = "Type '{0}' does not implement IXmlSerializable interface therefore can not proceed with serialization.";

	// Token: 0x040000ED RID: 237
	public const string Xml_CanNotDeserializeObjectType = "Unable to proceed with deserialization. Data does not implement IXMLSerializable, therefore polymorphism is not supported.";

	// Token: 0x040000EE RID: 238
	public const string Xml_DataTableInferenceNotSupported = "DataTable does not support schema inference from Xml.";

	// Token: 0x040000EF RID: 239
	public const string Xml_MultipleParentRows = "Cannot proceed with serializing DataTable '{0}'. It contains a DataRow which has multiple parent rows on the same Foreign Key.";

	// Token: 0x040000F0 RID: 240
	public const string Xml_IsDataSetAttributeMissingInSchema = "IsDataSet attribute is missing in input Schema.";

	// Token: 0x040000F1 RID: 241
	public const string Xml_TooManyIsDataSetAtributeInSchema = "Cannot determine the DataSet Element. IsDataSet attribute exist more than once.";

	// Token: 0x040000F2 RID: 242
	public const string Xml_DynamicWithoutXmlSerializable = "DataSet will not serialize types that implement IDynamicMetaObjectProvider but do not also implement IXmlSerializable.";

	// Token: 0x040000F3 RID: 243
	public const string Expr_NYI = "The feature not implemented. {0}.";

	// Token: 0x040000F4 RID: 244
	public const string Expr_MissingOperand = "Syntax error: Missing operand after '{0}' operator.";

	// Token: 0x040000F5 RID: 245
	public const string Expr_TypeMismatch = "Type mismatch in expression '{0}'.";

	// Token: 0x040000F6 RID: 246
	public const string Expr_ExpressionTooComplex = "Expression is too complex.";

	// Token: 0x040000F7 RID: 247
	public const string Expr_UnboundName = "Cannot find column [{0}].";

	// Token: 0x040000F8 RID: 248
	public const string Expr_InvalidString = "The expression contains an invalid string constant: {0}.";

	// Token: 0x040000F9 RID: 249
	public const string Expr_UndefinedFunction = "The expression contains undefined function call {0}().";

	// Token: 0x040000FA RID: 250
	public const string Expr_Syntax = "Syntax error in the expression.";

	// Token: 0x040000FB RID: 251
	public const string Expr_FunctionArgumentCount = "Invalid number of arguments: function {0}().";

	// Token: 0x040000FC RID: 252
	public const string Expr_MissingRightParen = "The expression is missing the closing parenthesis.";

	// Token: 0x040000FD RID: 253
	public const string Expr_UnknownToken = "Cannot interpret token '{0}' at position {1}.";

	// Token: 0x040000FE RID: 254
	public const string Expr_UnknownToken1 = "Expected {0}, but actual token at the position {2} is {1}.";

	// Token: 0x040000FF RID: 255
	public const string Expr_DatatypeConvertion = "Cannot convert from {0} to {1}.";

	// Token: 0x04000100 RID: 256
	public const string Expr_DatavalueConvertion = "Cannot convert value '{0}' to Type: {1}.";

	// Token: 0x04000101 RID: 257
	public const string Expr_InvalidName = "Invalid column name [{0}].";

	// Token: 0x04000102 RID: 258
	public const string Expr_InvalidDate = "The expression contains invalid date constant '{0}'.";

	// Token: 0x04000103 RID: 259
	public const string Expr_NonConstantArgument = "Only constant expressions are allowed in the expression list for the IN operator.";

	// Token: 0x04000104 RID: 260
	public const string Expr_InvalidPattern = "Error in Like operator: the string pattern '{0}' is invalid.";

	// Token: 0x04000105 RID: 261
	public const string Expr_InWithoutParentheses = "Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.";

	// Token: 0x04000106 RID: 262
	public const string Expr_ArgumentType = "Type mismatch in function argument: {0}(), argument {1}, expected {2}.";

	// Token: 0x04000107 RID: 263
	public const string Expr_ArgumentTypeInteger = "Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.";

	// Token: 0x04000108 RID: 264
	public const string Expr_TypeMismatchInBinop = "Cannot perform '{0}' operation on {1} and {2}.";

	// Token: 0x04000109 RID: 265
	public const string Expr_AmbiguousBinop = "Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.";

	// Token: 0x0400010A RID: 266
	public const string Expr_InWithoutList = "Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.";

	// Token: 0x0400010B RID: 267
	public const string Expr_UnsupportedOperator = "The expression contains unsupported operator '{0}'.";

	// Token: 0x0400010C RID: 268
	public const string Expr_InvalidNameBracketing = "The expression contains invalid name: '{0}'.";

	// Token: 0x0400010D RID: 269
	public const string Expr_MissingOperandBefore = "Syntax error: Missing operand before '{0}' operator.";

	// Token: 0x0400010E RID: 270
	public const string Expr_TooManyRightParentheses = "The expression has too many closing parentheses.";

	// Token: 0x0400010F RID: 271
	public const string Expr_UnresolvedRelation = "The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.";

	// Token: 0x04000110 RID: 272
	public const string Expr_AggregateArgument = "Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.";

	// Token: 0x04000111 RID: 273
	public const string Expr_AggregateUnbound = "Unbound reference in the aggregate expression '{0}'.";

	// Token: 0x04000112 RID: 274
	public const string Expr_EvalNoContext = "Cannot evaluate non-constant expression without current row.";

	// Token: 0x04000113 RID: 275
	public const string Expr_ExpressionUnbound = "Unbound reference in the expression '{0}'.";

	// Token: 0x04000114 RID: 276
	public const string Expr_ComputeNotAggregate = "Cannot evaluate. Expression '{0}' is not an aggregate.";

	// Token: 0x04000115 RID: 277
	public const string Expr_FilterConvertion = "Filter expression '{0}' does not evaluate to a Boolean term.";

	// Token: 0x04000116 RID: 278
	public const string Expr_InvalidType = "Invalid type name '{0}'.";

	// Token: 0x04000117 RID: 279
	public const string Expr_LookupArgument = "Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.";

	// Token: 0x04000118 RID: 280
	public const string Expr_InvokeArgument = "Need a row or a table to Invoke DataFilter.";

	// Token: 0x04000119 RID: 281
	public const string Expr_ArgumentOutofRange = "{0}() argument is out of range.";

	// Token: 0x0400011A RID: 282
	public const string Expr_IsSyntax = "Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.";

	// Token: 0x0400011B RID: 283
	public const string Expr_Overflow = "Value is either too large or too small for Type '{0}'.";

	// Token: 0x0400011C RID: 284
	public const string Expr_BindFailure = "Cannot find the parent relation '{0}'.";

	// Token: 0x0400011D RID: 285
	public const string Expr_InvalidHoursArgument = "'hours' argument is out of range. Value must be between -14 and +14.";

	// Token: 0x0400011E RID: 286
	public const string Expr_InvalidMinutesArgument = "'minutes' argument is out of range. Value must be between -59 and +59.";

	// Token: 0x0400011F RID: 287
	public const string Expr_InvalidTimeZoneRange = "Provided range for time one exceeds total of 14 hours.";

	// Token: 0x04000120 RID: 288
	public const string Expr_MismatchKindandTimeSpan = "Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.";

	// Token: 0x04000121 RID: 289
	public const string Expr_UnsupportedType = "A DataColumn of type '{0}' does not support expression.";

	// Token: 0x04000122 RID: 290
	public const string Data_EnforceConstraints = "Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.";

	// Token: 0x04000123 RID: 291
	public const string Data_CannotModifyCollection = "Collection itself is not modifiable.";

	// Token: 0x04000124 RID: 292
	public const string Data_CaseInsensitiveNameConflict = "The given name '{0}' matches at least two names in the collection object with different cases, but does not match either of them with the same case.";

	// Token: 0x04000125 RID: 293
	public const string Data_NamespaceNameConflict = "The given name '{0}' matches at least two names in the collection object with different namespaces.";

	// Token: 0x04000126 RID: 294
	public const string Data_InvalidOffsetLength = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x04000127 RID: 295
	public const string Data_ArgumentOutOfRange = "'{0}' argument is out of range.";

	// Token: 0x04000128 RID: 296
	public const string Data_ArgumentNull = "'{0}' argument cannot be null.";

	// Token: 0x04000129 RID: 297
	public const string Data_ArgumentContainsNull = "'{0}' argument contains null value.";

	// Token: 0x0400012A RID: 298
	public const string Data_TypeNotAllowed = "Type '{0}' is not allowed here. See https://go.microsoft.com/fwlink/?linkid=2132227 for more details.";

	// Token: 0x0400012B RID: 299
	public const string DataColumns_OutOfRange = "Cannot find column {0}.";

	// Token: 0x0400012C RID: 300
	public const string DataColumns_Add1 = "Column '{0}' already belongs to this DataTable.";

	// Token: 0x0400012D RID: 301
	public const string DataColumns_Add2 = "Column '{0}' already belongs to another DataTable.";

	// Token: 0x0400012E RID: 302
	public const string DataColumns_Add3 = "Cannot have more than one SimpleContent columns in a DataTable.";

	// Token: 0x0400012F RID: 303
	public const string DataColumns_Add4 = "Cannot add a SimpleContent column to a table containing element columns or nested relations.";

	// Token: 0x04000130 RID: 304
	public const string DataColumns_AddDuplicate = "A column named '{0}' already belongs to this DataTable.";

	// Token: 0x04000131 RID: 305
	public const string DataColumns_AddDuplicate2 = "Cannot add a column named '{0}': a nested table with the same name already belongs to this DataTable.";

	// Token: 0x04000132 RID: 306
	public const string DataColumns_AddDuplicate3 = "A column named '{0}' already belongs to this DataTable: cannot set a nested table name to the same name.";

	// Token: 0x04000133 RID: 307
	public const string DataColumns_Remove = "Cannot remove a column that doesn't belong to this table.";

	// Token: 0x04000134 RID: 308
	public const string DataColumns_RemovePrimaryKey = "Cannot remove this column, because it's part of the primary key.";

	// Token: 0x04000135 RID: 309
	public const string DataColumns_RemoveChildKey = "Cannot remove this column, because it is part of the parent key for relationship {0}.";

	// Token: 0x04000136 RID: 310
	public const string DataColumns_RemoveConstraint = "Cannot remove this column, because it is a part of the constraint {0} on the table {1}.";

	// Token: 0x04000137 RID: 311
	public const string DataColumn_AutoIncrementAndExpression = "Cannot set AutoIncrement property for a computed column.";

	// Token: 0x04000138 RID: 312
	public const string DataColumn_AutoIncrementAndDefaultValue = "Cannot set AutoIncrement property for a column with DefaultValue set.";

	// Token: 0x04000139 RID: 313
	public const string DataColumn_DefaultValueAndAutoIncrement = "Cannot set a DefaultValue on an AutoIncrement column.";

	// Token: 0x0400013A RID: 314
	public const string DataColumn_AutoIncrementSeed = "AutoIncrementStep must be a non-zero value.";

	// Token: 0x0400013B RID: 315
	public const string DataColumn_NameRequired = "ColumnName is required when it is part of a DataTable.";

	// Token: 0x0400013C RID: 316
	public const string DataColumn_ChangeDataType = "Cannot change DataType of a column once it has data.";

	// Token: 0x0400013D RID: 317
	public const string DataColumn_NullDataType = "Column requires a valid DataType.";

	// Token: 0x0400013E RID: 318
	public const string DataColumn_DefaultValueDataType = "The DefaultValue for column {0} is of type {1} and cannot be converted to {2}.";

	// Token: 0x0400013F RID: 319
	public const string DataColumn_DefaultValueDataType1 = "The DefaultValue for the column is of type {0} and cannot be converted to {1}.";

	// Token: 0x04000140 RID: 320
	public const string DataColumn_DefaultValueColumnDataType = "The DefaultValue for column {0} is of type {1}, but the column is of type {2}.";

	// Token: 0x04000141 RID: 321
	public const string DataColumn_ReadOnlyAndExpression = "Cannot change ReadOnly property for the expression column.";

	// Token: 0x04000142 RID: 322
	public const string DataColumn_UniqueAndExpression = "Cannot change Unique property for the expression column.";

	// Token: 0x04000143 RID: 323
	public const string DataColumn_ExpressionAndUnique = "Cannot create an expression on a column that has AutoIncrement or Unique.";

	// Token: 0x04000144 RID: 324
	public const string DataColumn_ExpressionAndReadOnly = "Cannot set expression because column cannot be made ReadOnly.";

	// Token: 0x04000145 RID: 325
	public const string DataColumn_ExpressionAndConstraint = "Cannot set Expression property on column {0}, because it is a part of a constraint.";

	// Token: 0x04000146 RID: 326
	public const string DataColumn_ExpressionInConstraint = "Cannot create a constraint based on Expression column {0}.";

	// Token: 0x04000147 RID: 327
	public const string DataColumn_ExpressionCircular = "Cannot set Expression property due to circular reference in the expression.";

	// Token: 0x04000148 RID: 328
	public const string DataColumn_NullKeyValues = "Column '{0}' has null values in it.";

	// Token: 0x04000149 RID: 329
	public const string DataColumn_NullValues = "Column '{0}' does not allow nulls.";

	// Token: 0x0400014A RID: 330
	public const string DataColumn_ReadOnly = "Column '{0}' is read only.";

	// Token: 0x0400014B RID: 331
	public const string DataColumn_NonUniqueValues = "Column '{0}' contains non-unique values.";

	// Token: 0x0400014C RID: 332
	public const string DataColumn_NotInTheTable = "Column '{0}' does not belong to table {1}.";

	// Token: 0x0400014D RID: 333
	public const string DataColumn_NotInAnyTable = "Column must belong to a table.";

	// Token: 0x0400014E RID: 334
	public const string DataColumn_SetFailed = "Couldn't store <{0}> in {1} Column.  Expected type is {2}.";

	// Token: 0x0400014F RID: 335
	public const string DataColumn_CannotSetToNull = "Cannot set Column '{0}' to be null. Please use DBNull instead.";

	// Token: 0x04000150 RID: 336
	public const string DataColumn_LongerThanMaxLength = "Cannot set column '{0}'. The value violates the MaxLength limit of this column.";

	// Token: 0x04000151 RID: 337
	public const string DataColumn_HasToBeStringType = "MaxLength applies to string data type only. You cannot set Column '{0}' property MaxLength to be non-negative number.";

	// Token: 0x04000152 RID: 338
	public const string DataColumn_CannotSetMaxLength = "Cannot set Column '{0}' property MaxLength to '{1}'. There is at least one string in the table longer than the new limit.";

	// Token: 0x04000153 RID: 339
	public const string DataColumn_CannotSetMaxLength2 = "Cannot set Column '{0}' property MaxLength. The Column is SimpleContent.";

	// Token: 0x04000154 RID: 340
	public const string DataColumn_CannotSimpleContentType = "Cannot set Column '{0}' property DataType to {1}. The Column is SimpleContent.";

	// Token: 0x04000155 RID: 341
	public const string DataColumn_CannotSimpleContent = "Cannot set Column '{0}' property MappingType to SimpleContent. The Column DataType is {1}.";

	// Token: 0x04000156 RID: 342
	public const string DataColumn_ExceedMaxLength = "Column '{0}' exceeds the MaxLength limit.";

	// Token: 0x04000157 RID: 343
	public const string DataColumn_NotAllowDBNull = "Column '{0}' does not allow DBNull.Value.";

	// Token: 0x04000158 RID: 344
	public const string DataColumn_CannotChangeNamespace = "Cannot change the Column '{0}' property Namespace. The Column is SimpleContent.";

	// Token: 0x04000159 RID: 345
	public const string DataColumn_AutoIncrementCannotSetIfHasData = "Cannot change AutoIncrement of a DataColumn with type '{0}' once it has data.";

	// Token: 0x0400015A RID: 346
	public const string DataColumn_NotInTheUnderlyingTable = "Column '{0}' does not belong to underlying table '{1}'.";

	// Token: 0x0400015B RID: 347
	public const string DataColumn_InvalidDataColumnMapping = "DataColumn with type '{0}' is a complexType. Can not serialize value of a complex type as Attribute";

	// Token: 0x0400015C RID: 348
	public const string DataColumn_CannotSetDateTimeModeForNonDateTimeColumns = "The DateTimeMode can be set only on DataColumns of type DateTime.";

	// Token: 0x0400015D RID: 349
	public const string DataColumn_DateTimeMode = "Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.";

	// Token: 0x0400015E RID: 350
	public const string DataColumn_INullableUDTwithoutStaticNull = "Type '{0}' does not contain static Null property or field.";

	// Token: 0x0400015F RID: 351
	public const string DataColumn_UDTImplementsIChangeTrackingButnotIRevertible = "Type '{0}' does not implement IRevertibleChangeTracking; therefore can not proceed with RejectChanges().";

	// Token: 0x04000160 RID: 352
	public const string DataColumn_SetAddedAndModifiedCalledOnNonUnchanged = "SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.";

	// Token: 0x04000161 RID: 353
	public const string DataColumn_OrdinalExceedMaximun = "Ordinal '{0}' exceeds the maximum number.";

	// Token: 0x04000162 RID: 354
	public const string DataColumn_NullableTypesNotSupported = "DataSet does not support System.Nullable<>.";

	// Token: 0x04000163 RID: 355
	public const string DataConstraint_NoName = "Cannot change the name of a constraint to empty string when it is in the ConstraintCollection.";

	// Token: 0x04000164 RID: 356
	public const string DataConstraint_Violation = "Cannot enforce constraints on constraint {0}.";

	// Token: 0x04000165 RID: 357
	public const string DataConstraint_ViolationValue = "Column '{0}' is constrained to be unique.  Value '{1}' is already present.";

	// Token: 0x04000166 RID: 358
	public const string DataConstraint_NotInTheTable = "Constraint '{0}' does not belong to this DataTable.";

	// Token: 0x04000167 RID: 359
	public const string DataConstraint_OutOfRange = "Cannot find constraint {0}.";

	// Token: 0x04000168 RID: 360
	public const string DataConstraint_Duplicate = "Constraint matches constraint named {0} already in collection.";

	// Token: 0x04000169 RID: 361
	public const string DataConstraint_DuplicateName = "A Constraint named '{0}' already belongs to this DataTable.";

	// Token: 0x0400016A RID: 362
	public const string DataConstraint_UniqueViolation = "These columns don't currently have unique values.";

	// Token: 0x0400016B RID: 363
	public const string DataConstraint_ForeignTable = "These columns don't point to this table.";

	// Token: 0x0400016C RID: 364
	public const string DataConstraint_ParentValues = "This constraint cannot be enabled as not all values have corresponding parent values.";

	// Token: 0x0400016D RID: 365
	public const string DataConstraint_AddFailed = "This constraint cannot be added since ForeignKey doesn't belong to table {0}.";

	// Token: 0x0400016E RID: 366
	public const string DataConstraint_RemoveFailed = "Cannot remove a constraint that doesn't belong to this table.";

	// Token: 0x0400016F RID: 367
	public const string DataConstraint_NeededForForeignKeyConstraint = "Cannot remove unique constraint '{0}'. Remove foreign key constraint '{1}' first.";

	// Token: 0x04000170 RID: 368
	public const string DataConstraint_CascadeDelete = "Cannot delete this row because constraints are enforced on relation {0}, and deleting this row will strand child rows.";

	// Token: 0x04000171 RID: 369
	public const string DataConstraint_CascadeUpdate = "Cannot make this change because constraints are enforced on relation {0}, and changing this value will strand child rows.";

	// Token: 0x04000172 RID: 370
	public const string DataConstraint_ClearParentTable = "Cannot clear table {0} because ForeignKeyConstraint {1} enforces constraints and there are child rows in {2}.";

	// Token: 0x04000173 RID: 371
	public const string DataConstraint_ForeignKeyViolation = "ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.";

	// Token: 0x04000174 RID: 372
	public const string DataConstraint_BadObjectPropertyAccess = "Property not accessible because '{0}'.";

	// Token: 0x04000175 RID: 373
	public const string DataConstraint_RemoveParentRow = "Cannot remove this row because it has child rows, and constraints on relation {0} are enforced.";

	// Token: 0x04000176 RID: 374
	public const string DataConstraint_AddPrimaryKeyConstraint = "Cannot add primary key constraint since primary key is already set for the table.";

	// Token: 0x04000177 RID: 375
	public const string DataConstraint_CantAddConstraintToMultipleNestedTable = "Cannot add constraint to DataTable '{0}' which is a child table in two nested relations.";

	// Token: 0x04000178 RID: 376
	public const string DataKey_TableMismatch = "Cannot create a Key from Columns that belong to different tables.";

	// Token: 0x04000179 RID: 377
	public const string DataKey_NoColumns = "Cannot have 0 columns.";

	// Token: 0x0400017A RID: 378
	public const string DataKey_TooManyColumns = "Cannot have more than {0} columns.";

	// Token: 0x0400017B RID: 379
	public const string DataKey_DuplicateColumns = "Cannot create a Key when the same column is listed more than once: '{0}'";

	// Token: 0x0400017C RID: 380
	public const string DataKey_RemovePrimaryKey = "Cannot remove unique constraint since it's the primary key of a table.";

	// Token: 0x0400017D RID: 381
	public const string DataKey_RemovePrimaryKey1 = "Cannot remove unique constraint since it's the primary key of table {0}.";

	// Token: 0x0400017E RID: 382
	public const string DataRelation_ColumnsTypeMismatch = "Parent Columns and Child Columns don't have type-matching columns.";

	// Token: 0x0400017F RID: 383
	public const string DataRelation_KeyColumnsIdentical = "ParentKey and ChildKey are identical.";

	// Token: 0x04000180 RID: 384
	public const string DataRelation_KeyLengthMismatch = "ParentColumns and ChildColumns should be the same length.";

	// Token: 0x04000181 RID: 385
	public const string DataRelation_KeyZeroLength = "ParentColumns and ChildColumns must not be zero length.";

	// Token: 0x04000182 RID: 386
	public const string DataRelation_ForeignRow = "The row doesn't belong to the same DataSet as this relation.";

	// Token: 0x04000183 RID: 387
	public const string DataRelation_NoName = "RelationName is required when it is part of a DataSet.";

	// Token: 0x04000184 RID: 388
	public const string DataRelation_ForeignTable = "GetChildRows requires a row whose Table is {0}, but the specified row's Table is {1}.";

	// Token: 0x04000185 RID: 389
	public const string DataRelation_ForeignDataSet = "This relation should connect two tables in this DataSet to be added to this DataSet.";

	// Token: 0x04000186 RID: 390
	public const string DataRelation_GetParentRowTableMismatch = "GetParentRow requires a row whose Table is {0}, but the specified row's Table is {1}.";

	// Token: 0x04000187 RID: 391
	public const string DataRelation_SetParentRowTableMismatch = "SetParentRow requires a child row whose Table is {0}, but the specified row's Table is {1}.";

	// Token: 0x04000188 RID: 392
	public const string DataRelation_DataSetMismatch = "Cannot have a relationship between tables in different DataSets.";

	// Token: 0x04000189 RID: 393
	public const string DataRelation_TablesInDifferentSets = "Cannot create a relation between tables in different DataSets.";

	// Token: 0x0400018A RID: 394
	public const string DataRelation_AlreadyExists = "A relation already exists for these child columns.";

	// Token: 0x0400018B RID: 395
	public const string DataRelation_DoesNotExist = "This relation doesn't belong to this relation collection.";

	// Token: 0x0400018C RID: 396
	public const string DataRelation_AlreadyInOtherDataSet = "This relation already belongs to another DataSet.";

	// Token: 0x0400018D RID: 397
	public const string DataRelation_AlreadyInTheDataSet = "This relation already belongs to this DataSet.";

	// Token: 0x0400018E RID: 398
	public const string DataRelation_DuplicateName = "A Relation named '{0}' already belongs to this DataSet.";

	// Token: 0x0400018F RID: 399
	public const string DataRelation_NotInTheDataSet = "Relation {0} does not belong to this DataSet.";

	// Token: 0x04000190 RID: 400
	public const string DataRelation_OutOfRange = "Cannot find relation {0}.";

	// Token: 0x04000191 RID: 401
	public const string DataRelation_TableNull = "Cannot create a collection on a null table.";

	// Token: 0x04000192 RID: 402
	public const string DataRelation_TableWasRemoved = "The table this collection displays relations for has been removed from its DataSet.";

	// Token: 0x04000193 RID: 403
	public const string DataRelation_ChildTableMismatch = "Cannot add a relation to this table's ParentRelation collection where this table isn't the child table.";

	// Token: 0x04000194 RID: 404
	public const string DataRelation_ParentTableMismatch = "Cannot add a relation to this table's ChildRelation collection where this table isn't the parent table.";

	// Token: 0x04000195 RID: 405
	public const string DataRelation_RelationNestedReadOnly = "Cannot set the 'Nested' property to false for this relation.";

	// Token: 0x04000196 RID: 406
	public const string DataRelation_TableCantBeNestedInTwoTables = "The same table '{0}' cannot be the child table in two nested relations.";

	// Token: 0x04000197 RID: 407
	public const string DataRelation_LoopInNestedRelations = "The table ({0}) cannot be the child table to itself in nested relations.";

	// Token: 0x04000198 RID: 408
	public const string DataRelation_CaseLocaleMismatch = "Cannot add a DataRelation or Constraint that has different Locale or CaseSensitive settings between its parent and child tables.";

	// Token: 0x04000199 RID: 409
	public const string DataRelation_ParentOrChildColumnsDoNotHaveDataSet = "Cannot create a DataRelation if Parent or Child Columns are not in a DataSet.";

	// Token: 0x0400019A RID: 410
	public const string DataRelation_InValidNestedRelation = "Nested table '{0}' which inherits its namespace cannot have multiple parent tables in different namespaces.";

	// Token: 0x0400019B RID: 411
	public const string DataRelation_InValidNamespaceInNestedRelation = "Nested table '{0}' with empty namespace cannot have multiple parent tables in different namespaces.";

	// Token: 0x0400019C RID: 412
	public const string DataRow_NotInTheDataSet = "The row doesn't belong to the same DataSet as this relation.";

	// Token: 0x0400019D RID: 413
	public const string DataRow_NotInTheTable = "Cannot perform this operation on a row not in the table.";

	// Token: 0x0400019E RID: 414
	public const string DataRow_ParentRowNotInTheDataSet = "This relation and child row don't belong to same DataSet.";

	// Token: 0x0400019F RID: 415
	public const string DataRow_EditInRowChanging = "Cannot change a proposed value in the RowChanging event.";

	// Token: 0x040001A0 RID: 416
	public const string DataRow_EndEditInRowChanging = "Cannot call EndEdit() inside an OnRowChanging event.";

	// Token: 0x040001A1 RID: 417
	public const string DataRow_BeginEditInRowChanging = "Cannot call BeginEdit() inside the RowChanging event.";

	// Token: 0x040001A2 RID: 418
	public const string DataRow_CancelEditInRowChanging = "Cannot call CancelEdit() inside an OnRowChanging event.  Throw an exception to cancel this update.";

	// Token: 0x040001A3 RID: 419
	public const string DataRow_DeleteInRowDeleting = "Cannot call Delete inside an OnRowDeleting event.  Throw an exception to cancel this delete.";

	// Token: 0x040001A4 RID: 420
	public const string DataRow_ValuesArrayLength = "Input array is longer than the number of columns in this table.";

	// Token: 0x040001A5 RID: 421
	public const string DataRow_NoCurrentData = "There is no Current data to access.";

	// Token: 0x040001A6 RID: 422
	public const string DataRow_NoOriginalData = "There is no Original data to access.";

	// Token: 0x040001A7 RID: 423
	public const string DataRow_NoProposedData = "There is no Proposed data to access.";

	// Token: 0x040001A8 RID: 424
	public const string DataRow_RemovedFromTheTable = "This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.";

	// Token: 0x040001A9 RID: 425
	public const string DataRow_DeletedRowInaccessible = "Deleted row information cannot be accessed through the row.";

	// Token: 0x040001AA RID: 426
	public const string DataRow_InvalidVersion = "Version must be Original, Current, or Proposed.";

	// Token: 0x040001AB RID: 427
	public const string DataRow_OutOfRange = "There is no row at position {0}.";

	// Token: 0x040001AC RID: 428
	public const string DataRow_RowInsertOutOfRange = "The row insert position {0} is invalid.";

	// Token: 0x040001AD RID: 429
	public const string DataRow_RowInsertMissing = "Values are missing in the rowOrder sequence for table '{0}'.";

	// Token: 0x040001AE RID: 430
	public const string DataRow_RowOutOfRange = "The given DataRow is not in the current DataRowCollection.";

	// Token: 0x040001AF RID: 431
	public const string DataRow_AlreadyInOtherCollection = "This row already belongs to another table.";

	// Token: 0x040001B0 RID: 432
	public const string DataRow_AlreadyInTheCollection = "This row already belongs to this table.";

	// Token: 0x040001B1 RID: 433
	public const string DataRow_AlreadyDeleted = "Cannot delete this row since it's already deleted.";

	// Token: 0x040001B2 RID: 434
	public const string DataRow_Empty = "This row is empty.";

	// Token: 0x040001B3 RID: 435
	public const string DataRow_AlreadyRemoved = "Cannot remove a row that's already been removed.";

	// Token: 0x040001B4 RID: 436
	public const string DataRow_MultipleParents = "A child row has multiple parents.";

	// Token: 0x040001B5 RID: 437
	public const string DataRow_InvalidRowBitPattern = "Unrecognized row state bit pattern.";

	// Token: 0x040001B6 RID: 438
	public const string DataSet_SetNameToEmpty = "Cannot change the name of the DataSet to an empty string.";

	// Token: 0x040001B7 RID: 439
	public const string DataSet_SetDataSetNameConflicting = "The name '{0}' is invalid. A DataSet cannot have the same name of the DataTable.";

	// Token: 0x040001B8 RID: 440
	public const string DataSet_UnsupportedSchema = "The schema namespace is invalid. Please use this one instead: {0}.";

	// Token: 0x040001B9 RID: 441
	public const string DataSet_CannotChangeCaseLocale = "Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.";

	// Token: 0x040001BA RID: 442
	public const string DataSet_CannotChangeSchemaSerializationMode = "SchemaSerializationMode property can be set only if it is overridden by derived DataSet.";

	// Token: 0x040001BB RID: 443
	public const string DataTable_ForeignPrimaryKey = "PrimaryKey columns do not belong to this table.";

	// Token: 0x040001BC RID: 444
	public const string DataTable_CannotAddToSimpleContent = "Cannot add a nested relation or an element column to a table containing a SimpleContent column.";

	// Token: 0x040001BD RID: 445
	public const string DataTable_NoName = "TableName is required when it is part of a DataSet.";

	// Token: 0x040001BE RID: 446
	public const string DataTable_MultipleSimpleContentColumns = "DataTable already has a simple content column.";

	// Token: 0x040001BF RID: 447
	public const string DataTable_MissingPrimaryKey = "Table doesn't have a primary key.";

	// Token: 0x040001C0 RID: 448
	public const string DataTable_InvalidSortString = " {0} isn't a valid Sort string entry.";

	// Token: 0x040001C1 RID: 449
	public const string DataTable_CanNotSerializeDataTableHierarchy = "Cannot serialize the DataTable. A DataTable being used in one or more DataColumn expressions is not a descendant of current DataTable.";

	// Token: 0x040001C2 RID: 450
	public const string DataTable_CanNotRemoteDataTable = "This DataTable can only be remoted as part of DataSet. One or more Expression Columns has reference to other DataTable(s).";

	// Token: 0x040001C3 RID: 451
	public const string DataTable_CanNotSetRemotingFormat = "Cannot have different remoting format property value for DataSet and DataTable.";

	// Token: 0x040001C4 RID: 452
	public const string DataTable_CanNotSerializeDataTableWithEmptyName = "Cannot serialize the DataTable. DataTable name is not set.";

	// Token: 0x040001C5 RID: 453
	public const string DataTable_DuplicateName = "A DataTable named '{0}' already belongs to this DataSet.";

	// Token: 0x040001C6 RID: 454
	public const string DataTable_DuplicateName2 = "A DataTable named '{0}' with the same Namespace '{1}' already belongs to this DataSet.";

	// Token: 0x040001C7 RID: 455
	public const string DataTable_SelfnestedDatasetConflictingName = "The table ({0}) cannot be the child table to itself in a nested relation: the DataSet name conflicts with the table name.";

	// Token: 0x040001C8 RID: 456
	public const string DataTable_DatasetConflictingName = "The name '{0}' is invalid. A DataTable cannot have the same name of the DataSet.";

	// Token: 0x040001C9 RID: 457
	public const string DataTable_AlreadyInOtherDataSet = "DataTable already belongs to another DataSet.";

	// Token: 0x040001CA RID: 458
	public const string DataTable_AlreadyInTheDataSet = "DataTable already belongs to this DataSet.";

	// Token: 0x040001CB RID: 459
	public const string DataTable_NotInTheDataSet = "Table {0} does not belong to this DataSet.";

	// Token: 0x040001CC RID: 460
	public const string DataTable_OutOfRange = "Cannot find table {0}.";

	// Token: 0x040001CD RID: 461
	public const string DataTable_InRelation = "Cannot remove a table that has existing relations.  Remove relations first.";

	// Token: 0x040001CE RID: 462
	public const string DataTable_InConstraint = "Cannot remove table {0}, because it referenced in ForeignKeyConstraint {1}.  Remove the constraint first.";

	// Token: 0x040001CF RID: 463
	public const string DataTable_TableNotFound = "DataTable '{0}' does not match to any DataTable in source.";

	// Token: 0x040001D0 RID: 464
	public const string DataMerge_MissingDefinition = "Target DataSet missing definition for {0}.";

	// Token: 0x040001D1 RID: 465
	public const string DataMerge_MissingConstraint = "Target DataSet missing {0} {1}.";

	// Token: 0x040001D2 RID: 466
	public const string DataMerge_DataTypeMismatch = "<target>.{0} and <source>.{0} have conflicting properties: DataType property mismatch.";

	// Token: 0x040001D3 RID: 467
	public const string DataMerge_PrimaryKeyMismatch = "<target>.PrimaryKey and <source>.PrimaryKey have different Length.";

	// Token: 0x040001D4 RID: 468
	public const string DataMerge_PrimaryKeyColumnsMismatch = "Mismatch columns in the PrimaryKey : <target>.{0} versus <source>.{1}.";

	// Token: 0x040001D5 RID: 469
	public const string DataMerge_ReltionKeyColumnsMismatch = "Relation {0} cannot be merged, because keys have mismatch columns.";

	// Token: 0x040001D6 RID: 470
	public const string DataMerge_MissingColumnDefinition = "Target table {0} missing definition for column {1}.";

	// Token: 0x040001D7 RID: 471
	public const string DataIndex_RecordStateRange = "The RowStates parameter must be set to a valid combination of values from the DataViewRowState enumeration.";

	// Token: 0x040001D8 RID: 472
	public const string DataIndex_FindWithoutSortOrder = "Find finds a row based on a Sort order, and no Sort order is specified.";

	// Token: 0x040001D9 RID: 473
	public const string DataIndex_KeyLength = "Expecting {0} value(s) for the key being indexed, but received {1} value(s).";

	// Token: 0x040001DA RID: 474
	public const string DataStorage_AggregateException = "Invalid usage of aggregate function {0}() and Type: {1}.";

	// Token: 0x040001DB RID: 475
	public const string DataStorage_InvalidStorageType = "Invalid storage type: {0}.";

	// Token: 0x040001DC RID: 476
	public const string DataStorage_ProblematicChars = "The DataSet Xml persistency does not support the value '{0}' as Char value, please use Byte storage instead.";

	// Token: 0x040001DD RID: 477
	public const string DataStorage_SetInvalidDataType = "Type of value has a mismatch with column type";

	// Token: 0x040001DE RID: 478
	public const string DataStorage_IComparableNotDefined = " Type '{0}' does not implement IComparable interface. Comparison cannot be done.";

	// Token: 0x040001DF RID: 479
	public const string DataView_SetFailed = "Cannot set {0}.";

	// Token: 0x040001E0 RID: 480
	public const string DataView_SetDataSetFailed = "Cannot change DataSet on a DataViewManager that's already the default view for a DataSet.";

	// Token: 0x040001E1 RID: 481
	public const string DataView_SetRowStateFilter = "RowStateFilter cannot show ModifiedOriginals and ModifiedCurrents at the same time.";

	// Token: 0x040001E2 RID: 482
	public const string DataView_SetTable = "Cannot change Table property on a DefaultView or a DataView coming from a DataViewManager.";

	// Token: 0x040001E3 RID: 483
	public const string DataView_CanNotSetDataSet = "Cannot change DataSet property once it is set.";

	// Token: 0x040001E4 RID: 484
	public const string DataView_CanNotUseDataViewManager = "DataSet must be set prior to using DataViewManager.";

	// Token: 0x040001E5 RID: 485
	public const string DataView_CanNotSetTable = "Cannot change Table property once it is set.";

	// Token: 0x040001E6 RID: 486
	public const string DataView_CanNotUse = "DataTable must be set prior to using DataView.";

	// Token: 0x040001E7 RID: 487
	public const string DataView_CanNotBindTable = "Cannot bind to DataTable with no name.";

	// Token: 0x040001E8 RID: 488
	public const string DataView_SetIListObject = "Cannot set an object into this list.";

	// Token: 0x040001E9 RID: 489
	public const string DataView_AddNewNotAllowNull = "Cannot call AddNew on a DataView where AllowNew is false.";

	// Token: 0x040001EA RID: 490
	public const string DataView_NotOpen = "DataView is not open.";

	// Token: 0x040001EB RID: 491
	public const string DataView_CreateChildView = "The relation is not parented to the table to which this DataView points.";

	// Token: 0x040001EC RID: 492
	public const string DataView_CanNotDelete = "Cannot delete on a DataSource where AllowDelete is false.";

	// Token: 0x040001ED RID: 493
	public const string DataView_CanNotEdit = "Cannot edit on a DataSource where AllowEdit is false.";

	// Token: 0x040001EE RID: 494
	public const string DataView_GetElementIndex = "Index {0} is either negative or above rows count.";

	// Token: 0x040001EF RID: 495
	public const string DataView_AddExternalObject = "Cannot add external objects to this list.";

	// Token: 0x040001F0 RID: 496
	public const string DataView_CanNotClear = "Cannot clear this list.";

	// Token: 0x040001F1 RID: 497
	public const string DataView_InsertExternalObject = "Cannot insert external objects to this list.";

	// Token: 0x040001F2 RID: 498
	public const string DataView_RemoveExternalObject = "Cannot remove objects not in the list.";

	// Token: 0x040001F3 RID: 499
	public const string DataROWView_PropertyNotFound = "{0} is neither a DataColumn nor a DataRelation for table {1}.";

	// Token: 0x040001F4 RID: 500
	public const string Range_Argument = "Min ({0}) must be less than or equal to max ({1}) in a Range object.";

	// Token: 0x040001F5 RID: 501
	public const string Range_NullRange = "This is a null range.";

	// Token: 0x040001F6 RID: 502
	public const string RecordManager_MinimumCapacity = "MinimumCapacity must be non-negative.";

	// Token: 0x040001F7 RID: 503
	public const string SqlConvert_ConvertFailed = " Cannot convert object of type '{0}' to object of type '{1}'.";

	// Token: 0x040001F8 RID: 504
	public const string DataSet_DefaultDataException = "Data Exception.";

	// Token: 0x040001F9 RID: 505
	public const string DataSet_DefaultConstraintException = "Constraint Exception.";

	// Token: 0x040001FA RID: 506
	public const string DataSet_DefaultDeletedRowInaccessibleException = "Deleted rows inaccessible.";

	// Token: 0x040001FB RID: 507
	public const string DataSet_DefaultDuplicateNameException = "Duplicate name not allowed.";

	// Token: 0x040001FC RID: 508
	public const string DataSet_DefaultInRowChangingEventException = "Operation not supported in the RowChanging event.";

	// Token: 0x040001FD RID: 509
	public const string DataSet_DefaultInvalidConstraintException = "Invalid constraint.";

	// Token: 0x040001FE RID: 510
	public const string DataSet_DefaultMissingPrimaryKeyException = "Missing primary key.";

	// Token: 0x040001FF RID: 511
	public const string DataSet_DefaultNoNullAllowedException = "Null not allowed.";

	// Token: 0x04000200 RID: 512
	public const string DataSet_DefaultReadOnlyException = "Column is marked read only.";

	// Token: 0x04000201 RID: 513
	public const string DataSet_DefaultRowNotInTableException = "Row not found in table.";

	// Token: 0x04000202 RID: 514
	public const string DataSet_DefaultVersionNotFoundException = "Version not found.";

	// Token: 0x04000203 RID: 515
	public const string Load_ReadOnlyDataModified = "ReadOnly Data is Modified.";

	// Token: 0x04000204 RID: 516
	public const string DataTableReader_InvalidDataTableReader = "DataTableReader is invalid for current DataTable '{0}'.";

	// Token: 0x04000205 RID: 517
	public const string DataTableReader_SchemaInvalidDataTableReader = "Schema of current DataTable '{0}' in DataTableReader has changed, DataTableReader is invalid.";

	// Token: 0x04000206 RID: 518
	public const string DataTableReader_CannotCreateDataReaderOnEmptyDataSet = "DataTableReader Cannot be created. There is no DataTable in DataSet.";

	// Token: 0x04000207 RID: 519
	public const string DataTableReader_DataTableReaderArgumentIsEmpty = "Cannot create DataTableReader. Argument is Empty.";

	// Token: 0x04000208 RID: 520
	public const string DataTableReader_ArgumentContainsNullValue = "Cannot create DataTableReader. Arguments contain null value.";

	// Token: 0x04000209 RID: 521
	public const string DataTableReader_InvalidRowInDataTableReader = "Current DataRow is either in Deleted or Detached state.";

	// Token: 0x0400020A RID: 522
	public const string DataTableReader_DataTableCleared = "Current DataTable '{0}' is empty. There is no DataRow in DataTable.";

	// Token: 0x0400020B RID: 523
	public const string RbTree_InvalidState = "DataTable internal index is corrupted: '{0}'.";

	// Token: 0x0400020C RID: 524
	public const string RbTree_EnumerationBroken = "Collection was modified; enumeration operation might not execute.";

	// Token: 0x0400020D RID: 525
	public const string NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration = "Simple type '{0}' has already be declared with different '{1}'.";

	// Token: 0x0400020E RID: 526
	public const string DataDom_Foliation = "Invalid foliation.";

	// Token: 0x0400020F RID: 527
	public const string DataDom_TableNameChange = "Cannot change the table name once the associated DataSet is mapped to a loaded XML document.";

	// Token: 0x04000210 RID: 528
	public const string DataDom_TableNamespaceChange = "Cannot change the table namespace once the associated DataSet is mapped to a loaded XML document.";

	// Token: 0x04000211 RID: 529
	public const string DataDom_ColumnNameChange = "Cannot change the column name once the associated DataSet is mapped to a loaded XML document.";

	// Token: 0x04000212 RID: 530
	public const string DataDom_ColumnNamespaceChange = "Cannot change the column namespace once the associated DataSet is mapped to a loaded XML document.";

	// Token: 0x04000213 RID: 531
	public const string DataDom_ColumnMappingChange = "Cannot change the ColumnMapping property once the associated DataSet is mapped to a loaded XML document.";

	// Token: 0x04000214 RID: 532
	public const string DataDom_TableColumnsChange = "Cannot add or remove columns from the table once the DataSet is mapped to a loaded XML document.";

	// Token: 0x04000215 RID: 533
	public const string DataDom_DataSetTablesChange = "Cannot add or remove tables from the DataSet once the DataSet is mapped to a loaded XML document.";

	// Token: 0x04000216 RID: 534
	public const string DataDom_DataSetNestedRelationsChange = "Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.";

	// Token: 0x04000217 RID: 535
	public const string DataDom_DataSetNull = "The DataSet parameter is invalid. It cannot be null.";

	// Token: 0x04000218 RID: 536
	public const string DataDom_DataSetNameChange = "Cannot change the DataSet name once the DataSet is mapped to a loaded XML document.";

	// Token: 0x04000219 RID: 537
	public const string DataDom_CloneNode = "This type of node cannot be cloned: {0}.";

	// Token: 0x0400021A RID: 538
	public const string DataDom_MultipleLoad = "Cannot load XmlDataDocument if it already contains data. Please use a new XmlDataDocument.";

	// Token: 0x0400021B RID: 539
	public const string DataDom_MultipleDataSet = "DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.";

	// Token: 0x0400021C RID: 540
	public const string DataDom_NotSupport_GetElementById = "GetElementById() is not supported on DataDocument.";

	// Token: 0x0400021D RID: 541
	public const string DataDom_NotSupport_EntRef = "Cannot create entity references on DataDocument.";

	// Token: 0x0400021E RID: 542
	public const string DataDom_NotSupport_Clear = "Clear function on DateSet and DataTable is not supported on XmlDataDocument.";

	// Token: 0x0400021F RID: 543
	public const string ADP_EmptyArray = "Expecting non-empty array for '{0}' parameter.";

	// Token: 0x04000220 RID: 544
	public const string SQL_WrongType = "Expecting argument of type {1}, but received type {0}.";

	// Token: 0x04000221 RID: 545
	public const string ADP_InvalidConnectionOptionValue = "Invalid value for key '{0}'.";

	// Token: 0x04000222 RID: 546
	public const string ADP_KeywordNotSupported = "Keyword not supported: '{0}'.";

	// Token: 0x04000223 RID: 547
	public const string ADP_InternalProviderError = "Internal .Net Framework Data Provider error {0}.";

	// Token: 0x04000224 RID: 548
	public const string ADP_NoQuoteChange = "The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.";

	// Token: 0x04000225 RID: 549
	public const string ADP_MissingSourceCommand = "The DataAdapter.SelectCommand property needs to be initialized.";

	// Token: 0x04000226 RID: 550
	public const string ADP_MissingSourceCommandConnection = "The DataAdapter.SelectCommand.Connection property needs to be initialized;";

	// Token: 0x04000227 RID: 551
	public const string ADP_InvalidMultipartName = "{0} \"{1}\".";

	// Token: 0x04000228 RID: 552
	public const string ADP_InvalidMultipartNameQuoteUsage = "{0} \"{1}\", incorrect usage of quotes.";

	// Token: 0x04000229 RID: 553
	public const string ADP_InvalidMultipartNameToManyParts = "{0} \"{1}\", the current limit of \"{2}\" is insufficient.";

	// Token: 0x0400022A RID: 554
	public const string ADP_ColumnSchemaExpression = "The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.";

	// Token: 0x0400022B RID: 555
	public const string ADP_ColumnSchemaMismatch = "Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.";

	// Token: 0x0400022C RID: 556
	public const string ADP_ColumnSchemaMissing1 = "Missing the DataColumn '{0}' for the SourceColumn '{2}'.";

	// Token: 0x0400022D RID: 557
	public const string ADP_ColumnSchemaMissing2 = "Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.";

	// Token: 0x0400022E RID: 558
	public const string ADP_InvalidSourceColumn = "SourceColumn is required to be a non-empty string.";

	// Token: 0x0400022F RID: 559
	public const string ADP_MissingColumnMapping = "Missing SourceColumn mapping for '{0}'.";

	// Token: 0x04000230 RID: 560
	public const string ADP_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the {2} method.";

	// Token: 0x04000231 RID: 561
	public const string ADP_MissingTableSchema = "Missing the '{0}' DataTable for the '{1}' SourceTable.";

	// Token: 0x04000232 RID: 562
	public const string ADP_InvalidSourceTable = "SourceTable is required to be a non-empty string";

	// Token: 0x04000233 RID: 563
	public const string ADP_MissingTableMapping = "Missing SourceTable mapping: '{0}'";

	// Token: 0x04000234 RID: 564
	public const string ADP_ConnectionRequired_Insert = "Update requires the InsertCommand to have a connection object. The Connection property of the InsertCommand has not been initialized.";

	// Token: 0x04000235 RID: 565
	public const string ADP_ConnectionRequired_Update = "Update requires the UpdateCommand to have a connection object. The Connection property of the UpdateCommand has not been initialized.";

	// Token: 0x04000236 RID: 566
	public const string ADP_ConnectionRequired_Delete = "Update requires the DeleteCommand to have a connection object. The Connection property of the DeleteCommand has not been initialized.";

	// Token: 0x04000237 RID: 567
	public const string ADP_ConnectionRequired_Batch = "Update requires a connection object.  The Connection property has not been initialized.";

	// Token: 0x04000238 RID: 568
	public const string ADP_ConnectionRequired_Clone = "Update requires the command clone to have a connection object. The Connection property of the command clone has not been initialized.";

	// Token: 0x04000239 RID: 569
	public const string ADP_OpenConnectionRequired_Insert = "Update requires the {0}Command to have an open connection object. {1}";

	// Token: 0x0400023A RID: 570
	public const string ADP_OpenConnectionRequired_Update = "Update requires the {0}Command to have an open connection object. {1}";

	// Token: 0x0400023B RID: 571
	public const string ADP_OpenConnectionRequired_Delete = "Update requires the {0}Command to have an open connection object. {1}";

	// Token: 0x0400023C RID: 572
	public const string ADP_OpenConnectionRequired_Clone = "Update requires the updating command to have an open connection object. {1}";

	// Token: 0x0400023D RID: 573
	public const string ADP_MissingSelectCommand = "The SelectCommand property has not been initialized before calling '{0}'.";

	// Token: 0x0400023E RID: 574
	public const string ADP_UnwantedStatementType = "The StatementType {0} is not expected here.";

	// Token: 0x0400023F RID: 575
	public const string ADP_FillSchemaRequiresSourceTableName = "FillSchema: expected a non-empty string for the SourceTable name.";

	// Token: 0x04000240 RID: 576
	public const string ADP_FillRequiresSourceTableName = "Fill: expected a non-empty string for the SourceTable name.";

	// Token: 0x04000241 RID: 577
	public const string ADP_FillChapterAutoIncrement = "Hierarchical chapter columns must map to an AutoIncrement DataColumn.";

	// Token: 0x04000242 RID: 578
	public const string ADP_MissingDataReaderFieldType = "DataReader.GetFieldType({0}) returned null.";

	// Token: 0x04000243 RID: 579
	public const string ADP_OnlyOneTableForStartRecordOrMaxRecords = "Only specify one item in the dataTables array when using non-zero values for startRecords or maxRecords.";

	// Token: 0x04000244 RID: 580
	public const string ADP_UpdateRequiresSourceTable = "Update unable to find TableMapping['{0}'] or DataTable '{0}'.";

	// Token: 0x04000245 RID: 581
	public const string ADP_UpdateRequiresSourceTableName = "Update: expected a non-empty SourceTable name.";

	// Token: 0x04000246 RID: 582
	public const string ADP_UpdateRequiresCommandClone = "Update requires the command clone to be valid.";

	// Token: 0x04000247 RID: 583
	public const string ADP_UpdateRequiresCommandSelect = "Auto SQL generation during Update requires a valid SelectCommand.";

	// Token: 0x04000248 RID: 584
	public const string ADP_UpdateRequiresCommandInsert = "Update requires a valid InsertCommand when passed DataRow collection with new rows.";

	// Token: 0x04000249 RID: 585
	public const string ADP_UpdateRequiresCommandUpdate = "Update requires a valid UpdateCommand when passed DataRow collection with modified rows.";

	// Token: 0x0400024A RID: 586
	public const string ADP_UpdateRequiresCommandDelete = "Update requires a valid DeleteCommand when passed DataRow collection with deleted rows.";

	// Token: 0x0400024B RID: 587
	public const string ADP_UpdateMismatchRowTable = "DataRow[{0}] is from a different DataTable than DataRow[0].";

	// Token: 0x0400024C RID: 588
	public const string ADP_RowUpdatedErrors = "RowUpdatedEvent: Errors occurred; no additional is information available.";

	// Token: 0x0400024D RID: 589
	public const string ADP_RowUpdatingErrors = "RowUpdatingEvent: Errors occurred; no additional is information available.";

	// Token: 0x0400024E RID: 590
	public const string ADP_ResultsNotAllowedDuringBatch = "When batching, the command's UpdatedRowSource property value of UpdateRowSource.FirstReturnedRecord or UpdateRowSource.Both is invalid.";

	// Token: 0x0400024F RID: 591
	public const string ADP_UpdateConcurrencyViolation_Update = "Concurrency violation: the UpdateCommand affected {0} of the expected {1} records.";

	// Token: 0x04000250 RID: 592
	public const string ADP_UpdateConcurrencyViolation_Delete = "Concurrency violation: the DeleteCommand affected {0} of the expected {1} records.";

	// Token: 0x04000251 RID: 593
	public const string ADP_UpdateConcurrencyViolation_Batch = "Concurrency violation: the batched command affected {0} of the expected {1} records.";

	// Token: 0x04000252 RID: 594
	public const string ADP_InvalidSourceBufferIndex = "Invalid source buffer (size of {0}) offset: {1}";

	// Token: 0x04000253 RID: 595
	public const string ADP_InvalidDestinationBufferIndex = "Invalid destination buffer (size of {0}) offset: {1}";

	// Token: 0x04000254 RID: 596
	public const string ADP_StreamClosed = "Invalid attempt to {0} when stream is closed.";

	// Token: 0x04000255 RID: 597
	public const string ADP_InvalidSeekOrigin = "Specified SeekOrigin value is invalid.";

	// Token: 0x04000256 RID: 598
	public const string ADP_DynamicSQLJoinUnsupported = "Dynamic SQL generation is not supported against multiple base tables.";

	// Token: 0x04000257 RID: 599
	public const string ADP_DynamicSQLNoTableInfo = "Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.";

	// Token: 0x04000258 RID: 600
	public const string ADP_DynamicSQLNoKeyInfoDelete = "Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.";

	// Token: 0x04000259 RID: 601
	public const string ADP_DynamicSQLNoKeyInfoUpdate = "Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.";

	// Token: 0x0400025A RID: 602
	public const string ADP_DynamicSQLNoKeyInfoRowVersionDelete = "Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.";

	// Token: 0x0400025B RID: 603
	public const string ADP_DynamicSQLNoKeyInfoRowVersionUpdate = "Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.";

	// Token: 0x0400025C RID: 604
	public const string ADP_DynamicSQLNestedQuote = "Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.";

	// Token: 0x0400025D RID: 605
	public const string SQL_InvalidBufferSizeOrIndex = "Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.";

	// Token: 0x0400025E RID: 606
	public const string SQL_InvalidDataLength = "Data length '{0}' is less than 0.";

	// Token: 0x0400025F RID: 607
	public const string SqlMisc_NullString = "Null";

	// Token: 0x04000260 RID: 608
	public const string SqlMisc_MessageString = "Message";

	// Token: 0x04000261 RID: 609
	public const string SqlMisc_ArithOverflowMessage = "Arithmetic Overflow.";

	// Token: 0x04000262 RID: 610
	public const string SqlMisc_DivideByZeroMessage = "Divide by zero error encountered.";

	// Token: 0x04000263 RID: 611
	public const string SqlMisc_NullValueMessage = "Data is Null. This method or property cannot be called on Null values.";

	// Token: 0x04000264 RID: 612
	public const string SqlMisc_TruncationMessage = "Numeric arithmetic causes truncation.";

	// Token: 0x04000265 RID: 613
	public const string SqlMisc_DateTimeOverflowMessage = "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";

	// Token: 0x04000266 RID: 614
	public const string SqlMisc_ConcatDiffCollationMessage = "Two strings to be concatenated have different collation.";

	// Token: 0x04000267 RID: 615
	public const string SqlMisc_CompareDiffCollationMessage = "Two strings to be compared have different collation.";

	// Token: 0x04000268 RID: 616
	public const string SqlMisc_InvalidFlagMessage = "Invalid flag value.";

	// Token: 0x04000269 RID: 617
	public const string SqlMisc_NumeToDecOverflowMessage = "Conversion from SqlDecimal to Decimal overflows.";

	// Token: 0x0400026A RID: 618
	public const string SqlMisc_ConversionOverflowMessage = "Conversion overflows.";

	// Token: 0x0400026B RID: 619
	public const string SqlMisc_InvalidDateTimeMessage = "Invalid SqlDateTime.";

	// Token: 0x0400026C RID: 620
	public const string SqlMisc_TimeZoneSpecifiedMessage = "A time zone was specified. SqlDateTime does not support time zones.";

	// Token: 0x0400026D RID: 621
	public const string SqlMisc_InvalidArraySizeMessage = "Invalid array size.";

	// Token: 0x0400026E RID: 622
	public const string SqlMisc_InvalidPrecScaleMessage = "Invalid numeric precision/scale.";

	// Token: 0x0400026F RID: 623
	public const string SqlMisc_FormatMessage = "The input wasn't in a correct format.";

	// Token: 0x04000270 RID: 624
	public const string SqlMisc_SqlTypeMessage = "SqlType error.";

	// Token: 0x04000271 RID: 625
	public const string SqlMisc_NoBufferMessage = "There is no buffer. Read or write operation failed.";

	// Token: 0x04000272 RID: 626
	public const string SqlMisc_BufferInsufficientMessage = "The buffer is insufficient. Read or write operation failed.";

	// Token: 0x04000273 RID: 627
	public const string SqlMisc_WriteNonZeroOffsetOnNullMessage = "Cannot write to non-zero offset, because current value is Null.";

	// Token: 0x04000274 RID: 628
	public const string SqlMisc_WriteOffsetLargerThanLenMessage = "Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.";

	// Token: 0x04000275 RID: 629
	public const string SqlMisc_NotFilledMessage = "SQL Type has not been loaded with data.";

	// Token: 0x04000276 RID: 630
	public const string SqlMisc_AlreadyFilledMessage = "SQL Type has already been loaded with data.";

	// Token: 0x04000277 RID: 631
	public const string SqlMisc_ClosedXmlReaderMessage = "Invalid attempt to access a closed XmlReader.";

	// Token: 0x04000278 RID: 632
	public const string SqlMisc_InvalidOpStreamClosed = "Invalid attempt to call {0} when the stream is closed.";

	// Token: 0x04000279 RID: 633
	public const string SqlMisc_InvalidOpStreamNonWritable = "Invalid attempt to call {0} when the stream non-writable.";

	// Token: 0x0400027A RID: 634
	public const string SqlMisc_InvalidOpStreamNonReadable = "Invalid attempt to call {0} when the stream non-readable.";

	// Token: 0x0400027B RID: 635
	public const string SqlMisc_InvalidOpStreamNonSeekable = "Invalid attempt to call {0} when the stream is non-seekable.";

	// Token: 0x0400027C RID: 636
	public const string ADP_DBConcurrencyExceptionMessage = "DB concurrency violation.";

	// Token: 0x0400027D RID: 637
	public const string ADP_InvalidMaxRecords = "The MaxRecords value of {0} is invalid; the value must be >= 0.";

	// Token: 0x0400027E RID: 638
	public const string ADP_CollectionIndexInt32 = "Invalid index {0} for this {1} with Count={2}.";

	// Token: 0x0400027F RID: 639
	public const string ADP_MissingTableMappingDestination = "Missing TableMapping when TableMapping.DataSetTable='{0}'.";

	// Token: 0x04000280 RID: 640
	public const string ADP_InvalidStartRecord = "The StartRecord value of {0} is invalid; the value must be >= 0.";

	// Token: 0x04000281 RID: 641
	public const string DataDom_EnforceConstraintsShouldBeOff = "Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.";

	// Token: 0x04000282 RID: 642
	public const string DataColumns_RemoveExpression = "Cannot remove this column, because it is part of an expression: {0} = {1}.";

	// Token: 0x04000283 RID: 643
	public const string DataRow_RowInsertTwice = "The rowOrder value={0} has been found twice for table named '{1}'.";

	// Token: 0x04000284 RID: 644
	public const string Xml_ElementTypeNotFound = "Cannot find ElementType name='{0}'.";

	// Token: 0x04000285 RID: 645
	public const string ADP_DbProviderFactories_InvariantNameNotFound = "The specified invariant name '{0}' wasn't found in the list of registered .NET Data Providers.";

	// Token: 0x04000286 RID: 646
	public const string ADP_DbProviderFactories_NoInstance = "The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.";

	// Token: 0x04000287 RID: 647
	public const string ADP_DbProviderFactories_FactoryNotLoadable = "The registered .NET Data Provider's DbProviderFactory implementation type '{0}' couldn't be loaded.";

	// Token: 0x04000288 RID: 648
	public const string ADP_DbProviderFactories_NoAssemblyQualifiedName = "The missing .NET Data Provider's assembly qualified name is required.";

	// Token: 0x04000289 RID: 649
	public const string ADP_DbProviderFactories_NotAFactoryType = "The type '{0}' doesn't inherit from DbProviderFactory.";

	// Token: 0x0400028A RID: 650
	public const string ADP_ConnectionAlreadyOpen = "The connection was not closed. {0}";

	// Token: 0x0400028B RID: 651
	public const string ADP_InternalConnectionError = "Internal DbConnection Error: {0}";

	// Token: 0x0400028C RID: 652
	public const string ADP_InvalidOffsetValue = "Invalid parameter Offset value '{0}'. The value must be greater than or equal to 0.";

	// Token: 0x0400028D RID: 653
	public const string ADP_TransactionPresent = "Connection currently has transaction enlisted.  Finish current transaction and retry.";

	// Token: 0x0400028E RID: 654
	public const string ADP_LocalTransactionPresent = "Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry.";

	// Token: 0x0400028F RID: 655
	public const string ADP_NoConnectionString = "The ConnectionString property has not been initialized.";

	// Token: 0x04000290 RID: 656
	public const string ADP_OpenConnectionPropertySet = "Not allowed to change the '{0}' property. {1}";

	// Token: 0x04000291 RID: 657
	public const string ADP_PendingAsyncOperation = "Can not start another operation while there is an asynchronous operation pending.";

	// Token: 0x04000292 RID: 658
	public const string ADP_PooledOpenTimeout = "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.";

	// Token: 0x04000293 RID: 659
	public const string ADP_NonPooledOpenTimeout = "Timeout attempting to open the connection.  The time period elapsed prior to attempting to open the connection has been exceeded.  This may have occurred because of too many simultaneous non-pooled connection attempts.";

	// Token: 0x04000294 RID: 660
	public const string ADP_SingleValuedProperty = "The only acceptable value for the property '{0}' is '{1}'.";

	// Token: 0x04000295 RID: 661
	public const string ADP_DoubleValuedProperty = "The acceptable values for the property '{0}' are '{1}' or '{2}'.";

	// Token: 0x04000296 RID: 662
	public const string ADP_InvalidPrefixSuffix = "Specified QuotePrefix and QuoteSuffix values do not match.";

	// Token: 0x04000297 RID: 663
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x04000298 RID: 664
	public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

	// Token: 0x04000299 RID: 665
	public const string Arg_RemoveArgNotFound = "Cannot remove the specified item because it was not found in the specified Collection.";

	// Token: 0x0400029A RID: 666
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x0400029B RID: 667
	public const string ADP_DeriveParametersNotSupported = "{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType. {1}.";

	// Token: 0x0400029C RID: 668
	public const string ADP_NoStoredProcedureExists = "The stored procedure '{0}' doesn't exist.";

	// Token: 0x0400029D RID: 669
	public const string ADP_MissingConnectionOptionValue = "Use of key '{0}' requires the key '{1}' to be present.";

	// Token: 0x0400029E RID: 670
	public const string ADP_InvalidConnectionOptionValueLength = "The value's length for key '{0}' exceeds it's limit of '{1}'.";

	// Token: 0x0400029F RID: 671
	public const string SQL_SqlCommandCommandText = "SqlCommand.DeriveParameters failed because the SqlCommand.CommandText property value is an invalid multipart name";

	// Token: 0x040002A0 RID: 672
	public const string SQL_BatchedUpdatesNotAvailableOnContextConnection = "Batching updates is not supported on the context connection.";

	// Token: 0x040002A1 RID: 673
	public const string SQL_BulkCopyDestinationTableName = "SqlBulkCopy.WriteToServer failed because the SqlBulkCopy.DestinationTableName is an invalid multipart name";

	// Token: 0x040002A2 RID: 674
	public const string SQL_TDSParserTableName = "Processing of results from SQL Server failed because of an invalid multipart name";

	// Token: 0x040002A3 RID: 675
	public const string SQL_TypeName = "SqlParameter.TypeName is an invalid multipart name";

	// Token: 0x040002A4 RID: 676
	public const string SQLMSF_FailoverPartnerNotSupported = "Connecting to a mirrored SQL Server instance using the MultiSubnetFailover connection option is not supported.";

	// Token: 0x040002A5 RID: 677
	public const string SQL_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the .Net Framework SqlClient Data Provider.";

	// Token: 0x040002A6 RID: 678
	public const string ADP_CommandTextRequired = "{0}: CommandText property has not been initialized";

	// Token: 0x040002A7 RID: 679
	public const string ADP_ConnectionRequired = "{0}: Connection property has not been initialized.";

	// Token: 0x040002A8 RID: 680
	public const string ADP_OpenConnectionRequired = "{0} requires an open and available Connection. {1}";

	// Token: 0x040002A9 RID: 681
	public const string ADP_TransactionConnectionMismatch = "The transaction is either not associated with the current connection or has been completed.";

	// Token: 0x040002AA RID: 682
	public const string ADP_TransactionRequired = "{0} requires the command to have a transaction when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.";

	// Token: 0x040002AB RID: 683
	public const string ADP_OpenReaderExists = "There is already an open DataReader associated with this Command which must be closed first.";

	// Token: 0x040002AC RID: 684
	public const string ADP_CalledTwice = "The method '{0}' cannot be called more than once for the same execution.";

	// Token: 0x040002AD RID: 685
	public const string ADP_InvalidCommandTimeout = "Invalid CommandTimeout value {0}; the value must be >= 0.";

	// Token: 0x040002AE RID: 686
	public const string ADP_UninitializedParameterSize = "{1}[{0}]: the Size property has an invalid size of 0.";

	// Token: 0x040002AF RID: 687
	public const string ADP_PrepareParameterType = "{0}.Prepare method requires all parameters to have an explicitly set type.";

	// Token: 0x040002B0 RID: 688
	public const string ADP_PrepareParameterSize = "{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.";

	// Token: 0x040002B1 RID: 689
	public const string ADP_PrepareParameterScale = "{0}.Prepare method requires parameters of type '{1}' have an explicitly set Precision and Scale.";

	// Token: 0x040002B2 RID: 690
	public const string ADP_MismatchedAsyncResult = "Mismatched end method call for asyncResult.  Expected call to {0} but {1} was called instead.";

	// Token: 0x040002B3 RID: 691
	public const string ADP_ClosedConnectionError = "Invalid operation. The connection is closed.";

	// Token: 0x040002B4 RID: 692
	public const string ADP_ConnectionIsDisabled = "The connection has been disabled.";

	// Token: 0x040002B5 RID: 693
	public const string ADP_EmptyDatabaseName = "Database cannot be null, the empty string, or string of only whitespace.";

	// Token: 0x040002B6 RID: 694
	public const string ADP_NonSequentialColumnAccess = "Invalid attempt to read from column ordinal '{0}'.  With CommandBehavior.SequentialAccess, you may only read from column ordinal '{1}' or greater.";

	// Token: 0x040002B7 RID: 695
	public const string ADP_InvalidDataType = "The parameter data type of {0} is invalid.";

	// Token: 0x040002B8 RID: 696
	public const string ADP_UnknownDataType = "No mapping exists from object type {0} to a known managed provider native type.";

	// Token: 0x040002B9 RID: 697
	public const string ADP_UnknownDataTypeCode = "Unable to handle an unknown TypeCode {0} returned by Type {1}.";

	// Token: 0x040002BA RID: 698
	public const string ADP_DbTypeNotSupported = "No mapping exists from DbType {0} to a known {1}.";

	// Token: 0x040002BB RID: 699
	public const string ADP_VersionDoesNotSupportDataType = "The version of SQL Server in use does not support datatype '{0}'.";

	// Token: 0x040002BC RID: 700
	public const string ADP_ParameterValueOutOfRange = "Parameter value '{0}' is out of range.";

	// Token: 0x040002BD RID: 701
	public const string ADP_BadParameterName = "Specified parameter name '{0}' is not valid.";

	// Token: 0x040002BE RID: 702
	public const string ADP_InvalidSizeValue = "Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.";

	// Token: 0x040002BF RID: 703
	public const string ADP_NegativeParameter = "Invalid value for argument '{0}'. The value must be greater than or equal to 0.";

	// Token: 0x040002C0 RID: 704
	public const string ADP_InvalidMetaDataValue = "Invalid value for this metadata.";

	// Token: 0x040002C1 RID: 705
	public const string ADP_ParameterConversionFailed = "Failed to convert parameter value from a {0} to a {1}.";

	// Token: 0x040002C2 RID: 706
	public const string ADP_ParallelTransactionsNotSupported = "{0} does not support parallel transactions.";

	// Token: 0x040002C3 RID: 707
	public const string ADP_TransactionZombied = "This {0} has completed; it is no longer usable.";

	// Token: 0x040002C4 RID: 708
	public const string ADP_InvalidDataLength2 = "Specified length '{0}' is out of range.";

	// Token: 0x040002C5 RID: 709
	public const string ADP_NonSeqByteAccess = "Invalid {2} attempt at dataIndex '{0}'.  With CommandBehavior.SequentialAccess, you may only read from dataIndex '{1}' or greater.";

	// Token: 0x040002C6 RID: 710
	public const string ADP_InvalidMinMaxPoolSizeValues = "Invalid min or max pool size values, min pool size cannot be greater than the max pool size.";

	// Token: 0x040002C7 RID: 711
	public const string SQL_InvalidPacketSizeValue = "Invalid 'Packet Size'.  The value must be an integer >= 512 and <= 32768.";

	// Token: 0x040002C8 RID: 712
	public const string SQL_NullEmptyTransactionName = "Invalid transaction or invalid name for a point at which to save within the transaction.";

	// Token: 0x040002C9 RID: 713
	public const string SQL_UserInstanceFailoverNotCompatible = "User Instance and Failover are not compatible options.  Please choose only one of the two in the connection string.";

	// Token: 0x040002CA RID: 714
	public const string SQL_EncryptionNotSupportedByClient = "The instance of SQL Server you attempted to connect to requires encryption but this machine does not support it.";

	// Token: 0x040002CB RID: 715
	public const string SQL_EncryptionNotSupportedByServer = "The instance of SQL Server you attempted to connect to does not support encryption.";

	// Token: 0x040002CC RID: 716
	public const string SQL_InvalidSQLServerVersionUnknown = "Unsupported SQL Server version.  The .Net Framework SqlClient Data Provider can only be used with SQL Server versions 7.0 and later.";

	// Token: 0x040002CD RID: 717
	public const string SQL_CannotCreateNormalizer = "Cannot create normalizer for '{0}'.";

	// Token: 0x040002CE RID: 718
	public const string SQL_CannotModifyPropertyAsyncOperationInProgress = "{0} cannot be changed while async operation is in progress.";

	// Token: 0x040002CF RID: 719
	public const string SQL_InstanceFailure = "Instance failure.";

	// Token: 0x040002D0 RID: 720
	public const string SQL_InvalidPartnerConfiguration = "Server {0}, database {1} is not configured for database mirroring.";

	// Token: 0x040002D1 RID: 721
	public const string SQL_MarsUnsupportedOnConnection = "The connection does not support MultipleActiveResultSets.";

	// Token: 0x040002D2 RID: 722
	public const string SQL_NonLocalSSEInstance = "SSE Instance re-direction is not supported for non-local user instances.";

	// Token: 0x040002D3 RID: 723
	public const string SQL_PendingBeginXXXExists = "The command execution cannot proceed due to a pending asynchronous operation already in progress.";

	// Token: 0x040002D4 RID: 724
	public const string SQL_NonXmlResult = "Invalid command sent to ExecuteXmlReader.  The command must return an Xml result.";

	// Token: 0x040002D5 RID: 725
	public const string SQL_InvalidParameterTypeNameFormat = "Invalid 3 part name format for TypeName.";

	// Token: 0x040002D6 RID: 726
	public const string SQL_InvalidParameterNameLength = "The length of the parameter '{0}' exceeds the limit of 128 characters.";

	// Token: 0x040002D7 RID: 727
	public const string SQL_PrecisionValueOutOfRange = "Precision value '{0}' is either less than 0 or greater than the maximum allowed precision of 38.";

	// Token: 0x040002D8 RID: 728
	public const string SQL_ScaleValueOutOfRange = "Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 38.";

	// Token: 0x040002D9 RID: 729
	public const string SQL_TimeScaleValueOutOfRange = "Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 7.";

	// Token: 0x040002DA RID: 730
	public const string SQL_ParameterInvalidVariant = "Parameter '{0}' exceeds the size limit for the sql_variant datatype.";

	// Token: 0x040002DB RID: 731
	public const string SQL_ParameterTypeNameRequired = "The {0} type parameter '{1}' must have a valid type name.";

	// Token: 0x040002DC RID: 732
	public const string SQL_InvalidInternalPacketSize = "Invalid internal packet size:";

	// Token: 0x040002DD RID: 733
	public const string SQL_InvalidTDSVersion = "The SQL Server instance returned an invalid or unsupported protocol version during login negotiation.";

	// Token: 0x040002DE RID: 734
	public const string SQL_InvalidTDSPacketSize = "Invalid Packet Size.";

	// Token: 0x040002DF RID: 735
	public const string SQL_ParsingError = "Internal connection fatal error.";

	// Token: 0x040002E0 RID: 736
	public const string SQL_ConnectionLockedForBcpEvent = "The connection cannot be used because there is an ongoing operation that must be finished.";

	// Token: 0x040002E1 RID: 737
	public const string SQL_SNIPacketAllocationFailure = "Memory allocation for internal connection failed.";

	// Token: 0x040002E2 RID: 738
	public const string SQL_SmallDateTimeOverflow = "SqlDbType.SmallDateTime overflow.  Value '{0}' is out of range.  Must be between 1/1/1900 12:00:00 AM and 6/6/2079 11:59:59 PM.";

	// Token: 0x040002E3 RID: 739
	public const string SQL_TimeOverflow = "SqlDbType.Time overflow.  Value '{0}' is out of range.  Must be between 00:00:00.0000000 and 23:59:59.9999999.";

	// Token: 0x040002E4 RID: 740
	public const string SQL_MoneyOverflow = "SqlDbType.SmallMoney overflow.  Value '{0}' is out of range.  Must be between -214,748.3648 and 214,748.3647.";

	// Token: 0x040002E5 RID: 741
	public const string SQL_CultureIdError = "The Collation specified by SQL Server is not supported.";

	// Token: 0x040002E6 RID: 742
	public const string SQL_OperationCancelled = "Operation cancelled by user.";

	// Token: 0x040002E7 RID: 743
	public const string SQL_SevereError = "A severe error occurred on the current command.  The results, if any, should be discarded.";

	// Token: 0x040002E8 RID: 744
	public const string SQL_SSPIGenerateError = "Failed to generate SSPI context.";

	// Token: 0x040002E9 RID: 745
	public const string SQL_KerberosTicketMissingError = "Cannot authenticate using Kerberos. Ensure Kerberos has been initialized on the client with 'kinit' and a Service Principal Name has been registered for the SQL Server to allow Kerberos authentication.";

	// Token: 0x040002EA RID: 746
	public const string SQL_SqlServerBrowserNotAccessible = "Cannot connect to SQL Server Browser. Ensure SQL Server Browser has been started.";

	// Token: 0x040002EB RID: 747
	public const string SQL_InvalidSSPIPacketSize = "Invalid SSPI packet size.";

	// Token: 0x040002EC RID: 748
	public const string SQL_SSPIInitializeError = "Cannot initialize SSPI package.";

	// Token: 0x040002ED RID: 749
	public const string SQL_Timeout = "Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.";

	// Token: 0x040002EE RID: 750
	public const string SQL_Timeout_PreLogin_Begin = "Connection Timeout Expired.  The timeout period elapsed at the start of the pre-login phase.  This could be because of insufficient time provided for connection timeout.";

	// Token: 0x040002EF RID: 751
	public const string SQL_Timeout_PreLogin_InitializeConnection = "Connection Timeout Expired.  The timeout period elapsed while attempting to create and initialize a socket to the server.  This could be either because the server was unreachable or unable to respond back in time.";

	// Token: 0x040002F0 RID: 752
	public const string SQL_Timeout_PreLogin_SendHandshake = "Connection Timeout Expired.  The timeout period elapsed while making a pre-login handshake request.  This could be because the server was unable to respond back in time.";

	// Token: 0x040002F1 RID: 753
	public const string SQL_Timeout_PreLogin_ConsumeHandshake = "Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement.  This could be because the pre-login handshake failed or the server was unable to respond back in time.";

	// Token: 0x040002F2 RID: 754
	public const string SQL_Timeout_Login_Begin = "Connection Timeout Expired.  The timeout period elapsed at the start of the login phase.  This could be because of insufficient time provided for connection timeout.";

	// Token: 0x040002F3 RID: 755
	public const string SQL_Timeout_Login_ProcessConnectionAuth = "Connection Timeout Expired.  The timeout period elapsed while attempting to authenticate the login.  This could be because the server failed to authenticate the user or the server was unable to respond back in time.";

	// Token: 0x040002F4 RID: 756
	public const string SQL_Timeout_PostLogin = "Connection Timeout Expired.  The timeout period elapsed during the post-login phase.  The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.";

	// Token: 0x040002F5 RID: 757
	public const string SQL_Timeout_FailoverInfo = "This failure occurred while attempting to connect to the {0} server.";

	// Token: 0x040002F6 RID: 758
	public const string SQL_Timeout_RoutingDestinationInfo = "This failure occurred while attempting to connect to the routing destination. The duration spent while attempting to connect to the original server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4};  ";

	// Token: 0x040002F7 RID: 759
	public const string SQL_Duration_PreLogin_Begin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0};";

	// Token: 0x040002F8 RID: 760
	public const string SQL_Duration_PreLoginHandshake = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; ";

	// Token: 0x040002F9 RID: 761
	public const string SQL_Duration_Login_Begin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; ";

	// Token: 0x040002FA RID: 762
	public const string SQL_Duration_Login_ProcessConnectionAuth = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; ";

	// Token: 0x040002FB RID: 763
	public const string SQL_Duration_PostLogin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4}; ";

	// Token: 0x040002FC RID: 764
	public const string SQL_UserInstanceFailure = "A user instance was requested in the connection string but the server specified does not support this option.";

	// Token: 0x040002FD RID: 765
	public const string SQL_InvalidRead = "Invalid attempt to read when no data is present.";

	// Token: 0x040002FE RID: 766
	public const string SQL_NonBlobColumn = "Invalid attempt to GetBytes on column '{0}'.  The GetBytes function can only be used on columns of type Text, NText, or Image.";

	// Token: 0x040002FF RID: 767
	public const string SQL_NonCharColumn = "Invalid attempt to GetChars on column '{0}'.  The GetChars function can only be used on columns of type Text, NText, Xml, VarChar or NVarChar.";

	// Token: 0x04000300 RID: 768
	public const string SQL_StreamNotSupportOnColumnType = "Invalid attempt to GetStream on column '{0}'. The GetStream function can only be used on columns of type Binary, Image, Udt or VarBinary.";

	// Token: 0x04000301 RID: 769
	public const string SQL_TextReaderNotSupportOnColumnType = "Invalid attempt to GetTextReader on column '{0}'. The GetTextReader function can only be used on columns of type Char, NChar, NText, NVarChar, Text or VarChar.";

	// Token: 0x04000302 RID: 770
	public const string SQL_XmlReaderNotSupportOnColumnType = "Invalid attempt to GetXmlReader on column '{0}'. The GetXmlReader function can only be used on columns of type Xml.";

	// Token: 0x04000303 RID: 771
	public const string SqlDelegatedTransaction_PromotionFailed = "Failure while attempting to promote transaction.";

	// Token: 0x04000304 RID: 772
	public const string SQL_BulkLoadMappingInaccessible = "The mapped collection is in use and cannot be accessed at this time;";

	// Token: 0x04000305 RID: 773
	public const string SQL_BulkLoadMappingsNamesOrOrdinalsOnly = "Mappings must be either all name or all ordinal based.";

	// Token: 0x04000306 RID: 774
	public const string SQL_BulkLoadCannotConvertValue = "The given value of type {0} from the data source cannot be converted to type {1} of the specified target column.";

	// Token: 0x04000307 RID: 775
	public const string SQL_BulkLoadNonMatchingColumnMapping = "The given ColumnMapping does not match up with any column in the source or destination.";

	// Token: 0x04000308 RID: 776
	public const string SQL_BulkLoadNonMatchingColumnName = "The given ColumnName '{0}' does not match up with any column in data source.";

	// Token: 0x04000309 RID: 777
	public const string SQL_BulkLoadStringTooLong = "String or binary data would be truncated.";

	// Token: 0x0400030A RID: 778
	public const string SQL_BulkLoadInvalidTimeout = "Timeout Value '{0}' is less than 0.";

	// Token: 0x0400030B RID: 779
	public const string SQL_BulkLoadInvalidVariantValue = "Value cannot be converted to SqlVariant.";

	// Token: 0x0400030C RID: 780
	public const string SQL_BulkLoadExistingTransaction = "Unexpected existing transaction.";

	// Token: 0x0400030D RID: 781
	public const string SQL_BulkLoadNoCollation = "Failed to obtain column collation information for the destination table. If the table is not in the current database the name must be qualified using the database name (e.g. [mydb]..[mytable](e.g. [mydb]..[mytable]); this also applies to temporary-tables (e.g. #mytable would be specified as tempdb..#mytable).";

	// Token: 0x0400030E RID: 782
	public const string SQL_BulkLoadConflictingTransactionOption = "Must not specify SqlBulkCopyOption.UseInternalTransaction and pass an external Transaction at the same time.";

	// Token: 0x0400030F RID: 783
	public const string SQL_BulkLoadInvalidOperationInsideEvent = "Function must not be called during event.";

	// Token: 0x04000310 RID: 784
	public const string SQL_BulkLoadMissingDestinationTable = "The DestinationTableName property must be set before calling this method.";

	// Token: 0x04000311 RID: 785
	public const string SQL_BulkLoadInvalidDestinationTable = "Cannot access destination table '{0}'.";

	// Token: 0x04000312 RID: 786
	public const string SQL_BulkLoadNotAllowDBNull = "Column '{0}' does not allow DBNull.Value.";

	// Token: 0x04000313 RID: 787
	public const string Sql_BulkLoadLcidMismatch = "The locale id '{0}' of the source column '{1}' and the locale id '{2}' of the destination column '{3}' do not match.";

	// Token: 0x04000314 RID: 788
	public const string SQL_BulkLoadPendingOperation = "Attempt to invoke bulk copy on an object that has a pending operation.";

	// Token: 0x04000315 RID: 789
	public const string SQL_CannotGetDTCAddress = "Unable to get the address of the distributed transaction coordinator for the server, from the server.  Is DTC enabled on the server?";

	// Token: 0x04000316 RID: 790
	public const string SQL_ConnectionDoomed = "The requested operation cannot be completed because the connection has been broken.";

	// Token: 0x04000317 RID: 791
	public const string SQL_OpenResultCountExceeded = "Open result count exceeded.";

	// Token: 0x04000318 RID: 792
	public const string SQL_StreamWriteNotSupported = "The Stream does not support writing.";

	// Token: 0x04000319 RID: 793
	public const string SQL_StreamReadNotSupported = "The Stream does not support reading.";

	// Token: 0x0400031A RID: 794
	public const string SQL_StreamSeekNotSupported = "The Stream does not support seeking.";

	// Token: 0x0400031B RID: 795
	public const string SQL_ExClientConnectionId = "ClientConnectionId:{0}";

	// Token: 0x0400031C RID: 796
	public const string SQL_ExErrorNumberStateClass = "Error Number:{0},State:{1},Class:{2}";

	// Token: 0x0400031D RID: 797
	public const string SQL_ExOriginalClientConnectionId = "ClientConnectionId before routing:{0}";

	// Token: 0x0400031E RID: 798
	public const string SQL_ExRoutingDestination = "Routing Destination:{0}";

	// Token: 0x0400031F RID: 799
	public const string SQL_UnsupportedSysTxVersion = "The currently loaded System.Transactions.dll does not support Global Transactions.";

	// Token: 0x04000320 RID: 800
	public const string SqlMisc_StreamErrorMessage = "An error occurred while reading.";

	// Token: 0x04000321 RID: 801
	public const string SqlMisc_TruncationMaxDataMessage = "Data returned is larger than 2Gb in size. Use SequentialAccess command behavior in order to get all of the data.";

	// Token: 0x04000322 RID: 802
	public const string SqlMisc_SubclassMustOverride = "Subclass did not override a required method.";

	// Token: 0x04000323 RID: 803
	public const string SqlUdtReason_NoUdtAttribute = "no UDT attribute";

	// Token: 0x04000324 RID: 804
	public const string SQLUDT_InvalidSqlType = "Specified type is not registered on the target server. {0}.";

	// Token: 0x04000325 RID: 805
	public const string Sql_InternalError = "Internal Error";

	// Token: 0x04000326 RID: 806
	public const string ADP_OperationAborted = "Operation aborted.";

	// Token: 0x04000327 RID: 807
	public const string ADP_OperationAbortedExceptionMessage = "Operation aborted due to an exception (see InnerException for details).";

	// Token: 0x04000328 RID: 808
	public const string ADP_TransactionCompletedButNotDisposed = "The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.";

	// Token: 0x04000329 RID: 809
	public const string SqlParameter_UnsupportedTVPOutputParameter = "ParameterDirection '{0}' specified for parameter '{1}' is not supported. Table-valued parameters only support ParameterDirection.Input.";

	// Token: 0x0400032A RID: 810
	public const string SqlParameter_DBNullNotSupportedForTVP = "DBNull value for parameter '{0}' is not supported. Table-valued parameters cannot be DBNull.";

	// Token: 0x0400032B RID: 811
	public const string SqlParameter_UnexpectedTypeNameForNonStruct = "TypeName specified for parameter '{0}'.  TypeName must only be set for Structured parameters.";

	// Token: 0x0400032C RID: 812
	public const string NullSchemaTableDataTypeNotSupported = "DateType column for field '{0}' in schema table is null.  DataType must be non-null.";

	// Token: 0x0400032D RID: 813
	public const string InvalidSchemaTableOrdinals = "Invalid column ordinals in schema table.  ColumnOrdinals, if present, must not have duplicates or gaps.";

	// Token: 0x0400032E RID: 814
	public const string SQL_EnumeratedRecordMetaDataChanged = "Metadata for field '{0}' of record '{1}' did not match the original record's metadata.";

	// Token: 0x0400032F RID: 815
	public const string SQL_EnumeratedRecordFieldCountChanged = "Number of fields in record '{0}' does not match the number in the original record.";

	// Token: 0x04000330 RID: 816
	public const string GT_Disabled = "Global Transactions are not enabled for this Azure SQL Database. Please contact Azure SQL Database support for assistance.";

	// Token: 0x04000331 RID: 817
	public const string SQL_UnknownSysTxIsolationLevel = "Unrecognized System.Transactions.IsolationLevel enumeration value: {0}.";

	// Token: 0x04000332 RID: 818
	public const string SQLNotify_AlreadyHasCommand = "This SqlCommand object is already associated with another SqlDependency object.";

	// Token: 0x04000333 RID: 819
	public const string SqlDependency_DatabaseBrokerDisabled = "The SQL Server Service Broker for the current database is not enabled, and as a result query notifications are not supported.  Please enable the Service Broker for this database if you wish to use notifications.";

	// Token: 0x04000334 RID: 820
	public const string SqlDependency_DefaultOptionsButNoStart = "When using SqlDependency without providing an options value, SqlDependency.Start() must be called prior to execution of a command added to the SqlDependency instance.";

	// Token: 0x04000335 RID: 821
	public const string SqlDependency_NoMatchingServerStart = "When using SqlDependency without providing an options value, SqlDependency.Start() must be called for each server that is being executed against.";

	// Token: 0x04000336 RID: 822
	public const string SqlDependency_NoMatchingServerDatabaseStart = "SqlDependency.Start has been called for the server the command is executing against more than once, but there is no matching server/user/database Start() call for current command.";

	// Token: 0x04000337 RID: 823
	public const string SqlDependency_EventNoDuplicate = "SqlDependency.OnChange does not support multiple event registrations for the same delegate.";

	// Token: 0x04000338 RID: 824
	public const string SqlDependency_IdMismatch = "No SqlDependency exists for the key.";

	// Token: 0x04000339 RID: 825
	public const string SqlDependency_InvalidTimeout = "Timeout specified is invalid. Timeout cannot be < 0.";

	// Token: 0x0400033A RID: 826
	public const string SqlDependency_DuplicateStart = "SqlDependency does not support calling Start() with different connection strings having the same server, user, and database in the same app domain.";

	// Token: 0x0400033B RID: 827
	public const string SqlMetaData_InvalidSqlDbTypeForConstructorFormat = "The dbType {0} is invalid for this constructor.";

	// Token: 0x0400033C RID: 828
	public const string SqlMetaData_NameTooLong = "The name is too long.";

	// Token: 0x0400033D RID: 829
	public const string SqlMetaData_SpecifyBothSortOrderAndOrdinal = "The sort order and ordinal must either both be specified, or neither should be specified (SortOrder.Unspecified and -1).  The values given were: order = {0}, ordinal = {1}.";

	// Token: 0x0400033E RID: 830
	public const string SqlProvider_InvalidDataColumnType = "The type of column '{0}' is not supported.  The type is '{1}'";

	// Token: 0x0400033F RID: 831
	public const string SqlProvider_NotEnoughColumnsInStructuredType = "There are not enough fields in the Structured type.  Structured types must have at least one field.";

	// Token: 0x04000340 RID: 832
	public const string SqlProvider_DuplicateSortOrdinal = "The sort ordinal {0} was specified twice.";

	// Token: 0x04000341 RID: 833
	public const string SqlProvider_MissingSortOrdinal = "The sort ordinal {0} was not specified.";

	// Token: 0x04000342 RID: 834
	public const string SqlProvider_SortOrdinalGreaterThanFieldCount = "The sort ordinal {0} on field {1} exceeds the total number of fields.";

	// Token: 0x04000343 RID: 835
	public const string SQLUDT_MaxByteSizeValue = "range: 0-8000";

	// Token: 0x04000344 RID: 836
	public const string SQLUDT_Unexpected = "unexpected error encountered in SqlClient data provider. {0}";

	// Token: 0x04000345 RID: 837
	public const string SQLUDT_UnexpectedUdtTypeName = "UdtTypeName property must be set only for UDT parameters.";

	// Token: 0x04000346 RID: 838
	public const string SQLUDT_InvalidUdtTypeName = "UdtTypeName property must be set for UDT parameters.";

	// Token: 0x04000347 RID: 839
	public const string SqlUdt_InvalidUdtMessage = "'{0}' is an invalid user defined type, reason: {1}.";

	// Token: 0x04000348 RID: 840
	public const string SQL_UDTTypeName = "SqlParameter.UdtTypeName is an invalid multipart name";

	// Token: 0x04000349 RID: 841
	public const string SQL_InvalidUdt3PartNameFormat = "Invalid 3 part name format for UdtTypeName.";

	// Token: 0x0400034A RID: 842
	public const string IEnumerableOfSqlDataRecordHasNoRows = "There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead.";

	// Token: 0x0400034B RID: 843
	public const string SNI_ERROR_1 = "I/O Error detected in read/write operation";

	// Token: 0x0400034C RID: 844
	public const string SNI_ERROR_2 = "Connection was terminated";

	// Token: 0x0400034D RID: 845
	public const string SNI_ERROR_3 = "Asynchronous operations not supported";

	// Token: 0x0400034E RID: 846
	public const string SNI_ERROR_5 = "Invalid parameter(s) found";

	// Token: 0x0400034F RID: 847
	public const string SNI_ERROR_6 = "Unsupported protocol specified";

	// Token: 0x04000350 RID: 848
	public const string SNI_ERROR_7 = "Invalid connection found when setting up new session protocol";

	// Token: 0x04000351 RID: 849
	public const string SNI_ERROR_8 = "Protocol not supported";

	// Token: 0x04000352 RID: 850
	public const string SNI_ERROR_9 = "Associating port with I/O completion mechanism failed";

	// Token: 0x04000353 RID: 851
	public const string SNI_ERROR_11 = "Timeout error";

	// Token: 0x04000354 RID: 852
	public const string SNI_ERROR_12 = "No server name supplied";

	// Token: 0x04000355 RID: 853
	public const string SNI_ERROR_13 = "TerminateListener() has been called";

	// Token: 0x04000356 RID: 854
	public const string SNI_ERROR_14 = "Win9x not supported";

	// Token: 0x04000357 RID: 855
	public const string SNI_ERROR_15 = "Function not supported";

	// Token: 0x04000358 RID: 856
	public const string SNI_ERROR_16 = "Shared-Memory heap error";

	// Token: 0x04000359 RID: 857
	public const string SNI_ERROR_17 = "Cannot find an ip/ipv6 type address to connect";

	// Token: 0x0400035A RID: 858
	public const string SNI_ERROR_18 = "Connection has been closed by peer";

	// Token: 0x0400035B RID: 859
	public const string SNI_ERROR_19 = "Physical connection is not usable";

	// Token: 0x0400035C RID: 860
	public const string SNI_ERROR_20 = "Connection has been closed";

	// Token: 0x0400035D RID: 861
	public const string SNI_ERROR_21 = "Encryption is enforced but there is no valid certificate";

	// Token: 0x0400035E RID: 862
	public const string SNI_ERROR_22 = "Couldn't load library";

	// Token: 0x0400035F RID: 863
	public const string SNI_ERROR_23 = "Cannot open a new thread in server process";

	// Token: 0x04000360 RID: 864
	public const string SNI_ERROR_24 = "Cannot post event to completion port";

	// Token: 0x04000361 RID: 865
	public const string SNI_ERROR_25 = "Connection string is not valid";

	// Token: 0x04000362 RID: 866
	public const string SNI_ERROR_26 = "Error Locating Server/Instance Specified";

	// Token: 0x04000363 RID: 867
	public const string SNI_ERROR_27 = "Error getting enabled protocols list from registry";

	// Token: 0x04000364 RID: 868
	public const string SNI_ERROR_28 = "Server doesn't support requested protocol";

	// Token: 0x04000365 RID: 869
	public const string SNI_ERROR_29 = "Shared Memory is not supported for clustered server connectivity";

	// Token: 0x04000366 RID: 870
	public const string SNI_ERROR_30 = "Invalid attempt bind to shared memory segment";

	// Token: 0x04000367 RID: 871
	public const string SNI_ERROR_31 = "Encryption(ssl/tls) handshake failed";

	// Token: 0x04000368 RID: 872
	public const string SNI_ERROR_32 = "Packet size too large for SSL Encrypt/Decrypt operations";

	// Token: 0x04000369 RID: 873
	public const string SNI_ERROR_33 = "SSRP error";

	// Token: 0x0400036A RID: 874
	public const string SNI_ERROR_34 = "Could not connect to the Shared Memory pipe";

	// Token: 0x0400036B RID: 875
	public const string SNI_ERROR_35 = "An internal exception was caught";

	// Token: 0x0400036C RID: 876
	public const string SNI_ERROR_36 = "The Shared Memory dll used to connect to SQL Server 2000 was not found";

	// Token: 0x0400036D RID: 877
	public const string SNI_ERROR_37 = "The SQL Server 2000 Shared Memory client dll appears to be invalid/corrupted";

	// Token: 0x0400036E RID: 878
	public const string SNI_ERROR_38 = "Cannot open a Shared Memory connection to SQL Server 2000";

	// Token: 0x0400036F RID: 879
	public const string SNI_ERROR_39 = "Shared memory connectivity to SQL Server 2000 is either disabled or not available on this machine";

	// Token: 0x04000370 RID: 880
	public const string SNI_ERROR_40 = "Could not open a connection to SQL Server";

	// Token: 0x04000371 RID: 881
	public const string SNI_ERROR_41 = "Cannot open a Shared Memory connection to a remote SQL server";

	// Token: 0x04000372 RID: 882
	public const string SNI_ERROR_42 = "Could not establish dedicated administrator connection (DAC) on default port. Make sure that DAC is enabled";

	// Token: 0x04000373 RID: 883
	public const string SNI_ERROR_43 = "An error occurred while obtaining the dedicated administrator connection (DAC) port. Make sure that SQL Browser is running, or check the error log for the port number";

	// Token: 0x04000374 RID: 884
	public const string SNI_ERROR_44 = "Could not compose Service Principal Name (SPN) for Windows Integrated Authentication. Possible causes are server(s) incorrectly specified to connection API calls, Domain Name System (DNS) lookup failure or memory shortage";

	// Token: 0x04000375 RID: 885
	public const string SNI_ERROR_47 = "Connecting with the MultiSubnetFailover connection option to a SQL Server instance configured with more than 64 IP addresses is not supported.";

	// Token: 0x04000376 RID: 886
	public const string SNI_ERROR_48 = "Connecting to a named SQL Server instance using the MultiSubnetFailover connection option is not supported.";

	// Token: 0x04000377 RID: 887
	public const string SNI_ERROR_49 = "Connecting to a SQL Server instance using the MultiSubnetFailover connection option is only supported when using the TCP protocol.";

	// Token: 0x04000378 RID: 888
	public const string SNI_ERROR_50 = "Local Database Runtime error occurred. ";

	// Token: 0x04000379 RID: 889
	public const string SNI_ERROR_51 = "An instance name was not specified while connecting to a Local Database Runtime. Specify an instance name in the format (localdb)\\instance_name.";

	// Token: 0x0400037A RID: 890
	public const string SNI_ERROR_52 = "Unable to locate a Local Database Runtime installation. Verify that SQL Server Express is properly installed and that the Local Database Runtime feature is enabled.";

	// Token: 0x0400037B RID: 891
	public const string SNI_ERROR_53 = "Invalid Local Database Runtime registry configuration found. Verify that SQL Server Express is properly installed.";

	// Token: 0x0400037C RID: 892
	public const string SNI_ERROR_54 = "Unable to locate the registry entry for SQLUserInstance.dll file path. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

	// Token: 0x0400037D RID: 893
	public const string SNI_ERROR_55 = "Registry value contains an invalid SQLUserInstance.dll file path. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

	// Token: 0x0400037E RID: 894
	public const string SNI_ERROR_56 = "Unable to load the SQLUserInstance.dll from the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

	// Token: 0x0400037F RID: 895
	public const string SNI_ERROR_57 = "Invalid SQLUserInstance.dll found at the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

	// Token: 0x04000380 RID: 896
	public const string Snix_Connect = "A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections.";

	// Token: 0x04000381 RID: 897
	public const string Snix_PreLoginBeforeSuccessfulWrite = "The client was unable to establish a connection because of an error during connection initialization process before login. Possible causes include the following:  the client tried to connect to an unsupported version of SQL Server; the server was too busy to accept new connections; or there was a resource limitation (insufficient memory or maximum allowed connections) on the server.";

	// Token: 0x04000382 RID: 898
	public const string Snix_PreLogin = "A connection was successfully established with the server, but then an error occurred during the pre-login handshake.";

	// Token: 0x04000383 RID: 899
	public const string Snix_LoginSspi = "A connection was successfully established with the server, but then an error occurred when obtaining the security/SSPI context information for integrated security login.";

	// Token: 0x04000384 RID: 900
	public const string Snix_Login = "A connection was successfully established with the server, but then an error occurred during the login process.";

	// Token: 0x04000385 RID: 901
	public const string Snix_EnableMars = "Connection open and login was successful, but then an error occurred while enabling MARS for this connection.";

	// Token: 0x04000386 RID: 902
	public const string Snix_AutoEnlist = "Connection open and login was successful, but then an error occurred while enlisting the connection into the current distributed transaction.";

	// Token: 0x04000387 RID: 903
	public const string Snix_GetMarsSession = "Failed to establish a MARS session in preparation to send the request to the server.";

	// Token: 0x04000388 RID: 904
	public const string Snix_Execute = "A transport-level error has occurred when sending the request to the server.";

	// Token: 0x04000389 RID: 905
	public const string Snix_Read = "A transport-level error has occurred when receiving results from the server.";

	// Token: 0x0400038A RID: 906
	public const string Snix_Close = "A transport-level error has occurred during connection clean-up.";

	// Token: 0x0400038B RID: 907
	public const string Snix_SendRows = "A transport-level error has occurred while sending information to the server.";

	// Token: 0x0400038C RID: 908
	public const string Snix_ProcessSspi = "A transport-level error has occurred during SSPI handshake.";

	// Token: 0x0400038D RID: 909
	public const string LocalDB_FailedGetDLLHandle = "Local Database Runtime: Cannot load SQLUserInstance.dll.";

	// Token: 0x0400038E RID: 910
	public const string LocalDB_MethodNotFound = "Invalid SQLUserInstance.dll found at the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

	// Token: 0x0400038F RID: 911
	public const string LocalDB_UnobtainableMessage = "Cannot obtain Local Database Runtime error message";

	// Token: 0x04000390 RID: 912
	public const string SQLROR_RecursiveRoutingNotSupported = "Two or more redirections have occurred. Only one redirection per login is allowed.";

	// Token: 0x04000391 RID: 913
	public const string SQLROR_FailoverNotSupported = "Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported.";

	// Token: 0x04000392 RID: 914
	public const string SQLROR_UnexpectedRoutingInfo = "Unexpected routing information received.";

	// Token: 0x04000393 RID: 915
	public const string SQLROR_InvalidRoutingInfo = "Invalid routing information received.";

	// Token: 0x04000394 RID: 916
	public const string SQLROR_TimeoutAfterRoutingInfo = "Server provided routing information, but timeout already expired.";

	// Token: 0x04000395 RID: 917
	public const string SQLCR_InvalidConnectRetryCountValue = "Invalid ConnectRetryCount value (should be 0-255).";

	// Token: 0x04000396 RID: 918
	public const string SQLCR_InvalidConnectRetryIntervalValue = "Invalid ConnectRetryInterval value (should be 1-60).";

	// Token: 0x04000397 RID: 919
	public const string SQLCR_NextAttemptWillExceedQueryTimeout = "Next reconnection attempt will exceed query timeout. Reconnection was terminated.";

	// Token: 0x04000398 RID: 920
	public const string SQLCR_EncryptionChanged = "The server did not preserve SSL encryption during a recovery attempt, connection recovery is not possible.";

	// Token: 0x04000399 RID: 921
	public const string SQLCR_TDSVestionNotPreserved = "The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible.";

	// Token: 0x0400039A RID: 922
	public const string SQLCR_AllAttemptsFailed = "The connection is broken and recovery is not possible.  The client driver attempted to recover the connection one or more times and all attempts failed.  Increase the value of ConnectRetryCount to increase the number of recovery attempts.";

	// Token: 0x0400039B RID: 923
	public const string SQLCR_UnrecoverableServer = "The connection is broken and recovery is not possible.  The connection is marked by the server as unrecoverable.  No attempt was made to restore the connection.";

	// Token: 0x0400039C RID: 924
	public const string SQLCR_UnrecoverableClient = "The connection is broken and recovery is not possible.  The connection is marked by the client driver as unrecoverable.  No attempt was made to restore the connection.";

	// Token: 0x0400039D RID: 925
	public const string SQLCR_NoCRAckAtReconnection = "The server did not acknowledge a recovery attempt, connection recovery is not possible.";

	// Token: 0x0400039E RID: 926
	public const string SQL_UnsupportedKeyword = "The keyword '{0}' is not supported on this platform.";

	// Token: 0x0400039F RID: 927
	public const string SQL_UnsupportedFeature = "The server is attempting to use a feature that is not supported on this platform.";

	// Token: 0x040003A0 RID: 928
	public const string SQL_UnsupportedToken = "Received an unsupported token '{0}' while reading data from the server.";

	// Token: 0x040003A1 RID: 929
	public const string SQL_DbTypeNotSupportedOnThisPlatform = "Type {0} is not supported on this platform.";

	// Token: 0x040003A2 RID: 930
	public const string SQL_NetworkLibraryNotSupported = "The keyword 'Network Library' is not supported on this platform, prefix the 'Data Source' with the protocol desired instead ('tcp:' for a TCP connection, or 'np:' for a Named Pipe connection).";

	// Token: 0x040003A3 RID: 931
	public const string SNI_PN0 = "HTTP Provider";

	// Token: 0x040003A4 RID: 932
	public const string SNI_PN1 = "Named Pipes Provider";

	// Token: 0x040003A5 RID: 933
	public const string SNI_PN2 = "Session Provider";

	// Token: 0x040003A6 RID: 934
	public const string SNI_PN3 = "Sign Provider";

	// Token: 0x040003A7 RID: 935
	public const string SNI_PN4 = "Shared Memory Provider";

	// Token: 0x040003A8 RID: 936
	public const string SNI_PN5 = "SMux Provider";

	// Token: 0x040003A9 RID: 937
	public const string SNI_PN6 = "SSL Provider";

	// Token: 0x040003AA RID: 938
	public const string SNI_PN7 = "TCP Provider";

	// Token: 0x040003AB RID: 939
	public const string SNI_PN8 = "";

	// Token: 0x040003AC RID: 940
	public const string SNI_PN9 = "SQL Network Interfaces";

	// Token: 0x040003AD RID: 941
	public const string AZURESQL_GenericEndpoint = ".database.windows.net";

	// Token: 0x040003AE RID: 942
	public const string AZURESQL_GermanEndpoint = ".database.cloudapi.de";

	// Token: 0x040003AF RID: 943
	public const string AZURESQL_UsGovEndpoint = ".database.usgovcloudapi.net";

	// Token: 0x040003B0 RID: 944
	public const string AZURESQL_ChinaEndpoint = ".database.chinacloudapi.cn";

	// Token: 0x040003B1 RID: 945
	public const string net_nego_channel_binding_not_supported = "No support for channel binding on operating systems other than Windows.";

	// Token: 0x040003B2 RID: 946
	public const string net_gssapi_operation_failed_detailed = "GSSAPI operation failed with error - {0} ({1}).";

	// Token: 0x040003B3 RID: 947
	public const string net_gssapi_operation_failed = "GSSAPI operation failed with status: {0} (Minor status: {1}).";

	// Token: 0x040003B4 RID: 948
	public const string net_ntlm_not_possible_default_cred = "NTLM authentication is not possible with default credentials on this platform.";

	// Token: 0x040003B5 RID: 949
	public const string net_nego_not_supported_empty_target_with_defaultcreds = "Target name should be non-empty if default credentials are passed.";

	// Token: 0x040003B6 RID: 950
	public const string net_nego_server_not_supported = "Server implementation is not supported.";

	// Token: 0x040003B7 RID: 951
	public const string net_nego_protection_level_not_supported = "Requested protection level is not supported with the GSSAPI implementation currently installed.";

	// Token: 0x040003B8 RID: 952
	public const string net_context_buffer_too_small = "Insufficient buffer space. Required: {0} Actual: {1}.";

	// Token: 0x040003B9 RID: 953
	public const string net_auth_message_not_encrypted = "Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.";

	// Token: 0x040003BA RID: 954
	public const string net_securitypackagesupport = "The requested security package is not supported.";

	// Token: 0x040003BB RID: 955
	public const string net_log_operation_failed_with_error = "{0} failed with error {1}.";

	// Token: 0x040003BC RID: 956
	public const string net_MethodNotImplementedException = "This method is not implemented by this class.";

	// Token: 0x040003BD RID: 957
	public const string event_OperationReturnedSomething = "{0} returned {1}.";

	// Token: 0x040003BE RID: 958
	public const string net_invalid_enum = "The specified value is not valid in the '{0}' enumeration.";

	// Token: 0x040003BF RID: 959
	public const string SSPIInvalidHandleType = "'{0}' is not a supported handle type.";

	// Token: 0x040003C0 RID: 960
	public const string LocalDBNotSupported = "LocalDB is not supported on this platform.";

	// Token: 0x040003C1 RID: 961
	public const string PlatformNotSupported_DataSqlClient = "System.Data.SqlClient is not supported on this platform.";

	// Token: 0x040003C2 RID: 962
	public const string SqlParameter_InvalidTableDerivedPrecisionForTvp = "Precision '{0}' required to send all values in column '{1}' exceeds the maximum supported precision '{2}'. The values must all fit in a single precision.";

	// Token: 0x040003C3 RID: 963
	public const string SqlProvider_InvalidDataColumnMaxLength = "The size of column '{0}' is not supported. The size is {1}.";

	// Token: 0x040003C4 RID: 964
	public const string MDF_InvalidXmlInvalidValue = "The metadata XML is invalid. The {1} column of the {0} collection must contain a non-empty string.";

	// Token: 0x040003C5 RID: 965
	public const string MDF_CollectionNameISNotUnique = "There are multiple collections named '{0}'.";

	// Token: 0x040003C6 RID: 966
	public const string MDF_InvalidXmlMissingColumn = "The metadata XML is invalid. The {0} collection must contain a {1} column and it must be a string column.";

	// Token: 0x040003C7 RID: 967
	public const string MDF_InvalidXml = "The metadata XML is invalid.";

	// Token: 0x040003C8 RID: 968
	public const string MDF_NoColumns = "The schema table contains no columns.";

	// Token: 0x040003C9 RID: 969
	public const string MDF_QueryFailed = "Unable to build the '{0}' collection because execution of the SQL query failed. See the inner exception for details.";

	// Token: 0x040003CA RID: 970
	public const string MDF_TooManyRestrictions = "More restrictions were provided than the requested schema ('{0}') supports.";

	// Token: 0x040003CB RID: 971
	public const string MDF_DataTableDoesNotExist = "The collection '{0}' is missing from the metadata XML.";

	// Token: 0x040003CC RID: 972
	public const string MDF_UndefinedCollection = "The requested collection ({0}) is not defined.";

	// Token: 0x040003CD RID: 973
	public const string MDF_UnsupportedVersion = " requested collection ({0}) is not supported by this version of the provider.";

	// Token: 0x040003CE RID: 974
	public const string MDF_MissingRestrictionColumn = "One or more of the required columns of the restrictions collection is missing.";

	// Token: 0x040003CF RID: 975
	public const string MDF_MissingRestrictionRow = "A restriction exists for which there is no matching row in the restrictions collection.";

	// Token: 0x040003D0 RID: 976
	public const string MDF_IncorrectNumberOfDataSourceInformationRows = "The DataSourceInformation table must contain exactly one row.";

	// Token: 0x040003D1 RID: 977
	public const string MDF_MissingDataSourceInformationColumn = "One of the required DataSourceInformation tables columns is missing.";

	// Token: 0x040003D2 RID: 978
	public const string MDF_AmbigousCollectionName = "The collection name '{0}' matches at least two collections with the same name but with different case, but does not match any of them exactly.";

	// Token: 0x040003D3 RID: 979
	public const string MDF_UnableToBuildCollection = "Unable to build schema collection '{0}';";

	// Token: 0x040003D4 RID: 980
	public const string ADP_InvalidArgumentLength = "The length of argument '{0}' exceeds its limit of '{1}'.";

	// Token: 0x040003D5 RID: 981
	public const string ADP_MustBeReadOnly = "{0} must be marked as read only.";

	// Token: 0x040003D6 RID: 982
	public const string ADP_InvalidMixedUsageOfSecureAndClearCredential = "Cannot use Credential with UserID, UID, Password, or PWD connection string keywords.";

	// Token: 0x040003D7 RID: 983
	public const string ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity = "Cannot use Credential with Integrated Security connection string keyword.";

	// Token: 0x040003D8 RID: 984
	public const string SQL_ChangePasswordArgumentMissing = "The '{0}' argument must not be null or empty.";

	// Token: 0x040003D9 RID: 985
	public const string SQL_ChangePasswordConflictsWithSSPI = "ChangePassword can only be used with SQL authentication, not with integrated security.";

	// Token: 0x040003DA RID: 986
	public const string SQL_ChangePasswordRequiresYukon = "ChangePassword requires SQL Server 9.0 or later.";

	// Token: 0x040003DB RID: 987
	public const string SQL_ChangePasswordUseOfUnallowedKey = "The keyword '{0}' must not be specified in the connectionString argument to ChangePassword.";

	// Token: 0x040003DC RID: 988
	public const string SQL_ParsingErrorWithState = "Internal connection fatal error. Error state: {0}.";

	// Token: 0x040003DD RID: 989
	public const string SQL_ParsingErrorValue = "Internal connection fatal error. Error state: {0}, Value: {1}.";

	// Token: 0x040003DE RID: 990
	public const string ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity = "Cannot set the AccessToken property if the 'Integrated Security' connection string keyword has been set to 'true' or 'SSPI'.";

	// Token: 0x040003DF RID: 991
	public const string ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword = "Cannot set the AccessToken property if 'UserID', 'UID', 'Password', or 'PWD' has been specified in connection string.";

	// Token: 0x040003E0 RID: 992
	public const string ADP_InvalidMixedUsageOfCredentialAndAccessToken = "Cannot set the Credential property if the AccessToken property is already set.";

	// Token: 0x040003E1 RID: 993
	public const string SQL_ParsingErrorFeatureId = "Internal connection fatal error. Error state: {0}, Feature Id: {1}.";

	// Token: 0x040003E2 RID: 994
	public const string SQL_ParsingErrorAuthLibraryType = "Internal connection fatal error. Error state: {0}, Authentication Library Type: {1}.";

	// Token: 0x040003E3 RID: 995
	public const string ADP_CollectionIsParent = "The {0} with is already contained by this {1}.";

	// Token: 0x040003E4 RID: 996
	public const string ADP_InvalidDataDirectory = "The DataDirectory substitute is not a string.";

	// Token: 0x040003E5 RID: 997
	public const string ADP_QuotePrefixNotSet = "{0} requires an open connection when the quote prefix has not been set.";

	// Token: 0x040003E6 RID: 998
	public const string MDF_InvalidRestrictionValue = "'{2}' is not a valid value for the '{1}' restriction of the '{0}' schema collection.";

	// Token: 0x040003E7 RID: 999
	public const string MDF_UndefinedPopulationMechanism = "The population mechanism '{0}' is not defined.";

	// Token: 0x040003E8 RID: 1000
	public const string ODBC_ODBCCommandText = "OdbcCommandBuilder.DeriveParameters failed because the OdbcCommand.CommandText property value is an invalid multipart name";

	// Token: 0x040003E9 RID: 1001
	public const string ODBC_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the .Net Framework Odbc Data Provider.";

	// Token: 0x040003EA RID: 1002
	public const string ADP_DatabaseNameTooLong = "The argument is too long.";

	// Token: 0x040003EB RID: 1003
	public const string ADP_DataReaderNoData = "No data exists for the row/column.";

	// Token: 0x040003EC RID: 1004
	public const string ADP_NumericToDecimalOverflow = "The numerical value is too large to fit into a 96 bit decimal.";

	// Token: 0x040003ED RID: 1005
	public const string ADP_DbRecordReadOnly = "'{0}' cannot be called when the record is read only.";

	// Token: 0x040003EE RID: 1006
	public const string ADP_OffsetOutOfRangeException = "Offset must refer to a location within the value.";

	// Token: 0x040003EF RID: 1007
	public const string ODBC_GetSchemaRestrictionRequired = "The ODBC managed provider requires that the TABLE_NAME restriction be specified and non-null for the GetSchema indexes collection.";

	// Token: 0x040003F0 RID: 1008
	public const string ADP_OdbcNoTypesFromProvider = "The ODBC provider did not return results from SQLGETTYPEINFO.";

	// Token: 0x040003F1 RID: 1009
	public const string OdbcConnection_ConnectionStringTooLong = "Connection string exceeds maximum allowed length of {0}.";

	// Token: 0x040003F2 RID: 1010
	public const string Odbc_UnknownSQLType = "Unknown SQL type - {0}.";

	// Token: 0x040003F3 RID: 1011
	public const string Odbc_NegativeArgument = "Invalid negative argument!";

	// Token: 0x040003F4 RID: 1012
	public const string Odbc_CantSetPropertyOnOpenConnection = "Can't set property on an open connection.";

	// Token: 0x040003F5 RID: 1013
	public const string Odbc_NoMappingForSqlTransactionLevel = "No valid mapping for a SQL_TRANSACTION '{0}' to a System.Data.IsolationLevel enumeration value.";

	// Token: 0x040003F6 RID: 1014
	public const string Odbc_CantEnableConnectionpooling = "{0} - unable to enable connection pooling...";

	// Token: 0x040003F7 RID: 1015
	public const string Odbc_CantAllocateEnvironmentHandle = "{0} - unable to allocate an environment handle.";

	// Token: 0x040003F8 RID: 1016
	public const string Odbc_FailedToGetDescriptorHandle = "{0} - unable to get descriptor handle.";

	// Token: 0x040003F9 RID: 1017
	public const string Odbc_NotInTransaction = "Not in a transaction";

	// Token: 0x040003FA RID: 1018
	public const string Odbc_ExceptionMessage = "{0} [{1}] {2}";

	// Token: 0x040003FB RID: 1019
	public const string Odbc_ConnectionClosed = "The connection is closed.";

	// Token: 0x040003FC RID: 1020
	public const string Odbc_OpenConnectionNoOwner = "An internal connection does not have an owner.";

	// Token: 0x040003FD RID: 1021
	public const string Odbc_PlatformNotSupported = "System.Data.ODBC is not supported on this platform.";

	// Token: 0x040003FE RID: 1022
	public const string Odbc_UnixOdbcNotFound = "Dependency unixODBC with minimum version 2.3.1 is required.";
}
