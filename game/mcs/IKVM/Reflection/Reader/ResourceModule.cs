using System;
using System.Collections.Generic;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A5 RID: 165
	internal sealed class ResourceModule : NonPEModule
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0001D5FD File Offset: 0x0001B7FD
		internal ResourceModule(ModuleReader manifest, int index, string location) : base(manifest.universe)
		{
			this.manifest = manifest;
			this.index = index;
			this.location = location;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0000225C File Offset: 0x0000045C
		public override int MDStreamVersion
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsResource()
		{
			return true;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001D620 File Offset: 0x0001B820
		public override Assembly Assembly
		{
			get
			{
				return this.manifest.Assembly;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001D62D File Offset: 0x0001B82D
		public override string FullyQualifiedName
		{
			get
			{
				return this.location ?? "<Unknown>";
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001D63E File Offset: 0x0001B83E
		public override string Name
		{
			get
			{
				if (this.location != null)
				{
					return Path.GetFileName(this.location);
				}
				return "<Unknown>";
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001D659 File Offset: 0x0001B859
		public override string ScopeName
		{
			get
			{
				return this.manifest.GetString(this.manifest.File.records[this.index].Name);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000225C File Offset: 0x0000045C
		public override Guid ModuleVersionId
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001D688 File Offset: 0x0001B888
		public override byte[] __ModuleHash
		{
			get
			{
				int hashValue = this.manifest.File.records[this.index].HashValue;
				if (hashValue != 0)
				{
					return this.manifest.GetBlobCopy(hashValue);
				}
				return Empty<byte>.Array;
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindType(TypeName typeName)
		{
			return null;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000AF70 File Offset: 0x00009170
		internal override void GetTypesImpl(List<Type> list)
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000AF82 File Offset: 0x00009182
		protected override Exception ArgumentOutOfRangeException()
		{
			return new NotSupportedException();
		}

		// Token: 0x04000395 RID: 917
		private readonly ModuleReader manifest;

		// Token: 0x04000396 RID: 918
		private readonly int index;

		// Token: 0x04000397 RID: 919
		private readonly string location;
	}
}
