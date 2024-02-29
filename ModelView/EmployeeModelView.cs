﻿namespace PrimeiraApi.ModelView;

public class EmployeeModelView
{
    public int id {  get; set; }
    public string name{ get; set; } = string.Empty;
    public int age { get; set; }

    public IFormFile? photo { get;  set; }
}
