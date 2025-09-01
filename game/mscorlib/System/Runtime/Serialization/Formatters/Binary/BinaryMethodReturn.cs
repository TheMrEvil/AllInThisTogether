using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200069A RID: 1690
	internal sealed class BinaryMethodReturn : IStreamable
	{
		// Token: 0x06003E34 RID: 15924 RVA: 0x000D697C File Offset: 0x000D4B7C
		[SecuritySafeCritical]
		static BinaryMethodReturn()
		{
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000D698D File Offset: 0x000D4B8D
		internal BinaryMethodReturn()
		{
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x000D699C File Offset: 0x000D4B9C
		internal object[] WriteArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			this.returnValue = returnValue;
			this.args = args;
			this.exception = exception;
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
						if (Converter.ToCode(this.argTypes[i]) <= InternalPrimitiveTypeE.Invalid && this.argTypes[i] != Converter.typeofString)
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
			if (returnValue == null)
			{
				this.messageEnum |= MessageEnum.NoReturnValue;
			}
			else if (returnValue.GetType() == typeof(void))
			{
				this.messageEnum |= MessageEnum.ReturnValueVoid;
			}
			else
			{
				this.returnType = returnValue.GetType();
				if (Converter.ToCode(this.returnType) > InternalPrimitiveTypeE.Invalid || this.returnType == Converter.typeofString)
				{
					this.messageEnum |= MessageEnum.ReturnValueInline;
				}
				else
				{
					num++;
					this.messageEnum |= MessageEnum.ReturnValueInArray;
				}
			}
			if (exception != null)
			{
				num++;
				this.messageEnum |= MessageEnum.ExceptionInArray;
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
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
				{
					this.callA[num2++] = returnValue;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
				{
					this.callA[num2++] = exception;
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

		// Token: 0x06003E37 RID: 15927 RVA: 0x000D6C48 File Offset: 0x000D4E48
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(22);
			sout.WriteInt32((int)this.messageEnum);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
			{
				IOUtil.WriteWithCode(this.returnType, this.returnValue, sout);
			}
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

		// Token: 0x06003E38 RID: 15928 RVA: 0x000D6CF4 File Offset: 0x000D4EF4
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.messageEnum = (MessageEnum)input.ReadInt32();
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.NoReturnValue))
			{
				this.returnValue = null;
			}
			else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueVoid))
			{
				this.returnValue = BinaryMethodReturn.instanceOfVoid;
			}
			else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
			{
				this.returnValue = IOUtil.ReadWithCode(input);
			}
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

		// Token: 0x06003E39 RID: 15929 RVA: 0x000D6DC0 File Offset: 0x000D4FC0
		[SecurityCritical]
		internal IMethodReturnMessage ReadArray(object[] returnA, IMethodCallMessage methodCallMessage, object handlerObject)
		{
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
			{
				this.args = returnA;
			}
			else
			{
				int num = 0;
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.args = (object[])returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.returnValue = returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.exception = (Exception)returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.callContext = returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.properties = returnA[num++];
				}
			}
			return new MethodResponse(methodCallMessage, handlerObject, new BinaryMethodReturnMessage(this.returnValue, this.args, this.exception, (LogicalCallContext)this.callContext, (object[])this.properties));
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000D6F2C File Offset: 0x000D512C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline);
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

		// Token: 0x04002850 RID: 10320
		private object returnValue;

		// Token: 0x04002851 RID: 10321
		private object[] args;

		// Token: 0x04002852 RID: 10322
		private Exception exception;

		// Token: 0x04002853 RID: 10323
		private object callContext;

		// Token: 0x04002854 RID: 10324
		private string scallContext;

		// Token: 0x04002855 RID: 10325
		private object properties;

		// Token: 0x04002856 RID: 10326
		private Type[] argTypes;

		// Token: 0x04002857 RID: 10327
		private bool bArgsPrimitive = true;

		// Token: 0x04002858 RID: 10328
		private MessageEnum messageEnum;

		// Token: 0x04002859 RID: 10329
		private object[] callA;

		// Token: 0x0400285A RID: 10330
		private Type returnType;

		// Token: 0x0400285B RID: 10331
		private static object instanceOfVoid = FormatterServices.GetUninitializedObject(Converter.typeofSystemVoid);
	}
}
