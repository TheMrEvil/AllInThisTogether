using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Serialization
{
	// Token: 0x0200012D RID: 301
	internal sealed class SurrogateDataContract : DataContract
	{
		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003CB97 File Offset: 0x0003AD97
		[SecuritySafeCritical]
		internal SurrogateDataContract(Type type, ISerializationSurrogate serializationSurrogate) : base(new SurrogateDataContract.SurrogateDataContractCriticalHelper(type, serializationSurrogate))
		{
			this.helper = (base.Helper as SurrogateDataContract.SurrogateDataContractCriticalHelper);
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003CBB7 File Offset: 0x0003ADB7
		internal ISerializationSurrogate SerializationSurrogate
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.SerializationSurrogate;
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003CBC4 File Offset: 0x0003ADC4
		public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
		{
			SerializationInfo serInfo = new SerializationInfo(base.UnderlyingType, XmlObjectSerializer.FormatterConverter, !context.UnsafeTypeForwardingEnabled);
			this.SerializationSurrogateGetObjectData(obj, serInfo, context.GetStreamingContext());
			context.WriteSerializationInfo(xmlWriter, base.UnderlyingType, serInfo);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003CC07 File Offset: 0x0003AE07
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object SerializationSurrogateSetObjectData(object obj, SerializationInfo serInfo, StreamingContext context)
		{
			return this.SerializationSurrogate.SetObjectData(obj, serInfo, context, null);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003CC18 File Offset: 0x0003AE18
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static object GetRealObject(IObjectReference obj, StreamingContext context)
		{
			return obj.GetRealObject(context);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003CC21 File Offset: 0x0003AE21
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object GetUninitializedObject(Type objType)
		{
			return FormatterServices.GetUninitializedObject(objType);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003CC29 File Offset: 0x0003AE29
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SerializationSurrogateGetObjectData(object obj, SerializationInfo serInfo, StreamingContext context)
		{
			this.SerializationSurrogate.GetObjectData(obj, serInfo, context);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
		{
			xmlReader.Read();
			Type underlyingType = base.UnderlyingType;
			object obj = underlyingType.IsArray ? Array.CreateInstance(underlyingType.GetElementType(), 0) : this.GetUninitializedObject(underlyingType);
			context.AddNewObject(obj);
			string objectId = context.GetObjectId();
			SerializationInfo serInfo = context.ReadSerializationInfo(xmlReader, underlyingType);
			object obj2 = this.SerializationSurrogateSetObjectData(obj, serInfo, context.GetStreamingContext());
			if (obj2 == null)
			{
				obj2 = obj;
			}
			if (obj2 is IDeserializationCallback)
			{
				((IDeserializationCallback)obj2).OnDeserialization(null);
			}
			if (obj2 is IObjectReference)
			{
				obj2 = SurrogateDataContract.GetRealObject((IObjectReference)obj2, context.GetStreamingContext());
			}
			context.ReplaceDeserializedObject(objectId, obj, obj2);
			xmlReader.ReadEndElement();
			return obj2;
		}

		// Token: 0x04000680 RID: 1664
		[SecurityCritical]
		private SurrogateDataContract.SurrogateDataContractCriticalHelper helper;

		// Token: 0x0200012E RID: 302
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SurrogateDataContractCriticalHelper : DataContract.DataContractCriticalHelper
		{
			// Token: 0x06000EFC RID: 3836 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
			internal SurrogateDataContractCriticalHelper(Type type, ISerializationSurrogate serializationSurrogate) : base(type)
			{
				this.serializationSurrogate = serializationSurrogate;
				string localName;
				string ns;
				DataContract.GetDefaultStableName(DataContract.GetClrTypeFullName(type), out localName, out ns);
				base.SetDataContractName(DataContract.CreateQualifiedName(localName, ns));
			}

			// Token: 0x17000344 RID: 836
			// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003CD1F File Offset: 0x0003AF1F
			internal ISerializationSurrogate SerializationSurrogate
			{
				get
				{
					return this.serializationSurrogate;
				}
			}

			// Token: 0x04000681 RID: 1665
			private ISerializationSurrogate serializationSurrogate;
		}
	}
}
