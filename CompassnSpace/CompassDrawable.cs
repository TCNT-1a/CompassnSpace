using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Reflection;
//using UIKit;


public class CompassDrawable : IDrawable
{
    public float Heading { get; set; }




    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        // Draw compass circle
        float centerX = dirtyRect.Center.X;
        float centerY = dirtyRect.Center.Y;
        float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2;
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawCircle(centerX, centerY, radius);

        // Draw 36 lines (10 degrees each)
        for (int i = 0; i < 36; i++)
        {
            float angle = i * 10;
            float x1 = centerX + (float)(radius * Math.Cos(Math.PI * angle / 180));
            float y1 = centerY + (float)(radius * Math.Sin(Math.PI * angle / 180));
            float x2 = centerX + (float)((radius - 10) * Math.Cos(Math.PI * angle / 180));
            float y2 = centerY + (float)((radius - 10) * Math.Sin(Math.PI * angle / 180));
            canvas.DrawLine(x1, y1, x2, y2);
        }

        // Draw labels
        DrawLabel(canvas, "N", centerX, centerY - radius + 20);
        DrawLabel(canvas, "E", centerX + radius - 20, centerY);
        DrawLabel(canvas, "S", centerX, centerY + radius - 20);
        DrawLabel(canvas, "W", centerX - radius + 20, centerY);

        // Draw compass needle
        canvas.Rotate(Heading, centerX, centerY);
        //canvas.DrawImage(_needleImage, centerX - _needleImage.Width / 2, centerY - _needleImage.Height / 2, _needleImage.Width, _needleImage.Height);
        canvas.SaveState();
        canvas.Rotate(Heading, centerX, centerY);
        DrawNeedle(canvas, centerX, centerY, radius);
        canvas.RestoreState();


        canvas.RestoreState();
    }

    private void DrawLabel(ICanvas canvas, string text, float x, float y)
    {
        canvas.FontColor = Colors.Black;
        canvas.FontSize = 20;
        canvas.DrawString(text, x, y, HorizontalAlignment.Center);
    }
    private void DrawNeedle(ICanvas canvas, float centerX, float centerY, float radius)
    {
        float needleLength = radius - 20;
        float needleWidth = 10;

        // Create path for the needle pointing north
        var northNeedlePath = new PathF();
        northNeedlePath.MoveTo(centerX, centerY - needleLength);
        northNeedlePath.LineTo(centerX - needleWidth / 2, centerY);
        northNeedlePath.LineTo(centerX + needleWidth / 2, centerY);
        northNeedlePath.Close();

        // Draw the north needle
        canvas.FillColor = Colors.Red;
        canvas.FillPath(northNeedlePath);

        // Create path for the needle pointing south
        var southNeedlePath = new PathF();
        southNeedlePath.MoveTo(centerX, centerY + needleLength);
        southNeedlePath.LineTo(centerX - needleWidth / 2, centerY);
        southNeedlePath.LineTo(centerX + needleWidth / 2, centerY);
        southNeedlePath.Close();

        // Draw the south needle
        canvas.FillColor = Colors.Blue;
        canvas.FillPath(southNeedlePath);
    }
   
}