using System;

namespace UnityEngine.SubsystemsImplementation.Extensions
{
	// Token: 0x0200001C RID: 28
	public static class SubsystemExtensions
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00002E98 File Offset: 0x00001098
		public static TProvider GetProvider<TSubsystem, TDescriptor, TProvider>(this SubsystemWithProvider<TSubsystem, TDescriptor, TProvider> subsystem) where TSubsystem : SubsystemWithProvider, new() where TDescriptor : SubsystemDescriptorWithProvider<TSubsystem, TProvider> where TProvider : SubsystemProvider<TSubsystem>
		{
			return subsystem.provider;
		}
	}
}
