using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing the elements of a collection member of a newly created object.</summary>
	// Token: 0x0200027C RID: 636
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x0003B874 File Offset: 0x00039A74
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers) : base(MemberBindingType.ListBinding, member)
		{
			this.Initializers = initializers;
		}

		/// <summary>Gets the element initializers for initializing a collection member of a newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection member with.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0003B885 File Offset: 0x00039A85
		public ReadOnlyCollection<ElementInit> Initializers
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializers>k__BackingField;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x0600128B RID: 4747 RVA: 0x0003B88D File Offset: 0x00039A8D
		public MemberListBinding Update(IEnumerable<ElementInit> initializers)
		{
			if (initializers != null && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListBind(base.Member, initializers);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00003A59 File Offset: 0x00001C59
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemberListBinding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A28 RID: 2600
		[CompilerGenerated]
		private readonly ReadOnlyCollection<ElementInit> <Initializers>k__BackingField;
	}
}
