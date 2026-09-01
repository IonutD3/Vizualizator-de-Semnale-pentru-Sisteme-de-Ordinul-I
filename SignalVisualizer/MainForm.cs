using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SignalVisualizer;

public partial class MainForm : Form
{
    private enum SignalType
    {
        Impulse,
        Step,
        Sine
    }

    private SignalType selectedSignal = SignalType.Step;
    private double gain = 1;
    private double timeConstant = 1;

    public MainForm()
    {
        InitializeComponent();
        UpdateTransferFunction();
        graphPanel.Invalidate();
    }

    private void ApplyParameters()
    {
        // Validează parametrii înainte de a-i folosi în calcule.
        if (!double.TryParse(gainTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedK) ||
            !double.TryParse(timeConstantTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedT) ||
            parsedT <= 0)
        {
            MessageBox.Show(
                "Introdu valori numerice valide. K poate fi orice număr, iar T trebuie să fie mai mare decât 0.",
                "Date invalide",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        gain = parsedK;
        timeConstant = parsedT;
        UpdateTransferFunction();
        graphPanel.Invalidate();
    }

    private void UpdateTransferFunction()
    {
        // Afișează funcția de transfer folosind valorile curente K și T.
        transferFunctionTextBox.Text = $"{FormatNumber(gain)} / ({FormatNumber(timeConstant)}s + 1)";
    }

    private void SelectSignal(SignalType signalType)
    {
        // Reține tipul de semnal selectat și redesenează graficul.
        selectedSignal = signalType;
        graphPanel.Invalidate();
    }

    private void ApplyParametersButton_Click(object sender, EventArgs e)
    {
        ApplyParameters();
    }

    private void ImpulseMenuItem_Click(object sender, EventArgs e)
    {
        SelectSignal(SignalType.Impulse);
    }

    private void StepMenuItem_Click(object sender, EventArgs e)
    {
        SelectSignal(SignalType.Step);
    }

    private void SineMenuItem_Click(object sender, EventArgs e)
    {
        SelectSignal(SignalType.Sine);
    }

    private void GraphPanel_Paint(object sender, PaintEventArgs e)
    {
        // Desenează axele, grila și răspunsul sistemului pe baza parametrilor curenți.
        DrawGraph(e.Graphics, graphPanel.ClientRectangle);
    }

    private void DrawGraph(Graphics graphics, Rectangle bounds)
    {
        graphics.Clear(Color.WhiteSmoke);

        if (bounds.Width < 100 || bounds.Height < 100)
        {
            return;
        }

        const int leftMargin = 55;
        const int rightMargin = 20;
        const int topMargin = 45;
        const int bottomMargin = 40;

        var plot = new Rectangle(
            leftMargin,
            topMargin,
            bounds.Width - leftMargin - rightMargin,
            bounds.Height - topMargin - bottomMargin);

        using var axisPen = new Pen(Color.DimGray, 1.5f);
        using var gridPen = new Pen(Color.Gainsboro, 1f);
        using var curvePen = new Pen(Color.RoyalBlue, 2.5f);
        using var textBrush = new SolidBrush(Color.DimGray);
        using var titleFont = new Font(Font.FontFamily, 10, FontStyle.Bold);
        using var labelFont = new Font(Font.FontFamily, 9);

        DrawGrid(graphics, plot, gridPen);
        DrawAxes(graphics, plot, axisPen);

        var duration = Math.Max(timeConstant * 8, 10);
        var samples = Math.Max(300, plot.Width * 2);
        var values = new PointF[samples];
        var minY = double.MaxValue;
        var maxY = double.MinValue;

        for (var i = 0; i < samples; i++)
        {
            var t = duration * i / (samples - 1.0);
            var y = CalculateResponse(t);
            values[i] = new PointF((float)t, (float)y);
            minY = Math.Min(minY, y);
            maxY = Math.Max(maxY, y);
        }

        var amplitude = Math.Max(Math.Abs(minY), Math.Abs(maxY));
        if (amplitude < 0.1)
        {
            amplitude = 1;
        }

        var padding = Math.Max(amplitude * 0.15, 0.1);
        minY = -amplitude - padding;
        maxY = amplitude + padding;

        for (var i = 0; i < values.Length; i++)
        {
            var x = plot.Left + values[i].X / duration * plot.Width;
            var y = plot.Bottom - (float)((values[i].Y - minY) / (maxY - minY) * plot.Height);
            values[i] = new PointF(x, y);
        }

        graphics.DrawLines(curvePen, values);

        var signalName = selectedSignal switch
        {
            SignalType.Impulse => "Răspuns la impuls",
            SignalType.Step => "Răspuns la treaptă",
            _ => "Răspuns la sinusoidă"
        };

        graphics.DrawString(signalName, titleFont, textBrush, plot.Left, 12);
        graphics.DrawString($"K = {FormatNumber(gain)}, T = {FormatNumber(timeConstant)}", labelFont, textBrush, plot.Right - 180, 14);
        graphics.DrawString("t", labelFont, textBrush, plot.Right - 5, plot.Bottom + 8);
        graphics.DrawString("y(t)", labelFont, textBrush, 8, plot.Top - 10);
    }

    private static void DrawGrid(Graphics graphics, Rectangle plot, Pen gridPen)
    {
        // Desenează o grilă discretă pentru citirea mai ușoară a graficului.
        const int divisions = 8;

        for (var i = 0; i <= divisions; i++)
        {
            var x = plot.Left + plot.Width * i / (float)divisions;
            var y = plot.Top + plot.Height * i / (float)divisions;
            graphics.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
            graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
        }
    }

    private static void DrawAxes(Graphics graphics, Rectangle plot, Pen axisPen)
    {
        // Desenează axele principale ale sistemului de coordonate.
        graphics.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
        graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);
    }

    private double CalculateResponse(double t)
    {
        // Calculează răspunsul la semnalul selectat pentru sistemul H(s) = K / (Ts + 1).
        return selectedSignal switch
        {
            SignalType.Impulse => gain / timeConstant * Math.Exp(-t / timeConstant),
            SignalType.Step => gain * (1 - Math.Exp(-t / timeConstant)),
            _ => CalculateSineResponse(t)
        };
    }

    private double CalculateSineResponse(double t)
    {
        // Calculează răspunsul la o sinusoidă cu frecvența unghiulară de 1 rad/s.
        const double omega = 1.0;
        var denominator = 1 + Math.Pow(omega * timeConstant, 2);
        var transient = omega * timeConstant * Math.Exp(-t / timeConstant);
        return gain / denominator * (Math.Sin(omega * t) - omega * timeConstant * Math.Cos(omega * t) + transient);
    }

    private static string FormatNumber(double value)
    {
        // Formatează valorile fără zerouri inutile și păstrează separatorul zecimal standard.
        return value.ToString("0.###", CultureInfo.InvariantCulture);
    }
}
