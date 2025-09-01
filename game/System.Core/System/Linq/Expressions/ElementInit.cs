using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an initializer for a single element of an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
	// Token: 0x02000256 RID: 598
	public sealed class ElementInit : IArgumentProvider
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x00038329 File Offset: 0x00036529
		internal ElementInit(MethodInfo addMethod, ReadOnlyCollection<Expression> arguments)
		{
			this.AddMethod = addMethod;
			this.Arguments = arguments;
		}

		/// <summary>Gets the instance method that is used to add an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that adds an element to a collection.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003833F File Offset: 0x0003653F
		public MethodInfo AddMethod
		{
			[CompilerGenerated]
			get
			{
				return this.<AddMethod>k__BackingField;
			}
		}

		/// <summary>Gets the collection of arguments that are passed to a method that adds an element to an <see cref="T:System.Collections.IEnumerable" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments for a method that adds an element to a collection.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00038347 File Offset: 0x00036547
		public ReadOnlyCollection<Expression> Arguments
		{
			[CompilerGenerated]
			get
			{
				return this.<Arguments>k__BackingField;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003834F File Offset: 0x0003654F
		public Expression GetArgument(int index)
		{
			return this.Arguments[index];
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0003835D File Offset: 0x0003655D
		public int ArgumentCount
		{
			get
			{
				return this.Arguments.Count;
			}
		}

		/// <summary>Returns a textual representation of an <see cref="T:System.Linq.Expressions.ElementInit" /> object.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.ElementInit" /> object.</returns>
		// Token: 0x0600108F RID: 4239 RVA: 0x0003836A File Offset: 0x0003656A
		public override string ToString()
		{
			return ExpressionStringBuilder.ElementInitBindingToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001090 RID: 4240 RVA: 0x00038372 File Offset: 0x00036572
		public ElementInit Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			return Expression.ElementInit(this.AddMethod, arguments);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0000235B File Offset: 0x0000055B
		internal ElementInit()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000991 RID: 2449
		[CompilerGenerated]
		private readonly MethodInfo <AddMethod>k__BackingField;

		// Token: 0x04000992 RID: 2450
		[CompilerGenerated]
		private readonly ReadOnlyCollection<Expression> <Arguments>k__BackingField;
	}
}
