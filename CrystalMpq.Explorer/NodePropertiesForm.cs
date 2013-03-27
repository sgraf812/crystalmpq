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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace CrystalMpq.Explorer
{
	internal sealed partial class NodePropertiesForm : Form
	{
		private MainForm mainForm;
		private TreeNode node;

		public NodePropertiesForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		public TreeNode Node
		{
			get { return node; }
			set
			{
				node = value;
				UpdateNodeInformation();
			}
		}

		private void EnableCheckBoxes(bool enable)
		{
			encryptedCheckBox.Enabled = enable;
			adjustedKeyCheckBox.Enabled = enable;
			singleUnitCheckBox.Enabled = enable;
			notCompressedRadioButton.Enabled = enable;
			dclCompressedRadioButton.Enabled = enable;
			multiCompressedRadioButton.Enabled = enable;
		}

		private void UpdateNodeInformation()
		{
			long size, compressedSize;

			GetNodeInfo(node, out size, out compressedSize);
			expandedSizeLabel.Text = Program.FormatFileSize(size);
			compressedSizeLabel.Text = Program.FormatFileSize(compressedSize);
			iconPictureBox.Image = node != null ? mainForm.file32ImageList.Images[Math.Max(node.ImageIndex, 0)] : null;
			if (node != null && node.Tag is MpqFile)
			{
				MpqFile mpqFile = (MpqFile)node.Tag;

				fileNameLabel.Text = mpqFile.Name;

				EnableCheckBoxes(true);

				if ((mpqFile.Flags & MpqFileFlags.Encrypted) != 0)
				{
					encryptedCheckBox.Checked = true;
					adjustedKeyCheckBox.Checked = (mpqFile.Flags & MpqFileFlags.PositionEncrypted) != 0;
					adjustedKeyCheckBox.Enabled = adjustedKeyCheckBox.Checked;
				}
				else
				{
					encryptedCheckBox.Checked = false;
					adjustedKeyCheckBox.Checked = false;
					adjustedKeyCheckBox.Enabled = false;
				}

				singleUnitCheckBox.Checked = ((mpqFile.Flags & MpqFileFlags.SingleBlock) != 0);

				if ((mpqFile.Flags & MpqFileFlags.DclCompressed) != 0)
				{
					dclCompressedRadioButton.Checked = true;
					multiCompressedRadioButton.Checked = false;
					notCompressedRadioButton.Checked = false;
				}
				else if ((mpqFile.Flags & MpqFileFlags.MultiCompressed) != 0)
				{
					dclCompressedRadioButton.Checked = false;
					multiCompressedRadioButton.Checked = true;
					notCompressedRadioButton.Checked = false;
				}
				else
				{
					dclCompressedRadioButton.Checked = false;
					multiCompressedRadioButton.Checked = false;
					notCompressedRadioButton.Checked = true;
				}

				patchCheckBox.Checked = (mpqFile.Flags & MpqFileFlags.Patch) != 0;
			}
			else
			{
				EnableCheckBoxes(false);
				fileNameLabel.Text = node.FullPath;
				encryptedCheckBox.Checked = false;
				adjustedKeyCheckBox.Checked = false;
				singleUnitCheckBox.Checked = false;
				dclCompressedRadioButton.Checked = false;
				multiCompressedRadioButton.Checked = false;
				notCompressedRadioButton.Checked = true;
			}
		}

		private void GetNodeInfo(TreeNode node, out long size, out long compressedSize)
		{
			size = 0;
			compressedSize = 0;

			if (node == null)
			{
				size = 0;
				compressedSize = 0;
			}
			else if (node.Tag == null || !(node.Tag is MpqFile))
			{
				size = 0;
				compressedSize = 0;
				foreach (TreeNode childNode in node.Nodes)
				{
					long childSize, childCompressedSize;
					GetNodeInfo(childNode, out childSize, out childCompressedSize);
					size += childSize;
					compressedSize += childCompressedSize;
				}
			}
			else
			{
				MpqFile mpqFile = (MpqFile)node.Tag;

				size = mpqFile.Size;
				compressedSize = mpqFile.CompressedSize;
			}
		}
	}
}