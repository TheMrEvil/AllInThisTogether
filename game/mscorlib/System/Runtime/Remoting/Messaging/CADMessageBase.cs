using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200060E RID: 1550
	internal class CADMessageBase
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x000CD484 File Offset: 0x000CB684
		public CADMessageBase(IMethodMessage msg)
		{
			CADMethodRef obj = new CADMethodRef(msg);
			this.serializedMethod = CADSerializer.SerializeObject(obj).GetBuffer();
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000CD4AF File Offset: 0x000CB6AF
		internal MethodBase GetMethod()
		{
			return ((CADMethodRef)CADSerializer.DeserializeObjectSafe(this.serializedMethod)).Resolve();
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000CD4C8 File Offset: 0x000CB6C8
		protected static Type[] GetSignature(MethodBase methodBase, bool load)
		{
			ParameterInfo[] parameters = methodBase.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				if (load)
				{
					array[i] = Type.GetType(parameters[i].ParameterType.AssemblyQualifiedName, true);
				}
				else
				{
					array[i] = parameters[i].ParameterType;
				}
			}
			return array;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000CD51C File Offset: 0x000CB71C
		internal static int MarshalProperties(IDictionary dict, ref ArrayList args)
		{
			int num = 0;
			MessageDictionary messageDictionary = dict as MessageDictionary;
			if (messageDictionary != null)
			{
				if (!messageDictionary.HasUserData())
				{
					return num;
				}
				IDictionary internalDictionary = messageDictionary.InternalDictionary;
				if (internalDictionary == null)
				{
					return num;
				}
				using (IDictionaryEnumerator enumerator = internalDictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (args == null)
						{
							args = new ArrayList();
						}
						args.Add(dictionaryEntry);
						num++;
					}
					return num;
				}
			}
			if (dict != null)
			{
				foreach (object obj2 in dict)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (args == null)
					{
						args = new ArrayList();
					}
					args.Add(dictionaryEntry2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000CD614 File Offset: 0x000CB814
		internal static void UnmarshalProperties(IDictionary dict, int count, ArrayList args)
		{
			for (int i = 0; i < count; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)args[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000CD650 File Offset: 0x000CB850
		private static bool IsPossibleToIgnoreMarshal(object obj)
		{
			Type type = obj.GetType();
			return type.IsPrimitive || type == typeof(void) || (type.IsArray && type.GetElementType().IsPrimitive && ((Array)obj).Rank == 1) || (obj is string || obj is DateTime || obj is TimeSpan);
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000CD6C0 File Offset: 0x000CB8C0
		protected object MarshalArgument(object arg, ref ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			if (CADMessageBase.IsPossibleToIgnoreMarshal(arg))
			{
				return arg;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			if (marshalByRefObject != null && !RemotingServices.IsTransparentProxy(marshalByRefObject))
			{
				return new CADObjRef(RemotingServices.Marshal(marshalByRefObject), Thread.GetDomainID());
			}
			if (args == null)
			{
				args = new ArrayList();
			}
			args.Add(arg);
			return new CADArgHolder(args.Count - 1);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000CD724 File Offset: 0x000CB924
		protected object UnmarshalArgument(object arg, ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			CADArgHolder cadargHolder = arg as CADArgHolder;
			if (cadargHolder != null)
			{
				return args[cadargHolder.index];
			}
			CADObjRef cadobjRef = arg as CADObjRef;
			if (cadobjRef != null)
			{
				return RemotingServices.Unmarshal(cadobjRef.objref.DeserializeInTheCurrentDomain(cadobjRef.SourceDomain, cadobjRef.TypeInfo));
			}
			if (arg is Array)
			{
				Array array = (Array)arg;
				Array array2;
				switch (Type.GetTypeCode(arg.GetType().GetElementType()))
				{
				case TypeCode.Boolean:
					array2 = new bool[array.Length];
					break;
				case TypeCode.Char:
					array2 = new char[array.Length];
					break;
				case TypeCode.SByte:
					array2 = new sbyte[array.Length];
					break;
				case TypeCode.Byte:
					array2 = new byte[array.Length];
					break;
				case TypeCode.Int16:
					array2 = new short[array.Length];
					break;
				case TypeCode.UInt16:
					array2 = new ushort[array.Length];
					break;
				case TypeCode.Int32:
					array2 = new int[array.Length];
					break;
				case TypeCode.UInt32:
					array2 = new uint[array.Length];
					break;
				case TypeCode.Int64:
					array2 = new long[array.Length];
					break;
				case TypeCode.UInt64:
					array2 = new ulong[array.Length];
					break;
				case TypeCode.Single:
					array2 = new float[array.Length];
					break;
				case TypeCode.Double:
					array2 = new double[array.Length];
					break;
				case TypeCode.Decimal:
					array2 = new decimal[array.Length];
					break;
				default:
					throw new NotSupportedException();
				}
				array.CopyTo(array2, 0);
				return array2;
			}
			switch (Type.GetTypeCode(arg.GetType()))
			{
			case TypeCode.Boolean:
				return (bool)arg;
			case TypeCode.Char:
				return (char)arg;
			case TypeCode.SByte:
				return (sbyte)arg;
			case TypeCode.Byte:
				return (byte)arg;
			case TypeCode.Int16:
				return (short)arg;
			case TypeCode.UInt16:
				return (ushort)arg;
			case TypeCode.Int32:
				return (int)arg;
			case TypeCode.UInt32:
				return (uint)arg;
			case TypeCode.Int64:
				return (long)arg;
			case TypeCode.UInt64:
				return (ulong)arg;
			case TypeCode.Single:
				return (float)arg;
			case TypeCode.Double:
				return (double)arg;
			case TypeCode.Decimal:
				return (decimal)arg;
			case TypeCode.DateTime:
				return new DateTime(((DateTime)arg).Ticks);
			case TypeCode.String:
				return string.Copy((string)arg);
			}
			if (arg is TimeSpan)
			{
				return new TimeSpan(((TimeSpan)arg).Ticks);
			}
			if (arg is IntPtr)
			{
				return (IntPtr)arg;
			}
			string str = "Parameter of type ";
			Type type = arg.GetType();
			throw new NotSupportedException(str + ((type != null) ? type.ToString() : null) + " cannot be unmarshalled");
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000CDA28 File Offset: 0x000CBC28
		internal object[] MarshalArguments(object[] arguments, ref ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.MarshalArgument(arguments[i], ref args);
			}
			return array;
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000CDA5C File Offset: 0x000CBC5C
		internal object[] UnmarshalArguments(object[] arguments, ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.UnmarshalArgument(arguments[i], args);
			}
			return array;
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000CDA90 File Offset: 0x000CBC90
		protected void SaveLogicalCallContext(IMethodMessage msg, ref ArrayList serializeList)
		{
			if (msg.LogicalCallContext != null && msg.LogicalCallContext.HasInfo)
			{
				if (serializeList == null)
				{
					serializeList = new ArrayList();
				}
				this._callContext = new CADArgHolder(serializeList.Count);
				serializeList.Add(msg.LogicalCallContext);
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000CDADD File Offset: 0x000CBCDD
		internal LogicalCallContext GetLogicalCallContext(ArrayList args)
		{
			if (this._callContext == null)
			{
				return null;
			}
			return (LogicalCallContext)args[this._callContext.index];
		}

		// Token: 0x04002678 RID: 9848
		protected object[] _args;

		// Token: 0x04002679 RID: 9849
		protected byte[] _serializedArgs;

		// Token: 0x0400267A RID: 9850
		protected int _propertyCount;

		// Token: 0x0400267B RID: 9851
		protected CADArgHolder _callContext;

		// Token: 0x0400267C RID: 9852
		internal byte[] serializedMethod;
	}
}
