using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the unary dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000321 RID: 801
	public abstract class UnaryOperationBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.BinaryOperationBinder" /> class.</summary>
		/// <param name="operation">The unary operation kind.</param>
		// Token: 0x06001819 RID: 6169 RVA: 0x0004FB99 File Offset: 0x0004DD99
		protected UnaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(UnaryOperationBinder.OperationIsValid(operation), "operation");
			this.Operation = operation;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
		public sealed override Type ReturnType
		{
			get
			{
				ExpressionType operation = this.Operation;
				if (operation - ExpressionType.IsTrue <= 1)
				{
					return typeof(bool);
				}
				return typeof(object);
			}
		}

		/// <summary>The unary operation kind.</summary>
		/// <returns>The object of the <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents the unary operation kind.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0004FBE8 File Offset: 0x0004DDE8
		public ExpressionType Operation
		{
			[CompilerGenerated]
			get
			{
				return this.<Operation>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic unary operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600181C RID: 6172 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
		public DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target)
		{
			return this.FallbackUnaryOperation(target, null);
		}

		/// <summary>Performs the binding of the unary dynamic operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic unary operation.</param>
		/// <param name="errorSuggestion">The binding result in case the binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600181D RID: 6173
		public abstract DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic unary operation.</summary>
		/// <param name="target">The target of the dynamic operation.</param>
		/// <param name="args">An array of arguments of the dynamic operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600181E RID: 6174 RVA: 0x0004FBFA File Offset: 0x0004DDFA
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindUnaryOperation(this);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004FC23 File Offset: 0x0004DE23
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Decrement)
			{
				if (operation - ExpressionType.Negate > 1 && operation != ExpressionType.Not && operation != ExpressionType.Decrement)
				{
					return false;
				}
			}
			else if (operation != ExpressionType.Extension && operation != ExpressionType.Increment && operation - ExpressionType.OnesComplement > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000BD8 RID: 3032
		[CompilerGenerated]
		private readonly ExpressionType <Operation>k__BackingField;
	}
}
