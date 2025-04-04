namespace LogicalMinimizationConsole;

public class TableBuilder<T1, T2, T3>
{
    private List<string> _rowArguments;
    
    private List<string> _columnArguments;

    private string[,] _content;

    private int _height;
    
    private int _width;
    
    private int[] _sizesHorizontal;
    
    public TableBuilder(List<T1> rowArguments, List<T2> columnArguments, T3[,] content)
    {
        _rowArguments = [];
        _columnArguments = [];

        FillArguments(rowArguments, columnArguments);
        FillContent(content);
        FindSizes();
    }

    private void FillContent(T3[,] content)
    {
        for (int i = 0; i < _height - 1; i++)
        {
            for (int j = 0; j < _width - 1; j++)
            {
                var piece = content[i, j]?.ToString();
                if (piece != null) _content[i, j] = piece;
                else throw new NullReferenceException();
            }
        }
    }

    private void FillArguments(List<T1> rowArguments, List<T2> columnArguments)
    {
        try
        {
            rowArguments.ForEach(x => _rowArguments.Add(x.ToString()));
            columnArguments.ForEach(x => _columnArguments.Add(x.ToString()));
        }
        catch (Exception e)
        {
            throw new Exception("Failed to build table");
        }
        
        _height = rowArguments.Count;
        _width = columnArguments.Count;
        _sizesHorizontal = new int[columnArguments.Count];
        _content = new string[_height - 1, _width - 1];
    }

    private void FindSizes()
    {
        for (int i = 0; i < _columnArguments.Count; i++)
            _sizesHorizontal[i] = Math.Max(_columnArguments[i].Length, FindMaxRowSize(i)) + 2;
    }

    private int FindMaxRowSize(int i)
    {
        int max = (_rowArguments[0] + "\\" + _columnArguments[0]).Length;
        if (i == 0) foreach (var row in _rowArguments) if (row.Length > max) max = row.Length;

        i -= 1;
        max = _content[i, 0].Length;
        for (int j = 1; j < _width - 1; j++) if (_content[i, j].Length > max) max = _content[i, j].Length;
        return max;
    }

    private string BuildHeader()
    {
        string result = " " + _rowArguments[0] + "\\" + _columnArguments[0] + " ";
        int left = (_sizesHorizontal[0] - result.Length - 2) / 2;
        int right = _sizesHorizontal[0] - left;
        result = $"{new string(' ', left)}{result}{new string(' ', right)}";

        for (int i = 1; i < _width; i++)
        {
            var part = $"{_columnArguments[i]}";
            left = (_sizesHorizontal[i] - part.Length) / 2;
            right = _sizesHorizontal[i] - left;
            result += $"{new string(' ', left)}{part}{new string(' ', right)}";
        }
        
        return result + "\n";
    }

    private string BuildBody()
    {
        string result = "";

        for (int i = 1; i < _height; i++)
        {
            var part = _rowArguments[i];
            int left = (_sizesHorizontal[0] - part.Length - 2) / 2;
            int right = _sizesHorizontal[0] - left;
            result += $"{new string(' ', left)}{part}{new string(' ', right)}";
            
            for (int j = 1; j < _width; j++)
            {
                part = _content[i - 1, j - 1];
                left = (_sizesHorizontal[j] - part.Length - 2) / 2;
                right = _sizesHorizontal[j] - left;
                result += $"{new string(' ', left)}{part}{new string(' ', right)}";
            }

            result += "\n";
        }
        
        return result;
    }

    public string Build()
    {
        return BuildHeader() + BuildBody();
    }
}