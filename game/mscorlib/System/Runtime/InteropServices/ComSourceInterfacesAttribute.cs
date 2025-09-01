using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies a list of interfaces that are exposed as COM event sources for the attributed class.</summary>
	// Token: 0x020006F4 RID: 1780
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the name of the event source interface.</summary>
		/// <param name="sourceInterfaces">A null-delimited list of fully qualified event source interface names.</param>
		// Token: 0x0600407B RID: 16507 RVA: 0x000E102A File Offset: 0x000DF22A
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the type to use as a source interface.</summary>
		/// <param name="sourceInterface">The <see cref="T:System.Type" /> of the source interface.</param>
		// Token: 0x0600407C RID: 16508 RVA: 0x000E1039 File Offset: 0x000DF239
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x0600407D RID: 16509 RVA: 0x000E104D File Offset: 0x000DF24D
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		/// <summary>Initializes a new instance of the <see langword="ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x0600407E RID: 16510 RVA: 0x000E1074 File Offset: 0x000DF274
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface.</param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface.</param>
		/// <param name="sourceInterface4">The <see cref="T:System.Type" /> of a source interface.</param>
		// Token: 0x0600407F RID: 16511 RVA: 0x000E10C4 File Offset: 0x000DF2C4
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName,
				"\0",
				sourceInterface4.FullName
			});
		}

		/// <summary>Gets the fully qualified name of the event source interface.</summary>
		/// <returns>The fully qualified name of the event source interface.</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000E1125 File Offset: 0x000DF325
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A44 RID: 10820
		internal string _val;
	}
}
