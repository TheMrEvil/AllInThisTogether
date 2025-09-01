using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x02000066 RID: 102
	[StructLayout(LayoutKind.Sequential)]
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06000175 RID: 373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FindProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x06000177 RID: 375 RVA: 0x00005888 File Offset: 0x00003A88
		private ComInteropProxy(Type t) : base(t)
		{
			this.com_object = __ComObject.CreateRCW(t);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000058A4 File Offset: 0x00003AA4
		private void CacheProxy()
		{
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(this.com_object.IUnknown, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				ComInteropProxy comInteropProxy2 = this;
				ComInteropProxy.AddProxy(this.com_object.IUnknown, ref comInteropProxy2);
				return;
			}
			Interlocked.Increment(ref this.ref_count);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000058E9 File Offset: 0x00003AE9
		private ComInteropProxy(IntPtr pUnk) : this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000058FC File Offset: 0x00003AFC
		internal ComInteropProxy(IntPtr pUnk, Type t) : base(t)
		{
			this.com_object = new __ComObject(pUnk, this);
			this.CacheProxy();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005920 File Offset: 0x00003B20
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr));
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				Marshal.Release(intPtr);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(intPtr);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005974 File Offset: 0x00003B74
		internal static ComInteropProxy CreateProxy(Type t)
		{
			IntPtr intPtr = __ComObject.CreateIUnknown(t);
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			ComInteropProxy comInteropProxy2;
			if (comInteropProxy != null)
			{
				Type type = comInteropProxy.com_object.GetType();
				if (type != t)
				{
					throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", type, t));
				}
				comInteropProxy2 = comInteropProxy;
				Marshal.Release(intPtr);
			}
			else
			{
				comInteropProxy2 = new ComInteropProxy(t);
				comInteropProxy2.com_object.Initialize(intPtr, comInteropProxy2);
			}
			return comInteropProxy2;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000059DC File Offset: 0x00003BDC
		public override IMessage Invoke(IMessage msg)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000059E8 File Offset: 0x00003BE8
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000059F0 File Offset: 0x00003BF0
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000059FC File Offset: 0x00003BFC
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x04000E25 RID: 3621
		private __ComObject com_object;

		// Token: 0x04000E26 RID: 3622
		private int ref_count = 1;

		// Token: 0x04000E27 RID: 3623
		private string type_name;
	}
}
