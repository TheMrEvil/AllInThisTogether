using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200017A RID: 378
	internal abstract class ImportedDefinition : IMemberDefinition
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0004C35B File Offset: 0x0004A55B
		protected ImportedDefinition(MemberInfo provider, MetadataImporter importer)
		{
			this.provider = provider;
			this.importer = importer;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0000212D File Offset: 0x0000032D
		public bool IsImported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004C371 File Offset: 0x0004A571
		public virtual string Name
		{
			get
			{
				return this.provider.Name;
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004C37E File Offset: 0x0004A57E
		public string[] ConditionalConditions()
		{
			if (this.cattrs == null)
			{
				this.ReadAttributes();
			}
			return this.cattrs.Conditionals;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004C399 File Offset: 0x0004A599
		public ObsoleteAttribute GetAttributeObsolete()
		{
			if (this.cattrs == null)
			{
				this.ReadAttributes();
			}
			return this.cattrs.Obsolete;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0004C3B4 File Offset: 0x0004A5B4
		public bool? CLSAttributeValue
		{
			get
			{
				if (this.cattrs == null)
				{
					this.ReadAttributes();
				}
				return this.cattrs.CLSAttributeValue;
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004C3CF File Offset: 0x0004A5CF
		protected void ReadAttributes()
		{
			this.cattrs = ImportedDefinition.AttributesBag.Read(this.provider, this.importer);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0000AF70 File Offset: 0x00009170
		public void SetIsAssigned()
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0000AF70 File Offset: 0x00009170
		public void SetIsUsed()
		{
		}

		// Token: 0x040007AF RID: 1967
		protected readonly MemberInfo provider;

		// Token: 0x040007B0 RID: 1968
		protected ImportedDefinition.AttributesBag cattrs;

		// Token: 0x040007B1 RID: 1969
		protected readonly MetadataImporter importer;

		// Token: 0x02000398 RID: 920
		protected class AttributesBag
		{
			// Token: 0x060026D5 RID: 9941 RVA: 0x000022F4 File Offset: 0x000004F4
			private static bool HasMissingType(ConstructorInfo ctor)
			{
				return false;
			}

			// Token: 0x060026D6 RID: 9942 RVA: 0x000B732C File Offset: 0x000B552C
			public static ImportedDefinition.AttributesBag Read(MemberInfo mi, MetadataImporter importer)
			{
				ImportedDefinition.AttributesBag attributesBag = null;
				List<string> list = null;
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(mi))
				{
					Type declaringType = customAttributeData.Constructor.DeclaringType;
					string name = declaringType.Name;
					if (name == "ObsoleteAttribute")
					{
						if (!(declaringType.Namespace != "System"))
						{
							if (attributesBag == null)
							{
								attributesBag = new ImportedDefinition.AttributesBag();
							}
							IList<CustomAttributeTypedArgument> constructorArguments = customAttributeData.ConstructorArguments;
							if (constructorArguments.Count == 1)
							{
								attributesBag.Obsolete = new ObsoleteAttribute((string)constructorArguments[0].Value);
							}
							else if (constructorArguments.Count == 2)
							{
								attributesBag.Obsolete = new ObsoleteAttribute((string)constructorArguments[0].Value, (bool)constructorArguments[1].Value);
							}
							else
							{
								attributesBag.Obsolete = new ObsoleteAttribute();
							}
						}
					}
					else if (name == "ConditionalAttribute")
					{
						if (!(declaringType.Namespace != "System.Diagnostics"))
						{
							if (attributesBag == null)
							{
								attributesBag = new ImportedDefinition.AttributesBag();
							}
							if (list == null)
							{
								list = new List<string>(2);
							}
							list.Add((string)customAttributeData.ConstructorArguments[0].Value);
						}
					}
					else if (name == "CLSCompliantAttribute")
					{
						if (!(declaringType.Namespace != "System"))
						{
							if (attributesBag == null)
							{
								attributesBag = new ImportedDefinition.AttributesBag();
							}
							attributesBag.CLSAttributeValue = new bool?((bool)customAttributeData.ConstructorArguments[0].Value);
						}
					}
					else if (mi.MemberType == MemberTypes.TypeInfo || mi.MemberType == MemberTypes.NestedType)
					{
						if (name == "DefaultMemberAttribute")
						{
							if (!(declaringType.Namespace != "System.Reflection"))
							{
								if (attributesBag == null)
								{
									attributesBag = new ImportedDefinition.AttributesBag();
								}
								attributesBag.DefaultIndexerName = (string)customAttributeData.ConstructorArguments[0].Value;
							}
						}
						else
						{
							if (name == "AttributeUsageAttribute")
							{
								if (declaringType.Namespace != "System" || ImportedDefinition.AttributesBag.HasMissingType(customAttributeData.Constructor))
								{
									continue;
								}
								if (attributesBag == null)
								{
									attributesBag = new ImportedDefinition.AttributesBag();
								}
								attributesBag.AttributeUsage = new AttributeUsageAttribute((AttributeTargets)customAttributeData.ConstructorArguments[0].Value);
								using (IEnumerator<CustomAttributeNamedArgument> enumerator2 = customAttributeData.NamedArguments.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										CustomAttributeNamedArgument customAttributeNamedArgument = enumerator2.Current;
										if (customAttributeNamedArgument.MemberInfo.Name == "AllowMultiple")
										{
											attributesBag.AttributeUsage.AllowMultiple = (bool)customAttributeNamedArgument.TypedValue.Value;
										}
										else if (customAttributeNamedArgument.MemberInfo.Name == "Inherited")
										{
											attributesBag.AttributeUsage.Inherited = (bool)customAttributeNamedArgument.TypedValue.Value;
										}
									}
									continue;
								}
							}
							if (name == "CoClassAttribute" && !(declaringType.Namespace != "System.Runtime.InteropServices") && !ImportedDefinition.AttributesBag.HasMissingType(customAttributeData.Constructor))
							{
								if (attributesBag == null)
								{
									attributesBag = new ImportedDefinition.AttributesBag();
								}
								attributesBag.CoClass = importer.ImportType((Type)customAttributeData.ConstructorArguments[0].Value);
							}
						}
					}
				}
				if (attributesBag == null)
				{
					return ImportedDefinition.AttributesBag.Default;
				}
				if (list != null)
				{
					attributesBag.Conditionals = list.ToArray();
				}
				return attributesBag;
			}

			// Token: 0x060026D7 RID: 9943 RVA: 0x00002CCC File Offset: 0x00000ECC
			public AttributesBag()
			{
			}

			// Token: 0x060026D8 RID: 9944 RVA: 0x000B7714 File Offset: 0x000B5914
			// Note: this type is marked as 'beforefieldinit'.
			static AttributesBag()
			{
			}

			// Token: 0x04000F9F RID: 3999
			public static readonly ImportedDefinition.AttributesBag Default = new ImportedDefinition.AttributesBag();

			// Token: 0x04000FA0 RID: 4000
			public AttributeUsageAttribute AttributeUsage;

			// Token: 0x04000FA1 RID: 4001
			public ObsoleteAttribute Obsolete;

			// Token: 0x04000FA2 RID: 4002
			public string[] Conditionals;

			// Token: 0x04000FA3 RID: 4003
			public string DefaultIndexerName;

			// Token: 0x04000FA4 RID: 4004
			public bool? CLSAttributeValue;

			// Token: 0x04000FA5 RID: 4005
			public TypeSpec CoClass;
		}
	}
}
