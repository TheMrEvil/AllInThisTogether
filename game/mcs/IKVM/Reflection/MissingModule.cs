using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000045 RID: 69
	internal sealed class MissingModule : NonPEModule
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00009CFF File Offset: 0x00007EFF
		internal MissingModule(Assembly assembly, int index) : base(assembly.universe)
		{
			this.assembly = assembly;
			this.index = index;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override int MDStreamVersion
		{
			get
			{
				throw new MissingModuleException(this);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00009D23 File Offset: 0x00007F23
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override string FullyQualifiedName
		{
			get
			{
				throw new MissingModuleException(this);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00009D2C File Offset: 0x00007F2C
		public override string Name
		{
			get
			{
				if (this.index == -1)
				{
					throw new MissingModuleException(this);
				}
				return this.assembly.ManifestModule.GetString(this.assembly.ManifestModule.File.records[this.index].Name);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override Guid ModuleVersionId
		{
			get
			{
				throw new MissingModuleException(this);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override string ScopeName
		{
			get
			{
				throw new MissingModuleException(this);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindType(TypeName typeName)
		{
			return null;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009D1B File Offset: 0x00007F1B
		internal override void GetTypesImpl(List<Type> list)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override void __GetDataDirectoryEntry(int index, out int rva, out int length)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override IList<CustomAttributeData> __GetPlaceholderAssemblyCustomAttributes(bool multiple, bool security)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override long __RelativeVirtualAddressToFileOffset(int rva)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override __StandAloneMethodSig __ResolveStandAloneMethodSig(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override int __Subsystem
		{
			get
			{
				throw new MissingModuleException(this);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009D1B File Offset: 0x00007F1B
		internal override void ExportTypes(int fileToken, ModuleBuilder manifestModule)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009D1B File Offset: 0x00007F1B
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			throw new MissingModuleException(this);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool __IsMissing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009D7E File Offset: 0x00007F7E
		protected override Exception InvalidOperationException()
		{
			return new MissingModuleException(this);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009D7E File Offset: 0x00007F7E
		protected override Exception NotSupportedException()
		{
			return new MissingModuleException(this);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00009D7E File Offset: 0x00007F7E
		protected override Exception ArgumentOutOfRangeException()
		{
			return new MissingModuleException(this);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00009D88 File Offset: 0x00007F88
		public override byte[] __ModuleHash
		{
			get
			{
				if (this.index == -1)
				{
					throw new MissingModuleException(this);
				}
				if (this.assembly.ManifestModule.File.records[this.index].HashValue == 0)
				{
					return null;
				}
				ByteReader blob = this.assembly.ManifestModule.GetBlob(this.assembly.ManifestModule.File.records[this.index].HashValue);
				return blob.ReadBytes(blob.Length);
			}
		}

		// Token: 0x04000171 RID: 369
		private readonly Assembly assembly;

		// Token: 0x04000172 RID: 370
		private readonly int index;
	}
}
