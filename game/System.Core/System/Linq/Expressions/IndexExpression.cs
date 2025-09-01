using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents indexing a property or array.</summary>
	// Token: 0x02000260 RID: 608
	[DebuggerTypeProxy(typeof(Expression.IndexExpressionProxy))]
	public sealed class IndexExpression : Expression, IArgumentProvider
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x0003A8F0 File Offset: 0x00038AF0
		internal IndexExpression(Expression instance, PropertyInfo indexer, IReadOnlyList<Expression> arguments)
		{
			indexer == null;
			this.Object = instance;
			this.Indexer = indexer;
			this._arguments = arguments;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0003A915 File Offset: 0x00038B15
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Index;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.IndexExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0003A919 File Offset: 0x00038B19
		public sealed override Type Type
		{
			get
			{
				if (this.Indexer != null)
				{
					return this.Indexer.PropertyType;
				}
				return this.Object.Type.GetElementType();
			}
		}

		/// <summary>An object to index.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the object to index.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0003A945 File Offset: 0x00038B45
		public Expression Object
		{
			[CompilerGenerated]
			get
			{
				return this.<Object>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, returns null otherwise.</summary>
		/// <returns>The <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, otherwise null.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0003A94D File Offset: 0x00038B4D
		public PropertyInfo Indexer
		{
			[CompilerGenerated]
			get
			{
				return this.<Indexer>k__BackingField;
			}
		}

		/// <summary>Gets the arguments that will be used to index the property or array.</summary>
		/// <returns>The read-only collection containing the arguments that will be used to index the property or array.</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0003A955 File Offset: 0x00038B55
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="object">The <see cref="P:System.Linq.Expressions.IndexExpression.Object" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.IndexExpression.Arguments" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060011C5 RID: 4549 RVA: 0x0003A962 File Offset: 0x00038B62
		public IndexExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if ((@object == this.Object & arguments != null) && ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			return Expression.MakeIndex(@object, this.Indexer, arguments);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003A992 File Offset: 0x00038B92
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003A9AD File Offset: 0x00038BAD
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitIndex(this);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003A9B8 File Offset: 0x00038BB8
		internal Expression Rewrite(Expression instance, Expression[] arguments)
		{
			return Expression.MakeIndex(instance, this.Indexer, arguments ?? this._arguments);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0000235B File Offset: 0x0000055B
		internal IndexExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040009F4 RID: 2548
		private IReadOnlyList<Expression> _arguments;

		// Token: 0x040009F5 RID: 2549
		[CompilerGenerated]
		private readonly Expression <Object>k__BackingField;

		// Token: 0x040009F6 RID: 2550
		[CompilerGenerated]
		private readonly PropertyInfo <Indexer>k__BackingField;
	}
}
