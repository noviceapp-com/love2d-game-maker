using System;
using System.Drawing;
using System.Windows.Forms;

public class SizeableTreeView : Panel
{
    private const int cGripSize = 20;
    private bool mDragging;
    private Point mDragPos;

    public SizeableTreeView()
    {
        this.DoubleBuffered = true;
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.BackColor = Color.White;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
    }

    private bool IsOnGrip(Point pos)
    {
        return pos.X >= this.ClientSize.Width-4;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        mDragging = IsOnGrip(e.Location);
        mDragPos = e.Location;
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        mDragging = false;
        base.OnMouseUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (mDragging)
        {
            if ((this.Width + e.X - mDragPos.X) >= LGM.Main.CorrectDPIvalues(198, this.CreateGraphics().DpiX))
            {
                this.Size = new Size(this.Width + e.X - mDragPos.X, this.Height + e.Y - mDragPos.Y);
            }
            mDragPos = e.Location;
        }
        base.OnMouseMove(e);
    }
}