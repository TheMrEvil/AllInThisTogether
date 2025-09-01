using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables customization of managed objects that extend from unmanaged objects during creation.</summary>
	// Token: 0x0200073D RID: 1853
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x0600411E RID: 16670 RVA: 0x0000259F File Offset: 0x0000079F
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000E1D5B File Offset: 0x000DFF5B
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		/// <summary>Registers a <see langword="delegate" /> that is called when an instance of a managed type, that extends from an unmanaged type, needs to allocate the aggregated unmanaged object.</summary>
		/// <param name="callback">A <see langword="delegate" /> that is called in place of <see langword="CoCreateInstance" />.</param>
		// Token: 0x06004120 RID: 16672 RVA: 0x000E1D70 File Offset: 0x000DFF70
		public static void RegisterObjectCreationCallback(ObjectCreationDelegate callback)
		{
			int i = 1;
			StackTrace stackTrace = new StackTrace(false);
			while (i < stackTrace.FrameCount)
			{
				MethodBase method = stackTrace.GetFrame(i).GetMethod();
				if (method.MemberType == MemberTypes.Constructor && method.IsStatic)
				{
					ExtensibleClassFactory.hashtable.Add(method.DeclaringType, callback);
					return;
				}
				i++;
			}
			throw new InvalidOperationException("RegisterObjectCreationCallback must be called from .cctor of class derived from ComImport type.");
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000E1DCF File Offset: 0x000DFFCF
		// Note: this type is marked as 'beforefieldinit'.
		static ExtensibleClassFactory()
		{
		}

		// Token: 0x04002BBE RID: 11198
		private static readonly Hashtable hashtable = new Hashtable();
	}
}
