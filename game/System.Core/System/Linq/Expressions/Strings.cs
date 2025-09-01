using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200029B RID: 667
	internal static class Strings
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0003C9F1 File Offset: 0x0003ABF1
		internal static string ReducibleMustOverrideReduce
		{
			get
			{
				return "reducible nodes must override Expression.Reduce()";
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0003C9F8 File Offset: 0x0003ABF8
		internal static string MustReduceToDifferent
		{
			get
			{
				return "node cannot reduce to itself or null";
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0003C9FF File Offset: 0x0003ABFF
		internal static string ReducedNotCompatible
		{
			get
			{
				return "cannot assign from the reduced node type to the original node type";
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0003CA06 File Offset: 0x0003AC06
		internal static string SetterHasNoParams
		{
			get
			{
				return "Setter must have parameters.";
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0003CA0D File Offset: 0x0003AC0D
		internal static string PropertyCannotHaveRefType
		{
			get
			{
				return "Property cannot have a managed pointer type.";
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0003CA14 File Offset: 0x0003AC14
		internal static string IndexesOfSetGetMustMatch
		{
			get
			{
				return "Indexing parameters of getter and setter must match.";
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0003CA1B File Offset: 0x0003AC1B
		internal static string AccessorsCannotHaveVarArgs
		{
			get
			{
				return "Accessor method should not have VarArgs.";
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0003CA22 File Offset: 0x0003AC22
		internal static string AccessorsCannotHaveByRefArgs
		{
			get
			{
				return "Accessor indexes cannot be passed ByRef.";
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0003CA29 File Offset: 0x0003AC29
		internal static string BoundsCannotBeLessThanOne
		{
			get
			{
				return "Bounds count cannot be less than 1";
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0003CA30 File Offset: 0x0003AC30
		internal static string TypeMustNotBeByRef
		{
			get
			{
				return "Type must not be ByRef";
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0003CA37 File Offset: 0x0003AC37
		internal static string TypeMustNotBePointer
		{
			get
			{
				return "Type must not be a pointer type";
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0003CA3E File Offset: 0x0003AC3E
		internal static string SetterMustBeVoid
		{
			get
			{
				return "Setter should have void type.";
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0003CA45 File Offset: 0x0003AC45
		internal static string PropertyTypeMustMatchGetter
		{
			get
			{
				return "Property type must match the value type of getter";
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0003CA4C File Offset: 0x0003AC4C
		internal static string PropertyTypeMustMatchSetter
		{
			get
			{
				return "Property type must match the value type of setter";
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0003CA53 File Offset: 0x0003AC53
		internal static string BothAccessorsMustBeStatic
		{
			get
			{
				return "Both accessors must be static.";
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0003CA5A File Offset: 0x0003AC5A
		internal static string OnlyStaticFieldsHaveNullInstance
		{
			get
			{
				return "Static field requires null instance, non-static field requires non-null instance.";
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0003CA61 File Offset: 0x0003AC61
		internal static string OnlyStaticPropertiesHaveNullInstance
		{
			get
			{
				return "Static property requires null instance, non-static property requires non-null instance.";
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0003CA68 File Offset: 0x0003AC68
		internal static string OnlyStaticMethodsHaveNullInstance
		{
			get
			{
				return "Static method requires null instance, non-static method requires non-null instance.";
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0003CA6F File Offset: 0x0003AC6F
		internal static string PropertyTypeCannotBeVoid
		{
			get
			{
				return "Property cannot have a void type.";
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0003CA76 File Offset: 0x0003AC76
		internal static string InvalidUnboxType
		{
			get
			{
				return "Can only unbox from an object or interface type to a value type.";
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0003CA7D File Offset: 0x0003AC7D
		internal static string ExpressionMustBeWriteable
		{
			get
			{
				return "Expression must be writeable";
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0003CA84 File Offset: 0x0003AC84
		internal static string ArgumentMustNotHaveValueType
		{
			get
			{
				return "Argument must not have a value type.";
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0003CA8B File Offset: 0x0003AC8B
		internal static string MustBeReducible
		{
			get
			{
				return "must be reducible node";
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0003CA92 File Offset: 0x0003AC92
		internal static string AllTestValuesMustHaveSameType
		{
			get
			{
				return "All test values must have the same type.";
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003CA99 File Offset: 0x0003AC99
		internal static string AllCaseBodiesMustHaveSameType
		{
			get
			{
				return "All case bodies and the default body must have the same type.";
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		internal static string DefaultBodyMustBeSupplied
		{
			get
			{
				return "Default body must be supplied if case bodies are not System.Void.";
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0003CAA7 File Offset: 0x0003ACA7
		internal static string LabelMustBeVoidOrHaveExpression
		{
			get
			{
				return "Label type must be System.Void if an expression is not supplied";
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0003CAAE File Offset: 0x0003ACAE
		internal static string LabelTypeMustBeVoid
		{
			get
			{
				return "Type must be System.Void for this label argument";
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0003CAB5 File Offset: 0x0003ACB5
		internal static string QuotedExpressionMustBeLambda
		{
			get
			{
				return "Quoted expression must be a lambda";
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0003CABC File Offset: 0x0003ACBC
		internal static string CollectionModifiedWhileEnumerating
		{
			get
			{
				return "Collection was modified; enumeration operation may not execute.";
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003CAC3 File Offset: 0x0003ACC3
		internal static string VariableMustNotBeByRef(object p0, object p1)
		{
			return SR.Format("Variable '{0}' uses unsupported type '{1}'. Reference types are not supported for variables.", p0, p1);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0003CAD1 File Offset: 0x0003ACD1
		internal static string CollectionReadOnly
		{
			get
			{
				return "Collection is read-only.";
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0003CAD8 File Offset: 0x0003ACD8
		internal static string AmbiguousMatchInExpandoObject(object p0)
		{
			return SR.Format("More than one key matching '{0}' was found in the ExpandoObject.", p0);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0003CAE5 File Offset: 0x0003ACE5
		internal static string SameKeyExistsInExpando(object p0)
		{
			return SR.Format("An element with the same key '{0}' already exists in the ExpandoObject.", p0);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003CAF2 File Offset: 0x0003ACF2
		internal static string KeyDoesNotExistInExpando(object p0)
		{
			return SR.Format("The specified key '{0}' does not exist in the ExpandoObject.", p0);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0003CAFF File Offset: 0x0003ACFF
		internal static string ArgCntMustBeGreaterThanNameCnt
		{
			get
			{
				return "Argument count must be greater than number of named arguments.";
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0003CB06 File Offset: 0x0003AD06
		internal static string InvalidMetaObjectCreated(object p0)
		{
			return SR.Format("An IDynamicMetaObjectProvider {0} created an invalid DynamicMetaObject instance.", p0);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003CB13 File Offset: 0x0003AD13
		internal static string BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return SR.Format("The result type '{0}' of the binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003CB22 File Offset: 0x0003AD22
		internal static string DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return SR.Format("The result of the dynamic binding produced by the object with type '{0}' for the binder '{1}' needs at least one restriction.", p0, p1);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0003CB30 File Offset: 0x0003AD30
		internal static string DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return SR.Format("The result type '{0}' of the dynamic binding produced by the object with type '{1}' for the binder '{2}' is not compatible with the result type '{3}' expected by the call site.", new object[]
			{
				p0,
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0003CB52 File Offset: 0x0003AD52
		internal static string DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return SR.Format("The result type '{0}' of the dynamic binding produced by binder '{1}' is not compatible with the result type '{2}' expected by the call site.", p0, p1, p2);
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0003CB61 File Offset: 0x0003AD61
		internal static string BindingCannotBeNull
		{
			get
			{
				return "Bind cannot return null.";
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0003CB68 File Offset: 0x0003AD68
		internal static string DuplicateVariable(object p0)
		{
			return SR.Format("Found duplicate parameter '{0}'. Each ParameterExpression in the list must be a unique object.", p0);
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0003CB75 File Offset: 0x0003AD75
		internal static string ArgumentTypeCannotBeVoid
		{
			get
			{
				return "Argument type cannot be void";
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003CB7C File Offset: 0x0003AD7C
		internal static string TypeParameterIsNotDelegate(object p0)
		{
			return SR.Format("Type parameter is {0}. Expected a delegate.", p0);
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0003CB89 File Offset: 0x0003AD89
		internal static string NoOrInvalidRuleProduced
		{
			get
			{
				return "No or Invalid rule produced";
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0003CB90 File Offset: 0x0003AD90
		internal static string TypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Type must be derived from System.Delegate";
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0003CB97 File Offset: 0x0003AD97
		internal static string FirstArgumentMustBeCallSite
		{
			get
			{
				return "First argument of delegate must be CallSite";
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0003CB9E File Offset: 0x0003AD9E
		internal static string StartEndMustBeOrdered
		{
			get
			{
				return "Start and End must be well ordered";
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0003CBA5 File Offset: 0x0003ADA5
		internal static string FaultCannotHaveCatchOrFinally
		{
			get
			{
				return "fault cannot be used with catch or finally clauses";
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		internal static string TryMustHaveCatchFinallyOrFault
		{
			get
			{
				return "try must have at least one catch, finally, or fault clause";
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0003CBB3 File Offset: 0x0003ADB3
		internal static string BodyOfCatchMustHaveSameTypeAsBodyOfTry
		{
			get
			{
				return "Body of catch must have the same type as body of try.";
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0003CBBA File Offset: 0x0003ADBA
		internal static string ExtensionNodeMustOverrideProperty(object p0)
		{
			return SR.Format("Extension node must override the property {0}.", p0);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0003CBC7 File Offset: 0x0003ADC7
		internal static string UserDefinedOperatorMustBeStatic(object p0)
		{
			return SR.Format("User-defined operator method '{0}' must be static.", p0);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		internal static string UserDefinedOperatorMustNotBeVoid(object p0)
		{
			return SR.Format("User-defined operator method '{0}' must not be void.", p0);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0003CBE1 File Offset: 0x0003ADE1
		internal static string CoercionOperatorNotDefined(object p0, object p1)
		{
			return SR.Format("No coercion operator is defined between types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003CBEF File Offset: 0x0003ADEF
		internal static string UnaryOperatorNotDefined(object p0, object p1)
		{
			return SR.Format("The unary operator {0} is not defined for the type '{1}'.", p0, p1);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003CBFD File Offset: 0x0003ADFD
		internal static string BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return SR.Format("The binary operator {0} is not defined for the types '{1}' and '{2}'.", p0, p1, p2);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		internal static string ReferenceEqualityNotDefined(object p0, object p1)
		{
			return SR.Format("Reference equality is not defined for the types '{0}' and '{1}'.", p0, p1);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0003CC1A File Offset: 0x0003AE1A
		internal static string OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return SR.Format("The operands for operator '{0}' do not match the parameters of method '{1}'.", p0, p1);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0003CC28 File Offset: 0x0003AE28
		internal static string OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return SR.Format("The return type of overload method for operator '{0}' does not match the parameter type of conversion method '{1}'.", p0, p1);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0003CC36 File Offset: 0x0003AE36
		internal static string ConversionIsNotSupportedForArithmeticTypes
		{
			get
			{
				return "Conversion is not supported for arithmetic types without operator overloading.";
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0003CC3D File Offset: 0x0003AE3D
		internal static string ArgumentMustBeArray
		{
			get
			{
				return "Argument must be array";
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0003CC44 File Offset: 0x0003AE44
		internal static string ArgumentMustBeBoolean
		{
			get
			{
				return "Argument must be boolean";
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		internal static string EqualityMustReturnBoolean(object p0)
		{
			return SR.Format("The user-defined equality method '{0}' must return a boolean value.", p0);
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0003CC58 File Offset: 0x0003AE58
		internal static string ArgumentMustBeFieldInfoOrPropertyInfo
		{
			get
			{
				return "Argument must be either a FieldInfo or PropertyInfo";
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0003CC5F File Offset: 0x0003AE5F
		internal static string ArgumentMustBeFieldInfoOrPropertyInfoOrMethod
		{
			get
			{
				return "Argument must be either a FieldInfo, PropertyInfo or MethodInfo";
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0003CC66 File Offset: 0x0003AE66
		internal static string ArgumentMustBeInstanceMember
		{
			get
			{
				return "Argument must be an instance member";
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0003CC6D File Offset: 0x0003AE6D
		internal static string ArgumentMustBeInteger
		{
			get
			{
				return "Argument must be of an integer type";
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0003CC74 File Offset: 0x0003AE74
		internal static string ArgumentMustBeArrayIndexType
		{
			get
			{
				return "Argument for array index must be of type Int32";
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0003CC7B File Offset: 0x0003AE7B
		internal static string ArgumentMustBeSingleDimensionalArrayType
		{
			get
			{
				return "Argument must be single-dimensional, zero-based array type";
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0003CC82 File Offset: 0x0003AE82
		internal static string ArgumentTypesMustMatch
		{
			get
			{
				return "Argument types do not match";
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003CC89 File Offset: 0x0003AE89
		internal static string CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return SR.Format("Cannot auto initialize elements of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0003CC96 File Offset: 0x0003AE96
		internal static string CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return SR.Format("Cannot auto initialize members of value type through property '{0}', use assignment instead", p0);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003CCA3 File Offset: 0x0003AEA3
		internal static string IncorrectTypeForTypeAs(object p0)
		{
			return SR.Format("The type used in TypeAs Expression must be of reference or nullable type, {0} is neither", p0);
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0003CCB0 File Offset: 0x0003AEB0
		internal static string CoalesceUsedOnNonNullType
		{
			get
			{
				return "Coalesce used with type that cannot be null";
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003CCB7 File Offset: 0x0003AEB7
		internal static string ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return SR.Format("An expression of type '{0}' cannot be used to initialize an array of type '{1}'", p0, p1);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003CCC5 File Offset: 0x0003AEC5
		internal static string ArgumentTypeDoesNotMatchMember(object p0, object p1)
		{
			return SR.Format(" Argument type '{0}' does not match the corresponding member type '{1}'", p0, p1);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003CCD3 File Offset: 0x0003AED3
		internal static string ArgumentMemberNotDeclOnType(object p0, object p1)
		{
			return SR.Format(" The member '{0}' is not declared on type '{1}' being created", p0, p1);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0003CCE1 File Offset: 0x0003AEE1
		internal static string ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return SR.Format("Expression of type '{0}' cannot be used for return type '{1}'", p0, p1);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003CCEF File Offset: 0x0003AEEF
		internal static string ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return SR.Format("Expression of type '{0}' cannot be used for assignment to type '{1}'", p0, p1);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0003CCFD File Offset: 0x0003AEFD
		internal static string ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return SR.Format("Expression of type '{0}' cannot be used for label of type '{1}'", p0, p1);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0003CD0B File Offset: 0x0003AF0B
		internal static string ExpressionTypeNotInvocable(object p0)
		{
			return SR.Format("Expression of type '{0}' cannot be invoked", p0);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0003CD18 File Offset: 0x0003AF18
		internal static string FieldNotDefinedForType(object p0, object p1)
		{
			return SR.Format("Field '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0003CD26 File Offset: 0x0003AF26
		internal static string InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return SR.Format("Instance field '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0003CD34 File Offset: 0x0003AF34
		internal static string FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return SR.Format("Field '{0}.{1}' is not defined for type '{2}'", p0, p1, p2);
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0003CD43 File Offset: 0x0003AF43
		internal static string IncorrectNumberOfIndexes
		{
			get
			{
				return "Incorrect number of indexes";
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0003CD4A File Offset: 0x0003AF4A
		internal static string IncorrectNumberOfLambdaDeclarationParameters
		{
			get
			{
				return "Incorrect number of parameters supplied for lambda declaration";
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0003CD51 File Offset: 0x0003AF51
		internal static string IncorrectNumberOfMembersForGivenConstructor
		{
			get
			{
				return " Incorrect number of members for constructor";
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0003CD58 File Offset: 0x0003AF58
		internal static string IncorrectNumberOfArgumentsForMembers
		{
			get
			{
				return "Incorrect number of arguments for the given members ";
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0003CD5F File Offset: 0x0003AF5F
		internal static string LambdaTypeMustBeDerivedFromSystemDelegate
		{
			get
			{
				return "Lambda type parameter must be derived from System.MulticastDelegate";
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0003CD66 File Offset: 0x0003AF66
		internal static string MemberNotFieldOrProperty(object p0)
		{
			return SR.Format("Member '{0}' not field or property", p0);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003CD73 File Offset: 0x0003AF73
		internal static string MethodContainsGenericParameters(object p0)
		{
			return SR.Format("Method {0} contains generic parameters", p0);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0003CD80 File Offset: 0x0003AF80
		internal static string MethodIsGeneric(object p0)
		{
			return SR.Format("Method {0} is a generic method definition", p0);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003CD8D File Offset: 0x0003AF8D
		internal static string MethodNotPropertyAccessor(object p0, object p1)
		{
			return SR.Format("The method '{0}.{1}' is not a property accessor", p0, p1);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0003CD9B File Offset: 0x0003AF9B
		internal static string PropertyDoesNotHaveGetter(object p0)
		{
			return SR.Format("The property '{0}' has no 'get' accessor", p0);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
		internal static string PropertyDoesNotHaveSetter(object p0)
		{
			return SR.Format("The property '{0}' has no 'set' accessor", p0);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0003CDB5 File Offset: 0x0003AFB5
		internal static string PropertyDoesNotHaveAccessor(object p0)
		{
			return SR.Format("The property '{0}' has no 'get' or 'set' accessors", p0);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003CDC2 File Offset: 0x0003AFC2
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return SR.Format("'{0}' is not a member of type '{1}'", p0, p1);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		internal static string NotAMemberOfAnyType(object p0)
		{
			return SR.Format("'{0}' is not a member of any type", p0);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003CDDD File Offset: 0x0003AFDD
		internal static string ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return SR.Format("ParameterExpression of type '{0}' cannot be used for delegate parameter of type '{1}'", p0, p1);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003CDEB File Offset: 0x0003AFEB
		internal static string PropertyNotDefinedForType(object p0, object p1)
		{
			return SR.Format("Property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003CDF9 File Offset: 0x0003AFF9
		internal static string InstancePropertyNotDefinedForType(object p0, object p1)
		{
			return SR.Format("Instance property '{0}' is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0003CE07 File Offset: 0x0003B007
		internal static string InstancePropertyWithoutParameterNotDefinedForType(object p0, object p1)
		{
			return SR.Format("Instance property '{0}' that takes no argument is not defined for type '{1}'", p0, p1);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003CE15 File Offset: 0x0003B015
		internal static string InstancePropertyWithSpecifiedParametersNotDefinedForType(object p0, object p1, object p2)
		{
			return SR.Format("Instance property '{0}{1}' is not defined for type '{2}'", p0, p1, p2);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003CE24 File Offset: 0x0003B024
		internal static string InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return SR.Format("Method '{0}' declared on type '{1}' cannot be called with instance of type '{2}'", p0, p1, p2);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003CE33 File Offset: 0x0003B033
		internal static string TypeMissingDefaultConstructor(object p0)
		{
			return SR.Format("Type '{0}' does not have a default constructor", p0);
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0003CE40 File Offset: 0x0003B040
		internal static string ElementInitializerMethodNotAdd
		{
			get
			{
				return "Element initializer method must be named 'Add'";
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003CE47 File Offset: 0x0003B047
		internal static string ElementInitializerMethodNoRefOutParam(object p0, object p1)
		{
			return SR.Format("Parameter '{0}' of element initializer method '{1}' must not be a pass by reference parameter", p0, p1);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0003CE55 File Offset: 0x0003B055
		internal static string ElementInitializerMethodWithZeroArgs
		{
			get
			{
				return "Element initializer method must have at least 1 parameter";
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0003CE5C File Offset: 0x0003B05C
		internal static string ElementInitializerMethodStatic
		{
			get
			{
				return "Element initializer method must be an instance method";
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003CE63 File Offset: 0x0003B063
		internal static string TypeNotIEnumerable(object p0)
		{
			return SR.Format("Type '{0}' is not IEnumerable", p0);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003CE70 File Offset: 0x0003B070
		internal static string UnhandledBinary(object p0)
		{
			return SR.Format("Unhandled binary: {0}", p0);
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0003CE7D File Offset: 0x0003B07D
		internal static string UnhandledBinding
		{
			get
			{
				return "Unhandled binding ";
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0003CE84 File Offset: 0x0003B084
		internal static string UnhandledBindingType(object p0)
		{
			return SR.Format("Unhandled Binding Type: {0}", p0);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0003CE91 File Offset: 0x0003B091
		internal static string UnhandledUnary(object p0)
		{
			return SR.Format("Unhandled unary: {0}", p0);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003CE9E File Offset: 0x0003B09E
		internal static string UnknownBindingType
		{
			get
			{
				return "Unknown binding type";
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0003CEA5 File Offset: 0x0003B0A5
		internal static string UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return SR.Format("The user-defined operator method '{1}' for operator '{0}' must have identical parameter and return types.", p0, p1);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003CEB3 File Offset: 0x0003B0B3
		internal static string UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return SR.Format("The user-defined operator method '{1}' for operator '{0}' must return the same type as its parameter or a derived type.", p0, p1);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003CEC1 File Offset: 0x0003B0C1
		internal static string LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return SR.Format("The user-defined operator method '{1}' for operator '{0}' must have associated boolean True and False operators.", p0, p1);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003CECF File Offset: 0x0003B0CF
		internal static string MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return SR.Format("No method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0003CEDD File Offset: 0x0003B0DD
		internal static string GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return SR.Format("No generic method '{0}' on type '{1}' is compatible with the supplied type arguments and arguments. No type arguments should be provided if the method is non-generic. ", p0, p1);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003CEEB File Offset: 0x0003B0EB
		internal static string MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return SR.Format("More than one method '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0003CEF9 File Offset: 0x0003B0F9
		internal static string PropertyWithMoreThanOneMatch(object p0, object p1)
		{
			return SR.Format("More than one property '{0}' on type '{1}' is compatible with the supplied arguments.", p0, p1);
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0003CF07 File Offset: 0x0003B107
		internal static string IncorrectNumberOfTypeArgsForFunc
		{
			get
			{
				return "An incorrect number of type arguments were specified for the declaration of a Func type.";
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0003CF0E File Offset: 0x0003B10E
		internal static string IncorrectNumberOfTypeArgsForAction
		{
			get
			{
				return "An incorrect number of type arguments were specified for the declaration of an Action type.";
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0003CF15 File Offset: 0x0003B115
		internal static string ArgumentCannotBeOfTypeVoid
		{
			get
			{
				return "Argument type cannot be System.Void.";
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0003CF1C File Offset: 0x0003B11C
		internal static string OutOfRange(object p0, object p1)
		{
			return SR.Format("{0} must be greater than or equal to {1}", p0, p1);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003CF2A File Offset: 0x0003B12A
		internal static string LabelTargetAlreadyDefined(object p0)
		{
			return SR.Format("Cannot redefine label '{0}' in an inner block.", p0);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0003CF37 File Offset: 0x0003B137
		internal static string LabelTargetUndefined(object p0)
		{
			return SR.Format("Cannot jump to undefined label '{0}'.", p0);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003CF44 File Offset: 0x0003B144
		internal static string ControlCannotLeaveFinally
		{
			get
			{
				return "Control cannot leave a finally block.";
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0003CF4B File Offset: 0x0003B14B
		internal static string ControlCannotLeaveFilterTest
		{
			get
			{
				return "Control cannot leave a filter test.";
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0003CF52 File Offset: 0x0003B152
		internal static string AmbiguousJump(object p0)
		{
			return SR.Format("Cannot jump to ambiguous label '{0}'.", p0);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0003CF5F File Offset: 0x0003B15F
		internal static string ControlCannotEnterTry
		{
			get
			{
				return "Control cannot enter a try block.";
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0003CF66 File Offset: 0x0003B166
		internal static string ControlCannotEnterExpression
		{
			get
			{
				return "Control cannot enter an expression--only statements can be jumped into.";
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0003CF6D File Offset: 0x0003B16D
		internal static string NonLocalJumpWithValue(object p0)
		{
			return SR.Format("Cannot jump to non-local label '{0}' with a value. Only jumps to labels defined in outer blocks can pass values.", p0);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0003CF7A File Offset: 0x0003B17A
		internal static string CannotCompileConstant(object p0)
		{
			return SR.Format("CompileToMethod cannot compile constant '{0}' because it is a non-trivial value, such as a live object. Instead, create an expression tree that can construct this value.", p0);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0003CF87 File Offset: 0x0003B187
		internal static string CannotCompileDynamic
		{
			get
			{
				return "Dynamic expressions are not supported by CompileToMethod. Instead, create an expression tree that uses System.Runtime.CompilerServices.CallSite.";
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0003CF8E File Offset: 0x0003B18E
		internal static string MethodBuilderDoesNotHaveTypeBuilder
		{
			get
			{
				return "MethodBuilder does not have a valid TypeBuilder";
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0003CF95 File Offset: 0x0003B195
		internal static string InvalidLvalue(object p0)
		{
			return SR.Format("Invalid lvalue for assignment: {0}.", p0);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0003CFA2 File Offset: 0x0003B1A2
		internal static string UndefinedVariable(object p0, object p1, object p2)
		{
			return SR.Format("variable '{0}' of type '{1}' referenced from scope '{2}', but it is not defined", p0, p1, p2);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0003CFB1 File Offset: 0x0003B1B1
		internal static string CannotCloseOverByRef(object p0, object p1)
		{
			return SR.Format("Cannot close over byref parameter '{0}' referenced in lambda '{1}'", p0, p1);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0003CFBF File Offset: 0x0003B1BF
		internal static string UnexpectedVarArgsCall(object p0)
		{
			return SR.Format("Unexpected VarArgs call to method '{0}'", p0);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003CFCC File Offset: 0x0003B1CC
		internal static string RethrowRequiresCatch
		{
			get
			{
				return "Rethrow statement is valid only inside a Catch block.";
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003CFD3 File Offset: 0x0003B1D3
		internal static string TryNotAllowedInFilter
		{
			get
			{
				return "Try expression is not allowed inside a filter body.";
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0003CFDA File Offset: 0x0003B1DA
		internal static string MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return SR.Format("When called from '{0}', rewriting a node of type '{1}' must return a non-null value of the same type. Alternatively, override '{2}' and change it to not visit children of this type.", p0, p1, p2);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0003CFE9 File Offset: 0x0003B1E9
		internal static string MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return SR.Format("Rewriting child expression from type '{0}' to type '{1}' is not allowed, because it would change the meaning of the operation. If this is intentional, override '{2}' and change it to allow this rewrite.", p0, p1, p2);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0003CFF8 File Offset: 0x0003B1F8
		internal static string MustRewriteWithoutMethod(object p0, object p1)
		{
			return SR.Format("Rewritten expression calls operator method '{0}', but the original node had no operator method. If this is intentional, override '{1}' and change it to allow this rewrite.", p0, p1);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0003D006 File Offset: 0x0003B206
		internal static string TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return SR.Format("TryExpression is not supported as an argument to method '{0}' because it has an argument with by-ref type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003D013 File Offset: 0x0003B213
		internal static string TryNotSupportedForValueTypeInstances(object p0)
		{
			return SR.Format("TryExpression is not supported as a child expression when accessing a member on type '{0}' because it is a value type. Construct the tree so the TryExpression is not nested inside of this expression.", p0);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0003D020 File Offset: 0x0003B220
		internal static string TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return SR.Format("Test value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0003D02E File Offset: 0x0003B22E
		internal static string SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return SR.Format("Switch value of type '{0}' cannot be used for the comparison method parameter of type '{1}'", p0, p1);
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0003D03C File Offset: 0x0003B23C
		internal static string NonStaticConstructorRequired
		{
			get
			{
				return "The constructor should not be static";
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0003D043 File Offset: 0x0003B243
		internal static string NonAbstractConstructorRequired
		{
			get
			{
				return "Can't compile a NewExpression with a constructor declared on an abstract class";
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0003D04A File Offset: 0x0003B24A
		internal static string ExpressionMustBeReadable
		{
			get
			{
				return "Expression must be readable";
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003D051 File Offset: 0x0003B251
		internal static string ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1)
		{
			return SR.Format("Expression of type '{0}' cannot be used for constructor parameter of type '{1}'", p0, p1);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0003D05F File Offset: 0x0003B25F
		internal static string EnumerationIsDone
		{
			get
			{
				return "Enumeration has either not started or has already finished.";
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003D066 File Offset: 0x0003B266
		internal static string TypeContainsGenericParameters(object p0)
		{
			return SR.Format("Type {0} contains generic parameters", p0);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0003D073 File Offset: 0x0003B273
		internal static string TypeIsGeneric(object p0)
		{
			return SR.Format("Type {0} is a generic type definition", p0);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0003D080 File Offset: 0x0003B280
		internal static string InvalidArgumentValue
		{
			get
			{
				return "Invalid argument value";
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0003D087 File Offset: 0x0003B287
		internal static string NonEmptyCollectionRequired
		{
			get
			{
				return "Non-empty collection required";
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003D08E File Offset: 0x0003B28E
		internal static string InvalidNullValue(object p0)
		{
			return SR.Format("The value null is not of type '{0}' and cannot be used in this collection.", p0);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003D09B File Offset: 0x0003B29B
		internal static string InvalidObjectType(object p0, object p1)
		{
			return SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this collection.", p0, p1);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003D0A9 File Offset: 0x0003B2A9
		internal static string ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2)
		{
			return SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}' of method '{2}'", p0, p1, p2);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0003D0B8 File Offset: 0x0003B2B8
		internal static string ExpressionTypeDoesNotMatchParameter(object p0, object p1)
		{
			return SR.Format("Expression of type '{0}' cannot be used for parameter of type '{1}'", p0, p1);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003D0C6 File Offset: 0x0003B2C6
		internal static string IncorrectNumberOfMethodCallArguments(object p0)
		{
			return SR.Format("Incorrect number of arguments supplied for call to method '{0}'", p0);
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0003D0D3 File Offset: 0x0003B2D3
		internal static string IncorrectNumberOfLambdaArguments
		{
			get
			{
				return "Incorrect number of arguments supplied for lambda invocation";
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003D0DA File Offset: 0x0003B2DA
		internal static string IncorrectNumberOfConstructorArguments
		{
			get
			{
				return "Incorrect number of arguments for constructor";
			}
		}
	}
}
