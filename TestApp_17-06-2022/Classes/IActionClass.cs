using System;
using System.Threading.Tasks;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Models;

namespace TestApp_17_06_2022.Classes
{
    public interface IActionClass<T> : IDisposable where T : class
    {
       Task<T> Login(LoginViewModel model);

       Task<T> Signin(LoginViewModel model);
    }
}