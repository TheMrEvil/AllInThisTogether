using System;

namespace Mono.CSharp
{
	// Token: 0x020002D1 RID: 721
	public class PredefinedTypes
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x000A8A10 File Offset: 0x000A6C10
		public PredefinedTypes(ModuleContainer module)
		{
			this.TypedReference = new PredefinedType(module, MemberKind.Struct, "System", "TypedReference");
			this.ArgIterator = new PredefinedType(module, MemberKind.Struct, "System", "ArgIterator");
			this.MarshalByRefObject = new PredefinedType(module, MemberKind.Class, "System", "MarshalByRefObject");
			this.RuntimeHelpers = new PredefinedType(module, MemberKind.Class, "System.Runtime.CompilerServices", "RuntimeHelpers");
			this.IAsyncResult = new PredefinedType(module, MemberKind.Interface, "System", "IAsyncResult");
			this.AsyncCallback = new PredefinedType(module, MemberKind.Delegate, "System", "AsyncCallback");
			this.RuntimeArgumentHandle = new PredefinedType(module, MemberKind.Struct, "System", "RuntimeArgumentHandle");
			this.CharSet = new PredefinedType(module, MemberKind.Enum, "System.Runtime.InteropServices", "CharSet");
			this.IsVolatile = new PredefinedType(module, MemberKind.Class, "System.Runtime.CompilerServices", "IsVolatile");
			this.IEnumeratorGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "IEnumerator", 1);
			this.IListGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "IList", 1);
			this.IReadOnlyListGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "IReadOnlyList", 1);
			this.ICollectionGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "ICollection", 1);
			this.IReadOnlyCollectionGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "IReadOnlyCollection", 1);
			this.IEnumerableGeneric = new PredefinedType(module, MemberKind.Interface, "System.Collections.Generic", "IEnumerable", 1);
			this.Nullable = new PredefinedType(module, MemberKind.Struct, "System", "Nullable", 1);
			this.Activator = new PredefinedType(module, MemberKind.Class, "System", "Activator");
			this.Interlocked = new PredefinedType(module, MemberKind.Class, "System.Threading", "Interlocked");
			this.Monitor = new PredefinedType(module, MemberKind.Class, "System.Threading", "Monitor");
			this.NotSupportedException = new PredefinedType(module, MemberKind.Class, "System", "NotSupportedException");
			this.RuntimeFieldHandle = new PredefinedType(module, MemberKind.Struct, "System", "RuntimeFieldHandle");
			this.RuntimeMethodHandle = new PredefinedType(module, MemberKind.Struct, "System", "RuntimeMethodHandle");
			this.SecurityAction = new PredefinedType(module, MemberKind.Enum, "System.Security.Permissions", "SecurityAction");
			this.Dictionary = new PredefinedType(module, MemberKind.Class, "System.Collections.Generic", "Dictionary", 2);
			this.Hashtable = new PredefinedType(module, MemberKind.Class, "System.Collections", "Hashtable");
			this.Expression = new PredefinedType(module, MemberKind.Class, "System.Linq.Expressions", "Expression");
			this.ExpressionGeneric = new PredefinedType(module, MemberKind.Class, "System.Linq.Expressions", "Expression", 1);
			this.MemberBinding = new PredefinedType(module, MemberKind.Class, "System.Linq.Expressions", "MemberBinding");
			this.ParameterExpression = new PredefinedType(module, MemberKind.Class, "System.Linq.Expressions", "ParameterExpression");
			this.FieldInfo = new PredefinedType(module, MemberKind.Class, "System.Reflection", "FieldInfo");
			this.MethodBase = new PredefinedType(module, MemberKind.Class, "System.Reflection", "MethodBase");
			this.MethodInfo = new PredefinedType(module, MemberKind.Class, "System.Reflection", "MethodInfo");
			this.ConstructorInfo = new PredefinedType(module, MemberKind.Class, "System.Reflection", "ConstructorInfo");
			this.CallSite = new PredefinedType(module, MemberKind.Class, "System.Runtime.CompilerServices", "CallSite");
			this.CallSiteGeneric = new PredefinedType(module, MemberKind.Class, "System.Runtime.CompilerServices", "CallSite", 1);
			this.Binder = new PredefinedType(module, MemberKind.Class, "Microsoft.CSharp.RuntimeBinder", "Binder");
			this.BinderFlags = new PredefinedType(module, MemberKind.Enum, "Microsoft.CSharp.RuntimeBinder", "CSharpBinderFlags");
			this.Action = new PredefinedType(module, MemberKind.Delegate, "System", "Action");
			this.AsyncVoidMethodBuilder = new PredefinedType(module, MemberKind.Struct, "System.Runtime.CompilerServices", "AsyncVoidMethodBuilder");
			this.AsyncTaskMethodBuilder = new PredefinedType(module, MemberKind.Struct, "System.Runtime.CompilerServices", "AsyncTaskMethodBuilder");
			this.AsyncTaskMethodBuilderGeneric = new PredefinedType(module, MemberKind.Struct, "System.Runtime.CompilerServices", "AsyncTaskMethodBuilder", 1);
			this.Task = new PredefinedType(module, MemberKind.Class, "System.Threading.Tasks", "Task");
			this.TaskGeneric = new PredefinedType(module, MemberKind.Class, "System.Threading.Tasks", "Task", 1);
			this.IAsyncStateMachine = new PredefinedType(module, MemberKind.Interface, "System.Runtime.CompilerServices", "IAsyncStateMachine");
			this.INotifyCompletion = new PredefinedType(module, MemberKind.Interface, "System.Runtime.CompilerServices", "INotifyCompletion");
			this.ICriticalNotifyCompletion = new PredefinedType(module, MemberKind.Interface, "System.Runtime.CompilerServices", "ICriticalNotifyCompletion");
			this.IFormattable = new PredefinedType(module, MemberKind.Interface, "System", "IFormattable");
			this.FormattableString = new PredefinedType(module, MemberKind.Class, "System", "FormattableString");
			this.FormattableStringFactory = new PredefinedType(module, MemberKind.Class, "System.Runtime.CompilerServices", "FormattableStringFactory");
			if (this.TypedReference.Define())
			{
				this.TypedReference.TypeSpec.IsSpecialRuntimeType = true;
			}
			if (this.ArgIterator.Define())
			{
				this.ArgIterator.TypeSpec.IsSpecialRuntimeType = true;
			}
			if (this.IEnumerableGeneric.Define())
			{
				this.IEnumerableGeneric.TypeSpec.IsArrayGenericInterface = true;
			}
			if (this.IListGeneric.Define())
			{
				this.IListGeneric.TypeSpec.IsArrayGenericInterface = true;
			}
			if (this.IReadOnlyListGeneric.Define())
			{
				this.IReadOnlyListGeneric.TypeSpec.IsArrayGenericInterface = true;
			}
			if (this.ICollectionGeneric.Define())
			{
				this.ICollectionGeneric.TypeSpec.IsArrayGenericInterface = true;
			}
			if (this.IReadOnlyCollectionGeneric.Define())
			{
				this.IReadOnlyCollectionGeneric.TypeSpec.IsArrayGenericInterface = true;
			}
			if (this.Nullable.Define())
			{
				this.Nullable.TypeSpec.IsNullableType = true;
			}
			if (this.ExpressionGeneric.Define())
			{
				this.ExpressionGeneric.TypeSpec.IsExpressionTreeType = true;
			}
			this.Task.Define();
			if (this.TaskGeneric.Define())
			{
				this.TaskGeneric.TypeSpec.IsGenericTask = true;
			}
			this.SwitchUserTypes = Switch.CreateSwitchUserTypes(module, this.Nullable.TypeSpec);
			this.IFormattable.Define();
			this.FormattableString.Define();
		}

		// Token: 0x04000CD7 RID: 3287
		public readonly PredefinedType ArgIterator;

		// Token: 0x04000CD8 RID: 3288
		public readonly PredefinedType TypedReference;

		// Token: 0x04000CD9 RID: 3289
		public readonly PredefinedType MarshalByRefObject;

		// Token: 0x04000CDA RID: 3290
		public readonly PredefinedType RuntimeHelpers;

		// Token: 0x04000CDB RID: 3291
		public readonly PredefinedType IAsyncResult;

		// Token: 0x04000CDC RID: 3292
		public readonly PredefinedType AsyncCallback;

		// Token: 0x04000CDD RID: 3293
		public readonly PredefinedType RuntimeArgumentHandle;

		// Token: 0x04000CDE RID: 3294
		public readonly PredefinedType CharSet;

		// Token: 0x04000CDF RID: 3295
		public readonly PredefinedType IsVolatile;

		// Token: 0x04000CE0 RID: 3296
		public readonly PredefinedType IEnumeratorGeneric;

		// Token: 0x04000CE1 RID: 3297
		public readonly PredefinedType IListGeneric;

		// Token: 0x04000CE2 RID: 3298
		public readonly PredefinedType IReadOnlyListGeneric;

		// Token: 0x04000CE3 RID: 3299
		public readonly PredefinedType ICollectionGeneric;

		// Token: 0x04000CE4 RID: 3300
		public readonly PredefinedType IReadOnlyCollectionGeneric;

		// Token: 0x04000CE5 RID: 3301
		public readonly PredefinedType IEnumerableGeneric;

		// Token: 0x04000CE6 RID: 3302
		public readonly PredefinedType Nullable;

		// Token: 0x04000CE7 RID: 3303
		public readonly PredefinedType Activator;

		// Token: 0x04000CE8 RID: 3304
		public readonly PredefinedType Interlocked;

		// Token: 0x04000CE9 RID: 3305
		public readonly PredefinedType Monitor;

		// Token: 0x04000CEA RID: 3306
		public readonly PredefinedType NotSupportedException;

		// Token: 0x04000CEB RID: 3307
		public readonly PredefinedType RuntimeFieldHandle;

		// Token: 0x04000CEC RID: 3308
		public readonly PredefinedType RuntimeMethodHandle;

		// Token: 0x04000CED RID: 3309
		public readonly PredefinedType SecurityAction;

		// Token: 0x04000CEE RID: 3310
		public readonly PredefinedType Dictionary;

		// Token: 0x04000CEF RID: 3311
		public readonly PredefinedType Hashtable;

		// Token: 0x04000CF0 RID: 3312
		public readonly TypeSpec[] SwitchUserTypes;

		// Token: 0x04000CF1 RID: 3313
		public readonly PredefinedType Expression;

		// Token: 0x04000CF2 RID: 3314
		public readonly PredefinedType ExpressionGeneric;

		// Token: 0x04000CF3 RID: 3315
		public readonly PredefinedType ParameterExpression;

		// Token: 0x04000CF4 RID: 3316
		public readonly PredefinedType FieldInfo;

		// Token: 0x04000CF5 RID: 3317
		public readonly PredefinedType MethodBase;

		// Token: 0x04000CF6 RID: 3318
		public readonly PredefinedType MethodInfo;

		// Token: 0x04000CF7 RID: 3319
		public readonly PredefinedType ConstructorInfo;

		// Token: 0x04000CF8 RID: 3320
		public readonly PredefinedType MemberBinding;

		// Token: 0x04000CF9 RID: 3321
		public readonly PredefinedType Binder;

		// Token: 0x04000CFA RID: 3322
		public readonly PredefinedType CallSite;

		// Token: 0x04000CFB RID: 3323
		public readonly PredefinedType CallSiteGeneric;

		// Token: 0x04000CFC RID: 3324
		public readonly PredefinedType BinderFlags;

		// Token: 0x04000CFD RID: 3325
		public readonly PredefinedType AsyncVoidMethodBuilder;

		// Token: 0x04000CFE RID: 3326
		public readonly PredefinedType AsyncTaskMethodBuilder;

		// Token: 0x04000CFF RID: 3327
		public readonly PredefinedType AsyncTaskMethodBuilderGeneric;

		// Token: 0x04000D00 RID: 3328
		public readonly PredefinedType Action;

		// Token: 0x04000D01 RID: 3329
		public readonly PredefinedType Task;

		// Token: 0x04000D02 RID: 3330
		public readonly PredefinedType TaskGeneric;

		// Token: 0x04000D03 RID: 3331
		public readonly PredefinedType IAsyncStateMachine;

		// Token: 0x04000D04 RID: 3332
		public readonly PredefinedType INotifyCompletion;

		// Token: 0x04000D05 RID: 3333
		public readonly PredefinedType ICriticalNotifyCompletion;

		// Token: 0x04000D06 RID: 3334
		public readonly PredefinedType IFormattable;

		// Token: 0x04000D07 RID: 3335
		public readonly PredefinedType FormattableString;

		// Token: 0x04000D08 RID: 3336
		public readonly PredefinedType FormattableStringFactory;
	}
}
