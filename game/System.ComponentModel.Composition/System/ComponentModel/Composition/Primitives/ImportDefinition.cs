using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Represents an import that is required by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
	// Token: 0x02000099 RID: 153
	public class ImportDefinition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class.</summary>
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B4C3 File Offset: 0x000096C3
		protected ImportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class with the specified constraint, contract name, and cardinality, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="constraint">An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must match to satisfy the import definition.</param>
		/// <param name="contractName">The contract name.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constraint" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cardinality" /> is not one of the values of <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" />.</exception>
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B4EF File Offset: 0x000096EF
		public ImportDefinition(Expression<Func<ExportDefinition, bool>> constraint, string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite) : this(contractName, cardinality, isRecomposable, isPrerequisite, MetadataServices.EmptyMetadata)
		{
			Requires.NotNull<Expression<Func<ExportDefinition, bool>>>(constraint, "constraint");
			this._constraint = constraint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class with the specified constraint, contract name, cardinality, and metadata, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="constraint">An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must match to satisfy the import definition.</param>
		/// <param name="contractName">The contract name.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="metadata">The metadata associated with the import.</param>
		// Token: 0x060003FA RID: 1018 RVA: 0x0000B514 File Offset: 0x00009714
		public ImportDefinition(Expression<Func<ExportDefinition, bool>> constraint, string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, IDictionary<string, object> metadata) : this(contractName, cardinality, isRecomposable, isPrerequisite, metadata)
		{
			Requires.NotNull<Expression<Func<ExportDefinition, bool>>>(constraint, "constraint");
			this._constraint = constraint;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000B538 File Offset: 0x00009738
		internal ImportDefinition(string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, IDictionary<string, object> metadata)
		{
			if (cardinality != ImportCardinality.ExactlyOne && cardinality != ImportCardinality.ZeroOrMore && cardinality != ImportCardinality.ZeroOrOne)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentOutOfRange_InvalidEnum, "cardinality", cardinality, typeof(ImportCardinality).Name), "cardinality");
			}
			this._contractName = (contractName ?? ImportDefinition.EmptyContractName);
			this._cardinality = cardinality;
			this._isRecomposable = isRecomposable;
			this._isPrerequisite = isPrerequisite;
			if (metadata != null)
			{
				this._metadata = metadata;
			}
		}

		/// <summary>Gets the name of the contract.</summary>
		/// <returns>The contract name.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public virtual string ContractName
		{
			get
			{
				return this._contractName;
			}
		}

		/// <summary>Gets the metadata associated with this import.</summary>
		/// <returns>A collection that contains the metadata associated with this import.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public virtual IDictionary<string, object> Metadata
		{
			get
			{
				return this._metadata;
			}
		}

		/// <summary>Gets the cardinality of the exports required by the import definition.</summary>
		/// <returns>One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />. The default is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public virtual ImportCardinality Cardinality
		{
			get
			{
				return this._cardinality;
			}
		}

		/// <summary>Gets an expression that defines conditions that the import must satisfy to match the import definition.</summary>
		/// <returns>An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must satisfy to match the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</returns>
		/// <exception cref="T:System.NotImplementedException">The property was not overridden by a derived class.</exception>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public virtual Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				if (this._constraint != null)
				{
					return this._constraint;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("Constraint");
			}
		}

		/// <summary>Gets a value that indicates whether the import definition must be satisfied before a part can start producing exported objects.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object can start producing exported objects; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000B613 File Offset: 0x00009813
		public virtual bool IsPrerequisite
		{
			get
			{
				return this._isPrerequisite;
			}
		}

		/// <summary>Gets a value that indicates whether the import definition can be satisfied multiple times.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000B61B File Offset: 0x0000981B
		public virtual bool IsRecomposable
		{
			get
			{
				return this._isRecomposable;
			}
		}

		/// <summary>Gets a value that indicates whether the export represented by the specified definition satisfies the constraints of this import definition.</summary>
		/// <param name="exportDefinition">The export definition to test.</param>
		/// <returns>
		///   <see langword="true" /> if the constraints are satisfied; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000402 RID: 1026 RVA: 0x0000B623 File Offset: 0x00009823
		public virtual bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			Requires.NotNull<ExportDefinition>(exportDefinition, "exportDefinition");
			if (this._compiledConstraint == null)
			{
				this._compiledConstraint = this.Constraint.Compile();
			}
			return this._compiledConstraint(exportDefinition);
		}

		/// <summary>Returns a string representation of the import definition.</summary>
		/// <returns>A string representation of the import definition.</returns>
		// Token: 0x06000403 RID: 1027 RVA: 0x0000B655 File Offset: 0x00009855
		public override string ToString()
		{
			return this.Constraint.Body.ToString();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000B667 File Offset: 0x00009867
		// Note: this type is marked as 'beforefieldinit'.
		static ImportDefinition()
		{
		}

		// Token: 0x0400018D RID: 397
		internal static readonly string EmptyContractName = string.Empty;

		// Token: 0x0400018E RID: 398
		private readonly Expression<Func<ExportDefinition, bool>> _constraint;

		// Token: 0x0400018F RID: 399
		private readonly ImportCardinality _cardinality = ImportCardinality.ExactlyOne;

		// Token: 0x04000190 RID: 400
		private readonly string _contractName = ImportDefinition.EmptyContractName;

		// Token: 0x04000191 RID: 401
		private readonly bool _isRecomposable;

		// Token: 0x04000192 RID: 402
		private readonly bool _isPrerequisite = true;

		// Token: 0x04000193 RID: 403
		private Func<ExportDefinition, bool> _compiledConstraint;

		// Token: 0x04000194 RID: 404
		private readonly IDictionary<string, object> _metadata = MetadataServices.EmptyMetadata;
	}
}
