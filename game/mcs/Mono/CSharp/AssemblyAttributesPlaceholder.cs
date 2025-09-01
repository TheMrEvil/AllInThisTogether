using System;

namespace Mono.CSharp
{
	// Token: 0x02000102 RID: 258
	internal class AssemblyAttributesPlaceholder : CompilerGeneratedContainer
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x0002F244 File Offset: 0x0002D444
		public AssemblyAttributesPlaceholder(ModuleContainer parent, string outputName) : base(parent, new MemberName(AssemblyAttributesPlaceholder.GetGeneratedName(outputName)), Modifiers.INTERNAL | Modifiers.STATIC)
		{
			this.assembly = new Field(this, new TypeExpression(parent.Compiler.BuiltinTypes.Object, base.Location), Modifiers.PUBLIC | Modifiers.STATIC, new MemberName(AssemblyAttributesPlaceholder.AssemblyFieldName), null);
			base.AddField(this.assembly);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002F2AC File Offset: 0x0002D4AC
		public void AddAssemblyAttribute(MethodSpec ctor, byte[] data)
		{
			this.assembly.SetCustomAttribute(ctor, data);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002F2BB File Offset: 0x0002D4BB
		public static string GetGeneratedName(string outputName)
		{
			return string.Format(AssemblyAttributesPlaceholder.TypeNamePrefix, outputName);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyAttributesPlaceholder()
		{
		}

		// Token: 0x0400063A RID: 1594
		private static readonly string TypeNamePrefix = "<$AssemblyAttributes${0}>";

		// Token: 0x0400063B RID: 1595
		public static readonly string AssemblyFieldName = "attributes";

		// Token: 0x0400063C RID: 1596
		private Field assembly;
	}
}
