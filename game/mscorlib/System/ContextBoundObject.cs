using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Defines the base class for all context-bound classes.</summary>
	// Token: 0x020001ED RID: 493
	[ComVisible(true)]
	[Serializable]
	public abstract class ContextBoundObject : MarshalByRefObject
	{
		/// <summary>Instantiates an instance of the <see cref="T:System.ContextBoundObject" /> class.</summary>
		// Token: 0x0600154D RID: 5453 RVA: 0x00053949 File Offset: 0x00051B49
		protected ContextBoundObject()
		{
		}
	}
}
