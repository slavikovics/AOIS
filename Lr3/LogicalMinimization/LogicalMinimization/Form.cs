﻿using LogicalParser;

namespace LogicalMinimization;

public class Form
{
    public List<Expression> Expressions { get; set; }

    public FormType Type { get; }

    public Form(List<Expression> expressions, FormType type)
    {
        Expressions = expressions;
        Type = type;
    }

    public void StickEverything()
    {
        bool changed;
        
        do changed = Stick();
        while (changed);
    }
    
    private Dictionary<string, bool> MergeDictionaries(Dictionary<string, bool> dict1, Dictionary<string, bool> dict2)
    {
        var result = new Dictionary<string, bool>();
        bool truth = Type != FormType.Conjunctive;

        if (truth) foreach (var key in dict1.Keys) result[key] = dict1[key];
        else foreach (var key in dict1.Keys) result[key] = !dict1[key];
        
        if (truth) foreach (var key in dict2.Keys) result.TryAdd(key, dict2[key]);
        else foreach (var key in dict2.Keys) result.TryAdd(key, !dict2[key]);
        
        return result;
    }
    
    public void RemoveUnnecessary()
    {
        if (Expressions.Count <= 1) return;
        List<Expression> toRemove = [];
        
        Dictionary<string, bool> optionFormula = [];
        foreach (var expression in Expressions)
        {
            foreach (var variable in expression.Variables) optionFormula.TryAdd(variable.Name, !variable.IsPositive);
        }

        foreach (var expression in Expressions)
        {
            Dictionary<string, bool> optionsExpression = [];
            foreach (var variable in expression.Variables) optionsExpression.Add(variable.Name, variable.IsPositive);

            Dictionary<string, bool> options = MergeDictionaries(optionsExpression, optionFormula);
                
            List<Expression> remainingExpressions = [];
            foreach (Expression e in Expressions) if (e != expression) remainingExpressions.Add(e);
            
            string form = new Form(remainingExpressions, Type).ToString();
            IEvaluatable formula = FormulaParser.Parse(form.ToString());
            
            bool result = formula.Evaluate(options);
            if ((result && Type == FormType.Disjunctive) || (!result && Type == FormType.Conjunctive)) toRemove.Add(expression);
        }
        
        foreach (var expression in toRemove) Expressions.Remove(expression);
    }

    public bool Stick()
    {
        List<Expression> result = [];
        bool[] wasStuck = new bool[Expressions.Count];
        
        for (int i = 0; i < wasStuck.Length; i++) wasStuck[i] = false;

        for (int i = 0; i < wasStuck.Length; i++)
        {
            if (wasStuck[i]) continue;
            
            for(int j = 0; j < wasStuck.Length; j++)
            {
                try
                {
                    Expression newExp = Expressions[i].StickTogether(Expressions[j]);
                    if (result.Find(x => x.ToString() == newExp.ToString()) is null) result.Add(newExp);
                    wasStuck[i] = true;
                }
                catch (ArgumentException e)
                {
                }
            }
        }

        FindRemainingExpressions(wasStuck, result);
        Expressions = result;

        foreach (var stuck in wasStuck) if (stuck) return true;
        return false;
    }

    private void FindRemainingExpressions(bool[] wasStuck, List<Expression> result)
    {
        for (int i = 0; i < wasStuck.Length; i++)
        {
            if (!wasStuck[i]) result.Add(Expressions[i]);
        }
    }

    public override string ToString()
    {
        if (Expressions.Count == 0) return "";
        
        string result = "";
        string sign = "|";
        if (Type == FormType.Conjunctive) sign = "&";

        for (int i = 0; i < Expressions.Count - 1; i++)
        {
            if (Expressions[i].Variables.Count != 0) result += Expressions[i].ToString(Type) + sign;
        }
        result += Expressions[^1].ToString(Type);

        return result;
    }
}