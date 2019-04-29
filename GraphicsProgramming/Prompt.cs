using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

public static class Prompt
{
    public static double[] ShowDialog(string title)
    {
        double[] transformCoordinates = new double[] { 0, 0, 0 };
        Form prompt = new Form()
        {
            Width = 365,
            Height = 320,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterScreen
        };
        Label xMoveLabel = new Label() { Left = 15, Top = 20, Text = "X Transform:" };
        TextBox xMoveInput = new TextBox() { Left = 20, Top = 50, Width = 300 };

        Label yMoveLabel = new Label() { Left = 15, Top = 100, Text = "Y Transform:" };
        TextBox yMoveInput = new TextBox() { Left = 15, Top = 130, Width = 300 };

        Label rotateLabel = new Label() { Left = 15, Top = 180, Text = "Rotate shape:" };
        TextBox rotateInput = new TextBox() { Left = 15, Top = 210, Width = 300 };

        Button confirmation = new Button() { Text = "Ok", Left = 125, Width = 100, Top = 240, DialogResult = DialogResult.OK };
        confirmation.Click += (sender, e) => { prompt.Close(); };

        prompt.Controls.Add(xMoveLabel);
        prompt.Controls.Add(xMoveInput);
        
        prompt.Controls.Add(yMoveLabel);
        prompt.Controls.Add(yMoveInput);

        prompt.Controls.Add(rotateLabel);
        prompt.Controls.Add(rotateInput);

        prompt.AcceptButton = confirmation;
        prompt.Controls.Add(confirmation);
        
        if(prompt.ShowDialog() == DialogResult.OK)
        {
            if (xMoveInput.Text.Length > 0)
            {
                try { transformCoordinates[0] = double.Parse(xMoveInput.Text); }
                catch (FormatException) { throw; }
            }

            if (yMoveInput.Text.Length > 0)
            {
                try { transformCoordinates[1] = double.Parse(yMoveInput.Text); }
                catch (FormatException) { throw; }
            }

            if (rotateInput.Text.Length > 0)
            {
                try { transformCoordinates[2] = double.Parse(rotateInput.Text); }
                catch (FormatException) { throw; }
            }

            return transformCoordinates;
        }
        else
        {
            return transformCoordinates;
        }
    }
}