using System;
using System.Globalization;

// Token: 0x02000012 RID: 18
internal static class SR
{
	// Token: 0x0600002E RID: 46 RVA: 0x0000228F File Offset: 0x0000048F
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000229D File Offset: 0x0000049D
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000022A7 File Offset: 0x000004A7
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000022AA File Offset: 0x000004AA
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000022AD File Offset: 0x000004AD
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000022C0 File Offset: 0x000004C0
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000022CE File Offset: 0x000004CE
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000022DD File Offset: 0x000004DD
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000022E8 File Offset: 0x000004E8
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000022A7 File Offset: 0x000004A7
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x040000C4 RID: 196
	public const string ReducibleMustOverrideReduce = "reducible nodes must override Expression.Reduce()";

	// Token: 0x040000C5 RID: 197
	public const string MustReduceToDifferent = "node cannot reduce to itself or null";

	// Token: 0x040000C6 RID: 198
	public const string ReducedNotCompatible = "cannot assign from the reduced node type to the original node type";

	// Token: 0x040000C7 RID: 199
	public const string SetterHasNoParams = "Setter must have parameters.";

	// Token: 0x040000C8 RID: 200
	public const string PropertyCannotHaveRefType = "Property cannot have a managed pointer type.";

	// Token: 0x040000C9 RID: 201
	public const string IndexesOfSetGetMustMatch = "Indexing parameters of getter and setter must match.";

	// Token: 0x040000CA RID: 202
	public const string AccessorsCannotHaveVarArgs = "Accessor method should not have VarArgs.";

	// Token: 0x040000CB RID: 203
	public const string AccessorsCannotHaveByRefArgs = "Accessor indexes cannot be passed ByRef.";

	// Token: 0x040000CC RID: 204
	public const string BoundsCannotBeLessThanOne = "Bounds count cannot be less than 1";

	// Token: 0x040000CD RID: 205
	public const string TypeMustNotBeByRef = "Type must not be ByRef";

	// Token: 0x040000CE RID: 206
	public const string TypeMustNotBePointer = "Type must not be a pointer type";

	// Token: 0x040000CF RID: 207
	public const string SetterMustBeVoid = "Setter should have void type.";

	// Token: 0x040000D0 RID: 208
	public const string PropertyTypeMustMatchGetter = "Property type must match the value type of getter";

	// Token: 0x040000D1 RID: 209
	public const string PropertyTypeMustMatchSetter = "Property type must match the value type of setter";

	// Token: 0x040000D2 RID: 210
	public const string BothAccessorsMustBeStatic = "Both accessors must be static.";

	// Token: 0x040000D3 RID: 211
	public const string OnlyStaticFieldsHaveNullInstance = "Static field requires null instance, non-static field requires non-null instance.";

	// Token: 0x040000D4 RID: 212
	public const string OnlyStaticPropertiesHaveNullInstance = "Static property requires null instance, non-static property requires non-null instance.";

	// Token: 0x040000D5 RID: 213
	public const string OnlyStaticMethodsHaveNullInstance = "Static method requires null instance, non-static method requires non-null instance.";

	// Token: 0x040000D6 RID: 214
	public const string PropertyTypeCannotBeVoid = "Property cannot have a void type.";

	// Token: 0x040000D7 RID: 215
	public const string InvalidUnboxType = "Can only unbox from an object or interface type to a value type.";

	// Token: 0x040000D8 RID: 216
	public const string ExpressionMustBeWriteable = "Expression must be writeable";

	// Token: 0x040000D9 RID: 217
	public const string ArgumentMustNotHaveValueType = "Argument must not have a value type.";

	// Token: 0x040000DA RID: 218
	public const string MustBeReducible = "must be reducible node";

	// Token: 0x040000DB RID: 219
	public const string AllTestValuesMustHaveSameType = "All test values must have the same type.";

	// Token: 0x040000DC RID: 220
	public const string AllCaseBodiesMustHaveSameType = "All case bodies and the default body must have the same type.";

	// Token: 0x040000DD RID: 221
	public const string DefaultBodyMustBeSupplied = "Default body must be supplied if case bodies are not System.Void.";

	// Token: 0x040000DE RID: 222
	public const string LabelMustBeVoidOrHaveExpression = "Label type must be System.Void if an expression is not supplied";

	// Token: 0x040000DF RID: 223
	public const string LabelTypeMustBeVoid = "Type must be System.Void for this label argument";

	// Token: 0x040000E0 RID: 224
	public const string QuotedExpressionMustBeLambda = "Quoted expression must be a lambda";

	// Token: 0x040000E1 RID: 225
	public const string VariableMustNotBeByRef = "Variable '{0}' uses unsupported type '{1}'. Reference types are not supported for variables.";

	// Token: 0x040000E2 RID: 226
	public const string DuplicateVariable = "Found duplicate parameter '{0}'. Each ParameterExpression in the list must be a unique object.";

	// Token: 0x040000E3 RID: 227
	public const string StartEndMustBeOrdered = "Start and End must be well ordered";

	// Token: 0x040000E4 RID: 228
	public const string FaultCannotHaveCatchOrFinally = "fault cannot be used with catch or finally clauses";

	// Token: 0x040000E5 RID: 229
	public const string TryMustHaveCatchFinallyOrFault = "try must have at least one catch, finally, or fault clause";

	// Token: 0x040000E6 RID: 230
	public const string BodyOfCatchMustHaveSameTypeAsBodyOfTry = "Body of catch must have the same type as body of try.";

	// Token: 0x040000E7 RID: 231
	public const string ExtensionNodeMustOverrideProperty = "Extension node must override the property {0}.";

	// Token: 0x040000E8 RID: 232
	public const string UserDefinedOperatorMustBeStatic = "User-defined operator method '{0}' must be static.";

	// Token: 0x040000E9 RID: 233
	public const string UserDefinedOperatorMustNotBeVoid = "User-defined operator method '{0}' must not be void.";

	// Token: 0x040000EA RID: 234
	public const string CoercionOperatorNotDefined = "No coercion operator is defined between types '{0}' and '{1}'.";

	// Token: 0x040000EB RID: 235
	public const string UnaryOperatorNotDefined = "The unary operator {0} is not defined for the type '{1}'.";

	// Token: 0x040000EC RID: 236
	public const string BinaryOperatorNotDefined = "The binary operator {0} is not defined for the types '{1}' and '{2}'.";

	// Token: 0x040000ED RID: 237
	public const string ReferenceEqualityNotDefined = "Reference equality is not defined for the types '{0}' and '{1}'.";

	// Token: 0x040000EE RID: 238
	public const string OperandTypesDoNotMatchParameters = "The operands for operator '{0}' do not match the parameters of method '{1}'.";

	// Token: 0x040000EF RID: 239
	public const string OverloadOperatorTypeDoesNotMatchConversionType = "The return type of overload method for operator '{0}' does not match the parameter type of conversion method '{1}'.";

	// Token: 0x040000F0 RID: 240
	public const string ConversionIsNotSupportedForArithmeticTypes = "Conversion is not supported for arithmetic types without operator overloading.";

	// Token: 0x040000F1 RID: 241
	public const string ArgumentMustBeArray = "Argument must be array";

	// Token: 0x040000F2 RID: 242
	public const string ArgumentMustBeBoolean = "Argument must be boolean";

	// Token: 0x040000F3 RID: 243
	public const string EqualityMustReturnBoolean = "The user-defined equality method '{0}' must return a boolean value.";

	// Token: 0x040000F4 RID: 244
	public const string ArgumentMustBeFieldInfoOrPropertyInfo = "Argument must be either a FieldInfo or PropertyInfo";

	// Token: 0x040000F5 RID: 245
	public const string ArgumentMustBeFieldInfoOrPropertyInfoOrMethod = "Argument must be either a FieldInfo, PropertyInfo or MethodInfo";

	// Token: 0x040000F6 RID: 246
	public const string ArgumentMustBeInstanceMember = "Argument must be an instance member";

	// Token: 0x040000F7 RID: 247
	public const string ArgumentMustBeInteger = "Argument must be of an integer type";

	// Token: 0x040000F8 RID: 248
	public const string ArgumentMustBeArrayIndexType = "Argument for array index must be of type Int32";

	// Token: 0x040000F9 RID: 249
	public const string ArgumentMustBeSingleDimensionalArrayType = "Argument must be single-dimensional, zero-based array type";

	// Token: 0x040000FA RID: 250
	public const string ArgumentTypesMustMatch = "Argument types do not match";

	// Token: 0x040000FB RID: 251
	public const string CannotAutoInitializeValueTypeElementThroughProperty = "Cannot auto initialize elements of value type through property '{0}', use assignment instead";

	// Token: 0x040000FC RID: 252
	public const string CannotAutoInitializeValueTypeMemberThroughProperty = "Cannot auto initialize members of value type through property '{0}', use assignment instead";

	// Token: 0x040000FD RID: 253
	public const string IncorrectTypeForTypeAs = "The type used in TypeAs Expression must be of reference or nullable type, {0} is neither";

	// Token: 0x040000FE RID: 254
	public const string CoalesceUsedOnNonNullType = "Coalesce used with type that cannot be null";

	// Token: 0x040000FF RID: 255
	public const string ExpressionTypeCannotInitializeArrayType = "An expression of type '{0}' cannot be used to initialize an array of type '{1}'";

	// Token: 0x04000100 RID: 256
	public const string ArgumentTypeDoesNotMatchMember = " Argument type '{0}' does not match the corresponding member type '{1}'";

	// Token: 0x04000101 RID: 257
	public const string ArgumentMemberNotDeclOnType = " The member '{0}' is not declared on type '{1}' being created";

	// Token: 0x04000102 RID: 258
	public const string ExpressionTypeDoesNotMatchReturn = "Expression of type '{0}' cannot be used for return type '{1}'";

	// Token: 0x04000103 RID: 259
	public const string ExpressionTypeDoesNotMatchAssignment = "Expression of type '{0}' cannot be used for assignment to type '{1}'";

	// Token: 0x04000104 RID: 260
	public const string ExpressionTypeDoesNotMatchLabel = "Expression of type '{0}' cannot be used for label of type '{1}'";

	// Token: 0x04000105 RID: 261
	public const string ExpressionTypeNotInvocable = "Expression of type '{0}' cannot be invoked";

	// Token: 0x04000106 RID: 262
	public const string FieldNotDefinedForType = "Field '{0}' is not defined for type '{1}'";

	// Token: 0x04000107 RID: 263
	public const string InstanceFieldNotDefinedForType = "Instance field '{0}' is not defined for type '{1}'";

	// Token: 0x04000108 RID: 264
	public const string FieldInfoNotDefinedForType = "Field '{0}.{1}' is not defined for type '{2}'";

	// Token: 0x04000109 RID: 265
	public const string IncorrectNumberOfIndexes = "Incorrect number of indexes";

	// Token: 0x0400010A RID: 266
	public const string IncorrectNumberOfLambdaDeclarationParameters = "Incorrect number of parameters supplied for lambda declaration";

	// Token: 0x0400010B RID: 267
	public const string IncorrectNumberOfMembersForGivenConstructor = " Incorrect number of members for constructor";

	// Token: 0x0400010C RID: 268
	public const string IncorrectNumberOfArgumentsForMembers = "Incorrect number of arguments for the given members ";

	// Token: 0x0400010D RID: 269
	public const string LambdaTypeMustBeDerivedFromSystemDelegate = "Lambda type parameter must be derived from System.MulticastDelegate";

	// Token: 0x0400010E RID: 270
	public const string MemberNotFieldOrProperty = "Member '{0}' not field or property";

	// Token: 0x0400010F RID: 271
	public const string MethodContainsGenericParameters = "Method {0} contains generic parameters";

	// Token: 0x04000110 RID: 272
	public const string MethodIsGeneric = "Method {0} is a generic method definition";

	// Token: 0x04000111 RID: 273
	public const string MethodNotPropertyAccessor = "The method '{0}.{1}' is not a property accessor";

	// Token: 0x04000112 RID: 274
	public const string PropertyDoesNotHaveGetter = "The property '{0}' has no 'get' accessor";

	// Token: 0x04000113 RID: 275
	public const string PropertyDoesNotHaveSetter = "The property '{0}' has no 'set' accessor";

	// Token: 0x04000114 RID: 276
	public const string PropertyDoesNotHaveAccessor = "The property '{0}' has no 'get' or 'set' accessors";

	// Token: 0x04000115 RID: 277
	public const string NotAMemberOfType = "'{0}' is not a member of type '{1}'";

	// Token: 0x04000116 RID: 278
	public const string NotAMemberOfAnyType = "'{0}' is not a member of any type";

	// Token: 0x04000117 RID: 279
	public const string UnsupportedExpressionType = "The expression type '{0}' is not supported";

	// Token: 0x04000118 RID: 280
	public const string ParameterExpressionNotValidAsDelegate = "ParameterExpression of type '{0}' cannot be used for delegate parameter of type '{1}'";

	// Token: 0x04000119 RID: 281
	public const string PropertyNotDefinedForType = "Property '{0}' is not defined for type '{1}'";

	// Token: 0x0400011A RID: 282
	public const string InstancePropertyNotDefinedForType = "Instance property '{0}' is not defined for type '{1}'";

	// Token: 0x0400011B RID: 283
	public const string InstancePropertyWithoutParameterNotDefinedForType = "Instance property '{0}' that takes no argument is not defined for type '{1}'";

	// Token: 0x0400011C RID: 284
	public const string InstancePropertyWithSpecifiedParametersNotDefinedForType = "Instance property '{0}{1}' is not defined for type '{2}'";

	// Token: 0x0400011D RID: 285
	public const string InstanceAndMethodTypeMismatch = "Method '{0}' declared on type '{1}' cannot be called with instance of type '{2}'";

	// Token: 0x0400011E RID: 286
	public const string TypeContainsGenericParameters = "Type {0} contains generic parameters";

	// Token: 0x0400011F RID: 287
	public const string TypeIsGeneric = "Type {0} is a generic type definition";

	// Token: 0x04000120 RID: 288
	public const string TypeMissingDefaultConstructor = "Type '{0}' does not have a default constructor";

	// Token: 0x04000121 RID: 289
	public const string ElementInitializerMethodNotAdd = "Element initializer method must be named 'Add'";

	// Token: 0x04000122 RID: 290
	public const string ElementInitializerMethodNoRefOutParam = "Parameter '{0}' of element initializer method '{1}' must not be a pass by reference parameter";

	// Token: 0x04000123 RID: 291
	public const string ElementInitializerMethodWithZeroArgs = "Element initializer method must have at least 1 parameter";

	// Token: 0x04000124 RID: 292
	public const string ElementInitializerMethodStatic = "Element initializer method must be an instance method";

	// Token: 0x04000125 RID: 293
	public const string TypeNotIEnumerable = "Type '{0}' is not IEnumerable";

	// Token: 0x04000126 RID: 294
	public const string UnhandledBinary = "Unhandled binary: {0}";

	// Token: 0x04000127 RID: 295
	public const string UnhandledBinding = "Unhandled binding ";

	// Token: 0x04000128 RID: 296
	public const string UnhandledBindingType = "Unhandled Binding Type: {0}";

	// Token: 0x04000129 RID: 297
	public const string UnhandledUnary = "Unhandled unary: {0}";

	// Token: 0x0400012A RID: 298
	public const string UnknownBindingType = "Unknown binding type";

	// Token: 0x0400012B RID: 299
	public const string UserDefinedOpMustHaveConsistentTypes = "The user-defined operator method '{1}' for operator '{0}' must have identical parameter and return types.";

	// Token: 0x0400012C RID: 300
	public const string UserDefinedOpMustHaveValidReturnType = "The user-defined operator method '{1}' for operator '{0}' must return the same type as its parameter or a derived type.";

	// Token: 0x0400012D RID: 301
	public const string LogicalOperatorMustHaveBooleanOperators = "The user-defined operator method '{1}' for operator '{0}' must have associated boolean True and False operators.";

	// Token: 0x0400012E RID: 302
	public const string MethodWithArgsDoesNotExistOnType = "No method '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x0400012F RID: 303
	public const string GenericMethodWithArgsDoesNotExistOnType = "No generic method '{0}' on type '{1}' is compatible with the supplied type arguments and arguments. No type arguments should be provided if the method is non-generic. ";

	// Token: 0x04000130 RID: 304
	public const string MethodWithMoreThanOneMatch = "More than one method '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x04000131 RID: 305
	public const string PropertyWithMoreThanOneMatch = "More than one property '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x04000132 RID: 306
	public const string IncorrectNumberOfTypeArgsForFunc = "An incorrect number of type arguments were specified for the declaration of a Func type.";

	// Token: 0x04000133 RID: 307
	public const string IncorrectNumberOfTypeArgsForAction = "An incorrect number of type arguments were specified for the declaration of an Action type.";

	// Token: 0x04000134 RID: 308
	public const string ArgumentCannotBeOfTypeVoid = "Argument type cannot be System.Void.";

	// Token: 0x04000135 RID: 309
	public const string OutOfRange = "{0} must be greater than or equal to {1}";

	// Token: 0x04000136 RID: 310
	public const string LabelTargetAlreadyDefined = "Cannot redefine label '{0}' in an inner block.";

	// Token: 0x04000137 RID: 311
	public const string LabelTargetUndefined = "Cannot jump to undefined label '{0}'.";

	// Token: 0x04000138 RID: 312
	public const string ControlCannotLeaveFinally = "Control cannot leave a finally block.";

	// Token: 0x04000139 RID: 313
	public const string ControlCannotLeaveFilterTest = "Control cannot leave a filter test.";

	// Token: 0x0400013A RID: 314
	public const string AmbiguousJump = "Cannot jump to ambiguous label '{0}'.";

	// Token: 0x0400013B RID: 315
	public const string ControlCannotEnterTry = "Control cannot enter a try block.";

	// Token: 0x0400013C RID: 316
	public const string ControlCannotEnterExpression = "Control cannot enter an expression--only statements can be jumped into.";

	// Token: 0x0400013D RID: 317
	public const string NonLocalJumpWithValue = "Cannot jump to non-local label '{0}' with a value. Only jumps to labels defined in outer blocks can pass values.";

	// Token: 0x0400013E RID: 318
	public const string CannotCompileConstant = "CompileToMethod cannot compile constant '{0}' because it is a non-trivial value, such as a live object. Instead, create an expression tree that can construct this value.";

	// Token: 0x0400013F RID: 319
	public const string CannotCompileDynamic = "Dynamic expressions are not supported by CompileToMethod. Instead, create an expression tree that uses System.Runtime.CompilerServices.CallSite.";

	// Token: 0x04000140 RID: 320
	public const string InvalidLvalue = "Invalid lvalue for assignment: {0}.";

	// Token: 0x04000141 RID: 321
	public const string UndefinedVariable = "variable '{0}' of type '{1}' referenced from scope '{2}', but it is not defined";

	// Token: 0x04000142 RID: 322
	public const string CannotCloseOverByRef = "Cannot close over byref parameter '{0}' referenced in lambda '{1}'";

	// Token: 0x04000143 RID: 323
	public const string UnexpectedVarArgsCall = "Unexpected VarArgs call to method '{0}'";

	// Token: 0x04000144 RID: 324
	public const string RethrowRequiresCatch = "Rethrow statement is valid only inside a Catch block.";

	// Token: 0x04000145 RID: 325
	public const string TryNotAllowedInFilter = "Try expression is not allowed inside a filter body.";

	// Token: 0x04000146 RID: 326
	public const string MustRewriteToSameNode = "When called from '{0}', rewriting a node of type '{1}' must return a non-null value of the same type. Alternatively, override '{2}' and change it to not visit children of this type.";

	// Token: 0x04000147 RID: 327
	public const string MustRewriteChildToSameType = "Rewriting child expression from type '{0}' to type '{1}' is not allowed, because it would change the meaning of the operation. If this is intentional, override '{2}' and change it to allow this rewrite.";

	// Token: 0x04000148 RID: 328
	public const string MustRewriteWithoutMethod = "Rewritten expression calls operator method '{0}', but the original node had no operator method. If this is intentional, override '{1}' and change it to allow this rewrite.";

	// Token: 0x04000149 RID: 329
	public const string InvalidNullValue = "The value null is not of type '{0}' and cannot be used in this collection.";

	// Token: 0x0400014A RID: 330
	public const string InvalidObjectType = "The value '{0}' is not of type '{1}' and cannot be used in this collection.";

	// Token: 0x0400014B RID: 331
	public const string TryNotSupportedForMethodsWithRefArgs = "TryExpression is not supported as an argument to method '{0}' because it has an argument with by-ref type. Construct the tree so the TryExpression is not nested inside of this expression.";

	// Token: 0x0400014C RID: 332
	public const string TryNotSupportedForValueTypeInstances = "TryExpression is not supported as a child expression when accessing a member on type '{0}' because it is a value type. Construct the tree so the TryExpression is not nested inside of this expression.";

	// Token: 0x0400014D RID: 333
	public const string EnumerationIsDone = "Enumeration has either not started or has already finished.";

	// Token: 0x0400014E RID: 334
	public const string TestValueTypeDoesNotMatchComparisonMethodParameter = "Test value of type '{0}' cannot be used for the comparison method parameter of type '{1}'";

	// Token: 0x0400014F RID: 335
	public const string SwitchValueTypeDoesNotMatchComparisonMethodParameter = "Switch value of type '{0}' cannot be used for the comparison method parameter of type '{1}'";

	// Token: 0x04000150 RID: 336
	public const string PdbGeneratorNeedsExpressionCompiler = "DebugInfoGenerator created by CreatePdbGenerator can only be used with LambdaExpression.CompileToMethod.";

	// Token: 0x04000151 RID: 337
	public const string InvalidArgumentValue = "Invalid argument value";

	// Token: 0x04000152 RID: 338
	public const string NonEmptyCollectionRequired = "Non-empty collection required";

	// Token: 0x04000153 RID: 339
	public const string CollectionModifiedWhileEnumerating = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000154 RID: 340
	public const string ExpressionMustBeReadable = "Expression must be readable";

	// Token: 0x04000155 RID: 341
	public const string ExpressionTypeDoesNotMatchMethodParameter = "Expression of type '{0}' cannot be used for parameter of type '{1}' of method '{2}'";

	// Token: 0x04000156 RID: 342
	public const string ExpressionTypeDoesNotMatchParameter = "Expression of type '{0}' cannot be used for parameter of type '{1}'";

	// Token: 0x04000157 RID: 343
	public const string ExpressionTypeDoesNotMatchConstructorParameter = "Expression of type '{0}' cannot be used for constructor parameter of type '{1}'";

	// Token: 0x04000158 RID: 344
	public const string IncorrectNumberOfMethodCallArguments = "Incorrect number of arguments supplied for call to method '{0}'";

	// Token: 0x04000159 RID: 345
	public const string IncorrectNumberOfLambdaArguments = "Incorrect number of arguments supplied for lambda invocation";

	// Token: 0x0400015A RID: 346
	public const string IncorrectNumberOfConstructorArguments = "Incorrect number of arguments for constructor";

	// Token: 0x0400015B RID: 347
	public const string NonStaticConstructorRequired = "The constructor should not be static";

	// Token: 0x0400015C RID: 348
	public const string NonAbstractConstructorRequired = "Can't compile a NewExpression with a constructor declared on an abstract class";

	// Token: 0x0400015D RID: 349
	public const string FirstArgumentMustBeCallSite = "First argument of delegate must be CallSite";

	// Token: 0x0400015E RID: 350
	public const string NoOrInvalidRuleProduced = "No or Invalid rule produced";

	// Token: 0x0400015F RID: 351
	public const string TypeMustBeDerivedFromSystemDelegate = "Type must be derived from System.Delegate";

	// Token: 0x04000160 RID: 352
	public const string TypeParameterIsNotDelegate = "Type parameter is {0}. Expected a delegate.";

	// Token: 0x04000161 RID: 353
	public const string ArgumentTypeCannotBeVoid = "Argument type cannot be void";

	// Token: 0x04000162 RID: 354
	public const string ArgCntMustBeGreaterThanNameCnt = "Argument count must be greater than number of named arguments.";

	// Token: 0x04000163 RID: 355
	public const string BinderNotCompatibleWithCallSite = "The result type '{0}' of the binder '{1}' is not compatible with the result type '{2}' expected by the call site.";

	// Token: 0x04000164 RID: 356
	public const string BindingCannotBeNull = "Bind cannot return null.";

	// Token: 0x04000165 RID: 357
	public const string DynamicBinderResultNotAssignable = "The result type '{0}' of the dynamic binding produced by binder '{1}' is not compatible with the result type '{2}' expected by the call site.";

	// Token: 0x04000166 RID: 358
	public const string DynamicBindingNeedsRestrictions = "The result of the dynamic binding produced by the object with type '{0}' for the binder '{1}' needs at least one restriction.";

	// Token: 0x04000167 RID: 359
	public const string DynamicObjectResultNotAssignable = "The result type '{0}' of the dynamic binding produced by the object with type '{1}' for the binder '{2}' is not compatible with the result type '{3}' expected by the call site.";

	// Token: 0x04000168 RID: 360
	public const string InvalidMetaObjectCreated = "An IDynamicMetaObjectProvider {0} created an invalid DynamicMetaObject instance.";

	// Token: 0x04000169 RID: 361
	public const string AmbiguousMatchInExpandoObject = "More than one key matching '{0}' was found in the ExpandoObject.";

	// Token: 0x0400016A RID: 362
	public const string CollectionReadOnly = "Collection is read-only.";

	// Token: 0x0400016B RID: 363
	public const string KeyDoesNotExistInExpando = "The specified key '{0}' does not exist in the ExpandoObject.";

	// Token: 0x0400016C RID: 364
	public const string SameKeyExistsInExpando = "An element with the same key '{0}' already exists in the ExpandoObject.";

	// Token: 0x0400016D RID: 365
	public const string Arg_KeyNotFoundWithKey = "The given key '{0}' was not present in the dictionary.";

	// Token: 0x0400016E RID: 366
	public const string EmptyEnumerable = "Enumeration yielded no results";

	// Token: 0x0400016F RID: 367
	public const string MoreThanOneElement = "Sequence contains more than one element";

	// Token: 0x04000170 RID: 368
	public const string MoreThanOneMatch = "Sequence contains more than one matching element";

	// Token: 0x04000171 RID: 369
	public const string NoElements = "Sequence contains no elements";

	// Token: 0x04000172 RID: 370
	public const string NoMatch = "Sequence contains no matching element";

	// Token: 0x04000173 RID: 371
	public const string ParallelPartitionable_NullReturn = "The return value must not be null.";

	// Token: 0x04000174 RID: 372
	public const string ParallelPartitionable_IncorretElementCount = "The returned array's length must equal the number of partitions requested.";

	// Token: 0x04000175 RID: 373
	public const string ParallelPartitionable_NullElement = "Elements returned must not be null.";

	// Token: 0x04000176 RID: 374
	public const string PLINQ_CommonEnumerator_Current_NotStarted = "Enumeration has not started. MoveNext must be called to initiate enumeration.";

	// Token: 0x04000177 RID: 375
	public const string PLINQ_ExternalCancellationRequested = "The query has been canceled via the token supplied to WithCancellation.";

	// Token: 0x04000178 RID: 376
	public const string PLINQ_DisposeRequested = "The query enumerator has been disposed.";

	// Token: 0x04000179 RID: 377
	public const string ParallelQuery_DuplicateTaskScheduler = "The WithTaskScheduler operator may be used at most once in a query.";

	// Token: 0x0400017A RID: 378
	public const string ParallelQuery_DuplicateDOP = "The WithDegreeOfParallelism operator may be used at most once in a query.";

	// Token: 0x0400017B RID: 379
	public const string ParallelQuery_DuplicateExecutionMode = "The WithExecutionMode operator may be used at most once in a query.";

	// Token: 0x0400017C RID: 380
	public const string PartitionerQueryOperator_NullPartitionList = "Partitioner returned null instead of a list of partitions.";

	// Token: 0x0400017D RID: 381
	public const string PartitionerQueryOperator_WrongNumberOfPartitions = "Partitioner returned a wrong number of partitions.";

	// Token: 0x0400017E RID: 382
	public const string PartitionerQueryOperator_NullPartition = "Partitioner returned a null partition.";

	// Token: 0x0400017F RID: 383
	public const string ParallelQuery_DuplicateWithCancellation = "The WithCancellation operator may by used at most once in a query.";

	// Token: 0x04000180 RID: 384
	public const string ParallelQuery_DuplicateMergeOptions = "The WithMergeOptions operator may be used at most once in a query.";

	// Token: 0x04000181 RID: 385
	public const string PLINQ_EnumerationPreviouslyFailed = "The query enumerator previously threw an exception.";

	// Token: 0x04000182 RID: 386
	public const string ParallelQuery_PartitionerNotOrderable = "AsOrdered may not be used with a partitioner that is not orderable.";

	// Token: 0x04000183 RID: 387
	public const string ParallelQuery_InvalidAsOrderedCall = "AsOrdered may only be called on the result of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.";

	// Token: 0x04000184 RID: 388
	public const string ParallelQuery_InvalidNonGenericAsOrderedCall = "Non-generic AsOrdered may only be called on the result of the non-generic AsParallel.";

	// Token: 0x04000185 RID: 389
	public const string ParallelEnumerable_BinaryOpMustUseAsParallel = "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.";

	// Token: 0x04000186 RID: 390
	public const string ParallelEnumerable_WithQueryExecutionMode_InvalidMode = "The executionMode argument contains an invalid value.";

	// Token: 0x04000187 RID: 391
	public const string ParallelEnumerable_WithMergeOptions_InvalidOptions = "The mergeOptions argument contains an invalid value.";

	// Token: 0x04000188 RID: 392
	public const string ArgumentNotIEnumerableGeneric = "{0} is not IEnumerable<>";

	// Token: 0x04000189 RID: 393
	public const string ArgumentNotValid = "Argument {0} is not valid";

	// Token: 0x0400018A RID: 394
	public const string NoMethodOnType = "There is no method '{0}' on type '{1}'";

	// Token: 0x0400018B RID: 395
	public const string NoMethodOnTypeMatchingArguments = "There is no method '{0}' on type '{1}' that matches the specified arguments";

	// Token: 0x0400018C RID: 396
	public const string EnumeratingNullEnumerableExpression = "Cannot enumerate a query created from a null IEnumerable<>";

	// Token: 0x0400018D RID: 397
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non negative number is required.";

	// Token: 0x0400018E RID: 398
	public const string ArgumentOutOfRange_NeedValidPipeAccessRights = "Invalid PipeAccessRights value.";

	// Token: 0x0400018F RID: 399
	public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x04000190 RID: 400
	public const string Argument_NeedNonemptyPipeName = "pipeName cannot be an empty string.";

	// Token: 0x04000191 RID: 401
	public const string Argument_NonContainerInvalidAnyFlag = "This flag may not be set on a pipe.";

	// Token: 0x04000192 RID: 402
	public const string Argument_EmptyServerName = "serverName cannot be an empty string.  Use \\\\\\\".\\\\\\\" for current machine.";

	// Token: 0x04000193 RID: 403
	public const string Argument_InvalidHandle = "Invalid handle.";

	// Token: 0x04000194 RID: 404
	public const string ArgumentNull_Buffer = "Buffer cannot be null.";

	// Token: 0x04000195 RID: 405
	public const string ArgumentNull_ServerName = "serverName cannot be null. Use \\\".\\\" for current machine.";

	// Token: 0x04000196 RID: 406
	public const string ArgumentOutOfRange_AnonymousReserved = "The pipeName \\\"anonymous\\\" is reserved.";

	// Token: 0x04000197 RID: 407
	public const string ArgumentOutOfRange_TransmissionModeByteOrMsg = "For named pipes, transmission mode can be TransmissionMode.Byte or PipeTransmissionMode.Message. For anonymous pipes, transmission mode can be TransmissionMode.Byte.";

	// Token: 0x04000198 RID: 408
	public const string ArgumentOutOfRange_DirectionModeInOutOrInOut = "For named pipes, the pipe direction can be PipeDirection.In, PipeDirection.Out or PipeDirection.InOut. For anonymous pipes, the pipe direction can be PipeDirection.In or PipeDirection.Out.";

	// Token: 0x04000199 RID: 409
	public const string ArgumentOutOfRange_ImpersonationInvalid = "TokenImpersonationLevel.None, TokenImpersonationLevel.Anonymous, TokenImpersonationLevel.Identification, TokenImpersonationLevel.Impersonation or TokenImpersonationLevel.Delegation required.";

	// Token: 0x0400019A RID: 410
	public const string ArgumentOutOfRange_OptionsInvalid = "options contains an invalid flag.";

	// Token: 0x0400019B RID: 411
	public const string ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable = "HandleInheritability.None or HandleInheritability.Inheritable required.";

	// Token: 0x0400019C RID: 412
	public const string ArgumentOutOfRange_InvalidTimeout = "Timeout must be non-negative or equal to -1 (Timeout.Infinite)";

	// Token: 0x0400019D RID: 413
	public const string ArgumentOutOfRange_MaxNumServerInstances = "maxNumberOfServerInstances must either be a value between 1 and 254, or NamedPipeServerStream.MaxAllowedServerInstances (to obtain the maximum number allowed by system resources).";

	// Token: 0x0400019E RID: 414
	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	// Token: 0x0400019F RID: 415
	public const string InvalidOperation_PipeNotYetConnected = "Pipe hasn't been connected yet.";

	// Token: 0x040001A0 RID: 416
	public const string InvalidOperation_PipeDisconnected = "Pipe is in a disconnected state.";

	// Token: 0x040001A1 RID: 417
	public const string InvalidOperation_PipeHandleNotSet = "Pipe handle has not been set.  Did your PipeStream implementation call InitializeHandle?";

	// Token: 0x040001A2 RID: 418
	public const string InvalidOperation_PipeNotAsync = "Pipe is not opened in asynchronous mode.";

	// Token: 0x040001A3 RID: 419
	public const string InvalidOperation_PipeReadModeNotMessage = "ReadMode is not of PipeTransmissionMode.Message.";

	// Token: 0x040001A4 RID: 420
	public const string InvalidOperation_PipeAlreadyConnected = "Already in a connected state.";

	// Token: 0x040001A5 RID: 421
	public const string InvalidOperation_PipeAlreadyDisconnected = "Already in a disconnected state.";

	// Token: 0x040001A6 RID: 422
	public const string IO_EOF_ReadBeyondEOF = "Unable to read beyond the end of the stream.";

	// Token: 0x040001A7 RID: 423
	public const string IO_FileNotFound = "Unable to find the specified file.";

	// Token: 0x040001A8 RID: 424
	public const string IO_FileNotFound_FileName = "Could not find file '{0}'.";

	// Token: 0x040001A9 RID: 425
	public const string IO_AlreadyExists_Name = "Cannot create \\\"{0}\\\" because a file or directory with the same name already exists.";

	// Token: 0x040001AA RID: 426
	public const string IO_FileExists_Name = "The file '{0}' already exists.";

	// Token: 0x040001AB RID: 427
	public const string IO_IO_PipeBroken = "Pipe is broken.";

	// Token: 0x040001AC RID: 428
	public const string IO_OperationAborted = "IO operation was aborted unexpectedly.";

	// Token: 0x040001AD RID: 429
	public const string IO_SharingViolation_File = "The process cannot access the file '{0}' because it is being used by another process.";

	// Token: 0x040001AE RID: 430
	public const string IO_SharingViolation_NoFileName = "The process cannot access the file because it is being used by another process.";

	// Token: 0x040001AF RID: 431
	public const string IO_PipeBroken = "Pipe is broken.";

	// Token: 0x040001B0 RID: 432
	public const string IO_InvalidPipeHandle = "Invalid pipe handle.";

	// Token: 0x040001B1 RID: 433
	public const string IO_PathNotFound_Path = "Could not find a part of the path '{0}'.";

	// Token: 0x040001B2 RID: 434
	public const string IO_PathNotFound_NoPathName = "Could not find a part of the path.";

	// Token: 0x040001B3 RID: 435
	public const string IO_PathTooLong = "The specified file name or path is too long, or a component of the specified path is too long.";

	// Token: 0x040001B4 RID: 436
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x040001B5 RID: 437
	public const string NotSupported_UnseekableStream = "Stream does not support seeking.";

	// Token: 0x040001B6 RID: 438
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x040001B7 RID: 439
	public const string NotSupported_AnonymousPipeUnidirectional = "Anonymous pipes can only be in one direction.";

	// Token: 0x040001B8 RID: 440
	public const string NotSupported_AnonymousPipeMessagesNotSupported = "Anonymous pipes do not support PipeTransmissionMode.Message ReadMode.";

	// Token: 0x040001B9 RID: 441
	public const string ObjectDisposed_PipeClosed = "Cannot access a closed pipe.";

	// Token: 0x040001BA RID: 442
	public const string UnauthorizedAccess_IODenied_Path = "Access to the path '{0}' is denied.";

	// Token: 0x040001BB RID: 443
	public const string UnauthorizedAccess_IODenied_NoPathName = "Access to the path is denied.";

	// Token: 0x040001BC RID: 444
	public const string ArgumentOutOfRange_FileLengthTooBig = "Specified file length was too large for the file system.";

	// Token: 0x040001BD RID: 445
	public const string PlatformNotSupported_MessageTransmissionMode = "Message transmission mode is not supported on this platform.";

	// Token: 0x040001BE RID: 446
	public const string PlatformNotSupported_RemotePipes = "Access to remote named pipes is not supported on this platform.";

	// Token: 0x040001BF RID: 447
	public const string PlatformNotSupported_InvalidPipeNameChars = "The name of a pipe on this platform must be a valid file name or a valid absolute path to a file name.";

	// Token: 0x040001C0 RID: 448
	public const string ObjectDisposed_StreamClosed = "Cannot access a closed Stream.";

	// Token: 0x040001C1 RID: 449
	public const string PlatformNotSupported_OperatingSystemError = "The operating system returned error '{0}' indicating that the operation is not supported.";

	// Token: 0x040001C2 RID: 450
	public const string IO_AllPipeInstancesAreBusy = "All pipe instances are busy.";

	// Token: 0x040001C3 RID: 451
	public const string IO_PathTooLong_Path = "The path '{0}' is too long, or a component of the specified path is too long.";

	// Token: 0x040001C4 RID: 452
	public const string UnauthorizedAccess_NotOwnedByCurrentUser = "Could not connect to the pipe because it was not owned by the current user.";

	// Token: 0x040001C5 RID: 453
	public const string UnauthorizedAccess_ClientIsNotCurrentUser = "Client connection (user id {0}) was refused because it was not owned by the current user (id {1}).";

	// Token: 0x040001C6 RID: 454
	public const string net_invalidversion = "This protocol version is not supported.";

	// Token: 0x040001C7 RID: 455
	public const string net_noseek = "This stream does not support seek operations.";

	// Token: 0x040001C8 RID: 456
	public const string net_invasync = "Cannot block a call on this socket while an earlier asynchronous call is in progress.";

	// Token: 0x040001C9 RID: 457
	public const string net_io_timeout_use_gt_zero = "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.";

	// Token: 0x040001CA RID: 458
	public const string net_notconnected = "The operation is not allowed on non-connected sockets.";

	// Token: 0x040001CB RID: 459
	public const string net_notstream = "The operation is not allowed on non-stream oriented sockets.";

	// Token: 0x040001CC RID: 460
	public const string net_stopped = "Not listening. You must call the Start() method before calling this method.";

	// Token: 0x040001CD RID: 461
	public const string net_udpconnected = "Cannot send packets to an arbitrary host while connected.";

	// Token: 0x040001CE RID: 462
	public const string net_readonlystream = "The stream does not support writing.";

	// Token: 0x040001CF RID: 463
	public const string net_writeonlystream = "The stream does not support reading.";

	// Token: 0x040001D0 RID: 464
	public const string net_InvalidAddressFamily = "The AddressFamily {0} is not valid for the {1} end point, use {2} instead.";

	// Token: 0x040001D1 RID: 465
	public const string net_InvalidEndPointAddressFamily = "The supplied EndPoint of AddressFamily {0} is not valid for this Socket, use {1} instead.";

	// Token: 0x040001D2 RID: 466
	public const string net_InvalidSocketAddressSize = "The supplied {0} is an invalid size for the {1} end point.";

	// Token: 0x040001D3 RID: 467
	public const string net_invalidAddressList = "None of the discovered or specified addresses match the socket address family.";

	// Token: 0x040001D4 RID: 468
	public const string net_completed_result = "This operation cannot be performed on a completed asynchronous result object.";

	// Token: 0x040001D5 RID: 469
	public const string net_protocol_invalid_family = "'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.";

	// Token: 0x040001D6 RID: 470
	public const string net_protocol_invalid_multicast_family = "Multicast family is not the same as the family of the '{0}' Client.";

	// Token: 0x040001D7 RID: 471
	public const string net_sockets_zerolist = "The parameter {0} must contain one or more elements.";

	// Token: 0x040001D8 RID: 472
	public const string net_sockets_blocking = "The operation is not allowed on a non-blocking Socket.";

	// Token: 0x040001D9 RID: 473
	public const string net_sockets_useblocking = "Use the Blocking property to change the status of the Socket.";

	// Token: 0x040001DA RID: 474
	public const string net_sockets_select = "The operation is not allowed on objects of type {0}. Use only objects of type {1}.";

	// Token: 0x040001DB RID: 475
	public const string net_sockets_toolarge_select = "The {0} list contains too many items; a maximum of {1} is allowed.";

	// Token: 0x040001DC RID: 476
	public const string net_sockets_empty_select = "All lists are either null or empty.";

	// Token: 0x040001DD RID: 477
	public const string net_sockets_mustbind = "You must call the Bind method before performing this operation.";

	// Token: 0x040001DE RID: 478
	public const string net_sockets_mustlisten = "You must call the Listen method before performing this operation.";

	// Token: 0x040001DF RID: 479
	public const string net_sockets_mustnotlisten = "You may not perform this operation after calling the Listen method.";

	// Token: 0x040001E0 RID: 480
	public const string net_sockets_mustnotbebound = "The socket must not be bound or connected.";

	// Token: 0x040001E1 RID: 481
	public const string net_sockets_namedmustnotbebound = "{0}: The socket must not be bound or connected.";

	// Token: 0x040001E2 RID: 482
	public const string net_sockets_invalid_ipaddress_length = "The number of specified IP addresses has to be greater than 0.";

	// Token: 0x040001E3 RID: 483
	public const string net_sockets_invalid_optionValue = "The specified value is not a valid '{0}'.";

	// Token: 0x040001E4 RID: 484
	public const string net_sockets_invalid_optionValue_all = "The specified value is not valid.";

	// Token: 0x040001E5 RID: 485
	public const string net_sockets_invalid_dnsendpoint = "The parameter {0} must not be of type DnsEndPoint.";

	// Token: 0x040001E6 RID: 486
	public const string net_sockets_disconnectedConnect = "Once the socket has been disconnected, you can only reconnect again asynchronously, and only to a different EndPoint.  BeginConnect must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x040001E7 RID: 487
	public const string net_sockets_disconnectedAccept = "Once the socket has been disconnected, you can only accept again asynchronously.  BeginAccept must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x040001E8 RID: 488
	public const string net_tcplistener_mustbestopped = "The TcpListener must not be listening before performing this operation.";

	// Token: 0x040001E9 RID: 489
	public const string net_socketopinprogress = "An asynchronous socket operation is already in progress using this SocketAsyncEventArgs instance.";

	// Token: 0x040001EA RID: 490
	public const string net_buffercounttoosmall = "The Buffer space specified by the Count property is insufficient for the AcceptAsync method.";

	// Token: 0x040001EB RID: 491
	public const string net_multibuffernotsupported = "Multiple buffers cannot be used with this method.";

	// Token: 0x040001EC RID: 492
	public const string net_ambiguousbuffers = "Buffer and BufferList properties cannot both be non-null.";

	// Token: 0x040001ED RID: 493
	public const string net_io_writefailure = "Unable to write data to the transport connection: {0}.";

	// Token: 0x040001EE RID: 494
	public const string net_io_readfailure = "Unable to read data from the transport connection: {0}.";

	// Token: 0x040001EF RID: 495
	public const string net_io_invalidasyncresult = "The IAsyncResult object was not returned from the corresponding asynchronous method on this class.";

	// Token: 0x040001F0 RID: 496
	public const string net_io_invalidendcall = "{0} can only be called once for each asynchronous operation.";

	// Token: 0x040001F1 RID: 497
	public const string net_value_cannot_be_negative = "The specified value cannot be negative.";

	// Token: 0x040001F2 RID: 498
	public const string ArgumentOutOfRange_Bounds_Lower_Upper = "Argument must be between {0} and {1}.";

	// Token: 0x040001F3 RID: 499
	public const string net_sockets_connect_multiconnect_notsupported = "Sockets on this platform are invalid for use after a failed connection attempt.";

	// Token: 0x040001F4 RID: 500
	public const string net_sockets_dualmode_receivefrom_notsupported = "This platform does not support packet information for dual-mode sockets.  If packet information is not required, use Socket.Receive.  If packet information is required set Socket.DualMode to false.";

	// Token: 0x040001F5 RID: 501
	public const string net_sockets_accept_receive_notsupported = "This platform does not support receiving data with Socket.AcceptAsync.  Instead, make a separate call to Socket.ReceiveAsync.";

	// Token: 0x040001F6 RID: 502
	public const string net_sockets_duplicateandclose_notsupported = "This platform does not support Socket.DuplicateAndClose.  Instead, create a new socket.";

	// Token: 0x040001F7 RID: 503
	public const string net_sockets_transmitfileoptions_notsupported = "This platform does not support TransmitFileOptions other than TransmitFileOptions.UseDefaultWorkerThread.";

	// Token: 0x040001F8 RID: 504
	public const string ArgumentOutOfRange_PathLengthInvalid = "The path '{0}' is of an invalid length for use with domain sockets on this platform.  The length must be between 1 and {1} characters, inclusive.";

	// Token: 0x040001F9 RID: 505
	public const string net_io_readwritefailure = "Unable to transfer data on the transport connection: {0}.";

	// Token: 0x040001FA RID: 506
	public const string PlatformNotSupported_AcceptSocket = "Accepting into an existing Socket is not supported on this platform.";

	// Token: 0x040001FB RID: 507
	public const string PlatformNotSupported_IOControl = "Socket.IOControl handles Windows-specific control codes and is not supported on this platform.";

	// Token: 0x040001FC RID: 508
	public const string PlatformNotSupported_IPProtectionLevel = "IP protection level cannot be controlled on this platform.";

	// Token: 0x040001FD RID: 509
	public const string InvalidOperation_BufferNotExplicitArray = "This operation may only be performed when the buffer was set using the SetBuffer overload that accepts an array.";

	// Token: 0x040001FE RID: 510
	public const string InvalidOperation_IncorrectToken = "The result of the operation was already consumed and may not be used again.";

	// Token: 0x040001FF RID: 511
	public const string InvalidOperation_MultipleContinuations = "Another continuation was already registered.";

	// Token: 0x04000200 RID: 512
	public const string Argument_InvalidOidValue = "The OID value was invalid.";

	// Token: 0x04000201 RID: 513
	public const string Argument_InvalidValue = "Value was invalid.";

	// Token: 0x04000202 RID: 514
	public const string Arg_CryptographyException = "Error occurred during a cryptographic operation.";

	// Token: 0x04000203 RID: 515
	public const string Cryptography_ArgECDHKeySizeMismatch = "The keys from both parties must be the same size to generate a secret agreement.";

	// Token: 0x04000204 RID: 516
	public const string Cryptography_ArgECDHRequiresECDHKey = "Keys used with the ECDiffieHellmanCng algorithm must have an algorithm group of ECDiffieHellman.";

	// Token: 0x04000205 RID: 517
	public const string Cryptography_TlsRequiresLabelAndSeed = "The TLS key derivation function requires both the label and seed properties to be set.";

	// Token: 0x04000206 RID: 518
	public const string Cryptography_TlsRequires64ByteSeed = "The TLS key derivation function requires a seed value of exactly 64 bytes.";

	// Token: 0x04000207 RID: 519
	public const string Cryptography_BadHashSize_ForAlgorithm = "The provided value of {0} bytes does not match the expected size of {1} bytes for the algorithm ({2}).";

	// Token: 0x04000208 RID: 520
	public const string Cryptography_Config_EncodedOIDError = "Encoded OID length is too large (greater than 0x7f bytes).";

	// Token: 0x04000209 RID: 521
	public const string Cryptography_CSP_NoPrivateKey = "Object contains only the public half of a key pair. A private key must also be provided.";

	// Token: 0x0400020A RID: 522
	public const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";

	// Token: 0x0400020B RID: 523
	public const string Cryptography_DSA_KeyGenNotSupported = "DSA keys can be imported, but new key generation is not supported on this platform.";

	// Token: 0x0400020C RID: 524
	public const string Cryptography_Encryption_MessageTooLong = "The message exceeds the maximum allowable length for the chosen options ({0}).";

	// Token: 0x0400020D RID: 525
	public const string Cryptography_ECXmlSerializationFormatRequired = "XML serialization of an elliptic curve key requires using an overload which specifies the XML format to be used.";

	// Token: 0x0400020E RID: 526
	public const string Cryptography_ECC_NamedCurvesOnly = "Only named curves are supported on this platform.";

	// Token: 0x0400020F RID: 527
	public const string Cryptography_HashAlgorithmNameNullOrEmpty = "The hash algorithm name cannot be null or empty.";

	// Token: 0x04000210 RID: 528
	public const string Cryptography_InvalidOID = "Object identifier (OID) is unknown.";

	// Token: 0x04000211 RID: 529
	public const string Cryptography_CurveNotSupported = "The specified curve '{0}' or its parameters are not valid for this platform.";

	// Token: 0x04000212 RID: 530
	public const string Cryptography_InvalidCurveOid = "The specified Oid is not valid. The Oid.FriendlyName or Oid.Value property must be set.";

	// Token: 0x04000213 RID: 531
	public const string Cryptography_InvalidCurveKeyParameters = "The specified key parameters are not valid. Q.X and Q.Y are required fields. Q.X, Q.Y must be the same length. If D is specified it must be the same length as Q.X and Q.Y for named curves or the same length as Order for explicit curves.";

	// Token: 0x04000214 RID: 532
	public const string Cryptography_InvalidDsaParameters_MissingFields = "The specified DSA parameters are not valid; P, Q, G and Y are all required.";

	// Token: 0x04000215 RID: 533
	public const string Cryptography_InvalidDsaParameters_MismatchedPGY = "The specified DSA parameters are not valid; P, G and Y must be the same length (the key size).";

	// Token: 0x04000216 RID: 534
	public const string Cryptography_InvalidDsaParameters_MismatchedQX = "The specified DSA parameters are not valid; Q and X (if present) must be the same length.";

	// Token: 0x04000217 RID: 535
	public const string Cryptography_InvalidDsaParameters_MismatchedPJ = "The specified DSA parameters are not valid; J (if present) must be shorter than P.";

	// Token: 0x04000218 RID: 536
	public const string Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey = "The specified DSA parameters are not valid; Seed, if present, must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x04000219 RID: 537
	public const string Cryptography_InvalidDsaParameters_QRestriction_ShortKey = "The specified DSA parameters are not valid; Q must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x0400021A RID: 538
	public const string Cryptography_InvalidDsaParameters_QRestriction_LargeKey = "The specified DSA parameters are not valid; Q's length must be one of 20, 32 or 64 bytes.";

	// Token: 0x0400021B RID: 539
	public const string Cryptography_InvalidECCharacteristic2Curve = "The specified Characteristic2 curve parameters are not valid. Polynomial, A, B, G.X, G.Y, and Order are required. A, B, G.X, G.Y must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x0400021C RID: 540
	public const string Cryptography_InvalidECPrimeCurve = "The specified prime curve parameters are not valid. Prime, A, B, G.X, G.Y and Order are required and must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x0400021D RID: 541
	public const string Cryptography_InvalidECNamedCurve = "The specified named curve parameters are not valid. Only the Oid parameter must be set.";

	// Token: 0x0400021E RID: 542
	public const string Cryptography_InvalidKeySize = "Specified key is not a valid size for this algorithm.";

	// Token: 0x0400021F RID: 543
	public const string Cryptography_InvalidKey_SemiWeak = "Specified key is a known semi-weak key for '{0}' and cannot be used.";

	// Token: 0x04000220 RID: 544
	public const string Cryptography_InvalidKey_Weak = "Specified key is a known weak key for '{0}' and cannot be used.";

	// Token: 0x04000221 RID: 545
	public const string Cryptography_InvalidIVSize = "Specified initialization vector (IV) does not match the block size for this algorithm.";

	// Token: 0x04000222 RID: 546
	public const string Cryptography_InvalidOperation = "This operation is not supported for this class.";

	// Token: 0x04000223 RID: 547
	public const string Cryptography_InvalidPadding = "Padding is invalid and cannot be removed.";

	// Token: 0x04000224 RID: 548
	public const string Cryptography_InvalidRsaParameters = "The specified RSA parameters are not valid; both Exponent and Modulus are required fields.";

	// Token: 0x04000225 RID: 549
	public const string Cryptography_InvalidPaddingMode = "Specified padding mode is not valid for this algorithm.";

	// Token: 0x04000226 RID: 550
	public const string Cryptography_Invalid_IA5String = "The string contains a character not in the 7 bit ASCII character set.";

	// Token: 0x04000227 RID: 551
	public const string Cryptography_KeyTooSmall = "The key is too small for the requested operation.";

	// Token: 0x04000228 RID: 552
	public const string Cryptography_MissingIV = "The cipher mode specified requires that an initialization vector (IV) be used.";

	// Token: 0x04000229 RID: 553
	public const string Cryptography_MissingKey = "No asymmetric key object has been associated with this formatter object.";

	// Token: 0x0400022A RID: 554
	public const string Cryptography_MissingOID = "Required object identifier (OID) cannot be found.";

	// Token: 0x0400022B RID: 555
	public const string Cryptography_MustTransformWholeBlock = "TransformBlock may only process bytes in block sized increments.";

	// Token: 0x0400022C RID: 556
	public const string Cryptography_NotValidPrivateKey = "Key is not a valid private key.";

	// Token: 0x0400022D RID: 557
	public const string Cryptography_NotValidPublicOrPrivateKey = "Key is not a valid public or private key.";

	// Token: 0x0400022E RID: 558
	public const string Cryptography_OAEP_Decryption_Failed = "Error occurred while decoding OAEP padding.";

	// Token: 0x0400022F RID: 559
	public const string Cryptography_OpenInvalidHandle = "Cannot open an invalid handle.";

	// Token: 0x04000230 RID: 560
	public const string Cryptography_PartialBlock = "The input data is not a complete block.";

	// Token: 0x04000231 RID: 561
	public const string Cryptography_PasswordDerivedBytes_FewBytesSalt = "Salt is not at least eight bytes.";

	// Token: 0x04000232 RID: 562
	public const string Cryptography_RC2_EKS40 = "EffectiveKeySize value must be at least 40 bits.";

	// Token: 0x04000233 RID: 563
	public const string Cryptography_RC2_EKSKS = "KeySize value must be at least as large as the EffectiveKeySize value.";

	// Token: 0x04000234 RID: 564
	public const string Cryptography_RC2_EKSKS2 = "EffectiveKeySize must be the same as KeySize in this implementation.";

	// Token: 0x04000235 RID: 565
	public const string Cryptography_Rijndael_BlockSize = "BlockSize must be 128 in this implementation.";

	// Token: 0x04000236 RID: 566
	public const string Cryptography_RSA_DecryptWrongSize = "The length of the data to decrypt is not valid for the size of this key.";

	// Token: 0x04000237 RID: 567
	public const string Cryptography_SignHash_WrongSize = "The provided hash value is not the expected size for the specified hash algorithm.";

	// Token: 0x04000238 RID: 568
	public const string Cryptography_TransformBeyondEndOfBuffer = "Attempt to transform beyond end of buffer.";

	// Token: 0x04000239 RID: 569
	public const string Cryptography_CipherModeNotSupported = "The specified CipherMode '{0}' is not supported.";

	// Token: 0x0400023A RID: 570
	public const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";

	// Token: 0x0400023B RID: 571
	public const string Cryptography_UnknownPaddingMode = "Unknown padding mode used.";

	// Token: 0x0400023C RID: 572
	public const string Cryptography_UnexpectedTransformTruncation = "CNG provider unexpectedly terminated encryption or decryption prematurely.";

	// Token: 0x0400023D RID: 573
	public const string Cryptography_Unmapped_System_Typed_Error = "The system cryptographic library returned error '{0}' of type '{1}'";

	// Token: 0x0400023E RID: 574
	public const string Cryptography_UnsupportedPaddingMode = "The specified PaddingMode is not supported.";

	// Token: 0x0400023F RID: 575
	public const string NotSupported_Method = "Method not supported.";

	// Token: 0x04000240 RID: 576
	public const string NotSupported_SubclassOverride = "Method not supported. Derived class must override.";

	// Token: 0x04000241 RID: 577
	public const string Cryptography_AlgorithmTypesMustBeVisible = "Algorithms added to CryptoConfig must be accessable from outside their assembly.";

	// Token: 0x04000242 RID: 578
	public const string Cryptography_AddNullOrEmptyName = "CryptoConfig cannot add a mapping for a null or empty name.";

	// Token: 0x04000243 RID: 579
	public const string Argument_Invalid_SafeHandleInvalidOrClosed = "The method cannot be called with an invalid or closed SafeHandle.";

	// Token: 0x04000244 RID: 580
	public const string Cryptography_ArgExpectedECDiffieHellmanCngPublicKey = "DeriveKeyMaterial requires an ECDiffieHellmanCngPublicKey.";

	// Token: 0x04000245 RID: 581
	public const string Cryptography_ArgDSARequiresDSAKey = "Keys used with the DSACng algorithm must have an algorithm group of DSA.";

	// Token: 0x04000246 RID: 582
	public const string Cryptography_ArgECDsaRequiresECDsaKey = "Keys used with the ECDsaCng algorithm must have an algorithm group of ECDsa.";

	// Token: 0x04000247 RID: 583
	public const string Cryptography_ArgRSARequiresRSAKey = "Keys used with the RSACng algorithm must have an algorithm group of RSA.";

	// Token: 0x04000248 RID: 584
	public const string Cryptography_CngKeyWrongAlgorithm = "This key is for algorithm '{0}'. Expected '{1}'.";

	// Token: 0x04000249 RID: 585
	public const string Cryptography_InvalidAlgorithmGroup = "The algorithm group '{0}' is invalid.";

	// Token: 0x0400024A RID: 586
	public const string Cryptography_InvalidAlgorithmName = "The algorithm name '{0}' is invalid.";

	// Token: 0x0400024B RID: 587
	public const string Cryptography_InvalidCipherMode = "Specified cipher mode is not valid for this algorithm.";

	// Token: 0x0400024C RID: 588
	public const string Cryptography_InvalidKeyBlobFormat = "The key blob format '{0}' is invalid.";

	// Token: 0x0400024D RID: 589
	public const string Cryptography_InvalidProviderName = "The provider name '{0}' is invalid.";

	// Token: 0x0400024E RID: 590
	public const string Cryptography_KeyBlobParsingError = "Key Blob not in expected format.";

	// Token: 0x0400024F RID: 591
	public const string Cryptography_OpenEphemeralKeyHandleWithoutEphemeralFlag = "The CNG key handle being opened was detected to be ephemeral, but the EphemeralKey open option was not specified.";

	// Token: 0x04000250 RID: 592
	public const string Cryptography_WeakKey = "Specified key is a known weak key for this algorithm and cannot be used.";

	// Token: 0x04000251 RID: 593
	public const string PlatformNotSupported_CryptographyCng = "Windows Cryptography Next Generation (CNG) is not supported on this platform.";

	// Token: 0x04000252 RID: 594
	public const string CountdownEvent_Increment_AlreadyZero = "The event is already signaled and cannot be incremented.";

	// Token: 0x04000253 RID: 595
	public const string CountdownEvent_Increment_AlreadyMax = "The increment operation would cause the CurrentCount to overflow.";

	// Token: 0x04000254 RID: 596
	public const string CountdownEvent_Decrement_BelowZero = "Invalid attempt made to decrement the event's count below zero.";

	// Token: 0x04000255 RID: 597
	public const string Common_OperationCanceled = "The operation was canceled.";

	// Token: 0x04000256 RID: 598
	public const string Barrier_Dispose = "The barrier has been disposed.";

	// Token: 0x04000257 RID: 599
	public const string Barrier_SignalAndWait_InvalidOperation_ZeroTotal = "The barrier has no registered participants.";

	// Token: 0x04000258 RID: 600
	public const string Barrier_SignalAndWait_ArgumentOutOfRange = "The specified timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000259 RID: 601
	public const string Barrier_RemoveParticipants_InvalidOperation = "The participantCount argument is greater than the number of participants that haven't yet arrived at the barrier in this phase.";

	// Token: 0x0400025A RID: 602
	public const string Barrier_RemoveParticipants_ArgumentOutOfRange = "The participantCount argument must be less than or equal the number of participants.";

	// Token: 0x0400025B RID: 603
	public const string Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x0400025C RID: 604
	public const string Barrier_InvalidOperation_CalledFromPHA = "This method may not be called from within the postPhaseAction.";

	// Token: 0x0400025D RID: 605
	public const string Barrier_AddParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x0400025E RID: 606
	public const string Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded = "The number of threads using the barrier exceeded the total number of registered participants.";

	// Token: 0x0400025F RID: 607
	public const string BarrierPostPhaseException = "The postPhaseAction failed with an exception.";

	// Token: 0x04000260 RID: 608
	public const string Barrier_ctor_ArgumentOutOfRange = "The participantCount argument must be non-negative and less than or equal to 32767.";

	// Token: 0x04000261 RID: 609
	public const string Barrier_AddParticipants_Overflow_ArgumentOutOfRange = "Adding participantCount participants would result in the number of participants exceeding the maximum number allowed.";

	// Token: 0x04000262 RID: 610
	public const string SynchronizationLockException_IncorrectDispose = "The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock.";

	// Token: 0x04000263 RID: 611
	public const string SynchronizationLockException_MisMatchedWrite = "The write lock is being released without being held.";

	// Token: 0x04000264 RID: 612
	public const string LockRecursionException_UpgradeAfterReadNotAllowed = "Upgradeable lock may not be acquired with read lock held.";

	// Token: 0x04000265 RID: 613
	public const string LockRecursionException_UpgradeAfterWriteNotAllowed = "Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer.";

	// Token: 0x04000266 RID: 614
	public const string SynchronizationLockException_MisMatchedUpgrade = "The upgradeable lock is being released without being held.";

	// Token: 0x04000267 RID: 615
	public const string SynchronizationLockException_MisMatchedRead = "The read lock is being released without being held.";

	// Token: 0x04000268 RID: 616
	public const string LockRecursionException_WriteAfterReadNotAllowed = "Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.";

	// Token: 0x04000269 RID: 617
	public const string LockRecursionException_RecursiveWriteNotAllowed = "Recursive write lock acquisitions not allowed in this mode.";

	// Token: 0x0400026A RID: 618
	public const string LockRecursionException_ReadAfterWriteNotAllowed = "A read lock may not be acquired with the write lock held in this mode.";

	// Token: 0x0400026B RID: 619
	public const string LockRecursionException_RecursiveUpgradeNotAllowed = "Recursive upgradeable lock acquisitions not allowed in this mode.";

	// Token: 0x0400026C RID: 620
	public const string LockRecursionException_RecursiveReadNotAllowed = "Recursive read lock acquisitions not allowed in this mode.";

	// Token: 0x0400026D RID: 621
	public const string Overflow_UInt16 = "Value was either too large or too small for a UInt16.";

	// Token: 0x0400026E RID: 622
	public const string ReaderWriterLock_Timeout = "The operation has timed out. {0}";

	// Token: 0x0400026F RID: 623
	public const string ArgumentOutOfRange_TimeoutMilliseconds = "Timeout value in milliseconds must be nonnegative and less than or equal to Int32.MaxValue, or -1 for an infinite timeout.";

	// Token: 0x04000270 RID: 624
	public const string ReaderWriterLock_NotOwner = "Attempt to release a lock that is not owned by the calling thread. {0}";

	// Token: 0x04000271 RID: 625
	public const string ExceptionFromHResult = "(Exception from HRESULT: 0x{0:X})";

	// Token: 0x04000272 RID: 626
	public const string ReaderWriterLock_InvalidLockCookie = "The specified lock cookie is invalid for this operation. {0}";

	// Token: 0x04000273 RID: 627
	public const string ReaderWriterLock_RestoreLockWithOwnedLocks = "ReaderWriterLock.RestoreLock was called without releasing all locks acquired since the call to ReleaseLock.";

	// Token: 0x04000274 RID: 628
	public const string HostExecutionContextManager_InvalidOperation_NotNewCaptureContext = "Cannot apply a context that has been marshaled across AppDomains, that was not acquired through a Capture operation or that has already been the argument to a Set call.";

	// Token: 0x04000275 RID: 629
	public const string HostExecutionContextManager_InvalidOperation_CannotOverrideSetWithoutRevert = "Must override both HostExecutionContextManager.SetHostExecutionContext and HostExecutionContextManager.Revert.";

	// Token: 0x04000276 RID: 630
	public const string HostExecutionContextManager_InvalidOperation_CannotUseSwitcherOtherThread = "Undo operation must be performed on the thread where the corresponding context was Set.";

	// Token: 0x04000277 RID: 631
	public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";

	// Token: 0x04000278 RID: 632
	public const string Arg_WrongType = "The value '{0}' is not of type '{1}' and cannot be used in this generic collection.";

	// Token: 0x04000279 RID: 633
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x0400027A RID: 634
	public const string ArgumentOutOfRange_SmallCapacity = "capacity was less than the current size.";

	// Token: 0x0400027B RID: 635
	public const string Argument_AddingDuplicate = "An item with the same key has already been added. Key: {0}";

	// Token: 0x0400027C RID: 636
	public const string InvalidOperation_ConcurrentOperationsNotSupported = "Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.";

	// Token: 0x0400027D RID: 637
	public const string InvalidOperation_EmptyQueue = "Queue empty.";

	// Token: 0x0400027E RID: 638
	public const string InvalidOperation_EnumOpCantHappen = "Enumeration has either not started or has already finished.";

	// Token: 0x0400027F RID: 639
	public const string InvalidOperation_EnumFailedVersion = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000280 RID: 640
	public const string InvalidOperation_EmptyStack = "Stack empty.";

	// Token: 0x04000281 RID: 641
	public const string InvalidOperation_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

	// Token: 0x04000282 RID: 642
	public const string InvalidOperation_EnumEnded = "Enumeration already finished.";

	// Token: 0x04000283 RID: 643
	public const string NotSupported_KeyCollectionSet = "Mutating a key collection derived from a dictionary is not allowed.";

	// Token: 0x04000284 RID: 644
	public const string NotSupported_ValueCollectionSet = "Mutating a value collection derived from a dictionary is not allowed.";

	// Token: 0x04000285 RID: 645
	public const string Arg_ArrayLengthsDiffer = "Array lengths must be the same.";

	// Token: 0x04000286 RID: 646
	public const string Arg_BitArrayTypeUnsupported = "Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].";

	// Token: 0x04000287 RID: 647
	public const string Arg_HSCapacityOverflow = "HashSet capacity is too big.";

	// Token: 0x04000288 RID: 648
	public const string Arg_HTCapacityOverflow = "Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.";

	// Token: 0x04000289 RID: 649
	public const string Arg_InsufficientSpace = "Insufficient space in the target location to copy the information.";

	// Token: 0x0400028A RID: 650
	public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

	// Token: 0x0400028B RID: 651
	public const string Argument_ArrayTooLarge = "The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.";

	// Token: 0x0400028C RID: 652
	public const string Argument_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x0400028D RID: 653
	public const string ArgumentOutOfRange_BiggerThanCollection = "Must be less than or equal to the size of the collection.";

	// Token: 0x0400028E RID: 654
	public const string ArgumentOutOfRange_Index = "Index was out of range. Must be non-negative and less than the size of the collection.";

	// Token: 0x0400028F RID: 655
	public const string ExternalLinkedListNode = "The LinkedList node does not belong to current LinkedList.";

	// Token: 0x04000290 RID: 656
	public const string LinkedListEmpty = "The LinkedList is empty.";

	// Token: 0x04000291 RID: 657
	public const string LinkedListNodeIsAttached = "The LinkedList node already belongs to a LinkedList.";

	// Token: 0x04000292 RID: 658
	public const string NotSupported_SortedListNestedWrite = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";

	// Token: 0x04000293 RID: 659
	public const string SortedSet_LowerValueGreaterThanUpperValue = "Must be less than or equal to upperValue.";

	// Token: 0x04000294 RID: 660
	public const string Serialization_InvalidOnDeser = "OnDeserialization method was called while the object was not being deserialized.";

	// Token: 0x04000295 RID: 661
	public const string Serialization_MismatchedCount = "The serialized Count information doesn't match the number of items.";

	// Token: 0x04000296 RID: 662
	public const string Serialization_MissingKeys = "The keys for this dictionary are missing.";

	// Token: 0x04000297 RID: 663
	public const string Serialization_MissingValues = "The values for this dictionary are missing.";

	// Token: 0x04000298 RID: 664
	public const string Argument_MapNameEmptyString = "Map name cannot be an empty string.";

	// Token: 0x04000299 RID: 665
	public const string Argument_EmptyFile = "A positive capacity must be specified for a Memory Mapped File backed by an empty file.";

	// Token: 0x0400029A RID: 666
	public const string Argument_NewMMFWriteAccessNotAllowed = "MemoryMappedFileAccess.Write is not permitted when creating new memory mapped files. Use MemoryMappedFileAccess.ReadWrite instead.";

	// Token: 0x0400029B RID: 667
	public const string Argument_ReadAccessWithLargeCapacity = "When specifying MemoryMappedFileAccess.Read access, the capacity must not be larger than the file size.";

	// Token: 0x0400029C RID: 668
	public const string Argument_NewMMFAppendModeNotAllowed = "FileMode.Append is not permitted when creating new memory mapped files. Instead, use MemoryMappedFileView to ensure write-only access within a specified region.";

	// Token: 0x0400029D RID: 669
	public const string Argument_NewMMFTruncateModeNotAllowed = "FileMode.Truncate is not permitted when creating new memory mapped files.";

	// Token: 0x0400029E RID: 670
	public const string ArgumentNull_MapName = "Map name cannot be null.";

	// Token: 0x0400029F RID: 671
	public const string ArgumentNull_FileStream = "fileStream cannot be null.";

	// Token: 0x040002A0 RID: 672
	public const string ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed = "The capacity cannot be greater than the size of the system's logical address space.";

	// Token: 0x040002A1 RID: 673
	public const string ArgumentOutOfRange_NeedPositiveNumber = "A positive number is required.";

	// Token: 0x040002A2 RID: 674
	public const string ArgumentOutOfRange_PositiveOrDefaultCapacityRequired = "The capacity must be greater than or equal to 0. 0 represents the size of the file being mapped.";

	// Token: 0x040002A3 RID: 675
	public const string ArgumentOutOfRange_PositiveOrDefaultSizeRequired = "The size must be greater than or equal to 0. If 0 is specified, the view extends from the specified offset to the end of the file mapping.";

	// Token: 0x040002A4 RID: 676
	public const string ArgumentOutOfRange_CapacityGEFileSizeRequired = "The capacity may not be smaller than the file size.";

	// Token: 0x040002A5 RID: 677
	public const string IO_NotEnoughMemory = "Not enough memory to map view.";

	// Token: 0x040002A6 RID: 678
	public const string InvalidOperation_CantCreateFileMapping = "Cannot create file mapping.";

	// Token: 0x040002A7 RID: 679
	public const string NotSupported_MMViewStreamsFixedLength = "MemoryMappedViewStreams are fixed length.";

	// Token: 0x040002A8 RID: 680
	public const string ObjectDisposed_ViewAccessorClosed = "Cannot access a closed accessor.";

	// Token: 0x040002A9 RID: 681
	public const string ObjectDisposed_StreamIsClosed = "Cannot access a closed Stream.";

	// Token: 0x040002AA RID: 682
	public const string PlatformNotSupported_NamedMaps = "Named maps are not supported.";

	// Token: 0x040002AB RID: 683
	public const string MethodBuilderDoesNotHaveTypeBuilder = "MethodBuilder does not have a valid TypeBuilder";

	// Token: 0x040002AC RID: 684
	public const string Cryptography_NonCompliantFIPSAlgorithm = "This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms.";

	// Token: 0x040002AD RID: 685
	public const string InvalidOperation_ViewIsNull = "The underlying MemoryMappedView object is null.";

	// Token: 0x040002AE RID: 686
	public const string ArgumentOutOfRange_InvalidPipeAccessRights = "Invalid PipeAccessRights flag.";

	// Token: 0x040002AF RID: 687
	public const string ArgumentOutOfRange_AdditionalAccessLimited = "additionalAccessRights is limited to the PipeAccessRights.ChangePermissions, PipeAccessRights.TakeOwnership, and PipeAccessRights.AccessSystemSecurity flags when creating NamedPipeServerStreams.";

	// Token: 0x040002B0 RID: 688
	public const string InterfaceType_Must_Be_Interface = "The type '{0}' must be an interface, not a class.";

	// Token: 0x040002B1 RID: 689
	public const string BaseType_Cannot_Be_Sealed = "The base type '{0}' cannot be sealed.";

	// Token: 0x040002B2 RID: 690
	public const string BaseType_Cannot_Be_Abstract = "The base type '{0}' cannot be abstract.";

	// Token: 0x040002B3 RID: 691
	public const string BaseType_Must_Have_Default_Ctor = "The base type '{0}' must have a public parameterless constructor.";

	// Token: 0x040002B4 RID: 692
	public const string Cryptography_Cert_AlreadyHasPrivateKey = "The certificate already has an associated private key.";

	// Token: 0x040002B5 RID: 693
	public const string Cryptography_PrivateKey_WrongAlgorithm = "The provided key does not match the public key algorithm for this certificate.";

	// Token: 0x040002B6 RID: 694
	public const string Cryptography_PrivateKey_DoesNotMatch = "The provided key does not match the public key for this certificate.";
}
