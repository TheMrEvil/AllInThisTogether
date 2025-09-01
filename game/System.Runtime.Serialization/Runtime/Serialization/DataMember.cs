using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C9 RID: 201
	internal class DataMember
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x000316C0 File Offset: 0x0002F8C0
		[SecuritySafeCritical]
		internal DataMember()
		{
			this.helper = new DataMember.CriticalHelper();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000316D3 File Offset: 0x0002F8D3
		[SecuritySafeCritical]
		internal DataMember(MemberInfo memberInfo)
		{
			this.helper = new DataMember.CriticalHelper(memberInfo);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000316E7 File Offset: 0x0002F8E7
		[SecuritySafeCritical]
		internal DataMember(string name)
		{
			this.helper = new DataMember.CriticalHelper(name);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000316FB File Offset: 0x0002F8FB
		[SecuritySafeCritical]
		internal DataMember(DataContract memberTypeContract, string name, bool isNullable, bool isRequired, bool emitDefaultValue, int order)
		{
			this.helper = new DataMember.CriticalHelper(memberTypeContract, name, isNullable, isRequired, emitDefaultValue, order);
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00031717 File Offset: 0x0002F917
		internal MemberInfo MemberInfo
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.MemberInfo;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00031724 File Offset: 0x0002F924
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00031731 File Offset: 0x0002F931
		internal string Name
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Name;
			}
			[SecurityCritical]
			set
			{
				this.helper.Name = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0003173F File Offset: 0x0002F93F
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0003174C File Offset: 0x0002F94C
		internal int Order
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.Order;
			}
			[SecurityCritical]
			set
			{
				this.helper.Order = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0003175A File Offset: 0x0002F95A
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00031767 File Offset: 0x0002F967
		internal bool IsRequired
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsRequired;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsRequired = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00031775 File Offset: 0x0002F975
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00031782 File Offset: 0x0002F982
		internal bool EmitDefaultValue
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.EmitDefaultValue;
			}
			[SecurityCritical]
			set
			{
				this.helper.EmitDefaultValue = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00031790 File Offset: 0x0002F990
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0003179D File Offset: 0x0002F99D
		internal bool IsNullable
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsNullable;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsNullable = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x000317AB File Offset: 0x0002F9AB
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x000317B8 File Offset: 0x0002F9B8
		internal bool IsGetOnlyCollection
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.IsGetOnlyCollection;
			}
			[SecurityCritical]
			set
			{
				this.helper.IsGetOnlyCollection = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000317C6 File Offset: 0x0002F9C6
		internal Type MemberType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.MemberType;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000317D3 File Offset: 0x0002F9D3
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x000317E0 File Offset: 0x0002F9E0
		internal DataContract MemberTypeContract
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.MemberTypeContract;
			}
			[SecurityCritical]
			set
			{
				this.helper.MemberTypeContract = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000317EE File Offset: 0x0002F9EE
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x000317FB File Offset: 0x0002F9FB
		internal bool HasConflictingNameAndType
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.HasConflictingNameAndType;
			}
			[SecurityCritical]
			set
			{
				this.helper.HasConflictingNameAndType = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00031809 File Offset: 0x0002FA09
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x00031816 File Offset: 0x0002FA16
		internal DataMember ConflictingMember
		{
			[SecuritySafeCritical]
			get
			{
				return this.helper.ConflictingMember;
			}
			[SecurityCritical]
			set
			{
				this.helper.ConflictingMember = value;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00031824 File Offset: 0x0002FA24
		internal DataMember BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
		{
			DataContract dataContract = this.MemberTypeContract.BindGenericParameters(paramContracts, boundContracts);
			return new DataMember(dataContract, this.Name, !dataContract.IsValueType, this.IsRequired, this.EmitDefaultValue, this.Order);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00031868 File Offset: 0x0002FA68
		internal bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
		{
			if (this == other)
			{
				return true;
			}
			DataMember dataMember = other as DataMember;
			if (dataMember != null)
			{
				bool flag = this.MemberTypeContract != null && !this.MemberTypeContract.IsValueType;
				bool flag2 = dataMember.MemberTypeContract != null && !dataMember.MemberTypeContract.IsValueType;
				return this.Name == dataMember.Name && (this.IsNullable || flag) == (dataMember.IsNullable || flag2) && this.IsRequired == dataMember.IsRequired && this.EmitDefaultValue == dataMember.EmitDefaultValue && this.MemberTypeContract.Equals(dataMember.MemberTypeContract, checkedContracts);
			}
			return false;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002E7D2 File Offset: 0x0002C9D2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040004B5 RID: 1205
		[SecurityCritical]
		private DataMember.CriticalHelper helper;

		// Token: 0x020000CA RID: 202
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class CriticalHelper
		{
			// Token: 0x06000BD2 RID: 3026 RVA: 0x00031913 File Offset: 0x0002FB13
			internal CriticalHelper()
			{
				this.emitDefaultValue = true;
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x00031922 File Offset: 0x0002FB22
			internal CriticalHelper(MemberInfo memberInfo)
			{
				this.emitDefaultValue = true;
				this.memberInfo = memberInfo;
			}

			// Token: 0x06000BD4 RID: 3028 RVA: 0x00031938 File Offset: 0x0002FB38
			internal CriticalHelper(string name)
			{
				this.Name = name;
			}

			// Token: 0x06000BD5 RID: 3029 RVA: 0x00031947 File Offset: 0x0002FB47
			internal CriticalHelper(DataContract memberTypeContract, string name, bool isNullable, bool isRequired, bool emitDefaultValue, int order)
			{
				this.MemberTypeContract = memberTypeContract;
				this.Name = name;
				this.IsNullable = isNullable;
				this.IsRequired = isRequired;
				this.EmitDefaultValue = emitDefaultValue;
				this.Order = order;
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0003197C File Offset: 0x0002FB7C
			internal MemberInfo MemberInfo
			{
				get
				{
					return this.memberInfo;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00031984 File Offset: 0x0002FB84
			// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0003198C File Offset: 0x0002FB8C
			internal string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00031995 File Offset: 0x0002FB95
			// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0003199D File Offset: 0x0002FB9D
			internal int Order
			{
				get
				{
					return this.order;
				}
				set
				{
					this.order = value;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000319A6 File Offset: 0x0002FBA6
			// (set) Token: 0x06000BDC RID: 3036 RVA: 0x000319AE File Offset: 0x0002FBAE
			internal bool IsRequired
			{
				get
				{
					return this.isRequired;
				}
				set
				{
					this.isRequired = value;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000319B7 File Offset: 0x0002FBB7
			// (set) Token: 0x06000BDE RID: 3038 RVA: 0x000319BF File Offset: 0x0002FBBF
			internal bool EmitDefaultValue
			{
				get
				{
					return this.emitDefaultValue;
				}
				set
				{
					this.emitDefaultValue = value;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000319C8 File Offset: 0x0002FBC8
			// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000319D0 File Offset: 0x0002FBD0
			internal bool IsNullable
			{
				get
				{
					return this.isNullable;
				}
				set
				{
					this.isNullable = value;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000319D9 File Offset: 0x0002FBD9
			// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x000319E1 File Offset: 0x0002FBE1
			internal bool IsGetOnlyCollection
			{
				get
				{
					return this.isGetOnlyCollection;
				}
				set
				{
					this.isGetOnlyCollection = value;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000319EC File Offset: 0x0002FBEC
			internal Type MemberType
			{
				get
				{
					FieldInfo fieldInfo = this.MemberInfo as FieldInfo;
					if (fieldInfo != null)
					{
						return fieldInfo.FieldType;
					}
					return ((PropertyInfo)this.MemberInfo).PropertyType;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00031A28 File Offset: 0x0002FC28
			// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00031A99 File Offset: 0x0002FC99
			internal DataContract MemberTypeContract
			{
				get
				{
					if (this.memberTypeContract == null && this.MemberInfo != null)
					{
						if (this.IsGetOnlyCollection)
						{
							this.memberTypeContract = DataContract.GetGetOnlyCollectionDataContract(DataContract.GetId(this.MemberType.TypeHandle), this.MemberType.TypeHandle, this.MemberType, SerializationMode.SharedContract);
						}
						else
						{
							this.memberTypeContract = DataContract.GetDataContract(this.MemberType);
						}
					}
					return this.memberTypeContract;
				}
				set
				{
					this.memberTypeContract = value;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00031AA2 File Offset: 0x0002FCA2
			// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00031AAA File Offset: 0x0002FCAA
			internal bool HasConflictingNameAndType
			{
				get
				{
					return this.hasConflictingNameAndType;
				}
				set
				{
					this.hasConflictingNameAndType = value;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00031AB3 File Offset: 0x0002FCB3
			// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00031ABB File Offset: 0x0002FCBB
			internal DataMember ConflictingMember
			{
				get
				{
					return this.conflictingMember;
				}
				set
				{
					this.conflictingMember = value;
				}
			}

			// Token: 0x040004B6 RID: 1206
			private DataContract memberTypeContract;

			// Token: 0x040004B7 RID: 1207
			private string name;

			// Token: 0x040004B8 RID: 1208
			private int order;

			// Token: 0x040004B9 RID: 1209
			private bool isRequired;

			// Token: 0x040004BA RID: 1210
			private bool emitDefaultValue;

			// Token: 0x040004BB RID: 1211
			private bool isNullable;

			// Token: 0x040004BC RID: 1212
			private bool isGetOnlyCollection;

			// Token: 0x040004BD RID: 1213
			private MemberInfo memberInfo;

			// Token: 0x040004BE RID: 1214
			private bool hasConflictingNameAndType;

			// Token: 0x040004BF RID: 1215
			private DataMember conflictingMember;
		}
	}
}
