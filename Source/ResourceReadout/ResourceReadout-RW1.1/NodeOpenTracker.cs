using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace ResourceReadout
{
	public class NodeOpenTracker : IExposable
	{
		private Dictionary<string, bool> nodeIsOpen = new Dictionary<string, bool>();

		public void SetNode(TreeNode_ThingCategory node, int openMask)
		{
			nodeIsOpen[node.Label] = node.IsOpen(openMask);
		}

		public bool ContainsNode(TreeNode_ThingCategory node)
		{
			return nodeIsOpen.ContainsKey(node.Label);
		}

		public bool GetValue(TreeNode_ThingCategory node)
		{
			return nodeIsOpen[node.Label];
		}

		public void ExposeData()
		{
			Scribe_Collections.Look(ref nodeIsOpen, "nodeIsOpen", LookMode.Value, LookMode.Value);
		}
	}
}
