using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PrimeiraApi.Model;
using PrimeiraApi.ModelView;

namespace PrimeiraApi.Controllers;

[ApiController]
[Route("Add/Employee")]
public class EmployeeController : Controller
{
    IEmployeeRepository _employeeRepository = new EmployeeRepository();

    [HttpPost]
    public IActionResult Add([FromForm] EmployeeModelView employeeModelView) 
    {
        Console.WriteLine($"{employeeModelView.photo.FileName}");
        var filePath = Path.Combine("Storage",employeeModelView.photo.FileName);


        using Stream fileStream = new FileStream(filePath,FileMode.Create);

        employeeModelView.photo.CopyTo(fileStream);

        var employee = new Employee(employeeModelView.name,employeeModelView.age,filePath);

        _employeeRepository.Add(employee);

        return Ok();
    }

    [HttpPost]
    [Route("{id}/download")]
    public IActionResult DownloadPhoto(int id) 
    {
        var employee = _employeeRepository.Get(id);

        var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

        return File(dataBytes,"image/jpg");
    }

    [HttpGet]
    public IActionResult Get() 
    {
        var  employee = _employeeRepository.Get();

        return Ok(employee);
    }
}
