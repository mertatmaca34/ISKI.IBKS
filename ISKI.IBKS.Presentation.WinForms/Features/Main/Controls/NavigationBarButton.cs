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

        // Default (inactive) colors
        public Color InactiveBackColor { get; set; } = Color.White;
        public Color InactiveForeColor { get; set; } = Color.DimGray;
        public Color HoverBackColor { get; set; } = Color.FromArgb(230, 230, 230);
        public Color MouseDownColor { get; set; } = Color.FromArgb(210, 210, 210);

        // Active (selected) colors
        public Color ActiveBackColor { get; set; } = Color.FromArgb(230, 240, 255);
        public Color ActiveForeColor { get; set; } = Color.FromArgb(0, 102, 204);
        public Color ActiveHoverBackColor { get; set; } = Color.FromArgb(210, 230, 255);

        private bool _isActive;

        /// <summary>
        /// Gets or sets whether this button is currently active/selected.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                ApplyCurrentStyle();
            }
        }

        public int CornerRadius { get; set; } = 8;

        public NavigationBarButton()
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = InactiveBackColor;
            ForeColor = InactiveForeColor;
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
            ApplyCurrentStyle();
        }

        private void NavigationBarButton_MouseDown(object? sender, MouseEventArgs e)
        {
            BackColor = MouseDownColor;
        }

        private void NavigationBarButton_MouseLeave(object? sender, EventArgs e)
        {
            ApplyCurrentStyle();
        }

        private void NavigationBarButton_MouseEnter(object? sender, EventArgs e)
        {
            BackColor = _isActive ? ActiveHoverBackColor : HoverBackColor;
        }

        /// <summary>
        /// Applies the appropriate style based on active state.
        /// </summary>
        private void ApplyCurrentStyle()
        {
            if (_isActive)
            {
                BackColor = ActiveBackColor;
                ForeColor = ActiveForeColor;
            }
            else
            {
                BackColor = InactiveBackColor;
                ForeColor = InactiveForeColor;
            }
        }
    }
}

