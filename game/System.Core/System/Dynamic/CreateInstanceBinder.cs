using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the dynamic create operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x020002FA RID: 762
	public abstract class CreateInstanceBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new intsance of the <see cref="T:System.Dynamic.CreateInstanceBinder" />.</summary>
		/// <param name="callInfo">The signature of the arguments at the call site.</param>
		// Token: 0x060016F4 RID: 5876 RVA: 0x0004CFD4 File Offset: 0x0004B1D4
		protected CreateInstanceBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this.CallInfo = callInfo;
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000374E6 File Offset: 0x000356E6
		public sealed override Type ReturnType
		{
			get
			{
				return typeof(object);
			}
		}

		/// <summary>Gets the signature of the arguments at the call site.</summary>
		/// <returns>The signature of the arguments at the call site.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0004CFEE File Offset: 0x0004B1EE
		public CallInfo CallInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<CallInfo>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016F7 RID: 5879 RVA: 0x0004CFF6 File Offset: 0x0004B1F6
		public DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackCreateInstance(target, args, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic create operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">The arguments of the dynamic create operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016F8 RID: 5880
		public abstract DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic create operation.</summary>
		/// <param name="target">The target of the dynamic create operation.</param>
		/// <param name="args">An array of arguments of the dynamic create operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0004D001 File Offset: 0x0004B201
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindCreateInstance(this, args);
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000B7D RID: 2941
		[CompilerGenerated]
		private readonly CallInfo <CallInfo>k__BackingField;
	}
}
