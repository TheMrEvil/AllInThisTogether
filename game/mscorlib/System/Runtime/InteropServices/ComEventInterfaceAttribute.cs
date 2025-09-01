using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies the source interface and the class that implements the methods of the event interface that is generated when a coclass is imported from a COM type library.</summary>
	// Token: 0x0200070D RID: 1805
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComEventInterfaceAttribute" /> class with the source interface and event provider class.</summary>
		/// <param name="SourceInterface">A <see cref="T:System.Type" /> that contains the original source interface from the type library. COM uses this interface to call back to the managed class.</param>
		/// <param name="EventProvider">A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface.</param>
		// Token: 0x060040B6 RID: 16566 RVA: 0x000E1577 File Offset: 0x000DF777
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		/// <summary>Gets the original source interface from the type library.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the source interface.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000E158D File Offset: 0x000DF78D
		public Type SourceInterface
		{
			get
			{
				return this._SourceInterface;
			}
		}

		/// <summary>Gets the class that implements the methods of the event interface.</summary>
		/// <returns>A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface.</returns>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000E1595 File Offset: 0x000DF795
		public Type EventProvider
		{
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x04002AE5 RID: 10981
		internal Type _SourceInterface;

		// Token: 0x04002AE6 RID: 10982
		internal Type _EventProvider;
	}
}
