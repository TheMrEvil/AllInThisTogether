using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic set index operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200031F RID: 799
	public abstract class SetIndexBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.SetIndexBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x0600180A RID: 6154 RVA: 0x0004FA7D File Offset: 0x0004DC7D
		protected SetIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0004FA97 File Offset: 0x0004DC97
		public CallInfo CallInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CallInfo>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic set index operation.</summary>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="args">An array of arguments of the dynamic set index operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600180D RID: 6157 RVA: 0x0004FAA0 File Offset: 0x0004DCA0
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length >= 2, "args");
			DynamicMetaObject value = args[args.Length - 1];
			DynamicMetaObject[] array = args.RemoveLast<DynamicMetaObject>();
			ContractUtils.RequiresNotNull(value, "args");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(array, "args");
			return target.BindSetIndex(this, array, value);
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>Performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x0600180F RID: 6159 RVA: 0x0004FB04 File Offset: 0x0004DD04
		public DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			return this.FallbackSetIndex(target, indexes, value, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic set index operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic set index operation.</param>
		/// <param name="indexes">The arguments of the dynamic set index operation.</param>
		/// <param name="value">The value to set to the collection.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x06001810 RID: 6160
		public abstract DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value, DynamicMetaObject errorSuggestion);

		// Token: 0x04000BD5 RID: 3029
		[CompilerGenerated]
		private readonly CallInfo <CallInfo>k__BackingField;
	}
}
