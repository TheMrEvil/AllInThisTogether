using System;

namespace Unity.Burst
{
	// Token: 0x02000012 RID: 18
	internal enum DiagnosticId
	{
		// Token: 0x040000C5 RID: 197
		ERR_InternalCompilerErrorInBackend = 100,
		// Token: 0x040000C6 RID: 198
		ERR_InternalCompilerErrorInFunction,
		// Token: 0x040000C7 RID: 199
		ERR_InternalCompilerErrorInInstruction,
		// Token: 0x040000C8 RID: 200
		ERR_OnlyStaticMethodsAllowed = 1000,
		// Token: 0x040000C9 RID: 201
		ERR_UnableToAccessManagedMethod,
		// Token: 0x040000CA RID: 202
		ERR_UnableToFindInterfaceMethod,
		// Token: 0x040000CB RID: 203
		ERR_UnexpectedEmptyMethodBody,
		// Token: 0x040000CC RID: 204
		ERR_ManagedArgumentsNotSupported,
		// Token: 0x040000CD RID: 205
		ERR_CatchConstructionNotSupported = 1006,
		// Token: 0x040000CE RID: 206
		ERR_CatchAndFilterConstructionNotSupported,
		// Token: 0x040000CF RID: 207
		ERR_LdfldaWithFixedArrayExpected,
		// Token: 0x040000D0 RID: 208
		ERR_PointerExpected,
		// Token: 0x040000D1 RID: 209
		ERR_LoadingFieldFromManagedObjectNotSupported,
		// Token: 0x040000D2 RID: 210
		ERR_LoadingFieldWithManagedTypeNotSupported,
		// Token: 0x040000D3 RID: 211
		ERR_LoadingArgumentWithManagedTypeNotSupported,
		// Token: 0x040000D4 RID: 212
		ERR_CallingBurstDiscardMethodWithReturnValueNotSupported = 1015,
		// Token: 0x040000D5 RID: 213
		ERR_CallingManagedMethodNotSupported,
		// Token: 0x040000D6 RID: 214
		ERR_InstructionUnboxNotSupported = 1019,
		// Token: 0x040000D7 RID: 215
		ERR_InstructionBoxNotSupported,
		// Token: 0x040000D8 RID: 216
		ERR_InstructionNewobjWithManagedTypeNotSupported,
		// Token: 0x040000D9 RID: 217
		ERR_AccessingManagedArrayNotSupported,
		// Token: 0x040000DA RID: 218
		ERR_InstructionLdtokenFieldNotSupported,
		// Token: 0x040000DB RID: 219
		ERR_InstructionLdtokenMethodNotSupported,
		// Token: 0x040000DC RID: 220
		ERR_InstructionLdtokenTypeNotSupported,
		// Token: 0x040000DD RID: 221
		ERR_InstructionLdtokenNotSupported,
		// Token: 0x040000DE RID: 222
		ERR_InstructionLdvirtftnNotSupported,
		// Token: 0x040000DF RID: 223
		ERR_InstructionNewarrNotSupported,
		// Token: 0x040000E0 RID: 224
		ERR_InstructionRethrowNotSupported,
		// Token: 0x040000E1 RID: 225
		ERR_InstructionCastclassNotSupported,
		// Token: 0x040000E2 RID: 226
		ERR_InstructionLdftnNotSupported = 1032,
		// Token: 0x040000E3 RID: 227
		ERR_InstructionLdstrNotSupported,
		// Token: 0x040000E4 RID: 228
		ERR_InstructionStsfldNotSupported,
		// Token: 0x040000E5 RID: 229
		ERR_InstructionEndfilterNotSupported,
		// Token: 0x040000E6 RID: 230
		ERR_InstructionEndfinallyNotSupported,
		// Token: 0x040000E7 RID: 231
		ERR_InstructionLeaveNotSupported,
		// Token: 0x040000E8 RID: 232
		ERR_InstructionNotSupported,
		// Token: 0x040000E9 RID: 233
		ERR_LoadingFromStaticFieldNotSupported,
		// Token: 0x040000EA RID: 234
		ERR_LoadingFromNonReadonlyStaticFieldNotSupported,
		// Token: 0x040000EB RID: 235
		ERR_LoadingFromManagedStaticFieldNotSupported,
		// Token: 0x040000EC RID: 236
		ERR_LoadingFromManagedNonReadonlyStaticFieldNotSupported,
		// Token: 0x040000ED RID: 237
		ERR_InstructionStfldToManagedObjectNotSupported,
		// Token: 0x040000EE RID: 238
		ERR_InstructionLdlenNonConstantLengthNotSupported,
		// Token: 0x040000EF RID: 239
		ERR_StructWithAutoLayoutNotSupported,
		// Token: 0x040000F0 RID: 240
		ERR_StructWithGenericParametersAndExplicitLayoutNotSupported = 1047,
		// Token: 0x040000F1 RID: 241
		ERR_StructSizeNotSupported,
		// Token: 0x040000F2 RID: 242
		ERR_StructZeroSizeNotSupported,
		// Token: 0x040000F3 RID: 243
		ERR_MarshalAsOnFieldNotSupported,
		// Token: 0x040000F4 RID: 244
		ERR_TypeNotSupported,
		// Token: 0x040000F5 RID: 245
		ERR_RequiredTypeModifierNotSupported,
		// Token: 0x040000F6 RID: 246
		ERR_ErrorWhileProcessingVariable,
		// Token: 0x040000F7 RID: 247
		ERR_UnableToResolveType,
		// Token: 0x040000F8 RID: 248
		ERR_UnableToResolveMethod,
		// Token: 0x040000F9 RID: 249
		ERR_ConstructorNotSupported,
		// Token: 0x040000FA RID: 250
		ERR_FunctionPointerMethodMissingBurstCompileAttribute,
		// Token: 0x040000FB RID: 251
		ERR_FunctionPointerTypeMissingBurstCompileAttribute,
		// Token: 0x040000FC RID: 252
		ERR_FunctionPointerMethodAndTypeMissingBurstCompileAttribute,
		// Token: 0x040000FD RID: 253
		INF_FunctionPointerMethodAndTypeMissingMonoPInvokeCallbackAttribute = 10590,
		// Token: 0x040000FE RID: 254
		ERR_MarshalAsOnParameterNotSupported = 1061,
		// Token: 0x040000FF RID: 255
		ERR_MarshalAsOnReturnTypeNotSupported,
		// Token: 0x04000100 RID: 256
		ERR_TypeNotBlittableForFunctionPointer,
		// Token: 0x04000101 RID: 257
		ERR_StructByValueNotSupported,
		// Token: 0x04000102 RID: 258
		ERR_StructsWithNonUnicodeCharsNotSupported = 1066,
		// Token: 0x04000103 RID: 259
		ERR_VectorsByValueNotSupported,
		// Token: 0x04000104 RID: 260
		ERR_MissingExternBindings,
		// Token: 0x04000105 RID: 261
		ERR_MarshalAsNativeTypeOnReturnTypeNotSupported,
		// Token: 0x04000106 RID: 262
		ERR_AssertTypeNotSupported = 1071,
		// Token: 0x04000107 RID: 263
		ERR_StoringToReadOnlyFieldNotAllowed,
		// Token: 0x04000108 RID: 264
		ERR_StoringToFieldInReadOnlyParameterNotAllowed,
		// Token: 0x04000109 RID: 265
		ERR_StoringToReadOnlyParameterNotAllowed,
		// Token: 0x0400010A RID: 266
		ERR_TypeManagerStaticFieldNotCompatible,
		// Token: 0x0400010B RID: 267
		ERR_UnableToFindTypeIndexForTypeManagerType,
		// Token: 0x0400010C RID: 268
		ERR_UnableToFindFieldForTypeManager,
		// Token: 0x0400010D RID: 269
		ERR_CircularStaticConstructorUsage = 1090,
		// Token: 0x0400010E RID: 270
		ERR_ExternalInternalCallsInStaticConstructorsNotSupported,
		// Token: 0x0400010F RID: 271
		ERR_PlatformNotSupported,
		// Token: 0x04000110 RID: 272
		ERR_InitModuleVerificationError,
		// Token: 0x04000111 RID: 273
		ERR_ModuleVerificationError,
		// Token: 0x04000112 RID: 274
		ERR_UnableToFindTypeRequiredForTypeManager,
		// Token: 0x04000113 RID: 275
		ERR_UnexpectedIntegerTypesForBinaryOperation,
		// Token: 0x04000114 RID: 276
		ERR_BinaryOperationNotSupported,
		// Token: 0x04000115 RID: 277
		ERR_CalliWithThisNotSupported,
		// Token: 0x04000116 RID: 278
		ERR_CalliNonCCallingConventionNotSupported,
		// Token: 0x04000117 RID: 279
		ERR_StringLiteralTooBig,
		// Token: 0x04000118 RID: 280
		ERR_LdftnNonCCallingConventionNotSupported,
		// Token: 0x04000119 RID: 281
		ERR_UnableToCallMethodOnInterfaceObject,
		// Token: 0x0400011A RID: 282
		ERR_UnsupportedCpuDependentBranch = 1199,
		// Token: 0x0400011B RID: 283
		ERR_InstructionTargetCpuFeatureNotAllowedInThisBlock,
		// Token: 0x0400011C RID: 284
		ERR_AssumeRangeTypeMustBeInteger,
		// Token: 0x0400011D RID: 285
		ERR_AssumeRangeTypeMustBeSameSign,
		// Token: 0x0400011E RID: 286
		ERR_UnsupportedSpillTransform = 1300,
		// Token: 0x0400011F RID: 287
		ERR_UnsupportedSpillTransformTooManyUsers,
		// Token: 0x04000120 RID: 288
		ERR_MethodNotSupported,
		// Token: 0x04000121 RID: 289
		ERR_VectorsLoadFieldIsAddress,
		// Token: 0x04000122 RID: 290
		ERR_ConstantExpressionRequired,
		// Token: 0x04000123 RID: 291
		ERR_PointerArgumentsUnexpectedAliasing = 1310,
		// Token: 0x04000124 RID: 292
		ERR_LoopIntrinsicMustBeCalledInsideLoop = 1320,
		// Token: 0x04000125 RID: 293
		ERR_LoopUnexpectedAutoVectorization,
		// Token: 0x04000126 RID: 294
		WRN_LoopIntrinsicCalledButLoopOptimizedAway,
		// Token: 0x04000127 RID: 295
		ERR_AssertArgTypesDiffer = 1330,
		// Token: 0x04000128 RID: 296
		ERR_StringInternalCompilerFixedStringTooManyUsers = 1340,
		// Token: 0x04000129 RID: 297
		ERR_StringInvalidFormatMissingClosingBrace,
		// Token: 0x0400012A RID: 298
		ERR_StringInvalidIntegerForArgumentIndex,
		// Token: 0x0400012B RID: 299
		ERR_StringInvalidFormatForArgument,
		// Token: 0x0400012C RID: 300
		ERR_StringArgumentIndexOutOfRange,
		// Token: 0x0400012D RID: 301
		ERR_StringInvalidArgumentType,
		// Token: 0x0400012E RID: 302
		ERR_DebugLogNotSupported,
		// Token: 0x0400012F RID: 303
		ERR_StringInvalidNonLiteralFormat,
		// Token: 0x04000130 RID: 304
		ERR_StringInvalidStringFormatMethod,
		// Token: 0x04000131 RID: 305
		ERR_StringInvalidArgument,
		// Token: 0x04000132 RID: 306
		ERR_StringArrayInvalidArrayCreation,
		// Token: 0x04000133 RID: 307
		ERR_StringArrayInvalidArraySize,
		// Token: 0x04000134 RID: 308
		ERR_StringArrayInvalidControlFlow,
		// Token: 0x04000135 RID: 309
		ERR_StringArrayInvalidArrayIndex,
		// Token: 0x04000136 RID: 310
		ERR_StringArrayInvalidArrayIndexOutOfRange,
		// Token: 0x04000137 RID: 311
		ERR_UnmanagedStringMethodMissing,
		// Token: 0x04000138 RID: 312
		ERR_UnmanagedStringMethodInvalid,
		// Token: 0x04000139 RID: 313
		ERR_ManagedStaticConstructor = 1360,
		// Token: 0x0400013A RID: 314
		ERR_StaticConstantArrayInStaticConstructor,
		// Token: 0x0400013B RID: 315
		WRN_ExceptionThrownInNonSafetyCheckGuardedFunction = 1370,
		// Token: 0x0400013C RID: 316
		WRN_ACallToMethodHasBeenDiscarded,
		// Token: 0x0400013D RID: 317
		ERR_AccessingNestedManagedArrayNotSupported = 1380,
		// Token: 0x0400013E RID: 318
		ERR_LdobjFromANonPointerNonReference,
		// Token: 0x0400013F RID: 319
		ERR_StringLiteralRequired,
		// Token: 0x04000140 RID: 320
		ERR_MultiDimensionalArrayUnsupported,
		// Token: 0x04000141 RID: 321
		ERR_NonBlittableAndNonManagedSequentialStructNotSupported,
		// Token: 0x04000142 RID: 322
		ERR_VarArgFunctionNotSupported
	}
}
