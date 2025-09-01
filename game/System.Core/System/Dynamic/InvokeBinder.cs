using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the invoke dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x0200031D RID: 797
	public abstract class InvokeBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.InvokeBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060017F9 RID: 6137 RVA: 0x0004F9BA File Offset: 0x0004DBBA
		protected InvokeBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0004F9D4 File Offset: 0x0004DBD4
		public CallInfo CallInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CallInfo>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017FC RID: 6140 RVA: 0x0004F9DC File Offset: 0x0004DBDC
		public DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvoke(target, args, null);
		}

		/// <summary>Performs the binding of the dynamic invoke operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">The arguments of the dynamic invoke operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017FD RID: 6141
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic invoke operation.</summary>
		/// <param name="target">The target of the dynamic invoke operation.</param>
		/// <param name="args">An array of arguments of the dynamic invoke operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060017FE RID: 6142 RVA: 0x0004F9E7 File Offset: 0x0004DBE7
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvoke(this, args);
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000BD1 RID: 3025
		[CompilerGenerated]
		private readonly CallInfo <CallInfo>k__BackingField;
	}
}
