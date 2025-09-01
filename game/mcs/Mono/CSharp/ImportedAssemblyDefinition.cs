using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017C RID: 380
	public class ImportedAssemblyDefinition : IAssemblyDefinition
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0004C4E0 File Offset: 0x0004A6E0
		public ImportedAssemblyDefinition(Assembly assembly)
		{
			this.assembly = assembly;
			this.aname = assembly.GetName();
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004C4FB File Offset: 0x0004A6FB
		public Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004C503 File Offset: 0x0004A703
		public string FullName
		{
			get
			{
				return this.aname.FullName;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004C510 File Offset: 0x0004A710
		public bool HasStrongName
		{
			get
			{
				return this.aname.GetPublicKey().Length != 0;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004C521 File Offset: 0x0004A721
		public bool IsCLSCompliant
		{
			get
			{
				return this.cls_compliant;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004C529 File Offset: 0x0004A729
		public string Location
		{
			get
			{
				return this.assembly.Location;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004C536 File Offset: 0x0004A736
		public string Name
		{
			get
			{
				return this.aname.Name;
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004C543 File Offset: 0x0004A743
		public byte[] GetPublicKeyToken()
		{
			return this.aname.GetPublicKeyToken();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0004C550 File Offset: 0x0004A750
		public AssemblyName GetAssemblyVisibleToName(IAssemblyDefinition assembly)
		{
			return this.internals_visible_to_cache[assembly];
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004C560 File Offset: 0x0004A760
		public bool IsFriendAssemblyTo(IAssemblyDefinition assembly)
		{
			if (this.internals_visible_to == null)
			{
				return false;
			}
			AssemblyName assemblyName = null;
			if (this.internals_visible_to_cache == null)
			{
				this.internals_visible_to_cache = new Dictionary<IAssemblyDefinition, AssemblyName>();
			}
			else if (this.internals_visible_to_cache.TryGetValue(assembly, out assemblyName))
			{
				return assemblyName != null;
			}
			byte[] array = assembly.GetPublicKeyToken();
			if (array != null && array.Length == 0)
			{
				array = null;
			}
			foreach (AssemblyName assemblyName2 in this.internals_visible_to)
			{
				if (!(assemblyName2.Name != assembly.Name))
				{
					if (array == null && assembly is AssemblyDefinition)
					{
						assemblyName = assemblyName2;
						break;
					}
					if (ArrayComparer.IsEqual<byte>(array, assemblyName2.GetPublicKeyToken()))
					{
						assemblyName = assemblyName2;
						break;
					}
				}
			}
			this.internals_visible_to_cache.Add(assembly, assemblyName);
			return assemblyName != null;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004C638 File Offset: 0x0004A838
		public void ReadAttributes()
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(this.assembly))
			{
				Type declaringType = customAttributeData.Constructor.DeclaringType;
				string name = declaringType.Name;
				if (name == "CLSCompliantAttribute")
				{
					if (declaringType.Namespace == "System")
					{
						this.cls_compliant = (bool)customAttributeData.ConstructorArguments[0].Value;
					}
				}
				else if (name == "InternalsVisibleToAttribute" && !(declaringType.Namespace != MetadataImporter.CompilerServicesNamespace))
				{
					string text = customAttributeData.ConstructorArguments[0].Value as string;
					if (text != null)
					{
						AssemblyName item;
						try
						{
							item = new AssemblyName(text);
						}
						catch (FileLoadException)
						{
							continue;
						}
						if (this.internals_visible_to == null)
						{
							this.internals_visible_to = new List<AssemblyName>();
						}
						this.internals_visible_to.Add(item);
					}
				}
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004C75C File Offset: 0x0004A95C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040007B4 RID: 1972
		private readonly Assembly assembly;

		// Token: 0x040007B5 RID: 1973
		private readonly AssemblyName aname;

		// Token: 0x040007B6 RID: 1974
		private bool cls_compliant;

		// Token: 0x040007B7 RID: 1975
		private List<AssemblyName> internals_visible_to;

		// Token: 0x040007B8 RID: 1976
		private Dictionary<IAssemblyDefinition, AssemblyName> internals_visible_to_cache;
	}
}
