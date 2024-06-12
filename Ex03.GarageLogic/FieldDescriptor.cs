using Ex03.GarageLogic;
using System;

public class FieldDescriptor
{
    public string Name { get; set; }
    public Type FieldType { get; set; }
    public Func<string, bool> ValidationFunc { get; set; }
    public Action<Vehicle, object> SetValueAction { get; set; }

    public FieldDescriptor(string name, Type fieldType, Func<string, bool> validationFunc)
    {
        Name = name;
        FieldType = fieldType;
        ValidationFunc = validationFunc;
    }

    public FieldDescriptor(string name, Type fieldType, Func<string, bool> validationFunc, Action<Vehicle, object> setValueAction)
    {
        Name = name;
        FieldType = fieldType;
        ValidationFunc = validationFunc;
        SetValueAction = setValueAction;
    }
}