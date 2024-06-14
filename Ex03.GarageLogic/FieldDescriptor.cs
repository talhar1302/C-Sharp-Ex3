using Ex03.GarageLogic;
using System;

public class FieldDescriptor
{
    public string Name { get; set; }
    public Type FieldType { get; set; }
    public Func<string, bool> ValidationFunc { get; set; }
    public Action<Vehicle, object> SetValueAction { get; set; }

    public FieldDescriptor(string i_Name, Type i_FieldType, Func<string, bool> i_ValidationFunc)
    {
        Name = i_Name;
        FieldType = i_FieldType;
        ValidationFunc = i_ValidationFunc;
    }

    public FieldDescriptor(string i_Name, Type i_FieldType, Func<string, bool> i_ValidationFunc, Action<Vehicle, object> i_SetValueAction)
    {
        Name = i_Name;
        FieldType = i_FieldType;
        ValidationFunc = i_ValidationFunc;
        SetValueAction = i_SetValueAction;
    }
}