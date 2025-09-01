using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies which constructor should be used when creating a part.</summary>
	// Token: 0x02000049 RID: 73
	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public class ImportingConstructorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportingConstructorAttribute" /> class.</summary>
		// Token: 0x0600020A RID: 522 RVA: 0x00003F2F File Offset: 0x0000212F
		public ImportingConstructorAttribute()
		{
		}
	}
}
