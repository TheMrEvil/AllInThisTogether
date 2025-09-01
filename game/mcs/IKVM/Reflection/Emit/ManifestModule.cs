using System;
using System.Collections.Generic;
using System.IO;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000D5 RID: 213
	internal sealed class ManifestModule : NonPEModule
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x000229CD File Offset: 0x00020BCD
		internal ManifestModule(AssemblyBuilder assembly) : base(assembly.universe)
		{
			this.assembly = assembly;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000229ED File Offset: 0x00020BED
		public override int MDStreamVersion
		{
			get
			{
				return this.assembly.mdStreamVersion;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x000229FA File Offset: 0x00020BFA
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindType(TypeName typeName)
		{
			return null;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000AF70 File Offset: 0x00009170
		internal override void GetTypesImpl(List<Type> list)
		{
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00022A02 File Offset: 0x00020C02
		public override string FullyQualifiedName
		{
			get
			{
				return Path.Combine(this.assembly.dir, "RefEmit_InMemoryManifestModule");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00022A19 File Offset: 0x00020C19
		public override string Name
		{
			get
			{
				return "<In Memory Module>";
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00022A20 File Offset: 0x00020C20
		public override Guid ModuleVersionId
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00022A28 File Offset: 0x00020C28
		public override string ScopeName
		{
			get
			{
				return "RefEmit_InMemoryManifestModule";
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000AF7B File Offset: 0x0000917B
		protected override Exception NotSupportedException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x04000413 RID: 1043
		private readonly AssemblyBuilder assembly;

		// Token: 0x04000414 RID: 1044
		private readonly Guid guid = Guid.NewGuid();
	}
}
