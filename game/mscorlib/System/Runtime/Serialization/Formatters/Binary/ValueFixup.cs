using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006BD RID: 1725
	internal sealed class ValueFixup
	{
		// Token: 0x06003FC4 RID: 16324 RVA: 0x000DF8ED File Offset: 0x000DDAED
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000DF90A File Offset: 0x000DDB0A
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000DF930 File Offset: 0x000DDB30
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Header reflection error: number of value members: {0}.", new object[]
						{
							member.Length
						}));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x040029B3 RID: 10675
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x040029B4 RID: 10676
		internal Array arrayObj;

		// Token: 0x040029B5 RID: 10677
		internal int[] indexMap;

		// Token: 0x040029B6 RID: 10678
		internal object header;

		// Token: 0x040029B7 RID: 10679
		internal object memberObject;

		// Token: 0x040029B8 RID: 10680
		internal static volatile MemberInfo valueInfo;

		// Token: 0x040029B9 RID: 10681
		internal ReadObjectInfo objectInfo;

		// Token: 0x040029BA RID: 10682
		internal string memberName;
	}
}
