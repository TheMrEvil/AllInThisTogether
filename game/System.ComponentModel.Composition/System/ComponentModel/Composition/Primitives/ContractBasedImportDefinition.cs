using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Represents an import that is required by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object and that can specify both a contract name and metadata.</summary>
	// Token: 0x02000092 RID: 146
	public class ContractBasedImportDefinition : ImportDefinition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class.</summary>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000AF23 File Offset: 0x00009123
		protected ContractBasedImportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class with the specified contract name, required type identity, required metadata, cardinality, and creation policy, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object required by the import definition.</param>
		/// <param name="requiredTypeIdentity">The type identity of the export type expected. Use the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method to generate a type identity for a given type. If no specific type is required, use <see langword="null" />.</param>
		/// <param name="requiredMetadata">A collection of key/value pairs that contain the metadata names and types required by the import definition; or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition.RequiredMetadata" /> property to an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition is required to be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">A value that indicates that the importer requires a specific creation policy for the exports used to satisfy this import. If no specific creation policy is needed, the default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="requiredMetadata" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cardinality" /> is not one of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" /> values.</exception>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000AF38 File Offset: 0x00009138
		public ContractBasedImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy) : this(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, MetadataServices.EmptyMetadata)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class with the specified contract name, required type identity, required and optional metadata, cardinality, and creation policy, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object required by the import definition.</param>
		/// <param name="requiredTypeIdentity">The type identity of the export type expected. Use the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method to generate a type identity for a given type. If no specific type is required, use <see langword="null" />.</param>
		/// <param name="requiredMetadata">A collection of key/value pairs that contain the metadata names and types required by the import definition; or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition.RequiredMetadata" /> property to an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition is required to be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">A value that indicates that the importer requires a specific creation policy for the exports used to satisfy this import. If no specific creation policy is needed, the default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</param>
		/// <param name="metadata">The metadata associated with this import.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="requiredMetadata" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cardinality" /> is not one of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" /> values.</exception>
		// Token: 0x060003DA RID: 986 RVA: 0x0000AF5B File Offset: 0x0000915B
		public ContractBasedImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata) : base(contractName, cardinality, isRecomposable, isPrerequisite, metadata)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			this._requiredTypeIdentity = requiredTypeIdentity;
			if (requiredMetadata != null)
			{
				this._requiredMetadata = requiredMetadata;
			}
			this._requiredCreationPolicy = requiredCreationPolicy;
		}

		/// <summary>Gets the expected type of the export that matches this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" />.</summary>
		/// <returns>A string that is generated by calling the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method on the type that this import expects. If the value is <see langword="null" />, this import does not expect a particular type.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000AF9B File Offset: 0x0000919B
		public virtual string RequiredTypeIdentity
		{
			get
			{
				return this._requiredTypeIdentity;
			}
		}

		/// <summary>Gets the metadata names of the export required by the import definition.</summary>
		/// <returns>A collection of <see cref="T:System.String" /> objects that contain the metadata names of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" />. The default is an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000AFA3 File Offset: 0x000091A3
		public virtual IEnumerable<KeyValuePair<string, Type>> RequiredMetadata
		{
			get
			{
				this.ValidateRequiredMetadata();
				return this._requiredMetadata;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000AFB4 File Offset: 0x000091B4
		private void ValidateRequiredMetadata()
		{
			if (!this._isRequiredMetadataValidated)
			{
				foreach (KeyValuePair<string, Type> keyValuePair in this._requiredMetadata)
				{
					if (keyValuePair.Key == null || keyValuePair.Value == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_NullElement, "requiredMetadata"));
					}
				}
				this._isRequiredMetadataValidated = true;
			}
		}

		/// <summary>Gets or sets a value that indicates that the importer requires a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the exports used to satisfy this import.</summary>
		/// <returns>One of the following values:  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />, if the importer does not require a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" />.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" /> to require that all exports used should be shared by all importers in the container.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" /> to require that all exports used should be non-shared in the container. In this case, each importer receives a separate instance.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B03C File Offset: 0x0000923C
		public virtual CreationPolicy RequiredCreationPolicy
		{
			get
			{
				return this._requiredCreationPolicy;
			}
		}

		/// <summary>Gets an expression that defines conditions that must be matched to satisfy the import described by this import definition.</summary>
		/// <returns>An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions that must be matched for the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> to be satisfied by an <see cref="T:System.ComponentModel.Composition.Primitives.Export" />.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000B044 File Offset: 0x00009244
		public override Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				if (this._constraint == null)
				{
					this._constraint = ConstraintServices.CreateConstraint(this.ContractName, this.RequiredTypeIdentity, this.RequiredMetadata, this.RequiredCreationPolicy);
				}
				return this._constraint;
			}
		}

		/// <summary>Returns a value indicating whether the constraint represented by this object is satisfied by the export represented by the given export definition.</summary>
		/// <param name="exportDefinition">The export definition to test.</param>
		/// <returns>
		///   <see langword="true" /> if the constraint is satisfied; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000B077 File Offset: 0x00009277
		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			Requires.NotNull<ExportDefinition>(exportDefinition, "exportDefinition");
			return StringComparers.ContractName.Equals(this.ContractName, exportDefinition.ContractName) && this.MatchRequiredMatadata(exportDefinition);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B0A8 File Offset: 0x000092A8
		private bool MatchRequiredMatadata(ExportDefinition definition)
		{
			if (!string.IsNullOrEmpty(this.RequiredTypeIdentity))
			{
				string value = definition.Metadata.GetValue("ExportTypeIdentity");
				if (!StringComparers.ContractName.Equals(this.RequiredTypeIdentity, value))
				{
					return false;
				}
			}
			foreach (KeyValuePair<string, Type> keyValuePair in this.RequiredMetadata)
			{
				string key = keyValuePair.Key;
				Type value2 = keyValuePair.Value;
				object obj = null;
				if (!definition.Metadata.TryGetValue(key, out obj))
				{
					return false;
				}
				if (obj != null)
				{
					if (!value2.IsInstanceOfType(obj))
					{
						return false;
					}
				}
				else if (value2.IsValueType)
				{
					return false;
				}
			}
			if (this.RequiredCreationPolicy == CreationPolicy.Any)
			{
				return true;
			}
			CreationPolicy value3 = definition.Metadata.GetValue("System.ComponentModel.Composition.CreationPolicy");
			return value3 == CreationPolicy.Any || value3 == this.RequiredCreationPolicy;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> object.</returns>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000B19C File Offset: 0x0000939C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("\n\tContractName\t{0}", this.ContractName));
			stringBuilder.Append(string.Format("\n\tRequiredTypeIdentity\t{0}", this.RequiredTypeIdentity));
			if (this._requiredCreationPolicy != CreationPolicy.Any)
			{
				stringBuilder.Append(string.Format("\n\tRequiredCreationPolicy\t{0}", this.RequiredCreationPolicy));
			}
			if (this._requiredMetadata.Count<KeyValuePair<string, Type>>() > 0)
			{
				stringBuilder.Append(string.Format("\n\tRequiredMetadata", Array.Empty<object>()));
				foreach (KeyValuePair<string, Type> keyValuePair in this._requiredMetadata)
				{
					stringBuilder.Append(string.Format("\n\t\t{0}\t({1})", keyValuePair.Key, keyValuePair.Value));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400017C RID: 380
		private readonly IEnumerable<KeyValuePair<string, Type>> _requiredMetadata = Enumerable.Empty<KeyValuePair<string, Type>>();

		// Token: 0x0400017D RID: 381
		private Expression<Func<ExportDefinition, bool>> _constraint;

		// Token: 0x0400017E RID: 382
		private readonly CreationPolicy _requiredCreationPolicy;

		// Token: 0x0400017F RID: 383
		private readonly string _requiredTypeIdentity;

		// Token: 0x04000180 RID: 384
		private bool _isRequiredMetadataValidated;
	}
}
