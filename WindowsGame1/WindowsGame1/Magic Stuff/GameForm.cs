#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Windows.Forms;
#endregion

namespace WindowsGame1
{
    public partial class GameForm : Form
    {
        static public GameForm instance;
        private SplitContainer splitContainer;

        public GameForm()
        {
            instance = this;
            InitializeComponent();
            splitContainer = splitContainer1;
        }

        public SplitContainer SplitContainer
        {
            get { return splitContainer; }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
