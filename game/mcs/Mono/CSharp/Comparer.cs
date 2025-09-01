using System;
using System.Collections;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000265 RID: 613
	public class Comparer : IComparer
	{
		// Token: 0x06001E3D RID: 7741 RVA: 0x00095492 File Offset: 0x00093692
		private Comparer(Comparer.ComparerFunc f)
		{
			this.cmp = f;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000954A1 File Offset: 0x000936A1
		public int Compare(object a, object b)
		{
			return this.cmp(a, b);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000954B0 File Offset: 0x000936B0
		private static int CompareType(object a, object b)
		{
			MemberInfo memberInfo = (Type)a;
			Type type = (Type)b;
			return string.Compare(memberInfo.Name, type.Name);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000954DA File Offset: 0x000936DA
		private static int CompareMemberInfo(object a, object b)
		{
			return string.Compare(((MemberInfo)a).Name, ((MemberInfo)b).Name);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000954F7 File Offset: 0x000936F7
		public static MemberInfo[] Sort(MemberInfo[] inf)
		{
			Array.Sort(inf, Comparer.MemberInfoComparer);
			return inf;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00095508 File Offset: 0x00093708
		private static int CompareMethodBase(object a, object b)
		{
			MethodBase methodBase = (MethodBase)a;
			MethodBase methodBase2 = (MethodBase)b;
			if (methodBase.IsStatic == methodBase2.IsStatic)
			{
				int num = Comparer.CompareMemberInfo(a, b);
				if (num != 0)
				{
					return num;
				}
				ParameterInfo[] parameters = methodBase.GetParameters();
				ParameterInfo[] parameters2 = methodBase2.GetParameters();
				int num2 = Math.Min(parameters.Length, parameters2.Length);
				for (int i = 0; i < num2; i++)
				{
					if ((num = Comparer.CompareType(parameters[i].ParameterType, parameters2[i].ParameterType)) != 0)
					{
						return num;
					}
				}
				return parameters.Length.CompareTo(parameters2.Length);
			}
			else
			{
				if (methodBase.IsStatic)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000955A6 File Offset: 0x000937A6
		public static MethodBase[] Sort(MethodBase[] inf)
		{
			Array.Sort(inf, Comparer.MethodBaseComparer);
			return inf;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000955B4 File Offset: 0x000937B4
		private static int ComparePropertyInfo(object a, object b)
		{
			PropertyInfo propertyInfo = (PropertyInfo)a;
			PropertyInfo propertyInfo2 = (PropertyInfo)b;
			bool isStatic = (propertyInfo.CanRead ? propertyInfo.GetGetMethod(true) : propertyInfo.GetSetMethod(true)).IsStatic;
			bool isStatic2 = (propertyInfo2.CanRead ? propertyInfo2.GetGetMethod(true) : propertyInfo2.GetSetMethod(true)).IsStatic;
			if (isStatic == isStatic2)
			{
				return Comparer.CompareMemberInfo(a, b);
			}
			if (isStatic)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0009561D File Offset: 0x0009381D
		public static PropertyInfo[] Sort(PropertyInfo[] inf)
		{
			Array.Sort(inf, Comparer.PropertyInfoComparer);
			return inf;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0009562C File Offset: 0x0009382C
		private static int CompareEventInfo(object a, object b)
		{
			EventInfo eventInfo = (EventInfo)a;
			EventInfo eventInfo2 = (EventInfo)b;
			bool isStatic = eventInfo.GetAddMethod(true).IsStatic;
			bool isStatic2 = eventInfo2.GetAddMethod(true).IsStatic;
			if (isStatic == isStatic2)
			{
				return Comparer.CompareMemberInfo(a, b);
			}
			if (isStatic)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00095671 File Offset: 0x00093871
		public static EventInfo[] Sort(EventInfo[] inf)
		{
			Array.Sort(inf, Comparer.EventInfoComparer);
			return inf;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00095680 File Offset: 0x00093880
		// Note: this type is marked as 'beforefieldinit'.
		static Comparer()
		{
		}

		// Token: 0x04000B39 RID: 2873
		private Comparer.ComparerFunc cmp;

		// Token: 0x04000B3A RID: 2874
		private static Comparer MemberInfoComparer = new Comparer(new Comparer.ComparerFunc(Comparer.CompareMemberInfo));

		// Token: 0x04000B3B RID: 2875
		private static Comparer MethodBaseComparer = new Comparer(new Comparer.ComparerFunc(Comparer.CompareMethodBase));

		// Token: 0x04000B3C RID: 2876
		private static Comparer PropertyInfoComparer = new Comparer(new Comparer.ComparerFunc(Comparer.ComparePropertyInfo));

		// Token: 0x04000B3D RID: 2877
		private static Comparer EventInfoComparer = new Comparer(new Comparer.ComparerFunc(Comparer.CompareEventInfo));

		// Token: 0x020003D5 RID: 981
		// (Invoke) Token: 0x06002788 RID: 10120
		private delegate int ComparerFunc(object a, object b);
	}
}
