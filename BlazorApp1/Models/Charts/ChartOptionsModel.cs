using System.Text.Json.Serialization;
using BlazorApp1.Models.Charts.Annotations;
using BlazorApp1.Models.Charts.Chart;
using BlazorApp1.Models.Charts.DataLabels;
using BlazorApp1.Models.Charts.Fill;
using BlazorApp1.Models.Charts.ForecastDataPoints;
using BlazorApp1.Models.Charts.Grid;
using BlazorApp1.Models.Charts.Legend;
using BlazorApp1.Models.Charts.NoData;
using BlazorApp1.Models.Charts.PlotOptions;
using BlazorApp1.Models.Charts.Responsive;
using BlazorApp1.Models.Charts.Series;
using BlazorApp1.Models.Charts.States;
using BlazorApp1.Models.Charts.Stroke;
using BlazorApp1.Models.Charts.Subtitle;
using BlazorApp1.Models.Charts.Theme;
using BlazorApp1.Models.Charts.Title;
using BlazorApp1.Models.Charts.Tooltip;
using BlazorApp1.Models.Charts.XAxis;
using BlazorApp1.Models.Charts.YAxis;

namespace BlazorApp1.Models.Charts;

public class ChartOptionsModel<TSeries, TXAxis>
{
    [JsonPropertyName("annotations")] public AnnotationsModel Annotations { get; set; } = new();
    [JsonPropertyName("chart")] public ChartModel Chart { get; set; } = new();
    //
    [JsonPropertyName("colors")]
    public List<string> Colors { get; set; } = new() {"var(--mud-palette-primary)", "var(--mud-palette-secondary)"};
    
    [JsonPropertyName("dataLabels")] public DataLabelsModel DataLabels { get; set; } = new();
    [JsonPropertyName("fill")] public FillModel Fill { get; set; } = new();
    
    [JsonPropertyName("forecastDataPoints")]
    public ForecastDataPointsModel ForecastDataPoints { get; set; } = new();

    // [JsonPropertyName("grid")] public GridModel Grid { get; set; } = new(); // TODO: Yaxis issue...
    [JsonPropertyName("labels")] public List<string> Labels { get; set; } = new();
    [JsonPropertyName("legend")] public LegendModel Legend { get; set; } = new();
    [JsonPropertyName("markers")] public LegendModel.MarkersModel Markers { get; set; } = new();
    [JsonPropertyName("noData")] public NoDataModel NoData { get; set; } = new();
    [JsonPropertyName("plotOptions")] public PlotOptionsModel PlotOptions { get; set; } = new();
    [JsonPropertyName("responsive")] public List<ResponsiveModel> Responsive { get; set; } = new();
    [JsonPropertyName("series")] public List<TSeries> Series { get; set; } = new();
    [JsonPropertyName("states")] public StatesModel States { get; set; } = new();
    [JsonPropertyName("stroke")] public StrokeModel Stroke { get; set; } = new();
    [JsonPropertyName("subtitle")] public SubtitleModel Subtitle { get; set; } = new();
    [JsonPropertyName("theme")] public ThemeModel Theme { get; set; } = new();
    [JsonPropertyName("title")] public TitleModel Title { get; set; } = new();
    [JsonPropertyName("tooltip")] public TooltipModel Tooltip { get; set; } = new();
    [JsonPropertyName("xaxis")] public XAxisModel<TXAxis> XAxis { get; set; } = new();
    [JsonPropertyName("yaxis")] public YAxisModel YAxis { get; set; } = new();
}