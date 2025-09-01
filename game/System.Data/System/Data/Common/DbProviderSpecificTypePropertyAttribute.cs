using System;
using System.Runtime.CompilerServices;

namespace System.Data.Common
{
	/// <summary>Identifies which provider-specific property in the strongly typed parameter classes is to be used when setting a provider-specific type.</summary>
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderSpecificTypePropertyAttribute" /> class.</summary>
		/// <param name="isProviderSpecificTypeProperty">Specifies whether this property is a provider-specific property.</param>
		// Token: 0x06002D01 RID: 11521 RVA: 0x000BEFC8 File Offset: 0x000BD1C8
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this.IsProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		/// <summary>Indicates whether the attributed property is a provider-specific type.</summary>
		/// <returns>
		///   <see langword="true" /> if the property that this attribute is applied to is a provider-specific type property; otherwise <see langword="false" />.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000BEFD7 File Offset: 0x000BD1D7
		public bool IsProviderSpecificTypeProperty
		{
			[CompilerGenerated]
			get
			{
				return this.<IsProviderSpecificTypeProperty>k__BackingField;
			}
		}

		// Token: 0x04001B8B RID: 7051
		[CompilerGenerated]
		private readonly bool <IsProviderSpecificTypeProperty>k__BackingField;
	}
}
