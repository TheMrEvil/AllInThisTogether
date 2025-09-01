using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200029C RID: 668
	internal class FieldModel
	{
		// Token: 0x06001917 RID: 6423 RVA: 0x0009041C File Offset: 0x0008E61C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist) : this(name, fieldType, fieldTypeDesc, checkSpecified, checkShouldPersist, false)
		{
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0009042C File Offset: 0x0008E62C
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist, bool readOnly)
		{
			this.fieldTypeDesc = fieldTypeDesc;
			this.name = name;
			this.fieldType = fieldType;
			this.checkSpecified = (checkSpecified ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.None);
			this.checkShouldPersist = checkShouldPersist;
			this.readOnly = readOnly;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00090468 File Offset: 0x0008E668
		internal FieldModel(MemberInfo memberInfo, Type fieldType, TypeDesc fieldTypeDesc)
		{
			this.name = memberInfo.Name;
			this.fieldType = fieldType;
			this.fieldTypeDesc = fieldTypeDesc;
			this.memberInfo = memberInfo;
			this.checkShouldPersistMethodInfo = memberInfo.DeclaringType.GetMethod("ShouldSerialize" + memberInfo.Name, new Type[0]);
			this.checkShouldPersist = (this.checkShouldPersistMethodInfo != null);
			FieldInfo field = memberInfo.DeclaringType.GetField(memberInfo.Name + "Specified");
			if (field != null)
			{
				if (field.FieldType != typeof(bool))
				{
					throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
					{
						field.Name,
						field.FieldType.FullName,
						typeof(bool).FullName
					}));
				}
				this.checkSpecified = (field.IsInitOnly ? SpecifiedAccessor.ReadOnly : SpecifiedAccessor.ReadWrite);
				this.checkSpecifiedMemberInfo = field;
			}
			else
			{
				PropertyInfo property = memberInfo.DeclaringType.GetProperty(memberInfo.Name + "Specified");
				if (property != null)
				{
					if (StructModel.CheckPropertyRead(property))
					{
						this.checkSpecified = (property.CanWrite ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.ReadOnly);
						this.checkSpecifiedMemberInfo = property;
					}
					if (this.checkSpecified != SpecifiedAccessor.None && property.PropertyType != typeof(bool))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.", new object[]
						{
							property.Name,
							property.PropertyType.FullName,
							typeof(bool).FullName
						}));
					}
				}
			}
			if (memberInfo is PropertyInfo)
			{
				this.readOnly = !((PropertyInfo)memberInfo).CanWrite;
				this.isProperty = true;
				return;
			}
			if (memberInfo is FieldInfo)
			{
				this.readOnly = ((FieldInfo)memberInfo).IsInitOnly;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0009064F File Offset: 0x0008E84F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00090657 File Offset: 0x0008E857
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0009065F File Offset: 0x0008E85F
		internal TypeDesc FieldTypeDesc
		{
			get
			{
				return this.fieldTypeDesc;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00090667 File Offset: 0x0008E867
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0009066F File Offset: 0x0008E86F
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00090677 File Offset: 0x0008E877
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0009067F File Offset: 0x0008E87F
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00090687 File Offset: 0x0008E887
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0009068F File Offset: 0x0008E88F
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00090697 File Offset: 0x0008E897
		internal bool IsProperty
		{
			get
			{
				return this.isProperty;
			}
		}

		// Token: 0x0400190D RID: 6413
		private SpecifiedAccessor checkSpecified;

		// Token: 0x0400190E RID: 6414
		private MemberInfo memberInfo;

		// Token: 0x0400190F RID: 6415
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x04001910 RID: 6416
		private MethodInfo checkShouldPersistMethodInfo;

		// Token: 0x04001911 RID: 6417
		private bool checkShouldPersist;

		// Token: 0x04001912 RID: 6418
		private bool readOnly;

		// Token: 0x04001913 RID: 6419
		private bool isProperty;

		// Token: 0x04001914 RID: 6420
		private Type fieldType;

		// Token: 0x04001915 RID: 6421
		private string name;

		// Token: 0x04001916 RID: 6422
		private TypeDesc fieldTypeDesc;
	}
}
