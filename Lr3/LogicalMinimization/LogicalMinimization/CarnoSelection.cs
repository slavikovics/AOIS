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
        // TODO check for repeating
        return new CarnoSelection(BottomX + 1, TopY, Height, Width);
    }
    
    public CarnoSelection Left()
    {
        // TODO check for repeating
        return new CarnoSelection(TopX - Width, TopY, Height, Width);
    }
    
    public CarnoSelection Up()
    {
        // TODO check for repeating
        return new CarnoSelection(TopX, TopY - Height, Height, Width);
    }
    
    public CarnoSelection Down()
    {
        // TODO check for repeating
        return new CarnoSelection(TopX, BottomY - 1, Height, Width);
    }

    public static bool operator ==(CarnoSelection first, CarnoSelection second)
    {
        // TODO replace with table-driven static method
        return first.Height == second.Height && first.Width == second.Width;
    }
    
    public static bool operator !=(CarnoSelection first, CarnoSelection second)
    {
        // TODO replace with table-driven static method
        return !(first.Height == second.Height && first.Width == second.Width);
    }

    public bool IsValid(bool[,] table)
    {
        // TODO check table values
        return true;
    }
}