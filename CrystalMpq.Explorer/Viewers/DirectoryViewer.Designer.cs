#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.Explorer.Viewers
{
	partial class DirectoryViewer
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.listView = new CrystalMpq.Explorer.EnhancedListView();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(150, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Visible = false;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(150, 25);
			this.mainToolStrip.TabIndex = 2;
			this.mainToolStrip.Visible = false;
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 128);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(150, 22);
			this.statusStrip.TabIndex = 3;
			this.statusStrip.Visible = false;
			// 
			// listView
			// 
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.ExplorerStyle = true;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(150, 150);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.VirtualMode = true;
			this.listView.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listView_CacheVirtualItems);
			this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
			this.listView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView_RetrieveVirtualItem);
			this.listView.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.listView_SearchForVirtualItem);
			// 
			// DirectoryViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.menuStrip);
			this.Name = "DirectoryViewer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private EnhancedListView listView;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
	}
}
