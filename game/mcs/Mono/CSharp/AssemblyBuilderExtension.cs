using System;
using System.Reflection;
using System.Security;

namespace Mono.CSharp
{
	// Token: 0x02000103 RID: 259
	public class AssemblyBuilderExtension
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0002F2DE File Offset: 0x0002D4DE
		public AssemblyBuilderExtension(CompilerContext ctx)
		{
			this.ctx = ctx;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002F2ED File Offset: 0x0002D4ED
		public virtual Module AddModule(string module)
		{
			this.ctx.Report.RuntimeMissingSupport(Location.Null, "-addmodule");
			return null;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002F30A File Offset: 0x0002D50A
		public virtual void AddPermissionRequests(PermissionSet[] permissions)
		{
			this.ctx.Report.RuntimeMissingSupport(Location.Null, "assembly declarative security");
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002F326 File Offset: 0x0002D526
		public virtual void AddTypeForwarder(TypeSpec type, Location loc)
		{
			this.ctx.Report.RuntimeMissingSupport(loc, "TypeForwardedToAttribute");
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002F33E File Offset: 0x0002D53E
		public virtual void DefineWin32IconResource(string fileName)
		{
			this.ctx.Report.RuntimeMissingSupport(Location.Null, "-win32icon");
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002F35A File Offset: 0x0002D55A
		public virtual void SetAlgorithmId(uint value, Location loc)
		{
			this.ctx.Report.RuntimeMissingSupport(loc, "AssemblyAlgorithmIdAttribute");
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F372 File Offset: 0x0002D572
		public virtual void SetCulture(string culture, Location loc)
		{
			this.ctx.Report.RuntimeMissingSupport(loc, "AssemblyCultureAttribute");
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002F38A File Offset: 0x0002D58A
		public virtual void SetFlags(uint flags, Location loc)
		{
			this.ctx.Report.RuntimeMissingSupport(loc, "AssemblyFlagsAttribute");
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002F3A2 File Offset: 0x0002D5A2
		public virtual void SetVersion(Version version, Location loc)
		{
			this.ctx.Report.RuntimeMissingSupport(loc, "AssemblyVersionAttribute");
		}

		// Token: 0x0400063D RID: 1597
		private readonly CompilerContext ctx;
	}
}
