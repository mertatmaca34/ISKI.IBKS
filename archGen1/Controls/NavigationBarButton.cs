using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace archGen1.Controls
{
    public class NavigationBarButton : Button
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
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
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.White;
            this.ForeColor = Color.DimGray;
            this.Size = new Size(75, 68);
            this.Font = new Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            this.MouseEnter += NavigationBarButton_MouseEnter;
            this.MouseLeave += NavigationBarButton_MouseLeave;
            this.MouseDown += NavigationBarButton_MouseDown;
            this.MouseUp += NavigationBarButton_MouseUp;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, CornerRadius, CornerRadius));

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
