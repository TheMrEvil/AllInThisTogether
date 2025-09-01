using System;

namespace System.Configuration
{
	/// <summary>Specifies the options to apply to a property.</summary>
	// Token: 0x0200002C RID: 44
	[Flags]
	public enum ConfigurationPropertyOptions
	{
		/// <summary>Indicates that no option applies to the property.</summary>
		// Token: 0x040000AC RID: 172
		None = 0,
		/// <summary>Indicates that the property is a default collection.</summary>
		// Token: 0x040000AD RID: 173
		IsDefaultCollection = 1,
		/// <summary>Indicates that the property is required.</summary>
		// Token: 0x040000AE RID: 174
		IsRequired = 2,
		/// <summary>Indicates that the property is a collection key.</summary>
		// Token: 0x040000AF RID: 175
		IsKey = 4,
		/// <summary>Indicates whether the type name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		// Token: 0x040000B0 RID: 176
		IsTypeStringTransformationRequired = 8,
		/// <summary>Indicates whether the assembly name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		// Token: 0x040000B1 RID: 177
		IsAssemblyStringTransformationRequired = 16,
		/// <summary>Indicates whether the configuration property's parent configuration section should be queried at serialization time to determine whether the configuration property should be serialized into XML.</summary>
		// Token: 0x040000B2 RID: 178
		IsVersionCheckRequired = 32
	}
}
