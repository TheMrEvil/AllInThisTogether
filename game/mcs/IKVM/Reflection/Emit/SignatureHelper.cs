using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F1 RID: 241
	public abstract class SignatureHelper
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002AEF3 File Offset: 0x000290F3
		private SignatureHelper(byte type)
		{
			this.type = type;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002AF02 File Offset: 0x00029102
		internal bool HasThis
		{
			get
			{
				return (this.type & 32) > 0;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BC6 RID: 3014
		internal abstract Type ReturnType { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0002AF10 File Offset: 0x00029110
		internal int ParameterCount
		{
			get
			{
				return (int)this.paramCount;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002AF18 File Offset: 0x00029118
		private static SignatureHelper Create(Module mod, byte type, Type returnType)
		{
			ModuleBuilder moduleBuilder = mod as ModuleBuilder;
			if (moduleBuilder != null)
			{
				return new SignatureHelper.Eager(moduleBuilder, type, returnType);
			}
			return new SignatureHelper.Lazy(type);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002AF3E File Offset: 0x0002913E
		public static SignatureHelper GetFieldSigHelper(Module mod)
		{
			return SignatureHelper.Create(mod, 6, null);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002AF48 File Offset: 0x00029148
		public static SignatureHelper GetLocalVarSigHelper()
		{
			return new SignatureHelper.Lazy(7);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002AF50 File Offset: 0x00029150
		public static SignatureHelper GetLocalVarSigHelper(Module mod)
		{
			return SignatureHelper.Create(mod, 7, null);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002AF5A File Offset: 0x0002915A
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			SignatureHelper signatureHelper = SignatureHelper.Create(mod, 8, returnType);
			signatureHelper.AddArgument(returnType);
			signatureHelper.paramCount = 0;
			signatureHelper.AddArguments(parameterTypes, null, null);
			return signatureHelper;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002AF7B File Offset: 0x0002917B
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			return SignatureHelper.GetPropertySigHelper(mod, CallingConventions.Standard, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002AF90 File Offset: 0x00029190
		public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			byte b = 8;
			if ((callingConvention & CallingConventions.HasThis) != (CallingConventions)0)
			{
				b |= 32;
			}
			SignatureHelper signatureHelper = SignatureHelper.Create(mod, b, returnType);
			signatureHelper.AddArgument(returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
			signatureHelper.paramCount = 0;
			signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			return signatureHelper;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002AFD0 File Offset: 0x000291D0
		public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, unmanagedCallingConvention, returnType);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002AFDA File Offset: 0x000291DA
		public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, callingConvention, returnType);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002AFE4 File Offset: 0x000291E4
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
		{
			byte b;
			switch (unmanagedCallConv)
			{
			case CallingConvention.Winapi:
			case CallingConvention.StdCall:
				b = 2;
				break;
			case CallingConvention.Cdecl:
				b = 1;
				break;
			case CallingConvention.ThisCall:
				b = 3;
				break;
			case CallingConvention.FastCall:
				b = 4;
				break;
			default:
				throw new ArgumentOutOfRangeException("unmanagedCallConv");
			}
			SignatureHelper signatureHelper = SignatureHelper.Create(mod, b, returnType);
			signatureHelper.AddArgument(returnType);
			signatureHelper.paramCount = 0;
			return signatureHelper;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002B040 File Offset: 0x00029240
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
		{
			byte b = 0;
			if ((callingConvention & CallingConventions.HasThis) != (CallingConventions)0)
			{
				b |= 32;
			}
			if ((callingConvention & CallingConventions.ExplicitThis) != (CallingConventions)0)
			{
				b |= 64;
			}
			if ((callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				b |= 5;
			}
			SignatureHelper signatureHelper = SignatureHelper.Create(mod, b, returnType);
			signatureHelper.AddArgument(returnType);
			signatureHelper.paramCount = 0;
			return signatureHelper;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002B087 File Offset: 0x00029287
		public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			SignatureHelper signatureHelper = SignatureHelper.Create(mod, 0, returnType);
			signatureHelper.AddArgument(returnType);
			signatureHelper.paramCount = 0;
			signatureHelper.AddArguments(parameterTypes, null, null);
			return signatureHelper;
		}

		// Token: 0x06000BD4 RID: 3028
		public abstract byte[] GetSignature();

		// Token: 0x06000BD5 RID: 3029
		internal abstract ByteBuffer GetSignature(ModuleBuilder module);

		// Token: 0x06000BD6 RID: 3030
		public abstract void AddSentinel();

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002B0A8 File Offset: 0x000292A8
		public void AddArgument(Type clsArgument)
		{
			this.AddArgument(clsArgument, false);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002B0B4 File Offset: 0x000292B4
		public void AddArgument(Type argument, bool pinned)
		{
			this.__AddArgument(argument, pinned, default(CustomModifiers));
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002B0D2 File Offset: 0x000292D2
		public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			this.__AddArgument(argument, false, CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers));
		}

		// Token: 0x06000BDA RID: 3034
		public abstract void __AddArgument(Type argument, bool pinned, CustomModifiers customModifiers);

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002B0E4 File Offset: 0x000292E4
		public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					this.__AddArgument(arguments[i], false, CustomModifiers.FromReqOpt(Util.NullSafeElementAt<Type[]>(requiredCustomModifiers, i), Util.NullSafeElementAt<Type[]>(optionalCustomModifiers, i)));
				}
			}
		}

		// Token: 0x040005EC RID: 1516
		protected readonly byte type;

		// Token: 0x040005ED RID: 1517
		protected ushort paramCount;

		// Token: 0x02000373 RID: 883
		private sealed class Lazy : SignatureHelper
		{
			// Token: 0x0600266E RID: 9838 RVA: 0x000B64E2 File Offset: 0x000B46E2
			internal Lazy(byte type) : base(type)
			{
			}

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x0600266F RID: 9839 RVA: 0x000B64F6 File Offset: 0x000B46F6
			internal override Type ReturnType
			{
				get
				{
					return this.args[0];
				}
			}

			// Token: 0x06002670 RID: 9840 RVA: 0x0000225C File Offset: 0x0000045C
			public override byte[] GetSignature()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002671 RID: 9841 RVA: 0x000B6504 File Offset: 0x000B4704
			internal override ByteBuffer GetSignature(ModuleBuilder module)
			{
				ByteBuffer byteBuffer = new ByteBuffer(16);
				Signature.WriteSignatureHelper(module, byteBuffer, this.type, this.paramCount, this.args);
				return byteBuffer;
			}

			// Token: 0x06002672 RID: 9842 RVA: 0x000B6533 File Offset: 0x000B4733
			public override void AddSentinel()
			{
				this.args.Add(MarkerType.Sentinel);
			}

			// Token: 0x06002673 RID: 9843 RVA: 0x000B6548 File Offset: 0x000B4748
			public override void __AddArgument(Type argument, bool pinned, CustomModifiers customModifiers)
			{
				if (pinned)
				{
					this.args.Add(MarkerType.Pinned);
				}
				foreach (CustomModifiers.Entry entry in customModifiers)
				{
					this.args.Add(entry.IsRequired ? MarkerType.ModReq : MarkerType.ModOpt);
					this.args.Add(entry.Type);
				}
				this.args.Add(argument);
				this.paramCount += 1;
			}

			// Token: 0x04000F34 RID: 3892
			private readonly List<Type> args = new List<Type>();
		}

		// Token: 0x02000374 RID: 884
		private sealed class Eager : SignatureHelper
		{
			// Token: 0x06002674 RID: 9844 RVA: 0x000B65F0 File Offset: 0x000B47F0
			internal Eager(ModuleBuilder module, byte type, Type returnType) : base(type)
			{
				this.module = module;
				this.returnType = returnType;
				this.bb.Write(type);
				if (type != 6)
				{
					this.bb.Write(0);
				}
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002675 RID: 9845 RVA: 0x000B6630 File Offset: 0x000B4830
			internal override Type ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x06002676 RID: 9846 RVA: 0x000B6638 File Offset: 0x000B4838
			public override byte[] GetSignature()
			{
				return this.GetSignature(null).ToArray();
			}

			// Token: 0x06002677 RID: 9847 RVA: 0x000B6648 File Offset: 0x000B4848
			internal override ByteBuffer GetSignature(ModuleBuilder module)
			{
				if (this.type != 6)
				{
					this.bb.Position = 1;
					this.bb.Insert(MetadataWriter.GetCompressedUIntLength((int)this.paramCount) - this.bb.GetCompressedUIntLength());
					this.bb.WriteCompressedUInt((int)this.paramCount);
				}
				return this.bb;
			}

			// Token: 0x06002678 RID: 9848 RVA: 0x000B66A3 File Offset: 0x000B48A3
			public override void AddSentinel()
			{
				this.bb.Write(65);
			}

			// Token: 0x06002679 RID: 9849 RVA: 0x000B66B4 File Offset: 0x000B48B4
			public override void __AddArgument(Type argument, bool pinned, CustomModifiers customModifiers)
			{
				if (pinned)
				{
					this.bb.Write(69);
				}
				foreach (CustomModifiers.Entry entry in customModifiers)
				{
					this.bb.Write(entry.IsRequired ? 31 : 32);
					Signature.WriteTypeSpec(this.module, this.bb, entry.Type);
				}
				Signature.WriteTypeSpec(this.module, this.bb, argument ?? this.module.universe.System_Void);
				this.paramCount += 1;
			}

			// Token: 0x04000F35 RID: 3893
			private readonly ModuleBuilder module;

			// Token: 0x04000F36 RID: 3894
			private readonly ByteBuffer bb = new ByteBuffer(16);

			// Token: 0x04000F37 RID: 3895
			private readonly Type returnType;
		}
	}
}
