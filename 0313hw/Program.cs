using System.Text.RegularExpressions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.MapGet("/", () => "Hello World!");*/

app.UseStaticFiles();

app.Run(async (context) =>
{
    /*    ќпределить сайт дл€ создани€ приглашений пользователей. 
        Ќа главной странице сайта выводим приветствие в виде отдельно HTML страницы с использованием стилей. 
        Ќа этой странице добавл€ем ссылку на форму приглашени€. Ќа форме приглашени€, пользователь вводит им€, email и номер телефона. 
        ѕосле отправки приглашени€, данные добавл€ем в коллекцию класса и проводим переадресацию на страницу с благодарностью за регистрацию.*/
    var path = context.Request.Path.ToString();
    var response = context.Response;
    var request = context.Request;

    List<Person> people = new List<Person>();

    response.ContentType = "text/html; charset=utf-8";
    switch (path.ToLower())
    {
        case "/":
            {
                response.Redirect("index.html");

                break;
            }
        case "/thanks.html" when (request.Method == HttpMethods.Post):
            {
                string name = request.Form["name"];
                string email = request.Form["email"];
                string phone = request.Form["phone"];

                if (name == String.Empty||email==String.Empty||phone==String.Empty)
                {
                    response.Redirect("form.html");
                }
                else
                {
                    people.Add(new Person(name, email, phone));

                    response.Redirect("thanks.html");
                }

                break;
            }
    }
});

app.Run();
public class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Person(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }
}
