using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000084 RID: 132
	internal class ReflectionParameter : ReflectionItem
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000A906 File Offset: 0x00008B06
		public ReflectionParameter(ParameterInfo parameter)
		{
			Assumes.NotNull<ParameterInfo>(parameter);
			this._parameter = parameter;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A91B File Offset: 0x00008B1B
		public ParameterInfo UnderlyingParameter
		{
			get
			{
				return this._parameter;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A923 File Offset: 0x00008B23
		public override string Name
		{
			get
			{
				return this.UnderlyingParameter.Name;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A930 File Offset: 0x00008B30
		public override string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Parameter=\"{1}\")", this.UnderlyingParameter.Member.GetDisplayName(), this.UnderlyingParameter.Name);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A95C File Offset: 0x00008B5C
		public override Type ReturnType
		{
			get
			{
				return this.UnderlyingParameter.ParameterType;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A969 File Offset: 0x00008B69
		public override ReflectionItemType ItemType
		{
			get
			{
				return ReflectionItemType.Parameter;
			}
		}

		// Token: 0x0400016D RID: 365
		private readonly ParameterInfo _parameter;
	}
}
