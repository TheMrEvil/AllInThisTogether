using System;

namespace Mono.CSharp
{
	// Token: 0x0200015D RID: 349
	public class BuilderContext
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x00047C79 File Offset: 0x00045E79
		public bool HasSet(BuilderContext.Options options)
		{
			return (this.flags & options) == options;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00047C86 File Offset: 0x00045E86
		public BuilderContext.FlagsHandle With(BuilderContext.Options options, bool enable)
		{
			return new BuilderContext.FlagsHandle(this, options, enable ? options : ((BuilderContext.Options)0));
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00002CCC File Offset: 0x00000ECC
		public BuilderContext()
		{
		}

		// Token: 0x04000768 RID: 1896
		protected BuilderContext.Options flags;

		// Token: 0x0200038D RID: 909
		[Flags]
		public enum Options
		{
			// Token: 0x04000F80 RID: 3968
			CheckedScope = 1,
			// Token: 0x04000F81 RID: 3969
			AccurateDebugInfo = 2,
			// Token: 0x04000F82 RID: 3970
			OmitDebugInfo = 4,
			// Token: 0x04000F83 RID: 3971
			ConstructorScope = 8,
			// Token: 0x04000F84 RID: 3972
			AsyncBody = 16
		}

		// Token: 0x0200038E RID: 910
		public struct FlagsHandle : IDisposable
		{
			// Token: 0x060026C2 RID: 9922 RVA: 0x000B6F08 File Offset: 0x000B5108
			public FlagsHandle(BuilderContext ec, BuilderContext.Options flagsToSet)
			{
				this = new BuilderContext.FlagsHandle(ec, flagsToSet, flagsToSet);
			}

			// Token: 0x060026C3 RID: 9923 RVA: 0x000B6F13 File Offset: 0x000B5113
			public FlagsHandle(BuilderContext ec, BuilderContext.Options mask, BuilderContext.Options val)
			{
				this.ec = ec;
				this.invmask = ~mask;
				this.oldval = (ec.flags & mask);
				ec.flags = ((ec.flags & this.invmask) | (val & mask));
			}

			// Token: 0x060026C4 RID: 9924 RVA: 0x000B6F49 File Offset: 0x000B5149
			public void Dispose()
			{
				this.ec.flags = ((this.ec.flags & this.invmask) | this.oldval);
			}

			// Token: 0x04000F85 RID: 3973
			private readonly BuilderContext ec;

			// Token: 0x04000F86 RID: 3974
			private readonly BuilderContext.Options invmask;

			// Token: 0x04000F87 RID: 3975
			private readonly BuilderContext.Options oldval;
		}
	}
}
