using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic get member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200031A RID: 794
	public abstract class GetMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.GetMemberBinder" />.</summary>
		/// <param name="name">The name of the member to obtain.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x060017EF RID: 6127 RVA: 0x0004F956 File Offset: 0x0004DB56
		protected GetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to obtain.</summary>
		/// <returns>The name of the member to obtain.</returns>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0004F977 File Offset: 0x0004DB77
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the case is ignored, otherwise false.</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0004F97F File Offset: 0x0004DB7F
		public bool IgnoreCase
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreCase>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017F3 RID: 6131 RVA: 0x0004F987 File Offset: 0x0004DB87
		public DynamicMetaObject FallbackGetMember(DynamicMetaObject target)
		{
			return this.FallbackGetMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic get member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017F4 RID: 6132
		public abstract DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic get member operation.</summary>
		/// <param name="target">The target of the dynamic get member operation.</param>
		/// <param name="args">An array of arguments of the dynamic get member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017F5 RID: 6133 RVA: 0x0004F991 File Offset: 0x0004DB91
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindGetMember(this);
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000BCF RID: 3023
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000BD0 RID: 3024
		[CompilerGenerated]
		private readonly bool <IgnoreCase>k__BackingField;
	}
}
