using Microsoft.VisualBasic.CompilerServices;

namespace LogicalMinimization;

public class CarnoSelection
{
    // Top left corner
    public int TopX { get; private set; }

    public int TopY { get; private set; }
    
    // Bottom right corner
    public int BottomX { get; private set; }

    public int BottomY { get; private set; }

    public int Width => Math.Abs(BottomX - TopX) + 1;

    public int Height => Math.Abs(TopY - BottomY) + 1;

    public int Square => Width * Height;

    public int TableHeight { get; private set; }

    public int TableWidth { get; private set; }

    public CarnoSelection(int x, int y, int tableHeight, int tableWidth, int height = 1, int width = 1)
    {
        TableHeight = tableHeight;
        TableWidth = TableWidth;
        
        TopX = x;
        if (TopX < 0) TopX += tableWidth;
        
        TopY = y;
        if (TopY < 0) TopY += tableHeight;
        
        BottomX = x + width - 1;
        BottomY = y + height - 1;
    }

    public CarnoSelection Right()
    {
        var right = new CarnoSelection(BottomX + 1, TopY, Height, Width);
        if (right.BottomX >= TopX) throw new Exception("Intersection happens");
        return right;
    }
    
    public CarnoSelection Left()
    {
        var left = new CarnoSelection(TopX - Width, TopY, Height, Width);
        if (left.TopX <= BottomX) throw new Exception("Intersection happens");
        return left;
    }
    
    public CarnoSelection Up()
    {
        var up = new CarnoSelection(TopX, TopY - Height, Height, Width);
        if (up.TopY <= BottomY) throw new Exception("Intersection happens");
        return up;
    }
    
    public CarnoSelection Down()
    {
        var down = new CarnoSelection(TopX, BottomY - 1, Height, Width);
        if (down.BottomY >= TopY) throw new Exception("Intersection happens");
        return down;
    }

    public bool IsValid(bool[,] table)
    {
        for (int i = TopX; i <= BottomX; i++)
            for (int j = TopY; j <= BottomY; j++)
                if (!table[j, i]) return false;
        
        return true;
    }
}