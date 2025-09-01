using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;

namespace Mono.CSharp
{
	// Token: 0x02000282 RID: 642
	internal class AssemblyBuilderMonoSpecific : AssemblyBuilderExtension
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x0009A494 File Offset: 0x00098694
		public AssemblyBuilderMonoSpecific(AssemblyBuilder ab, CompilerContext ctx) : base(ctx)
		{
			this.builder = ab;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0009A4A4 File Offset: 0x000986A4
		public override Module AddModule(string module)
		{
			Module result;
			try
			{
				if (AssemblyBuilderMonoSpecific.adder_method == null)
				{
					AssemblyBuilderMonoSpecific.adder_method = typeof(AssemblyBuilder).GetMethod("AddModule", BindingFlags.Instance | BindingFlags.NonPublic);
				}
				result = (Module)AssemblyBuilderMonoSpecific.adder_method.Invoke(this.builder, new object[]
				{
					module
				});
			}
			catch
			{
				result = base.AddModule(module);
			}
			return result;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0009A514 File Offset: 0x00098714
		public override void AddPermissionRequests(PermissionSet[] permissions)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.add_permission == null)
				{
					AssemblyBuilderMonoSpecific.add_permission = typeof(AssemblyBuilder).GetMethod("AddPermissionRequests", BindingFlags.Instance | BindingFlags.NonPublic);
				}
				AssemblyBuilderMonoSpecific.add_permission.Invoke(this.builder, permissions);
			}
			catch
			{
				base.AddPermissionRequests(permissions);
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0009A574 File Offset: 0x00098774
		public override void AddTypeForwarder(TypeSpec type, Location loc)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.add_type_forwarder == null)
				{
					AssemblyBuilderMonoSpecific.add_type_forwarder = typeof(AssemblyBuilder).GetMethod("AddTypeForwarder", BindingFlags.Instance | BindingFlags.NonPublic);
				}
				AssemblyBuilderMonoSpecific.add_type_forwarder.Invoke(this.builder, new object[]
				{
					type.GetMetaInfo()
				});
			}
			catch
			{
				base.AddTypeForwarder(type, loc);
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0009A5E0 File Offset: 0x000987E0
		public override void DefineWin32IconResource(string fileName)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.win32_icon_define == null)
				{
					AssemblyBuilderMonoSpecific.win32_icon_define = typeof(AssemblyBuilder).GetMethod("DefineIconResource", BindingFlags.Instance | BindingFlags.NonPublic);
				}
				AssemblyBuilderMonoSpecific.win32_icon_define.Invoke(this.builder, new object[]
				{
					fileName
				});
			}
			catch
			{
				base.DefineWin32IconResource(fileName);
			}
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0009A648 File Offset: 0x00098848
		public override void SetAlgorithmId(uint value, Location loc)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.assembly_algorithm == null)
				{
					AssemblyBuilderMonoSpecific.assembly_algorithm = typeof(AssemblyBuilder).GetField("algid", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);
				}
				AssemblyBuilderMonoSpecific.assembly_algorithm.SetValue(this.builder, value);
			}
			catch
			{
				base.SetAlgorithmId(value, loc);
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0009A6B0 File Offset: 0x000988B0
		public override void SetCulture(string culture, Location loc)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.assembly_culture == null)
				{
					AssemblyBuilderMonoSpecific.assembly_culture = typeof(AssemblyBuilder).GetField("culture", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);
				}
				AssemblyBuilderMonoSpecific.assembly_culture.SetValue(this.builder, culture);
			}
			catch
			{
				base.SetCulture(culture, loc);
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0009A710 File Offset: 0x00098910
		public override void SetFlags(uint flags, Location loc)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.assembly_flags == null)
				{
					AssemblyBuilderMonoSpecific.assembly_flags = typeof(AssemblyBuilder).GetField("flags", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);
				}
				AssemblyBuilderMonoSpecific.assembly_flags.SetValue(this.builder, flags);
			}
			catch
			{
				base.SetFlags(flags, loc);
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0009A778 File Offset: 0x00098978
		public override void SetVersion(Version version, Location loc)
		{
			try
			{
				if (AssemblyBuilderMonoSpecific.assembly_version == null)
				{
					AssemblyBuilderMonoSpecific.assembly_version = typeof(AssemblyBuilder).GetField("version", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField);
				}
				AssemblyBuilderMonoSpecific.assembly_version.SetValue(this.builder, version.ToString(4));
			}
			catch
			{
				base.SetVersion(version, loc);
			}
		}

		// Token: 0x04000B7D RID: 2941
		private static MethodInfo adder_method;

		// Token: 0x04000B7E RID: 2942
		private static MethodInfo add_permission;

		// Token: 0x04000B7F RID: 2943
		private static MethodInfo add_type_forwarder;

		// Token: 0x04000B80 RID: 2944
		private static MethodInfo win32_icon_define;

		// Token: 0x04000B81 RID: 2945
		private static FieldInfo assembly_version;

		// Token: 0x04000B82 RID: 2946
		private static FieldInfo assembly_algorithm;

		// Token: 0x04000B83 RID: 2947
		private static FieldInfo assembly_culture;

		// Token: 0x04000B84 RID: 2948
		private static FieldInfo assembly_flags;

		// Token: 0x04000B85 RID: 2949
		private AssemblyBuilder builder;
	}
}
