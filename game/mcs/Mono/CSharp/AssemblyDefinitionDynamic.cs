using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000281 RID: 641
	public class AssemblyDefinitionDynamic : AssemblyDefinition
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x0009A327 File Offset: 0x00098527
		public AssemblyDefinitionDynamic(ModuleContainer module, string name) : base(module, name)
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0009A331 File Offset: 0x00098531
		public AssemblyDefinitionDynamic(ModuleContainer module, string name, string fileName) : base(module, name, fileName)
		{
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0009A33C File Offset: 0x0009853C
		public Module IncludeModule(string moduleFile)
		{
			return this.builder_extra.AddModule(moduleFile);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0009A34A File Offset: 0x0009854A
		public override ModuleBuilder CreateModuleBuilder()
		{
			if (this.file_name == null)
			{
				return this.Builder.DefineDynamicModule(base.Name, false);
			}
			return base.CreateModuleBuilder();
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0009A370 File Offset: 0x00098570
		public bool Create(AppDomain domain, AssemblyBuilderAccess access)
		{
			base.ResolveAssemblySecurityAttributes();
			AssemblyName name = base.CreateAssemblyName();
			this.Builder = ((this.file_name == null) ? domain.DefineDynamicAssembly(name, access) : domain.DefineDynamicAssembly(name, access, AssemblyDefinitionDynamic.Dirname(this.file_name)));
			this.module.Create(this, this.CreateModuleBuilder());
			this.builder_extra = new AssemblyBuilderMonoSpecific(this.Builder, base.Compiler);
			return true;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0009A3E0 File Offset: 0x000985E0
		private static string Dirname(string name)
		{
			int num = name.LastIndexOf('/');
			if (num != -1)
			{
				return name.Substring(0, num);
			}
			num = name.LastIndexOf('\\');
			if (num != -1)
			{
				return name.Substring(0, num);
			}
			return ".";
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0009A420 File Offset: 0x00098620
		protected override void SaveModule(PortableExecutableKinds pekind, ImageFileMachine machine)
		{
			try
			{
				typeof(AssemblyBuilder).GetProperty("IsModuleOnly", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetSetMethod(true).Invoke(this.Builder, new object[]
				{
					true
				});
			}
			catch
			{
				base.SaveModule(pekind, machine);
			}
			this.Builder.Save(this.file_name, pekind, machine);
		}
	}
}
