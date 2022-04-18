using NoteBook_ASP.Models;
using System.Text.RegularExpressions;

// ��������� ������
List<Person> persons = new()
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom1", SurName = "Waits1", LastName = "Ivanich1", PhoneNumber = "111", Address = "adr1", Description = "descr1"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom2", SurName = "Waits2", LastName = "Ivanich2", PhoneNumber = "222", Address = "adr2", Description = "descr2"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom3", SurName = "Waits3", LastName = "Ivanich3", PhoneNumber = "333", Address = "adr3", Description = "descr3"}
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    //string expressionForNumber = "^/api/users/([0 - 9]+)$";   // ���� id ������������ �����

    // 2e752824-1657-4c7f-844b-6ec2e168e99c
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    if (path == "/api/users" && request.Method == "GET")
    {
        await GetAllPeople(response);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        // �������� id �� ������ url
        string? id = path.Value?.Split("/")[3];
        await GetPerson(id, response, request);
    }
    else if (path == "/api/users" && request.Method == "POST")
    {
        await CreatePerson(response, request);
    }
    else if (path == "/api/users" && request.Method == "PUT")
    {
        await UpdatePerson(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response, request);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

// ��������� ���� �������������
async Task GetAllPeople(HttpResponse response)
{
    await response.WriteAsJsonAsync(persons);
}
// ��������� ������ ������������ �� id
async Task GetPerson(string? id, HttpResponse response, HttpRequest request)
{
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
    try
    {
        // �������� ������ ������������
        var person = await request.ReadFromJsonAsync<Person>();
        if (person != null)
        {
            // ������������� id ��� ������ ������������
            person.Id = Guid.NewGuid().ToString();
            // ��������� ������������ � ������
            persons.Add(person);
            await response.WriteAsJsonAsync(person);
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
