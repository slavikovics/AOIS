using Microsoft.VisualBasic.CompilerServices;

namespace LogicalMinimization;

public class KarnaughSelection
{
    // Top left corner
    public int TopX { get; private set; }

    public int TopY { get; private set; }
    
    // Bottom right corner
    public int BottomX { get; private set; }

    public int BottomY { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int Square => Width * Height;

    public int TableHeight { get; private set; }

    public int TableWidth { get; private set; }

    public KarnaughSelection(int x, int y, int tableHeight, int tableWidth, int height = 1, int width = 1)
    {
        TableHeight = tableHeight;
        TableWidth = tableWidth;
        Height = height;
        Width = width;
        
        TopX = x;
        if (TopX < 0) TopX += tableWidth;
        else if (TopX >= tableWidth) TopX -= tableWidth;
        
        TopY = y;
        if (TopY < 0) TopY += tableHeight;
        else if (TopY >= tableHeight) TopY -= tableHeight; 
        
        BottomX = x + width - 1;
        if (BottomX < 0) BottomX += tableWidth;
        else if (BottomX >= tableWidth) BottomX -= tableWidth;
        
        BottomY = y + height - 1;
        if (BottomY < 0) BottomY += tableHeight;
        else if (BottomY >= tableHeight) BottomY -= tableHeight;
    }

    public KarnaughSelection? Right()
    {
        var right = new KarnaughSelection(TopX, TopY, TableHeight, TableWidth, Height, Width * 2);
        if (right.BottomX >= TopX && right.BottomX <= BottomX) return null;
        return right;
    }
    
    public KarnaughSelection? Left()
    {
        var left = new KarnaughSelection(TopX - Width, TopY, TableHeight, TableWidth, Height, Width * 2);
        if (left.TopX <= BottomX && left.TopX >= TopX) return null;
        return left;
    }
    
    public KarnaughSelection? Up()
    {
        var up = new KarnaughSelection(TopX, TopY - Height, TableHeight, TableWidth, Height * 2, Width);
        if (up.TopY <= BottomY && up.TopY >= TopY) return null;
        return up;
    }
    
    public KarnaughSelection? Down()
    {
        var down = new KarnaughSelection(TopX, TopY, TableHeight, TableWidth, Height * 2, Width);
        if (down.BottomY >= TopY && down.BottomY <= BottomY) return null;
        return down;
    }

    public bool IsValid(ref bool[,] table, bool clearZone = false, List<MapRectangle>? rectangles = null)
    {
        // TODO fix this method when Bottom is on the left and top is on the right
        // TODO fix check for overlapping (filling the same squares several times)

        if (Height > TableHeight || Width > TableWidth) return false;

        bool xOverlap = BottomX < TopX;
        bool yOverlap = BottomY < TopY;
        bool isValid = true;

        if (xOverlap && !yOverlap)
        {
            isValid = isValid && IsZoneValid(ref table, TopX, TopY, TableWidth - 1, BottomY, clearZone, rectangles);
            isValid = isValid && IsZoneValid(ref table, 0, TopY, BottomX, BottomY, clearZone, rectangles);
            return isValid;
        }

        if (!xOverlap && yOverlap)
        {
            isValid = isValid && IsZoneValid(ref table, TopX, TopY, BottomX, TableHeight - 1, clearZone, rectangles);
            isValid = isValid && IsZoneValid(ref table, TopX, 0, BottomX, BottomY, clearZone, rectangles);
            return isValid;
        }
        
        if (xOverlap && yOverlap)
        {
            isValid = isValid && IsZoneValid(ref table, TopX, TopY, TableWidth - 1, TableHeight - 1, clearZone, rectangles);
            isValid = isValid && IsZoneValid(ref table, 0, 0, BottomX, BottomY, clearZone, rectangles);
            isValid = isValid && IsZoneValid(ref table, TopX, 0, TableWidth - 1, BottomY, clearZone, rectangles);
            isValid = isValid && IsZoneValid(ref table, 0, TopY, BottomX, TableHeight - 1, clearZone, rectangles);
            return isValid;
        }

        isValid = isValid && IsZoneValid(ref table, TopX, TopY, BottomX, BottomY, clearZone, rectangles);
        return isValid;
    }

    public static bool IsZoneValid(ref bool[,] table, int zoneTopX, int zoneTopY, int zoneBottomX, int zoneBottomY, bool clearZone, List<MapRectangle>? rectangles)
    {
        for (int i = zoneTopX; i <= zoneBottomX; i++)
        for (int j = zoneTopY; j <= zoneBottomY; j++)
        {
            if (!table[j, i] && !clearZone) return false;
            table[j, i] = !clearZone;
            if (rectangles is not null) rectangles.Add(new MapRectangle(i, j));
        }
        
        return true;
    }
}