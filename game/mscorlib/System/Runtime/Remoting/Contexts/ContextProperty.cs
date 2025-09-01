using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Holds the name/value pair of the property name and the object representing the property of a context.</summary>
	// Token: 0x02000590 RID: 1424
	[ComVisible(true)]
	public class ContextProperty
	{
		// Token: 0x060037BF RID: 14271 RVA: 0x000C8AED File Offset: 0x000C6CED
		private ContextProperty(string name, object prop)
		{
			this.name = name;
			this.prop = prop;
		}

		/// <summary>Gets the name of the T:System.Runtime.Remoting.Contexts.ContextProperty class.</summary>
		/// <returns>The name of the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> class.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000C8B03 File Offset: 0x000C6D03
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the object representing the property of a context.</summary>
		/// <returns>The object representing the property of a context.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000C8B0B File Offset: 0x000C6D0B
		public virtual object Property
		{
			get
			{
				return this.prop;
			}
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000173AD File Offset: 0x000155AD
		internal ContextProperty()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040025B2 RID: 9650
		private string name;

		// Token: 0x040025B3 RID: 9651
		private object prop;
	}
}
