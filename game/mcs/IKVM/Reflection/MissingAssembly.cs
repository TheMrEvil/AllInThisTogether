using System;
using System.Collections.Generic;
using System.IO;

namespace IKVM.Reflection
{
	// Token: 0x02000044 RID: 68
	internal sealed class MissingAssembly : Assembly
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00009CC5 File Offset: 0x00007EC5
		internal MissingAssembly(Universe universe, string name) : base(universe)
		{
			this.module = new MissingModule(this, -1);
			this.fullName = name;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override Type[] GetTypes()
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009CEA File Offset: 0x00007EEA
		public override AssemblyName GetName()
		{
			return new AssemblyName(this.fullName);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override string ImageRuntimeVersion
		{
			get
			{
				throw new MissingAssemblyException(this);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009CF7 File Offset: 0x00007EF7
		public override Module ManifestModule
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override MethodInfo EntryPoint
		{
			get
			{
				throw new MissingAssemblyException(this);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override string Location
		{
			get
			{
				throw new MissingAssemblyException(this);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override AssemblyName[] GetReferencedAssemblies()
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override Module[] GetModules(bool getResourceModules)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override Module GetModule(string name)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override string[] GetManifestResourceNames()
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public override Stream GetManifestResourceStream(string resourceName)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool __IsMissing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindType(TypeName typeName)
		{
			return null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009CE2 File Offset: 0x00007EE2
		internal override IList<CustomAttributeData> GetCustomAttributesData(Type attributeType)
		{
			throw new MissingAssemblyException(this);
		}

		// Token: 0x04000170 RID: 368
		private readonly MissingModule module;
	}
}
