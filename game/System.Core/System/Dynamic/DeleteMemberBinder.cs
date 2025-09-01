using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete member operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x020002FC RID: 764
	public abstract class DeleteMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" />.</summary>
		/// <param name="name">The name of the member to delete.</param>
		/// <param name="ignoreCase">Is true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x06001702 RID: 5890 RVA: 0x0004D06E File Offset: 0x0004B26E
		protected DeleteMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
		}

		/// <summary>Gets the name of the member to delete.</summary>
		/// <returns>The name of the member to delete.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0004D08F File Offset: 0x0004B28F
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Gets the value indicating if the string comparison should ignore the case of the member name.</summary>
		/// <returns>True if the string comparison should ignore the case, otherwise false.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0004D097 File Offset: 0x0004B297
		public bool IgnoreCase
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreCase>k__BackingField;
			}
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x000358FE File Offset: 0x00033AFE
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001706 RID: 5894 RVA: 0x0004D09F File Offset: 0x0004B29F
		public DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target)
		{
			return this.FallbackDeleteMember(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001707 RID: 5895
		public abstract DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic delete member operation.</summary>
		/// <param name="target">The target of the dynamic delete member operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001708 RID: 5896 RVA: 0x0004D0A9 File Offset: 0x0004B2A9
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindDeleteMember(this);
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000B7F RID: 2943
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000B80 RID: 2944
		[CompilerGenerated]
		private readonly bool <IgnoreCase>k__BackingField;
	}
}
