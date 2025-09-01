using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Xml.Linq
{
	/// <summary>Represents a node or an attribute in an XML tree.</summary>
	// Token: 0x0200005A RID: 90
	public abstract class XObject : IXmlLineInfo
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00003E36 File Offset: 0x00002036
		internal XObject()
		{
		}

		/// <summary>Gets the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the base URI for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000F818 File Offset: 0x0000DA18
		public string BaseUri
		{
			get
			{
				XObject xobject = this;
				BaseUriAnnotation baseUriAnnotation;
				for (;;)
				{
					if (xobject == null || xobject.annotations != null)
					{
						if (xobject == null)
						{
							goto IL_33;
						}
						baseUriAnnotation = xobject.Annotation<BaseUriAnnotation>();
						if (baseUriAnnotation != null)
						{
							break;
						}
						xobject = xobject.parent;
					}
					else
					{
						xobject = xobject.parent;
					}
				}
				return baseUriAnnotation.baseUri;
				IL_33:
				return string.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XDocument" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000F860 File Offset: 0x0000DA60
		public XDocument Document
		{
			get
			{
				XObject xobject = this;
				while (xobject.parent != null)
				{
					xobject = xobject.parent;
				}
				return xobject as XDocument;
			}
		}

		/// <summary>Gets the node type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The node type for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600035F RID: 863
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Linq.XElement" /> of this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000F886 File Offset: 0x0000DA86
		public XElement Parent
		{
			get
			{
				return this.parent as XElement;
			}
		}

		/// <summary>Adds an object to the annotation list of this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="annotation">An object that contains the annotation to add.</param>
		// Token: 0x06000361 RID: 865 RVA: 0x0000F894 File Offset: 0x0000DA94
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this.annotations == null)
			{
				object obj;
				if (!(annotation is object[]))
				{
					obj = annotation;
				}
				else
				{
					(obj = new object[1])[0] = annotation;
				}
				this.annotations = obj;
				return;
			}
			object[] array = this.annotations as object[];
			if (array == null)
			{
				this.annotations = new object[]
				{
					this.annotations,
					annotation
				};
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this.annotations = array;
			}
			array[num] = annotation;
		}

		/// <summary>Gets the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of the annotation to retrieve.</param>
		/// <returns>The <see cref="T:System.Object" /> that contains the first annotation object that matches the specified type, or <see langword="null" /> if no annotation is of the specified type.</returns>
		// Token: 0x06000362 RID: 866 RVA: 0x0000F92C File Offset: 0x0000DB2C
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (XHelper.IsInstanceOfType(this.annotations, type))
					{
						return this.annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (XHelper.IsInstanceOfType(obj, type))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F99C File Offset: 0x0000DB9C
		private object AnnotationForSealedType(Type type)
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (this.annotations.GetType() == type)
					{
						return this.annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (obj.GetType() == type)
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Gets the first annotation object of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of the annotation to retrieve.</typeparam>
		/// <returns>The first annotation object that matches the specified type, or <see langword="null" /> if no annotation is of the specified type.</returns>
		// Token: 0x06000364 RID: 868 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public T Annotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					return this.annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of the annotations to retrieve.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Object" /> that contains the annotations that match the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x06000365 RID: 869 RVA: 0x0000FA6A File Offset: 0x0000DC6A
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this.AnnotationsIterator(type);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FA87 File Offset: 0x0000DC87
		private IEnumerable<object> AnnotationsIterator(Type type)
		{
			if (this.annotations != null)
			{
				object[] a = this.annotations as object[];
				if (a == null)
				{
					if (XHelper.IsInstanceOfType(this.annotations, type))
					{
						yield return this.annotations;
					}
				}
				else
				{
					int num;
					for (int i = 0; i < a.Length; i = num + 1)
					{
						object obj = a[i];
						if (obj == null)
						{
							break;
						}
						if (XHelper.IsInstanceOfType(obj, type))
						{
							yield return obj;
						}
						num = i;
					}
				}
				a = null;
			}
			yield break;
		}

		/// <summary>Gets a collection of annotations of the specified type for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of the annotations to retrieve.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the annotations for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x06000367 RID: 871 RVA: 0x0000FA9E File Offset: 0x0000DC9E
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] a = this.annotations as object[];
				if (a == null)
				{
					T t = this.annotations as T;
					if (t != null)
					{
						yield return t;
					}
				}
				else
				{
					int num;
					for (int i = 0; i < a.Length; i = num + 1)
					{
						object obj = a[i];
						if (obj == null)
						{
							break;
						}
						T t2 = obj as T;
						if (t2 != null)
						{
							yield return t2;
						}
						num = i;
					}
				}
				a = null;
			}
			yield break;
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <param name="type">The type of annotations to remove.</param>
		// Token: 0x06000368 RID: 872 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (XHelper.IsInstanceOfType(this.annotations, type))
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!XHelper.IsInstanceOfType(obj, type))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j == 0)
					{
						this.annotations = null;
						return;
					}
					while (j < i)
					{
						array[j++] = null;
					}
				}
			}
		}

		/// <summary>Removes the annotations of the specified type from this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <typeparam name="T">The type of annotations to remove.</typeparam>
		// Token: 0x06000369 RID: 873 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public void RemoveAnnotations<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (this.annotations is T)
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j == 0)
					{
						this.annotations = null;
						return;
					}
					while (j < i)
					{
						array[j++] = null;
					}
				}
			}
		}

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants have changed.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600036A RID: 874 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		// (remove) Token: 0x0600036B RID: 875 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public event EventHandler<XObjectChangeEventArgs> Changed
		{
			add
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					xobjectChangeAnnotation = new XObjectChangeAnnotation();
					this.AddAnnotation(xobjectChangeAnnotation);
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xobjectChangeAnnotation2.changed, value);
			}
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xobjectChangeAnnotation2.changed, value);
				if (xobjectChangeAnnotation.changing == null && xobjectChangeAnnotation.changed == null)
				{
					this.RemoveAnnotations<XObjectChangeAnnotation>();
				}
			}
		}

		/// <summary>Raised when this <see cref="T:System.Xml.Linq.XObject" /> or any of its descendants are about to change.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600036C RID: 876 RVA: 0x0000FC44 File Offset: 0x0000DE44
		// (remove) Token: 0x0600036D RID: 877 RVA: 0x0000FC84 File Offset: 0x0000DE84
		public event EventHandler<XObjectChangeEventArgs> Changing
		{
			add
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					xobjectChangeAnnotation = new XObjectChangeAnnotation();
					this.AddAnnotation(xobjectChangeAnnotation);
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xobjectChangeAnnotation2.changing, value);
			}
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xobjectChangeAnnotation2.changing, value);
				if (xobjectChangeAnnotation.changing == null && xobjectChangeAnnotation.changed == null)
				{
					this.RemoveAnnotations<XObjectChangeAnnotation>();
				}
			}
		}

		/// <summary>Gets a value indicating whether or not this <see cref="T:System.Xml.Linq.XObject" /> has line information.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Xml.Linq.XObject" /> has line information; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600036E RID: 878 RVA: 0x0000FCCD File Offset: 0x0000DECD
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.Annotation<LineInfoAnnotation>() != null;
		}

		/// <summary>Gets the line number that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line number reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		int IXmlLineInfo.LineNumber
		{
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.lineNumber;
				}
				return 0;
			}
		}

		/// <summary>Gets the line position that the underlying <see cref="T:System.Xml.XmlReader" /> reported for this <see cref="T:System.Xml.Linq.XObject" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the line position reported by the <see cref="T:System.Xml.XmlReader" /> for this <see cref="T:System.Xml.Linq.XObject" />.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		int IXmlLineInfo.LinePosition
		{
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.linePosition;
				}
				return 0;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000FD17 File Offset: 0x0000DF17
		internal bool HasBaseUri
		{
			get
			{
				return this.Annotation<BaseUriAnnotation>() != null;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000FD24 File Offset: 0x0000DF24
		internal bool NotifyChanged(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						result = true;
						if (xobjectChangeAnnotation.changed != null)
						{
							xobjectChangeAnnotation.changed(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FD78 File Offset: 0x0000DF78
		internal bool NotifyChanging(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						result = true;
						if (xobjectChangeAnnotation.changing != null)
						{
							xobjectChangeAnnotation.changing(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FDCB File Offset: 0x0000DFCB
		internal void SetBaseUri(string baseUri)
		{
			this.AddAnnotation(new BaseUriAnnotation(baseUri));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FDD9 File Offset: 0x0000DFD9
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		internal bool SkipNotify()
		{
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					if (xobject.Annotation<XObjectChangeAnnotation>() != null)
					{
						return false;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return true;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FE24 File Offset: 0x0000E024
		internal SaveOptions GetSaveOptionsFromAnnotations()
		{
			XObject xobject = this;
			object obj;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					obj = xobject.AnnotationForSealedType(typeof(SaveOptions));
					if (obj != null)
					{
						goto Block_3;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return SaveOptions.None;
			Block_3:
			return (SaveOptions)obj;
		}

		// Token: 0x040001C7 RID: 455
		internal XContainer parent;

		// Token: 0x040001C8 RID: 456
		internal object annotations;

		// Token: 0x0200005B RID: 91
		[CompilerGenerated]
		private sealed class <AnnotationsIterator>d__16 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06000378 RID: 888 RVA: 0x0000FE6F File Offset: 0x0000E06F
			[DebuggerHidden]
			public <AnnotationsIterator>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000379 RID: 889 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000FE8C File Offset: 0x0000E08C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XObject xobject = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					if (xobject.annotations == null)
					{
						return false;
					}
					a = (xobject.annotations as object[]);
					if (a != null)
					{
						i = 0;
						goto IL_CD;
					}
					if (XHelper.IsInstanceOfType(xobject.annotations, type))
					{
						this.<>2__current = xobject.annotations;
						this.<>1__state = 1;
						return true;
					}
					goto IL_DD;
				case 1:
					this.<>1__state = -1;
					goto IL_DD;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				IL_BD:
				int num2 = i;
				i = num2 + 1;
				IL_CD:
				if (i < a.Length)
				{
					object obj = a[i];
					if (obj != null)
					{
						if (XHelper.IsInstanceOfType(obj, type))
						{
							this.<>2__current = obj;
							this.<>1__state = 2;
							return true;
						}
						goto IL_BD;
					}
				}
				IL_DD:
				a = null;
				return false;
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000FF7E File Offset: 0x0000E17E
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600037C RID: 892 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000FF7E File Offset: 0x0000E17E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0000FF88 File Offset: 0x0000E188
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				XObject.<AnnotationsIterator>d__16 <AnnotationsIterator>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<AnnotationsIterator>d__ = this;
				}
				else
				{
					<AnnotationsIterator>d__ = new XObject.<AnnotationsIterator>d__16(0);
					<AnnotationsIterator>d__.<>4__this = this;
				}
				<AnnotationsIterator>d__.type = type;
				return <AnnotationsIterator>d__;
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000FFD7 File Offset: 0x0000E1D7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x040001C9 RID: 457
			private int <>1__state;

			// Token: 0x040001CA RID: 458
			private object <>2__current;

			// Token: 0x040001CB RID: 459
			private int <>l__initialThreadId;

			// Token: 0x040001CC RID: 460
			public XObject <>4__this;

			// Token: 0x040001CD RID: 461
			private Type type;

			// Token: 0x040001CE RID: 462
			public Type <>3__type;

			// Token: 0x040001CF RID: 463
			private object[] <a>5__2;

			// Token: 0x040001D0 RID: 464
			private int <i>5__3;
		}

		// Token: 0x0200005C RID: 92
		[CompilerGenerated]
		private sealed class <Annotations>d__17<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator where T : class
		{
			// Token: 0x06000380 RID: 896 RVA: 0x0000FFDF File Offset: 0x0000E1DF
			[DebuggerHidden]
			public <Annotations>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000381 RID: 897 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000FFFC File Offset: 0x0000E1FC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XObject xobject = this;
				switch (num)
				{
				case 0:
				{
					this.<>1__state = -1;
					if (xobject.annotations == null)
					{
						return false;
					}
					a = (xobject.annotations as object[]);
					if (a != null)
					{
						i = 0;
						goto IL_DC;
					}
					T t = xobject.annotations as T;
					if (t != null)
					{
						this.<>2__current = t;
						this.<>1__state = 1;
						return true;
					}
					goto IL_EC;
				}
				case 1:
					this.<>1__state = -1;
					goto IL_EC;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				IL_CA:
				int num2 = i;
				i = num2 + 1;
				IL_DC:
				if (i < a.Length)
				{
					object obj = a[i];
					if (obj != null)
					{
						T t2 = obj as T;
						if (t2 != null)
						{
							this.<>2__current = t2;
							this.<>1__state = 2;
							return true;
						}
						goto IL_CA;
					}
				}
				IL_EC:
				a = null;
				return false;
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000383 RID: 899 RVA: 0x000100FD File Offset: 0x0000E2FD
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000384 RID: 900 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000385 RID: 901 RVA: 0x00010105 File Offset: 0x0000E305
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000386 RID: 902 RVA: 0x00010114 File Offset: 0x0000E314
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				XObject.<Annotations>d__17<T> <Annotations>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Annotations>d__ = this;
				}
				else
				{
					<Annotations>d__ = new XObject.<Annotations>d__17<T>(0);
					<Annotations>d__.<>4__this = this;
				}
				return <Annotations>d__;
			}

			// Token: 0x06000387 RID: 903 RVA: 0x00010157 File Offset: 0x0000E357
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x040001D1 RID: 465
			private int <>1__state;

			// Token: 0x040001D2 RID: 466
			private T <>2__current;

			// Token: 0x040001D3 RID: 467
			private int <>l__initialThreadId;

			// Token: 0x040001D4 RID: 468
			public XObject <>4__this;

			// Token: 0x040001D5 RID: 469
			private object[] <a>5__2;

			// Token: 0x040001D6 RID: 470
			private int <i>5__3;
		}
	}
}
