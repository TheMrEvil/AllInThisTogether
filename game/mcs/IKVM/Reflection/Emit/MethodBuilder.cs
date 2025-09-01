using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E8 RID: 232
	public sealed class MethodBuilder : MethodInfo
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x000257E8 File Offset: 0x000239E8
		internal MethodBuilder(TypeBuilder typeBuilder, string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			this.typeBuilder = typeBuilder;
			this.name = name;
			this.pseudoToken = typeBuilder.ModuleBuilder.AllocPseudoToken();
			this.attributes = attributes;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				callingConvention |= CallingConventions.HasThis;
			}
			this.callingConvention = callingConvention;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00025844 File Offset: 0x00023A44
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(16);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002584E File Offset: 0x00023A4E
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.rva != -1)
			{
				throw new InvalidOperationException();
			}
			if (this.ilgen == null)
			{
				this.ilgen = new ILGenerator(this.typeBuilder.ModuleBuilder, streamSize);
			}
			return this.ilgen;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00025884 File Offset: 0x00023A84
		public void __ReleaseILGenerator()
		{
			if (this.ilgen != null)
			{
				this.rva = this.ilgen.WriteBody(this.initLocals);
				this.ilgen = null;
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000258AC File Offset: 0x00023AAC
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000258BC File Offset: 0x00023ABC
		private void SetDllImportPseudoCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			CallingConvention? fieldValue = customBuilder.GetFieldValue<CallingConvention>("CallingConvention");
			CharSet? fieldValue2 = customBuilder.GetFieldValue<CharSet>("CharSet");
			this.SetDllImportPseudoCustomAttribute((string)customBuilder.GetConstructorArgument(0), (string)customBuilder.GetFieldValue("EntryPoint"), fieldValue, fieldValue2, (bool?)customBuilder.GetFieldValue("BestFitMapping"), (bool?)customBuilder.GetFieldValue("ThrowOnUnmappableChar"), (bool?)customBuilder.GetFieldValue("SetLastError"), (bool?)customBuilder.GetFieldValue("PreserveSig"), (bool?)customBuilder.GetFieldValue("ExactSpelling"));
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00025958 File Offset: 0x00023B58
		internal void SetDllImportPseudoCustomAttribute(string dllName, string entryName, CallingConvention? nativeCallConv, CharSet? nativeCharSet, bool? bestFitMapping, bool? throwOnUnmappableChar, bool? setLastError, bool? preserveSig, bool? exactSpelling)
		{
			short num = 256;
			if (bestFitMapping != null)
			{
				num |= (bestFitMapping.Value ? 16 : 32);
			}
			if (throwOnUnmappableChar != null)
			{
				num |= (throwOnUnmappableChar.Value ? 4096 : 8192);
			}
			if (nativeCallConv != null)
			{
				num &= -1793;
				switch (nativeCallConv.Value)
				{
				case System.Runtime.InteropServices.CallingConvention.Winapi:
					num |= 256;
					break;
				case System.Runtime.InteropServices.CallingConvention.Cdecl:
					num |= 512;
					break;
				case System.Runtime.InteropServices.CallingConvention.StdCall:
					num |= 768;
					break;
				case System.Runtime.InteropServices.CallingConvention.ThisCall:
					num |= 1024;
					break;
				case System.Runtime.InteropServices.CallingConvention.FastCall:
					num |= 1280;
					break;
				}
			}
			if (nativeCharSet != null)
			{
				num &= -7;
				switch (nativeCharSet.Value)
				{
				case CharSet.None:
				case CharSet.Ansi:
					num |= 2;
					break;
				case CharSet.Unicode:
					num |= 4;
					break;
				case CharSet.Auto:
					num |= 6;
					break;
				}
			}
			if (exactSpelling != null && exactSpelling.Value)
			{
				num |= 1;
			}
			if (preserveSig == null || preserveSig.Value)
			{
				this.implFlags |= MethodImplAttributes.PreserveSig;
			}
			if (setLastError != null && setLastError.Value)
			{
				num |= 64;
			}
			ImplMapTable.Record newRecord = default(ImplMapTable.Record);
			newRecord.MappingFlags = num;
			newRecord.MemberForwarded = this.pseudoToken;
			newRecord.ImportName = this.ModuleBuilder.Strings.Add(entryName ?? this.name);
			newRecord.ImportScope = this.ModuleBuilder.ModuleRef.FindOrAddRecord((dllName == null) ? 0 : this.ModuleBuilder.Strings.Add(dllName));
			this.ModuleBuilder.ImplMap.AddRecord(newRecord);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00025B2C File Offset: 0x00023D2C
		private void SetMethodImplAttribute(CustomAttributeBuilder customBuilder)
		{
			int parameterCount = customBuilder.Constructor.ParameterCount;
			MethodImplOptions methodImplOptions;
			if (parameterCount != 0)
			{
				if (parameterCount != 1)
				{
					throw new NotSupportedException();
				}
				object constructorArgument = customBuilder.GetConstructorArgument(0);
				if (constructorArgument is short)
				{
					methodImplOptions = (MethodImplOptions)((short)constructorArgument);
				}
				else if (constructorArgument is int)
				{
					methodImplOptions = (MethodImplOptions)((int)constructorArgument);
				}
				else
				{
					methodImplOptions = (MethodImplOptions)constructorArgument;
				}
			}
			else
			{
				methodImplOptions = (MethodImplOptions)0;
			}
			MethodCodeType? fieldValue = customBuilder.GetFieldValue<MethodCodeType>("MethodCodeType");
			this.implFlags = (MethodImplAttributes)methodImplOptions;
			if (fieldValue != null)
			{
				this.implFlags |= (MethodImplAttributes)fieldValue.Value;
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00025BBC File Offset: 0x00023DBC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			KnownCA knownCA = customBuilder.KnownCA;
			if (knownCA <= KnownCA.MethodImplAttribute)
			{
				if (knownCA == KnownCA.DllImportAttribute)
				{
					this.SetDllImportPseudoCustomAttribute(customBuilder.DecodeBlob(this.Module.Assembly));
					this.attributes |= MethodAttributes.PinvokeImpl;
					return;
				}
				if (knownCA == KnownCA.MethodImplAttribute)
				{
					this.SetMethodImplAttribute(customBuilder.DecodeBlob(this.Module.Assembly));
					return;
				}
			}
			else
			{
				if (knownCA == KnownCA.PreserveSigAttribute)
				{
					this.implFlags |= MethodImplAttributes.PreserveSig;
					return;
				}
				if (knownCA == KnownCA.SpecialNameAttribute)
				{
					this.attributes |= MethodAttributes.SpecialName;
					return;
				}
				if (knownCA == KnownCA.SuppressUnmanagedCodeSecurityAttribute)
				{
					this.attributes |= MethodAttributes.HasSecurity;
				}
			}
			this.ModuleBuilder.SetCustomAttribute(this.pseudoToken, customBuilder);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00025C7D File Offset: 0x00023E7D
		public void __AddDeclarativeSecurity(CustomAttributeBuilder customBuilder)
		{
			this.attributes |= MethodAttributes.HasSecurity;
			if (this.declarativeSecurity == null)
			{
				this.declarativeSecurity = new List<CustomAttributeBuilder>();
			}
			this.declarativeSecurity.Add(customBuilder);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00025CB0 File Offset: 0x00023EB0
		public void AddDeclarativeSecurity(SecurityAction securityAction, PermissionSet permissionSet)
		{
			this.ModuleBuilder.AddDeclarativeSecurity(this.pseudoToken, securityAction, permissionSet);
			this.attributes |= MethodAttributes.HasSecurity;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00025CD7 File Offset: 0x00023ED7
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.implFlags = attributes;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00025CE0 File Offset: 0x00023EE0
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			if (this.parameters == null)
			{
				this.parameters = new List<ParameterBuilder>();
			}
			this.ModuleBuilder.Param.AddVirtualRecord();
			ParameterBuilder parameterBuilder = new ParameterBuilder(this.ModuleBuilder, position, attributes, strParamName);
			if (this.parameters.Count == 0 || position >= this.parameters[this.parameters.Count - 1].Position)
			{
				this.parameters.Add(parameterBuilder);
			}
			else
			{
				for (int i = 0; i < this.parameters.Count; i++)
				{
					if (this.parameters[i].Position > position)
					{
						this.parameters.Insert(i, parameterBuilder);
						break;
					}
				}
			}
			return parameterBuilder;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00025D95 File Offset: 0x00023F95
		private void CheckSig()
		{
			if (this.methodSignature != null)
			{
				throw new InvalidOperationException("The method signature can not be modified after it has been used.");
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00025DAA File Offset: 0x00023FAA
		public void SetParameters(params Type[] parameterTypes)
		{
			this.CheckSig();
			this.parameterTypes = Util.Copy(parameterTypes);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00025DBE File Offset: 0x00023FBE
		public void SetReturnType(Type returnType)
		{
			this.CheckSig();
			this.returnType = (returnType ?? this.Module.universe.System_Void);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00025DE1 File Offset: 0x00023FE1
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.SetSignature(returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeOptionalCustomModifiers, returnTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, parameterTypeRequiredCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00025DFE File Offset: 0x00023FFE
		public void __SetSignature(Type returnType, CustomModifiers returnTypeCustomModifiers, Type[] parameterTypes, CustomModifiers[] parameterTypeCustomModifiers)
		{
			this.SetSignature(returnType, parameterTypes, PackedCustomModifiers.CreateFromExternal(returnTypeCustomModifiers, parameterTypeCustomModifiers, Util.NullSafeLength<Type>(parameterTypes)));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00025E16 File Offset: 0x00024016
		private void SetSignature(Type returnType, Type[] parameterTypes, PackedCustomModifiers customModifiers)
		{
			this.CheckSig();
			this.returnType = (returnType ?? this.Module.universe.System_Void);
			this.parameterTypes = Util.Copy(parameterTypes);
			this.customModifiers = customModifiers;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00025E4C File Offset: 0x0002404C
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			this.CheckSig();
			if (this.gtpb != null)
			{
				throw new InvalidOperationException("Generic parameters already defined.");
			}
			this.gtpb = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				this.gtpb[i] = new GenericTypeParameterBuilder(names[i], this, i);
			}
			return (GenericTypeParameterBuilder[])this.gtpb.Clone();
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00025EB0 File Offset: 0x000240B0
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return new GenericMethodInstance(this.typeBuilder, this, typeArguments);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00025EBF File Offset: 0x000240BF
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (this.gtpb == null)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00025ED0 File Offset: 0x000240D0
		public override Type[] GetGenericArguments()
		{
			return Util.Copy(this.gtpb);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00025EDD File Offset: 0x000240DD
		internal override Type GetGenericMethodArgument(int index)
		{
			return this.gtpb[index];
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00025EE7 File Offset: 0x000240E7
		internal override int GetGenericMethodArgumentCount()
		{
			if (this.gtpb != null)
			{
				return this.gtpb.Length;
			}
			return 0;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00025EFB File Offset: 0x000240FB
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00025F03 File Offset: 0x00024103
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new MethodBuilder.ParameterInfoImpl(this, -1);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00025F0C File Offset: 0x0002410C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00025F14 File Offset: 0x00024114
		public void __SetAttributes(MethodAttributes attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00025F1D File Offset: 0x0002411D
		public void __SetCallingConvention(CallingConventions callingConvention)
		{
			this.callingConvention = callingConvention;
			this.methodSignature = null;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00025F2D File Offset: 0x0002412D
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.implFlags;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00025F38 File Offset: 0x00024138
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] array = new ParameterInfo[this.parameterTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new MethodBuilder.ParameterInfoImpl(this, i);
			}
			return array;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00025F6C File Offset: 0x0002416C
		internal override int ParameterCount
		{
			get
			{
				return this.parameterTypes.Length;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00025F76 File Offset: 0x00024176
		public override Type DeclaringType
		{
			get
			{
				if (!this.typeBuilder.IsModulePseudoType)
				{
					return this.typeBuilder;
				}
				return null;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00025F8D File Offset: 0x0002418D
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00025F95 File Offset: 0x00024195
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00025F9D File Offset: 0x0002419D
		public override int MetadataToken
		{
			get
			{
				return this.pseudoToken;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00025FA5 File Offset: 0x000241A5
		public override bool IsGenericMethod
		{
			get
			{
				return this.gtpb != null;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00025FA5 File Offset: 0x000241A5
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.gtpb != null;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00025FB0 File Offset: 0x000241B0
		public override Module Module
		{
			get
			{
				return this.typeBuilder.Module;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00025FB0 File Offset: 0x000241B0
		public Module GetModule()
		{
			return this.typeBuilder.Module;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00025FBD File Offset: 0x000241BD
		public MethodToken GetToken()
		{
			return new MethodToken(this.pseudoToken);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000225C File Offset: 0x0000045C
		public override MethodBody GetMethodBody()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override int __MethodRVA
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00025FCA File Offset: 0x000241CA
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00025FD2 File Offset: 0x000241D2
		public bool InitLocals
		{
			get
			{
				return this.initLocals;
			}
			set
			{
				this.initLocals = value;
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00025FDB File Offset: 0x000241DB
		public void __AddUnmanagedExport(string name, int ordinal)
		{
			this.ModuleBuilder.AddUnmanagedExport(name, ordinal, this, new RelativeVirtualAddress(uint.MaxValue));
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00025FF1 File Offset: 0x000241F1
		public void CreateMethodBody(byte[] il, int count)
		{
			if (il == null)
			{
				throw new NotSupportedException();
			}
			if (il.Length != count)
			{
				Array.Resize<byte>(ref il, count);
			}
			this.SetMethodBody(il, 16, null, null, null);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00026018 File Offset: 0x00024218
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			ByteBuffer methodBodies = this.ModuleBuilder.methodBodies;
			if (localSignature == null && exceptionHandlers == null && maxStack <= 8 && il.Length < 64)
			{
				this.rva = methodBodies.Position;
				ILGenerator.WriteTinyHeader(methodBodies, il.Length);
			}
			else
			{
				methodBodies.Align(4);
				this.rva = methodBodies.Position;
				ILGenerator.WriteFatHeader(methodBodies, this.initLocals, exceptionHandlers != null, (ushort)maxStack, il.Length, (localSignature == null) ? 0 : this.ModuleBuilder.GetSignatureToken(localSignature, localSignature.Length).Token);
			}
			if (tokenFixups != null)
			{
				ILGenerator.AddTokenFixups(methodBodies.Position, this.ModuleBuilder.tokenFixupOffsets, tokenFixups);
			}
			methodBodies.Write(il);
			if (exceptionHandlers != null)
			{
				List<ILGenerator.ExceptionBlock> list = new List<ILGenerator.ExceptionBlock>();
				foreach (ExceptionHandler h in exceptionHandlers)
				{
					list.Add(new ILGenerator.ExceptionBlock(h));
				}
				ILGenerator.WriteExceptionHandlers(methodBodies, list);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00026118 File Offset: 0x00024318
		internal void Bake()
		{
			this.nameIndex = this.ModuleBuilder.Strings.Add(this.name);
			this.signature = this.ModuleBuilder.GetSignatureBlobIndex(this.MethodSignature);
			this.__ReleaseILGenerator();
			if (this.declarativeSecurity != null)
			{
				this.ModuleBuilder.AddDeclarativeSecurity(this.pseudoToken, this.declarativeSecurity);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002617D File Offset: 0x0002437D
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.typeBuilder.ModuleBuilder;
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002618C File Offset: 0x0002438C
		internal void WriteMethodDefRecord(int baseRVA, MetadataWriter mw, ref int paramList)
		{
			if (this.rva != -1)
			{
				mw.Write(this.rva + baseRVA);
			}
			else
			{
				mw.Write(0);
			}
			mw.Write((short)this.implFlags);
			mw.Write((short)this.attributes);
			mw.WriteStringIndex(this.nameIndex);
			mw.WriteBlobIndex(this.signature);
			mw.WriteParam(paramList);
			if (this.parameters != null)
			{
				paramList += this.parameters.Count;
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002620C File Offset: 0x0002440C
		internal void WriteParamRecords(MetadataWriter mw)
		{
			if (this.parameters != null)
			{
				foreach (ParameterBuilder parameterBuilder in this.parameters)
				{
					parameterBuilder.WriteParamRecord(mw);
				}
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00026268 File Offset: 0x00024468
		internal void FixupToken(int token, ref int parameterToken)
		{
			this.typeBuilder.ModuleBuilder.RegisterTokenFixup(this.pseudoToken, token);
			if (this.parameters != null)
			{
				foreach (ParameterBuilder parameterBuilder in this.parameters)
				{
					int num = parameterToken;
					parameterToken = num + 1;
					parameterBuilder.FixupToken(num);
				}
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000262E0 File Offset: 0x000244E0
		internal override MethodSignature MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					this.methodSignature = MethodSignature.MakeFromBuilder(this.returnType ?? this.typeBuilder.Universe.System_Void, this.parameterTypes ?? Type.EmptyTypes, this.customModifiers, this.callingConvention, (this.gtpb == null) ? 0 : this.gtpb.Length);
				}
				return this.methodSignature;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002634E File Offset: 0x0002454E
		internal override int ImportTo(ModuleBuilder other)
		{
			return other.ImportMethodOrField(this.typeBuilder, this.name, this.MethodSignature);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00026368 File Offset: 0x00024568
		internal void CheckBaked()
		{
			this.typeBuilder.CheckBaked();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00026375 File Offset: 0x00024575
		internal override int GetCurrentToken()
		{
			if (this.typeBuilder.ModuleBuilder.IsSaved)
			{
				return this.typeBuilder.ModuleBuilder.ResolvePseudoToken(this.pseudoToken);
			}
			return this.pseudoToken;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000263A6 File Offset: 0x000245A6
		internal override bool IsBaked
		{
			get
			{
				return this.typeBuilder.IsBaked;
			}
		}

		// Token: 0x040004B0 RID: 1200
		private readonly TypeBuilder typeBuilder;

		// Token: 0x040004B1 RID: 1201
		private readonly string name;

		// Token: 0x040004B2 RID: 1202
		private readonly int pseudoToken;

		// Token: 0x040004B3 RID: 1203
		private int nameIndex;

		// Token: 0x040004B4 RID: 1204
		private int signature;

		// Token: 0x040004B5 RID: 1205
		private Type returnType;

		// Token: 0x040004B6 RID: 1206
		private Type[] parameterTypes;

		// Token: 0x040004B7 RID: 1207
		private PackedCustomModifiers customModifiers;

		// Token: 0x040004B8 RID: 1208
		private MethodAttributes attributes;

		// Token: 0x040004B9 RID: 1209
		private MethodImplAttributes implFlags;

		// Token: 0x040004BA RID: 1210
		private ILGenerator ilgen;

		// Token: 0x040004BB RID: 1211
		private int rva = -1;

		// Token: 0x040004BC RID: 1212
		private CallingConventions callingConvention;

		// Token: 0x040004BD RID: 1213
		private List<ParameterBuilder> parameters;

		// Token: 0x040004BE RID: 1214
		private GenericTypeParameterBuilder[] gtpb;

		// Token: 0x040004BF RID: 1215
		private List<CustomAttributeBuilder> declarativeSecurity;

		// Token: 0x040004C0 RID: 1216
		private MethodSignature methodSignature;

		// Token: 0x040004C1 RID: 1217
		private bool initLocals = true;

		// Token: 0x0200036C RID: 876
		private sealed class ParameterInfoImpl : ParameterInfo
		{
			// Token: 0x06002652 RID: 9810 RVA: 0x000B6067 File Offset: 0x000B4267
			internal ParameterInfoImpl(MethodBuilder method, int parameter)
			{
				this.method = method;
				this.parameter = parameter;
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06002653 RID: 9811 RVA: 0x000B6080 File Offset: 0x000B4280
			private ParameterBuilder ParameterBuilder
			{
				get
				{
					if (this.method.parameters != null)
					{
						foreach (ParameterBuilder parameterBuilder in this.method.parameters)
						{
							if (parameterBuilder.Position - 1 == this.parameter)
							{
								return parameterBuilder;
							}
						}
					}
					return null;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06002654 RID: 9812 RVA: 0x000B60F8 File Offset: 0x000B42F8
			public override string Name
			{
				get
				{
					ParameterBuilder parameterBuilder = this.ParameterBuilder;
					if (parameterBuilder == null)
					{
						return null;
					}
					return parameterBuilder.Name;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x06002655 RID: 9813 RVA: 0x000B6117 File Offset: 0x000B4317
			public override Type ParameterType
			{
				get
				{
					if (this.parameter != -1)
					{
						return this.method.parameterTypes[this.parameter];
					}
					return this.method.returnType;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x06002656 RID: 9814 RVA: 0x000B6140 File Offset: 0x000B4340
			public override ParameterAttributes Attributes
			{
				get
				{
					ParameterBuilder parameterBuilder = this.ParameterBuilder;
					if (parameterBuilder == null)
					{
						return ParameterAttributes.None;
					}
					return (ParameterAttributes)parameterBuilder.Attributes;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x06002657 RID: 9815 RVA: 0x000B615F File Offset: 0x000B435F
			public override int Position
			{
				get
				{
					return this.parameter;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002658 RID: 9816 RVA: 0x000B6168 File Offset: 0x000B4368
			public override object RawDefaultValue
			{
				get
				{
					ParameterBuilder parameterBuilder = this.ParameterBuilder;
					if (parameterBuilder != null && (parameterBuilder.Attributes & 4096) != 0)
					{
						return this.method.ModuleBuilder.Constant.GetRawConstantValue(this.method.ModuleBuilder, parameterBuilder.PseudoToken);
					}
					if (parameterBuilder != null && (parameterBuilder.Attributes & 16) != 0)
					{
						return Missing.Value;
					}
					return null;
				}
			}

			// Token: 0x06002659 RID: 9817 RVA: 0x000B61C9 File Offset: 0x000B43C9
			public override CustomModifiers __GetCustomModifiers()
			{
				return this.method.customModifiers.GetParameterCustomModifiers(this.parameter);
			}

			// Token: 0x0600265A RID: 9818 RVA: 0x000B3F81 File Offset: 0x000B2181
			public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
			{
				fieldMarshal = default(FieldMarshal);
				return false;
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x0600265B RID: 9819 RVA: 0x000B61E1 File Offset: 0x000B43E1
			public override MemberInfo Member
			{
				get
				{
					return this.method;
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x0600265C RID: 9820 RVA: 0x000B61EC File Offset: 0x000B43EC
			public override int MetadataToken
			{
				get
				{
					ParameterBuilder parameterBuilder = this.ParameterBuilder;
					if (parameterBuilder == null)
					{
						return 134217728;
					}
					return parameterBuilder.PseudoToken;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x0600265D RID: 9821 RVA: 0x000B620F File Offset: 0x000B440F
			internal override Module Module
			{
				get
				{
					return this.method.Module;
				}
			}

			// Token: 0x04000F1F RID: 3871
			private readonly MethodBuilder method;

			// Token: 0x04000F20 RID: 3872
			private readonly int parameter;
		}
	}
}
