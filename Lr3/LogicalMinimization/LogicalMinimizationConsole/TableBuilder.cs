namespace LogicalMinimizationConsole;

public class TableBuilder<T>
{
    private List<string> _rowArguments;
    
    private List<string> _columnArguments;

    private string[,] _content;

    private int _height;
    
    private int _width;
    
    private int[] _sizesHorizontal;
    
    public TableBuilder(List<T> rowArguments, List<T> columnArguments, T[,] content)
    {
        _rowArguments = [];
        _columnArguments = [];

        FillArguments(rowArguments, columnArguments);
        FillContent(content);
        FindSizes();
    }

    private void FillContent(T[,] content)
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

    private void FillArguments(List<T> rowArguments, List<T> columnArguments)
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
            _sizesHorizontal[i] = Math.Max(_columnArguments[i].Length, FindMaxRowSize(i));
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

    public string BuildHeader()
    {
        string result = " " + _rowArguments[0] + "\\" + _columnArguments[0] + " ";
        for (int i = 1; i < _width; i++) result += $" {_columnArguments[i]} ";
        
        return result + "\n";
    }

    public string BuildBody()
    {
        string result = "";
        
        for (int i = 0; i < _height; i++)
        
        for (int j = 1; j < _width; j++)
        {
            
        }
        
        return result;
    }
}