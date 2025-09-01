using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000045 RID: 69
	public struct MetadataBasedRelativeCacheLocationGenerator : IRelativeCacheLocationGenerator
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BB64 File Offset: 0x00009D64
		public static MetadataBasedRelativeCacheLocationGenerator Inferred
		{
			get
			{
				MetadataBasedRelativeCacheLocationGenerator result = default(MetadataBasedRelativeCacheLocationGenerator);
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string company;
				if (executingAssembly == null)
				{
					company = null;
				}
				else
				{
					AssemblyCompanyAttribute customAttribute = executingAssembly.GetCustomAttribute<AssemblyCompanyAttribute>();
					company = ((customAttribute != null) ? customAttribute.Company : null);
				}
				result.Company = company;
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				object obj;
				if (entryAssembly == null)
				{
					obj = null;
				}
				else
				{
					AssemblyProductAttribute customAttribute2 = entryAssembly.GetCustomAttribute<AssemblyProductAttribute>();
					obj = ((customAttribute2 != null) ? customAttribute2.Product : null);
				}
				object product;
				if ((product = obj) == null)
				{
					Assembly entryAssembly2 = Assembly.GetEntryAssembly();
					if (entryAssembly2 == null)
					{
						product = null;
					}
					else
					{
						AssemblyName name = entryAssembly2.GetName();
						product = ((name != null) ? name.Name : null);
					}
				}
				result.Product = product;
				return result;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BBE3 File Offset: 0x00009DE3
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000BBEB File Offset: 0x00009DEB
		public string Company
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Company>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Company>k__BackingField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public string Product
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Product>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Product>k__BackingField = value;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000BC08 File Offset: 0x00009E08
		public string GetRelativeCacheFilePath(IServiceHub serviceHub)
		{
			return Path.Combine(this.Company ?? "Parse", this.Product ?? "_global", (serviceHub.MetadataController.HostManifestData.ShortVersion ?? "1.0.0.0") + ".pc");
		}

		// Token: 0x0400009F RID: 159
		[CompilerGenerated]
		private string <Company>k__BackingField;

		// Token: 0x040000A0 RID: 160
		[CompilerGenerated]
		private string <Product>k__BackingField;
	}
}
