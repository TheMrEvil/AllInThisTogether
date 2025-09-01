using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the binary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x020002EF RID: 751
	public abstract class BinaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> class.</summary>
		/// <param name="operation">The binary operation kind.</param>
		// Token: 0x060016BC RID: 5820 RVA: 0x0004C864 File Offset: 0x0004AA64
		protected BinaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(BinaryOperationBinder.OperationIsValid(operation), "operation");
			this.Operation = operation;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The result type of the operation.</returns>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>The binary operation kind.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> object representing the kind of binary operation.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0004C883 File Offset: 0x0004AA83
		public ExpressionType Operation
		{
			[CompilerGenerated]
			get
			{
				return this.<Operation>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016BF RID: 5823 RVA: 0x0004C88B File Offset: 0x0004AA8B
		public DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg)
		{
			return this.FallbackBinaryOperation(target, arg, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the binary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic binary operation.</param>
		/// <param name="arg">The right hand side operand of the dynamic binary operation.</param>
		/// <param name="errorSuggestion">The binding result if the binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016C0 RID: 5824
		public abstract DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic binary operation.</summary>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016C1 RID: 5825 RVA: 0x0004C898 File Offset: 0x0004AA98
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject dynamicMetaObject = args[0];
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			return target.BindBinaryOperation(this, dynamicMetaObject);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004C8E4 File Offset: 0x0004AAE4
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Multiply)
			{
				if (operation != ExpressionType.Add && operation != ExpressionType.And)
				{
					switch (operation)
					{
					case ExpressionType.Divide:
					case ExpressionType.Equal:
					case ExpressionType.ExclusiveOr:
					case ExpressionType.GreaterThan:
					case ExpressionType.GreaterThanOrEqual:
					case ExpressionType.LeftShift:
					case ExpressionType.LessThan:
					case ExpressionType.LessThanOrEqual:
					case ExpressionType.Modulo:
					case ExpressionType.Multiply:
						break;
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.ListInit:
					case ExpressionType.MemberAccess:
					case ExpressionType.MemberInit:
						return false;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (operation)
				{
				case ExpressionType.NotEqual:
				case ExpressionType.Or:
				case ExpressionType.Power:
				case ExpressionType.RightShift:
				case ExpressionType.Subtract:
					break;
				case ExpressionType.OrElse:
				case ExpressionType.Parameter:
				case ExpressionType.Quote:
					return false;
				default:
					if (operation != ExpressionType.Extension && operation - ExpressionType.AddAssign > 10)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x04000B68 RID: 2920
		[CompilerGenerated]
		private readonly ExpressionType <Operation>k__BackingField;
	}
}
