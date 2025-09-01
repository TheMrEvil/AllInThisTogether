using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that a type, property, field, or method provides a particular export.</summary>
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class ExportAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under the default contract name.</summary>
		// Token: 0x060001AA RID: 426 RVA: 0x0000585F File Offset: 0x00003A5F
		public ExportAttribute() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under a contract name derived from the specified type.</summary>
		/// <param name="contractType">A type from which to derive the contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> to use the default contract name.</param>
		// Token: 0x060001AB RID: 427 RVA: 0x00005869 File Offset: 0x00003A69
		public ExportAttribute(Type contractType) : this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under the specified contract name.</summary>
		/// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		// Token: 0x060001AC RID: 428 RVA: 0x00005873 File Offset: 0x00003A73
		public ExportAttribute(string contractName) : this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the specified type under the specified contract name.</summary>
		/// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type to export.</param>
		// Token: 0x060001AD RID: 429 RVA: 0x0000587D File Offset: 0x00003A7D
		public ExportAttribute(string contractName, Type contractType)
		{
			this.ContractName = contractName;
			this.ContractType = contractType;
		}

		/// <summary>Gets the contract name that is used to export the type or member marked with this attribute.</summary>
		/// <returns>The contract name that is used to export the type or member marked with this attribute. The default value is an empty string ("").</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005893 File Offset: 0x00003A93
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000589B File Offset: 0x00003A9B
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

		/// <summary>Gets the contract type that is exported by the member that this attribute is attached to.</summary>
		/// <returns>The type of export that is be provided. The default value is <see langword="null" />, which means that the type will be obtained by looking at the type on the member that this export is attached to.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000058A4 File Offset: 0x00003AA4
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000058AC File Offset: 0x00003AAC
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

		// Token: 0x040000AE RID: 174
		[CompilerGenerated]
		private string <ContractName>k__BackingField;

		// Token: 0x040000AF RID: 175
		[CompilerGenerated]
		private Type <ContractType>k__BackingField;
	}
}
