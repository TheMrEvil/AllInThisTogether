using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x02000006 RID: 6
	internal readonly struct XPathEvaluator
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003058 File Offset: 0x00001258
		public object Evaluate<T>(XNode node, string expression, IXmlNamespaceResolver resolver) where T : class
		{
			object obj = node.CreateNavigator().Evaluate(expression, resolver);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				return this.EvaluateIterator<T>(xpathNodeIterator);
			}
			if (!(obj is T))
			{
				throw new InvalidOperationException(SR.Format("The XPath expression evaluated to unexpected type {0}.", obj.GetType()));
			}
			return (T)((object)obj);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000030AE File Offset: 0x000012AE
		private IEnumerable<T> EvaluateIterator<T>(XPathNodeIterator result)
		{
			foreach (object obj in result)
			{
				XPathNavigator xpathNavigator = (XPathNavigator)obj;
				object r = xpathNavigator.UnderlyingObject;
				if (!(r is T))
				{
					throw new InvalidOperationException(SR.Format("The XPath expression evaluated to unexpected type {0}.", r.GetType()));
				}
				yield return (T)((object)r);
				XText t = r as XText;
				if (t != null && t.GetParent() != null)
				{
					do
					{
						t = (t.NextNode as XText);
						if (t == null)
						{
							break;
						}
						yield return (T)((object)t);
					}
					while (t != t.GetParent().LastNode);
				}
				r = null;
				t = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x02000007 RID: 7
		[CompilerGenerated]
		private sealed class <EvaluateIterator>d__1<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000043 RID: 67 RVA: 0x000030CA File Offset: 0x000012CA
			[DebuggerHidden]
			public <EvaluateIterator>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000030E4 File Offset: 0x000012E4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00003120 File Offset: 0x00001320
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = result.GetEnumerator();
						this.<>1__state = -3;
						goto IL_13F;
					case 1:
						this.<>1__state = -3;
						t = (r as XText);
						if (t == null || t.GetParent() == null)
						{
							goto IL_131;
						}
						break;
					case 2:
						this.<>1__state = -3;
						if (t == t.GetParent().LastNode)
						{
							goto IL_131;
						}
						break;
					default:
						return false;
					}
					t = (t.NextNode as XText);
					if (t != null)
					{
						this.<>2__current = (T)((object)t);
						this.<>1__state = 2;
						return true;
					}
					IL_131:
					r = null;
					t = null;
					IL_13F:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						XPathNavigator xpathNavigator = (XPathNavigator)enumerator.Current;
						r = xpathNavigator.UnderlyingObject;
						if (!(r is T))
						{
							throw new InvalidOperationException(SR.Format("The XPath expression evaluated to unexpected type {0}.", r.GetType()));
						}
						this.<>2__current = (T)((object)r);
						this.<>1__state = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000046 RID: 70 RVA: 0x000032B4 File Offset: 0x000014B4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000047 RID: 71 RVA: 0x000032DD File Offset: 0x000014DD
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000049 RID: 73 RVA: 0x000032EC File Offset: 0x000014EC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600004A RID: 74 RVA: 0x000032FC File Offset: 0x000014FC
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				XPathEvaluator.<EvaluateIterator>d__1<T> <EvaluateIterator>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EvaluateIterator>d__ = this;
				}
				else
				{
					<EvaluateIterator>d__ = new XPathEvaluator.<EvaluateIterator>d__1<T>(0);
				}
				<EvaluateIterator>d__.<>4__this = ref this;
				<EvaluateIterator>d__.result = result;
				return <EvaluateIterator>d__;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x0000334B File Offset: 0x0000154B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000029 RID: 41
			private int <>1__state;

			// Token: 0x0400002A RID: 42
			private T <>2__current;

			// Token: 0x0400002B RID: 43
			private int <>l__initialThreadId;

			// Token: 0x0400002C RID: 44
			private XPathNodeIterator result;

			// Token: 0x0400002D RID: 45
			public XPathNodeIterator <>3__result;

			// Token: 0x0400002E RID: 46
			public XPathEvaluator <>4__this;

			// Token: 0x0400002F RID: 47
			public XPathEvaluator <>3__<>4__this;

			// Token: 0x04000030 RID: 48
			private IEnumerator <>7__wrap1;

			// Token: 0x04000031 RID: 49
			private object <r>5__3;

			// Token: 0x04000032 RID: 50
			private XText <t>5__4;
		}
	}
}
