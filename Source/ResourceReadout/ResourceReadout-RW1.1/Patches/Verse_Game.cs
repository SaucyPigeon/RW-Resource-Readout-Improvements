using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;

namespace ResourceReadout.Patches
{
	[HarmonyPatch(typeof(Game))]
	public static class Verse_Game
	{
		[HarmonyPatch("ExposeSmallComponents")]
		public static void Prefix()
		{
#if DEBUG
			Log.Message("Saving NodeOpenTracker.");
#endif
			const bool saveDestroyedThings = false;
			Scribe_Deep.Look<NodeOpenTracker>(ref Loader.NodeOpenTracker, saveDestroyedThings, "nodeOpenTracker", new object[0]);
		}
	}
}
