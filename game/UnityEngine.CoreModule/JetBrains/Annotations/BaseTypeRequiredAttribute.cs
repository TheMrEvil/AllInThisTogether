using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000C0 RID: 192
	[BaseTypeRequired(typeof(Attribute))]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00005D39 File Offset: 0x00003F39
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00005D4A File Offset: 0x00003F4A
		[NotNull]
		public Type BaseType
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseType>k__BackingField;
			}
		}

		// Token: 0x04000249 RID: 585
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Type <BaseType>k__BackingField;
	}
}
