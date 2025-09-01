using System;
using System.ComponentModel.Composition.Primitives;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that a property, field, or parameter value should be provided by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.object</summary>
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class ImportAttribute : Attribute, IAttributedImport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the default contract name.</summary>
		// Token: 0x060001E3 RID: 483 RVA: 0x00005D86 File Offset: 0x00003F86
		public ImportAttribute() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the contract name derived from the specified type.</summary>
		/// <param name="contractType">The type to derive the contract name of the export from, or <see langword="null" /> to use the default contract name.</param>
		// Token: 0x060001E4 RID: 484 RVA: 0x00005D8F File Offset: 0x00003F8F
		public ImportAttribute(Type contractType) : this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the export to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x00005D99 File Offset: 0x00003F99
		public ImportAttribute(string contractName) : this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the specified contract name and type.</summary>
		/// <param name="contractName">The contract name of the export to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type of the export to import.</param>
		// Token: 0x060001E6 RID: 486 RVA: 0x00005DA3 File Offset: 0x00003FA3
		public ImportAttribute(string contractName, Type contractType)
		{
			this.ContractName = contractName;
			this.ContractType = contractType;
		}

		/// <summary>Gets the contract name of the export to import.</summary>
		/// <returns>The contract name of the export to import. The default is an empty string ("").</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005DB9 File Offset: 0x00003FB9
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00005DC1 File Offset: 0x00003FC1
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

		/// <summary>Gets the type of the export to import.</summary>
		/// <returns>The type of the export to import.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005DCA File Offset: 0x00003FCA
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00005DD2 File Offset: 0x00003FD2
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

		/// <summary>Gets or sets a value that indicates whether the property, field, or parameter will be set to its type's default value when an export with the contract name is not present in the container.</summary>
		/// <returns>
		///   <see langword="true" /> if the property, field, or parameter will be set to its type's default value when there is no export with the <see cref="P:System.ComponentModel.Composition.ImportAttribute.ContractName" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005DDB File Offset: 0x00003FDB
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public bool AllowDefault
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowDefault>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowDefault>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the property or field will be recomposed when exports with a matching contract have changed in the container.</summary>
		/// <returns>
		///   <see langword="true" /> if the property or field allows recomposition when exports with a matching <see cref="P:System.ComponentModel.Composition.ImportAttribute.ContractName" /> are added or removed from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00005DEC File Offset: 0x00003FEC
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00005DF4 File Offset: 0x00003FF4
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
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005DFD File Offset: 0x00003FFD
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00005E05 File Offset: 0x00004005
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
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005E0E File Offset: 0x0000400E
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00005E16 File Offset: 0x00004016
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005E1F File Offset: 0x0000401F
		ImportCardinality IAttributedImport.Cardinality
		{
			get
			{
				if (this.AllowDefault)
				{
					return ImportCardinality.ZeroOrOne;
				}
				return ImportCardinality.ExactlyOne;
			}
		}

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private string <ContractName>k__BackingField;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		private Type <ContractType>k__BackingField;

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private bool <AllowDefault>k__BackingField;

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		private bool <AllowRecomposition>k__BackingField;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		private CreationPolicy <RequiredCreationPolicy>k__BackingField;

		// Token: 0x040000CA RID: 202
		[CompilerGenerated]
		private ImportSource <Source>k__BackingField;
	}
}
