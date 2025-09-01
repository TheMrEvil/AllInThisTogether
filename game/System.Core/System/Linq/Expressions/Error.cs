using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000257 RID: 599
	internal static class Error
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x0003838B File Offset: 0x0003658B
		internal static Exception ReducibleMustOverrideReduce()
		{
			return new ArgumentException(Strings.ReducibleMustOverrideReduce);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00038397 File Offset: 0x00036597
		internal static Exception ArgCntMustBeGreaterThanNameCnt()
		{
			return new ArgumentException(Strings.ArgCntMustBeGreaterThanNameCnt);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000383A3 File Offset: 0x000365A3
		internal static Exception InvalidMetaObjectCreated(object p0)
		{
			return new InvalidOperationException(Strings.InvalidMetaObjectCreated(p0));
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000383B0 File Offset: 0x000365B0
		internal static Exception AmbiguousMatchInExpandoObject(object p0)
		{
			return new AmbiguousMatchException(Strings.AmbiguousMatchInExpandoObject(p0));
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000383BD File Offset: 0x000365BD
		internal static Exception SameKeyExistsInExpando(object key)
		{
			return new ArgumentException(Strings.SameKeyExistsInExpando(key), "key");
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000383CF File Offset: 0x000365CF
		internal static Exception KeyDoesNotExistInExpando(object p0)
		{
			return new KeyNotFoundException(Strings.KeyDoesNotExistInExpando(p0));
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000383DC File Offset: 0x000365DC
		internal static Exception CollectionModifiedWhileEnumerating()
		{
			return new InvalidOperationException(Strings.CollectionModifiedWhileEnumerating);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000383E8 File Offset: 0x000365E8
		internal static Exception CollectionReadOnly()
		{
			return new NotSupportedException(Strings.CollectionReadOnly);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000383F4 File Offset: 0x000365F4
		internal static Exception MustReduceToDifferent()
		{
			return new ArgumentException(Strings.MustReduceToDifferent);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00038400 File Offset: 0x00036600
		internal static Exception BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinderNotCompatibleWithCallSite(p0, p1, p2));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0003840F File Offset: 0x0003660F
		internal static Exception DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DynamicBindingNeedsRestrictions(p0, p1));
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0003841D File Offset: 0x0003661D
		internal static Exception DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return new InvalidCastException(Strings.DynamicObjectResultNotAssignable(p0, p1, p2, p3));
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003842D File Offset: 0x0003662D
		internal static Exception DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DynamicBinderResultNotAssignable(p0, p1, p2));
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0003843C File Offset: 0x0003663C
		internal static Exception BindingCannotBeNull()
		{
			return new InvalidOperationException(Strings.BindingCannotBeNull);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00038448 File Offset: 0x00036648
		internal static Exception ReducedNotCompatible()
		{
			return new ArgumentException(Strings.ReducedNotCompatible);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00038454 File Offset: 0x00036654
		internal static Exception SetterHasNoParams(string paramName)
		{
			return new ArgumentException(Strings.SetterHasNoParams, paramName);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00038461 File Offset: 0x00036661
		internal static Exception PropertyCannotHaveRefType(string paramName)
		{
			return new ArgumentException(Strings.PropertyCannotHaveRefType, paramName);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003846E File Offset: 0x0003666E
		internal static Exception IndexesOfSetGetMustMatch(string paramName)
		{
			return new ArgumentException(Strings.IndexesOfSetGetMustMatch, paramName);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003847B File Offset: 0x0003667B
		internal static Exception TypeParameterIsNotDelegate(object p0)
		{
			return new InvalidOperationException(Strings.TypeParameterIsNotDelegate(p0));
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00038488 File Offset: 0x00036688
		internal static Exception FirstArgumentMustBeCallSite()
		{
			return new ArgumentException(Strings.FirstArgumentMustBeCallSite);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00038494 File Offset: 0x00036694
		internal static Exception AccessorsCannotHaveVarArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveVarArgs, paramName);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000384A1 File Offset: 0x000366A1
		private static Exception AccessorsCannotHaveByRefArgs(string paramName)
		{
			return new ArgumentException(Strings.AccessorsCannotHaveByRefArgs, paramName);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000384AE File Offset: 0x000366AE
		internal static Exception AccessorsCannotHaveByRefArgs(string paramName, int index)
		{
			return Error.AccessorsCannotHaveByRefArgs(Error.GetParamName(paramName, index));
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000384BC File Offset: 0x000366BC
		internal static Exception TypeMustBeDerivedFromSystemDelegate()
		{
			return new ArgumentException(Strings.TypeMustBeDerivedFromSystemDelegate);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000384C8 File Offset: 0x000366C8
		internal static Exception NoOrInvalidRuleProduced()
		{
			return new InvalidOperationException(Strings.NoOrInvalidRuleProduced);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000384D4 File Offset: 0x000366D4
		internal static Exception BoundsCannotBeLessThanOne(string paramName)
		{
			return new ArgumentException(Strings.BoundsCannotBeLessThanOne, paramName);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000384E1 File Offset: 0x000366E1
		internal static Exception TypeMustNotBeByRef(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBeByRef, paramName);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000384EE File Offset: 0x000366EE
		internal static Exception TypeMustNotBePointer(string paramName)
		{
			return new ArgumentException(Strings.TypeMustNotBePointer, paramName);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000384FB File Offset: 0x000366FB
		internal static Exception SetterMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.SetterMustBeVoid, paramName);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00038508 File Offset: 0x00036708
		internal static Exception PropertyTypeMustMatchGetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchGetter, paramName);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00038515 File Offset: 0x00036715
		internal static Exception PropertyTypeMustMatchSetter(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeMustMatchSetter, paramName);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00038522 File Offset: 0x00036722
		internal static Exception BothAccessorsMustBeStatic(string paramName)
		{
			return new ArgumentException(Strings.BothAccessorsMustBeStatic, paramName);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003852F File Offset: 0x0003672F
		internal static Exception OnlyStaticFieldsHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticFieldsHaveNullInstance, paramName);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003853C File Offset: 0x0003673C
		internal static Exception OnlyStaticPropertiesHaveNullInstance(string paramName)
		{
			return new ArgumentException(Strings.OnlyStaticPropertiesHaveNullInstance, paramName);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00038549 File Offset: 0x00036749
		internal static Exception OnlyStaticMethodsHaveNullInstance()
		{
			return new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00038555 File Offset: 0x00036755
		internal static Exception PropertyTypeCannotBeVoid(string paramName)
		{
			return new ArgumentException(Strings.PropertyTypeCannotBeVoid, paramName);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00038562 File Offset: 0x00036762
		internal static Exception InvalidUnboxType(string paramName)
		{
			return new ArgumentException(Strings.InvalidUnboxType, paramName);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003856F File Offset: 0x0003676F
		internal static Exception ExpressionMustBeWriteable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeWriteable, paramName);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0003857C File Offset: 0x0003677C
		internal static Exception ArgumentMustNotHaveValueType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustNotHaveValueType, paramName);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00038589 File Offset: 0x00036789
		internal static Exception MustBeReducible()
		{
			return new ArgumentException(Strings.MustBeReducible);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00038595 File Offset: 0x00036795
		internal static Exception AllTestValuesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllTestValuesMustHaveSameType, paramName);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000385A2 File Offset: 0x000367A2
		internal static Exception AllCaseBodiesMustHaveSameType(string paramName)
		{
			return new ArgumentException(Strings.AllCaseBodiesMustHaveSameType, paramName);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000385AF File Offset: 0x000367AF
		internal static Exception DefaultBodyMustBeSupplied(string paramName)
		{
			return new ArgumentException(Strings.DefaultBodyMustBeSupplied, paramName);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000385BC File Offset: 0x000367BC
		internal static Exception LabelMustBeVoidOrHaveExpression(string paramName)
		{
			return new ArgumentException(Strings.LabelMustBeVoidOrHaveExpression, paramName);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000385C9 File Offset: 0x000367C9
		internal static Exception LabelTypeMustBeVoid(string paramName)
		{
			return new ArgumentException(Strings.LabelTypeMustBeVoid, paramName);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000385D6 File Offset: 0x000367D6
		internal static Exception QuotedExpressionMustBeLambda(string paramName)
		{
			return new ArgumentException(Strings.QuotedExpressionMustBeLambda, paramName);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000385E3 File Offset: 0x000367E3
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.VariableMustNotBeByRef(p0, p1), paramName);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000385F2 File Offset: 0x000367F2
		internal static Exception VariableMustNotBeByRef(object p0, object p1, string paramName, int index)
		{
			return Error.VariableMustNotBeByRef(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00038602 File Offset: 0x00036802
		private static Exception DuplicateVariable(object p0, string paramName)
		{
			return new ArgumentException(Strings.DuplicateVariable(p0), paramName);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00038610 File Offset: 0x00036810
		internal static Exception DuplicateVariable(object p0, string paramName, int index)
		{
			return Error.DuplicateVariable(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003861F File Offset: 0x0003681F
		internal static Exception StartEndMustBeOrdered()
		{
			return new ArgumentException(Strings.StartEndMustBeOrdered);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003862B File Offset: 0x0003682B
		internal static Exception FaultCannotHaveCatchOrFinally(string paramName)
		{
			return new ArgumentException(Strings.FaultCannotHaveCatchOrFinally, paramName);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00038638 File Offset: 0x00036838
		internal static Exception TryMustHaveCatchFinallyOrFault()
		{
			return new ArgumentException(Strings.TryMustHaveCatchFinallyOrFault);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00038644 File Offset: 0x00036844
		internal static Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry()
		{
			return new ArgumentException(Strings.BodyOfCatchMustHaveSameTypeAsBodyOfTry);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00038650 File Offset: 0x00036850
		internal static Exception ExtensionNodeMustOverrideProperty(object p0)
		{
			return new InvalidOperationException(Strings.ExtensionNodeMustOverrideProperty(p0));
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003865D File Offset: 0x0003685D
		internal static Exception UserDefinedOperatorMustBeStatic(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustBeStatic(p0), paramName);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003866B File Offset: 0x0003686B
		internal static Exception UserDefinedOperatorMustNotBeVoid(object p0, string paramName)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustNotBeVoid(p0), paramName);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00038679 File Offset: 0x00036879
		internal static Exception CoercionOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CoercionOperatorNotDefined(p0, p1));
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00038687 File Offset: 0x00036887
		internal static Exception UnaryOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnaryOperatorNotDefined(p0, p1));
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00038695 File Offset: 0x00036895
		internal static Exception BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinaryOperatorNotDefined(p0, p1, p2));
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000386A4 File Offset: 0x000368A4
		internal static Exception ReferenceEqualityNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ReferenceEqualityNotDefined(p0, p1));
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000386B2 File Offset: 0x000368B2
		internal static Exception OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OperandTypesDoNotMatchParameters(p0, p1));
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000386C0 File Offset: 0x000368C0
		internal static Exception OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OverloadOperatorTypeDoesNotMatchConversionType(p0, p1));
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000386CE File Offset: 0x000368CE
		internal static Exception ConversionIsNotSupportedForArithmeticTypes()
		{
			return new InvalidOperationException(Strings.ConversionIsNotSupportedForArithmeticTypes);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000386DA File Offset: 0x000368DA
		internal static Exception ArgumentTypeCannotBeVoid()
		{
			return new ArgumentException(Strings.ArgumentTypeCannotBeVoid);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000386E6 File Offset: 0x000368E6
		internal static Exception ArgumentMustBeArray(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArray, paramName);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000386F3 File Offset: 0x000368F3
		internal static Exception ArgumentMustBeBoolean(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeBoolean, paramName);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00038700 File Offset: 0x00036900
		internal static Exception EqualityMustReturnBoolean(object p0, string paramName)
		{
			return new ArgumentException(Strings.EqualityMustReturnBoolean(p0), paramName);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003870E File Offset: 0x0003690E
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfo(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfo, paramName);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003871B File Offset: 0x0003691B
		private static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod, paramName);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00038728 File Offset: 0x00036928
		internal static Exception ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(string paramName, int index)
		{
			return Error.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(Error.GetParamName(paramName, index));
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00038736 File Offset: 0x00036936
		private static Exception ArgumentMustBeInstanceMember(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInstanceMember, paramName);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00038743 File Offset: 0x00036943
		internal static Exception ArgumentMustBeInstanceMember(string paramName, int index)
		{
			return Error.ArgumentMustBeInstanceMember(Error.GetParamName(paramName, index));
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00038751 File Offset: 0x00036951
		private static Exception ArgumentMustBeInteger(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeInteger, paramName);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003875E File Offset: 0x0003695E
		internal static Exception ArgumentMustBeInteger(string paramName, int index)
		{
			return Error.ArgumentMustBeInteger(Error.GetParamName(paramName, index));
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003876C File Offset: 0x0003696C
		internal static Exception ArgumentMustBeArrayIndexType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeArrayIndexType, paramName);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00038779 File Offset: 0x00036979
		internal static Exception ArgumentMustBeArrayIndexType(string paramName, int index)
		{
			return Error.ArgumentMustBeArrayIndexType(Error.GetParamName(paramName, index));
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00038787 File Offset: 0x00036987
		internal static Exception ArgumentMustBeSingleDimensionalArrayType(string paramName)
		{
			return new ArgumentException(Strings.ArgumentMustBeSingleDimensionalArrayType, paramName);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00038794 File Offset: 0x00036994
		internal static Exception ArgumentTypesMustMatch()
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000387A0 File Offset: 0x000369A0
		internal static Exception ArgumentTypesMustMatch(string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch, paramName);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000387AD File Offset: 0x000369AD
		internal static Exception CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeElementThroughProperty(p0));
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000387BA File Offset: 0x000369BA
		internal static Exception CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeMemberThroughProperty(p0));
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000387C7 File Offset: 0x000369C7
		internal static Exception IncorrectTypeForTypeAs(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectTypeForTypeAs(p0), paramName);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000387D5 File Offset: 0x000369D5
		internal static Exception CoalesceUsedOnNonNullType()
		{
			return new InvalidOperationException(Strings.CoalesceUsedOnNonNullType);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000387E1 File Offset: 0x000369E1
		internal static Exception ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ExpressionTypeCannotInitializeArrayType(p0, p1));
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000387EF File Offset: 0x000369EF
		private static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentTypeDoesNotMatchMember(p0, p1), paramName);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000387FE File Offset: 0x000369FE
		internal static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentTypeDoesNotMatchMember(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003880E File Offset: 0x00036A0E
		private static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ArgumentMemberNotDeclOnType(p0, p1), paramName);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003881D File Offset: 0x00036A1D
		internal static Exception ArgumentMemberNotDeclOnType(object p0, object p1, string paramName, int index)
		{
			return Error.ArgumentMemberNotDeclOnType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003882D File Offset: 0x00036A2D
		internal static Exception ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchReturn(p0, p1));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003883B File Offset: 0x00036A3B
		internal static Exception ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchAssignment(p0, p1));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00038849 File Offset: 0x00036A49
		internal static Exception ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchLabel(p0, p1));
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00038857 File Offset: 0x00036A57
		internal static Exception ExpressionTypeNotInvocable(object p0, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeNotInvocable(p0), paramName);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00038865 File Offset: 0x00036A65
		internal static Exception FieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.FieldNotDefinedForType(p0, p1));
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00038873 File Offset: 0x00036A73
		internal static Exception InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstanceFieldNotDefinedForType(p0, p1));
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00038881 File Offset: 0x00036A81
		internal static Exception FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.FieldInfoNotDefinedForType(p0, p1, p2));
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00038890 File Offset: 0x00036A90
		internal static Exception IncorrectNumberOfIndexes()
		{
			return new ArgumentException(Strings.IncorrectNumberOfIndexes);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003889C File Offset: 0x00036A9C
		internal static Exception IncorrectNumberOfLambdaDeclarationParameters()
		{
			return new ArgumentException(Strings.IncorrectNumberOfLambdaDeclarationParameters);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000388A8 File Offset: 0x00036AA8
		internal static Exception IncorrectNumberOfMembersForGivenConstructor()
		{
			return new ArgumentException(Strings.IncorrectNumberOfMembersForGivenConstructor);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000388B4 File Offset: 0x00036AB4
		internal static Exception IncorrectNumberOfArgumentsForMembers()
		{
			return new ArgumentException(Strings.IncorrectNumberOfArgumentsForMembers);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000388C0 File Offset: 0x00036AC0
		internal static Exception LambdaTypeMustBeDerivedFromSystemDelegate(string paramName)
		{
			return new ArgumentException(Strings.LambdaTypeMustBeDerivedFromSystemDelegate, paramName);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000388CD File Offset: 0x00036ACD
		internal static Exception MemberNotFieldOrProperty(object p0, string paramName)
		{
			return new ArgumentException(Strings.MemberNotFieldOrProperty(p0), paramName);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000388DB File Offset: 0x00036ADB
		internal static Exception MethodContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodContainsGenericParameters(p0), paramName);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000388E9 File Offset: 0x00036AE9
		internal static Exception MethodIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.MethodIsGeneric(p0), paramName);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000388F7 File Offset: 0x00036AF7
		private static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.MethodNotPropertyAccessor(p0, p1), paramName);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00038906 File Offset: 0x00036B06
		internal static Exception MethodNotPropertyAccessor(object p0, object p1, string paramName, int index)
		{
			return Error.MethodNotPropertyAccessor(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00038916 File Offset: 0x00036B16
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveGetter(p0), paramName);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00038924 File Offset: 0x00036B24
		internal static Exception PropertyDoesNotHaveGetter(object p0, string paramName, int index)
		{
			return Error.PropertyDoesNotHaveGetter(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00038933 File Offset: 0x00036B33
		internal static Exception PropertyDoesNotHaveSetter(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveSetter(p0), paramName);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00038941 File Offset: 0x00036B41
		internal static Exception PropertyDoesNotHaveAccessor(object p0, string paramName)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveAccessor(p0), paramName);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003894F File Offset: 0x00036B4F
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfType(p0, p1), paramName);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003895E File Offset: 0x00036B5E
		internal static Exception NotAMemberOfType(object p0, object p1, string paramName, int index)
		{
			return Error.NotAMemberOfType(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003896E File Offset: 0x00036B6E
		internal static Exception NotAMemberOfAnyType(object p0, string paramName)
		{
			return new ArgumentException(Strings.NotAMemberOfAnyType(p0), paramName);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003897C File Offset: 0x00036B7C
		internal static Exception ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return new ArgumentException(Strings.ParameterExpressionNotValidAsDelegate(p0, p1));
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003898A File Offset: 0x00036B8A
		internal static Exception PropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.PropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00038999 File Offset: 0x00036B99
		internal static Exception InstancePropertyNotDefinedForType(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.InstancePropertyNotDefinedForType(p0, p1), paramName);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000389A8 File Offset: 0x00036BA8
		internal static Exception InstancePropertyWithoutParameterNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstancePropertyWithoutParameterNotDefinedForType(p0, p1));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000389B6 File Offset: 0x00036BB6
		internal static Exception InstancePropertyWithSpecifiedParametersNotDefinedForType(object p0, object p1, object p2, string paramName)
		{
			return new ArgumentException(Strings.InstancePropertyWithSpecifiedParametersNotDefinedForType(p0, p1, p2), paramName);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000389C6 File Offset: 0x00036BC6
		internal static Exception InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.InstanceAndMethodTypeMismatch(p0, p1, p2));
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000389D5 File Offset: 0x00036BD5
		internal static Exception TypeMissingDefaultConstructor(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeMissingDefaultConstructor(p0), paramName);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000389E3 File Offset: 0x00036BE3
		internal static Exception ElementInitializerMethodNotAdd(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNotAdd, paramName);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000389F0 File Offset: 0x00036BF0
		internal static Exception ElementInitializerMethodNoRefOutParam(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNoRefOutParam(p0, p1), paramName);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000389FF File Offset: 0x00036BFF
		internal static Exception ElementInitializerMethodWithZeroArgs(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodWithZeroArgs, paramName);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00038A0C File Offset: 0x00036C0C
		internal static Exception ElementInitializerMethodStatic(string paramName)
		{
			return new ArgumentException(Strings.ElementInitializerMethodStatic, paramName);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00038A19 File Offset: 0x00036C19
		internal static Exception TypeNotIEnumerable(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeNotIEnumerable(p0), paramName);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00038A27 File Offset: 0x00036C27
		internal static Exception UnhandledBinary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledBinary(p0), paramName);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00038A35 File Offset: 0x00036C35
		internal static Exception UnhandledBinding()
		{
			return new ArgumentException(Strings.UnhandledBinding);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00038A41 File Offset: 0x00036C41
		internal static Exception UnhandledBindingType(object p0)
		{
			return new ArgumentException(Strings.UnhandledBindingType(p0));
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00038A4E File Offset: 0x00036C4E
		internal static Exception UnhandledUnary(object p0, string paramName)
		{
			return new ArgumentException(Strings.UnhandledUnary(p0), paramName);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00038A5C File Offset: 0x00036C5C
		internal static Exception UnknownBindingType(int index)
		{
			return new ArgumentException(Strings.UnknownBindingType, string.Format("bindings[{0}]", index));
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00038A78 File Offset: 0x00036C78
		internal static Exception UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveConsistentTypes(p0, p1));
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00038A86 File Offset: 0x00036C86
		internal static Exception UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveValidReturnType(p0, p1));
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00038A94 File Offset: 0x00036C94
		internal static Exception LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return new ArgumentException(Strings.LogicalOperatorMustHaveBooleanOperators(p0, p1));
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00038AA2 File Offset: 0x00036CA2
		internal static Exception MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00038AB0 File Offset: 0x00036CB0
		internal static Exception GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.GenericMethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00038ABE File Offset: 0x00036CBE
		internal static Exception MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00038ACC File Offset: 0x00036CCC
		internal static Exception PropertyWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.PropertyWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00038ADA File Offset: 0x00036CDA
		internal static Exception IncorrectNumberOfTypeArgsForFunc(string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForFunc, paramName);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00038AE7 File Offset: 0x00036CE7
		internal static Exception IncorrectNumberOfTypeArgsForAction(string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForAction, paramName);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00038AF4 File Offset: 0x00036CF4
		internal static Exception ArgumentCannotBeOfTypeVoid(string paramName)
		{
			return new ArgumentException(Strings.ArgumentCannotBeOfTypeVoid, paramName);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00038B01 File Offset: 0x00036D01
		internal static Exception OutOfRange(string paramName, object p1)
		{
			return new ArgumentOutOfRangeException(paramName, Strings.OutOfRange(paramName, p1));
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00038B10 File Offset: 0x00036D10
		internal static Exception LabelTargetAlreadyDefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetAlreadyDefined(p0));
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00038B1D File Offset: 0x00036D1D
		internal static Exception LabelTargetUndefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetUndefined(p0));
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00038B2A File Offset: 0x00036D2A
		internal static Exception ControlCannotLeaveFinally()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFinally);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00038B36 File Offset: 0x00036D36
		internal static Exception ControlCannotLeaveFilterTest()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFilterTest);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00038B42 File Offset: 0x00036D42
		internal static Exception AmbiguousJump(object p0)
		{
			return new InvalidOperationException(Strings.AmbiguousJump(p0));
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00038B4F File Offset: 0x00036D4F
		internal static Exception ControlCannotEnterTry()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterTry);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00038B5B File Offset: 0x00036D5B
		internal static Exception ControlCannotEnterExpression()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterExpression);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00038B67 File Offset: 0x00036D67
		internal static Exception NonLocalJumpWithValue(object p0)
		{
			return new InvalidOperationException(Strings.NonLocalJumpWithValue(p0));
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00038B74 File Offset: 0x00036D74
		internal static Exception CannotCompileConstant(object p0)
		{
			return new InvalidOperationException(Strings.CannotCompileConstant(p0));
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00038B81 File Offset: 0x00036D81
		internal static Exception CannotCompileDynamic()
		{
			return new NotSupportedException(Strings.CannotCompileDynamic);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00038B8D File Offset: 0x00036D8D
		internal static Exception MethodBuilderDoesNotHaveTypeBuilder()
		{
			return new ArgumentException(Strings.MethodBuilderDoesNotHaveTypeBuilder);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00038B99 File Offset: 0x00036D99
		internal static Exception InvalidLvalue(ExpressionType p0)
		{
			return new InvalidOperationException(Strings.InvalidLvalue(p0));
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00038BAB File Offset: 0x00036DAB
		internal static Exception UndefinedVariable(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.UndefinedVariable(p0, p1, p2));
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00038BBA File Offset: 0x00036DBA
		internal static Exception CannotCloseOverByRef(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CannotCloseOverByRef(p0, p1));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00038BC8 File Offset: 0x00036DC8
		internal static Exception UnexpectedVarArgsCall(object p0)
		{
			return new InvalidOperationException(Strings.UnexpectedVarArgsCall(p0));
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00038BD5 File Offset: 0x00036DD5
		internal static Exception RethrowRequiresCatch()
		{
			return new InvalidOperationException(Strings.RethrowRequiresCatch);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00038BE1 File Offset: 0x00036DE1
		internal static Exception TryNotAllowedInFilter()
		{
			return new InvalidOperationException(Strings.TryNotAllowedInFilter);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00038BED File Offset: 0x00036DED
		internal static Exception MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteToSameNode(p0, p1, p2));
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00038BFC File Offset: 0x00036DFC
		internal static Exception MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteChildToSameType(p0, p1, p2));
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00038C0B File Offset: 0x00036E0B
		internal static Exception MustRewriteWithoutMethod(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MustRewriteWithoutMethod(p0, p1));
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00038C19 File Offset: 0x00036E19
		internal static Exception TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForMethodsWithRefArgs(p0));
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00038C26 File Offset: 0x00036E26
		internal static Exception TryNotSupportedForValueTypeInstances(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForValueTypeInstances(p0));
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00038C33 File Offset: 0x00036E33
		internal static Exception TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.TestValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00038C41 File Offset: 0x00036E41
		internal static Exception SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.SwitchValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0000CC09 File Offset: 0x0000AE09
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0000CC41 File Offset: 0x0000AE41
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00038C4F File Offset: 0x00036E4F
		internal static Exception NonStaticConstructorRequired(string paramName)
		{
			return new ArgumentException(Strings.NonStaticConstructorRequired, paramName);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00038C5C File Offset: 0x00036E5C
		internal static Exception NonAbstractConstructorRequired()
		{
			return new InvalidOperationException(Strings.NonAbstractConstructorRequired);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00038C68 File Offset: 0x00036E68
		internal static Exception InvalidProgram()
		{
			return new InvalidProgramException();
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00038C6F File Offset: 0x00036E6F
		internal static Exception EnumerationIsDone()
		{
			return new InvalidOperationException(Strings.EnumerationIsDone);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00038C7B File Offset: 0x00036E7B
		private static Exception TypeContainsGenericParameters(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeContainsGenericParameters(p0), paramName);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00038C89 File Offset: 0x00036E89
		internal static Exception TypeContainsGenericParameters(object p0, string paramName, int index)
		{
			return Error.TypeContainsGenericParameters(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00038C98 File Offset: 0x00036E98
		internal static Exception TypeIsGeneric(object p0, string paramName)
		{
			return new ArgumentException(Strings.TypeIsGeneric(p0), paramName);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00038CA6 File Offset: 0x00036EA6
		internal static Exception TypeIsGeneric(object p0, string paramName, int index)
		{
			return Error.TypeIsGeneric(p0, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00038CB5 File Offset: 0x00036EB5
		internal static Exception IncorrectNumberOfConstructorArguments()
		{
			return new ArgumentException(Strings.IncorrectNumberOfConstructorArguments);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00038CC1 File Offset: 0x00036EC1
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2), paramName);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00038CD1 File Offset: 0x00036ED1
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00038CE3 File Offset: 0x00036EE3
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchParameter(p0, p1), paramName);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00038CF2 File Offset: 0x00036EF2
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00038D02 File Offset: 0x00036F02
		internal static Exception IncorrectNumberOfLambdaArguments()
		{
			return new InvalidOperationException(Strings.IncorrectNumberOfLambdaArguments);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00038D0E File Offset: 0x00036F0E
		internal static Exception IncorrectNumberOfMethodCallArguments(object p0, string paramName)
		{
			return new ArgumentException(Strings.IncorrectNumberOfMethodCallArguments(p0), paramName);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00038D1C File Offset: 0x00036F1C
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1), paramName);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00038D2B File Offset: 0x00036F2B
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1, string paramName, int index)
		{
			return Error.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1, Error.GetParamName(paramName, index));
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00038D3B File Offset: 0x00036F3B
		internal static Exception ExpressionMustBeReadable(string paramName)
		{
			return new ArgumentException(Strings.ExpressionMustBeReadable, paramName);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00038D48 File Offset: 0x00036F48
		internal static Exception ExpressionMustBeReadable(string paramName, int index)
		{
			return Error.ExpressionMustBeReadable(Error.GetParamName(paramName, index));
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00038D56 File Offset: 0x00036F56
		internal static Exception InvalidArgumentValue(string paramName)
		{
			return new ArgumentException(Strings.InvalidArgumentValue, paramName);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00038D63 File Offset: 0x00036F63
		internal static Exception NonEmptyCollectionRequired(string paramName)
		{
			return new ArgumentException(Strings.NonEmptyCollectionRequired, paramName);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00038D70 File Offset: 0x00036F70
		internal static Exception InvalidNullValue(Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidNullValue(type), paramName);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00038D7E File Offset: 0x00036F7E
		internal static Exception InvalidTypeException(object value, Type type, string paramName)
		{
			return new ArgumentException(Strings.InvalidObjectType(((value != null) ? value.GetType() : null) ?? "null", type), paramName);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00038DA1 File Offset: 0x00036FA1
		private static string GetParamName(string paramName, int index)
		{
			if (index >= 0)
			{
				return string.Format("{0}[{1}]", paramName, index);
			}
			return paramName;
		}
	}
}
