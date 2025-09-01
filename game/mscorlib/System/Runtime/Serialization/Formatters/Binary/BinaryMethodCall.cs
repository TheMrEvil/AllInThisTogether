using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000699 RID: 1689
	internal sealed class BinaryMethodCall
	{
		// Token: 0x06003E2D RID: 15917 RVA: 0x000D6404 File Offset: 0x000D4604
		internal object[] WriteArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			this.uri = uri;
			this.methodName = methodName;
			this.typeName = typeName;
			this.instArgs = instArgs;
			this.args = args;
			this.methodSignature = methodSignature;
			this.callContext = callContext;
			this.properties = properties;
			int num = 0;
			if (args == null || args.Length == 0)
			{
				this.messageEnum = MessageEnum.NoArgs;
			}
			else
			{
				this.argTypes = new Type[args.Length];
				this.bArgsPrimitive = true;
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] != null)
					{
						this.argTypes[i] = args[i].GetType();
						if ((Converter.ToCode(this.argTypes[i]) <= InternalPrimitiveTypeE.Invalid && this.argTypes[i] != Converter.typeofString) || args[i] is ISerializable)
						{
							this.bArgsPrimitive = false;
							break;
						}
					}
				}
				if (this.bArgsPrimitive)
				{
					this.messageEnum = MessageEnum.ArgsInline;
				}
				else
				{
					num++;
					this.messageEnum = MessageEnum.ArgsInArray;
				}
			}
			if (instArgs != null)
			{
				num++;
				this.messageEnum |= MessageEnum.GenericMethod;
			}
			if (methodSignature != null)
			{
				num++;
				this.messageEnum |= MessageEnum.MethodSignatureInArray;
			}
			if (callContext == null)
			{
				this.messageEnum |= MessageEnum.NoContext;
			}
			else if (callContext is string)
			{
				this.messageEnum |= MessageEnum.ContextInline;
			}
			else
			{
				num++;
				this.messageEnum |= MessageEnum.ContextInArray;
			}
			if (properties != null)
			{
				num++;
				this.messageEnum |= MessageEnum.PropertyInArray;
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray) && num == 1)
			{
				this.messageEnum ^= MessageEnum.ArgsInArray;
				this.messageEnum |= MessageEnum.ArgsIsArray;
				return args;
			}
			if (num > 0)
			{
				int num2 = 0;
				this.callA = new object[num];
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					this.callA[num2++] = args;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					this.callA[num2++] = instArgs;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					this.callA[num2++] = methodSignature;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					this.callA[num2++] = callContext;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					this.callA[num2] = properties;
				}
				return this.callA;
			}
			return null;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000D6668 File Offset: 0x000D4868
		internal void Write(__BinaryWriter sout)
		{
			sout.WriteByte(21);
			sout.WriteInt32((int)this.messageEnum);
			IOUtil.WriteStringWithCode(this.methodName, sout);
			IOUtil.WriteStringWithCode(this.typeName, sout);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				IOUtil.WriteStringWithCode((string)this.callContext, sout);
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				sout.WriteInt32(this.args.Length);
				for (int i = 0; i < this.args.Length; i++)
				{
					IOUtil.WriteWithCode(this.argTypes[i], this.args[i], sout);
				}
			}
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000D6708 File Offset: 0x000D4908
		[SecurityCritical]
		internal void Read(__BinaryParser input)
		{
			this.messageEnum = (MessageEnum)input.ReadInt32();
			this.methodName = (string)IOUtil.ReadWithCode(input);
			this.typeName = (string)IOUtil.ReadWithCode(input);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				this.scallContext = (string)IOUtil.ReadWithCode(input);
				this.callContext = new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = this.scallContext
					}
				};
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				this.args = IOUtil.ReadArgs(input);
			}
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000D679C File Offset: 0x000D499C
		[SecurityCritical]
		internal IMethodCallMessage ReadArray(object[] callA, object handlerObject)
		{
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
			{
				this.args = callA;
			}
			else
			{
				int num = 0;
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.args = (object[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.instArgs = (Type[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.methodSignature = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.callContext = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.properties = callA[num++];
				}
			}
			return new MethodCall(handlerObject, new BinaryMethodCallMessage(this.uri, this.methodName, this.typeName, this.instArgs, this.args, this.methodSignature, (LogicalCallContext)this.callContext, (object[])this.properties));
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void Dump()
		{
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000D6918 File Offset: 0x000D4B18
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
				{
					string text = this.callContext as string;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
				{
					for (int i = 0; i < this.args.Length; i++)
					{
					}
				}
			}
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000D696D File Offset: 0x000D4B6D
		public BinaryMethodCall()
		{
		}

		// Token: 0x04002843 RID: 10307
		private string uri;

		// Token: 0x04002844 RID: 10308
		private string methodName;

		// Token: 0x04002845 RID: 10309
		private string typeName;

		// Token: 0x04002846 RID: 10310
		private Type[] instArgs;

		// Token: 0x04002847 RID: 10311
		private object[] args;

		// Token: 0x04002848 RID: 10312
		private object methodSignature;

		// Token: 0x04002849 RID: 10313
		private object callContext;

		// Token: 0x0400284A RID: 10314
		private string scallContext;

		// Token: 0x0400284B RID: 10315
		private object properties;

		// Token: 0x0400284C RID: 10316
		private Type[] argTypes;

		// Token: 0x0400284D RID: 10317
		private bool bArgsPrimitive = true;

		// Token: 0x0400284E RID: 10318
		private MessageEnum messageEnum;

		// Token: 0x0400284F RID: 10319
		private object[] callA;
	}
}
