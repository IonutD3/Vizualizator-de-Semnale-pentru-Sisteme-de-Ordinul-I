namespace SignalVisualizer;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox gainTextBox = null!;
    private TextBox timeConstantTextBox = null!;
    private TextBox transferFunctionTextBox = null!;
    private Label gainLabel = null!;
    private Label timeConstantLabel = null!;
    private Label transferFunctionLabel = null!;
    private Button applyParametersButton = null!;
    private MenuStrip signalMenuStrip = null!;
    private ToolStripMenuItem impulseMenuItem = null!;
    private ToolStripMenuItem stepMenuItem = null!;
    private ToolStripMenuItem sineMenuItem = null!;
    private SignalPlotPanel graphPanel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        gainTextBox = new TextBox();
        timeConstantTextBox = new TextBox();
        transferFunctionTextBox = new TextBox();
        gainLabel = new Label();
        timeConstantLabel = new Label();
        transferFunctionLabel = new Label();
        applyParametersButton = new Button();
        signalMenuStrip = new MenuStrip();
        impulseMenuItem = new ToolStripMenuItem();
        stepMenuItem = new ToolStripMenuItem();
        sineMenuItem = new ToolStripMenuItem();
        graphPanel = new SignalPlotPanel();
        signalMenuStrip.SuspendLayout();
        SuspendLayout();

        gainTextBox.Location = new Point(20, 67);
        gainTextBox.Name = "gainTextBox";
        gainTextBox.Size = new Size(125, 27);
        gainTextBox.TabIndex = 0;
        gainTextBox.Text = "1";

        timeConstantTextBox.Location = new Point(20, 122);
        timeConstantTextBox.Name = "timeConstantTextBox";
        timeConstantTextBox.Size = new Size(125, 27);
        timeConstantTextBox.TabIndex = 1;
        timeConstantTextBox.Text = "1";

        transferFunctionTextBox.Location = new Point(178, 122);
        transferFunctionTextBox.Name = "transferFunctionTextBox";
        transferFunctionTextBox.ReadOnly = true;
        transferFunctionTextBox.Size = new Size(210, 27);
        transferFunctionTextBox.TabIndex = 2;

        gainLabel.AutoSize = true;
        gainLabel.Location = new Point(20, 44);
        gainLabel.Name = "gainLabel";
        gainLabel.Size = new Size(18, 20);
        gainLabel.TabIndex = 3;
        gainLabel.Text = "K";

        timeConstantLabel.AutoSize = true;
        timeConstantLabel.Location = new Point(20, 99);
        timeConstantLabel.Name = "timeConstantLabel";
        timeConstantLabel.Size = new Size(17, 20);
        timeConstantLabel.TabIndex = 4;
        timeConstantLabel.Text = "T";

        transferFunctionLabel.AutoSize = true;
        transferFunctionLabel.Location = new Point(178, 99);
        transferFunctionLabel.Name = "transferFunctionLabel";
        transferFunctionLabel.Size = new Size(19, 20);
        transferFunctionLabel.TabIndex = 5;
        transferFunctionLabel.Text = "H(s)";

        applyParametersButton.Location = new Point(178, 66);
        applyParametersButton.Name = "applyParametersButton";
        applyParametersButton.Size = new Size(210, 29);
        applyParametersButton.TabIndex = 6;
        applyParametersButton.Text = "Aplică parametrii";
        applyParametersButton.UseVisualStyleBackColor = true;
        applyParametersButton.Click += ApplyParametersButton_Click;

        signalMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            impulseMenuItem,
            stepMenuItem,
            sineMenuItem
        });
        signalMenuStrip.Location = new Point(0, 0);
        signalMenuStrip.Name = "signalMenuStrip";
        signalMenuStrip.Size = new Size(980, 28);
        signalMenuStrip.TabIndex = 7;

        impulseMenuItem.Name = "impulseMenuItem";
        impulseMenuItem.Size = new Size(65, 24);
        impulseMenuItem.Text = "Impuls";
        impulseMenuItem.Click += ImpulseMenuItem_Click;

        stepMenuItem.Name = "stepMenuItem";
        stepMenuItem.Size = new Size(68, 24);
        stepMenuItem.Text = "Treaptă";
        stepMenuItem.Click += StepMenuItem_Click;

        sineMenuItem.Name = "sineMenuItem";
        sineMenuItem.Size = new Size(84, 24);
        sineMenuItem.Text = "Sinusoidă";
        sineMenuItem.Click += SineMenuItem_Click;

        graphPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        graphPanel.BackColor = Color.WhiteSmoke;
        graphPanel.Location = new Point(20, 170);
        graphPanel.Name = "graphPanel";
        graphPanel.Size = new Size(940, 250);
        graphPanel.TabIndex = 8;
        graphPanel.Paint += GraphPanel_Paint;

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(980, 450);
        Controls.Add(graphPanel);
        Controls.Add(transferFunctionLabel);
        Controls.Add(transferFunctionTextBox);
        Controls.Add(applyParametersButton);
        Controls.Add(timeConstantLabel);
        Controls.Add(gainLabel);
        Controls.Add(timeConstantTextBox);
        Controls.Add(gainTextBox);
        Controls.Add(signalMenuStrip);
        MainMenuStrip = signalMenuStrip;
        MinimumSize = new Size(700, 400);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Signal Visualizer";
        signalMenuStrip.ResumeLayout(false);
        signalMenuStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}

internal sealed class SignalPlotPanel : Panel
{
    public SignalPlotPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
}
