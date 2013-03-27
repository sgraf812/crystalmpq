#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	/// <summary>A control used to display the children of a <see cref="System.Windows.Forms.TreeNode"/> used to represent directories in a <see cref="System.Windows.Forms.ListView"/>.</summary>
	/// <remarks>This control integrates tightly with the <see cref="MainForm"/> and is not supposed to be used from anywhere else.</remarks>
	internal sealed partial class DirectoryViewer : FileViewer
	{
		/// <summary>The <see cref="MainForm"/> owning this <see cref="DirectoryViewer"/>.</summary>
		private MainForm mainForm;
		/// <summary>The node whose children are actually displayed by this control.</summary>
		private TreeNode rootNode;
		/// <summary>An item cache holding items for the virtual <see cref="System.Windows.Forms.ListView"/>.</summary>
		private ListViewItem[] itemCache;
		/// <summary>The index of the last item stored in the cache.</summary>
		/// <remarks>This value may not be higher than the item count or the cache size, but it can be lower.</remarks>
		private int cacheLastItemIndex;

		/// <summary>Initializes a new instance of the <see cref="DirectoryViewer"/> class.</summary>
		/// <param name="mainForm">The main form.</param>
		/// <param name="host">The host object.</param>
		public DirectoryViewer(MainForm mainForm, IHost host)
			: base(host)
		{
			this.mainForm = mainForm;
			InitializeComponent();
			listView.SmallImageList = mainForm.file16ImageList;
			listView.LargeImageList = mainForm.file32ImageList;
		}

		/// <summary>Gets the <see cref="System.Windows.Forms.MenuStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public override MenuStrip Menu { get { return menuStrip; } }
		/// <summary>Gets the <see cref="System.Windows.Forms.ToolStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public override ToolStrip MainToolStrip { get { return mainToolStrip; } }
		/// <summary>Gets the <see cref="System.Windows.Forms.StatusStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public override StatusStrip StatusStrip { get { return statusStrip; } }

		/// <summary>Gets or sets the root node whose items should be displayed.</summary>
		/// <value>The root node.</value>
		public TreeNode RootNode
		{
			get
			{
				return rootNode;
			}
			set
			{
				if (value != rootNode)
				{
					rootNode = value;
					UpdateView();
				}
			}
		}

		/// <summary>Updates the view.</summary>
		/// <remarks>This will be called when the viewed directory has changed.</remarks>
		private void UpdateView()
		{
			// The list view will work in virtual mode from now on, as preloading it with items proved to be really slow…

			listView.BeginUpdate();

			listView.VirtualListSize = 0;

			// Instead of directly filling the list view, we can use an item cache big enough to hold all items.
			// Items will be allocated and cached on the fly. Nothing is actually cached here.
			if (itemCache == null || itemCache.Length < rootNode.Nodes.Count)
				itemCache = new ListViewItem[rootNode.Nodes.Count];
			// If the cache is already big enough, we just have to clear it.
			else for (int i = 0; i < itemCache.Length; i++) itemCache[i] = null;

			listView.VirtualListSize = rootNode.Nodes.Count;

			listView.EndUpdate();
		}

		/// <summary>Creates a <see cref="System.Windows.Forms.ListViewItem"/> for the corresponding <see cref="System.Windows.Forms.TreeNode"/>.</summary>
		/// <param name="node">The reference node.</param>
		/// <returns></returns>
		private ListViewItem MakeItem(TreeNode node)
		{
			var lvi = new ListViewItem();

			lvi.Text = node.Text;
			lvi.ImageIndex = Math.Max(0, node.ImageIndex);
			lvi.Tag = node;

			return lvi;
		}

		/// <summary>Handles the ItemActivate event of the listView control.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void listView_ItemActivate(object sender, EventArgs e)
		{
			var node = itemCache[listView.SelectedIndices[0]].Tag as TreeNode;

			node.TreeView.SelectedNode = node;
		}

		/// <summary>Handles the RetrieveVirtualItem event of the listView control.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.RetrieveVirtualItemEventArgs"/> instance containing the event data.</param>
		private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if ((e.Item = itemCache[e.ItemIndex]) == null)
				e.Item = itemCache[e.ItemIndex] = MakeItem(rootNode.Nodes[e.ItemIndex]);
		}

		/// <summary>Handles the CacheVirtualItems event of the listView control.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.CacheVirtualItemsEventArgs"/> instance containing the event data.</param>
		private void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
		{
			for (int i = e.StartIndex; i < e.EndIndex; i++)
				if (itemCache[i] == null)
					itemCache[i] = MakeItem(rootNode.Nodes[i]);
		}

		/// <summary>Handles the SearchForVirtualItem event of the listView control.</summary>
		/// <remarks>This will be used for keyboard search, which is an important UI feature.</remarks>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.SearchForVirtualItemEventArgs"/> instance containing the event data.</param>
		private void listView_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
		{
			// Note 1: The IsPrefix property of SearchForVirtualItemEventArgs seems to be false for keyboard prefix search events…
			// Since this is the only use we'll have of this method, we can assume this is always a prefix search.
			// Note 2: We'll assume that we are always searching forward. (This is important for "proximity" search)

			if (rootNode == null) return;

			// Since we are processing filenames, the invariant culture will be used here.
			var nodes = rootNode.Nodes;
			// Assuming the nodes re sorted alphabetically (they should be !), the binary search is the most efficient choice.
			int prefixLength = e.Text.Length;
			int low, high; // These will represent the dynamic search range.
			int difference;

			// Match the prefix against the start item.
			difference = string.Compare(nodes[e.StartIndex].Text, 0, e.Text, 0, prefixLength, StringComparison.OrdinalIgnoreCase);

			// Depending on the prefix comparison, we can either adjust search parameters or even terminate the search. ;)
			if (difference == 0)
			{
				// If the test is successful, then the start item was the item we were looking for !
				e.Index = e.StartIndex;
				return;
			}
			else if (difference > 0)
			{
				// If prefix may only be found before the start item, search before
				low = 0;
				if (e.StartIndex > 0) high = e.StartIndex - 1;
				else return; // If start item was the first item, then the search failed.
			}
			else
			{
				// If prefix may only be found after the start item, search after
				if (e.StartIndex + 1 < nodes.Count) low = e.StartIndex + 1;
				else return; // If start item was the last item, then the search failed.
				high = nodes.Count - 1;
			}

			// In this algorithm, we may have low == high, but this is a normal (final) case.
			do
			{
				int pivot = (low + high) / 2;
				string text = nodes[pivot].Text;

				difference = string.Compare(text, 0, e.Text, 0, Math.Min(prefixLength, text.Length), StringComparison.OrdinalIgnoreCase);

				if (difference == 0)
				{
					// Once a match is found, adjust the search parameters accordingly
					high = (e.Index = pivot) - 1;
					if (text.Length == prefixLength) return; // If the full string was matched by the "prefix", we have our final result
				}
				else if (difference > 0) { if ((high = pivot - 1) < 0) return; }
				else if ((low = pivot + 1) == nodes.Count) return;
			} while (low <= high);
		}
	}
}
