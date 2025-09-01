using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>The dynamic call site binder that participates in the <see cref="T:System.Dynamic.DynamicMetaObject" /> binding protocol.</summary>
	// Token: 0x020002FE RID: 766
	public abstract class DynamicMetaObjectBinder : CallSiteBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObjectBinder" /> class.</summary>
		// Token: 0x06001722 RID: 5922 RVA: 0x0004D366 File Offset: 0x0004B566
		protected DynamicMetaObjectBinder()
		{
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000374E6 File Offset: 0x000356E6
		public virtual Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Performs the runtime binding of the dynamic operation on a set of arguments.</summary>
		/// <param name="args">An array of arguments to the dynamic operation.</param>
		/// <param name="parameters">The array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> instances that represent the parameters of the call site in the binding process.</param>
		/// <param name="returnLabel">A LabelTarget used to return the result of the dynamic binding.</param>
		/// <returns>An Expression that performs tests on the dynamic operation arguments, and performs the dynamic operation if the tests are valid. If the tests fail on subsequent occurrences of the dynamic operation, Bind will be called again to produce a new <see cref="T:System.Linq.Expressions.Expression" /> for the new argument types.</returns>
		// Token: 0x06001724 RID: 5924 RVA: 0x0004D370 File Offset: 0x0004B570
		public sealed override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
		{
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.RequiresNotNull(parameters, "parameters");
			ContractUtils.RequiresNotNull(returnLabel, "returnLabel");
			if (args.Length == 0)
			{
				throw Error.OutOfRange("args.Length", 1);
			}
			if (parameters.Count == 0)
			{
				throw Error.OutOfRange("parameters.Count", 1);
			}
			if (args.Length != parameters.Count)
			{
				throw new ArgumentOutOfRangeException("args");
			}
			Type type;
			if (this.IsStandardBinder)
			{
				type = this.ReturnType;
				if (returnLabel.Type != typeof(void) && !TypeUtils.AreReferenceAssignable(returnLabel.Type, type))
				{
					throw Error.BinderNotCompatibleWithCallSite(type, this, returnLabel.Type);
				}
			}
			else
			{
				type = returnLabel.Type;
			}
			DynamicMetaObject dynamicMetaObject = DynamicMetaObject.Create(args[0], parameters[0]);
			DynamicMetaObject[] args2 = DynamicMetaObjectBinder.CreateArgumentMetaObjects(args, parameters);
			DynamicMetaObject dynamicMetaObject2 = this.Bind(dynamicMetaObject, args2);
			if (dynamicMetaObject2 == null)
			{
				throw Error.BindingCannotBeNull();
			}
			Expression expression = dynamicMetaObject2.Expression;
			BindingRestrictions restrictions = dynamicMetaObject2.Restrictions;
			if (type != typeof(void) && !TypeUtils.AreReferenceAssignable(type, expression.Type))
			{
				if (dynamicMetaObject.Value is IDynamicMetaObjectProvider)
				{
					throw Error.DynamicObjectResultNotAssignable(expression.Type, dynamicMetaObject.Value.GetType(), this, type);
				}
				throw Error.DynamicBinderResultNotAssignable(expression.Type, this, type);
			}
			else
			{
				if (this.IsStandardBinder && args[0] is IDynamicMetaObjectProvider && restrictions == BindingRestrictions.Empty)
				{
					throw Error.DynamicBindingNeedsRestrictions(dynamicMetaObject.Value.GetType(), this);
				}
				if (expression.NodeType != ExpressionType.Goto)
				{
					expression = Expression.Return(returnLabel, expression);
				}
				if (restrictions != BindingRestrictions.Empty)
				{
					expression = Expression.IfThen(restrictions.ToExpression(), expression);
				}
				return expression;
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004D510 File Offset: 0x0004B710
		private static DynamicMetaObject[] CreateArgumentMetaObjects(object[] args, ReadOnlyCollection<ParameterExpression> parameters)
		{
			DynamicMetaObject[] array;
			if (args.Length != 1)
			{
				array = new DynamicMetaObject[args.Length - 1];
				for (int i = 1; i < args.Length; i++)
				{
					array[i - 1] = DynamicMetaObject.Create(args[i], parameters[i]);
				}
			}
			else
			{
				array = DynamicMetaObject.EmptyMetaObjects;
			}
			return array;
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic operation.</summary>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001726 RID: 5926
		public abstract DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args);

		/// <summary>Gets an expression that will cause the binding to be updated. It indicates that the expression's binding is no longer valid. This is typically used when the "version" of a dynamic object has changed.</summary>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of the resulting expression; any type is allowed.</param>
		/// <returns>The update expression.</returns>
		// Token: 0x06001727 RID: 5927 RVA: 0x0004D559 File Offset: 0x0004B759
		public Expression GetUpdateExpression(Type type)
		{
			return Expression.Goto(CallSiteBinder.UpdateLabel, type);
		}

		/// <summary>Defers the binding of the operation until later time when the runtime values of all dynamic operation arguments have been computed.</summary>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001728 RID: 5928 RVA: 0x0004D568 File Offset: 0x0004B768
		public DynamicMetaObject Defer(DynamicMetaObject target, params DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			if (args == null)
			{
				return this.MakeDeferred(target.Restrictions, new DynamicMetaObject[]
				{
					target
				});
			}
			return this.MakeDeferred(target.Restrictions.Merge(BindingRestrictions.Combine(args)), args.AddFirst(target));
		}

		/// <summary>Defers the binding of the operation until later time when the runtime values of all dynamic operation arguments have been computed.</summary>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001729 RID: 5929 RVA: 0x0004D5B8 File Offset: 0x0004B7B8
		public DynamicMetaObject Defer(params DynamicMetaObject[] args)
		{
			return this.MakeDeferred(BindingRestrictions.Combine(args), args);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0004D5C8 File Offset: 0x0004B7C8
		private DynamicMetaObject MakeDeferred(BindingRestrictions rs, params DynamicMetaObject[] args)
		{
			Expression[] expressions = DynamicMetaObject.GetExpressions(args);
			Type delegateType = DelegateHelpers.MakeDeferredSiteDelegate(args, this.ReturnType);
			return new DynamicMetaObject(DynamicExpression.Make(this.ReturnType, delegateType, this, new TrueReadOnlyCollection<Expression>(expressions)), rs);
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000023D1 File Offset: 0x000005D1
		internal virtual bool IsStandardBinder
		{
			get
			{
				return false;
			}
		}
	}
}
