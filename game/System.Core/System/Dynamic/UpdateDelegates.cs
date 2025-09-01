using System;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	// Token: 0x02000322 RID: 802
	internal static class UpdateDelegates
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x0004FC54 File Offset: 0x0004DE54
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute1<T0, TRet>(CallSite site, T0 arg0)
		{
			CallSite<Func<CallSite, T0, TRet>> callSite = (CallSite<Func<CallSite, T0, TRet>>)site;
			Func<CallSite, T0, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, TRet>[] rules;
			Func<CallSite, T0, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0004FDCC File Offset: 0x0004DFCC
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch1<T0, TRet>(CallSite site, T0 arg0)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0004FDEC File Offset: 0x0004DFEC
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute2<T0, T1, TRet>(CallSite site, T0 arg0, T1 arg1)
		{
			CallSite<Func<CallSite, T0, T1, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, TRet>>)site;
			Func<CallSite, T0, T1, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, TRet>[] rules;
			Func<CallSite, T0, T1, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0004FF70 File Offset: 0x0004E170
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch2<T0, T1, TRet>(CallSite site, T0 arg0, T1 arg1)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0004FF90 File Offset: 0x0004E190
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute3<T0, T1, T2, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			CallSite<Func<CallSite, T0, T1, T2, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, TRet>>)site;
			Func<CallSite, T0, T1, T2, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, TRet>[] rules;
			Func<CallSite, T0, T1, T2, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00050120 File Offset: 0x0004E320
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch3<T0, T1, T2, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00050140 File Offset: 0x0004E340
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute4<T0, T1, T2, T3, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000502E0 File Offset: 0x0004E4E0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch4<T0, T1, T2, T3, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00050300 File Offset: 0x0004E500
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute5<T0, T1, T2, T3, T4, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000504B0 File Offset: 0x0004E6B0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch5<T0, T1, T2, T3, T4, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000504D0 File Offset: 0x0004E6D0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute6<T0, T1, T2, T3, T4, T5, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00050690 File Offset: 0x0004E890
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch6<T0, T1, T2, T3, T4, T5, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000506B0 File Offset: 0x0004E8B0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute7<T0, T1, T2, T3, T4, T5, T6, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00050880 File Offset: 0x0004EA80
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch7<T0, T1, T2, T3, T4, T5, T6, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000508A0 File Offset: 0x0004EAA0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute8<T0, T1, T2, T3, T4, T5, T6, T7, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00050A80 File Offset: 0x0004EC80
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch8<T0, T1, T2, T3, T4, T5, T6, T7, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00050AA0 File Offset: 0x0004ECA0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00050C90 File Offset: 0x0004EE90
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch9<T0, T1, T2, T3, T4, T5, T6, T7, T8, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00050CB0 File Offset: 0x0004EEB0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet UpdateAndExecute10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>> callSite = (CallSite<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>)site;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>[] rules;
			Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet> func;
			if ((rules = CallSiteOps.GetRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					func = rules[i];
					if (func != target)
					{
						callSite.Target = func;
						TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return result;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>> ruleCache = CallSiteOps.GetRuleCache<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				func = rules[j];
				callSite.Target = func;
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, func);
						CallSiteOps.MoveRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(ruleCache, func, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			func = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9
			};
			for (;;)
			{
				callSite.Target = target;
				func = (callSite.Target = callSite.Binder.BindCore<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, args));
				try
				{
					TRet result = func(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return result;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Func<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>>(callSite, func);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00050EB0 File Offset: 0x0004F0B0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static TRet NoMatch10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			site._match = false;
			return default(TRet);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00050ED0 File Offset: 0x0004F0D0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid1<T0>(CallSite site, T0 arg0)
		{
			CallSite<Action<CallSite, T0>> callSite = (CallSite<Action<CallSite, T0>>)site;
			Action<CallSite, T0> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0>[] rules;
			Action<CallSite, T0> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0>>(callSite, args));
				try
				{
					action(site, arg0);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid1<T0>(CallSite site, T0 arg0)
		{
			site._match = false;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00051040 File Offset: 0x0004F240
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid2<T0, T1>(CallSite site, T0 arg0, T1 arg1)
		{
			CallSite<Action<CallSite, T0, T1>> callSite = (CallSite<Action<CallSite, T0, T1>>)site;
			Action<CallSite, T0, T1> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1>[] rules;
			Action<CallSite, T0, T1> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1>>(callSite, args));
				try
				{
					action(site, arg0, arg1);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid2<T0, T1>(CallSite site, T0 arg0, T1 arg1)
		{
			site._match = false;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000511B0 File Offset: 0x0004F3B0
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid3<T0, T1, T2>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			CallSite<Action<CallSite, T0, T1, T2>> callSite = (CallSite<Action<CallSite, T0, T1, T2>>)site;
			Action<CallSite, T0, T1, T2> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2>[] rules;
			Action<CallSite, T0, T1, T2> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid3<T0, T1, T2>(CallSite site, T0 arg0, T1 arg1, T2 arg2)
		{
			site._match = false;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0005132C File Offset: 0x0004F52C
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid4<T0, T1, T2, T3>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3>>)site;
			Action<CallSite, T0, T1, T2, T3> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3>[] rules;
			Action<CallSite, T0, T1, T2, T3> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid4<T0, T1, T2, T3>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			site._match = false;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000514B8 File Offset: 0x0004F6B8
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid5<T0, T1, T2, T3, T4>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4>>)site;
			Action<CallSite, T0, T1, T2, T3, T4> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid5<T0, T1, T2, T3, T4>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			site._match = false;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00051654 File Offset: 0x0004F854
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid6<T0, T1, T2, T3, T4, T5>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4, T5>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid6<T0, T1, T2, T3, T4, T5>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			site._match = false;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00051800 File Offset: 0x0004FA00
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid7<T0, T1, T2, T3, T4, T5, T6>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid7<T0, T1, T2, T3, T4, T5, T6>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			site._match = false;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000519BC File Offset: 0x0004FBBC
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid8<T0, T1, T2, T3, T4, T5, T6, T7>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid8<T0, T1, T2, T3, T4, T5, T6, T7>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			site._match = false;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00051B88 File Offset: 0x0004FD88
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid9<T0, T1, T2, T3, T4, T5, T6, T7, T8>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			site._match = false;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00051D68 File Offset: 0x0004FF68
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void UpdateAndExecuteVoid10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> callSite = (CallSite<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>)site;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> target = callSite.Target;
			site = callSite.GetMatchmaker();
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>[] rules;
			Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action;
			if ((rules = CallSiteOps.GetRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite)) != null)
			{
				for (int i = 0; i < rules.Length; i++)
				{
					action = rules[i];
					if (action != target)
					{
						callSite.Target = action;
						action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
						if (CallSiteOps.GetMatch(site))
						{
							CallSiteOps.UpdateRules<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, i);
							callSite.ReleaseMatchmaker(site);
							return;
						}
						CallSiteOps.ClearMatch(site);
					}
				}
			}
			RuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> ruleCache = CallSiteOps.GetRuleCache<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite);
			rules = ruleCache.GetRules();
			for (int j = 0; j < rules.Length; j++)
			{
				action = rules[j];
				callSite.Target = action;
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, action);
						CallSiteOps.MoveRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(ruleCache, action, j);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
			action = null;
			object[] args = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				arg8,
				arg9
			};
			for (;;)
			{
				callSite.Target = target;
				action = (callSite.Target = callSite.Binder.BindCore<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, args));
				try
				{
					action(site, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
					if (CallSiteOps.GetMatch(site))
					{
						callSite.ReleaseMatchmaker(site);
						return;
					}
				}
				finally
				{
					if (CallSiteOps.GetMatch(site))
					{
						CallSiteOps.AddRule<Action<CallSite, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(callSite, action);
					}
				}
				CallSiteOps.ClearMatch(site);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00051034 File Offset: 0x0004F234
		[Obsolete("pregenerated CallSite<T>.Update delegate", true)]
		internal static void NoMatchVoid10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(CallSite site, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			site._match = false;
		}
	}
}
