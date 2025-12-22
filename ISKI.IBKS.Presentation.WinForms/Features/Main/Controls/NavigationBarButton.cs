using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main.Controls
{
    public class NavigationBarButton : Button
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern nint CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public new Color DefaultBackColor = Color.White;
        public Color HoverBackColor = Color.FromArgb(230, 230, 230);
        public Color MouseDownColor = Color.FromArgb(210, 210, 210);

        public int CornerRadius { get; set; } = 8;

        public NavigationBarButton()
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.White;
            ForeColor = Color.DimGray;
            Size = new Size(75, 68);
            Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            MouseEnter += NavigationBarButton_MouseEnter;
            MouseLeave += NavigationBarButton_MouseLeave;
            MouseDown += NavigationBarButton_MouseDown;
            MouseUp += NavigationBarButton_MouseUp;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            Invalidate();
        }

        private void NavigationBarButton_MouseUp(object? sender, MouseEventArgs e)
        {
            SetColor(DefaultBackColor);
        }

        private void NavigationBarButton_MouseDown(object? sender, MouseEventArgs e)
        {
            SetColor(MouseDownColor);
        }

        private void NavigationBarButton_MouseLeave(object? sender, EventArgs e)
        {
            SetColor(DefaultBackColor);
        }

        private void NavigationBarButton_MouseEnter(object? sender, EventArgs e)
        {
            SetColor(HoverBackColor);
        }

        public void SetColor(Color color)
        {
            BackColor = color;
        }
    }
}
