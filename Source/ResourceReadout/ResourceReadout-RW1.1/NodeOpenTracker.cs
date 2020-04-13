using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace ResourceReadout
{
	public class NodeOpenTracker : IExposable, IEnumerable<string>, ICollection<string>
	{
		private HashSet<string> set = new HashSet<string>();

		public int Count => ((ICollection<string>)set).Count;

		public bool IsReadOnly => ((ICollection<string>)set).IsReadOnly;

		public bool Contains(TreeNode_ThingCategory node)
		{
			return set.Contains(node.Label);
		}

		public void Add(TreeNode_ThingCategory node)
		{
			set.Add(node.Label);
		}

		public void ExposeData()
		{
			Scribe_Collections.Look(ref set, "set", LookMode.Value);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return ((IEnumerable<string>)set).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<string>)set).GetEnumerator();
		}

		public void Add(string item)
		{
			((ICollection<string>)set).Add(item);
		}

		public void Clear()
		{
			((ICollection<string>)set).Clear();
		}

		public bool Contains(string item)
		{
			return ((ICollection<string>)set).Contains(item);
		}

		public void CopyTo(string[] array, int arrayIndex)
		{
			((ICollection<string>)set).CopyTo(array, arrayIndex);
		}

		public bool Remove(string item)
		{
			return ((ICollection<string>)set).Remove(item);
		}
	}
}
