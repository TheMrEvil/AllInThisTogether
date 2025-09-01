using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic binding and a binding logic of an object participating in the dynamic binding.</summary>
	// Token: 0x020002FD RID: 765
	public class DynamicMetaObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		// Token: 0x0600170A RID: 5898 RVA: 0x0004D0D2 File Offset: 0x0004B2D2
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			this.Expression = expression;
			this.Restrictions = restrictions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> class.</summary>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <param name="restrictions">The set of binding restrictions under which the binding is valid.</param>
		/// <param name="value">The runtime value represented by the <see cref="T:System.Dynamic.DynamicMetaObject" />.</param>
		// Token: 0x0600170B RID: 5899 RVA: 0x0004D109 File Offset: 0x0004B309
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value) : this(expression, restrictions)
		{
			this._value = value;
		}

		/// <summary>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</summary>
		/// <returns>The expression representing the <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0004D11A File Offset: 0x0004B31A
		public Expression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
		}

		/// <summary>The set of binding restrictions under which the binding is valid.</summary>
		/// <returns>The set of binding restrictions.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0004D122 File Offset: 0x0004B322
		public BindingRestrictions Restrictions
		{
			[CompilerGenerated]
			get
			{
				return this.<Restrictions>k__BackingField;
			}
		}

		/// <summary>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>The runtime value represented by this <see cref="T:System.Dynamic.DynamicMetaObject" />.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0004D12A File Offset: 0x0004B32A
		public object Value
		{
			get
			{
				if (!this.HasValue)
				{
					return null;
				}
				return this._value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value.</summary>
		/// <returns>True if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has the runtime value, otherwise false.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0004D13C File Offset: 0x0004B33C
		public bool HasValue
		{
			get
			{
				return this._value != DynamicMetaObject.s_noValueSentinel;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the runtime value or null if the <see cref="T:System.Dynamic.DynamicMetaObject" /> has no value associated with it.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the runtime value or null.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0004D150 File Offset: 0x0004B350
		public Type RuntimeType
		{
			get
			{
				if (!this.HasValue)
				{
					return null;
				}
				Type type = this.Expression.Type;
				if (type.IsValueType)
				{
					return type;
				}
				object value = this.Value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		/// <summary>Gets the limit type of the <see cref="T:System.Dynamic.DynamicMetaObject" />.</summary>
		/// <returns>
		///     <see cref="P:System.Dynamic.DynamicMetaObject.RuntimeType" /> if runtime value is available, a type of the <see cref="P:System.Dynamic.DynamicMetaObject.Expression" /> otherwise.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0004D18E File Offset: 0x0004B38E
		public Type LimitType
		{
			get
			{
				return this.RuntimeType ?? this.Expression.Type;
			}
		}

		/// <summary>Performs the binding of the dynamic conversion operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.ConvertBinder" /> that represents the details of the dynamic operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001712 RID: 5906 RVA: 0x0004D1A5 File Offset: 0x0004B3A5
		public virtual DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackConvert(this);
		}

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001713 RID: 5907 RVA: 0x0004D1B9 File Offset: 0x0004B3B9
		public virtual DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetMember(this);
		}

		/// <summary>Performs the binding of the dynamic set member operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set member operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001714 RID: 5908 RVA: 0x0004D1CD File Offset: 0x0004B3CD
		public virtual DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetMember(this, value);
		}

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001715 RID: 5909 RVA: 0x0004D1E2 File Offset: 0x0004B3E2
		public virtual DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteMember(this);
		}

		/// <summary>Performs the binding of the dynamic get index operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.GetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the get index operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001716 RID: 5910 RVA: 0x0004D1F6 File Offset: 0x0004B3F6
		public virtual DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.SetIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the set index operation.</param>
		/// <param name="value">The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the value for the set index operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001717 RID: 5911 RVA: 0x0004D20B File Offset: 0x0004B40B
		public virtual DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetIndex(this, indexes, value);
		}

		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - indexes for the delete index operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001718 RID: 5912 RVA: 0x0004D221 File Offset: 0x0004B421
		public virtual DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteIndex(this, indexes);
		}

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeMemberBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke member operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001719 RID: 5913 RVA: 0x0004D236 File Offset: 0x0004B436
		public virtual DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvokeMember(this, args);
		}

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.InvokeBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the invoke operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600171A RID: 5914 RVA: 0x0004D24B File Offset: 0x0004B44B
		public virtual DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvoke(this, args);
		}

		/// <summary>Performs the binding of the dynamic create instance operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.CreateInstanceBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="args">An array of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances - arguments to the create instance operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600171B RID: 5915 RVA: 0x0004D260 File Offset: 0x0004B460
		public virtual DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackCreateInstance(this, args);
		}

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.UnaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600171C RID: 5916 RVA: 0x0004D275 File Offset: 0x0004B475
		public virtual DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackUnaryOperation(this);
		}

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <param name="binder">An instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> that represents the details of the dynamic operation.</param>
		/// <param name="arg">An instance of the <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the right hand side of the binary operation.</param>
		/// <returns>The new <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600171D RID: 5917 RVA: 0x0004D289 File Offset: 0x0004B489
		public virtual DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackBinaryOperation(this, arg);
		}

		/// <summary>Returns the enumeration of all dynamic member names.</summary>
		/// <returns>The list of dynamic member names.</returns>
		// Token: 0x0600171E RID: 5918 RVA: 0x0004D29E File Offset: 0x0004B49E
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return Array.Empty<string>();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
		internal static Expression[] GetExpressions(DynamicMetaObject[] objects)
		{
			ContractUtils.RequiresNotNull(objects, "objects");
			Expression[] array = new Expression[objects.Length];
			for (int i = 0; i < objects.Length; i++)
			{
				DynamicMetaObject dynamicMetaObject = objects[i];
				ContractUtils.RequiresNotNull(dynamicMetaObject, "objects");
				Expression expression = dynamicMetaObject.Expression;
				array[i] = expression;
			}
			return array;
		}

		/// <summary>Creates a meta-object for the specified object.</summary>
		/// <param name="value">The object to get a meta-object for.</param>
		/// <param name="expression">The expression representing this <see cref="T:System.Dynamic.DynamicMetaObject" /> during the dynamic binding process.</param>
		/// <returns>If the given object implements <see cref="T:System.Dynamic.IDynamicMetaObjectProvider" /> and is not a remote object from outside the current AppDomain, returns the object's specific meta-object returned by <see cref="M:System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression)" />. Otherwise a plain new meta-object with no restrictions is created and returned.</returns>
		// Token: 0x06001720 RID: 5920 RVA: 0x0004D2F0 File Offset: 0x0004B4F0
		public static DynamicMetaObject Create(object value, Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = value as IDynamicMetaObjectProvider;
			if (dynamicMetaObjectProvider == null)
			{
				return new DynamicMetaObject(expression, BindingRestrictions.Empty, value);
			}
			DynamicMetaObject metaObject = dynamicMetaObjectProvider.GetMetaObject(expression);
			if (metaObject == null || !metaObject.HasValue || metaObject.Value == null || metaObject.Expression != expression)
			{
				throw Error.InvalidMetaObjectCreated(dynamicMetaObjectProvider.GetType());
			}
			return metaObject;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0004D350 File Offset: 0x0004B550
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicMetaObject()
		{
		}

		/// <summary>Represents an empty array of type <see cref="T:System.Dynamic.DynamicMetaObject" />. This field is read only.</summary>
		// Token: 0x04000B81 RID: 2945
		public static readonly DynamicMetaObject[] EmptyMetaObjects = Array.Empty<DynamicMetaObject>();

		// Token: 0x04000B82 RID: 2946
		private static readonly object s_noValueSentinel = new object();

		// Token: 0x04000B83 RID: 2947
		private readonly object _value = DynamicMetaObject.s_noValueSentinel;

		// Token: 0x04000B84 RID: 2948
		[CompilerGenerated]
		private readonly Expression <Expression>k__BackingField;

		// Token: 0x04000B85 RID: 2949
		[CompilerGenerated]
		private readonly BindingRestrictions <Restrictions>k__BackingField;
	}
}
