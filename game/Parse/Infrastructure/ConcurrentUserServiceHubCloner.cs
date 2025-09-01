using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Platform.Users;

namespace Parse.Infrastructure
{
	// Token: 0x0200003F RID: 63
	public class ConcurrentUserServiceHubCloner : IServiceHubCloner, IServiceHubMutator
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B2F6 File Offset: 0x000094F6
		public bool Valid
		{
			[CompilerGenerated]
			get
			{
				return this.<Valid>k__BackingField;
			}
		} = 1;

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B2FE File Offset: 0x000094FE
		public IServiceHub BuildHub(in IServiceHub reference, IServiceHubComposer composer, params IServiceHubMutator[] requestedMutators)
		{
			return composer.BuildHub(null, reference, requestedMutators.Concat(new ConcurrentUserServiceHubCloner[]
			{
				this
			}).ToArray<IServiceHubMutator>());
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B31E File Offset: 0x0000951E
		public void Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			target.Cloner = this;
			target.CurrentUserController = new ParseCurrentUserController(new TransientCacheController(), composedHub.ClassController, composedHub.Decoder);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B347 File Offset: 0x00009547
		public ConcurrentUserServiceHubCloner()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B356 File Offset: 0x00009556
		IServiceHub IServiceHubCloner.BuildHub(in IServiceHub reference, IServiceHubComposer composer, IServiceHubMutator[] requestedMutators)
		{
			return this.BuildHub(reference, composer, requestedMutators);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B361 File Offset: 0x00009561
		void IServiceHubMutator.Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			this.Mutate(ref target, composedHub);
		}

		// Token: 0x04000090 RID: 144
		[CompilerGenerated]
		private readonly bool <Valid>k__BackingField;
	}
}
