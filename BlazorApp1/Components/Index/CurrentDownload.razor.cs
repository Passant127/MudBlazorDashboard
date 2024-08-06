using MudBlazor;
using MudBlazor.Utilities;
using BlazorApp1.Models.Charts;
using BlazorApp1.Models.Charts.Chart;
using BlazorApp1.Models.Charts.DataLabels;
using BlazorApp1.Models.Charts.Legend;
using BlazorApp1.Models.Charts.PlotOptions;
using BlazorApp1.Models.Charts.Stroke;
using BlazorApp1.Models.Charts.Tooltip;

namespace BlazorApp1.Components.Index;

public partial class CurrentDownload : MudComponentBase
{
    private ChartOptionsModel<int, int>? _chartOptions = null;

    private string Classname =>
        new CssBuilder()
            .AddClass(Class)
            .Build();

    protected override void OnInitialized()
    {
        _chartOptions = new ChartOptionsModel<int, int>
        {
            Chart = new ChartModel
            {
                Type = ChartTypes.Donut,
                Toolbar = new ChartModel.ToolbarModel
                {
                    Show = false
                },
                Width = "100%",
                Height = "440px",
                Id = "currentDownload"
            },
            Series = new List<int> {297, 214, 113, 54},
            Tooltip = new TooltipModel
            {
                Enabled = false
            },
            Labels = new List<string> {"Android", "IOS", "Mac", "Windows"},
            Stroke = new StrokeModel
            {
                Show = false
            },
            DataLabels = new DataLabelsModel
            {
                Enabled = false
            },
            Legend = new LegendModel
            {
                ItemMargin = new LegendModel.ItemMarginModel
                {
                    Vertical = 60
                }
            },
            PlotOptions = new PlotOptionsModel
            {
                Pie = new PlotOptionsModel.PieModel
                {
                    Donut = new PlotOptionsModel.PieModel.DonutModel
                    {
                        Size = "93%",
                        Labels = new PlotOptionsModel.PieModel.DonutModel.LabelsModel
                        {
                            Show = true,
                            Total = new PlotOptionsModel.PieModel.DonutModel.LabelsModel.TotalModel
                            {
                                Show = true
                            }
                        }
                    }
                }
            },
            Colors = new List<string>
            {
                "rgba(var(--mud-palette-primary-rgb) , 1)", "rgba(var(--mud-palette-primary-rgb) , 0.8)",
                "rgba(var(--mud-palette-primary-rgb) , 0.6)", "rgba(var(--mud-palette-primary-rgb) , 0.4)"
            }
        };
    }
}