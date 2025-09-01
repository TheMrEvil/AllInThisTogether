using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000015 RID: 21
	public class SubsystemDescriptorWithProvider<TSubsystem, TProvider> : SubsystemDescriptorWithProvider where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002B0E File Offset: 0x00000D0E
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B1C File Offset: 0x00000D1C
		public TSubsystem Create()
		{
			TSubsystem tsubsystem = SubsystemManager.FindStandaloneSubsystemByDescriptor(this) as TSubsystem;
			bool flag = tsubsystem != null;
			TSubsystem result;
			if (flag)
			{
				result = tsubsystem;
			}
			else
			{
				TProvider tprovider = this.CreateProvider();
				bool flag2 = tprovider == null;
				if (flag2)
				{
					result = default(TSubsystem);
				}
				else
				{
					tsubsystem = ((base.subsystemTypeOverride != null) ? ((TSubsystem)((object)Activator.CreateInstance(base.subsystemTypeOverride))) : Activator.CreateInstance<TSubsystem>());
					tsubsystem.Initialize(this, tprovider);
					SubsystemManager.AddStandaloneSubsystem(tsubsystem);
					result = tsubsystem;
				}
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002BB4 File Offset: 0x00000DB4
		internal sealed override void ThrowIfInvalid()
		{
			bool flag = base.providerType == null;
			if (flag)
			{
				throw new InvalidOperationException("Invalid descriptor - must supply a valid providerType field!");
			}
			bool flag2 = !base.providerType.IsSubclassOf(typeof(TProvider));
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Can't create provider - providerType '{0}' is not a subclass of '{1}'!", base.providerType.ToString(), typeof(TProvider).ToString()));
			}
			bool flag3 = base.subsystemTypeOverride != null && !base.subsystemTypeOverride.IsSubclassOf(typeof(TSubsystem));
			if (flag3)
			{
				throw new InvalidOperationException(string.Format("Can't create provider - subsystemTypeOverride '{0}' is not a subclass of '{1}'!", base.subsystemTypeOverride.ToString(), typeof(TSubsystem).ToString()));
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C74 File Offset: 0x00000E74
		internal TProvider CreateProvider()
		{
			TProvider tprovider = (TProvider)((object)Activator.CreateInstance(base.providerType));
			return tprovider.TryInitialize() ? tprovider : default(TProvider);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public SubsystemDescriptorWithProvider()
		{
		}
	}
}
