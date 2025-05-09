﻿using LogicalParser;

namespace LogicalMinimization;

public class KarnaughMap
{
    public List<string> ColumnVariables { get; private set; }

    public List<string> ColumnArguments { get; private set; }

    public List<string> RowVariables { get; private set; }

    public List<string> RowArguments { get; private set; }

    public MapValue[,] Table;

    public Dictionary<string, bool>[,] MatchTable { get; private set; }

    public IEvaluatable Formula { get; private set; }

    public KarnaughMap(IEvaluatable formula)
    {
        Formula = formula;
        SetUp(formula.ToString());
    }

    public KarnaughMap(string input)
    {
        Formula = FormulaParser.Parse(input);
        SetUp(input);
    }

    private void SetUp(string input)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(input);
        SplitVariables(variables);
        InitializeTable();
    }

    private void SplitVariables(List<string> variables)
    {
        int row = variables.Count / 2;
        int column = variables.Count - row;

        RowVariables = variables.GetRange(0, row);
        ColumnVariables = variables.GetRange(row, column);
    }

    public List<KarnaughSelection> FindAllSelections()
    {
        List<KarnaughSelection> result = [];

        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                if (Table[j, i].Value && !Table[j, i].WasUsed)
                {
                    KarnaughSelection selection = FindSelection(Table, RowArguments.Count, ColumnArguments.Count, i, j);
                    selection.IsValid(ref Table, true);
                    result.Add(selection);
                }
            }
        }

        return result;
    }

    public List<MapRectangle> FindMapRectanglesForSelection(KarnaughSelection selection)
    {
        List<MapRectangle> mapRectangles = new List<MapRectangle>();
        selection.IsValid(ref Table, false, mapRectangles);

        foreach (var rectangle in mapRectangles) rectangle.Match = MatchTable[rectangle.Y, rectangle.X];
        return mapRectangles;
    }

    public Expression BuildExpressionForSelection(KarnaughSelection selection)
    {
        var mapRectangles = FindMapRectanglesForSelection(selection);
        var referenceMatch = mapRectangles[0].Match;
        var remainingVariables = new List<string>();
        remainingVariables.AddRange(RowVariables);
        remainingVariables.AddRange(ColumnVariables);
        
        List<Dictionary<string, bool>> allMatches = [];
        
        for (int i = 1; i < mapRectangles.Count; i++) allMatches.Add(mapRectangles[i].Match);

        foreach (var match in allMatches)
        {
            List<string> variablesToRemove = [];
            
            foreach (var variable in remainingVariables) 
            {
                if (match[variable] != referenceMatch[variable]) variablesToRemove.Add(variable);
            }
            
            variablesToRemove.ForEach(x => remainingVariables.Remove(x));
        }

        List<Variable> resultVariables = [];
        foreach (var variable in remainingVariables)
        {
            resultVariables.Add(new Variable(variable, referenceMatch[variable]));
        }

        return new Expression(resultVariables);
    }

    public Form MinimizeToDisjunctional()
    {
        var selections = FindAllSelections();
        List<Expression> expressions = [];

        foreach (var selection in selections) expressions.Add(BuildExpressionForSelection(selection));
        return new Form(expressions, FormType.Disjunctive);
    }

    public Form MinimizeToConjunctional()
    {
        InvertTable();
        var selections = FindAllSelections();
        List<Expression> expressions = [];
        
        foreach (var selection in selections) expressions.Add(BuildExpressionForSelection(selection));
        foreach (var expression in expressions)
        {
            foreach (var variable in expression.Variables) variable.IsPositive = !variable.IsPositive;
        }
        
        return new Form(expressions, FormType.Conjunctive);
    }

    private void InvertTable()
    {
        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                if (Table[j, i].Value) Table[j, i].Value = false;
                else Table[j, i].Value = true;
            }
        }
    }

    public static KarnaughSelection FindSelection(MapValue[,] table, int height, int width, int x, int y)
    {
        var initial = new KarnaughSelection(x, y, height, width);
        List<KarnaughSelection> currentGen = [initial];
        List<KarnaughSelection> nextGen = [];

        do
        {
            foreach (var selection in currentGen)
            {
                var nearRight = selection.Right();
                if (nearRight is not null && nearRight.IsValid(ref table)) nextGen.Add(nearRight);

                var nearLeft = selection.Left();
                if (nearLeft is not null && nearLeft.IsValid(ref table)) nextGen.Add(nearLeft);
                
                var nearUp = selection.Up();
                if (nearUp is not null && nearUp.IsValid(ref table)) nextGen.Add(nearUp);
                
                var nearDown = selection.Down();
                if (nearDown is not null && nearDown.IsValid(ref table)) nextGen.Add(nearDown);
            }

            if (nextGen.Count == 0)
            {
                var biggest = currentGen[0];
                int biggestCount = currentGen[0].CountProfit(ref table);

                foreach (var selection in currentGen)
                {
                    int selectionCount = selection.CountProfit(ref table);
                    
                    if (selectionCount > biggestCount)
                    {
                        biggest = selection;
                        biggestCount = selectionCount;
                    }
                }
                    
                return biggest;
            }

            currentGen.Clear();
            currentGen.AddRange(nextGen);
            nextGen.Clear();
            
        } while (true);
    }

    private void InitializeTable()
    {
        ColumnArguments = GenerateGrayCode(ColumnVariables.Count);
        RowArguments = GenerateGrayCode(RowVariables.Count);
        Table = new MapValue[RowArguments.Count, ColumnArguments.Count];
        MatchTable = new Dictionary<string, bool>[RowArguments.Count, ColumnArguments.Count];

        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                var match = MatchVariablesWithArguments(ColumnArguments[i], RowArguments[j]);
                Table[j, i] = new MapValue(Formula.Evaluate(match));
                MatchTable[j, i] = match;
            }
        }
    }

    private Dictionary<string, bool> MatchVariablesWithArguments(string columnArguments, string rowArguments)
    {
        Dictionary<string, bool> match = [];
        
        for (int i = 0; i < ColumnVariables.Count; i++)
        {
            if (columnArguments[i] == '1') match.Add(ColumnVariables[i], true);
            else match.Add(ColumnVariables[i], false);
        }
        
        for (int i = 0; i < RowVariables.Count; i++)
        {
            if (rowArguments[i] == '1') match.Add(RowVariables[i], true);
            else match.Add(RowVariables[i], false);
        }

        return match;
    }
    
    private static List<string> GenerateGrayCode(int n)
    {
        List<string> result = new List<string>();
        
        result.Add("0");
        result.Add("1");

        for (int i = 2; i <= n; i++)
        {
            List<string> reversed = new List<string>(result);
            reversed.Reverse();
            
            for (int j = 0; j < result.Count; j++) result[j] = "0" + result[j];
            for (int j = 0; j < reversed.Count; j++) reversed[j] = "1" + reversed[j];
            
            result.AddRange(reversed);
        }

        return result;
    }

    public override string ToString()
    {
        string resp = "";

        for (int i = 0; i < RowArguments.Count; i++)
        {
            for (int j = 0; j < ColumnArguments.Count; j++)
            {
                if (Table[i, j].Value) resp += "1";
                else resp += "0";
            }
            resp += "\n";
        }

        return resp;
    }
}