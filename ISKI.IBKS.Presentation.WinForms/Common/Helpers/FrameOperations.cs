namespace ISKI.IBKS.Presentation.WinForms.Common.Helpers;

/// <summary>
/// Helper class for managing frame-based animations in the UI.
/// Used for alternating between bitmap frames for visual effects.
/// </summary>
public static class FrameOperations
{
    /// <summary>
    /// Changes a panel's background image if it's different from the current one.
    /// </summary>
    public static void ChangePanelFrame(Panel panel, Bitmap nextFrame)
    {
        if (panel.BackgroundImage == nextFrame)
        {
            return;
        }
        else
        {
            panel.BackgroundImage = nextFrame;
        }
    }

    /// <summary>
    /// Changes a PictureBox's image based on pump state.
    /// </summary>
    public static void ChangePictureBoxFrame(PictureBox pictureBox, Bitmap animation, Bitmap idle, PumpState pumpState)
    {
        if (pictureBox.Image == animation && pumpState == PumpState.Working)
        {
            return;
        }
        else if (pictureBox.Image == animation && pumpState == PumpState.Idle)
        {
            pictureBox.Image = idle;
        }
        else if (pictureBox.Image == idle && pumpState == PumpState.Idle)
        {
            return;
        }
        else if (pictureBox.Image == idle && pumpState == PumpState.Working)
        {
            pictureBox.Image = animation;
        }
    }

    /// <summary>
    /// Alternates a UserControl's background image between two frames.
    /// </summary>
    public static void ChangeControlFrame(UserControl control, Bitmap frame1, Bitmap frame2)
    {
        if (control.BackgroundImage == frame1)
        {
            control.BackgroundImage = frame2;
        }
        else
        {
            control.BackgroundImage = frame1;
        }
    }
}

/// <summary>
/// Pump state enumeration for animation control.
/// </summary>
public enum PumpState
{
    Idle,
    Working
}
