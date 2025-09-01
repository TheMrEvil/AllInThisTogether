using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the invoke member dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200031E RID: 798
	public abstract class InvokeMemberBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.InvokeMemberBinder" />.</summary>
		/// <param name="name">The name of the member to invoke.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x06001800 RID: 6144 RVA: 0x0004FA07 File Offset: 0x0004DC07
		protected InvokeMemberBinder(string name, bool ignoreCase, CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.Name = name;
			this.IgnoreCase = ignoreCase;
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the name of the member to invoke.</summary>
		/// <returns>The name of the member to invoke.</returns>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0004FA3A File Offset: 0x0004DC3A
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
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0004FA42 File Offset: 0x0004DC42
		public bool IgnoreCase
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreCase>k__BackingField;
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0004FA4A File Offset: 0x0004DC4A
		public CallInfo CallInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CallInfo>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic invoke member operation.</summary>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001805 RID: 6149 RVA: 0x0004FA52 File Offset: 0x0004DC52
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvokeMember(this, args);
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001807 RID: 6151 RVA: 0x0004FA72 File Offset: 0x0004DC72
		public DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvokeMember(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke member operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic invoke member operation.</param>
		/// <param name="args">The arguments of the dynamic invoke member operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001808 RID: 6152
		public abstract DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>When overridden in the derived class, performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001809 RID: 6153
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		// Token: 0x04000BD2 RID: 3026
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000BD3 RID: 3027
		[CompilerGenerated]
		private readonly bool <IgnoreCase>k__BackingField;

		// Token: 0x04000BD4 RID: 3028
		[CompilerGenerated]
		private readonly CallInfo <CallInfo>k__BackingField;
	}
}
