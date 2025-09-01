using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020008F8 RID: 2296
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeMethodInfo : MethodInfo, ISerializable
	{
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004D50 RID: 19792 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x000F404D File Offset: 0x000F224D
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x000F39DF File Offset: 0x000F1BDF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x000F4058 File Offset: 0x000F2258
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags format = serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic;
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, format));
			}
			stringBuilder.Append("(");
			RuntimeParameterInfo.FormatParameters(stringBuilder, this.GetParametersNoCopy(), this.CallingConvention, serialization);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x000F40C4 File Offset: 0x000F22C4
		public override Delegate CreateDelegate(Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, this);
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x000F40CD File Offset: 0x000F22CD
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x000F40D7 File Offset: 0x000F22D7
		public override string ToString()
		{
			return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig(false);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x000F40F5 File Offset: 0x000F22F5
		internal RuntimeModule GetRuntimeModule()
		{
			return ((RuntimeType)this.DeclaringType).GetRuntimeModule();
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x000F4108 File Offset: 0x000F2308
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x000F415D File Offset: 0x000F235D
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x000F417C File Offset: 0x000F237C
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, IntPtr.Zero, false);
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x000F4190 File Offset: 0x000F2390
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle, RuntimeTypeHandle reflectedType)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, reflectedType.Value, false);
		}

		// Token: 0x06004D5C RID: 19804
		[PreserveDependency(".ctor(System.Reflection.ExceptionHandlingClause[],System.Reflection.LocalVariableInfo[],System.Byte[],System.Boolean,System.Int32,System.Int32)", "System.Reflection.MethodBody")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBodyInternal(IntPtr handle);

		// Token: 0x06004D5D RID: 19805 RVA: 0x000F41A6 File Offset: 0x000F23A6
		internal static MethodBody GetMethodBody(IntPtr handle)
		{
			return RuntimeMethodInfo.GetMethodBodyInternal(handle);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x000F41AE File Offset: 0x000F23AE
		internal static MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(method_handle, type_handle, true);
		}

		// Token: 0x06004D5F RID: 19807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MethodBase GetMethodFromHandleInternalType_native(IntPtr method_handle, IntPtr type_handle, bool genericCheck);

		// Token: 0x06004D60 RID: 19808 RVA: 0x000F41B8 File Offset: 0x000F23B8
		internal RuntimeMethodInfo()
		{
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x000F41C0 File Offset: 0x000F23C0
		internal RuntimeMethodInfo(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x06004D62 RID: 19810
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x06004D63 RID: 19811
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodInfo get_base_method(RuntimeMethodInfo method, bool definition);

		// Token: 0x06004D64 RID: 19812
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeMethodInfo method);

		// Token: 0x06004D65 RID: 19813 RVA: 0x000F41D5 File Offset: 0x000F23D5
		public override MethodInfo GetBaseDefinition()
		{
			return RuntimeMethodInfo.get_base_method(this, true);
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x000F41DE File Offset: 0x000F23DE
		internal MethodInfo GetBaseMethod()
		{
			return RuntimeMethodInfo.get_base_method(this, false);
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x000F41E7 File Offset: 0x000F23E7
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x000F41EF File Offset: 0x000F23EF
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004D69 RID: 19817 RVA: 0x000F41E7 File Offset: 0x000F23E7
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x000F41FC File Offset: 0x000F23FC
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x000F4204 File Offset: 0x000F2404
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x000F4214 File Offset: 0x000F2414
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo.Length == 0)
			{
				return parametersInfo;
			}
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			Array.FastCopy(parametersInfo, 0, array, 0, parametersInfo.Length);
			return array;
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x000F424B File Offset: 0x000F244B
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x000F4259 File Offset: 0x000F2459
		internal override int GetParametersCount()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this).Length;
		}

		// Token: 0x06004D6F RID: 19823
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06004D70 RID: 19824 RVA: 0x000F426C File Offset: 0x000F246C
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (!base.IsStatic && !this.DeclaringType.IsInstanceOfType(obj))
			{
				if (obj == null)
				{
					throw new TargetException("Non-static method requires a target.");
				}
				throw new TargetException("Object does not match target type.");
			}
			else
			{
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				ParameterInfo[] parametersInternal = this.GetParametersInternal();
				RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInternal, culture, invokeAttr);
				if (this.ContainsGenericParameters)
				{
					throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
				}
				object result = null;
				Exception ex;
				if ((invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default)
				{
					try
					{
						result = this.InternalInvoke(obj, parameters, out ex);
						goto IL_90;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (OverflowException)
					{
						throw;
					}
					catch (Exception inner)
					{
						throw new TargetInvocationException(inner);
					}
				}
				result = this.InternalInvoke(obj, parameters, out ex);
				IL_90:
				if (ex != null)
				{
					throw ex;
				}
				return result;
			}
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x000F4338 File Offset: 0x000F2538
		internal static void ConvertValues(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					ParameterInfo parameterInfo = pinfo[i];
					if (obj == Type.Missing)
					{
						if (parameterInfo.DefaultValue == DBNull.Value)
						{
							throw new ArgumentException(Environment.GetResourceString("Missing parameter does not have a default value."), "parameters");
						}
						args[i] = parameterInfo.DefaultValue;
					}
					else
					{
						RuntimeType runtimeType = (RuntimeType)parameterInfo.ParameterType;
						args[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
					}
				}
				return;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x000F43C6 File Offset: 0x000F25C6
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x000F43D3 File Offset: 0x000F25D3
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x000F43E0 File Offset: 0x000F25E0
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x000F43ED File Offset: 0x000F25ED
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x000F43F5 File Offset: 0x000F25F5
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x000F4402 File Offset: 0x000F2602
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return RuntimeMethodInfo.get_name(this);
			}
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D7B RID: 19835
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPInvoke(out PInvokeAttributes flags, out string entryPoint, out string dllName);

		// Token: 0x06004D7C RID: 19836 RVA: 0x000F441C File Offset: 0x000F261C
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = DllImportAttribute.GetCustomAttribute(this);
			}
			return array;
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x000F44A0 File Offset: 0x000F26A0
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new CustomAttributeData(typeof(PreserveSigAttribute).GetConstructor(Type.EmptyTypes));
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = this.GetDllImportAttributeData();
			}
			return array;
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x000F4538 File Offset: 0x000F2738
		private CustomAttributeData GetDllImportAttributeData()
		{
			if ((this.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string value = null;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string value2;
			this.GetPInvoke(out pinvokeAttributes, out value2, out value);
			CharSet charSet;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				goto IL_5C;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				goto IL_5C;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				goto IL_5C;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				goto IL_5C;
			}
			charSet = CharSet.None;
			IL_5C:
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			CallingConvention callingConvention;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvWinapi)
				{
					callingConvention = System.Runtime.InteropServices.CallingConvention.Winapi;
					goto IL_BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
				{
					callingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
					goto IL_BB;
				}
			}
			else
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvStdcall)
				{
					callingConvention = System.Runtime.InteropServices.CallingConvention.StdCall;
					goto IL_BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvThiscall)
				{
					callingConvention = System.Runtime.InteropServices.CallingConvention.ThisCall;
					goto IL_BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
				{
					callingConvention = System.Runtime.InteropServices.CallingConvention.FastCall;
					goto IL_BB;
				}
			}
			callingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
			IL_BB:
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (this.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			CustomAttributeTypedArgument[] ctorArgs = new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(string), value)
			};
			Type typeFromHandle = typeof(DllImportAttribute);
			CustomAttributeNamedArgument[] namedArgs = new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), value2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), charSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), flag),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), flag2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), flag5),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), callingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), flag3),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), flag4)
			};
			return new CustomAttributeData(typeFromHandle.GetConstructor(new Type[]
			{
				typeof(string)
			}), ctorArgs, namedArgs);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x000F4784 File Offset: 0x000F2984
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("not a generic method definition");
			}
			if (this.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length");
			}
			bool flag = false;
			foreach (Type type in methodInstantiation)
			{
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				if (!(type is RuntimeType))
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (RuntimeFeature.IsDynamicCodeSupported)
				{
					return new MethodOnTypeBuilderInst(this, methodInstantiation);
				}
				throw new NotSupportedException("User types are not supported under full aot");
			}
			else
			{
				MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
				if (methodInfo == null)
				{
					throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
				}
				return methodInfo;
			}
		}

		// Token: 0x06004D80 RID: 19840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x06004D81 RID: 19841
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x06004D82 RID: 19842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x06004D83 RID: 19843 RVA: 0x000F4849 File Offset: 0x000F2A49
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004D84 RID: 19844
		public override extern bool IsGenericMethodDefinition { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004D85 RID: 19845
		public override extern bool IsGenericMethod { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x000F4860 File Offset: 0x000F2A60
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x000F48A1 File Offset: 0x000F2AA1
		public override MethodBody GetMethodBody()
		{
			return RuntimeMethodInfo.GetMethodBody(this.mhandle);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x000F3C38 File Offset: 0x000F1E38
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D89 RID: 19849
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x000F48AE File Offset: 0x000F2AAE
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x000F48B9 File Offset: 0x000F2AB9
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x000F48C4 File Offset: 0x000F2AC4
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x000F48CF File Offset: 0x000F2ACF
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeMethodInfo>(other);
		}

		// Token: 0x0400305A RID: 12378
		internal IntPtr mhandle;

		// Token: 0x0400305B RID: 12379
		private string name;

		// Token: 0x0400305C RID: 12380
		private Type reftype;
	}
}
