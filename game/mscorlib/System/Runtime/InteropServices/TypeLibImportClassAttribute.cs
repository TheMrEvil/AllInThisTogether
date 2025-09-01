using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies which <see cref="T:System.Type" /> exclusively uses an interface. This class cannot be inherited.</summary>
	// Token: 0x020006EC RID: 1772
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> class specifying the <see cref="T:System.Type" /> that exclusively uses an interface.</summary>
		/// <param name="importClass">The <see cref="T:System.Type" /> object that exclusively uses an interface.</param>
		// Token: 0x0600406E RID: 16494 RVA: 0x000E0FB2 File Offset: 0x000DF1B2
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		/// <summary>Gets the name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</summary>
		/// <returns>The name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000E0FC6 File Offset: 0x000DF1C6
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x04002A3B RID: 10811
		internal string _importClassName;
	}
}
