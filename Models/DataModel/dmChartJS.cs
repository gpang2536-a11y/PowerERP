public class dmChartJS
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string XAxisTitle { get; set; }
    public string YAxisTitle { get; set; }
    public List<string> SerialName { get; set; } = new List<string>();
    public List<string> Labels { get; set; } = new List<string>();
    public List<int[]> DataSets { get; set; } = new List<int[]>();
    public List<string> ForegroundColor { get; set; } = new List<string>();
    public List<string> BackgroundColor { get; set; } = new List<string>();
    public List<int> DataIntValue1 { get; set; } = new List<int>();
    public List<int> DataIntValue2 { get; set; } = new List<int>();
    public List<int> DataIntValue3 { get; set; } = new List<int>();
    public List<int> DataIntValue4 { get; set; } = new List<int>();
    public List<int> DataIntValue5 { get; set; } = new List<int>();
    public List<decimal> DataDecimalValue1 { get; set; } = new List<decimal>();
    public List<decimal> DataDecimalValue2 { get; set; } = new List<decimal>();
    public List<decimal> DataDecimalValue3 { get; set; } = new List<decimal>();
    public List<decimal> DataDecimalValue4 { get; set; } = new List<decimal>();
    public List<decimal> DataDecimalValue5 { get; set; } = new List<decimal>();
}