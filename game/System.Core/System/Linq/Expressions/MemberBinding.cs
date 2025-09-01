using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent bindings that are used to initialize members of a newly created object derive.</summary>
	// Token: 0x02000277 RID: 631
	public abstract class MemberBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.MemberBinding" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Linq.Expressions.MemberBindingType" /> that discriminates the type of binding that is represented.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to be initialized.</param>
		// Token: 0x06001266 RID: 4710 RVA: 0x0003B57E File Offset: 0x0003977E
		[Obsolete("Do not use this constructor. It will be removed in future releases.")]
		protected MemberBinding(MemberBindingType type, MemberInfo member)
		{
			this.BindingType = type;
			this.Member = member;
		}

		/// <summary>Gets the type of binding that is represented.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.MemberBindingType" /> values.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0003B594 File Offset: 0x00039794
		public MemberBindingType BindingType
		{
			[CompilerGenerated]
			get
			{
				return this.<BindingType>k__BackingField;
			}
		}

		/// <summary>Gets the field or property to be initialized.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be initialized.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0003B59C File Offset: 0x0003979C
		public MemberInfo Member
		{
			[CompilerGenerated]
			get
			{
				return this.<Member>k__BackingField;
			}
		}

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</returns>
		// Token: 0x06001269 RID: 4713 RVA: 0x0003B5A4 File Offset: 0x000397A4
		public override string ToString()
		{
			return ExpressionStringBuilder.MemberBindingToString(this);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003B5AC File Offset: 0x000397AC
		internal virtual void ValidateAsDefinedHere(int index)
		{
			throw Error.UnknownBindingType(index);
		}

		// Token: 0x04000A21 RID: 2593
		[CompilerGenerated]
		private readonly MemberBindingType <BindingType>k__BackingField;

		// Token: 0x04000A22 RID: 2594
		[CompilerGenerated]
		private readonly MemberInfo <Member>k__BackingField;
	}
}
