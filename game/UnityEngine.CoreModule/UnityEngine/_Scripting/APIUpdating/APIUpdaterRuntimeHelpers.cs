using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine._Scripting.APIUpdating
{
	// Token: 0x0200026D RID: 621
	internal class APIUpdaterRuntimeHelpers
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x0002B670 File Offset: 0x00029870
		[RequiredByNativeCode]
		internal static bool GetMovedFromAttributeDataForType(Type sourceType, out string assembly, out string nsp, out string klass)
		{
			klass = null;
			nsp = null;
			assembly = null;
			object[] customAttributes = sourceType.GetCustomAttributes(typeof(MovedFromAttribute), false);
			bool flag = customAttributes.Length != 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				MovedFromAttribute movedFromAttribute = (MovedFromAttribute)customAttributes[0];
				klass = movedFromAttribute.data.className;
				nsp = movedFromAttribute.data.nameSpace;
				assembly = movedFromAttribute.data.assembly;
				result = true;
			}
			return result;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0002B6E0 File Offset: 0x000298E0
		[RequiredByNativeCode]
		internal static bool GetObsoleteTypeRedirection(Type sourceType, out string assemblyName, out string nsp, out string className)
		{
			object[] customAttributes = sourceType.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			assemblyName = null;
			nsp = null;
			className = null;
			bool flag = customAttributes.Length != 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ObsoleteAttribute obsoleteAttribute = (ObsoleteAttribute)customAttributes[0];
				string message = obsoleteAttribute.Message;
				bool flag2 = string.IsNullOrEmpty(message);
				if (flag2)
				{
					result = false;
				}
				else
				{
					string text = "(UnityUpgradable) -> ";
					int num = message.IndexOf(text);
					bool flag3 = num >= 0;
					if (flag3)
					{
						string text2 = message.Substring(num + text.Length).Trim();
						bool flag4 = text2.Length == 0;
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = text2[0] == '[';
							int num2;
							if (flag5)
							{
								num2 = text2.IndexOf(']');
								bool flag6 = num2 == -1;
								if (flag6)
								{
									return false;
								}
								assemblyName = text2.Substring(1, num2 - 1);
								text2 = text2.Substring(num2 + 1).Trim();
							}
							else
							{
								assemblyName = sourceType.Assembly.GetName().Name;
							}
							num2 = text2.LastIndexOf('.');
							bool flag7 = num2 > -1;
							if (flag7)
							{
								className = text2.Substring(num2 + 1);
								text2 = text2.Substring(0, num2);
							}
							else
							{
								className = text2;
								text2 = "";
							}
							bool flag8 = text2.Length > 0;
							if (flag8)
							{
								nsp = text2;
							}
							else
							{
								nsp = sourceType.Namespace;
							}
							result = true;
						}
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00002072 File Offset: 0x00000272
		public APIUpdaterRuntimeHelpers()
		{
		}
	}
}
