using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	/// <summary>Defines the compatibility guarantee of a component, type, or type member that may span multiple versions.</summary>
	// Token: 0x0200063D RID: 1597
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		/// <summary>Gets a value that indicates the guaranteed level of compatibility of a library, type, or type member that spans multiple versions.</summary>
		/// <returns>One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000D114F File Offset: 0x000CF34F
		public ComponentGuaranteesOptions Guarantees
		{
			[CompilerGenerated]
			get
			{
				return this.<Guarantees>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ComponentGuaranteesAttribute" /> class with a value that indicates a library, type, or member's guaranteed level of compatibility across multiple versions.</summary>
		/// <param name="guarantees">One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</param>
		// Token: 0x06003C2A RID: 15402 RVA: 0x000D1157 File Offset: 0x000CF357
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this.Guarantees = guarantees;
		}

		// Token: 0x040026F0 RID: 9968
		[CompilerGenerated]
		private readonly ComponentGuaranteesOptions <Guarantees>k__BackingField;
	}
}
