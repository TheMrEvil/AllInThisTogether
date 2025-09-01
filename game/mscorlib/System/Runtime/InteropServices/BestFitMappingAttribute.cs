using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls whether Unicode characters are converted to the closest matching ANSI characters.</summary>
	// Token: 0x02000710 RID: 1808
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> class set to the value of the <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> property.</summary>
		/// <param name="BestFitMapping">
		///   <see langword="true" /> to indicate that best-fit mapping is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
		// Token: 0x060040C1 RID: 16577 RVA: 0x000E1608 File Offset: 0x000DF808
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		/// <summary>Gets the best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		/// <returns>
		///   <see langword="true" /> if best-fit mapping is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x000E1617 File Offset: 0x000DF817
		public bool BestFitMapping
		{
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x04002AED RID: 10989
		internal bool _bestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI '?' character.</summary>
		// Token: 0x04002AEE RID: 10990
		public bool ThrowOnUnmappableChar;
	}
}
