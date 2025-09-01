using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the types defined within an assembly were originally defined in a type library.</summary>
	// Token: 0x020006F1 RID: 1777
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> class with the name of the original type library file.</summary>
		/// <param name="tlbFile">The location of the original type library file.</param>
		// Token: 0x06004076 RID: 16502 RVA: 0x000E0FFC File Offset: 0x000DF1FC
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		/// <summary>Gets the name of the original type library file.</summary>
		/// <returns>The name of the original type library file.</returns>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x000E100B File Offset: 0x000DF20B
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A3E RID: 10814
		internal string _val;
	}
}
