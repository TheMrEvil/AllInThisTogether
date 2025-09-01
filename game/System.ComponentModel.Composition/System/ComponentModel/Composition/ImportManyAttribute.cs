using System;
using System.ComponentModel.Composition.Primitives;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that a property, field, or parameter should be populated with all matching exports by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</summary>
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class ImportManyAttribute : Attribute, IAttributedImport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the default contract name.</summary>
		// Token: 0x060001FB RID: 507 RVA: 0x00005E74 File Offset: 0x00004074
		public ImportManyAttribute() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the contract name derived from the specified type.</summary>
		/// <param name="contractType">The type to derive the contract name of the exports to import, or <see langword="null" /> to use the default contract name.</param>
		// Token: 0x060001FC RID: 508 RVA: 0x00005E7D File Offset: 0x0000407D
		public ImportManyAttribute(Type contractType) : this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the exports to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		// Token: 0x060001FD RID: 509 RVA: 0x00005E87 File Offset: 0x00004087
		public ImportManyAttribute(string contractName) : this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the specified contract name and contract type.</summary>
		/// <param name="contractName">The contract name of the exports to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type of the export to import.</param>
		// Token: 0x060001FE RID: 510 RVA: 0x00005E91 File Offset: 0x00004091
		public ImportManyAttribute(string contractName, Type contractType)
		{
			this.ContractName = contractName;
			this.ContractType = contractType;
		}

		/// <summary>Gets the contract name of the exports to import.</summary>
		/// <returns>The contract name of the exports to import. The default value is an empty string ("").</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005EA7 File Offset: 0x000040A7
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00005EAF File Offset: 0x000040AF
		public string ContractName
		{
			[CompilerGenerated]
			get
			{
				return this.<ContractName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContractName>k__BackingField = value;
			}
		}

		/// <summary>Gets the contract type of the export to import.</summary>
		/// <returns>The type of the export that this import is expecting. The default value is <see langword="null" />, which means that the type will be obtained by looking at the type on the member that this import is attached to. If the type is <see cref="T:System.Object" />, the import will match any exported type.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00005EB8 File Offset: 0x000040B8
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00005EC0 File Offset: 0x000040C0
		public Type ContractType
		{
			[CompilerGenerated]
			get
			{
				return this.<ContractType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContractType>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the decorated property or field will be recomposed when exports that provide the matching contract change.</summary>
		/// <returns>
		///   <see langword="true" /> if the property or field allows for recomposition when exports that provide the same <see cref="P:System.ComponentModel.Composition.ImportManyAttribute.ContractName" /> are added or removed from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />.  
		/// The default value is <see langword="false" />.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00005EC9 File Offset: 0x000040C9
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00005ED1 File Offset: 0x000040D1
		public bool AllowRecomposition
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowRecomposition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowRecomposition>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the importer requires a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the exports used to satisfy this import.</summary>
		/// <returns>One of the following values:  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />, if the importer does not require a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" />. This is the default.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" /> to require that all used exports be shared by all parts in the container.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" /> to require that all used exports be non-shared in a container. In this case, each part receives their own instance.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00005EDA File Offset: 0x000040DA
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00005EE2 File Offset: 0x000040E2
		public CreationPolicy RequiredCreationPolicy
		{
			[CompilerGenerated]
			get
			{
				return this.<RequiredCreationPolicy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RequiredCreationPolicy>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the scopes from which this import may be satisfied.</summary>
		/// <returns>A value that specifies the scopes from which this import may be satisfied.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005EEB File Offset: 0x000040EB
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00005EF3 File Offset: 0x000040F3
		public ImportSource Source
		{
			[CompilerGenerated]
			get
			{
				return this.<Source>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Source>k__BackingField = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00005EFC File Offset: 0x000040FC
		ImportCardinality IAttributedImport.Cardinality
		{
			get
			{
				return ImportCardinality.ZeroOrMore;
			}
		}

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		private string <ContractName>k__BackingField;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private Type <ContractType>k__BackingField;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		private bool <AllowRecomposition>k__BackingField;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		private CreationPolicy <RequiredCreationPolicy>k__BackingField;

		// Token: 0x040000D0 RID: 208
		[CompilerGenerated]
		private ImportSource <Source>k__BackingField;
	}
}
