using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Baza_wiazek_przyciskow_20240205;

// A click opens the document; dragging over its caption keeps native text selection.
public sealed class DocumentButton : Button
{
    private Point? pressPosition;
    private bool dragged;

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        pressPosition = e.GetPosition(this);
        dragged = false;
        base.OnPreviewMouseLeftButtonDown(e);
    }

    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        if (pressPosition is Point start && e.LeftButton == MouseButtonState.Pressed)
        {
            Point current = e.GetPosition(this);
            dragged |= Math.Abs(current.X - start.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                       Math.Abs(current.Y - start.Y) >= SystemParameters.MinimumVerticalDragDistance;
        }
        base.OnPreviewMouseMove(e);
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        Point current = e.GetPosition(this);
        bool open = pressPosition.HasValue && !dragged &&
                    new Rect(RenderSize).Contains(current);
        pressPosition = null;
        // The caption handles mouse input itself, so route a stationary release to the button.
        e.Handled = true;
        if (Mouse.Captured is UIElement captured) captured.ReleaseMouseCapture();
        if (open) OnClick();
        base.OnPreviewMouseLeftButtonUp(e);
    }
}
