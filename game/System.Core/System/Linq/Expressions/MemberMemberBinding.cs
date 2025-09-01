using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing members of a member of a newly created object.</summary>
	// Token: 0x0200027D RID: 637
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x0003B8AF File Offset: 0x00039AAF
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings) : base(MemberBindingType.MemberBinding, member)
		{
			this.Bindings = bindings;
		}

		/// <summary>Gets the bindings that describe how to initialize the members of a member.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects that describe how to initialize the members of the member.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			[CompilerGenerated]
			get
			{
				return this.<Bindings>k__BackingField;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001290 RID: 4752 RVA: 0x0003B8C8 File Offset: 0x00039AC8
		public MemberMemberBinding Update(IEnumerable<MemberBinding> bindings)
		{
			if (bindings != null && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberBind(base.Member, bindings);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00003A59 File Offset: 0x00001C59
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemberMemberBinding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A29 RID: 2601
		[CompilerGenerated]
		private readonly ReadOnlyCollection<MemberBinding> <Bindings>k__BackingField;
	}
}
