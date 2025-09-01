using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class KdTree<TKey, TValue> : IKdTree<TKey, TValue>, IEnumerable<KdTreeNode<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000021D5 File Offset: 0x000003D5
		public KdTree(int dimensions, ITypeMath<TKey> typeMath)
		{
			this.dimensions = dimensions;
			this.typeMath = typeMath;
			this.Count = 0;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F2 File Offset: 0x000003F2
		public KdTree(int dimensions, ITypeMath<TKey> typeMath, AddDuplicateBehavior addDuplicateBehavior) : this(dimensions, typeMath)
		{
			this.AddDuplicateBehavior = addDuplicateBehavior;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002203 File Offset: 0x00000403
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000220B File Offset: 0x0000040B
		public AddDuplicateBehavior AddDuplicateBehavior
		{
			[CompilerGenerated]
			get
			{
				return this.<AddDuplicateBehavior>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AddDuplicateBehavior>k__BackingField = value;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002214 File Offset: 0x00000414
		public bool Add(TKey[] point, TValue value)
		{
			KdTreeNode<TKey, TValue> value2 = new KdTreeNode<TKey, TValue>(point, value);
			if (this.root == null)
			{
				this.root = new KdTreeNode<TKey, TValue>(point, value);
			}
			else
			{
				int num = -1;
				KdTreeNode<TKey, TValue> kdTreeNode = this.root;
				int compare;
				for (;;)
				{
					num = (num + 1) % this.dimensions;
					if (this.typeMath.AreEqual(point, kdTreeNode.Point))
					{
						switch (this.AddDuplicateBehavior)
						{
						case AddDuplicateBehavior.Skip:
							return false;
						case AddDuplicateBehavior.Error:
							goto IL_6D;
						case AddDuplicateBehavior.Update:
							kdTreeNode.Value = value;
							goto IL_90;
						case AddDuplicateBehavior.Collect:
							goto IL_7C;
						}
						break;
					}
					IL_90:
					compare = this.typeMath.Compare(point[num], kdTreeNode.Point[num]);
					if (kdTreeNode[compare] == null)
					{
						goto Block_4;
					}
					kdTreeNode = kdTreeNode[compare];
				}
				throw new Exception("Unexpected AddDuplicateBehavior");
				IL_6D:
				throw new DuplicateNodeError();
				IL_7C:
				kdTreeNode.AddDuplicate(value);
				return false;
				Block_4:
				kdTreeNode[compare] = value2;
			}
			int count = this.Count;
			this.Count = count + 1;
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002304 File Offset: 0x00000504
		private void ReadChildNodes(KdTreeNode<TKey, TValue> removedNode)
		{
			if (removedNode.IsLeaf)
			{
				return;
			}
			Queue<KdTreeNode<TKey, TValue>> queue = new Queue<KdTreeNode<TKey, TValue>>();
			Queue<KdTreeNode<TKey, TValue>> queue2 = new Queue<KdTreeNode<TKey, TValue>>();
			if (removedNode.LeftChild != null)
			{
				queue2.Enqueue(removedNode.LeftChild);
			}
			if (removedNode.RightChild != null)
			{
				queue2.Enqueue(removedNode.RightChild);
			}
			while (queue2.Count > 0)
			{
				KdTreeNode<TKey, TValue> kdTreeNode = queue2.Dequeue();
				queue.Enqueue(kdTreeNode);
				for (int i = -1; i <= 1; i += 2)
				{
					if (kdTreeNode[i] != null)
					{
						queue2.Enqueue(kdTreeNode[i]);
						kdTreeNode[i] = null;
					}
				}
			}
			while (queue.Count > 0)
			{
				KdTreeNode<TKey, TValue> kdTreeNode2 = queue.Dequeue();
				int count = this.Count;
				this.Count = count - 1;
				this.Add(kdTreeNode2.Point, kdTreeNode2.Value);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023CC File Offset: 0x000005CC
		public void RemoveAt(TKey[] point)
		{
			if (this.root == null)
			{
				return;
			}
			KdTreeNode<TKey, TValue> kdTreeNode;
			if (this.typeMath.AreEqual(point, this.root.Point))
			{
				kdTreeNode = this.root;
				this.root = null;
				int count = this.Count;
				this.Count = count - 1;
				this.ReadChildNodes(kdTreeNode);
				return;
			}
			kdTreeNode = this.root;
			int num = -1;
			for (;;)
			{
				num = (num + 1) % this.dimensions;
				int compare = this.typeMath.Compare(point[num], kdTreeNode.Point[num]);
				if (kdTreeNode[compare] == null)
				{
					break;
				}
				if (this.typeMath.AreEqual(point, kdTreeNode[compare].Point))
				{
					KdTreeNode<TKey, TValue> removedNode = kdTreeNode[compare];
					kdTreeNode[compare] = null;
					int count = this.Count;
					this.Count = count - 1;
					this.ReadChildNodes(removedNode);
				}
				else
				{
					kdTreeNode = kdTreeNode[compare];
				}
				if (kdTreeNode == null)
				{
					return;
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024B4 File Offset: 0x000006B4
		public KdTreeNode<TKey, TValue>[] GetNearestNeighbours(TKey[] point, int count)
		{
			if (count > this.Count)
			{
				count = this.Count;
			}
			if (count < 0)
			{
				throw new ArgumentException("Number of neighbors cannot be negative");
			}
			if (count == 0)
			{
				return new KdTreeNode<TKey, TValue>[0];
			}
			NearestNeighbourList<KdTreeNode<TKey, TValue>, TKey> nearestNeighbourList = new NearestNeighbourList<KdTreeNode<TKey, TValue>, TKey>(count, this.typeMath);
			HyperRect<TKey> rect = HyperRect<TKey>.Infinite(this.dimensions, this.typeMath);
			this.AddNearestNeighbours(this.root, point, rect, 0, nearestNeighbourList, this.typeMath.MaxValue);
			count = nearestNeighbourList.Count;
			KdTreeNode<TKey, TValue>[] array = new KdTreeNode<TKey, TValue>[count];
			for (int i = 0; i < count; i++)
			{
				array[count - i - 1] = nearestNeighbourList.RemoveFurtherest();
			}
			return array;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002550 File Offset: 0x00000750
		private void AddNearestNeighbours(KdTreeNode<TKey, TValue> node, TKey[] target, HyperRect<TKey> rect, int depth, NearestNeighbourList<KdTreeNode<TKey, TValue>, TKey> nearestNeighbours, TKey maxSearchRadiusSquared)
		{
			if (node == null)
			{
				return;
			}
			int num = depth % this.dimensions;
			HyperRect<TKey> hyperRect = rect.Clone();
			hyperRect.MaxPoint[num] = node.Point[num];
			HyperRect<TKey> hyperRect2 = rect.Clone();
			hyperRect2.MinPoint[num] = node.Point[num];
			int num2 = this.typeMath.Compare(target[num], node.Point[num]);
			HyperRect<TKey> rect2 = (num2 <= 0) ? hyperRect : hyperRect2;
			HyperRect<TKey> rect3 = (num2 <= 0) ? hyperRect2 : hyperRect;
			KdTreeNode<TKey, TValue> kdTreeNode = (num2 <= 0) ? node.LeftChild : node.RightChild;
			KdTreeNode<TKey, TValue> node2 = (num2 <= 0) ? node.RightChild : node.LeftChild;
			if (kdTreeNode != null)
			{
				this.AddNearestNeighbours(kdTreeNode, target, rect2, depth + 1, nearestNeighbours, maxSearchRadiusSquared);
			}
			TKey[] closestPoint = rect3.GetClosestPoint(target, this.typeMath);
			TKey tkey = this.typeMath.DistanceSquaredBetweenPoints(closestPoint, target);
			if (this.typeMath.Compare(tkey, maxSearchRadiusSquared) <= 0)
			{
				if (nearestNeighbours.IsCapacityReached)
				{
					if (this.typeMath.Compare(tkey, nearestNeighbours.GetFurtherestDistance()) < 0)
					{
						this.AddNearestNeighbours(node2, target, rect3, depth + 1, nearestNeighbours, maxSearchRadiusSquared);
					}
				}
				else
				{
					this.AddNearestNeighbours(node2, target, rect3, depth + 1, nearestNeighbours, maxSearchRadiusSquared);
				}
			}
			tkey = this.typeMath.DistanceSquaredBetweenPoints(node.Point, target);
			if (this.typeMath.Compare(tkey, maxSearchRadiusSquared) <= 0)
			{
				nearestNeighbours.Add(node, tkey);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026C8 File Offset: 0x000008C8
		public KdTreeNode<TKey, TValue>[] RadialSearch(TKey[] center, TKey radius, int count)
		{
			NearestNeighbourList<KdTreeNode<TKey, TValue>, TKey> nearestNeighbourList = new NearestNeighbourList<KdTreeNode<TKey, TValue>, TKey>(count, this.typeMath);
			this.AddNearestNeighbours(this.root, center, HyperRect<TKey>.Infinite(this.dimensions, this.typeMath), 0, nearestNeighbourList, this.typeMath.Multiply(radius, radius));
			count = nearestNeighbourList.Count;
			KdTreeNode<TKey, TValue>[] array = new KdTreeNode<TKey, TValue>[count];
			for (int i = 0; i < count; i++)
			{
				array[count - i - 1] = nearestNeighbourList.RemoveFurtherest();
			}
			return array;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002740 File Offset: 0x00000940
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Count>k__BackingField = value;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000274C File Offset: 0x0000094C
		public bool TryFindValueAt(TKey[] point, out TValue value)
		{
			KdTreeNode<TKey, TValue> kdTreeNode = this.root;
			int num = -1;
			while (kdTreeNode != null)
			{
				if (this.typeMath.AreEqual(point, kdTreeNode.Point))
				{
					value = kdTreeNode.Value;
					return true;
				}
				num = (num + 1) % this.dimensions;
				int compare = this.typeMath.Compare(point[num], kdTreeNode.Point[num]);
				kdTreeNode = kdTreeNode[compare];
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027C4 File Offset: 0x000009C4
		public TValue FindValueAt(TKey[] point)
		{
			TValue result;
			if (this.TryFindValueAt(point, out result))
			{
				return result;
			}
			return default(TValue);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027E8 File Offset: 0x000009E8
		public bool TryFindValue(TValue value, out TKey[] point)
		{
			if (this.root == null)
			{
				point = null;
				return false;
			}
			Queue<KdTreeNode<TKey, TValue>> queue = new Queue<KdTreeNode<TKey, TValue>>();
			queue.Enqueue(this.root);
			while (queue.Count > 0)
			{
				KdTreeNode<TKey, TValue> kdTreeNode = queue.Dequeue();
				if (kdTreeNode.Value.Equals(value))
				{
					point = kdTreeNode.Point;
					return true;
				}
				for (int i = -1; i <= 1; i += 2)
				{
					KdTreeNode<TKey, TValue> kdTreeNode2 = kdTreeNode[i];
					if (kdTreeNode2 != null)
					{
						queue.Enqueue(kdTreeNode2);
					}
				}
			}
			point = null;
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000286C File Offset: 0x00000A6C
		public TKey[] FindValue(TValue value)
		{
			TKey[] result;
			if (this.TryFindValue(value, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002888 File Offset: 0x00000A88
		private void AddNodeToStringBuilder(KdTreeNode<TKey, TValue> node, StringBuilder sb, int depth)
		{
			sb.AppendLine(node.ToString());
			for (int i = -1; i <= 1; i += 2)
			{
				for (int j = 0; j <= depth; j++)
				{
					sb.Append("\t");
				}
				sb.Append((i == -1) ? "L " : "R ");
				if (node[i] == null)
				{
					sb.AppendLine("");
				}
				else
				{
					this.AddNodeToStringBuilder(node[i], sb, depth + 1);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002908 File Offset: 0x00000B08
		public override string ToString()
		{
			if (this.root == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.AddNodeToStringBuilder(this.root, stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002940 File Offset: 0x00000B40
		private void AddNodesToList(KdTreeNode<TKey, TValue> node, List<KdTreeNode<TKey, TValue>> nodes)
		{
			if (node == null)
			{
				return;
			}
			nodes.Add(node);
			for (int i = -1; i <= 1; i += 2)
			{
				if (node[i] != null)
				{
					this.AddNodesToList(node[i], nodes);
					node[i] = null;
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002984 File Offset: 0x00000B84
		private void SortNodesArray(KdTreeNode<TKey, TValue>[] nodes, int byDimension, int fromIndex, int toIndex)
		{
			for (int i = fromIndex + 1; i <= toIndex; i++)
			{
				int num = i;
				for (;;)
				{
					KdTreeNode<TKey, TValue> kdTreeNode = nodes[num - 1];
					KdTreeNode<TKey, TValue> kdTreeNode2 = nodes[num];
					if (this.typeMath.Compare(kdTreeNode2.Point[byDimension], kdTreeNode.Point[byDimension]) >= 0)
					{
						break;
					}
					nodes[num - 1] = kdTreeNode2;
					nodes[num] = kdTreeNode;
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029E0 File Offset: 0x00000BE0
		private void AddNodesBalanced(KdTreeNode<TKey, TValue>[] nodes, int byDimension, int fromIndex, int toIndex)
		{
			if (fromIndex == toIndex)
			{
				this.Add(nodes[fromIndex].Point, nodes[fromIndex].Value);
				nodes[fromIndex] = null;
				return;
			}
			this.SortNodesArray(nodes, byDimension, fromIndex, toIndex);
			int num = fromIndex + (int)Math.Round((double)((float)(toIndex + 1 - fromIndex) / 2f)) - 1;
			this.Add(nodes[num].Point, nodes[num].Value);
			nodes[num] = null;
			int byDimension2 = (byDimension + 1) % this.dimensions;
			if (fromIndex < num)
			{
				this.AddNodesBalanced(nodes, byDimension2, fromIndex, num - 1);
			}
			if (toIndex > num)
			{
				this.AddNodesBalanced(nodes, byDimension2, num + 1, toIndex);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A7C File Offset: 0x00000C7C
		public void Balance()
		{
			List<KdTreeNode<TKey, TValue>> list = new List<KdTreeNode<TKey, TValue>>();
			this.AddNodesToList(this.root, list);
			this.Clear();
			this.AddNodesBalanced(list.ToArray(), 0, 0, list.Count - 1);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AB8 File Offset: 0x00000CB8
		private void RemoveChildNodes(KdTreeNode<TKey, TValue> node)
		{
			for (int i = -1; i <= 1; i += 2)
			{
				if (node[i] != null)
				{
					this.RemoveChildNodes(node[i]);
					node[i] = null;
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002AEF File Offset: 0x00000CEF
		public void Clear()
		{
			if (this.root != null)
			{
				this.RemoveChildNodes(this.root);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B08 File Offset: 0x00000D08
		public void SaveToFile(string filename)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using (FileStream fileStream = File.Create(filename))
			{
				binaryFormatter.Serialize(fileStream, this);
				fileStream.Flush();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B4C File Offset: 0x00000D4C
		public static KdTree<TKey, TValue> LoadFromFile(string filename)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			KdTree<TKey, TValue> result;
			using (FileStream fileStream = File.Open(filename, FileMode.Open))
			{
				result = (KdTree<TKey, TValue>)binaryFormatter.Deserialize(fileStream);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B94 File Offset: 0x00000D94
		public IEnumerator<KdTreeNode<TKey, TValue>> GetEnumerator()
		{
			Stack<KdTreeNode<TKey, TValue>> left = new Stack<KdTreeNode<TKey, TValue>>();
			Stack<KdTreeNode<TKey, TValue>> right = new Stack<KdTreeNode<TKey, TValue>>();
			Action<KdTreeNode<TKey, TValue>> addLeft = delegate(KdTreeNode<TKey, TValue> node)
			{
				if (node.LeftChild != null)
				{
					left.Push(node.LeftChild);
				}
			};
			Action<KdTreeNode<TKey, TValue>> addRight = delegate(KdTreeNode<TKey, TValue> node)
			{
				if (node.RightChild != null)
				{
					right.Push(node.RightChild);
				}
			};
			if (this.root != null)
			{
				yield return this.root;
				addLeft(this.root);
				addRight(this.root);
				for (;;)
				{
					if (left.Any<KdTreeNode<TKey, TValue>>())
					{
						KdTreeNode<TKey, TValue> kdTreeNode = left.Pop();
						addLeft(kdTreeNode);
						addRight(kdTreeNode);
						yield return kdTreeNode;
					}
					else
					{
						if (!right.Any<KdTreeNode<TKey, TValue>>())
						{
							break;
						}
						KdTreeNode<TKey, TValue> kdTreeNode2 = right.Pop();
						addLeft(kdTreeNode2);
						addRight(kdTreeNode2);
						yield return kdTreeNode2;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BA3 File Offset: 0x00000DA3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000008 RID: 8
		private int dimensions;

		// Token: 0x04000009 RID: 9
		private ITypeMath<TKey> typeMath;

		// Token: 0x0400000A RID: 10
		private KdTreeNode<TKey, TValue> root;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private AddDuplicateBehavior <AddDuplicateBehavior>k__BackingField;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private int <Count>k__BackingField;

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x06000083 RID: 131 RVA: 0x00003176 File Offset: 0x00001376
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x06000084 RID: 132 RVA: 0x0000317E File Offset: 0x0000137E
			internal void <GetEnumerator>b__0(KdTreeNode<TKey, TValue> node)
			{
				if (node.LeftChild != null)
				{
					this.left.Push(node.LeftChild);
				}
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00003199 File Offset: 0x00001399
			internal void <GetEnumerator>b__1(KdTreeNode<TKey, TValue> node)
			{
				if (node.RightChild != null)
				{
					this.right.Push(node.RightChild);
				}
			}

			// Token: 0x0400001B RID: 27
			public Stack<KdTreeNode<TKey, TValue>> left;

			// Token: 0x0400001C RID: 28
			public Stack<KdTreeNode<TKey, TValue>> right;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__33 : IEnumerator<KdTreeNode<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000086 RID: 134 RVA: 0x000031B4 File Offset: 0x000013B4
			[DebuggerHidden]
			public <GetEnumerator>d__33(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x000031C3 File Offset: 0x000013C3
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000088 RID: 136 RVA: 0x000031C8 File Offset: 0x000013C8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				KdTree<TKey, TValue> kdTree = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					CS$<>8__locals1 = new KdTree<TKey, TValue>.<>c__DisplayClass33_0();
					CS$<>8__locals1.left = new Stack<KdTreeNode<TKey, TValue>>();
					CS$<>8__locals1.right = new Stack<KdTreeNode<TKey, TValue>>();
					addLeft = new Action<KdTreeNode<TKey, TValue>>(CS$<>8__locals1.<GetEnumerator>b__0);
					addRight = new Action<KdTreeNode<TKey, TValue>>(CS$<>8__locals1.<GetEnumerator>b__1);
					if (kdTree.root != null)
					{
						this.<>2__current = kdTree.root;
						this.<>1__state = 1;
						return true;
					}
					return false;
				case 1:
					this.<>1__state = -1;
					addLeft(kdTree.root);
					addRight(kdTree.root);
					break;
				case 2:
					this.<>1__state = -1;
					break;
				case 3:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (CS$<>8__locals1.left.Any<KdTreeNode<TKey, TValue>>())
				{
					KdTreeNode<TKey, TValue> obj = CS$<>8__locals1.left.Pop();
					addLeft(obj);
					addRight(obj);
					this.<>2__current = obj;
					this.<>1__state = 2;
					return true;
				}
				if (CS$<>8__locals1.right.Any<KdTreeNode<TKey, TValue>>())
				{
					KdTreeNode<TKey, TValue> obj2 = CS$<>8__locals1.right.Pop();
					addLeft(obj2);
					addRight(obj2);
					this.<>2__current = obj2;
					this.<>1__state = 3;
					return true;
				}
				return false;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000089 RID: 137 RVA: 0x00003350 File Offset: 0x00001550
			KdTreeNode<TKey, TValue> IEnumerator<KdTreeNode<!0, !1>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00003358 File Offset: 0x00001558
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600008B RID: 139 RVA: 0x0000335F File Offset: 0x0000155F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400001D RID: 29
			private int <>1__state;

			// Token: 0x0400001E RID: 30
			private KdTreeNode<TKey, TValue> <>2__current;

			// Token: 0x0400001F RID: 31
			public KdTree<TKey, TValue> <>4__this;

			// Token: 0x04000020 RID: 32
			private KdTree<TKey, TValue>.<>c__DisplayClass33_0 <>8__1;

			// Token: 0x04000021 RID: 33
			private Action<KdTreeNode<TKey, TValue>> <addLeft>5__2;

			// Token: 0x04000022 RID: 34
			private Action<KdTreeNode<TKey, TValue>> <addRight>5__3;
		}
	}
}
