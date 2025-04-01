﻿using Microsoft.VisualBasic.CompilerServices;

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
        TableWidth = tableWidth;
        
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

    public CarnoSelection? Right()
    {
        var right = new CarnoSelection(TopX, TopY, TableHeight, TableWidth, Height, Width * 2);
        if (right.BottomX >= TopX && right.BottomX <= BottomX) return null;
        return right;
    }
    
    public CarnoSelection? Left()
    {
        var left = new CarnoSelection(TopX - Width, TopY, TableHeight, TableWidth, Height, Width * 2);
        if (left.TopX <= BottomX && left.TopX >= TopX) return null;
        return left;
    }
    
    public CarnoSelection? Up()
    {
        var up = new CarnoSelection(TopX, TopY - Height, TableHeight, TableWidth, Height * 2, Width);
        if (up.TopY <= BottomY && up.TopY >= TopY) return null;
        return up;
    }
    
    public CarnoSelection? Down()
    {
        var down = new CarnoSelection(TopX, TopY, TableHeight, TableWidth, Height * 2, Width);
        if (down.BottomY >= TopY && down.BottomY <= BottomY) return null;
        return down;
    }

    public bool IsValid(bool[,] table)
    {
        // TODO fix this method when Bottom is on the left and top is on the right
        // TODO fix check for overlapping (filling the same squares several times)
        
        for (int i = TopX; i <= BottomX; i++)
            for (int j = TopY; j <= BottomY; j++)
                if (!table[j, i]) return false;
        
        return true;
    }
}