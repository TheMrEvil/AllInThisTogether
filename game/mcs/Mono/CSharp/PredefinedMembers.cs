using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002D2 RID: 722
	public class PredefinedMembers
	{
		// Token: 0x0600226B RID: 8811 RVA: 0x000A90C4 File Offset: 0x000A72C4
		public PredefinedMembers(ModuleContainer module)
		{
			PredefinedTypes types = module.PredefinedTypes;
			PredefinedAttributes predefinedAttributes = module.PredefinedAttributes;
			BuiltinTypes builtinTypes = module.Compiler.BuiltinTypes;
			TypeParameter definition = new TypeParameter(0, new MemberName("T"), null, null, Variance.None);
			this.ActivatorCreateInstance = new PredefinedMember<MethodSpec>(module, types.Activator, MemberFilter.Method("CreateInstance", 1, ParametersCompiled.EmptyReadOnlyParameters, null));
			this.AsyncTaskMethodBuilderCreate = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("Create", 0, ParametersCompiled.EmptyReadOnlyParameters, types.AsyncTaskMethodBuilder.TypeSpec));
			this.AsyncTaskMethodBuilderSetResult = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("SetResult", 0, ParametersCompiled.EmptyReadOnlyParameters, builtinTypes.Void));
			this.AsyncTaskMethodBuilderSetStateMachine = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, "SetStateMachine", MemberKind.Method, () => new TypeSpec[]
			{
				types.IAsyncStateMachine.TypeSpec
			}, builtinTypes.Void);
			this.AsyncTaskMethodBuilderSetException = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("SetException", 0, ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Exception
			}), builtinTypes.Void));
			this.AsyncTaskMethodBuilderOnCompleted = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("AwaitOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderOnCompletedUnsafe = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("AwaitUnsafeOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderStart = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Method("Start", 1, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderTask = new PredefinedMember<PropertySpec>(module, types.AsyncTaskMethodBuilder, MemberFilter.Property("Task", null));
			this.AsyncTaskMethodBuilderGenericCreate = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Method("Create", 0, ParametersCompiled.EmptyReadOnlyParameters, types.AsyncVoidMethodBuilder.TypeSpec));
			this.AsyncTaskMethodBuilderGenericSetResult = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, "SetResult", MemberKind.Method, () => new TypeSpec[]
			{
				types.AsyncTaskMethodBuilderGeneric.TypeSpec.MemberDefinition.TypeParameters[0]
			}, builtinTypes.Void);
			this.AsyncTaskMethodBuilderGenericSetStateMachine = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, "SetStateMachine", MemberKind.Method, () => new TypeSpec[]
			{
				types.IAsyncStateMachine.TypeSpec
			}, builtinTypes.Void);
			this.AsyncTaskMethodBuilderGenericSetException = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Method("SetException", 0, ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Exception
			}), builtinTypes.Void));
			this.AsyncTaskMethodBuilderGenericOnCompleted = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Method("AwaitOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderGenericOnCompletedUnsafe = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Method("AwaitUnsafeOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderGenericStart = new PredefinedMember<MethodSpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Method("Start", 1, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncTaskMethodBuilderGenericTask = new PredefinedMember<PropertySpec>(module, types.AsyncTaskMethodBuilderGeneric, MemberFilter.Property("Task", null));
			this.AsyncVoidMethodBuilderCreate = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("Create", 0, ParametersCompiled.EmptyReadOnlyParameters, types.AsyncVoidMethodBuilder.TypeSpec));
			this.AsyncVoidMethodBuilderSetException = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("SetException", 0, null, builtinTypes.Void));
			this.AsyncVoidMethodBuilderSetResult = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("SetResult", 0, ParametersCompiled.EmptyReadOnlyParameters, builtinTypes.Void));
			this.AsyncVoidMethodBuilderSetStateMachine = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, "SetStateMachine", MemberKind.Method, () => new TypeSpec[]
			{
				types.IAsyncStateMachine.TypeSpec
			}, builtinTypes.Void);
			this.AsyncVoidMethodBuilderOnCompleted = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("AwaitOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncVoidMethodBuilderOnCompletedUnsafe = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("AwaitUnsafeOnCompleted", 2, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(1, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncVoidMethodBuilderStart = new PredefinedMember<MethodSpec>(module, types.AsyncVoidMethodBuilder, MemberFilter.Method("Start", 1, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null)
			}, false), builtinTypes.Void));
			this.AsyncStateMachineAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.AsyncStateMachine, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Type
			})));
			this.DebuggerBrowsableAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.DebuggerBrowsable, MemberFilter.Constructor(null));
			this.DecimalCtor = new PredefinedMember<MethodSpec>(module, builtinTypes.Decimal, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Int,
				builtinTypes.Int,
				builtinTypes.Int,
				builtinTypes.Bool,
				builtinTypes.Byte
			})));
			this.DecimalCtorInt = new PredefinedMember<MethodSpec>(module, builtinTypes.Decimal, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Int
			})));
			this.DecimalCtorLong = new PredefinedMember<MethodSpec>(module, builtinTypes.Decimal, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Long
			})));
			this.DecimalConstantAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.DecimalConstant, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Byte,
				builtinTypes.Byte,
				builtinTypes.UInt,
				builtinTypes.UInt,
				builtinTypes.UInt
			})));
			this.DefaultMemberAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.DefaultMember, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.String
			})));
			this.DelegateCombine = new PredefinedMember<MethodSpec>(module, builtinTypes.Delegate, "Combine", new TypeSpec[]
			{
				builtinTypes.Delegate,
				builtinTypes.Delegate
			});
			this.DelegateRemove = new PredefinedMember<MethodSpec>(module, builtinTypes.Delegate, "Remove", new TypeSpec[]
			{
				builtinTypes.Delegate,
				builtinTypes.Delegate
			});
			this.DelegateEqual = new PredefinedMember<MethodSpec>(module, builtinTypes.Delegate, new MemberFilter(Operator.GetMetadataName(Operator.OpType.Equality), 0, MemberKind.Operator, null, builtinTypes.Bool));
			this.DelegateInequal = new PredefinedMember<MethodSpec>(module, builtinTypes.Delegate, new MemberFilter(Operator.GetMetadataName(Operator.OpType.Inequality), 0, MemberKind.Operator, null, builtinTypes.Bool));
			this.DynamicAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.Dynamic, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				ArrayContainer.MakeType(module, builtinTypes.Bool)
			})));
			this.FieldInfoGetFieldFromHandle = new PredefinedMember<MethodSpec>(module, types.FieldInfo, "GetFieldFromHandle", MemberKind.Method, new PredefinedType[]
			{
				types.RuntimeFieldHandle
			});
			this.FieldInfoGetFieldFromHandle2 = new PredefinedMember<MethodSpec>(module, types.FieldInfo, "GetFieldFromHandle", MemberKind.Method, new PredefinedType[]
			{
				types.RuntimeFieldHandle,
				new PredefinedType(builtinTypes.RuntimeTypeHandle)
			});
			this.FixedBufferAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.FixedBuffer, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Type,
				builtinTypes.Int
			})));
			this.IDisposableDispose = new PredefinedMember<MethodSpec>(module, builtinTypes.IDisposable, "Dispose", TypeSpec.EmptyTypes);
			this.IEnumerableGetEnumerator = new PredefinedMember<MethodSpec>(module, builtinTypes.IEnumerable, "GetEnumerator", TypeSpec.EmptyTypes);
			this.InterlockedCompareExchange = new PredefinedMember<MethodSpec>(module, types.Interlocked, MemberFilter.Method("CompareExchange", 0, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.NONE),
				new ParameterData(null, Parameter.Modifier.NONE)
			}, new BuiltinTypeSpec[]
			{
				builtinTypes.Int,
				builtinTypes.Int,
				builtinTypes.Int
			}, false), builtinTypes.Int));
			this.InterlockedCompareExchange_T = new PredefinedMember<MethodSpec>(module, types.Interlocked, MemberFilter.Method("CompareExchange", 1, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.REF),
				new ParameterData(null, Parameter.Modifier.NONE),
				new ParameterData(null, Parameter.Modifier.NONE)
			}, new TypeParameterSpec[]
			{
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null),
				new TypeParameterSpec(0, definition, SpecialConstraint.None, Variance.None, null)
			}, false), null));
			this.MethodInfoGetMethodFromHandle = new PredefinedMember<MethodSpec>(module, types.MethodBase, "GetMethodFromHandle", MemberKind.Method, new PredefinedType[]
			{
				types.RuntimeMethodHandle
			});
			this.MethodInfoGetMethodFromHandle2 = new PredefinedMember<MethodSpec>(module, types.MethodBase, "GetMethodFromHandle", MemberKind.Method, new PredefinedType[]
			{
				types.RuntimeMethodHandle,
				new PredefinedType(builtinTypes.RuntimeTypeHandle)
			});
			this.MonitorEnter = new PredefinedMember<MethodSpec>(module, types.Monitor, "Enter", new TypeSpec[]
			{
				builtinTypes.Object
			});
			this.MonitorEnter_v4 = new PredefinedMember<MethodSpec>(module, types.Monitor, MemberFilter.Method("Enter", 0, new ParametersImported(new ParameterData[]
			{
				new ParameterData(null, Parameter.Modifier.NONE),
				new ParameterData(null, Parameter.Modifier.REF)
			}, new BuiltinTypeSpec[]
			{
				builtinTypes.Object,
				builtinTypes.Bool
			}, false), null));
			this.MonitorExit = new PredefinedMember<MethodSpec>(module, types.Monitor, "Exit", new TypeSpec[]
			{
				builtinTypes.Object
			});
			this.RuntimeCompatibilityWrapNonExceptionThrows = new PredefinedMember<PropertySpec>(module, predefinedAttributes.RuntimeCompatibility, MemberFilter.Property("WrapNonExceptionThrows", builtinTypes.Bool));
			this.RuntimeHelpersInitializeArray = new PredefinedMember<MethodSpec>(module, types.RuntimeHelpers, "InitializeArray", new TypeSpec[]
			{
				builtinTypes.Array,
				builtinTypes.RuntimeFieldHandle
			});
			this.RuntimeHelpersOffsetToStringData = new PredefinedMember<PropertySpec>(module, types.RuntimeHelpers, MemberFilter.Property("OffsetToStringData", builtinTypes.Int));
			this.SecurityActionRequestMinimum = new PredefinedMember<ConstSpec>(module, types.SecurityAction, "RequestMinimum", MemberKind.Field, new PredefinedType[]
			{
				types.SecurityAction
			});
			this.StringEmpty = new PredefinedMember<FieldSpec>(module, builtinTypes.String, MemberFilter.Field("Empty", builtinTypes.String));
			this.StringEqual = new PredefinedMember<MethodSpec>(module, builtinTypes.String, new MemberFilter(Operator.GetMetadataName(Operator.OpType.Equality), 0, MemberKind.Operator, null, builtinTypes.Bool));
			this.StringInequal = new PredefinedMember<MethodSpec>(module, builtinTypes.String, new MemberFilter(Operator.GetMetadataName(Operator.OpType.Inequality), 0, MemberKind.Operator, null, builtinTypes.Bool));
			this.StructLayoutAttributeCtor = new PredefinedMember<MethodSpec>(module, predefinedAttributes.StructLayout, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				builtinTypes.Short
			})));
			this.StructLayoutCharSet = new PredefinedMember<FieldSpec>(module, predefinedAttributes.StructLayout, "CharSet", MemberKind.Field, new PredefinedType[]
			{
				types.CharSet
			});
			this.StructLayoutSize = new PredefinedMember<FieldSpec>(module, predefinedAttributes.StructLayout, MemberFilter.Field("Size", builtinTypes.Int));
			this.TypeGetTypeFromHandle = new PredefinedMember<MethodSpec>(module, builtinTypes.Type, "GetTypeFromHandle", new TypeSpec[]
			{
				builtinTypes.RuntimeTypeHandle
			});
		}

		// Token: 0x04000D09 RID: 3337
		public readonly PredefinedMember<MethodSpec> ActivatorCreateInstance;

		// Token: 0x04000D0A RID: 3338
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderCreate;

		// Token: 0x04000D0B RID: 3339
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderStart;

		// Token: 0x04000D0C RID: 3340
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderSetResult;

		// Token: 0x04000D0D RID: 3341
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderSetException;

		// Token: 0x04000D0E RID: 3342
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderSetStateMachine;

		// Token: 0x04000D0F RID: 3343
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderOnCompleted;

		// Token: 0x04000D10 RID: 3344
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderOnCompletedUnsafe;

		// Token: 0x04000D11 RID: 3345
		public readonly PredefinedMember<PropertySpec> AsyncTaskMethodBuilderTask;

		// Token: 0x04000D12 RID: 3346
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericCreate;

		// Token: 0x04000D13 RID: 3347
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericStart;

		// Token: 0x04000D14 RID: 3348
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericSetResult;

		// Token: 0x04000D15 RID: 3349
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericSetException;

		// Token: 0x04000D16 RID: 3350
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericSetStateMachine;

		// Token: 0x04000D17 RID: 3351
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericOnCompleted;

		// Token: 0x04000D18 RID: 3352
		public readonly PredefinedMember<MethodSpec> AsyncTaskMethodBuilderGenericOnCompletedUnsafe;

		// Token: 0x04000D19 RID: 3353
		public readonly PredefinedMember<PropertySpec> AsyncTaskMethodBuilderGenericTask;

		// Token: 0x04000D1A RID: 3354
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderCreate;

		// Token: 0x04000D1B RID: 3355
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderStart;

		// Token: 0x04000D1C RID: 3356
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderSetException;

		// Token: 0x04000D1D RID: 3357
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderSetResult;

		// Token: 0x04000D1E RID: 3358
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderSetStateMachine;

		// Token: 0x04000D1F RID: 3359
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderOnCompleted;

		// Token: 0x04000D20 RID: 3360
		public readonly PredefinedMember<MethodSpec> AsyncVoidMethodBuilderOnCompletedUnsafe;

		// Token: 0x04000D21 RID: 3361
		public readonly PredefinedMember<MethodSpec> AsyncStateMachineAttributeCtor;

		// Token: 0x04000D22 RID: 3362
		public readonly PredefinedMember<MethodSpec> DebuggerBrowsableAttributeCtor;

		// Token: 0x04000D23 RID: 3363
		public readonly PredefinedMember<MethodSpec> DecimalCtor;

		// Token: 0x04000D24 RID: 3364
		public readonly PredefinedMember<MethodSpec> DecimalCtorInt;

		// Token: 0x04000D25 RID: 3365
		public readonly PredefinedMember<MethodSpec> DecimalCtorLong;

		// Token: 0x04000D26 RID: 3366
		public readonly PredefinedMember<MethodSpec> DecimalConstantAttributeCtor;

		// Token: 0x04000D27 RID: 3367
		public readonly PredefinedMember<MethodSpec> DefaultMemberAttributeCtor;

		// Token: 0x04000D28 RID: 3368
		public readonly PredefinedMember<MethodSpec> DelegateCombine;

		// Token: 0x04000D29 RID: 3369
		public readonly PredefinedMember<MethodSpec> DelegateEqual;

		// Token: 0x04000D2A RID: 3370
		public readonly PredefinedMember<MethodSpec> DelegateInequal;

		// Token: 0x04000D2B RID: 3371
		public readonly PredefinedMember<MethodSpec> DelegateRemove;

		// Token: 0x04000D2C RID: 3372
		public readonly PredefinedMember<MethodSpec> DynamicAttributeCtor;

		// Token: 0x04000D2D RID: 3373
		public readonly PredefinedMember<MethodSpec> FieldInfoGetFieldFromHandle;

		// Token: 0x04000D2E RID: 3374
		public readonly PredefinedMember<MethodSpec> FieldInfoGetFieldFromHandle2;

		// Token: 0x04000D2F RID: 3375
		public readonly PredefinedMember<MethodSpec> IDisposableDispose;

		// Token: 0x04000D30 RID: 3376
		public readonly PredefinedMember<MethodSpec> IEnumerableGetEnumerator;

		// Token: 0x04000D31 RID: 3377
		public readonly PredefinedMember<MethodSpec> InterlockedCompareExchange;

		// Token: 0x04000D32 RID: 3378
		public readonly PredefinedMember<MethodSpec> InterlockedCompareExchange_T;

		// Token: 0x04000D33 RID: 3379
		public readonly PredefinedMember<MethodSpec> FixedBufferAttributeCtor;

		// Token: 0x04000D34 RID: 3380
		public readonly PredefinedMember<MethodSpec> MethodInfoGetMethodFromHandle;

		// Token: 0x04000D35 RID: 3381
		public readonly PredefinedMember<MethodSpec> MethodInfoGetMethodFromHandle2;

		// Token: 0x04000D36 RID: 3382
		public readonly PredefinedMember<MethodSpec> MonitorEnter;

		// Token: 0x04000D37 RID: 3383
		public readonly PredefinedMember<MethodSpec> MonitorEnter_v4;

		// Token: 0x04000D38 RID: 3384
		public readonly PredefinedMember<MethodSpec> MonitorExit;

		// Token: 0x04000D39 RID: 3385
		public readonly PredefinedMember<PropertySpec> RuntimeCompatibilityWrapNonExceptionThrows;

		// Token: 0x04000D3A RID: 3386
		public readonly PredefinedMember<MethodSpec> RuntimeHelpersInitializeArray;

		// Token: 0x04000D3B RID: 3387
		public readonly PredefinedMember<PropertySpec> RuntimeHelpersOffsetToStringData;

		// Token: 0x04000D3C RID: 3388
		public readonly PredefinedMember<ConstSpec> SecurityActionRequestMinimum;

		// Token: 0x04000D3D RID: 3389
		public readonly PredefinedMember<FieldSpec> StringEmpty;

		// Token: 0x04000D3E RID: 3390
		public readonly PredefinedMember<MethodSpec> StringEqual;

		// Token: 0x04000D3F RID: 3391
		public readonly PredefinedMember<MethodSpec> StringInequal;

		// Token: 0x04000D40 RID: 3392
		public readonly PredefinedMember<MethodSpec> StructLayoutAttributeCtor;

		// Token: 0x04000D41 RID: 3393
		public readonly PredefinedMember<FieldSpec> StructLayoutCharSet;

		// Token: 0x04000D42 RID: 3394
		public readonly PredefinedMember<FieldSpec> StructLayoutSize;

		// Token: 0x04000D43 RID: 3395
		public readonly PredefinedMember<MethodSpec> TypeGetTypeFromHandle;

		// Token: 0x02000401 RID: 1025
		[CompilerGenerated]
		private sealed class <>c__DisplayClass59_0
		{
			// Token: 0x06002832 RID: 10290 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass59_0()
			{
			}

			// Token: 0x06002833 RID: 10291 RVA: 0x000BEC31 File Offset: 0x000BCE31
			internal TypeSpec[] <.ctor>b__0()
			{
				return new TypeSpec[]
				{
					this.types.IAsyncStateMachine.TypeSpec
				};
			}

			// Token: 0x06002834 RID: 10292 RVA: 0x000BEC4C File Offset: 0x000BCE4C
			internal TypeSpec[] <.ctor>b__1()
			{
				return new TypeSpec[]
				{
					this.types.AsyncTaskMethodBuilderGeneric.TypeSpec.MemberDefinition.TypeParameters[0]
				};
			}

			// Token: 0x06002835 RID: 10293 RVA: 0x000BEC31 File Offset: 0x000BCE31
			internal TypeSpec[] <.ctor>b__2()
			{
				return new TypeSpec[]
				{
					this.types.IAsyncStateMachine.TypeSpec
				};
			}

			// Token: 0x06002836 RID: 10294 RVA: 0x000BEC31 File Offset: 0x000BCE31
			internal TypeSpec[] <.ctor>b__3()
			{
				return new TypeSpec[]
				{
					this.types.IAsyncStateMachine.TypeSpec
				};
			}

			// Token: 0x04001166 RID: 4454
			public PredefinedTypes types;
		}
	}
}
