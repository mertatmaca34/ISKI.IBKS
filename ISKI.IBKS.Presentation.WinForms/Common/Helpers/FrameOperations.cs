namespace ISKI.IBKS.Presentation.WinForms.Common.Helpers;

public static class FrameOperations
{
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

