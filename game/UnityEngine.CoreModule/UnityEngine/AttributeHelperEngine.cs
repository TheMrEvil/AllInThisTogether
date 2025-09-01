using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001ED RID: 493
	internal class AttributeHelperEngine
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x00023BA0 File Offset: 0x00021DA0
		[RequiredByNativeCode]
		private static Type GetParentTypeDisallowingMultipleInclusion(Type type)
		{
			Type result = null;
			while (type != null && type != typeof(MonoBehaviour))
			{
				bool flag = Attribute.IsDefined(type, typeof(DisallowMultipleComponent));
				if (flag)
				{
					result = type;
				}
				type = type.BaseType;
			}
			return result;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00023BF4 File Offset: 0x00021DF4
		[RequiredByNativeCode]
		private static Type[] GetRequiredComponents(Type klass)
		{
			List<Type> list = null;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				RequireComponent[] array = (RequireComponent[])klass.GetCustomAttributes(typeof(RequireComponent), false);
				Type baseType = klass.BaseType;
				foreach (RequireComponent requireComponent in array)
				{
					bool flag = list == null && array.Length == 1 && baseType == typeof(MonoBehaviour);
					if (flag)
					{
						return new Type[]
						{
							requireComponent.m_Type0,
							requireComponent.m_Type1,
							requireComponent.m_Type2
						};
					}
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<Type>();
					}
					bool flag3 = requireComponent.m_Type0 != null;
					if (flag3)
					{
						list.Add(requireComponent.m_Type0);
					}
					bool flag4 = requireComponent.m_Type1 != null;
					if (flag4)
					{
						list.Add(requireComponent.m_Type1);
					}
					bool flag5 = requireComponent.m_Type2 != null;
					if (flag5)
					{
						list.Add(requireComponent.m_Type2);
					}
				}
				klass = baseType;
			}
			bool flag6 = list == null;
			if (flag6)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00023D44 File Offset: 0x00021F44
		private static int GetExecuteMode(Type klass)
		{
			object[] customAttributes = klass.GetCustomAttributes(typeof(ExecuteAlways), false);
			bool flag = customAttributes.Length != 0;
			int result;
			if (flag)
			{
				result = 2;
			}
			else
			{
				object[] customAttributes2 = klass.GetCustomAttributes(typeof(ExecuteInEditMode), false);
				bool flag2 = customAttributes2.Length != 0;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00023D98 File Offset: 0x00021F98
		[RequiredByNativeCode]
		private static int CheckIsEditorScript(Type klass)
		{
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				int executeMode = AttributeHelperEngine.GetExecuteMode(klass);
				bool flag = executeMode > 0;
				if (flag)
				{
					return executeMode;
				}
				klass = klass.BaseType;
			}
			return 0;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00023DE4 File Offset: 0x00021FE4
		[RequiredByNativeCode]
		private static int GetDefaultExecutionOrderFor(Type klass)
		{
			DefaultExecutionOrder customAttributeOfType = AttributeHelperEngine.GetCustomAttributeOfType<DefaultExecutionOrder>(klass);
			bool flag = customAttributeOfType == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = customAttributeOfType.order;
			}
			return result;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00023E10 File Offset: 0x00022010
		private static T GetCustomAttributeOfType<T>(Type klass) where T : Attribute
		{
			Type typeFromHandle = typeof(T);
			object[] customAttributes = klass.GetCustomAttributes(typeFromHandle, true);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			T result;
			if (flag)
			{
				result = (T)((object)customAttributes[0]);
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00002072 File Offset: 0x00000272
		public AttributeHelperEngine()
		{
		}
	}
}
