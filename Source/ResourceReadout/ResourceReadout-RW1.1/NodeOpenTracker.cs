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
	public class NodeOpenTracker : IExposable, IEnumerable<string>
	{
		private HashSet<string> set = new HashSet<string>();

		public bool Contains(TreeNode_ThingCategory node)
		{
			return set.Contains(node.Label);
		}

		public void Add(TreeNode_ThingCategory node)
		{
			set.Add(node.Label);
		}

		public void Remove(TreeNode_ThingCategory node)
		{
			set.Remove(node.Label);
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
	}
}
