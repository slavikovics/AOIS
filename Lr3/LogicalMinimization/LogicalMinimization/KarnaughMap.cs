﻿using LogicalParser;

namespace LogicalMinimization;

public class KarnaughMap
{
    public List<string> ColumnVariables { get; private set; }

    public List<string> ColumnArguments { get; private set; }

    public List<string> RowVariables { get; private set; }

    public List<string> RowArguments { get; private set; }

    public bool[,] Table { get; private set; }

    public Dictionary<string, bool>[,] MatchTable { get; private set; }

    public IEvaluatable Formula { get; private set; }

    public KarnaughMap(IEvaluatable formula)
    {
        SetUp(formula);
    }

    public KarnaughMap(string input)
    {
        var formula = FormulaParser.Parse(input);
        SetUp(formula);
    }

    private void SetUp(IEvaluatable formula)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        Formula = formula;
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
        bool[,] table = (bool[,]) Table.Clone();
        List<KarnaughSelection> result = [];

        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                if (table[j, i])
                {
                    KarnaughSelection selection = FindSelection(Table, RowArguments.Count, ColumnArguments.Count, i, j);
                    selection.IsValid(ref table);
                    result.Add(selection);
                }
            }
        }

        var combos = GetAllCombinations(result);
        combos = combos.OrderBy(combo => combo.Count).ToList();

        foreach (var combo in combos)
        {
            bool[,] check = (bool[,]) Table.Clone();
            foreach (var selection in combo) selection.IsValid(ref check, true);
            
            if (IsTableFullFalse(check)) return combo;
        }

        return result;
    }

    public List<MapRectangle> FindMapRectanglesForSelection(KarnaughSelection selection)
    {
        bool[,] table = (bool[,]) Table.Clone();
        List<MapRectangle> mapRectangles = new List<MapRectangle>();
        selection.IsValid(ref table, false, mapRectangles);

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
        for (int j = 0; j < RowArguments.Count; j++)
            if (Table[j, i]) Table[j, i] = false;
            else Table[j, i] = true;
    }

    public bool IsTableFullFalse(bool[,] table)
    {
        for (int i = 0; i < ColumnArguments.Count; i++)
            for (int j = 0; j < RowArguments.Count; j++)
                if (table[j, i]) return false;
        
        return true;
    }
    
    public static List<List<KarnaughSelection>> GetAllCombinations(List<KarnaughSelection> inputList)
    {
        var result = new List<List<KarnaughSelection>>();
        for (int size = 1; size <= inputList.Count; size++)
        {
            result.AddRange(GetCombinations(inputList, size));
        }

        return result;
    }

    public static IEnumerable<List<KarnaughSelection>> GetCombinations(List<KarnaughSelection> list, int length)
    {
        if (length == 0)
            return new List<List<KarnaughSelection>> { new List<KarnaughSelection>() };

        if (!list.Any())
            return new List<List<KarnaughSelection>>();

        var firstElement = list[0];
        var rest = list.Skip(1).ToList();

        var combinationsWithoutFirst = GetCombinations(rest, length);
        var combinationsWithFirst = GetCombinations(rest, length - 1)
            .Select(combo => new List<KarnaughSelection>(combo) { firstElement });

        return combinationsWithFirst.Concat(combinationsWithoutFirst);
    }

    public static KarnaughSelection FindSelection(bool[,] table, int height, int width, int x, int y)
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
                foreach (var selection in currentGen) 
                    if (selection.Square > biggest.Square) biggest = selection;
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
        Table = new bool[RowArguments.Count, ColumnArguments.Count];
        MatchTable = new Dictionary<string, bool>[RowArguments.Count, ColumnArguments.Count];

        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                var match = MatchVariablesWithArguments(ColumnArguments[i], RowArguments[j]);
                Table[j, i] = Formula.Evaluate(match);
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
                if (Table[i, j]) resp += "1";
                else resp += "0";
            }
            resp += "\n";
        }

        return resp;
    }
}