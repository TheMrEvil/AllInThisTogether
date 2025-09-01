using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Provides a base class for specifying dynamic behavior at run time. This class must be inherited from; you cannot instantiate it directly.</summary>
	// Token: 0x020002FF RID: 767
	[Serializable]
	public class DynamicObject : IDynamicMetaObjectProvider
	{
		/// <summary>Enables derived types to initialize a new instance of the <see cref="T:System.Dynamic.DynamicObject" /> type.</summary>
		// Token: 0x0600172C RID: 5932 RVA: 0x00002162 File Offset: 0x00000362
		protected DynamicObject()
		{
		}

		/// <summary>Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.</summary>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
		// Token: 0x0600172D RID: 5933 RVA: 0x0004D602 File Offset: 0x0004B802
		public virtual bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.</summary>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x0600172E RID: 5934 RVA: 0x000023D1 File Offset: 0x000005D1
		public virtual bool TrySetMember(SetMemberBinder binder, object value)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that delete an object member. This method is not intended for use in C# or Visual Basic.</summary>
		/// <param name="binder">Provides information about the deletion.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x0600172F RID: 5935 RVA: 0x000023D1 File Offset: 0x000005D1
		public virtual bool TryDeleteMember(DeleteMemberBinder binder)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that invoke a member. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as calling a method.</summary>
		/// <param name="binder">Provides information about the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleMethod". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="args">The arguments that are passed to the object member during the invoke operation. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the member invocation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001730 RID: 5936 RVA: 0x0004D608 File Offset: 0x0004B808
		public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations that convert an object from one type to another.</summary>
		/// <param name="binder">Provides information about the conversion operation. The binder.Type property provides the type to which the object must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Type returns the <see cref="T:System.String" /> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns <see langword="true" /> for explicit conversion and <see langword="false" /> for implicit conversion.</param>
		/// <param name="result">The result of the type conversion operation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001731 RID: 5937 RVA: 0x0004D602 File Offset: 0x0004B802
		public virtual bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that initialize a new instance of a dynamic object. This method is not intended for use in C# or Visual Basic.</summary>
		/// <param name="binder">Provides information about the initialization operation.</param>
		/// <param name="args">The arguments that are passed to the object during initialization. For example, for the new SampleType(100) operation, where SampleType is the type derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the initialization.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001732 RID: 5938 RVA: 0x0004D608 File Offset: 0x0004B808
		public virtual bool TryCreateInstance(CreateInstanceBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.</summary>
		/// <param name="binder">Provides information about the invoke operation.</param>
		/// <param name="args">The arguments that are passed to the object during the invoke operation. For example, for the sampleObject(100) operation, where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
		/// <param name="result">The result of the object invocation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.</returns>
		// Token: 0x06001733 RID: 5939 RVA: 0x0004D608 File Offset: 0x0004B808
		public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for binary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as addition and multiplication.</summary>
		/// <param name="binder">Provides information about the binary operation. The binder.Operation property returns an <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the sum = first + second statement, where first and second are derived from the <see langword="DynamicObject" /> class, binder.Operation returns ExpressionType.Add.</param>
		/// <param name="arg">The right operand for the binary operation. For example, for the sum = first + second statement, where first and second are derived from the <see langword="DynamicObject" /> class, <paramref name="arg" /> is equal to second.</param>
		/// <param name="result">The result of the binary operation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001734 RID: 5940 RVA: 0x0004D608 File Offset: 0x0004B808
		public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides implementation for unary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as negation, increment, or decrement.</summary>
		/// <param name="binder">Provides information about the unary operation. The binder.Operation property returns an <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the negativeNumber = -number statement, where number is derived from the <see langword="DynamicObject" /> class, binder.Operation returns "Negate".</param>
		/// <param name="result">The result of the unary operation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001735 RID: 5941 RVA: 0x0004D602 File Offset: 0x0004B802
		public virtual bool TryUnaryOperation(UnaryOperationBinder binder, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that get a value by index. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for indexing operations.</summary>
		/// <param name="binder">Provides information about the operation. </param>
		/// <param name="indexes">The indexes that are used in the operation. For example, for the sampleObject[3] operation in C# (sampleObject(3) in Visual Basic), where sampleObject is derived from the <see langword="DynamicObject" /> class, <paramref name="indexes[0]" /> is equal to 3.</param>
		/// <param name="result">The result of the index operation.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
		// Token: 0x06001736 RID: 5942 RVA: 0x0004D608 File Offset: 0x0004B808
		public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			result = null;
			return false;
		}

		/// <summary>Provides the implementation for operations that set a value by index. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations that access objects by a specified index.</summary>
		/// <param name="binder">Provides information about the operation. </param>
		/// <param name="indexes">The indexes that are used in the operation. For example, for the sampleObject[3] = 10 operation in C# (sampleObject(3) = 10 in Visual Basic), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="indexes[0]" /> is equal to 3.</param>
		/// <param name="value">The value to set to the object that has the specified index. For example, for the sampleObject[3] = 10 operation in C# (sampleObject(3) = 10 in Visual Basic), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="value" /> is equal to 10.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.</returns>
		// Token: 0x06001737 RID: 5943 RVA: 0x000023D1 File Offset: 0x000005D1
		public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		/// <summary>Provides the implementation for operations that delete an object by index. This method is not intended for use in C# or Visual Basic.</summary>
		/// <param name="binder">Provides information about the deletion.</param>
		/// <param name="indexes">The indexes to be deleted.</param>
		/// <returns>
		///     <see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
		// Token: 0x06001738 RID: 5944 RVA: 0x000023D1 File Offset: 0x000005D1
		public virtual bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		/// <summary>Returns the enumeration of all dynamic member names. </summary>
		/// <returns>A sequence that contains dynamic member names.</returns>
		// Token: 0x06001739 RID: 5945 RVA: 0x0004D29E File Offset: 0x0004B49E
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return Array.Empty<string>();
		}

		/// <summary>Provides a <see cref="T:System.Dynamic.DynamicMetaObject" /> that dispatches to the dynamic virtual methods. The object can be encapsulated inside another <see cref="T:System.Dynamic.DynamicMetaObject" /> to provide custom behavior for individual actions. This method supports the Dynamic Language Runtime infrastructure for language implementers and it is not intended to be used directly from your code.</summary>
		/// <param name="parameter">The expression that represents <see cref="T:System.Dynamic.DynamicMetaObject" /> to dispatch to the dynamic virtual methods.</param>
		/// <returns>An object of the <see cref="T:System.Dynamic.DynamicMetaObject" /> type.</returns>
		// Token: 0x0600173A RID: 5946 RVA: 0x0004D60E File Offset: 0x0004B80E
		public virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicObject.MetaDynamic(parameter, this);
		}

		// Token: 0x02000300 RID: 768
		private sealed class MetaDynamic : DynamicMetaObject
		{
			// Token: 0x0600173B RID: 5947 RVA: 0x0004D617 File Offset: 0x0004B817
			internal MetaDynamic(Expression expression, DynamicObject value) : base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x0600173C RID: 5948 RVA: 0x0004D626 File Offset: 0x0004B826
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				return this.Value.GetDynamicMemberNames();
			}

			// Token: 0x0600173D RID: 5949 RVA: 0x0004D634 File Offset: 0x0004B834
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryGetMember))
				{
					return this.CallMethodWithResult<GetMemberBinder>(CachedReflectionInfo.DynamicObject_TryGetMember, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, GetMemberBinder b, DynamicMetaObject e) => b.FallbackGetMember(@this, e));
				}
				return base.BindGetMember(binder);
			}

			// Token: 0x0600173E RID: 5950 RVA: 0x0004D688 File Offset: 0x0004B888
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TrySetMember))
				{
					DynamicMetaObject localValue = value;
					return this.CallMethodReturnLast<SetMemberBinder>(CachedReflectionInfo.DynamicObject_TrySetMember, binder, DynamicObject.MetaDynamic.s_noArgs, value.Expression, (DynamicObject.MetaDynamic @this, SetMemberBinder b, DynamicMetaObject e) => b.FallbackSetMember(@this, localValue, e));
				}
				return base.BindSetMember(binder, value);
			}

			// Token: 0x0600173F RID: 5951 RVA: 0x0004D6DC File Offset: 0x0004B8DC
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryDeleteMember))
				{
					return this.CallMethodNoResult<DeleteMemberBinder>(CachedReflectionInfo.DynamicObject_TryDeleteMember, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, DeleteMemberBinder b, DynamicMetaObject e) => b.FallbackDeleteMember(@this, e));
				}
				return base.BindDeleteMember(binder);
			}

			// Token: 0x06001740 RID: 5952 RVA: 0x0004D730 File Offset: 0x0004B930
			public override DynamicMetaObject BindConvert(ConvertBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryConvert))
				{
					return this.CallMethodWithResult<ConvertBinder>(CachedReflectionInfo.DynamicObject_TryConvert, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, ConvertBinder b, DynamicMetaObject e) => b.FallbackConvert(@this, e));
				}
				return base.BindConvert(binder);
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x0004D784 File Offset: 0x0004B984
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult<InvokeMemberBinder>(CachedReflectionInfo.DynamicObject_TryInvokeMember, binder, DynamicMetaObject.GetExpressions(args), this.BuildCallMethodWithResult<GetMemberBinder>(CachedReflectionInfo.DynamicObject_TryGetMember, new DynamicObject.MetaDynamic.GetBinderAdapter(binder), DynamicObject.MetaDynamic.s_noArgs, binder.FallbackInvokeMember(this, args, null), (DynamicObject.MetaDynamic @this, GetMemberBinder ignored, DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
				return binder.FallbackInvokeMember(this, args, errorSuggestion);
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x0004D810 File Offset: 0x0004BA10
			public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryCreateInstance))
				{
					DynamicMetaObject[] localArgs = args;
					return this.CallMethodWithResult<CreateInstanceBinder>(CachedReflectionInfo.DynamicObject_TryCreateInstance, binder, DynamicMetaObject.GetExpressions(args), (DynamicObject.MetaDynamic @this, CreateInstanceBinder b, DynamicMetaObject e) => b.FallbackCreateInstance(@this, localArgs, e));
				}
				return base.BindCreateInstance(binder, args);
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x0004D860 File Offset: 0x0004BA60
			public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryInvoke))
				{
					DynamicMetaObject[] localArgs = args;
					return this.CallMethodWithResult<InvokeBinder>(CachedReflectionInfo.DynamicObject_TryInvoke, binder, DynamicMetaObject.GetExpressions(args), (DynamicObject.MetaDynamic @this, InvokeBinder b, DynamicMetaObject e) => b.FallbackInvoke(@this, localArgs, e));
				}
				return base.BindInvoke(binder, args);
			}

			// Token: 0x06001744 RID: 5956 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
			public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryBinaryOperation))
				{
					DynamicMetaObject localArg = arg;
					return this.CallMethodWithResult<BinaryOperationBinder>(CachedReflectionInfo.DynamicObject_TryBinaryOperation, binder, new Expression[]
					{
						arg.Expression
					}, (DynamicObject.MetaDynamic @this, BinaryOperationBinder b, DynamicMetaObject e) => b.FallbackBinaryOperation(@this, localArg, e));
				}
				return base.BindBinaryOperation(binder, arg);
			}

			// Token: 0x06001745 RID: 5957 RVA: 0x0004D908 File Offset: 0x0004BB08
			public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryUnaryOperation))
				{
					return this.CallMethodWithResult<UnaryOperationBinder>(CachedReflectionInfo.DynamicObject_TryUnaryOperation, binder, DynamicObject.MetaDynamic.s_noArgs, (DynamicObject.MetaDynamic @this, UnaryOperationBinder b, DynamicMetaObject e) => b.FallbackUnaryOperation(@this, e));
				}
				return base.BindUnaryOperation(binder);
			}

			// Token: 0x06001746 RID: 5958 RVA: 0x0004D95C File Offset: 0x0004BB5C
			public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryGetIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					return this.CallMethodWithResult<GetIndexBinder>(CachedReflectionInfo.DynamicObject_TryGetIndex, binder, DynamicMetaObject.GetExpressions(indexes), (DynamicObject.MetaDynamic @this, GetIndexBinder b, DynamicMetaObject e) => b.FallbackGetIndex(@this, localIndexes, e));
				}
				return base.BindGetIndex(binder, indexes);
			}

			// Token: 0x06001747 RID: 5959 RVA: 0x0004D9AC File Offset: 0x0004BBAC
			public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TrySetIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					DynamicMetaObject localValue = value;
					return this.CallMethodReturnLast<SetIndexBinder>(CachedReflectionInfo.DynamicObject_TrySetIndex, binder, DynamicMetaObject.GetExpressions(indexes), value.Expression, (DynamicObject.MetaDynamic @this, SetIndexBinder b, DynamicMetaObject e) => b.FallbackSetIndex(@this, localIndexes, localValue, e));
				}
				return base.BindSetIndex(binder, indexes, value);
			}

			// Token: 0x06001748 RID: 5960 RVA: 0x0004DA08 File Offset: 0x0004BC08
			public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
			{
				if (this.IsOverridden(CachedReflectionInfo.DynamicObject_TryDeleteIndex))
				{
					DynamicMetaObject[] localIndexes = indexes;
					return this.CallMethodNoResult<DeleteIndexBinder>(CachedReflectionInfo.DynamicObject_TryDeleteIndex, binder, DynamicMetaObject.GetExpressions(indexes), (DynamicObject.MetaDynamic @this, DeleteIndexBinder b, DynamicMetaObject e) => b.FallbackDeleteIndex(@this, localIndexes, e));
				}
				return base.BindDeleteIndex(binder, indexes);
			}

			// Token: 0x06001749 RID: 5961 RVA: 0x0004DA58 File Offset: 0x0004BC58
			private static ReadOnlyCollection<Expression> GetConvertedArgs(params Expression[] args)
			{
				Expression[] array = new Expression[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = Expression.Convert(args[i], typeof(object));
				}
				return new TrueReadOnlyCollection<Expression>(array);
			}

			// Token: 0x0600174A RID: 5962 RVA: 0x0004DA98 File Offset: 0x0004BC98
			private static Expression ReferenceArgAssign(Expression callArgs, Expression[] args)
			{
				ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = null;
				for (int i = 0; i < args.Length; i++)
				{
					ParameterExpression parameterExpression = args[i] as ParameterExpression;
					ContractUtils.Requires(parameterExpression != null, "args");
					if (parameterExpression.IsByRef)
					{
						if (readOnlyCollectionBuilder == null)
						{
							readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>();
						}
						readOnlyCollectionBuilder.Add(Expression.Assign(parameterExpression, Expression.Convert(Expression.ArrayIndex(callArgs, Utils.Constant(i)), parameterExpression.Type)));
					}
				}
				if (readOnlyCollectionBuilder != null)
				{
					return Expression.Block(readOnlyCollectionBuilder);
				}
				return Utils.Empty;
			}

			// Token: 0x0600174B RID: 5963 RVA: 0x0004DB10 File Offset: 0x0004BD10
			private static Expression[] BuildCallArgs<TBinder>(TBinder binder, Expression[] parameters, Expression arg0, Expression arg1) where TBinder : DynamicMetaObjectBinder
			{
				if (parameters != DynamicObject.MetaDynamic.s_noArgs)
				{
					if (arg1 == null)
					{
						return new Expression[]
						{
							DynamicObject.MetaDynamic.Constant<TBinder>(binder),
							arg0
						};
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant<TBinder>(binder),
						arg0,
						arg1
					};
				}
				else
				{
					if (arg1 == null)
					{
						return new Expression[]
						{
							DynamicObject.MetaDynamic.Constant<TBinder>(binder)
						};
					}
					return new Expression[]
					{
						DynamicObject.MetaDynamic.Constant<TBinder>(binder),
						arg1
					};
				}
			}

			// Token: 0x0600174C RID: 5964 RVA: 0x0004DB7A File Offset: 0x0004BD7A
			private static ConstantExpression Constant<TBinder>(TBinder binder)
			{
				return Expression.Constant(binder, typeof(TBinder));
			}

			// Token: 0x0600174D RID: 5965 RVA: 0x0004DB91 File Offset: 0x0004BD91
			private DynamicMetaObject CallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				return this.CallMethodWithResult<TBinder>(method, binder, args, fallback, null);
			}

			// Token: 0x0600174E RID: 5966 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
			private DynamicMetaObject CallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback, DynamicObject.MetaDynamic.Fallback<TBinder> fallbackInvoke) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject fallbackResult = fallback(this, binder, null);
				DynamicMetaObject errorSuggestion = this.BuildCallMethodWithResult<TBinder>(method, binder, args, fallbackResult, fallbackInvoke);
				return fallback(this, binder, errorSuggestion);
			}

			// Token: 0x0600174F RID: 5967 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
			private DynamicMetaObject BuildCallMethodWithResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicMetaObject fallbackResult, DynamicObject.MetaDynamic.Fallback<TBinder> fallbackInvoke) where TBinder : DynamicMetaObjectBinder
			{
				if (!this.IsOverridden(method))
				{
					return fallbackResult;
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = (method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? Expression.Parameter(typeof(object[]), null) : Expression.Parameter(typeof(object), null);
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (binder.ReturnType != typeof(object))
				{
					UnaryExpression ifTrue = Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType);
					string value = Strings.DynamicObjectResultNotAssignable("{0}", this.Value.GetType(), binder.GetType(), binder.ReturnType);
					Expression test;
					if (binder.ReturnType.IsValueType && Nullable.GetUnderlyingType(binder.ReturnType) == null)
					{
						test = Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType);
					}
					else
					{
						test = Expression.OrElse(Expression.Equal(dynamicMetaObject.Expression, Utils.Null), Expression.TypeIs(dynamicMetaObject.Expression, binder.ReturnType));
					}
					dynamicMetaObject = new DynamicMetaObject(Expression.Condition(test, ifTrue, Expression.Throw(Expression.New(CachedReflectionInfo.InvalidCastException_Ctor_String, new TrueReadOnlyCollection<Expression>(new Expression[]
					{
						Expression.Call(CachedReflectionInfo.String_Format_String_ObjectArray, Expression.Constant(value), Expression.NewArrayInit(typeof(object), new TrueReadOnlyCollection<Expression>(new Expression[]
						{
							Expression.Condition(Expression.Equal(dynamicMetaObject.Expression, Utils.Null), Expression.Constant("null"), Expression.Call(dynamicMetaObject.Expression, CachedReflectionInfo.Object_GetType), typeof(object))
						})))
					})), binder.ReturnType), binder.ReturnType), dynamicMetaObject.Restrictions);
				}
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(this, binder, dynamicMetaObject);
				}
				return new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					(method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)) : Expression.Assign(parameterExpression2, convertedArgs[0]),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression2, parameterExpression)), Expression.Block((method != CachedReflectionInfo.DynamicObject_TryBinaryOperation) ? DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args) : Utils.Empty, dynamicMetaObject.Expression), fallbackResult.Expression, binder.ReturnType)
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
			}

			// Token: 0x06001750 RID: 5968 RVA: 0x0004DEA0 File Offset: 0x0004C0A0
			private DynamicMetaObject CallMethodReturnLast<TBinder>(MethodInfo method, TBinder binder, Expression[] args, Expression value, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject dynamicMetaObject = fallback(this, binder, null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), null);
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression2, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression2, Expression.Assign(parameterExpression, Expression.Convert(value, typeof(object))))), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression2, args), parameterExpression), dynamicMetaObject.Expression, typeof(object))
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(this, binder, errorSuggestion);
			}

			// Token: 0x06001751 RID: 5969 RVA: 0x0004DF90 File Offset: 0x0004C190
			private DynamicMetaObject CallMethodNoResult<TBinder>(MethodInfo method, TBinder binder, Expression[] args, DynamicObject.MetaDynamic.Fallback<TBinder> fallback) where TBinder : DynamicMetaObjectBinder
			{
				DynamicMetaObject dynamicMetaObject = fallback(this, binder, null);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), null);
				ReadOnlyCollection<Expression> convertedArgs = DynamicObject.MetaDynamic.GetConvertedArgs(args);
				DynamicMetaObject errorSuggestion = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.NewArrayInit(typeof(object), convertedArgs)),
					Expression.Condition(Expression.Call(this.GetLimitedSelf(), method, DynamicObject.MetaDynamic.BuildCallArgs<TBinder>(binder, args, parameterExpression, null)), Expression.Block(DynamicObject.MetaDynamic.ReferenceArgAssign(parameterExpression, args), Utils.Empty), dynamicMetaObject.Expression, typeof(void))
				})), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
				return fallback(this, binder, errorSuggestion);
			}

			// Token: 0x06001752 RID: 5970 RVA: 0x0004E058 File Offset: 0x0004C258
			private bool IsOverridden(MethodInfo method)
			{
				foreach (MethodInfo methodInfo in this.Value.GetType().GetMember(method.Name, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public))
				{
					if (methodInfo.DeclaringType != typeof(DynamicObject) && methodInfo.GetBaseDefinition() == method)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001753 RID: 5971 RVA: 0x0004E0BE File Offset: 0x0004C2BE
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x06001754 RID: 5972 RVA: 0x0004E0C6 File Offset: 0x0004C2C6
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, typeof(DynamicObject)))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, typeof(DynamicObject));
			}

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06001755 RID: 5973 RVA: 0x0004E100 File Offset: 0x0004C300
			private new DynamicObject Value
			{
				get
				{
					return (DynamicObject)base.Value;
				}
			}

			// Token: 0x06001756 RID: 5974 RVA: 0x0004E10D File Offset: 0x0004C30D
			// Note: this type is marked as 'beforefieldinit'.
			static MetaDynamic()
			{
			}

			// Token: 0x04000B86 RID: 2950
			private static readonly Expression[] s_noArgs = new Expression[0];

			// Token: 0x02000301 RID: 769
			// (Invoke) Token: 0x06001758 RID: 5976
			private delegate DynamicMetaObject Fallback<TBinder>(DynamicObject.MetaDynamic @this, TBinder binder, DynamicMetaObject errorSuggestion);

			// Token: 0x02000302 RID: 770
			private sealed class GetBinderAdapter : GetMemberBinder
			{
				// Token: 0x0600175B RID: 5979 RVA: 0x0004E11A File Offset: 0x0004C31A
				internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
				{
				}

				// Token: 0x0600175C RID: 5980 RVA: 0x000080E3 File Offset: 0x000062E3
				public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x02000303 RID: 771
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600175D RID: 5981 RVA: 0x0004E12E File Offset: 0x0004C32E
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600175E RID: 5982 RVA: 0x00002162 File Offset: 0x00000362
				public <>c()
				{
				}

				// Token: 0x0600175F RID: 5983 RVA: 0x0004E13A File Offset: 0x0004C33A
				internal DynamicMetaObject <BindGetMember>b__2_0(DynamicObject.MetaDynamic @this, GetMemberBinder b, DynamicMetaObject e)
				{
					return b.FallbackGetMember(@this, e);
				}

				// Token: 0x06001760 RID: 5984 RVA: 0x0004E144 File Offset: 0x0004C344
				internal DynamicMetaObject <BindDeleteMember>b__4_0(DynamicObject.MetaDynamic @this, DeleteMemberBinder b, DynamicMetaObject e)
				{
					return b.FallbackDeleteMember(@this, e);
				}

				// Token: 0x06001761 RID: 5985 RVA: 0x0004E14E File Offset: 0x0004C34E
				internal DynamicMetaObject <BindConvert>b__5_0(DynamicObject.MetaDynamic @this, ConvertBinder b, DynamicMetaObject e)
				{
					return b.FallbackConvert(@this, e);
				}

				// Token: 0x06001762 RID: 5986 RVA: 0x0004E158 File Offset: 0x0004C358
				internal DynamicMetaObject <BindUnaryOperation>b__10_0(DynamicObject.MetaDynamic @this, UnaryOperationBinder b, DynamicMetaObject e)
				{
					return b.FallbackUnaryOperation(@this, e);
				}

				// Token: 0x04000B87 RID: 2951
				public static readonly DynamicObject.MetaDynamic.<>c <>9 = new DynamicObject.MetaDynamic.<>c();

				// Token: 0x04000B88 RID: 2952
				public static DynamicObject.MetaDynamic.Fallback<GetMemberBinder> <>9__2_0;

				// Token: 0x04000B89 RID: 2953
				public static DynamicObject.MetaDynamic.Fallback<DeleteMemberBinder> <>9__4_0;

				// Token: 0x04000B8A RID: 2954
				public static DynamicObject.MetaDynamic.Fallback<ConvertBinder> <>9__5_0;

				// Token: 0x04000B8B RID: 2955
				public static DynamicObject.MetaDynamic.Fallback<UnaryOperationBinder> <>9__10_0;
			}

			// Token: 0x02000304 RID: 772
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06001763 RID: 5987 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06001764 RID: 5988 RVA: 0x0004E162 File Offset: 0x0004C362
				internal DynamicMetaObject <BindSetMember>b__0(DynamicObject.MetaDynamic @this, SetMemberBinder b, DynamicMetaObject e)
				{
					return b.FallbackSetMember(@this, this.localValue, e);
				}

				// Token: 0x04000B8C RID: 2956
				public DynamicMetaObject localValue;
			}

			// Token: 0x02000305 RID: 773
			[CompilerGenerated]
			private sealed class <>c__DisplayClass6_0
			{
				// Token: 0x06001765 RID: 5989 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass6_0()
				{
				}

				// Token: 0x06001766 RID: 5990 RVA: 0x0004E172 File Offset: 0x0004C372
				internal DynamicMetaObject <BindInvokeMember>b__0(DynamicObject.MetaDynamic @this, GetMemberBinder ignored, DynamicMetaObject e)
				{
					return this.binder.FallbackInvoke(e, this.args, null);
				}

				// Token: 0x04000B8D RID: 2957
				public InvokeMemberBinder binder;

				// Token: 0x04000B8E RID: 2958
				public DynamicMetaObject[] args;
			}

			// Token: 0x02000306 RID: 774
			[CompilerGenerated]
			private sealed class <>c__DisplayClass7_0
			{
				// Token: 0x06001767 RID: 5991 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass7_0()
				{
				}

				// Token: 0x06001768 RID: 5992 RVA: 0x0004E187 File Offset: 0x0004C387
				internal DynamicMetaObject <BindCreateInstance>b__0(DynamicObject.MetaDynamic @this, CreateInstanceBinder b, DynamicMetaObject e)
				{
					return b.FallbackCreateInstance(@this, this.localArgs, e);
				}

				// Token: 0x04000B8F RID: 2959
				public DynamicMetaObject[] localArgs;
			}

			// Token: 0x02000307 RID: 775
			[CompilerGenerated]
			private sealed class <>c__DisplayClass8_0
			{
				// Token: 0x06001769 RID: 5993 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass8_0()
				{
				}

				// Token: 0x0600176A RID: 5994 RVA: 0x0004E197 File Offset: 0x0004C397
				internal DynamicMetaObject <BindInvoke>b__0(DynamicObject.MetaDynamic @this, InvokeBinder b, DynamicMetaObject e)
				{
					return b.FallbackInvoke(@this, this.localArgs, e);
				}

				// Token: 0x04000B90 RID: 2960
				public DynamicMetaObject[] localArgs;
			}

			// Token: 0x02000308 RID: 776
			[CompilerGenerated]
			private sealed class <>c__DisplayClass9_0
			{
				// Token: 0x0600176B RID: 5995 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass9_0()
				{
				}

				// Token: 0x0600176C RID: 5996 RVA: 0x0004E1A7 File Offset: 0x0004C3A7
				internal DynamicMetaObject <BindBinaryOperation>b__0(DynamicObject.MetaDynamic @this, BinaryOperationBinder b, DynamicMetaObject e)
				{
					return b.FallbackBinaryOperation(@this, this.localArg, e);
				}

				// Token: 0x04000B91 RID: 2961
				public DynamicMetaObject localArg;
			}

			// Token: 0x02000309 RID: 777
			[CompilerGenerated]
			private sealed class <>c__DisplayClass11_0
			{
				// Token: 0x0600176D RID: 5997 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass11_0()
				{
				}

				// Token: 0x0600176E RID: 5998 RVA: 0x0004E1B7 File Offset: 0x0004C3B7
				internal DynamicMetaObject <BindGetIndex>b__0(DynamicObject.MetaDynamic @this, GetIndexBinder b, DynamicMetaObject e)
				{
					return b.FallbackGetIndex(@this, this.localIndexes, e);
				}

				// Token: 0x04000B92 RID: 2962
				public DynamicMetaObject[] localIndexes;
			}

			// Token: 0x0200030A RID: 778
			[CompilerGenerated]
			private sealed class <>c__DisplayClass12_0
			{
				// Token: 0x0600176F RID: 5999 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass12_0()
				{
				}

				// Token: 0x06001770 RID: 6000 RVA: 0x0004E1C7 File Offset: 0x0004C3C7
				internal DynamicMetaObject <BindSetIndex>b__0(DynamicObject.MetaDynamic @this, SetIndexBinder b, DynamicMetaObject e)
				{
					return b.FallbackSetIndex(@this, this.localIndexes, this.localValue, e);
				}

				// Token: 0x04000B93 RID: 2963
				public DynamicMetaObject[] localIndexes;

				// Token: 0x04000B94 RID: 2964
				public DynamicMetaObject localValue;
			}

			// Token: 0x0200030B RID: 779
			[CompilerGenerated]
			private sealed class <>c__DisplayClass13_0
			{
				// Token: 0x06001771 RID: 6001 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass13_0()
				{
				}

				// Token: 0x06001772 RID: 6002 RVA: 0x0004E1DD File Offset: 0x0004C3DD
				internal DynamicMetaObject <BindDeleteIndex>b__0(DynamicObject.MetaDynamic @this, DeleteIndexBinder b, DynamicMetaObject e)
				{
					return b.FallbackDeleteIndex(@this, this.localIndexes, e);
				}

				// Token: 0x04000B95 RID: 2965
				public DynamicMetaObject[] localIndexes;
			}
		}
	}
}
