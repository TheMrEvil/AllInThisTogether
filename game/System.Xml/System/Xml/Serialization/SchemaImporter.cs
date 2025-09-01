using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Describes a schema importer.</summary>
	// Token: 0x020002A4 RID: 676
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SchemaImporter
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x00091864 File Offset: 0x0008FA64
		internal SchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, CodeDomProvider codeProvider, ImportContext context)
		{
			if (!schemas.Contains("http://www.w3.org/2001/XMLSchema"))
			{
				schemas.AddReference(XmlSchemas.XsdSchema);
				schemas.SchemaSet.Add(XmlSchemas.XsdSchema);
			}
			if (!schemas.Contains("http://www.w3.org/XML/1998/namespace"))
			{
				schemas.AddReference(XmlSchemas.XmlSchema);
				schemas.SchemaSet.Add(XmlSchemas.XmlSchema);
			}
			this.schemas = schemas;
			this.options = options;
			this.codeProvider = codeProvider;
			this.context = context;
			this.Schemas.SetCache(this.Context.Cache, this.Context.ShareTypes);
			SchemaImporterExtensionsSection schemaImporterExtensionsSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SchemaImporterExtensionsSectionPath) as SchemaImporterExtensionsSection;
			if (schemaImporterExtensionsSection != null)
			{
				this.extensions = schemaImporterExtensionsSection.SchemaImporterExtensionsInternal;
				return;
			}
			this.extensions = new SchemaImporterExtensionCollection();
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00091932 File Offset: 0x0008FB32
		internal ImportContext Context
		{
			get
			{
				if (this.context == null)
				{
					this.context = new ImportContext();
				}
				return this.context;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0009194D File Offset: 0x0008FB4D
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		/// <summary>Gets a collection of schema importer extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> containing a collection of schema importer extensions.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00091968 File Offset: 0x0008FB68
		public SchemaImporterExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new SchemaImporterExtensionCollection();
				}
				return this.extensions;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00091983 File Offset: 0x0008FB83
		internal Hashtable ImportedElements
		{
			get
			{
				return this.Context.Elements;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00091990 File Offset: 0x0008FB90
		internal Hashtable ImportedMappings
		{
			get
			{
				return this.Context.Mappings;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0009199D File Offset: 0x0008FB9D
		internal CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this.Context.TypeIdentifiers;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x000919AA File Offset: 0x0008FBAA
		internal XmlSchemas Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemas();
				}
				return this.schemas;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x000919C5 File Offset: 0x0008FBC5
		internal TypeScope Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new TypeScope();
				}
				return this.scope;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x000919E0 File Offset: 0x0008FBE0
		internal NameTable GroupsInUse
		{
			get
			{
				if (this.groupsInUse == null)
				{
					this.groupsInUse = new NameTable();
				}
				return this.groupsInUse;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x000919FB File Offset: 0x0008FBFB
		internal NameTable TypesInUse
		{
			get
			{
				if (this.typesInUse == null)
				{
					this.typesInUse = new NameTable();
				}
				return this.typesInUse;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00091A16 File Offset: 0x0008FC16
		internal CodeGenerationOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00091A20 File Offset: 0x0008FC20
		internal void MakeDerived(StructMapping structMapping, Type baseType, bool baseTypeCanBeIndirect)
		{
			structMapping.ReferencedByTopLevelElement = true;
			if (baseType != null)
			{
				TypeDesc typeDesc = this.Scope.GetTypeDesc(baseType);
				if (typeDesc != null)
				{
					TypeDesc typeDesc2 = structMapping.TypeDesc;
					if (baseTypeCanBeIndirect)
					{
						while (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
						{
							typeDesc2 = typeDesc2.BaseTypeDesc;
						}
					}
					if (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Type {0} cannot derive from {1} because it already has base type {2}.", new object[]
						{
							structMapping.TypeDesc.FullName,
							baseType.FullName,
							typeDesc2.BaseTypeDesc.FullName
						}));
					}
					typeDesc2.BaseTypeDesc = typeDesc;
				}
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00091AC7 File Offset: 0x0008FCC7
		internal string GenerateUniqueTypeName(string typeName)
		{
			typeName = CodeIdentifier.MakeValid(typeName);
			return this.TypeIdentifiers.AddUnique(typeName, typeName);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00091AE0 File Offset: 0x0008FCE0
		private StructMapping CreateRootMapping()
		{
			TypeDesc typeDesc = this.Scope.GetTypeDesc(typeof(object));
			return new StructMapping
			{
				TypeDesc = typeDesc,
				Members = new MemberMapping[0],
				IncludeInSchema = false,
				TypeName = "anyType",
				Namespace = "http://www.w3.org/2001/XMLSchema"
			};
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00091B38 File Offset: 0x0008FD38
		internal StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
			}
			return this.root;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00091B54 File Offset: 0x0008FD54
		internal StructMapping ImportRootMapping()
		{
			if (!this.rootImported)
			{
				this.rootImported = true;
				this.ImportDerivedTypes(XmlQualifiedName.Empty);
			}
			return this.GetRootMapping();
		}

		// Token: 0x06001977 RID: 6519
		internal abstract void ImportDerivedTypes(XmlQualifiedName baseName);

		// Token: 0x06001978 RID: 6520 RVA: 0x00091B78 File Offset: 0x0008FD78
		internal void AddReference(XmlQualifiedName name, NameTable references, string error)
		{
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return;
			}
			if (references[name] != null)
			{
				throw new InvalidOperationException(Res.GetString(error, new object[]
				{
					name.Name,
					name.Namespace
				}));
			}
			references[name] = name;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00091BCD File Offset: 0x0008FDCD
		internal void RemoveReference(XmlQualifiedName name, NameTable references)
		{
			references[name] = null;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00091BD7 File Offset: 0x0008FDD7
		internal void AddReservedIdentifiersForDataBinding(CodeIdentifiers scope)
		{
			if ((this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None)
			{
				scope.AddReserved(CodeExporter.PropertyChangedEvent.Name);
				scope.AddReserved(CodeExporter.RaisePropertyChangedEventMethod.Name);
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00067344 File Offset: 0x00065544
		internal SchemaImporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001931 RID: 6449
		private XmlSchemas schemas;

		// Token: 0x04001932 RID: 6450
		private StructMapping root;

		// Token: 0x04001933 RID: 6451
		private CodeGenerationOptions options;

		// Token: 0x04001934 RID: 6452
		private CodeDomProvider codeProvider;

		// Token: 0x04001935 RID: 6453
		private TypeScope scope;

		// Token: 0x04001936 RID: 6454
		private ImportContext context;

		// Token: 0x04001937 RID: 6455
		private bool rootImported;

		// Token: 0x04001938 RID: 6456
		private NameTable typesInUse;

		// Token: 0x04001939 RID: 6457
		private NameTable groupsInUse;

		// Token: 0x0400193A RID: 6458
		private SchemaImporterExtensionCollection extensions;
	}
}
