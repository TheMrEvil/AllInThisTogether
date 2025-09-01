using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x020000A6 RID: 166
	internal static class MacSupport
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x000174F8 File Offset: 0x000156F8
		static MacSupport()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (string.Equals(assembly.GetName().Name, "System.Windows.Forms"))
				{
					Type type = assembly.GetType("System.Windows.Forms.XplatUICarbon");
					if (type != null)
					{
						MacSupport.hwnd_delegate = (Delegate)type.GetTypeInfo().GetField("HwndDelegate", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					}
				}
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00017584 File Offset: 0x00015784
		internal static CocoaContext GetCGContextForNSView(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			IntPtr value = MacSupport.objc_msgSend(MacSupport.objc_getClass("NSView"), MacSupport.sel_registerName("focusView"));
			IntPtr focusHandle = IntPtr.Zero;
			if (value != handle)
			{
				if (!MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("lockFocusIfCanDraw")))
				{
					return null;
				}
				focusHandle = handle;
			}
			IntPtr intPtr = MacSupport.objc_msgSend(MacSupport.objc_msgSend(MacSupport.objc_msgSend(handle, MacSupport.sel_registerName("window")), MacSupport.sel_registerName("graphicsContext")), MacSupport.sel_registerName("graphicsPort"));
			bool flag = MacSupport.bool_objc_msgSend(handle, MacSupport.sel_registerName("isFlipped"));
			MacSupport.CGContextSaveGState(intPtr);
			Size size;
			if (IntPtr.Size == 4)
			{
				CGRect32 cgrect = default(CGRect32);
				MacSupport.objc_msgSend_stret(ref cgrect, handle, MacSupport.sel_registerName("bounds"));
				if (flag)
				{
					MacSupport.CGContextTranslateCTM32(intPtr, cgrect.origin.x, cgrect.size.height);
					MacSupport.CGContextScaleCTM32(intPtr, 1f, -1f);
				}
				size = new Size((int)cgrect.size.width, (int)cgrect.size.height);
			}
			else
			{
				CGRect64 cgrect2 = default(CGRect64);
				MacSupport.objc_msgSend_stret(ref cgrect2, handle, MacSupport.sel_registerName("bounds"));
				if (flag)
				{
					MacSupport.CGContextTranslateCTM64(intPtr, cgrect2.origin.x, cgrect2.size.height);
					MacSupport.CGContextScaleCTM64(intPtr, 1.0, -1.0);
				}
				size = new Size((int)cgrect2.size.width, (int)cgrect2.size.height);
			}
			return new CocoaContext(focusHandle, intPtr, size.Width, size.Height);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00017724 File Offset: 0x00015924
		internal static CarbonContext GetCGContextForView(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr port = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (IntPtr.Size == 8)
			{
				throw new NotSupportedException();
			}
			intPtr2 = MacSupport.GetControlOwner(handle);
			if (handle == IntPtr.Zero || intPtr2 == IntPtr.Zero)
			{
				port = MacSupport.GetQDGlobalsThePort();
				MacSupport.CreateCGContextForPort(port, ref intPtr);
				CGRect32 cgrect = MacSupport.CGDisplayBounds32(MacSupport.CGMainDisplayID());
				return new CarbonContext(port, intPtr, (int)cgrect.size.width, (int)cgrect.size.height);
			}
			QDRect qdrect = default(QDRect);
			CGRect32 cgrect2 = default(CGRect32);
			port = MacSupport.GetWindowPort(intPtr2);
			intPtr = MacSupport.GetContext(port);
			MacSupport.GetWindowBounds(intPtr2, 32U, ref qdrect);
			MacSupport.HIViewGetBounds(handle, ref cgrect2);
			MacSupport.HIViewConvertRect(ref cgrect2, handle, IntPtr.Zero);
			if (cgrect2.size.height < 0f)
			{
				cgrect2.size.height = 0f;
			}
			if (cgrect2.size.width < 0f)
			{
				cgrect2.size.width = 0f;
			}
			MacSupport.CGContextTranslateCTM32(intPtr, cgrect2.origin.x, (float)(qdrect.bottom - qdrect.top) - (cgrect2.origin.y + cgrect2.size.height));
			CGRect32 rect = new CGRect32(0f, 0f, cgrect2.size.width, cgrect2.size.height);
			MacSupport.CGContextSaveGState(intPtr);
			Rectangle[] array = (Rectangle[])MacSupport.hwnd_delegate.DynamicInvoke(new object[]
			{
				handle
			});
			if (array != null && array.Length != 0)
			{
				int num = array.Length;
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect32(intPtr, rect);
				for (int i = 0; i < num; i++)
				{
					MacSupport.CGContextAddRect32(intPtr, new CGRect32((float)array[i].X, cgrect2.size.height - (float)array[i].Y - (float)array[i].Height, (float)array[i].Width, (float)array[i].Height));
				}
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextEOClip(intPtr);
			}
			else
			{
				MacSupport.CGContextBeginPath(intPtr);
				MacSupport.CGContextAddRect32(intPtr, rect);
				MacSupport.CGContextClosePath(intPtr);
				MacSupport.CGContextClip(intPtr);
			}
			return new CarbonContext(port, intPtr, (int)cgrect2.size.width, (int)cgrect2.size.height);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000179A0 File Offset: 0x00015BA0
		internal static IntPtr GetContext(IntPtr port)
		{
			IntPtr zero = IntPtr.Zero;
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CreateCGContextForPort(port, ref zero);
			}
			return zero;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000179E8 File Offset: 0x00015BE8
		internal static void ReleaseContext(IntPtr port, IntPtr context)
		{
			MacSupport.CGContextRestoreGState(context);
			object obj = MacSupport.lockobj;
			lock (obj)
			{
				MacSupport.CFRelease(context);
			}
		}

		// Token: 0x06000A36 RID: 2614
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_getClass(string className);

		// Token: 0x06000A37 RID: 2615
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector, string argument);

		// Token: 0x06000A38 RID: 2616
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr basePtr, IntPtr selector);

		// Token: 0x06000A39 RID: 2617
		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref CGRect32 arect, IntPtr basePtr, IntPtr selector);

		// Token: 0x06000A3A RID: 2618
		[DllImport("libobjc.dylib")]
		public static extern void objc_msgSend_stret(ref CGRect64 arect, IntPtr basePtr, IntPtr selector);

		// Token: 0x06000A3B RID: 2619
		[DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern bool bool_objc_msgSend(IntPtr handle, IntPtr selector);

		// Token: 0x06000A3C RID: 2620
		[DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern bool bool_objc_msgSend_IntPtr(IntPtr handle, IntPtr selector, IntPtr argument);

		// Token: 0x06000A3D RID: 2621
		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		// Token: 0x06000A3E RID: 2622
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGMainDisplayID();

		// Token: 0x06000A3F RID: 2623
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGDisplayBounds")]
		internal static extern CGRect32 CGDisplayBounds32(IntPtr display);

		// Token: 0x06000A40 RID: 2624
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetBounds(IntPtr vHnd, ref CGRect32 r);

		// Token: 0x06000A41 RID: 2625
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertRect(ref CGRect32 r, IntPtr a, IntPtr b);

		// Token: 0x06000A42 RID: 2626
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlOwner(IntPtr aView);

		// Token: 0x06000A43 RID: 2627
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref QDRect rect);

		// Token: 0x06000A44 RID: 2628
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowPort(IntPtr hWnd);

		// Token: 0x06000A45 RID: 2629
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetQDGlobalsThePort();

		// Token: 0x06000A46 RID: 2630
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CreateCGContextForPort(IntPtr port, ref IntPtr context);

		// Token: 0x06000A47 RID: 2631
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CFRelease(IntPtr context);

		// Token: 0x06000A48 RID: 2632
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDBeginCGContext(IntPtr port, ref IntPtr context);

		// Token: 0x06000A49 RID: 2633
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void QDEndCGContext(IntPtr port, ref IntPtr context);

		// Token: 0x06000A4A RID: 2634
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextTranslateCTM")]
		internal static extern void CGContextTranslateCTM32(IntPtr context, float tx, float ty);

		// Token: 0x06000A4B RID: 2635
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextScaleCTM")]
		internal static extern void CGContextScaleCTM32(IntPtr context, float x, float y);

		// Token: 0x06000A4C RID: 2636
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextTranslateCTM")]
		internal static extern void CGContextTranslateCTM64(IntPtr context, double tx, double ty);

		// Token: 0x06000A4D RID: 2637
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextScaleCTM")]
		internal static extern void CGContextScaleCTM64(IntPtr context, double x, double y);

		// Token: 0x06000A4E RID: 2638
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextFlush(IntPtr context);

		// Token: 0x06000A4F RID: 2639
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSynchronize(IntPtr context);

		// Token: 0x06000A50 RID: 2640
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr CGPathCreateMutable();

		// Token: 0x06000A51 RID: 2641
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon", EntryPoint = "CGContextAddRect")]
		internal static extern void CGContextAddRect32(IntPtr context, CGRect32 rect);

		// Token: 0x06000A52 RID: 2642
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextBeginPath(IntPtr context);

		// Token: 0x06000A53 RID: 2643
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClosePath(IntPtr context);

		// Token: 0x06000A54 RID: 2644
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextAddPath(IntPtr context, IntPtr path);

		// Token: 0x06000A55 RID: 2645
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextClip(IntPtr context);

		// Token: 0x06000A56 RID: 2646
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOClip(IntPtr context);

		// Token: 0x06000A57 RID: 2647
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextEOFillPath(IntPtr context);

		// Token: 0x06000A58 RID: 2648
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextSaveGState(IntPtr context);

		// Token: 0x06000A59 RID: 2649
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern void CGContextRestoreGState(IntPtr context);

		// Token: 0x04000632 RID: 1586
		internal static Hashtable contextReference = new Hashtable();

		// Token: 0x04000633 RID: 1587
		internal static object lockobj = new object();

		// Token: 0x04000634 RID: 1588
		internal static Delegate hwnd_delegate;
	}
}
