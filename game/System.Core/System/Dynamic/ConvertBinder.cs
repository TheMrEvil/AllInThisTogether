using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents the convert dynamic operation at the call site, providing the binding semantic and the details about the operation.</summary>
	// Token: 0x020002F9 RID: 761
	public abstract class ConvertBinder : DynamicMetaObjectBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Dynamic.ConvertBinder" />.</summary>
		/// <param name="type">The type to convert to.</param>
		/// <param name="explicit">Is true if the conversion should consider explicit conversions; otherwise, false.</param>
		// Token: 0x060016EC RID: 5868 RVA: 0x0004CF68 File Offset: 0x0004B168
		protected ConvertBinder(Type type, bool @explicit)
		{
			ContractUtils.RequiresNotNull(type, "type");
			this.Type = type;
			this.Explicit = @explicit;
		}

		/// <summary>The type to convert to.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type to convert to.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0004CF89 File Offset: 0x0004B189
		public Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Gets the value indicating if the conversion should consider explicit conversions.</summary>
		/// <returns>
		///     <see langword="True" /> if there is an explicit conversion, otherwise <see langword="false" />.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0004CF91 File Offset: 0x0004B191
		public bool Explicit
		{
			[CompilerGenerated]
			get
			{
				return this.<Explicit>k__BackingField;
			}
		}

		/// <summary>Performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016EF RID: 5871 RVA: 0x0004CF99 File Offset: 0x0004B199
		public DynamicMetaObject FallbackConvert(DynamicMetaObject target)
		{
			return this.FallbackConvert(target, null);
		}

		/// <summary>When overridden in the derived class, performs the binding of the dynamic convert operation if the target dynamic object cannot bind.</summary>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="errorSuggestion">The binding result to use if binding fails, or null.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016F0 RID: 5872
		public abstract DynamicMetaObject FallbackConvert(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		/// <summary>Performs the binding of the dynamic convert operation.</summary>
		/// <param name="target">The target of the dynamic convert operation.</param>
		/// <param name="args">An array of arguments of the dynamic convert operation.</param>
		/// <returns>The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.</returns>
		// Token: 0x060016F1 RID: 5873 RVA: 0x0004CFA3 File Offset: 0x0004B1A3
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindConvert(this);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00007E1D File Offset: 0x0000601D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		/// <summary>The result type of the operation.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the result type of the operation.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0004CFCC File Offset: 0x0004B1CC
		public sealed override Type ReturnType
		{
			get
			{
				return this.Type;
			}
		}

		// Token: 0x04000B7B RID: 2939
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x04000B7C RID: 2940
		[CompilerGenerated]
		private readonly bool <Explicit>k__BackingField;
	}
}
