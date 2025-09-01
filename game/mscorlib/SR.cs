using System;
using System.Globalization;

// Token: 0x0200003C RID: 60
internal static class SR
{
	// Token: 0x06000077 RID: 119 RVA: 0x00002719 File Offset: 0x00000919
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00002727 File Offset: 0x00000927
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000270D File Offset: 0x0000090D
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00002731 File Offset: 0x00000931
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00002734 File Offset: 0x00000934
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00002747 File Offset: 0x00000947
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00002755 File Offset: 0x00000955
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00002764 File Offset: 0x00000964
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000276F File Offset: 0x0000096F
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000270D File Offset: 0x0000090D
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000270D File Offset: 0x0000090D
	public static object GetObject(string name)
	{
		return name;
	}

	// Token: 0x040001F2 RID: 498
	public const string RTL = "RTL_False";

	// Token: 0x040001F3 RID: 499
	public const string ContinueButtonText = "Continue";

	// Token: 0x040001F4 RID: 500
	public const string DebugMessageTruncated = "{0}...\n<truncated>";

	// Token: 0x040001F5 RID: 501
	public const string DebugAssertTitleShort = "Assertion Failed";

	// Token: 0x040001F6 RID: 502
	public const string DebugAssertTitle = "Assertion Failed: Cancel=Debug, OK=Continue";

	// Token: 0x040001F7 RID: 503
	public const string NotSupported = "This operation is not supported.";

	// Token: 0x040001F8 RID: 504
	public const string DebugLaunchFailed = "Cannot launch the debugger.  Make sure that a Microsoft (R) .NET Framework debugger is properly installed.";

	// Token: 0x040001F9 RID: 505
	public const string DebugLaunchFailedTitle = "Microsoft .NET Framework Debug Launch Failure";

	// Token: 0x040001FA RID: 506
	public const string ObjectDisposed = "Object {0} has been disposed and can no longer be used.";

	// Token: 0x040001FB RID: 507
	public const string ExceptionOccurred = "An exception occurred writing trace output to log file '{0}'. {1}";

	// Token: 0x040001FC RID: 508
	public const string MustAddListener = "Only TraceListeners can be added to a TraceListenerCollection.";

	// Token: 0x040001FD RID: 509
	public const string ToStringNull = "(null)";

	// Token: 0x040001FE RID: 510
	public const string EnumConverterInvalidValue = "The value '{0}' is not a valid value for the enum '{1}'.";

	// Token: 0x040001FF RID: 511
	public const string ConvertFromException = "{0} cannot convert from {1}.";

	// Token: 0x04000200 RID: 512
	public const string ConvertToException = "'{0}' is unable to convert '{1}' to '{2}'.";

	// Token: 0x04000201 RID: 513
	public const string ConvertInvalidPrimitive = "{0} is not a valid value for {1}.";

	// Token: 0x04000202 RID: 514
	public const string ErrorMissingPropertyAccessors = "Accessor methods for the {0} property are missing.";

	// Token: 0x04000203 RID: 515
	public const string ErrorInvalidPropertyType = "Invalid type for the {0} property.";

	// Token: 0x04000204 RID: 516
	public const string ErrorMissingEventAccessors = "Accessor methods for the {0} event are missing.";

	// Token: 0x04000205 RID: 517
	public const string ErrorInvalidEventHandler = "Invalid event handler for the {0} event.";

	// Token: 0x04000206 RID: 518
	public const string ErrorInvalidEventType = "Invalid type for the {0} event.";

	// Token: 0x04000207 RID: 519
	public const string InvalidMemberName = "Invalid member name.";

	// Token: 0x04000208 RID: 520
	public const string ErrorBadExtenderType = "The {0} extender provider is not compatible with the {1} type.";

	// Token: 0x04000209 RID: 521
	public const string NullableConverterBadCtorArg = "The specified type is not a nullable type.";

	// Token: 0x0400020A RID: 522
	public const string TypeDescriptorExpectedElementType = "Expected types in the collection to be of type {0}.";

	// Token: 0x0400020B RID: 523
	public const string TypeDescriptorSameAssociation = "Cannot create an association when the primary and secondary objects are the same.";

	// Token: 0x0400020C RID: 524
	public const string TypeDescriptorAlreadyAssociated = "The primary and secondary objects are already associated with each other.";

	// Token: 0x0400020D RID: 525
	public const string TypeDescriptorProviderError = "The type description provider {0} has returned null from {1} which is illegal.";

	// Token: 0x0400020E RID: 526
	public const string TypeDescriptorUnsupportedRemoteObject = "The object {0} is being remoted by a proxy that does not support interface discovery.  This type of remoted object is not supported.";

	// Token: 0x0400020F RID: 527
	public const string TypeDescriptorArgsCountMismatch = "The number of elements in the Type and Object arrays must match.";

	// Token: 0x04000210 RID: 528
	public const string ErrorCreateSystemEvents = "Failed to create system events window thread.";

	// Token: 0x04000211 RID: 529
	public const string ErrorCreateTimer = "Cannot create timer.";

	// Token: 0x04000212 RID: 530
	public const string ErrorKillTimer = "Cannot end timer.";

	// Token: 0x04000213 RID: 531
	public const string ErrorSystemEventsNotSupported = "System event notifications are not supported under the current context. Server processes, for example, may not support global system event notifications.";

	// Token: 0x04000214 RID: 532
	public const string ErrorGetTempPath = "Cannot get temporary file name";

	// Token: 0x04000215 RID: 533
	public const string CHECKOUTCanceled = "The checkout was canceled by the user.";

	// Token: 0x04000216 RID: 534
	public const string ErrorInvalidServiceInstance = "The service instance must derive from or implement {0}.";

	// Token: 0x04000217 RID: 535
	public const string ErrorServiceExists = "The service {0} already exists in the service container.";

	// Token: 0x04000218 RID: 536
	public const string Argument_InvalidNumberStyles = "An undefined NumberStyles value is being used.";

	// Token: 0x04000219 RID: 537
	public const string Argument_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	// Token: 0x0400021A RID: 538
	public const string Argument_ByteArrayLengthMustBeAMultipleOf4 = "The Byte[] length must be a multiple of 4.";

	// Token: 0x0400021B RID: 539
	public const string Argument_InvalidCharactersInString = "The string contained an invalid character.";

	// Token: 0x0400021C RID: 540
	public const string Argument_ParsedStringWasInvalid = "The parsed string was invalid.";

	// Token: 0x0400021D RID: 541
	public const string Argument_MustBeBigInt = "The parameter must be a BigInteger.";

	// Token: 0x0400021E RID: 542
	public const string Format_InvalidFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x0400021F RID: 543
	public const string Format_TooLarge = "The value is too large to be represented by this format specifier.";

	// Token: 0x04000220 RID: 544
	public const string ArgumentOutOfRange_MustBeLessThanUInt32MaxValue = "The value must be less than UInt32.MaxValue (2^32).";

	// Token: 0x04000221 RID: 545
	public const string ArgumentOutOfRange_MustBeNonNeg = "The number must be greater than or equal to zero.";

	// Token: 0x04000222 RID: 546
	public const string NotSupported_NumberStyle = "The NumberStyle option is not supported.";

	// Token: 0x04000223 RID: 547
	public const string Overflow_BigIntInfinity = "BigInteger cannot represent infinity.";

	// Token: 0x04000224 RID: 548
	public const string Overflow_NotANumber = "The value is not a number.";

	// Token: 0x04000225 RID: 549
	public const string Overflow_ParseBigInteger = "The value could not be parsed.";

	// Token: 0x04000226 RID: 550
	public const string Overflow_Int32 = "Value was either too large or too small for an Int32.";

	// Token: 0x04000227 RID: 551
	public const string Overflow_Int64 = "Value was either too large or too small for an Int64.";

	// Token: 0x04000228 RID: 552
	public const string Overflow_UInt32 = "Value was either too large or too small for a UInt32.";

	// Token: 0x04000229 RID: 553
	public const string Overflow_UInt64 = "Value was either too large or too small for a UInt64.";

	// Token: 0x0400022A RID: 554
	public const string Overflow_Decimal = "Value was either too large or too small for a Decimal.";

	// Token: 0x0400022B RID: 555
	public const string Argument_FrameworkNameTooShort = "FrameworkName cannot have less than two components or more than three components.";

	// Token: 0x0400022C RID: 556
	public const string Argument_FrameworkNameInvalid = "FrameworkName is invalid.";

	// Token: 0x0400022D RID: 557
	public const string Argument_FrameworkNameInvalidVersion = "FrameworkName version component is invalid.";

	// Token: 0x0400022E RID: 558
	public const string Argument_FrameworkNameMissingVersion = "FrameworkName version component is missing.";

	// Token: 0x0400022F RID: 559
	public const string ArgumentNull_Key = "Key cannot be null.";

	// Token: 0x04000230 RID: 560
	public const string Argument_InvalidValue = "Argument {0} should be larger than {1}.";

	// Token: 0x04000231 RID: 561
	public const string Arg_MultiRank = "Multi dimension array is not supported on this operation.";

	// Token: 0x04000232 RID: 562
	public const string Barrier_ctor_ArgumentOutOfRange = "The participantCount argument must be non-negative and less than or equal to 32767.";

	// Token: 0x04000233 RID: 563
	public const string Barrier_AddParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x04000234 RID: 564
	public const string Barrier_AddParticipants_Overflow_ArgumentOutOfRange = "Adding participantCount participants would result in the number of participants exceeding the maximum number allowed.";

	// Token: 0x04000235 RID: 565
	public const string Barrier_InvalidOperation_CalledFromPHA = "This method may not be called from within the postPhaseAction.";

	// Token: 0x04000236 RID: 566
	public const string Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x04000237 RID: 567
	public const string Barrier_RemoveParticipants_ArgumentOutOfRange = "The participantCount argument must be less than or equal the number of participants.";

	// Token: 0x04000238 RID: 568
	public const string Barrier_RemoveParticipants_InvalidOperation = "The participantCount argument is greater than the number of participants that haven't yet arrived at the barrier in this phase.";

	// Token: 0x04000239 RID: 569
	public const string Barrier_SignalAndWait_ArgumentOutOfRange = "The specified timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x0400023A RID: 570
	public const string Barrier_SignalAndWait_InvalidOperation_ZeroTotal = "The barrier has no registered participants.";

	// Token: 0x0400023B RID: 571
	public const string Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded = "The number of threads using the barrier exceeded the total number of registered participants.";

	// Token: 0x0400023C RID: 572
	public const string Barrier_Dispose = "The barrier has been disposed.";

	// Token: 0x0400023D RID: 573
	public const string BarrierPostPhaseException = "The postPhaseAction failed with an exception.";

	// Token: 0x0400023E RID: 574
	public const string UriTypeConverter_ConvertFrom_CannotConvert = "{0} cannot convert from {1}.";

	// Token: 0x0400023F RID: 575
	public const string UriTypeConverter_ConvertTo_CannotConvert = "{0} cannot convert {1} to {2}.";

	// Token: 0x04000240 RID: 576
	public const string ISupportInitializeDescr = "Specifies support for transacted initialization.";

	// Token: 0x04000241 RID: 577
	public const string CantModifyListSortDescriptionCollection = "Once a ListSortDescriptionCollection has been created it can't be modified.";

	// Token: 0x04000242 RID: 578
	public const string Argument_NullComment = "The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.";

	// Token: 0x04000243 RID: 579
	public const string InvalidPrimitiveType = "Invalid Primitive Type: {0}. Consider using CodeObjectCreateExpression.";

	// Token: 0x04000244 RID: 580
	public const string Cannot_Specify_Both_Compiler_Path_And_Version = "Cannot specify both the '{0}' and '{1}' CodeDom provider options to choose a compiler. Please remove one of them.";

	// Token: 0x04000245 RID: 581
	public const string CodeGenOutputWriter = "The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.";

	// Token: 0x04000246 RID: 582
	public const string CodeGenReentrance = "This code generation API cannot be called while the generator is being used to generate something else.";

	// Token: 0x04000247 RID: 583
	public const string InvalidLanguageIdentifier = "The identifier:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent identifier name. Check to see if CodeGenerator.IsValidLanguageIndependentIdentifier allows the identifier name.";

	// Token: 0x04000248 RID: 584
	public const string InvalidTypeName = "The type name:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent type name.";

	// Token: 0x04000249 RID: 585
	public const string Empty_attribute = "The '{0}' attribute cannot be an empty string.";

	// Token: 0x0400024A RID: 586
	public const string Invalid_nonnegative_integer_attribute = "The '{0}' attribute must be a non-negative integer.";

	// Token: 0x0400024B RID: 587
	public const string CodeDomProvider_NotDefined = "There is no CodeDom provider defined for the language.";

	// Token: 0x0400024C RID: 588
	public const string Language_Names_Cannot_Be_Empty = "You need to specify a non-empty String for a language name in the CodeDom configuration section.";

	// Token: 0x0400024D RID: 589
	public const string Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based = "An extension name in the CodeDom configuration section must be a non-empty string which starts with a period.";

	// Token: 0x0400024E RID: 590
	public const string Unable_To_Locate_Type = "The CodeDom provider type \"{0}\" could not be located.";

	// Token: 0x0400024F RID: 591
	public const string NotSupported_CodeDomAPI = "This CodeDomProvider does not support this method.";

	// Token: 0x04000250 RID: 592
	public const string ArityDoesntMatch = "The total arity specified in '{0}' does not match the number of TypeArguments supplied.  There were '{1}' TypeArguments supplied.";

	// Token: 0x04000251 RID: 593
	public const string PartialTrustErrorTextReplacement = "<The original value of this property potentially contains file system information and has been suppressed.>";

	// Token: 0x04000252 RID: 594
	public const string PartialTrustIllegalProvider = "When used in partial trust, langID must be C#, VB, J#, or JScript, and the language provider must be in the global assembly cache.";

	// Token: 0x04000253 RID: 595
	public const string IllegalAssemblyReference = "Assembly references cannot begin with '-', or contain a '/' or '\\'.";

	// Token: 0x04000254 RID: 596
	public const string NullOrEmpty_Value_in_Property = "The '{0}' property cannot contain null or empty strings.";

	// Token: 0x04000255 RID: 597
	public const string AutoGen_Comment_Line1 = "auto-generated>";

	// Token: 0x04000256 RID: 598
	public const string AutoGen_Comment_Line2 = "This code was generated by a tool.";

	// Token: 0x04000257 RID: 599
	public const string AutoGen_Comment_Line3 = "Runtime Version:";

	// Token: 0x04000258 RID: 600
	public const string AutoGen_Comment_Line4 = "Changes to this file may cause incorrect behavior and will be lost if";

	// Token: 0x04000259 RID: 601
	public const string AutoGen_Comment_Line5 = "the code is regenerated.";

	// Token: 0x0400025A RID: 602
	public const string CantContainNullEntries = "Array '{0}' cannot contain null entries.";

	// Token: 0x0400025B RID: 603
	public const string InvalidPathCharsInChecksum = "The CodeChecksumPragma file name '{0}' contains invalid path characters.";

	// Token: 0x0400025C RID: 604
	public const string InvalidRegion = "The region directive '{0}' contains invalid characters.  RegionText cannot contain any new line characters.";

	// Token: 0x0400025D RID: 605
	public const string Provider_does_not_support_options = "This CodeDomProvider type does not have a constructor that takes providerOptions - \"{0}\"";

	// Token: 0x0400025E RID: 606
	public const string MetaExtenderName = "{0} on {1}";

	// Token: 0x0400025F RID: 607
	public const string InvalidEnumArgument = "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.";

	// Token: 0x04000260 RID: 608
	public const string InvalidArgument = "'{1}' is not a valid value for '{0}'.";

	// Token: 0x04000261 RID: 609
	public const string InvalidNullArgument = "Null is not a valid value for {0}.";

	// Token: 0x04000262 RID: 610
	public const string LicExceptionTypeOnly = "A valid license cannot be granted for the type {0}. Contact the manufacturer of the component for more information.";

	// Token: 0x04000263 RID: 611
	public const string LicExceptionTypeAndInstance = "An instance of type '{1}' was being created, and a valid license could not be granted for the type '{0}'. Please,  contact the manufacturer of the component for more information.";

	// Token: 0x04000264 RID: 612
	public const string LicMgrContextCannotBeChanged = "The CurrentContext property of the LicenseManager is currently locked and cannot be changed.";

	// Token: 0x04000265 RID: 613
	public const string LicMgrAlreadyLocked = "The CurrentContext property of the LicenseManager is already locked by another user.";

	// Token: 0x04000266 RID: 614
	public const string LicMgrDifferentUser = "The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser.";

	// Token: 0x04000267 RID: 615
	public const string InvalidElementType = "Element type {0} is not supported.";

	// Token: 0x04000268 RID: 616
	public const string InvalidIdentifier = "Identifier '{0}' is not valid.";

	// Token: 0x04000269 RID: 617
	public const string ExecFailedToCreate = "Failed to create file {0}.";

	// Token: 0x0400026A RID: 618
	public const string ExecTimeout = "Timed out waiting for a program to execute. The command being executed was {0}.";

	// Token: 0x0400026B RID: 619
	public const string ExecBadreturn = "An invalid return code was encountered waiting for a program to execute. The command being executed was {0}.";

	// Token: 0x0400026C RID: 620
	public const string ExecCantGetRetCode = "Unable to get the return code for a program being executed. The command that was being executed was '{0}'.";

	// Token: 0x0400026D RID: 621
	public const string ExecCantExec = "Cannot execute a program. The command being executed was {0}.";

	// Token: 0x0400026E RID: 622
	public const string ExecCantRevert = "Cannot execute a program. Impersonation failed.";

	// Token: 0x0400026F RID: 623
	public const string CompilerNotFound = "Compiler executable file {0} cannot be found.";

	// Token: 0x04000270 RID: 624
	public const string DuplicateFileName = "The file name '{0}' was already in the collection.";

	// Token: 0x04000271 RID: 625
	public const string CollectionReadOnly = "Collection is read-only.";

	// Token: 0x04000272 RID: 626
	public const string BitVectorFull = "Bit vector is full.";

	// Token: 0x04000273 RID: 627
	public const string ArrayConverterText = "{0} Array";

	// Token: 0x04000274 RID: 628
	public const string CollectionConverterText = "(Collection)";

	// Token: 0x04000275 RID: 629
	public const string MultilineStringConverterText = "(Text)";

	// Token: 0x04000276 RID: 630
	public const string CultureInfoConverterDefaultCultureString = "(Default)";

	// Token: 0x04000277 RID: 631
	public const string CultureInfoConverterInvalidCulture = "The {0} culture cannot be converted to a CultureInfo object on this computer.";

	// Token: 0x04000278 RID: 632
	public const string InvalidPrimitive = "The text {0} is not a valid {1}.";

	// Token: 0x04000279 RID: 633
	public const string TimerInvalidInterval = "'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.";

	// Token: 0x0400027A RID: 634
	public const string TraceSwitchLevelTooHigh = "Attempted to set {0} to a value that is too high.  Setting level to TraceLevel.Verbose";

	// Token: 0x0400027B RID: 635
	public const string TraceSwitchLevelTooLow = "Attempted to set {0} to a value that is too low.  Setting level to TraceLevel.Off";

	// Token: 0x0400027C RID: 636
	public const string TraceSwitchInvalidLevel = "The Level must be set to a value in the enumeration TraceLevel.";

	// Token: 0x0400027D RID: 637
	public const string TraceListenerIndentSize = "The IndentSize property must be non-negative.";

	// Token: 0x0400027E RID: 638
	public const string TraceListenerFail = "Fail:";

	// Token: 0x0400027F RID: 639
	public const string TraceAsTraceSource = "Trace";

	// Token: 0x04000280 RID: 640
	public const string InvalidLowBoundArgument = "'{1}' is not a valid value for '{0}'. '{0}' must be greater than {2}.";

	// Token: 0x04000281 RID: 641
	public const string DuplicateComponentName = "Duplicate component name '{0}'.  Component names must be unique and case-insensitive.";

	// Token: 0x04000282 RID: 642
	public const string NotImplemented = "{0}: Not implemented";

	// Token: 0x04000283 RID: 643
	public const string OutOfMemory = "Could not allocate needed memory.";

	// Token: 0x04000284 RID: 644
	public const string EOF = "End of data stream encountered.";

	// Token: 0x04000285 RID: 645
	public const string IOError = "Unknown input/output failure.";

	// Token: 0x04000286 RID: 646
	public const string BadChar = "Unexpected Character: '{0}'.";

	// Token: 0x04000287 RID: 647
	public const string toStringNone = "(none)";

	// Token: 0x04000288 RID: 648
	public const string toStringUnknown = "(unknown)";

	// Token: 0x04000289 RID: 649
	public const string InvalidEnum = "{0} is not a valid {1} value.";

	// Token: 0x0400028A RID: 650
	public const string IndexOutOfRange = "Index {0} is out of range.";

	// Token: 0x0400028B RID: 651
	public const string ErrorPropertyAccessorException = "Property accessor '{0}' on object '{1}' threw the following exception:'{2}'";

	// Token: 0x0400028C RID: 652
	public const string InvalidOperation = "Invalid operation.";

	// Token: 0x0400028D RID: 653
	public const string EmptyStack = "Stack has no items in it.";

	// Token: 0x0400028E RID: 654
	public const string PerformanceCounterDesc = "Represents a Windows performance counter component.";

	// Token: 0x0400028F RID: 655
	public const string PCCategoryName = "Category name of the performance counter object.";

	// Token: 0x04000290 RID: 656
	public const string PCCounterName = "Counter name of the performance counter object.";

	// Token: 0x04000291 RID: 657
	public const string PCInstanceName = "Instance name of the performance counter object.";

	// Token: 0x04000292 RID: 658
	public const string PCMachineName = "Specifies the machine from where to read the performance data.";

	// Token: 0x04000293 RID: 659
	public const string PCInstanceLifetime = "Specifies the lifetime of the instance.";

	// Token: 0x04000294 RID: 660
	public const string PropertyCategoryAction = "Action";

	// Token: 0x04000295 RID: 661
	public const string PropertyCategoryAppearance = "Appearance";

	// Token: 0x04000296 RID: 662
	public const string PropertyCategoryAsynchronous = "Asynchronous";

	// Token: 0x04000297 RID: 663
	public const string PropertyCategoryBehavior = "Behavior";

	// Token: 0x04000298 RID: 664
	public const string PropertyCategoryData = "Data";

	// Token: 0x04000299 RID: 665
	public const string PropertyCategoryDDE = "DDE";

	// Token: 0x0400029A RID: 666
	public const string PropertyCategoryDesign = "Design";

	// Token: 0x0400029B RID: 667
	public const string PropertyCategoryDragDrop = "Drag Drop";

	// Token: 0x0400029C RID: 668
	public const string PropertyCategoryFocus = "Focus";

	// Token: 0x0400029D RID: 669
	public const string PropertyCategoryFont = "Font";

	// Token: 0x0400029E RID: 670
	public const string PropertyCategoryFormat = "Format";

	// Token: 0x0400029F RID: 671
	public const string PropertyCategoryKey = "Key";

	// Token: 0x040002A0 RID: 672
	public const string PropertyCategoryList = "List";

	// Token: 0x040002A1 RID: 673
	public const string PropertyCategoryLayout = "Layout";

	// Token: 0x040002A2 RID: 674
	public const string PropertyCategoryDefault = "Misc";

	// Token: 0x040002A3 RID: 675
	public const string PropertyCategoryMouse = "Mouse";

	// Token: 0x040002A4 RID: 676
	public const string PropertyCategoryPosition = "Position";

	// Token: 0x040002A5 RID: 677
	public const string PropertyCategoryText = "Text";

	// Token: 0x040002A6 RID: 678
	public const string PropertyCategoryScale = "Scale";

	// Token: 0x040002A7 RID: 679
	public const string PropertyCategoryWindowStyle = "Window Style";

	// Token: 0x040002A8 RID: 680
	public const string PropertyCategoryConfig = "Configurations";

	// Token: 0x040002A9 RID: 681
	public const string ArgumentNull_ArrayWithNullElements = "The array cannot contain null elements.";

	// Token: 0x040002AA RID: 682
	public const string OnlyAllowedOnce = "This operation is only allowed once per object.";

	// Token: 0x040002AB RID: 683
	public const string BeginIndexNotNegative = "Start index cannot be less than 0 or greater than input length.";

	// Token: 0x040002AC RID: 684
	public const string LengthNotNegative = "Length cannot be less than 0 or exceed input length.";

	// Token: 0x040002AD RID: 685
	public const string UnimplementedState = "Unimplemented state.";

	// Token: 0x040002AE RID: 686
	public const string UnexpectedOpcode = "Unexpected opcode in regular expression generation: {0}.";

	// Token: 0x040002AF RID: 687
	public const string NoResultOnFailed = "Result cannot be called on a failed Match.";

	// Token: 0x040002B0 RID: 688
	public const string UnterminatedBracket = "Unterminated [] set.";

	// Token: 0x040002B1 RID: 689
	public const string TooManyParens = "Too many )'s.";

	// Token: 0x040002B2 RID: 690
	public const string NestedQuantify = "Nested quantifier {0}.";

	// Token: 0x040002B3 RID: 691
	public const string QuantifyAfterNothing = "Quantifier {x,y} following nothing.";

	// Token: 0x040002B4 RID: 692
	public const string InternalError = "Internal error in ScanRegex.";

	// Token: 0x040002B5 RID: 693
	public const string IllegalRange = "Illegal {x,y} with x > y.";

	// Token: 0x040002B6 RID: 694
	public const string NotEnoughParens = "Not enough )'s.";

	// Token: 0x040002B7 RID: 695
	public const string BadClassInCharRange = "Cannot include class \\{0} in character range.";

	// Token: 0x040002B8 RID: 696
	public const string ReversedCharRange = "[x-y] range in reverse order.";

	// Token: 0x040002B9 RID: 697
	public const string UndefinedReference = "(?({0}) ) reference to undefined group.";

	// Token: 0x040002BA RID: 698
	public const string MalformedReference = "(?({0}) ) malformed.";

	// Token: 0x040002BB RID: 699
	public const string UnrecognizedGrouping = "Unrecognized grouping construct.";

	// Token: 0x040002BC RID: 700
	public const string UnterminatedComment = "Unterminated (?#...) comment.";

	// Token: 0x040002BD RID: 701
	public const string IllegalEndEscape = "Illegal \\ at end of pattern.";

	// Token: 0x040002BE RID: 702
	public const string MalformedNameRef = "Malformed \\k<...> named back reference.";

	// Token: 0x040002BF RID: 703
	public const string UndefinedBackref = "Reference to undefined group number {0}.";

	// Token: 0x040002C0 RID: 704
	public const string UndefinedNameRef = "Reference to undefined group name {0}.";

	// Token: 0x040002C1 RID: 705
	public const string TooFewHex = "Insufficient hexadecimal digits.";

	// Token: 0x040002C2 RID: 706
	public const string MissingControl = "Missing control character.";

	// Token: 0x040002C3 RID: 707
	public const string UnrecognizedControl = "Unrecognized control character.";

	// Token: 0x040002C4 RID: 708
	public const string UnrecognizedEscape = "Unrecognized escape sequence \\{0}.";

	// Token: 0x040002C5 RID: 709
	public const string IllegalCondition = "Illegal conditional (?(...)) expression.";

	// Token: 0x040002C6 RID: 710
	public const string TooManyAlternates = "Too many | in (?()|).";

	// Token: 0x040002C7 RID: 711
	public const string MakeException = "parsing \"{0}\" - {1}";

	// Token: 0x040002C8 RID: 712
	public const string IncompleteSlashP = "Incomplete \\p{X} character escape.";

	// Token: 0x040002C9 RID: 713
	public const string MalformedSlashP = "Malformed \\p{X} character escape.";

	// Token: 0x040002CA RID: 714
	public const string InvalidGroupName = "Invalid group name: Group names must begin with a word character.";

	// Token: 0x040002CB RID: 715
	public const string CapnumNotZero = "Capture number cannot be zero.";

	// Token: 0x040002CC RID: 716
	public const string AlternationCantCapture = "Alternation conditions do not capture and cannot be named.";

	// Token: 0x040002CD RID: 717
	public const string AlternationCantHaveComment = "Alternation conditions cannot be comments.";

	// Token: 0x040002CE RID: 718
	public const string CaptureGroupOutOfRange = "Capture group numbers must be less than or equal to Int32.MaxValue.";

	// Token: 0x040002CF RID: 719
	public const string SubtractionMustBeLast = "A subtraction must be the last element in a character class.";

	// Token: 0x040002D0 RID: 720
	public const string UnknownProperty = "Unknown property '{0}'.";

	// Token: 0x040002D1 RID: 721
	public const string ReplacementError = "Replacement pattern error.";

	// Token: 0x040002D2 RID: 722
	public const string CountTooSmall = "Count cannot be less than -1.";

	// Token: 0x040002D3 RID: 723
	public const string EnumNotStarted = "Enumeration has either not started or has already finished.";

	// Token: 0x040002D4 RID: 724
	public const string Arg_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x040002D5 RID: 725
	public const string RegexMatchTimeoutException_Occurred = "The RegEx engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors.";

	// Token: 0x040002D6 RID: 726
	public const string IllegalDefaultRegexMatchTimeoutInAppDomain = "AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.";

	// Token: 0x040002D7 RID: 727
	public const string FileObject_AlreadyOpen = "The file is already open.  Call Close before trying to open the FileObject again.";

	// Token: 0x040002D8 RID: 728
	public const string FileObject_Closed = "The FileObject is currently closed.  Try opening it.";

	// Token: 0x040002D9 RID: 729
	public const string FileObject_NotWhileWriting = "File information cannot be queried while open for writing.";

	// Token: 0x040002DA RID: 730
	public const string FileObject_FileDoesNotExist = "File information cannot be queried if the file does not exist.";

	// Token: 0x040002DB RID: 731
	public const string FileObject_MustBeClosed = "This operation can only be done when the FileObject is closed.";

	// Token: 0x040002DC RID: 732
	public const string FileObject_MustBeFileName = "You must specify a file name, not a relative or absolute path.";

	// Token: 0x040002DD RID: 733
	public const string FileObject_InvalidInternalState = "FileObject's open mode wasn't set to a valid value.  This FileObject is corrupt.";

	// Token: 0x040002DE RID: 734
	public const string FileObject_PathNotSet = "The path has not been set, or is an empty string.  Please ensure you specify some path.";

	// Token: 0x040002DF RID: 735
	public const string FileObject_Reading = "The file is currently open for reading.  Close the file and reopen it before attempting this.";

	// Token: 0x040002E0 RID: 736
	public const string FileObject_Writing = "The file is currently open for writing.  Close the file and reopen it before attempting this.";

	// Token: 0x040002E1 RID: 737
	public const string FileObject_InvalidEnumeration = "Enumerator is positioned before the first line or after the last line of the file.";

	// Token: 0x040002E2 RID: 738
	public const string FileObject_NoReset = "Reset is not supported on a FileLineEnumerator.";

	// Token: 0x040002E3 RID: 739
	public const string DirectoryObject_MustBeDirName = "You must specify a directory name, not a relative or absolute path.";

	// Token: 0x040002E4 RID: 740
	public const string DirectoryObjectPathDescr = "The fully qualified, or relative path to the directory you wish to read from. E.g., \"c:\\temp\".";

	// Token: 0x040002E5 RID: 741
	public const string FileObjectDetectEncodingDescr = "Determines whether the file will be parsed to see if it has a byte order mark indicating its encoding.  If it does, this will be used rather than the current specified encoding.";

	// Token: 0x040002E6 RID: 742
	public const string FileObjectEncodingDescr = "The encoding to use when reading the file. UTF-8 is the default.";

	// Token: 0x040002E7 RID: 743
	public const string FileObjectPathDescr = "The fully qualified, or relative path to the file you wish to read from. E.g., \"myfile.txt\".";

	// Token: 0x040002E8 RID: 744
	public const string Arg_EnumIllegalVal = "Illegal enum value: {0}.";

	// Token: 0x040002E9 RID: 745
	public const string Arg_OutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x040002EA RID: 746
	public const string Argument_InvalidPermissionState = "Invalid permission state.";

	// Token: 0x040002EB RID: 747
	public const string Argument_InvalidOidValue = "The OID value was invalid.";

	// Token: 0x040002EC RID: 748
	public const string Argument_WrongType = "Operation on type '{0}' attempted with target of incorrect type.";

	// Token: 0x040002ED RID: 749
	public const string Arg_EmptyOrNullString = "String cannot be empty or null.";

	// Token: 0x040002EE RID: 750
	public const string Arg_EmptyOrNullArray = "Array cannot be empty or null.";

	// Token: 0x040002EF RID: 751
	public const string Argument_InvalidClassAttribute = "The value of \"class\" attribute is invalid.";

	// Token: 0x040002F0 RID: 752
	public const string Argument_InvalidNameType = "The value of \"nameType\" is invalid.";

	// Token: 0x040002F1 RID: 753
	public const string InvalidOperation_DuplicateItemNotAllowed = "Duplicate items are not allowed in the collection.";

	// Token: 0x040002F2 RID: 754
	public const string Cryptography_Asn_MismatchedOidInCollection = "The AsnEncodedData object does not have the same OID for the collection.";

	// Token: 0x040002F3 RID: 755
	public const string Cryptography_Cms_Envelope_Empty_Content = "Cannot create CMS enveloped for empty content.";

	// Token: 0x040002F4 RID: 756
	public const string Cryptography_Cms_Invalid_Recipient_Info_Type = "The recipient info type {0} is not valid.";

	// Token: 0x040002F5 RID: 757
	public const string Cryptography_Cms_Invalid_Subject_Identifier_Type = "The subject identifier type {0} is not valid.";

	// Token: 0x040002F6 RID: 758
	public const string Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch = "The subject identifier type {0} does not match the value data type {1}.";

	// Token: 0x040002F7 RID: 759
	public const string Cryptography_Cms_Key_Agree_Date_Not_Available = "The Date property is not available for none KID key agree recipient.";

	// Token: 0x040002F8 RID: 760
	public const string Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available = "The OtherKeyAttribute property is not available for none KID key agree recipient.";

	// Token: 0x040002F9 RID: 761
	public const string Cryptography_Cms_MessageNotSigned = "The CMS message is not signed.";

	// Token: 0x040002FA RID: 762
	public const string Cryptography_Cms_MessageNotSignedByNoSignature = "The CMS message is not signed by NoSignature.";

	// Token: 0x040002FB RID: 763
	public const string Cryptography_Cms_MessageNotEncrypted = "The CMS message is not encrypted.";

	// Token: 0x040002FC RID: 764
	public const string Cryptography_Cms_Not_Supported = "The Cryptographic Message Standard (CMS) is not supported on this platform.";

	// Token: 0x040002FD RID: 765
	public const string Cryptography_Cms_RecipientCertificateNotFound = "The recipient certificate is not specified.";

	// Token: 0x040002FE RID: 766
	public const string Cryptography_Cms_Sign_Empty_Content = "Cannot create CMS signature for empty content.";

	// Token: 0x040002FF RID: 767
	public const string Cryptography_Cms_Sign_No_Signature_First_Signer = "CmsSigner has to be the first signer with NoSignature.";

	// Token: 0x04000300 RID: 768
	public const string Cryptography_DpApi_InvalidMemoryLength = "The length of the data should be a multiple of 16 bytes.";

	// Token: 0x04000301 RID: 769
	public const string Cryptography_InvalidHandle = "{0} is an invalid handle.";

	// Token: 0x04000302 RID: 770
	public const string Cryptography_InvalidContextHandle = "The chain context handle is invalid.";

	// Token: 0x04000303 RID: 771
	public const string Cryptography_InvalidStoreHandle = "The store handle is invalid.";

	// Token: 0x04000304 RID: 772
	public const string Cryptography_Oid_InvalidValue = "The OID value is invalid.";

	// Token: 0x04000305 RID: 773
	public const string Cryptography_Pkcs9_ExplicitAddNotAllowed = "The PKCS 9 attribute cannot be explicitly added to the collection.";

	// Token: 0x04000306 RID: 774
	public const string Cryptography_Pkcs9_InvalidOid = "The OID does not represent a valid PKCS 9 attribute.";

	// Token: 0x04000307 RID: 775
	public const string Cryptography_Pkcs9_MultipleSigningTimeNotAllowed = "Cannot add multiple PKCS 9 signing time attributes.";

	// Token: 0x04000308 RID: 776
	public const string Cryptography_Pkcs9_AttributeMismatch = "The parameter should be a PKCS 9 attribute.";

	// Token: 0x04000309 RID: 777
	public const string Cryptography_X509_AddFailed = "Adding certificate with index '{0}' failed.";

	// Token: 0x0400030A RID: 778
	public const string Cryptography_X509_BadEncoding = "Input data cannot be coded as a valid certificate.";

	// Token: 0x0400030B RID: 779
	public const string Cryptography_X509_ExportFailed = "The certificate export operation failed.";

	// Token: 0x0400030C RID: 780
	public const string Cryptography_X509_ExtensionMismatch = "The parameter should be an X509Extension.";

	// Token: 0x0400030D RID: 781
	public const string Cryptography_X509_InvalidFindType = "Invalid find type.";

	// Token: 0x0400030E RID: 782
	public const string Cryptography_X509_InvalidFindValue = "Invalid find value.";

	// Token: 0x0400030F RID: 783
	public const string Cryptography_X509_InvalidEncodingFormat = "Invalid encoding format.";

	// Token: 0x04000310 RID: 784
	public const string Cryptography_X509_InvalidContentType = "Invalid content type.";

	// Token: 0x04000311 RID: 785
	public const string Cryptography_X509_KeyMismatch = "The public key of the certificate does not match the value specified.";

	// Token: 0x04000312 RID: 786
	public const string Cryptography_X509_RemoveFailed = "Removing certificate with index '{0}' failed.";

	// Token: 0x04000313 RID: 787
	public const string Cryptography_X509_StoreNotOpen = "The X509 certificate store has not been opened.";

	// Token: 0x04000314 RID: 788
	public const string Environment_NotInteractive = "The current session is not interactive.";

	// Token: 0x04000315 RID: 789
	public const string NotSupported_InvalidKeyImpl = "Only asymmetric keys that implement ICspAsymmetricAlgorithm are supported.";

	// Token: 0x04000316 RID: 790
	public const string NotSupported_KeyAlgorithm = "The certificate key algorithm is not supported.";

	// Token: 0x04000317 RID: 791
	public const string NotSupported_PlatformRequiresNT = "This operation is only supported on Windows 2000, Windows XP, and higher.";

	// Token: 0x04000318 RID: 792
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x04000319 RID: 793
	public const string Security_InvalidValue = "The {0} value was invalid.";

	// Token: 0x0400031A RID: 794
	public const string Unknown_Error = "Unknown error.";

	// Token: 0x0400031B RID: 795
	public const string security_ServiceNameCollection_EmptyServiceName = "A service name must not be null or empty.";

	// Token: 0x0400031C RID: 796
	public const string security_ExtendedProtectionPolicy_UseDifferentConstructorForNever = "To construct a policy with PolicyEnforcement.Never, the single-parameter constructor must be used.";

	// Token: 0x0400031D RID: 797
	public const string security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection = "The ServiceNameCollection must contain at least one service name.";

	// Token: 0x0400031E RID: 798
	public const string security_ExtendedProtection_NoOSSupport = "This operation requires OS support for extended protection.";

	// Token: 0x0400031F RID: 799
	public const string net_nonClsCompliantException = "A non-CLS Compliant Exception (i.e. an object that does not derive from System.Exception) was thrown.";

	// Token: 0x04000320 RID: 800
	public const string net_illegalConfigWith = "The '{0}' attribute cannot appear when '{1}' is present.";

	// Token: 0x04000321 RID: 801
	public const string net_illegalConfigWithout = "The '{0}' attribute can only appear when '{1}' is present.";

	// Token: 0x04000322 RID: 802
	public const string net_resubmitcanceled = "An error occurred on an automatic resubmission of the request.";

	// Token: 0x04000323 RID: 803
	public const string net_redirect_perm = "WebPermission demand failed for redirect URI.";

	// Token: 0x04000324 RID: 804
	public const string net_resubmitprotofailed = "Cannot handle redirect from HTTP/HTTPS protocols to other dissimilar ones.";

	// Token: 0x04000325 RID: 805
	public const string net_invalidversion = "This protocol version is not supported.";

	// Token: 0x04000326 RID: 806
	public const string net_toolong = "The size of {0} is too long. It cannot be longer than {1} characters.";

	// Token: 0x04000327 RID: 807
	public const string net_connclosed = "The underlying connection was closed: {0}.";

	// Token: 0x04000328 RID: 808
	public const string net_mutualauthfailed = "The requirement for mutual authentication was not met by the remote server.";

	// Token: 0x04000329 RID: 809
	public const string net_invasync = "Cannot block a call on this socket while an earlier asynchronous call is in progress.";

	// Token: 0x0400032A RID: 810
	public const string net_inasync = "An asynchronous call is already in progress. It must be completed or canceled before you can call this method.";

	// Token: 0x0400032B RID: 811
	public const string net_mustbeuri = "The {0} parameter must represent a valid Uri (see inner exception).";

	// Token: 0x0400032C RID: 812
	public const string net_format_shexp = "The shell expression '{0}' could not be parsed because it is formatted incorrectly.";

	// Token: 0x0400032D RID: 813
	public const string net_cannot_load_proxy_helper = "Failed to load the proxy script runtime environment from the Microsoft.JScript assembly.";

	// Token: 0x0400032E RID: 814
	public const string net_io_no_0timeouts = "NetworkStream does not support a 0 millisecond timeout, use a value greater than zero for the timeout instead.";

	// Token: 0x0400032F RID: 815
	public const string net_tooManyRedirections = "Too many automatic redirections were attempted.";

	// Token: 0x04000330 RID: 816
	public const string net_authmodulenotregistered = "The supplied authentication module is not registered.";

	// Token: 0x04000331 RID: 817
	public const string net_authschemenotregistered = "There is no registered module for this authentication scheme.";

	// Token: 0x04000332 RID: 818
	public const string net_proxyschemenotsupported = "The ServicePointManager does not support proxies with the {0} scheme.";

	// Token: 0x04000333 RID: 819
	public const string net_maxsrvpoints = "The maximum number of service points was exceeded.";

	// Token: 0x04000334 RID: 820
	public const string net_notconnected = "The operation is not allowed on non-connected sockets.";

	// Token: 0x04000335 RID: 821
	public const string net_notstream = "The operation is not allowed on non-stream oriented sockets.";

	// Token: 0x04000336 RID: 822
	public const string net_nocontentlengthonget = "Content-Length or Chunked Encoding cannot be set for an operation that does not write data.";

	// Token: 0x04000337 RID: 823
	public const string net_contentlengthmissing = "When performing a write operation with AllowWriteStreamBuffering set to false, you must either set ContentLength to a non-negative number or set SendChunked to true.";

	// Token: 0x04000338 RID: 824
	public const string net_nonhttpproxynotallowed = "The URI scheme for the supplied IWebProxy has the illegal value '{0}'. Only 'http' is supported.";

	// Token: 0x04000339 RID: 825
	public const string net_need_writebuffering = "This request requires buffering data to succeed.";

	// Token: 0x0400033A RID: 826
	public const string net_nodefaultcreds = "Default credentials cannot be supplied for the {0} authentication scheme.";

	// Token: 0x0400033B RID: 827
	public const string net_stopped = "Not listening. You must call the Start() method before calling this method.";

	// Token: 0x0400033C RID: 828
	public const string net_udpconnected = "Cannot send packets to an arbitrary host while connected.";

	// Token: 0x0400033D RID: 829
	public const string net_no_concurrent_io_allowed = "The stream does not support concurrent IO read or write operations.";

	// Token: 0x0400033E RID: 830
	public const string net_needmorethreads = "There were not enough free threads in the ThreadPool to complete the operation.";

	// Token: 0x0400033F RID: 831
	public const string net_MethodNotSupportedException = "This method is not supported by this class.";

	// Token: 0x04000340 RID: 832
	public const string net_ProtocolNotSupportedException = "The '{0}' protocol is not supported by this class.";

	// Token: 0x04000341 RID: 833
	public const string net_SelectModeNotSupportedException = "The '{0}' select mode is not supported by this class.";

	// Token: 0x04000342 RID: 834
	public const string net_InvalidSocketHandle = "The socket handle is not valid.";

	// Token: 0x04000343 RID: 835
	public const string net_InvalidAddressFamily = "The AddressFamily {0} is not valid for the {1} end point, use {2} instead.";

	// Token: 0x04000344 RID: 836
	public const string net_InvalidEndPointAddressFamily = "The supplied EndPoint of AddressFamily {0} is not valid for this Socket, use {1} instead.";

	// Token: 0x04000345 RID: 837
	public const string net_InvalidSocketAddressSize = "The supplied {0} is an invalid size for the {1} end point.";

	// Token: 0x04000346 RID: 838
	public const string net_invalidAddressList = "None of the discovered or specified addresses match the socket address family.";

	// Token: 0x04000347 RID: 839
	public const string net_invalidPingBufferSize = "The buffer length must not exceed 65500 bytes.";

	// Token: 0x04000348 RID: 840
	public const string net_cant_perform_during_shutdown = "This operation cannot be performed while the AppDomain is shutting down.";

	// Token: 0x04000349 RID: 841
	public const string net_cant_create_environment = "Unable to create another web proxy script environment at this time.";

	// Token: 0x0400034A RID: 842
	public const string net_protocol_invalid_family = "'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.";

	// Token: 0x0400034B RID: 843
	public const string net_protocol_invalid_multicast_family = "Multicast family is not the same as the family of the '{0}' Client.";

	// Token: 0x0400034C RID: 844
	public const string net_empty_osinstalltype = "The Registry value '{0}' was either empty or not a string type.";

	// Token: 0x0400034D RID: 845
	public const string net_unknown_osinstalltype = "Unknown Windows installation type '{0}'.";

	// Token: 0x0400034E RID: 846
	public const string net_cant_determine_osinstalltype = "Can't determine OS installation type: Can't read key '{0}'. Exception message: {1}";

	// Token: 0x0400034F RID: 847
	public const string net_osinstalltype = "Current OS installation type is '{0}'.";

	// Token: 0x04000350 RID: 848
	public const string net_entire_body_not_written = "You must write ContentLength bytes to the request stream before calling [Begin]GetResponse.";

	// Token: 0x04000351 RID: 849
	public const string net_must_provide_request_body = "You must provide a request body if you set ContentLength>0 or SendChunked==true.  Do this by calling [Begin]GetRequestStream before [Begin]GetResponse.";

	// Token: 0x04000352 RID: 850
	public const string net_sockets_zerolist = "The parameter {0} must contain one or more elements.";

	// Token: 0x04000353 RID: 851
	public const string net_sockets_blocking = "The operation is not allowed on a non-blocking Socket.";

	// Token: 0x04000354 RID: 852
	public const string net_sockets_useblocking = "Use the Blocking property to change the status of the Socket.";

	// Token: 0x04000355 RID: 853
	public const string net_sockets_select = "The operation is not allowed on objects of type {0}. Use only objects of type {1}.";

	// Token: 0x04000356 RID: 854
	public const string net_sockets_toolarge_select = "The {0} list contains too many items; a maximum of {1} is allowed.";

	// Token: 0x04000357 RID: 855
	public const string net_sockets_empty_select = "All lists are either null or empty.";

	// Token: 0x04000358 RID: 856
	public const string net_sockets_mustbind = "You must call the Bind method before performing this operation.";

	// Token: 0x04000359 RID: 857
	public const string net_sockets_mustlisten = "You must call the Listen method before performing this operation.";

	// Token: 0x0400035A RID: 858
	public const string net_sockets_mustnotlisten = "You may not perform this operation after calling the Listen method.";

	// Token: 0x0400035B RID: 859
	public const string net_sockets_mustnotbebound = "The socket must not be bound or connected.";

	// Token: 0x0400035C RID: 860
	public const string net_sockets_namedmustnotbebound = "{0}: The socket must not be bound or connected.";

	// Token: 0x0400035D RID: 861
	public const string net_sockets_invalid_socketinformation = "The specified value for the socket information is invalid.";

	// Token: 0x0400035E RID: 862
	public const string net_sockets_invalid_ipaddress_length = "The number of specified IP addresses has to be greater than 0.";

	// Token: 0x0400035F RID: 863
	public const string net_sockets_invalid_optionValue = "The specified value is not a valid '{0}'.";

	// Token: 0x04000360 RID: 864
	public const string net_sockets_invalid_optionValue_all = "The specified value is not valid.";

	// Token: 0x04000361 RID: 865
	public const string net_sockets_invalid_dnsendpoint = "The parameter {0} must not be of type DnsEndPoint.";

	// Token: 0x04000362 RID: 866
	public const string net_sockets_disconnectedConnect = "Once the socket has been disconnected, you can only reconnect again asynchronously, and only to a different EndPoint.  BeginConnect must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x04000363 RID: 867
	public const string net_sockets_disconnectedAccept = "Once the socket has been disconnected, you can only accept again asynchronously.  BeginAccept must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x04000364 RID: 868
	public const string net_tcplistener_mustbestopped = "The TcpListener must not be listening before performing this operation.";

	// Token: 0x04000365 RID: 869
	public const string net_sockets_no_duplicate_async = "BeginConnect cannot be called while another asynchronous operation is in progress on the same Socket.";

	// Token: 0x04000366 RID: 870
	public const string net_socketopinprogress = "An asynchronous socket operation is already in progress using this SocketAsyncEventArgs instance.";

	// Token: 0x04000367 RID: 871
	public const string net_buffercounttoosmall = "The Buffer space specified by the Count property is insufficient for the AcceptAsync method.";

	// Token: 0x04000368 RID: 872
	public const string net_multibuffernotsupported = "Multiple buffers cannot be used with this method.";

	// Token: 0x04000369 RID: 873
	public const string net_ambiguousbuffers = "Buffer and BufferList properties cannot both be non-null.";

	// Token: 0x0400036A RID: 874
	public const string net_sockets_ipv6only = "This operation is only valid for IPv6 Sockets.";

	// Token: 0x0400036B RID: 875
	public const string net_perfcounter_initialized_success = "System.Net performance counters initialization completed successful.";

	// Token: 0x0400036C RID: 876
	public const string net_perfcounter_initialized_error = "System.Net performance counters initialization completed with errors. See System.Net trace file for more information.";

	// Token: 0x0400036D RID: 877
	public const string net_perfcounter_nocategory = "Performance counter category '{0}' doesn't exist. No System.Net performance counter values available.";

	// Token: 0x0400036E RID: 878
	public const string net_perfcounter_initialization_started = "System.Net performance counter initialization started.";

	// Token: 0x0400036F RID: 879
	public const string net_perfcounter_cant_queue_workitem = "Can't queue counter initialization logic on a thread pool thread. System.Net performance counters will not be available.";

	// Token: 0x04000370 RID: 880
	public const string net_config_proxy = "Error creating the Web Proxy specified in the 'system.net/defaultProxy' configuration section.";

	// Token: 0x04000371 RID: 881
	public const string net_config_proxy_module_not_public = "The specified proxy module type is not public.";

	// Token: 0x04000372 RID: 882
	public const string net_config_authenticationmodules = "Error creating the modules specified in the 'system.net/authenticationModules' configuration section.";

	// Token: 0x04000373 RID: 883
	public const string net_config_webrequestmodules = "Error creating the modules specified in the 'system.net/webRequestModules' configuration section.";

	// Token: 0x04000374 RID: 884
	public const string net_config_requestcaching = "Error creating the Web Request caching policy specified in the 'system.net/requestCaching' configuration section.";

	// Token: 0x04000375 RID: 885
	public const string net_config_section_permission = "Insufficient permissions for setting the configuration section '{0}'.";

	// Token: 0x04000376 RID: 886
	public const string net_config_element_permission = "Insufficient permissions for setting the configuration element '{0}'.";

	// Token: 0x04000377 RID: 887
	public const string net_config_property_permission = "Insufficient permissions for setting the configuration property '{0}'.";

	// Token: 0x04000378 RID: 888
	public const string net_WebResponseParseError_InvalidHeaderName = "Header name is invalid";

	// Token: 0x04000379 RID: 889
	public const string net_WebResponseParseError_InvalidContentLength = "'Content-Length' header value is invalid";

	// Token: 0x0400037A RID: 890
	public const string net_WebResponseParseError_IncompleteHeaderLine = "Invalid header name";

	// Token: 0x0400037B RID: 891
	public const string net_WebResponseParseError_CrLfError = "CR must be followed by LF";

	// Token: 0x0400037C RID: 892
	public const string net_WebResponseParseError_InvalidChunkFormat = "Response chunk format is invalid";

	// Token: 0x0400037D RID: 893
	public const string net_WebResponseParseError_UnexpectedServerResponse = "Unexpected server response received";

	// Token: 0x0400037E RID: 894
	public const string net_webstatus_Success = "Status success";

	// Token: 0x0400037F RID: 895
	public const string net_webstatus_ReceiveFailure = "An unexpected error occurred on a receive";

	// Token: 0x04000380 RID: 896
	public const string net_webstatus_SendFailure = "An unexpected error occurred on a send";

	// Token: 0x04000381 RID: 897
	public const string net_webstatus_PipelineFailure = "A pipeline failure occurred";

	// Token: 0x04000382 RID: 898
	public const string net_webstatus_RequestCanceled = "The request was canceled";

	// Token: 0x04000383 RID: 899
	public const string net_webstatus_ConnectionClosed = "The connection was closed unexpectedly";

	// Token: 0x04000384 RID: 900
	public const string net_webstatus_TrustFailure = "Could not establish trust relationship for the SSL/TLS secure channel";

	// Token: 0x04000385 RID: 901
	public const string net_webstatus_SecureChannelFailure = "Could not create SSL/TLS secure channel";

	// Token: 0x04000386 RID: 902
	public const string net_webstatus_ServerProtocolViolation = "The server committed a protocol violation";

	// Token: 0x04000387 RID: 903
	public const string net_webstatus_KeepAliveFailure = "A connection that was expected to be kept alive was closed by the server";

	// Token: 0x04000388 RID: 904
	public const string net_webstatus_ProxyNameResolutionFailure = "The proxy name could not be resolved";

	// Token: 0x04000389 RID: 905
	public const string net_webstatus_MessageLengthLimitExceeded = "The message length limit was exceeded";

	// Token: 0x0400038A RID: 906
	public const string net_webstatus_CacheEntryNotFound = "The request cache-only policy does not allow a network request and the response is not found in cache";

	// Token: 0x0400038B RID: 907
	public const string net_webstatus_RequestProhibitedByCachePolicy = "The request could not be satisfied using a cache-only policy";

	// Token: 0x0400038C RID: 908
	public const string net_webstatus_RequestProhibitedByProxy = "The IWebProxy object associated with the request did not allow the request to proceed";

	// Token: 0x0400038D RID: 909
	public const string net_httpstatuscode_NoContent = "No Content";

	// Token: 0x0400038E RID: 910
	public const string net_httpstatuscode_NonAuthoritativeInformation = "Non Authoritative Information";

	// Token: 0x0400038F RID: 911
	public const string net_httpstatuscode_ResetContent = "Reset Content";

	// Token: 0x04000390 RID: 912
	public const string net_httpstatuscode_PartialContent = "Partial Content";

	// Token: 0x04000391 RID: 913
	public const string net_httpstatuscode_MultipleChoices = "Multiple Choices Redirect";

	// Token: 0x04000392 RID: 914
	public const string net_httpstatuscode_Ambiguous = "Ambiguous Redirect";

	// Token: 0x04000393 RID: 915
	public const string net_httpstatuscode_MovedPermanently = "Moved Permanently Redirect";

	// Token: 0x04000394 RID: 916
	public const string net_httpstatuscode_Moved = "Moved Redirect";

	// Token: 0x04000395 RID: 917
	public const string net_httpstatuscode_Found = "Found Redirect";

	// Token: 0x04000396 RID: 918
	public const string net_httpstatuscode_Redirect = "Redirect";

	// Token: 0x04000397 RID: 919
	public const string net_httpstatuscode_SeeOther = "See Other";

	// Token: 0x04000398 RID: 920
	public const string net_httpstatuscode_RedirectMethod = "Redirect Method";

	// Token: 0x04000399 RID: 921
	public const string net_httpstatuscode_NotModified = "Not Modified";

	// Token: 0x0400039A RID: 922
	public const string net_httpstatuscode_UseProxy = "Use Proxy Redirect";

	// Token: 0x0400039B RID: 923
	public const string net_httpstatuscode_TemporaryRedirect = "Temporary Redirect";

	// Token: 0x0400039C RID: 924
	public const string net_httpstatuscode_RedirectKeepVerb = "Redirect Keep Verb";

	// Token: 0x0400039D RID: 925
	public const string net_httpstatuscode_BadRequest = "Bad Request";

	// Token: 0x0400039E RID: 926
	public const string net_httpstatuscode_Unauthorized = "Unauthorized";

	// Token: 0x0400039F RID: 927
	public const string net_httpstatuscode_PaymentRequired = "Payment Required";

	// Token: 0x040003A0 RID: 928
	public const string net_httpstatuscode_Forbidden = "Forbidden";

	// Token: 0x040003A1 RID: 929
	public const string net_httpstatuscode_NotFound = "Not Found";

	// Token: 0x040003A2 RID: 930
	public const string net_httpstatuscode_MethodNotAllowed = "Method Not Allowed";

	// Token: 0x040003A3 RID: 931
	public const string net_httpstatuscode_NotAcceptable = "Not Acceptable";

	// Token: 0x040003A4 RID: 932
	public const string net_httpstatuscode_ProxyAuthenticationRequired = "Proxy Authentication Required";

	// Token: 0x040003A5 RID: 933
	public const string net_httpstatuscode_RequestTimeout = "Request Timeout";

	// Token: 0x040003A6 RID: 934
	public const string net_httpstatuscode_Conflict = "Conflict";

	// Token: 0x040003A7 RID: 935
	public const string net_httpstatuscode_Gone = "Gone";

	// Token: 0x040003A8 RID: 936
	public const string net_httpstatuscode_LengthRequired = "Length Required";

	// Token: 0x040003A9 RID: 937
	public const string net_httpstatuscode_InternalServerError = "Internal Server Error";

	// Token: 0x040003AA RID: 938
	public const string net_httpstatuscode_NotImplemented = "Not Implemented";

	// Token: 0x040003AB RID: 939
	public const string net_httpstatuscode_BadGateway = "Bad Gateway";

	// Token: 0x040003AC RID: 940
	public const string net_httpstatuscode_ServiceUnavailable = "Server Unavailable";

	// Token: 0x040003AD RID: 941
	public const string net_httpstatuscode_GatewayTimeout = "Gateway Timeout";

	// Token: 0x040003AE RID: 942
	public const string net_httpstatuscode_HttpVersionNotSupported = "Http Version Not Supported";

	// Token: 0x040003AF RID: 943
	public const string net_emptystringset = "This property cannot be set to an empty string.";

	// Token: 0x040003B0 RID: 944
	public const string net_emptystringcall = "The parameter '{0}' cannot be an empty string.";

	// Token: 0x040003B1 RID: 945
	public const string net_headers_req = "This collection holds response headers and cannot contain the specified request header.";

	// Token: 0x040003B2 RID: 946
	public const string net_headers_rsp = "This collection holds request headers and cannot contain the specified response header.";

	// Token: 0x040003B3 RID: 947
	public const string net_headers_toolong = "Header values cannot be longer than {0} characters.";

	// Token: 0x040003B4 RID: 948
	public const string net_WebHeaderInvalidNonAsciiChars = "Specified value has invalid non-ASCII characters.";

	// Token: 0x040003B5 RID: 949
	public const string net_WebHeaderMissingColon = "Specified value does not have a ':' separator.";

	// Token: 0x040003B6 RID: 950
	public const string net_headerrestrict = "The '{0}' header must be modified using the appropriate property or method.";

	// Token: 0x040003B7 RID: 951
	public const string net_io_completionportwasbound = "The socket has already been bound to an io completion port.";

	// Token: 0x040003B8 RID: 952
	public const string net_io_writefailure = "Unable to write data to the transport connection: {0}.";

	// Token: 0x040003B9 RID: 953
	public const string net_io_readfailure = "Unable to read data from the transport connection: {0}.";

	// Token: 0x040003BA RID: 954
	public const string net_io_connectionclosed = "The connection was closed";

	// Token: 0x040003BB RID: 955
	public const string net_io_transportfailure = "Unable to create a transport connection.";

	// Token: 0x040003BC RID: 956
	public const string net_io_internal_bind = "Internal Error: A socket handle could not be bound to a completion port.";

	// Token: 0x040003BD RID: 957
	public const string net_io_invalidnestedcall = "The {0} method cannot be called when another {1} operation is pending.";

	// Token: 0x040003BE RID: 958
	public const string net_io_must_be_rw_stream = "The stream has to be read/write.";

	// Token: 0x040003BF RID: 959
	public const string net_io_header_id = "Found a wrong header field {0} read = {1}, expected = {2}.";

	// Token: 0x040003C0 RID: 960
	public const string net_io_out_range = "The byte count must not exceed {0} bytes for this stream type.";

	// Token: 0x040003C1 RID: 961
	public const string net_io_encrypt = "The encryption operation failed, see inner exception.";

	// Token: 0x040003C2 RID: 962
	public const string net_io_decrypt = "The decryption operation failed, see inner exception.";

	// Token: 0x040003C3 RID: 963
	public const string net_io_read = "The read operation failed, see inner exception.";

	// Token: 0x040003C4 RID: 964
	public const string net_io_write = "The write operation failed, see inner exception.";

	// Token: 0x040003C5 RID: 965
	public const string net_io_eof = "Received an unexpected EOF or 0 bytes from the transport stream.";

	// Token: 0x040003C6 RID: 966
	public const string net_io_async_result = "The parameter: {0} is not valid. Use the object returned from corresponding Begin async call.";

	// Token: 0x040003C7 RID: 967
	public const string net_tls_version = "The SSL version is not supported.";

	// Token: 0x040003C8 RID: 968
	public const string net_perm_target = "Cannot cast target permission type.";

	// Token: 0x040003C9 RID: 969
	public const string net_perm_both_regex = "Cannot subset Regex. Only support if both patterns are identical.";

	// Token: 0x040003CA RID: 970
	public const string net_perm_none = "There are no permissions to check.";

	// Token: 0x040003CB RID: 971
	public const string net_perm_attrib_count = "The value for '{0}' must be specified.";

	// Token: 0x040003CC RID: 972
	public const string net_perm_invalid_val = "The parameter value '{0}={1}' is invalid.";

	// Token: 0x040003CD RID: 973
	public const string net_perm_attrib_multi = "The permission '{0}={1}' cannot be added. Add a separate Attribute statement.";

	// Token: 0x040003CE RID: 974
	public const string net_perm_epname = "The argument value '{0}' is invalid for creating a SocketPermission object.";

	// Token: 0x040003CF RID: 975
	public const string net_perm_invalid_val_in_element = "The '{0}' element contains one or more invalid values.";

	// Token: 0x040003D0 RID: 976
	public const string net_invalid_ip_addr = "IPv4 address 0.0.0.0 and IPv6 address ::0 are unspecified addresses that cannot be used as a target address.";

	// Token: 0x040003D1 RID: 977
	public const string dns_bad_ip_address = "An invalid IP address was specified.";

	// Token: 0x040003D2 RID: 978
	public const string net_bad_mac_address = "An invalid physical address was specified.";

	// Token: 0x040003D3 RID: 979
	public const string net_ping = "An exception occurred during a Ping request.";

	// Token: 0x040003D4 RID: 980
	public const string net_bad_ip_address_prefix = "An invalid IP address prefix was specified.";

	// Token: 0x040003D5 RID: 981
	public const string net_max_ip_address_list_length_exceeded = "Too many addresses to sort. The maximum number of addresses allowed are {0}.";

	// Token: 0x040003D6 RID: 982
	public const string net_ipv4_not_installed = "IPv4 is not installed.";

	// Token: 0x040003D7 RID: 983
	public const string net_ipv6_not_installed = "IPv6 is not installed.";

	// Token: 0x040003D8 RID: 984
	public const string net_webclient = "An exception occurred during a WebClient request.";

	// Token: 0x040003D9 RID: 985
	public const string net_webclient_ContentType = "The Content-Type header cannot be changed from its default value for this request.";

	// Token: 0x040003DA RID: 986
	public const string net_webclient_Multipart = "The Content-Type header cannot be set to a multipart type for this request.";

	// Token: 0x040003DB RID: 987
	public const string net_webclient_no_concurrent_io_allowed = "WebClient does not support concurrent I/O operations.";

	// Token: 0x040003DC RID: 988
	public const string net_webclient_invalid_baseaddress = "The specified value is not a valid base address.";

	// Token: 0x040003DD RID: 989
	public const string net_container_add_cookie = "An error occurred when adding a cookie to the container.";

	// Token: 0x040003DE RID: 990
	public const string net_cookie_invalid = "Invalid contents for cookie = '{0}'.";

	// Token: 0x040003DF RID: 991
	public const string net_cookie_size = "The value size of the cookie is '{0}'. This exceeds the configured maximum size, which is '{1}'.";

	// Token: 0x040003E0 RID: 992
	public const string net_cookie_parse_header = "An error occurred when parsing the Cookie header for Uri '{0}'.";

	// Token: 0x040003E1 RID: 993
	public const string net_cookie_attribute = "The '{0}'='{1}' part of the cookie is invalid.";

	// Token: 0x040003E2 RID: 994
	public const string net_cookie_format = "Cookie format error.";

	// Token: 0x040003E3 RID: 995
	public const string net_cookie_capacity_range = "'{0}' has to be greater than '{1}' and less than '{2}'.";

	// Token: 0x040003E4 RID: 996
	public const string net_set_token = "Failed to impersonate a thread doing authentication of a Web Request.";

	// Token: 0x040003E5 RID: 997
	public const string net_revert_token = "Failed to revert the thread token after authenticating a Web Request.";

	// Token: 0x040003E6 RID: 998
	public const string net_ssl_io_async_context = "Async context creation failed.";

	// Token: 0x040003E7 RID: 999
	public const string net_ssl_io_encrypt = "The encryption operation failed, see inner exception.";

	// Token: 0x040003E8 RID: 1000
	public const string net_ssl_io_decrypt = "The decryption operation failed, see inner exception.";

	// Token: 0x040003E9 RID: 1001
	public const string net_ssl_io_context_expired = "The security context has expired.";

	// Token: 0x040003EA RID: 1002
	public const string net_ssl_io_handshake_start = "The handshake failed. The remote side has dropped the stream.";

	// Token: 0x040003EB RID: 1003
	public const string net_ssl_io_handshake = "The handshake failed, see inner exception.";

	// Token: 0x040003EC RID: 1004
	public const string net_ssl_io_frame = "The handshake failed due to an unexpected packet format.";

	// Token: 0x040003ED RID: 1005
	public const string net_ssl_io_corrupted = "The stream is corrupted due to an invalid SSL version number in the SSL protocol header.";

	// Token: 0x040003EE RID: 1006
	public const string net_ssl_io_cert_validation = "The remote certificate is invalid according to the validation procedure.";

	// Token: 0x040003EF RID: 1007
	public const string net_ssl_io_invalid_end_call = "{0} can only be called once for each asynchronous operation.";

	// Token: 0x040003F0 RID: 1008
	public const string net_ssl_io_invalid_begin_call = "{0} cannot be called when another {1} operation is pending.";

	// Token: 0x040003F1 RID: 1009
	public const string net_ssl_io_no_server_cert = "The server mode SSL must use a certificate with the associated private key.";

	// Token: 0x040003F2 RID: 1010
	public const string net_auth_bad_client_creds = "The server has rejected the client credentials.";

	// Token: 0x040003F3 RID: 1011
	public const string net_auth_bad_client_creds_or_target_mismatch = "Either the target name is incorrect or the server has rejected the client credentials.";

	// Token: 0x040003F4 RID: 1012
	public const string net_auth_context_expectation = "A security requirement was not fulfilled during authentication. Required: {0}, negotiated: {1}.";

	// Token: 0x040003F5 RID: 1013
	public const string net_auth_context_expectation_remote = "A remote side security requirement was not fulfilled during authentication. Try increasing the ProtectionLevel and/or ImpersonationLevel.";

	// Token: 0x040003F6 RID: 1014
	public const string net_auth_supported_impl_levels = "The supported values are Identification, Impersonation or Delegation.";

	// Token: 0x040003F7 RID: 1015
	public const string net_auth_no_anonymous_support = "The TokenImpersonationLevel.Anonymous level is not supported for authentication.";

	// Token: 0x040003F8 RID: 1016
	public const string net_auth_reauth = "This operation is not allowed on a security context that has already been authenticated.";

	// Token: 0x040003F9 RID: 1017
	public const string net_auth_noauth = "This operation is only allowed using a successfully authenticated context.";

	// Token: 0x040003FA RID: 1018
	public const string net_auth_client_server = "Once authentication is attempted as the client or server, additional authentication attempts must use the same client or server role.";

	// Token: 0x040003FB RID: 1019
	public const string net_auth_noencryption = "This authenticated context does not support data encryption.";

	// Token: 0x040003FC RID: 1020
	public const string net_auth_SSPI = "Authentication failed, see inner exception.";

	// Token: 0x040003FD RID: 1021
	public const string net_auth_failure = "Authentication failed, see inner exception.";

	// Token: 0x040003FE RID: 1022
	public const string net_auth_eof = "Authentication failed because the remote party has closed the transport stream.";

	// Token: 0x040003FF RID: 1023
	public const string net_auth_alert = "Authentication failed on the remote side (the stream might still be available for additional authentication attempts).";

	// Token: 0x04000400 RID: 1024
	public const string net_auth_ignored_reauth = "Re-authentication failed because the remote party continued to encrypt more than {0} bytes before answering re-authentication.";

	// Token: 0x04000401 RID: 1025
	public const string net_auth_empty_read = "Protocol error: cannot proceed with SSPI handshake because an empty blob was received.";

	// Token: 0x04000402 RID: 1026
	public const string net_auth_must_specify_extended_protection_scheme = "An extended protection policy must specify either a custom channel binding or a custom service name collection.";

	// Token: 0x04000403 RID: 1027
	public const string net_frame_size = "Received an invalid authentication frame. The message size is limited to {0} bytes, attempted to read {1} bytes.";

	// Token: 0x04000404 RID: 1028
	public const string net_frame_read_io = "Received incomplete authentication message. Remote party has probably closed the connection.";

	// Token: 0x04000405 RID: 1029
	public const string net_frame_read_size = "Cannot determine the frame size or a corrupted frame was received.";

	// Token: 0x04000406 RID: 1030
	public const string net_frame_max_size = "The payload size is limited to {0}, attempted set it to {1}.";

	// Token: 0x04000407 RID: 1031
	public const string net_jscript_load = "The proxy JScript file threw an exception while being initialized: {0}.";

	// Token: 0x04000408 RID: 1032
	public const string net_proxy_not_gmt = "The specified value is not a valid GMT time.";

	// Token: 0x04000409 RID: 1033
	public const string net_proxy_invalid_dayofweek = "The specified value is not a valid day of the week.";

	// Token: 0x0400040A RID: 1034
	public const string net_proxy_invalid_url_format = "The system proxy settings contain an invalid proxy server setting: '{0}'.";

	// Token: 0x0400040B RID: 1035
	public const string net_param_not_string = "Argument must be a string instead of {0}.";

	// Token: 0x0400040C RID: 1036
	public const string net_value_cannot_be_negative = "The specified value cannot be negative.";

	// Token: 0x0400040D RID: 1037
	public const string net_invalid_offset = "Value of offset cannot be negative or greater than the length of the buffer.";

	// Token: 0x0400040E RID: 1038
	public const string net_offset_plus_count = "Sum of offset and count cannot be greater than the length of the buffer.";

	// Token: 0x0400040F RID: 1039
	public const string net_cannot_be_false = "The specified value cannot be false.";

	// Token: 0x04000410 RID: 1040
	public const string net_cache_shadowstream_not_writable = "Shadow stream must be writable.";

	// Token: 0x04000411 RID: 1041
	public const string net_cache_validator_fail = "The validation method {0}() returned a failure for this request.";

	// Token: 0x04000412 RID: 1042
	public const string net_cache_access_denied = "For this RequestCache object, {0} access is denied.";

	// Token: 0x04000413 RID: 1043
	public const string net_cache_validator_result = "The validation method {0}() returned the unexpected status: {1}.";

	// Token: 0x04000414 RID: 1044
	public const string net_cache_retrieve_failure = "Cache retrieve failed: {0}.";

	// Token: 0x04000415 RID: 1045
	public const string net_cache_not_supported_body = "The cached response is not supported for a request with a content body.";

	// Token: 0x04000416 RID: 1046
	public const string net_cache_not_supported_command = "The cached response is not supported for a request with the specified request method.";

	// Token: 0x04000417 RID: 1047
	public const string net_cache_not_accept_response = "The cache protocol refused the server response. To allow automatic request retrying, set request.AllowAutoRedirect to true.";

	// Token: 0x04000418 RID: 1048
	public const string net_cache_method_failed = "The request (Method = {0}) cannot be served from the cache and will fail because of the effective CachePolicy: {1}.";

	// Token: 0x04000419 RID: 1049
	public const string net_cache_key_failed = "The request failed because no cache entry (CacheKey = {0}) was found and the effective CachePolicy is {1}.";

	// Token: 0x0400041A RID: 1050
	public const string net_cache_no_stream = "The cache protocol returned a cached response but the cache entry is invalid because it has a null stream. (Cache Key = {0}).";

	// Token: 0x0400041B RID: 1051
	public const string net_cache_unsupported_partial_stream = "A partial content stream does not support this operation or some method argument is out of range.";

	// Token: 0x0400041C RID: 1052
	public const string net_cache_not_configured = "No cache protocol is available for this request.";

	// Token: 0x0400041D RID: 1053
	public const string net_cache_non_seekable_stream_not_supported = "The transport stream instance passed in the RangeStream constructor is not seekable and therefore is not supported.";

	// Token: 0x0400041E RID: 1054
	public const string net_invalid_cast = "Invalid cast from {0} to {1}.";

	// Token: 0x0400041F RID: 1055
	public const string net_collection_readonly = "The collection is read-only.";

	// Token: 0x04000420 RID: 1056
	public const string net_not_ipermission = "Specified value does not contain 'IPermission' as its tag.";

	// Token: 0x04000421 RID: 1057
	public const string net_no_classname = "Specified value does not contain a 'class' attribute.";

	// Token: 0x04000422 RID: 1058
	public const string net_no_typename = "The value class attribute is not valid.";

	// Token: 0x04000423 RID: 1059
	public const string net_servicePointAddressNotSupportedInHostMode = "This property is not supported for protocols that do not use URI.";

	// Token: 0x04000424 RID: 1060
	public const string net_Websockets_WebSocketBaseFaulted = "An exception caused the WebSocket to enter the Aborted state. Please see the InnerException, if present, for more details.";

	// Token: 0x04000425 RID: 1061
	public const string net_WebSockets_Generic = "An internal WebSocket error occurred. Please see the innerException, if present, for more details.";

	// Token: 0x04000426 RID: 1062
	public const string net_WebSockets_NotAWebSocket_Generic = "A WebSocket operation was called on a request or response that is not a WebSocket.";

	// Token: 0x04000427 RID: 1063
	public const string net_WebSockets_UnsupportedWebSocketVersion_Generic = "Unsupported WebSocket version.";

	// Token: 0x04000428 RID: 1064
	public const string net_WebSockets_HeaderError_Generic = "The WebSocket request or response contained unsupported header(s).";

	// Token: 0x04000429 RID: 1065
	public const string net_WebSockets_UnsupportedProtocol_Generic = "The WebSocket request or response operation was called with unsupported protocol(s).";

	// Token: 0x0400042A RID: 1066
	public const string net_WebSockets_ClientSecWebSocketProtocolsBlank = "The WebSocket client sent a blank '{0}' header; this is not allowed by the WebSocket protocol specification. The client should omit the header if the client is not negotiating any sub-protocols.";

	// Token: 0x0400042B RID: 1067
	public const string net_WebSockets_InvalidState_Generic = "The WebSocket instance cannot be used for communication because it has been transitioned into an invalid state.";

	// Token: 0x0400042C RID: 1068
	public const string net_WebSockets_InvalidMessageType_Generic = "The received  message type is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x0400042D RID: 1069
	public const string net_WebSockets_ConnectionClosedPrematurely_Generic = "The remote party closed the WebSocket connection without completing the close handshake.";

	// Token: 0x0400042E RID: 1070
	public const string net_WebSockets_Scheme = "Only Uris starting with 'ws://' or 'wss://' are supported.";

	// Token: 0x0400042F RID: 1071
	public const string net_WebSockets_AlreadyStarted = "The WebSocket has already been started.";

	// Token: 0x04000430 RID: 1072
	public const string net_WebSockets_Connect101Expected = "The server returned status code '{0}' when status code '101' was expected.";

	// Token: 0x04000431 RID: 1073
	public const string net_WebSockets_InvalidResponseHeader = "The '{0}' header value '{1}' is invalid.";

	// Token: 0x04000432 RID: 1074
	public const string net_WebSockets_NotConnected = "The WebSocket is not connected.";

	// Token: 0x04000433 RID: 1075
	public const string net_WebSockets_InvalidRegistration = "The WebSocket schemes must be registered with the HttpWebRequest class.";

	// Token: 0x04000434 RID: 1076
	public const string net_WebSockets_NoDuplicateProtocol = "Duplicate protocols are not allowed: '{0}'.";

	// Token: 0x04000435 RID: 1077
	public const string net_log_exception = "Exception in {0}::{1} - {2}.";

	// Token: 0x04000436 RID: 1078
	public const string net_log_sspi_enumerating_security_packages = "Enumerating security packages:";

	// Token: 0x04000437 RID: 1079
	public const string net_log_sspi_security_package_not_found = "Security package '{0}' was not found.";

	// Token: 0x04000438 RID: 1080
	public const string net_log_sspi_security_context_input_buffer = "{0}(In-Buffer length={1}, Out-Buffer length={2}, returned code={3}).";

	// Token: 0x04000439 RID: 1081
	public const string net_log_sspi_security_context_input_buffers = "{0}(In-Buffers count={1}, Out-Buffer length={2}, returned code={3}).";

	// Token: 0x0400043A RID: 1082
	public const string net_log_sspi_selected_cipher_suite = "{0}(Protocol={1}, Cipher={2} {3} bit strength, Hash={4} {5} bit strength, Key Exchange={6} {7} bit strength).";

	// Token: 0x0400043B RID: 1083
	public const string net_log_remote_certificate = "Remote certificate: {0}.";

	// Token: 0x0400043C RID: 1084
	public const string net_log_locating_private_key_for_certificate = "Locating the private key for the certificate: {0}.";

	// Token: 0x0400043D RID: 1085
	public const string net_log_cert_is_of_type_2 = "Certificate is of type X509Certificate2 and contains the private key.";

	// Token: 0x0400043E RID: 1086
	public const string net_log_found_cert_in_store = "Found the certificate in the {0} store.";

	// Token: 0x0400043F RID: 1087
	public const string net_log_did_not_find_cert_in_store = "Cannot find the certificate in either the LocalMachine store or the CurrentUser store.";

	// Token: 0x04000440 RID: 1088
	public const string net_log_open_store_failed = "Opening Certificate store {0} failed, exception: {1}.";

	// Token: 0x04000441 RID: 1089
	public const string net_log_got_certificate_from_delegate = "Got a certificate from the client delegate.";

	// Token: 0x04000442 RID: 1090
	public const string net_log_no_delegate_and_have_no_client_cert = "Client delegate did not provide a certificate; and there are not other user-provided certificates. Need to attempt a session restart.";

	// Token: 0x04000443 RID: 1091
	public const string net_log_no_delegate_but_have_client_cert = "Client delegate did not provide a certificate; but there are other user-provided certificates\".";

	// Token: 0x04000444 RID: 1092
	public const string net_log_attempting_restart_using_cert = "Attempting to restart the session using the user-provided certificate: {0}.";

	// Token: 0x04000445 RID: 1093
	public const string net_log_no_issuers_try_all_certs = "We have user-provided certificates. The server has not specified any issuers, so try all the certificates.";

	// Token: 0x04000446 RID: 1094
	public const string net_log_server_issuers_look_for_matching_certs = "We have user-provided certificates. The server has specified {0} issuer(s). Looking for certificates that match any of the issuers.";

	// Token: 0x04000447 RID: 1095
	public const string net_log_selected_cert = "Selected certificate: {0}.";

	// Token: 0x04000448 RID: 1096
	public const string net_log_n_certs_after_filtering = "Left with {0} client certificates to choose from.";

	// Token: 0x04000449 RID: 1097
	public const string net_log_finding_matching_certs = "Trying to find a matching certificate in the certificate store.";

	// Token: 0x0400044A RID: 1098
	public const string net_log_using_cached_credential = "Using the cached credential handle.";

	// Token: 0x0400044B RID: 1099
	public const string net_log_remote_cert_user_declared_valid = "Remote certificate was verified as valid by the user.";

	// Token: 0x0400044C RID: 1100
	public const string net_log_remote_cert_user_declared_invalid = "Remote certificate was verified as invalid by the user.";

	// Token: 0x0400044D RID: 1101
	public const string net_log_remote_cert_has_no_errors = "Remote certificate has no errors.";

	// Token: 0x0400044E RID: 1102
	public const string net_log_remote_cert_has_errors = "Remote certificate has errors:";

	// Token: 0x0400044F RID: 1103
	public const string net_log_remote_cert_not_available = "The remote server did not provide a certificate.";

	// Token: 0x04000450 RID: 1104
	public const string net_log_remote_cert_name_mismatch = "Certificate name mismatch.";

	// Token: 0x04000451 RID: 1105
	public const string net_log_proxy_autodetect_script_location_parse_error = "WebProxy failed to parse the auto-detected location of a proxy script:\"{0}\" into a Uri.";

	// Token: 0x04000452 RID: 1106
	public const string net_log_proxy_autodetect_failed = "WebProxy failed to autodetect a Uri for a proxy script.";

	// Token: 0x04000453 RID: 1107
	public const string net_log_proxy_script_execution_error = "WebProxy caught an exception while executing the ScriptReturn script: {0}.";

	// Token: 0x04000454 RID: 1108
	public const string net_log_proxy_script_download_compile_error = "WebProxy caught an exception while  downloading/compiling the proxy script: {0}.";

	// Token: 0x04000455 RID: 1109
	public const string net_log_proxy_system_setting_update = "ScriptEngine was notified of a potential change in the system's proxy settings and will update WebProxy settings.";

	// Token: 0x04000456 RID: 1110
	public const string net_log_proxy_update_due_to_ip_config_change = "ScriptEngine was notified of a change in the IP configuration and will update WebProxy settings.";

	// Token: 0x04000457 RID: 1111
	public const string net_log_proxy_called_with_null_parameter = "{0} was called with a null '{1}' parameter.";

	// Token: 0x04000458 RID: 1112
	public const string net_log_proxy_called_with_invalid_parameter = "{0} was called with an invalid parameter.";

	// Token: 0x04000459 RID: 1113
	public const string net_log_proxy_ras_supported = "RAS supported: {0}";

	// Token: 0x0400045A RID: 1114
	public const string net_log_proxy_ras_notsupported_exception = "RAS is not supported. Can't create RasHelper instance.";

	// Token: 0x0400045B RID: 1115
	public const string net_log_proxy_winhttp_cant_open_session = "Can't open WinHttp session. Error code: {0}.";

	// Token: 0x0400045C RID: 1116
	public const string net_log_proxy_winhttp_getproxy_failed = "Can't retrieve proxy settings for Uri '{0}'. Error code: {1}.";

	// Token: 0x0400045D RID: 1117
	public const string net_log_proxy_winhttp_timeout_error = "Can't specify proxy discovery timeout. Error code: {0}.";

	// Token: 0x0400045E RID: 1118
	public const string net_log_cache_validation_failed_resubmit = "Resubmitting this request because cache cannot validate the response.";

	// Token: 0x0400045F RID: 1119
	public const string net_log_cache_refused_server_response = "Caching protocol has refused the server response. To allow automatic request retrying set request.AllowAutoRedirect=true.";

	// Token: 0x04000460 RID: 1120
	public const string net_log_cache_ftp_proxy_doesnt_support_partial = "This FTP request is configured to use a proxy through HTTP protocol. Cache revalidation and partially cached responses are not supported.";

	// Token: 0x04000461 RID: 1121
	public const string net_log_cache_ftp_method = "FTP request method={0}.";

	// Token: 0x04000462 RID: 1122
	public const string net_log_cache_ftp_supports_bin_only = "Caching is not supported for non-binary FTP request mode.";

	// Token: 0x04000463 RID: 1123
	public const string net_log_cache_replacing_entry_with_HTTP_200 = "Replacing cache entry metadata with 'HTTP/1.1 200 OK' status line to satisfy HTTP cache protocol logic.";

	// Token: 0x04000464 RID: 1124
	public const string net_log_cache_now_time = "[Now Time (UTC)] = {0}.";

	// Token: 0x04000465 RID: 1125
	public const string net_log_cache_max_age_absolute = "[MaxAge] Absolute time expiration check (sensitive to clock skew), cache Expires: {0}.";

	// Token: 0x04000466 RID: 1126
	public const string net_log_cache_age1 = "[Age1] Now - LastSynchronized = [Age1] Now - LastSynchronized = {0}, Last Synchronized: {1}.";

	// Token: 0x04000467 RID: 1127
	public const string net_log_cache_age1_date_header = "[Age1] NowTime-Date Header = {0}, Date Header: {1}.";

	// Token: 0x04000468 RID: 1128
	public const string net_log_cache_age1_last_synchronized = "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}.";

	// Token: 0x04000469 RID: 1129
	public const string net_log_cache_age1_last_synchronized_age_header = "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}, Age Header: {2}.";

	// Token: 0x0400046A RID: 1130
	public const string net_log_cache_age2 = "[Age2] AgeHeader = {0}.";

	// Token: 0x0400046B RID: 1131
	public const string net_log_cache_max_age_cache_s_max_age = "[MaxAge] Cache s_MaxAge = {0}.";

	// Token: 0x0400046C RID: 1132
	public const string net_log_cache_max_age_expires_date = "[MaxAge] Cache Expires - Date = {0}, Expires: {1}.";

	// Token: 0x0400046D RID: 1133
	public const string net_log_cache_max_age_cache_max_age = "[MaxAge] Cache MaxAge = {0}.";

	// Token: 0x0400046E RID: 1134
	public const string net_log_cache_no_max_age_use_10_percent = "[MaxAge] Cannot compute Cache MaxAge, use 10% since LastModified: {0}, LastModified: {1}.";

	// Token: 0x0400046F RID: 1135
	public const string net_log_cache_no_max_age_use_default = "[MaxAge] Cannot compute Cache MaxAge, using default RequestCacheValidator.UnspecifiedMaxAge: {0}.";

	// Token: 0x04000470 RID: 1136
	public const string net_log_cache_validator_invalid_for_policy = "This validator should not be called for policy : {0}.";

	// Token: 0x04000471 RID: 1137
	public const string net_log_cache_response_last_modified = "Response LastModified={0},  ContentLength= {1}.";

	// Token: 0x04000472 RID: 1138
	public const string net_log_cache_cache_last_modified = "Cache    LastModified={0},  ContentLength= {1}.";

	// Token: 0x04000473 RID: 1139
	public const string net_log_cache_partial_and_non_zero_content_offset = "A Cache Entry is partial and the user request has non zero ContentOffset = {0}. A restart from cache is not supported for partial cache entries.";

	// Token: 0x04000474 RID: 1140
	public const string net_log_cache_response_valid_based_on_policy = "Response is valid based on Policy = {0}.";

	// Token: 0x04000475 RID: 1141
	public const string net_log_cache_null_response_failure = "Response is null so this Request should fail.";

	// Token: 0x04000476 RID: 1142
	public const string net_log_cache_ftp_response_status = "FTP Response Status={0}, {1}.";

	// Token: 0x04000477 RID: 1143
	public const string net_log_cache_resp_valid_based_on_retry = "Accept this response as valid based on the retry count = {0}.";

	// Token: 0x04000478 RID: 1144
	public const string net_log_cache_no_update_based_on_method = "Cache is not updated based on the request Method = {0}.";

	// Token: 0x04000479 RID: 1145
	public const string net_log_cache_removed_existing_invalid_entry = "Existing entry is removed because it was found invalid.";

	// Token: 0x0400047A RID: 1146
	public const string net_log_cache_not_updated_based_on_policy = "Cache is not updated based on Policy = {0}.";

	// Token: 0x0400047B RID: 1147
	public const string net_log_cache_not_updated_because_no_response = "Cache is not updated because there is no response associated with the request.";

	// Token: 0x0400047C RID: 1148
	public const string net_log_cache_removed_existing_based_on_method = "Existing cache entry is removed based on the request Method = {0}.";

	// Token: 0x0400047D RID: 1149
	public const string net_log_cache_existing_not_removed_because_unexpected_response_status = "Existing cache entry should but cannot be removed due to unexpected response Status = ({0}) {1}.";

	// Token: 0x0400047E RID: 1150
	public const string net_log_cache_removed_existing_based_on_policy = "Existing cache entry is removed based on Policy = {0}.";

	// Token: 0x0400047F RID: 1151
	public const string net_log_cache_not_updated_based_on_ftp_response_status = "Cache is not updated based on the FTP response status. Expected = {0}, actual = {1}.";

	// Token: 0x04000480 RID: 1152
	public const string net_log_cache_update_not_supported_for_ftp_restart = "Cache update is not supported for restarted FTP responses. Restart offset = {0}.";

	// Token: 0x04000481 RID: 1153
	public const string net_log_cache_removed_entry_because_ftp_restart_response_changed = "Existing cache entry is removed since a restarted response was changed on the server, cache LastModified date = {0}, new LastModified date = {1}.";

	// Token: 0x04000482 RID: 1154
	public const string net_log_cache_last_synchronized = "The cache entry last synchronized time = {0}.";

	// Token: 0x04000483 RID: 1155
	public const string net_log_cache_suppress_update_because_synched_last_minute = "Suppressing cache update since the entry was synchronized within the last minute.";

	// Token: 0x04000484 RID: 1156
	public const string net_log_cache_updating_last_synchronized = "Updating cache entry last synchronized time = {0}.";

	// Token: 0x04000485 RID: 1157
	public const string net_log_cache_cannot_remove = "{0} Cannot Remove (throw): Key = {1}, Error = {2}.";

	// Token: 0x04000486 RID: 1158
	public const string net_log_cache_key_status = "{0}, Key = {1}, -> Status = {2}.";

	// Token: 0x04000487 RID: 1159
	public const string net_log_cache_key_remove_failed_status = "{0}, Key = {1}, Remove operation failed -> Status = {2}.";

	// Token: 0x04000488 RID: 1160
	public const string net_log_cache_usecount_file = "{0}, UseCount = {1}, File = {2}.";

	// Token: 0x04000489 RID: 1161
	public const string net_log_cache_stream = "{0}, stream = {1}.";

	// Token: 0x0400048A RID: 1162
	public const string net_log_cache_filename = "{0} -> Filename = {1}, Status = {2}.";

	// Token: 0x0400048B RID: 1163
	public const string net_log_cache_lookup_failed = "{0}, Lookup operation failed -> {1}.";

	// Token: 0x0400048C RID: 1164
	public const string net_log_cache_exception = "{0}, Exception = {1}.";

	// Token: 0x0400048D RID: 1165
	public const string net_log_cache_expected_length = "Expected length (0=none)= {0}.";

	// Token: 0x0400048E RID: 1166
	public const string net_log_cache_last_modified = "LastModified    (0=none)= {0}.";

	// Token: 0x0400048F RID: 1167
	public const string net_log_cache_expires = "Expires         (0=none)= {0}.";

	// Token: 0x04000490 RID: 1168
	public const string net_log_cache_max_stale = "MaxStale (sec)          = {0}.";

	// Token: 0x04000491 RID: 1169
	public const string net_log_cache_dumping_metadata = "...Dumping Metadata...";

	// Token: 0x04000492 RID: 1170
	public const string net_log_cache_create_failed = "Create operation failed -> {0}.";

	// Token: 0x04000493 RID: 1171
	public const string net_log_cache_set_expires = "Set Expires               ={0}.";

	// Token: 0x04000494 RID: 1172
	public const string net_log_cache_set_last_modified = "Set LastModified          ={0}.";

	// Token: 0x04000495 RID: 1173
	public const string net_log_cache_set_last_synchronized = "Set LastSynchronized      ={0}.";

	// Token: 0x04000496 RID: 1174
	public const string net_log_cache_enable_max_stale = "Enable MaxStale (sec) ={0}.";

	// Token: 0x04000497 RID: 1175
	public const string net_log_cache_disable_max_stale = "Disable MaxStale (set to 0).";

	// Token: 0x04000498 RID: 1176
	public const string net_log_cache_set_new_metadata = "Set new Metadata.";

	// Token: 0x04000499 RID: 1177
	public const string net_log_cache_dumping = "...Dumping...";

	// Token: 0x0400049A RID: 1178
	public const string net_log_cache_key = "{0}, Key = {1}.";

	// Token: 0x0400049B RID: 1179
	public const string net_log_cache_no_commit = "{0}, Nothing was written to the stream, do not commit that cache entry.";

	// Token: 0x0400049C RID: 1180
	public const string net_log_cache_error_deleting_filename = "{0}, Error deleting a Filename = {1}.";

	// Token: 0x0400049D RID: 1181
	public const string net_log_cache_update_failed = "{0}, Key = {1}, Update operation failed -> {2}.";

	// Token: 0x0400049E RID: 1182
	public const string net_log_cache_delete_failed = "{0}, Key = {1}, Delete operation failed -> {2}.";

	// Token: 0x0400049F RID: 1183
	public const string net_log_cache_commit_failed = "{0}, Key = {1}, Commit operation failed -> {2}.";

	// Token: 0x040004A0 RID: 1184
	public const string net_log_cache_committed_as_partial = "{0}, Key = {1}, Committed entry as partial, not cached bytes count = {2}.";

	// Token: 0x040004A1 RID: 1185
	public const string net_log_cache_max_stale_and_update_status = "{0}, MaxStale = {1}, Update Status = {2}.";

	// Token: 0x040004A2 RID: 1186
	public const string net_log_cache_failing_request_with_exception = "Failing request with the WebExceptionStatus = {0}.";

	// Token: 0x040004A3 RID: 1187
	public const string net_log_cache_request_method = "Request Method = {0}.";

	// Token: 0x040004A4 RID: 1188
	public const string net_log_cache_http_status_parse_failure = "Cannot Parse Cache HTTP Status Line: {0}.";

	// Token: 0x040004A5 RID: 1189
	public const string net_log_cache_http_status_line = "Entry Status Line = HTTP/{0} {1} {2}.";

	// Token: 0x040004A6 RID: 1190
	public const string net_log_cache_cache_control = "Cache Cache-Control = {0}.";

	// Token: 0x040004A7 RID: 1191
	public const string net_log_cache_invalid_http_version = "The cached version is invalid, assuming HTTP 1.0.";

	// Token: 0x040004A8 RID: 1192
	public const string net_log_cache_no_http_response_header = "This Cache Entry does not carry HTTP response headers.";

	// Token: 0x040004A9 RID: 1193
	public const string net_log_cache_http_header_parse_error = "Cannot parse HTTP headers in entry metadata, offending string: {0}.";

	// Token: 0x040004AA RID: 1194
	public const string net_log_cache_metadata_name_value_parse_error = "Cannot parse all strings in system metadata as \"name:value\", offending string: {0}.";

	// Token: 0x040004AB RID: 1195
	public const string net_log_cache_content_range_error = "Invalid format of Response Content-Range:{0}.";

	// Token: 0x040004AC RID: 1196
	public const string net_log_cache_cache_control_error = "Invalid CacheControl header = {0}.";

	// Token: 0x040004AD RID: 1197
	public const string net_log_cache_unexpected_status = "The cache protocol method {0} has returned unexpected status: {1}.";

	// Token: 0x040004AE RID: 1198
	public const string net_log_cache_object_and_exception = "{0} exception: {1}.";

	// Token: 0x040004AF RID: 1199
	public const string net_log_cache_revalidation_not_needed = "{0}, No cache entry revalidation is needed.";

	// Token: 0x040004B0 RID: 1200
	public const string net_log_cache_not_updated_based_on_cache_protocol_status = "{0}, Cache is not updated based on the current cache protocol status = {1}.";

	// Token: 0x040004B1 RID: 1201
	public const string net_log_cache_closing_cache_stream = "{0}: {1} Closing effective cache stream, type = {2}, cache entry key = {3}.";

	// Token: 0x040004B2 RID: 1202
	public const string net_log_cache_exception_ignored = "{0}: an exception (ignored) on {1} = {2}.";

	// Token: 0x040004B3 RID: 1203
	public const string net_log_cache_no_cache_entry = "{0} has requested a cache response but the entry does not exist (Stream.Null).";

	// Token: 0x040004B4 RID: 1204
	public const string net_log_cache_null_cached_stream = "{0} has requested a cache response but the cached stream is null.";

	// Token: 0x040004B5 RID: 1205
	public const string net_log_cache_requested_combined_but_null_cached_stream = "{0} has requested a combined response but the cached stream is null.";

	// Token: 0x040004B6 RID: 1206
	public const string net_log_cache_returned_range_cache = "{0} has returned a range cache stream, Offset = {1}, Length = {2}.";

	// Token: 0x040004B7 RID: 1207
	public const string net_log_cache_entry_not_found_freshness_undefined = "{0}, Cache Entry not found, freshness result = Undefined.";

	// Token: 0x040004B8 RID: 1208
	public const string net_log_cache_dumping_cache_context = "...Dumping Cache Context...";

	// Token: 0x040004B9 RID: 1209
	public const string net_log_cache_result = "{0}, result = {1}.";

	// Token: 0x040004BA RID: 1210
	public const string net_log_cache_uri_with_query_has_no_expiration = "Request Uri has a Query, and no explicit expiration time is provided.";

	// Token: 0x040004BB RID: 1211
	public const string net_log_cache_uri_with_query_and_cached_resp_from_http_10 = "Request Uri has a Query, and cached response is from HTTP 1.0 server.";

	// Token: 0x040004BC RID: 1212
	public const string net_log_cache_valid_as_fresh_or_because_policy = "Valid as fresh or because of Cache Policy = {0}.";

	// Token: 0x040004BD RID: 1213
	public const string net_log_cache_accept_based_on_retry_count = "Accept this response base on the retry count = {0}.";

	// Token: 0x040004BE RID: 1214
	public const string net_log_cache_date_header_older_than_cache_entry = "Response Date header value is older than that of the cache entry.";

	// Token: 0x040004BF RID: 1215
	public const string net_log_cache_server_didnt_satisfy_range = "Server did not satisfy the range: {0}.";

	// Token: 0x040004C0 RID: 1216
	public const string net_log_cache_304_received_on_unconditional_request = "304 response was received on an unconditional request.";

	// Token: 0x040004C1 RID: 1217
	public const string net_log_cache_304_received_on_unconditional_request_expected_200_206 = "304 response was received on an unconditional request, but expected response code is 200 or 206.";

	// Token: 0x040004C2 RID: 1218
	public const string net_log_cache_last_modified_header_older_than_cache_entry = "HTTP 1.0 Response Last-Modified header value is older than that of the cache entry.";

	// Token: 0x040004C3 RID: 1219
	public const string net_log_cache_freshness_outside_policy_limits = "Response freshness is not within the specified policy limits.";

	// Token: 0x040004C4 RID: 1220
	public const string net_log_cache_need_to_remove_invalid_cache_entry_304 = "Need to remove an invalid cache entry with status code == 304(NotModified).";

	// Token: 0x040004C5 RID: 1221
	public const string net_log_cache_resp_status = "Response Status = {0}.";

	// Token: 0x040004C6 RID: 1222
	public const string net_log_cache_resp_304_or_request_head = "Response==304 or Request was HEAD, updating cache entry.";

	// Token: 0x040004C7 RID: 1223
	public const string net_log_cache_dont_update_cached_headers = "Do not update Cached Headers.";

	// Token: 0x040004C8 RID: 1224
	public const string net_log_cache_update_cached_headers = "Update Cached Headers.";

	// Token: 0x040004C9 RID: 1225
	public const string net_log_cache_partial_resp_not_combined_with_existing_entry = "A partial response is not combined with existing cache entry, Cache Stream Size = {0}, response Range Start = {1}.";

	// Token: 0x040004CA RID: 1226
	public const string net_log_cache_request_contains_conditional_header = "User Request contains a conditional header.";

	// Token: 0x040004CB RID: 1227
	public const string net_log_cache_not_a_get_head_post = "This was Not a GET, HEAD or POST request.";

	// Token: 0x040004CC RID: 1228
	public const string net_log_cache_cannot_update_cache_if_304 = "Cannot update cache if Response status == 304 and a cache entry was not found.";

	// Token: 0x040004CD RID: 1229
	public const string net_log_cache_cannot_update_cache_with_head_resp = "Cannot update cache with HEAD response if the cache entry does not exist.";

	// Token: 0x040004CE RID: 1230
	public const string net_log_cache_http_resp_is_null = "HttpWebResponse is null.";

	// Token: 0x040004CF RID: 1231
	public const string net_log_cache_resp_cache_control_is_no_store = "Response Cache-Control = no-store.";

	// Token: 0x040004D0 RID: 1232
	public const string net_log_cache_resp_cache_control_is_public = "Response Cache-Control = public.";

	// Token: 0x040004D1 RID: 1233
	public const string net_log_cache_resp_cache_control_is_private = "Response Cache-Control = private, and Cache is public.";

	// Token: 0x040004D2 RID: 1234
	public const string net_log_cache_resp_cache_control_is_private_plus_headers = "Response Cache-Control = private+Headers, removing those headers.";

	// Token: 0x040004D3 RID: 1235
	public const string net_log_cache_resp_older_than_cache = "HttpWebResponse date is older than of the cached one.";

	// Token: 0x040004D4 RID: 1236
	public const string net_log_cache_revalidation_required = "Response revalidation is always required but neither Last-Modified nor ETag header is set on the response.";

	// Token: 0x040004D5 RID: 1237
	public const string net_log_cache_needs_revalidation = "Response can be cached although it will always require revalidation.";

	// Token: 0x040004D6 RID: 1238
	public const string net_log_cache_resp_allows_caching = "Response explicitly allows caching = Cache-Control: {0}.";

	// Token: 0x040004D7 RID: 1239
	public const string net_log_cache_auth_header_and_no_s_max_age = "Request carries Authorization Header and no s-maxage, proxy-revalidate or public directive found.";

	// Token: 0x040004D8 RID: 1240
	public const string net_log_cache_post_resp_without_cache_control_or_expires = "POST Response without Cache-Control or Expires headers.";

	// Token: 0x040004D9 RID: 1241
	public const string net_log_cache_valid_based_on_status_code = "Valid based on Status Code: {0}.";

	// Token: 0x040004DA RID: 1242
	public const string net_log_cache_resp_no_cache_control = "Response with no CacheControl and Status Code = {0}.";

	// Token: 0x040004DB RID: 1243
	public const string net_log_cache_age = "Cache Age = {0}, Cache MaxAge = {1}.";

	// Token: 0x040004DC RID: 1244
	public const string net_log_cache_policy_min_fresh = "Client Policy MinFresh = {0}.";

	// Token: 0x040004DD RID: 1245
	public const string net_log_cache_policy_max_age = "Client Policy MaxAge = {0}.";

	// Token: 0x040004DE RID: 1246
	public const string net_log_cache_policy_cache_sync_date = "Client Policy CacheSyncDate (UTC) = {0}, Cache LastSynchronizedUtc = {1}.";

	// Token: 0x040004DF RID: 1247
	public const string net_log_cache_policy_max_stale = "Client Policy MaxStale = {0}.";

	// Token: 0x040004E0 RID: 1248
	public const string net_log_cache_control_no_cache = "Cached CacheControl = no-cache.";

	// Token: 0x040004E1 RID: 1249
	public const string net_log_cache_control_no_cache_removing_some_headers = "Cached CacheControl = no-cache, Removing some headers.";

	// Token: 0x040004E2 RID: 1250
	public const string net_log_cache_control_must_revalidate = "Cached CacheControl = must-revalidate and Cache is not fresh.";

	// Token: 0x040004E3 RID: 1251
	public const string net_log_cache_cached_auth_header = "The cached entry has Authorization Header and cache is not fresh.";

	// Token: 0x040004E4 RID: 1252
	public const string net_log_cache_cached_auth_header_no_control_directive = "The cached entry has Authorization Header and no Cache-Control directive present that would allow to use that entry.";

	// Token: 0x040004E5 RID: 1253
	public const string net_log_cache_after_validation = "After Response Cache Validation.";

	// Token: 0x040004E6 RID: 1254
	public const string net_log_cache_resp_status_304 = "Response status == 304 but the cache entry does not exist.";

	// Token: 0x040004E7 RID: 1255
	public const string net_log_cache_head_resp_has_different_content_length = "A response resulted from a HEAD request has different Content-Length header.";

	// Token: 0x040004E8 RID: 1256
	public const string net_log_cache_head_resp_has_different_content_md5 = "A response resulted from a HEAD request has different Content-MD5 header.";

	// Token: 0x040004E9 RID: 1257
	public const string net_log_cache_head_resp_has_different_etag = "A response resulted from a HEAD request has different ETag header.";

	// Token: 0x040004EA RID: 1258
	public const string net_log_cache_304_head_resp_has_different_last_modified = "A 304 response resulted from a HEAD request has different Last-Modified header.";

	// Token: 0x040004EB RID: 1259
	public const string net_log_cache_existing_entry_has_to_be_discarded = "An existing cache entry has to be discarded.";

	// Token: 0x040004EC RID: 1260
	public const string net_log_cache_existing_entry_should_be_discarded = "An existing cache entry should be discarded.";

	// Token: 0x040004ED RID: 1261
	public const string net_log_cache_206_resp_non_matching_entry = "A 206 Response has been received and either ETag or Last-Modified header value does not match cache entry.";

	// Token: 0x040004EE RID: 1262
	public const string net_log_cache_206_resp_starting_position_not_adjusted = "The starting position for 206 Response is not adjusted to the end of cache entry.";

	// Token: 0x040004EF RID: 1263
	public const string net_log_cache_combined_resp_requested = "Creation of a combined response has been requested from the cache protocol.";

	// Token: 0x040004F0 RID: 1264
	public const string net_log_cache_updating_headers_on_304 = "Updating headers on 304 response.";

	// Token: 0x040004F1 RID: 1265
	public const string net_log_cache_suppressing_headers_update_on_304 = "Suppressing cache headers update on 304, new headers don't add anything.";

	// Token: 0x040004F2 RID: 1266
	public const string net_log_cache_status_code_not_304_206 = "A Response Status Code is not 304 or 206.";

	// Token: 0x040004F3 RID: 1267
	public const string net_log_cache_sxx_resp_cache_only = "A 5XX Response and Cache-Only like policy, serving from cache.";

	// Token: 0x040004F4 RID: 1268
	public const string net_log_cache_sxx_resp_can_be_replaced = "A 5XX Response that can be replaced by existing cache entry.";

	// Token: 0x040004F5 RID: 1269
	public const string net_log_cache_vary_header_empty = "Cache entry Vary header is empty.";

	// Token: 0x040004F6 RID: 1270
	public const string net_log_cache_vary_header_contains_asterisks = "Cache entry Vary header contains '*'.";

	// Token: 0x040004F7 RID: 1271
	public const string net_log_cache_no_headers_in_metadata = "No request headers are found in cached metadata to test based on the cached response Vary header.";

	// Token: 0x040004F8 RID: 1272
	public const string net_log_cache_vary_header_mismatched_count = "Vary header: Request and cache header fields count does not match, header name = {0}.";

	// Token: 0x040004F9 RID: 1273
	public const string net_log_cache_vary_header_mismatched_field = "Vary header: A Cache header field mismatch the request one, header name = {0}, cache field = {1}, request field = {2}.";

	// Token: 0x040004FA RID: 1274
	public const string net_log_cache_vary_header_match = "All required Request headers match based on cached Vary response header.";

	// Token: 0x040004FB RID: 1275
	public const string net_log_cache_range = "Request Range (not in Cache yet) = Range:{0}.";

	// Token: 0x040004FC RID: 1276
	public const string net_log_cache_range_invalid_format = "Invalid format of Request Range:{0}.";

	// Token: 0x040004FD RID: 1277
	public const string net_log_cache_range_not_in_cache = "Cannot serve from Cache, Range:{0}.";

	// Token: 0x040004FE RID: 1278
	public const string net_log_cache_range_in_cache = "Serving Request Range from cache, Range:{0}.";

	// Token: 0x040004FF RID: 1279
	public const string net_log_cache_partial_resp = "Serving Partial Response (206) from cache, Content-Range:{0}.";

	// Token: 0x04000500 RID: 1280
	public const string net_log_cache_range_request_range = "Range Request (user specified), Range: {0}.";

	// Token: 0x04000501 RID: 1281
	public const string net_log_cache_could_be_partial = "Could be a Partial Cached Response, Size = {0}, Response Content Length = {1}.";

	// Token: 0x04000502 RID: 1282
	public const string net_log_cache_condition_if_none_match = "Request Condition = If-None-Match:{0}.";

	// Token: 0x04000503 RID: 1283
	public const string net_log_cache_condition_if_modified_since = "Request Condition = If-Modified-Since:{0}.";

	// Token: 0x04000504 RID: 1284
	public const string net_log_cache_cannot_construct_conditional_request = "A Conditional Request cannot be constructed.";

	// Token: 0x04000505 RID: 1285
	public const string net_log_cache_cannot_construct_conditional_range_request = "A Conditional Range request cannot be constructed.";

	// Token: 0x04000506 RID: 1286
	public const string net_log_cache_entry_size_too_big = "Cached Entry Size = {0} is too big, cannot do a range request.";

	// Token: 0x04000507 RID: 1287
	public const string net_log_cache_condition_if_range = "Request Condition = If-Range:{0}.";

	// Token: 0x04000508 RID: 1288
	public const string net_log_cache_conditional_range_not_implemented_on_http_10 = "A Conditional Range request on Http <= 1.0 is not implemented.";

	// Token: 0x04000509 RID: 1289
	public const string net_log_cache_saving_request_headers = "Saving Request Headers, Vary: {0}.";

	// Token: 0x0400050A RID: 1290
	public const string net_log_cache_only_byte_range_implemented = "Ranges other than bytes are not implemented.";

	// Token: 0x0400050B RID: 1291
	public const string net_log_cache_multiple_complex_range_not_implemented = "Multiple/complexe ranges are not implemented.";

	// Token: 0x0400050C RID: 1292
	public const string net_log_digest_hash_algorithm_not_supported = "The hash algorithm is not supported by Digest authentication: {0}.";

	// Token: 0x0400050D RID: 1293
	public const string net_log_digest_qop_not_supported = "The Quality of Protection value is not supported by Digest authentication: {0}.";

	// Token: 0x0400050E RID: 1294
	public const string net_log_digest_requires_nonce = "A nonce parameter required for Digest authentication was not found or was preceded by an invalid parameter.";

	// Token: 0x0400050F RID: 1295
	public const string net_log_auth_invalid_challenge = "The challenge string is not valid for this authentication module: {0}";

	// Token: 0x04000510 RID: 1296
	public const string net_log_unknown = "unknown";

	// Token: 0x04000511 RID: 1297
	public const string net_log_operation_returned_something = "{0} returned {1}.";

	// Token: 0x04000512 RID: 1298
	public const string net_log_buffered_n_bytes = "Buffered {0} bytes.";

	// Token: 0x04000513 RID: 1299
	public const string net_log_method_equal = "Method={0}.";

	// Token: 0x04000514 RID: 1300
	public const string net_log_releasing_connection = "Releasing FTP connection#{0}.";

	// Token: 0x04000515 RID: 1301
	public const string net_log_unexpected_exception = "Unexpected exception in {0}.";

	// Token: 0x04000516 RID: 1302
	public const string net_log_server_response_error_code = "Error code {0} was received from server response.";

	// Token: 0x04000517 RID: 1303
	public const string net_log_resubmitting_request = "Resubmitting request.";

	// Token: 0x04000518 RID: 1304
	public const string net_log_retrieving_localhost_exception = "An unexpected exception while retrieving the local address list: {0}.";

	// Token: 0x04000519 RID: 1305
	public const string net_log_resolved_servicepoint_may_not_be_remote_server = "A resolved ServicePoint host could be wrongly considered as a remote server.";

	// Token: 0x0400051A RID: 1306
	public const string net_log_closed_idle = "{0}#{1} - Closed as idle.";

	// Token: 0x0400051B RID: 1307
	public const string net_log_received_status_line = "Received status line: Version={0}, StatusCode={1}, StatusDescription={2}.";

	// Token: 0x0400051C RID: 1308
	public const string net_log_sending_headers = "Sending headers\r\n{{\r\n{0}}}.";

	// Token: 0x0400051D RID: 1309
	public const string net_log_received_headers = "Received headers\r\n{{\r\n{0}}}.";

	// Token: 0x0400051E RID: 1310
	public const string net_log_shell_expression_pattern_format_warning = "ShellServices.ShellExpression.Parse() was called with a badly formatted 'pattern':{0}.";

	// Token: 0x0400051F RID: 1311
	public const string net_log_exception_in_callback = "Exception in callback: {0}.";

	// Token: 0x04000520 RID: 1312
	public const string net_log_sending_command = "Sending command [{0}]";

	// Token: 0x04000521 RID: 1313
	public const string net_log_received_response = "Received response [{0}]";

	// Token: 0x04000522 RID: 1314
	public const string net_log_socket_connected = "Created connection from {0} to {1}.";

	// Token: 0x04000523 RID: 1315
	public const string net_log_socket_accepted = "Accepted connection from {0} to {1}.";

	// Token: 0x04000524 RID: 1316
	public const string net_log_socket_not_logged_file = "Not logging data from file: {0}.";

	// Token: 0x04000525 RID: 1317
	public const string net_log_socket_connect_dnsendpoint = "Connecting to a DnsEndPoint.";

	// Token: 0x04000526 RID: 1318
	public const string MailAddressInvalidFormat = "The specified string is not in the form required for an e-mail address.";

	// Token: 0x04000527 RID: 1319
	public const string MailSubjectInvalidFormat = "The specified string is not in the form required for a subject.";

	// Token: 0x04000528 RID: 1320
	public const string MailBase64InvalidCharacter = "An invalid character was found in the Base-64 stream.";

	// Token: 0x04000529 RID: 1321
	public const string MailCollectionIsReadOnly = "The collection is read-only.";

	// Token: 0x0400052A RID: 1322
	public const string MailDateInvalidFormat = "The date is in an invalid format.";

	// Token: 0x0400052B RID: 1323
	public const string MailHeaderFieldAlreadyExists = "The specified singleton field already exists in the collection and cannot be added.";

	// Token: 0x0400052C RID: 1324
	public const string MailHeaderFieldInvalidCharacter = "An invalid character was found in the mail header: '{0}'.";

	// Token: 0x0400052D RID: 1325
	public const string MailHeaderFieldMalformedHeader = "The mail header is malformed.";

	// Token: 0x0400052E RID: 1326
	public const string MailHeaderFieldMismatchedName = "The header name does not match this property.";

	// Token: 0x0400052F RID: 1327
	public const string MailHeaderIndexOutOfBounds = "The index value is outside the bounds of the array.";

	// Token: 0x04000530 RID: 1328
	public const string MailHeaderItemAccessorOnlySingleton = "The Item property can only be used with singleton fields.";

	// Token: 0x04000531 RID: 1329
	public const string MailHeaderListHasChanged = "The underlying list has been changed and the enumeration is out of date.";

	// Token: 0x04000532 RID: 1330
	public const string MailHeaderResetCalledBeforeEOF = "The stream should have been consumed before resetting.";

	// Token: 0x04000533 RID: 1331
	public const string MailHeaderTargetArrayTooSmall = "The target array is too small to contain all the headers.";

	// Token: 0x04000534 RID: 1332
	public const string MailHeaderInvalidCID = "The ContentID cannot contain a '<' or '>' character.";

	// Token: 0x04000535 RID: 1333
	public const string MailHostNotFound = "The SMTP host was not found.";

	// Token: 0x04000536 RID: 1334
	public const string MailReaderGetContentStreamAlreadyCalled = "GetContentStream() can only be called once.";

	// Token: 0x04000537 RID: 1335
	public const string MailReaderTruncated = "Premature end of stream.";

	// Token: 0x04000538 RID: 1336
	public const string MailWriterIsInContent = "This operation cannot be performed while in content.";

	// Token: 0x04000539 RID: 1337
	public const string MailServerDoesNotSupportStartTls = "Server does not support secure connections.";

	// Token: 0x0400053A RID: 1338
	public const string MailServerResponse = "The server response was: {0}";

	// Token: 0x0400053B RID: 1339
	public const string SSPIAuthenticationOrSPNNull = "AuthenticationType and ServicePrincipalName cannot be specified as null for server's SSPI Negotiation module.";

	// Token: 0x0400053C RID: 1340
	public const string SSPIPInvokeError = "{0} failed with error {1}.";

	// Token: 0x0400053D RID: 1341
	public const string SmtpAlreadyConnected = "Already connected.";

	// Token: 0x0400053E RID: 1342
	public const string SmtpAuthenticationFailed = "Authentication failed.";

	// Token: 0x0400053F RID: 1343
	public const string SmtpAuthenticationFailedNoCreds = "Authentication failed due to lack of credentials.";

	// Token: 0x04000540 RID: 1344
	public const string SmtpDataStreamOpen = "Data stream is still open.";

	// Token: 0x04000541 RID: 1345
	public const string SmtpDefaultMimePreamble = "This is a multi-part MIME message.";

	// Token: 0x04000542 RID: 1346
	public const string SmtpDefaultSubject = "@@SOAP Application Message";

	// Token: 0x04000543 RID: 1347
	public const string SmtpInvalidResponse = "Smtp server returned an invalid response.";

	// Token: 0x04000544 RID: 1348
	public const string SmtpNotConnected = "Not connected.";

	// Token: 0x04000545 RID: 1349
	public const string SmtpSystemStatus = "System status, or system help reply.";

	// Token: 0x04000546 RID: 1350
	public const string SmtpHelpMessage = "Help message.";

	// Token: 0x04000547 RID: 1351
	public const string SmtpServiceReady = "Service ready.";

	// Token: 0x04000548 RID: 1352
	public const string SmtpServiceClosingTransmissionChannel = "Service closing transmission channel.";

	// Token: 0x04000549 RID: 1353
	public const string SmtpOK = "Completed.";

	// Token: 0x0400054A RID: 1354
	public const string SmtpUserNotLocalWillForward = "User not local; will forward to specified path.";

	// Token: 0x0400054B RID: 1355
	public const string SmtpStartMailInput = "Start mail input; end with <CRLF>.<CRLF>.";

	// Token: 0x0400054C RID: 1356
	public const string SmtpServiceNotAvailable = "Service not available, closing transmission channel.";

	// Token: 0x0400054D RID: 1357
	public const string SmtpMailboxBusy = "Mailbox unavailable.";

	// Token: 0x0400054E RID: 1358
	public const string SmtpLocalErrorInProcessing = "Error in processing.";

	// Token: 0x0400054F RID: 1359
	public const string SmtpInsufficientStorage = "Insufficient system storage.";

	// Token: 0x04000550 RID: 1360
	public const string SmtpPermissionDenied = "Client does not have permission to Send As this sender.";

	// Token: 0x04000551 RID: 1361
	public const string SmtpCommandUnrecognized = "Syntax error, command unrecognized.";

	// Token: 0x04000552 RID: 1362
	public const string SmtpSyntaxError = "Syntax error in parameters or arguments.";

	// Token: 0x04000553 RID: 1363
	public const string SmtpCommandNotImplemented = "Command not implemented.";

	// Token: 0x04000554 RID: 1364
	public const string SmtpBadCommandSequence = "Bad sequence of commands.";

	// Token: 0x04000555 RID: 1365
	public const string SmtpCommandParameterNotImplemented = "Command parameter not implemented.";

	// Token: 0x04000556 RID: 1366
	public const string SmtpMailboxUnavailable = "Mailbox unavailable.";

	// Token: 0x04000557 RID: 1367
	public const string SmtpUserNotLocalTryAlternatePath = "User not local; please try a different path.";

	// Token: 0x04000558 RID: 1368
	public const string SmtpExceededStorageAllocation = "Exceeded storage allocation.";

	// Token: 0x04000559 RID: 1369
	public const string SmtpMailboxNameNotAllowed = "Mailbox name not allowed.";

	// Token: 0x0400055A RID: 1370
	public const string SmtpTransactionFailed = "Transaction failed.";

	// Token: 0x0400055B RID: 1371
	public const string SmtpSendMailFailure = "Failure sending mail.";

	// Token: 0x0400055C RID: 1372
	public const string SmtpRecipientFailed = "Unable to send to a recipient.";

	// Token: 0x0400055D RID: 1373
	public const string SmtpRecipientRequired = "A recipient must be specified.";

	// Token: 0x0400055E RID: 1374
	public const string SmtpFromRequired = "A from address must be specified.";

	// Token: 0x0400055F RID: 1375
	public const string SmtpAllRecipientsFailed = "Unable to send to all recipients.";

	// Token: 0x04000560 RID: 1376
	public const string SmtpClientNotPermitted = "Client does not have permission to submit mail to this server.";

	// Token: 0x04000561 RID: 1377
	public const string SmtpMustIssueStartTlsFirst = "The SMTP server requires a secure connection or the client was not authenticated.";

	// Token: 0x04000562 RID: 1378
	public const string SmtpNeedAbsolutePickupDirectory = "Only absolute directories are allowed for pickup directory.";

	// Token: 0x04000563 RID: 1379
	public const string SmtpGetIisPickupDirectoryFailed = "Cannot get IIS pickup directory.";

	// Token: 0x04000564 RID: 1380
	public const string SmtpPickupDirectoryDoesnotSupportSsl = "SSL must not be enabled for pickup-directory delivery methods.";

	// Token: 0x04000565 RID: 1381
	public const string SmtpOperationInProgress = "Previous operation is still in progress.";

	// Token: 0x04000566 RID: 1382
	public const string SmtpAuthResponseInvalid = "The server returned an invalid response in the authentication handshake.";

	// Token: 0x04000567 RID: 1383
	public const string SmtpEhloResponseInvalid = "The server returned an invalid response to the EHLO command.";

	// Token: 0x04000568 RID: 1384
	public const string SmtpNonAsciiUserNotSupported = "The client or server is only configured for E-mail addresses with ASCII local-parts: {0}.";

	// Token: 0x04000569 RID: 1385
	public const string SmtpInvalidHostName = "The address has an invalid host name: {0}.";

	// Token: 0x0400056A RID: 1386
	public const string MimeTransferEncodingNotSupported = "The MIME transfer encoding '{0}' is not supported.";

	// Token: 0x0400056B RID: 1387
	public const string SeekNotSupported = "Seeking is not supported on this stream.";

	// Token: 0x0400056C RID: 1388
	public const string WriteNotSupported = "Writing is not supported on this stream.";

	// Token: 0x0400056D RID: 1389
	public const string InvalidHexDigit = "Invalid hex digit '{0}'.";

	// Token: 0x0400056E RID: 1390
	public const string InvalidSSPIContext = "The SSPI context is not valid.";

	// Token: 0x0400056F RID: 1391
	public const string InvalidSSPIContextKey = "A null session key was obtained from SSPI.";

	// Token: 0x04000570 RID: 1392
	public const string InvalidSSPINegotiationElement = "Invalid SSPI BinaryNegotiationElement.";

	// Token: 0x04000571 RID: 1393
	public const string InvalidHeaderName = "An invalid character was found in header name.";

	// Token: 0x04000572 RID: 1394
	public const string InvalidHeaderValue = "An invalid character was found in header value.";

	// Token: 0x04000573 RID: 1395
	public const string CannotGetEffectiveTimeOfSSPIContext = "Cannot get the effective time of the SSPI context.";

	// Token: 0x04000574 RID: 1396
	public const string CannotGetExpiryTimeOfSSPIContext = "Cannot get the expiry time of the SSPI context.";

	// Token: 0x04000575 RID: 1397
	public const string ReadNotSupported = "Reading is not supported on this stream.";

	// Token: 0x04000576 RID: 1398
	public const string InvalidAsyncResult = "The AsyncResult is not valid.";

	// Token: 0x04000577 RID: 1399
	public const string UnspecifiedHost = "The SMTP host was not specified.";

	// Token: 0x04000578 RID: 1400
	public const string InvalidPort = "The specified port is invalid. The port must be greater than 0.";

	// Token: 0x04000579 RID: 1401
	public const string SmtpInvalidOperationDuringSend = "This operation cannot be performed while a message is being sent.";

	// Token: 0x0400057A RID: 1402
	public const string MimePartCantResetStream = "One of the streams has already been used and can't be reset to the origin.";

	// Token: 0x0400057B RID: 1403
	public const string MediaTypeInvalid = "The specified media type is invalid.";

	// Token: 0x0400057C RID: 1404
	public const string ContentTypeInvalid = "The specified content type is invalid.";

	// Token: 0x0400057D RID: 1405
	public const string ContentDispositionInvalid = "The specified content disposition is invalid.";

	// Token: 0x0400057E RID: 1406
	public const string AttributeNotSupported = "'{0}' is not a valid configuration attribute for type '{1}'.";

	// Token: 0x0400057F RID: 1407
	public const string Cannot_remove_with_null = "Cannot remove with null name.";

	// Token: 0x04000580 RID: 1408
	public const string Config_base_elements_only = "Only elements allowed.";

	// Token: 0x04000581 RID: 1409
	public const string Config_base_no_child_nodes = "Child nodes not allowed.";

	// Token: 0x04000582 RID: 1410
	public const string Config_base_required_attribute_empty = "Required attribute '{0}' cannot be empty.";

	// Token: 0x04000583 RID: 1411
	public const string Config_base_required_attribute_missing = "Required attribute '{0}' not found.";

	// Token: 0x04000584 RID: 1412
	public const string Config_base_time_overflow = "The time span for the property '{0}' exceeds the maximum that can be stored in the configuration.";

	// Token: 0x04000585 RID: 1413
	public const string Config_base_type_must_be_configurationvalidation = "The ConfigurationValidation attribute must be derived from ConfigurationValidation.";

	// Token: 0x04000586 RID: 1414
	public const string Config_base_type_must_be_typeconverter = "The ConfigurationPropertyConverter attribute must be derived from TypeConverter.";

	// Token: 0x04000587 RID: 1415
	public const string Config_base_unknown_format = "Unknown";

	// Token: 0x04000588 RID: 1416
	public const string Config_base_unrecognized_attribute = "Unrecognized attribute '{0}'. Note that attribute names are case-sensitive.";

	// Token: 0x04000589 RID: 1417
	public const string Config_base_unrecognized_element = "Unrecognized element.";

	// Token: 0x0400058A RID: 1418
	public const string Config_invalid_boolean_attribute = "The property '{0}' must have value 'true' or 'false'.";

	// Token: 0x0400058B RID: 1419
	public const string Config_invalid_integer_attribute = "The '{0}' attribute must be set to an integer value.";

	// Token: 0x0400058C RID: 1420
	public const string Config_invalid_positive_integer_attribute = "The '{0}' attribute must be set to a positive integer value.";

	// Token: 0x0400058D RID: 1421
	public const string Config_invalid_type_attribute = "The '{0}' attribute must be set to a valid Type name.";

	// Token: 0x0400058E RID: 1422
	public const string Config_missing_required_attribute = "The '{0}' attribute must be specified on the '{1}' tag.";

	// Token: 0x0400058F RID: 1423
	public const string Config_name_value_file_section_file_invalid_root = "The root element must match the name of the section referencing the file, '{0}'";

	// Token: 0x04000590 RID: 1424
	public const string Config_provider_must_implement_type = "Provider must implement the class '{0}'.";

	// Token: 0x04000591 RID: 1425
	public const string Config_provider_name_null_or_empty = "Provider name cannot be null or empty.";

	// Token: 0x04000592 RID: 1426
	public const string Config_provider_not_found = "The provider was not found in the collection.";

	// Token: 0x04000593 RID: 1427
	public const string Config_property_name_cannot_be_empty = "Property '{0}' cannot be empty or null.";

	// Token: 0x04000594 RID: 1428
	public const string Config_section_cannot_clear_locked_section = "Cannot clear section handlers.  Section '{0}' is locked.";

	// Token: 0x04000595 RID: 1429
	public const string Config_section_record_not_found = "SectionRecord not found.";

	// Token: 0x04000596 RID: 1430
	public const string Config_source_cannot_contain_file = "The 'File' property cannot be used with the ConfigSource property.";

	// Token: 0x04000597 RID: 1431
	public const string Config_system_already_set = "The configuration system can only be set once.  Configuration system is already set";

	// Token: 0x04000598 RID: 1432
	public const string Config_unable_to_read_security_policy = "Unable to read security policy.";

	// Token: 0x04000599 RID: 1433
	public const string Config_write_xml_returned_null = "WriteXml returned null.";

	// Token: 0x0400059A RID: 1434
	public const string Cannot_clear_sections_within_group = "Server cannot clear configuration sections from within section groups.  <clear/> must be a child of <configSections>.";

	// Token: 0x0400059B RID: 1435
	public const string Cannot_exit_up_top_directory = "Cannot use a leading .. to exit above the top directory.";

	// Token: 0x0400059C RID: 1436
	public const string Could_not_create_listener = "Couldn't create listener '{0}'.";

	// Token: 0x0400059D RID: 1437
	public const string TL_InitializeData_NotSpecified = "initializeData needs to be valid for this TraceListener.";

	// Token: 0x0400059E RID: 1438
	public const string Could_not_create_type_instance = "Could not create {0}.";

	// Token: 0x0400059F RID: 1439
	public const string Could_not_find_type = "Couldn't find type for class {0}.";

	// Token: 0x040005A0 RID: 1440
	public const string Could_not_get_constructor = "Couldn't find constructor for class {0}.";

	// Token: 0x040005A1 RID: 1441
	public const string EmptyTypeName_NotAllowed = "switchType needs to be a valid class name. It can't be empty.";

	// Token: 0x040005A2 RID: 1442
	public const string Incorrect_base_type = "The specified type, '{0}' is not derived from the appropriate base type, '{1}'.";

	// Token: 0x040005A3 RID: 1443
	public const string Only_specify_one = "'switchValue' and 'switchName' cannot both be specified on source '{0}'.";

	// Token: 0x040005A4 RID: 1444
	public const string Provider_Already_Initialized = "This provider instance has already been initialized.";

	// Token: 0x040005A5 RID: 1445
	public const string Reference_listener_cant_have_properties = "A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.";

	// Token: 0x040005A6 RID: 1446
	public const string Reference_to_nonexistent_listener = "Listener '{0}' does not exist in the sharedListeners section.";

	// Token: 0x040005A7 RID: 1447
	public const string SettingsPropertyNotFound = "The settings property '{0}' was not found.";

	// Token: 0x040005A8 RID: 1448
	public const string SettingsPropertyReadOnly = "The settings property '{0}' is read-only.";

	// Token: 0x040005A9 RID: 1449
	public const string SettingsPropertyWrongType = "The settings property '{0}' is of a non-compatible type.";

	// Token: 0x040005AA RID: 1450
	public const string Type_isnt_tracelistener = "Could not add trace listener {0} because it is not a subclass of TraceListener.";

	// Token: 0x040005AB RID: 1451
	public const string Unable_to_convert_type_from_string = "Could not find a type-converter to convert object if type '{0}' from string.";

	// Token: 0x040005AC RID: 1452
	public const string Unable_to_convert_type_to_string = "Could not find a type-converter to convert object if type '{0}' to string.";

	// Token: 0x040005AD RID: 1453
	public const string Value_must_be_numeric = "Error in trace switch '{0}': The value of a switch must be integral.";

	// Token: 0x040005AE RID: 1454
	public const string Could_not_create_from_default_value = "The property '{0}' could not be created from it's default value. Error message: {1}";

	// Token: 0x040005AF RID: 1455
	public const string Could_not_create_from_default_value_2 = "The property '{0}' could not be created from it's default value because the default value is of a different type.";

	// Token: 0x040005B0 RID: 1456
	public const string InvalidDirName = "The directory name {0} is invalid.";

	// Token: 0x040005B1 RID: 1457
	public const string FSW_IOError = "Error reading the {0} directory.";

	// Token: 0x040005B2 RID: 1458
	public const string PatternInvalidChar = "The character '{0}' in the pattern provided is not valid.";

	// Token: 0x040005B3 RID: 1459
	public const string BufferSizeTooLarge = "The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer.";

	// Token: 0x040005B4 RID: 1460
	public const string FSW_ChangedFilter = "Flag to indicate which change event to monitor.";

	// Token: 0x040005B5 RID: 1461
	public const string FSW_Enabled = "Flag to indicate whether this component is active or not.";

	// Token: 0x040005B6 RID: 1462
	public const string FSW_Filter = "The file pattern filter.";

	// Token: 0x040005B7 RID: 1463
	public const string FSW_IncludeSubdirectories = "Flag to watch subdirectories.";

	// Token: 0x040005B8 RID: 1464
	public const string FSW_Path = "The path to the directory to monitor.";

	// Token: 0x040005B9 RID: 1465
	public const string FSW_SynchronizingObject = "The object used to marshal the event handler calls issued as a result of a Directory change.";

	// Token: 0x040005BA RID: 1466
	public const string FSW_Changed = "Occurs when a file and/or directory change matches the filter.";

	// Token: 0x040005BB RID: 1467
	public const string FSW_Created = "Occurs when a file and/or directory creation matches the filter.";

	// Token: 0x040005BC RID: 1468
	public const string FSW_Deleted = "Occurs when a file and/or directory deletion matches the filter.";

	// Token: 0x040005BD RID: 1469
	public const string FSW_Renamed = "Occurs when a file and/or directory rename matches the filter.";

	// Token: 0x040005BE RID: 1470
	public const string FSW_BufferOverflow = "Too many changes at once in directory:{0}.";

	// Token: 0x040005BF RID: 1471
	public const string FileSystemWatcherDesc = "Monitors file system change notifications and raises events when a directory or file changes.";

	// Token: 0x040005C0 RID: 1472
	public const string NotSet = "[Not Set]";

	// Token: 0x040005C1 RID: 1473
	public const string TimerAutoReset = "Indicates whether the timer will be restarted when it is enabled.";

	// Token: 0x040005C2 RID: 1474
	public const string TimerEnabled = "Indicates whether the timer is enabled to fire events at a defined interval.";

	// Token: 0x040005C3 RID: 1475
	public const string TimerInterval = "The number of milliseconds between timer events.";

	// Token: 0x040005C4 RID: 1476
	public const string TimerIntervalElapsed = "Occurs when the Interval has elapsed.";

	// Token: 0x040005C5 RID: 1477
	public const string TimerSynchronizingObject = "The object used to marshal the event handler calls issued when an interval has elapsed.";

	// Token: 0x040005C6 RID: 1478
	public const string MismatchedCounterTypes = "Mismatched counter types.";

	// Token: 0x040005C7 RID: 1479
	public const string NoPropertyForAttribute = "Could not find a property for the attribute '{0}'.";

	// Token: 0x040005C8 RID: 1480
	public const string InvalidAttributeType = "The value of attribute '{0}' could not be converted to the proper type.";

	// Token: 0x040005C9 RID: 1481
	public const string Generic_ArgCantBeEmptyString = "'{0}' can not be empty string.";

	// Token: 0x040005CA RID: 1482
	public const string BadLogName = "Event log names must consist of printable characters and cannot contain \\, *, ?, or spaces";

	// Token: 0x040005CB RID: 1483
	public const string InvalidProperty = "Invalid value {1} for property {0}.";

	// Token: 0x040005CC RID: 1484
	public const string CantMonitorEventLog = "Cannot monitor EntryWritten events for this EventLog. This might be because the EventLog is on a remote machine which is not a supported scenario.";

	// Token: 0x040005CD RID: 1485
	public const string InitTwice = "Cannot initialize the same object twice.";

	// Token: 0x040005CE RID: 1486
	public const string InvalidParameter = "Invalid value '{1}' for parameter '{0}'.";

	// Token: 0x040005CF RID: 1487
	public const string MissingParameter = "Must specify value for {0}.";

	// Token: 0x040005D0 RID: 1488
	public const string ParameterTooLong = "The size of {0} is too big. It cannot be longer than {1} characters.";

	// Token: 0x040005D1 RID: 1489
	public const string LocalSourceAlreadyExists = "Source {0} already exists on the local computer.";

	// Token: 0x040005D2 RID: 1490
	public const string SourceAlreadyExists = "Source {0} already exists on the computer '{1}'.";

	// Token: 0x040005D3 RID: 1491
	public const string LocalLogAlreadyExistsAsSource = "Log {0} has already been registered as a source on the local computer.";

	// Token: 0x040005D4 RID: 1492
	public const string LogAlreadyExistsAsSource = "Log {0} has already been registered as a source on the computer '{1}'.";

	// Token: 0x040005D5 RID: 1493
	public const string DuplicateLogName = "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.";

	// Token: 0x040005D6 RID: 1494
	public const string RegKeyMissing = "Cannot open registry key {0}\\{1}\\{2} on computer '{3}'.";

	// Token: 0x040005D7 RID: 1495
	public const string LocalRegKeyMissing = "Cannot open registry key {0}\\{1}\\{2}.";

	// Token: 0x040005D8 RID: 1496
	public const string RegKeyMissingShort = "Cannot open registry key {0} on computer {1}.";

	// Token: 0x040005D9 RID: 1497
	public const string InvalidParameterFormat = "Invalid format for argument {0}.";

	// Token: 0x040005DA RID: 1498
	public const string NoLogName = "Log to delete was not specified.";

	// Token: 0x040005DB RID: 1499
	public const string RegKeyNoAccess = "Cannot open registry key {0} on computer {1}. You might not have access.";

	// Token: 0x040005DC RID: 1500
	public const string MissingLog = "Cannot find Log {0} on computer '{1}'.";

	// Token: 0x040005DD RID: 1501
	public const string SourceNotRegistered = "The source '{0}' is not registered on machine '{1}', or you do not have write access to the {2} registry key.";

	// Token: 0x040005DE RID: 1502
	public const string LocalSourceNotRegistered = "Source {0} is not registered on the local computer.";

	// Token: 0x040005DF RID: 1503
	public const string CantRetrieveEntries = "Cannot retrieve all entries.";

	// Token: 0x040005E0 RID: 1504
	public const string IndexOutOfBounds = "Index {0} is out of bounds.";

	// Token: 0x040005E1 RID: 1505
	public const string CantReadLogEntryAt = "Cannot read log entry number {0}.  The event log may be corrupt.";

	// Token: 0x040005E2 RID: 1506
	public const string MissingLogProperty = "Log property value has not been specified.";

	// Token: 0x040005E3 RID: 1507
	public const string CantOpenLog = "Cannot open log {0} on machine {1}. Windows has not provided an error code.";

	// Token: 0x040005E4 RID: 1508
	public const string NeedSourceToOpen = "Source property was not set before opening the event log in write mode.";

	// Token: 0x040005E5 RID: 1509
	public const string NeedSourceToWrite = "Source property was not set before writing to the event log.";

	// Token: 0x040005E6 RID: 1510
	public const string CantOpenLogAccess = "Cannot open log for source '{0}'. You may not have write access.";

	// Token: 0x040005E7 RID: 1511
	public const string LogEntryTooLong = "Log entry string is too long. A string written to the event log cannot exceed 32766 characters.";

	// Token: 0x040005E8 RID: 1512
	public const string TooManyReplacementStrings = "The maximum allowed number of replacement strings is 255.";

	// Token: 0x040005E9 RID: 1513
	public const string LogSourceMismatch = "The source '{0}' is not registered in log '{1}'. (It is registered in log '{2}'.) \" The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.";

	// Token: 0x040005EA RID: 1514
	public const string NoAccountInfo = "Cannot obtain account information.";

	// Token: 0x040005EB RID: 1515
	public const string NoCurrentEntry = "No current EventLog entry available, cursor is located before the first or after the last element of the enumeration.";

	// Token: 0x040005EC RID: 1516
	public const string MessageNotFormatted = "The description for Event ID '{0}' in Source '{1}' cannot be found.  The local computer may not have the necessary registry information or message DLL files to display the message, or you may not have permission to access them.  The following information is part of the event:";

	// Token: 0x040005ED RID: 1517
	public const string EventID = "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.";

	// Token: 0x040005EE RID: 1518
	public const string LogDoesNotExists = "The event log '{0}' on computer '{1}' does not exist.";

	// Token: 0x040005EF RID: 1519
	public const string InvalidCustomerLogName = "The log name: '{0}' is invalid for customer log creation.";

	// Token: 0x040005F0 RID: 1520
	public const string CannotDeleteEqualSource = "The event log source '{0}' cannot be deleted, because it's equal to the log name.";

	// Token: 0x040005F1 RID: 1521
	public const string RentionDaysOutOfRange = "'retentionDays' must be between 1 and 365 days.";

	// Token: 0x040005F2 RID: 1522
	public const string MaximumKilobytesOutOfRange = "MaximumKilobytes must be between 64 KB and 4 GB, and must be in 64K increments.";

	// Token: 0x040005F3 RID: 1523
	public const string SomeLogsInaccessible = "The source was not found, but some or all event logs could not be searched.  Inaccessible logs: {0}.";

	// Token: 0x040005F4 RID: 1524
	public const string SomeLogsInaccessibleToCreate = "The source was not found, but some or all event logs could not be searched.  To create the source, you need permission to read all event logs to make sure that the new source name is unique.  Inaccessible logs: {0}.";

	// Token: 0x040005F5 RID: 1525
	public const string BadConfigSwitchValue = "The config value for Switch '{0}' was invalid.";

	// Token: 0x040005F6 RID: 1526
	public const string ConfigSectionsUnique = "The '{0}' section can only appear once per config file.";

	// Token: 0x040005F7 RID: 1527
	public const string ConfigSectionsUniquePerSection = "The '{0}' tag can only appear once per section.";

	// Token: 0x040005F8 RID: 1528
	public const string SourceListenerDoesntExist = "The listener '{0}' added to source '{1}' must have a listener with the same name defined in the main Trace listeners section.";

	// Token: 0x040005F9 RID: 1529
	public const string SourceSwitchDoesntExist = "The source '{0}' must have a switch with the same name defined in the Switches section.";

	// Token: 0x040005FA RID: 1530
	public const string CategoryHelpCorrupt = "Cannot load Category Help data because an invalid index '{0}' was read from the registry.";

	// Token: 0x040005FB RID: 1531
	public const string CounterNameCorrupt = "Cannot load Counter Name data because an invalid index '{0}' was read from the registry.";

	// Token: 0x040005FC RID: 1532
	public const string CounterDataCorrupt = "Cannot load Performance Counter data because an unexpected registry key value type was read from '{0}'.";

	// Token: 0x040005FD RID: 1533
	public const string ReadOnlyCounter = "Cannot update Performance Counter, this object has been initialized as ReadOnly.";

	// Token: 0x040005FE RID: 1534
	public const string ReadOnlyRemoveInstance = "Cannot remove Performance Counter Instance, this object as been initialized as ReadOnly.";

	// Token: 0x040005FF RID: 1535
	public const string NotCustomCounter = "The requested Performance Counter is not a custom counter, it has to be initialized as ReadOnly.";

	// Token: 0x04000600 RID: 1536
	public const string CategoryNameMissing = "Failed to initialize because CategoryName is missing.";

	// Token: 0x04000601 RID: 1537
	public const string CounterNameMissing = "Failed to initialize because CounterName is missing.";

	// Token: 0x04000602 RID: 1538
	public const string InstanceNameProhibited = "Counter is single instance, instance name '{0}' is not valid for this counter category.";

	// Token: 0x04000603 RID: 1539
	public const string InstanceNameRequired = "Counter is not single instance, an instance name needs to be specified.";

	// Token: 0x04000604 RID: 1540
	public const string MissingInstance = "Instance {0} does not exist in category {1}.";

	// Token: 0x04000605 RID: 1541
	public const string PerformanceCategoryExists = "Cannot create Performance Category '{0}' because it already exists.";

	// Token: 0x04000606 RID: 1542
	public const string InvalidCounterName = "Invalid empty or null string for counter name.";

	// Token: 0x04000607 RID: 1543
	public const string DuplicateCounterName = "Cannot create Performance Category with counter name {0} because the name is a duplicate.";

	// Token: 0x04000608 RID: 1544
	public const string CantChangeCategoryRegistration = "Cannot create or delete the Performance Category '{0}' because access is denied.";

	// Token: 0x04000609 RID: 1545
	public const string CantDeleteCategory = "Cannot delete Performance Category because this category is not registered or is a system category.";

	// Token: 0x0400060A RID: 1546
	public const string MissingCategory = "Category does not exist.";

	// Token: 0x0400060B RID: 1547
	public const string MissingCategoryDetail = "Category {0} does not exist.";

	// Token: 0x0400060C RID: 1548
	public const string CantReadCategory = "Cannot read Category {0}.";

	// Token: 0x0400060D RID: 1549
	public const string MissingCounter = "Counter {0} does not exist.";

	// Token: 0x0400060E RID: 1550
	public const string CategoryNameNotSet = "Category name property has not been set.";

	// Token: 0x0400060F RID: 1551
	public const string CounterExists = "Could not locate Performance Counter with specified category name '{0}', counter name '{1}'.";

	// Token: 0x04000610 RID: 1552
	public const string CantReadCategoryIndex = "Could not Read Category Index: {0}.";

	// Token: 0x04000611 RID: 1553
	public const string CantReadCounter = "Counter '{0}' does not exist in the specified Category.";

	// Token: 0x04000612 RID: 1554
	public const string CantReadInstance = "Instance '{0}' does not exist in the specified Category.";

	// Token: 0x04000613 RID: 1555
	public const string RemoteWriting = "Cannot write to a Performance Counter in a remote machine.";

	// Token: 0x04000614 RID: 1556
	public const string CounterLayout = "The Counter layout for the Category specified is invalid, a counter of the type:  AverageCount64, AverageTimer32, CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, CounterMultiTimer100NsInverse, RawFraction, or SampleFraction has to be immediately followed by any of the base counter types: AverageBase, CounterMultiBase, RawBase or SampleBase.";

	// Token: 0x04000615 RID: 1557
	public const string PossibleDeadlock = "The operation couldn't be completed, potential internal deadlock.";

	// Token: 0x04000616 RID: 1558
	public const string SharedMemoryGhosted = "Cannot access shared memory, AppDomain has been unloaded.";

	// Token: 0x04000617 RID: 1559
	public const string HelpNotAvailable = "Help not available.";

	// Token: 0x04000618 RID: 1560
	public const string PerfInvalidHelp = "Invalid help string. Its length must be in the range between '{0}' and '{1}'.";

	// Token: 0x04000619 RID: 1561
	public const string PerfInvalidCounterName = "Invalid counter name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed.";

	// Token: 0x0400061A RID: 1562
	public const string PerfInvalidCategoryName = "Invalid category name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed.";

	// Token: 0x0400061B RID: 1563
	public const string MustAddCounterCreationData = "Only objects of type CounterCreationData can be added to a CounterCreationDataCollection.";

	// Token: 0x0400061C RID: 1564
	public const string RemoteCounterAdmin = "Creating or Deleting Performance Counter Categories on remote machines is not supported.";

	// Token: 0x0400061D RID: 1565
	public const string NoInstanceInformation = "The {0} category doesn't provide any instance information, no accurate data can be returned.";

	// Token: 0x0400061E RID: 1566
	public const string PerfCounterPdhError = "There was an error calculating the PerformanceCounter value (0x{0}).";

	// Token: 0x0400061F RID: 1567
	public const string MultiInstanceOnly = "Category '{0}' is marked as multi-instance.  Performance counters in this category can only be created with instance names.";

	// Token: 0x04000620 RID: 1568
	public const string SingleInstanceOnly = "Category '{0}' is marked as single-instance.  Performance counters in this category can only be created without instance names.";

	// Token: 0x04000621 RID: 1569
	public const string InstanceNameTooLong = "Instance names used for writing to custom counters must be 127 characters or less.";

	// Token: 0x04000622 RID: 1570
	public const string CategoryNameTooLong = "Category names must be 1024 characters or less.";

	// Token: 0x04000623 RID: 1571
	public const string InstanceLifetimeProcessonReadOnly = "InstanceLifetime is unused by ReadOnly counters.";

	// Token: 0x04000624 RID: 1572
	public const string InstanceLifetimeProcessforSingleInstance = "Single instance categories are only valid with the Global lifetime.";

	// Token: 0x04000625 RID: 1573
	public const string InstanceAlreadyExists = "Instance '{0}' already exists with a lifetime of Process.  It cannot be recreated or reused until it has been removed or until the process using it has exited.";

	// Token: 0x04000626 RID: 1574
	public const string CantSetLifetimeAfterInitialized = "The InstanceLifetime cannot be set after the instance has been initialized.  You must use the default constructor and set the CategoryName, InstanceName, CounterName, InstanceLifetime and ReadOnly properties manually before setting the RawValue.";

	// Token: 0x04000627 RID: 1575
	public const string ProcessLifetimeNotValidInGlobal = "PerformanceCounterInstanceLifetime.Process is not valid in the global shared memory.  If your performance counter category was created with an older version of the Framework, it uses the global shared memory.  Either use PerformanceCounterInstanceLifetime.Global, or if applications running on older versions of the Framework do not need to write to your category, delete and recreate it.";

	// Token: 0x04000628 RID: 1576
	public const string CantConvertProcessToGlobal = "An instance with a lifetime of Process can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Process.";

	// Token: 0x04000629 RID: 1577
	public const string CantConvertGlobalToProcess = "An instance with a lifetime of Global can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Global.";

	// Token: 0x0400062A RID: 1578
	public const string PCNotSupportedUnderAppContainer = "Writeable performance counters are not allowed when running in AppContainer.";

	// Token: 0x0400062B RID: 1579
	public const string PriorityClassNotSupported = "The AboveNormal and BelowNormal priority classes are not available on this platform.";

	// Token: 0x0400062C RID: 1580
	public const string WinNTRequired = "Feature requires Windows NT.";

	// Token: 0x0400062D RID: 1581
	public const string Win2kRequired = "Feature requires Windows 2000.";

	// Token: 0x0400062E RID: 1582
	public const string NoAssociatedProcess = "No process is associated with this object.";

	// Token: 0x0400062F RID: 1583
	public const string ProcessIdRequired = "Feature requires a process identifier.";

	// Token: 0x04000630 RID: 1584
	public const string NotSupportedRemote = "Feature is not supported for remote machines.";

	// Token: 0x04000631 RID: 1585
	public const string NoProcessInfo = "Process has exited, so the requested information is not available.";

	// Token: 0x04000632 RID: 1586
	public const string WaitTillExit = "Process must exit before requested information can be determined.";

	// Token: 0x04000633 RID: 1587
	public const string NoProcessHandle = "Process was not started by this object, so requested information cannot be determined.";

	// Token: 0x04000634 RID: 1588
	public const string MissingProccess = "Process with an Id of {0} is not running.";

	// Token: 0x04000635 RID: 1589
	public const string BadMinWorkset = "Minimum working set size is invalid. It must be less than or equal to the maximum working set size.";

	// Token: 0x04000636 RID: 1590
	public const string BadMaxWorkset = "Maximum working set size is invalid. It must be greater than or equal to the minimum working set size.";

	// Token: 0x04000637 RID: 1591
	public const string WinNTRequiredForRemote = "Operating system does not support accessing processes on remote computers. This feature requires Windows NT or later.";

	// Token: 0x04000638 RID: 1592
	public const string ProcessHasExited = "Cannot process request because the process ({0}) has exited.";

	// Token: 0x04000639 RID: 1593
	public const string ProcessHasExitedNoId = "Cannot process request because the process has exited.";

	// Token: 0x0400063A RID: 1594
	public const string ThreadExited = "The request cannot be processed because the thread ({0}) has exited.";

	// Token: 0x0400063B RID: 1595
	public const string Win2000Required = "Feature requires Windows 2000 or later.";

	// Token: 0x0400063C RID: 1596
	public const string ProcessNotFound = "Thread {0} found, but no process {1} found.";

	// Token: 0x0400063D RID: 1597
	public const string CantGetProcessId = "Cannot retrieve process identifier from the process handle.";

	// Token: 0x0400063E RID: 1598
	public const string ProcessDisabled = "Process performance counter is disabled, so the requested operation cannot be performed.";

	// Token: 0x0400063F RID: 1599
	public const string WaitReasonUnavailable = "WaitReason is only available if the ThreadState is Wait.";

	// Token: 0x04000640 RID: 1600
	public const string NotSupportedRemoteThread = "Feature is not supported for threads on remote computers.";

	// Token: 0x04000641 RID: 1601
	public const string UseShellExecuteRequiresSTA = "Current thread is not in Single Thread Apartment (STA) mode. Starting a process with UseShellExecute set to True requires the current thread be in STA mode.  Ensure that your Main function has STAThreadAttribute marked.";

	// Token: 0x04000642 RID: 1602
	public const string CantRedirectStreams = "The Process object must have the UseShellExecute property set to false in order to redirect IO streams.";

	// Token: 0x04000643 RID: 1603
	public const string CantUseEnvVars = "The Process object must have the UseShellExecute property set to false in order to use environment variables.";

	// Token: 0x04000644 RID: 1604
	public const string CantStartAsUser = "The Process object must have the UseShellExecute property set to false in order to start a process as a user.";

	// Token: 0x04000645 RID: 1605
	public const string CouldntConnectToRemoteMachine = "Couldn't connect to remote machine.";

	// Token: 0x04000646 RID: 1606
	public const string CouldntGetProcessInfos = "Couldn't get process information from performance counter.";

	// Token: 0x04000647 RID: 1607
	public const string InputIdleUnkownError = "WaitForInputIdle failed.  This could be because the process does not have a graphical interface.";

	// Token: 0x04000648 RID: 1608
	public const string FileNameMissing = "Cannot start process because a file name has not been provided.";

	// Token: 0x04000649 RID: 1609
	public const string EnvironmentBlock = "The environment block provided doesn't have the correct format.";

	// Token: 0x0400064A RID: 1610
	public const string EnumProcessModuleFailed = "Unable to enumerate the process modules.";

	// Token: 0x0400064B RID: 1611
	public const string EnumProcessModuleFailedDueToWow = "A 32 bit processes cannot access modules of a 64 bit process.";

	// Token: 0x0400064C RID: 1612
	public const string PendingAsyncOperation = "An async read operation has already been started on the stream.";

	// Token: 0x0400064D RID: 1613
	public const string NoAsyncOperation = "No async read operation is in progress on the stream.";

	// Token: 0x0400064E RID: 1614
	public const string InvalidApplication = "The specified executable is not a valid application for this OS platform.";

	// Token: 0x0400064F RID: 1615
	public const string StandardOutputEncodingNotAllowed = "StandardOutputEncoding is only supported when standard output is redirected.";

	// Token: 0x04000650 RID: 1616
	public const string StandardErrorEncodingNotAllowed = "StandardErrorEncoding is only supported when standard error is redirected.";

	// Token: 0x04000651 RID: 1617
	public const string CountersOOM = "Custom counters file view is out of memory.";

	// Token: 0x04000652 RID: 1618
	public const string MappingCorrupted = "Cannot continue the current operation, the performance counters memory mapping has been corrupted.";

	// Token: 0x04000653 RID: 1619
	public const string SetSecurityDescriptorFailed = "Cannot initialize security descriptor initialized.";

	// Token: 0x04000654 RID: 1620
	public const string CantCreateFileMapping = "Cannot create file mapping.";

	// Token: 0x04000655 RID: 1621
	public const string CantMapFileView = "Cannot map view of file.";

	// Token: 0x04000656 RID: 1622
	public const string CantGetMappingSize = "Cannot calculate the size of the file view.";

	// Token: 0x04000657 RID: 1623
	public const string CantGetStandardOut = "StandardOut has not been redirected or the process hasn't started yet.";

	// Token: 0x04000658 RID: 1624
	public const string CantGetStandardIn = "StandardIn has not been redirected.";

	// Token: 0x04000659 RID: 1625
	public const string CantGetStandardError = "StandardError has not been redirected.";

	// Token: 0x0400065A RID: 1626
	public const string CantMixSyncAsyncOperation = "Cannot mix synchronous and asynchronous operation on process stream.";

	// Token: 0x0400065B RID: 1627
	public const string NoFileMappingSize = "Cannot retrieve file mapping size while initializing configuration settings.";

	// Token: 0x0400065C RID: 1628
	public const string EnvironmentBlockTooLong = "The environment block used to start a process cannot be longer than 65535 bytes.  Your environment block is {0} bytes long.  Remove some environment variables and try again.";

	// Token: 0x0400065D RID: 1629
	public const string Arg_SecurityException = "The port name cannot start with '\\'.";

	// Token: 0x0400065E RID: 1630
	public const string ArgumentNull_Array = "Array cannot be null.";

	// Token: 0x0400065F RID: 1631
	public const string ArgumentNull_Buffer = "Buffer cannot be null.";

	// Token: 0x04000660 RID: 1632
	public const string IO_UnknownError = "Unknown Error '{0}'.";

	// Token: 0x04000661 RID: 1633
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x04000662 RID: 1634
	public const string ObjectDisposed_WriterClosed = "Can not write to a closed TextWriter.";

	// Token: 0x04000663 RID: 1635
	public const string NotSupportedOS = "GetPortNames is not supported on Win9x platforms.";

	// Token: 0x04000664 RID: 1636
	public const string BaudRate = "The baud rate to use on this serial port.";

	// Token: 0x04000665 RID: 1637
	public const string DataBits = "The number of data bits per transmitted/received byte.";

	// Token: 0x04000666 RID: 1638
	public const string DiscardNull = "Whether to discard null bytes received on the port before adding to serial buffer.";

	// Token: 0x04000667 RID: 1639
	public const string DtrEnable = "Whether to enable the Data Terminal Ready (DTR) line during communications.";

	// Token: 0x04000668 RID: 1640
	public const string EncodingMonitoringDescription = "The encoding to use when reading and writing strings.";

	// Token: 0x04000669 RID: 1641
	public const string Handshake = "The handshaking protocol for flow control in data exchange, which can be None.";

	// Token: 0x0400066A RID: 1642
	public const string NewLine = "The string used by ReadLine and WriteLine to denote a new line.";

	// Token: 0x0400066B RID: 1643
	public const string Parity = "The scheme for parity checking each received byte and marking each transmitted byte.";

	// Token: 0x0400066C RID: 1644
	public const string ParityReplace = "Byte with which to replace bytes received with parity errors.";

	// Token: 0x0400066D RID: 1645
	public const string PortName = "The name of the communications port to open.";

	// Token: 0x0400066E RID: 1646
	public const string ReadBufferSize = "The size of the read buffer in bytes.  This is the maximum number of read bytes which can be buffered.";

	// Token: 0x0400066F RID: 1647
	public const string ReadTimeout = "The read timeout in Milliseconds.";

	// Token: 0x04000670 RID: 1648
	public const string ReceivedBytesThreshold = "Number of bytes required to be available before the Read event is fired.";

	// Token: 0x04000671 RID: 1649
	public const string RtsEnable = "Whether to enable the Request To Send (RTS) line during communications.";

	// Token: 0x04000672 RID: 1650
	public const string SerialPortDesc = "Represents a serial port resource.";

	// Token: 0x04000673 RID: 1651
	public const string StopBits = "The number of stop bits per transmitted/received byte.";

	// Token: 0x04000674 RID: 1652
	public const string WriteBufferSize = "The size of the write buffer in bytes.  This is the maximum number of bytes which can be queued for write.";

	// Token: 0x04000675 RID: 1653
	public const string WriteTimeout = "The write timeout in milliseconds.";

	// Token: 0x04000676 RID: 1654
	public const string SerialErrorReceived = "Raised each time when an error is received from the SerialPort.";

	// Token: 0x04000677 RID: 1655
	public const string SerialPinChanged = "Raised each time when pin is changed on the SerialPort.";

	// Token: 0x04000678 RID: 1656
	public const string SerialDataReceived = "Raised each time when data is received from the SerialPort.";

	// Token: 0x04000679 RID: 1657
	public const string CounterType = "The type of this counter.";

	// Token: 0x0400067A RID: 1658
	public const string CounterName = "The name of this counter.";

	// Token: 0x0400067B RID: 1659
	public const string CounterHelp = "Help information for this counter.";

	// Token: 0x0400067C RID: 1660
	public const string EventLogDesc = "Provides interaction with Windows event logs.";

	// Token: 0x0400067D RID: 1661
	public const string ErrorDataReceived = "User event handler to call for async IO with StandardError stream.";

	// Token: 0x0400067E RID: 1662
	public const string LogEntries = "The contents of the log.";

	// Token: 0x0400067F RID: 1663
	public const string LogLog = "Gets or sets the name of the log to read from and write to.";

	// Token: 0x04000680 RID: 1664
	public const string LogMachineName = "The name of the machine on which to read or write events.";

	// Token: 0x04000681 RID: 1665
	public const string LogMonitoring = "Indicates if the component monitors the event log for changes.";

	// Token: 0x04000682 RID: 1666
	public const string LogSynchronizingObject = "The object used to marshal the event handler calls issued as a result of an EventLog change.";

	// Token: 0x04000683 RID: 1667
	public const string LogSource = "The application name (source name) to use when writing to the event log.";

	// Token: 0x04000684 RID: 1668
	public const string LogEntryWritten = "Raised each time any application writes an entry to the event log.";

	// Token: 0x04000685 RID: 1669
	public const string LogEntryMachineName = "The machine on which this event log resides.";

	// Token: 0x04000686 RID: 1670
	public const string LogEntryData = "The binary data associated with this entry in the event log.";

	// Token: 0x04000687 RID: 1671
	public const string LogEntryIndex = "The sequence of this entry in the event log.";

	// Token: 0x04000688 RID: 1672
	public const string LogEntryCategory = "The category for this message.";

	// Token: 0x04000689 RID: 1673
	public const string LogEntryCategoryNumber = "An application-specific category number assigned to this entry.";

	// Token: 0x0400068A RID: 1674
	public const string LogEntryEventID = "The number identifying the message for this source.";

	// Token: 0x0400068B RID: 1675
	public const string LogEntryEntryType = "The type of entry - Information, Warning, etc.";

	// Token: 0x0400068C RID: 1676
	public const string LogEntryMessage = "The text of the message for this entry";

	// Token: 0x0400068D RID: 1677
	public const string LogEntrySource = "The name of the application that wrote this entry.";

	// Token: 0x0400068E RID: 1678
	public const string LogEntryReplacementStrings = "The application-supplied strings used in the message.";

	// Token: 0x0400068F RID: 1679
	public const string LogEntryResourceId = "The full number identifying the message in the event message dll.";

	// Token: 0x04000690 RID: 1680
	public const string LogEntryTimeGenerated = "The time at which the application logged this entry.";

	// Token: 0x04000691 RID: 1681
	public const string LogEntryTimeWritten = "The time at which the system logged this entry to the event log.";

	// Token: 0x04000692 RID: 1682
	public const string LogEntryUserName = "The username of the account associated with this entry by the writing application.";

	// Token: 0x04000693 RID: 1683
	public const string OutputDataReceived = "User event handler to call for async IO with StandardOutput stream.";

	// Token: 0x04000694 RID: 1684
	public const string PC_CounterHelp = "The description message for this counter.";

	// Token: 0x04000695 RID: 1685
	public const string PC_CounterType = "The counter type indicates how to interpret the value of the counter, for example an actual count or a rate of change.";

	// Token: 0x04000696 RID: 1686
	public const string PC_ReadOnly = "Indicates if the counter is read only.  Remote counters and counters not created using this component are read-only.";

	// Token: 0x04000697 RID: 1687
	public const string PC_RawValue = "Directly accesses the raw value of this counter.  The counter must have been created using this component.";

	// Token: 0x04000698 RID: 1688
	public const string ProcessAssociated = "Indicates if the process component is associated with a real process.";

	// Token: 0x04000699 RID: 1689
	public const string ProcessDesc = "Provides access to local and remote processes, enabling starting and stopping of local processes.";

	// Token: 0x0400069A RID: 1690
	public const string ProcessExitCode = "The value returned from the associated process when it terminated.";

	// Token: 0x0400069B RID: 1691
	public const string ProcessTerminated = "Indicates if the associated process has been terminated.";

	// Token: 0x0400069C RID: 1692
	public const string ProcessExitTime = "The time that the associated process exited.";

	// Token: 0x0400069D RID: 1693
	public const string ProcessHandle = "Returns the native handle for this process.   The handle is only available if the process was started using this component.";

	// Token: 0x0400069E RID: 1694
	public const string ProcessHandleCount = "The number of native handles associated with this process.";

	// Token: 0x0400069F RID: 1695
	public const string ProcessId = "The unique identifier for the process.";

	// Token: 0x040006A0 RID: 1696
	public const string ProcessMachineName = "The name of the machine the running the process.";

	// Token: 0x040006A1 RID: 1697
	public const string ProcessMainModule = "The main module for the associated process.";

	// Token: 0x040006A2 RID: 1698
	public const string ProcessModules = "The modules that have been loaded by the associated process.";

	// Token: 0x040006A3 RID: 1699
	public const string ProcessSynchronizingObject = "The object used to marshal the event handler calls issued as a result of a Process exit.";

	// Token: 0x040006A4 RID: 1700
	public const string ProcessSessionId = "The identifier for the session of the process.";

	// Token: 0x040006A5 RID: 1701
	public const string ProcessThreads = "The threads running in the associated process.";

	// Token: 0x040006A6 RID: 1702
	public const string ProcessEnableRaisingEvents = "Whether the process component should watch for the associated process to exit, and raise the Exited event.";

	// Token: 0x040006A7 RID: 1703
	public const string ProcessExited = "If the WatchForExit property is set to true, then this event is raised when the associated process exits.";

	// Token: 0x040006A8 RID: 1704
	public const string ProcessFileName = "The name of the application, document or URL to start.";

	// Token: 0x040006A9 RID: 1705
	public const string ProcessWorkingDirectory = "The initial working directory for the process.";

	// Token: 0x040006AA RID: 1706
	public const string ProcessBasePriority = "The base priority computed based on the priority class that all threads run relative to.";

	// Token: 0x040006AB RID: 1707
	public const string ProcessMainWindowHandle = "The handle of the main window for the process.";

	// Token: 0x040006AC RID: 1708
	public const string ProcessMainWindowTitle = "The caption of the main window for the process.";

	// Token: 0x040006AD RID: 1709
	public const string ProcessMaxWorkingSet = "The maximum amount of physical memory the process has required since it was started.";

	// Token: 0x040006AE RID: 1710
	public const string ProcessMinWorkingSet = "The minimum amount of physical memory the process has required since it was started.";

	// Token: 0x040006AF RID: 1711
	public const string ProcessNonpagedSystemMemorySize = "The number of bytes of non pageable system  memory the process is using.";

	// Token: 0x040006B0 RID: 1712
	public const string ProcessPagedMemorySize = "The current amount of memory that can be paged to disk that the process is using.";

	// Token: 0x040006B1 RID: 1713
	public const string ProcessPagedSystemMemorySize = "The number of bytes of pageable system memory the process is using.";

	// Token: 0x040006B2 RID: 1714
	public const string ProcessPeakPagedMemorySize = "The maximum amount of memory that can be paged to disk that the process has used since it was started.";

	// Token: 0x040006B3 RID: 1715
	public const string ProcessPeakWorkingSet = "The maximum amount of physical memory the process has used since it was started.";

	// Token: 0x040006B4 RID: 1716
	public const string ProcessPeakVirtualMemorySize = "The maximum amount of virtual memory the process has allocated since it was started.";

	// Token: 0x040006B5 RID: 1717
	public const string ProcessPriorityBoostEnabled = "Whether this process would like a priority boost when the user interacts with it.";

	// Token: 0x040006B6 RID: 1718
	public const string ProcessPriorityClass = "The priority that the threads in the process run relative to.";

	// Token: 0x040006B7 RID: 1719
	public const string ProcessPrivateMemorySize = "The current amount of memory that the process has allocated that cannot be shared with other processes.";

	// Token: 0x040006B8 RID: 1720
	public const string ProcessPrivilegedProcessorTime = "The amount of CPU time the process spent inside the operating system core.";

	// Token: 0x040006B9 RID: 1721
	public const string ProcessProcessName = "The name of the process.";

	// Token: 0x040006BA RID: 1722
	public const string ProcessProcessorAffinity = "A bit mask which represents the processors that the threads within the process are allowed to run on.";

	// Token: 0x040006BB RID: 1723
	public const string ProcessResponding = "Whether this process is currently responding.";

	// Token: 0x040006BC RID: 1724
	public const string ProcessStandardError = "Standard error stream of the process.";

	// Token: 0x040006BD RID: 1725
	public const string ProcessStandardInput = "Standard input stream of the process.";

	// Token: 0x040006BE RID: 1726
	public const string ProcessStandardOutput = "Standard output stream of the process.";

	// Token: 0x040006BF RID: 1727
	public const string ProcessStartInfo = "Specifies information used to start a process.";

	// Token: 0x040006C0 RID: 1728
	public const string ProcessStartTime = "The time at which the process was started.";

	// Token: 0x040006C1 RID: 1729
	public const string ProcessTotalProcessorTime = "The amount of CPU time the process has used.";

	// Token: 0x040006C2 RID: 1730
	public const string ProcessUserProcessorTime = "The amount of CPU time the process spent outside the operating system core.";

	// Token: 0x040006C3 RID: 1731
	public const string ProcessVirtualMemorySize = "The amount of virtual memory the process has currently allocated.";

	// Token: 0x040006C4 RID: 1732
	public const string ProcessWorkingSet = "The current amount of physical memory the process is using.";

	// Token: 0x040006C5 RID: 1733
	public const string ProcModModuleName = "The name of the module.";

	// Token: 0x040006C6 RID: 1734
	public const string ProcModFileName = "The file name of the module.";

	// Token: 0x040006C7 RID: 1735
	public const string ProcModBaseAddress = "The memory address that the module loaded at.";

	// Token: 0x040006C8 RID: 1736
	public const string ProcModModuleMemorySize = "The amount of virtual memory required by the code and data in the module file.";

	// Token: 0x040006C9 RID: 1737
	public const string ProcModEntryPointAddress = "The memory address of the function that runs when the module is loaded.";

	// Token: 0x040006CA RID: 1738
	public const string ProcessVerb = "The verb to apply to the document specified by the FileName property.";

	// Token: 0x040006CB RID: 1739
	public const string ProcessArguments = "Command line arguments that will be passed to the application specified by the FileName property.";

	// Token: 0x040006CC RID: 1740
	public const string ProcessErrorDialog = "Whether to show an error dialog to the user if there is an error.";

	// Token: 0x040006CD RID: 1741
	public const string ProcessWindowStyle = "How the main window should be created when the process starts.";

	// Token: 0x040006CE RID: 1742
	public const string ProcessCreateNoWindow = "Whether to start the process without creating a new window to contain it.";

	// Token: 0x040006CF RID: 1743
	public const string ProcessEnvironmentVariables = "Set of environment variables that apply to this process and child processes.";

	// Token: 0x040006D0 RID: 1744
	public const string ProcessRedirectStandardInput = "Whether the process command input is read from the Process instance's StandardInput member.";

	// Token: 0x040006D1 RID: 1745
	public const string ProcessRedirectStandardOutput = "Whether the process output is written to the Process instance's StandardOutput member.";

	// Token: 0x040006D2 RID: 1746
	public const string ProcessRedirectStandardError = "Whether the process's error output is written to the Process instance's StandardError member.";

	// Token: 0x040006D3 RID: 1747
	public const string ProcessUseShellExecute = "Whether to use the operating system shell to start the process.";

	// Token: 0x040006D4 RID: 1748
	public const string ThreadBasePriority = "The current base priority of the thread.";

	// Token: 0x040006D5 RID: 1749
	public const string ThreadCurrentPriority = "The current priority level of the thread.";

	// Token: 0x040006D6 RID: 1750
	public const string ThreadId = "The unique identifier for the thread.";

	// Token: 0x040006D7 RID: 1751
	public const string ThreadPriorityBoostEnabled = "Whether the thread would like a priority boost when the user interacts with UI associated with the thread.";

	// Token: 0x040006D8 RID: 1752
	public const string ThreadPriorityLevel = "The priority level of the thread.";

	// Token: 0x040006D9 RID: 1753
	public const string ThreadPrivilegedProcessorTime = "The amount of CPU time the thread spent inside the operating system core.";

	// Token: 0x040006DA RID: 1754
	public const string ThreadStartAddress = "The memory address of the function that was run when the thread started.";

	// Token: 0x040006DB RID: 1755
	public const string ThreadStartTime = "The time the thread was started.";

	// Token: 0x040006DC RID: 1756
	public const string ThreadThreadState = "The execution state of the thread.";

	// Token: 0x040006DD RID: 1757
	public const string ThreadTotalProcessorTime = "The amount of CPU time the thread has consumed since it was started.";

	// Token: 0x040006DE RID: 1758
	public const string ThreadUserProcessorTime = "The amount of CPU time the thread spent outside the operating system core.";

	// Token: 0x040006DF RID: 1759
	public const string ThreadWaitReason = "The reason the thread is waiting, if it is waiting.";

	// Token: 0x040006E0 RID: 1760
	public const string VerbEditorDefault = "(Default)";

	// Token: 0x040006E1 RID: 1761
	public const string AppSettingsReaderNoKey = "The key '{0}' does not exist in the appSettings configuration section.";

	// Token: 0x040006E2 RID: 1762
	public const string AppSettingsReaderNoParser = "Type '{0}' does not have a Parse method.";

	// Token: 0x040006E3 RID: 1763
	public const string AppSettingsReaderCantParse = "The value '{0}' was found in the appSettings configuration section for key '{1}', and this value is not a valid {2}.";

	// Token: 0x040006E4 RID: 1764
	public const string AppSettingsReaderEmptyString = "(empty string)";

	// Token: 0x040006E5 RID: 1765
	public const string InvalidPermissionState = "Invalid permission state.";

	// Token: 0x040006E6 RID: 1766
	public const string PermissionNumberOfElements = "The number of elements on the access path must be the same as the number of tag names.";

	// Token: 0x040006E7 RID: 1767
	public const string PermissionItemExists = "The item provided already exists.";

	// Token: 0x040006E8 RID: 1768
	public const string PermissionItemDoesntExist = "The requested item doesn't exist.";

	// Token: 0x040006E9 RID: 1769
	public const string PermissionBadParameterEnum = "Parameter must be of type enum.";

	// Token: 0x040006EA RID: 1770
	public const string PermissionInvalidLength = "Length must be greater than {0}.";

	// Token: 0x040006EB RID: 1771
	public const string PermissionTypeMismatch = "Type mismatch.";

	// Token: 0x040006EC RID: 1772
	public const string Argument_NotAPermissionElement = "'securityElement' was not a permission element.";

	// Token: 0x040006ED RID: 1773
	public const string Argument_InvalidXMLBadVersion = "Invalid Xml - can only parse elements of version one.";

	// Token: 0x040006EE RID: 1774
	public const string InvalidPermissionLevel = "Invalid permission level.";

	// Token: 0x040006EF RID: 1775
	public const string TargetNotWebBrowserPermissionLevel = "Target not WebBrowserPermissionLevel.";

	// Token: 0x040006F0 RID: 1776
	public const string WebBrowserBadXml = "Bad Xml {0}";

	// Token: 0x040006F1 RID: 1777
	public const string KeyedCollNeedNonNegativeNum = "Need a non negative number for capacity.";

	// Token: 0x040006F2 RID: 1778
	public const string KeyedCollDuplicateKey = "Cannot add item since the item with the key already exists in the collection.";

	// Token: 0x040006F3 RID: 1779
	public const string KeyedCollReferenceKeyNotFound = "The key reference with respect to which the insertion operation was to be performed was not found.";

	// Token: 0x040006F4 RID: 1780
	public const string KeyedCollKeyNotFound = "Cannot find the key {0} in the collection.";

	// Token: 0x040006F5 RID: 1781
	public const string KeyedCollInvalidKey = "Keys must be non-null non-empty Strings.";

	// Token: 0x040006F6 RID: 1782
	public const string KeyedCollCapacityOverflow = "Capacity overflowed and went negative.  Check capacity of the collection.";

	// Token: 0x040006F7 RID: 1783
	public const string OrderedDictionary_ReadOnly = "The OrderedDictionary is readonly and cannot be modified.";

	// Token: 0x040006F8 RID: 1784
	public const string OrderedDictionary_SerializationMismatch = "There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries.";

	// Token: 0x040006F9 RID: 1785
	public const string Async_ExceptionOccurred = "An exception occurred during the operation, making the result invalid.  Check InnerException for exception details.";

	// Token: 0x040006FA RID: 1786
	public const string Async_QueueingFailed = "Queuing WaitCallback failed.";

	// Token: 0x040006FB RID: 1787
	public const string Async_OperationCancelled = "Operation has been cancelled.";

	// Token: 0x040006FC RID: 1788
	public const string Async_OperationAlreadyCompleted = "This operation has already had OperationCompleted called on it and further calls are illegal.";

	// Token: 0x040006FD RID: 1789
	public const string Async_NullDelegate = "A non-null SendOrPostCallback must be supplied.";

	// Token: 0x040006FE RID: 1790
	public const string BackgroundWorker_AlreadyRunning = "BackgroundWorker is already running.";

	// Token: 0x040006FF RID: 1791
	public const string BackgroundWorker_CancellationNotSupported = "BackgroundWorker does not support cancellation.";

	// Token: 0x04000700 RID: 1792
	public const string BackgroundWorker_OperationCompleted = "Operation has already been completed.";

	// Token: 0x04000701 RID: 1793
	public const string BackgroundWorker_ProgressNotSupported = "BackgroundWorker does not support progress.";

	// Token: 0x04000702 RID: 1794
	public const string BackgroundWorker_WorkerAlreadyRunning = "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.";

	// Token: 0x04000703 RID: 1795
	public const string BackgroundWorker_WorkerDoesntReportProgress = "This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.";

	// Token: 0x04000704 RID: 1796
	public const string BackgroundWorker_WorkerDoesntSupportCancellation = "This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation.";

	// Token: 0x04000705 RID: 1797
	public const string Async_ProgressChangedEventArgs_ProgressPercentage = "Percentage progress made in operation.";

	// Token: 0x04000706 RID: 1798
	public const string Async_ProgressChangedEventArgs_UserState = "User-supplied state to identify operation.";

	// Token: 0x04000707 RID: 1799
	public const string Async_AsyncEventArgs_Cancelled = "True if operation was cancelled.";

	// Token: 0x04000708 RID: 1800
	public const string Async_AsyncEventArgs_Error = "Exception that occurred during operation.  Null if no error.";

	// Token: 0x04000709 RID: 1801
	public const string Async_AsyncEventArgs_UserState = "User-supplied state to identify operation.";

	// Token: 0x0400070A RID: 1802
	public const string BackgroundWorker_CancellationPending = "Has the user attempted to cancel the operation? To be accessed from DoWork event handler.";

	// Token: 0x0400070B RID: 1803
	public const string BackgroundWorker_DoWork = "Event handler to be run on a different thread when the operation begins.";

	// Token: 0x0400070C RID: 1804
	public const string BackgroundWorker_IsBusy = "Is the worker still currently working on a background operation?";

	// Token: 0x0400070D RID: 1805
	public const string BackgroundWorker_ProgressChanged = "Raised when the worker thread indicates that some progress has been made.";

	// Token: 0x0400070E RID: 1806
	public const string BackgroundWorker_RunWorkerCompleted = "Raised when the worker has completed (either through success, failure, or cancellation).";

	// Token: 0x0400070F RID: 1807
	public const string BackgroundWorker_WorkerReportsProgress = "Whether the worker will report progress.";

	// Token: 0x04000710 RID: 1808
	public const string BackgroundWorker_WorkerSupportsCancellation = "Whether the worker supports cancellation.";

	// Token: 0x04000711 RID: 1809
	public const string BackgroundWorker_DoWorkEventArgs_Argument = "Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync.";

	// Token: 0x04000712 RID: 1810
	public const string BackgroundWorker_DoWorkEventArgs_Result = "Result from the worker function.";

	// Token: 0x04000713 RID: 1811
	public const string BackgroundWorker_Desc = "Executes an operation on a separate thread.";

	// Token: 0x04000714 RID: 1812
	public const string InstanceCreationEditorDefaultText = "(New...)";

	// Token: 0x04000715 RID: 1813
	public const string PropertyTabAttributeBadPropertyTabScope = "Scope must be PropertyTabScope.Document or PropertyTabScope.Component";

	// Token: 0x04000716 RID: 1814
	public const string PropertyTabAttributeTypeLoadException = "Couldn't find type {0}";

	// Token: 0x04000717 RID: 1815
	public const string PropertyTabAttributeArrayLengthMismatch = "tabClasses must have the same number of items as tabScopes";

	// Token: 0x04000718 RID: 1816
	public const string PropertyTabAttributeParamsBothNull = "An array of tab type names or tab types must be specified";

	// Token: 0x04000719 RID: 1817
	public const string InstanceDescriptorCannotBeStatic = "Parameter cannot be static.";

	// Token: 0x0400071A RID: 1818
	public const string InstanceDescriptorMustBeStatic = "Parameter must be static.";

	// Token: 0x0400071B RID: 1819
	public const string InstanceDescriptorMustBeReadable = "Parameter must be readable.";

	// Token: 0x0400071C RID: 1820
	public const string InstanceDescriptorLengthMismatch = "Length mismatch.";

	// Token: 0x0400071D RID: 1821
	public const string ToolboxItemAttributeFailedGetType = "Failed to create ToolboxItem of type: {0}";

	// Token: 0x0400071E RID: 1822
	public const string PropertyDescriptorCollectionBadValue = "Parameter must be of type PropertyDescriptor.";

	// Token: 0x0400071F RID: 1823
	public const string PropertyDescriptorCollectionBadKey = "Parameter must be of type int or string.";

	// Token: 0x04000720 RID: 1824
	public const string AspNetHostingPermissionBadXml = "Bad Xml {0}";

	// Token: 0x04000721 RID: 1825
	public const string CorruptedGZipHeader = "The magic number in GZip header is not correct. Make sure you are passing in a GZip stream.";

	// Token: 0x04000722 RID: 1826
	public const string UnknownCompressionMode = "The compression mode specified in GZip header is unknown.";

	// Token: 0x04000723 RID: 1827
	public const string UnknownState = "Decoder is in some unknown state. This might be caused by corrupted data.";

	// Token: 0x04000724 RID: 1828
	public const string InvalidHuffmanData = "Failed to construct a huffman tree using the length array. The stream might be corrupted.";

	// Token: 0x04000725 RID: 1829
	public const string InvalidCRC = "The CRC in GZip footer does not match the CRC calculated from the decompressed data.";

	// Token: 0x04000726 RID: 1830
	public const string InvalidStreamSize = "The stream size in GZip footer does not match the real stream size.";

	// Token: 0x04000727 RID: 1831
	public const string UnknownBlockType = "Unknown block type. Stream might be corrupted.";

	// Token: 0x04000728 RID: 1832
	public const string InvalidBlockLength = "Block length does not match with its complement.";

	// Token: 0x04000729 RID: 1833
	public const string GenericInvalidData = "Found invalid data while decoding.";

	// Token: 0x0400072A RID: 1834
	public const string CannotReadFromDeflateStream = "Reading from the compression stream is not supported.";

	// Token: 0x0400072B RID: 1835
	public const string CannotWriteToDeflateStream = "Writing to the compression stream is not supported.";

	// Token: 0x0400072C RID: 1836
	public const string NotReadableStream = "The base stream is not readable.";

	// Token: 0x0400072D RID: 1837
	public const string NotWriteableStream = "The base stream is not writeable.";

	// Token: 0x0400072E RID: 1838
	public const string InvalidArgumentOffsetCount = "Offset plus count is larger than the length of target array.";

	// Token: 0x0400072F RID: 1839
	public const string InvalidBeginCall = "Only one asynchronous reader is allowed time at one time.";

	// Token: 0x04000730 RID: 1840
	public const string InvalidEndCall = "EndRead is only callable when there is one pending asynchronous reader.";

	// Token: 0x04000731 RID: 1841
	public const string StreamSizeOverflow = "The gzip stream can't contain more than 4GB data.";

	// Token: 0x04000732 RID: 1842
	public const string ZLibErrorDLLLoadError = "The underlying compression routine could not be loaded correctly.";

	// Token: 0x04000733 RID: 1843
	public const string ZLibErrorUnexpected = "The underlying compression routine returned an unexpected error code.";

	// Token: 0x04000734 RID: 1844
	public const string ZLibErrorInconsistentStream = "The stream state of the underlying compression routine is inconsistent.";

	// Token: 0x04000735 RID: 1845
	public const string ZLibErrorSteamFreedPrematurely = "The stream state of the underlying compression routine was freed prematurely.";

	// Token: 0x04000736 RID: 1846
	public const string ZLibErrorNotEnoughMemory = "The underlying compression routine could not reserve sufficient memory.";

	// Token: 0x04000737 RID: 1847
	public const string ZLibErrorIncorrectInitParameters = "The underlying compression routine received incorrect initialization parameters.";

	// Token: 0x04000738 RID: 1848
	public const string ZLibErrorVersionMismatch = "The version of the underlying compression routine does not match expected version.";

	// Token: 0x04000739 RID: 1849
	public const string InvalidOperation_HCCountOverflow = "Handle collector count overflows or underflows.";

	// Token: 0x0400073A RID: 1850
	public const string Argument_InvalidThreshold = "maximumThreshold cannot be less than initialThreshold.";

	// Token: 0x0400073B RID: 1851
	public const string Argument_SemaphoreInitialMaximum = "The initial count for the semaphore must be greater than or equal to zero and less than the maximum count.";

	// Token: 0x0400073C RID: 1852
	public const string Argument_WaitHandleNameTooLong = "The name can be no more than 260 characters in length.";

	// Token: 0x0400073D RID: 1853
	public const string WaitHandleCannotBeOpenedException_InvalidHandle = "A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.";

	// Token: 0x0400073E RID: 1854
	public const string ArgumentNotAPermissionElement = "Argument was not a permission Element.";

	// Token: 0x0400073F RID: 1855
	public const string ArgumentWrongType = "Argument should be of type {0}.";

	// Token: 0x04000740 RID: 1856
	public const string BadXmlVersion = "Xml version was wrong.";

	// Token: 0x04000741 RID: 1857
	public const string BinarySerializationNotSupported = "Binary serialization is current not supported by the LocalFileSettingsProvider.";

	// Token: 0x04000742 RID: 1858
	public const string BothScopeAttributes = "The setting {0} has both an ApplicationScopedSettingAttribute and a UserScopedSettingAttribute.";

	// Token: 0x04000743 RID: 1859
	public const string NoScopeAttributes = "The setting {0} does not have either an ApplicationScopedSettingAttribute or UserScopedSettingAttribute.";

	// Token: 0x04000744 RID: 1860
	public const string PositionOutOfRange = "Position cannot be less than zero.";

	// Token: 0x04000745 RID: 1861
	public const string ProviderInstantiationFailed = "Failed to instantiate provider: {0}.";

	// Token: 0x04000746 RID: 1862
	public const string ProviderTypeLoadFailed = "Failed to load provider type: {0}.";

	// Token: 0x04000747 RID: 1863
	public const string SaveAppScopedNotSupported = "Error saving {0} - The LocalFileSettingsProvider does not support saving changes to application-scoped settings.";

	// Token: 0x04000748 RID: 1864
	public const string SettingsResetFailed = "Failed to reset settings: unable to access the configuration section.";

	// Token: 0x04000749 RID: 1865
	public const string SettingsSaveFailed = "Failed to save settings: {0}";

	// Token: 0x0400074A RID: 1866
	public const string SettingsSaveFailedNoSection = "Failed to save settings: unable to access the configuration section.";

	// Token: 0x0400074B RID: 1867
	public const string StringDeserializationFailed = "Could not use String deserialization for setting: {0}.";

	// Token: 0x0400074C RID: 1868
	public const string StringSerializationFailed = "Could not use String serialization for setting: {0}.";

	// Token: 0x0400074D RID: 1869
	public const string UnknownSerializationFormat = "Unknown serialization format specified.";

	// Token: 0x0400074E RID: 1870
	public const string UnknownSeekOrigin = "Unknown SeekOrigin specified.";

	// Token: 0x0400074F RID: 1871
	public const string UnknownUserLevel = "Unknown ConfigurationUserLevel specified.";

	// Token: 0x04000750 RID: 1872
	public const string UserSettingsNotSupported = "The current configuration system does not support user-scoped settings.";

	// Token: 0x04000751 RID: 1873
	public const string XmlDeserializationFailed = "Could not use Xml deserialization for setting: {0}.";

	// Token: 0x04000752 RID: 1874
	public const string XmlSerializationFailed = "Could not use Xml serialization for setting: {0}.";

	// Token: 0x04000753 RID: 1875
	public const string MemberRelationshipService_RelationshipNotSupported = "Relationships between {0}.{1} and {2}.{3} are not supported.";

	// Token: 0x04000754 RID: 1876
	public const string MaskedTextProviderPasswordAndPromptCharError = "The PasswordChar and PromptChar values cannot be the same.";

	// Token: 0x04000755 RID: 1877
	public const string MaskedTextProviderInvalidCharError = "The specified character value is not allowed for this property.";

	// Token: 0x04000756 RID: 1878
	public const string MaskedTextProviderMaskNullOrEmpty = "The Mask value cannot be null or empty.";

	// Token: 0x04000757 RID: 1879
	public const string MaskedTextProviderMaskInvalidChar = "The specified mask contains invalid characters.";

	// Token: 0x04000758 RID: 1880
	public const string StandardOleMarshalObjectGetMarshalerFailed = "Failed to get marshaler for IID {0}.";

	// Token: 0x04000759 RID: 1881
	public const string SoundAPIBadSoundLocation = "Could not determine a universal resource identifier for the sound location.";

	// Token: 0x0400075A RID: 1882
	public const string SoundAPIFileDoesNotExist = "Please be sure a sound file exists at the specified location.";

	// Token: 0x0400075B RID: 1883
	public const string SoundAPIFormatNotSupported = "Sound API only supports playing PCM wave files.";

	// Token: 0x0400075C RID: 1884
	public const string SoundAPIInvalidWaveFile = "The file located at {0} is not a valid wave file.";

	// Token: 0x0400075D RID: 1885
	public const string SoundAPIInvalidWaveHeader = "The wave header is corrupt.";

	// Token: 0x0400075E RID: 1886
	public const string SoundAPILoadTimedOut = "The request to load the wave file in memory timed out.";

	// Token: 0x0400075F RID: 1887
	public const string SoundAPILoadTimeout = "The LoadTimeout property of a SoundPlayer cannot be negative.";

	// Token: 0x04000760 RID: 1888
	public const string SoundAPIReadError = "There was an error reading the file located at {0}. Please make sure that a valid wave file exists at the specified location.";

	// Token: 0x04000761 RID: 1889
	public const string WrongActionForCtor = "Constructor supports only the '{0}' action.";

	// Token: 0x04000762 RID: 1890
	public const string MustBeResetAddOrRemoveActionForCtor = "Constructor only supports either a Reset, Add, or Remove action.";

	// Token: 0x04000763 RID: 1891
	public const string ResetActionRequiresNullItem = "Reset action must be initialized with no changed items.";

	// Token: 0x04000764 RID: 1892
	public const string ResetActionRequiresIndexMinus1 = "Reset action must be initialized with index -1.";

	// Token: 0x04000765 RID: 1893
	public const string IndexCannotBeNegative = "Index cannot be negative.";

	// Token: 0x04000766 RID: 1894
	public const string ObservableCollectionReentrancyNotAllowed = "Cannot change ObservableCollection during a CollectionChanged event.";

	// Token: 0x04000767 RID: 1895
	public const string Arg_ArgumentOutOfRangeException = "Specified argument was out of the range of valid values.";

	// Token: 0x04000768 RID: 1896
	public const string mono_net_io_shutdown = "mono_net_io_shutdown";

	// Token: 0x04000769 RID: 1897
	public const string mono_net_io_renegotiate = "mono_net_io_renegotiate";

	// Token: 0x0400076A RID: 1898
	public const string net_ssl_io_already_shutdown = "Write operations are not allowed after the channel was shutdown.";

	// Token: 0x0400076B RID: 1899
	public const string net_log_set_socketoption_reuseport_default_on = "net_log_set_socketoption_reuseport_default_on";

	// Token: 0x0400076C RID: 1900
	public const string net_log_set_socketoption_reuseport_not_supported = "net_log_set_socketoption_reuseport_not_supported";

	// Token: 0x0400076D RID: 1901
	public const string net_log_set_socketoption_reuseport = "net_log_set_socketoption_reuseport";

	// Token: 0x0400076E RID: 1902
	public const string net_ssl_app_protocols_invalid = "The application protocol list is invalid.";

	// Token: 0x0400076F RID: 1903
	public const string net_ssl_app_protocol_invalid = "The application protocol value is invalid.";

	// Token: 0x04000770 RID: 1904
	public const string net_conflicting_options = "The '{0}' option was already set in the SslStream constructor.";

	// Token: 0x04000771 RID: 1905
	public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";

	// Token: 0x04000772 RID: 1906
	public const string Arg_WrongType = "The value '{0}' is not of type '{1}' and cannot be used in this generic collection.";

	// Token: 0x04000773 RID: 1907
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x04000774 RID: 1908
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x04000775 RID: 1909
	public const string ArgumentOutOfRange_SmallCapacity = "capacity was less than the current size.";

	// Token: 0x04000776 RID: 1910
	public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x04000777 RID: 1911
	public const string Argument_AddingDuplicate = "An item with the same key has already been added. Key: {0}";

	// Token: 0x04000778 RID: 1912
	public const string InvalidOperation_ConcurrentOperationsNotSupported = "Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.";

	// Token: 0x04000779 RID: 1913
	public const string InvalidOperation_EmptyQueue = "Queue empty.";

	// Token: 0x0400077A RID: 1914
	public const string InvalidOperation_EnumOpCantHappen = "Enumeration has either not started or has already finished.";

	// Token: 0x0400077B RID: 1915
	public const string InvalidOperation_EnumFailedVersion = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x0400077C RID: 1916
	public const string InvalidOperation_EmptyStack = "Stack empty.";

	// Token: 0x0400077D RID: 1917
	public const string InvalidOperation_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

	// Token: 0x0400077E RID: 1918
	public const string InvalidOperation_EnumEnded = "Enumeration already finished.";

	// Token: 0x0400077F RID: 1919
	public const string NotSupported_KeyCollectionSet = "Mutating a key collection derived from a dictionary is not allowed.";

	// Token: 0x04000780 RID: 1920
	public const string NotSupported_ValueCollectionSet = "Mutating a value collection derived from a dictionary is not allowed.";

	// Token: 0x04000781 RID: 1921
	public const string Arg_ArrayLengthsDiffer = "Array lengths must be the same.";

	// Token: 0x04000782 RID: 1922
	public const string Arg_BitArrayTypeUnsupported = "Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].";

	// Token: 0x04000783 RID: 1923
	public const string Arg_HSCapacityOverflow = "HashSet capacity is too big.";

	// Token: 0x04000784 RID: 1924
	public const string Arg_HTCapacityOverflow = "Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.";

	// Token: 0x04000785 RID: 1925
	public const string Arg_InsufficientSpace = "Insufficient space in the target location to copy the information.";

	// Token: 0x04000786 RID: 1926
	public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

	// Token: 0x04000787 RID: 1927
	public const string Argument_ArrayTooLarge = "The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.";

	// Token: 0x04000788 RID: 1928
	public const string Argument_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x04000789 RID: 1929
	public const string ArgumentOutOfRange_BiggerThanCollection = "Must be less than or equal to the size of the collection.";

	// Token: 0x0400078A RID: 1930
	public const string ArgumentOutOfRange_Index = "Index was out of range. Must be non-negative and less than the size of the collection.";

	// Token: 0x0400078B RID: 1931
	public const string ExternalLinkedListNode = "The LinkedList node does not belong to current LinkedList.";

	// Token: 0x0400078C RID: 1932
	public const string LinkedListEmpty = "The LinkedList is empty.";

	// Token: 0x0400078D RID: 1933
	public const string LinkedListNodeIsAttached = "The LinkedList node already belongs to a LinkedList.";

	// Token: 0x0400078E RID: 1934
	public const string NotSupported_SortedListNestedWrite = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";

	// Token: 0x0400078F RID: 1935
	public const string SortedSet_LowerValueGreaterThanUpperValue = "Must be less than or equal to upperValue.";

	// Token: 0x04000790 RID: 1936
	public const string Serialization_InvalidOnDeser = "OnDeserialization method was called while the object was not being deserialized.";

	// Token: 0x04000791 RID: 1937
	public const string Serialization_MismatchedCount = "The serialized Count information doesn't match the number of items.";

	// Token: 0x04000792 RID: 1938
	public const string Serialization_MissingKeys = "The keys for this dictionary are missing.";

	// Token: 0x04000793 RID: 1939
	public const string Serialization_MissingValues = "The values for this dictionary are missing.";

	// Token: 0x04000794 RID: 1940
	public const string Arg_KeyNotFoundWithKey = "The given key '{0}' was not present in the dictionary.";

	// Token: 0x04000795 RID: 1941
	public const string BlockingCollection_Add_ConcurrentCompleteAdd = "CompleteAdding may not be used concurrently with additions to the collection.";

	// Token: 0x04000796 RID: 1942
	public const string BlockingCollection_Add_Failed = "The underlying collection didn't accept the item.";

	// Token: 0x04000797 RID: 1943
	public const string BlockingCollection_CantAddAnyWhenCompleted = "At least one of the specified collections is marked as complete with regards to additions.";

	// Token: 0x04000798 RID: 1944
	public const string BlockingCollection_CantTakeAnyWhenAllDone = "All collections are marked as complete with regards to additions.";

	// Token: 0x04000799 RID: 1945
	public const string BlockingCollection_CantTakeWhenDone = "The collection argument is empty and has been marked as complete with regards to additions.";

	// Token: 0x0400079A RID: 1946
	public const string BlockingCollection_Completed = "The collection has been marked as complete with regards to additions.";

	// Token: 0x0400079B RID: 1947
	public const string BlockingCollection_CopyTo_IncorrectType = "The array argument is of the incorrect type.";

	// Token: 0x0400079C RID: 1948
	public const string BlockingCollection_CopyTo_MultiDim = "The array argument is multidimensional.";

	// Token: 0x0400079D RID: 1949
	public const string BlockingCollection_CopyTo_NonNegative = "The index argument must be greater than or equal zero.";

	// Token: 0x0400079E RID: 1950
	public const string Collection_CopyTo_TooManyElems = "The number of elements in the collection is greater than the available space from index to the end of the destination array.";

	// Token: 0x0400079F RID: 1951
	public const string BlockingCollection_ctor_BoundedCapacityRange = "The boundedCapacity argument must be positive.";

	// Token: 0x040007A0 RID: 1952
	public const string BlockingCollection_ctor_CountMoreThanCapacity = "The collection argument contains more items than are allowed by the boundedCapacity.";

	// Token: 0x040007A1 RID: 1953
	public const string BlockingCollection_Disposed = "The collection has been disposed.";

	// Token: 0x040007A2 RID: 1954
	public const string BlockingCollection_Take_CollectionModified = "The underlying collection was modified from outside of the BlockingCollection<T>.";

	// Token: 0x040007A3 RID: 1955
	public const string BlockingCollection_TimeoutInvalid = "The specified timeout must represent a value between -1 and {0}, inclusive.";

	// Token: 0x040007A4 RID: 1956
	public const string BlockingCollection_ValidateCollectionsArray_DispElems = "The collections argument contains at least one disposed element.";

	// Token: 0x040007A5 RID: 1957
	public const string BlockingCollection_ValidateCollectionsArray_LargeSize = "The collections length is greater than the supported range for 32 bit machine.";

	// Token: 0x040007A6 RID: 1958
	public const string BlockingCollection_ValidateCollectionsArray_NullElems = "The collections argument contains at least one null element.";

	// Token: 0x040007A7 RID: 1959
	public const string BlockingCollection_ValidateCollectionsArray_ZeroSize = "The collections argument is a zero-length array.";

	// Token: 0x040007A8 RID: 1960
	public const string Common_OperationCanceled = "The operation was canceled.";

	// Token: 0x040007A9 RID: 1961
	public const string ConcurrentBag_Ctor_ArgumentNullException = "The collection argument is null.";

	// Token: 0x040007AA RID: 1962
	public const string ConcurrentBag_CopyTo_ArgumentNullException = "The array argument is null.";

	// Token: 0x040007AB RID: 1963
	public const string Collection_CopyTo_ArgumentOutOfRangeException = "The index argument must be greater than or equal zero.";

	// Token: 0x040007AC RID: 1964
	public const string ConcurrentCollection_SyncRoot_NotSupported = "The SyncRoot property may not be used for the synchronization of concurrent collections.";

	// Token: 0x040007AD RID: 1965
	public const string ConcurrentDictionary_ArrayIncorrectType = "The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.";

	// Token: 0x040007AE RID: 1966
	public const string ConcurrentDictionary_SourceContainsDuplicateKeys = "The source argument contains duplicate keys.";

	// Token: 0x040007AF RID: 1967
	public const string ConcurrentDictionary_ConcurrencyLevelMustBePositive = "The concurrencyLevel argument must be positive.";

	// Token: 0x040007B0 RID: 1968
	public const string ConcurrentDictionary_CapacityMustNotBeNegative = "The capacity argument must be greater than or equal to zero.";

	// Token: 0x040007B1 RID: 1969
	public const string ConcurrentDictionary_IndexIsNegative = "The index argument is less than zero.";

	// Token: 0x040007B2 RID: 1970
	public const string ConcurrentDictionary_ArrayNotLargeEnough = "The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.";

	// Token: 0x040007B3 RID: 1971
	public const string ConcurrentDictionary_KeyAlreadyExisted = "The key already existed in the dictionary.";

	// Token: 0x040007B4 RID: 1972
	public const string ConcurrentDictionary_ItemKeyIsNull = "TKey is a reference type and item.Key is null.";

	// Token: 0x040007B5 RID: 1973
	public const string ConcurrentDictionary_TypeOfKeyIncorrect = "The key was of an incorrect type for this dictionary.";

	// Token: 0x040007B6 RID: 1974
	public const string ConcurrentDictionary_TypeOfValueIncorrect = "The value was of an incorrect type for this dictionary.";

	// Token: 0x040007B7 RID: 1975
	public const string ConcurrentStack_PushPopRange_CountOutOfRange = "The count argument must be greater than or equal to zero.";

	// Token: 0x040007B8 RID: 1976
	public const string ConcurrentStack_PushPopRange_InvalidCount = "The sum of the startIndex and count arguments must be less than or equal to the collection's Count.";

	// Token: 0x040007B9 RID: 1977
	public const string ConcurrentStack_PushPopRange_StartOutOfRange = "The startIndex argument must be greater than or equal to zero.";

	// Token: 0x040007BA RID: 1978
	public const string Partitioner_DynamicPartitionsNotSupported = "Dynamic partitions are not supported by this partitioner.";

	// Token: 0x040007BB RID: 1979
	public const string PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed = "Can not call GetEnumerator on partitions after the source enumerable is disposed";

	// Token: 0x040007BC RID: 1980
	public const string PartitionerStatic_CurrentCalledBeforeMoveNext = "MoveNext must be called at least once before calling Current.";

	// Token: 0x040007BD RID: 1981
	public const string ConcurrentBag_Enumerator_EnumerationNotStartedOrAlreadyFinished = "Enumeration has either not started or has already finished.";

	// Token: 0x040007BE RID: 1982
	public const string Argument_AddingDuplicate__ = "Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'";

	// Token: 0x040007BF RID: 1983
	public const string Argument_ImplementIComparable = "At least one object must implement IComparable.";

	// Token: 0x040007C0 RID: 1984
	public const string Arg_RemoveArgNotFound = "Cannot remove the specified item because it was not found in the specified Collection.";

	// Token: 0x040007C1 RID: 1985
	public const string ArgumentNull_Dictionary = "Dictionary cannot be null.";

	// Token: 0x040007C2 RID: 1986
	public const string ArgumentOutOfRange_QueueGrowFactor = "Queue grow factor must be between {0} and {1}.";

	// Token: 0x040007C3 RID: 1987
	public const string Array = "{0} Array";

	// Token: 0x040007C4 RID: 1988
	public const string Collection = "(Collection)";

	// Token: 0x040007C5 RID: 1989
	public const string none = "(none)";

	// Token: 0x040007C6 RID: 1990
	public const string Null = "(null)";

	// Token: 0x040007C7 RID: 1991
	public const string Text = "(Text)";

	// Token: 0x040007C8 RID: 1992
	public const string InvalidColor = "Color '{0}' is not valid.";

	// Token: 0x040007C9 RID: 1993
	public const string TextParseFailedFormat = "Text \"{0}\" cannot be parsed. The expected text format is \"{1}\".";

	// Token: 0x040007CA RID: 1994
	public const string PropertyValueInvalidEntry = "IDictionary parameter contains at least one entry that is not valid. Ensure all values are consistent with the object's properties.";

	// Token: 0x040007CB RID: 1995
	public const string ArgumentException_BufferNotFromPool = "The buffer is not associated with this pool and may not be returned to it.";

	// Token: 0x040007CC RID: 1996
	public const string Arg_FileIsDirectory_Name = "The target file '{0}' is a directory, not a file.";

	// Token: 0x040007CD RID: 1997
	public const string Arg_HandleNotAsync = "Handle does not support asynchronous operations. The parameters to the FileStream constructor may need to be changed to indicate that the handle was opened synchronously (that is, it was not opened for overlapped I/O).";

	// Token: 0x040007CE RID: 1998
	public const string Arg_HandleNotSync = "Handle does not support synchronous operations. The parameters to the FileStream constructor may need to be changed to indicate that the handle was opened asynchronously (that is, it was opened explicitly for overlapped I/O).";

	// Token: 0x040007CF RID: 1999
	public const string Arg_InvalidFileAttrs = "Invalid File or Directory attributes value.";

	// Token: 0x040007D0 RID: 2000
	public const string Arg_InvalidHandle = "Invalid handle.";

	// Token: 0x040007D1 RID: 2001
	public const string Arg_InvalidSearchPattern = "Search pattern cannot contain '..' to move up directories and can be contained only internally in file/directory names, as in 'a..b'.";

	// Token: 0x040007D2 RID: 2002
	public const string Arg_Path2IsRooted = "Second path fragment must not be a drive or UNC name.";

	// Token: 0x040007D3 RID: 2003
	public const string Arg_PathIsVolume = "Path must not be a drive.";

	// Token: 0x040007D4 RID: 2004
	public const string ArgumentNull_FileName = "File name cannot be null.";

	// Token: 0x040007D5 RID: 2005
	public const string ArgumentNull_Path = "Path cannot be null.";

	// Token: 0x040007D6 RID: 2006
	public const string ArgumentOutOfRange_Enum = "Enum value was out of legal range.";

	// Token: 0x040007D7 RID: 2007
	public const string ArgumentOutOfRange_FileLengthTooBig = "Specified file length was too large for the file system.";

	// Token: 0x040007D8 RID: 2008
	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	// Token: 0x040007D9 RID: 2009
	public const string Argument_EmptyFileName = "Empty file name is not legal.";

	// Token: 0x040007DA RID: 2010
	public const string Argument_EmptyPath = "Empty path name is not legal.";

	// Token: 0x040007DB RID: 2011
	public const string Argument_FileNotResized = "The stream's length cannot be changed.";

	// Token: 0x040007DC RID: 2012
	public const string Argument_InvalidAppendMode = "Append access can be requested only in write-only mode.";

	// Token: 0x040007DD RID: 2013
	public const string Argument_InvalidFileModeAndAccessCombo = "Combining FileMode: {0} with FileAccess: {1} is invalid.";

	// Token: 0x040007DE RID: 2014
	public const string Argument_InvalidPathChars = "Illegal characters in path '{0}'.";

	// Token: 0x040007DF RID: 2015
	public const string Argument_InvalidSeekOrigin = "Invalid seek origin.";

	// Token: 0x040007E0 RID: 2016
	public const string Argument_InvalidSubPath = "The directory specified, '{0}', is not a subdirectory of '{1}'.";

	// Token: 0x040007E1 RID: 2017
	public const string Argument_PathEmpty = "Path cannot be the empty string or all whitespace.";

	// Token: 0x040007E2 RID: 2018
	public const string IO_AlreadyExists_Name = "Cannot create '{0}' because a file or directory with the same name already exists.";

	// Token: 0x040007E3 RID: 2019
	public const string IO_BindHandleFailed = "BindHandle for ThreadPool failed on this handle.";

	// Token: 0x040007E4 RID: 2020
	public const string IO_CannotCreateDirectory = "The specified directory '{0}' cannot be created.";

	// Token: 0x040007E5 RID: 2021
	public const string IO_EOF_ReadBeyondEOF = "Unable to read beyond the end of the stream.";

	// Token: 0x040007E6 RID: 2022
	public const string IO_FileCreateAlreadyExists = "Cannot create a file when that file already exists.";

	// Token: 0x040007E7 RID: 2023
	public const string IO_FileExists_Name = "The file '{0}' already exists.";

	// Token: 0x040007E8 RID: 2024
	public const string IO_FileNotFound = "Unable to find the specified file.";

	// Token: 0x040007E9 RID: 2025
	public const string IO_FileNotFound_FileName = "Could not find file '{0}'.";

	// Token: 0x040007EA RID: 2026
	public const string IO_FileStreamHandlePosition = "The OS handle's position is not what FileStream expected. Do not use a handle simultaneously in one FileStream and in Win32 code or another FileStream. This may cause data loss.";

	// Token: 0x040007EB RID: 2027
	public const string IO_FileTooLong2GB = "The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size.";

	// Token: 0x040007EC RID: 2028
	public const string IO_FileTooLongOrHandleNotSync = "IO operation will not work. Most likely the file will become too long or the handle was not opened to support synchronous IO operations.";

	// Token: 0x040007ED RID: 2029
	public const string IO_PathNotFound_NoPathName = "Could not find a part of the path.";

	// Token: 0x040007EE RID: 2030
	public const string IO_PathNotFound_Path = "Could not find a part of the path '{0}'.";

	// Token: 0x040007EF RID: 2031
	public const string IO_PathTooLong = "The specified file name or path is too long, or a component of the specified path is too long.";

	// Token: 0x040007F0 RID: 2032
	public const string IO_SeekAppendOverwrite = "Unable seek backward to overwrite data that previously existed in a file opened in Append mode.";

	// Token: 0x040007F1 RID: 2033
	public const string IO_SetLengthAppendTruncate = "Unable to truncate data that previously existed in a file opened in Append mode.";

	// Token: 0x040007F2 RID: 2034
	public const string IO_SharingViolation_File = "The process cannot access the file '{0}' because it is being used by another process.";

	// Token: 0x040007F3 RID: 2035
	public const string IO_SharingViolation_NoFileName = "The process cannot access the file because it is being used by another process.";

	// Token: 0x040007F4 RID: 2036
	public const string IO_SourceDestMustBeDifferent = "Source and destination path must be different.";

	// Token: 0x040007F5 RID: 2037
	public const string IO_SourceDestMustHaveSameRoot = "Source and destination path must have identical roots. Move will not work across volumes.";

	// Token: 0x040007F6 RID: 2038
	public const string IO_SyncOpOnUIThread = "Synchronous operations should not be performed on the UI thread.  Consider wrapping this method in Task.Run.";

	// Token: 0x040007F7 RID: 2039
	public const string IO_UnknownFileName = "[Unknown]";

	// Token: 0x040007F8 RID: 2040
	public const string IndexOutOfRange_IORaceCondition = "Probable I/O race condition detected while copying memory. The I/O package is not thread safe by default. In multithreaded applications, a stream must be accessed in a thread-safe way, such as a thread-safe wrapper returned by TextReader's or TextWriter's Synchronized methods. This also applies to classes like StreamWriter and StreamReader.";

	// Token: 0x040007F9 RID: 2041
	public const string NotSupported_UnseekableStream = "Stream does not support seeking.";

	// Token: 0x040007FA RID: 2042
	public const string ObjectDisposed_FileClosed = "Cannot access a closed file.";

	// Token: 0x040007FB RID: 2043
	public const string UnauthorizedAccess_IODenied_NoPathName = "Access to the path is denied.";

	// Token: 0x040007FC RID: 2044
	public const string UnauthorizedAccess_IODenied_Path = "Access to the path '{0}' is denied.";

	// Token: 0x040007FD RID: 2045
	public const string ObjectDisposed_StreamClosed = "Cannot access a closed Stream.";

	// Token: 0x040007FE RID: 2046
	public const string PlatformNotSupported_FileEncryption = "File encryption is not supported on this platform.";

	// Token: 0x040007FF RID: 2047
	public const string IO_PathTooLong_Path = "The path '{0}' is too long, or a component of the specified path is too long.";

	// Token: 0x04000800 RID: 2048
	public const string InvalidDirName_NotExists = "The directory name '{0}' does not exist.";

	// Token: 0x04000801 RID: 2049
	public const string EventStream_FailedToStart = "Failed to start the EventStream";

	// Token: 0x04000802 RID: 2050
	public const string IOException_INotifyInstanceSystemLimitExceeded = "The system limit on the number of inotify instances has been reached.";

	// Token: 0x04000803 RID: 2051
	public const string IOException_INotifyInstanceUserLimitExceeded_Value = "The configured user limit ({0}) on the number of inotify instances has been reached.";

	// Token: 0x04000804 RID: 2052
	public const string IOException_INotifyWatchesUserLimitExceeded_Value = "The configured user limit ({0}) on the number of inotify watches has been reached.";

	// Token: 0x04000805 RID: 2053
	public const string IOException_INotifyInstanceUserLimitExceeded = "The configured user limit on the number of inotify instances has been reached.";

	// Token: 0x04000806 RID: 2054
	public const string IOException_INotifyWatchesUserLimitExceeded = "The configured user limit on the number of inotify watches has been reached.";

	// Token: 0x04000807 RID: 2055
	public const string BaseStream_Invalid_Not_Open = "The BaseStream is only available when the port is open.";

	// Token: 0x04000808 RID: 2056
	public const string PortNameEmpty_String = "The PortName cannot be empty.";

	// Token: 0x04000809 RID: 2057
	public const string Port_not_open = "The port is closed.";

	// Token: 0x0400080A RID: 2058
	public const string Port_already_open = "The port is already open.";

	// Token: 0x0400080B RID: 2059
	public const string Cant_be_set_when_open = "'{0}' cannot be set while the port is open.";

	// Token: 0x0400080C RID: 2060
	public const string Max_Baud = "The maximum baud rate for the device is {0}.";

	// Token: 0x0400080D RID: 2061
	public const string In_Break_State = "The port is in the break state and cannot be written to.";

	// Token: 0x0400080E RID: 2062
	public const string Write_timed_out = "The write timed out.";

	// Token: 0x0400080F RID: 2063
	public const string CantSetRtsWithHandshaking = "RtsEnable cannot be accessed if Handshake is set to RequestToSend or RequestToSendXOnXOff.";

	// Token: 0x04000810 RID: 2064
	public const string NotSupportedEncoding = "SerialPort does not support encoding '{0}'.  The supported encodings include ASCIIEncoding, UTF8Encoding, UnicodeEncoding, UTF32Encoding, and most single or double byte code pages.  For a complete list please see the documentation.";

	// Token: 0x04000811 RID: 2065
	public const string Arg_InvalidSerialPort = "The given port name does not start with COM/com or does not resolve to a valid serial port.";

	// Token: 0x04000812 RID: 2066
	public const string Arg_InvalidSerialPortExtended = "The given port name is invalid.  It may be a valid port, but not a serial port.";

	// Token: 0x04000813 RID: 2067
	public const string ArgumentOutOfRange_Bounds_Lower_Upper = "Argument must be between {0} and {1}.";

	// Token: 0x04000814 RID: 2068
	public const string ArgumentOutOfRange_NeedNonNegNumRequired = "Non-negative number required.";

	// Token: 0x04000815 RID: 2069
	public const string ArgumentOutOfRange_Timeout = "The timeout must be greater than or equal to -1.";

	// Token: 0x04000816 RID: 2070
	public const string ArgumentOutOfRange_WriteTimeout = "The timeout must be either a positive number or -1.";

	// Token: 0x04000817 RID: 2071
	public const string IO_OperationAborted = "The I/O operation has been aborted because of either a thread exit or an application request.";

	// Token: 0x04000818 RID: 2072
	public const string InvalidNullEmptyArgument = "Argument {0} cannot be null or zero-length.";

	// Token: 0x04000819 RID: 2073
	public const string Arg_WrongAsyncResult = "IAsyncResult object did not come from the corresponding async method on this type.";

	// Token: 0x0400081A RID: 2074
	public const string InvalidOperation_EndReadCalledMultiple = "EndRead can only be called once for each asynchronous operation.";

	// Token: 0x0400081B RID: 2075
	public const string InvalidOperation_EndWriteCalledMultiple = "EndWrite can only be called once for each asynchronous operation.";

	// Token: 0x0400081C RID: 2076
	public const string IO_PortNotFound = "The specified port does not exist.";

	// Token: 0x0400081D RID: 2077
	public const string IO_PortNotFoundFileName = "The port '{0}' does not exist.";

	// Token: 0x0400081E RID: 2078
	public const string PlatformNotSupported_IOPorts = "System.IO.Ports is currently only supported on Windows.";

	// Token: 0x0400081F RID: 2079
	public const string PlatformNotSupported_SerialPort_GetPortNames = "Enumeration of serial port names is not supported on the current platform.";

	// Token: 0x04000820 RID: 2080
	public const string NotSupported_CannotCallEqualsOnSpan = "Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.";

	// Token: 0x04000821 RID: 2081
	public const string NotSupported_CannotCallGetHashCodeOnSpan = "GetHashCode() on Span and ReadOnlySpan is not supported.";

	// Token: 0x04000822 RID: 2082
	public const string Argument_InvalidTypeWithPointersNotSupported = "Cannot use type '{0}'. Only value types without pointers or references are supported.";

	// Token: 0x04000823 RID: 2083
	public const string Argument_DestinationTooShort = "Destination is too short.";

	// Token: 0x04000824 RID: 2084
	public const string MemoryDisposed = "Memory<T> has been disposed.";

	// Token: 0x04000825 RID: 2085
	public const string OutstandingReferences = "Release all references before disposing this instance.";

	// Token: 0x04000826 RID: 2086
	public const string Argument_BadFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x04000827 RID: 2087
	public const string Argument_GWithPrecisionNotSupported = "The 'G' format combined with a precision is not supported.";

	// Token: 0x04000828 RID: 2088
	public const string Argument_CannotParsePrecision = "Characters following the format symbol must be a number of {0} or less.";

	// Token: 0x04000829 RID: 2089
	public const string Argument_PrecisionTooLarge = "Precision cannot be larger than {0}.";

	// Token: 0x0400082A RID: 2090
	public const string Argument_OverlapAlignmentMismatch = "Overlapping spans have mismatching alignment.";

	// Token: 0x0400082B RID: 2091
	public const string EndPositionNotReached = "End position was not reached during enumeration.";

	// Token: 0x0400082C RID: 2092
	public const string UnexpectedSegmentType = "Unexpected segment type.";

	// Token: 0x0400082D RID: 2093
	public const string net_log_listener_delegate_exception = "Sending 500 response, AuthenticationSchemeSelectorDelegate threw an exception: {0}.";

	// Token: 0x0400082E RID: 2094
	public const string net_log_listener_unsupported_authentication_scheme = "Received a request with an unsupported authentication scheme, Authorization:{0} SupportedSchemes:{1}.";

	// Token: 0x0400082F RID: 2095
	public const string net_log_listener_unmatched_authentication_scheme = "Received a request with an unmatched or no authentication scheme. AuthenticationSchemes:{0}, Authorization:{1}.";

	// Token: 0x04000830 RID: 2096
	public const string net_io_invalidasyncresult = "The IAsyncResult object was not returned from the corresponding asynchronous method on this class.";

	// Token: 0x04000831 RID: 2097
	public const string net_io_invalidendcall = "{0} can only be called once for each asynchronous operation.";

	// Token: 0x04000832 RID: 2098
	public const string net_listener_cannot_set_custom_cbt = "Custom channel bindings are not supported.";

	// Token: 0x04000833 RID: 2099
	public const string net_listener_detach_error = "Can't detach Url group from request queue. Status code: {0}.";

	// Token: 0x04000834 RID: 2100
	public const string net_listener_scheme = "Only Uri prefixes starting with 'http://' or 'https://' are supported.";

	// Token: 0x04000835 RID: 2101
	public const string net_listener_host = "Only Uri prefixes with a valid hostname are supported.";

	// Token: 0x04000836 RID: 2102
	public const string net_listener_not_supported = "The request is not supported.";

	// Token: 0x04000837 RID: 2103
	public const string net_listener_mustcall = "Please call the {0} method before calling this method.";

	// Token: 0x04000838 RID: 2104
	public const string net_listener_slash = "Only Uri prefixes ending in '/' are allowed.";

	// Token: 0x04000839 RID: 2105
	public const string net_listener_already = "Failed to listen on prefix '{0}' because it conflicts with an existing registration on the machine.";

	// Token: 0x0400083A RID: 2106
	public const string net_log_listener_no_cbt_disabled = "No channel binding check because extended protection is disabled.";

	// Token: 0x0400083B RID: 2107
	public const string net_log_listener_no_cbt_http = "No channel binding check for requests without a secure channel.";

	// Token: 0x0400083C RID: 2108
	public const string net_log_listener_no_cbt_trustedproxy = "No channel binding check for the trusted proxy scenario.";

	// Token: 0x0400083D RID: 2109
	public const string net_log_listener_cbt = "Channel binding check enabled.";

	// Token: 0x0400083E RID: 2110
	public const string net_log_listener_no_spn_kerberos = "No explicit service name check because Kerberos authentication already validates the service name.";

	// Token: 0x0400083F RID: 2111
	public const string net_log_listener_no_spn_disabled = "No service name check because extended protection is disabled.";

	// Token: 0x04000840 RID: 2112
	public const string net_log_listener_no_spn_cbt = "No service name check because the channel binding was already checked.";

	// Token: 0x04000841 RID: 2113
	public const string net_log_listener_no_spn_whensupported = "No service name check because the client did not provide a service name and the server was configured for PolicyEnforcement.WhenSupported.";

	// Token: 0x04000842 RID: 2114
	public const string net_log_listener_no_spn_loopback = "No service name check because the authentication was from a client on the local machine.";

	// Token: 0x04000843 RID: 2115
	public const string net_log_listener_spn = "Client provided service name '{0}'.";

	// Token: 0x04000844 RID: 2116
	public const string net_log_listener_spn_passed = "Service name check succeeded.";

	// Token: 0x04000845 RID: 2117
	public const string net_log_listener_spn_failed = "Service name check failed.";

	// Token: 0x04000846 RID: 2118
	public const string net_log_listener_spn_failed_always = "Service name check failed because the client did not provide a service name and the server was configured for PolicyEnforcement.Always.";

	// Token: 0x04000847 RID: 2119
	public const string net_log_listener_spn_failed_empty = "No acceptable service names were configured!";

	// Token: 0x04000848 RID: 2120
	public const string net_log_listener_spn_failed_dump = "Dumping acceptable service names:";

	// Token: 0x04000849 RID: 2121
	public const string net_log_listener_spn_add = "Adding default service name '{0}' from prefix '{1}'.";

	// Token: 0x0400084A RID: 2122
	public const string net_log_listener_spn_not_add = "No default service name added for prefix '{0}'.";

	// Token: 0x0400084B RID: 2123
	public const string net_log_listener_spn_remove = "Removing default service name '{0}' from prefix '{1}'.";

	// Token: 0x0400084C RID: 2124
	public const string net_log_listener_spn_not_remove = "No default service name removed for prefix '{0}'.";

	// Token: 0x0400084D RID: 2125
	public const string net_listener_no_spns = "No service names could be determined from the registered prefixes. Either add prefixes from which default service names can be derived or specify an ExtendedProtectionPolicy object which contains an explicit list of service names.";

	// Token: 0x0400084E RID: 2126
	public const string net_ssp_dont_support_cbt = "The Security Service Providers don't support extended protection. Please install the latest Security Service Providers update.";

	// Token: 0x0400084F RID: 2127
	public const string net_PropertyNotImplementedException = "This property is not implemented by this class.";

	// Token: 0x04000850 RID: 2128
	public const string net_array_too_small = "The target array is too small.";

	// Token: 0x04000851 RID: 2129
	public const string net_listener_mustcompletecall = "The in-progress method {0} must be completed first.";

	// Token: 0x04000852 RID: 2130
	public const string net_listener_invalid_cbt_type = "Querying the {0} Channel Binding is not supported.";

	// Token: 0x04000853 RID: 2131
	public const string net_listener_callinprogress = "Cannot re-call {0} while a previous call is still in progress.";

	// Token: 0x04000854 RID: 2132
	public const string net_log_listener_cant_create_uri = "Can't create Uri from string '{0}://{1}{2}{3}'.";

	// Token: 0x04000855 RID: 2133
	public const string net_log_listener_cant_convert_raw_path = "Can't convert Uri path '{0}' using encoding '{1}'.";

	// Token: 0x04000856 RID: 2134
	public const string net_log_listener_cant_convert_percent_value = "Can't convert percent encoded value '{0}'.";

	// Token: 0x04000857 RID: 2135
	public const string net_log_listener_cant_convert_to_utf8 = "Can't convert string '{0}' into UTF-8 bytes: {1}";

	// Token: 0x04000858 RID: 2136
	public const string net_log_listener_cant_convert_bytes = "Can't convert bytes '{0}' into UTF-16 characters: {1}";

	// Token: 0x04000859 RID: 2137
	public const string net_invalidstatus = "The status code must be exactly three digits.";

	// Token: 0x0400085A RID: 2138
	public const string net_WebHeaderInvalidControlChars = "Specified value has invalid Control characters.";

	// Token: 0x0400085B RID: 2139
	public const string net_rspsubmitted = "This operation cannot be performed after the response has been submitted.";

	// Token: 0x0400085C RID: 2140
	public const string net_nochunkuploadonhttp10 = "Chunked encoding upload is not supported on the HTTP/1.0 protocol.";

	// Token: 0x0400085D RID: 2141
	public const string net_cookie_exists = "Cookie already exists.";

	// Token: 0x0400085E RID: 2142
	public const string net_clsmall = "The Content-Length value must be greater than or equal to zero.";

	// Token: 0x0400085F RID: 2143
	public const string net_wrongversion = "Only HTTP/1.0 and HTTP/1.1 version requests are currently supported.";

	// Token: 0x04000860 RID: 2144
	public const string net_noseek = "This stream does not support seek operations.";

	// Token: 0x04000861 RID: 2145
	public const string net_writeonlystream = "The stream does not support reading.";

	// Token: 0x04000862 RID: 2146
	public const string net_entitytoobig = "Bytes to be written to the stream exceed the Content-Length bytes size specified.";

	// Token: 0x04000863 RID: 2147
	public const string net_io_notenoughbyteswritten = "Cannot close stream until all bytes are written.";

	// Token: 0x04000864 RID: 2148
	public const string net_listener_close_urlgroup_error = "Can't close Url group. Status code: {0}.";

	// Token: 0x04000865 RID: 2149
	public const string net_WebSockets_NativeSendResponseHeaders = "An error occurred when sending the WebSocket HTTP upgrade response during the {0} operation. The HRESULT returned is '{1}'";

	// Token: 0x04000866 RID: 2150
	public const string net_WebSockets_ClientAcceptingNoProtocols = "The WebSocket client did not request any protocols, but server attempted to accept '{0}' protocol(s). ";

	// Token: 0x04000867 RID: 2151
	public const string net_WebSockets_AcceptUnsupportedProtocol = "The WebSocket client request requested '{0}' protocol(s), but server is only accepting '{1}' protocol(s).";

	// Token: 0x04000868 RID: 2152
	public const string net_WebSockets_AcceptNotAWebSocket = "The {0} operation was called on an incoming request that did not specify a '{1}: {2}' header or the {2} header not contain '{3}'. {2} specified by the client was '{4}'.";

	// Token: 0x04000869 RID: 2153
	public const string net_WebSockets_AcceptHeaderNotFound = "The {0} operation was called on an incoming WebSocket request without required '{1}' header. ";

	// Token: 0x0400086A RID: 2154
	public const string net_WebSockets_AcceptUnsupportedWebSocketVersion = "The {0} operation was called on an incoming request with WebSocket version '{1}', expected '{2}'. ";

	// Token: 0x0400086B RID: 2155
	public const string net_WebSockets_InvalidEmptySubProtocol = "Empty string is not a valid subprotocol value. Please use \\\"null\\\" to specify no value.";

	// Token: 0x0400086C RID: 2156
	public const string net_WebSockets_InvalidCharInProtocolString = "The WebSocket protocol '{0}' is invalid because it contains the invalid character '{1}'.";

	// Token: 0x0400086D RID: 2157
	public const string net_WebSockets_ReasonNotNull = "The close status description '{0}' is invalid. When using close status code '{1}' the description must be null.";

	// Token: 0x0400086E RID: 2158
	public const string net_WebSockets_InvalidCloseStatusCode = "The close status code '{0}' is reserved for system use only and cannot be specified when calling this method.";

	// Token: 0x0400086F RID: 2159
	public const string net_WebSockets_InvalidCloseStatusDescription = "The close status description '{0}' is too long. The UTF8-representation of the status description must not be longer than {1} bytes.";

	// Token: 0x04000870 RID: 2160
	public const string net_WebSockets_ArgumentOutOfRange_TooSmall = "The argument must be a value greater than {0}.";

	// Token: 0x04000871 RID: 2161
	public const string net_WebSockets_ArgumentOutOfRange_TooBig = "The value of the '{0}' parameter ({1}) must be less than or equal to {2}.";

	// Token: 0x04000872 RID: 2162
	public const string net_WebSockets_UnsupportedPlatform = "The WebSocket protocol is not supported on this platform.";

	// Token: 0x04000873 RID: 2163
	public const string net_readonlystream = "The stream does not support writing.";

	// Token: 0x04000874 RID: 2164
	public const string net_WebSockets_InvalidState_ClosedOrAborted = "The '{0}' instance cannot be used for communication because it has been transitioned into the '{1}' state.";

	// Token: 0x04000875 RID: 2165
	public const string net_WebSockets_ReceiveAsyncDisallowedAfterCloseAsync = "The WebSocket is in an invalid state for this operation. The '{0}' method has already been called before on this instance. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x04000876 RID: 2166
	public const string net_Websockets_AlreadyOneOutstandingOperation = "There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.";

	// Token: 0x04000877 RID: 2167
	public const string net_WebSockets_InvalidMessageType = "The received message type '{2}' is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x04000878 RID: 2168
	public const string net_WebSockets_InvalidBufferType = "The buffer type '{0}' is invalid. Valid buffer types are: '{1}', '{2}', '{3}', '{4}', '{5}'.";

	// Token: 0x04000879 RID: 2169
	public const string net_WebSockets_ArgumentOutOfRange_InternalBuffer = "The byte array must have a length of at least '{0}' bytes.  ";

	// Token: 0x0400087A RID: 2170
	public const string net_WebSockets_Argument_InvalidMessageType = "The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ";

	// Token: 0x0400087B RID: 2171
	public const string net_securitypackagesupport = "The requested security package is not supported.";

	// Token: 0x0400087C RID: 2172
	public const string net_log_operation_failed_with_error = "{0} failed with error {1}.";

	// Token: 0x0400087D RID: 2173
	public const string net_MethodNotImplementedException = "This method is not implemented by this class.";

	// Token: 0x0400087E RID: 2174
	public const string event_OperationReturnedSomething = "{0} returned {1}.";

	// Token: 0x0400087F RID: 2175
	public const string net_invalid_enum = "The specified value is not valid in the '{0}' enumeration.";

	// Token: 0x04000880 RID: 2176
	public const string net_auth_message_not_encrypted = "Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.";

	// Token: 0x04000881 RID: 2177
	public const string SSPIInvalidHandleType = "'{0}' is not a supported handle type.";

	// Token: 0x04000882 RID: 2178
	public const string net_io_operation_aborted = "I/O operation aborted: '{0}'.";

	// Token: 0x04000883 RID: 2179
	public const string net_invalid_path = "Invalid path.";

	// Token: 0x04000884 RID: 2180
	public const string net_listener_auth_errors = "Authentication errors.";

	// Token: 0x04000885 RID: 2181
	public const string net_listener_close = "Listener closed.";

	// Token: 0x04000886 RID: 2182
	public const string net_invalid_port = "Invalid port in prefix.";

	// Token: 0x04000887 RID: 2183
	public const string net_WebSockets_InvalidState = "The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'";

	// Token: 0x04000888 RID: 2184
	public const string net_unknown_prefix = "The URI prefix is not recognized.";

	// Token: 0x04000889 RID: 2185
	public const string net_reqsubmitted = "This operation cannot be performed after the request has been submitted.";

	// Token: 0x0400088A RID: 2186
	public const string net_io_timeout_use_ge_zero = "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.";

	// Token: 0x0400088B RID: 2187
	public const string net_writestarted = "This property cannot be set after writing has started.";

	// Token: 0x0400088C RID: 2188
	public const string net_badmethod = "Cannot set null or blank methods on request.";

	// Token: 0x0400088D RID: 2189
	public const string net_servererror = "The remote server returned an error: ({0}) {1}.";

	// Token: 0x0400088E RID: 2190
	public const string net_reqaborted = "The request was aborted: The request was canceled.";

	// Token: 0x0400088F RID: 2191
	public const string net_OperationNotSupportedException = "This operation is not supported.";

	// Token: 0x04000890 RID: 2192
	public const string net_nouploadonget = "Cannot send a content-body with this verb-type.";

	// Token: 0x04000891 RID: 2193
	public const string net_repcall = "Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.";

	// Token: 0x04000892 RID: 2194
	public const string net_securityprotocolnotsupported = "The requested security protocol is not supported.";

	// Token: 0x04000893 RID: 2195
	public const string net_requestaborted = "The request was aborted: {0}.";

	// Token: 0x04000894 RID: 2196
	public const string net_webstatus_Timeout = "The operation has timed out.";

	// Token: 0x04000895 RID: 2197
	public const string net_baddate = "The value of the date string in the header is invalid.";

	// Token: 0x04000896 RID: 2198
	public const string net_connarg = "Keep-Alive and Close may not be set using this property.";

	// Token: 0x04000897 RID: 2199
	public const string net_fromto = "The From parameter cannot be less than To.";

	// Token: 0x04000898 RID: 2200
	public const string net_needchunked = "TransferEncoding requires the SendChunked property to be set to true.";

	// Token: 0x04000899 RID: 2201
	public const string net_no100 = "100-Continue may not be set using this property.";

	// Token: 0x0400089A RID: 2202
	public const string net_nochunked = "Chunked encoding must be set via the SendChunked property.";

	// Token: 0x0400089B RID: 2203
	public const string net_nottoken = "The supplied string is not a valid HTTP token.";

	// Token: 0x0400089C RID: 2204
	public const string net_rangetoosmall = "The From or To parameter cannot be less than 0.";

	// Token: 0x0400089D RID: 2205
	public const string net_rangetype = "A different range specifier has already been added to this request.";

	// Token: 0x0400089E RID: 2206
	public const string net_toosmall = "The specified value must be greater than 0.";

	// Token: 0x0400089F RID: 2207
	public const string net_WebHeaderInvalidCRLFChars = "Specified value has invalid CRLF characters.";

	// Token: 0x040008A0 RID: 2208
	public const string net_WebHeaderInvalidHeaderChars = "Specified value has invalid HTTP Header characters.";

	// Token: 0x040008A1 RID: 2209
	public const string net_timeout = "The operation has timed out.";

	// Token: 0x040008A2 RID: 2210
	public const string net_completed_result = "This operation cannot be performed on a completed asynchronous result object.";

	// Token: 0x040008A3 RID: 2211
	public const string net_PropertyNotSupportedException = "This property is not supported by this class.";

	// Token: 0x040008A4 RID: 2212
	public const string net_InvalidStatusCode = "The server returned a status code outside the valid range of 100-599.";

	// Token: 0x040008A5 RID: 2213
	public const string net_io_timeout_use_gt_zero = "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.";

	// Token: 0x040008A6 RID: 2214
	public const string net_ftp_servererror = "The remote server returned an error: {0}.";

	// Token: 0x040008A7 RID: 2215
	public const string net_ftp_active_address_different = "The data connection was made from an address that is different than the address to which the FTP connection was made.";

	// Token: 0x040008A8 RID: 2216
	public const string net_ftp_invalid_method_name = "FTP Method names cannot be null or empty.";

	// Token: 0x040008A9 RID: 2217
	public const string net_ftp_invalid_renameto = "The RenameTo filename cannot be null or empty.";

	// Token: 0x040008AA RID: 2218
	public const string net_ftp_invalid_response_filename = "The server returned the filename ({0}) which is not valid.";

	// Token: 0x040008AB RID: 2219
	public const string net_ftp_invalid_status_response = "The status response ({0}) is not expected in response to '{1}' command.";

	// Token: 0x040008AC RID: 2220
	public const string net_ftp_invalid_uri = "The requested URI is invalid for this FTP command.";

	// Token: 0x040008AD RID: 2221
	public const string net_ftp_no_defaultcreds = "Default credentials are not supported on an FTP request.";

	// Token: 0x040008AE RID: 2222
	public const string net_ftp_response_invalid_format = "The response string '{0}' has invalid format.";

	// Token: 0x040008AF RID: 2223
	public const string net_ftp_server_failed_passive = "The server failed the passive mode request with status response ({0}).";

	// Token: 0x040008B0 RID: 2224
	public const string net_ftp_unsupported_method = "This method is not supported.";

	// Token: 0x040008B1 RID: 2225
	public const string net_ftp_protocolerror = "The underlying connection was closed: The server committed a protocol violation";

	// Token: 0x040008B2 RID: 2226
	public const string net_ftp_receivefailure = "The underlying connection was closed: An unexpected error occurred on a receive";

	// Token: 0x040008B3 RID: 2227
	public const string net_webstatus_NameResolutionFailure = "The remote name could not be resolved";

	// Token: 0x040008B4 RID: 2228
	public const string net_webstatus_ConnectFailure = "Unable to connect to the remote server";

	// Token: 0x040008B5 RID: 2229
	public const string net_ftpstatuscode_ServiceNotAvailable = "Service not available, closing control connection.";

	// Token: 0x040008B6 RID: 2230
	public const string net_ftpstatuscode_CantOpenData = "Can't open data connection";

	// Token: 0x040008B7 RID: 2231
	public const string net_ftpstatuscode_ConnectionClosed = "Connection closed; transfer aborted";

	// Token: 0x040008B8 RID: 2232
	public const string net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy = "File unavailable (e.g., file busy)";

	// Token: 0x040008B9 RID: 2233
	public const string net_ftpstatuscode_ActionAbortedLocalProcessingError = "Local error in processing";

	// Token: 0x040008BA RID: 2234
	public const string net_ftpstatuscode_ActionNotTakenInsufficientSpace = "Insufficient storage space in system";

	// Token: 0x040008BB RID: 2235
	public const string net_ftpstatuscode_CommandSyntaxError = "Syntax error, command unrecognized";

	// Token: 0x040008BC RID: 2236
	public const string net_ftpstatuscode_ArgumentSyntaxError = "Syntax error in parameters or arguments";

	// Token: 0x040008BD RID: 2237
	public const string net_ftpstatuscode_CommandNotImplemented = "Command not implemented";

	// Token: 0x040008BE RID: 2238
	public const string net_ftpstatuscode_BadCommandSequence = "Bad sequence of commands";

	// Token: 0x040008BF RID: 2239
	public const string net_ftpstatuscode_NotLoggedIn = "Not logged in";

	// Token: 0x040008C0 RID: 2240
	public const string net_ftpstatuscode_AccountNeeded = "Need account for storing files";

	// Token: 0x040008C1 RID: 2241
	public const string net_ftpstatuscode_ActionNotTakenFileUnavailable = "File unavailable (e.g., file not found, no access)";

	// Token: 0x040008C2 RID: 2242
	public const string net_ftpstatuscode_ActionAbortedUnknownPageType = "Page type unknown";

	// Token: 0x040008C3 RID: 2243
	public const string net_ftpstatuscode_FileActionAborted = "Exceeded storage allocation (for current directory or data set)";

	// Token: 0x040008C4 RID: 2244
	public const string net_ftpstatuscode_ActionNotTakenFilenameNotAllowed = "File name not allowed";

	// Token: 0x040008C5 RID: 2245
	public const string net_invalid_host = "The specified value is not a valid Host header string.";

	// Token: 0x040008C6 RID: 2246
	public const string net_sockets_connect_multiconnect_notsupported = "Sockets on this platform are invalid for use after a failed connection attempt.";

	// Token: 0x040008C7 RID: 2247
	public const string net_sockets_dualmode_receivefrom_notsupported = "This platform does not support packet information for dual-mode sockets.  If packet information is not required, use Socket.Receive.  If packet information is required set Socket.DualMode to false.";

	// Token: 0x040008C8 RID: 2248
	public const string net_sockets_accept_receive_notsupported = "This platform does not support receiving data with Socket.AcceptAsync.  Instead, make a separate call to Socket.ReceiveAsync.";

	// Token: 0x040008C9 RID: 2249
	public const string net_sockets_duplicateandclose_notsupported = "This platform does not support Socket.DuplicateAndClose.  Instead, create a new socket.";

	// Token: 0x040008CA RID: 2250
	public const string net_sockets_transmitfileoptions_notsupported = "This platform does not support TransmitFileOptions other than TransmitFileOptions.UseDefaultWorkerThread.";

	// Token: 0x040008CB RID: 2251
	public const string ArgumentOutOfRange_PathLengthInvalid = "The path '{0}' is of an invalid length for use with domain sockets on this platform.  The length must be between 1 and {1} characters, inclusive.";

	// Token: 0x040008CC RID: 2252
	public const string net_io_readwritefailure = "Unable to transfer data on the transport connection: {0}.";

	// Token: 0x040008CD RID: 2253
	public const string PlatformNotSupported_AcceptSocket = "Accepting into an existing Socket is not supported on this platform.";

	// Token: 0x040008CE RID: 2254
	public const string PlatformNotSupported_IOControl = "Socket.IOControl handles Windows-specific control codes and is not supported on this platform.";

	// Token: 0x040008CF RID: 2255
	public const string PlatformNotSupported_IPProtectionLevel = "IP protection level cannot be controlled on this platform.";

	// Token: 0x040008D0 RID: 2256
	public const string InvalidOperation_BufferNotExplicitArray = "This operation may only be performed when the buffer was set using the SetBuffer overload that accepts an array.";

	// Token: 0x040008D1 RID: 2257
	public const string InvalidOperation_IncorrectToken = "The result of the operation was already consumed and may not be used again.";

	// Token: 0x040008D2 RID: 2258
	public const string InvalidOperation_MultipleContinuations = "Another continuation was already registered.";

	// Token: 0x040008D3 RID: 2259
	public const string net_http_httpmethod_format_error = "The format of the HTTP method is invalid.";

	// Token: 0x040008D4 RID: 2260
	public const string net_http_httpmethod_notsupported_error = "The HTTP method '{0}' is not supported on this platform.";

	// Token: 0x040008D5 RID: 2261
	public const string net_http_reasonphrase_format_error = "The reason phrase must not contain new-line characters.";

	// Token: 0x040008D6 RID: 2262
	public const string net_http_copyto_array_too_small = "The number of elements is greater than the available space from arrayIndex to the end of the destination array.";

	// Token: 0x040008D7 RID: 2263
	public const string net_http_headers_not_found = "The given header was not found.";

	// Token: 0x040008D8 RID: 2264
	public const string net_http_headers_single_value_header = "Cannot add value because header '{0}' does not support multiple values.";

	// Token: 0x040008D9 RID: 2265
	public const string net_http_headers_invalid_header_name = "The header name format is invalid.";

	// Token: 0x040008DA RID: 2266
	public const string net_http_headers_invalid_value = "The format of value '{0}' is invalid.";

	// Token: 0x040008DB RID: 2267
	public const string net_http_headers_not_allowed_header_name = "Misused header name. Make sure request headers are used with HttpRequestMessage, response headers with HttpResponseMessage, and content headers with HttpContent objects.";

	// Token: 0x040008DC RID: 2268
	public const string net_http_headers_invalid_host_header = "The specified value is not a valid 'Host' header string.";

	// Token: 0x040008DD RID: 2269
	public const string net_http_headers_invalid_from_header = "The specified value is not a valid 'From' header string.";

	// Token: 0x040008DE RID: 2270
	public const string net_http_headers_invalid_etag_name = "The specified value is not a valid quoted string.";

	// Token: 0x040008DF RID: 2271
	public const string net_http_headers_invalid_range = "Invalid range. At least one of the two parameters must not be null.";

	// Token: 0x040008E0 RID: 2272
	public const string net_http_headers_no_newlines = "New-line characters in header values must be followed by a white-space character.";

	// Token: 0x040008E1 RID: 2273
	public const string net_http_content_buffersize_exceeded = "Cannot write more bytes to the buffer than the configured maximum buffer size: {0}.";

	// Token: 0x040008E2 RID: 2274
	public const string net_http_content_no_task_returned = "The async operation did not return a System.Threading.Tasks.Task object.";

	// Token: 0x040008E3 RID: 2275
	public const string net_http_content_stream_already_read = "The stream was already consumed. It cannot be read again.";

	// Token: 0x040008E4 RID: 2276
	public const string net_http_content_readonly_stream = "The stream does not support writing.";

	// Token: 0x040008E5 RID: 2277
	public const string net_http_content_invalid_charset = "The character set provided in ContentType is invalid. Cannot read content as string using an invalid character set.";

	// Token: 0x040008E6 RID: 2278
	public const string net_http_content_stream_copy_error = "Error while copying content to a stream.";

	// Token: 0x040008E7 RID: 2279
	public const string net_http_argument_empty_string = "The value cannot be null or empty.";

	// Token: 0x040008E8 RID: 2280
	public const string net_http_client_request_already_sent = "The request message was already sent. Cannot send the same request message multiple times.";

	// Token: 0x040008E9 RID: 2281
	public const string net_http_operation_started = "This instance has already started one or more requests. Properties can only be modified before sending the first request.";

	// Token: 0x040008EA RID: 2282
	public const string net_http_client_execution_error = "An error occurred while sending the request.";

	// Token: 0x040008EB RID: 2283
	public const string net_http_client_absolute_baseaddress_required = "The base address must be an absolute URI.";

	// Token: 0x040008EC RID: 2284
	public const string net_http_client_invalid_requesturi = "An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set.";

	// Token: 0x040008ED RID: 2285
	public const string net_http_client_http_baseaddress_required = "Only 'http' and 'https' schemes are allowed.";

	// Token: 0x040008EE RID: 2286
	public const string net_http_parser_invalid_base64_string = "Value '{0}' is not a valid Base64 string. Error: {1}";

	// Token: 0x040008EF RID: 2287
	public const string net_http_handler_noresponse = "Handler did not return a response message.";

	// Token: 0x040008F0 RID: 2288
	public const string net_http_handler_norequest = "A request message must be provided. It cannot be null.";

	// Token: 0x040008F1 RID: 2289
	public const string net_http_message_not_success_statuscode = "Response status code does not indicate success: {0} ({1}).";

	// Token: 0x040008F2 RID: 2290
	public const string net_http_content_field_too_long = "The field cannot be longer than {0} characters.";

	// Token: 0x040008F3 RID: 2291
	public const string net_http_log_headers_no_newlines = "Value for header '{0}' contains invalid new-line characters. Value: '{1}'.";

	// Token: 0x040008F4 RID: 2292
	public const string net_http_log_headers_invalid_quality = "The 'q' value is invalid: '{0}'.";

	// Token: 0x040008F5 RID: 2293
	public const string net_http_log_headers_wrong_email_format = "Value '{0}' is not a valid email address. Error: {1}";

	// Token: 0x040008F6 RID: 2294
	public const string net_http_handler_not_assigned = "The inner handler has not been assigned.";

	// Token: 0x040008F7 RID: 2295
	public const string net_http_invalid_enable_first = "The {0} property must be set to '{1}' to use this property.";

	// Token: 0x040008F8 RID: 2296
	public const string net_http_content_buffersize_limit = "Buffering more than {0} bytes is not supported.";

	// Token: 0x040008F9 RID: 2297
	public const string net_http_value_not_supported = "The value '{0}' is not supported for property '{1}'.";

	// Token: 0x040008FA RID: 2298
	public const string net_http_io_read = "The read operation failed, see inner exception.";

	// Token: 0x040008FB RID: 2299
	public const string net_http_io_read_incomplete = "Unable to read data from the transport connection. The connection was closed before all data could be read. Expected {0} bytes, read {1} bytes.";

	// Token: 0x040008FC RID: 2300
	public const string net_http_io_write = "The write operation failed, see inner exception.";

	// Token: 0x040008FD RID: 2301
	public const string net_http_chunked_not_allowed_with_empty_content = "'Transfer-Encoding: chunked' header can not be used when content object is not specified.";

	// Token: 0x040008FE RID: 2302
	public const string net_http_invalid_cookiecontainer = "When using CookieUsePolicy.UseSpecifiedCookieContainer, the CookieContainer property must not be null.";

	// Token: 0x040008FF RID: 2303
	public const string net_http_invalid_proxyusepolicy = "When using a non-null Proxy, the WindowsProxyUsePolicy property must be set to WindowsProxyUsePolicy.UseCustomProxy.";

	// Token: 0x04000900 RID: 2304
	public const string net_http_invalid_proxy = "When using WindowsProxyUsePolicy.UseCustomProxy, the Proxy property must not be null.";

	// Token: 0x04000901 RID: 2305
	public const string net_http_value_must_be_greater_than = "The specified value must be greater than {0}.";

	// Token: 0x04000902 RID: 2306
	public const string net_http_unix_invalid_credential = "The libcurl library in use ({0}) does not support different credentials for different authentication schemes.";

	// Token: 0x04000903 RID: 2307
	public const string net_http_unix_https_support_unavailable_libcurl = "The libcurl library in use ({0}) does not support HTTPS.";

	// Token: 0x04000904 RID: 2308
	public const string net_http_content_no_concurrent_reads = "The stream does not support concurrent read operations.";

	// Token: 0x04000905 RID: 2309
	public const string net_http_username_empty_string = "The username for a credential object cannot be null or empty.";

	// Token: 0x04000906 RID: 2310
	public const string net_http_no_concurrent_io_allowed = "The stream does not support concurrent I/O read or write operations.";

	// Token: 0x04000907 RID: 2311
	public const string net_http_invalid_response = "The server returned an invalid or unrecognized response.";

	// Token: 0x04000908 RID: 2312
	public const string net_http_unix_handler_disposed = "The handler was disposed of while active operations were in progress.";

	// Token: 0x04000909 RID: 2313
	public const string net_http_buffer_insufficient_length = "The buffer was not long enough.";

	// Token: 0x0400090A RID: 2314
	public const string net_http_response_headers_exceeded_length = "The HTTP response headers length exceeded the set limit of {0} bytes.";

	// Token: 0x0400090B RID: 2315
	public const string ArgumentOutOfRange_NeedNonNegativeNum = "Non-negative number required.";

	// Token: 0x0400090C RID: 2316
	public const string net_http_libcurl_callback_notsupported_sslbackend = "The handler does not support custom handling of certificates with this combination of libcurl ({0}) and its SSL backend (\"{1}\"). An SSL backend based on \"{2}\" is required. Consider using System.Net.Http.SocketsHttpHandler.";

	// Token: 0x0400090D RID: 2317
	public const string net_http_libcurl_callback_notsupported_os = "The handler does not support custom handling of certificates on this operating system. Consider using System.Net.Http.SocketsHttpHandler.";

	// Token: 0x0400090E RID: 2318
	public const string net_http_libcurl_clientcerts_notsupported_sslbackend = "The handler does not support client authentication certificates with this combination of libcurl ({0}) and its SSL backend (\"{1}\"). An SSL backend based on \"{2}\" is required. Consider using System.Net.Http.SocketsHttpHandler.";

	// Token: 0x0400090F RID: 2319
	public const string net_http_libcurl_clientcerts_notsupported_os = "The handler does not support client authentication certificates on this operating system. Consider using System.Net.Http.SocketsHttpHandler.";

	// Token: 0x04000910 RID: 2320
	public const string net_http_libcurl_revocation_notsupported_sslbackend = "The handler does not support changing revocation settings with this combination of libcurl ({0}) and its SSL backend (\"{1}\"). An SSL backend based on \"{2}\" is required. Consider using System.Net.Http.SocketsHttpHandler.";

	// Token: 0x04000911 RID: 2321
	public const string net_http_feature_requires_Windows10Version1607 = "Using this feature requires Windows 10 Version 1607.";

	// Token: 0x04000912 RID: 2322
	public const string net_http_feature_UWPClientCertSupportRequiresCertInPersonalCertificateStore = "Client certificate was not found in the personal (\\\"MY\\\") certificate store. In UWP, client certificates are only supported if they have been added to that certificate store.";

	// Token: 0x04000913 RID: 2323
	public const string net_http_invalid_proxy_scheme = "Only the 'http' scheme is allowed for proxies.";

	// Token: 0x04000914 RID: 2324
	public const string net_http_request_invalid_char_encoding = "Request headers must contain only ASCII characters.";

	// Token: 0x04000915 RID: 2325
	public const string net_http_ssl_connection_failed = "The SSL connection could not be established, see inner exception.";

	// Token: 0x04000916 RID: 2326
	public const string net_http_unsupported_chunking = "HTTP 1.0 does not support chunking.";

	// Token: 0x04000917 RID: 2327
	public const string net_http_unsupported_version = "Request HttpVersion 0.X is not supported.  Use 1.0 or above.";

	// Token: 0x04000918 RID: 2328
	public const string IO_SeekBeforeBegin = "An attempt was made to move the position before the beginning of the stream.";

	// Token: 0x04000919 RID: 2329
	public const string net_http_request_no_host = "CONNECT request must contain Host header.";

	// Token: 0x0400091A RID: 2330
	public const string net_http_winhttp_error = "Error {0} calling {1}, '{2}'.";

	// Token: 0x0400091B RID: 2331
	public const string net_http_authconnectionfailure = "Authentication failed because the connection could not be reused.";

	// Token: 0x0400091C RID: 2332
	public const string net_nego_server_not_supported = "Server implementation is not supported";

	// Token: 0x0400091D RID: 2333
	public const string net_nego_protection_level_not_supported = "Requested protection level is not supported with the gssapi implementation currently installed.";

	// Token: 0x0400091E RID: 2334
	public const string net_context_buffer_too_small = "Insufficient buffer space. Required: {0} Actual: {1}.";

	// Token: 0x0400091F RID: 2335
	public const string net_gssapi_operation_failed_detailed = "GSSAPI operation failed with error - {0} ({1}).";

	// Token: 0x04000920 RID: 2336
	public const string net_gssapi_operation_failed = "GSSAPI operation failed with status: {0} (Minor status: {1}).";

	// Token: 0x04000921 RID: 2337
	public const string net_nego_channel_binding_not_supported = "No support for channel binding on operating systems other than Windows.";

	// Token: 0x04000922 RID: 2338
	public const string net_ntlm_not_possible_default_cred = "NTLM authentication is not possible with default credentials on this platform.";

	// Token: 0x04000923 RID: 2339
	public const string net_nego_not_supported_empty_target_with_defaultcreds = "Target name should be non empty if default credentials are passed.";

	// Token: 0x04000924 RID: 2340
	public const string Arg_ElementsInSourceIsGreaterThanDestination = "Number of elements in source vector is greater than the destination array";

	// Token: 0x04000925 RID: 2341
	public const string Arg_NullArgumentNullRef = "The method was called with a null array argument.";

	// Token: 0x04000926 RID: 2342
	public const string Arg_TypeNotSupported = "Specified type is not supported";

	// Token: 0x04000927 RID: 2343
	public const string Arg_InsufficientNumberOfElements = "At least {0} element(s) are expected in the parameter \"{1}\".";

	// Token: 0x04000928 RID: 2344
	public const string NoMetadataTokenAvailable = "There is no metadata token available for the given member.";

	// Token: 0x04000929 RID: 2345
	public const string PlatformNotSupported_ReflectionTypeExtensions = "System.Reflection.TypeExtensions is not supported on NET Standard 1.3 or 1.5.";

	// Token: 0x0400092A RID: 2346
	public const string Argument_EmptyApplicationName = "ApplicationId cannot have an empty string for the name.";

	// Token: 0x0400092B RID: 2347
	public const string ArgumentOutOfRange_GenericPositive = "Value must be positive.";

	// Token: 0x0400092C RID: 2348
	public const string ArgumentOutOfRange_MustBePositive = "'{0}' must be greater than zero.";

	// Token: 0x0400092D RID: 2349
	public const string InvalidOperation_ComputerName = "Computer name could not be obtained.";

	// Token: 0x0400092E RID: 2350
	public const string InvalidOperation_GetVersion = "OSVersion's call to GetVersionEx failed";

	// Token: 0x0400092F RID: 2351
	public const string PersistedFiles_NoHomeDirectory = "The home directory of the current user could not be determined.";

	// Token: 0x04000930 RID: 2352
	public const string Argument_BadResourceScopeTypeBits = "Unknown value for the ResourceScope: {0}  Too many resource type bits may be set.";

	// Token: 0x04000931 RID: 2353
	public const string Argument_BadResourceScopeVisibilityBits = "Unknown value for the ResourceScope: {0}  Too many resource visibility bits may be set.";

	// Token: 0x04000932 RID: 2354
	public const string ArgumentNull_TypeRequiredByResourceScope = "The type parameter cannot be null when scoping the resource's visibility to Private or Assembly.";

	// Token: 0x04000933 RID: 2355
	public const string Argument_ResourceScopeWrongDirection = "Resource type in the ResourceScope enum is going from a more restrictive resource type to a more general one.  From: \"{0}\"  To: \"{1}\"";

	// Token: 0x04000934 RID: 2356
	public const string AppDomain_Name = "Name:";

	// Token: 0x04000935 RID: 2357
	public const string AppDomain_NoContextPolicies = "There are no context policies.";

	// Token: 0x04000936 RID: 2358
	public const string Arg_MustBeTrue = "Argument must be true.";

	// Token: 0x04000937 RID: 2359
	public const string Arg_CannotUnloadAppDomainException = "Attempt to unload the AppDomain failed.";

	// Token: 0x04000938 RID: 2360
	public const string Arg_AppDomainUnloadedException = "Attempted to access an unloaded AppDomain.";

	// Token: 0x04000939 RID: 2361
	public const string ZeroLengthString = "String cannot have zero length.";

	// Token: 0x0400093A RID: 2362
	public const string EntryPointNotFound = "Entry point not found in assembly";

	// Token: 0x0400093B RID: 2363
	public const string Arg_ContextMarshalException = "Attempted to marshal an object across a context boundary.";

	// Token: 0x0400093C RID: 2364
	public const string AppDomain_Policy_PrincipalTwice = "Default principal object cannot be set twice.";

	// Token: 0x0400093D RID: 2365
	public const string ArgumentNull_Collection = "Collection cannot be null.";

	// Token: 0x0400093E RID: 2366
	public const string ArgumentOutOfRange_ArrayListInsert = "Insertion index was out of range. Must be non-negative and less than or equal to size.";

	// Token: 0x0400093F RID: 2367
	public const string ArgumentOutOfRange_Count = "Count must be positive and count must refer to a location within the string/array/collection.";

	// Token: 0x04000940 RID: 2368
	public const string ArgumentOutOfRange_HashtableLoadFactor = "Load factor needs to be between 0.1 and 1.0.";

	// Token: 0x04000941 RID: 2369
	public const string ArgumentOutOfRange_MustBeNonNegNum = "'{0}' must be non-negative.";

	// Token: 0x04000942 RID: 2370
	public const string Arg_CannotMixComparisonInfrastructure = "The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.";

	// Token: 0x04000943 RID: 2371
	public const string InvalidOperation_HashInsertFailed = "Hashtable insert failed. Load factor too high. The most common cause is multiple threads writing to the Hashtable simultaneously.";

	// Token: 0x04000944 RID: 2372
	public const string InvalidOperation_UnderlyingArrayListChanged = "This range in the underlying list is invalid. A possible cause is that elements were removed.";

	// Token: 0x04000945 RID: 2373
	public const string NotSupported_FixedSizeCollection = "Collection was of a fixed size.";

	// Token: 0x04000946 RID: 2374
	public const string NotSupported_RangeCollection = "The specified operation is not supported on Ranges.";

	// Token: 0x04000947 RID: 2375
	public const string NotSupported_ReadOnlyCollection = "Collection is read-only.";

	// Token: 0x04000948 RID: 2376
	public const string Serialization_KeyValueDifferentSizes = "The keys and values arrays have different sizes.";

	// Token: 0x04000949 RID: 2377
	public const string Serialization_NullKey = "One of the serialized keys is null.";

	// Token: 0x0400094A RID: 2378
	public const string NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed = "Cannot write to a BufferedStream while the read buffer is not empty if the underlying stream is not seekable. Ensure that the stream underlying this BufferedStream can seek or avoid interleaving read and write operations on this BufferedStream.";

	// Token: 0x0400094B RID: 2379
	public const string Argument_StreamNotReadable = "Stream was not readable.";

	// Token: 0x0400094C RID: 2380
	public const string Argument_StreamNotWritable = "Stream was not writable.";

	// Token: 0x0400094D RID: 2381
	public const string ObjectDisposed_ReaderClosed = "Cannot read from a closed TextReader.";

	// Token: 0x0400094E RID: 2382
	public const string ArgumentNull_Child = "Cannot have a null child.";

	// Token: 0x0400094F RID: 2383
	public const string Argument_AttributeNamesMustBeUnique = "Attribute names must be unique.";

	// Token: 0x04000950 RID: 2384
	public const string Argument_InvalidElementName = "Invalid element name '{0}'.";

	// Token: 0x04000951 RID: 2385
	public const string Argument_InvalidElementTag = "Invalid element tag '{0}'.";

	// Token: 0x04000952 RID: 2386
	public const string Argument_InvalidElementText = "Invalid element text '{0}'.";

	// Token: 0x04000953 RID: 2387
	public const string Argument_InvalidElementValue = "Invalid element value '{0}'.";

	// Token: 0x04000954 RID: 2388
	public const string Argument_InvalidFlag = "Value of flags is invalid.";

	// Token: 0x04000955 RID: 2389
	public const string PlatformNotSupported_AppDomains = "Secondary AppDomains are not supported on this platform.";

	// Token: 0x04000956 RID: 2390
	public const string PlatformNotSupported_CAS = "Code Access Security is not supported on this platform.";

	// Token: 0x04000957 RID: 2391
	public const string PlatformNotSupported_AppDomain_ResMon = "AppDomain resource monitoring is not supported on this platform.";

	// Token: 0x04000958 RID: 2392
	public const string Argument_EmptyValue = "Value cannot be empty.";

	// Token: 0x04000959 RID: 2393
	public const string PlatformNotSupported_RuntimeInformation = "RuntimeInformation is not supported for Portable Class Libraries.";

	// Token: 0x0400095A RID: 2394
	public const string Overflow_Negative_Unsigned = "Negative values do not have an unsigned representation.";

	// Token: 0x0400095B RID: 2395
	public const string Cryptography_Oid_InvalidName = "No OID value matches this name.";

	// Token: 0x0400095C RID: 2396
	public const string Cryptography_SSE_InvalidDataSize = "NoLength of the data to encrypt is invalid.";

	// Token: 0x0400095D RID: 2397
	public const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";

	// Token: 0x0400095E RID: 2398
	public const string ObjectDisposed_Generic = "Cannot access a disposed object.";

	// Token: 0x0400095F RID: 2399
	public const string Cryptography_Asn_EnumeratedValueRequiresNonFlagsEnum = "ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.";

	// Token: 0x04000960 RID: 2400
	public const string Cryptography_Asn_NamedBitListRequiresFlagsEnum = "Named bit list operations require an enum with the [Flags] attribute.";

	// Token: 0x04000961 RID: 2401
	public const string Cryptography_Asn_NamedBitListValueTooBig = "The encoded named bit list value is larger than the value size of the '{0}' enum.";

	// Token: 0x04000962 RID: 2402
	public const string Cryptography_Asn_UniversalValueIsFixed = "Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.";

	// Token: 0x04000963 RID: 2403
	public const string Cryptography_Asn_UnusedBitCountRange = "Unused bit count must be between 0 and 7, inclusive.";

	// Token: 0x04000964 RID: 2404
	public const string Cryptography_AsnSerializer_AmbiguousFieldType = "Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.";

	// Token: 0x04000965 RID: 2405
	public const string Cryptography_AsnSerializer_Choice_AllowNullNonNullable = "[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.";

	// Token: 0x04000966 RID: 2406
	public const string Cryptography_AsnSerializer_Choice_ConflictingTagMapping = "The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.";

	// Token: 0x04000967 RID: 2407
	public const string Cryptography_AsnSerializer_Choice_DefaultValueDisallowed = "Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.";

	// Token: 0x04000968 RID: 2408
	public const string Cryptography_AsnSerializer_Choice_NoChoiceWasMade = "An instance of [Choice] type '{0}' has no non-null fields.";

	// Token: 0x04000969 RID: 2409
	public const string Cryptography_AsnSerializer_Choice_NonNullableField = "Field '{0}' on [Choice] type '{1}' can not be assigned a null value.";

	// Token: 0x0400096A RID: 2410
	public const string Cryptography_AsnSerializer_Choice_TooManyValues = "Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.";

	// Token: 0x0400096B RID: 2411
	public const string Cryptography_AsnSerializer_Choice_TypeCycle = "Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.";

	// Token: 0x0400096C RID: 2412
	public const string Cryptography_AsnSerializer_MultipleAsnTypeAttributes = "Field '{0}' on type '{1}' has multiple attributes deriving from '{2}' when at most one is permitted.";

	// Token: 0x0400096D RID: 2413
	public const string Cryptography_AsnSerializer_NoJaggedArrays = "Type '{0}' cannot be serialized or deserialized because it is an array of arrays.";

	// Token: 0x0400096E RID: 2414
	public const string Cryptography_AsnSerializer_NoMultiDimensionalArrays = "Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.";

	// Token: 0x0400096F RID: 2415
	public const string Cryptography_AsnSerializer_NoOpenTypes = "Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.";

	// Token: 0x04000970 RID: 2416
	public const string Cryptography_AsnSerializer_Optional_NonNullableField = "Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.";

	// Token: 0x04000971 RID: 2417
	public const string Cryptography_AsnSerializer_PopulateFriendlyNameOnString = "Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.";

	// Token: 0x04000972 RID: 2418
	public const string Cryptography_AsnSerializer_SetValueException = "Unable to set field {0} on type {1}.";

	// Token: 0x04000973 RID: 2419
	public const string Cryptography_AsnSerializer_SpecificTagChoice = "Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.";

	// Token: 0x04000974 RID: 2420
	public const string Cryptography_AsnSerializer_UnexpectedTypeForAttribute = "Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.";

	// Token: 0x04000975 RID: 2421
	public const string Cryptography_AsnSerializer_UtcTimeTwoDigitYearMaxTooSmall = "Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).";

	// Token: 0x04000976 RID: 2422
	public const string Cryptography_AsnSerializer_UnhandledType = "Could not determine how to serialize or deserialize type '{0}'.";

	// Token: 0x04000977 RID: 2423
	public const string Cryptography_AsnWriter_EncodeUnbalancedStack = "Encode cannot be called while a Sequence or SetOf is still open.";

	// Token: 0x04000978 RID: 2424
	public const string Cryptography_AsnWriter_PopWrongTag = "Cannot pop the requested tag as it is not currently in progress.";

	// Token: 0x04000979 RID: 2425
	public const string Cryptography_BadHashValue = "The hash value is not correct.";

	// Token: 0x0400097A RID: 2426
	public const string Cryptography_BadSignature = "Invalid signature.";

	// Token: 0x0400097B RID: 2427
	public const string Cryptography_Cms_CannotDetermineSignatureAlgorithm = "Could not determine signature algorithm for the signer certificate.";

	// Token: 0x0400097C RID: 2428
	public const string Cryptography_Cms_IncompleteCertChain = "The certificate chain is incomplete, the self-signed root authority could not be determined.";

	// Token: 0x0400097D RID: 2429
	public const string Cryptography_Cms_Invalid_Originator_Identifier_Choice = "Invalid originator identifier choice {0} found in decoded CMS.";

	// Token: 0x0400097E RID: 2430
	public const string Cryptography_Cms_InvalidMessageType = "Invalid cryptographic message type.";

	// Token: 0x0400097F RID: 2431
	public const string Cryptography_Cms_InvalidSignerHashForSignatureAlg = "SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.";

	// Token: 0x04000980 RID: 2432
	public const string Cryptography_Cms_MissingAuthenticatedAttribute = "The cryptographic message does not contain an expected authenticated attribute.";

	// Token: 0x04000981 RID: 2433
	public const string Cryptography_Cms_NoCounterCounterSigner = "Only one level of counter-signatures are supported on this platform.";

	// Token: 0x04000982 RID: 2434
	public const string Cryptography_Cms_NoRecipients = "The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.";

	// Token: 0x04000983 RID: 2435
	public const string Cryptography_Cms_NoSignerCert = "No signer certificate was provided. This platform does not implement the certificate picker UI.";

	// Token: 0x04000984 RID: 2436
	public const string Cryptography_Cms_NoSignerAtIndex = "The signed cryptographic message does not have a signer for the specified signer index.";

	// Token: 0x04000985 RID: 2437
	public const string Cryptography_Cms_RecipientNotFound = "The enveloped-data message does not contain the specified recipient.";

	// Token: 0x04000986 RID: 2438
	public const string Cryptography_Cms_RecipientType_NotSupported = "The recipient type '{0}' is not supported for encryption or decryption on this platform.";

	// Token: 0x04000987 RID: 2439
	public const string Cryptography_Cms_SignerNotFound = "Cannot find the original signer.";

	// Token: 0x04000988 RID: 2440
	public const string Cryptography_Cms_Signing_RequiresPrivateKey = "A certificate with a private key is required.";

	// Token: 0x04000989 RID: 2441
	public const string Cryptography_Cms_TrustFailure = "Certificate trust could not be established. The first reported error is: {0}";

	// Token: 0x0400098A RID: 2442
	public const string Cryptography_Cms_UnknownAlgorithm = "Unknown algorithm '{0}'.";

	// Token: 0x0400098B RID: 2443
	public const string Cryptography_Cms_UnknownKeySpec = "Unable to determine the type of key handle from this keyspec {0}.";

	// Token: 0x0400098C RID: 2444
	public const string Cryptography_Cms_WrongKeyUsage = "The certificate is not valid for the requested usage.";

	// Token: 0x0400098D RID: 2445
	public const string Cryptography_Pkcs_InvalidSignatureParameters = "Invalid signature paramters.";

	// Token: 0x0400098E RID: 2446
	public const string Cryptography_Pkcs_PssParametersMissing = "PSS parameters were not present.";

	// Token: 0x0400098F RID: 2447
	public const string Cryptography_Pkcs_PssParametersHashMismatch = "This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).";

	// Token: 0x04000990 RID: 2448
	public const string Cryptography_Pkcs_PssParametersMgfHashMismatch = "This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).";

	// Token: 0x04000991 RID: 2449
	public const string Cryptography_Pkcs_PssParametersMgfNotSupported = "Mask generation function '{0}' is not supported by this platform.";

	// Token: 0x04000992 RID: 2450
	public const string Cryptography_Pkcs_PssParametersSaltMismatch = "PSS salt size {0} is not supported by this platform with hash algorithm {1}.";

	// Token: 0x04000993 RID: 2451
	public const string Cryptography_TimestampReq_BadNonce = "The response from the timestamping server did not match the request nonce.";

	// Token: 0x04000994 RID: 2452
	public const string Cryptography_TimestampReq_BadResponse = "The response from the timestamping server was not understood.";

	// Token: 0x04000995 RID: 2453
	public const string Cryptography_TimestampReq_Failure = "The timestamping server did not grant the request. The request status is '{0}' with failure info '{1}'.";

	// Token: 0x04000996 RID: 2454
	public const string Cryptography_TimestampReq_NoCertFound = "The timestamping request required the TSA certificate in the response, but it was not found.";

	// Token: 0x04000997 RID: 2455
	public const string Cryptography_TimestampReq_UnexpectedCertFound = "The timestamping request required the TSA certificate not be included in the response, but certificates were present.";

	// Token: 0x04000998 RID: 2456
	public const string InvalidOperation_WrongOidInAsnCollection = "AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.";

	// Token: 0x04000999 RID: 2457
	public const string PlatformNotSupported_CryptographyPkcs = "System.Security.Cryptography.Pkcs is only supported on Windows platforms.";

	// Token: 0x0400099A RID: 2458
	public const string Cryptography_Invalid_IA5String = "The string contains a character not in the 7 bit ASCII character set.";

	// Token: 0x0400099B RID: 2459
	public const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";

	// Token: 0x0400099C RID: 2460
	public const string Cryptography_WriteEncodedValue_OneValueAtATime = "The input to WriteEncodedValue must represent a single encoded value with no trailing data.";

	// Token: 0x0400099D RID: 2461
	public const string Arg_CryptographyException = "Error occurred during a cryptographic operation.";

	// Token: 0x0400099E RID: 2462
	public const string Cryptography_CryptoStream_FlushFinalBlockTwice = "FlushFinalBlock() method was called twice on a CryptoStream. It can only be called once.";

	// Token: 0x0400099F RID: 2463
	public const string Cryptography_DefaultAlgorithm_NotSupported = "This platform does not allow the automatic selection of an algorithm.";

	// Token: 0x040009A0 RID: 2464
	public const string Cryptography_HashNotYetFinalized = "Hash must be finalized before the hash value is retrieved.";

	// Token: 0x040009A1 RID: 2465
	public const string Cryptography_InvalidFeedbackSize = "Specified feedback size is not valid for this algorithm.";

	// Token: 0x040009A2 RID: 2466
	public const string Cryptography_InvalidBlockSize = "Specified block size is not valid for this algorithm.";

	// Token: 0x040009A3 RID: 2467
	public const string Cryptography_InvalidCipherMode = "Specified cipher mode is not valid for this algorithm.";

	// Token: 0x040009A4 RID: 2468
	public const string Cryptography_InvalidIVSize = "Specified initialization vector (IV) does not match the block size for this algorithm.";

	// Token: 0x040009A5 RID: 2469
	public const string Cryptography_InvalidKeySize = "Specified key is not a valid size for this algorithm.";

	// Token: 0x040009A6 RID: 2470
	public const string Cryptography_InvalidPaddingMode = "Specified padding mode is not valid for this algorithm.";

	// Token: 0x040009A7 RID: 2471
	public const string HashNameMultipleSetNotSupported = "Setting the hashname after it's already been set is not supported on this platform.";

	// Token: 0x040009A8 RID: 2472
	public const string CryptoConfigNotSupported = "Accessing a hash algorithm by manipulating the HashName property is not supported on this platform. Instead, you must instantiate one of the supplied subtypes (such as HMACSHA1.)";

	// Token: 0x040009A9 RID: 2473
	public const string InvalidOperation_IncorrectImplementation = "The algorithm's implementation is incorrect.";

	// Token: 0x040009AA RID: 2474
	public const string Cryptography_DpApi_ProfileMayNotBeLoaded = "The data protection operation was unsuccessful. This may have been caused by not having the user profile loaded for the current thread's user context, which may be the case when the thread is impersonating.";

	// Token: 0x040009AB RID: 2475
	public const string PlatformNotSupported_CryptographyProtectedData = "Windows Data Protection API (DPAPI) is not supported on this platform.";

	// Token: 0x040009AC RID: 2476
	public const string Cryptography_Partial_Chain = "A certificate chain could not be built to a trusted root authority.";

	// Token: 0x040009AD RID: 2477
	public const string Cryptography_Xml_BadWrappedKeySize = "Bad wrapped key size.";

	// Token: 0x040009AE RID: 2478
	public const string Cryptography_Xml_CipherValueElementRequired = "A Cipher Data element should have either a CipherValue or a CipherReference element.";

	// Token: 0x040009AF RID: 2479
	public const string Cryptography_Xml_CreateHashAlgorithmFailed = "Could not create hash algorithm object.";

	// Token: 0x040009B0 RID: 2480
	public const string Cryptography_Xml_CreateTransformFailed = "Could not create the XML transformation identified by the URI {0}.";

	// Token: 0x040009B1 RID: 2481
	public const string Cryptography_Xml_CreatedKeyFailed = "Failed to create signing key.";

	// Token: 0x040009B2 RID: 2482
	public const string Cryptography_Xml_DigestMethodRequired = "A DigestMethod must be specified on a Reference prior to generating XML.";

	// Token: 0x040009B3 RID: 2483
	public const string Cryptography_Xml_DigestValueRequired = "A Reference must contain a DigestValue.";

	// Token: 0x040009B4 RID: 2484
	public const string Cryptography_Xml_EnvelopedSignatureRequiresContext = "An XmlDocument context is required for enveloped transforms.";

	// Token: 0x040009B5 RID: 2485
	public const string Cryptography_Xml_InvalidElement = "Malformed element {0}.";

	// Token: 0x040009B6 RID: 2486
	public const string Cryptography_Xml_InvalidEncryptionProperty = "Malformed encryption property element.";

	// Token: 0x040009B7 RID: 2487
	public const string Cryptography_Xml_InvalidKeySize = "The key size should be a non negative integer.";

	// Token: 0x040009B8 RID: 2488
	public const string Cryptography_Xml_InvalidReference = "Malformed reference element.";

	// Token: 0x040009B9 RID: 2489
	public const string Cryptography_Xml_InvalidSignatureLength = "The length of the signature with a MAC should be less than the hash output length.";

	// Token: 0x040009BA RID: 2490
	public const string Cryptography_Xml_InvalidSignatureLength2 = "The length in bits of the signature with a MAC should be a multiple of 8.";

	// Token: 0x040009BB RID: 2491
	public const string Cryptography_Xml_InvalidX509IssuerSerialNumber = "X509 issuer serial number is invalid.";

	// Token: 0x040009BC RID: 2492
	public const string Cryptography_Xml_KeyInfoRequired = "A KeyInfo element is required to check the signature.";

	// Token: 0x040009BD RID: 2493
	public const string Cryptography_Xml_KW_BadKeySize = "The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.";

	// Token: 0x040009BE RID: 2494
	public const string Cryptography_Xml_LoadKeyFailed = "Signing key is not loaded.";

	// Token: 0x040009BF RID: 2495
	public const string Cryptography_Xml_MissingAlgorithm = "Symmetric algorithm is not specified.";

	// Token: 0x040009C0 RID: 2496
	public const string Cryptography_Xml_MissingCipherData = "Cipher data is not specified.";

	// Token: 0x040009C1 RID: 2497
	public const string Cryptography_Xml_MissingDecryptionKey = "Unable to retrieve the decryption key.";

	// Token: 0x040009C2 RID: 2498
	public const string Cryptography_Xml_MissingEncryptionKey = "Unable to retrieve the encryption key.";

	// Token: 0x040009C3 RID: 2499
	public const string Cryptography_Xml_NotSupportedCryptographicTransform = "The specified cryptographic transform is not supported.";

	// Token: 0x040009C4 RID: 2500
	public const string Cryptography_Xml_ReferenceElementRequired = "At least one Reference element is required.";

	// Token: 0x040009C5 RID: 2501
	public const string Cryptography_Xml_ReferenceTypeRequired = "The Reference type must be set in an EncryptedReference object.";

	// Token: 0x040009C6 RID: 2502
	public const string Cryptography_Xml_SelfReferenceRequiresContext = "An XmlDocument context is required to resolve the Reference Uri {0}.";

	// Token: 0x040009C7 RID: 2503
	public const string Cryptography_Xml_SignatureDescriptionNotCreated = "SignatureDescription could not be created for the signature algorithm supplied.";

	// Token: 0x040009C8 RID: 2504
	public const string Cryptography_Xml_SignatureMethodKeyMismatch = "The key does not fit the SignatureMethod.";

	// Token: 0x040009C9 RID: 2505
	public const string Cryptography_Xml_SignatureMethodRequired = "A signature method is required.";

	// Token: 0x040009CA RID: 2506
	public const string Cryptography_Xml_SignatureValueRequired = "Signature requires a SignatureValue.";

	// Token: 0x040009CB RID: 2507
	public const string Cryptography_Xml_SignedInfoRequired = "Signature requires a SignedInfo.";

	// Token: 0x040009CC RID: 2508
	public const string Cryptography_Xml_TransformIncorrectInputType = "The input type was invalid for this transform.";

	// Token: 0x040009CD RID: 2509
	public const string Cryptography_Xml_IncorrectObjectType = "Type of input object is invalid.";

	// Token: 0x040009CE RID: 2510
	public const string Cryptography_Xml_UnknownTransform = "Unknown transform has been encountered.";

	// Token: 0x040009CF RID: 2511
	public const string Cryptography_Xml_UriNotResolved = "Unable to resolve Uri {0}.";

	// Token: 0x040009D0 RID: 2512
	public const string Cryptography_Xml_UriNotSupported = " The specified Uri is not supported.";

	// Token: 0x040009D1 RID: 2513
	public const string Cryptography_Xml_UriRequired = "A Uri attribute is required for a CipherReference element.";

	// Token: 0x040009D2 RID: 2514
	public const string Cryptography_Xml_XrmlMissingContext = "Null Context property encountered.";

	// Token: 0x040009D3 RID: 2515
	public const string Cryptography_Xml_XrmlMissingIRelDecryptor = "IRelDecryptor is required.";

	// Token: 0x040009D4 RID: 2516
	public const string Cryptography_Xml_XrmlMissingIssuer = "Issuer node is required.";

	// Token: 0x040009D5 RID: 2517
	public const string Cryptography_Xml_XrmlMissingLicence = "License node is required.";

	// Token: 0x040009D6 RID: 2518
	public const string Cryptography_Xml_XrmlUnableToDecryptGrant = "Unable to decrypt grant content.";

	// Token: 0x040009D7 RID: 2519
	public const string Log_ActualHashValue = "Actual hash value: {0}";

	// Token: 0x040009D8 RID: 2520
	public const string Log_BeginCanonicalization = "Beginning canonicalization using \"{0}\" ({1}).";

	// Token: 0x040009D9 RID: 2521
	public const string Log_BeginSignatureComputation = "Beginning signature computation.";

	// Token: 0x040009DA RID: 2522
	public const string Log_BeginSignatureVerification = "Beginning signature verification.";

	// Token: 0x040009DB RID: 2523
	public const string Log_BuildX509Chain = "Building and verifying the X509 chain for certificate {0}.";

	// Token: 0x040009DC RID: 2524
	public const string Log_CanonicalizationSettings = "Canonicalization transform is using resolver {0} and base URI \"{1}\".";

	// Token: 0x040009DD RID: 2525
	public const string Log_CanonicalizedOutput = "Output of canonicalization transform: {0}";

	// Token: 0x040009DE RID: 2526
	public const string Log_CertificateChain = "Certificate chain:";

	// Token: 0x040009DF RID: 2527
	public const string Log_CheckSignatureFormat = "Checking signature format using format validator \"[{0}] {1}.{2}\".";

	// Token: 0x040009E0 RID: 2528
	public const string Log_CheckSignedInfo = "Checking signature on SignedInfo with id \"{0}\".";

	// Token: 0x040009E1 RID: 2529
	public const string Log_FormatValidationSuccessful = "Signature format validation was successful.";

	// Token: 0x040009E2 RID: 2530
	public const string Log_FormatValidationNotSuccessful = "Signature format validation failed.";

	// Token: 0x040009E3 RID: 2531
	public const string Log_KeyUsages = "Found key usages \"{0}\" in extension {1} on certificate {2}.";

	// Token: 0x040009E4 RID: 2532
	public const string Log_NoNamespacesPropagated = "No namespaces are being propagated.";

	// Token: 0x040009E5 RID: 2533
	public const string Log_PropagatingNamespace = "Propagating namespace {0}=\"{1}\".";

	// Token: 0x040009E6 RID: 2534
	public const string Log_RawSignatureValue = "Raw signature: {0}";

	// Token: 0x040009E7 RID: 2535
	public const string Log_ReferenceHash = "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.";

	// Token: 0x040009E8 RID: 2536
	public const string Log_RevocationMode = "Revocation mode for chain building: {0}.";

	// Token: 0x040009E9 RID: 2537
	public const string Log_RevocationFlag = "Revocation flag for chain building: {0}.";

	// Token: 0x040009EA RID: 2538
	public const string Log_SigningAsymmetric = "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.";

	// Token: 0x040009EB RID: 2539
	public const string Log_SigningHmac = "Calculating signature using keyed hash algorithm {0}.";

	// Token: 0x040009EC RID: 2540
	public const string Log_SigningReference = "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).";

	// Token: 0x040009ED RID: 2541
	public const string Log_TransformedReferenceContents = "Transformed reference contents: {0}";

	// Token: 0x040009EE RID: 2542
	public const string Log_UnsafeCanonicalizationMethod = "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.";

	// Token: 0x040009EF RID: 2543
	public const string Log_UrlTimeout = "URL retrieval timeout for chain building: {0}.";

	// Token: 0x040009F0 RID: 2544
	public const string Log_VerificationFailed = "Verification failed checking {0}.";

	// Token: 0x040009F1 RID: 2545
	public const string Log_VerificationFailed_References = "references";

	// Token: 0x040009F2 RID: 2546
	public const string Log_VerificationFailed_SignedInfo = "SignedInfo";

	// Token: 0x040009F3 RID: 2547
	public const string Log_VerificationFailed_X509Chain = "X509 chain verification";

	// Token: 0x040009F4 RID: 2548
	public const string Log_VerificationFailed_X509KeyUsage = "X509 key usage verification";

	// Token: 0x040009F5 RID: 2549
	public const string Log_VerificationFlag = "Verification flags for chain building: {0}.";

	// Token: 0x040009F6 RID: 2550
	public const string Log_VerificationTime = "Verification time for chain building: {0}.";

	// Token: 0x040009F7 RID: 2551
	public const string Log_VerificationWithKeySuccessful = "Verification with key {0} was successful.";

	// Token: 0x040009F8 RID: 2552
	public const string Log_VerificationWithKeyNotSuccessful = "Verification with key {0} was not successful.";

	// Token: 0x040009F9 RID: 2553
	public const string Log_VerifyReference = "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".";

	// Token: 0x040009FA RID: 2554
	public const string Log_VerifySignedInfoAsymmetric = "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.";

	// Token: 0x040009FB RID: 2555
	public const string Log_VerifySignedInfoHmac = "Verifying SignedInfo using keyed hash algorithm {0}.";

	// Token: 0x040009FC RID: 2556
	public const string Log_X509ChainError = "Error building X509 chain: {0}: {1}.";

	// Token: 0x040009FD RID: 2557
	public const string Log_XmlContext = "Using context: {0}";

	// Token: 0x040009FE RID: 2558
	public const string Log_SignedXmlRecursionLimit = "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).";

	// Token: 0x040009FF RID: 2559
	public const string Log_UnsafeTransformMethod = "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.";

	// Token: 0x04000A00 RID: 2560
	public const string Arg_InvalidType = "Invalid type.";

	// Token: 0x04000A01 RID: 2561
	public const string Chain_NoPolicyMatch = "The certificate has invalid policy.";

	// Token: 0x04000A02 RID: 2562
	public const string Cryptography_BadHashSize_ForAlgorithm = "The provided value of {0} bytes does not match the expected size of {1} bytes for the algorithm ({2}).";

	// Token: 0x04000A03 RID: 2563
	public const string Cryptography_Cert_AlreadyHasPrivateKey = "The certificate already has an associated private key.";

	// Token: 0x04000A04 RID: 2564
	public const string Cryptography_CertReq_AlgorithmMustMatch = "The issuer certificate public key algorithm ({0}) does not match the value for this certificate request ({1}), use the X509SignatureGenerator overload.";

	// Token: 0x04000A05 RID: 2565
	public const string Cryptography_CertReq_BasicConstraintsRequired = "The issuer certificate does not have a Basic Constraints extension.";

	// Token: 0x04000A06 RID: 2566
	public const string Cryptography_CertReq_DatesReversed = "The provided notBefore value is later than the notAfter value.";

	// Token: 0x04000A07 RID: 2567
	public const string Cryptography_CertReq_DateTooOld = "The value predates 1950 and has no defined encoding.";

	// Token: 0x04000A08 RID: 2568
	public const string Cryptography_CertReq_DuplicateExtension = "An X509Extension with OID '{0}' has already been specified.";

	// Token: 0x04000A09 RID: 2569
	public const string Cryptography_CertReq_IssuerBasicConstraintsInvalid = "The issuer certificate does not have an appropriate value for the Basic Constraints extension.";

	// Token: 0x04000A0A RID: 2570
	public const string Cryptography_CertReq_IssuerKeyUsageInvalid = "The issuer certificate's Key Usage extension is present but does not contain the KeyCertSign flag.";

	// Token: 0x04000A0B RID: 2571
	public const string Cryptography_CertReq_IssuerRequiresPrivateKey = "The provided issuer certificate does not have an associated private key.";

	// Token: 0x04000A0C RID: 2572
	public const string Cryptography_CertReq_NotAfterNotNested = "The requested notAfter value ({0}) is later than issuerCertificate.NotAfter ({1}).";

	// Token: 0x04000A0D RID: 2573
	public const string Cryptography_CertReq_NotBeforeNotNested = "The requested notBefore value ({0}) is earlier than issuerCertificate.NotBefore ({1}).";

	// Token: 0x04000A0E RID: 2574
	public const string Cryptography_CertReq_NoKeyProvided = "This method cannot be used since no signing key was provided via a constructor, use an overload accepting an X509SignatureGenerator instead.";

	// Token: 0x04000A0F RID: 2575
	public const string Cryptography_CertReq_RSAPaddingRequired = "The issuer certificate uses an RSA key but no RSASignaturePadding was provided to a constructor. If one cannot be provided, use the X509SignatureGenerator overload.";

	// Token: 0x04000A10 RID: 2576
	public const string Cryptography_CSP_NoPrivateKey = "Object contains only the public half of a key pair. A private key must also be provided.";

	// Token: 0x04000A11 RID: 2577
	public const string Cryptography_CurveNotSupported = "The specified curve '{0}' or its parameters are not valid for this platform.";

	// Token: 0x04000A12 RID: 2578
	public const string Cryptography_ECC_NamedCurvesOnly = "Only named curves are supported on this platform.";

	// Token: 0x04000A13 RID: 2579
	public const string Cryptography_Encryption_MessageTooLong = "The message exceeds the maximum allowable length for the chosen options ({0}).";

	// Token: 0x04000A14 RID: 2580
	public const string Cryptography_HashAlgorithmNameNullOrEmpty = "The hash algorithm name cannot be null or empty.";

	// Token: 0x04000A15 RID: 2581
	public const string Cryptography_InvalidOID = "Object identifier (OID) is unknown.";

	// Token: 0x04000A16 RID: 2582
	public const string Cryptography_InvalidPublicKey_Object = "The provided PublicKey object is invalid, valid Oid and EncodedKeyValue property values are required.";

	// Token: 0x04000A17 RID: 2583
	public const string Cryptography_InvalidRsaParameters = "The specified RSA parameters are not valid; both Exponent and Modulus are required fields.";

	// Token: 0x04000A18 RID: 2584
	public const string Cryptography_KeyTooSmall = "The key is too small for the requested operation.";

	// Token: 0x04000A19 RID: 2585
	public const string Cryptography_OAEP_Decryption_Failed = "Error occurred while decoding OAEP padding.";

	// Token: 0x04000A1A RID: 2586
	public const string Cryptography_OpenInvalidHandle = "Cannot open an invalid handle.";

	// Token: 0x04000A1B RID: 2587
	public const string Cryptography_PrivateKey_DoesNotMatch = "The provided key does not match the public key for this certificate.";

	// Token: 0x04000A1C RID: 2588
	public const string Cryptography_PrivateKey_WrongAlgorithm = "The provided key does not match the public key algorithm for this certificate.";

	// Token: 0x04000A1D RID: 2589
	public const string Cryptography_RSA_DecryptWrongSize = "The length of the data to decrypt is not valid for the size of this key.";

	// Token: 0x04000A1E RID: 2590
	public const string Cryptography_SignHash_WrongSize = "The provided hash value is not the expected size for the specified hash algorithm.";

	// Token: 0x04000A1F RID: 2591
	public const string Cryptography_Unix_X509_DisallowedStoreNotEmpty = "The Disallowed store is not supported on this platform, but already has data. All files under '{0}' must be removed.";

	// Token: 0x04000A20 RID: 2592
	public const string Cryptography_Unix_X509_MachineStoresReadOnly = "Unix LocalMachine X509Stores are read-only for all users.";

	// Token: 0x04000A21 RID: 2593
	public const string Cryptography_Unix_X509_MachineStoresRootOnly = "Unix LocalMachine X509Store is limited to the Root and CertificateAuthority stores.";

	// Token: 0x04000A22 RID: 2594
	public const string Cryptography_Unix_X509_NoDisallowedStore = "The Disallowed store is not supported on this platform.";

	// Token: 0x04000A23 RID: 2595
	public const string Cryptography_Unix_X509_PropertyNotSettable = "The {0} value cannot be set on Unix.";

	// Token: 0x04000A24 RID: 2596
	public const string Cryptography_UnknownKeyAlgorithm = "'{0}' is not a known key algorithm.";

	// Token: 0x04000A25 RID: 2597
	public const string Cryptography_Unix_X509_SerializedExport = "X509ContentType.SerializedCert and X509ContentType.SerializedStore are not supported on Unix.";

	// Token: 0x04000A26 RID: 2598
	public const string Cryptography_Unmapped_System_Typed_Error = "The system cryptographic library returned error '{0}' of type '{1}'";

	// Token: 0x04000A27 RID: 2599
	public const string Cryptography_X509_InvalidFlagCombination = "The flags '{0}' may not be specified together.";

	// Token: 0x04000A28 RID: 2600
	public const string Cryptography_X509_PKCS7_NoSigner = "Cannot find the original signer.";

	// Token: 0x04000A29 RID: 2601
	public const string Cryptography_X509_StoreAddFailure = "The X509 certificate could not be added to the store.";

	// Token: 0x04000A2A RID: 2602
	public const string Cryptography_X509_StoreNoFileAvailable = "The X509 certificate could not be added to the store because all candidate file names were in use.";

	// Token: 0x04000A2B RID: 2603
	public const string Cryptography_X509_StoreNotFound = "The specified X509 certificate store does not exist.";

	// Token: 0x04000A2C RID: 2604
	public const string Cryptography_X509_StoreReadOnly = "The X509 certificate store is read-only.";

	// Token: 0x04000A2D RID: 2605
	public const string Cryptography_X509_StoreCannotCreate = "The platform does not have a definition for an X509 certificate store named '{0}' with a StoreLocation of '{1}', and does not support creating it.";

	// Token: 0x04000A2E RID: 2606
	public const string NotSupported_ECDsa_Csp = "CryptoApi ECDsa keys are not supported.";

	// Token: 0x04000A2F RID: 2607
	public const string NotSupported_Export_MultiplePrivateCerts = "Only one certificate with a private key can be exported in a single PFX.";

	// Token: 0x04000A30 RID: 2608
	public const string NotSupported_LegacyBasicConstraints = "The X509 Basic Constraints extension with OID 2.5.29.10 is not supported.";

	// Token: 0x04000A31 RID: 2609
	public const string NotSupported_ImmutableX509Certificate = "X509Certificate is immutable on this platform. Use the equivalent constructor instead.";

	// Token: 0x04000A32 RID: 2610
	public const string Security_AccessDenied = "Access is denied.";

	// Token: 0x04000A33 RID: 2611
	public const string Cryptography_FileStatusError = "Unable to get file status.";

	// Token: 0x04000A34 RID: 2612
	public const string Cryptography_InvalidDirectoryPermissions = "Invalid directory permissions. The directory '{0}' must be readable, writable and executable by the owner.";

	// Token: 0x04000A35 RID: 2613
	public const string Cryptography_OwnerNotCurrentUser = "The owner of '{0}' is not the current user.";

	// Token: 0x04000A36 RID: 2614
	public const string Cryptography_InvalidFilePermissions = "Invalid file permissions. The file '{0}' must readable and writable by the current owner and by no one else, and the permissions could not be changed to meet that criteria.";

	// Token: 0x04000A37 RID: 2615
	public const string Cryptography_Invalid_X500Name = "The string contains an invalid X500 name attribute key, oid, value or delimiter.";

	// Token: 0x04000A38 RID: 2616
	public const string Cryptography_X509_NoEphemeralPfx = "This platform does not support loading with EphemeralKeySet. Remove the flag to allow keys to be temporarily created on disk.";

	// Token: 0x04000A39 RID: 2617
	public const string Cryptography_X509Store_WouldModifyUserTrust = "Removing the requested certificate would modify user trust settings, and has been denied.";

	// Token: 0x04000A3A RID: 2618
	public const string Cryptography_X509Store_WouldModifyAdminTrust = "Removing the requested certificate would modify admin trust settings, and has been denied.";

	// Token: 0x04000A3B RID: 2619
	public const string Cryptography_DSA_KeyGenNotSupported = "DSA keys can be imported, but new key generation is not supported on this platform.";

	// Token: 0x04000A3C RID: 2620
	public const string Cryptography_InvalidDsaParameters_MissingFields = "The specified DSA parameters are not valid; P, Q, G and Y are all required.";

	// Token: 0x04000A3D RID: 2621
	public const string Cryptography_InvalidDsaParameters_MismatchedPGY = "The specified DSA parameters are not valid; P, G and Y must be the same length (the key size).";

	// Token: 0x04000A3E RID: 2622
	public const string Cryptography_InvalidDsaParameters_MismatchedQX = "The specified DSA parameters are not valid; Q and X (if present) must be the same length.";

	// Token: 0x04000A3F RID: 2623
	public const string Cryptography_InvalidDsaParameters_MismatchedPJ = "The specified DSA parameters are not valid; J (if present) must be shorter than P.";

	// Token: 0x04000A40 RID: 2624
	public const string Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey = "The specified DSA parameters are not valid; Seed, if present, must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x04000A41 RID: 2625
	public const string Cryptography_InvalidDsaParameters_QRestriction_ShortKey = "The specified DSA parameters are not valid; Q must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x04000A42 RID: 2626
	public const string Cryptography_InvalidDsaParameters_QRestriction_LargeKey = "The specified DSA parameters are not valid; Q's length must be one of 20, 32 or 64 bytes.";

	// Token: 0x04000A43 RID: 2627
	public const string InvalidEmptyArgument = "Argument {0} cannot be zero-length.";

	// Token: 0x04000A44 RID: 2628
	public const string PlatformNotSupported_CompileToAssembly = "This platform does not support writing compiled regular expressions to an assembly.";

	// Token: 0x04000A45 RID: 2629
	public const string Parallel_Invoke_ActionNull = "One of the actions was null.";

	// Token: 0x04000A46 RID: 2630
	public const string Parallel_ForEach_OrderedPartitionerKeysNotNormalized = "This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.";

	// Token: 0x04000A47 RID: 2631
	public const string Parallel_ForEach_PartitionerNotDynamic = "The Partitioner used here must support dynamic partitioning.";

	// Token: 0x04000A48 RID: 2632
	public const string Parallel_ForEach_PartitionerReturnedNull = "The Partitioner used here returned a null partitioner source.";

	// Token: 0x04000A49 RID: 2633
	public const string Parallel_ForEach_NullEnumerator = "The Partitioner source returned a null enumerator.";

	// Token: 0x04000A4A RID: 2634
	public const string ParallelState_Break_InvalidOperationException_BreakAfterStop = "Break was called after Stop was called.";

	// Token: 0x04000A4B RID: 2635
	public const string ParallelState_Stop_InvalidOperationException_StopAfterBreak = "Stop was called after Break was called.";

	// Token: 0x04000A4C RID: 2636
	public const string ParallelState_NotSupportedException_UnsupportedMethod = "This method is not supported.";

	// Token: 0x04000A4D RID: 2637
	public const string ArgumentOutOfRange_InvalidThreshold = "The specified threshold for creating dictionary is out of range.";

	// Token: 0x04000A4E RID: 2638
	public const string Argument_ItemNotExist = "The specified item does not exist in this KeyedCollection.";

	// Token: 0x04000A4F RID: 2639
	public const string AmbiguousImplementationException_NullMessage = "Ambiguous implementation found.";

	// Token: 0x04000A50 RID: 2640
	public const string Arg_AccessException = "Cannot access member.";

	// Token: 0x04000A51 RID: 2641
	public const string Arg_AccessViolationException = "Attempted to read or write protected memory. This is often an indication that other memory is corrupt.";

	// Token: 0x04000A52 RID: 2642
	public const string Arg_ApplicationException = "Error in the application.";

	// Token: 0x04000A53 RID: 2643
	public const string Arg_ArgumentException = "Value does not fall within the expected range.";

	// Token: 0x04000A54 RID: 2644
	public const string Arg_ArithmeticException = "Overflow or underflow in the arithmetic operation.";

	// Token: 0x04000A55 RID: 2645
	public const string Arg_ArrayTypeMismatchException = "Attempted to access an element as a type incompatible with the array.";

	// Token: 0x04000A56 RID: 2646
	public const string Arg_ArrayZeroError = "Array must not be of length zero.";

	// Token: 0x04000A57 RID: 2647
	public const string Arg_BadImageFormatException = "Format of the executable (.exe) or library (.dll) is invalid.";

	// Token: 0x04000A58 RID: 2648
	public const string Arg_BogusIComparer = "Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.";

	// Token: 0x04000A59 RID: 2649
	public const string Arg_CannotBeNaN = "TimeSpan does not accept floating point Not-a-Number values.";

	// Token: 0x04000A5A RID: 2650
	public const string Arg_CannotHaveNegativeValue = "String cannot contain a minus sign if the base is not 10.";

	// Token: 0x04000A5B RID: 2651
	public const string Arg_CopyNonBlittableArray = "Arrays must contain only blittable data in order to be copied to unmanaged memory.";

	// Token: 0x04000A5C RID: 2652
	public const string Arg_CopyOutOfRange = "Requested range extends past the end of the array.";

	// Token: 0x04000A5D RID: 2653
	public const string Arg_DataMisalignedException = "A datatype misalignment was detected in a load or store instruction.";

	// Token: 0x04000A5E RID: 2654
	public const string Arg_DateTimeRange = "Combination of arguments to the DateTime constructor is out of the legal range.";

	// Token: 0x04000A5F RID: 2655
	public const string Arg_DirectoryNotFoundException = "Attempted to access a path that is not on the disk.";

	// Token: 0x04000A60 RID: 2656
	public const string Arg_DecBitCtor = "Decimal byte array constructor requires an array of length four containing valid decimal bytes.";

	// Token: 0x04000A61 RID: 2657
	public const string Arg_DivideByZero = "Attempted to divide by zero.";

	// Token: 0x04000A62 RID: 2658
	public const string Arg_DlgtNullInst = "Delegate to an instance method cannot have null 'this'.";

	// Token: 0x04000A63 RID: 2659
	public const string Arg_DlgtTypeMis = "Delegates must be of the same type.";

	// Token: 0x04000A64 RID: 2660
	public const string Arg_DuplicateWaitObjectException = "Duplicate objects in argument.";

	// Token: 0x04000A65 RID: 2661
	public const string Arg_EHClauseNotFilter = "This ExceptionHandlingClause is not a filter.";

	// Token: 0x04000A66 RID: 2662
	public const string Arg_EnumAndObjectMustBeSameType = "Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.";

	// Token: 0x04000A67 RID: 2663
	public const string Arg_EntryPointNotFoundException = "Entry point was not found.";

	// Token: 0x04000A68 RID: 2664
	public const string Arg_EntryPointNotFoundExceptionParameterized = "Unable to find an entry point named '{0}' in DLL '{1}'.";

	// Token: 0x04000A69 RID: 2665
	public const string Arg_ExecutionEngineException = "Internal error in the runtime.";

	// Token: 0x04000A6A RID: 2666
	public const string Arg_ExternalException = "External component has thrown an exception.";

	// Token: 0x04000A6B RID: 2667
	public const string Arg_FieldAccessException = "Attempted to access a field that is not accessible by the caller.";

	// Token: 0x04000A6C RID: 2668
	public const string Arg_FormatException = "One of the identified items was in an invalid format.";

	// Token: 0x04000A6D RID: 2669
	public const string Arg_GuidArrayCtor = "Byte array for GUID must be exactly {0} bytes long.";

	// Token: 0x04000A6E RID: 2670
	public const string Arg_HexStyleNotSupported = "The number style AllowHexSpecifier is not supported on floating point data types.";

	// Token: 0x04000A6F RID: 2671
	public const string Arg_IndexOutOfRangeException = "Index was outside the bounds of the array.";

	// Token: 0x04000A70 RID: 2672
	public const string Arg_InsufficientExecutionStackException = "Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.";

	// Token: 0x04000A71 RID: 2673
	public const string Arg_InvalidBase = "Invalid Base.";

	// Token: 0x04000A72 RID: 2674
	public const string Arg_InvalidCastException = "Specified cast is not valid.";

	// Token: 0x04000A73 RID: 2675
	public const string Arg_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	// Token: 0x04000A74 RID: 2676
	public const string Arg_InvalidOperationException = "Operation is not valid due to the current state of the object.";

	// Token: 0x04000A75 RID: 2677
	public const string Arg_OleAutDateInvalid = " Not a legal OleAut date.";

	// Token: 0x04000A76 RID: 2678
	public const string Arg_OleAutDateScale = "OleAut date did not convert to a DateTime correctly.";

	// Token: 0x04000A77 RID: 2679
	public const string Arg_InvalidRuntimeTypeHandle = "Invalid RuntimeTypeHandle.";

	// Token: 0x04000A78 RID: 2680
	public const string Arg_IOException = "I/O error occurred.";

	// Token: 0x04000A79 RID: 2681
	public const string Arg_KeyNotFound = "The given key was not present in the dictionary.";

	// Token: 0x04000A7A RID: 2682
	public const string Arg_LongerThanSrcString = "Source string was not long enough. Check sourceIndex and count.";

	// Token: 0x04000A7B RID: 2683
	public const string Arg_LowerBoundsMustMatch = "The arrays' lower bounds must be identical.";

	// Token: 0x04000A7C RID: 2684
	public const string Arg_MissingFieldException = "Attempted to access a non-existing field.";

	// Token: 0x04000A7D RID: 2685
	public const string Arg_MethodAccessException = "Attempt to access the method failed.";

	// Token: 0x04000A7E RID: 2686
	public const string Arg_MissingMemberException = "Attempted to access a missing member.";

	// Token: 0x04000A7F RID: 2687
	public const string Arg_MissingMethodException = "Attempted to access a missing method.";

	// Token: 0x04000A80 RID: 2688
	public const string Arg_MulticastNotSupportedException = "Attempted to add multiple callbacks to a delegate that does not support multicast.";

	// Token: 0x04000A81 RID: 2689
	public const string Arg_MustBeBoolean = "Object must be of type Boolean.";

	// Token: 0x04000A82 RID: 2690
	public const string Arg_MustBeByte = "Object must be of type Byte.";

	// Token: 0x04000A83 RID: 2691
	public const string Arg_MustBeChar = "Object must be of type Char.";

	// Token: 0x04000A84 RID: 2692
	public const string Arg_MustBeDateTime = "Object must be of type DateTime.";

	// Token: 0x04000A85 RID: 2693
	public const string Arg_MustBeDateTimeOffset = "Object must be of type DateTimeOffset.";

	// Token: 0x04000A86 RID: 2694
	public const string Arg_MustBeDecimal = "Object must be of type Decimal.";

	// Token: 0x04000A87 RID: 2695
	public const string Arg_MustBeDouble = "Object must be of type Double.";

	// Token: 0x04000A88 RID: 2696
	public const string Arg_MustBeEnum = "Type provided must be an Enum.";

	// Token: 0x04000A89 RID: 2697
	public const string Arg_MustBeGuid = "Object must be of type GUID.";

	// Token: 0x04000A8A RID: 2698
	public const string Arg_MustBeInt16 = "Object must be of type Int16.";

	// Token: 0x04000A8B RID: 2699
	public const string Arg_MustBeInt32 = "Object must be of type Int32.";

	// Token: 0x04000A8C RID: 2700
	public const string Arg_MustBeInt64 = "Object must be of type Int64.";

	// Token: 0x04000A8D RID: 2701
	public const string Arg_MustBePrimArray = "Object must be an array of primitives.";

	// Token: 0x04000A8E RID: 2702
	public const string Arg_MustBeSByte = "Object must be of type SByte.";

	// Token: 0x04000A8F RID: 2703
	public const string Arg_MustBeSingle = "Object must be of type Single.";

	// Token: 0x04000A90 RID: 2704
	public const string Arg_MustBeStatic = "Method must be a static method.";

	// Token: 0x04000A91 RID: 2705
	public const string Arg_MustBeString = "Object must be of type String.";

	// Token: 0x04000A92 RID: 2706
	public const string Arg_MustBeStringPtrNotAtom = "The pointer passed in as a String must not be in the bottom 64K of the process's address space.";

	// Token: 0x04000A93 RID: 2707
	public const string Arg_MustBeTimeSpan = "Object must be of type TimeSpan.";

	// Token: 0x04000A94 RID: 2708
	public const string Arg_MustBeUInt16 = "Object must be of type UInt16.";

	// Token: 0x04000A95 RID: 2709
	public const string Arg_MustBeUInt32 = "Object must be of type UInt32.";

	// Token: 0x04000A96 RID: 2710
	public const string Arg_MustBeUInt64 = "Object must be of type UInt64.";

	// Token: 0x04000A97 RID: 2711
	public const string Arg_MustBeVersion = "Object must be of type Version.";

	// Token: 0x04000A98 RID: 2712
	public const string Arg_NeedAtLeast1Rank = "Must provide at least one rank.";

	// Token: 0x04000A99 RID: 2713
	public const string Arg_Need2DArray = "Array was not a two-dimensional array.";

	// Token: 0x04000A9A RID: 2714
	public const string Arg_Need3DArray = "Array was not a three-dimensional array.";

	// Token: 0x04000A9B RID: 2715
	public const string Arg_NegativeArgCount = "Argument count must not be negative.";

	// Token: 0x04000A9C RID: 2716
	public const string Arg_NotFiniteNumberException = "Arg_NotFiniteNumberException = Number encountered was not a finite quantity.";

	// Token: 0x04000A9D RID: 2717
	public const string Arg_NotGenericParameter = "Method may only be called on a Type for which Type.IsGenericParameter is true.";

	// Token: 0x04000A9E RID: 2718
	public const string Arg_NotImplementedException = "The method or operation is not implemented.";

	// Token: 0x04000A9F RID: 2719
	public const string Arg_NotSupportedException = "Specified method is not supported.";

	// Token: 0x04000AA0 RID: 2720
	public const string Arg_NotSupportedNonZeroLowerBound = "Arrays with non-zero lower bounds are not supported.";

	// Token: 0x04000AA1 RID: 2721
	public const string Arg_NullReferenceException = "Object reference not set to an instance of an object.";

	// Token: 0x04000AA2 RID: 2722
	public const string Arg_ObjObjEx = "Object of type '{0}' cannot be converted to type '{1}'.";

	// Token: 0x04000AA3 RID: 2723
	public const string Arg_OverflowException = "Arithmetic operation resulted in an overflow.";

	// Token: 0x04000AA4 RID: 2724
	public const string Arg_OutOfMemoryException = "Insufficient memory to continue the execution of the program.";

	// Token: 0x04000AA5 RID: 2725
	public const string Arg_PlatformNotSupported = "Operation is not supported on this platform.";

	// Token: 0x04000AA6 RID: 2726
	public const string Arg_ParamName_Name = "Parameter name: {0}";

	// Token: 0x04000AA7 RID: 2727
	public const string Arg_PathEmpty = "The path is empty.";

	// Token: 0x04000AA8 RID: 2728
	public const string Arg_PathIllegalUNC_Path = "The UNC path '{0}' should be of the form \\\\\\\\server\\\\share.";

	// Token: 0x04000AA9 RID: 2729
	public const string Arg_RankException = "Attempted to operate on an array with the incorrect number of dimensions.";

	// Token: 0x04000AAA RID: 2730
	public const string Arg_RankIndices = "Indices length does not match the array rank.";

	// Token: 0x04000AAB RID: 2731
	public const string Arg_RanksAndBounds = "Number of lengths and lowerBounds must match.";

	// Token: 0x04000AAC RID: 2732
	public const string Arg_RegGetOverflowBug = "RegistryKey.GetValue does not allow a String that has a length greater than Int32.MaxValue.";

	// Token: 0x04000AAD RID: 2733
	public const string Arg_RegKeyNotFound = "The specified registry key does not exist.";

	// Token: 0x04000AAE RID: 2734
	public const string Arg_RegInvalidKeyName = "Registry key name must start with a valid base key name.";

	// Token: 0x04000AAF RID: 2735
	public const string Arg_StackOverflowException = "Operation caused a stack overflow.";

	// Token: 0x04000AB0 RID: 2736
	public const string Arg_SynchronizationLockException = "Object synchronization method was called from an unsynchronized block of code.";

	// Token: 0x04000AB1 RID: 2737
	public const string Arg_SystemException = "System error.";

	// Token: 0x04000AB2 RID: 2738
	public const string Arg_TargetInvocationException = "Exception has been thrown by the target of an invocation.";

	// Token: 0x04000AB3 RID: 2739
	public const string Arg_TargetParameterCountException = "Number of parameters specified does not match the expected number.";

	// Token: 0x04000AB4 RID: 2740
	public const string Arg_DefaultValueMissingException = "Missing parameter does not have a default value.";

	// Token: 0x04000AB5 RID: 2741
	public const string Arg_ThreadStartException = "Thread failed to start.";

	// Token: 0x04000AB6 RID: 2742
	public const string Arg_ThreadStateException = "Thread was in an invalid state for the operation being executed.";

	// Token: 0x04000AB7 RID: 2743
	public const string Arg_TimeoutException = "The operation has timed out.";

	// Token: 0x04000AB8 RID: 2744
	public const string Arg_TypeAccessException = "Attempt to access the type failed.";

	// Token: 0x04000AB9 RID: 2745
	public const string Arg_TypeLoadException = "Failure has occurred while loading a type.";

	// Token: 0x04000ABA RID: 2746
	public const string Arg_UnauthorizedAccessException = "Attempted to perform an unauthorized operation.";

	// Token: 0x04000ABB RID: 2747
	public const string Arg_VersionString = "Version string portion was too short or too long.";

	// Token: 0x04000ABC RID: 2748
	public const string Argument_AbsolutePathRequired = "Absolute path information is required.";

	// Token: 0x04000ABD RID: 2749
	public const string Argument_AdjustmentRulesNoNulls = "The AdjustmentRule array cannot contain null elements.";

	// Token: 0x04000ABE RID: 2750
	public const string Argument_AdjustmentRulesOutOfOrder = "The elements of the AdjustmentRule array must be in chronological order and must not overlap.";

	// Token: 0x04000ABF RID: 2751
	public const string Argument_CodepageNotSupported = "{0} is not a supported code page.";

	// Token: 0x04000AC0 RID: 2752
	public const string Argument_CompareOptionOrdinal = "CompareOption.Ordinal cannot be used with other options.";

	// Token: 0x04000AC1 RID: 2753
	public const string Argument_ConflictingDateTimeRoundtripStyles = "The DateTimeStyles value RoundtripKind cannot be used with the values AssumeLocal, AssumeUniversal or AdjustToUniversal.";

	// Token: 0x04000AC2 RID: 2754
	public const string Argument_ConflictingDateTimeStyles = "The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.";

	// Token: 0x04000AC3 RID: 2755
	public const string Argument_ConversionOverflow = "Conversion buffer overflow.";

	// Token: 0x04000AC4 RID: 2756
	public const string Argument_ConvertMismatch = "The conversion could not be completed because the supplied DateTime did not have the Kind property set correctly.  For example, when the Kind property is DateTimeKind.Local, the source time zone must be TimeZoneInfo.Local.";

	// Token: 0x04000AC5 RID: 2757
	public const string Argument_CultureInvalidIdentifier = "{0} is an invalid culture identifier.";

	// Token: 0x04000AC6 RID: 2758
	public const string Argument_CultureIetfNotSupported = "Culture IETF Name {0} is not a recognized IETF name.";

	// Token: 0x04000AC7 RID: 2759
	public const string Argument_CultureIsNeutral = "Culture ID {0} (0x{0:X4}) is a neutral culture; a region cannot be created from it.";

	// Token: 0x04000AC8 RID: 2760
	public const string Argument_CultureNotSupported = "Culture is not supported.";

	// Token: 0x04000AC9 RID: 2761
	public const string Argument_CustomCultureCannotBePassedByNumber = "Customized cultures cannot be passed by LCID, only by name.";

	// Token: 0x04000ACA RID: 2762
	public const string Argument_DateTimeBadBinaryData = "The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x04000ACB RID: 2763
	public const string Argument_DateTimeHasTicks = "The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds.";

	// Token: 0x04000ACC RID: 2764
	public const string Argument_DateTimeHasTimeOfDay = "The supplied DateTime includes a TimeOfDay setting.   This is not supported.";

	// Token: 0x04000ACD RID: 2765
	public const string Argument_DateTimeIsInvalid = "The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid.";

	// Token: 0x04000ACE RID: 2766
	public const string Argument_DateTimeIsNotAmbiguous = "The supplied DateTime is not in an ambiguous time range.";

	// Token: 0x04000ACF RID: 2767
	public const string Argument_DateTimeKindMustBeUnspecified = "The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified.";

	// Token: 0x04000AD0 RID: 2768
	public const string Argument_DateTimeKindMustBeUnspecifiedOrUtc = "The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.";

	// Token: 0x04000AD1 RID: 2769
	public const string Argument_DateTimeOffsetInvalidDateTimeStyles = "The DateTimeStyles value 'NoCurrentDateDefault' is not allowed when parsing DateTimeOffset.";

	// Token: 0x04000AD2 RID: 2770
	public const string Argument_DateTimeOffsetIsNotAmbiguous = "The supplied DateTimeOffset is not in an ambiguous time range.";

	// Token: 0x04000AD3 RID: 2771
	public const string Argument_EmptyDecString = "Decimal separator cannot be the empty string.";

	// Token: 0x04000AD4 RID: 2772
	public const string Argument_EmptyName = "Empty name is not legal.";

	// Token: 0x04000AD5 RID: 2773
	public const string Argument_EmptyWaithandleArray = "Waithandle array may not be empty.";

	// Token: 0x04000AD6 RID: 2774
	public const string Argument_EncoderFallbackNotEmpty = "Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.";

	// Token: 0x04000AD7 RID: 2775
	public const string Argument_EncodingConversionOverflowBytes = "The output byte buffer is too small to contain the encoded data, encoding '{0}' fallback '{1}'.";

	// Token: 0x04000AD8 RID: 2776
	public const string Argument_EncodingConversionOverflowChars = "The output char buffer is too small to contain the decoded characters, encoding '{0}' fallback '{1}'.";

	// Token: 0x04000AD9 RID: 2777
	public const string Argument_EncodingNotSupported = "'{0}' is not a supported encoding name. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.";

	// Token: 0x04000ADA RID: 2778
	public const string Argument_EnumTypeDoesNotMatch = "The argument type, '{0}', is not the same as the enum type '{1}'.";

	// Token: 0x04000ADB RID: 2779
	public const string Argument_FallbackBufferNotEmpty = "Cannot change fallback when buffer is not empty. Previous Convert() call left data in the fallback buffer.";

	// Token: 0x04000ADC RID: 2780
	public const string Argument_InvalidArgumentForComparison = "Type of argument is not compatible with the generic comparer.";

	// Token: 0x04000ADD RID: 2781
	public const string Argument_InvalidArrayLength = "Length of the array must be {0}.";

	// Token: 0x04000ADE RID: 2782
	public const string Argument_InvalidCalendar = "Not a valid calendar for the given culture.";

	// Token: 0x04000ADF RID: 2783
	public const string Argument_InvalidCharSequence = "Invalid Unicode code point found at index {0}.";

	// Token: 0x04000AE0 RID: 2784
	public const string Argument_InvalidCharSequenceNoIndex = "String contains invalid Unicode code points.";

	// Token: 0x04000AE1 RID: 2785
	public const string Argument_InvalidCodePageBytesIndex = "Unable to translate bytes {0} at index {1} from specified code page to Unicode.";

	// Token: 0x04000AE2 RID: 2786
	public const string Argument_InvalidCodePageConversionIndex = "Unable to translate Unicode character \\\\u{0:X4} at index {1} to specified code page.";

	// Token: 0x04000AE3 RID: 2787
	public const string Argument_InvalidCultureName = "Culture name '{0}' is not supported.";

	// Token: 0x04000AE4 RID: 2788
	public const string Argument_InvalidDateTimeKind = "Invalid DateTimeKind value.";

	// Token: 0x04000AE5 RID: 2789
	public const string Argument_InvalidDateTimeStyles = "An undefined DateTimeStyles value is being used.";

	// Token: 0x04000AE6 RID: 2790
	public const string Argument_InvalidDigitSubstitution = "The DigitSubstitution property must be of a valid member of the DigitShapes enumeration. Valid entries include Context, NativeNational or None.";

	// Token: 0x04000AE7 RID: 2791
	public const string Argument_InvalidEnumValue = "The value '{0}' is not valid for this usage of the type {1}.";

	// Token: 0x04000AE8 RID: 2792
	public const string Argument_InvalidGroupSize = "Every element in the value array should be between one and nine, except for the last element, which can be zero.";

	// Token: 0x04000AE9 RID: 2793
	public const string Argument_InvalidHighSurrogate = "Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.";

	// Token: 0x04000AEA RID: 2794
	public const string Argument_InvalidId = "The specified ID parameter '{0}' is not supported.";

	// Token: 0x04000AEB RID: 2795
	public const string Argument_InvalidLowSurrogate = "Found a low surrogate char without a preceding high surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.";

	// Token: 0x04000AEC RID: 2796
	public const string Argument_InvalidNativeDigitCount = "The NativeDigits array must contain exactly ten members.";

	// Token: 0x04000AED RID: 2797
	public const string Argument_InvalidNativeDigitValue = "Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit.";

	// Token: 0x04000AEE RID: 2798
	public const string Argument_InvalidNeutralRegionName = "The region name {0} should not correspond to neutral culture; a specific culture name is required.";

	// Token: 0x04000AEF RID: 2799
	public const string Argument_InvalidNormalizationForm = "Invalid normalization form.";

	// Token: 0x04000AF0 RID: 2800
	public const string Argument_InvalidREG_TZI_FORMAT = "The REG_TZI_FORMAT structure is corrupt.";

	// Token: 0x04000AF1 RID: 2801
	public const string Argument_InvalidResourceCultureName = "The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.";

	// Token: 0x04000AF2 RID: 2802
	public const string Argument_InvalidSerializedString = "The specified serialized string '{0}' is not supported.";

	// Token: 0x04000AF3 RID: 2803
	public const string Argument_InvalidTimeSpanStyles = "An undefined TimeSpanStyles value is being used.";

	// Token: 0x04000AF4 RID: 2804
	public const string Argument_MustBeFalse = "Argument must be initialized to false";

	// Token: 0x04000AF5 RID: 2805
	public const string Argument_MustBeRuntimeType = "Type must be a runtime Type object.";

	// Token: 0x04000AF6 RID: 2806
	public const string Argument_NoEra = "No Era was supplied.";

	// Token: 0x04000AF7 RID: 2807
	public const string Argument_NoRegionInvariantCulture = "There is no region associated with the Invariant Culture (Culture ID: 0x7F).";

	// Token: 0x04000AF8 RID: 2808
	public const string Argument_NotIsomorphic = "Object contains non-primitive or non-blittable data.";

	// Token: 0x04000AF9 RID: 2809
	public const string Argument_OffsetLocalMismatch = "The UTC Offset of the local dateTime parameter does not match the offset argument.";

	// Token: 0x04000AFA RID: 2810
	public const string Argument_OffsetPrecision = "Offset must be specified in whole minutes.";

	// Token: 0x04000AFB RID: 2811
	public const string Argument_OffsetOutOfRange = "Offset must be within plus or minus 14 hours.";

	// Token: 0x04000AFC RID: 2812
	public const string Argument_OffsetUtcMismatch = "The UTC Offset for Utc DateTime instances must be 0.";

	// Token: 0x04000AFD RID: 2813
	public const string Argument_OneOfCulturesNotSupported = "Culture name {0} or {1} is not supported.";

	// Token: 0x04000AFE RID: 2814
	public const string Argument_OnlyMscorlib = "Only mscorlib's assembly is valid.";

	// Token: 0x04000AFF RID: 2815
	public const string Argument_OutOfOrderDateTimes = "The DateStart property must come before the DateEnd property.";

	// Token: 0x04000B00 RID: 2816
	public const string ArgumentOutOfRange_HugeArrayNotSupported = "Arrays larger than 2GB are not supported.";

	// Token: 0x04000B01 RID: 2817
	public const string ArgumentOutOfRange_Length = "The specified length exceeds maximum capacity of SecureString.";

	// Token: 0x04000B02 RID: 2818
	public const string ArgumentOutOfRange_LengthTooLarge = "The specified length exceeds the maximum value of {0}.";

	// Token: 0x04000B03 RID: 2819
	public const string ArgumentOutOfRange_NeedValidId = "The ID parameter must be in the range {0} through {1}.";

	// Token: 0x04000B04 RID: 2820
	public const string Argument_InvalidTypeName = "The name of the type is invalid.";

	// Token: 0x04000B05 RID: 2821
	public const string Argument_PathFormatNotSupported_Path = "The format of the path '{0}' is not supported.";

	// Token: 0x04000B06 RID: 2822
	public const string Argument_RecursiveFallback = "Recursive fallback not allowed for character \\\\u{0:X4}.";

	// Token: 0x04000B07 RID: 2823
	public const string Argument_RecursiveFallbackBytes = "Recursive fallback not allowed for bytes {0}.";

	// Token: 0x04000B08 RID: 2824
	public const string Argument_ResultCalendarRange = "The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive.";

	// Token: 0x04000B09 RID: 2825
	public const string Argument_TimeSpanHasSeconds = "The TimeSpan parameter cannot be specified more precisely than whole minutes.";

	// Token: 0x04000B0A RID: 2826
	public const string Argument_TimeZoneNotFound = "The time zone ID '{0}' was not found on the local computer.";

	// Token: 0x04000B0B RID: 2827
	public const string Argument_TimeZoneInfoBadTZif = "The tzfile does not begin with the magic characters 'TZif'.  Please verify that the file is not corrupt.";

	// Token: 0x04000B0C RID: 2828
	public const string Argument_TimeZoneInfoInvalidTZif = "The TZif data structure is corrupt.";

	// Token: 0x04000B0D RID: 2829
	public const string Argument_ToExclusiveLessThanFromExclusive = "fromInclusive must be less than or equal to toExclusive.";

	// Token: 0x04000B0E RID: 2830
	public const string Argument_TransitionTimesAreIdentical = "The DaylightTransitionStart property must not equal the DaylightTransitionEnd property.";

	// Token: 0x04000B0F RID: 2831
	public const string Argument_UTCOutOfRange = "The UTC time represented when the offset is applied must be between year 0 and 10,000.";

	// Token: 0x04000B10 RID: 2832
	public const string ArgumentException_OtherNotArrayOfCorrectLength = "Object is not a array with the same number of elements as the array to compare it to.";

	// Token: 0x04000B11 RID: 2833
	public const string ArgumentException_TupleIncorrectType = "Argument must be of type {0}.";

	// Token: 0x04000B12 RID: 2834
	public const string ArgumentException_TupleLastArgumentNotATuple = "The last element of an eight element tuple must be a Tuple.";

	// Token: 0x04000B13 RID: 2835
	public const string ArgumentException_ValueTupleIncorrectType = "Argument must be of type {0}.";

	// Token: 0x04000B14 RID: 2836
	public const string ArgumentException_ValueTupleLastArgumentNotAValueTuple = "The last element of an eight element ValueTuple must be a ValueTuple.";

	// Token: 0x04000B15 RID: 2837
	public const string ArgumentNull_ArrayElement = "At least one element in the specified array was null.";

	// Token: 0x04000B16 RID: 2838
	public const string ArgumentNull_ArrayValue = "Found a null value within an array.";

	// Token: 0x04000B17 RID: 2839
	public const string ArgumentNull_Generic = "Value cannot be null.";

	// Token: 0x04000B18 RID: 2840
	public const string ArgumentNull_Obj = "Object cannot be null.";

	// Token: 0x04000B19 RID: 2841
	public const string ArgumentNull_String = "String reference not set to an instance of a String.";

	// Token: 0x04000B1A RID: 2842
	public const string ArgumentNull_Type = "Type cannot be null.";

	// Token: 0x04000B1B RID: 2843
	public const string ArgumentNull_Waithandles = "The waitHandles parameter cannot be null.";

	// Token: 0x04000B1C RID: 2844
	public const string ArgumentOutOfRange_AddValue = "Value to add was out of range.";

	// Token: 0x04000B1D RID: 2845
	public const string ArgumentOutOfRange_ActualValue = "Actual value was {0}.";

	// Token: 0x04000B1E RID: 2846
	public const string ArgumentOutOfRange_BadYearMonthDay = "Year, Month, and Day parameters describe an un-representable DateTime.";

	// Token: 0x04000B1F RID: 2847
	public const string ArgumentOutOfRange_BadHourMinuteSecond = "Hour, Minute, and Second parameters describe an un-representable DateTime.";

	// Token: 0x04000B20 RID: 2848
	public const string ArgumentOutOfRange_CalendarRange = "Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive.";

	// Token: 0x04000B21 RID: 2849
	public const string ArgumentOutOfRange_Capacity = "Capacity exceeds maximum capacity.";

	// Token: 0x04000B22 RID: 2850
	public const string ArgumentOutOfRange_DateArithmetic = "The added or subtracted value results in an un-representable DateTime.";

	// Token: 0x04000B23 RID: 2851
	public const string ArgumentOutOfRange_DateTimeBadMonths = "Months value must be between +/-120000.";

	// Token: 0x04000B24 RID: 2852
	public const string ArgumentOutOfRange_DateTimeBadTicks = "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x04000B25 RID: 2853
	public const string ArgumentOutOfRange_DateTimeBadYears = "Years value must be between +/-10000.";

	// Token: 0x04000B26 RID: 2854
	public const string ArgumentOutOfRange_Day = "Day must be between 1 and {0} for month {1}.";

	// Token: 0x04000B27 RID: 2855
	public const string ArgumentOutOfRange_DayOfWeek = "The DayOfWeek enumeration must be in the range 0 through 6.";

	// Token: 0x04000B28 RID: 2856
	public const string ArgumentOutOfRange_DayParam = "The Day parameter must be in the range 1 through 31.";

	// Token: 0x04000B29 RID: 2857
	public const string ArgumentOutOfRange_DecimalRound = "Decimal can only round to between 0 and 28 digits of precision.";

	// Token: 0x04000B2A RID: 2858
	public const string ArgumentOutOfRange_DecimalScale = "Decimal's scale value must be between 0 and 28, inclusive.";

	// Token: 0x04000B2B RID: 2859
	public const string ArgumentOutOfRange_EndIndexStartIndex = "endIndex cannot be greater than startIndex.";

	// Token: 0x04000B2C RID: 2860
	public const string ArgumentOutOfRange_Era = "Time value was out of era range.";

	// Token: 0x04000B2D RID: 2861
	public const string ArgumentOutOfRange_FileTimeInvalid = "Not a valid Win32 FileTime.";

	// Token: 0x04000B2E RID: 2862
	public const string ArgumentOutOfRange_GetByteCountOverflow = "Too many characters. The resulting number of bytes is larger than what can be returned as an int.";

	// Token: 0x04000B2F RID: 2863
	public const string ArgumentOutOfRange_GetCharCountOverflow = "Too many bytes. The resulting number of chars is larger than what can be returned as an int.";

	// Token: 0x04000B30 RID: 2864
	public const string ArgumentOutOfRange_IndexCount = "Index and count must refer to a location within the string.";

	// Token: 0x04000B31 RID: 2865
	public const string ArgumentOutOfRange_IndexCountBuffer = "Index and count must refer to a location within the buffer.";

	// Token: 0x04000B32 RID: 2866
	public const string ArgumentOutOfRange_IndexLength = "Index and length must refer to a location within the string.";

	// Token: 0x04000B33 RID: 2867
	public const string ArgumentOutOfRange_IndexString = "Index was out of range. Must be non-negative and less than the length of the string.";

	// Token: 0x04000B34 RID: 2868
	public const string ArgumentOutOfRange_InvalidEraValue = "Era value was not valid.";

	// Token: 0x04000B35 RID: 2869
	public const string ArgumentOutOfRange_InvalidHighSurrogate = "A valid high surrogate character is between 0xd800 and 0xdbff, inclusive.";

	// Token: 0x04000B36 RID: 2870
	public const string ArgumentOutOfRange_InvalidLowSurrogate = "A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive.";

	// Token: 0x04000B37 RID: 2871
	public const string ArgumentOutOfRange_InvalidUTF32 = "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).";

	// Token: 0x04000B38 RID: 2872
	public const string ArgumentOutOfRange_LengthGreaterThanCapacity = "The length cannot be greater than the capacity.";

	// Token: 0x04000B39 RID: 2873
	public const string ArgumentOutOfRange_ListInsert = "Index must be within the bounds of the List.";

	// Token: 0x04000B3A RID: 2874
	public const string ArgumentOutOfRange_ListItem = "Index was out of range. Must be non-negative and less than the size of the list.";

	// Token: 0x04000B3B RID: 2875
	public const string ArgumentOutOfRange_ListRemoveAt = "Index was out of range. Must be non-negative and less than the size of the list.";

	// Token: 0x04000B3C RID: 2876
	public const string ArgumentOutOfRange_Month = "Month must be between one and twelve.";

	// Token: 0x04000B3D RID: 2877
	public const string ArgumentOutOfRange_MonthParam = "The Month parameter must be in the range 1 through 12.";

	// Token: 0x04000B3E RID: 2878
	public const string ArgumentOutOfRange_MustBeNonNegInt32 = "Value must be non-negative and less than or equal to Int32.MaxValue.";

	// Token: 0x04000B3F RID: 2879
	public const string ArgumentOutOfRange_NeedNonNegOrNegative1 = "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.";

	// Token: 0x04000B40 RID: 2880
	public const string ArgumentOutOfRange_NegativeCapacity = "Capacity must be positive.";

	// Token: 0x04000B41 RID: 2881
	public const string ArgumentOutOfRange_NegativeCount = "Count cannot be less than zero.";

	// Token: 0x04000B42 RID: 2882
	public const string ArgumentOutOfRange_NegativeLength = "Length cannot be less than zero.";

	// Token: 0x04000B43 RID: 2883
	public const string ArgumentOutOfRange_NoGCLohSizeGreaterTotalSize = "lohSize can't be greater than totalSize";

	// Token: 0x04000B44 RID: 2884
	public const string ArgumentOutOfRange_OffsetLength = "Offset and length must refer to a position in the string.";

	// Token: 0x04000B45 RID: 2885
	public const string ArgumentOutOfRange_OffsetOut = "Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.";

	// Token: 0x04000B46 RID: 2886
	public const string ArgumentOutOfRange_PartialWCHAR = "Pointer startIndex and length do not refer to a valid string.";

	// Token: 0x04000B47 RID: 2887
	public const string ArgumentOutOfRange_Range = "Valid values are between {0} and {1}, inclusive.";

	// Token: 0x04000B48 RID: 2888
	public const string ArgumentOutOfRange_RoundingDigits = "Rounding digits must be between 0 and 15, inclusive.";

	// Token: 0x04000B49 RID: 2889
	public const string ArgumentOutOfRange_SmallMaxCapacity = "MaxCapacity must be one or greater.";

	// Token: 0x04000B4A RID: 2890
	public const string ArgumentOutOfRange_StartIndex = "StartIndex cannot be less than zero.";

	// Token: 0x04000B4B RID: 2891
	public const string ArgumentOutOfRange_StartIndexLargerThanLength = "startIndex cannot be larger than length of string.";

	// Token: 0x04000B4C RID: 2892
	public const string ArgumentOutOfRange_StartIndexLessThanLength = "startIndex must be less than length of string.";

	// Token: 0x04000B4D RID: 2893
	public const string ArgumentOutOfRange_UtcOffset = "The TimeSpan parameter must be within plus or minus 14.0 hours.";

	// Token: 0x04000B4E RID: 2894
	public const string ArgumentOutOfRange_UtcOffsetAndDaylightDelta = "The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours.";

	// Token: 0x04000B4F RID: 2895
	public const string ArgumentOutOfRange_Version = "Version's parameters must be greater than or equal to zero.";

	// Token: 0x04000B50 RID: 2896
	public const string ArgumentOutOfRange_Week = "The Week parameter must be in the range 1 through 5.";

	// Token: 0x04000B51 RID: 2897
	public const string ArgumentOutOfRange_Year = "Year must be between 1 and 9999.";

	// Token: 0x04000B52 RID: 2898
	public const string Arithmetic_NaN = "Function does not accept floating point Not-a-Number values.";

	// Token: 0x04000B53 RID: 2899
	public const string ArrayTypeMismatch_CantAssignType = "Source array type cannot be assigned to destination array type.";

	// Token: 0x04000B54 RID: 2900
	public const string BadImageFormatException_CouldNotLoadFileOrAssembly = "Could not load file or assembly '{0}'. An attempt was made to load a program with an incorrect format.";

	// Token: 0x04000B55 RID: 2901
	public const string CollectionCorrupted = "A prior operation on this collection was interrupted by an exception. Collection's state is no longer trusted.";

	// Token: 0x04000B56 RID: 2902
	public const string Exception_EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";

	// Token: 0x04000B57 RID: 2903
	public const string Exception_WasThrown = "Exception of type '{0}' was thrown.";

	// Token: 0x04000B58 RID: 2904
	public const string Format_BadBase64Char = "The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.";

	// Token: 0x04000B59 RID: 2905
	public const string Format_BadBase64CharArrayLength = "Invalid length for a Base-64 char array or string.";

	// Token: 0x04000B5A RID: 2906
	public const string Format_BadBoolean = "String was not recognized as a valid Boolean.";

	// Token: 0x04000B5B RID: 2907
	public const string Format_BadFormatSpecifier = "Format specifier '{0}' was invalid.";

	// Token: 0x04000B5C RID: 2908
	public const string Format_NoFormatSpecifier = "No format specifiers were provided.";

	// Token: 0x04000B5D RID: 2909
	public const string Format_BadQuote = "Cannot find a matching quote character for the character '{0}'.";

	// Token: 0x04000B5E RID: 2910
	public const string Format_EmptyInputString = "Input string was either empty or contained only whitespace.";

	// Token: 0x04000B5F RID: 2911
	public const string Format_GuidHexPrefix = "Expected hex 0x in '{0}'.";

	// Token: 0x04000B60 RID: 2912
	public const string Format_GuidInvLen = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).";

	// Token: 0x04000B61 RID: 2913
	public const string Format_GuidInvalidChar = "Guid string should only contain hexadecimal characters.";

	// Token: 0x04000B62 RID: 2914
	public const string Format_GuidBrace = "Expected {0xdddddddd, etc}.";

	// Token: 0x04000B63 RID: 2915
	public const string Format_GuidComma = "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).";

	// Token: 0x04000B64 RID: 2916
	public const string Format_GuidBraceAfterLastNumber = "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).";

	// Token: 0x04000B65 RID: 2917
	public const string Format_GuidDashes = "Dashes are in the wrong position for GUID parsing.";

	// Token: 0x04000B66 RID: 2918
	public const string Format_GuidEndBrace = "Could not find the ending brace.";

	// Token: 0x04000B67 RID: 2919
	public const string Format_ExtraJunkAtEnd = "Additional non-parsable characters are at the end of the string.";

	// Token: 0x04000B68 RID: 2920
	public const string Format_GuidUnrecognized = "Unrecognized Guid format.";

	// Token: 0x04000B69 RID: 2921
	public const string Format_IndexOutOfRange = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.";

	// Token: 0x04000B6A RID: 2922
	public const string Format_InvalidGuidFormatSpecification = "Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.";

	// Token: 0x04000B6B RID: 2923
	public const string Format_InvalidString = "Input string was not in a correct format.";

	// Token: 0x04000B6C RID: 2924
	public const string Format_NeedSingleChar = "String must be exactly one character long.";

	// Token: 0x04000B6D RID: 2925
	public const string Format_NoParsibleDigits = "Could not find any recognizable digits.";

	// Token: 0x04000B6E RID: 2926
	public const string Format_BadTimeSpan = "String was not recognized as a valid TimeSpan.";

	// Token: 0x04000B6F RID: 2927
	public const string InsufficientMemory_MemFailPoint = "Insufficient available memory to meet the expected demands of an operation at this time.  Please try again later.";

	// Token: 0x04000B70 RID: 2928
	public const string InsufficientMemory_MemFailPoint_TooBig = "Insufficient memory to meet the expected demands of an operation, and this system is likely to never satisfy this request.  If this is a 32 bit system, consider booting in 3 GB mode.";

	// Token: 0x04000B71 RID: 2929
	public const string InsufficientMemory_MemFailPoint_VAFrag = "Insufficient available memory to meet the expected demands of an operation at this time, possibly due to virtual address space fragmentation.  Please try again later.";

	// Token: 0x04000B72 RID: 2930
	public const string InvalidCast_CannotCastNullToValueType = "Null object cannot be converted to a value type.";

	// Token: 0x04000B73 RID: 2931
	public const string InvalidCast_DownCastArrayElement = "At least one element in the source array could not be cast down to the destination array type.";

	// Token: 0x04000B74 RID: 2932
	public const string InvalidCast_FromTo = "Invalid cast from '{0}' to '{1}'.";

	// Token: 0x04000B75 RID: 2933
	public const string InvalidCast_IConvertible = "Object must implement IConvertible.";

	// Token: 0x04000B76 RID: 2934
	public const string InvalidCast_StoreArrayElement = "Object cannot be stored in an array of this type.";

	// Token: 0x04000B77 RID: 2935
	public const string InvalidOperation_Calling = "WinRT Interop has already been initialized and cannot be initialized again.";

	// Token: 0x04000B78 RID: 2936
	public const string InvalidOperation_DateTimeParsing = "Internal Error in DateTime and Calendar operations.";

	// Token: 0x04000B79 RID: 2937
	public const string InvalidOperation_HandleIsNotInitialized = "Handle is not initialized.";

	// Token: 0x04000B7A RID: 2938
	public const string InvalidOperation_IComparerFailed = "Failed to compare two elements in the array.";

	// Token: 0x04000B7B RID: 2939
	public const string InvalidOperation_NoValue = "Nullable object must have a value.";

	// Token: 0x04000B7C RID: 2940
	public const string InvalidOperation_NullArray = "The underlying array is null.";

	// Token: 0x04000B7D RID: 2941
	public const string InvalidOperation_Overlapped_Pack = "Cannot pack a packed Overlapped again.";

	// Token: 0x04000B7E RID: 2942
	public const string InvalidOperation_ReadOnly = "Instance is read-only.";

	// Token: 0x04000B7F RID: 2943
	public const string InvalidOperation_ThreadWrongThreadStart = "The thread was created with a ThreadStart delegate that does not accept a parameter.";

	// Token: 0x04000B80 RID: 2944
	public const string InvalidOperation_UnknownEnumType = "Unknown enum type.";

	// Token: 0x04000B81 RID: 2945
	public const string InvalidOperation_WriteOnce = "This property has already been set and cannot be modified.";

	// Token: 0x04000B82 RID: 2946
	public const string InvalidOperation_ArrayCreateInstance_NotARuntimeType = "Array.CreateInstance() can only accept Type objects created by the runtime.";

	// Token: 0x04000B83 RID: 2947
	public const string InvalidOperation_TooEarly = "Internal Error: This operation cannot be invoked in an eager class constructor.";

	// Token: 0x04000B84 RID: 2948
	public const string InvalidOperation_NullContext = "Cannot call Set on a null context";

	// Token: 0x04000B85 RID: 2949
	public const string InvalidOperation_CannotUseAFCOtherThread = "AsyncFlowControl object must be used on the thread where it was created.";

	// Token: 0x04000B86 RID: 2950
	public const string InvalidOperation_CannotRestoreUnsupressedFlow = "Cannot restore context flow when it is not suppressed.";

	// Token: 0x04000B87 RID: 2951
	public const string InvalidOperation_CannotSupressFlowMultipleTimes = "Context flow is already suppressed.";

	// Token: 0x04000B88 RID: 2952
	public const string InvalidOperation_CannotUseAFCMultiple = "AsyncFlowControl object can be used only once to call Undo().";

	// Token: 0x04000B89 RID: 2953
	public const string InvalidOperation_AsyncFlowCtrlCtxMismatch = "AsyncFlowControl objects can be used to restore flow only on a Context that had its flow suppressed.";

	// Token: 0x04000B8A RID: 2954
	public const string InvalidOperation_AsyncIOInProgress = "The stream is currently in use by a previous operation on the stream.";

	// Token: 0x04000B8B RID: 2955
	public const string InvalidProgram_Default = "Common Language Runtime detected an invalid program.";

	// Token: 0x04000B8C RID: 2956
	public const string InvalidProgram_Specific = "Common Language Runtime detected an invalid program. The body of method '{0}' is invalid.";

	// Token: 0x04000B8D RID: 2957
	public const string InvalidProgram_Vararg = "Method '{0}' has a variable argument list. Variable argument lists are not supported in .NET Core.";

	// Token: 0x04000B8E RID: 2958
	public const string InvalidProgram_CallVirtFinalize = "Object.Finalize() can not be called directly. It is only callable by the runtime.";

	// Token: 0x04000B8F RID: 2959
	public const string InvalidProgram_NativeCallable = "NativeCallable method cannot be called from managed code.";

	// Token: 0x04000B90 RID: 2960
	public const string InvalidTimeZone_InvalidRegistryData = "The time zone ID '{0}' was found on the local computer, but the registry information was corrupt.";

	// Token: 0x04000B91 RID: 2961
	public const string InvalidTimeZone_InvalidFileData = "The time zone ID '{0}' was found on the local computer, but the file at '{1}' was corrupt.";

	// Token: 0x04000B92 RID: 2962
	public const string InvalidTimeZone_InvalidJulianDay = "Invalid Julian day in POSIX strings.";

	// Token: 0x04000B93 RID: 2963
	public const string InvalidTimeZone_NJulianDayNotSupported = "Julian n day in POSIX strings is not supported.";

	// Token: 0x04000B94 RID: 2964
	public const string InvalidTimeZone_NoTTInfoStructures = "There are no ttinfo structures in the tzfile.  At least one ttinfo structure is required in order to construct a TimeZoneInfo object.";

	// Token: 0x04000B95 RID: 2965
	public const string InvalidTimeZone_UnparseablePosixMDateString = "'{0}' is not a valid POSIX-TZ-environment-variable MDate rule.  A valid rule has the format 'Mm.w.d'.";

	// Token: 0x04000B96 RID: 2966
	public const string IO_DriveNotFound_Drive = "Could not find the drive '{0}'. The drive might not be ready or might not be mapped.";

	// Token: 0x04000B97 RID: 2967
	public const string IO_FileName_Name = "File name: '{0}'";

	// Token: 0x04000B98 RID: 2968
	public const string IO_FileLoad = "Could not load the specified file.";

	// Token: 0x04000B99 RID: 2969
	public const string IO_FileLoad_FileName = "Could not load the file '{0}'.";

	// Token: 0x04000B9A RID: 2970
	public const string Lazy_CreateValue_NoParameterlessCtorForT = "The lazily-initialized type does not have a public, parameterless constructor.";

	// Token: 0x04000B9B RID: 2971
	public const string Lazy_ctor_ModeInvalid = "The mode argument specifies an invalid value.";

	// Token: 0x04000B9C RID: 2972
	public const string Lazy_StaticInit_InvalidOperation = "ValueFactory returned null.";

	// Token: 0x04000B9D RID: 2973
	public const string Lazy_ToString_ValueNotCreated = "Value is not created.";

	// Token: 0x04000B9E RID: 2974
	public const string Lazy_Value_RecursiveCallsToValue = "ValueFactory attempted to access the Value property of this instance.";

	// Token: 0x04000B9F RID: 2975
	public const string MissingConstructor_Name = "Constructor on type '{0}' not found.";

	// Token: 0x04000BA0 RID: 2976
	public const string MustUseCCRewrite = "An assembly (probably '{1}') must be rewritten using the code contracts binary rewriter (CCRewrite) because it is calling Contract.{0} and the CONTRACTS_FULL symbol is defined.  Remove any explicit definitions of the CONTRACTS_FULL symbol from your project and rebuild.  CCRewrite can be downloaded from http://go.microsoft.com/fwlink/?LinkID=169180. \\r\\nAfter the rewriter is installed, it can be enabled in Visual Studio from the project's Properties page on the Code Contracts pane.  Ensure that 'Perform Runtime Contract Checking' is enabled, which will define CONTRACTS_FULL.";

	// Token: 0x04000BA1 RID: 2977
	public const string NotSupported_MaxWaitHandles = "The number of WaitHandles must be less than or equal to 64.";

	// Token: 0x04000BA2 RID: 2978
	public const string NotSupported_NoCodepageData = "No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.";

	// Token: 0x04000BA3 RID: 2979
	public const string NotSupported_StringComparison = "The string comparison type passed in is currently not supported.";

	// Token: 0x04000BA4 RID: 2980
	public const string NotSupported_VoidArray = "Arrays of System.Void are not supported.";

	// Token: 0x04000BA5 RID: 2981
	public const string NotSupported_ByRefLike = "Cannot create boxed ByRef-like values.";

	// Token: 0x04000BA6 RID: 2982
	public const string NotSupported_Type = "Type is not supported.";

	// Token: 0x04000BA7 RID: 2983
	public const string NotSupported_WaitAllSTAThread = "WaitAll for multiple handles on a STA thread is not supported.";

	// Token: 0x04000BA8 RID: 2984
	public const string ObjectDisposed_ObjectName_Name = "Object name: '{0}'.";

	// Token: 0x04000BA9 RID: 2985
	public const string Overflow_Byte = "Value was either too large or too small for an unsigned byte.";

	// Token: 0x04000BAA RID: 2986
	public const string Overflow_Char = "Value was either too large or too small for a character.";

	// Token: 0x04000BAB RID: 2987
	public const string Overflow_Double = "Value was either too large or too small for a Double.";

	// Token: 0x04000BAC RID: 2988
	public const string Overflow_TimeSpanElementTooLarge = "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.";

	// Token: 0x04000BAD RID: 2989
	public const string Overflow_Duration = "The duration cannot be returned for TimeSpan.MinValue because the absolute value of TimeSpan.MinValue exceeds the value of TimeSpan.MaxValue.";

	// Token: 0x04000BAE RID: 2990
	public const string Overflow_Int16 = "Value was either too large or too small for an Int16.";

	// Token: 0x04000BAF RID: 2991
	public const string Overflow_NegateTwosCompNum = "Negating the minimum value of a twos complement number is invalid.";

	// Token: 0x04000BB0 RID: 2992
	public const string Overflow_NegativeUnsigned = "The string was being parsed as an unsigned number and could not have a negative sign.";

	// Token: 0x04000BB1 RID: 2993
	public const string Overflow_SByte = "Value was either too large or too small for a signed byte.";

	// Token: 0x04000BB2 RID: 2994
	public const string Overflow_Single = "Value was either too large or too small for a Single.";

	// Token: 0x04000BB3 RID: 2995
	public const string Overflow_TimeSpanTooLong = "TimeSpan overflowed because the duration is too long.";

	// Token: 0x04000BB4 RID: 2996
	public const string Overflow_UInt16 = "Value was either too large or too small for a UInt16.";

	// Token: 0x04000BB5 RID: 2997
	public const string Rank_MultiDimNotSupported = "Only single dimension arrays are supported here.";

	// Token: 0x04000BB6 RID: 2998
	public const string RuntimeWrappedException = "An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.";

	// Token: 0x04000BB7 RID: 2999
	public const string SpinWait_SpinUntil_ArgumentNull = "The condition argument is null.";

	// Token: 0x04000BB8 RID: 3000
	public const string Serialization_CorruptField = "The value of the field '{0}' is invalid.  The serialized data is corrupt.";

	// Token: 0x04000BB9 RID: 3001
	public const string Serialization_InvalidData = "An error occurred while deserializing the object.  The serialized data is corrupt.";

	// Token: 0x04000BBA RID: 3002
	public const string Serialization_InvalidEscapeSequence = "The serialized data contained an invalid escape sequence '\\\\{0}'.";

	// Token: 0x04000BBB RID: 3003
	public const string Serialization_InvalidType = "Only system-provided types can be passed to the GetUninitializedObject method. '{0}' is not a valid instance of a type.";

	// Token: 0x04000BBC RID: 3004
	public const string SpinWait_SpinUntil_TimeoutWrong = "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000BBD RID: 3005
	public const string Threading_AbandonedMutexException = "The wait completed due to an abandoned mutex.";

	// Token: 0x04000BBE RID: 3006
	public const string Threading_SemaphoreFullException = "Adding the specified count to the semaphore would cause it to exceed its maximum count.";

	// Token: 0x04000BBF RID: 3007
	public const string Threading_ThreadInterrupted = "Thread was interrupted from a waiting state.";

	// Token: 0x04000BC0 RID: 3008
	public const string Threading_WaitHandleCannotBeOpenedException = "No handle of the given name exists.";

	// Token: 0x04000BC1 RID: 3009
	public const string Threading_WaitHandleCannotBeOpenedException_InvalidHandle = "A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.";

	// Token: 0x04000BC2 RID: 3010
	public const string TimeZoneNotFound_MissingData = "The time zone ID '{0}' was not found on the local computer.";

	// Token: 0x04000BC3 RID: 3011
	public const string TypeInitialization_Default = "Type constructor threw an exception.";

	// Token: 0x04000BC4 RID: 3012
	public const string TypeInitialization_Type = "The type initializer for '{0}' threw an exception.";

	// Token: 0x04000BC5 RID: 3013
	public const string TypeInitialization_Type_NoTypeAvailable = "A type initializer threw an exception. To determine which type, inspect the InnerException's StackTrace property.";

	// Token: 0x04000BC6 RID: 3014
	public const string Verification_Exception = "Operation could destabilize the runtime.";

	// Token: 0x04000BC7 RID: 3015
	public const string Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType = "Enum underlying type and the object must be same type or object. Type passed in was '{0}'; the enum underlying type was '{1}'.";

	// Token: 0x04000BC8 RID: 3016
	public const string Format_InvalidEnumFormatSpecification = "Format String can be only 'G', 'g', 'X', 'x', 'F', 'f', 'D' or 'd'.";

	// Token: 0x04000BC9 RID: 3017
	public const string Arg_MustBeEnumBaseTypeOrEnum = "The value passed in must be an enum base or an underlying type for an enum, such as an Int32.";

	// Token: 0x04000BCA RID: 3018
	public const string Arg_EnumUnderlyingTypeAndObjectMustBeSameType = "Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.";

	// Token: 0x04000BCB RID: 3019
	public const string Arg_MustBeType = "Type must be a type provided by the runtime.";

	// Token: 0x04000BCC RID: 3020
	public const string Arg_MustContainEnumInfo = "Must specify valid information for parsing in the string.";

	// Token: 0x04000BCD RID: 3021
	public const string Arg_EnumValueNotFound = "Requested value '{0}' was not found.";

	// Token: 0x04000BCE RID: 3022
	public const string Argument_StringZeroLength = "String cannot be of zero length.";

	// Token: 0x04000BCF RID: 3023
	public const string Argument_StringFirstCharIsZero = "The first char in the string is the null character.";

	// Token: 0x04000BD0 RID: 3024
	public const string Argument_LongEnvVarValue = "Environment variable name or value is too long.";

	// Token: 0x04000BD1 RID: 3025
	public const string Argument_IllegalEnvVarName = "Environment variable name cannot contain equal character.";

	// Token: 0x04000BD2 RID: 3026
	public const string AssumptionFailed = "Assumption failed.";

	// Token: 0x04000BD3 RID: 3027
	public const string AssumptionFailed_Cnd = "Assumption failed: {0}";

	// Token: 0x04000BD4 RID: 3028
	public const string AssertionFailed = "Assertion failed.";

	// Token: 0x04000BD5 RID: 3029
	public const string AssertionFailed_Cnd = "Assertion failed: {0}";

	// Token: 0x04000BD6 RID: 3030
	public const string PreconditionFailed = "Precondition failed.";

	// Token: 0x04000BD7 RID: 3031
	public const string PreconditionFailed_Cnd = "Precondition failed: {0}";

	// Token: 0x04000BD8 RID: 3032
	public const string PostconditionFailed = "Postcondition failed.";

	// Token: 0x04000BD9 RID: 3033
	public const string PostconditionFailed_Cnd = "Postcondition failed: {0}";

	// Token: 0x04000BDA RID: 3034
	public const string PostconditionOnExceptionFailed = "Postcondition failed after throwing an exception.";

	// Token: 0x04000BDB RID: 3035
	public const string PostconditionOnExceptionFailed_Cnd = "Postcondition failed after throwing an exception: {0}";

	// Token: 0x04000BDC RID: 3036
	public const string InvariantFailed = "Invariant failed.";

	// Token: 0x04000BDD RID: 3037
	public const string InvariantFailed_Cnd = "Invariant failed: {0}";

	// Token: 0x04000BDE RID: 3038
	public const string MissingEncodingNameResource = "Could not find a resource entry for the encoding codepage '{0} - {1}'";

	// Token: 0x04000BDF RID: 3039
	public const string Globalization_cp_1200 = "Unicode";

	// Token: 0x04000BE0 RID: 3040
	public const string Globalization_cp_1201 = "Unicode (Big-Endian)";

	// Token: 0x04000BE1 RID: 3041
	public const string Globalization_cp_12000 = "Unicode (UTF-32)";

	// Token: 0x04000BE2 RID: 3042
	public const string Globalization_cp_12001 = "Unicode (UTF-32 Big-Endian)";

	// Token: 0x04000BE3 RID: 3043
	public const string Globalization_cp_20127 = "US-ASCII";

	// Token: 0x04000BE4 RID: 3044
	public const string Globalization_cp_28591 = "Western European (ISO)";

	// Token: 0x04000BE5 RID: 3045
	public const string Globalization_cp_65000 = "Unicode (UTF-7)";

	// Token: 0x04000BE6 RID: 3046
	public const string Globalization_cp_65001 = "Unicode (UTF-8)";

	// Token: 0x04000BE7 RID: 3047
	public const string DebugAssertBanner = "---- DEBUG ASSERTION FAILED ----";

	// Token: 0x04000BE8 RID: 3048
	public const string DebugAssertLongMessage = "---- Assert Long Message ----";

	// Token: 0x04000BE9 RID: 3049
	public const string DebugAssertShortMessage = "---- Assert Short Message ----";

	// Token: 0x04000BEA RID: 3050
	public const string InvalidCast_Empty = "Object cannot be cast to Empty.";

	// Token: 0x04000BEB RID: 3051
	public const string Arg_UnknownTypeCode = "Unknown TypeCode value.";

	// Token: 0x04000BEC RID: 3052
	public const string Format_BadDatePattern = "Could not determine the order of year, month, and date from '{0}'.";

	// Token: 0x04000BED RID: 3053
	public const string Format_BadDateTime = "String '{0}' was not recognized as a valid DateTime.";

	// Token: 0x04000BEE RID: 3054
	public const string Format_BadDateTimeCalendar = "The DateTime represented by the string '{0}' is not supported in calendar '{1}'.";

	// Token: 0x04000BEF RID: 3055
	public const string Format_BadDayOfWeek = "String '{0}' was not recognized as a valid DateTime because the day of week was incorrect.";

	// Token: 0x04000BF0 RID: 3056
	public const string Format_DateOutOfRange = "The DateTime represented by the string '{0}' is out of range.";

	// Token: 0x04000BF1 RID: 3057
	public const string Format_MissingIncompleteDate = "There must be at least a partial date with a year present in the input string '{0}'.";

	// Token: 0x04000BF2 RID: 3058
	public const string Format_OffsetOutOfRange = "The time zone offset of string '{0}' must be within plus or minus 14 hours.";

	// Token: 0x04000BF3 RID: 3059
	public const string Format_RepeatDateTimePattern = "DateTime pattern '{0}' appears more than once with different values.";

	// Token: 0x04000BF4 RID: 3060
	public const string Format_UnknownDateTimeWord = "The string '{0}' was not recognized as a valid DateTime. There is an unknown word starting at index '{1}'.";

	// Token: 0x04000BF5 RID: 3061
	public const string Format_UTCOutOfRange = "The UTC representation of the date '{0}' falls outside the year range 1-9999.";

	// Token: 0x04000BF6 RID: 3062
	public const string RFLCT_Ambiguous = "Ambiguous match found.";

	// Token: 0x04000BF7 RID: 3063
	public const string AggregateException_ctor_DefaultMessage = "One or more errors occurred.";

	// Token: 0x04000BF8 RID: 3064
	public const string AggregateException_ctor_InnerExceptionNull = "An element of innerExceptions was null.";

	// Token: 0x04000BF9 RID: 3065
	public const string AggregateException_DeserializationFailure = "The serialization stream contains no inner exceptions.";

	// Token: 0x04000BFA RID: 3066
	public const string AggregateException_InnerException = "(Inner Exception #{0}) ";

	// Token: 0x04000BFB RID: 3067
	public const string ArgumentOutOfRange_TimeoutTooLarge = "Time-out interval must be less than 2^32-2.";

	// Token: 0x04000BFC RID: 3068
	public const string ArgumentOutOfRange_PeriodTooLarge = "Period must be less than 2^32-2.";

	// Token: 0x04000BFD RID: 3069
	public const string TaskScheduler_FromCurrentSynchronizationContext_NoCurrent = "The current SynchronizationContext may not be used as a TaskScheduler.";

	// Token: 0x04000BFE RID: 3070
	public const string TaskScheduler_ExecuteTask_WrongTaskScheduler = "ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler.";

	// Token: 0x04000BFF RID: 3071
	public const string TaskScheduler_InconsistentStateAfterTryExecuteTaskInline = "The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked.";

	// Token: 0x04000C00 RID: 3072
	public const string TaskSchedulerException_ctor_DefaultMessage = "An exception was thrown by a TaskScheduler.";

	// Token: 0x04000C01 RID: 3073
	public const string Task_MultiTaskContinuation_FireOptions = "It is invalid to exclude specific continuation kinds for continuations off of multiple tasks.";

	// Token: 0x04000C02 RID: 3074
	public const string Task_ContinueWith_ESandLR = "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.";

	// Token: 0x04000C03 RID: 3075
	public const string Task_MultiTaskContinuation_EmptyTaskList = "The tasks argument contains no tasks.";

	// Token: 0x04000C04 RID: 3076
	public const string Task_MultiTaskContinuation_NullTask = "The tasks argument included a null value.";

	// Token: 0x04000C05 RID: 3077
	public const string Task_FromAsync_PreferFairness = "It is invalid to specify TaskCreationOptions.PreferFairness in calls to FromAsync.";

	// Token: 0x04000C06 RID: 3078
	public const string Task_FromAsync_LongRunning = "It is invalid to specify TaskCreationOptions.LongRunning in calls to FromAsync.";

	// Token: 0x04000C07 RID: 3079
	public const string AsyncMethodBuilder_InstanceNotInitialized = "The builder was not properly initialized.";

	// Token: 0x04000C08 RID: 3080
	public const string TaskT_TransitionToFinal_AlreadyCompleted = "An attempt was made to transition a task to a final state when it had already completed.";

	// Token: 0x04000C09 RID: 3081
	public const string TaskT_DebuggerNoResult = "{Not yet computed}";

	// Token: 0x04000C0A RID: 3082
	public const string OperationCanceled = "The operation was canceled.";

	// Token: 0x04000C0B RID: 3083
	public const string CancellationToken_CreateLinkedToken_TokensIsEmpty = "No tokens were supplied.";

	// Token: 0x04000C0C RID: 3084
	public const string CancellationTokenSource_Disposed = "The CancellationTokenSource has been disposed.";

	// Token: 0x04000C0D RID: 3085
	public const string CancellationToken_SourceDisposed = "The CancellationTokenSource associated with this CancellationToken has been disposed.";

	// Token: 0x04000C0E RID: 3086
	public const string TaskExceptionHolder_UnknownExceptionType = "(Internal)Expected an Exception or an IEnumerable<Exception>";

	// Token: 0x04000C0F RID: 3087
	public const string TaskExceptionHolder_UnhandledException = "A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.";

	// Token: 0x04000C10 RID: 3088
	public const string Task_Delay_InvalidMillisecondsDelay = "The value needs to be either -1 (signifying an infinite timeout), 0 or a positive integer.";

	// Token: 0x04000C11 RID: 3089
	public const string Task_Delay_InvalidDelay = "The value needs to translate in milliseconds to -1 (signifying an infinite timeout), 0 or a positive integer less than or equal to Int32.MaxValue.";

	// Token: 0x04000C12 RID: 3090
	public const string Task_Dispose_NotCompleted = "A task may only be disposed if it is in a completion state (RanToCompletion, Faulted or Canceled).";

	// Token: 0x04000C13 RID: 3091
	public const string Task_WaitMulti_NullTask = "The tasks array included at least one null element.";

	// Token: 0x04000C14 RID: 3092
	public const string Task_ContinueWith_NotOnAnything = "The specified TaskContinuationOptions excluded all continuation kinds.";

	// Token: 0x04000C15 RID: 3093
	public const string Task_RunSynchronously_AlreadyStarted = "RunSynchronously may not be called on a task that was already started.";

	// Token: 0x04000C16 RID: 3094
	public const string Task_ThrowIfDisposed = "The task has been disposed.";

	// Token: 0x04000C17 RID: 3095
	public const string Task_RunSynchronously_TaskCompleted = "RunSynchronously may not be called on a task that has already completed.";

	// Token: 0x04000C18 RID: 3096
	public const string Task_RunSynchronously_Promise = "RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method.";

	// Token: 0x04000C19 RID: 3097
	public const string Task_RunSynchronously_Continuation = "RunSynchronously may not be called on a continuation task.";

	// Token: 0x04000C1A RID: 3098
	public const string Task_Start_AlreadyStarted = "Start may not be called on a task that was already started.";

	// Token: 0x04000C1B RID: 3099
	public const string Task_Start_ContinuationTask = "Start may not be called on a continuation task.";

	// Token: 0x04000C1C RID: 3100
	public const string Task_Start_Promise = "Start may not be called on a promise-style task.";

	// Token: 0x04000C1D RID: 3101
	public const string Task_Start_TaskCompleted = "Start may not be called on a task that has completed.";

	// Token: 0x04000C1E RID: 3102
	public const string TaskCanceledException_ctor_DefaultMessage = "A task was canceled.";

	// Token: 0x04000C1F RID: 3103
	public const string TaskCompletionSourceT_TrySetException_NoExceptions = "The exceptions collection was empty.";

	// Token: 0x04000C20 RID: 3104
	public const string TaskCompletionSourceT_TrySetException_NullException = "The exceptions collection included at least one null element.";

	// Token: 0x04000C21 RID: 3105
	public const string Argument_MinMaxValue = "'{0}' cannot be greater than {1}.";

	// Token: 0x04000C22 RID: 3106
	public const string ExecutionContext_ExceptionInAsyncLocalNotification = "An exception was not handled in an AsyncLocal<T> notification callback.";

	// Token: 0x04000C23 RID: 3107
	public const string InvalidOperation_WrongAsyncResultOrEndCalledMultiple = "Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.";

	// Token: 0x04000C24 RID: 3108
	public const string SpinLock_IsHeldByCurrentThread = "Thread tracking is disabled.";

	// Token: 0x04000C25 RID: 3109
	public const string SpinLock_TryEnter_LockRecursionException = "The calling thread already holds the lock.";

	// Token: 0x04000C26 RID: 3110
	public const string SpinLock_Exit_SynchronizationLockException = "The calling thread does not hold the lock.";

	// Token: 0x04000C27 RID: 3111
	public const string SpinLock_TryReliableEnter_ArgumentException = "The tookLock argument must be set to false before calling this method.";

	// Token: 0x04000C28 RID: 3112
	public const string SpinLock_TryEnter_ArgumentOutOfRange = "The timeout must be a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000C29 RID: 3113
	public const string ManualResetEventSlim_Disposed = "The event has been disposed.";

	// Token: 0x04000C2A RID: 3114
	public const string ManualResetEventSlim_ctor_SpinCountOutOfRange = "The spinCount argument must be in the range 0 to {0}, inclusive.";

	// Token: 0x04000C2B RID: 3115
	public const string ManualResetEventSlim_ctor_TooManyWaiters = "There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported.";

	// Token: 0x04000C2C RID: 3116
	public const string InvalidOperation_SendNotSupportedOnWindowsRTSynchronizationContext = "Send is not supported in the Windows Runtime SynchronizationContext";

	// Token: 0x04000C2D RID: 3117
	public const string SemaphoreSlim_Disposed = "The semaphore has been disposed.";

	// Token: 0x04000C2E RID: 3118
	public const string SemaphoreSlim_Release_CountWrong = "The releaseCount argument must be greater than zero.";

	// Token: 0x04000C2F RID: 3119
	public const string SemaphoreSlim_Wait_TimeoutWrong = "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000C30 RID: 3120
	public const string SemaphoreSlim_ctor_MaxCountWrong = "The maximumCount argument must be a positive number. If a maximum is not required, use the constructor without a maxCount parameter.";

	// Token: 0x04000C31 RID: 3121
	public const string SemaphoreSlim_ctor_InitialCountWrong = "The initialCount argument must be non-negative and less than or equal to the maximumCount.";

	// Token: 0x04000C32 RID: 3122
	public const string ThreadLocal_ValuesNotAvailable = "The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true.";

	// Token: 0x04000C33 RID: 3123
	public const string ThreadLocal_Value_RecursiveCallsToValue = "ValueFactory attempted to access the Value property of this instance.";

	// Token: 0x04000C34 RID: 3124
	public const string ThreadLocal_Disposed = "The ThreadLocal object has been disposed.";

	// Token: 0x04000C35 RID: 3125
	public const string LockRecursionException_WriteAfterReadNotAllowed = "Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.";

	// Token: 0x04000C36 RID: 3126
	public const string LockRecursionException_RecursiveWriteNotAllowed = "Recursive write lock acquisitions not allowed in this mode.";

	// Token: 0x04000C37 RID: 3127
	public const string LockRecursionException_ReadAfterWriteNotAllowed = "A read lock may not be acquired with the write lock held in this mode.";

	// Token: 0x04000C38 RID: 3128
	public const string LockRecursionException_RecursiveUpgradeNotAllowed = "Recursive upgradeable lock acquisitions not allowed in this mode.";

	// Token: 0x04000C39 RID: 3129
	public const string LockRecursionException_RecursiveReadNotAllowed = "Recursive read lock acquisitions not allowed in this mode.";

	// Token: 0x04000C3A RID: 3130
	public const string SynchronizationLockException_IncorrectDispose = "The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock.";

	// Token: 0x04000C3B RID: 3131
	public const string SynchronizationLockException_MisMatchedWrite = "The write lock is being released without being held.";

	// Token: 0x04000C3C RID: 3132
	public const string LockRecursionException_UpgradeAfterReadNotAllowed = "Upgradeable lock may not be acquired with read lock held.";

	// Token: 0x04000C3D RID: 3133
	public const string LockRecursionException_UpgradeAfterWriteNotAllowed = "Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer.";

	// Token: 0x04000C3E RID: 3134
	public const string SynchronizationLockException_MisMatchedUpgrade = "The upgradeable lock is being released without being held.";

	// Token: 0x04000C3F RID: 3135
	public const string SynchronizationLockException_MisMatchedRead = "The read lock is being released without being held.";

	// Token: 0x04000C40 RID: 3136
	public const string InvalidOperation_TimeoutsNotSupported = "Timeouts are not supported on this stream.";

	// Token: 0x04000C41 RID: 3137
	public const string NotSupported_SubclassOverride = "Derived classes must provide an implementation.";

	// Token: 0x04000C42 RID: 3138
	public const string InvalidOperation_NoPublicRemoveMethod = "Cannot remove the event handler since no public remove method exists for the event.";

	// Token: 0x04000C43 RID: 3139
	public const string InvalidOperation_NoPublicAddMethod = "Cannot add the event handler since no public add method exists for the event.";

	// Token: 0x04000C44 RID: 3140
	public const string SerializationException = "Serialization error.";

	// Token: 0x04000C45 RID: 3141
	public const string Serialization_NotFound = "Member '{0}' was not found.";

	// Token: 0x04000C46 RID: 3142
	public const string Serialization_OptionalFieldVersionValue = "Version value must be positive.";

	// Token: 0x04000C47 RID: 3143
	public const string Serialization_SameNameTwice = "Cannot add the same member twice to a SerializationInfo object.";

	// Token: 0x04000C48 RID: 3144
	public const string NotSupported_AbstractNonCLS = "This non-CLS method is not implemented.";

	// Token: 0x04000C49 RID: 3145
	public const string NotSupported_NoTypeInfo = "Cannot resolve {0} to a TypeInfo object.";

	// Token: 0x04000C4A RID: 3146
	public const string Arg_CustomAttributeFormatException = "Binary format of the specified custom attribute was invalid.";

	// Token: 0x04000C4B RID: 3147
	public const string Argument_InvalidMemberForNamedArgument = "The member must be either a field or a property.";

	// Token: 0x04000C4C RID: 3148
	public const string Arg_InvalidFilterCriteriaException = "Specified filter criteria was invalid.";

	// Token: 0x04000C4D RID: 3149
	public const string Arg_ParmArraySize = "Must specify one or more parameters.";

	// Token: 0x04000C4E RID: 3150
	public const string Arg_MustBePointer = "Type must be a Pointer.";

	// Token: 0x04000C4F RID: 3151
	public const string Argument_InvalidEnum = "The Enum type should contain one and only one instance field.";

	// Token: 0x04000C50 RID: 3152
	public const string Argument_MustHaveAttributeBaseClass = "Type passed in must be derived from System.Attribute or System.Attribute itself.";

	// Token: 0x04000C51 RID: 3153
	public const string InvalidFilterCriteriaException_CritString = "A String must be provided for the filter criteria.";

	// Token: 0x04000C52 RID: 3154
	public const string InvalidFilterCriteriaException_CritInt = "An Int32 must be provided for the filter criteria.";

	// Token: 0x04000C53 RID: 3155
	public const string InvalidOperation_NotSupportedOnWinRTEvent = "Adding or removing event handlers dynamically is not supported on WinRT events.";

	// Token: 0x04000C54 RID: 3156
	public const string PlatformNotSupported_ReflectionOnly = "ReflectionOnly loading is not supported on this platform.";

	// Token: 0x04000C55 RID: 3157
	public const string PlatformNotSupported_OSXFileLocking = "Locking/unlocking file regions is not supported on this platform. Use FileShare on the entire file instead.";

	// Token: 0x04000C56 RID: 3158
	public const string PlatformNotSupported_ReflectionEmit = "Dynamic code generation is not supported on this platform.";

	// Token: 0x04000C57 RID: 3159
	public const string MissingMember_Name = "Member '{0}' not found.";

	// Token: 0x04000C58 RID: 3160
	public const string MissingMethod_Name = "Method '{0}' not found.";

	// Token: 0x04000C59 RID: 3161
	public const string MissingField_Name = "Field '{0}' not found.";

	// Token: 0x04000C5A RID: 3162
	public const string Format_StringZeroLength = "String cannot have zero length.";

	// Token: 0x04000C5B RID: 3163
	public const string Security_CannotReadFileData = "The time zone ID '{0}' was found on the local computer, but the application does not have permission to read the file.";

	// Token: 0x04000C5C RID: 3164
	public const string Security_CannotReadRegistryData = "The time zone ID '{0}' was found on the local computer, but the application does not have permission to read the registry information.";

	// Token: 0x04000C5D RID: 3165
	public const string Security_InvalidAssemblyPublicKey = "Invalid assembly public key.";

	// Token: 0x04000C5E RID: 3166
	public const string Security_RegistryPermission = "Requested registry access is not allowed.";

	// Token: 0x04000C5F RID: 3167
	public const string ClassLoad_General = "Could not load type '{0}' from assembly '{1}'.";

	// Token: 0x04000C60 RID: 3168
	public const string ClassLoad_RankTooLarge = "'{0}' from assembly '{1}' has too many dimensions.";

	// Token: 0x04000C61 RID: 3169
	public const string ClassLoad_ExplicitGeneric = "Could not load type '{0}' from assembly '{1}' because generic types cannot have explicit layout.";

	// Token: 0x04000C62 RID: 3170
	public const string ClassLoad_BadFormat = "Could not load type '{0}' from assembly '{1}' because the format is invalid.";

	// Token: 0x04000C63 RID: 3171
	public const string ClassLoad_ValueClassTooLarge = "Array of type '{0}' from assembly '{1}' cannot be created because base value type is too large.";

	// Token: 0x04000C64 RID: 3172
	public const string ClassLoad_ExplicitLayout = "Could not load type '{0}' from assembly '{1}' because it contains an object field at offset '{2}' that is incorrectly aligned or overlapped by a non-object field.";

	// Token: 0x04000C65 RID: 3173
	public const string EE_MissingMethod = "Method not found: '{0}'.";

	// Token: 0x04000C66 RID: 3174
	public const string EE_MissingField = "Field not found: '{0}'.";

	// Token: 0x04000C67 RID: 3175
	public const string UnauthorizedAccess_RegistryKeyGeneric_Key = "Access to the registry key '{0}' is denied.";

	// Token: 0x04000C68 RID: 3176
	public const string UnknownError_Num = "Unknown error '{0}'.";

	// Token: 0x04000C69 RID: 3177
	public const string Argument_NeedStructWithNoRefs = "The specified Type must be a struct containing no references.";

	// Token: 0x04000C6A RID: 3178
	public const string ArgumentOutOfRange_AddressSpace = "The number of bytes cannot exceed the virtual address space on a 32 bit machine.";

	// Token: 0x04000C6B RID: 3179
	public const string ArgumentOutOfRange_UIntPtrMax = "The length of the buffer must be less than the maximum UIntPtr value for your platform.";

	// Token: 0x04000C6C RID: 3180
	public const string Arg_BufferTooSmall = "Not enough space available in the buffer.";

	// Token: 0x04000C6D RID: 3181
	public const string InvalidOperation_MustCallInitialize = "You must call Initialize on this object instance before using it.";

	// Token: 0x04000C6E RID: 3182
	public const string Argument_InvalidSafeBufferOffLen = "Offset and length were greater than the size of the SafeBuffer.";

	// Token: 0x04000C6F RID: 3183
	public const string Argument_NotEnoughBytesToRead = "There are not enough bytes remaining in the accessor to read at this position.";

	// Token: 0x04000C70 RID: 3184
	public const string Argument_NotEnoughBytesToWrite = "There are not enough bytes remaining in the accessor to write at this position.";

	// Token: 0x04000C71 RID: 3185
	public const string Argument_OffsetAndCapacityOutOfBounds = "Offset and capacity were greater than the size of the view.";

	// Token: 0x04000C72 RID: 3186
	public const string ArgumentOutOfRange_UnmanagedMemStreamLength = "UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress.";

	// Token: 0x04000C73 RID: 3187
	public const string Argument_UnmanagedMemAccessorWrapAround = "The UnmanagedMemoryAccessor capacity and offset would wrap around the high end of the address space.";

	// Token: 0x04000C74 RID: 3188
	public const string ArgumentOutOfRange_StreamLength = "Stream length must be non-negative and less than 2^31 - 1 - origin.";

	// Token: 0x04000C75 RID: 3189
	public const string ArgumentOutOfRange_UnmanagedMemStreamWrapAround = "The UnmanagedMemoryStream capacity would wrap around the high end of the address space.";

	// Token: 0x04000C76 RID: 3190
	public const string InvalidOperation_CalledTwice = "The method cannot be called twice on the same instance.";

	// Token: 0x04000C77 RID: 3191
	public const string IO_FixedCapacity = "Unable to expand length of this stream beyond its capacity.";

	// Token: 0x04000C78 RID: 3192
	public const string IO_StreamTooLong = "Stream was too long.";

	// Token: 0x04000C79 RID: 3193
	public const string Arg_BadDecimal = "Read an invalid decimal value from the buffer.";

	// Token: 0x04000C7A RID: 3194
	public const string NotSupported_Reading = "Accessor does not support reading.";

	// Token: 0x04000C7B RID: 3195
	public const string NotSupported_UmsSafeBuffer = "This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.";

	// Token: 0x04000C7C RID: 3196
	public const string NotSupported_Writing = "Accessor does not support writing.";

	// Token: 0x04000C7D RID: 3197
	public const string IndexOutOfRange_UMSPosition = "Unmanaged memory stream position was beyond the capacity of the stream.";

	// Token: 0x04000C7E RID: 3198
	public const string ObjectDisposed_ViewAccessorClosed = "Cannot access a closed accessor.";

	// Token: 0x04000C7F RID: 3199
	public const string ArgumentOutOfRange_PositionLessThanCapacityRequired = "The position may not be greater or equal to the capacity of the accessor.";

	// Token: 0x04000C80 RID: 3200
	public const string Arg_EndOfStreamException = "Attempted to read past the end of the stream.";

	// Token: 0x04000C81 RID: 3201
	public const string Argument_InvalidHandle = "'handle' has been disposed or is an invalid handle.";

	// Token: 0x04000C82 RID: 3202
	public const string Argument_AlreadyBoundOrSyncHandle = "'handle' has already been bound to the thread pool, or was not opened for asynchronous I/O.";

	// Token: 0x04000C83 RID: 3203
	public const string Argument_PreAllocatedAlreadyAllocated = "'preAllocated' is already in use.";

	// Token: 0x04000C84 RID: 3204
	public const string Argument_NativeOverlappedAlreadyFree = "'overlapped' has already been freed.";

	// Token: 0x04000C85 RID: 3205
	public const string Argument_NativeOverlappedWrongBoundHandle = "'overlapped' was not allocated by this ThreadPoolBoundHandle instance.";

	// Token: 0x04000C86 RID: 3206
	public const string NotSupported_FileStreamOnNonFiles = "FileStream was asked to open a device that was not a file. For support for devices like 'com1:' or 'lpt1:', call CreateFile, then use the FileStream constructors that take an OS handle as an IntPtr.";

	// Token: 0x04000C87 RID: 3207
	public const string Arg_ResourceFileUnsupportedVersion = "The ResourceReader class does not know how to read this version of .resources files.";

	// Token: 0x04000C88 RID: 3208
	public const string Resources_StreamNotValid = "Stream is not a valid resource file.";

	// Token: 0x04000C89 RID: 3209
	public const string BadImageFormat_ResourcesHeaderCorrupted = "Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file.";

	// Token: 0x04000C8A RID: 3210
	public const string BadImageFormat_NegativeStringLength = "Corrupt .resources file. String length must be non-negative.";

	// Token: 0x04000C8B RID: 3211
	public const string BadImageFormat_ResourcesNameInvalidOffset = "Corrupt .resources file. The Invalid offset into name section is .";

	// Token: 0x04000C8C RID: 3212
	public const string BadImageFormat_TypeMismatch = "Corrupt .resources file.  The specified type doesn't match the available data in the stream.";

	// Token: 0x04000C8D RID: 3213
	public const string BadImageFormat_ResourceNameCorrupted_NameIndex = "Corrupt .resources file. The resource name for name index that extends past the end of the stream is ";

	// Token: 0x04000C8E RID: 3214
	public const string BadImageFormat_ResourcesDataInvalidOffset = "Corrupt .resources file. Invalid offset  into data section is ";

	// Token: 0x04000C8F RID: 3215
	public const string Format_Bad7BitInt32 = "Too many bytes in what should have been a 7 bit encoded Int32.";

	// Token: 0x04000C90 RID: 3216
	public const string BadImageFormat_InvalidType = "Corrupt .resources file.  The specified type doesn't exist.";

	// Token: 0x04000C91 RID: 3217
	public const string ResourceReaderIsClosed = "ResourceReader is closed.";

	// Token: 0x04000C92 RID: 3218
	public const string Arg_MissingManifestResourceException = "Unable to find manifest resource.";

	// Token: 0x04000C93 RID: 3219
	public const string UnauthorizedAccess_MemStreamBuffer = "MemoryStream's internal buffer cannot be accessed.";

	// Token: 0x04000C94 RID: 3220
	public const string NotSupported_MemStreamNotExpandable = "Memory stream is not expandable.";

	// Token: 0x04000C95 RID: 3221
	public const string ArgumentNull_Stream = "Stream cannot be null.";

	// Token: 0x04000C96 RID: 3222
	public const string IO_InvalidStringLen_Len = "BinaryReader encountered an invalid string length of {0} characters.";

	// Token: 0x04000C97 RID: 3223
	public const string ArgumentOutOfRange_BinaryReaderFillBuffer = "The number of bytes requested does not fit into BinaryReader's internal buffer.";

	// Token: 0x04000C98 RID: 3224
	public const string Serialization_InsufficientDeserializationState = "Insufficient state to deserialize the object. Missing field '{0}'.";

	// Token: 0x04000C99 RID: 3225
	public const string NotSupported_UnitySerHolder = "The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself.";

	// Token: 0x04000C9A RID: 3226
	public const string Serialization_UnableToFindModule = "The given module {0} cannot be found within the assembly {1}.";

	// Token: 0x04000C9B RID: 3227
	public const string Argument_InvalidUnity = "Invalid Unity type.";

	// Token: 0x04000C9C RID: 3228
	public const string InvalidOperation_InvalidHandle = "The handle is invalid.";

	// Token: 0x04000C9D RID: 3229
	public const string PlatformNotSupported_NamedSynchronizationPrimitives = "The named version of this synchronization primitive is not supported on this platform.";

	// Token: 0x04000C9E RID: 3230
	public const string Overflow_MutexReacquireCount = "The current thread attempted to reacquire a mutex that has reached its maximum acquire count.";

	// Token: 0x04000C9F RID: 3231
	public const string Serialization_InsufficientState = "Insufficient state to return the real object.";

	// Token: 0x04000CA0 RID: 3232
	public const string Serialization_UnknownMember = "Cannot get the member '{0}'.";

	// Token: 0x04000CA1 RID: 3233
	public const string Serialization_NullSignature = "The method signature cannot be null.";

	// Token: 0x04000CA2 RID: 3234
	public const string Serialization_MemberTypeNotRecognized = "Unknown member type.";

	// Token: 0x04000CA3 RID: 3235
	public const string Serialization_BadParameterInfo = "Non existent ParameterInfo. Position bigger than member's parameters length.";

	// Token: 0x04000CA4 RID: 3236
	public const string Serialization_NoParameterInfo = "Serialized member does not have a ParameterInfo.";

	// Token: 0x04000CA5 RID: 3237
	public const string ArgumentNull_Assembly = "Assembly cannot be null.";

	// Token: 0x04000CA6 RID: 3238
	public const string Arg_InvalidNeutralResourcesLanguage_Asm_Culture = "The NeutralResourcesLanguageAttribute on the assembly \"{0}\" specifies an invalid culture name: \"{1}\".";

	// Token: 0x04000CA7 RID: 3239
	public const string Arg_InvalidNeutralResourcesLanguage_FallbackLoc = "The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".";

	// Token: 0x04000CA8 RID: 3240
	public const string Arg_InvalidSatelliteContract_Asm_Ver = "Satellite contract version attribute on the assembly '{0}' specifies an invalid version: {1}.";

	// Token: 0x04000CA9 RID: 3241
	public const string Arg_ResMgrNotResSet = "Type parameter must refer to a subclass of ResourceSet.";

	// Token: 0x04000CAA RID: 3242
	public const string BadImageFormat_ResourceNameCorrupted = "Corrupt .resources file. A resource name extends past the end of the stream.";

	// Token: 0x04000CAB RID: 3243
	public const string BadImageFormat_ResourcesNameTooLong = "Corrupt .resources file. Resource name extends past the end of the file.";

	// Token: 0x04000CAC RID: 3244
	public const string InvalidOperation_ResMgrBadResSet_Type = "'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.";

	// Token: 0x04000CAD RID: 3245
	public const string InvalidOperation_ResourceNotStream_Name = "Resource '{0}' was not a Stream - call GetObject instead.";

	// Token: 0x04000CAE RID: 3246
	public const string MissingManifestResource_MultipleBlobs = "A case-insensitive lookup for resource file \"{0}\" in assembly \"{1}\" found multiple entries. Remove the duplicates or specify the exact case.";

	// Token: 0x04000CAF RID: 3247
	public const string MissingManifestResource_NoNeutralAsm = "Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure \"{0}\" was correctly embedded or linked into assembly \"{1}\" at compile time, or that all the satellite assemblies required are loadable and fully signed.";

	// Token: 0x04000CB0 RID: 3248
	public const string MissingManifestResource_NoNeutralDisk = "Could not find any resources appropriate for the specified culture (or the neutral culture) on disk.";

	// Token: 0x04000CB1 RID: 3249
	public const string MissingManifestResource_NoPRIresources = "Unable to open Package Resource Index.";

	// Token: 0x04000CB2 RID: 3250
	public const string MissingManifestResource_ResWFileNotLoaded = "Unable to load resources for resource file \"{0}\" in package \"{1}\".";

	// Token: 0x04000CB3 RID: 3251
	public const string MissingSatelliteAssembly_Culture_Name = "The satellite assembly named \"{1}\" for fallback culture \"{0}\" either could not be found or could not be loaded. This is generally a setup problem. Please consider reinstalling or repairing the application.";

	// Token: 0x04000CB4 RID: 3252
	public const string MissingSatelliteAssembly_Default = "Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application.";

	// Token: 0x04000CB5 RID: 3253
	public const string NotSupported_ObsoleteResourcesFile = "Found an obsolete .resources file in assembly '{0}'. Rebuild that .resources file then rebuild that assembly.";

	// Token: 0x04000CB6 RID: 3254
	public const string NotSupported_ResourceObjectSerialization = "Cannot read resources that depend on serialization.";

	// Token: 0x04000CB7 RID: 3255
	public const string ObjectDisposed_ResourceSet = "Cannot access a closed resource set.";

	// Token: 0x04000CB8 RID: 3256
	public const string Arg_ResourceNameNotExist = "The specified resource name \"{0}\" does not exist in the resource file.";

	// Token: 0x04000CB9 RID: 3257
	public const string BadImageFormat_ResourceDataLengthInvalid = "Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.";

	// Token: 0x04000CBA RID: 3258
	public const string BadImageFormat_ResourcesIndexTooLong = "Corrupt .resources file. String for name index '{0}' extends past the end of the file.";

	// Token: 0x04000CBB RID: 3259
	public const string InvalidOperation_ResourceNotString_Name = "Resource '{0}' was not a String - call GetObject instead.";

	// Token: 0x04000CBC RID: 3260
	public const string InvalidOperation_ResourceNotString_Type = "Resource was of type '{0}' instead of String - call GetObject instead.";

	// Token: 0x04000CBD RID: 3261
	public const string NotSupported_WrongResourceReader_Type = "This .resources file should not be read with this reader. The resource reader type is \"{0}\".";

	// Token: 0x04000CBE RID: 3262
	public const string Arg_MustBeDelegate = "Type must derive from Delegate.";

	// Token: 0x04000CBF RID: 3263
	public const string NotSupported_GlobalMethodSerialization = "Serialization of global methods (including implicit serialization via the use of asynchronous delegates) is not supported.";

	// Token: 0x04000CC0 RID: 3264
	public const string NotSupported_DelegateSerHolderSerial = "DelegateSerializationHolder objects are designed to represent a delegate during serialization and are not serializable themselves.";

	// Token: 0x04000CC1 RID: 3265
	public const string DelegateSer_InsufficientMetadata = "The delegate cannot be serialized properly due to missing metadata for the target method.";

	// Token: 0x04000CC2 RID: 3266
	public const string Argument_NoUninitializedStrings = "Uninitialized Strings cannot be created.";

	// Token: 0x04000CC3 RID: 3267
	public const string ArgumentOutOfRangeException_NoGCRegionSizeTooLarge = "totalSize is too large. For more information about setting the maximum size, see \\\"Latency Modes\\\" in http://go.microsoft.com/fwlink/?LinkId=522706.";

	// Token: 0x04000CC4 RID: 3268
	public const string InvalidOperationException_AlreadyInNoGCRegion = "The NoGCRegion mode was already in progress.";

	// Token: 0x04000CC5 RID: 3269
	public const string InvalidOperationException_NoGCRegionAllocationExceeded = "Allocated memory exceeds specified memory for NoGCRegion mode.";

	// Token: 0x04000CC6 RID: 3270
	public const string InvalidOperationException_NoGCRegionInduced = "Garbage collection was induced in NoGCRegion mode.";

	// Token: 0x04000CC7 RID: 3271
	public const string InvalidOperationException_NoGCRegionNotInProgress = "NoGCRegion mode must be set.";

	// Token: 0x04000CC8 RID: 3272
	public const string InvalidOperationException_SetLatencyModeNoGC = "The NoGCRegion mode is in progress. End it and then set a different mode.";

	// Token: 0x04000CC9 RID: 3273
	public const string InvalidOperation_NotWithConcurrentGC = "This API is not available when the concurrent GC is enabled.";

	// Token: 0x04000CCA RID: 3274
	public const string ThreadState_AlreadyStarted = "Thread is running or terminated; it cannot restart.";

	// Token: 0x04000CCB RID: 3275
	public const string ThreadState_Dead_Priority = "Thread is dead; priority cannot be accessed.";

	// Token: 0x04000CCC RID: 3276
	public const string ThreadState_Dead_State = "Thread is dead; state cannot be accessed.";

	// Token: 0x04000CCD RID: 3277
	public const string ThreadState_NotStarted = "Thread has not been started.";

	// Token: 0x04000CCE RID: 3278
	public const string ThreadState_SetPriorityFailed = "Unable to set thread priority.";

	// Token: 0x04000CCF RID: 3279
	public const string Serialization_InvalidFieldState = "Object fields may not be properly initialized.";

	// Token: 0x04000CD0 RID: 3280
	public const string Acc_CreateAbst = "Cannot create an abstract class.";

	// Token: 0x04000CD1 RID: 3281
	public const string Acc_CreateGeneric = "Cannot create a type for which Type.ContainsGenericParameters is true.";

	// Token: 0x04000CD2 RID: 3282
	public const string NotSupported_ManagedActivation = "Cannot create uninitialized instances of types requiring managed activation.";

	// Token: 0x04000CD3 RID: 3283
	public const string PlatformNotSupported_ResourceManager_ResWFileUnsupportedMethod = "ResourceManager method '{0}' is not supported when reading from .resw resource files.";

	// Token: 0x04000CD4 RID: 3284
	public const string PlatformNotSupported_ResourceManager_ResWFileUnsupportedProperty = "ResourceManager property '{0}' is not supported when reading from .resw resource files.";

	// Token: 0x04000CD5 RID: 3285
	public const string Serialization_NonSerType = "Type '{0}' in Assembly '{1}' is not marked as serializable.";

	// Token: 0x04000CD6 RID: 3286
	public const string InvalidCast_DBNull = "Object cannot be cast to DBNull.";

	// Token: 0x04000CD7 RID: 3287
	public const string NotSupported_NYI = "This feature is not currently implemented.";

	// Token: 0x04000CD8 RID: 3288
	public const string Delegate_GarbageCollected = "The corresponding delegate has been garbage collected. Please make sure the delegate is still referenced by managed code when you are using the marshalled native function pointer.";

	// Token: 0x04000CD9 RID: 3289
	public const string Arg_AmbiguousMatchException = "Ambiguous match found.";

	// Token: 0x04000CDA RID: 3290
	public const string NotSupported_ChangeType = "ChangeType operation is not supported.";

	// Token: 0x04000CDB RID: 3291
	public const string Arg_EmptyArray = "Array may not be empty.";

	// Token: 0x04000CDC RID: 3292
	public const string MissingMember = "Member not found.";

	// Token: 0x04000CDD RID: 3293
	public const string MissingField = "Field not found.";

	// Token: 0x04000CDE RID: 3294
	public const string InvalidCast_FromDBNull = "Object cannot be cast from DBNull to other types.";

	// Token: 0x04000CDF RID: 3295
	public const string NotSupported_DBNullSerial = "Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed.";

	// Token: 0x04000CE0 RID: 3296
	public const string Serialization_StringBuilderCapacity = "The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length.";

	// Token: 0x04000CE1 RID: 3297
	public const string Serialization_StringBuilderMaxCapacity = "The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length.";

	// Token: 0x04000CE2 RID: 3298
	public const string PlatformNotSupported_Remoting = "Remoting is not supported on this platform.";

	// Token: 0x04000CE3 RID: 3299
	public const string PlatformNotSupported_StrongNameSigning = "Strong-name signing is not supported on this platform.";

	// Token: 0x04000CE4 RID: 3300
	public const string Serialization_MissingDateTimeData = "Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'.";

	// Token: 0x04000CE5 RID: 3301
	public const string Serialization_DateTimeTicksOutOfRange = "Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x04000CE6 RID: 3302
	public const string FeatureRemoved_Message = "Code to support feature '{0}' was removed during publishing. If this is in error, update the project configuration to not disable feature '{0}'.";

	// Token: 0x04000CE7 RID: 3303
	public const string Arg_InvalidANSIString = "The ANSI string passed in could not be converted from the default ANSI code page to Unicode.";

	// Token: 0x04000CE8 RID: 3304
	public const string PlatformNotSupported_ArgIterator = "ArgIterator is not supported on this platform.";

	// Token: 0x04000CE9 RID: 3305
	public const string Arg_TypeUnloadedException = "Type had been unloaded.";

	// Token: 0x04000CEA RID: 3306
	public const string Overflow_Currency = "Value was either too large or too small for a Currency.";

	// Token: 0x04000CEB RID: 3307
	public const string PlatformNotSupported_SecureBinarySerialization = "Secure binary serialization is not supported on this platform.";

	// Token: 0x04000CEC RID: 3308
	public const string Serialization_InvalidPtrValue = "An IntPtr or UIntPtr with an eight byte value cannot be deserialized on a machine with a four byte word size.";

	// Token: 0x04000CED RID: 3309
	public const string EventSource_AbstractMustNotDeclareEventMethods = "Abstract event source must not declare event methods ({0} with ID {1}).";

	// Token: 0x04000CEE RID: 3310
	public const string EventSource_AbstractMustNotDeclareKTOC = "Abstract event source must not declare {0} nested type.";

	// Token: 0x04000CEF RID: 3311
	public const string EventSource_AddScalarOutOfRange = "Getting out of bounds during scalar addition.";

	// Token: 0x04000CF0 RID: 3312
	public const string EventSource_BadHexDigit = "Bad Hexidecimal digit \"{0}\".";

	// Token: 0x04000CF1 RID: 3313
	public const string EventSource_ChannelTypeDoesNotMatchEventChannelValue = "Channel {0} does not match event channel value {1}.";

	// Token: 0x04000CF2 RID: 3314
	public const string EventSource_DataDescriptorsOutOfRange = "Data descriptors are out of range.";

	// Token: 0x04000CF3 RID: 3315
	public const string EventSource_DuplicateStringKey = "Multiple definitions for string \"{0}\".";

	// Token: 0x04000CF4 RID: 3316
	public const string EventSource_EnumKindMismatch = "The type of {0} is not expected in {1}.";

	// Token: 0x04000CF5 RID: 3317
	public const string EventSource_EvenHexDigits = "Must have an even number of Hexidecimal digits.";

	// Token: 0x04000CF6 RID: 3318
	public const string EventSource_EventChannelOutOfRange = "Channel {0} has a value of {1} which is outside the legal range (16-254).";

	// Token: 0x04000CF7 RID: 3319
	public const string EventSource_EventIdReused = "Event {0} has ID {1} which is already in use.";

	// Token: 0x04000CF8 RID: 3320
	public const string EventSource_EventMustHaveTaskIfNonDefaultOpcode = "Event {0} (with ID {1}) has a non-default opcode but not a task.";

	// Token: 0x04000CF9 RID: 3321
	public const string EventSource_EventMustNotBeExplicitImplementation = "Event method {0} (with ID {1}) is an explicit interface method implementation. Re-write method as implicit implementation.";

	// Token: 0x04000CFA RID: 3322
	public const string EventSource_EventNameDoesNotEqualTaskPlusOpcode = "Event {0} (with ID {1}) has a name that is not the concatenation of its task name and opcode.";

	// Token: 0x04000CFB RID: 3323
	public const string EventSource_EventNameReused = "Event name {0} used more than once.  If you wish to overload a method, the overloaded method should have a NonEvent attribute.";

	// Token: 0x04000CFC RID: 3324
	public const string EventSource_EventParametersMismatch = "Event {0} was called with {1} argument(s), but it is defined with {2} parameter(s).";

	// Token: 0x04000CFD RID: 3325
	public const string EventSource_EventSourceGuidInUse = "An instance of EventSource with Guid {0} already exists.";

	// Token: 0x04000CFE RID: 3326
	public const string EventSource_EventTooBig = "The payload for a single event is too large.";

	// Token: 0x04000CFF RID: 3327
	public const string EventSource_EventWithAdminChannelMustHaveMessage = "Event {0} specifies an Admin channel {1}. It must specify a Message property.";

	// Token: 0x04000D00 RID: 3328
	public const string EventSource_IllegalKeywordsValue = "Keyword {0} has a value of {1} which is outside the legal range (0-0x0000080000000000).";

	// Token: 0x04000D01 RID: 3329
	public const string EventSource_IllegalOpcodeValue = "Opcode {0} has a value of {1} which is outside the legal range (11-238).";

	// Token: 0x04000D02 RID: 3330
	public const string EventSource_IllegalTaskValue = "Task {0} has a value of {1} which is outside the legal range (1-65535).";

	// Token: 0x04000D03 RID: 3331
	public const string EventSource_IllegalValue = "Illegal value \"{0}\" (prefix strings with @ to indicate a literal string).";

	// Token: 0x04000D04 RID: 3332
	public const string EventSource_IncorrentlyAuthoredTypeInfo = "Incorrectly-authored TypeInfo - a type should be serialized as one field or as one group";

	// Token: 0x04000D05 RID: 3333
	public const string EventSource_InvalidCommand = "Invalid command value.";

	// Token: 0x04000D06 RID: 3334
	public const string EventSource_InvalidEventFormat = "Can't specify both etw event format flags.";

	// Token: 0x04000D07 RID: 3335
	public const string EventSource_KeywordCollision = "Keywords {0} and {1} are defined with the same value ({2}).";

	// Token: 0x04000D08 RID: 3336
	public const string EventSource_KeywordNeedPowerOfTwo = "Value {0} for keyword {1} needs to be a power of 2.";

	// Token: 0x04000D09 RID: 3337
	public const string EventSource_ListenerCreatedInsideCallback = "Creating an EventListener inside a EventListener callback.";

	// Token: 0x04000D0A RID: 3338
	public const string EventSource_ListenerNotFound = "Listener not found.";

	// Token: 0x04000D0B RID: 3339
	public const string EventSource_ListenerWriteFailure = "An error occurred when writing to a listener.";

	// Token: 0x04000D0C RID: 3340
	public const string EventSource_MaxChannelExceeded = "Attempt to define more than the maximum limit of 8 channels for a provider.";

	// Token: 0x04000D0D RID: 3341
	public const string EventSource_MismatchIdToWriteEvent = "Event {0} was assigned event ID {1} but {2} was passed to WriteEvent.";

	// Token: 0x04000D0E RID: 3342
	public const string EventSource_NeedGuid = "The Guid of an EventSource must be non zero.";

	// Token: 0x04000D0F RID: 3343
	public const string EventSource_NeedName = "The name of an EventSource must not be null.";

	// Token: 0x04000D10 RID: 3344
	public const string EventSource_NeedPositiveId = "Event IDs must be positive integers.";

	// Token: 0x04000D11 RID: 3345
	public const string EventSource_NoFreeBuffers = "No Free Buffers available from the operating system (e.g. event rate too fast).";

	// Token: 0x04000D12 RID: 3346
	public const string EventSource_NonCompliantTypeError = "The API supports only anonymous types or types decorated with the EventDataAttribute. Non-compliant type: {0} dataType.";

	// Token: 0x04000D13 RID: 3347
	public const string EventSource_NoRelatedActivityId = "EventSource expects the first parameter of the Event method to be of type Guid and to be named \"relatedActivityId\" when calling WriteEventWithRelatedActivityId.";

	// Token: 0x04000D14 RID: 3348
	public const string EventSource_NotSupportedArrayOfBinary = "Arrays of Binary are not supported.";

	// Token: 0x04000D15 RID: 3349
	public const string EventSource_NotSupportedArrayOfNil = "Arrays of Nil are not supported.";

	// Token: 0x04000D16 RID: 3350
	public const string EventSource_NotSupportedArrayOfNullTerminatedString = "Arrays of null-terminated string are not supported.";

	// Token: 0x04000D17 RID: 3351
	public const string EventSource_NotSupportedCustomSerializedData = "Enumerables of custom-serialized data are not supported";

	// Token: 0x04000D18 RID: 3352
	public const string EventSource_NotSupportedNestedArraysEnums = "Nested arrays/enumerables are not supported.";

	// Token: 0x04000D19 RID: 3353
	public const string EventSource_NullInput = "Null passed as a event argument.";

	// Token: 0x04000D1A RID: 3354
	public const string EventSource_OpcodeCollision = "Opcodes {0} and {1} are defined with the same value ({2}).";

	// Token: 0x04000D1B RID: 3355
	public const string EventSource_PinArrayOutOfRange = "Pins are out of range.";

	// Token: 0x04000D1C RID: 3356
	public const string EventSource_RecursiveTypeDefinition = "Recursive type definition is not supported.";

	// Token: 0x04000D1D RID: 3357
	public const string EventSource_SessionIdError = "Bit position in AllKeywords ({0}) must equal the command argument named \"EtwSessionKeyword\" ({1}).";

	// Token: 0x04000D1E RID: 3358
	public const string EventSource_StopsFollowStarts = "An event with stop suffix must follow a corresponding event with a start suffix.";

	// Token: 0x04000D1F RID: 3359
	public const string EventSource_TaskCollision = "Tasks {0} and {1} are defined with the same value ({2}).";

	// Token: 0x04000D20 RID: 3360
	public const string EventSource_TaskOpcodePairReused = "Event {0} (with ID {1}) has the same task/opcode pair as event {2} (with ID {3}).";

	// Token: 0x04000D21 RID: 3361
	public const string EventSource_TooManyArgs = "Too many arguments.";

	// Token: 0x04000D22 RID: 3362
	public const string EventSource_TooManyFields = "Too many fields in structure.";

	// Token: 0x04000D23 RID: 3363
	public const string EventSource_ToString = "EventSource({0}, {1})";

	// Token: 0x04000D24 RID: 3364
	public const string EventSource_TraitEven = "There must be an even number of trait strings (they are key-value pairs).";

	// Token: 0x04000D25 RID: 3365
	public const string EventSource_TypeMustBeSealedOrAbstract = "Event source types must be sealed or abstract.";

	// Token: 0x04000D26 RID: 3366
	public const string EventSource_TypeMustDeriveFromEventSource = "Event source types must derive from EventSource.";

	// Token: 0x04000D27 RID: 3367
	public const string EventSource_UndefinedChannel = "Use of undefined channel value {0} for event {1}.";

	// Token: 0x04000D28 RID: 3368
	public const string EventSource_UndefinedKeyword = "Use of undefined keyword value {0} for event {1}.";

	// Token: 0x04000D29 RID: 3369
	public const string EventSource_UndefinedOpcode = "Use of undefined opcode value {0} for event {1}.";

	// Token: 0x04000D2A RID: 3370
	public const string EventSource_UnknownEtwTrait = "Unknown ETW trait \"{0}\".";

	// Token: 0x04000D2B RID: 3371
	public const string EventSource_UnsupportedEventTypeInManifest = "Unsupported type {0} in event source.";

	// Token: 0x04000D2C RID: 3372
	public const string EventSource_UnsupportedMessageProperty = "Event {0} specifies an illegal or unsupported formatting message (\"{1}\").";

	// Token: 0x04000D2D RID: 3373
	public const string EventSource_VarArgsParameterMismatch = "The parameters to the Event method do not match the parameters to the WriteEvent method. This may cause the event to be displayed incorrectly.";

	// Token: 0x04000D2E RID: 3374
	public const string Arg_SurrogatesNotAllowedAsSingleChar = "Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead.";

	// Token: 0x04000D2F RID: 3375
	public const string CustomAttributeFormat_InvalidFieldFail = "'{0}' field specified was not found.";

	// Token: 0x04000D30 RID: 3376
	public const string CustomAttributeFormat_InvalidPropertyFail = "'{0}' property specified was not found.";

	// Token: 0x04000D31 RID: 3377
	public const string ArrayTypeMismatch_ConstrainedCopy = "Array.ConstrainedCopy will only work on array types that are provably compatible, without any form of boxing, unboxing, widening, or casting of each array element.  Change the array types (i.e., copy a Derived[] to a Base[]), or use a mitigation strategy in the CER for Array.Copy's less powerful reliability contract, such as cloning the array or throwing away the potentially corrupt destination array.";

	// Token: 0x04000D32 RID: 3378
	public const string Arg_DllNotFoundException = "Dll was not found.";

	// Token: 0x04000D33 RID: 3379
	public const string Arg_DllNotFoundExceptionParameterized = "Unable to load DLL '{0}': The specified module could not be found.";

	// Token: 0x04000D34 RID: 3380
	public const string Arg_DriveNotFoundException = "Attempted to access a drive that is not available.";

	// Token: 0x04000D35 RID: 3381
	public const string WrongSizeArrayInNStruct = "Type could not be marshaled because the length of an embedded array instance does not match the declared length in the layout.";

	// Token: 0x04000D36 RID: 3382
	public const string Arg_InteropMarshalUnmappableChar = "Cannot marshal: Encountered unmappable character.";

	// Token: 0x04000D37 RID: 3383
	public const string Arg_MarshalDirectiveException = "Marshaling directives are invalid.";

	// Token: 0x04000D38 RID: 3384
	public const string Arg_RegSubKeyValueAbsent = "No value exists with that name.";

	// Token: 0x04000D39 RID: 3385
	public const string Arg_RegValStrLenBug = "Registry value names should not be greater than 16,383 characters.";

	// Token: 0x04000D3A RID: 3386
	public const string Serialization_DelegatesNotSupported = "Serializing delegates is not supported on this platform.";

	// Token: 0x04000D3B RID: 3387
	public const string Arg_OpenType = "Cannot create an instance of {0} as it is an open type.";

	// Token: 0x04000D3C RID: 3388
	public const string Arg_PlatformNotSupported_AssemblyName_GetAssemblyName = "AssemblyName.GetAssemblyName() is not supported on this platform.";

	// Token: 0x04000D3D RID: 3389
	public const string NotSupported_OpenType = "Cannot create arrays of open type.";

	// Token: 0x04000D3E RID: 3390
	public const string NotSupported_ByRefLikeArray = "Cannot create arrays of ByRef-like values.";

	// Token: 0x04000D3F RID: 3391
	public const string StackTrace_AtWord = "   at ";

	// Token: 0x04000D40 RID: 3392
	public const string StackTrace_EndStackTraceFromPreviousThrow = "--- End of stack trace from previous location where exception was thrown ---";

	// Token: 0x04000D41 RID: 3393
	public const string InvalidAssemblyName = "The given assembly name or codebase was invalid";

	// Token: 0x04000D42 RID: 3394
	public const string Argument_HasToBeArrayClass = "Must be an array type.";

	// Token: 0x04000D43 RID: 3395
	public const string Argument_IdnBadBidi = "Left to right characters may not be mixed with right to left characters in IDN labels.";

	// Token: 0x04000D44 RID: 3396
	public const string Argument_IdnBadLabelSize = "IDN labels must be between 1 and 63 characters long.";

	// Token: 0x04000D45 RID: 3397
	public const string Argument_IdnBadNameSize = "IDN names must be between 1 and {0} characters long.";

	// Token: 0x04000D46 RID: 3398
	public const string Argument_IdnBadPunycode = "Invalid IDN encoded string.";

	// Token: 0x04000D47 RID: 3399
	public const string Argument_IdnBadStd3 = "Label contains character '{0}' not allowed with UseStd3AsciiRules";

	// Token: 0x04000D48 RID: 3400
	public const string Argument_IdnIllegalName = "Decoded string is not a valid IDN name.";

	// Token: 0x04000D49 RID: 3401
	public const string InvalidOperation_NotGenericType = "This operation is only valid on generic types.";

	// Token: 0x04000D4A RID: 3402
	public const string NotSupported_SignatureType = "This method is not supported on signature types.";

	// Token: 0x04000D4B RID: 3403
	public const string Memory_OutstandingReferences = "Release all references before disposing this instance.";

	// Token: 0x04000D4C RID: 3404
	public const string HashCode_HashCodeNotSupported = "HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.";

	// Token: 0x04000D4D RID: 3405
	public const string HashCode_EqualityNotSupported = "HashCode is a mutable struct and should not be compared with other HashCodes.";

	// Token: 0x04000D4E RID: 3406
	public const string IO_InvalidReadLength = "The read operation returned an invalid length.";

	// Token: 0x04000D4F RID: 3407
	public const string Arg_BasePathNotFullyQualified = "Basepath argument is not fully qualified.";

	// Token: 0x04000D50 RID: 3408
	public const string NullReference_InvokeNullRefReturned = "The target method returned a null reference.";

	// Token: 0x04000D51 RID: 3409
	public const string Thread_Operation_RequiresCurrentThread = "This operation must be performed on the same thread as that represented by the Thread instance.";

	// Token: 0x04000D52 RID: 3410
	public const string InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple = "Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.";

	// Token: 0x04000D53 RID: 3411
	public const string InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple = "Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.";

	// Token: 0x04000D54 RID: 3412
	public const string ArgumentOutOfRange_Week_ISO = "The week parameter must be in the range 1 through 53.";

	// Token: 0x04000D55 RID: 3413
	public const string net_uri_BadAuthority = "Invalid URI: The Authority/Host could not be parsed.";

	// Token: 0x04000D56 RID: 3414
	public const string net_uri_BadAuthorityTerminator = "Invalid URI: The Authority/Host cannot end with a backslash character ('\\\\').";

	// Token: 0x04000D57 RID: 3415
	public const string net_uri_BadFormat = "Invalid URI: The format of the URI could not be determined.";

	// Token: 0x04000D58 RID: 3416
	public const string net_uri_NeedFreshParser = "The URI parser instance passed into 'uriParser' parameter is already registered with the scheme name '{0}'.";

	// Token: 0x04000D59 RID: 3417
	public const string net_uri_AlreadyRegistered = "A URI scheme name '{0}' already has a registered custom parser.";

	// Token: 0x04000D5A RID: 3418
	public const string net_uri_BadHostName = "Invalid URI: The hostname could not be parsed.";

	// Token: 0x04000D5B RID: 3419
	public const string net_uri_BadPort = "Invalid URI: Invalid port specified.";

	// Token: 0x04000D5C RID: 3420
	public const string net_uri_BadScheme = "Invalid URI: The URI scheme is not valid.";

	// Token: 0x04000D5D RID: 3421
	public const string net_uri_BadString = "Invalid URI: There is an invalid sequence in the string.";

	// Token: 0x04000D5E RID: 3422
	public const string net_uri_BadUserPassword = "Invalid URI: The username:password construct is badly formed.";

	// Token: 0x04000D5F RID: 3423
	public const string net_uri_CannotCreateRelative = "A relative URI cannot be created because the 'uriString' parameter represents an absolute URI.";

	// Token: 0x04000D60 RID: 3424
	public const string net_uri_SchemeLimit = "Invalid URI: The Uri scheme is too long.";

	// Token: 0x04000D61 RID: 3425
	public const string net_uri_EmptyUri = "Invalid URI: The URI is empty.";

	// Token: 0x04000D62 RID: 3426
	public const string net_uri_InvalidUriKind = "The value '{0}' passed for the UriKind parameter is invalid.";

	// Token: 0x04000D63 RID: 3427
	public const string net_uri_MustRootedPath = "Invalid URI: A Dos path must be rooted, for example, 'c:\\\\'.";

	// Token: 0x04000D64 RID: 3428
	public const string net_uri_NotAbsolute = "This operation is not supported for a relative URI.";

	// Token: 0x04000D65 RID: 3429
	public const string net_uri_PortOutOfRange = "A derived type '{0}' has reported an invalid value for the Uri port '{1}'.";

	// Token: 0x04000D66 RID: 3430
	public const string net_uri_SizeLimit = "Invalid URI: The Uri string is too long.";

	// Token: 0x04000D67 RID: 3431
	public const string net_uri_UserDrivenParsing = "A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.";

	// Token: 0x04000D68 RID: 3432
	public const string net_uri_NotJustSerialization = "UriComponents.SerializationInfoString must not be combined with other UriComponents.";

	// Token: 0x04000D69 RID: 3433
	public const string net_uri_BadUnicodeHostForIdn = "An invalid Unicode character by IDN standards was specified in the host.";

	// Token: 0x04000D6A RID: 3434
	public const string Argument_ExtraNotValid = "Extra portion of URI not valid.";

	// Token: 0x04000D6B RID: 3435
	public const string Argument_InvalidUriSubcomponent = "The subcomponent, {0}, of this uri is not valid.";

	// Token: 0x04000D6C RID: 3436
	public const string AccessControl_InvalidHandle = "The supplied handle is invalid. This can happen when trying to set an ACL on an anonymous kernel object.";

	// Token: 0x04000D6D RID: 3437
	public const string Arg_RegSubKeyAbsent = "Cannot delete a subkey tree because the subkey does not exist.";

	// Token: 0x04000D6E RID: 3438
	public const string Arg_RegKeyDelHive = "Cannot delete a registry hive's subtree.";

	// Token: 0x04000D6F RID: 3439
	public const string Arg_RegKeyNoRemoteConnect = "No remote connection to '{0}' while trying to read the registry.";

	// Token: 0x04000D70 RID: 3440
	public const string Arg_RegKeyOutOfRange = "Registry HKEY was out of the legal range.";

	// Token: 0x04000D71 RID: 3441
	public const string Arg_RegKeyStrLenBug = "Registry key names should not be greater than 255 characters.";

	// Token: 0x04000D72 RID: 3442
	public const string Arg_RegBadKeyKind = "The specified RegistryValueKind is an invalid value.";

	// Token: 0x04000D73 RID: 3443
	public const string Arg_RegSetMismatchedKind = "The type of the value object did not match the specified RegistryValueKind or the object could not be properly converted.";

	// Token: 0x04000D74 RID: 3444
	public const string Arg_RegSetBadArrType = "RegistryKey.SetValue does not support arrays of type '{0}'. Only Byte[] and String[] are supported.";

	// Token: 0x04000D75 RID: 3445
	public const string Arg_RegSetStrArrNull = "RegistryKey.SetValue does not allow a String[] that contains a null String reference.";

	// Token: 0x04000D76 RID: 3446
	public const string Arg_DllInitFailure = "One machine may not have remote administration enabled, or both machines may not be running the remote registry service.";

	// Token: 0x04000D77 RID: 3447
	public const string Argument_InvalidRegistryOptionsCheck = "The specified RegistryOptions value is invalid.";

	// Token: 0x04000D78 RID: 3448
	public const string Argument_InvalidRegistryViewCheck = "The specified RegistryView value is invalid.";

	// Token: 0x04000D79 RID: 3449
	public const string Argument_InvalidRegistryKeyPermissionCheck = "The specified RegistryKeyPermissionCheck value is invalid.";

	// Token: 0x04000D7A RID: 3450
	public const string InvalidOperation_RegRemoveSubKey = "Registry key has subkeys and recursive removes are not supported by this method.";

	// Token: 0x04000D7B RID: 3451
	public const string ObjectDisposed_RegKeyClosed = "Cannot access a closed registry key.";

	// Token: 0x04000D7C RID: 3452
	public const string PlatformNotSupported_Registry = "Registry is not supported on this platform.";

	// Token: 0x04000D7D RID: 3453
	public const string UnauthorizedAccess_RegistryNoWrite = "Cannot write to the registry key.";

	// Token: 0x04000D7E RID: 3454
	public const string Cryptography_ArgECDHKeySizeMismatch = "The keys from both parties must be the same size to generate a secret agreement.";

	// Token: 0x04000D7F RID: 3455
	public const string Cryptography_ArgECDHRequiresECDHKey = "Keys used with the ECDiffieHellmanCng algorithm must have an algorithm group of ECDiffieHellman.";

	// Token: 0x04000D80 RID: 3456
	public const string Cryptography_TlsRequiresLabelAndSeed = "The TLS key derivation function requires both the label and seed properties to be set.";

	// Token: 0x04000D81 RID: 3457
	public const string Cryptography_TlsRequires64ByteSeed = "The TLS key derivation function requires a seed value of exactly 64 bytes.";

	// Token: 0x04000D82 RID: 3458
	public const string Cryptography_Config_EncodedOIDError = "Encoded OID length is too large (greater than 0x7f bytes).";

	// Token: 0x04000D83 RID: 3459
	public const string Cryptography_ECXmlSerializationFormatRequired = "XML serialization of an elliptic curve key requires using an overload which specifies the XML format to be used.";

	// Token: 0x04000D84 RID: 3460
	public const string Cryptography_InvalidCurveOid = "The specified Oid is not valid. The Oid.FriendlyName or Oid.Value property must be set.";

	// Token: 0x04000D85 RID: 3461
	public const string Cryptography_InvalidCurveKeyParameters = "The specified key parameters are not valid. Q.X and Q.Y are required fields. Q.X, Q.Y must be the same length. If D is specified it must be the same length as Q.X and Q.Y for named curves or the same length as Order for explicit curves.";

	// Token: 0x04000D86 RID: 3462
	public const string Cryptography_InvalidECCharacteristic2Curve = "The specified Characteristic2 curve parameters are not valid. Polynomial, A, B, G.X, G.Y, and Order are required. A, B, G.X, G.Y must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x04000D87 RID: 3463
	public const string Cryptography_InvalidECPrimeCurve = "The specified prime curve parameters are not valid. Prime, A, B, G.X, G.Y and Order are required and must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x04000D88 RID: 3464
	public const string Cryptography_InvalidECNamedCurve = "The specified named curve parameters are not valid. Only the Oid parameter must be set.";

	// Token: 0x04000D89 RID: 3465
	public const string Cryptography_InvalidKey_SemiWeak = "Specified key is a known semi-weak key for '{0}' and cannot be used.";

	// Token: 0x04000D8A RID: 3466
	public const string Cryptography_InvalidKey_Weak = "Specified key is a known weak key for '{0}' and cannot be used.";

	// Token: 0x04000D8B RID: 3467
	public const string Cryptography_InvalidOperation = "This operation is not supported for this class.";

	// Token: 0x04000D8C RID: 3468
	public const string Cryptography_InvalidPadding = "Padding is invalid and cannot be removed.";

	// Token: 0x04000D8D RID: 3469
	public const string Cryptography_MissingIV = "The cipher mode specified requires that an initialization vector (IV) be used.";

	// Token: 0x04000D8E RID: 3470
	public const string Cryptography_MissingKey = "No asymmetric key object has been associated with this formatter object.";

	// Token: 0x04000D8F RID: 3471
	public const string Cryptography_MissingOID = "Required object identifier (OID) cannot be found.";

	// Token: 0x04000D90 RID: 3472
	public const string Cryptography_MustTransformWholeBlock = "TransformBlock may only process bytes in block sized increments.";

	// Token: 0x04000D91 RID: 3473
	public const string Cryptography_NotValidPrivateKey = "Key is not a valid private key.";

	// Token: 0x04000D92 RID: 3474
	public const string Cryptography_NotValidPublicOrPrivateKey = "Key is not a valid public or private key.";

	// Token: 0x04000D93 RID: 3475
	public const string Cryptography_PartialBlock = "The input data is not a complete block.";

	// Token: 0x04000D94 RID: 3476
	public const string Cryptography_PasswordDerivedBytes_FewBytesSalt = "Salt is not at least eight bytes.";

	// Token: 0x04000D95 RID: 3477
	public const string Cryptography_RC2_EKS40 = "EffectiveKeySize value must be at least 40 bits.";

	// Token: 0x04000D96 RID: 3478
	public const string Cryptography_RC2_EKSKS = "KeySize value must be at least as large as the EffectiveKeySize value.";

	// Token: 0x04000D97 RID: 3479
	public const string Cryptography_RC2_EKSKS2 = "EffectiveKeySize must be the same as KeySize in this implementation.";

	// Token: 0x04000D98 RID: 3480
	public const string Cryptography_Rijndael_BlockSize = "BlockSize must be 128 in this implementation.";

	// Token: 0x04000D99 RID: 3481
	public const string Cryptography_TransformBeyondEndOfBuffer = "Attempt to transform beyond end of buffer.";

	// Token: 0x04000D9A RID: 3482
	public const string Cryptography_CipherModeNotSupported = "The specified CipherMode '{0}' is not supported.";

	// Token: 0x04000D9B RID: 3483
	public const string Cryptography_UnknownPaddingMode = "Unknown padding mode used.";

	// Token: 0x04000D9C RID: 3484
	public const string Cryptography_UnexpectedTransformTruncation = "CNG provider unexpectedly terminated encryption or decryption prematurely.";

	// Token: 0x04000D9D RID: 3485
	public const string Cryptography_UnsupportedPaddingMode = "The specified PaddingMode is not supported.";

	// Token: 0x04000D9E RID: 3486
	public const string NotSupported_Method = "Method not supported.";

	// Token: 0x04000D9F RID: 3487
	public const string Cryptography_AlgorithmTypesMustBeVisible = "Algorithms added to CryptoConfig must be accessable from outside their assembly.";

	// Token: 0x04000DA0 RID: 3488
	public const string Cryptography_AddNullOrEmptyName = "CryptoConfig cannot add a mapping for a null or empty name.";

	// Token: 0x04000DA1 RID: 3489
	public const string ArgumentOutOfRange_ConsoleKey = "Console key values must be between 0 and 255 inclusive.";

	// Token: 0x04000DA2 RID: 3490
	public const string Arg_InvalidComObjectException = "Attempt has been made to use a COM object that does not have a backing class factory.";

	// Token: 0x04000DA3 RID: 3491
	public const string Arg_MustBeNullTerminatedString = "The string must be null-terminated.";

	// Token: 0x04000DA4 RID: 3492
	public const string Arg_InvalidOleVariantTypeException = "Specified OLE variant was invalid.";

	// Token: 0x04000DA5 RID: 3493
	public const string Arg_SafeArrayRankMismatchException = "Specified array was not of the expected rank.";

	// Token: 0x04000DA6 RID: 3494
	public const string Arg_SafeArrayTypeMismatchException = "Specified array was not of the expected type.";

	// Token: 0x04000DA7 RID: 3495
	public const string TypeNotDelegate = "'Type '{0}' is not a delegate type.  EventTokenTable may only be used with delegate types.'";

	// Token: 0x04000DA8 RID: 3496
	public const string InvalidOperationException_ActorGraphCircular = "An Actor must not create a circular reference between itself (or one of its child Actors) and one of its parents.";

	// Token: 0x04000DA9 RID: 3497
	public const string InvalidOperation_ClaimCannotBeRemoved = "The Claim '{0}' was not able to be removed.  It is either not part of this Identity or it is a Claim that is owned by the Principal that contains this Identity. For example, the Principal will own the Claim when creating a GenericPrincipal with roles. The roles will be exposed through the Identity that is passed in the constructor, but not actually owned by the Identity.  Similar logic exists for a RolePrincipal.";

	// Token: 0x04000DAA RID: 3498
	public const string PlatformNotSupported_Serialization = "This instance contains state that cannot be serialized and deserialized on this platform.";

	// Token: 0x04000DAB RID: 3499
	public const string PrivilegeNotHeld_Default = "The process does not possess some privilege required for this operation.";

	// Token: 0x04000DAC RID: 3500
	public const string PrivilegeNotHeld_Named = "The process does not possess the '{0}' privilege which is required for this operation.";

	// Token: 0x04000DAD RID: 3501
	public const string CountdownEvent_Decrement_BelowZero = "Invalid attempt made to decrement the event's count below zero.";

	// Token: 0x04000DAE RID: 3502
	public const string CountdownEvent_Increment_AlreadyZero = "The event is already signaled and cannot be incremented.";

	// Token: 0x04000DAF RID: 3503
	public const string CountdownEvent_Increment_AlreadyMax = "The increment operation would cause the CurrentCount to overflow.";

	// Token: 0x04000DB0 RID: 3504
	public const string ArrayWithOffsetOverflow = "ArrayWithOffset: offset exceeds array size.";

	// Token: 0x04000DB1 RID: 3505
	public const string Arg_NotIsomorphic = "Object contains non-primitive or non-blittable data.";

	// Token: 0x04000DB2 RID: 3506
	public const string StructArrayTooLarge = "Array size exceeds addressing limitations.";

	// Token: 0x04000DB3 RID: 3507
	public const string IO_DriveNotFound = "Could not find the drive. The drive might not be ready or might not be mapped.";

	// Token: 0x04000DB4 RID: 3508
	public const string Argument_MustSupplyParent = "When supplying the ID of a containing object, the FieldInfo that identifies the current field within that object must also be supplied.";

	// Token: 0x04000DB5 RID: 3509
	public const string Argument_MemberAndArray = "Cannot supply both a MemberInfo and an Array to indicate the parent of a value type.";

	// Token: 0x04000DB6 RID: 3510
	public const string Argument_MustSupplyContainer = "When supplying a FieldInfo for fixing up a nested type, a valid ID for that containing object must also be supplied.";

	// Token: 0x04000DB7 RID: 3511
	public const string Serialization_NoID = "Object has never been assigned an objectID";

	// Token: 0x04000DB8 RID: 3512
	public const string Arg_SwitchExpressionException = "Non-exhaustive switch expression failed to match its input.";

	// Token: 0x04000DB9 RID: 3513
	public const string SwitchExpressionException_UnmatchedValue = "Unmatched value was {0}.";

	// Token: 0x04000DBA RID: 3514
	public const string Argument_InvalidRandomRange = "Range of random number does not contain at least one possibility.";

	// Token: 0x04000DBB RID: 3515
	public const string BufferWriterAdvancedTooFar = "Cannot advance past the end of the buffer, which has a size of {0}.";

	// Token: 0x04000DBC RID: 3516
	public const string net_gssapi_operation_failed_detailed_majoronly = "GSSAPI operation failed with error - {0}.";

	// Token: 0x04000DBD RID: 3517
	public const string net_gssapi_operation_failed_majoronly = "SSAPI operation failed with status: {0}.";
}
