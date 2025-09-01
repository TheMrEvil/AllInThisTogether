using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic delete index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x020002FB RID: 763
	public abstract class DeleteIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.DeleteIndexBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060016FB RID: 5883 RVA: 0x0004D021 File Offset: 0x0004B221
		protected DeleteIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000358FE File Offset: 0x00033AFE
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0004D03B File Offset: 0x0004B23B
		public CallInfo CallInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CallInfo>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic delete index operation.</summary>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="args">An array of arguments of the dynamic delete index operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016FE RID: 5886 RVA: 0x0004D043 File Offset: 0x0004B243
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindDeleteIndex(this, args);
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001700 RID: 5888 RVA: 0x0004D063 File Offset: 0x0004B263
		public DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackDeleteIndex(target, indexes, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic delete index operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic delete index operation.</param>
		/// <param name="indexes">The arguments of the dynamic delete index operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001701 RID: 5889
		public abstract DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);

		// Token: 0x04000B7E RID: 2942
		[CompilerGenerated]
		private readonly CallInfo <CallInfo>k__BackingField;
	}
}
