using System;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x02000003 RID: 3
	public static class AttributeTypeExtensions
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000208A File Offset: 0x0000028A
		public static AttributeTypeCache Cache
		{
			[CompilerGenerated]
			get
			{
				return AttributeTypeExtensions.<Cache>k__BackingField;
			}
		} = new AttributeTypeCache();

		// Token: 0x06000006 RID: 6 RVA: 0x00002091 File Offset: 0x00000291
		public static T GetAttribute<T>(this Type type, AttributeTargets targets = AttributeTargets.All, bool inherit = true) where T : Attribute
		{
			return AttributeTypeExtensions.Cache.GetSingleAttribute<T>(type, targets, inherit);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A0 File Offset: 0x000002A0
		public static T[] GetAttributes<T>(this Type type, AttributeTargets targets = AttributeTargets.All, bool inherit = true) where T : Attribute
		{
			return AttributeTypeExtensions.Cache.GetMultiAttributes<T>(type, targets, inherit);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AF File Offset: 0x000002AF
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeTypeExtensions()
		{
		}

		// Token: 0x04000002 RID: 2
		[CompilerGenerated]
		private static readonly AttributeTypeCache <Cache>k__BackingField;
	}
}
