using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000076 RID: 118
	internal class ReflectionField : ReflectionWritableMember
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00009E45 File Offset: 0x00008045
		public ReflectionField(FieldInfo field)
		{
			Assumes.NotNull<FieldInfo>(field);
			this._field = field;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00009E5A File Offset: 0x0000805A
		public FieldInfo UndelyingField
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00009E62 File Offset: 0x00008062
		public override MemberInfo UnderlyingMember
		{
			get
			{
				return this.UndelyingField;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00005907 File Offset: 0x00003B07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00009E6A File Offset: 0x0000806A
		public override bool CanWrite
		{
			get
			{
				return !this.UndelyingField.IsInitOnly;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00009E7A File Offset: 0x0000807A
		public override bool RequiresInstance
		{
			get
			{
				return !this.UndelyingField.IsStatic;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00009E8A File Offset: 0x0000808A
		public override Type ReturnType
		{
			get
			{
				return this.UndelyingField.FieldType;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00005907 File Offset: 0x00003B07
		public override ReflectionItemType ItemType
		{
			get
			{
				return ReflectionItemType.Field;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009E97 File Offset: 0x00008097
		public override object GetValue(object instance)
		{
			return this.UndelyingField.SafeGetValue(instance);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009EA5 File Offset: 0x000080A5
		public override void SetValue(object instance, object value)
		{
			this.UndelyingField.SafeSetValue(instance, value);
		}

		// Token: 0x0400014B RID: 331
		private readonly FieldInfo _field;
	}
}
