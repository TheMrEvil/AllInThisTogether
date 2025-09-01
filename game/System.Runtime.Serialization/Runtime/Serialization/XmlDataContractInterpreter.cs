using System;
using System.Reflection;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x0200015E RID: 350
	internal class XmlDataContractInterpreter
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x0004718C File Offset: 0x0004538C
		public XmlDataContractInterpreter(XmlDataContract contract)
		{
			this.contract = contract;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004719C File Offset: 0x0004539C
		public IXmlSerializable CreateXmlSerializable()
		{
			Type underlyingType = this.contract.UnderlyingType;
			object obj;
			if (underlyingType.IsValueType)
			{
				obj = FormatterServices.GetUninitializedObject(underlyingType);
			}
			else
			{
				obj = this.GetConstructor().Invoke(new object[0]);
			}
			return (IXmlSerializable)obj;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000471E0 File Offset: 0x000453E0
		private ConstructorInfo GetConstructor()
		{
			Type underlyingType = this.contract.UnderlyingType;
			if (underlyingType.IsValueType)
			{
				return null;
			}
			ConstructorInfo constructor = underlyingType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
			if (constructor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.GetString("IXmlSerializable Type '{0}' must have default constructor.", new object[]
				{
					DataContract.GetClrTypeFullName(underlyingType)
				})));
			}
			return constructor;
		}

		// Token: 0x04000955 RID: 2389
		private XmlDataContract contract;
	}
}
