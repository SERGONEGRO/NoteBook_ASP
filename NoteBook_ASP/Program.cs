using NoteBook_ASP.Models;
using System.Text.RegularExpressions;

Console.WriteLine("сервер запущен");
// ��������� ������
List<Person> persons = new()
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom1", SurName = "Waits1", LastName = "Ivanich1", PhoneNumber = "111", Address = "adr1", Description = "descr1"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom2", SurName = "Waits2", LastName = "Ivanich2", PhoneNumber = "222", Address = "adr2", Description = "descr2"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom3", SurName = "Waits3", LastName = "Ivanich3", PhoneNumber = "333", Address = "adr3", Description = "descr3"}
};
Console.WriteLine("БД Создана");

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    Console.WriteLine(path);
    Console.WriteLine(request.Method);
    //string expressionForNumber = "^/api/users/([0 - 9]+)$";   // ���� id ������������ �����

    // 2e752824-1657-4c7f-844b-6ec2e168e99c
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

    if (path == "/api/users" && request.Method == "GET")
    {
        Console.WriteLine("Запрос всех");
        await GetAllPeople(response);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        // �������� id �� ������ url
        string? id = path.Value?.Split("/")[3];
        Console.WriteLine("запрошена запись с id = {0}", id);
        await GetPerson(id, response, request);
    }
    else if (path == "/html/person_new.html" && request.Method == "POST")
    {
        Console.WriteLine("запрос CreatePerson");
        await CreatePerson(response, request);
        Console.WriteLine("переход на страницу по умолчанию");
        response.ContentType = "text/html; charset=utf-8";   // error here!
        await response.SendFileAsync("html/index.html");
    }
    else if (path == "/api/users" && request.Method == "PUT")
    {
        Console.WriteLine("запрос UpdatePerson");
        await UpdatePerson(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        Console.WriteLine("запрос DeletePerson");
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response, request);
    }
    //открытие пустой страницы для создания
    else if (path == "/html/person_new.html")
    {
        Console.WriteLine("запрос открытия пустого окна для создания новой записи");
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/person_new.html");
    }
    else
    {
        Console.WriteLine("запрос страницы по умолчанию");
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

// ��������� ���� �������������
async Task GetAllPeople(HttpResponse response)
{
    Console.WriteLine("выполнение GetAllPeople");
    await response.WriteAsJsonAsync(persons);
}
// ��������� ������ ������������ �� id
async Task GetPerson(string? id, HttpResponse response, HttpRequest request)
{
    Console.WriteLine("выполнение GetPerson");
    // �������� ������������ �� id
    Person? person = persons.FirstOrDefault((u) => u.Id == id);
    // ���� ������������ ������, ���������� ���
    if (person != null)
        await response.WriteAsJsonAsync(person);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "������������ �� ������" });
    }
}

async Task DeletePerson(string? id, HttpResponse response, HttpRequest request)
{
    Console.WriteLine("выполнение DeletePerson");
    // �������� ������������ �� id
    Person? person = persons.FirstOrDefault((u) => u.Id == id);
    // ���� ������������ ������, ������� ���
    if (person != null)
    {
        persons.Remove(person);
        await response.WriteAsJsonAsync(person);
    }
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "������������ �� ������" });
    }
}

async Task CreatePerson(HttpResponse response, HttpRequest request)
{
    Console.WriteLine("выполнение CreatePerson");
    try
    {
        // �������� ������ ������������
        var person = await request.ReadFromJsonAsync<Person>();
        if (person != null)
        {
            // ������������� id ��� ������ ������������
            person.Id = Guid.NewGuid().ToString();
            Console.WriteLine(person.Id);
            Console.WriteLine(person.FullName);
            // ��������� ������������ � ������
            persons.Add(person);
            //отсылаем на первоначальную страницу
            await response.SendFileAsync("html/index.html");
            //await response.WriteAsJsonAsync(person);
        }
        else
        {
            throw new Exception("������������ ������");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "������������ ������" });
    }
}

async Task UpdatePerson(HttpResponse response, HttpRequest request)
{
    Console.WriteLine("выполнение UpdatePerson");
    try
    {
        // �������� ������ ������������
        Person? personData = await request.ReadFromJsonAsync<Person>();
        if (personData != null)
        {
            // �������� ������������ �� id
            var person = persons.FirstOrDefault(u => u.Id == personData.Id);
            // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
            if (person != null)
            {
                person.Name = personData.Name;
                person.SurName = personData.SurName;
                person.LastName = personData.LastName;
                person.PhoneNumber = personData.PhoneNumber;
                person.Address = personData.Address;
                person.Description = personData.Description;
                await response.WriteAsJsonAsync(person);
            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "������������ �� ������" });
            }
        }
        else
        {
            throw new Exception("������������ ������");
        }
    }
    catch (Exception)
    {
        response.StatusCode = 400;
        await response.WriteAsJsonAsync(new { message = "������������ ������" });
    }
}
