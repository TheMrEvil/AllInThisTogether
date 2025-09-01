using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Android;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeConditional("PLATFORM_ANDROID")]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[UsedByNativeCode]
	public static class AndroidJNIHelper
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		public static extern bool debug { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600006A RID: 106 RVA: 0x000064A4 File Offset: 0x000046A4
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return AndroidJNIHelper.GetConstructorID(javaClass, "");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000064C4 File Offset: 0x000046C4
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000064E0 File Offset: 0x000046E0
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, "", false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006500 File Offset: 0x00004700
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000651C File Offset: 0x0000471C
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006538 File Offset: 0x00004738
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, "", false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006558 File Offset: 0x00004758
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, false);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006574 File Offset: 0x00004774
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006590 File Offset: 0x00004790
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000065A8 File Offset: 0x000047A8
		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle value = GCHandle.Alloc(proxy);
			IntPtr result;
			try
			{
				result = _AndroidJNIHelper.CreateJavaProxy(Common.GetActivity().Get<AndroidJavaObject>("mUnityPlayer").GetRawObject(), GCHandle.ToIntPtr(value), proxy);
			}
			catch
			{
				value.Free();
				throw;
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006600 File Offset: 0x00004800
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006618 File Offset: 0x00004818
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006630 File Offset: 0x00004830
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000663C File Offset: 0x0000483C
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006658 File Offset: 0x00004858
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006674 File Offset: 0x00004874
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000668C File Offset: 0x0000488C
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000066A4 File Offset: 0x000048A4
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000066BC File Offset: 0x000048BC
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000066D8 File Offset: 0x000048D8
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000066F4 File Offset: 0x000048F4
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
