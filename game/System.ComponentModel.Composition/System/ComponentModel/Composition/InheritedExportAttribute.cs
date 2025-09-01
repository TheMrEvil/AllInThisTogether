using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that a type provides a particular export, and that subclasses of that type will also provide that export.</summary>
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public class InheritedExportAttribute : ExportAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class.</summary>
		// Token: 0x0600020B RID: 523 RVA: 0x00005EFF File Offset: 0x000040FF
		public InheritedExportAttribute() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract type.</summary>
		/// <param name="contractType">The type of the contract.</param>
		// Token: 0x0600020C RID: 524 RVA: 0x00005F09 File Offset: 0x00004109
		public InheritedExportAttribute(Type contractType) : this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract name.</summary>
		/// <param name="contractName">The name of the contract.</param>
		// Token: 0x0600020D RID: 525 RVA: 0x00005F13 File Offset: 0x00004113
		public InheritedExportAttribute(string contractName) : this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract name and type.</summary>
		/// <param name="contractName">The name of the contract.</param>
		/// <param name="contractType">The type of the contract.</param>
		// Token: 0x0600020E RID: 526 RVA: 0x00005F1D File Offset: 0x0000411D
		public InheritedExportAttribute(string contractName, Type contractType) : base(contractName, contractType)
		{
		}
	}
}
