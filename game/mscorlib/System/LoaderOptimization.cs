using System;

namespace System
{
	/// <summary>An enumeration used with the <see cref="T:System.LoaderOptimizationAttribute" /> class to specify loader optimizations for an executable.</summary>
	// Token: 0x020001CA RID: 458
	public enum LoaderOptimization
	{
		/// <summary>Ignored by the common language runtime.</summary>
		// Token: 0x0400144E RID: 5198
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DisallowBindings = 4,
		/// <summary>Do not use. This mask selects the domain-related values, screening out the unused <see cref="F:System.LoaderOptimization.DisallowBindings" /> flag.</summary>
		// Token: 0x0400144F RID: 5199
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DomainMask = 3,
		/// <summary>Indicates that the application will probably have many domains that use the same code, and the loader must share maximal internal resources across application domains.</summary>
		// Token: 0x04001450 RID: 5200
		MultiDomain = 2,
		/// <summary>Indicates that the application will probably host unique code in multiple domains, and the loader must share resources across application domains only for globally available (strong-named) assemblies that have been added to the global assembly cache.</summary>
		// Token: 0x04001451 RID: 5201
		MultiDomainHost,
		/// <summary>Indicates that no optimizations for sharing internal resources are specified. If the default domain or hosting interface specified an optimization, then the loader uses that; otherwise, the loader uses <see cref="F:System.LoaderOptimization.SingleDomain" />.</summary>
		// Token: 0x04001452 RID: 5202
		NotSpecified = 0,
		/// <summary>Indicates that the application will probably have a single domain, and loader must not share internal resources across application domains.</summary>
		// Token: 0x04001453 RID: 5203
		SingleDomain
	}
}
