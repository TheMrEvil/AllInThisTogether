using System;

namespace System.Runtime
{
	// Token: 0x0200001C RID: 28
	internal static class FxCop
	{
		// Token: 0x0200006B RID: 107
		public static class Category
		{
			// Token: 0x04000220 RID: 544
			public const string Design = "Microsoft.Design";

			// Token: 0x04000221 RID: 545
			public const string Globalization = "Microsoft.Globalization";

			// Token: 0x04000222 RID: 546
			public const string Maintainability = "Microsoft.Maintainability";

			// Token: 0x04000223 RID: 547
			public const string MSInternal = "Microsoft.MSInternal";

			// Token: 0x04000224 RID: 548
			public const string Naming = "Microsoft.Naming";

			// Token: 0x04000225 RID: 549
			public const string Performance = "Microsoft.Performance";

			// Token: 0x04000226 RID: 550
			public const string Reliability = "Microsoft.Reliability";

			// Token: 0x04000227 RID: 551
			public const string Security = "Microsoft.Security";

			// Token: 0x04000228 RID: 552
			public const string Usage = "Microsoft.Usage";

			// Token: 0x04000229 RID: 553
			public const string Configuration = "Configuration";

			// Token: 0x0400022A RID: 554
			public const string ReliabilityBasic = "Reliability";

			// Token: 0x0400022B RID: 555
			public const string Xaml = "XAML";
		}

		// Token: 0x0200006C RID: 108
		public static class Rule
		{
			// Token: 0x0400022C RID: 556
			public const string AptcaMethodsShouldOnlyCallAptcaMethods = "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods";

			// Token: 0x0400022D RID: 557
			public const string AssembliesShouldHaveValidStrongNames = "CA2210:AssembliesShouldHaveValidStrongNames";

			// Token: 0x0400022E RID: 558
			public const string AvoidCallingProblematicMethods = "CA2001:AvoidCallingProblematicMethods";

			// Token: 0x0400022F RID: 559
			public const string AvoidExcessiveComplexity = "CA1502:AvoidExcessiveComplexity";

			// Token: 0x04000230 RID: 560
			public const string AvoidNamespacesWithFewTypes = "CA1020:AvoidNamespacesWithFewTypes";

			// Token: 0x04000231 RID: 561
			public const string AvoidOutParameters = "CA1021:AvoidOutParameters";

			// Token: 0x04000232 RID: 562
			public const string AvoidUncalledPrivateCode = "CA1811:AvoidUncalledPrivateCode";

			// Token: 0x04000233 RID: 563
			public const string AvoidUninstantiatedInternalClasses = "CA1812:AvoidUninstantiatedInternalClasses";

			// Token: 0x04000234 RID: 564
			public const string AvoidUnsealedAttributes = "CA1813:AvoidUnsealedAttributes";

			// Token: 0x04000235 RID: 565
			public const string CollectionPropertiesShouldBeReadOnly = "CA2227:CollectionPropertiesShouldBeReadOnly";

			// Token: 0x04000236 RID: 566
			public const string CollectionsShouldImplementGenericInterface = "CA1010:CollectionsShouldImplementGenericInterface";

			// Token: 0x04000237 RID: 567
			public const string ConfigurationPropertyAttributeRule = "Configuration102:ConfigurationPropertyAttributeRule";

			// Token: 0x04000238 RID: 568
			public const string ConfigurationValidatorAttributeRule = "Configuration104:ConfigurationValidatorAttributeRule";

			// Token: 0x04000239 RID: 569
			public const string ConsiderPassingBaseTypesAsParameters = "CA1011:ConsiderPassingBaseTypesAsParameters";

			// Token: 0x0400023A RID: 570
			public const string CommunicationObjectThrowIf = "Reliability106";

			// Token: 0x0400023B RID: 571
			public const string ConfigurationPropertyNameRule = "Configuration103:ConfigurationPropertyNameRule";

			// Token: 0x0400023C RID: 572
			public const string DefaultParametersShouldNotBeUsed = "CA1026:DefaultParametersShouldNotBeUsed";

			// Token: 0x0400023D RID: 573
			public const string DefineAccessorsForAttributeArguments = "CA1019:DefineAccessorsForAttributeArguments";

			// Token: 0x0400023E RID: 574
			public const string DiagnosticsUtilityIsFatal = "Reliability108";

			// Token: 0x0400023F RID: 575
			public const string DisposableFieldsShouldBeDisposed = "CA2213:DisposableFieldsShouldBeDisposed";

			// Token: 0x04000240 RID: 576
			public const string DoNotCallOverridableMethodsInConstructors = "CA2214:DoNotCallOverridableMethodsInConstructors";

			// Token: 0x04000241 RID: 577
			public const string DoNotCatchGeneralExceptionTypes = "CA1031:DoNotCatchGeneralExceptionTypes";

			// Token: 0x04000242 RID: 578
			public const string DoNotDeclareReadOnlyMutableReferenceTypes = "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes";

			// Token: 0x04000243 RID: 579
			public const string DoNotDeclareVisibleInstanceFields = "CA1051:DoNotDeclareVisibleInstanceFields";

			// Token: 0x04000244 RID: 580
			public const string DoNotLockOnObjectsWithWeakIdentity = "CA2002:DoNotLockOnObjectsWithWeakIdentity";

			// Token: 0x04000245 RID: 581
			public const string DoNotIgnoreMethodResults = "CA1806:DoNotIgnoreMethodResults";

			// Token: 0x04000246 RID: 582
			public const string DoNotIndirectlyExposeMethodsWithLinkDemands = "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands";

			// Token: 0x04000247 RID: 583
			public const string DoNotPassLiteralsAsLocalizedParameters = "CA1303:DoNotPassLiteralsAsLocalizedParameters";

			// Token: 0x04000248 RID: 584
			public const string DoNotRaiseReservedExceptionTypes = "CA2201:DoNotRaiseReservedExceptionTypes";

			// Token: 0x04000249 RID: 585
			public const string EnumsShouldHaveZeroValue = "CA1008:EnumsShouldHaveZeroValue";

			// Token: 0x0400024A RID: 586
			public const string FlagsEnumsShouldHavePluralNames = "CA1714:FlagsEnumsShouldHavePluralNames";

			// Token: 0x0400024B RID: 587
			public const string GenericMethodsShouldProvideTypeParameter = "CA1004:GenericMethodsShouldProvideTypeParameter";

			// Token: 0x0400024C RID: 588
			public const string IdentifiersShouldBeSpelledCorrectly = "CA1704:IdentifiersShouldBeSpelledCorrectly";

			// Token: 0x0400024D RID: 589
			public const string IdentifiersShouldHaveCorrectSuffix = "CA1710:IdentifiersShouldHaveCorrectSuffix";

			// Token: 0x0400024E RID: 590
			public const string IdentifiersShouldNotContainTypeNames = "CA1720:IdentifiersShouldNotContainTypeNames";

			// Token: 0x0400024F RID: 591
			public const string IdentifiersShouldNotHaveIncorrectSuffix = "CA1711:IdentifiersShouldNotHaveIncorrectSuffix";

			// Token: 0x04000250 RID: 592
			public const string IdentifiersShouldNotMatchKeywords = "CA1716:IdentifiersShouldNotMatchKeywords";

			// Token: 0x04000251 RID: 593
			public const string ImplementStandardExceptionConstructors = "CA1032:ImplementStandardExceptionConstructors";

			// Token: 0x04000252 RID: 594
			public const string InstantiateArgumentExceptionsCorrectly = "CA2208:InstantiateArgumentExceptionsCorrectly";

			// Token: 0x04000253 RID: 595
			public const string InitializeReferenceTypeStaticFieldsInline = "CA1810:InitializeReferenceTypeStaticFieldsInline";

			// Token: 0x04000254 RID: 596
			public const string InterfaceMethodsShouldBeCallableByChildTypes = "CA1033:InterfaceMethodsShouldBeCallableByChildTypes";

			// Token: 0x04000255 RID: 597
			public const string MarkISerializableTypesWithSerializable = "CA2237:MarkISerializableTypesWithSerializable";

			// Token: 0x04000256 RID: 598
			public const string InvariantAssertRule = "Reliability101:InvariantAssertRule";

			// Token: 0x04000257 RID: 599
			public const string IsFatalRule = "Reliability108:IsFatalRule";

			// Token: 0x04000258 RID: 600
			public const string MarkMembersAsStatic = "CA1822:MarkMembersAsStatic";

			// Token: 0x04000259 RID: 601
			public const string NestedTypesShouldNotBeVisible = "CA1034:NestedTypesShouldNotBeVisible";

			// Token: 0x0400025A RID: 602
			public const string NormalizeStringsToUppercase = "CA1308:NormalizeStringsToUppercase";

			// Token: 0x0400025B RID: 603
			public const string OperatorOverloadsHaveNamedAlternates = "CA2225:OperatorOverloadsHaveNamedAlternates";

			// Token: 0x0400025C RID: 604
			public const string PropertyNamesShouldNotMatchGetMethods = "CA1721:PropertyNamesShouldNotMatchGetMethods";

			// Token: 0x0400025D RID: 605
			public const string PropertyTypesMustBeXamlVisible = "XAML1002:PropertyTypesMustBeXamlVisible";

			// Token: 0x0400025E RID: 606
			public const string PropertyExternalTypesMustBeKnown = "XAML1010:PropertyExternalTypesMustBeKnown";

			// Token: 0x0400025F RID: 607
			public const string ReplaceRepetitiveArgumentsWithParamsArray = "CA1025:ReplaceRepetitiveArgumentsWithParamsArray";

			// Token: 0x04000260 RID: 608
			public const string ResourceStringsShouldBeSpelledCorrectly = "CA1703:ResourceStringsShouldBeSpelledCorrectly";

			// Token: 0x04000261 RID: 609
			public const string ReviewSuppressUnmanagedCodeSecurityUsage = "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage";

			// Token: 0x04000262 RID: 610
			public const string ReviewUnusedParameters = "CA1801:ReviewUnusedParameters";

			// Token: 0x04000263 RID: 611
			public const string SecureAsserts = "CA2106:SecureAsserts";

			// Token: 0x04000264 RID: 612
			public const string SecureGetObjectDataOverrides = "CA2110:SecureGetObjectDataOverrides";

			// Token: 0x04000265 RID: 613
			public const string ShortAcronymsShouldBeUppercase = "CA1706:ShortAcronymsShouldBeUppercase";

			// Token: 0x04000266 RID: 614
			public const string SpecifyIFormatProvider = "CA1305:SpecifyIFormatProvider";

			// Token: 0x04000267 RID: 615
			public const string SpecifyMarshalingForPInvokeStringArguments = "CA2101:SpecifyMarshalingForPInvokeStringArguments";

			// Token: 0x04000268 RID: 616
			public const string StaticHolderTypesShouldNotHaveConstructors = "CA1053:StaticHolderTypesShouldNotHaveConstructors";

			// Token: 0x04000269 RID: 617
			public const string SystemAndMicrosoftNamespacesRequireApproval = "CA:SystemAndMicrosoftNamespacesRequireApproval";

			// Token: 0x0400026A RID: 618
			public const string UsePropertiesWhereAppropriate = "CA1024:UsePropertiesWhereAppropriate";

			// Token: 0x0400026B RID: 619
			public const string UriPropertiesShouldNotBeStrings = "CA1056:UriPropertiesShouldNotBeStrings";

			// Token: 0x0400026C RID: 620
			public const string VariableNamesShouldNotMatchFieldNames = "CA1500:VariableNamesShouldNotMatchFieldNames";

			// Token: 0x0400026D RID: 621
			public const string ThunkCallbackRule = "Reliability109:ThunkCallbackRule";

			// Token: 0x0400026E RID: 622
			public const string TransparentMethodsMustNotReferenceCriticalCode = "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule";

			// Token: 0x0400026F RID: 623
			public const string TypeConvertersMustBePublic = "XAML1004:TypeConvertersMustBePublic";

			// Token: 0x04000270 RID: 624
			public const string TypesMustHaveXamlCallableConstructors = "XAML1007:TypesMustHaveXamlCallableConstructors";

			// Token: 0x04000271 RID: 625
			public const string TypeNamesShouldNotMatchNamespaces = "CA1724:TypeNamesShouldNotMatchNamespaces";

			// Token: 0x04000272 RID: 626
			public const string TypesShouldHavePublicParameterlessConstructors = "XAML1009:TypesShouldHavePublicParameterlessConstructors";

			// Token: 0x04000273 RID: 627
			public const string UseEventsWhereAppropriate = "CA1030:UseEventsWhereAppropriate";

			// Token: 0x04000274 RID: 628
			public const string UseNewGuidHelperRule = "Reliability113:UseNewGuidHelperRule";

			// Token: 0x04000275 RID: 629
			public const string WrapExceptionsRule = "Reliability102:WrapExceptionsRule";
		}
	}
}
