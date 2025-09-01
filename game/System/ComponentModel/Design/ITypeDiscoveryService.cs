using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Discovers available types at design time.</summary>
	// Token: 0x0200046D RID: 1133
	public interface ITypeDiscoveryService
	{
		/// <summary>Retrieves the list of available types.</summary>
		/// <param name="baseType">The base type to match. Can be <see langword="null" />.</param>
		/// <param name="excludeGlobalTypes">Indicates whether types from all referenced assemblies should be checked.</param>
		/// <returns>A collection of types that match the criteria specified by <paramref name="baseType" /> and <paramref name="excludeGlobalTypes" />.</returns>
		// Token: 0x0600247F RID: 9343
		ICollection GetTypes(Type baseType, bool excludeGlobalTypes);
	}
}
