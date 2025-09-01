using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class AndroidJavaProxy
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002168 File Offset: 0x00000368
		public AndroidJavaProxy(string javaInterface) : this(new AndroidJavaClass(javaInterface))
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002194 File Offset: 0x00000394
		~AndroidJavaProxy()
		{
			AndroidJNISafe.DeleteWeakGlobalRef(this.proxyObject);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			int num = 0;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				bool flag = args[i] == null;
				if (flag)
				{
					array[i] = null;
					num++;
				}
				else
				{
					array[i] = args[i].GetType();
				}
			}
			try
			{
				MethodInfo methodInfo = null;
				bool flag2 = num > 0;
				if (flag2)
				{
					MethodInfo[] methods = base.GetType().GetMethods(bindingAttr);
					int num2 = 0;
					foreach (MethodInfo methodInfo2 in methods)
					{
						bool flag3 = methodName != methodInfo2.Name;
						if (!flag3)
						{
							ParameterInfo[] parameters = methodInfo2.GetParameters();
							bool flag4 = parameters.Length != args.Length;
							if (!flag4)
							{
								bool flag5 = true;
								for (int k = 0; k < parameters.Length; k++)
								{
									bool flag6 = array[k] == null;
									if (flag6)
									{
										bool isValueType = parameters[k].ParameterType.IsValueType;
										if (isValueType)
										{
											flag5 = false;
											break;
										}
									}
									else
									{
										bool flag7 = !parameters[k].ParameterType.IsAssignableFrom(array[k]);
										if (flag7)
										{
											flag5 = false;
											break;
										}
									}
								}
								bool flag8 = !flag5;
								if (!flag8)
								{
									num2++;
									methodInfo = methodInfo2;
								}
							}
						}
					}
					bool flag9 = num2 > 1;
					if (flag9)
					{
						throw new Exception("Ambiguous overloads found for " + methodName + " with given parameters");
					}
				}
				else
				{
					methodInfo = base.GetType().GetMethod(methodName, bindingAttr, null, array, null);
				}
				bool flag10 = methodInfo != null;
				if (flag10)
				{
					return _AndroidJNIHelper.Box(methodInfo.Invoke(this, args));
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array3 = new string[args.Length];
			for (int l = 0; l < array3.Length; l++)
			{
				bool flag11 = array[l] == null;
				if (flag11)
				{
					array3[l] = "null";
				}
				else
				{
					array3[l] = array[l].ToString();
				}
			}
			bool flag12 = ex != null;
			if (flag12)
			{
				string[] array4 = new string[6];
				int num3 = 0;
				Type type = base.GetType();
				array4[num3] = ((type != null) ? type.ToString() : null);
				array4[1] = ".";
				array4[2] = methodName;
				array4[3] = "(";
				array4[4] = string.Join(",", array3);
				array4[5] = ")";
				throw new TargetInvocationException(string.Concat(array4), ex);
			}
			string[] array5 = new string[7];
			array5[0] = "No such proxy method: ";
			int num4 = 1;
			Type type2 = base.GetType();
			array5[num4] = ((type2 != null) ? type2.ToString() : null);
			array5[2] = ".";
			array5[3] = methodName;
			array5[4] = "(";
			array5[5] = string.Join(",", array3);
			array5[6] = ")";
			Exception ex4 = new Exception(string.Concat(array5));
			IntPtr intPtr = AndroidReflection.CreateInvocationError(ex4, true);
			return (intPtr == IntPtr.Zero) ? null : new AndroidJavaObject(intPtr);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002504 File Offset: 0x00000704
		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
				bool flag = !(array[i] is AndroidJavaObject);
				if (flag)
				{
					bool flag2 = javaArgs[i] != null;
					if (flag2)
					{
						javaArgs[i].Dispose();
					}
				}
			}
			return this.Invoke(methodName, array);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002574 File Offset: 0x00000774
		public virtual bool equals(AndroidJavaObject obj)
		{
			IntPtr obj2 = (obj == null) ? IntPtr.Zero : obj.GetRawObject();
			return AndroidJNI.IsSameObject(this.proxyObject, obj2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025A4 File Offset: 0x000007A4
		public virtual int hashCode()
		{
			jvalue[] array = new jvalue[1];
			array[0].l = this.GetRawProxy();
			return AndroidJNISafe.CallStaticIntMethod(AndroidJavaProxy.s_JavaLangSystemClass, AndroidJavaProxy.s_HashCodeMethodID, array);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025E4 File Offset: 0x000007E4
		public virtual string toString()
		{
			return ((this != null) ? this.ToString() : null) + " <c# proxy java object>";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002610 File Offset: 0x00000810
		internal AndroidJavaObject GetProxyObject()
		{
			return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(this.GetRawProxy());
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002630 File Offset: 0x00000830
		internal IntPtr GetRawProxy()
		{
			IntPtr intPtr = IntPtr.Zero;
			bool flag = this.proxyObject != IntPtr.Zero;
			if (flag)
			{
				intPtr = AndroidJNI.NewLocalRef(this.proxyObject);
				bool flag2 = intPtr == IntPtr.Zero;
				if (flag2)
				{
					AndroidJNI.DeleteWeakGlobalRef(this.proxyObject);
					this.proxyObject = IntPtr.Zero;
				}
			}
			bool flag3 = intPtr == IntPtr.Zero;
			if (flag3)
			{
				intPtr = AndroidJNIHelper.CreateJavaProxy(this);
				this.proxyObject = AndroidJNI.NewWeakGlobalRef(intPtr);
			}
			return intPtr;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026B9 File Offset: 0x000008B9
		// Note: this type is marked as 'beforefieldinit'.
		static AndroidJavaProxy()
		{
		}

		// Token: 0x04000005 RID: 5
		public readonly AndroidJavaClass javaInterface;

		// Token: 0x04000006 RID: 6
		internal IntPtr proxyObject = IntPtr.Zero;

		// Token: 0x04000007 RID: 7
		private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

		// Token: 0x04000008 RID: 8
		private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(AndroidJavaProxy.s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", true);
	}
}
