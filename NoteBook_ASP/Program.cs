using NoteBook_ASP.Models;
using System.Text.RegularExpressions;

Console.WriteLine("сервер запущен");

/// <summary>
/// Create DB
/// </summary>
List<Person> persons = new()
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom1", SurName = "Waits1", LastName = "Ivanich1", PhoneNumber = "111", Address = "adr1", Description = "descr1"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom2", SurName = "Waits2", LastName = "Ivanich2", PhoneNumber = "222", Address = "adr2", Description = "descr2"},
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom3", SurName = "Waits3", LastName = "Ivanich3", PhoneNumber = "333", Address = "adr3", Description = "descr3"}
};
Console.WriteLine("БД Создана");

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

string? editId = string.Empty;    //переменная для хранения id редактируемой записи


/// <summary>
/// start app
/// </summary>
app.Run(async (context) =>
{
    
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;
    PrintResponseInfo(response, request);
    Console.WriteLine(@"request path = {0}", path.ToString() );
 
    //вычисление id
    string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

    //default main page
    if (path == "/api/users" && request.Method == "GET")
    {
        Console.WriteLine("Запрос всех");
        await GetAllPeople(response, request);
    }
    //если запрос на сохранение id - пока не знаю как по-другому сделать
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "POST")
    {
        editId = path.Value?.Split("/")[3];
        Console.WriteLine("запрошено сохранение id = {0}", editId);
    }
    //если запрос на получения пользователя, отправленного на редактирование
    if (path == "/api/getuser" && request.Method == "GET")
    {
        Console.WriteLine("запрошена запись с id = {0} для редактирования", editId);
        await GetPerson(editId, response, request);
    }

    //Если пришел запрос на сохранение нового пользователя
    else if (path == "/person_new.html" && request.Method == "POST")
    {
        Console.WriteLine("запрос CreatePerson");
        await CreatePerson(response, request);
        Console.WriteLine("переход на страницу по умолчанию");
        response.ContentType = "text/html; charset=utf-8";   // error here!
        await response.SendFileAsync("html/index.html");
    }

    //Если пришел запрос на обновление данных пользователя
    else if (path == "/api/users" && request.Method == "PUT")
    {
        Console.WriteLine("запрос UpdatePerson");
        await UpdatePerson(response, request);
    }

    //Если пришел запрос на удаление пользователя
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        Console.WriteLine("запрос DeletePerson");
        string? id = path.Value?.Split("/")[3];
        await DeletePerson(id, response, request);
    }

    //открытие пустой страницы для создания
    else if (path == "/person_new.html")
    {
        Console.WriteLine("запрос открытия пустого окна для создания новой записи");
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/person_new.html");
    }

    //открытие пустой страницы для редактирования
    else if (path == "/person_edit.html")
    {
        Console.WriteLine("запрос открытия пустого окна для редактирования записи");
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/person_edit.html");
    }

    //В других случаях открывается главная страница
    else
    {
        try
        {
            await PrintResponseInfo(response, request);
            response.ContentType = "text/html; charset=utf-8";
            await PrintResponseInfo(response, request);
            await response.SendFileAsync("html/index.html");

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"Ошибка в ответе №{0}", response.GetHashCode().ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
        
    }
});

app.Run();

/// <summary>
/// Print info
/// </summary>
async Task PrintResponseInfo(HttpResponse response,HttpRequest request)
{
    Console.WriteLine(@"HasStarted={0} " +
                      //$"ContentType={1}\t" +
                      @"StatusCode={1} " +
                      @"HashCode={2} " +
                      @"requestPAth={3} " +
                      @"requestMethod={4}",
                      response.HasStarted.ToString(),
                      //response.ContentType.ToString(),
                      response.StatusCode.ToString(),
                      response.GetHashCode().ToString(),
                      request.Path.ToString(),
                      request.Method.ToString());    
}

/// <summary>
/// get all clients
/// </summary>
async Task GetAllPeople(HttpResponse response,HttpRequest request)
{
    await PrintResponseInfo(response, request);
    await response.WriteAsJsonAsync(persons);
    await PrintResponseInfo(response, request);
    Console.WriteLine("выполнено GetAllPeople");
}

/// <summary>
/// get current client
/// </summary>
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

/// <summary>
/// Delete client
/// </summary>
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

/// <summary>
/// Create client
/// </summary>
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
            Console.WriteLine("Создана запись {0} с id = {1}",person.FullName, person.Id);
            // ��������� ������������ � ������
            persons.Add(person);
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

/// <summary>
/// Update client
/// </summary>
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
