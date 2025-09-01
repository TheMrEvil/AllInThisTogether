using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000087 RID: 135
	internal class ReflectionType : ReflectionMember
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000AB52 File Offset: 0x00008D52
		public ReflectionType(Type type)
		{
			Assumes.NotNull<Type>(type);
			this._type = type;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000AB67 File Offset: 0x00008D67
		public override MemberInfo UnderlyingMember
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00005907 File Offset: 0x00003B07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00005907 File Offset: 0x00003B07
		public override bool RequiresInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000AB67 File Offset: 0x00008D67
		public override Type ReturnType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000AB6F File Offset: 0x00008D6F
		public override ReflectionItemType ItemType
		{
			get
			{
				return ReflectionItemType.Type;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AB72 File Offset: 0x00008D72
		public override object GetValue(object instance)
		{
			return instance;
		}

		// Token: 0x04000171 RID: 369
		private Type _type;
	}
}
