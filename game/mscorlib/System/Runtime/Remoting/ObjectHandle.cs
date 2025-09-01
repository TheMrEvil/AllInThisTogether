using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Wraps marshal-by-value object references, allowing them to be returned through an indirection.</summary>
	// Token: 0x02000564 RID: 1380
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Remoting.ObjectHandle" /> class, wrapping the given object <paramref name="o" />.</summary>
		/// <param name="o">The object that is wrapped by the new <see cref="T:System.Runtime.Remoting.ObjectHandle" />.</param>
		// Token: 0x0600361F RID: 13855 RVA: 0x000C2AC5 File Offset: 0x000C0CC5
		public ObjectHandle(object o)
		{
			this._wrapped = o;
		}

		/// <summary>Initializes the lifetime lease of the wrapped object.</summary>
		/// <returns>An initialized <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> that allows you to control the lifetime of the wrapped object.</returns>
		// Token: 0x06003620 RID: 13856 RVA: 0x000C2AD4 File Offset: 0x000C0CD4
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		/// <summary>Returns the wrapped object.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x06003621 RID: 13857 RVA: 0x000C2ADC File Offset: 0x000C0CDC
		public object Unwrap()
		{
			return this._wrapped;
		}

		// Token: 0x0400252C RID: 9516
		private object _wrapped;
	}
}
