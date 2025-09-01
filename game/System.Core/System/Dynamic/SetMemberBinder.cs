using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic set member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x02000320 RID: 800
	public abstract class SetMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.SetMemberBinder" />.</summary>
		/// <param name="name">The name of the member to obtain.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0004FB10 File Offset: 0x0004DD10
		protected SetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to obtain.</summary>
		/// <returns>The name of the member to obtain.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0004FB31 File Offset: 0x0004DD31
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
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0004FB39 File Offset: 0x0004DD39
		public bool IgnoreCase
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreCase>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic set member operation.</summary>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="args">An array of arguments of the dynamic set member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001815 RID: 6165 RVA: 0x0004FB44 File Offset: 0x0004DD44
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject value = args[0];
			ContractUtils.RequiresNotNull(value, "args");
			return target.BindSetMember(this, value);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic set member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="value">The value to set to the member.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001817 RID: 6167 RVA: 0x0004FB8E File Offset: 0x0004DD8E
		public DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value)
		{
			return this.FallbackSetMember(target, value, null);
		}

		/// <summary>Performs the binding of the dynamic set member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic set member operation.</param>
		/// <param name="value">The value to set to the member.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001818 RID: 6168
		public abstract DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion);

		// Token: 0x04000BD6 RID: 3030
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000BD7 RID: 3031
		[CompilerGenerated]
		private readonly bool <IgnoreCase>k__BackingField;
	}
}
