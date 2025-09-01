using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x0200004B RID: 75
	public sealed class RawModule : IDisposable
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000A6F9 File Offset: 0x000088F9
		internal RawModule(ModuleReader module)
		{
			this.module = module;
			this.isManifestModule = (module.Assembly != null);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000A717 File Offset: 0x00008917
		public string Location
		{
			get
			{
				return this.module.FullyQualifiedName;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A724 File Offset: 0x00008924
		public bool IsManifestModule
		{
			get
			{
				return this.isManifestModule;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A72C File Offset: 0x0000892C
		public Guid ModuleVersionId
		{
			get
			{
				return this.module.ModuleVersionId;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A739 File Offset: 0x00008939
		public string ImageRuntimeVersion
		{
			get
			{
				return this.module.__ImageRuntimeVersion;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000A746 File Offset: 0x00008946
		public int MDStreamVersion
		{
			get
			{
				return this.module.MDStreamVersion;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000A753 File Offset: 0x00008953
		private void CheckManifestModule()
		{
			if (!this.IsManifestModule)
			{
				throw new BadImageFormatException("Module does not contain a manifest");
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000A768 File Offset: 0x00008968
		public AssemblyName GetAssemblyName()
		{
			this.CheckManifestModule();
			return this.module.Assembly.GetName();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000A780 File Offset: 0x00008980
		public AssemblyName[] GetReferencedAssemblies()
		{
			return this.module.__GetReferencedAssemblies();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A78D File Offset: 0x0000898D
		public void Dispose()
		{
			if (!this.imported)
			{
				this.module.Dispose();
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A7A2 File Offset: 0x000089A2
		internal AssemblyReader ToAssembly()
		{
			if (this.imported)
			{
				throw new InvalidOperationException();
			}
			this.imported = true;
			return (AssemblyReader)this.module.Assembly;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A7C9 File Offset: 0x000089C9
		internal Module ToModule(Assembly assembly)
		{
			if (this.module.Assembly != null)
			{
				throw new InvalidOperationException();
			}
			this.imported = true;
			this.module.SetAssembly(assembly);
			return this.module;
		}

		// Token: 0x04000189 RID: 393
		private readonly ModuleReader module;

		// Token: 0x0400018A RID: 394
		private readonly bool isManifestModule;

		// Token: 0x0400018B RID: 395
		private bool imported;
	}
}
