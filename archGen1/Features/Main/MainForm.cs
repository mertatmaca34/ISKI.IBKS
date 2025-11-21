using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace archGen1.Features.Main
{
    public partial class MainForm : Form, IMainFormView
    {
        public event EventHandler? HomePageButtonClick;

        public MainForm()
        {
            InitializeComponent();

            HomePageButton.Click += (s, e) => HomePageButtonClick?.Invoke(s,e);
        }

        public void ShowMert()
        {
            throw new NotImplementedException();
        }
    }
}