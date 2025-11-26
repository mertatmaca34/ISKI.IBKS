using ISKI.IBKS.Presentation.WinForms.Features.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main
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