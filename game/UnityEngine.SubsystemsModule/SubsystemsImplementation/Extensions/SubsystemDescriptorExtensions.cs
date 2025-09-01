using System;

namespace UnityEngine.SubsystemsImplementation.Extensions
{
	// Token: 0x0200001B RID: 27
	public static class SubsystemDescriptorExtensions
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002E6C File Offset: 0x0000106C
		public static SubsystemProxy<TSubsystem, TProvider> CreateProxy<TSubsystem, TProvider>(this SubsystemDescriptorWithProvider<TSubsystem, TProvider> descriptor) where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
		{
			TProvider tprovider = descriptor.CreateProvider();
			return (tprovider != null) ? new SubsystemProxy<TSubsystem, TProvider>(tprovider) : null;
		}
	}
}
