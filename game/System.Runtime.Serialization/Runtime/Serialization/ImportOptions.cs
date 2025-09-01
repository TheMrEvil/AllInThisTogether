using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Represents the options that can be set on an <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" />.</summary>
	// Token: 0x020000E9 RID: 233
	public class ImportOptions
	{
		/// <summary>Gets or sets a value that specifies whether generated data contract classes will be marked with the <see cref="T:System.SerializableAttribute" /> attribute in addition to the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute.</summary>
		/// <returns>
		///   <see langword="true" /> to generate classes with the <see cref="T:System.SerializableAttribute" /> applied; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00035384 File Offset: 0x00033584
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0003538C File Offset: 0x0003358C
		public bool GenerateSerializable
		{
			get
			{
				return this.generateSerializable;
			}
			set
			{
				this.generateSerializable = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether generated code will be marked internal or public.</summary>
		/// <returns>
		///   <see langword="true" /> if the code will be marked <see langword="internal" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00035395 File Offset: 0x00033595
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0003539D File Offset: 0x0003359D
		public bool GenerateInternal
		{
			get
			{
				return this.generateInternal;
			}
			set
			{
				this.generateInternal = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether types in generated code should implement the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the generated code should implement the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> interface; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x000353A6 File Offset: 0x000335A6
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x000353AE File Offset: 0x000335AE
		public bool EnableDataBinding
		{
			get
			{
				return this.enableDataBinding;
			}
			set
			{
				this.enableDataBinding = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance that provides the means to check whether particular options for a target language are supported.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that provides the means to check whether particular options for a target language are supported.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x000353B7 File Offset: 0x000335B7
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x000353BF File Offset: 0x000335BF
		public CodeDomProvider CodeProvider
		{
			get
			{
				return this.codeProvider;
			}
			set
			{
				this.codeProvider = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Generic.IList`1" /> containing types referenced in generated code.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> that contains the referenced types.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000353C8 File Offset: 0x000335C8
		public ICollection<Type> ReferencedTypes
		{
			get
			{
				if (this.referencedTypes == null)
				{
					this.referencedTypes = new List<Type>();
				}
				return this.referencedTypes;
			}
		}

		/// <summary>Gets a collection of types that represents data contract collections that should be referenced when generating code for collections, such as lists or dictionaries of items.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> that contains the referenced collection types.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x000353E3 File Offset: 0x000335E3
		public ICollection<Type> ReferencedCollectionTypes
		{
			get
			{
				if (this.referencedCollectionTypes == null)
				{
					this.referencedCollectionTypes = new List<Type>();
				}
				return this.referencedCollectionTypes;
			}
		}

		/// <summary>Gets a dictionary that contains the mapping of data contract namespaces to the CLR namespaces that must be used to generate code during an import operation.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IDictionary`2" /> that contains the namespace mappings.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000353FE File Offset: 0x000335FE
		public IDictionary<string, string> Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Dictionary<string, string>();
				}
				return this.namespaces;
			}
		}

		/// <summary>Gets or sets a value that determines whether all XML schema types, even those that do not conform to a data contract schema, will be imported.</summary>
		/// <returns>
		///   <see langword="true" /> to import all schema types; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00035419 File Offset: 0x00033619
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00035421 File Offset: 0x00033621
		public bool ImportXmlType
		{
			get
			{
				return this.importXmlType;
			}
			set
			{
				this.importXmlType = value;
			}
		}

		/// <summary>Gets or sets a data contract surrogate that can be used to modify the code generated during an import operation.</summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> interface that handles schema import.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003542A File Offset: 0x0003362A
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00035432 File Offset: 0x00033632
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.dataContractSurrogate;
			}
			set
			{
				this.dataContractSurrogate = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ImportOptions" /> class.</summary>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0000222F File Offset: 0x0000042F
		public ImportOptions()
		{
		}

		// Token: 0x04000615 RID: 1557
		private bool generateSerializable;

		// Token: 0x04000616 RID: 1558
		private bool generateInternal;

		// Token: 0x04000617 RID: 1559
		private bool enableDataBinding;

		// Token: 0x04000618 RID: 1560
		private CodeDomProvider codeProvider;

		// Token: 0x04000619 RID: 1561
		private ICollection<Type> referencedTypes;

		// Token: 0x0400061A RID: 1562
		private ICollection<Type> referencedCollectionTypes;

		// Token: 0x0400061B RID: 1563
		private IDictionary<string, string> namespaces;

		// Token: 0x0400061C RID: 1564
		private bool importXmlType;

		// Token: 0x0400061D RID: 1565
		private IDataContractSurrogate dataContractSurrogate;
	}
}
